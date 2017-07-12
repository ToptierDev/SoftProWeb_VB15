
var ApiUrl_UnitDefineProject = rootHost + "/webapi/UnitDefineProject/";
var ApiUrl_AutoComplete = rootHost + "/webapi/AutoComplete/";


var app = angular.module('myApp', ['ngTable', 'ngMaterial', 'ngMessages', 'material.svgAssetsCache']);
app.config(function ($logProvider) {
    $logProvider.debugEnabled(true);
});
angular.module('myApp').config(function ($mdDateLocaleProvider) {
    var bYears = 0;
    if (culture == 'th-TH') {
        bYears = 543;
        var shortMonths = ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'];
        $mdDateLocaleProvider.months = ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'];
        $mdDateLocaleProvider.shortMonths = shortMonths;
        $mdDateLocaleProvider.days = ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'];
        $mdDateLocaleProvider.shortDays = ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'];

        $mdDateLocaleProvider.monthHeaderFormatter = function (date) {
            return shortMonths[date.getMonth()] + ' ' + (date.getFullYear() + bYears);
        };
    }
    $mdDateLocaleProvider.formatDate = function (date) {
        if (date == null || date == '') {
            return '';
        } else {

            //console.log(`${moment(date).format('DD/MM')}/${moment(date).get('year') + bYears}`);
            //return `${moment(date).format('DD/MM')}/${moment(date).get('year') + bYears}`;
            return moment(date).format('DD/MM') + '/' + (moment(date).get('year') + bYears);
        }
    };
    $mdDateLocaleProvider.parseDate = function (dateString) {
        var dateArray = dateString.split("/");
        dateString = dateArray[1] + "/" + dateArray[0] + "/" + (dateArray[2] - bYears);
        var m = moment(dateString, 'L', true);
        return m.isValid() ? m.toDate() : new Date(NaN);
    };

    //$mdDateLocaleProvider.formatDate = function (date) {
    //    if (date == null || date == '') {
    //        return '';
    //    } else {
    //        var bYears = 0;
    //        if (culture == 'th-TH') bYears = 543;
    //        date.setFullYear(date.getFullYear() + bYears);
    //        return moment(date).format('DD/MM/YYYY');
    //    }
    //};
});
app.filter('cDate', function ($filter) {
    return function (input) {
        if (input == null) { return ""; }
        var bYears = 0;
        if (culture == 'th-TH') bYears = 543;
        var d = new Date(input);
        d.setFullYear(d.getFullYear() + bYears);
        //Please write the formula for getting the buddhist date here
        //Below is a rough conversion of adding 365*bYears days to todays date

        // Convert 'days' to milliseconds
        // var millies = 1000 * 60 * 60 * 24 * 365 * bYears;

        // console.log(d);
        var _date = $filter('date')(new Date(d), 'dd/MM/yyyy');

        return _date.toUpperCase();

    };
});

//*********************
//$Scope Function
//**********************
var mainController = app.controller('mainController', function ($scope, $filter, $rootScope, NgTableParams, $log, $compile, $animate, $timeout,$http) {
    $rootScope.pageState = "LIST";
    $log.info($rootScope.pageState);
    $scope.mainData = {};
    $scope.mainData.List_vwED03UNIT = [];
    $scope.mainData.List_vwED03UNIT_EDITINGROW = [];
    $scope.txtSearchProject;
    $scope.txtStart;
    $scope.txtCount;
    $scope.txtFPDCode;
    $scope.isUnitChange = false;
    $log.info('loginfo');
    $log.debug('logdebug');
    //project dropdown
    $scope.ED01PROJ = [];
    setED01PROJ();

    setED03UNITSTATUS();
    setED03UNITTYPE();
    
    //#################### Loaddata
    $scope.getData = function () {
        if (isEmpty($scope.txtSearchProject)) {
            $('#resPleaseSelect').show();
            //OpenDialogError('กรุณาเลือก โครงการ');
            return;
        }
        $('#resPleaseSelect').hide();
        showOverlay();
        url = ApiUrl_UnitDefineProject + '?id=' + escape(undefinedToEmpty($scope.txtSearchProject));
        url += '&phase=' + escape(undefinedToEmpty($scope.txtSearchPhase));
        url += '&zone=' + escape(undefinedToEmpty($scope.txtSearchZone));
        url += '&unitfrom=' + escape(undefinedToEmpty($scope.txtSearchUnitFrom));
        url += '&unitto=' + escape(undefinedToEmpty($scope.txtSearchUnitTo));
        $.ajax({
            type: 'GET',
            //url: ApiUrl_UnitDefineProject + '?id=' + $scope.txtSearchProject + '&culture=' + culture,//'A115070001',
            url:url,
            // headers: getAuthenticateHeaders(),
            data: '',
        }).done(function (data) {
            $scope.mainData = data;
            $log.info(data);
            $rootScope.pageState = "LIST";
            $scope.$apply();
        }).fail(
                  function (jqXHR, textStatus, errorThrown) {
                      OpenDialogError(jqXHR.status + ':' + jqXHR.errorThrown);
                      console.log(jqXHR);
        }).complete(function () {
            closeOverlay();
        });
    };

   

    //Refresh FSERNO
    $scope.generateFSERNO = function () {
        if (isEmpty($scope.txtSearchProject) ) {
            OpenDialogError('กรุณาเลือกโครงการ');
            return;
        }
        console.log($scope.mainData.List_vwED03UNIT.FREPRJNO);
        if (!($scope.txtSearchProject === "")) {
            if (!($scope.txtStartFSERNO === "")) {
                var pStart = $scope.txtStartFSERNO;
                var countFSERNO = 0;
                for (i = 0 ; i < $scope.mainData.List_vwED03UNIT.length; i++) {
                    $scope.mainData.List_vwED03UNIT[i].FADDRNO = "";
                    if (!(($scope.mainData.List_vwED03UNIT[i].FAREA === 0 ||
                        $scope.mainData.List_vwED03UNIT[i].FAREA === "" ||
                        $scope.mainData.List_vwED03UNIT[i].FAREA === null) ||
                        ($scope.mainData.List_vwED03UNIT[i].FPDCODE === "" ||
                        $scope.mainData.List_vwED03UNIT[i].FPDCODE === null))) {
                        if (countFSERNO === 0) {
                            $scope.mainData.List_vwED03UNIT[i].FADDRNO = pStart;
                        } else {
                            $scope.mainData.List_vwED03UNIT[i].FADDRNO = pStart + '/' + countFSERNO;
                        }
                        countFSERNO += 1;
                    }
                }
                updateFSERNO();
            } else {
                OpenDialogError('กรุณาใส่เลขที่บ้านเริ่มต้น');
            }
        }
    }

    //Add Row By Start,Count 
    $scope.generateRows = function () {
        if (isEmpty($scope.txtSearchProject)) {
            OpenDialogError('กรุณาเลือกโครงการ');
            return;
        }
        console.log($scope.mainData.List_vwED03UNIT.FREPRJNO);
        if (!isEmpty($scope.txtSearchProject)) {
            if (!(isEmpty($scope.txtStart)  || isEmpty($scope.txtCount)) ) {
                var pStart = $scope.txtStart;
                var pCount = parseInt($scope.txtCount);
                var pIntStart = "";
                var pStrStart = "";
              
                $scope.mainData.List_vwED03UNIT_EDITINGROW.pop();
                for (var i = 0, len = pStart.length; i < len; i++) {
                    try {
                        if (parseInt(pStart[i])) {
                            pIntStart = $scope.txtStart.substring(i);
                            break;
                        } else {
                            pStrStart += pStart[i];
                        }
                    } catch (ex) {

                    }

                }
                for (i = 0 ; i < pCount; i++) {
                    newRow = {};
                    newRow.FREPRJNO = $scope.txtSearchProject;
                    newRow.FSERIALNO = pStrStart + pIntStart;
                    if (!($scope.txtFPDCode === "")){
                        newRow.FPDCODE = $scope.txtFPDCode;
                        newRow.FPDNAME = $scope.txtFPDName;
                        newRow.FDESC = $scope.SD05PDDS.FDESC;
                        newRow.FAREA = $scope.SD05PDDS.FAREA;
                        newRow.FBUILTIN = $scope.SD05PDDS.FBUILTIN;
                    }
                 


                    pIntStart = (parseInt(pIntStart) + 1);
                  
                    $scope.mainData.List_vwED03UNIT_EDITINGROW.push(newRow);
                }

                $scope.addNewRow();

            } else {
                OpenDialogError('ใส่ให้ครบ');
            }
        }else {
            OpenDialogError('กรุณาใส่โครงการ');
        }
    }
    //Delete Data
    $scope.delete = function (index) {
        console.log('delete');
       

     
        if ($rootScope.pageState == 'NEW') {
            $scope.mainData.List_vwED03UNIT_EDITINGROW.splice(index, 1);
            return;
        }
        console.log($rootScope.pageState);
        //mapping view to table and remove unnecesary data

        var pId = $scope.mainData.List_vwED03UNIT_EDITINGROW[index].FSERIALNO;
        var pId2 = $scope.mainData.List_vwED03UNIT_EDITINGROW[index].FREPRJNO;
        if (!confirm("Want to delete UNIT# " + pId + "?")) {
            return false;
        }
        console.log("Parameter Delete : " + pId, pId2);
        if (!(pId === "" && pId2 === "")) {
            deleteData(pId, pId2, index);
        } else {
            $scope.mainData.List_vwED03UNIT_EDITINGROW.splice(index, 1);
            //$scope.$apply();
        }
        
    };
    $scope.deleteAll = function () {
        console.log('deleteAll');
        console.log($rootScope.pageState);
        //$scope.mainData.List_vwFD11PROP_EDITINGROW = [];
        listid = [];
        listindex = [];
        projectId = $scope.mainData.List_vwED03UNIT[0].FREPRJNO;
        for (i = 0; i < $scope.mainData.List_vwED03UNIT.length; i++) {
            if ($scope.mainData.List_vwED03UNIT[i].isRowChecked) {
                listid.push($scope.mainData.List_vwED03UNIT[i].FSERIALNO);
                listindex.push(i);
               
            }
        }
        if (listindex.length > 0) {
            if (!confirm("Want to delete UNIT# " + listid.join(',') + "?")) {
                return false;
            }
            deleteAllData(listid, projectId, listindex);
        } else {

            OpenDialogError('กรุณาเลือกข้อมูลที่ต้องลบ');
        }

    };

    //Add Row 1 
    $scope.addNewRow = function () {
            newRow = {};
            newRow.FREPRJNO = $scope.txtSearchProject;
            newRow.FSERIALNO = "";
            newRow.rowState = 'NEW';
            //$scope.UnitChage(index);
            console.log($scope.arrUpdateQueue);
            $scope.mainData.List_vwED03UNIT_EDITINGROW.push(newRow);
      
    }
    //Remove Row 1 
    $scope.removeRow = function (index) {
        $scope.mainData.List_vwED03UNIT_EDITINGROW.splice(index, 1)
    }
    $scope.addNewRowSpecial = function () {
       
            if ($scope.mainData.List_vwED03UNIT_EDITINGROW.length > 1) {
                var pObject = $scope.mainData.List_vwED03UNIT_EDITINGROW[$scope.mainData.List_vwED03UNIT_EDITINGROW.length - 2];

                console.log(pObject);
                if (!isEmpty(pObject.FSERIALNO)) {
                    $scope.addNewRow();
                } else {
                    OpenDialogError('กรุณาใส่ UNIT');
                }
            } else {
                $scope.addNewRow();

            }
    }

    //###################Check Dup Unit
    $scope.chkDup = function (index, event) {
       
            var pObject = $scope.mainData.List_vwED03UNIT_EDITINGROW[index];
            var pFREPRJNO = pObject.FREPRJNO;
            var pFSERIALNO = pObject.FSERIALNO;
            console.log('pFREPRJNO:' + pFREPRJNO);
            console.log('pFSERIALNO:' + pFSERIALNO);
            
            
            if (!(isEmpty(pFREPRJNO) || isEmpty(pFSERIALNO))) {
                url = ApiUrl_AutoComplete + '?dataSource=ED03UNIT_Master&query=' + pFREPRJNO + "_" + pFSERIALNO;

                $http(
                {
                    method: 'GET',
                    url: url,
                }).then(function successCallback(response) {
                    if (response.data.ErrorView.IsError) {
                        OpenDialogError(response.data.ErrorView.Message);
                        $animate.removeClass($('#row_' + index), 'danger');
                        pObject.FSERIALNO = pObject.e_FSERIALNO;
                    } else {
                            console.log('chkDup success');
                    }
                }, function errorCallback(response) {
                    console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
                });
            } else {
                OpenDialogError('กรุณาใส่ UNIT');
            }
        
    }

    //###################Check Dup Typehourse
    $scope.setDescTypeHouse = function (index, event) {

        console.log('setDescTypeHouse');
        var pObject = $scope.mainData.List_vwED03UNIT_EDITINGROW[index];
        var pFPDCODE = pObject.FPDCODE;
        if (!isEmpty(pFPDCODE)) {
            url = ApiUrl_AutoComplete + '?dataSource=SD05PDDS_Master&query=' + pFPDCODE;

            $http(
            {
                method: 'GET',
                url: url,
            }).then(function successCallback(response) {
                if (response.data.ErrorView.IsError) {
                    OpenDialogError(response.data.ErrorView.Message);
                    pObject.FPDCODE = pObject.FPDCODE_OLD;
                    pObject.FPDNAME = pObject.FPDNAME_OLD;
                    pObject.FDESC = pObject.FDESC_OLD;
                    pObject.FAREA = pObject.FAREA_OLD;
                    pObject.FBUILTIN = pObject.FBUILTIN_OLD;
                } else {
                    pObject.FPDCODE = response.data.Datas.Data1.FPDCODE;
                    pObject.FPDNAME = response.data.Datas.Data1.FPDNAME;
                    pObject.FDESC = response.data.Datas.Data1.FDESC;
                    pObject.FAREA = response.data.Datas.Data1.FAREA;
                    pObject.FBUILTIN = response.data.Datas.Data1.FBUILTIN;
                   
                }
            }, function errorCallback(response) {
                OpenDialogError(response.status + ':' + response.statusText);
                console.log('Error: Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
            });
        } 

    }
    $scope.checkFPDCODE = function () {
        if (!isEmpty($scope.txtFPDCode)) {
            url = ApiUrl_AutoComplete + '?dataSource=SD05PDDS_Master&query=' + $scope.txtFPDCode;

            $http(
            {
                method: 'GET',
                url: url,
            }).then(function successCallback(response) {
                if (response.data.ErrorView.IsError) {
                    OpenDialogError(response.data.ErrorView.Message);
                    $scope.txtFPDCode = '';
                    $scope.txtFPDName = '';
                    $scope.SD05PDDS = {};
                } else {
                    $scope.txtFPDCode = response.data.Datas.Data1.FPDCODE;
                    $scope.txtFPDName = response.data.Datas.Data1.FPDNAME;
                    $scope.SD05PDDS = response.data.Datas.Data1;
                    



                    $scope.generateRows();
                }
            }, function errorCallback(response) {
                OpenDialogError(response.status + ':' + response.statusText);
                console.log('Error: Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
            });
        }
    }
   
    $scope.arrUpdateQueue = [];
 
    $scope.change = function (index) {
        $animate.addClass($('#row_' + index), 'danger');
        if ($scope.arrUpdateQueue.indexOf(index) === -1) {
            $scope.arrUpdateQueue.push(index);
        }
    }

   

    $scope.bindProject = function () {

        //fking ie
        //$scope.mainData.ED01PROJ = $scope.ED01PROJ.find(x => x.FREPRJNO === $scope.txtSearchProject);
        //$scope.mainData.ED01PROJ.FTOTAREA = $scope.mainData.ED01PROJ.FTOTALAREA;
        //$scope.mainData.ED01PROJ.FRELOCAT1 = $scope.mainData.ED01PROJ.FRELOCATE1;
        //$scope.mainData.ED01PROJ.FRELOCAT2 = $scope.mainData.ED01PROJ.FRELOCATE2;
        //$scope.mainData.ED01PROJ.FRELOCAT3 = $scope.mainData.ED01PROJ.FRELOCATE3;
        //console.log($scope.mainData.ED01PROJ);
        setED02PHASE();
    };
    //*********************
    //Function
    //**********************
    
    //############################ Dropdown Project
    function setED01PROJ() {
        url = ApiUrl_AutoComplete + '?dataSource=ED01PROJ&query=' + currentuser;
        $http(
        {
            method: 'GET',
            url: url,
        }).then(function successCallback(response) {
            $scope.ED01PROJ = response.data;
            $timeout(function () {
                $('#selProject').trigger("chosen:updated");

            }, 2000)
        }, function errorCallback(response) {
            console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
        });
    }

    //############################ Dropdown Status
    function setED03UNITSTATUS() {
        url = ApiUrl_AutoComplete + '?dataSource=ED03UNITSTATUS&query=';
        $http(
        {
            method: 'GET',
            url: url,
        }).then(function successCallback(response) {
            $scope.ED03UNITSTATUS = response.data;
            $timeout(function () {
                $('.status-select').chosen();
                $('.status-select').trigger("chosen:updated");
            }, 1000)
        }, function errorCallback(response) {
            console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
        });
    }

    //############################ Dropdown Type House
    function setED02PHASE() {
        var pFREPRJNO = $scope.txtSearchProject;
        
        url = ApiUrl_AutoComplete + '?dataSource=ED02PHAS&query=' + pFREPRJNO;
        $http(
        {
            method: 'GET',
            url: url,
        }).then(function successCallback(response) {
            $scope.ED02PHAS = response.data.Datas.Data1;
            $timeout(function () {
                //chosen - select
                //$('.status-select').chosen();
                $('.chosen-select').trigger("chosen:updated");
            }, 1000)
        }, function errorCallback(response) {
            console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
        });
       
    }

    //############################ Dropdown Phase
    function setED03UNITTYPE() {
        url = ApiUrl_AutoComplete + '?dataSource=ED03UNITTYPE&query=';
        $http(
        {
            method: 'GET',
            url: url,
        }).then(function successCallback(response) {
            $scope.ED03UNITTYPE = response.data.Datas.Data1;
            $timeout(function () {
                $('.status-select').chosen();
                $('.status-select').trigger("chosen:updated");
            }, 1000)
        }, function errorCallback(response) {
            console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
        });
    }
    //Save

    $scope.save = function () {
        if ($rootScope.pageState == 'EDIT') {
            updateData();
        } else if ($rootScope.pageState == 'NEW') {
            insertData();
        }
    };
 
    //############################Insert
    function insertData() {
      
        console.log('inserting');
        newRows = JSON.parse(JSON.stringify($scope.mainData.List_vwED03UNIT_EDITINGROW));
        newRows.pop();
        saveData = {};
        saveData.List_vwED03UNIT = [];
        saveData.List_ED03UNIT = [];
        for (i = 0; i < newRows.length; i++) {
            newRows[i].FHBKDATE = cJsonDate(newRows[i].FHBKDATE);
            saveData.List_ED03UNIT.push(newRows[i]);
        }
        showOverlay();
        $.ajax({
            type: 'POST',
            url: ApiUrl_UnitDefineProject,//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: saveData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data);
            } else {
                newRecords = data.Datas.Data1;
                $scope.mainData.List_vwED03UNIT_EDITINGROW = [];
                arrEditedIndex = [];
                newIndex = $scope.mainData.List_vwED03UNIT_EDITINGROW.length;
                for (i = 0; i < newRecords.length; i++) {
                    //newRecords[i].FAREA = newRecords[i].e_FSTDAREA;
                    //newRecords[i].FBUILTIN = newRecords[i].e_FSTDBUILT;
                    newRecords[i].rowState = 'EDIT';
                    arrEditedIndex.push(newIndex++);
                    //$scope.mainData.List_vwED03UNIT.push(newRecords[i]);
                    $scope.mainData.List_vwED03UNIT_EDITINGROW.push(newRecords[i]);
                    

                }
                $scope.$apply();
                blinkEditedRow(arrEditedIndex);
                $scope.txtStart = '';
                $scope.txtCount = '';
                $scope.txtFPDCode = '';
                $rootScope.pageState = "EDIT";
            }
        }).fail(
           function (jqXHR, textStatus, errorThrown) {
               OpenDialogError(jqXHR.status + ':' + jqXHR.errorThrown);
               console.log(jqXHR);
           }).complete(
           function (data) {
               closeOverlay();
           });
    };

    //############################Update
    function updateData() {

        if (isEmpty($scope.arrUpdateQueue) || $scope.arrUpdateQueue.length == 0) {
            OpenDialogError('ไม่มีการแก้ไขข้อมูล')
            return;
        }
        saveData = {};
        saveData.List_vwED03UNIT = [];
        for (i = 0; i < $scope.arrUpdateQueue.length; i++) {
            saveData.List_vwED03UNIT.push($scope.mainData.List_vwED03UNIT_EDITINGROW[i]);
        }
        showOverlay();
        $.ajax({
            type: 'PUT',
            url: ApiUrl_UnitDefineProject,//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: saveData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data);
            } else {
                console.log('update at row:' + $scope.arrUpdateQueue.join(', '));
                for (i = 0; i < $scope.arrUpdateQueue.length; i++) {
                    $scope.mainData.List_vwED03UNIT_EDITINGROW[i].FPDCODE_OLD = $scope.mainData.List_vwED03UNIT_EDITINGROW[i].FPDCODE;
                    $scope.mainData.List_vwED03UNIT_EDITINGROW[i].FPDNAME_OLD = $scope.mainData.List_vwED03UNIT_EDITINGROW[i].FPDNAME;
                    $scope.mainData.List_vwED03UNIT_EDITINGROW[i].FDESC_OLD = $scope.mainData.List_vwED03UNIT_EDITINGROW[i].FDESC;
                    $scope.mainData.List_vwED03UNIT_EDITINGROW[i].FAREA_OLD = $scope.mainData.List_vwED03UNIT_EDITINGROW[i].FAREA;
                    $scope.mainData.List_vwED03UNIT_EDITINGROW[i].FBUILTIN_OLD = $scope.mainData.List_vwED03UNIT_EDITINGROW[i].FBUILTIN;

                }
                blinkEditedRow($scope.arrUpdateQueue);
                SaveSuccess();
            }
        }).fail(
          function (jqXHR, textStatus, errorThrown) {
              OpenDialogError(jqXHR.status + ':' + jqXHR.errorThrown);
              console.log(jqXHR);
          }).complete(
          function (data) {
              closeOverlay();
          });
    }

    function updateFSERNO() {
        saveData = {};
        saveData.List_vwED03UNIT = $scope.mainData.List_vwED03UNIT;

        showOverlay();
        $.ajax({
            type: 'PUT',
            url: ApiUrl_UnitDefineProject,//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: saveData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data);
            } else {
                $scope.txtStartFSERNO = "";
                CloseDivFSERNO();
                OpenDialogSuccess('ออกเลขที่บ้านสำเร็จ');
            }
        }).fail(
          function (jqXHR, textStatus, errorThrown) {
              OpenDialogError(jqXHR.status + ':' + jqXHR.errorThrown);
              console.log(jqXHR);
          }).complete(
          function (data) {
          }).complete(
        function() 
        {
            closeOverlay();
        });
    }

    //############################Delete
    function deleteData(id, id2, index) {
        $.ajax({
            type: 'DELETE',
            url: ApiUrl_UnitDefineProject + '?id=' + escape(id) + "&pProject=" + escape(id2),//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: $scope.mainData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data);
            } else {
                $scope.mainData.List_vwED03UNIT_EDITINGROW.splice(index, 1);
                $scope.$apply();
                OpenDialogSuccess('ลบ ' + data.Datas.Data1.id + ' สำเร๊จ');
            }
        }).fail(
           function (jqXHR, textStatus, errorThrown) {
               OpenDialogError(jqXHR.status + ':' + jqXHR.errorThrown);
               console.log(jqXHR);
           }).complete(
           function (data) {
           });
    };
    function deleteAllData(listid,projectId, listindex) {
        id = listid.join("_");
        $.ajax({
            type: 'DELETE',
            url: ApiUrl_UnitDefineProject + '?id=' + escape(id) + "&pProject=" + escape(projectId),//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: $scope.mainData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data);
            } else {
                for (i = listindex.length - 1; i >= 0; i--) {
                    $scope.mainData.List_vwED03UNIT.splice(listindex[i], 1);
                }
                $scope.$apply();
                OpenDialogSuccess('ลบ ' + data.Datas.Data1.id + ' สำเร็จ');
            }
        }).fail(
           function (jqXHR, textStatus, errorThrown) {
               OpenDialogError(jqXHR.status + ':' + jqXHR.errorThrown);
               console.log(jqXHR);
           }).complete(
           function (data) {
           });
    };
    
    //**********************
    //end inline edit
    //**********************

    //############################ btn Add
    $scope.showAddPage = function () {
        if (isEmpty($scope.txtSearchProject)) {
            OpenDialogError('กรุณาเลือกโครงการ');
        } else {
            $scope.mainData.List_vwED03UNIT_EDITINGROW = [];
            $scope.addNewRow();
            $scope.isUnitChange = true;
            $rootScope.pageState = 'NEW'
        }
    }

    $scope.closePageEditAdd = function () {
        $rootScope.pageState = 'LIST';
        $scope.checkAll = false;
        $scope.getData();
    }


    //############### btn Edit
    $scope.showEditPage = function () {
        $scope.mainData.List_vwED03UNIT_EDITINGROW = [];

        for (i = 0; i < $scope.mainData.List_vwED03UNIT.length; i++) {
            if ($scope.mainData.List_vwED03UNIT[i].isRowChecked) {
                $scope.mainData.List_vwED03UNIT_EDITINGROW.push($scope.mainData.List_vwED03UNIT[i]);

            }
        }
        //$scope.addNewRow();
        if ($scope.mainData.List_vwED03UNIT_EDITINGROW.length > 0) {
            $rootScope.pageState = 'EDIT';
        } else {
            OpenDialogError('กรุณาเลือกข้อมูลที่ต้องการแก้ไข');
        }

    }
    $scope.List_vwED03UNIT_EDITINGROW_checkAllRow = function () {
        $scope.mainData.List_vwED03UNIT_EDITINGROW = [];
        for (i = 0; i < $scope.mainData.List_vwED03UNIT.length; i++) {
            $scope.mainData.List_vwED03UNIT[i].isRowChecked = $scope.checkAll;
        }
    }


    function blinkEditedRow(arrIndex) {
        var tempTr = [];
        for (i = 0; i < arrIndex.length; i++) {
            console.log('blinkEditedRow #row_' + arrIndex[i]);
            trUpdated = $('#row_' + arrIndex[i]);
            tempTr.push(trUpdated);
        }
        tempTr.forEach(function (entry) {
            entry.removeClass('danger');
            entry.addClass('blink-saved');
            $timeout(function () {
                entry.removeClass('blink-saved');
            }, 1000);
        });
        $scope.arrUpdateQueue = [];
    }

    //utility
    function cJsonDate(date) {
        if (date == null || date == '') {
            return date;
        } else {
            return (new Date(date)).toJSON();
        }
    }
    $scope.isEmpty = function (obj) {
        return isEmpty(obj);
    }
    function isEmpty(obj) {
        return undefinedToEmpty(obj) == '';
    }
    function undefinedToEmpty(obj) {
        if (obj == undefined || obj == null) {
            return '';
        } else {
            return obj;
        }
    }


});






var scopeForDebug = {}
$(function () {
    scopeForDebug = angular.element('[ng-controller]').scope();

})
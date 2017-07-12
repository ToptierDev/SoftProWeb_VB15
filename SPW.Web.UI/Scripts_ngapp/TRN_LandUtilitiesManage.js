
var ApiUrl_LandUtilitiesManage = rootHost + "/webapi/LandUtilitiesManage/";
var ApiUrl_AutoComplete = rootHost + "/webapi/AutoComplete/";


var app = angular.module('myApp', ['ngTable', 'ngMaterial', 'ngMessages', 'material.svgAssetsCache', 'dynamicNumber']);
app.config(['dynamicNumberStrategyProvider', function (dynamicNumberStrategyProvider) {
    dynamicNumberStrategyProvider.addStrategy('price', {
        numInt: 15,
        numFract: 2,
        numSep: '.',
        numPos: true,
        numNeg: true,
        numRound: 'round',
        numThousand: true,
        numThousandSep:''
    });
}]);

//angular.module('myApp').config(function ($mdDateLocaleProvider) {
//    var bYears = 0;
//    if (culture == 'th-TH') {
//        bYears = 543;
//        var shortMonths = ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'];
//        $mdDateLocaleProvider.months = ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'];
//        $mdDateLocaleProvider.shortMonths = shortMonths;
//        $mdDateLocaleProvider.days = ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'];
//        $mdDateLocaleProvider.shortDays = ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'];

//        $mdDateLocaleProvider.monthHeaderFormatter = function (date) {
//            return shortMonths[date.getMonth()] + ' ' + (date.getFullYear() + bYears);
//        };
//    }
//    $mdDateLocaleProvider.formatDate = function (date) {
//        if (date == null || date == '') {
//            return '';
//        } else {

//            //console.log(`${moment(date).format('DD/MM')}/${moment(date).get('year') + bYears}`);
//            //return `${moment(date).format('DD/MM')}/${moment(date).get('year') + bYears}`;
//            return moment(date).format('DD/MM') + '/' + (moment(date).get('year') + bYears);
//        }
//    };
//    $mdDateLocaleProvider.parseDate = function (dateString) {
//        var dateArray = dateString.split("/");
//        dateString = dateArray[1] + "/" + dateArray[0] + "/" + (dateArray[2] - bYears);
//        var m = moment(dateString, 'L', true);
//        return m.isValid() ? m.toDate() : new Date(NaN);
//    };

//    //$mdDateLocaleProvider.formatDate = function (date) {
//    //    if (date == null || date == '') {
//    //        return '';
//    //    } else {
//    //        var bYears = 0;
//    //        if (culture == 'th-TH') bYears = 543;
//    //        date.setFullYear(date.getFullYear() + bYears);
//    //        return moment(date).format('DD/MM/YYYY');
//    //    }
//    //};
//});
app.filter('cDate', function ($filter) {
    return function (input) {
     
      
        if (input == null) { return ""; }
        if (input.length < 15) { return input; }
        var bYears = 0;
        if (culture == 'th-TH') bYears = 543;
        var d = new Date(input);
        if (d.getFullYear()<2300){
            d.setFullYear(d.getFullYear() + bYears);
        }
        //Please write the formula for getting the buddhist date here
        //Below is a rough conversion of adding 365*bYears days to todays date

        // Convert 'days' to milliseconds
        // var millies = 1000 * 60 * 60 * 24 * 365 * bYears;
        var _date = $filter('date')(new Date(d), 'dd/MM/yyyy');
        return _date.toUpperCase();

    };
});

app.directive('jqdatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            var bYears = 0;
            if (culture == 'th-TH') bYears = 543;
            var dateFormat = 'dd/MM/yyyy';
            ngModelCtrl.$formatters.unshift(function (modelValue) {
                if (modelValue == undefined || modelValue == null || modelValue == '') {
                    return '';
                } else {
                    return moment(modelValue).format('DD/MM') + '/' + (moment(modelValue).get('year') + bYears);
                }
            });

            $(element).datepicker({
                language: culture.toLowerCase(),
                isRTL: false,
                format: 'dd/mm/yyyy',
                autoclose: true,
                onSelect: function (date) {
                    var ngModelName = this.attributes['ng-model'].value;
                    console.log(date);
                    // if value for the specified ngModel is a property of 
                    // another object on the scope
                    if (ngModelName.indexOf(".") != -1) {
                        var objAttributes = ngModelName.split(".");
                        var lastAttribute = objAttributes.pop();
                        var partialObjString = objAttributes.join(".");
                        var partialObj = eval("scope." + partialObjString);

                        partialObj[lastAttribute] = date;
                    }
                        // if value for the specified ngModel is directly on the scope
                    else {
                        scope[ngModelName] = date;
                    }
                    scope.$apply();
                }

            });
        }
    };
});

var mainController = app.controller('mainController', function ($scope, $filter, $rootScope, NgTableParams, $log, $compile, $animate, $timeout, $http, $locale) {
    $scope.totalDisplayed = 100000;
    $rootScope.pageState = "LIST";
    $scope.mainData = {};
    $scope.mainData.List_vwFD11PROP = [];
    $scope.mainData.List_vwFD11PROP_EDITINGROW = [];
    $scope.txtSearchProject;
    $scope.txtStart;
    $scope.txtCount;

    $scope.ED01PROJ = [];
    setED01PROJ();

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

    //############################ DATASOURCE_ED03UNIT (for autocomplete)
    function setDATASOURCE_ED03UNIT() {
        
        url = ApiUrl_AutoComplete + '?dataSource=list-available-ed03unit-by-freprjno&query=' + $scope.txtSearchProject;
        $http(
        {
            method: 'GET',
            url: url,
        }).then(function successCallback(response) {
            if (response.data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data.ErrorView.Message);
            }else{
                $scope.DATASOURCE_ED03UNIT = response.data.Datas.Data1;
            }

        }, function errorCallback(response) {
            console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
        });
    }





    $scope.bindProject = function () {
        setDATASOURCE_ED03UNIT();
        //fking ie
        //$scope.mainData.ED01PROJ = $scope.ED01PROJ.find(x => x.FREPRJNO === $scope.txtSearchProject);
        //$scope.mainData.ED01PROJ.FTOTAREA = $scope.mainData.ED01PROJ.FTOTALAREA;
        //$scope.mainData.ED01PROJ.FRELOCAT1 = $scope.mainData.ED01PROJ.FRELOCATE1;
        //$scope.mainData.ED01PROJ.FRELOCAT2 = $scope.mainData.ED01PROJ.FRELOCATE2;
        //$scope.mainData.ED01PROJ.FRELOCAT3 = $scope.mainData.ED01PROJ.FRELOCATE3;
        //console.log($scope.mainData.ED01PROJ);
    };

    $scope.getData = function () {
        $rootScope.pageState = 'LIST';
        $scope.totalDisplayed = 20;
        if (undefinedToEmpty($scope.txtSearchProject) == '') {
            //OpenDialogError('กรุณาเลือกโครงการ');
            $('#resPleaseSelect').show();
            return;
        }
        $('#resPleaseSelect').hide();
        showOverlay();
        url = ApiUrl_LandUtilitiesManage + '?id=' + escape(undefinedToEmpty($scope.txtSearchProject));
        url += '&unitfrom=' + escape(undefinedToEmpty($scope.txtSearchUnitFrom));
        url += '&unitto=' + escape(undefinedToEmpty($scope.txtSearchUnitTo));
        url += '&pcpiecefrom=' + escape(undefinedToEmpty($scope.txtSearchFPCPIECEFrom));
        url += '&pcpieceto=' + escape(undefinedToEmpty($scope.txtSearchFPCPIECETo));
        url += '&culture=' + escape(culture);
        $.ajax({
            type: 'GET',
            //url: ApiUrl_LandUtilitiesManage + '?id=' + $scope.txtSearchProject + '&culture=' + culture,//'A115070001',
            url:url,
            // headers: getAuthenticateHeaders(),
            data: '',
        }).done(function (data) {
            $scope.mainData = data;
            $log.info(data);
            
            //$scope.addNewRow();
            $scope.$apply();
            $timeout(function () {
                setTableWidth();
            }, 1000);
        }).fail(
                  function (jqXHR, textStatus, errorThrown) {
                      OpenDialogError(jqXHR.status + ':' + jqXHR.errorThrown);
                      console.log(jqXHR);
        }).complete(function () {
            closeOverlay();
        });
    };

    $scope.save = function () {
        var arrSer = [];
        for (var i = 0; i < $scope.mainData.List_vwFD11PROP_EDITINGROW.length; i++) {
            arrSer.push($scope.mainData.List_vwFD11PROP_EDITINGROW[i].FPCPIECE);
        }
        var sorted_arr = arrSer.slice().sort(); // You can define the comparing function here. 
        // JS by default uses a crappy string compare.
        // (we use slice to clone the array so the
        // original array won't be modified)
        var dupArr = [];
        for (var i = 0; i < arrSer.length - 1; i++) {
            if (sorted_arr[i + 1] == sorted_arr[i]) {
                dupArr.push(sorted_arr[i]);
            }
        }
        if (dupArr.length > 0) {
            OpenDialogError('โฉนด: ' + dupArr.join(', ') + ' ซ้ำ');
            return;
        }
        if ($rootScope.pageState == 'EDIT') {
            updateData();
        } else if ($rootScope.pageState == 'NEW') {
            insertData();
        }
    };

    $scope.delete = function (index) {
        console.log('delete');
        console.log($rootScope.pageState);
        //mapping view to table and remove unnecesary data

        var pId = $scope.mainData.List_vwFD11PROP_EDITINGROW[index].FASSETNO;
        console.log(pId);

        if (!confirm("Want to delete ASSET# " + pId + "?")) {
            return false;
        }
        if (!(pId === "")) {
            deleteData(pId, index);
        } else {
            $scope.mainData.List_vwFD11PROP_EDITINGROW.splice(index, 1);
            //$scope.$apply();
        }
        
    };
    $scope.deleteAll = function () {
        console.log('deleteAll');
        console.log($rootScope.pageState);
        $scope.mainData.List_vwFD11PROP_EDITINGROW = [];
        listid = [];
        listindex = [];
        for (i = 0; i < $scope.mainData.List_vwFD11PROP.length; i++) {
            if ($scope.mainData.List_vwFD11PROP[i].isRowChecked) {
                listid.push($scope.mainData.List_vwFD11PROP[i].FASSETNO);
                listindex.push(i);
            }
        }
        if (listindex.length > 0) {
            if (!confirm("Want to delete UNIT# " + listid.join(',') + "?")) {
                return false;
            }
            deleteAllData(listid, listindex);
        } else {

            OpenDialogError('กรุณาเลือกข้อมูลที่ต้องลบ');
        }
       
    };
    function insertData() {
        console.log('inserting');
        newRows = JSON.parse(JSON.stringify( $scope.mainData.List_vwFD11PROP_EDITINGROW));
        //fix date format
        newRows.pop(); //mapping view to table and remove unnecesary data
        saveData = {};
        saveData.List_vwFD11PROP = [];
        saveData.List_FD11PROP = [];
        for (i = 0; i < newRows.length; i++) {
            newRows[i].FHBKDATE = cJsonDate(newRows[i].FHBKDATE);
            console.log(newRows[i].FHBKDATE);
            saveData.List_FD11PROP.push(newRows[i]);
        }
        if (!validateAllInput(saveData.List_FD11PROP)) {
            return;
        }
        showOverlay();
        $.ajax({
            type: 'POST',
            url: ApiUrl_LandUtilitiesManage,//'A115070001',
            // headers: getAuthenticateHeaders(),
            data:  saveData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data);
            } else {
                
                    //insert many row
                    newRecords = data.Datas.Data1;
                    $scope.mainData.List_vwFD11PROP_EDITINGROW = [];
                    //if (!($scope.mainData.List_vwFD11PROP_EDITINGROW === undefined)) {
                    //    $scope.mainData.List_vwFD11PROP_EDITINGROW.splice($scope.mainData.List_vwFD11PROP_EDITINGROW.length - 1, 1);
                    //} else {
                    //    $scope.mainData.List_vwFD11PROP_EDITINGROW = [];
                    //}

                    arrNewIndex = [];
                    newIndex = $scope.mainData.List_vwFD11PROP_EDITINGROW.length;
                    for (i = 0; i < newRecords.length; i++) {
                        arrNewIndex.push(newIndex++);
                        //$scope.mainData.List_vwFD11PROP.push(newRecords[i]);
                        $scope.mainData.List_vwFD11PROP_EDITINGROW.push(newRecords[i]);

                    }
                    //$scope.addNewRow();
                    $scope.$apply();
                    blinkEditedRow(arrNewIndex);
                    $scope.txtStart = '';
                    $scope.txtCount = '';

           
                    //insert one row
                    //newRecords = data.Datas.Data1;
                    //var pObject = $scope.mainData.List_vwFD11PROP_EDITINGROW[$scope.mainData.List_vwFD11PROP_EDITINGROW.length - 2];
                    //pObject.FASSETNO = newRecords[0].FASSETNO;
                    //pObject.FPCPIECE_OLD = pObject.FPCPIECE;
                    //pObject.FSERNO_OLD = pObject.FSERNO;
                    //blinkEditedRow([$scope.mainData.List_vwFD11PROP_EDITINGROW.length - 2]);
                    $rootScope.pageState = 'EDIT';
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
    };
    function updateData() {
        if (isEmpty($scope.arrUpdateQueue) || $scope.arrUpdateQueue.length == 0) {
             OpenDialogError('ไม่มีการแก้ไขข้อมูล')
                return;
        }
        saveData = {};//JSON.parse(JSON.stringify($scope.mainData));
        //$scope.saveData.fD11PROP = $scope.saveData.List_vwFD11PROP[index];
        //$scope.saveData.fD11PROP = $scope.saveData.List_vwFD11PROP_EDITINGROW[index];
        saveData.List_FD11PROP = [];
        //$scope.saveData.List_vwFD11PROP = [];
        for (i = 0; i < $scope.arrUpdateQueue.length; i++) {
            var updateIndex = $scope.arrUpdateQueue[i];
            $scope.mainData.List_vwFD11PROP_EDITINGROW[updateIndex].FHBKDATE = cJsonDate($scope.mainData.List_vwFD11PROP_EDITINGROW[updateIndex].FHBKDATE);
            saveData.List_FD11PROP.push($scope.mainData.List_vwFD11PROP_EDITINGROW[updateIndex]);

        }
        if (!validateAllInput(saveData.List_FD11PROP)) {
            return;
        }
        showOverlay();
        $.ajax({
            type: 'PUT',
            url: ApiUrl_LandUtilitiesManage, //+ escape($scope.saveData.fD11PROP.FASSETNO),//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: saveData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                //console.log('update fail at row:' + index)
                console.log(data);
            } else {
                console.log('update at row:' + $scope.arrUpdateQueue.join(', '))
                for (i = 0; i < $scope.arrUpdateQueue.length; i++) {
                    $scope.mainData.List_vwFD11PROP_EDITINGROW[i].FPCPIECE_OLD = $scope.mainData.List_vwFD11PROP_EDITINGROW[i].FPCPIECE;
                    $scope.mainData.List_vwFD11PROP_EDITINGROW[i].FSERNO_OLD = $scope.mainData.List_vwFD11PROP_EDITINGROW[i].FSERNO;
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
 
    function validateAllInput(arrData) {
        if (isEmpty(arrData)) {
            OpenDialogError('ไม่พบข้อมูลแก้ไข');
            return false;
        }
        for(i=0;i<arrData.length;i++){
            if (isEmpty(arrData[i].FPCPIECE)) {
                OpenDialogError('กรุณากรอกเลขที่โฉนดให้ครบถ้วน');
                return false;
            }
        }
        return true;
    }
    function deleteData(id, index) {
        $.ajax({
            type: 'DELETE',
            url: ApiUrl_LandUtilitiesManage + '?id=' + escape(id),//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: $scope.mainData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data);
            } else {
                $scope.mainData.List_vwFD11PROP_EDITINGROW.splice(index, 1);
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
    function deleteAllData(listid, listindex) {
        id = listid.join("_");
        $.ajax({
            type: 'DELETE',
            url: ApiUrl_LandUtilitiesManage + '?id=' + escape(id),//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: $scope.mainData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data);
            } else {
                for (i = listindex.length - 1; i >= 0; i--) {
                    $scope.mainData.List_vwFD11PROP.splice(listindex[i], 1);
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

    //record maintain
    $scope.addNewRow = function () {
        newRow = {};
        newRow.FASSETNO = "";
        newRow.FREPRJNO = $scope.txtSearchProject;
        newRow.FPCPIECE = "";
        console.log('add empty row:');
        console.log(newRow);
        $scope.mainData.List_vwFD11PROP_EDITINGROW.push(newRow);
    }


    $scope.addNewRowDataDefualt = function () {
        if ($scope.mainData.List_vwFD11PROP_EDITINGROW.length > 1) {
            var pObject = $scope.mainData.List_vwFD11PROP_EDITINGROW[$scope.mainData.List_vwFD11PROP_EDITINGROW.length - 2];
            if (!isEmpty(pObject.FPCPIECE)) {
                $scope.addNewRow();
            } else {
                OpenDialogError('กรุณากรอกข้อมูลให้ครบ');
            }
        } else if ($scope.mainData.List_vwFD11PROP_EDITINGROW.length = 1) {
            $scope.addNewRow();
        }
            //$scope.save(newRows);
        
    }

    //$scope.addNewRowDataDefualt = function () {
    //    var pLast = $scope.mainData.List_vwFD11PROP[$scope.mainData.List_vwFD11PROP.length - 2].FASSETNO;
    //    var pLastFPCPIECE = $scope.mainData.List_vwFD11PROP[$scope.mainData.List_vwFD11PROP.length - 2].FPCPIECE;
    //    if (!(pLast === "" || pLastFPCPIECE === "")) {
    //        var pNewFREPRJNO = pLast.split(" ")[0];
    //        var pNewFASSETNO = pLast.split(" ")[1];
    //        var pFASSETNOFull;
    //        //$scope.mainData.List_vwFD11PROP.splice(-1);
    //        newRow = {};
    //        pFASSETNOFull = pNewFREPRJNO + " " + ("0000" + (parseInt(pNewFASSETNO) + 1)).slice(-4);
    //        newRow.FASSETNO = pFASSETNOFull;
    //        newRow.FREPRJNO = pNewFREPRJNO;
    //        newRow.FPCPIECE = parseInt(pLastFPCPIECE) + 1;
    //        //newRow = {};
    //        //$scope.mainData.List_vwFD11PROP.push(newRow);
    //        newRows = [];
    //        newRows.push(newRow);
    //        $scope.save(newRows);
    //    }
    //}
 


    $scope.generateRows = function () {
        if (undefinedToEmpty($scope.txtSearchProject) == '') {
            //OpenDialogError('กรุณาเลือกโครงการ');
            $('#resPleaseSelect').show();
            return;
        }
        if (!isEmpty($scope.txtSearchProject)) {
            $('#resPleaseSelect').hide();
            if (!($scope.txtStart === "") && !($scope.txtCount === "")) {
                var pStart = $scope.txtStart;
                var pCount = parseInt($scope.txtCount);
                //var newRows = [];
                $scope.mainData.List_vwFD11PROP_EDITINGROW.pop();
                for (i = 0 ; i < pCount; i++) {
                    newRow = {};
                    newRow.FREPRJNO = $scope.txtSearchProject;
                    //set fassetno to avoid invalid modelstate
                    newRow.FASSETNO = '';
                    newRow.FPCPIECE = pStart++;
                    //newRows.push(newRow);
                    $scope.mainData.List_vwFD11PROP_EDITINGROW.push(newRow);
                }
                $scope.addNewRow();
                //$scope.save(newRows, "");
            } else {
                OpenDialogError('ใส่ให้ครบ');
            }
        } else {
            OpenDialogError('กรุณาใส่โครงการ');
        }

    }

    $scope.showAddPage = function () {
        if (undefinedToEmpty($scope.txtSearchProject) == '') {
            OpenDialogError('กรุณาเลือกโครงการ');
            $('#resPleaseSelect').show();
        } else {
            $('#resPleaseSelect').hide();
            $scope.mainData.List_vwFD11PROP_EDITINGROW = [];
            $scope.addNewRow();

            $scope.txtStart = '';
            $scope.txtCount = '';
            $rootScope.pageState = 'NEW'
            $timeout(function () {
                setTableWidth();
            }, 1000);
        }
    }

    $scope.closePageEditAdd = function () {
        $rootScope.pageState = 'LIST';
        $scope.checkAll = false;
        $scope.arrUpdateQueue = [];
        $scope.getData();
    }

    $scope.showEditPage = function () {
        $scope.mainData.List_vwFD11PROP_EDITINGROW = [];
        
        for (i = 0; i < $scope.mainData.List_vwFD11PROP.length; i++) {
            if ($scope.mainData.List_vwFD11PROP[i].isRowChecked) {
                $scope.mainData.List_vwFD11PROP_EDITINGROW.push($scope.mainData.List_vwFD11PROP[i]);
            }
        }
        if ($scope.mainData.List_vwFD11PROP_EDITINGROW.length>0){
            $rootScope.pageState = 'EDIT';
            $timeout(function () {
                setTableWidth();
            }, 1000);
        } else {
            OpenDialogError('กรุณาเลือกข้อมูลที่ต้องการแก้ไข');
        }
  
    }
    $scope.List_vwFD11PROP_checkAllRow = function () {
        $scope.mainData.List_vwFD11PROP_EDITINGROW = [];
        for (i = 0; i < $scope.mainData.List_vwFD11PROP.length; i++) {
            $scope.mainData.List_vwFD11PROP[i].isRowChecked = $scope.checkAll;
        }
    }
    

    //$scope.generateRows = function () {
    //    if (!($scope.mainData.ED01PROJ.FREPRJNO === "")) {
    //        if (!($scope.txtStart === "") && !($scope.txtCount === "")) {
    //            var pLast = $scope.mainData.List_vwFD11PROP[$scope.mainData.List_vwFD11PROP.length - 2].FASSETNO;
    //            var pNewFREPRJNO = pLast.split(" ")[0];
    //            var pNewFASSETNO = parseInt(pLast.split(" ")[1]) + 1;

    //            var pStart = $scope.txtStart;
    //            var pCount = parseInt(pNewFASSETNO) + parseInt($scope.txtCount);

    //            var pFASSETNOFull;
    //            var newRows = [];

    //            for (i = pNewFASSETNO ; i < pCount; i++) {
    //                newRow = {};
    //                newRow.FREPRJNO = $scope.mainData.ED01PROJ.FREPRJNO;
    //                newRow.FASSETNO = newRow.FREPRJNO + " " + ("0000" + i).slice(-4);
    //                newRow.FPCPIECE = pStart++;
    //                newRows.push(newRow);
    //            }
    //            $scope.save(newRows);
    //        } else {
    //            OpenDialogError('ใส่ให้ครบ');
    //        }
    //    } else {
    //        OpenDialogError('กรุณาใส่โครงการ');
    //    }
    //}
    //utility
    function cJsonDate(date) {
        if (date == null || date == '' || date.length > 15) {
            return date;
        } else {
            arrDate = date.split('/');
            if (culture.toLowerCase() == 'th-th') {
                arrDate[2] -= 543;
            }
            var sysDate = arrDate[2] + '-' + arrDate[1] + '-' + arrDate[0];
            return (new Date(sysDate)).toJSON();
        }
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
    $scope.addDisplayLimit = function () {
        $scope.totalDisplayed = +$scope.totalDisplayed + 20;
        $scope.$apply();
    };

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
            }, 2000);
        });
        $scope.arrUpdateQueue = [];
    }
 


    $scope.arrUpdateQueue = [];

    $scope.chkDup = function (index, event) {
     
        var pObject = $scope.mainData.List_vwFD11PROP_EDITINGROW[index];
        if (pObject.FSERNO == pObject.FSERNO_OLD) {
            pObject.e_FPDCODE = pObject.e_FPDCODE_OLD;
            pObject.FPCINST = pObject.FPCINST_OLD;
            return;
        }
        var pFASSETNO = pObject.FASSETNO;
        var pFREPRJNO = pObject.FREPRJNO;
        var pFSERNO = pObject.FSERNO;
        console.log(pFSERNO);
        console.log((pFSERNO === "" && pFSERNO === null));
        if (!(isEmpty(pFSERNO) || isEmpty(pFREPRJNO) )) {
            url = ApiUrl_AutoComplete + '?dataSource=ED03UNIT_Available&query=' + undefinedToEmpty(pFASSETNO) + "_" + pFREPRJNO + "_" + pFSERNO;

            $http(
            {
                method: 'GET',
                url: url,
            }).then(function successCallback(response) {
                if (response.data.ErrorView.IsError) {
                    //OpenDialogError('หมายเลขแปลงตามผัง : ' + pFSERNO + ' ถูกใช้งานแล้ว');
                    OpenDialogError(response.data.ErrorView.Message);
                        pObject.FSERNO = pObject.FSERNO_OLD;
                        pObject.e_FPDCODE = pObject.e_FPDCODE_OLD;
                        pObject.FPCINST = pObject.FPCINST_OLD;
                  
                } else {
                    pObject.e_FPDCODE = response.data.Datas.Data1.FPDCODE;
                    pObject.FPCINST = response.data.Datas.Data1.FADDRNO;
               
                }
                //if (response.data.length === 0) {
                //    OpenDialogError('หมายเลขแปลงตามผัง : ' + pObject.FSERNO + ' ถูกใช้งานแล้ว');
                //    //pObject.FSERNO = "";
                //    //pObject.e_FPDCODE = "";
                //    //pObject.FPCINST = "";
                //    $animate.removeClass($('#row_' + arrTempQueue[index]), 'danger');
                //    return
                //} else {
                //    pObject.e_FPDCODE = response.data[0].FPDCODE;
                //    pObject.FPCINST = response.data[0].FADDRNO;
                //    $scope.arrUpdateQueue.push(index);
                //    $scope.blur(index, event);
                //}

            }, function errorCallback(response) {
                console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
            });
        } else {

            pObject.e_FPDCODE = '';
            pObject.FPCINST = '';
        }
   
    }

    //###################Check Dup FPCPIECE (UNIT)
    $scope.setFPCPIECE = function (index, event) {
     
            var pObject = $scope.mainData.List_vwFD11PROP_EDITINGROW[index];
            var pFASSETNO = pObject.FASSETNO;
            var pFREPRJNO = pObject.FREPRJNO;
            var pFPCPIECE = pObject.FPCPIECE;
            console.log('!isEmpty(pFPCPIECE) ' + !isEmpty(pFPCPIECE));
            if (isEmpty(pFPCPIECE)) {
                OpenDialogError('กรุณากรอกโฉนด');
                return;
            }
            if (pObject.FPCPIECE === pObject.FPCPIECE_OLD) {
                console.log('เลขเดิมไม่ต้องเช็ค');
               // $animate.removeClass($('#row_' + index), 'danger');
                return;
            }

                url = ApiUrl_AutoComplete + '?dataSource=FD11PROP_setFPCPIECE&query=' + pFREPRJNO + "_" + pFPCPIECE;

                $http(
                {
                    method: 'GET',
                    url: url,
                }).then(function successCallback(response) {
                    if (response.data.ErrorView.IsError) {
                        OpenDialogError(response.data.ErrorView.Message);
                        pObject.FPCPIECE = pObject.FPCPIECE_OLD;
                        
                        
                        //pObject.FPDNAME = '';
                        //pObject.FDESC = '';
                        //pObject.FAREA = '';
                    } else {
                      
                    }
                }, function errorCallback(response) {
                    console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);

                });

    }
  
    $scope.change = function (index) {
        $animate.addClass($('#row_' + index), 'danger');
        if ($scope.arrUpdateQueue.indexOf(index) === -1) {
            $scope.arrUpdateQueue.push(index);
        }
    }
 

    $rootScope.$watch('pageState', function (newVal, oldVal) {
        $timeout(function () { $('select').trigger("chosen:updated"); }, 100)
        
        console.log('pageState:' + newVal);
    });
    $scope.selectedIndex = -1;
    $scope.setSelected = function (index) {
        console.log(index);
        $scope.selectedIndex = index;
    };
});



var scopeForDebug = {}
$(function () {
    scopeForDebug = angular.element('[ng-controller]').scope();

})
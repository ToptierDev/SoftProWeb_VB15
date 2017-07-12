
var ApiUrl_TRN_ReservationAndApproveRecord = rootHost + "/webapi/TRN_ReservationAndApproveRecord/";
var ApiUrl_AutoComplete = rootHost + "/webapi/AutoComplete/";


var app = angular.module('myApp', ['ngTable', 'ngMaterial', 'ngMessages', 'material.svgAssetsCache']);
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

            return moment(date).format('DD/MM') + '/' + (moment(date).get('year') + bYears);
        }
    };
    $mdDateLocaleProvider.parseDate = function (dateString) {
        var dateArray = dateString.split("/");
        dateString = dateArray[1] + "/" + dateArray[0] + "/" + (dateArray[2] - bYears);
        var m = moment(dateString, 'L', true);
        return m.isValid() ? m.toDate() : new Date(NaN);
    };

});
app.filter('cDate', function ($filter) {
    return function (input) {
        if (input == null) { return ""; }
        var bYears = 0;
        if (culture == 'th-TH') bYears = 543;
        var d = new Date(input);
        d.setFullYear(d.getFullYear() + bYears);
        var _date = $filter('date')(new Date(d), 'dd/MM/yyyy');

        return _date.toUpperCase();

    };
});


var mainController = app.controller('mainController', function ($scope, $filter, $rootScope, NgTableParams, $http, $timeout, $q, $log) {

    $rootScope.pageState = "EDIT";
    $scope.mainData = {};
    $scope.getData = function () {
        url = ApiUrl_TRN_ReservationAndApproveRecord 
        $rootScope.pageState = "EDIT";
        showOverlay();
    
        $.ajax({
            type: 'GET',
            url: url,
            // headers: getAuthenticateHeaders(),
            data: '',
        }).done(function (data) {
            console.log(data);
            if (data.ErrorView.IsError) {
                OpenDialogError('Error:' + data.ErrorView.Message);
            } else {

                $scope.mainData = data.Datas.Data1;
                $scope.mainData.preCode = $scope.mainData.OD11BKT1.FTRNYEAR + $scope.mainData.OD11BKT1.FTRNCD;
              
               // $scope.addNewRow();
                $scope.$apply();
              
                //$timeout(function () {
                //    $('#divEditContainer select[data-id]').trigger("chosen:updated");
                    closeOverlay();
                //}, 500)
            }
        }).fail(
            function (jqXHR, textStatus, errorThrown) {
                OpenDialogError('Error:' + jqXHR.status);
                console.log(jqXHR);
                closeOverlay();
            });
    };


    $scope.getNewData = function () {
      
        url = ApiUrl_ProjectPriceList + '?query='
            + '&culture=' + culture + '&mode=new';
       
        showOverlay();
        $.ajax({
            type: "GET",
            url: url,
            contentType: "json",
            dataType: "json",
        }).done(function (data) {
            console.log('getData Done');
            console.log(data);
            if (data.ErrorView.IsError) {
                OpenDialogError('Error:' + data.ErrorView.Message);
               
            } else {

                $scope.mainData = data.Datas.Data1;
                $scope.$apply();
                initNewRecord();
                closeOverlay();
            }
        }).fail(
            function (jqXHR, textStatus, errorThrown) {
                OpenDialogError('Error:' + jqXHR.status);
                console.log(jqXHR);
               
            });
    };




    $scope.showEditPage = function (FREPRJNO, FREPHASE, FREZONE, FTRNNO) {
        $scope.getData(FREPRJNO, FREPHASE, FREZONE, FTRNNO);
      
    }
    $scope.showInsertPage = function () {
        $rootScope.pageState = "NEW";
       
        if (isEmpty($scope.mainData.ED04RECF.FREPRJNO) || isEmpty($scope.mainData.ED04RECF.FREPHASE) || isEmpty($scope.mainData.ED04RECF.FREZONE)) {
            clearNewData();
            return;
        } else {
            $scope.getNewData();
        }


    }


    $scope.deleteData = function (id, index, item) {
        if (!confirm("Want to delete # " + id + "?")) {
            return false;
        }
        $.ajax({
            type: 'DELETE',
            url: ApiUrl_ProjectPriceList + '?id='
                + escape(id),//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: $scope.mainData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data);
            } else {
                //$scope.searchResult.splice(index, 1);
                $scope.searchResult.splice($scope.searchResult.indexOf(item), 1);
                $scope.searchResultTable.reload()
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


    // edit zone



    function initNewRecord() {
        if (isEmpty($scope.mainData.ED04RECF.FREPRJNO) || isEmpty($scope.mainData.ED04RECF.FREPHASE) || isEmpty($scope.mainData.ED04RECF.FREZONE)) {
            clearNewData();
            return;
        }
        $log.info('initNewRecord');
        //$scope.mainData = {};
        //$scope.mainData.ED11PAJ1 = {};
        //$scope.mainData.ED11PAJ1.FTRNYEAR = systemDateString.slice(-4);
        //$scope.mainData.ED11PAJ1.FTRNCD = 'PA';
        //$scope.mainData.preCode = $scope.mainData.ED11PAJ1.FTRNYEAR + $scope.mainData.ED11PAJ1.FTRNCD;
        //$scope.mainData.ED04RECF = {};
        //$scope.mainData.vw_List_ED11PAJ2 = [];
        $scope.mainData.ED11PAJ1 = JSON.parse(JSON.stringify($scope.mainData.ED04RECF));
        $scope.mainData.ED11PAJ1.FMDOWN = $scope.mainData.ED04RECF.FDOWNMON;
        $scope.mainData.ED11PAJ1.FTRNYEAR = systemDateString.slice(-4);
        $scope.mainData.ED11PAJ1.FTRNCD = 'PA';
        $scope.mainData.preCode = $scope.mainData.ED11PAJ1.FTRNYEAR + $scope.mainData.ED11PAJ1.FTRNCD;
      
        $scope.mainData.ED11PAJ1.FVOUDATE = cJsonDate(Date.now());

        $scope.addNewRow();
        initAllCheckBox();
        $scope.$apply;
        $timeout(function () {
            $('#divEditContainer select[data-id]').trigger("chosen:updated")

        }, 500)

    };
    function clearNewData() {
        $('select[data-id]').trigger("chosen:updated")

        $scope.mainData.ED11PAJ1 = {};
        $scope.mainData.vw_List_ED11PAJ2 = [];
        $scope.mainData.ED11PAJ1.FTRNYEAR = systemDateString.slice(-4);
        $scope.mainData.ED11PAJ1.FTRNCD = 'PA';
        $scope.mainData.preCode = $scope.mainData.ED11PAJ1.FTRNYEAR + $scope.mainData.ED11PAJ1.FTRNCD;
       
        // $scope.mainData.ED04RECF = {};
        $('select[data-id]').trigger("chosen:updated")
    }

    function initAllCheckBox() {
        $scope.chkFPRICETYPE = $scope.initChkFPRICETYPE();
        $scope.chkFPPUBLICTY = $scope.initChkFPPUBLICTY();
    }


    $scope.cancel = function () {
        $rootScope.pageState = 'LIST';
        if (!isEmpty($scope.mainData.ED04RECF.FREPRJNO)) {
            $scope.getSearchResult();
            return;
        }
        $('select[data-id]').trigger("chosen:updated")
    };



    $scope.addNewRow = function () {
        if ($scope.mainData.vw_List_ED11PAJ2 == undefined) {
            $scope.mainData.vw_List_ED11PAJ2 = [];
        }
        newRow = {};
        newRow.FTRNNO = $scope.mainData.ED11PAJ1.FTRNNO;
        $scope.mainData.vw_List_ED11PAJ2.push(newRow);

    }
    $scope.removeRow = function (index) {
        $scope.mainData.vw_List_ED11PAJ2.splice(index, 1)
    }


    $scope.save = function () {
        console.log('saving');

        if (!validateAllInput()) {
            console.log('validateAllInput fial');
            return false
        }
        console.log($rootScope.pageState);

        $scope.removeRow(-1);
        //$scope.saveData = {};
        //$scope.saveData.Data1 = $scope.mainData.ED04RECF;
        //$scope.saveData.Data2 = $scope.mainData.ED11PAJ1;
        //$scope.saveData.Data3 = $scope.mainData.vw_List_ED11PAJ2;
        $scope.mainData.ED11PAJ1.FMDATE = cJsonDate($scope.mainData.ED11PAJ1.FMDATE);
        $scope.mainData.ED11PAJ1.FVOUDATE = cJsonDate($scope.mainData.ED11PAJ1.FVOUDATE);
      
        for (i = 0; i < $scope.mainData.vw_List_ED11PAJ2.length; i++) {
            $scope.mainData.vw_List_ED11PAJ2[i].FITEMNO = i + 1;
        }
        $scope.mainData.List_ED11PAJ2 = $scope.mainData.vw_List_ED11PAJ2

        if ($rootScope.pageState == 'NEW') {
            insertData();
        } else {
            updateData();
        }

        $scope.addNewRow();

    };

    //###################Check Dup Unit
    $scope.chkDup = function (index, event) {

        var pObject = $scope.mainData.ED04RECF;
        var pObject2 = $scope.mainData.vw_List_ED11PAJ2[index];
        var pFREPRJNO = pObject.FREPRJNO;
        var pFREPHASE = pObject.FREPHASE;
        var pFREZONE = pObject.FREZONE;
        var pFSERIALNO = pObject2.FSERIALNO;
        console.log('chkDup:' + pFREPRJNO + "_" + pFREPHASE + "_" + pFREZONE + "_" + pFSERIALNO);
        var chkDupClient = $scope.mainData.vw_List_ED11PAJ2.filter(function (obj) {
            return obj.FSERIALNO === pFSERIALNO;
        });
        console.log(chkDupClient);
        if (chkDupClient.length>1) {
            OpenDialogError('UNIT ' + pFSERIALNO + ' ซ้ำ');
            pObject2.FPDCODE = '';
            pObject2.FPDNAME = '';
            pObject2.FAREA = '';
            pObject2.FSERIALNO = '';
            return;
        }
        if (!(isEmpty(pFREPRJNO)
            || isEmpty(pFREPHASE)
            || isEmpty(pFREZONE)
            || isEmpty(pFSERIALNO))) {
            url = ApiUrl_AutoComplete + '?dataSource=UNITDUP_ProjectPrice&query=' + pFREPRJNO + "_" + pFREPHASE + "_" + pFREZONE + "_" + pFSERIALNO;
            console.log(url);
            $http(
            {
                method: 'GET',
                url: url,
            }).then(function successCallback(response) {
                if (response.data.ErrorView.IsError) {
                    OpenDialogError(response.data.ErrorView.Message);
                    pObject2.FPDCODE = '';
                    pObject2.FPDNAME = '';
                    pObject2.FAREA = '';
                    pObject2.FSERIALNO = '';
                } else {
                    pObject2.FPDCODE = response.data.Datas.Data1.FPDCODE;;
                    pObject2.FPDNAME = response.data.Datas.Data1.FPDNAME;;
                    pObject2.FAREA = response.data.Datas.Data1.FAREA;
                    pObject2.FSERIALNO = response.data.Datas.Data1.FSERIALNO;
                    //$scope.arrUpdateQueue.push(index);
                    //$scope.blur(index, event);
                }
            }, function errorCallback(response) {
                console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
            });
        } else {
            OpenDialogError('กรุณากรอก UNIT');
        }

    }


    function insertData() {
        $.ajax({
            type: 'POST',
            url: ApiUrl_ProjectPriceList + '?userid=' + $scope.CurrentUser,//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: $scope.mainData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                erMessage = data.ErrorView.Message;
               
                OpenDialogError(erMessage);
                console.log(data);
            } else {
                $scope.mainData = {};
                $scope.mainData = data.Datas.Data1;
                $scope.mainData.preCode = $scope.mainData.ED11PAJ1.FTRNYEAR + $scope.mainData.ED11PAJ1.FTRNCD;
                $scope.mainData.vw_List_ED11PAJ2 = JSON.parse(JSON.stringify($scope.mainData.List_ED11PAJ2));
             
                $rootScope.pageState = 'EDIT';
                $scope.addNewRow();
                $scope.$apply();
                SaveSuccess();
            }
        }).fail(
           function (jqXHR, textStatus, errorThrown) {
               OpenDialogError(jqXHR.status + ':' + jqXHR.errorThrown);
               console.log(jqXHR);
           }).complete(
           function (data) {


           });
    };
    function updateData() {
        $.ajax({
            type: 'PUT',
            url: ApiUrl_ProjectPriceList + '?id=' + escape($scope.mainData.ED11PAJ1.FTRNNO) + '&userid=' + $scope.CurrentUser,//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: $scope.mainData,
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                OpenDialogError(data.ErrorView.Message);
                console.log(data);
            } else {
                SaveSuccess();
            }
        }).fail(
           function (jqXHR, textStatus, errorThrown) {

               OpenDialogError(jqXHR.status + ':' + jqXHR.errorThrown);
               console.log(jqXHR);



           }).complete(
           function (data) {


           });
    };



    function cJsonDate(date) {
        if (isEmpty(date)) {
            return date;
        } else {
            return (new Date(date)).toJSON();
        }
    }






    function validateAllInput() {
        console.log($scope.mainData);
        if ($scope.mainData.preCode === undefined || $scope.mainData.preCode.length < 5) {
            OpenDialogError('PreCode  ไม่ถูกต้อง')
            return false;
        }

        for (i = 0; i < $scope.mainData.vw_List_ED11PAJ2.length-1; i++) {
            if (isEmpty($scope.mainData.vw_List_ED11PAJ2[i].FSERIALNO)) {
                OpenDialogError('กรุณากรอก UNIT ให้ครบ');
                return false;
            }
        }
        if (isEmpty($scope.mainData.ED04RECF.FREPRJNO)) {
            OpenDialogError('กรุณาเลือก โครงการ');
            return false;
        }
        if (isEmpty($scope.mainData.ED04RECF.FREPHASE)) {
            OpenDialogError('กรุณาเลือก เฟส');
            return false;
        }
        if (isEmpty($scope.mainData.ED04RECF.FREZONE)) {
            OpenDialogError('กรุณาเลือก โซน');
            return false;
        }
     



        //more validation here
        console.log('val PASS');
        return true;
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


    //checkbox
    $scope.chkFPRICETYPE_change = function () {
        if ($scope.chkFPRICETYPE) {
            $scope.mainData.ED11PAJ1.FPRICETYPE = 1;
        } else {
            $scope.mainData.ED11PAJ1.FPRICETYPE = 0;

        }
    }


    $scope.initChkFPRICETYPE = function () {
        flag = false;
        if ($scope.mainData.ED11PAJ1 != undefined) {
            if ($scope.mainData.ED11PAJ1.FPRICETYPE == 1) {
                flag = true;
            }
        }
        console.log('initChkFPRICETYPE:' + flag);
        return flag;
    }


    //checkbox
    $scope.chkFPPUBLICTY_change = function () {
        if ($scope.chkFPPUBLICTY) {
            $scope.mainData.ED11PAJ1.FPPUBLICTY = 1;
        } else {
            $scope.mainData.ED11PAJ1.FPPUBLICTY = 0;

        }
    }


    $scope.initChkFPPUBLICTY = function () {
        flag = false;
        if ($scope.mainData.ED11PAJ1 != undefined) {
            if ($scope.mainData.ED11PAJ1.FPPUBLICTY == 1) {
                flag = true;
            }
        }
        return flag;
    }

});





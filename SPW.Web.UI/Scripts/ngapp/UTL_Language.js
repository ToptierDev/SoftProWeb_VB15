
var ApiUrl_UTL_Language = rootHost + "/webapi/UTL_Language/";
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
   
    $rootScope.pageState = "LIST";
    $scope.mainData = {};
    $scope.getSearchResult = function () {
        console.log('getSearchResult');
       
        url = ApiUrl_UTL_Language 
        $.ajax({
            type: "GET",
            url: url,
            contentType: "json",
            dataType: "json",
            success: function (data) {
                if (data.ErrorView.IsError) {
                    OpenDialogError('Error:' + data.ErrorView.Message);
                } else {

                }
                $scope.searchResult = data.Datas.Data1;
                console.log($scope.searchResult);
                $scope.$apply();

            },
            error: function (xhr) {

                OpenDialogError('Error:' + xhr.responseText);
            }
        });
    }
    $scope.getData = function (FREPRJNO, FREPHASE, FREZONE, FTRNNO) {
        console.log(FTRNNO + ',' + FREPRJNO + ',' + FREPHASE + ',' + FREZONE);
        url = ApiUrl_UTL_Language + '?query='
            + escape(FREPRJNO + '_' + FREPHASE + '_' + FREZONE + '_' + FTRNNO)
        + '&culture=' + culture;
        $rootScope.pageState = "EDIT";
        showOverlay();

        $.ajax({
            type: 'GET',
            url: url,
            // headers: getAuthenticateHeaders(),
            data: '',
        }).done(function (data) {
            console.log('getData Done');
            console.log(data);
            if (data.ErrorView.IsError) {
                OpenDialogError('Error:' + data.ErrorView.Message);
            } else {

                $scope.mainData = data.Datas.Data1;
                $scope.mainData.preCode = $scope.mainData.ED11PAJ1.FTRNYEAR + $scope.mainData.ED11PAJ1.FTRNCD;

                $scope.addNewRow();
                $scope.$apply();
                if (isEmpty($scope.ED03UNIT)) {
                    $scope.setED03UNIT(true)
                }
                $timeout(function () {
                    $('#divEditContainer select[data-id]').trigger("chosen:updated");
                    closeOverlay();
                }, 500)
            }
        }).fail(
            function (jqXHR, textStatus, errorThrown) {
                OpenDialogError('Error:' + jqXHR.status);
                console.log(jqXHR);
                closeOverlay();
            });
    };


    $scope.getNewData = function () {
   
        url = ApiUrl_UTL_Language + '?query='
            + escape($scope.mainData.ED04RECF.FREPRJNO + '_' + $scope.mainData.ED04RECF.FREPHASE + '_' + $scope.mainData.ED04RECF.FREZONE)
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
                initAllCheckBox();
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
            url: ApiUrl_UTL_Language + '?id='
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
      
        $scope.cleanResource();
       
            insertData();
     

    };


    $scope.cleanResource = function () {
        $scope.searchResult = $scope.searchResult.filter(function (el) {
            return !isEmpty(el.ResourceName)
            && !(isEmpty(el.ResourceValueLC) && isEmpty(el.ResourceValueEN))
        });

    }
    function insertData() {
        var viewModel = JSON.parse(JSON.stringify($scope.searchResult));
        $.ajax({
            type: 'POST',
            url: ApiUrl_UTL_Language ,
            // headers: getAuthenticateHeaders(),
            data: { '': viewModel },
        }).done(function (data) {
            if (data.ErrorView.IsError) {
                erMessage = data.ErrorView.Message;

                OpenDialogError(erMessage);
                console.log(data);
            } else {
                $scope.getSearchResult()
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
            url: ApiUrl_UTL_Language + '?id=' + escape($scope.mainData.ED11PAJ1.FTRNNO) + '&userid=' + $scope.CurrentUser,//'A115070001',
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




});




var scopeForDebug = {}
$(function () {
    scopeForDebug = angular.element('[ng-controller]').scope();
    scopeForDebug.getSearchResult();
})
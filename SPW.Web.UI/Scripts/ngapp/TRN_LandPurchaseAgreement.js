
var ApiUrl_LandPurchaseAgreement = rootHost + "/webapi/LandPurchaseAgreement/";
var ApiUrl_AD01VEN1 = rootHost + "/webapi/AD01VEN1/";
var ApiUrl_Atc_FD11PROP = rootHost + "/webapi/Atc_FD11PROP/";
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

app.directive('ngReallyClick', [function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.bind('click', function () {
                var message = attrs.ngReallyMessage;
                if (message && confirm(message)) {
                    scope.$apply(attrs.ngReallyClick);
                }
            });
        }
    }
}]);




var tableController = app.controller('tableController', function ($scope, $filter, $rootScope, NgTableParams) {
   
    $rootScope.pageState = "LIST";
    $scope.searchQuery = '';
    $scope.searchResult = [];
    $scope.queryResult = { $: "" };

    $scope.getSearchResult = function () {
        console.log('getSearchResult');
        if (isEmpty($scope.searchQuery)) {
            $('#resPleaseEnter').show();
            return;
        }
        $('#resPleaseEnter').hide();

        url = ApiUrl_LandPurchaseAgreement + '?query=' + $scope.searchQuery + '&culture=' + culture+'&mode=search';
        $.ajax({
            type: "GET",
            url: url,
            contentType: "json",
            dataType: "json",
            success: function (data) {
                
                if (data.ErrorView.IsError) {
                    OpenDialogError(data.ErrorView.Message);
                    console.log(data.ErrorView);
                } else {

                    $scope.searchResult = data.Datas.Data1;
                    console.log($scope.searchResult);


                    $scope.searchResultTable = new NgTableParams({
                        page: 1,
                        count: 10,
                        filter:$scope.queryResult
                    }, {
                        total: $scope.searchResult.length,

                        getData: function (params) {
                            $scope.finalSearchResult = params.sorting() ? $filter('orderBy')($scope.searchResult, params.orderBy()) : $scope.searchResult;
                            $scope.finalSearchResult = params.filter() ? $filter('filter')($scope.finalSearchResult, params.filter()) : $scope.finalSearchResult;
                            $scope.finalSearchResult = $scope.finalSearchResult.slice((params.page() - 1) * params.count(), params.page() * params.count());
                            console.log($scope.finalSearchResult);
                            return $scope.finalSearchResult;
                        }
                    });
                    $scope.$apply();
                }
            },
            error: function (xhr) {
                OpenDialogError(xhr.responseText);
            }
        });
    }


    $scope.showEditPage = function (id) {
        console.log(id);
        $rootScope.$emit("GetDataService", id);
    }
    $scope.showInsertPage = function () {
        $rootScope.$emit("InsertDataService");
    }
   
    
    $scope.deleteData = function (id, index, item) {
        if (!confirm("Want to delete # " + id + "?")) {
            return false;
        }
        $.ajax({
            type: 'DELETE',
            url: ApiUrl_LandPurchaseAgreement +'?id='+ escape(id),//'A115070001',
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




    $rootScope.$on("GetSearchResult", function (even, key) {
        $scope.getSearchResult(key);
       
    });
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

var editPageController = app.controller('editPageController', function ($http, $scope, $filter, $rootScope, $timeout, $q, $log, NgTableParams) {
    
    $scope.mainData = {};
    var self = this;
    $scope.coreDivision = [];
    setCoreDivision();
    $rootScope.$on("GetDataService", function (even, id) {

        $scope.getData(id);
        $rootScope.pageState = "EDIT";
    });
    $rootScope.$on("InsertDataService", function (even) {
        $rootScope.pageState = "NEW";
        initNewRecord();
        console.log($scope.pageState);
        self.AssetNo_states = AssetNo_loadAllAsset($http, '');

        $("select").val('').trigger("chosen:updated");
    });
    $rootScope.$on("DeleteDataService", function (even, id) {
        
        deleteData(id);
    });
    function initNewRecord() {

        $log.info('initNewRecord');
        $scope.mainData = {};
        //set AD11INV1
        $scope.mainData.AD11INV1 = {};
        $scope.mainData.AD11INV1.FYEAR = systemDateString.slice(-4);
        $scope.mainData.AD11INV1.FVOUCD = 'LA';
        $scope.mainData.AD11INV1.FDIVCODE = '';
        $scope.mainData.preCode = $scope.mainData.AD11INV1.FYEAR + $scope.mainData.AD11INV1.FVOUCD;
        //set AD01VEN1
        $scope.mainData.AD01VEN1 = {};
        //set FD11PROP
        $scope.mainData.FD11PROP = {};
        //set FD11PROP
        $scope.mainData.List_AD11INV3 = [];
        $scope.addNewRow();
        
        //clear autocomplete search keyword
        self.searchText = '';
        self.AssetNo_searchText = '';
        self.AssetPc_searchText = '';

       $scope.$apply;
    };
    $scope.getData = function (id) {
        $.ajax({
            type: 'GET',
            url: ApiUrl_LandPurchaseAgreement + '?id='+id+'&culture='+culture,//'A115070001',
            // headers: getAuthenticateHeaders(),
            data: '',
        }).done(function (data) {
            $scope.mainData = data;
            $scope.mainData.preCode = $scope.mainData.AD11INV1.FYEAR + $scope.mainData.AD11INV1.FVOUCD;
            self.AssetNo_states = AssetNo_loadAllAsset($http,$scope.mainData.AD11INV1.FASSETNO);
            console.log(data);
            //$scope.detailTable = new NgTableParams({

            //}, {
            //    total: $scope.mainData.List_AD11INV3.length,
            //    counts: [],
            //    getData: function (params) {

            //        return $scope.mainData.List_AD11INV3;
            //    }
            //});

            $scope.calTotalAmount();
            calAssetQty();
            $scope.addNewRow();
            self.searchText = $scope.mainData.AD11INV1.FSUCODE;
            self.AssetNo_searchText = $scope.mainData.AD11INV1.FASSETNO;
            self.AssetPc_searchText = $scope.mainData.FD11PROP.FPCPIECE;
            $scope.$apply();
            $("select").trigger("chosen:updated");
        }).fail(
            function (jqXHR, textStatus, errorThrown) {
                alert(jqXHR.status);
                alert("ApiUrl_LandPurchaseAgreement has error!");
            });
    };

    $scope.addNewRow = function () {
        newRow = {};
        newRow.FVOUNO = $scope.mainData.AD11INV1.FVOUNO;
        $scope.mainData.List_AD11INV3.push(newRow);

    }
    $scope.removeRow = function (index) {
        $scope.mainData.List_AD11INV3.splice(index, 1)
        $scope.calTotalAmount();
    }


    $scope.save = function () {
        console.log('saving');
       
        if (!validateAllInput()) {
            console.log('validateAllInput fial');
            return false
        } 
        console.log($rootScope.pageState);
        $scope.removeRow(-1);
        $scope.mainData.AD11INV1.FYEAR = $scope.mainData.preCode.slice(0,4);
        $scope.mainData.AD11INV1.FVOUCD = $scope.mainData.preCode.slice(4);
        $scope.mainData.AD11INV1.FMDATE = cJsonDate($scope.mainData.AD11INV1.FMDATE);
        $scope.mainData.AD11INV1.FINVDATE = cJsonDate($scope.mainData.AD11INV1.FINVDATE);
        $scope.mainData.AD11INV1.FVOUDATE = cJsonDate($scope.mainData.AD11INV1.FVOUDATE);
        $scope.mainData.AD11INV1.FVOUDATE = cJsonDate($scope.mainData.AD11INV1.FVOUDATE);



        console.log($scope.mainData.List_AD11INV3);
        for (i = 0; i < $scope.mainData.List_AD11INV3.length; i++) {
            $scope.mainData.List_AD11INV3[i].FCOLPRDNO = i + 1;
            $scope.mainData.List_AD11INV3[i].FVOUNO = $scope.mainData.AD11INV1.FVOUNO;
            $scope.mainData.List_AD11INV3[i].FDUEDATE = cJsonDate($scope.mainData.List_AD11INV3[i].FDUEDATE);
          
        }
        if ($rootScope.pageState == 'NEW') {
            insertData();
        } else {
            updateData();
        }

        $scope.addNewRow();

    };
    function insertData() {
        $.ajax({
            type: 'POST',
            url: ApiUrl_LandPurchaseAgreement + $scope.mainData.AD11INV1.FVOUNO,//'A115070001',
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
    function updateData() {
        $.ajax({
            type: 'PUT',
            url: ApiUrl_LandPurchaseAgreement +'?id='+  escape($scope.mainData.AD11INV1.FVOUNO),//'A115070001',
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
        if (date == null || date == '') {
            return date;
        }else{
            return (new Date(date)).toJSON();
        }
    }


    function calAssetQty() {
        total = $scope.mainData.FD11PROP.FQTY;
        r = Math.floor(total / 400);
        n = Math.floor((total % 400)/100);
        v = Math.floor((total % 100)) ;
        //console.log(total);
        $scope.mainData.FD11PROP.FQTY_display = r + '-' + n + '-' + v;
       // console.log($scope.mainData.FD11PROP.FQTY);
    }

    $scope.calByTotal = function () {
        $.each($scope.mainData.List_AD11INV3,
           function (index) {
              
               $scope.calByPercent(index);
           });
    };
    $scope.calByPercent = function (index) {
        fullAmount = $scope.mainData.AD11INV1.FAMOUNT;
        percent = $scope.mainData.List_AD11INV3[index].FPPAY;
        result = fullAmount * percent / 100.00
        $scope.mainData.List_AD11INV3[index].FAPAY = result;
        $scope.calTotalAmount();
    };
    $scope.calByAmount = function (index) {
        fullAmount = $scope.mainData.AD11INV1.FAMOUNT;
        amount = $scope.mainData.List_AD11INV3[index].FAPAY;
        result = amount * 100.00 / fullAmount;
        $scope.mainData.List_AD11INV3[index].FPPAY = result;
        $scope.calTotalAmount();
    }

    $scope.calTotalAmount = function () {


        sumPercent = 0;
        sum = 0;
        $.each($scope.mainData.List_AD11INV3,
            function () {
                sumPercent += parseFloat(this.FPPAY) || 0;
                sum += parseFloat(this.FAPAY) || 0;
            });
        $scope.mainData.CalPercent = sumPercent;
        $scope.mainData.CalAmount = sum;
        $scope.mainData.RestPercent = 100.00 - sumPercent;
        $scope.mainData.RestAmount = $scope.mainData.AD11INV1.FAMOUNT - sum;
        //$scope.$apply;
    }
    function setCoreDivision() {
        url = ApiUrl_AutoComplete + '?dataSource=BD10DIVI&query=';
       
        $http(
        {
            method: 'GET',
            url: url,
        }).then(function successCallback(response) {
            $scope.coreDivision= response.data;

            $('select').trigger("chosen:updated");
        }, function errorCallback(response) {
            console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
        });
       
    }

    function validateAllInput(){
        console.log($scope.mainData);
        if ($scope.mainData.preCode === undefined || $scope.mainData.preCode.length < 5) {
            OpenDialogError('PreCode  ไม่ถูกต้อง')
            return false;
        }
        if ($scope.mainData.AD11INV1.FVOUNO === undefined || $scope.mainData.AD11INV1.FVOUNO.length < 5){
            OpenDialogError('เลขที่สัญญาไม่ถูกต้อง')
            return false;
        }
        if ($scope.mainData.AD11INV1.FDIVCODE === undefined ) {
            OpenDialogError('กรุณาเลือก DIVCODE')
            return false;
        }
        if ($scope.mainData.AD11INV1.FSUCODE === undefined) {
            OpenDialogError('กรุณาเลือก รหัสผู้ขาย')
            return false;
        }
        if ($scope.mainData.AD11INV1.FASSETNO === undefined) {
            OpenDialogError('กรุณาเลือก รหัสแปลงที่ดิน')
            return false;
        }
        if ($scope.mainData.FD11PROP=== undefined|| $scope.mainData.FD11PROP.FPCPIECE === undefined) {
            OpenDialogError('กรุณาเลือก เลขที่โฉนด')
            return false;
        }
        if ($scope.mainData.CalAmount === undefined || $scope.mainData.CalAmount==0) {
            OpenDialogError('กำหนดราคาไม่ถูกต้อง')
            return false;
        }
        if ($scope.mainData.List_AD11INV3 === undefined || $scope.mainData.List_AD11INV3.length<2) {
            OpenDialogError('กรุณากำหนดการแบ่งงวดจ่าย');
            return false;
        }
        for (i = 0; i < $scope.mainData.List_AD11INV3.length-1; i++) {
            if ($scope.mainData.List_AD11INV3[i].FPPAY === undefined || $scope.mainData.List_AD11INV3[i].FPPAY ==0) {
                OpenDialogError('กรุณาลบการแบ่งงวดจ่ายที่ไม่มีข้อมูลออกก่อนทำการบันทึก');
                return false;
            }
        }

        if ($scope.mainData.CalPercent === undefined || $scope.mainData.CalPercent != 100) {
            OpenDialogError('จำนวนเงินสุทธิกับราคาที่เดินรวมไม่เท่ากัน');
            return false;
        }

        console.log('val PASS');
        return true;
    }

  //  $scope.getData('A115070001');





    //*********************************************************************
    // auto complete SUCODE
    // ***********************************************************
  
    self.simulateQuery = false;
    //self.states = loadAllProducts($http);
    self.selectedItem =  null;
    self.searchText = null;
    self.querySearch = querySearch;
    self.selectedItemChange = selectedItemChange;
    self.searchTextChange = searchTextChange;

   
    function querySearch(query) {
        loadVendor(query);
        if (self.simulateQuery) {
           
            deferred = $q.defer();
            $timeout(function () { deferred.resolve(self.states); }, Math.random() * 1000, false);
            return deferred.promise;

        } else {
           return self.states
        }
   
      
  


        //console.log('querySearch' + query);
        //var results = query ? self.states.filter(createFilterFor(query)) : self.states;
       
        //if (self.simulateQuery) {
        //    deferred = $q.defer();
        //    $timeout(function () { deferred.resolve(results); }, Math.random() * 1000, false);
        //    return deferred.promise;

        //} else {
        //    return results;
        //}

    }
    function searchTextChange(text) {
        $log.info('Text changed to ' + text);
    }
    function selectedItemChange(item) {
        if (item == undefined) {
            $scope.mainData.AD11INV1.FSUCODE = null;
            $scope.mainData.AD01VEN1.FSUCODE = null;
            $scope.mainData.AD01VEN1.FSUNAME = null;
        } else {
            $log.info('Item changed to ' + JSON.stringify(item));
            $scope.mainData.AD11INV1.FSUCODE = item.value;
            $scope.mainData.AD01VEN1.FSUCODE = item.value;
            $scope.mainData.AD01VEN1.FSUNAME = item.FSUNAME;
           
        }
    }
    function loadVendor(query) {
        url = ApiUrl_AutoComplete + '?dataSource=AD01VEN1&query=' + escape(query)
        result = [];
        $http(
       {
           method: 'GET',
           url: url,
       }).then(function successCallback(response) {
           items = response.data;
          
           angular.forEach(items, function (item, key) {

               result.push(
               {
                   value: item.FSUCODE,
                   display: item.FSUCODE + '- ' + item.FSUNAME,
                   FSUNAME: item.FSUNAME,
                   object:item
               });
           });
           self.states = result;
           
       }, function errorCallback(response) {
           console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
       });
    }
    function loadAllProducts($http) {
        var allProducts = [];
        var url = '';
        var result = [];
        url = ApiUrl_AD01VEN1;
        $http(
        {
            method: 'GET',
            url: url,
        }).then(function successCallback(response) {
            allProducts = response.data;
            angular.forEach(allProducts, function (product, key) {

                result.push(
                {
                    value: product.FSUCODE,
                    display: product.FSUCODE + '- ' + product.FSUNAME,
                    FSUNAME: product.FSUNAME

                });
            });
        }, function errorCallback(response) {
            console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
        });
        return result;
    }
    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);

        return function filterFn(item) {
          
            found = ((angular.lowercase(item.value + ',' + item.FSUNAME)).indexOf(lowercaseQuery) > -1);
            //console.log(found);
            return found;
        };

    }
    //*********************************************************************
    // end auto complete SUCODE
    // ***********************************************************

    //*********************************************************************
    // auto complete ASSETNO
    // ***********************************************************

    //self.AssetNo_states = AssetNo_loadAllProducts($http);
    self.AssetNo_selectedItem = null;
    self.AssetNo_searchText = null;
    self.AssetNo_querySearch = AssetNo_querySearch;
    self.AssetNo_selectedItemChange = AssetNo_selectedItemChange;
    self.AssetNo_searchTextChange = AssetNo_searchTextChange;


    function AssetNo_querySearch(query) {
        console.log('AssetNo_querySearch' + query);
        var results = query ? self.AssetNo_states.filter(AssetNo_createFilterFor(query)) : self.AssetNo_states;
        //var deferred = $q.defer();
        //$timeout(function () { deferred.resolve(results); }, Math.random() * 1000, false);
        //return deferred.promise;

        if (self.simulateQuery) {
            deferred = $q.defer();
            $timeout(function () { deferred.resolve(results); }, Math.random() * 1000, false);
            return deferred.promise;
        } else {
            return results;
        }
    }
    function AssetNo_searchTextChange(text) {
        $log.info('Text changed to ' + text);
    }
    function AssetNo_selectedItemChange(item) {
       
        if (item == undefined) {
            $scope.mainData.AD11INV1.FASSETNO = null;
            $scope.mainData.FD11PROP = null;
        } else {
            $log.info('Item changed to ' );
            $log.info(item.obj);
            $scope.mainData.AD11INV1.FASSETNO = item.value;
            $scope.mainData.FD11PROP = item.obj;
            calAssetQty();
        }
    }

    function AssetNo_loadAllAsset($http,query) {
        
        url = ApiUrl_AutoComplete + '?dataSource=FD11PROP&query=' + escape(query);
        console.log(url);
        var allProducts = [];
        //var url = '';
        var result = [];
        url = url;
        $http(
        {
            method: 'GET',
            url: url,
        }).then(function successCallback(response) {
            allProducts = response.data;
            //console.log(allProducts);
            console.log(allProducts.length);
            angular.forEach(allProducts, function (product, key) {

                result.push(
                {
                    value: product.FASSETNO,
                    display: product.FASSETNO + '- ' + product.FASSETNM + '- ' + product.FPCPIECE,
                    obj: product

                });
            });
        }, function errorCallback(response) {
            console.log('Oops! Something went wrong while fetching the data. Status Code: ' + response.status + ' Status statusText: ' + response.statusText);
        });
        return result;
    }
    function AssetNo_createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);

        return function filterFn(item) {
          
            found = ((angular.lowercase(item.obj.FASSETNO + ',' + item.obj.FPCPIECE )).indexOf(lowercaseQuery) > -1);
            //console.log(found);
            return found;
        };

    }
    //*********************************************************************
    // end auto complete ASSETNO
    // ***********************************************************

  
  
});





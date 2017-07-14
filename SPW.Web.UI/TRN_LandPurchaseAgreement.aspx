<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TRN_LandPurchaseAgreement.aspx.vb" Inherits="SPW.Web.UI.TRN_LandPurchaseAgreement" EnableEventValidation="false" %>

<%@ Import Namespace="SPW.Helper" %>
<%@ Import Namespace="SPW.BLL" %>
<%@ Import Namespace="SPW.DAL" %>
<%@ Register Src="~/Usercontrol/AngularJSScript.ascx" TagPrefix="uc1" TagName="AngularJSScript" %>
<%@ Register Src="~/Usercontrol/DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <script src="//cdnjs.cloudflare.com/ajax/libs/angular.js/1.4.2/angular.js"></script>--%>
    <uc1:AngularJSScript runat="server" id="AngularJSScript" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:DatePicker runat="server" ID="DatePicker" />
    <div class="row form-control-container" ng-app="myApp">
        <div class="col-lg-12 col-md-12 col-sm-12" style="text-align: left;">
            <div id="page-wrapper">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12">
                            <ol class="breadcrumb">
                                <li>
                                    <i class="glyph-icon icon-globe"></i><a href="Main.aspx">
                                        <label><%=grtt("resHomePage") %></label>

                                    </a>
                                </li>
                                <li>
                                    <i class="glyph-icon icon-circle-o">
                                        <label><%=Me.GetMenuName() %></label>

                                    </i>
                                </li>
                            </ol>
                        </div>
                    </div>
                    <!-- result container -->
                    <div id="divResultsContainer" ng-controller="tableController" class="row" ng-show="$root.pageState=='LIST'">
                        <div class="col-lg-12 col-md-12 col-sm-12" style="margin-left: 2px;">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-10 col-sm-10">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <h3>
                                                            <label><%=Me.GetMenuName() %></label>
                                                        </h3>
                                                    </td>
                                                    <td style="text-align: right;">

                                                        <a ng-click="showInsertPage()" title="<%=grtt("resAddData") %>"
                                                            class="btn btn-info tooltip-button glyph-icon icon-plus <%=IIf(Me.GetPermission().isAdd, "", "hide") %>"></a>
                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">

                                    <div class="row ">
                                        <div class=" col-sm-12">
                                            <div class="row">
                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    <label><%=grtt("resSearch") %></label>
                                                                </div>
                                                                <div class="col-lg-10 col-md-12 col-sm-12">
                                                                    <input ng-model="searchQuery" class="form-control searchfilterbox"
                                                                        ng-keyup="$event.keyCode == 13 ? getSearchResult() : null" />
                                                                    <label id="resPleaseEnter" style="display: none;"><%=grtt("resPleaseEnter") %></label>

                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="col-lg-2 col-md-12 col-sm-12"></div>
                                                                <div class="col-lg-10 col-md-12 col-sm-12">

                                                                    <label class="font-red">(<%=grtt("resSearch") %> <%=grtt("resVouNo") %>,<%=grtt("resAssetNo") %>,<%=grtt("resPcPiece") %>)</label>


                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>
                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                    <a href="#" class="btn btn-info" ng-click="getSearchResult()">

                                                        <i class="glyph-icon icon-search"></i>

                                                        <%=grtt("resSearch") %>
                                                                      
                                                    </a>
                                                </div>



                                            </div>

                                        </div>

                                    </div>


                                    <div ng-show="searchResult.length>0">
                                        <div class="row text-right">
                                            <div class="col-md-4 float-right">
                                                <input ng-model="queryResult[queryBy]" class="form-control" /><select class="hidden" ng-model="queryBy" ng-init="queryBy='$'">
                                                    <option value="$"><%#grtt("resSelectAll") %></option>
                                                    <option value="FTRNNO"><%#grtt("resFTRNNO") %></option>
                                                </select>
                                            </div>
                                            <div class="col-md-2 float-right">
                                                <label><%#grtt("resSearchInResult") %></label>
                                            </div>
                                        </div>
                                        <div class="row" style="overflow-x: auto; overflow-y: hidden;">
                                            <table ng-table="searchResultTable" show-filter="true" class="table table-striped">
                                                <tr ng-repeat="s in finalSearchResult | filter:queryResult">
                                                    <td data-title="'<%#grtt("resEdit") %>'">
                                                        <a ng-href="#edit" ng-click="showEditPage(s.FVOUNO)" class="btn btn-info btn-xs glyph-icon icon-typicons-edit"></a>
                                                    </td>
                                                    <td data-title="'<%#grtt("resDelete") %>'" class="<%#IIf(Me.GetPermission().isDelete, "", "hide") %>">
                                                        <a ng-href="#delete"
                                                            ng-click="deleteData(s.FVOUNO,$index,s)"
                                                            class="btn btn-danger btn-xs glyph-icon icon-typicons-trash"></a>

                                                    </td>
                                                    <td data-title="'<%#grtt("resVouNo") %>'" sortable="'FVOUNO'">{{s.FVOUNO}}
                                                    </td>
                                                    <td data-title="'<%#grtt("resAssetNo") %>'" sortable="'FASSETNO'">{{s.FASSETNO}}
                                                    </td>
                                                    <td data-title="'<%#grtt("resPcPiece") %>'" sortable="'FPCPIECE'">{{s.FPCPIECE}}
                                                    </td>
                                                    <td data-title="'<%#grtt("resSuCode") %>'" sortable="'FSUCODE'">{{s.FSUCODE}}
                                                    </td>
                                                    <td data-title="'<%#grtt("resSuName") %>'" sortable="'FSUNAME'">{{s.FSUNAME}}
                                                    </td>
                                                    <td data-title="'<%#grtt("resVouDate") %>'" sortable="'FVOUDATE'">{{s.FVOUDATE| cDate}}
                                                    </td>
                                                    <td data-title="'<%#grtt("resApproveDate") %>'" sortable="'FMDATE'">{{s.FMDATE| cDate}}
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- edit container -->
                    <div id="divEditContainer" ng-controller="editPageController as eCtrl" class="row" ng-show="$root.pageState=='NEW'||$root.pageState=='EDIT'">

                        <div>
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <label><%=grtt("resEditHeading") %></label>
                                    <div class="float-right">
                                        <a ng-click="save()"
                                            ng-class="{'hide':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')}"
                                            class="btn btn-info tooltip-button glyph-icon icon-save"></a>
                                        <a ng-click="$root.pageState='LIST'" class="btn remove-border btn-danger tooltip-button glyph-icon icon-close"></a>

                                    </div>
                                </div>
                                <!--Master สัญญาซื้อขายที่ดิน ad11inv1-->
                                <div class="divRowPadding"
                                    ng-class="{'disabled':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')||('False'=='<%=Me.GetPermission().isAdd %>'&&$root.pageState=='NEW')}">
                                    <div class="row">

                                        <div class="col-md-1">
                                            <label><%=grtt("resPreCode") %></label>
                                        </div>
                                        <div class="col-md-3">
                                            <input ng-model="mainData.preCode" name="preCode" required readonly />
                                            <div ng-messages="form1.preCode.$error">
                                                <div ng-message="required">
                                                    <%=grtt("resPreCodeIsRequired") %>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-1">

                                
                                            <label class="after-asterisk" ><%=grtt("resDivCode") %></label>
                                         </div>
                                        <div class="col-md-1">
                                            <input ng-model="mainData.AD11INV1.FDIVCODE" maxlength="2" readonly style="width: 60px;" />
                                        </div>
                                        <div class="col-md-6">


                                            <select ng-model="mainData.AD11INV1.FDIVCODE" ng-options="item.FDIVCODE as item.FDIVNAME for item in coreDivision"
                                                class="chosen-select"
                                                style="width: 200px;">
                                                <option value="" class="hidden"><%=grtt("resPleaseSelect") %></option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-1">
                                            <label class="after-asterisk"><%=grtt("resVouNo") %></label>
                                        </div>
                                        <div class="col-md-3">
                                            <input ng-model="mainData.AD11INV1.FVOUNO" maxlength="18" ng-readonly="$root.pageState!='NEW'" />
                                        </div>
                                        <div class="col-md-1">
                                            <label><%=grtt("resVouDate") %></label>
                                        </div>
                                        <div class="col-md-3">

                                            <input ng-model="mainData.AD11INV1.FVOUDATE" md-placeholder="Enter date" jqdatepicker />
                                        </div>
                                        <div class="col-md-1">
                                            <label><%=grtt("resApproveDate") %></label>
                                        </div>
                                        <div class="col-md-3">
                                            <input ng-model="mainData.AD11INV1.FMDATE" md-placeholder="Enter date" jqdatepicker />
                                        </div>
                                    </div>

                                    <!--Ref ผู้ขาย ad01ven1 -->
                                    <div class="row">
                                        <div class="col-md-1">
                                      <label class="after-asterisk" ><%=grtt("resSuCode") %></label>
                                            <%--    <input  ng-model="mainData.AD11INV1.FSUCODE"/>--%>
                                        </div>
                                        <div class="col-md-3">
                                            <md-autocomplete flex=""
                                                md-input-name="autocompleteField"
                                                md-input-minlength="0"
                                                md-input-maxlength="18"
                                                md-min-length="0"
                                                md-no-cache="true"
                                                md-search-text-change="eCtrl.searchTextChange(eCtrl.searchText)"
                                                md-selected-item-change="eCtrl.selectedItemChange(item)"
                                                md-selected-item="eCtrl.selectedItem"
                                                md-search-text="eCtrl.searchText"
                                                md-items="item in eCtrl.querySearch(eCtrl.searchText)"
                                                md-item-text="item.value"
                                                md-require-match="">
          <md-item-template>
            <span md-highlight-text="eCtrl.searchText">{{item.display}}</span>
          </md-item-template>
        </md-autocomplete>

                                            <!--
Copyright 2016 Google Inc. All Rights Reserved. 
Use of this source code is governed by an MIT-style license that can be foundin the LICENSE file at http://material.angularjs.org/HEAD/license.
-->

                                        </div>
                                        <div class="col-md-1">
                                            <label><%=grtt("resSuName") %></label>
                                        </div>
                                        <div class="col-md-7">
                                            <input ng-model="mainData.AD01VEN1.FSUNAME" maxlength="200" readonly />
                                        </div>
                                    </div>
                                    <!--Ref ข้อมูลที่เดิน fd11prop -->
                                    <div class="row">
                                        <div class="col-md-1">

                                            <label class="after-asterisk"><%=grtt("resAssetNo") %></label>
                                            <%--  
                                            <input  ng-model="mainData.AD11INV1.FASSETNO"/>
                                            --%>
                                        </div>
                                        <div class="col-md-3">

                                            <md-autocomplete flex=""
                                                md-input-name="AssetNo_autocompleteField"
                                                md-input-minlength="0"
                                                md-input-maxlength="18"
                                                md-min-length="0"
                                                md-no-cache="true"
                                                md-search-text-change="eCtrl.AssetNo_searchTextChange(eCtrl.AssetNo_searchText)"
                                                md-selected-item-change="eCtrl.AssetNo_selectedItemChange(item)"
                                                md-selected-item="eCtrl.AssetNo_selectedItem"
                                                md-search-text="eCtrl.AssetNo_searchText"
                                                md-items="item in eCtrl.AssetNo_querySearch(eCtrl.AssetNo_searchText)"
                                                md-item-text="item.value"
                                                md-require-match="">
          <md-item-template>
            <span md-highlight-text="eCtrl.AssetNo_searchText">{{item.obj.FASSETNO}}, {{item.obj.FPCPIECE}} </span>
          </md-item-template>


        </md-autocomplete>





                                        </div>
                                        <div class="col-md-1">
                                            <label><%=grtt("resAssetName") %></label>
                                        </div>
                                        <div class="col-md-3">
                                            <input ng-model="mainData.FD11PROP.FASSETNM" maxlength="120" readonly />
                                        </div>
                                        <div class="col-md-1">
                                            <label><%=grtt("resAssetQty") %></label>
                                        </div>
                                        <div class="col-md-3">
                                            <input ng-model="mainData.FD11PROP.FQTY_display" readonly />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-1">

                                            <label  class="after-asterisk" ><%=grtt("resAssetPcPiece") %></label>
                                            <%--    <input  ng-model="mainData.FD11PROP.FPCPIECE"/>
                                            --%>
                                        </div>
                                        <div class="col-md-3">

                                            <md-autocomplete flex=""
                                                md-input-name="AssetNo_autocompleteField"
                                                md-input-minlength="0"
                                                md-input-maxlength="18"
                                                md-min-length="0"
                                                md-no-cache="true"
                                                md-search-text-change="eCtrl.AssetNo_searchTextChange(eCtrl.AssetPc_searchText)"
                                                md-selected-item-change="eCtrl.AssetNo_selectedItemChange(item)"
                                                md-selected-item="eCtrl.AssetNo_selectedItem"
                                                md-search-text="eCtrl.AssetPc_searchText"
                                                md-items="item in eCtrl.AssetNo_querySearch(eCtrl.AssetPc_searchText)"
                                                md-item-text="item.obj.FPCPIECE"
                                                md-require-match="">
          <md-item-template>
            <span md-highlight-text="eCtrl.AssetNo_searchText">{{item.obj.FPCPIECE}}, {{item.obj.FASSETNO}}</span>
          </md-item-template>


        </md-autocomplete>





                                        </div>
                                        <div class="col-md-1">
                                            <label><%=grtt("resAssetPcLndNo") %></label>
                                        </div>
                                        <div class="col-md-3">
                                            <input ng-model="mainData.FD11PROP.FPCLNDNO" readonly />
                                        </div>
                                        <div class="col-md-1">
                                            <label><%=grtt("resAssetInvoiceDate") %></label>
                                        </div>
                                        <div class="col-md-3">

                                            <input ng-model="mainData.AD11INV1.FINVDATE" jqdatepicker placeholder="Enter date" />
                                        </div>
                                    </div>

                                </div>

                                <!--Ref คำนวนผลรวมของ ad11inv3-->
                                <div class="row" ng-class="{'disabled':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')||('False'=='<%=Me.GetPermission().isAdd %>'&&$root.pageState=='NEW')}">
                                    <fieldset class="col-md-12">
                                        <legend><%=grtt("resSummary") %></legend>

                                        <div class="col-md-12">
                                            <table class="caltable">
                                                <tr>
                                                    <td>
                                                        <label><%=grtt("resTotalAssetPrice") %></label>
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="mainData.AD11INV1.FAMOUNT" ng-change="calByTotal()" awnum="price" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <%=grtt("resPayPercent") %>
                                                    </td>
                                                    <td>
                                                        <%=grtt("resPayAmount") %>
                                                    </td>

                                                </tr>
                                                <tr ng-class="classCalculateRestAmount()">
                                                    <td>
                                                        <label><%=grtt("resRestAmount") %></label>
                                                    </td>
                                                    <td>
                                                        <input ng-model="mainData.RestPercent" readonly class="percent" type="text" awnum="price" />
                                                    </td>

                                                    <td>
                                                        <input ng-model="mainData.RestAmount" readonly type="text" awnum="price" />
                                                    </td>
                                                </tr>
                                                <tr ng-class="classCalculateTotalAmount()">
                                                    <td>
                                                        <label><%=grtt("resTotalAmount") %></label></td>
                                                    <td>
                                                        <input ng-model="mainData.CalPercent" readonly class="percent" type="text" awnum="price" /></td>
                                                    <td>
                                                        <input ng-model="mainData.CalAmount" readonly type="text" awnum="price" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </fieldset>

                                </div>



                                <!--Ref งวดจ่าย ad11inv3-->

                                <div class="row" style="overflow-x: auto; overflow-y: hidden;" ng-class="{'disabled':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')||('False'=='<%=Me.GetPermission().isAdd %>'&&$root.pageState=='NEW')}">
                                    <div class="col-md-12 detailTable listInv3">
                                        <table class="table table-striped">
                                            <tr>
                                                <th style="width: 70px;"><%=grtt("resAction") %></th>
                                                <th style="width: 50px;"><%=grtt("resNo.") %></th>
                                                <th style="width: 60px;"><%=grtt("resPayPercent") %></th>
                                                <th style="width: 160px;"><%=grtt("resPayAmount") %></th>
                                                <th style="width: 145px;"><%=grtt("resDueDate") %></th>
                                                <th><%=grtt("resNotation") %></th>
                                            </tr>
                                            <tr ng-repeat="s in mainData.List_AD11INV3" ng-click="$last && addNewRow()">
                                                <td>
                                                    <a ng-click="removeRow($index)"
                                                        ng-show="!$last"
                                                        class="btn btn-danger btn-xs glyph-icon icon-typicons-trash"></a>
                                                </td>
                                                <td>{{$index+1}}
                                                </td>
                                                <td>
                                                    <input type="text" ng-change="calByPercent($index)" ng-model="s.FPPAY" class="percent" awnum="price" />
                                                </td>
                                                <td>
                                                    <input type="text" ng-change="calByAmount($index)" ng-model="s.FAPAY" awnum="price" />
                                                </td>
                                                <td>

                                                    <input ng-model="s.FDUEDATE" jqdatepicker placeholder="Enter date" />
                                                </td>
                                                <td>
                                                    <input ng-model="s.FDESC" maxlength="80" />
                                                </td>

                                            </tr>
                                        </table>
                                    </div>

                                </div>

                            </div>



                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade .bs-example-modal-sm" id="overlay" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000; opacity: 0.6;">
            <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #000; font-size: 36px; left: 40%; top: 30%;">
                <img src="image/loading.gif" alt="Loading ..." style="height: 200px" /></span>
        </div>
    </div>


    <%-- <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="Script/jquery-ui.js"></script>--%>
    <script type="text/javascript">
        var rootHost = window.location.origin + '<%=Request.ApplicationPath %>';
        var culture = '<%= CultureDate%>';
        var systemDateString = '<%=Me.ToSystemDateString(DateTime.Now)%>';
    </script>
    <script src="Scripts_ngapp/TRN_LandPurchaseAgreement.js?v=<%=Me.assetVersion %>"></script>
    <script type="text/javascript">
        function showOverlay() {
            $("#overlay").modal();
        }
        function closeOverlay() {
            $("#overlay").modal('hide');
        }
        function SaveSuccess() {
            OpenDialogSuccess('<%=grtt("resSaveSuccess") %>')
        }
        function SaveFail() {
            OpenDialogSuccess('<%=grtt("resSaveFail") %>')
        }
        function OpenDialogError(Msg) {
            noty({
                text: '<i class="glyph-icon icon-times-circle mrg5R"></i> ' + Msg,
                type: 'error',
                dismissQueue: true,
                theme: 'agileUI',
                layout: 'center'
            });
        }
        function OpenDialogSuccess(Msg) {
            noty({
                text: '<i class="glyph-icon icon-check-circle mrg5R"></i> ' + Msg,
                type: 'success',
                dismissQueue: true,
                theme: 'agileUI',
                layout: 'center'
            });
        }
        function genEditIcon(menuId) {
            return ' <a class="btn btn-edit glyph-icon icon-edit" href="javascript:EditData(\'"' + menuId + '"\');"></a>';
        }


    </script>
</asp:Content>

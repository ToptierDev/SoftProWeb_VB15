<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TRN_LandUtilitiesManage.aspx.vb" Inherits="SPW.Web.UI.TRN_LandUtilitiesManage" EnableEventValidation="false" %>

<%@ Register Src="~/Usercontrol/AngularJSScript.ascx" TagPrefix="uc1" TagName="AngularJSScript" %>
<%@ Register Src="~/Usercontrol/FixedTableHeader.ascx" TagPrefix="uc1" TagName="FixedTableHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <uc1:AngularJSScript runat="server" ID="AngularJSScript" />

    <style>
        body {
            min-width: 1024px;
        }

        #page-content {
            /*margin-left: 240px;*/
            padding-left: 0px;
        }

        /*.detailTable {
            position: relative;
        }

        #detailtable {
            table-layout: fixed;
            width: 1000px;
        }

        #detailtable thead {
            position: absolute;
            margin-top: -42px;
            z-index: 1;
            width: 1000px;
        }

        #detailtable tbody {
            position: absolute;
            width: 1000px;
            overflow-x: overlay;
        }


        #detailtable thead th {
            border-left: #e4e4e4 1px solid;
            padding: 0;
            font-size: 12px;
            font-weight: lighter;
            word-break: break-word;
        }

        #detailtable {
            margin-top: 41px;
        }*/

      

           .detailTable tbody > tr > th
        , .detailTable tr > th
        ,.detailTable tbody > tr > td
        , .detailTable tr > td
        , .detailTable thead > tr > td 
        ,.detailTable2 tbody > tr > th
        , .detailTable2 tr > th
        ,.detailTable2 tbody > tr > td
        , .detailTable2 tr > td
        , .detailTable2 thead > tr > td 
        {
            padding: 1px;
        }

        .detailTable tbody > tr > th
        , .detailTable tr > th
        , .detailTable tr > td 
        ,.detailTable2 tbody > tr > th
        , .detailTable2 tr > th
        , .detailTable2 tr > td 
        {
            border: solid 1px;
        }

       

        .spw-landutil-container input {
            width: 100%;
        }

        input[type=number] {
            text-align: right;
        }

        /*.spw-landutil-container td {
            padding: 1px !important;
        }*/

        .md-icon-button + .md-datepicker-input-container {
            /*margin-left: 0px !important;*/
        }

        .md-button.md-icon-button {
            /*padding: 0px !important;*/
            /*width: 0px !important;*/
            /*height: 0px !important;*/
        }

        .md-button {
            min-height: 0px !important;
        }

        .md-datepicker {
            vertical-align: top !important;
        }

        .detailTable input[type=text].form-control, input[type=number].form-control {
            padding: 1px 1px;
        }

        .FASSETNO {
            width: 80px !important;
        }

        .FPCPIECE {
            width: 90px !important;
        }

        .FPCLNDNO {
            width: 75px !important;
        }

        .FPCWIDTH {
            width: 90px !important;
        }

        .FPCBETWEEN {
            width: 100px !important;
        }

        .FPCBOOK {
            width: 50px !important;
        }

        .FPCPAGE {
            width: 50px !important;
        }

        .FQTY {
            width: 60px !important;
        }

        .FSERNO {
            width: 100px !important;
        }

        .FMORTGYN {
            width: 80px !important;
        }

        .FASSPRCA {
            width: 80px !important;
            text-align:right;
        }

        .e_FPDCODE {
            width: 100px !important;
        }

        .FPCINST {
            width: 60px !important;
        }

        .FPCNOTE {
            width: 80px !important;
        }

        .FAREAOUT {
            width: 80px !important;
            text-align:right;
        }

        .FAREAIN {
            width: 100px !important;
            text-align:right;
        }

        .FAREAPARK {
            width: 100px !important;
            text-align:right;
        }

        .FPWIEGHAREA {
            width: 140px !important;
            text-align:right;
        }

        .FPAREAOUT {
            width: 140px !important;
            text-align:right;
        }

        .FPAREAIN {
            width: 140px !important;
            text-align:right;
        }

        .FPAREAPARK {
            width: 140px !important;
            text-align:right;
        }

        .FHBKDATE {
            width: 120px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row spw-landutil-container" ng-app="myApp" ng-controller="mainController">
        <div class="col-lg-12 col-md-12 col-sm-12">
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
                    <!-- main detail -->
                    <div class="col-lg-12 col-md-12 col-sm-12 form-control-container">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <div class="row">
                                    <h3 class="col-lg-10 col-md-10 col-sm-10">
                                        <%--<%=grtt("resProjectData") %>--%>
                                        <%=Me.GetMenuName() %>
                                    </h3>
                                    <div class="float-right">
                                        <a ng-hide="$root.pageState!='LIST'"
                                            ng-click="showAddPage()"
                                            title=""
                                            class="btn btn-info tooltip-button glyph-icon icon-plus <%=IIf(Me.GetPermission().isAdd, "", "hide") %>" data-original-title="<%=grtt("resAddData") %>"></a>
                                        
                                        <a ng-hide="$root.pageState=='LIST'"
                                            ng-click="save()"
                                            ng-class="{'hide':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')||('False'=='<%=Me.GetPermission().isAdd %>'&&$root.pageState=='NEW')}"
                                            title=""
                                            class="btn btn-info tooltip-button glyph-icon icon-save" data-original-title="<%=grtt("resSave") %>"></a>
                                        <a ng-hide="$root.pageState=='LIST'"
                                            ng-click="closePageEditAdd()"
                                            title=""
                                            class="btn btn-danger tooltip-button glyph-icon icon-close" data-original-title="<%=grtt("resCancel") %>"></a>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <!-- main row1  -->
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="row">
                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <%=grtt("resFREPRJNM") %>
                                                    </div>
                                                    <div ng-class="{'col-sm-5': $root.pageState=='LIST' , 'col-sm-10': $root.pageState!='LIST'  }">
                                                        <%--<input ng-model="txtSearchProject"
                                                            ng-keyup="$event.keyCode == 13 ? getData() : null"
                                                            autocomplete="off"
                                                            name="srch-term"
                                                            id="srch-term"
                                                            type="text"
                                                            ng-enit="''"
                                                            class="form-control" />--%>

                                                        <select id="selProject" ng-model="txtSearchProject"
                                                            ng-disabled="$root.pageState!='LIST'"
                                                            ng-change="bindProject()"
                                                            class="chosen-select disabled-aslabel disabled-aslargelabel">
                                                            <option value="" class="hidden"><%=grtt("resPleaseChooseProject") %></option>
                                                            <option ng-repeat="c in ED01PROJ" value="{{c.FREPRJNO}}">{{c.FREPRJNM}}</option>
                                                        </select>
                                                    <label id="resPleaseSelect" style="display: none;"><%=grtt("resPleaseSelect") %></label>
                                                    </div>
                                                       <div class="col-sm-2" ng-show="$root.pageState=='LIST'">
                                                        <%=grtt("resFPCPIECE") %>
                                                    </div>
                                                    <div class="col-lg-3 col-sm-3" ng-show="$root.pageState=='LIST'">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-sm-6">
                                                                <input type="text" ng-model="txtSearchFPCPIECEFrom"
                                                                    class="form-control" />
                                                            </div>
                                                            <div class="col-lg-6 col-sm-6">
                                                                <input type="text" ng-model="txtSearchFPCPIECETo"
                                                                    class="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                      
                                                  <div class=" col-sm-offset-7 col-sm-2" ng-show="$root.pageState=='LIST'">
                                                        <%=grtt("resFSERNO") %>
                                                    </div>
                                                    <div class="col-lg-3 col-sm-3" ng-show="$root.pageState=='LIST'">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-sm-6">
                                                                <input type="text" ng-model="txtSearchUnitFrom"
                                                                    class="form-control" />
                                                            </div>
                                                            <div class="col-lg-6 col-sm-6">
                                                                <input type="text" ng-model="txtSearchUnitTo"
                                                                    class="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <br />
                                                <div ng-show="$root.pageState=='LIST'">
                                                    <!-- main row2  -->
                                                    <div class="row">
                                                        <div class="col-sm-2">
                                                            <%=grtt("resFRELOCAT1") %>
                                                        </div>
                                                        <div class="col-sm-5">
                                                            <input type="text"
                                                                ng-model="mainData.ED01PROJ.FRELOCAT1"
                                                                class="form-control"
                                                                readonly />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <%=grtt("resFTOTAREA") %>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <input type="text"
                                                                ng-model="mainData.ED01PROJ.FTOTAREA"
                                                                class="form-control"
                                                                readonly />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <!-- main row3  -->
                                                    <div class="row">
                                                        <div class="col-sm-offset-2 col-sm-5">
                                                            <input type="text"
                                                                ng-model="mainData.ED01PROJ.FRELOCAT2"
                                                                class="form-control"
                                                                readonly />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <%=grtt("resFNOOFLAND") %>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <input type="text"
                                                                ng-model="mainData.ED01PROJ.FNOOFLAND"
                                                                class="form-control"
                                                                readonly />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <!-- main row4  -->
                                                    <div class="row">
                                                        <div class="col-sm-offset-2 col-sm-5">
                                                            <input type="text"
                                                                ng-model="mainData.ED01PROJ.FRELOCAT3"
                                                                class="form-control"
                                                                readonly />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <%=grtt("resFLANDNO") %>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <input type="text"
                                                                ng-model="mainData.ED01PROJ.FLANDNO"
                                                                class="form-control"
                                                                readonly />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;" ng-show="$root.pageState=='LIST'">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <a href="#" ng-click="getData()" class="btn   btn-info">
                                                               
                                                                    <i class="glyph-icon icon-search"></i>
                                                               
                                                                    <%=grtt("resSearch") %>
                                                              
                                                            </a>
                                                    </div>
                                                </div>
                                          
                                            </div>
                                        </div>
                                        <div class="row" ng-show="$root.pageState=='LIST' && mainData.List_vwFD11PROP.length>0">
                                                     <br />
                                                     <div class="col-lg-offset-1 col-md-offset-1 
                                                     col-lg-11 col-md-11 col-sm-12">
                                                            <a ng-href="#edit" ng-click="showEditPage()" class="btn btn-info">
                                                               
                                                                    <i class="glyph-icon icon-edit"></i>
                                                              
                                                                    <%=grtt("resEdit") %>
                                                              
                                                            </a>
                                            
                                                            <a ng-href="#delete" ng-click="deleteAll()" class="btn  btn-danger <%=IIf(Me.GetPermission().isDelete, "", "hide") %>">
                                                                    <i class="glyph-icon icon-trash"></i> <%=grtt("resDelete") %>
                                                            </a>
                                                    </div>
                                       

                                        </div>


                                    </div>
                                </div>
                                <br />
                                <!-- main row5 running โฉนด -->
                                <div>
                                    <div ng-show="$root.pageState=='NEW'">
                                        <div class="row">
                                            <h3 class="col-lg-12 col-md-12 col-sm-12 panel-heading"><span><%=grtt("resAddFNOOFLAND") %></span></h3>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-1 col-md-1 col-sm-1">
                                                <%=grtt("resStartFPCPIECE") %>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <input type="text"
                                                    ng-model="txtStart"
                                                    ng-enit="''"
                                                    class="form-control" 
                                                    awnum ="price"
                                                    />


                                            </div>
                                            <div class="col-lg-1 col-md-1 col-sm-1">
                                                <%=grtt("resAddCount") %>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                <input type="text"
                                                    ng-model="txtCount"
                                                    ng-enit="''"
                                                    awnum ="price"
                                                    class="form-control" />
                                            </div>
                                            <div class="col-lg-2 col-md-2 col-sm-2">
                                                <a href="#" ng-click="generateRows()" class="btn btn-info"><%=grtt("resAddFASSETNO") %></a>
                                            </div>
                                        </div>
                                    </div>
                                    <!--ตารางโฉนด-->
                                    <br />
                                    <div class="row" ng-show="mainData.List_vwFD11PROP.length>0 && $root.pageState=='LIST'">
                                        <div class="row ">
                                            <div class="col-md-4 float-right">
                                                <input ng-model="queryResult[queryBy]" class="form-control " />
                                                
                                            </div>
                                            <div class="col-md-2 float-right">
                                                <select  class="hidden" ng-model="queryBy" ng-init="queryBy='$'">
                                                    <option value="$"><%#grtt("resSearchAll") %></option>
                                                    <option value="FASSETNO"><%#grtt("resFASSETNO") %></option>
                                                </select>
                                                <label><%#grtt("resSearchInResult") %></label>
                                            </div>
                                        </div>
                                        <div class="col-lg-12 col-md-12 col-sm-12 detailTable  listInv3">
                                            <div class="detailTable-fixheader">
                                                <table class="table table-striped fixed-header">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                <input type="checkbox"
                                                                    ng-change="List_vwFD11PROP_checkAllRow()"
                                                                    ng-model="checkAll"
                                                                    ng-init="checkAll=false" />
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFASSETNO") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCPIECE") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCLNDNO") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCWIDTH") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCBETWEEN") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCBOOK") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCPAGE") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFQTY") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFSERNO") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFMORTGYN") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFASSPRCA") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPDCODE") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFADDRNO") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCNOTE") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFAREAOUT") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFAREAIN") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFAREAPARK") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPWIEGHAREA") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPAREAOUT") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPAREAIN") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPAREAPARK") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFHBKDATE") %>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr ng-repeat="s in mainData.List_vwFD11PROP | filter:queryResult"
                                                            ng-class="{'danger': s.isRowChecked}">
                                                            <td style="padding: 10px;">
                                                                <input type="checkbox" ng-model="s.isRowChecked" />
                                                            </td>
                                                            <td>
                                                                <label class="FASSETNO">{{s.FASSETNO}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPCPIECE">{{s.FPCPIECE}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPCLNDNO">{{s.FPCLNDNO}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPCWIDTH">{{s.FPCWIDTH}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPCBETWEEN">{{s.FPCBETWEEN}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPCBOOK">{{s.FPCBOOK}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPCPAGE">{{s.FPCPAGE}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FQTY text-right">{{s.FQTY}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FSERNO">{{s.FSERNO}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FMORTGYN">{{s.FMORTGYN}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FASSPRCA">{{s.FASSPRCA |awnum:'price'}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="e_FPDCODE">{{s.e_FPDCODE}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPCINST">{{s.FPCINST}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPCNOTE">{{s.FPCNOTE}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FAREAOUT text-right">{{s.FAREAOUT|awnum:'price'}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FAREAIN text-right">{{s.FAREAIN|awnum:'price'}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FAREAPARK text-right">{{s.FAREAPARK|awnum:'price'}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPWIEGHAREA text-right">{{s.FPWIEGHAREA|awnum:'price'}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPAREAOUT text-right">{{s.FPAREAOUT|awnum:'price'}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPAREAIN text-right">{{s.FPAREAIN|awnum:'price'}}</label>
                                                            </td>
                                                            <td>
                                                                <label class="FPAREAPARK">{{s.FPAREAPARK|awnum:'price'}}</label>
                                                            </td>
                                                            <td>
                                                                <label style="width: 120px;">{{s.FHBKDATE|cDate}}</label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                     <datalist id="DATASOURCE_ED03UNIT">
    <option ng-repeat="ed in DATASOURCE_ED03UNIT" value="{{ed.FSERIALNO}}">{{ed.FSERIALNO}} {{ed.FPDCODE}}</option>
  </datalist>
                                    <div class="row" ng-show="mainData.List_vwFD11PROP_EDITINGROW.length>0 && $root.pageState!='LIST'">
                                        <div class="col-lg-12 col-md-12 col-sm-12 detailTable2 bg-transition listInv3">
                                            <%--<div class="wrapper1">
                                                <div class="div1"></div>
                                            </div>--%>
                                            <div class="detailTable-fixheader2">
                                                <table id="detailtable" 
                                                    class="table table-striped fixed-header2">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 30px;" class="<%=IIf(Me.GetPermission().isDelete, "", "hide") %>">
                                                                <%=grtt("resDeleteButton") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFASSETNO") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCPIECE") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCLNDNO") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCWIDTH") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCBETWEEN") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCBOOK") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCPAGE") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFQTY") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFSERNO") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFMORTGYN") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFASSPRCA") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPDCODE") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFADDRNO") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPCNOTE") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFAREAOUT") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFAREAIN") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFAREAPARK") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPWIEGHAREA") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPAREAOUT") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPAREAIN") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFPAREAPARK") %>
                                                            </th>
                                                            <th>
                                                                <%=grtt("resFHBKDATE") %>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody 
                                                    ng-class="{'disabled':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')||('False'=='<%=Me.GetPermission().isAdd %>'&&$root.pageState=='NEW')}">
                                                        <tr id="row_{{$index}}" class="<%=IIf(Me.GetPermission().isDelete, "", "hide") %>"
                                                            ng-class="{selected: $index === selectedIndex}"
                                                            ng-repeat="e in mainData.List_vwFD11PROP_EDITINGROW"
                                                            ng-click="setSelected($index);$root.pageState=='NEW' && $last && addNewRowDataDefualt()">
                                                            <td style="text-align: center; width: 30px;">
                                                                <a ng-href="#delete"
                                                                    ng-click="delete($index)"
                                                                    ng-hide="$root.pageState=='NEW' && $last"
                                                                    class="btn btn-danger btn-xs glyph-icon icon-typicons-trash"></a>
                                                            </td>
                                                            <td>
                                                                <input type="text" readonly
                                                                    ng-model="e.FASSETNO"
                                                                    ng-change="change($index);"
                                                                    class="form-control FASSETNO"
                                                                    maxlength="10" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-init="e.FPCPIECE_OLD=e.FPCPIECE"
                                                                    ng-blur="(!$last ||$root.pageState=='EDIT') && setFPCPIECE($index,$event);"
                                                                    ng-change="change($index)"
                                                                    ng-model="e.FPCPIECE"
                                                                    class="form-control FPCPIECE"
                                                                    maxlength="10" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FPCLNDNO"
                                                                    class="form-control FPCLNDNO"
                                                                    maxlength="15" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FPCWIDTH"
                                                                    class="form-control FPCWIDTH"
                                                                    maxlength="6" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FPCBETWEEN"
                                                                    class="form-control FPCBETWEEN"
                                                                    maxlength="35" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FPCBOOK"
                                                                    class="form-control FPCBOOK"
                                                                    maxlength="10" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FPCPAGE"
                                                                    class="form-control FPCPAGE"
                                                                    maxlength="10" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FQTY"
                                                                    class="form-control text-right FQTY"
                                                                    onkeypress="return Check_Key_Decimal(this,event)" />
                                                            </td>
                                                            <td>
                                                              
                                                                 <input type="text" list ="DATASOURCE_ED03UNIT"
                                                                             ng-init="e.FSERNO_OLD=e.FSERNO"
                                                                    ng-blur="chkDup($index,$event);"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FSERNO"
                                                                    class="form-control FSERNO"
                                                                    maxlength="28"  />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FMORTGYN"
                                                                    class="form-control FMORTGYN"
                                                                    maxlength="3" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FASSPRCA"
                                                                    class="form-control FASSPRCA"
                                                    awnum ="price" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-init="e.e_FPDCODE_OLD=e.e_FPDCODE"
                                                                    ng-model="e.e_FPDCODE"
                                                                    readonly
                                                                    class="form-control e_FPDCODE" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-init="e.FPCINST_OLD=e.FPCINST"
                                                                    ng-model="e.FPCINST"
                                                                    readonly
                                                                    class="form-control FPCINST"
                                                                    maxlength="50" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FPCNOTE"
                                                                    class="form-control FPCNOTE" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FAREAOUT"
                                                                    class="form-control text-right FAREAOUT"
                                                    awnum ="price" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FAREAIN"
                                                                    class="form-control text-right FAREAIN"
                                                    awnum ="price" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FAREAPARK"
                                                                    class="form-control text-right FAREAPARK"
                                                    awnum ="price" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FPWIEGHAREA"
                                                                    class="form-control text-right FPWIEGHAREA"
                                                    awnum ="price" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-focus="focus($index,$event);"
                                                                    ng-blur="blur($index,$event);"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FPAREAOUT"
                                                                    class="form-control text-right FPAREAOUT"
                                                    awnum ="price" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FPAREAIN"
                                                                    class="form-control text-right FPAREAIN"
                                                    awnum ="price" />
                                                            </td>
                                                            <td>
                                                                <input type="text"
                                                                    ng-change="change($index);"
                                                                    ng-model="e.FPAREAPARK"
                                                                    class="form-control text-right FPAREAPARK"
                                                    awnum ="price"/>
                                                            </td>
                                                            <td>
                                                             
                                            <input ng-model="e.FHBKDATE" ng-change="change($index);" placeholder="Enter date" jqdatepicker class="FHBKDATE"/>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-heading">
                                <div class="row">
                                    <h3 class="col-lg-10 col-md-10 col-sm-10">
                                     &nbsp;
                                    </h3>
                                    <div class="float-right">
                                        <a ng-hide="$root.pageState!='LIST'"
                                            ng-click="showAddPage()"
                                            title=""
                                            class="btn btn-info tooltip-button glyph-icon icon-plus <%=IIf(Me.GetPermission().isAdd, "", "hide") %>" data-original-title="<%=grtt("resAddData") %>"></a>

                                        <a ng-hide="$root.pageState=='LIST'"
                                            ng-click="save()"
                                            title=""
                                            ng-class="{'hide':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')||('False'=='<%=Me.GetPermission().isAdd %>'&&$root.pageState=='NEW')}"
                                            class="btn btn-info tooltip-button glyph-icon icon-save" data-original-title="<%=grtt("resSave") %>"></a>
                                        <a ng-hide="$root.pageState=='LIST'"
                                            ng-click="closePageEditAdd()"
                                            title=""
                                            class="btn btn-danger tooltip-button glyph-icon icon-close" data-original-title="<%=grtt("resCancel") %>"></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade .bs-example-modal-sm" id="DeleteModal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="H3">
                        <%=GetResource("msg_header_delete", "MSG", "1") %></h4>
                </div>
                <div class="modal-body">
                    <%=Me.GetResource("msg_body_delete", "MSG", "1") %>
                </div>
                <div class="modal-footer">
                    <button id="btnDeleteConfrim" runat="server" type="button" class="btn btn-danger" onclick="javascript:CallDeleteData();"></button>
                    <button id="btnCancelConfrim" runat="server" type="button" class="btn btn-default" data-dismiss="modal"></button>
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
    <script type="text/javascript">
        var rootHost = window.location.origin + '<%=Request.ApplicationPath %>';
        var culture = '<%= CultureDate%>';
        var systemDateString = '<%=Me.ToSystemDateString(DateTime.Now)%>';
        var currentuser = '<%= CurrentUser.UserID%>';
    </script>
    <script src="Scripts_ngapp/TRN_LandUtilitiesManage.js?v=<%=Me.assetVersion %>"></script>
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

        function DeleteSuccess() {
            OpenDialogSuccess('<%=grtt("resDeleteSuccess") %>')
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

        function ConfirmDelete(pKey) {

            $("#DeleteModal").modal();
        }

        function genEditIcon(menuId) {
            return ' <a href="javascript:EditData(\'"' + menuId + '"\');"><i class="glyph-icon icon-typicons-edit"></i></a>';
        }

        function Check_Key_Decimal(Money, e)//check key number&dot only and decimal 4 digit
        {

            var key;
            if (document.all) {
                key = window.event.keyCode;
            }
            else {
                key = e.which;
            }

            if (key != 45 && key != 46) {
                if (key < 48 || key > 57) {
                    if (key == 0 || key == 8) {
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    if (String(Money.value).split(".").length > 1) {
                        if (String(Money.value).split(".")[1].length > 3) {
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                    else {
                        return true;
                    }
                }
            }
            else {
                if (Money.value.indexOf(".") > -1) {
                    return false;
                } else {
                    return true;
                }
            }
        }


        function setFormat(txt, e) {
            if (txt.value == "") {
                txt.value = 0.00;
            }
            txt.value = fnFormatMoney(parseFloat(replaceAll(txt.value, ',', '')));

            if (txt.value == "0.00") {
                txt.value = "";
            }
        }

        function fnFormatMoney(values) {
            return values.toFixed(2).replace(/./g, function (c, i, a) { return i && c !== "." && ((a.length - i) % 3 === 0) ? ',' + c : c; });
        }

        function replaceAll(str, find, replace) {
            return str.replace(new RegExp(find, 'g'), replace);
        }


        //$(window).scroll(function () {
        //    if ($(window).scrollTop() == $(document).height() - $(window).height()) {
        //        lastAddedLiveFunc();
        //    }
        //});

        //function lastAddedLiveFunc() {
        //    console.log(angular.element('[ng-controller]').scope().totalDisplayed);
        //    angular.element('[ng-controller]').scope().addDisplayLimit();
        //};


        //$(window).scroll(function () {
        //    var documentHeight = $(document).height();
        //    var scrollDifference = $(window).height() + $(window).scrollTop();

        //    if (documentHeight <= scrollDifference + 1) {
        //        lastAddedLiveFunc();
        //    }
        //});



    </script>
    <uc1:FixedTableHeader runat="server" ID="FixedTableHeader" />
</asp:Content>

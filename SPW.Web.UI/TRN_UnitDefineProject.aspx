<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TRN_UnitDefineProject.aspx.vb" Inherits="SPW.Web.UI.TRN_UnitDefineProject" %>

<%@ Register Src="~/Usercontrol/AngularJSScript.ascx" TagPrefix="uc1" TagName="AngularJSScript" %>
<%@ Register Src="~/Usercontrol/FixedTableHeader.ascx" TagPrefix="uc1" TagName="FixedTableHeader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <uc1:AngularJSScript runat="server" ID="AngularJSScript" />
    <style type="text/css">
        body {
            min-width: 1024px;
        }

        #page-content {
            margin-left: 240px;
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

        .FRESTATUS {
            width: 120px !important;
        }

        .FSERIALNO {
            width: 65px !important;
        }

        .FTYUNIT {
            width: 120px !important;
        }

        .FPDCODE {
            width: 120px !important;
        }

        .FPDNAME {
            width: 300px !important;
        }

        .FDESC {
            width: 200px !important;
        }

        .FAREA {
            width: 80px !important;
            text-align:right;
        }

        .FADDRNO {
            width: 90px !important;
        }

        .FPCPIECE {
            width: 100px !important;
        }

        .FREPHASE {
            width: 80px !important;
            text-align:right;
        }

        .FREZONE {
            width: 80px !important;
            text-align:right;
        }

        .FREBLOCK {
            width: 80px !important;
            text-align:right;
        }

        .FSTDBUILT {
            width: 100px !important;
            text-align:right;
        }

        .FBUILTIN {
            width: 80px !important;
            text-align:right;
        }

        .FTOWER {
            width: 80px !important;
            text-align:right;
        }

        .FFLOOR {
            width: 80px !important;
            text-align:right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row spw-landutil-container" ng-app="myApp" ng-controller="mainController as ctrl">
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
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 form-control-container" style="margin-left: 2px;">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9 col-md-9 col-sm-9">
                                            <h3>
                                                <%=Me.GetMenuName() %>
                                            </h3>
                                        </div>
                                            <div class="col-lg-3 col-md-3 col-sm-3 text-right">
                                                <a ng-hide="$root.pageState!='LIST'"
                                                    ng-click="showAddPage()"
                                                    title=""
                                                    class="btn btn-info tooltip-button glyph-icon icon-plus <%=IIf(Me.GetPermission().isAdd, "", "hide") %>" data-original-title="<%=grtt("resAddData") %>"></a>
                                                 <a ng-hide="$root.pageState=='LIST'"
                                                    ng-click="save()"
                                                    ng-class="{'hide':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')}"
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
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                            <div class="row">
                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                            <label><%=grtt("resProject") %></label>

                                                        </div>
                                                        <div class="col-lg-10 col-md-12 col-sm-12">
                                                            <%-- <input type="text" ng-model="txtSearchProject" class="form-control"
                                                        ng-keyup="$event.keyCode == 13 ? getData() : null"
                                                 
                                                         />--%>

                                                            <select
                                                                id="selProject"
                                                                data-placeholder="<%=grtt("resPleaseChooseProject") %>"
                                                                ng-model="txtSearchProject"
                                                                ng-disabled="$root.pageState!='LIST'"
                                                                ng-change="clearGrid()"
                                                                data-ng-options="c as c.ED01PROJ.FREPRJNM for c in PPZB_MASTER.projects track by c.FREPRJNO"
                                                                class="chosen-select disabled-aslabel disabled-aslargelabel">
                                                               
                                                <option value="" ><%=grtt("resPleaseChooseProject") %></option>
                                                            </select>
                                                             <label id="resPleaseSelect" style="display:none;"><%=grtt("resPleaseSelect") %></label>

                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" ng-show="$root.pageState=='LIST'">
                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                            <label><%=grtt("resPhase") %></label>
                                                        </div>
                                                        <div class="col-lg-4 col-md-12 col-sm-12">
                                                         <%--   <input type="text" class="form-control"
                                                                ng-model="txtSearchPhase" />--%>

                                                            <select ng-model="txtSearchPhase"
                                                                            class="chosen-select"
                                                                 ng-change="clearGrid()"
                                                                data-ng-options="c as c.ED02PHAS.FREPHASE for c in txtSearchProject.phases track by c.FREPHASE"

                                                                >
                                                <option value="" ><%=grtt("resSelectAll") %></option>
                                                                          
                                                                        </select>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" ng-show="$root.pageState=='LIST'">
                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                            <label><%=grtt("resZone") %></label>
                                                        </div>
                                                        <div class="col-lg-4 col-md-12 col-sm-12">
                                                             <select ng-model="txtSearchZone"
                                                                            class="chosen-select"
                                                                 ng-change="clearGrid()"
                                                                data-ng-options="c as c.ED04RECF.FREZONE for c in txtSearchPhase.zones track by c.FREZONE"
                                                                >
                                                <option value="" ><%=grtt("resSelectAll") %></option>
                                                                          
                                                                        </select>


                                                          <%--  <input type="text" class="form-control"
                                                                ng-model="txtSearchZone" />--%>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" ng-show="$root.pageState=='LIST'">
                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                            <label><%=grtt("resUnit") %></label>
                                                        </div>
                                                        <div class="col-lg-4 col-md-12 col-sm-12">
                                                            <div class="row">
                                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                                    <input type="text" class="form-control"
                                                                        ng-model="txtSearchUnitFrom"  />
                                                                </div>
                                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                                    <input type="text" class="form-control"
                                                                        ng-model="txtSearchUnitTo" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;" ng-show="$root.pageState=='LIST'">
                                                    <div class="row" ng-show="$root.pageState=='LIST'">
                                                        <div class="col-lg-3 col-md-3 col-sm-3" ng-show="$root.pageState=='LIST'">
                                                                <a href="#" ng-click="getData()" class="btn btn-info">
                                                                  
                                                                        <i class="glyph-icon icon-search"></i>
                                                                 
                                                                        <%=grtt("resSearch") %>
                                                                  
                                                                </a>
                                                        </div>
                                                  
                                                    </div>
                                             </div>
                                            </div>
                                   <%--   <div class="row" ng-show="mainData.List_vwED03UNIT.length>0 && $root.pageState=='LIST'">
                                          <br />
                                                <div class="col-lg-offset-1 col-md-offset-1 
                                                     col-lg-11 col-md-11 col-sm-12" ng-show="$root.pageState=='LIST'">
                                                                <a  ng-click="showEditPage()" class="btn btn-info ">
                                                                        <i class="glyph-icon icon-edit"></i>
                                                                        <%=grtt("resEdit") %>
                                                                
                                                                </a>
                                                                <a href="#" onclick="OpenDivFSERNO();" class="btn btn-warning <%=IIf(Me.GetPermission().isEdit, "", "hide") %>">
                                                                        <i class="glyph-icon icon-refresh"></i>
                                                                        <%=grtt("resRefreshFSERNO") %>
                                                                </a>
                                                                <a  ng-click="deleteAll()" class="btn btn-danger <%=IIf(Me.GetPermission().isDelete, "", "hide") %>">
                                                                        <i class="glyph-icon icon-trash"></i>
                                                                        <%=grtt("resDelete") %>
                                                                </a>
                                                        </div>

                                      </div>--%>
                                            <div class="row" ng-hide="$root.pageState!='NEW'">

                                                      <hr/>
                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                    <div class="row">
                                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                                            <label><%=grtt("resCount") %></label>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                                            <input type="text"
                                                                ng-model="txtCount"
                                                                ng-enit="''"
                                                                onkeypress="return Check_Key_Decimal(this,event)"
                                                                class="form-control" />
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 text-center">
                                                            <label><%=grtt("resStart") %></label>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                                            <input type="text"
                                                                ng-model="txtStart"
                                                                ng-enit="''"
                                                                class="form-control" />
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2 text-center">
                                                            <label><%=grtt("resType") %></label>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                                            <input type="text"
                                                                ng-model="txtFPDCode"
                                                                class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                        <a href="#" ng-click="checkFPDCODE()" class="btn  btn-info">
                                                        
                                                                <i class="glyph-icon icon-plus"></i>
                                                          
                                                                <%=grtt("resAdd") %>
                                                          
                                                        </a>
                                                </div>
                                               
                                            </div>
                                            <hr />
                                            <div class="row" ng-show="mainData.List_vwED03UNIT.length>0 && $root.pageState=='LIST'">

                                                <div class="col-sm12">
                                                   <div class=" col-sm-6" ng-show="$root.pageState=='LIST'">
                                                                <a  ng-click="showEditPage()" class="btn btn-info ">
                                                                        <i class="glyph-icon icon-edit"></i>
                                                                        <%=grtt("resEdit") %>
                                                                
                                                                </a>
                                                                <a href="#" onclick="OpenDivFSERNO();" class="btn btn-warning <%=IIf(Me.GetPermission().isEdit, "", "hide") %>">
                                                                        <i class="glyph-icon icon-refresh"></i>
                                                                        <%=grtt("resRefreshFSERNO") %>
                                                                </a>
                                                                <a   confirm-click 
                                                                ng-click="(getAllChecked().length==0||confirmClick(getAllChecked())) && deleteAll()" class="btn btn-danger <%=IIf(Me.GetPermission().isDelete, "", "hide") %>">
                                                                        <i class="glyph-icon icon-trash"></i>
                                                                        <%=grtt("resDelete") %>
                                                                </a>
                                                        </div>


                                                         <div class="row text-right">    
                         <div class="col-md-4 float-right">      <input ng-model="queryResult[queryBy]" class="form-control" /><select class="hidden" ng-model="queryBy" ng-init="queryBy='$'">
                    <option  value="$"><%#grtt("resSelectAll") %></option>
                    <option value="FTRNNO"><%#grtt("resFTRNNO") %></option>
                </select> 
                             </div>   
                         <div class="col-md-2 float-right">            
     <label> <%#grtt("resSearchInResult") %></label>
                             </div> 
                                                             </div>
                         </div>  
                                                
                                                <div class="col-lg-12 col-md-12 col-sm-12 detailTable  listInv3">
                                                    <div class="detailTable-fixheader" >
                                                        <table class="table table-striped fixed-header">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        <input type="checkbox"
                                                                            ng-change="List_vwED03UNIT_EDITINGROW_checkAllRow()"
                                                                            ng-model="checkAll" 
                                                                            ng-init="checkAll=false" />
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFRESTATUS") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resSERIALNO") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFTYUNIT") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFPDCODE") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFPDNAME") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resDESC") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resAREA") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFADDRESSNO") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resPCPIE") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFREPHASE") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFREZONE") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFREBLOCK") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFBUILTRN") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFTOWER") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFFLOOR") %>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr
                                                                    ng-repeat="s in mainData.List_vwED03UNIT | filter:queryResult"" 
                                                                    ng-class="{'danger': s.isRowChecked}">
                                                                    <td style="padding:10px;">
                                                                        <input type="checkbox" ng-model="s.isRowChecked" />
                                                                    </td>
                                                                    <td>
                                                                        <label class="FRESTATUS">{{s.FRESTATUSDESC}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FSERIALNO">{{s.FSERIALNO}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FTYUNIT">{{s.FRESTYPEDESC}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FPDCODE">{{s.FPDCODE}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FPDNAME">{{s.FPDNAME}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FDESC">{{s.FDESC}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FAREA">{{s.FAREA|awnum:'price'}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FADDRNO">{{s.FADDRNO}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FPCPIECE">{{s.FPCPIECE}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FREPHASE">{{s.FREPHASE}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FREZONE">{{s.FREZONE}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FREBLOCK">{{s.FREBLOCK}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FSTDBUILT">{{s.FBUILTIN}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FTOWER">{{s.FTOWER}}</label>
                                                                    </td>
                                                                    <td>
                                                                        <label class="FFLOOR">{{s.FFLOOR}}</label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                             <datalist id="DATASOURCE_SD05PDDS">
    <option ng-repeat="fpd in DATASOURCE_SD05PDDS" value="{{fpd.FPDCODE}}">{{fpd.FPDCODE}} {{fpd.FPDNAME}}</option>
  </datalist>
                                            <div class="row" ng-show="mainData.List_vwED03UNIT_EDITINGROW.length>0 && $root.pageState!='LIST'">
                                                <div class="col-lg-12 col-md-12 col-sm-12 detailTable2 bg-transition listInv3 ">
                                                    <div class="detailTable-fixheader2">
                                                        <table id="detailtable" class="table table-striped fixed-header2"
                                                            >
                                                            <thead>
                                                                <tr>
                                                                    <th style="width: 30px;">
                                                                        <%=grtt("resDeleteButton") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFRESTATUS") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resSERIALNO") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFTYUNIT") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFPDCODE") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFPDNAME") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resDESC") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resAREA") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFADDRESSNO") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resPCPIE") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFREPHASE") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFREZONE") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFREBLOCK") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFBUILTRN") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFTOWER") %>
                                                                    </th>
                                                                    <th>
                                                                        <%=grtt("resFFLOOR") %>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody ng-class="{'disabled':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')||('False'=='<%=Me.GetPermission().isAdd %>'&&$root.pageState=='NEW')}">
                                                                <tr id="row_{{$index}}" ng-repeat="e in mainData.List_vwED03UNIT_EDITINGROW"
                                                                     ng-class="{selected: $index === selectedIndex}"
                                                                    ng-click="setSelected($index);$root.pageState=='NEW' && $last && addNewRowSpecial()">
                                                                    <td style="width: 4.35%; text-align: center">
                                                                        <a ng-href="#delete"
                                                                            confirm-click
                                                                    ng-click="confirmClick(e.FSERIALNO)&&delete($index)"
                                                                            ng-hide="$last && $root.pageState=='NEW'"
                                                                            class="btn btn-danger btn-xs glyph-icon icon-typicons-trash"></a>
                                                                       
                                                                    </td>
                                                                    <td>
                                                           
                                                                        <select ng-model="e.FRESTATUS" disabled
                                                                            ng-init ="e.FRESTATUS=e.FRESTATUS || '0'"
                                                                            ng-change="change($index);"
                                                                            data-placeholder="<%=grtt("resPleaseChooseStatus") %>"
                                                                            class="form-control FRESTATUS">
                                                                            <option ng-repeat="c in ED03UNITSTATUS" value="{{c.FRESTATUS}}">{{c.Display}}</option>
                                                                        </select>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text"
                                                                            ng-model="e.FSERIALNO"
                                                                            ng-disabled="$root.pageState!='NEW'"
                                                                            ng-change="change($index);"
                                                                            class="form-control FSERIALNO" 
                                                                            maxlength="10" />
                                                                        <input type="hidden" ng-model="e.e_FSERIALNO" />
                                                                    </td>
                                                                    <td>
                                                                        <select ng-model="e.FTYUNIT"
                                                                            ng-change="change($index);"
                                                                            data-placeholder="<%=grtt("resPleaseChooseUnit") %>"
                                                                            class="form-control FTYUNIT">
                                                                            <option ng-repeat="c in ED03UNITTYPE" value="{{c.FRETYPE}}">{{c.Display}}</option>
                                                                        </select>
                                                                    </td>
                                                                    <!--แบบบ้าน -->
                                                                    <td>
                                                                        <input type="text" list ="DATASOURCE_SD05PDDS"
                                                                            ng-init="e.FPDCODE_OLD=e.FPDCODE"
                                                                            ng-blur="setDescTypeHouse($index,$event);"
                                                                            ng-change="change($index);"
                                                                            ng-model="e.FPDCODE"
                                                                            class="form-control FPDCODE" 
                                                                            maxlength="25" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text" readonly
                                                                            ng-init="e.FPDNAME_OLD=e.FPDNAME"
                                                                            ng-model="e.FPDNAME"
                                                                            ng-change="change($index);"
                                                                            class="form-control FPDNAME" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text"
                                                                            ng-init="e.FDESC_OLD=e.FDESC"
                                                                            ng-model="e.FDESC"
                                                                            ng-change="change($index);"
                                                                            class="form-control FDESC" readonly/>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text"
                                                                            ng-init="e.FAREA_OLD=e.FAREA"
                                                                            ng-model="e.FAREA"
                                                                            ng-change="change($index);"
                                                                            class="form-control FAREA" 
                                                                            <%--onkeypress="return Check_Key_Decimal(this,event)" --%>
                                                                            awnum ="price"
                                                                            />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text"
                                                                            ng-model="e.FADDRNO"
                                                                            ng-change="change($index);"
                                                                            class="form-control FADDRNO" 
                                                                            maxlength="10" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text"
                                                                            ng-model="e.FPCPIECE"
                                                                            ng-change="change($index);"
                                                                            readonly
                                                                            class="form-control FPCPIECE" />
                                                                    </td>
                                                                    <td>
                                                                        <%--  <select ng-model="e.FREPHASE"
                                                                            ng-change="change($index);"
                                                                            data-placeholder="<%=grtt("resPleaseChoosePhase") %>"
                                                                            class="form-control FREPHASE">
                                                                            <option ng-repeat="c in ED02PHAS" value="{{c.FREPHASE}}">{{c.FREPHASE}}</option>
                                                                        </select>--%>
                                                                        <select ng-model="e.FREPHASE_SELECT"
                                                                            ng-init="e.FREPHASE_SELECT = txtSearchProject.phases[getIndexFromValue(txtSearchProject.phases,e.FREPHASE,'FREPHASE')] "
                                                                            class="form-control FREPHASE"
                                                                            ng-change="change($index);"
                                                                            ng-selected="e.FREPHASE=e.FREPHASE_SELECT.FREPHASE"
                                                                            data-ng-options="c as c.ED02PHAS.FREPHASE for c in txtSearchProject.phases track by c.FREPHASE">
                                                                            <option value=""><%=grtt("resPleaseChoosePhase") %></option>
                                                                        </select>
                                                                     

                                                                    </td>
                                                                    <td>

                                                                        <select ng-model="e.FREZONE_SELECT"
                                                                            ng-readonly="isEmpty(e.FREPHASE)"
                                                                            ng-init="e.FREZONE_SELECT = e.FREPHASE_SELECT.zones[getIndexFromValue(e.FREPHASE_SELECT.zones,e.FREZONE,'FREZONE')] "
                                                                            class="form-control FREZONE"
                                                                            ng-change="change($index);"
                                                                            ng-selected="e.FREZONE=e.FREZONE_SELECT.FREZONE"
                                                                            data-ng-options="c as c.ED04RECF.FREZONE for c in e.FREPHASE_SELECT.zones track by c.FREZONE">
                                                                            <option value=""><%=grtt("resPleaseChooseZone") %></option>
                                                                        </select>
                                                                      
                                                                   
                                                                     <%--   <input type="text"
                                                                            ng-readonly="isEmpty(e.FREPHASE)"
                                                                            ng-model="e.FREZONE"
                                                                            ng-change="change($index);"
                                                                            class="form-control FREZONE"
                                                                            maxlength="4" />--%>

                                                                    </td>
                                                                    <td>
                                                                        
                                                                        <select ng-model="e.FREBLOCK_SELECT"
                                                                            ng-readonly="isEmpty(e.FREZONE)"
                                                                            ng-init="e.FREBLOCK_SELECT = e.FREZONE_SELECT.blocks[getIndexFromValue(e.FREZONE_SELECT.blocks,e.FREBLOCK,'FREBLOCK')] "
                                                                            class="form-control FREBLOCK"
                                                                            ng-change="change($index);"
                                                                            ng-selected="e.FREBLOCK=e.FREBLOCK_SELECT.FREBLOCK"
                                                                            data-ng-options="c as c.ED04BLOK.FREBLOCK for c in e.FREZONE_SELECT.blocks track by c.FREBLOCK">
                                                                            <option value=""><%=grtt("resPleaseChooseBlock") %></option>
                                                                        </select>
                                                                      

                                                                      <%--  <input type="text"
                                                                            ng-readonly="isEmpty(e.FREZONE)"
                                                                            ng-model="e.FREBLOCK"
                                                                            ng-change="change($index);"
                                                                            class="form-control FREBLOCK" autocomplete="off" 
                                                                            maxlength="2" />--%>
                                                                    </td>
                                                                    <td>
                                                                        <input type="text"
                                                                            ng-init="e.FBUILTIN_OLD=e.FBUILTIN"
                                                                            ng-model="e.FBUILTIN"
                                                                            ng-change="change($index);"
                                                                            class="form-control FBUILTIN"
                                                                            onkeypress="return Check_Key_Decimal(this,event)" />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text"
                                                                            ng-model="e.FTOWER"
                                                                            ng-change="change($index);"
                                                                            class="form-control FTOWER" 
                                                                            maxlength="5"  />
                                                                    </td>
                                                                    <td>
                                                                        <input type="text"
                                                                            ng-model="e.FFLOOR"
                                                                            ng-change="change($index);"
                                                                            class="form-control FFLOOR" 
                                                                            maxlength="3" />
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9 col-md-9 col-sm-9">
                                            &nbsp;
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 text-right">
                                            <a ng-hide="$root.pageState!='LIST'"
                                                ng-click="showAddPage()"
                                                title=""
                                                class="btn btn-info tooltip-button glyph-icon icon-plus <%=IIf(Me.GetPermission().isAdd, "", "hide") %>" data-original-title="<%=grtt("resAddData") %>"></a>
                                    
                                             <a ng-hide="$root.pageState=='LIST'"
                                                ng-click="save()"
                                                title=""
                                                ng-class="{'hide':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')}"
                                                class="btn btn-info tooltip-button glyph-icon icon-save " data-original-title="<%=grtt("resSave") %>"></a>
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

                 <script type="text/ng-template" id="myModalContent.html">
           <div class="dialog-modal"> 
                  <div class="modal-header" ng-show="modalTitle"> 
                      <h3 class="modal-title"><%=GetResource("msg_header_delete", "MSG") %> {{modalTitle}}</h3> 
                  </div> 
                  <div class="modal-body"><%=Me.GetResource("msg_body_delete", "MSG") %> {{modalBody}}</div> 
                  <div class="modal-footer"> 
                      <button class="btn btn-primary" ng-click="ok()" ng-show="okButton">{{okButton}}</button> 
                      <button class="btn btn-warning" ng-click="cancel()" ng-show="cancelButton">{{cancelButton}}</button> 
                  </div> 
              </div>
             </script>
            </div>
         
       
 
         
            <div class="modal fade .bs-example-modal-sm" id="divFSERNO" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="H3"><span><%=grtt("resHeaderRerunFSERNO") %></span></h4>
                        </div>
                        <div class="modal-body">
                            <input type="text"
                                ng-model="txtStartFSERNO"
                                ng-enit="''"
                                onkeypress="return Check_Key_Decimal(this,event)"
                                class="form-control" 
                                maxlength="4" />
                        </div>
                        <div class="modal-footer">
                            <button id="btnRerunFSERNOConfrim"
                                runat="server"
                                type="button"
                                class="btn btn-success"
                                ng-click="generateFSERNO()">
                                <%#grtt("resbtnRerunFSERNO") %></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var rootHost = window.location.origin + '<%=Request.ApplicationPath %>';
        var culture = '<%= CultureDate%>';
        var systemDateString = '<%=Me.ToSystemDateString(DateTime.Now)%>';
        var currentuser = '<%= CurrentUser.UserID%>';
    </script>
    <script src="Scripts_ngapp/TRN_UnitDefineProject.js?v=<%=Me.assetVersion %>"></script>
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

        function OpenDivFSERNO() {
            $("#divFSERNO").modal();
        }

        function CloseDivFSERNO() {
            $("#divFSERNO").modal('hide');
        }
        


        
    </script>
    <uc1:FixedTableHeader runat="server" id="FixedTableHeader" />
</asp:Content>

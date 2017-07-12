<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TRN_ProjectPriceList.aspx.vb" Inherits="SPW.Web.UI.TRN_ProjectPriceList" EnableEventValidation="false" %>

<%@ Import Namespace="SPW.Helper" %>
<%@ Import Namespace="SPW.BLL" %>
<%@ Import Namespace="SPW.DAL" %>
<%@ Register Src="~/Usercontrol/AngularJSScript.ascx" TagPrefix="uc1" TagName="AngularJSScript" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <script src="//cdnjs.cloudflare.com/ajax/libs/angular.js/1.4.2/angular.js"></script>--%>
    <uc1:AngularJSScript runat="server" ID="AngularJSScript" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row form-control-container" ng-app="myApp" ng-controller="mainController" ng-init="CurrentUser='<%=CurrentUser.UserID%>'">
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
                    <div id="divResultsContainer" class="row" ng-show="$root.pageState=='LIST'">
                        <div class="col-lg-12 col-md-12 col-sm-12" style="margin-left: 2px;">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-10 col-sm-10">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <h3 class="col-lg-10 col-md-10 col-sm-10">
                                                            <%=Me.GetMenuName() %>
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
                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                    <label><%=grtt("resProject") %></label>
                                                </div>
                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                    <select data-id="selProject" ng-model="mainData.ED04RECF.FREPRJNO" 
                                                        class="chosen-select" ng-change="setED02PHASE()">
                                                        <option value="" class="hidden"><%=grtt("resPleaseChooseProject") %></option>
                                                        <option ng-repeat="prjItem in ED01PROJ" value="{{prjItem.FREPRJNO+' '}}">{{prjItem.FREPRJNM}}</option>
                                                    </select>
                                                    <label id="resPleaseSelect" style="display: none;"><%=grtt("resPleaseSelect") %></label>
                                                </div>
                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                        <a href="#" ng-click="getSearchResult()" class="btn btn-info">
                                                                <i class="glyph-icon icon-search"></i>
                                                                <%=grtt("resSearch") %>
                                                        </a>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                    <label><%=grtt("resPhase") %></label>
                                                </div>
                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                    <select data-id="selPhase" ng-model="mainData.ED04RECF.FREPHASE" ng-change="setED03UNIT()"
                                                        class="chosen-select">
                                                        <option value=""><%=grtt("resSelectAll") %></option>
                                                        <option ng-repeat="phaseItem in ED02PHAS" value="{{phaseItem.FREPHASE}}">{{phaseItem.FREPHASE}}</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                    <label><%=grtt("resZone") %></label>
                                                </div>
                                                <div class="col-lg-6 col-md-12 col-sm-12">

                                                    <select data-id="selZone" ng-model="mainData.ED04RECF.FREZONE"
                                                        class="chosen-select">
                                                        <option value=""><%=grtt("resSelectAll") %></option>
                                                        <option ng-repeat="zoneItem in ED03UNIT" value="{{zoneItem}}">{{zoneItem}}</option>
                                                    </select>

                                                </div>
                                             
                                            </div>
                                        </div>
                                    </div>
                                    <hr />

                                    <div ng-show="searchResult.length>0">  
                            
                     <div class="row text-right">    
                         <div class="col-md-4 float-right">    
                               <input ng-model="queryResult[queryBy]" class="form-control" />
                           
                             </div>   
                         <div class="col-md-2 float-right">  
                             <label><%=grtt("resSearchInResault") %></label>
                               <select class=" hidden" ng-model="queryBy" ng-init="queryBy='$'">
                    <option  value="$"><%#grtt("resSearchAll") %></option>
                    <option value="FTRNNO"><%#grtt("resFTRNNO") %></option>
                    <option value="FVOUDATE"><%#grtt("resFVOUDATE") %></option>
                    <option value="FREPRJNO"><%#grtt("resFREPRJNO") %></option>
                </select>           
                             </div> 
                         </div>  
                                        <div style="overflow-x:auto;overflow-y:hidden;">
                                        <table ng-table="searchResultTable" show-filter="true" class="table table-striped">
                                            <tr ng-repeat="s in finalSearchResult ">
                                                <td style="width: min-width: 70px;" data-title="'<%#grtt("resAction") %>'">
                                                    <a  ng-click="showEditPage(s.FREPRJNO,s.FREPHASE,s.FREZONE,s.FTRNNO)" class="btn btn-info btn-xs glyph-icon icon-typicons-edit"></a>
                                                    <a  ng-click="deleteData(s.FTRNNO,$index,s)" class="btn btn-danger btn-xs glyph-icon icon-typicons-trash <%=IIf(Me.GetPermission().isDelete, "", "hide") %>"></a>
                                                </td>
                                                <td data-title="'<%#grtt("resFTRNNO") %>'" sortable="'FTRNNO'">{{s.FTRNNO}}
                                                    </td>
                                                    <td data-title="'<%#grtt("resFSERIALNO") %>'" sortable="'FSERIALNOCOUNT'"  data-container="body" data-toggle="tooltip" data-placement="right" data-original-title="{{s.FSERIALNO}}">
                                                        {{s.FSERIALNOCOUNT}}
                                                    </td>
                                                    <td data-title="'<%#grtt("resFVOUDATE") %>'" sortable="'FVOUDATE'">{{s.FVOUDATE| cDate}}
                                                    </td>
                                                <td data-title="'<%#grtt("resFREPRJNO") %>'" sortable="'FREPRJNO'">{{s.FREPRJNO}}
                                                </td>
                                                <td data-title="'<%#grtt("resFREPRJNM") %>'" sortable="'FREPRJNM'">{{s.FREPRJNM}}
                                                </td>
                                                <td data-title="'<%#grtt("resFREPHASE") %>'" sortable="'FREPHASE'">{{s.FREPHASE}}
                                                </td>
                                                <td data-title="'<%#grtt("resFREZONE") %>'" sortable="'FREZONE'">{{s.FREZONE}}
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
                    <div id="divEditContainer" class="row" ng-show="$root.pageState=='NEW'||$root.pageState=='EDIT'">

                        <div>
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9 col-md-9 col-sm-9">
                                            <h3>
                                                <%=grtt("resEditHeading") %>
                                            </h3>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 text-right">                        
                                            <a ng-click="approve()" ng-show="$root.pageState=='EDIT'&&mainData.ED11PAJ1.FUPDFLAG!='Y'" class="btn glyph-icon icon-approve btn-success tooltip-button  <%=IIf(Me.GetPermission().isApprove, "", "hide") %>"></a>  
                                            <a ng-click="unapprove()" ng-show="$root.pageState=='EDIT'&&mainData.ED11PAJ1.FUPDFLAG=='Y'" class="btn glyph-icon icon-unapprove btn-warning tooltip-button <%=IIf(Me.GetPermission().isApprove, "", "hide") %>"></a>
                                            <a ng-click="save()" ng-show="$root.pageState=='NEW'||($root.pageState=='EDIT'&&mainData.ED11PAJ1.FUPDFLAG!='Y')" class="btn btn-info tooltip-button glyph-icon icon-save"
                                               ng-class="{'hide':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')||('False'=='<%=Me.GetPermission().isAdd %>'&&$root.pageState=='NEW')}"></a>
                                            <a ng-click="cancel()" class="btn btn-danger tooltip-button glyph-icon icon-close"></a>
                                        </div>
                                    </div>
                                </div>
                                <!--Master red:ED04RECF blue:ED11PAJ1-->
                                <div class="divRowPadding " ng-class="{'disabled':isEmpty($root.pageState=='NEW'||($root.pageState=='EDIT'&&mainData.ED11PAJ1.FUPDFLAG!='Y'))}">
                                   <%-- {{isEmpty(mainData.ED04RECF.FREZONE)}}
                                    {{('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')}}--%>
                                    <div class="row" ng-class="{'disabled':isEmpty(mainData.ED04RECF.FREZONE)||('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')}" >

                                        <div class="col-md-2 ">
                                            <label><%=grtt("resPreCode") %></label>
                                        </div>
                                        <div class="col-md-2 ">
                                            <input ng-model="mainData.preCode" type="text" class="form-control" required readonly />
                                        </div>
                                        <div class="col-md-4 ">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-3 ">
                                            <label class="nestcheckbox">
                                                <input type="checkbox" ng-init="chkFPRICETYPE=initChkFPRICETYPE()" ng-model="chkFPRICETYPE" ng-change="chkFPRICETYPE_change()">&nbsp;<%=grtt("resFPRICRTYPE") %></label>
                                     
                                        </div>
                                    </div>
                                    <div class="row" ng-class="{'disabled':isEmpty(mainData.ED04RECF.FREZONE)}" >
                                        <div class="col-md-2 ">
                                            <label><%=grtt("resFTRNNO") %></label>
                                        </div>
                                        <div class="col-md-2 ">
                                            <input ng-model="mainData.ED11PAJ1.FTRNNO" type="text" class="form-control" readonly />
                                        </div>
                                        <div class="col-md-1 ">
                                            <label><%=grtt("resFVOUDATE") %></label>
                                        </div>
                                        <div class="col-md-2 ">
                                            <input ng-model="mainData.ED11PAJ1.FVOUDATE" placeholder="Enter date" jqdatepicker />
                                        </div>
                                        <div class="col-md-1 ">
                                            <label><%=grtt("resFMDATE") %></label>
                                        </div>
                                        <div class="col-md-2 ">
                                            <input ng-model="mainData.ED11PAJ1.FMDATE  | cDate" readonly  />
                                        </div>
                                    </div>

                                    <!-- dropdown mainData.ED04RECF.FREPRJNO -->
                                    <div class="row">
                                        <div class="col-md-2 ">
                                            <label class="after-asterisk" ><%=grtt("resProject") %></label>
                                        </div>
                                        <div class="col-lg-8 ">
                                            <select data-id="selProject" ng-model="mainData.ED04RECF.FREPRJNO" ng-disabled="$root.pageState=='EDIT'"
                                                class="chosen-select disabled-aslabel  disabled-aslargelabel" ng-change="setED02PHASE()">
                                                <option value="" ><%=grtt("resPleaseChooseProject") %></option>
                                                <option ng-repeat="prjItem in ED01PROJ" value="{{prjItem.FREPRJNO+' '}}">{{prjItem.FREPRJNM}}</option>
                                            </select>
                                        </div>

                                         <div class="col-lg-8 ">
                                           
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-2 ">
                                            <label class="after-asterisk" ><%=grtt("resPhase") %></label>
                                        </div>
                                        <div class="col-lg-8 " >
                                            <select data-id="selPhase" ng-model="mainData.ED04RECF.FREPHASE" ng-change="setED03UNIT()" ng-disabled="$root.pageState=='EDIT'"
                                                class="chosen-select disabled-aslabel">
                                                <option value="" ><%=grtt("resPleaseChoosePhase") %></option>
                                                <option ng-repeat="phaseItem in ED02PHAS" value="{{phaseItem.FREPHASE}}">{{phaseItem.FREPHASE}}</option>
                                            </select>
                                        </div>
                                         
                                    </div>

                                    <div class="row">
                                        <div class="col-md-2 ">
                                            <label class="after-asterisk" ><%=grtt("resZone") %></label>
                                        </div>
                                        <div class="col-lg-8 " >
                                            <select data-id="selZone" 
                                                ng-model="mainData.ED04RECF.FREZONE" 
                                                ng-change="getNewData()"
                                                ng-disabled="$root.pageState=='EDIT'"
                                                class="chosen-select disabled-aslabel">
                                                <option value="" ><%=grtt("resPleaseChooseZone") %></option>
                                                <option ng-repeat="zoneItem in ED03UNIT" value="{{zoneItem}}">{{zoneItem}}</option>
                                            </select>
                                        </div>
                                     
                                    </div>

                                    <div class="row" ng-class="{'disabled':isEmpty(mainData.ED04RECF.FREZONE)}" >
                                        <div class="col-md-12 col-lg-5">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFRINTA") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED04RECF.FRINTA" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resFRINTB") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED04RECF.FRINTB" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resFRINTC") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED04RECF.FRINTC" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFRMINBOOK") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED04RECF.FRMINBOOK" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                                <div class="col-md-4">
                                                    <input ng-model="mainData.ED04RECF.FPBOOKA" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resBaht") %></label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFRCONTRACT") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED04RECF.FRCONTRACT" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                                <div class="col-md-4">
                                                    <input ng-model="mainData.ED04RECF.FPCONTRACTA" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resBaht") %></label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFRDOWN") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED04RECF.FRDOWN" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" />
                                                </div>
                                                <div class="col-md-4">
                                                    <label><%=grtt("resFDOWNMON") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FMDOWN" type="text" class="form-control" onkeypress="return Check_Key(event);" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFMPUBLIC") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED04RECF.FMPUBLIC" type="text" class="form-control" onkeypress="return Check_Key(event);" />
                                                </div>
                                                <div class="col-md-4">
                                                    <label><%=grtt("resMonth") %></label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-lg-7">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label class="nestcheckbox">
                                                        <input type="radio" ng-model="mainData.ED11PAJ1.FADJTYPE" value="1">&nbsp;<%=grtt("resFADJTYPE1") %></label>
                                                </div>
                                                <div class="col-md-4">
                                                    <label class="nestcheckbox">
                                                        <input type="radio" ng-model="mainData.ED11PAJ1.FADJTYPE" value="2">&nbsp;<%=grtt("resFADJTYPE2") %></label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFAREAUPRC") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FAREAUPRC" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resSquareYard") %></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFPCORNER1") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FPCORNER1" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price"  />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resSquareYard") %></label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFPOVERAREA") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FPOVERAREA" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resSquareYard") %></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFPCORNER2") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FPCORNER2" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFPMROADA") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FPMROADA" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resFPMROADB") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FPMROADB" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resFPMROADC") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FPMROADC" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><%=grtt("resFPPUBLICA") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FPPUBLICA" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resFPPUBLICB") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FPPUBLICB" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label><%=grtt("resFPPUBLICC") %></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <input ng-model="mainData.ED11PAJ1.FPPUBLICC" type="text" class="form-control text-right" onkeypress="return Check_Key_Decimal(this,event);" awnum ="price" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-4">
                                                    <label class="nestcheckbox">
                                                        <input type="checkbox" ng-init="chkFPPUBLICTY=initChkFPPUBLICTY()" ng-model="chkFPPUBLICTY" ng-change="chkFPPUBLICTY_change()">&nbsp;<%=grtt("resFPPUBLICTY") %></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <!--Ref green:vw_List_ED11PAJ2-->

                                      <datalist id="DATASOURCE_ED03UNIT">
    <option ng-repeat="ed in DATASOURCE_ED03UNIT" value="{{ed.FSERIALNO}}">{{ed.FSERIALNO}} {{ed.FPDCODE}}</option>
  </datalist>
                                    <div class="row" ng-class="{'disabled':isEmpty(mainData.ED04RECF.FREZONE)}" style="overflow-x:auto;overflow-y:hidden;">
                                        <div class="col-md-12 detailTable listInv3">
                                            <table class="table table-striped">
                                                <tr>
                                                    <th style="width: 70px;" class=" <%=IIf(Me.GetPermission().isDelete, "", "hide") %>"><%=grtt("resAction") %></th>
                                                    <th><%=grtt("resNo.") %></th>
                                                    <th><%=grtt("resFPDCODE") %></th>
                                                    <th><%=grtt("resFPDNAME") %></th>
                                                    <th><%=grtt("resFSERIALNO") %></th>
                                                    <th><%=grtt("resFPROPRICE") %></th>
                                                    <th><%=grtt("resFDISCPRICE") %></th>
                                                    <th><%=grtt("resFSTDPRICE") %></th>
                                                    <th><%=grtt("resFAREA") %></th>
                                                </tr>
                                                <tr ng-repeat="s in mainData.vw_List_ED11PAJ2" ng-click="$last && addNewRow()"
                                                    ng-class="{'disabled':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')}">
                                                    <td class=" <%=IIf(Me.GetPermission().isDelete, "", "hide") %>">
                                                        <a ng-href="#delete" ng-click="removeRow($index)"
                                                            ng-show="!$last"
                                                            class="btn btn-danger btn-xs glyph-icon icon-typicons-trash <%=IIf(Me.GetPermission().isDelete, "", "hide") %>"></a>
                                                    </td>
                                                    <td  >
                                                        <label ng-show="!$last">{{$index+1}}</label>
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="s.FPDCODE" maxlength="25" readonly />
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="s.FPDNAME" readonly />
                                                    </td>
                                                    <td>
                                                        <input type="text" list ="DATASOURCE_ED03UNIT" ng-model="s.FSERIALNO" maxlength="10" ng-blur="chkDup($index,$event);"  />
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="s.FPROPRICE" onkeypress="return Check_Key_Decimal(this,event)" awnum ="price"  class="text-right"/>
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="s.FDISCPRICE" onkeypress="return Check_Key_Decimal(this,event)" awnum ="price"  class="text-right"/>
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="s.FSTDPRICE" onkeypress="return Check_Key_Decimal(this,event)" awnum ="price"  class="text-right"/>
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="s.FAREA" readonly awnum ="price" />
                                                    </td>
                                                    
                                                </tr>
                                            </table>
                                        </div>

                                    </div>
                                </div>
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9 col-md-9 col-sm-9">
                                            &nbsp;
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 text-right">
                                            <a ng-click="approve()" ng-show="$root.pageState=='EDIT'&&mainData.ED11PAJ1.FUPDFLAG!='Y'" class="btn glyph-icon icon-approve btn-success tooltip-button  <%=IIf(Me.GetPermission().isApprove, "", "hide") %>"></a>
                                            <a ng-click="unapprove()" ng-show="$root.pageState=='EDIT'&&mainData.ED11PAJ1.FUPDFLAG=='Y'" class="btn glyph-icon icon-unapprove btn-warning tooltip-button <%=IIf(Me.GetPermission().isApprove, "", "hide") %>"></a>
                                            <a ng-click="save()" ng-show="$root.pageState=='NEW'||($root.pageState=='EDIT'&&mainData.ED11PAJ1.FUPDFLAG!='Y')" class="btn btn-info tooltip-button glyph-icon icon-save"
                                               ng-class="{'hide':('False'=='<%=Me.GetPermission().isEdit %>'&&$root.pageState=='EDIT')||('False'=='<%=Me.GetPermission().isAdd %>'&&$root.pageState=='NEW')}"></a>
                                            <a ng-click="cancel()" class="btn btn-danger tooltip-button glyph-icon icon-close"></a>
                                        </div>
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
        var currentuser = '<%= CurrentUser.UserID%>';
    </script>
    <script src="Scripts_ngapp/TRN_ProjectPriceList.js?v=<%=Me.assetVersion %>"></script>
    <script type="text/javascript">
        var resLanguage = {};
        $(function () {
            setLanguage();
        })

        function setLanguage() {
            resLanguage.resFDISCPRICEoverFPROPRICE = '<%=grtt("resFDISCPRICE") %> <%=grtt("resnotover")%> <%=grtt("resFPROPRICE") %>';
            resLanguage.resFPROPRICEoverFSTDPRICE = '<%=grtt("resFPROPRICE") %> <%=grtt("resnotover")%> <%=grtt("resFSTDPRICE") %>';
        }

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
            closeOverlay();
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
            return ' <a href="javascript:EditData(\'"' + menuId + '"\');"><i class="glyph-icon icon-typicons-edit"></i></a>';
        }

        var scopeForDebug = {}
        $(function () {
            scopeForDebug = angular.element('[ng-controller]').scope();

        })

        //เชคให้รับเฉพาะตัวเลขเท่านั้น
        function Check_Key(e) {
            var key;
            if (document.all) {
                key = window.event.keyCode;
            } else {
                key = e.which;
            }
            if (key < 48 || key > 57) {
                if (key == 0 || key == 8) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return true;
            }
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

    </script>
</asp:Content>

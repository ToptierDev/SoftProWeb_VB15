<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TRN_ReservationAndApproveRecord.aspx.vb" Inherits="SPW.Web.UI.TRN_ReservationAndApproveRecord" EnableEventValidation="false" %>

<%@ Import Namespace="SPW.Helper" %>
<%@ Import Namespace="SPW.BLL" %>
<%@ Import Namespace="SPW.DAL" %>
<%@ Register Src="~/Usercontrol/AngularJSScript.ascx" TagPrefix="uc1" TagName="AngularJSScript" %>

<%@ Register Src="Usercontrol/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <script src="//cdnjs.cloudflare.com/ajax/libs/angular.js/1.4.2/angular.js"></script>--%>
    <uc1:AngularJSScript runat="server" ID="AngularJSScript" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:DatePicker runat="server" />
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

                    <div id="divEditContainer" class="row" ng-show="$root.pageState=='NEW'||$root.pageState=='EDIT'">

                        <div>
                            <div class="panel panel-primary">
                                <!--Header content-->
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9 col-md-9 col-sm-9">
                                            <h3>
                                                <%=GetMenuName() %>
                                            </h3>
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 text-right">
                                            <a ng-click="save()" class="btn btn-info tooltip-button glyph-icon icon-save"></a>
                                            <a ng-click="cancel()" class="btn btn-danger tooltip-button glyph-icon icon-close"></a>
                                        </div>
                                    </div>
                                </div>

                                <div class="divRowPadding">
                                    <!--Transaction Header Data Booking Info OD11BKT1-->
                                    <div class="row">
                                        <div class="col-md-12">
                                            ข้อมูล transaction header
                                            <div class="row">
                                                <div class="col-md-1 ">
                                                    <label><%=grtt("resPreCode") %></label>
                                                </div>
                                                <div class="col-md-2 ">
                                                    <input ng-model="mainData.preCode" type="text" autocomplete="off" class="form-control" required readonly />
                                                </div>
                                                <div class="col-md-1 ">
                                                    <label><%=grtt("resDivCode") %></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <select ng-model="mainData.AD11INV1.FDIVCODE" ng-options="item.FDIVCODE as item.FDIVNAME for item in coreDivision"
                                                        class="chosen-select"
                                                        style="width: 200px;">
                                                        <option value="" class="hidden"><%=grtt("resPleaseSelect") %></option>
                                                    </select>
                                                </div>
                                                <div class="col-md-1 ">
                                                    <label class="after-asterisk" ><%=grtt("resProject") %></label>
                                                </div>
                                                <div class="col-lg-4 ">
                                                    <select data-id="selProject" ng-model="mainData.ED04RECF.FREPRJNO"
                                                        class="chosen-select disabled-aslabel  disabled-aslargelabel" ng-change="setED02PHASE()">
                                                        <option value=""><%=grtt("resPleaseChooseProject") %></option>
                                                        <option ng-repeat="prjItem in ED01PROJ" value="{{prjItem.FREPRJNO+' '}}">{{prjItem.Display}}</option>
                                                    </select>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-1 ">
                                                    <label><%=grtt("resFTRNNO") %></label>
                                                </div>
                                                <div class="col-md-2 ">
                                                    <input ng-model="mainData.OD11BKT1.FTRNNO" autocomplete="off" type="text" class="form-control" maxlength="18" />
                                                </div>
                                                <div class="col-md-1 ">
                                                    <label><%=grtt("resFVOUDATE") %></label>
                                                </div>
                                                <div class="col-md-3 ">
                                                    <input ng-model="mainData.OD11BKT1.FVOUDATE" placeholder="Enter date" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                </div>
                                                <div class="col-md-1 ">
                                                    <label><%=grtt("resFMDATE") %></label>
                                                </div>
                                                <div class="col-md-4 ">
                                                    <input ng-model="mainData.OD11BKT1.FMDATE" placeholder="Enter date" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-1 ">
                                                    <label><%=grtt("resFSERIALNO") %></label>
                                                </div>
                                                <div class="col-md-2 ">
                                                    <input ng-model="mainData.OD11BKT1.FSERIALNO" autocomplete="off" type="text" class="form-control" maxlength="10" />
                                                </div>
                                                <div class="col-md-1 ">
                                                    <label>แบบบ้าน+</label>
                                                </div>
                                                <div class="col-md-3 ">
                                                    <input ng-model="mainData.OD11BKT1.preCode" autocomplete="off" type="text" class="form-control" />
                                                </div>
                                                <div class="col-md-1 ">
                                                    <label><%=grtt("resFPDCODE") %></label>
                                                </div>
                                                <div class="col-md-3 ">
                                                    <input ng-model="mainData.OD11BKT1.FPDCODE" autocomplete="off" type="text" class="form-control" maxlength="25" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-1 ">
                                                    <label>ขนาด+</label>
                                                </div>
                                                <div class="col-md-2 ">
                                                    <input ng-model="mainData.OD11BKT1.FPDCODE" autocomplete="off" type="text" class="form-control" onkeypress="return Check_Key_Decimal(this,event)" />
                                                </div>
                                                <div class="col-md-1 ">
                                                    <label>เฟส+</label>
                                                </div>
                                                <div class="col-md-3 ">
                                                    <input ng-model="mainData.OD11BKT1.FPDCODE" autocomplete="off" type="text" class="form-control" readonly />
                                                </div>
                                                <div class="col-md-1 ">
                                                    <label>โซน+</label>
                                                </div>
                                                <div class="col-md-3 ">
                                                    <input ng-model="mainData.OD11BKT1.FPDCODE" autocomplete="off" type="text" class="form-control" readonly />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-1 ">
                                                    <label>ผู้จอง+</label>
                                                </div>
                                                <div class="col-md-3" style="padding-right: 0px;">
                                                    <input ng-model="mainData.OD11BKT1.FCONTCODE" autocomplete="off" type="text" class="form-control" />
                                                </div>
                                                <div class="col-md-7 ">
                                                    <input ng-model="mainData.OD11BKT1.FCONTCODE" autocomplete="off" type="text" class="form-control" />
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-1 ">
                                                    <label><%=grtt("resFANETPRC") %></label>
                                                </div>
                                                <div class="col-md-2 ">
                                                    <input ng-model="mainData.OD11BKT1.FANETPRC" autocomplete="off" type="text" class="form-control" onkeypress="return Check_Key_Decimal(this,event)" />
                                                </div>
                                                <div class="col-md-1 ">
                                                    <label><%=grtt("resFABOOK") %></label>
                                                </div>
                                                <div class="col-md-2 ">
                                                    <input ng-model="mainData.OD11BKT1.FABOOK" autocomplete="off" type="text" class="form-control" onkeypress="return Check_Key_Decimal(this,event)" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div id="tabContainer">
                                        <!--Transaction Tab1 Booking Info OD11BKT1-->
                                        <div class="row">
                                            <div class="col-md-12">
                                                ข้อมูล Tab1 Booking Info OD11BKT1
                                                <br />
                                                FTRNNO:  {{mainData.OD11BKT1.FTRNNO}}
                                                  <div class="row">
                                                      <div class="col-md-1 ">
                                                          <label><%=grtt("resFADISC") %></label>
                                                      </div>
                                                      <div class="col-md-2 ">
                                                          <input ng-model="mainData.OD11BKT1.FADISC" autocomplete="off" type="text" class="form-control" onkeypress="return Check_Key_Decimal(this,event)" />
                                                      </div>
                                                  </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label>ผู้แนะนำ+</label>
                                                    </div>
                                                    <div class="col-md-6 ">
                                                        <table border="0">
                                                            <tr>
                                                                <td style="width: 30%;">
                                                                    <input ng-model="mainData.OD11BKT1.FADISC" autocomplete="off" style="background-color: #FFE0C0;" type="text" class="form-control" /></td>
                                                                <td>
                                                                    <input ng-model="mainData.OD11BKT1.FADISC" autocomplete="off" type="text" class="form-control" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label>ค่าแนะนำ+</label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.preCode" type="text" autocomplete="off" class="form-control" onkeypress="return Check_Key_Decimal(this,event)" />
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <label class="nestcheckbox">
                                                            <input type="checkbox">&nbsp;ไม่สะสม+</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <!--Transaction Tab2 Booking Info OD11BKT1-->
                                        <div class="row">
                                            <div class="col-md-12">
                                                ข้อมูล Tab2 Booking Info OD11BKT1
                                                <br />
                                                FTRNNO:  {{mainData.OD11BKT1.FTRNNO}}
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label><%=grtt("resFPCONTRACT") %></label>
                                                    </div>
                                                    <div class="col-md-3 ">
                                                        <table border="0">
                                                            <tr>
                                                                <td style="width: 30%;">
                                                                    <input ng-model="mainData.OD11BKT1.FPCONTRACT" autocomplete="off" type="text" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" /></td>
                                                                <td>
                                                                    <input ng-model="mainData.OD11BKT1.FACONTRACT" autocomplete="off" type="text" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                    </div>
                                                    <div class="col-md-1 text-right">
                                                        <label><%=grtt("resFAGRAPPDT") %></label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.OD11BKT1.FAGRAPPDT" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label><%=grtt("resFADOWN") %></label>
                                                    </div>
                                                    <div class="col-md-3 ">
                                                        <table border="0">
                                                            <tr>
                                                                <td style="width: 30%;">
                                                                    <input ng-model="mainData.OD11BKT1.FADOWN" type="text" autocomplete="off" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" /></td>
                                                                <td>
                                                                    <input ng-model="mainData.OD11BKT1.FADOWNRT" type="text" autocomplete="off" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" /></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="col-md-1 text-right">
                                                        <label><%=grtt("resFADOWNMN") %></label>
                                                    </div>
                                                    <div class="col-md-1 ">
                                                        <input ng-model="mainData.OD11BKT1.FADOWNMN" type="text" class="form-control" autocomplete="off" onkeypress="return Check_Key_Decimal(this,event)" />
                                                    </div>
                                                    <div class="col-md-1 text-right">
                                                        <label><%=grtt("resFADOWNST") %></label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.OD11BKT1.FADOWNST" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6 ">
                                                    </div>
                                                    <div class="col-md-1 text-right">
                                                        <label><%=grtt("resFADOWNMR") %></label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.OD11BKT1.FADOWNMR" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label><%=grtt("resFAFINAL") %></label>
                                                    </div>
                                                    <div class="col-md-3 ">
                                                        <input ng-model="mainData.OD11BKT1.FAFINAL" type="text" autocomplete="off" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" />
                                                    </div>
                                                    <div class="col-md-3 text-right">
                                                        <label><%=grtt("resFAFINALDT") %></label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.OD11BKT1.FAFINALDT" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label>รวมทั้งสิ้น+</label>
                                                    </div>
                                                    <div class="col-md-3 ">
                                                        <input ng-model="mainData.OD11BKT1.FAFINAL" type="text" autocomplete="off" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" />
                                                    </div>
                                                    <div class="col-md-3 text-right">
                                                        <label><%=grtt("resFAFINALDT") %></label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.OD11BKT1.FAFINALDT" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                    </div>
                                                    <div class="col-md-3 ">
                                                        <label class="nestcheckbox">
                                                            <input type="checkbox">&nbsp;ต่อหลัง+</label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-7 text-right">
                                                        <label><%=grtt("resFPUBLICMN") %></label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.OD11BKT1.FPUBLICMN" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <label><%=grtt("resFPUBLICMNMonth") %></label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="panel panel-primary">
                                                        <div class="panel-body">
                                                            <h3>รายชื่อผู้ทำสัญญา+</h3>
                                                            <hr class="width-100" />
                                                            <div class="row">
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCUCODE1") %></label>
                                                                </div>
                                                                <div class="col-md-7 ">
                                                                    <table border="0">
                                                                        <tr>
                                                                            <td style="width: 20%;">
                                                                                <input ng-model="mainData.OD11BKT1.FCUCODE1" autocomplete="off" style="background-color: #FFE0C0;" type="text" class="form-control" maxlength="8" /></td>
                                                                            <td>
                                                                                <input ng-model="mainData.OD11BKT1.FCUCODE1" autocomplete="off" type="text" class="form-control" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <label class="nestcheckbox">
                                                                        <input type="checkbox">&nbsp;บัตรประชาชน+</label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <label class="nestcheckbox">
                                                                        <input type="checkbox">&nbsp;ทะเบียนบ้าน+</label>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCUCODE2") %></label>
                                                                </div>
                                                                <div class="col-md-7 ">
                                                                    <table border="0">
                                                                        <tr>
                                                                            <td style="width: 20%;">
                                                                                <input ng-model="mainData.OD11BKT1.FCUCODE2" autocomplete="off" style="background-color: #FFE0C0;" type="text" class="form-control" maxlength="8" /></td>
                                                                            <td>
                                                                                <input ng-model="mainData.OD11BKT1.FCUCODE2" autocomplete="off" type="text" class="form-control" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <label class="nestcheckbox">
                                                                        <input type="checkbox">&nbsp;บัตรประชาชน+</label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <label class="nestcheckbox">
                                                                        <input type="checkbox">&nbsp;ทะเบียนบ้าน+</label>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCUCODE3") %></label>
                                                                </div>
                                                                <div class="col-md-7 ">
                                                                    <table border="0">
                                                                        <tr>
                                                                            <td style="width: 20%;">
                                                                                <input ng-model="mainData.OD11BKT1.FCUCODE3" autocomplete="off" style="background-color: #FFE0C0;" type="text" class="form-control" maxlength="8" /></td>
                                                                            <td>
                                                                                <input ng-model="mainData.OD11BKT1.FCUCODE3" autocomplete="off" type="text" class="form-control" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <label class="nestcheckbox">
                                                                        <input type="checkbox">&nbsp;บัตรประชาชน+</label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <label class="nestcheckbox">
                                                                        <input type="checkbox">&nbsp;ทะเบียนบ้าน+</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3 ">
                                                        <label><%=grtt("resFOLDBOOKNO") %></label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.OD11BKT1.FOLDBOOKNO" type="text" autocomplete="off" class="form-control" maxlength="18" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <!--Transaction Tab3 รับชำระเงิน BD24CRRG List_RD26ORRG List_REPRINTLOG-->
                                        <div class="row">
                                            <div class="col-md-12">
                                                ข้อมูล Tab3 รายละเอียดการรับเงิน BD24CRRG
                                                <br />
                                                {{mainData.BD24CRRG.FCASHTYPE}}-
                                             {{mainData.BD24CRRG.FBKCHQ}}-
                                             {{mainData.BD24CRRG.FCHQNO}}
                                            </div>
                                            <div class="col-md-12">
                                                ข้อมูล Tab3 ใบเสร๊จรับเงิน List_RD26ORRG
                                                <ul>
                                                    <li ng-repeat="rec in mainData.List_RD26ORRG">{{ rec.FRECEIPTNO}}-
                                                        {{ rec.FREFNO}}-
                                                        {{ rec.FAGRNO}}-
                                                        {{ rec.FCOLPRDNO}}-
                                                        {{ rec.FCREDITCD}}
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-md-12">
                                                ข้อมูล Tab3 ประวัติพิมพ์ใบเสร๊จ List_REPRINTLOG
                                                <ul>
                                                    <li ng-repeat="rec_hist in mainData.List_REPRINTLOG">{{ rec_hist.FRINDX}}-
                                                        {{ rec_hist.FRECEIPTNO}}
                                                    </li>
                                                </ul>

                                            </div>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="panel panel-primary">
                                                        <div class="panel-body">
                                                            <h3>การรับชำระเงินจอง+</h3>
                                                            <hr class="width-100" />
                                                            <div class="row">
                                                                <div class="col-md-1 ">
                                                                    <label>ชำระเงิน1+</label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <input ng-model="mainData.BD24CRRG.FCASHTYPE" autocomplete="off" type="text" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" />
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label>ประเภทการชำระเงิน+</label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <select ng-model="mainData.BD24CRRG.FCASHTYPE"
                                                                        class="chosen-select"
                                                                        style="width: 200px;">
                                                                        <option value=""><%=grtt("resCash") %></option>
                                                                        <option value=""><%=grtt("resTranfer") %></option>
                                                                        <option value=""><%=grtt("resCheque") %></option>
                                                                        <option value=""><%=grtt("resCreditCard") %></option>
                                                                    </select>
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label>โอนเงิน+</label>
                                                                    <label><%=grtt("resFBKCHQ") %></label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <select ng-model="mainData.BD24CRRG.FCASHTYPE"
                                                                        class="chosen-select">
                                                                        <option value=""><%=grtt("resPleaseSelect") %></option>
                                                                    </select>
                                                                    <input ng-model="mainData.OD11BKT1.FBKCHQ" type="text" autocomplete="off" style="background-color: #FFE0C0; width: 100px;" class="form-control" maxlength="4" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-3 ">
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCASHTYPE") %></label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <select ng-model="mainData.BD24CRRG.FCASHTYPE"
                                                                        class="chosen-select"
                                                                        style="width: 200px;">
                                                                        <option value=""><%=grtt("resVisa") %></option>
                                                                        <option value=""><%=grtt("resMaster") %></option>
                                                                        <option value=""><%=grtt("resAmex") %></option>
                                                                    </select>
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFBKCHQBR") %></label>
                                                                    <label>เลขที่บัตร+</label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <input ng-model="mainData.OD11BKT1.FBKCHQBR" autocomplete="off" type="text" class="form-control" maxlength="40" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6 ">
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCHQNO") %></label>
                                                                    <label>เลขที่อ้างอิง+</label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <input ng-model="mainData.OD11BKT1.FCHQNO" type="text" autocomplete="off" style="background-color: #FFE0C0;" class="form-control" maxlength="20" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6 ">
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCHQDATE") %></label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <input ng-model="mainData.OD11BKT1.FCHQDATE" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                                </div>
                                                            </div>
                                                            <hr class="width-100" />
                                                            <div class="row">
                                                                <div class="col-md-1 ">
                                                                    <label>ชำระเงิน2+</label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <input ng-model="mainData.BD24CRRG.FCASHTYPE" type="text" autocomplete="off" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" />
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label>ประเภทการชำระเงิน+</label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <select ng-model="mainData.BD24CRRG.FCASHTYPE"
                                                                        class="chosen-select"
                                                                        style="width: 200px;">
                                                                        <option value=""><%=grtt("resCash") %></option>
                                                                        <option value=""><%=grtt("resTranfer") %></option>
                                                                        <option value=""><%=grtt("resCheque") %></option>
                                                                        <option value=""><%=grtt("resCreditCard") %></option>
                                                                    </select>
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label>โอนเงิน+</label>
                                                                    <label><%=grtt("resFBKCHQ") %></label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <select ng-model="mainData.BD24CRRG.FCASHTYPE"
                                                                        class="chosen-select">
                                                                        <option value=""><%=grtt("resPleaseSelect") %></option>
                                                                    </select>
                                                                    <input ng-model="mainData.OD11BKT1.FBKCHQ" type="text" autocomplete="off" style="background-color: #FFE0C0; width: 100px;" class="form-control" maxlength="4" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-3 ">
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCASHTYPE") %></label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <select ng-model="mainData.BD24CRRG.FCASHTYPE"
                                                                        class="chosen-select"
                                                                        style="width: 200px;">
                                                                        <option value=""><%=grtt("resVisa") %></option>
                                                                        <option value=""><%=grtt("resMaster") %></option>
                                                                        <option value=""><%=grtt("resAmex") %></option>
                                                                    </select>
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFBKCHQBR") %></label>
                                                                    <label>เลขที่บัตร+</label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <input ng-model="mainData.OD11BKT1.FBKCHQBR" autocomplete="off" type="text" class="form-control" maxlength="40" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6 ">
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCHQNO") %></label>
                                                                    <label>เลขที่อ้างอิง+</label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <input ng-model="mainData.OD11BKT1.FCHQNO" type="text" autocomplete="off" style="background-color: #FFE0C0;" class="form-control" maxlength="20" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6 ">
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCHQDATE") %></label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <input ng-model="mainData.OD11BKT1.FCHQDATE" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                                </div>
                                                            </div>
                                                            <hr class="width-100" />
                                                            <div class="row">
                                                                <div class="col-md-1 ">
                                                                    <label>ชำระเงิน3+</label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <input ng-model="mainData.BD24CRRG.FCASHTYPE" type="text" autocomplete="off" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" />
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label>ประเภทการชำระเงิน+</label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <select ng-model="mainData.BD24CRRG.FCASHTYPE"
                                                                        class="chosen-select"
                                                                        style="width: 200px;">
                                                                        <option value=""><%=grtt("resCash") %></option>
                                                                        <option value=""><%=grtt("resTranfer") %></option>
                                                                        <option value=""><%=grtt("resCheque") %></option>
                                                                        <option value=""><%=grtt("resCreditCard") %></option>
                                                                    </select>
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label>โอนเงิน+</label>
                                                                    <label><%=grtt("resFBKCHQ") %></label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <select ng-model="mainData.BD24CRRG.FCASHTYPE"
                                                                        class="chosen-select">
                                                                        <option value=""><%=grtt("resPleaseSelect") %></option>
                                                                    </select>
                                                                    <input ng-model="mainData.OD11BKT1.FBKCHQ" type="text" autocomplete="off" style="background-color: #FFE0C0; width: 100px;" class="form-control" maxlength="4" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-3 ">
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCASHTYPE") %></label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <select ng-model="mainData.BD24CRRG.FCASHTYPE"
                                                                        class="chosen-select"
                                                                        style="width: 200px;">
                                                                        <option value=""><%=grtt("resVisa") %></option>
                                                                        <option value=""><%=grtt("resMaster") %></option>
                                                                        <option value=""><%=grtt("resAmex") %></option>
                                                                    </select>
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFBKCHQBR") %></label>
                                                                    <label>เลขที่บัตร+</label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <input ng-model="mainData.OD11BKT1.FBKCHQBR" type="text" autocomplete="off" class="form-control" maxlength="40" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6 ">
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCHQNO") %></label>
                                                                    <label>เลขที่อ้างอิง+</label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <input ng-model="mainData.OD11BKT1.FCHQNO" type="text" autocomplete="off" style="background-color: #FFE0C0;" class="form-control" maxlength="20" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-6 ">
                                                                </div>
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFCHQDATE") %></label>
                                                                </div>
                                                                <div class="col-md-3 ">
                                                                    <input ng-model="mainData.OD11BKT1.FCHQDATE" type="text" autocomplete="off" onkeypress="return Check_Key_Date(this,event)" class="datepicker form-control" data-date-format="dd/mm/yyyy" maxlength="10" />
                                                                </div>
                                                            </div>
                                                            <hr class="width-100" />
                                                            <div class="row">
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFABOOKPAID") %></label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <input ng-model="mainData.OD11BKT1.FABOOKPAID" autocomplete="off" type="text" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-1 ">
                                                                    <label><%=grtt("resFRECVBY") %></label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <input ng-model="mainData.OD11BKT1.FRECVBY" autocomplete="off" type="text" class="form-control" maxlength="6" />
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <label>เลขที่ใบเสร็จ+</label>
                                                                </div>
                                                                <div class="col-md-2 ">
                                                                    <input ng-model="mainData.OD11BKT1.FRECVBY" autocomplete="off" type="text" class="form-control" />
                                                                </div>
                                                                <div class="col-md-4 ">
                                                                    <select ng-model="mainData.OD11BKT1.FRECVBY"
                                                                        class="chosen-select">
                                                                        <option value=""><%=grtt("resPleaseSelect") %></option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-6 ">
                                                    </div>
                                                    <div class="col-md-6 ">
                                                        <div class="panel panel-primary">
                                                            <div class="panel-body">
                                                                <h3>พิมพ์ใบเสร็จ+</h3>
                                                                <hr class="width-100" />
                                                                <div class="col-md-2 ">
                                                                    <label>รายการ+</label>
                                                                </div>
                                                                <div class="col-md-6 ">
                                                                    <select ng-model="mainData.BD24CRRG.FCASHTYPE"
                                                                        class="chosen-select">
                                                                        <option value=""><%=grtt("resPleaseSelect") %></option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <!--Transaction Tab4 โปรโมชั่น List_OD21LAGD List_OD21LAGD2 List_OD21LAPM -->
                                        <div class="row">
                                            <div class="col-md-12">
                                                ข้อมูล Tab4 รายการโปรโมชั่น List_OD21LAGD
                                                 <table>
                                                     <tr ng-repeat="pro1 in mainData.List_OD21LAGD">
                                                         <td>
                                                             <label class="nestcheckbox">
                                                                 <input type="checkbox">&nbsp;</label>
                                                         </td>
                                                         <td>{{ pro1.FTRNNO}}
                                                         </td>
                                                         <td>{{ pro1.FDESCTY}}
                                                         </td>
                                                         <td>{{ pro1.FCREDITCD}}
                                                         </td>
                                                         <td>{{ pro1.FITEMNO}}
                                                         </td>
                                                     </tr>
                                                 </table>
                                            </div>
                                            <div class="col-md-12">
                                                ข้อมูล Tab4 รายการสินค้าโปรโมชั่น List_OD21LAGD2
                                                  <table>
                                                      <tr ng-repeat="pro1 in mainData.List_OD21LAGD2">
                                                          <td>{{ pro2.FTRNNO}}
                                                          </td>
                                                          <td>{{ pro2.FPRMCODE}}
                                                          </td>
                                                          <td>{{ pro2.FPDCODE}}
                                                          </td>
                                                          <td>{{ pro2.ItemNo}}
                                                          </td>
                                                      </tr>
                                                  </table>
                                            </div>
                                            <div class="col-md-12">
                                                ข้อมูล Tab4 ประวัติการเปลี่ยนแปลงรายการโปรโมชั่น List_OD21LAPM
                                                <ul>
                                                    <li ng-repeat="pro_hist in mainData.List_OD21LAPM">{{ pro_hist.FTRNNO}}-
                                                        {{ pro_hist.FAGRNO}}-
                                                        {{ pro_hist.FITEMNO}}
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-2 ">
                                                    </div>
                                                    <div class="col-md-1 ">
                                                        <label>รวมราคา+</label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.OD11BKT1.FABOOKPAID" autocomplete="off" type="text" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" />
                                                    </div>
                                                    <div class="col-md-1 ">
                                                        <label>VAT 7%+</label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.OD11BKT1.FABOOKPAID" autocomplete="off" type="text" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" />
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <label>รวมราคาทั้งหมด+</label>
                                                    </div>
                                                    <div class="col-md-2 ">
                                                        <input ng-model="mainData.OD11BKT1.FABOOKPAID" autocomplete="off" type="text" onkeypress="return Check_Key_Decimal(this,event)" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                        <!--Transaction Tab5 รายละเอียดผู้ติดต่อ OD50RCVD-->
                                        <div class="row">
                                            <div class="col-md-12">
                                                ข้อมูล Tab5 รายละเอียดผู้ติดต่อ OD50RCVD
                                                <br />
                                                FCONTCODE:  {{mainData.OD50RCVD.FCONTCODE}}
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label><%=grtt("resFADD1") %></label>
                                                    </div>
                                                    <div class="col-md-5 ">
                                                        <input ng-model="mainData.OD50RCVD.FADD1" autocomplete="off" type="text" class="form-control" maxlength="255" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                    </div>
                                                    <div class="col-md-5 ">
                                                        <input ng-model="mainData.OD50RCVD.FADD2" autocomplete="off" type="text" class="form-control" maxlength="80" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label><%=grtt("resFADD3") %></label>
                                                    </div>
                                                    <div class="col-md-5 ">
                                                        <input ng-model="mainData.OD50RCVD.FADD3" autocomplete="off" type="text" style="background-color: #FFE0C0;" class="form-control" maxlength="50" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label><%=grtt("resFPROVINCE") %></label>
                                                    </div>
                                                    <div class="col-md-4 ">
                                                        <input ng-model="mainData.OD50RCVD.FPROVINCE" autocomplete="off" type="text" class="form-control" maxlength="25" />
                                                    </div>
                                                    <div class="col-md-1 ">
                                                        <input ng-model="mainData.OD50RCVD.FPOSTAL" autocomplete="off" onkeypress="return Check_Key(event);" type="text" class="form-control" maxlength="5" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label>ตั้งอยู่ที่+</label>
                                                    </div>
                                                    <div class="col-md-5 ">
                                                        <input ng-model="mainData.OD50RCVD.FADD3" autocomplete="off" type="text" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label><%=grtt("resFTELNO") %></label>
                                                    </div>
                                                    <div class="col-md-3 ">
                                                        <input ng-model="mainData.OD50RCVD.FTELNO" autocomplete="off" type="text" class="form-control" maxlength="40" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1 ">
                                                        <label><%=grtt("resFEMAIL") %></label>
                                                    </div>
                                                    <div class="col-md-3 ">
                                                        <input ng-model="mainData.OD50RCVD.FEMAIL" autocomplete="off" type="text" class="form-control" maxlength="50" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <!--Footer content-->
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-lg-9 col-md-9 col-sm-9">
                                            &nbsp;
                                        </div>
                                        <div class="col-lg-3 col-md-3 col-sm-3 text-right">
                                            <a ng-click="save()" class="btn btn-info tooltip-button glyph-icon icon-save"></a>
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
    <script src="Scripts_ngapp/TRN_ReservationAndApproveRecord.js?v=<%=Me.assetVersion %>"></script>
    <script>
        var scopeForDebug = {}
        $(function () {
            scopeForDebug = angular.element('[ng-controller]').scope();
            scopeForDebug.getData();
        })
    </script>


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

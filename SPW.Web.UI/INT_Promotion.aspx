<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="INT_Promotion.aspx.vb" Inherits="SPW.Web.UI.INT_Promotion" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Import Namespace="SPW.Helper" %>
<%@ Import Namespace="SPW.BLL" %>
<%@ Import Namespace="SPW.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modal-dialog {
            position: fixed;
            top: 35%;
            left: 40%;
        }

        .Text_right {
            text-align: right;
        } input.multi {
            display: none;
        }
          .MultiFile-label {
    padding: 5px;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">--%>

                <asp:HiddenField ID="hddParameterMenuID" runat="server" />
                <asp:HiddenField ID="hddReloadGrid" runat="server" />
                <asp:HiddenField ID="hddpSortBy" runat="server" />
                <asp:HiddenField ID="hddpSortType" runat="server" />
                <asp:HiddenField ID="hddpPagingDefault" runat="server" />
                <asp:HiddenField ID="hddKeyID" runat="server" />
                <asp:HiddenField ID="hddpPageInfo" runat="server" />
                <asp:HiddenField ID="hddpSearch" runat="server" />
                <asp:HiddenField ID="hddpFlagSearch" runat="server" />
                <asp:HiddenField ID="hddpActive" runat="server" />
                <asp:HiddenField ID="hddpNotActive" runat="server" />
                <asp:HiddenField ID="hddpAll" runat="server" />
                <asp:HiddenField ID="hddpClientID" runat="server" />
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
                <asp:HiddenField ID="hddaProject" runat="server" />
                <asp:HiddenField ID="hddpLG" runat="server" />
                <asp:HiddenField ID="hddDeleteBeforeCheck" runat="server" />

                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12" style="text-align: left;">
                        <div id="page-wrapper">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <ol class="breadcrumb">
                                            <li>
                                                <i class="glyph-icon icon-globe"></i><a href="Main.aspx">
                                                    <asp:Label ID="lblMain1" runat="server" Font-Underline="true" ForeColor="#1c82e1"></asp:Label></a>
                                            </li>
                                            <li>
                                                <i class="glyph-icon icon-circle-o">
                                                    <asp:Label ID="lblMain2" runat="server"></asp:Label></i>
                                            </li>
                                        </ol>
                                    </div>
                                </div>
                                <asp:Panel ID="pnMain" runat="server" DefaultButton="btnSearch">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12" style="margin-left: 2px;">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-10 col-sm-10">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <h3>
                                                                            <asp:Label ID="lblMain3" runat="server"></asp:Label>
                                                                        </h3>
                                                                    </td>
                                                                    <td style="text-align: right;">
                                                                        <a id="btnMSGAddData" class="btn btn-info glyph-icon icon-plus tooltip-button" runat="server" href="javascript:CallAddData();" Visible='<%#Me.GetPermission().isAdd %>'></a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-lg-12">
                                                        <div class="row">
                                                            <div class="col-lg-8 col-md-12 col-sm-12">
                                                                <div class="row">
                                                                    <div class="col-lg-1 col-md-12 col-sm-12">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsProject" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-8 col-md-12 col-sm-12">
                                                                        <%--<asp:TextBox ID="txtsProject" autocomplete="off" runat="server" CssClass="form-control" onkeypress="return AutocompletedPromotion(this,event);" onClick="AutocompletedPromotion(this,event);"></asp:TextBox>--%>
                                                                        <asp:TextBox ID="txtsProject" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:Label ID="lblMassage6" runat="server" ForeColor="Red"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-1 col-md-12 col-sm-12">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsActive" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-8 col-md-12 col-sm-12">
                                                                        <label>
                                                                        <asp:CheckBox ID="chksActive" runat="server" />
                                                                            <%=grtt("resShowActiveStatus") %>
                                                                            </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-12 col-sm-12">
                                                                <a href="javascript:CallLoaddata();" class="btn  btn-info">
                                                                    <i class="glyph-icon icon-search"></i>
                                                                    <asp:Label ID="lblsSearch" runat="server"></asp:Label>
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            &nbsp;
                                                        </div>
                                                    </div>

                                                    <div class="row">

                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <div class="row" style="overflow-x:auto;overflow-y:hidden;">
                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                    <%
                                                                        Dim lcPromotion As New List(Of Promotion_ViewModel)
                                                                        If False Then
                                                                            'If hddReloadGrid.Value <> String.Empty Then
                                                                            If GetDataPromotion() IsNot Nothing Then
                                                                                lcPromotion = GetDataPromotion()
                                                                    %>
                                                                    <table id="grdView" class="table table-striped table-bordered table-hover" style="width: 100%">
                                                                        <thead>
                                                                            <tr>
                                                                                <th style="text-align: center; vertical-align: text-top;">
                                                                                    <asp:Label ID="TextHd1" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; vertical-align: text-top;">
                                                                                    <asp:Label ID="TextHd2" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; vertical-align: text-top;">
                                                                                    <asp:Label ID="TextHd3" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; vertical-align: text-top;">
                                                                                    <asp:Label ID="TextHd4" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; vertical-align: text-top;">
                                                                                    <asp:Label ID="TextHd5" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; vertical-align: text-top;">
                                                                                    <asp:Label ID="TextHd6" runat="server"></asp:Label></th>
                                                                                <%--<th style="text-align: center; vertical-align: text-top;">
                                                                                    <asp:Label ID="TextHd7" runat="server"></asp:Label></th>--%>
                                                                                <th style="text-align: center; vertical-align: text-top;">
                                                                                    <asp:Label ID="TextHd8" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; vertical-align: text-top;">
                                                                                    <asp:Label ID="TextHd9" runat="server"></asp:Label></th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tfoot style="display: none;">
                                                                            <tr>
                                                                                <th style="text-align: center; text-anchor: middle;">
                                                                                    <asp:Label ID="TextFt1" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; text-anchor: middle;">
                                                                                    <asp:Label ID="TextFt2" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; text-anchor: middle;">
                                                                                    <asp:Label ID="TextFt3" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; text-anchor: middle;">
                                                                                    <asp:Label ID="TextFt4" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; text-anchor: middle;">
                                                                                    <asp:Label ID="TextFt5" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; text-anchor: middle;">
                                                                                    <asp:Label ID="TextFt6" runat="server"></asp:Label></th>
                                                                                <%--<th style="text-align: center; text-anchor: middle;">
                                                                                    <asp:Label ID="TextFt7" runat="server"></asp:Label></th>--%>
                                                                                <th style="text-align: center; text-anchor: middle;">
                                                                                    <asp:Label ID="TextFt8" runat="server"></asp:Label></th>
                                                                                <th style="text-align: center; text-anchor: middle;">
                                                                                    <asp:Label ID="TextFt9" runat="server"></asp:Label></th>
                                                                            </tr>
                                                                        </tfoot>
                                                                        <tbody>
                                                                            <%
                                                                                Dim i As Integer = 0
                                                                                For Each sublcPromotion As Promotion_ViewModel In lcPromotion

                                                                                    Dim strb As New StringBuilder()
                                                                            %>
                                                                            <tr>                                                                                
                                                                                <%
                                                                                    strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-edit'  href='javascript:CallEditData(&#39;" + IIf(sublcPromotion.FPDCODE <> String.Empty, sublcPromotion.FPDCODE, String.Empty) + "&#39;);' class='tooltip-button' data-placement='left' title='Edit Data'></a></td>")
                                                                                    strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-danger glyph-icon icon-trash' href='javascript:ConfirmDelete(&#39;" + IIf(sublcPromotion.FPDCODE <> String.Empty, sublcPromotion.FPDCODE, String.Empty) + "&#39;);' class='tooltip-button' data-placement='left' title='Delete Data'></a></td>")
                                                                                    strb.Append("<td style='width:1%;text-align: center;'>" + i.ToString + "</td>")
                                                                                    strb.Append("<td style='width:15%;'>" + IIf(sublcPromotion.FPDCODE <> String.Empty, sublcPromotion.FPDCODE, String.Empty) + "</td>")
                                                                                    strb.Append("<td style='width:30%;'>" + IIf(Me.WebCulture.ToUpper = "EN", IIf(sublcPromotion.FPDNAME <> String.Empty, sublcPromotion.FPDNAME, String.Empty), IIf(sublcPromotion.FPDNAMET <> String.Empty, sublcPromotion.FPDNAMET, String.Empty)) + "</td>")
                                                                                    strb.Append("<td style='width:20%;'>" + IIf(sublcPromotion.FREPRJNM <> String.Empty, sublcPromotion.FREPRJNM, hddpAll.Value) + "</td>")
                                                                                    strb.Append("<td style='width:20%;'>" + IIf(Me.WebCulture.ToUpper = "EN", IIf(sublcPromotion.FPDNAMETYPE <> String.Empty, sublcPromotion.FPDNAMETYPE, hddpAll.Value), IIf(sublcPromotion.FPDNAMETYPET <> String.Empty, sublcPromotion.FPDNAMETYPET, hddpAll.Value)) + "</td>")
                                                                                    strb.Append("<td style='width:5%;'>" + IIf(sublcPromotion.FNOTUSE = "Y", hddpNotActive.Value, hddpActive.Value) + "</td>")

                                                                                    'strb.Append("<td style='width:10%;'>" + IIf(sublcPromotion.CreateDate IsNot Nothing, CDate(sublcPromotion.CreateDate).ToString("dd/MM/yyyy"), String.Empty) + "</td>")

                                                                                    HttpContext.Current.Response.Write(strb.ToString())
                                                                                %>
                                                                            </tr>
                                                                            <%
                        Next
                                                                            %>
                                                                        </tbody>
                                                                    </table>
                                                                    <%
                                                                            End If
                                                                            hddReloadGrid.Value = String.Empty
                                                                        End If
                                                                    %>
                                                                </div>

                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                    <table id="newGrid" class="table table-striped table-bordered table-hover" width="100%">
                                                                        <thead>
                                                                            <tr>
                                                                                <th><%=grtt("resEDIT") %></th>
                                                                                <th><%=grtt("resDELETE") %></th>
                                                                                <th><%=grtt("resNo.") %></th>
                                                                                <th><%=grtt("resPROMOTIONCODE") %></th>
                                                                                <th><%=grtt("resPROMOTIONNAME") %></th>
                                                                                <th><%=grtt("resFREPRJNM") %></th>
                                                                                <th><%=grtt("resFPDNAMETYPE") %></th>
                                                                                <th><%=grtt("resFNOTUSE") %></th>
                                                                                <th><%=grtt("resFSTATUS") %></th>

                                                                            </tr>
                                                                        </thead>


                                                                    </table>

                                                                    <style>
                                                                        #newGrid td:nth-child(1){ /* first column */
                                                                            text-align: center;
                                                                            width: 1%;
                                                                        }

                                                                        #newGrid td:nth-child(2) { /* second column */
                                                                            text-align: center;
                                                                            width: 1%;
                                                                        }

                                                                        #newGrid td:nth-child(3) {
                                                                            text-align: center;
                                                                            width: 1%;
                                                                        }

                                                                        #newGrid td:nth-child(4) {
                                                                            width: 15%;
                                                                        }

                                                                        #newGrid td:nth-child(5) {
                                                                            width: 30%;
                                                                        }

                                                                        #newGrid td:nth-child(6) {
                                                                            width: 20%;
                                                                        }

                                                                        #newGrid td:nth-child(7) {
                                                                            width: 20%;
                                                                        }

                                                                        #newGrid td:nth-child(8) {
                                                                            width: 5%;
                                                                        }
                                                                        #newGrid td:nth-child(9) {
                                                                            width: 15%;
                                                                        }
                                                                    </style>

                                                                </div>


                                                            </div>
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </asp:Panel>
                                <asp:Panel ID="pnDialog" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-9 col-sm-9">
                                            <div class="container-fluid">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <div class="row">
                                                            <div class="panel panel-primary">
                                                                <div class="panel-heading">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <h3 class="panel-title">
                                                                                    <asp:Label ID="lblMain4" runat="server"></asp:Label>
                                                                                </h3>
                                                                            </td>
                                                                            <td style="text-align: right;">

                                                                                <asp:Button ID="btnSaveTemp" runat="server" OnClientClick="javascript:CheckData();return false" CssClass="hide" />


                                                                                <asp:Button ID="btnApprove" runat="server" CssClass="hide" />

                                                                                <asp:Button ID="btnUnApprove" runat="server" CssClass="hide" />
                                                                                <a id="btnMSGApprove" runat="server" href="javascript:CallApprove();" class="btn glyph-icon icon-approve btn-success tooltip-button" Visible='<%#Me.GetPermission().isApprove %>'></a>
                                                                                <a id="btnMSGUnApprove" runat="server" href="javascript:CallUnApprove();" class="btn glyph-icon icon-unapprove btn-warning tooltip-button" Visible='<%#Me.GetPermission().isApprove %>'></a>
                                                                                <%If (hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit) Then %>
                                                                                    <a id="btnMSGSaveData" runat="server" href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button"></a>
                                                                                <%End If %>
                                                                                <a id="btnMSGCancelData" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="panel-body <%=IIf((hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit), "", "disabled") %>">
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaPromotionCode" runat="server"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaPromotionCode" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="50" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipPromotionCode") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-8 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaPromotionName" autocomplete="off" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                                                            <asp:Label ID="lblLGNameEN" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-4 col-md-12 col-sm-12">
                                                                            &nbsp;
                                                                        </div>
                                                                        <div class="col-lg-8 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaPromotionNameT" autocomplete="off" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                                                            <asp:Label ID="lblLGNameTH" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaPromotionAC" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaPromotionAC" autocomplete="off" runat="server" CssClass="form-control" MaxLength="20" BackColor="#F5F5F5"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: center;">
                                                                            <asp:Label ID="lblaApproveBy" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaApproveBy" autocomplete="off" runat="server" CssClass="form-control" Enabled="false" BackColor="#F5F5F5"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaFlagPromotionStatus" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:CheckBox ID="chkaFlagPromotionStatus" runat="server" />
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaFlagStandBooking" runat="server" Visible="false"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-1 col-md-12 col-sm-12">
                                                                            <asp:CheckBox ID="chkaFlagStandBooking" runat="server" Visible="false" />
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-4 col-md-12 col-sm-12" style="vertical-align: middle;">
                                                                            <fieldset>
                                                                                <legend>&nbsp;
                                                                                </legend>
                                                                                <div class="row">
                                                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                        <asp:CheckBox ID="rdbFlagUtility" runat="server" onchange="checkGridNull();"/>
                                                                                    </div>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div class="col-lg-8 col-md-12 col-sm-12">
                                                                            <fieldset>
                                                                                <legend>
                                                                                    <asp:Label ID="lblMain5" runat="server"></asp:Label>
                                                                                </legend>
                                                                                <div class="row">
                                                                                    <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                        <asp:CheckBox ID="rdbFlagPR" runat="server" />
                                                                                    </div>
                                                                                    <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                        <asp:CheckBox ID="chkFlagVat" runat="server" onclick="CalTotalAll()" />
                                                                                    </div>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaPriceNotOver" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaPriceNotOver" autocomplete="off" runat="server" CssClass="form-control Text_right" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return CheckMustnotOver();"></asp:TextBox>
                                                                            <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: center;">
                                                                            <asp:Label ID="lblaPriceCashDiscount" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaPriceCashDiscount" autocomplete="off" runat="server" CssClass="form-control Text_right" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat(this,event);"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaProject" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-10 col-md-12 col-sm-12">
                                                                            <asp:DropDownList ID="ddlaProject" AppendDataBoundItems="True" runat="server" CssClass="chosen-select" ></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaFPDCode" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-10 col-md-12 col-sm-12">
                                                                            <table style="width: 100%">
                                                                                <tr>
                                                                                    <td style="width: 30%">
                                                                                        <asp:TextBox ID="txtaFPDCode" autocomplete="off" runat="server" BackColor="#ffe0c0" CssClass="form-control" onkeypress="return AutocompletedFPD(this,event);" onClick="AutocompletedFPD(this,event);"></asp:TextBox>
                                                                                        <asp:HiddenField ID="hddaFPDCode" runat="server" />
                                                                                        <asp:HiddenField ID="hddaFPDName" runat="server" />
                                                                                        <asp:Button ID="btnReloadFPD" runat="server" CssClass="hide" />
                                                                                    </td>
                                                                                    <td style="width: 70%">
                                                                                        <asp:TextBox ID="txtaFPDName" autocomplete="off" runat="server" CssClass="form-control" Enabled="false" BackColor="#F5F5F5"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaDecsription" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-10 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaDecsription" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                  
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12" style="vertical-align: middle;">
                                                                            <div class="panel panel-primary">
                                                                                <div class="panel-body">
                                                                                    <div class="row">
                                                                                        <div class="col-lf-12 col-md-12 col-sm-12">
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:GridView ID="grdView2" runat="server" CssClass="table table-hover table-striped table-bordered table-responsive"
                                                                                                        EmptyDataText="No Items." EmptyDataRowStyle-CssClass="gridview-nodata" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" Style="width: 100%">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField>
                                                                                                                <HeaderTemplate>
                                                                                                                    <%# GetWebMessage("gProductCode", "Text", hddParameterMenuID.Value)%>
                                                                                                                </HeaderTemplate>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtgProductCode" autocomplete="off" runat="server" BackColor="#ffe0c0" CssClass="form-control" Style="height: 28px; vertical-align: top;"></asp:TextBox>
                                                                                                                    <asp:HiddenField ID="hddID" runat="server" />
                                                                                                                    <asp:HiddenField ID="hddFlagSetAdd" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle />
                                                                                                                <ItemStyle HorizontalAlign="left" Width="15%" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <HeaderTemplate>
                                                                                                                    <%# GetWebMessage("gProductName", "Text", hddParameterMenuID.Value)%>
                                                                                                                </HeaderTemplate>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtgProductName" autocomplete="off" runat="server" CssClass="form-control" Style="height: 28px; vertical-align: top;" Enabled="false"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle />
                                                                                                                <ItemStyle HorizontalAlign="left" Width="28%" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <HeaderTemplate>
                                                                                                                    <%# GetWebMessage("gUOM", "Text", hddParameterMenuID.Value)%>
                                                                                                                </HeaderTemplate>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtgUOM" autocomplete="off" runat="server" CssClass="form-control" Style="height: 28px; vertical-align: top;" Enabled="false"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle />
                                                                                                                <ItemStyle HorizontalAlign="right" Width="10%" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <HeaderTemplate>
                                                                                                                    <%# GetWebMessage("gQTY", "Text", hddParameterMenuID.Value)%>
                                                                                                                </HeaderTemplate>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtgQTY" autocomplete="off" runat="server" CssClass="form-control text-right" Style="height: 28px; vertical-align: top;" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat0(this,event);"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle />
                                                                                                                <ItemStyle HorizontalAlign="right" Width="10%" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <HeaderTemplate>
                                                                                                                    <%# GetWebMessage("gUnitPrice", "Text", hddParameterMenuID.Value)%>
                                                                                                                </HeaderTemplate>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtgUnitPrice" autocomplete="off" runat="server" CssClass="form-control text-right" Style="height: 28px; vertical-align: top;" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat(this,event);"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle />
                                                                                                                <ItemStyle HorizontalAlign="right" Width="16%" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <HeaderTemplate>
                                                                                                                    <%# GetWebMessage("gTotalPrice", "Text", hddParameterMenuID.Value)%>
                                                                                                                </HeaderTemplate>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtgTotalPrice" autocomplete="off" runat="server" CssClass="form-control text-right" Style="height: 28px; vertical-align: top;" onKeyPress="return Check_Key_Decimal(this,event)" onblur="CalTotalAll();return setFormat(this,event);" Enabled="false"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle />
                                                                                                                <ItemStyle HorizontalAlign="right" Width="16%" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <HeaderTemplate>
                                                                                                                    <%# GetWebMessage("col_delete", "Text", "1")%>
                                                                                                                </HeaderTemplate>
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="btnDelete" ImageUrl="~/image/delete.jpg" Style="width: 30px; height: 30px; margin-left: 10px; margin-top: 7px;" ToolTip="Click to Delete" />
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle />
                                                                                                                <ItemStyle HorizontalAlign="Center" Width="5%" VerticalAlign="Middle" />
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                    <asp:Button ID="btnGridAdd" runat="server" CssClass="hide" />
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <table style="width: 100%">
                                                                                <tr>
                                                                                    <td style="width: 10%; text-align: center;">
                                                                                        <asp:Label ID="lblaTotalPrice" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 20%">
                                                                                        <asp:TextBox ID="txtaTotalPrice" autocomplete="off" runat="server" CssClass="form-control text-right" Enabled="false" BackColor="#F5F5F5" Style="width: 98%"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 10%; text-align: center;">
                                                                                        <asp:Label ID="lblaVat" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 20%">
                                                                                        <asp:TextBox ID="txtaVat" runat="server" CssClass="form-control text-right" Enabled="false" BackColor="#F5F5F5" Style="width: 98%"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 10%; text-align: center;">
                                                                                        <asp:Label ID="lblaTotalAll" runat="server"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 20%">
                                                                                        <asp:TextBox ID="txtaTotalAll" autocomplete="off" runat="server" CssClass="form-control text-right" Enabled="false" BackColor="#F5F5F5"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>  <br />





                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12" style="vertical-align: middle;">
                                                                            <div class="panel panel-primary">
                                                                                <div class="panel-body">
                                                                                    <div class="row">
                                                                                        <div class="col-lf-12 col-md-12 col-sm-12">

                                                                                                <h3><%=grtt("resCurrentPromotionFile") %></h3>
                                                                                                <asp:Repeater ID="rptPdf" runat="server">
                                                                                                    <ItemTemplate>
                                                                                                        <div class="MultiFile-label col-sm-12">
                                                                                                            <a class="btn btn-danger MultiFile-remove glyph-icon icon-trash"
                                                                                                                onclick="$(this).next().prop('checked', true).parent().hide()"></a>
                                                                                                            <input type="checkbox" id="chkDeletePdf" runat="server" class="hidden" />
                                                                                                            <asp:HyperLink ID="hplPdf" runat="server" Target="_blank"></asp:HyperLink>
                                                                                                            <br />
                                                                                                        </div>
                                                                                                    </ItemTemplate>
                                                                                                </asp:Repeater>
                                                                                            <hr class="width-100" />
                                                                                                <h3><%=grtt("resAddNewPromotionFile") %></h3>
                                                                                                <asp:FileUpload ID="fileUploadMulti" runat="server" class="multi nutClick" accept=".pdf" />
                                                                                                <br />
                                                                                                <br />
                                                                                                <a class="btn btn-info" onclick="$('.nutClick').trigger('click');">
                                                                                                    <i class="glyph-icon icon-plus"></i>
                                                                                                    <%=grtt("resAddPromotionFile") %>
                                                                                                </a>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>





                                                                </div>
                                                                <div class="panel-heading">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td></td>
                                                                            <td style="text-align: right;">
                                                                                <%If (hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit) Then %>
                                                                                    <a id="A1" runat="server" href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button"></a>
                                                                                <%End if %>
                                                                                <a id="A2" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <%--<div class="panel-footer">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            
                                                                        </div>
                                                                    </div>
                                                                </div>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
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
                                    <asp:Label ID="lblHeaderDelete" runat="server" ForeColor="red"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <asp:TextBox ID="lblBodydelete" runat="server" BackColor="White" BorderStyle="None" BorderWidth="0" Enabled="false" Style="width: 100%"></asp:TextBox>
                                <asp:HiddenField ID="hddBodydelete" runat="server" />
                            </div>
                            <div class="modal-footer">
                                <button type="button" id="btnMSGDeleteData" runat="server" class="btn btn-danger" onclick="javascript:CallDeleteData();"></button>
                                <button type="button" id="btnMSGCancelDataS" runat="server" class="btn btn-default" data-dismiss="modal"></button>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnSearch" runat="server" CssClass="hide" />
                <asp:Button ID="btnAdd" runat="server" CssClass="hide" />
                <asp:Button ID="btnEdit" runat="server" CssClass="hide" />
                <asp:Button ID="btnDelete" runat="server" CssClass="hide" />
                <asp:Button ID="btnSave" runat="server" CssClass="hide" />
                <asp:Button ID="btnCancel" runat="server" CssClass="hide" />
            <%--</asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnReloadFPD" />
        </Triggers>
    </asp:UpdatePanel>--%>

    <script src="Scripts_custom/jquery.MultiFile_custompdf.js?v=<%=Me.assetVersion %>"></script>
    <div class="modal fade .bs-example-modal-sm" id="overlay" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000; opacity: 0.6;">
            <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #000; font-size: 36px; left: 40%; top: 30%;">
                <img src="image/loading.gif" alt="Loading ..." style="height: 200px" /></span>
        </div>
    </div>
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="Script/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            
            SetInitial();
        });

        function SetInitial() {
           
            var hddpPageInfo = document.getElementById("<%= hddpPageInfo.ClientID%>");
            var hddpSearch = document.getElementById("<%= hddpSearch.ClientID%>");
            var hddpPagingDefault = document.getElementById("<%= hddpPagingDefault.ClientID%>");
            <%--if ($('#grdView') != null) {
                if (!$.fn.DataTable.isDataTable('#grdView')) {
                    var t = $('#grdView').DataTable({
                        "order": [[$('#<%=hddpSortBy.ClientID%>').val(), $('#<%=hddpSortType.ClientID%>').val()]],
                        "pageLength": parseInt($('#<%=hddpPagingDefault.ClientID%>').val()),
                        "bDeferRender": true,
                        "columnDefs": [{
                            "searchable": false,
                            "orderable": false,
                            "targets": [0, 6, 7]
                        }]
                    });

                    t.on('order.dt search.dt', function () {
                        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                            cell.innerHTML = i + 1;
                        });
                    }).draw();

                    t.on('page.dt', function () {
                        var info = t.page.info();
                        hddpPageInfo.value = info.page;
                    })

                    $('ul.pagination').on('click', function (e) {
                        var info = t.page.info();
                        hddpPageInfo.value = info.page;
                    });

                    if ($('#<%=hddpPageInfo.ClientID%>').val() != "") {
                        t.page(parseInt($('#<%=hddpPageInfo.ClientID%>').val())).draw(false);
                    }

                    t.on('search.dt keyup', function () {
                        var values = $('.dataTables_filter input').val();
                        hddpSearch.value = values;

                        var info = t.page.info();
                        hddpPageInfo.value = info.page;
                    })

                    if ($('#<%=hddpSearch.ClientID%>').val() != "") {
                        t.search($('#<%=hddpSearch.ClientID%>').val()).draw(false);
                    }

                    t.on('length.dt', function (e, settings, len) {
                        hddpPagingDefault.value = len;

                        var info = t.page.info();
                        hddpPageInfo.value = info.page;
                    });
                   
                }
            }--%>

          

              <% 

        Dim lcPromotion As New List(Of Promotion_ViewModel)
        If hddReloadGrid.Value <> String.Empty Then
            If GetDataPromotion() IsNot Nothing Then
                lcPromotion = GetDataPromotion()


                Dim js = New System.Web.Script.Serialization.JavaScriptSerializer
                js.MaxJsonLength = 999999999
                                             %>  
            var dataSource= <%=js.Serialize(lcPromotion) %>;
            var resAll='<%=grtt("resAll")%>';
            var culture ='<%=Me.WebCulture.ToUpper%>';
            for(i=0;i<dataSource.length;i++){
                var d= dataSource[i];
                if(culture=='TH'){
                    d.FPDNAME=d.FPDNAMET
                    d.FPDNAMETYPE=d.FPDNAMETYPET
                }
                if(d.FNOTUSE == 'Y'){
                    d.FNOTUSE='<%=grtt("resInActive")%>'
                }else{
                    d.FNOTUSE='<%=grtt("resActive")%>'
                }
                d.FREPRJNM=isEmpty(d.FREPRJNM)?resAll:d.FREPRJNM;
                d.FPDNAMETYPE=isEmpty(d.FPDNAMETYPE)?resAll:d.FPDNAMETYPE;
                if (!isEmpty(d.FUPDFLAG)){
                    if (d.FUPDFLAG == "Y"){
                        d.FUPDFLAG = "Approved";
                    }
                }
                d.EditButton='<a class="btn btn-info glyph-icon icon-edit"';
                d.EditButton+=' href="javascript:CallEditData(&#39;'+ d.FPDCODE+'&#39;)"></a>';
                if (!isEmpty(d.FUPDFLAG)){
                    if (d.FUPDFLAG == "Approved"){
                        d.DeleteButton='';
                    }else{
                        d.DeleteButton='<a class="btn btn-danger glyph-icon icon-trash"';
                        d.DeleteButton+=' href="javascript:ConfirmDelete(&#39;'+ d.FPDCODE+'&#39;)"></a>';
                    }
                }else{
                    d.DeleteButton='<a class="btn btn-danger glyph-icon icon-trash"';
                    d.DeleteButton+=' href="javascript:ConfirmDelete(&#39;'+ d.FPDCODE+'&#39;)"></a>';
                }
                
                                                
            };
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
        
            var isDelete = <%#Me.GetPermission().isDelete.ToString.ToLower %>;
            var grid =$('#newGrid').DataTable({
                "order": [[$('#<%=hddpSortBy.ClientID%>').val(), $('#<%=hddpSortType.ClientID%>').val()]],
                "pageLength": parseInt($('#<%=hddpPagingDefault.ClientID%>').val()),
                "columnDefs": [{
                    "searchable": false,
                    "orderable": false,
                    "targets": [0, 1, 2]
                },
                { "targets": [1], "visible": isDelete }],
                   
                data: dataSource,
                columns: [
                    { "data": "EditButton" },
                    { "data": "DeleteButton" },
                    { "data": "FPDCODE" },
                    { "data": "FPDCODE" },
                    { "data": "FPDNAME" },
                    { "data": "FREPRJNM" },
                    { "data": "FPDNAMETYPE" },
                    { "data": "FNOTUSE" },
                    { "data": "FUPDFLAG" }
                ]
            });
            //,
            //    "scrollY": "400px",
            //    "scrollX": true
            //try{
            //    new $.fn.dataTable.FixedColumns(grid);
            //}catch(ex){
            //    console.log(ex);
            //}

            grid.on('order.dt search.dt', function () {
                grid.column(2, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
            grid.on('page.dt', function () {
                var info = grid.page.info();
                hddpPageInfo.value = info.page;
            })

            $('ul.pagination').on('click', function (e) {
                var info = grid.page.info();
                hddpPageInfo.value = info.page;
            });

            if ($('#<%=hddpPageInfo.ClientID%>').val() != "") {
                grid.page(parseInt($('#<%=hddpPageInfo.ClientID%>').val())).draw(false);
            }

            grid.on('search.dt keyup', function () {
                var values = $('.dataTables_filter input').val();
                hddpSearch.value = values;

                var info = grid.page.info();
                hddpPageInfo.value = info.page;
            })

            if ($('#<%=hddpSearch.ClientID%>').val() != "") {
                grid.search($('#<%=hddpSearch.ClientID%>').val()).draw(false);
            }

            grid.on('length.dt', function (e, settings, len) {
                hddpPagingDefault.value = len;

                var info = grid.page.info();
                hddpPageInfo.value = info.page;
            });
            <% End If
            hddReloadGrid.Value = String.Empty
        End If
            %>

        }

        function showOverlay() {
            $("#overlay").modal();
        }

        function CallCancel() {
            showOverlay();
            __doPostBack('<%= btnCancel.UniqueID%>');
        }

        function CallApprove() {
            showOverlay();
            __doPostBack('<%= btnApprove.UniqueID%>');
        }

        function CallUnApprove() {
            showOverlay();
            __doPostBack('<%= btnUnApprove.UniqueID%>');
        }

        function CallLoaddata() {            
           <%-- if ($('#<%=txtsProject.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtsProject.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "";
                return;
            } else {
                $('#<%=txtsProject.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "none";
            }--%>
            showOverlay();
            __doPostBack('<%= btnSearch.UniqueID%>');
        }

        function CallAddData() {
            showOverlay();
            __doPostBack('<%= btnAdd.UniqueID%>');
        }

        function CallEditData(pKey) {
            showOverlay();
            var hddKeyID = document.getElementById("<%= hddKeyID.ClientID%>");
            hddKeyID.value = pKey;
            __doPostBack('<%= btnEdit.UniqueID%>');
        }

        function CallDeleteData() {
            showOverlay();
            __doPostBack('<%= btnDelete.UniqueID%>');
        }

        function AutocompletedPromotion(txt, e) {
            var key;
            if (document.all) {
                key = window.event.keyCode;
            }
            else {
                try {
                    key = e.which;
                } catch (s) {

                }
            }

            if (key == 47 ||
                key == 91 ||
                key == 38 ||
                key == 92 ||
                key == 35 ||
                key == 44 ||
                key == 43 ||
                key == 36 ||
                key == 126 ||
                key == 37 ||
                key == 39 ||
                key == 34 ||
                key == 42 ||
                key == 63 ||
                key == 60 ||
                key == 62 ||
                key == 123 ||
                key == 125 ||
                key == 93 ||
                key == 33 ||
                key == 64 ||
                key == 94) {
                return false;
            }
            var hddpLG = document.getElementById("<%= hddpLG.ClientID%>");
            $("#<%=txtsProject.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("./API/Api_Promotion.asmx/GetPromotion")%>',
                        data: "{ 'ptKeyID': '" + request.term + "', 'ptLG': '" + hddpLG.value + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('|')[0],
                                    val: item.split('|')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=txtsProject.ClientID%>").val(i.item.label);
                },
                minLength: 0,
                matchContains: true
            }).on('click', function () { $(this).keydown(); });

            }

            function AutocompletedFPD(txtaFPDCode, e) {
                var key;
                if (document.all) {
                    key = window.event.keyCode;
                }
                else {
                    try {
                        key = e.which;
                    } catch (s) {

                    }
                }

                if (key == 47 ||
                    key == 91 ||
                    key == 38 ||
                    key == 92 ||
                    key == 35 ||
                    key == 44 ||
                    key == 43 ||
                    key == 36 ||
                    key == 126 ||
                    key == 37 ||
                    key == 39 ||
                    key == 34 ||
                    key == 42 ||
                    key == 63 ||
                    key == 60 ||
                    key == 62 ||
                    key == 123 ||
                    key == 125 ||
                    key == 93 ||
                    key == 33 ||
                    key == 64 ||
                    key == 94) {
                    return false;
                }
                var hddaProject = document.getElementById("<%= hddaProject.ClientID%>");
                var ddlaProject = document.getElementById("<%= ddlaProject.ClientID%>");
                if (ddlaProject.value == "") {
                    hddaProject.value = "";
                }
                $("#<%=txtaFPDCode.ClientID%>").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("./API/Api_Promotion.asmx/GetFPD")%>',
                            data: "{ 'ptFPD': '" + request.term + "', 'ptProject': '" + hddaProject.value + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('|')[0],
                                        val: item.split('|')[1]
                                    }
                                }))
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    },
                    select: function (e, i) {
                        $("#<%=txtaFPDCode.ClientID%>").val(i.item.label);
                        $("#<%=txtaFPDName.ClientID%>").val(i.item.val);

                        //PostbackSelectFPD();
                    },
                    minLength: 0,
                    matchContains: true
                }).on('click', function () { $(this).keydown(); });
               <%-- var hddaFPDName = document.getElementById("<%=hddaFPDName.ClientID%>");
                var txtaFPDCode = document.getElementById("<%=txtaFPDCode.ClientID%>");
                var txtaFPDName = document.getElementById("<%=txtaFPDName.ClientID%>");
                if (hddaFPDName.value != "")
                {
                    txtaFPDCode.value = hddaFPDName.value.split(':')[0]
                    txtaFPDName.value = hddaFPDName.value.split(':')[1]
                }--%>
            }

       

        function AutocompletedProduct(txtgProductCode, txtgProductName, txtgUOM, IDs, e) {
            var txtgProductCode = document.getElementById(txtgProductCode);
            var txtgProductName = document.getElementById(txtgProductName);
            var txtgUOM = document.getElementById(txtgUOM);
            var key;
            if (document.all) {
                key = window.event.keyCode;
            }
            else {
                try {
                    key = e.which;
                } catch (s) {

                }
            }

            if (key == 47 ||
                key == 91 ||
                key == 38 ||
                key == 92 ||
                key == 35 ||
                key == 44 ||
                key == 43 ||
                key == 36 ||
                key == 126 ||
                key == 37 ||
                key == 39 ||
                key == 34 ||
                key == 42 ||
                key == 63 ||
                key == 60 ||
                key == 62 ||
                key == 123 ||
                key == 125 ||
                key == 93 ||
                key == 33 ||
                key == 64 ||
                key == 94) {
                return false;
            }
            var hddpLG = document.getElementById("<%= hddpLG.ClientID%>");
            var rdbFlagUtility = document.getElementById("<%= rdbFlagUtility.ClientID%>");
            $(txtgProductCode).autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("./API/Api_Promotion.asmx/GetProduct")%>',
                        data: "{ 'ptKeyID': '" + request.term + "', 'ptLG': '" + hddpLG.value + "', 'ptFlag': '" + rdbFlagUtility.checked + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('|')[0] + " - " + item.split('|')[1] + " - " + item.split('|')[2],
                                    value: item.split('|')[0]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    event.preventDefault();
                    $(txtgProductCode).val(i.item.value);
                    $(txtgProductName).val(i.item.label.split(' - ')[1]);
                    $(txtgUOM).val(i.item.label.split(' - ')[2]);
                    var chkBool = true;
                    var pDup;
                    var grid = document.getElementById("<%= grdView2.ClientID %>");
                    if (grid != null) {
                        var count = grid.rows.length;
                        if (count > 3) {
                            for (var i = 0; i < grid.rows.length; i++) {
                                inputs = grid.rows[i].getElementsByTagName("input");
                                for (var j = 0; j < inputs.length; j++) {
                                    if (inputs[j].type == "text") {
                                        if (j == "0") {
                                            if (inputs[1].value != IDs) {
                                                if (txtgProductCode.value == inputs[j].value) {
                                                    chkBool = false;
                                                    pDup = inputs[j].value;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (chkBool == false) {
                        txtgProductCode.value = "";
                        txtgProductName.value = "";
                        txtgUOM.value = "";
                        OpenDialogError("<%# Me.GetResource("msg_duplicate_table", "MSG", "1") %>" + pDup);
                        return false;
                    }

                },
                minLength: 0,
                matchContains: true
            }).on('click', function () { $(this).keydown(); });

        }

        function PostbackSelectFPD() {
            __doPostBack('<%= btnReloadFPD.UniqueID%>');
            }

            function addRowGridView(txtClientID) {
                var hddpClientID = document.getElementById("<%= hddpClientID.ClientID%>");
                hddpClientID.value = txtClientID
                __doPostBack('<%= btnGridAdd.UniqueID%>');
        }

        function CalTotalAll() {
            var pTotalCal = 0.0;
            var txtaTotalPrice = document.getElementById("<%= txtaTotalPrice.ClientID %>");
            var txtaVat = document.getElementById("<%= txtaVat.ClientID %>");
            var txtaTotalAll = document.getElementById("<%= txtaTotalAll.ClientID %>");
            var grid = document.getElementById("<%= grdView2.ClientID %>");
            var isIncludeVat = !($('#<%= chkFlagVat.ClientID %>').prop('checked'));

            if (grid != null) {
                var count = grid.rows.length;
                if (count != null) {
                    for (var i = 0; i < grid.rows.length; i++) {
                        inputs = grid.rows[i].getElementsByTagName("input");
                        for (var j = 0; j < inputs.length; j++) {
                            if (inputs[j].type == "text") {
                                if (j == "7") {
                                    txtgTotalPrice = inputs[j].value;
                                    if (txtgTotalPrice == "") {
                                        txtgTotalPrice = 0;
                                    }
                                    pTotalCal = parseFloat(numberWithCommasValue(pTotalCal)) + parseFloat(numberWithCommasValue(txtgTotalPrice));
                                    if (txtgTotalPrice == 0) {
                                        txtgTotalPrice = "";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            txtaTotalPrice.value = "";
            txtaVat.value = "";
            txtaTotalAll.value = "";
            if (pTotalCal != "") {
                txtaTotalPrice.value = fnFormatMoney(parseFloat(pTotalCal));
                if(isIncludeVat){
                  txtaVat.value = fnFormatMoney(parseFloat(parseFloat(pTotalCal) * parseInt(7)) / parseInt(100));
                }else{
                    txtaVat.value='0'
                }
                txtaTotalAll.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalPrice.value, ',', '')) + parseFloat(replaceAll(txtaVat.value, ',', '')))

                txtaTotalPrice.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalPrice.value, ',', '')));
                txtaVat.value = fnFormatMoney(parseFloat(replaceAll(txtaVat.value, ',', '')));
                txtaTotalAll.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalAll.value, ',', '')));
            }

            CheckMustnotOver();
        }

        function CheckMustnotOver() {
            var txtaTotalAll = document.getElementById("<%= txtaTotalAll.ClientID %>");
            var txtaPriceNotOver = document.getElementById("<%= txtaPriceNotOver.ClientID %>");
            if (txtaPriceNotOver.value != "" && txtaTotalAll.value != "") {
                var PriceNotOver = replaceAll(txtaPriceNotOver.value, ',', '')
                var TotalAll = replaceAll(txtaTotalAll.value, ',', '')
                if (parseFloat(TotalAll) > parseFloat(PriceNotOver)) {
                    $('#<%=txtaPriceNotOver.ClientID%>').addClass("parsley-error")
                    $('#<%=txtaTotalAll.ClientID%>').addClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                } else {
                    $('#<%=txtaPriceNotOver.ClientID%>').removeClass("parsley-error")
                    $('#<%=txtaTotalAll.ClientID%>').removeClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
                }
            }
            if (txtaPriceNotOver.value != "") {
                txtaPriceNotOver.value = fnFormatMoney(parseFloat(replaceAll(txtaPriceNotOver.value, ',', '')));
            }
        }

        function CalTotalInGridAll(txtgQTY, txtgUnitPrice, txtgTotalPrice) {
            var txtgQTY = document.getElementById(txtgQTY);
            var txtgUnitPrice = document.getElementById(txtgUnitPrice);
            var txtgTotalPrice = document.getElementById(txtgTotalPrice);

            if (txtgQTY.value != "" && txtgUnitPrice.value != "") {
                txtgTotalPrice.value = fnFormatMoney(parseFloat(replaceAll(txtgQTY.value, ',', '')) * parseFloat(replaceAll(txtgUnitPrice.value, ',', '')));


                txtgUnitPrice.value = fnFormatMoney(parseFloat(replaceAll(txtgUnitPrice.value, ',', '')));
                txtgTotalPrice.value = fnFormatMoney(parseFloat(replaceAll(txtgTotalPrice.value, ',', '')));
            }else if(txtgQTY.value == "" || txtgUnitPrice.value == "") {
                txtgTotalPrice.value = "";
            }

            CalTotalAll();
        }

        function ConfirmDelete(pKey) {
            var hddKeyID = document.getElementById("<%= hddKeyID.ClientID%>");
            var lblBodydelete = document.getElementById("<%= lblBodydelete.ClientID%>");

            hddKeyID.value = pKey;
            lblBodydelete.value = lblBodydelete.defaultValue + " " + pKey + " ?";

            $("#DeleteModal").modal();
        }

        function CallSaveData() {
            showOverlay();
            __doPostBack('<%= btnSave.UniqueID%>');
            return;
        }

        function CheckData() {
            if ($('#<%=txtaPromotionCode.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaPromotionCode.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";                             
                scrollAndFocus('#<%=txtaPromotionCode.ClientID%>');
                return;
            } else {
                $('#<%=txtaPromotionCode.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }

            var txtaTotalAll = document.getElementById("<%= txtaTotalAll.ClientID %>");
            var txtaPriceNotOver = document.getElementById("<%= txtaPriceNotOver.ClientID %>");
            if (txtaPriceNotOver.value != "" && txtaTotalAll.value != "") {
                var PriceNotOver = replaceAll(txtaPriceNotOver.value, ',', '')
                var TotalAll = replaceAll(txtaTotalAll.value, ',', '')
                if (parseFloat(TotalAll) > parseFloat(PriceNotOver)) {
                    $('#<%=txtaPriceNotOver.ClientID%>').addClass("parsley-error")
                    $('#<%=txtaTotalAll.ClientID%>').addClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";                            
                    scrollAndFocus('#<%=txtaPriceNotOver.ClientID%>');
                    return;
                } else {
                    $('#<%=txtaPriceNotOver.ClientID%>').removeClass("parsley-error")
                    $('#<%=txtaTotalAll.ClientID%>').removeClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
                }
            }


            CallSaveData();
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

        function setFormat(txt, e) {
            var txt = document.getElementById(txt.id);
            if (txt.value == "") {
                txt.value = 0.00;
            }
            txt.value = fnFormatMoney(parseFloat(replaceAll(txt.value, ',', '')));

            if (txt.value == "0.00") {
                txt.value = "";
            }
        }

        function setFormat0(txt, e) {
            var txt = document.getElementById(txt.id);
            if (txt.value == "") {
                txt.value = 0;
            }
            txt.value = fnFormatMoney0(parseFloat(replaceAll(txt.value, ',', '')));

            if (txt.value == "0") {
                txt.value = "";
            }
        }

        function replaceAll(str, find, replace) {
            return str.replace(new RegExp(find, 'g'), replace);
        }

        function fnFormatMoney(values) {
            return values.toFixed(2).replace(/./g, function (c, i, a) { return i && c !== "." && ((a.length - i) % 3 === 0) ? ',' + c : c; });
        }

        function fnFormatMoney0(values) {
            return values.toFixed(0).replace(/./g, function (c, i, a) { return i && c !== "." && ((a.length - i) % 3 === 0) ? ',' + c : c; });
        }

        function numberWithCommasValue(valWithComma) {
            return valWithComma.toString().replace(/,/g, '');
        }

        function numberWithCommas0(x) {
            if (x) {
                x = numberWithCommasValue(x);
                return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            }
            return '0';
        }

        function numberWithCommas(x) {
            if (x) {
                x = numberWithCommasValue(x);
                if (!isNaN(x))
                    x = roundToTwo(x);
                var result = x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                if (result.indexOf('.') == -1)
                    return result + '.00';
                else if (result.indexOf('.') + 2 == result.length) // 12345.4 to 12345.40
                    return result + '0';
                return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            }
            return '0.00';
        }

        function Check_Key_Decimal(txtMoney, e)//check key number&dot only and decimal 4 digit
        {
            var Money = document.getElementById(txtMoney.id);
            var key;
            if (document.all) {
                key = window.event.keyCode;
            }
            else {
                key = e.which;
            }

            if (key != 46) {
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

        function removespacial(txt, e) {
            var key;
            if (document.all) {
                key = window.event.keyCode;
            }
            else {
                key = e.which;
            }

            if (key == 47 ||
                key == 91 ||
                key == 38 ||
                key == 92 ||
                key == 35 ||
                key == 44 ||
                key == 43 ||
                key == 40 ||
                key == 41 ||
                key == 36 ||
                key == 126 ||
                key == 37 ||
                key == 46 ||
                key == 39 ||
                key == 34 ||
                key == 58 ||
                key == 42 ||
                key == 63 ||
                key == 60 ||
                key == 62 ||
                key == 123 ||
                key == 125 ||
                key == 93 ||
                key == 33 ||
                key == 64 ||
                key == 94) {
                return false;
            } else {
                return true;
            }
        }

        function FocusSet(txt) {
            var txt = document.getElementById(txt);
            txt.focus();
        }

        function checkGridNull(){
            var chk = document.getElementById('<%= rdbFlagUtility.ClientID%>');
            var grd = document.getElementById('<%= grdView2.ClientID%>');
            var hddDeleteBeforeCheck = document.getElementById('<%= hddDeleteBeforeCheck.ClientID%>');
            if (grd != null){
                var count = grd.rows.length;
                if (count > 2) {
                    chk.checked = false;
                    OpenDialogError(hddDeleteBeforeCheck.value);
                }
            }
        }
    </script>
</asp:Content>

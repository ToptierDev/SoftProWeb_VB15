<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ORG_Project.aspx.vb" Inherits="SPW.Web.UI.ORG_Project" EnableEventValidation="false" %>

<%@ Register Src="Usercontrol/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc2" %>
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

        .fcright {
            text-align: right;
        }

        .setpadding .panel-body {
            padding: 0px;
            padding-top: 15px;
        }

        select.chosen-select.disabled-aslabel[disabled] + div.chosen-container {
            border: 0;
        }

        select.chosen-select.disabled-aslabel[disabled] + div.chosen-container span {
            color: black;
            margin-left: -10px;
            cursor: default;
            font-weight: bold;
        }

        select.chosen-select.disabled-aslabel[disabled].disabled-aslargelabel + div.chosen-container span {
            font-size: 20px;
        }

        select.chosen-select.disabled-aslabel[disabled] + div.chosen-container div {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:DatePicker runat="server" />
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <asp:HiddenField ID="hddParameterMenuID" runat="server" />
                <asp:HiddenField ID="hddReloadGrid" runat="server" />
                <asp:HiddenField ID="hddpSortBy" runat="server" />
                <asp:HiddenField ID="hddpSortType" runat="server" />
                <asp:HiddenField ID="hddpPagingDefault" runat="server" />
                <asp:HiddenField ID="hddKeyID" runat="server" />
                <asp:HiddenField ID="hddpPageInfo" runat="server" />
                <asp:HiddenField ID="hddpSearch" runat="server" />
                <asp:HiddenField ID="hddpClientID" runat="server" />
                <asp:HiddenField ID="hddTypeDoc" runat="server" />
                <asp:HiddenField ID="hddColumnDoc" runat="server" />
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="hide"></asp:FileUpload>
                <asp:HiddenField ID="hddpAutoCode" runat="server" />
                <asp:HiddenField ID="hddpAutoName" runat="server" />
                <asp:HiddenField ID="hddFlanddoc" runat="server" />
                <asp:HiddenField ID="hddFREQDOC" runat="server" />
                <asp:HiddenField ID="hddFCONSTRDOC" runat="server" />
                <asp:HiddenField ID="hddFCONSTRDOC1" runat="server" />
                <asp:HiddenField ID="hddFREQDOC1" runat="server" />
                <asp:HiddenField ID="hddFREQDOC2" runat="server" />
                <asp:HiddenField ID="hddFREQDOC3" runat="server" />
                <asp:HiddenField ID="hddFREQDOC4" runat="server" />
                <asp:HiddenField ID="hddFREQDOC5" runat="server" />
                <asp:HiddenField ID="hddFREQDOC6" runat="server" />
                <asp:HiddenField ID="hddFREQDOC7" runat="server" />
                <asp:HiddenField ID="hddFREQDOC8" runat="server" />
                <asp:HiddenField ID="hddFREQDOC9" runat="server" />
                <asp:HiddenField ID="hddFREQDOC10" runat="server" />
                <asp:HiddenField ID="hddMSGAddFile" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteFile" runat="server" />
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
                <asp:HiddenField ID="hddpTypeBrownser" runat="server" />
                <asp:HiddenField ID="hddMasterLG" runat="server" />
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">
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
                                                        <div class="col-lg-11 col-md-11 col-sm-11">
                                                            <h3>
                                                                <asp:Label ID="lblMain3" runat="server"></asp:Label>
                                                            </h3>
                                                        </div>
                                                        <div class="col-lg-1 col-md-1 col-sm-1" style="text-align: right;">
                                                            <a id="btnMSGAddData" runat="server" href="javascript:CallAddData();" class="btn btn-info glyph-icon icon-plus tooltip-button" visible='<%#Me.GetPermission().isAdd %>'></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row" style="overflow-x: auto; overflow-y: hidden;">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <%
                                                                Dim lc As New List(Of vw_Project_join)
                                                                If hddReloadGrid.Value <> String.Empty Then
                                                                    If GetDataSetProject() IsNot Nothing Then
                                                                        lc = GetDataSetProject()
                                                            %>
                                                            <table id="grdView" class="table table-striped table-bordered table-hover" style="width: 100%">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd4" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd5" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd1" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd2" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd3" runat="server"></asp:Label></th>
                                                                    </tr>
                                                                </thead>
                                                                <tfoot style="display: none;">
                                                                    <tr>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt4" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt5" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt1" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt2" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt3" runat="server"></asp:Label></th>
                                                                    </tr>
                                                                </tfoot>
                                                                <tbody>
                                                                    <%
                                                                        Dim i As Integer = 0
                                                                        For Each sublc As vw_Project_join In lc
                                                                            Dim strb As New StringBuilder()
                                                                    %>
                                                                    <tr>
                                                                        <%
                                                                            strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-edit'  href='javascript:CallEditData(&#39;" + IIf(sublc.FREPRJNO <> String.Empty, sublc.FREPRJNO, String.Empty) + "&#39;);'></a></td>")
                                                                            If sublc.e_FREPRJNO = String.Empty Then
                                                                                strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-danger glyph-icon icon-trash' href='javascript:ConfirmDelete(&#39;" + IIf(sublc.FREPRJNO <> String.Empty, sublc.FREPRJNO, String.Empty) + "&#39;);'></a></td>")
                                                                            Else
                                                                                strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                            End If
                                                                            strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                            strb.Append("<td style='width:20%;'>" + IIf(sublc.FREPRJNO <> String.Empty, sublc.FREPRJNO, String.Empty) + "</td>")
                                                                            strb.Append("<td style='width:80%;'>" + IIf(sublc.FREPRJNM <> String.Empty, sublc.FREPRJNM, String.Empty) + "</td>")

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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnDialog" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                            <div class="container-fluid">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12" style="margin-left: 2px;">
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
                                                                                <%If (hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit) Then %>
                                                                                <a id="btnMSGSaveData" runat="server" href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button"></a>
                                                                                <%End if %>
                                                                                <a id="btnMSGCancelData" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-lg-5 col-md-12 col-sm-12">
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblFreprjno" runat="server" Text=""></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-5 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtFreprjno" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="6" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipFreprjno") %>'></asp:TextBox>
                                                                                    <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                    <asp:CheckBox ID="chbFboiyn" runat="server" class="checkbox-inline" />
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblFRePrjNm" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtFRePrjNm" autocomplete="off" runat="server" CssClass="form-control" MaxLength="120" onblur="return CheckProductCode();"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblFrelocat1" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtFrelocat1" autocomplete="off" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtFrelocat2" autocomplete="off" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblFrelocat3" runat="server" Text=""></asp:Label>
                                                                                    <%--<table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 40%;">
                                                                                                <asp:Label ID="lblFrelocat3" runat="server" Text=""></asp:Label></td>
                                                                                            <td style="width: 60%; text-align: right;">
                                                                                                <asp:TextBox ID="txtFprovcdFcitycd" autocomplete="off" runat="server" cssclass="form-control" MaxLength="4"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>--%>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:TextBox ID="txtFrelocat3" autocomplete="off" runat="server" BackColor="#ffe0c0" CssClass="form-control" MaxLength="100" onkeypress="return AutocompletedPostal(this,event);" onClick="AutocompletedPostal(this,event);"></asp:TextBox>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div class="row">
                                                                                        <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                            <asp:Label ID="lblFreprovinc" runat="server" Text=""></asp:Label>
                                                                                        </div>
                                                                                        <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                            <table style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td style="width: 66%;">
                                                                                                        <asp:TextBox ID="txtFreprovinc" autocomplete="off" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td style="width: 2%;"></td>
                                                                                                    <td style="width: 32%;">
                                                                                                        <asp:TextBox ID="txtFrepostal" autocomplete="off" runat="server" CssClass="form-control" MaxLength="5" onKeyPress="return Check_Key(event);"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <br />
                                                                            <%--<div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblFlandno" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtFlandno" runat="server" autocomplete="off" class="form-control fcright"  onKeyPress="return Check_Key(event);" onBlur="return Check_Format_Number(this);"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblFlandMudNo" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtFlandMudNo" runat="server" autocomplete="off" class="form-control fcright"  onKeyPress="return Check_Key(event);" onBlur="return Check_Format_Number(this);"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />--%>
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblCodeMat" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:TextBox ID="txtCodeMat" autocomplete="off" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblFlanddoc" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <a href="javascript:CallAddDoc('FLAND','FLANDDOC');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value)  %>"></a>
                                                                                                        </td>
                                                                                                        <td runat="server" id="tdFlandoc">
                                                                                                            <a href="javascript:CallDeleteDoc('FLAND','FLANDDOC');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value)  %>"></a>
                                                                                                        </td>
                                                                                                        <td runat="server" id="tdFlandocUpload">
                                                                                                            <a id="hrefFlandoc" runat="server" href="javascript:CallHerfDoc('FLAND','FLANDDOC');" data-placement="right"
                                                                                                                class="btn btn-info glyph-icon icon-file tooltip-button"></a>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblFredesc1" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtFredesc1" runat="server" autocomplete="off" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtFredesc2" runat="server" autocomplete="off" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtFredesc3" runat="server" autocomplete="off" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblFtotarea" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 32%;">
                                                                                                <asp:TextBox ID="txtFtotarea" autocomplete="off" runat="server" CssClass="form-control fcright"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 35%; text-align: center;">
                                                                                                <asp:Label ID="lblFnoofland" runat="server" Text=""></asp:Label>
                                                                                            </td>
                                                                                            <td style="width: 1%; text-align: right;"></td>
                                                                                            <td style="width: 32%;">
                                                                                                <asp:TextBox ID="txtFnoofland" autocomplete="off" runat="server" CssClass="form-control fcright"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaFLADNO" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 100%;">
                                                                                                <asp:TextBox ID="txtaFLADNO" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaFPCLANDNO" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 100%;">
                                                                                                <asp:TextBox ID="txtaFPCLANDNO" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                            <br />

                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                    <div class="panel panel-primary">
                                                                                        <div class="panel-heading">
                                                                                            <h3>
                                                                                                <asp:Label ID="lblSubChanod" runat="server" Text=""></asp:Label>
                                                                                            </h3>
                                                                                        </div>
                                                                                        <div class="panel-body">
                                                                                            <div class="row">
                                                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:GridView ID="grdView2" runat="server" CssClass="table table-hover table-striped table-bordered table-responsive"
                                                                                                                EmptyDataText="No Items." EmptyDataRowStyle-CssClass="gridview-nodata" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" Style="width: 100%">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <HeaderTemplate>
                                                                                                                            <%# GetResource("lblgFASSETNO", "Text", hddParameterMenuID.Value)%>
                                                                                                                        </HeaderTemplate>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="txtgFASSETNO" autocomplete="off" runat="server" Style="height: 28px; vertical-align: top; padding: 0.5px 0.5px; background-color: transparent;" MaxLength="10"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle />
                                                                                                                        <ItemStyle HorizontalAlign="left" Width="24%" Height="15px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <HeaderTemplate>
                                                                                                                            <%# GetResource("lblChanodNo", "Text", hddParameterMenuID.Value)%>
                                                                                                                        </HeaderTemplate>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="txtgChanodNo" autocomplete="off" runat="server" Style="height: 28px; vertical-align: top; padding: 0.5px 0.5px; background-color: transparent;" MaxLength="255"></asp:Label>
                                                                                                                            <asp:HiddenField ID="hddID" runat="server" />
                                                                                                                            <asp:HiddenField ID="hddFlagSetAdd" runat="server" />
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle />
                                                                                                                        <ItemStyle HorizontalAlign="left" Width="24%" Height="15px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <HeaderTemplate>
                                                                                                                            <%# GetResource("lblgArea", "Text", hddParameterMenuID.Value)%>
                                                                                                                        </HeaderTemplate>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="txtgArea" autocomplete="off" runat="server" Style="height: 28px; vertical-align: top; padding: 0.5px 0.5px; background-color: transparent;" onKeyPress="return Check_Key_Decimal(this,event)" onBlur="return Check_Format2Digit(this);"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle />
                                                                                                                        <ItemStyle HorizontalAlign="right" Width="24%" Height="15px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <HeaderTemplate>
                                                                                                                            <%# GetResource("lblChanodMud", "Text", hddParameterMenuID.Value)%>
                                                                                                                        </HeaderTemplate>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="txtgChanodMud" autocomplete="off" runat="server" Style="height: 28px; vertical-align: top; padding: 0.5px 0.5px; background-color: transparent;" MaxLength="255"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle />
                                                                                                                        <ItemStyle HorizontalAlign="right" Width="24%" Height="15px" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <HeaderTemplate>
                                                                                                                            <%# GetResource("col_edit", "Text", "1")%>
                                                                                                                        </HeaderTemplate>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="btnEdit" ImageUrl="~/image/edit.png" Style="width: 15px; height: 15px; margin-left: 9px; margin-top: 7px;" />
                                                                                                                        </ItemTemplate>
                                                                                                                        <HeaderStyle />
                                                                                                                        <ItemStyle HorizontalAlign="Center" Width="4%" VerticalAlign="Middle" />
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
                                                                        </div>
                                                                        <div class="col-lg-7 col-md-12 col-sm-12">
                                                                            <div class="panel panel-primary">
                                                                                <div class="setpadding">
                                                                                    <div class="panel-body">
                                                                                        <div class="example-box-wrapper">
                                                                                            <ul class="nav-responsive nav nav-tabs">
                                                                                                <li id="liTab1" runat="server" class="active"><a href="#tabProj1" data-toggle="tab">
                                                                                                    <%=grtt("lblLicenses") %></a></li>
                                                                                                <li id="liTab2" runat="server"><a href="#tabProj2" data-toggle="tab">
                                                                                                    <%=grtt("resProjectStatus") %></a></li>
                                                                                            </ul>
                                                                                            <div class="tab-content">
                                                                                                <div class="tab-pane active" id="tabProj1">
                                                                                                    <div class="panel-body">
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:Label ID="lblNo" runat="server" Text="Label"></asp:Label></td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:Label ID="lblEDate" runat="server" Text=""></asp:Label></td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <asp:Label ID="lblDocument" runat="server" Text=""></asp:Label></td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblLandAllocation" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt2" autocomplete="off" runat="server" class="form-control" MaxLength="9" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate0" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate13" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOCAdd">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOCUpload">
                                                                                                                                                    <a id="hrefFREQDOC" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC');"
                                                                                                                                                        class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblConstructionPorject" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt11" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate1" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate14" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFCONSTRDOCAdd">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FCONSTRDOC');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFCONSTRDOC">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FCONSTRDOC');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFCONSTRDOCUpload">
                                                                                                                                                    <a id="hrefFCONSTRDOC" runat="server" href="javascript:CallHerfDoc('FREQ','FCONSTRDOC');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblBuildingHomes" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt12" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate2" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate15" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFCONSTRDOC1Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FCONSTRDOC1');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFCONSTRDOC1">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FCONSTRDOC1');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFCONSTRDOC1Upload">
                                                                                                                                                    <a id="hrefFCONSTRDOC1" runat="server" href="javascript:CallHerfDoc('FREQ','FCONSTRDOC1');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblCityPlan" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt13" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate3" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate16" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOC1Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC1');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC1">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC1');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC1Upload">
                                                                                                                                                    <a id="hrefFREQDOC1" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC1');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblCommercialLand" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt14" autocomplete="off" runat="server" class="form-control" MaxLength="19" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate4" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate17" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOC2Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC2');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC2">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC2');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC2Upload">
                                                                                                                                                    <a id="hrefFREQDOC2" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC2');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblGarbage" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt15" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate5" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate18" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOC3Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC3');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC3">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC3');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC3Upload">
                                                                                                                                                    <a id="hrefFREQDOC3" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC3');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblContacts" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt16" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate6" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate19" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOC4Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC4');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC4">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC4');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC4Upload">
                                                                                                                                                    <a id="hrefFREQDOC4" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC4');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblDrainage" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt17" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate7" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate20" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOC5Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC5');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC5">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC5');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC5Upload">
                                                                                                                                                    <a id="hrefFREQDOC5" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC5');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblExpandElectricity" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt18" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate8" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate21" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOC6Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC6');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC6">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC6');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC6Upload">
                                                                                                                                                    <a id="hrefFREQDOC6" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC6');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblPlumbingElectricity" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt19" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate9" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate22" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOC7Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC7');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC7">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC7');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC7Upload">
                                                                                                                                                    <a id="hrefFREQDOC7" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC7');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblScienceHill" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt20" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate10" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate23" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOC8Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC8');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC8">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC8');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGDeleteFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC8Upload">
                                                                                                                                                    <a id="hrefFREQDOC8" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC8');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblBillboards" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt21" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate11" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate24" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOC9Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC9');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC9">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC9');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC9Upload">
                                                                                                                                                    <a id="hrefFREQDOC9" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC9');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                                <asp:Label ID="lblFilling" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                                                <table style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 30%;">
                                                                                                                            <asp:TextBox ID="txt22" autocomplete="off" runat="server" class="form-control" MaxLength="15" onblur="return checkNullShowAdd();"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate12" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%;">
                                                                                                                            <asp:TextBox ID="txtDate25" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" Style="width: 99%;"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;">
                                                                                                                            <table style="width: 100%">
                                                                                                                                <tr>
                                                                                                                                    <td>
                                                                                                                                        <table>
                                                                                                                                            <tr>
                                                                                                                                                <td runat="server" id="tdFREQDOC10Add">
                                                                                                                                                    <a href="javascript:CallAddDoc('FREQ','FREQDOC10');" class="glyph-icon icon-plus btn btn-success tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC10">
                                                                                                                                                    <a href="javascript:CallDeleteDoc('FREQ','FREQDOC10');" class="btn btn-danger glyph-icon icon-minus tooltip-button disabledbtn" title="<% Response.Write(hddMSGAddFile.Value) %>"></a>
                                                                                                                                                </td>
                                                                                                                                                <td runat="server" id="tdFREQDOC10Upload">
                                                                                                                                                    <a id="hrefFREQDOC10" runat="server" href="javascript:CallHerfDoc('FREQ','FREQDOC10');" class="btn btn-info glyph-icon icon-file tooltip-button" title="Upload"></a>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="tab-pane " id="tabProj2">
                                                                                                    <div class="panel-body">
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <%=grtt("resProjectType") %>
                                                                                                            </div>
                                                                                                            <div class="col-lg-9 col-md-12 col-sm-12">
                                                                                                                <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="chosen-select disabled-aslabel"></asp:DropDownList>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <br />
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <%=grtt("resProjectBrand") %>
                                                                                                            </div>
                                                                                                            <div class="col-lg-9 col-md-12 col-sm-12">
                                                                                                                <asp:TextBox ID="txtProjectBrand" autocomplete="off" runat="server" CssClass="form-control" MaxLength="80"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <br />
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <%=grtt("resProjectStartDate") %>
                                                                                                            </div>
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <asp:TextBox ID="txtProjectStartDate" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <%=grtt("resProjectEndDate") %>
                                                                                                            </div>
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <asp:TextBox ID="txtProjectEndDate" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <br />
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <%=grtt("resConstructionStatus") %>
                                                                                                            </div>
                                                                                                            <div class="col-lg-9 col-md-12 col-sm-12">
                                                                                                                <asp:DropDownList ID="ddlConstructionStatus" runat="server" CssClass="chosen-select disabled-aslabel"></asp:DropDownList>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <br />
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <%=grtt("resConstructionStartDate") %>
                                                                                                            </div>
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <asp:TextBox ID="txtConstructionStartDate" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <%=grtt("resConstructionEndDate") %>
                                                                                                            </div>
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <asp:TextBox ID="txtConstructionEndDate" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class=" form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <br />
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <%=grtt("resSaleStatus") %>
                                                                                                            </div>
                                                                                                            <div class="col-lg-9 col-md-12 col-sm-12">
                                                                                                                <asp:DropDownList ID="ddlSaleStatus" runat="server" CssClass="chosen-select disabled-aslabel"></asp:DropDownList>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <br />
                                                                                                        <div class="row">
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <%=grtt("resSaleStartDate") %>
                                                                                                            </div>
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <asp:TextBox ID="txtSaleStartDate" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                <asp:Label ID="lblMassage4" runat="server" ForeColor="Red"></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <%=grtt("resSaleEndDate") %>
                                                                                                            </div>
                                                                                                            <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                                                <asp:TextBox ID="txtSaleEndDate" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                                                <asp:Label ID="lblMassage5" runat="server" ForeColor="Red"></asp:Label>
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
                                                                        <%--<div class="panel-footer">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div class="content-box remove-border dashboard-buttons clearfix">
                                                                                            <a href="javascript:CheckData();" class="btn vertical-button remove-border btn-info" title="Save Data">
                                                                                                <span class="glyph-icon icon-separator-vertical">
                                                                                                    <i class="glyph-icon icon-save"></i>
                                                                                                </span>
                                                                                                <span class="button-content">
                                                                                                    <asp:Label ID="lblsSave" runat="server"></asp:Label>
                                                                                                </span>
                                                                                            </a>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="content-box remove-border dashboard-buttons clearfix">
                                                                                            <a href="javascript:CallCancel();" class="btn vertical-button remove-border btn-danger" title="Cancel Data">
                                                                                                <span class="glyph-icon icon-separator-vertical">
                                                                                                    <i class="glyph-icon icon-close"></i>
                                                                                                </span>
                                                                                                <span class="button-content">
                                                                                                    <asp:Label ID="lblsCancel" runat="server"></asp:Label>
                                                                                                </span>
                                                                                            </a>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            
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
                            </div>
                            <div class="modal-footer">
                                <button id="btnMSGDeleteData" runat="server" type="button" class="btn btn-danger" onclick="javascript:CallDeleteData();"></button>
                                <button id="btnMSGCancelDataS" runat="server" type="button" class="btn btn-default" data-dismiss="modal"></button>
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
                <asp:Button ID="btnFileUpload" runat="server" CssClass="hide" />
                <asp:Button ID="btnDeleteFileUpload" runat="server" CssClass="hide" />
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnReload" runat="server" CssClass="hide" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnFileUpload" />
            <asp:PostBackTrigger ControlID="btnDeleteFileUpload" />
        </Triggers>
    </asp:UpdatePanel>
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
            var isEdit = <%#Me.GetPermission().isEdit.ToString.ToLower %>;
            var isAdd = <%#Me.GetPermission().isAdd.ToString.ToLower %>;
            var hddKeyID = document.getElementById('<%=hddKeyID.ClientID%>')
            if (isEdit == false && hddKeyID.value != ""){
                $("input").addClass('disabled');
                $("select").attr('disabled', 'disabled');
                $(".disabledbtn").addClass('hide');
            }else if(isAdd == true && hddKeyID.value == ""){
                $("input").removeClass('disabled');
                $("select").removeAttr('disabled');
                $(".disabledbtn").removeClass('hide');
            }
            var currentdate = new Date();
            currentdate.setDate(currentdate.getDate());
            var hddMasterLG = $('[id$=hddMasterLG]');
            if (hddMasterLG.val() == "TH") {
                $('#<%=txtConstructionStartDate.ClientID%>').datepicker({
                    language: 'th-th',
                    isRTL: false,
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                }).on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('#<%=txtConstructionEndDate.ClientID%>').datepicker('setStartDate', minDate);
                });

                $('#<%=txtConstructionEndDate.ClientID%>').datepicker({
                    language: 'th-th',
                    isRTL: false,
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    startDate: currentdate
                }); 

                $('#<%=txtSaleStartDate.ClientID%>').datepicker({
                    language: 'th-th',
                    isRTL: false,
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                }).on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('#<%=txtSaleEndDate.ClientID%>').datepicker('setStartDate', minDate);
                });

                $('#<%=txtSaleEndDate.ClientID%>').datepicker({
                    language: 'th-th',
                    isRTL: false,
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    startDate: currentdate
                }); 
                
            } else {
                $('#<%=txtConstructionStartDate.ClientID%>').datepicker({
                    language: 'en',
                    isRTL: false,
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    startDate: currentdate,
                }).on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('#<%=txtConstructionEndDate.ClientID%>').datepicker('setStartDate', minDate);
                });

                $('#<%=txtConstructionEndDate.ClientID%>').datepicker({
                    language: 'en',
                    isRTL: false,
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    startDate: currentdate
                }); 

                $('#<%=txtSaleStartDate.ClientID%>').datepicker({
                    language: 'en',
                    isRTL: false,
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    startDate: currentdate,
                }).on('changeDate', function (selected) {
                    var minDate = new Date(selected.date.valueOf());
                    $('#<%=txtSaleEndDate.ClientID%>').datepicker('setStartDate', minDate);
                });

                $('#<%=txtSaleEndDate.ClientID%>').datepicker({
                    language: 'en',
                    isRTL: false,
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    startDate: currentdate
                }); 


            }


            var hddpPageInfo = document.getElementById("<%= hddpPageInfo.ClientID%>");
            var hddpSearch = document.getElementById("<%= hddpSearch.ClientID%>");
            var hddpPagingDefault = document.getElementById("<%= hddpPagingDefault.ClientID%>");
            var isEdit = <%#Me.GetPermission().isEdit.ToString.ToLower %>;
            var isDelete = <%#Me.GetPermission().isDelete.ToString.ToLower %>;
            if ($('#grdView') != null) {
                if (!$.fn.DataTable.isDataTable('#grdView')) {
                    var t = $('#grdView').DataTable({
                        "order": [[$('#<%=hddpSortBy.ClientID%>').val(), $('#<%=hddpSortType.ClientID%>').val()]],
                        "pageLength": parseInt($('#<%=hddpPagingDefault.ClientID%>').val()),
                        "columnDefs": [{
                            "searchable": false,
                            "orderable": false,
                            "targets": [0, 1, 2]
                        },
                        { "targets": [1], "visible": isDelete }]
                    });

                    t.on('order.dt search.dt', function () {
                        t.column(2, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
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
            }
        }

        //Auto Completed
        function AutocompletedCodeMat(txt, e) {
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
            $("#<%=txtCodeMat.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("./API/Api_Project.asmx/GetCodeMat")%>',
                        data: "{ 'ptKeyID': '" + request.term + "' }",
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
                    $("#<%=txtCodeMat.ClientID%>").val(i.item.label);
                },
                minLength: 0,
                matchContains: true
            }).on('click', function () { $(this).keydown(); });

            }

            function AutocompletedPostal(txtFrelocat3, e) {
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
                $("#<%=txtFrelocat3.ClientID%>").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("./API/Api_Project.asmx/GetAuto")%>',
                            data: "{ 'ptPostal': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.split('-')[0],
                                        val: item.split('-')[1]
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
                        $("#<%=hddpAutoName.ClientID%>").val(i.item.val);

                        PostbackSelectPostal();
                    },
                    minLength: 0,
                    matchContains: true
                }).on('click', function () { $(this).keydown(); });

                }

                function showOverlay() {
                    $("#overlay").modal();
                }

                function closeOverlay() {
                    $("#overlay").modal('hide');
                }

                function PostbackSelectPostal() {
                    __doPostBack('<%= btnReload.UniqueID%>');
                }

                function CallLoadHrefNewtab(Urls) {
                    window.open(Urls, '_blank');
                }

                function CallAddDoc(Type, Column) {
                    var hddTypeDoc = document.getElementById("<%= hddTypeDoc.ClientID%>");
                    hddTypeDoc.value = Type
                    var hddColumnDoc = document.getElementById("<%= hddColumnDoc.ClientID%>");
                    hddColumnDoc.value = Column
                    var hddpTypeBrownser = document.getElementById("<%= hddpTypeBrownser.ClientID%>");
                    var detectBrownser = hddpTypeBrownser.value
                    if (detectBrownser == "in") {
                        showOverlay();
                        $("#<%=FileUpload1.ClientID%>").trigger('click');
                        __doPostBack('<%= btnFileUpload.UniqueID%>');
                    } else if (detectBrownser == "ch") {
                        $("#<%=FileUpload1.ClientID%>").trigger('click');
                        $("#<%=FileUpload1.ClientID%>").change(function () {
                            showOverlay();
                            __doPostBack('<%= btnFileUpload.UniqueID%>');
                        });
                    } else {
                        showOverlay();
                        $("#<%=FileUpload1.ClientID%>").trigger('click');
                        __doPostBack('<%= btnFileUpload.UniqueID%>');
                    }
            }

            function CallDeleteDoc(Type, Column) {
                var hddTypeDoc = document.getElementById("<%= hddTypeDoc.ClientID%>");
                hddTypeDoc.value = Type
                var hddColumnDoc = document.getElementById("<%= hddColumnDoc.ClientID%>");
                hddColumnDoc.value = Column
                showOverlay();
                __doPostBack('<%= btnDeleteFileUpload.UniqueID%>');
            }

            function CallHerfDoc(Type, Column) {
                var hddFlanddoc = document.getElementById("<%= hddFlanddoc.ClientID%>");
                var hddFREQDOC = document.getElementById("<%= hddFREQDOC.ClientID%>");
                var hddFCONSTRDOC = document.getElementById("<%= hddFCONSTRDOC.ClientID%>");
                var hddFCONSTRDOC1 = document.getElementById("<%= hddFCONSTRDOC1.ClientID%>");
                var hddFREQDOC1 = document.getElementById("<%= hddFREQDOC1.ClientID%>");
                var hddFREQDOC2 = document.getElementById("<%= hddFREQDOC2.ClientID%>");
                var hddFREQDOC3 = document.getElementById("<%= hddFREQDOC3.ClientID%>");
                var hddFREQDOC4 = document.getElementById("<%= hddFREQDOC4.ClientID%>");
                var hddFREQDOC5 = document.getElementById("<%= hddFREQDOC5.ClientID%>");
                var hddFREQDOC6 = document.getElementById("<%= hddFREQDOC6.ClientID%>");
                var hddFREQDOC7 = document.getElementById("<%= hddFREQDOC7.ClientID%>");
                var hddFREQDOC8 = document.getElementById("<%= hddFREQDOC8.ClientID%>");
                var hddFREQDOC9 = document.getElementById("<%= hddFREQDOC9.ClientID%>");
                var hddFREQDOC10 = document.getElementById("<%= hddFREQDOC10.ClientID%>");
                if (Column == "FLANDDOC") {
                    CallLoadHrefNewtab(hddFlanddoc.value);
                } else if (Column == "FREQDOC") {
                    CallLoadHrefNewtab(hddFREQDOC.value);
                } else if (Column == "FCONSTRDOC") {
                    CallLoadHrefNewtab(hddFCONSTRDOC.value);
                } else if (Column == "FCONSTRDOC1") {
                    CallLoadHrefNewtab(hddFCONSTRDOC1.value);
                } else if (Column == "FREQDOC1") {
                    CallLoadHrefNewtab(hddFREQDOC1.value);
                } else if (Column == "FREQDOC2") {
                    CallLoadHrefNewtab(hddFREQDOC2.value);
                } else if (Column == "FREQDOC3") {
                    CallLoadHrefNewtab(hddFREQDOC3.value);
                } else if (Column == "FREQDOC4") {
                    CallLoadHrefNewtab(hddFREQDOC4.value);
                } else if (Column == "FREQDOC5") {
                    CallLoadHrefNewtab(hddFREQDOC5.value);
                } else if (Column == "FREQDOC6") {
                    CallLoadHrefNewtab(hddFREQDOC6.value);
                } else if (Column == "FREQDOC7") {
                    CallLoadHrefNewtab(hddFREQDOC7.value);
                } else if (Column == "FREQDOC8") {
                    CallLoadHrefNewtab(hddFREQDOC8.value);
                } else if (Column == "FREQDOC9") {
                    CallLoadHrefNewtab(hddFREQDOC9.value);
                } else if (Column == "FREQDOC10") {
                    CallLoadHrefNewtab(hddFREQDOC10.value);
                }
            }
            function addRowGridView(txtClientID) {
                var hddpClientID = document.getElementById("<%= hddpClientID.ClientID%>");
                hddpClientID.value = txtClientID
                __doPostBack('<%= btnGridAdd.UniqueID%>');
            }

            function FocusSet(txt) {
                var txt = document.getElementById(txt);
                txt.focus();
            }

            function CallCancel() {
                showOverlay();
                __doPostBack('<%= btnCancel.UniqueID%>');
            }

            function CallLoaddata() {
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

            function ConfirmDelete(pKey) {
                var hddKeyID = document.getElementById("<%= hddKeyID.ClientID%>");
                var lblBodydelete = document.getElementById("<%= lblBodydelete.ClientID%>");

                hddKeyID.value = pKey;
                lblBodydelete.value = lblBodydelete.defaultValue + " " + pKey + " ?";

                $("#DeleteModal").modal();
            }

            function checkNullShowAdd() {
                var txt2 = document.getElementById("<%= txt2.ClientID%>");
                var txt11 = document.getElementById("<%= txt11.ClientID%>");
                var txt12 = document.getElementById("<%= txt12.ClientID%>");
                var txt13 = document.getElementById("<%= txt13.ClientID%>");
                var txt14 = document.getElementById("<%= txt14.ClientID%>");
                var txt15 = document.getElementById("<%= txt15.ClientID%>");
                var txt16 = document.getElementById("<%= txt16.ClientID%>");
                var txt17 = document.getElementById("<%= txt17.ClientID%>");
                var txt18 = document.getElementById("<%= txt18.ClientID%>");
                var txt19 = document.getElementById("<%= txt19.ClientID%>");
                var txt20 = document.getElementById("<%= txt20.ClientID%>");
                var txt21 = document.getElementById("<%= txt21.ClientID%>");
                var txt22 = document.getElementById("<%= txt22.ClientID%>");
                var tdFREQDOCAdd = document.getElementById("<%= tdFREQDOCAdd.ClientID%>");
                var tdFCONSTRDOCAdd = document.getElementById("<%= tdFCONSTRDOCAdd.ClientID%>");
                var tdFCONSTRDOC1Add = document.getElementById("<%= tdFCONSTRDOC1Add.ClientID%>");
                var tdFREQDOC1Add = document.getElementById("<%= tdFREQDOC1Add.ClientID%>");
                var tdFREQDOC2Add = document.getElementById("<%= tdFREQDOC2Add.ClientID%>");
                var tdFREQDOC3Add = document.getElementById("<%= tdFREQDOC3Add.ClientID%>");
                var tdFREQDOC4Add = document.getElementById("<%= tdFREQDOC4Add.ClientID%>");
                var tdFREQDOC5Add = document.getElementById("<%= tdFREQDOC5Add.ClientID%>");
                var tdFREQDOC6Add = document.getElementById("<%= tdFREQDOC6Add.ClientID%>");
                var tdFREQDOC7Add = document.getElementById("<%= tdFREQDOC7Add.ClientID%>");
                var tdFREQDOC8Add = document.getElementById("<%= tdFREQDOC8Add.ClientID%>");
                var tdFREQDOC9Add = document.getElementById("<%= tdFREQDOC9Add.ClientID%>");
                var tdFREQDOC10Add = document.getElementById("<%= tdFREQDOC10Add.ClientID%>");
                if (txt2.value != "") {
                    tdFREQDOCAdd.style.display = "";
                } else {
                    tdFREQDOCAdd.style.display = "none";
                }
                if (txt11.value != "") {
                    tdFCONSTRDOCAdd.style.display = "";
                } else {
                    tdFCONSTRDOCAdd.style.display = "none";
                }
                if (txt12.value != "") {
                    tdFCONSTRDOC1Add.style.display = "";
                } else {
                    tdFCONSTRDOC1Add.style.display = "none";
                }
                if (txt13.value != "") {
                    tdFREQDOC1Add.style.display = "";
                } else {
                    tdFREQDOC1Add.style.display = "none";
                }
                if (txt14.value != "") {
                    tdFREQDOC2Add.style.display = "";
                } else {
                    tdFREQDOC2Add.style.display = "none";
                }
                if (txt15.value != "") {
                    tdFREQDOC3Add.style.display = "";
                } else {
                    tdFREQDOC3Add.style.display = "none";
                }
                if (txt16.value != "") {
                    tdFREQDOC4Add.style.display = "";
                } else {
                    tdFREQDOC4Add.style.display = "none";
                }
                if (txt17.value != "") {
                    tdFREQDOC5Add.style.display = "";
                } else {
                    tdFREQDOC5Add.style.display = "none";
                }
                if (txt18.value != "") {
                    tdFREQDOC6Add.style.display = "";
                } else {
                    tdFREQDOC6Add.style.display = "none";
                }
                if (txt19.value != "") {
                    tdFREQDOC7Add.style.display = "";
                } else {
                    tdFREQDOC7Add.style.display = "none";
                }
                if (txt20.value != "") {
                    tdFREQDOC8Add.style.display = "";
                } else {
                    tdFREQDOC8Add.style.display = "none";
                }
                if (txt21.value != "") {
                    tdFREQDOC9Add.style.display = "";
                } else {
                    tdFREQDOC9Add.style.display = "none";
                }
                if (txt22.value != "") {
                    tdFREQDOC10Add.style.display = "";
                } else {
                    tdFREQDOC10Add.style.display = "none";
                }
            }

            function CallSaveData() {
                showOverlay();
                __doPostBack('<%= btnSave.UniqueID%>');
                return;
            }


        
            function CheckConstructionStatus() {
                var sConstructionStatus = $('#<%=ddlConstructionStatus.ClientID%>').val();
           
                if (sConstructionStatus == "1"){
                    if ($('#<%=txtConstructionStartDate.ClientID%>').val().replace(" ", "") == "") {
                        OpenDialogError('<%=grtt("resPleaseEnter") %>' + ' ' + '<%=grtt("resConstructionStartDate") %>');
                    }
                }
                else if (sConstructionStatus == "2"){
                    if ($('#<%=txtConstructionEndDate.ClientID%>').val().replace(" ", "") == "") {
                        OpenDialogError('<%=grtt("resPleaseEnter") %>' + ' ' + '<%=grtt("resConstructionEndDate") %>');
                    }
                }

        }

        function CheckSaleStatus() {

            var sSaleStatus = $('#<%=ddlSaleStatus.ClientID%>').val();

        if (sSaleStatus == "1"){
            if ($('#<%=txtSaleStartDate.ClientID%>').val().replace(" ", "") == "") {
                OpenDialogError('<%=grtt("resPleaseEnter") %>' + ' ' + '<%=grtt("resSaleStartDate") %>');
            }
        }else if (sSaleStatus == "2"){
            if ($('#<%=txtSaleEndDate.ClientID%>').val().replace(" ", "") == "") {
                OpenDialogError('<%=grtt("resPleaseEnter") %>' + ' ' + '<%=grtt("resSaleEndDate") %>');
            }
        }

}

function CheckData() {
    if ($('#<%=txtFreprjno.ClientID%>').val().replace(" ", "") == "") {
        $('#<%=txtFreprjno.ClientID%>').addClass("parsley-error")
        document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
        scrollAndFocus('#<%=txtFreprjno.ClientID%>');
        return;
    } else {
        $('#<%=txtFreprjno.ClientID%>').removeClass("parsley-error")
        document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
    }



    $('#<%=txtConstructionStartDate.ClientID%>').removeClass("parsley-error")
    document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
    $('#<%=txtConstructionEndDate.ClientID%>').removeClass("parsley-error")
        document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";  
        $('#<%=txtSaleStartDate.ClientID%>').removeClass("parsley-error")
        document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "none";
        $('#<%=txtSaleEndDate.ClientID%>').removeClass("parsley-error")
        document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "none";

        var sConstructionStatus = $('#<%=ddlConstructionStatus.ClientID%>').val();
            if (sConstructionStatus == "1"){
                if ($('#<%=txtConstructionStartDate.ClientID%>').val().replace(" ", "") == "") {
                    $('#<%=txtConstructionStartDate.ClientID%>').addClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                    scrollAndFocus('#<%=txtConstructionStartDate.ClientID%>');
                    OpenDialogError('<%=grtt("resPleaseEnter") %>' + ' ' + '<%=grtt("resConstructionStartDate") %>');
                    return;
                } else {

                    $('#<%=txtConstructionStartDate.ClientID%>').removeClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";

                }  
            }
            if (sConstructionStatus == "2"){
                if ($('#<%=txtConstructionEndDate.ClientID%>').val().replace(" ", "") == "") {
                    $('#<%=txtConstructionEndDate.ClientID%>').addClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
                    scrollAndFocus('#<%=txtConstructionEndDate.ClientID%>');
                    OpenDialogError('<%=grtt("resPleaseEnter") %>' + ' ' + '<%=grtt("resConstructionEndDate") %>');
                    return;
                } else {
                    $('#<%=txtConstructionEndDate.ClientID%>').removeClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";                       
                }

            }

            var xCSD =$('#<%=txtConstructionStartDate.ClientID%>').val().replace(" ", "");
    var xCED =$('#<%=txtConstructionEndDate.ClientID%>').val().replace(" ", "");
    if (xCSD != "" && xCED != ""){
        var aSD = xCSD.split('/');
        var dateSD = new Date (aSD[2],aSD[1],aSD[0]);
        var ddSD = dateSD.getDate();
        var mmSD = dateSD.getMonth(); //January is 0!
        var yyyySD = dateSD.getFullYear();
        if(ddSD<10){
            ddSD='0'+ddSD;
        } 
        if(mmSD<10){
            mmSD='0'+mmSD;
        } 
                
        if ($('#<%=hddMasterLG.ClientID%>').val() == "TH") {
                yyyySD = (parseInt(yyyySD)-543).toString();
            }
            var todaySD = yyyySD+mmSD+ddSD;
                
            var aED = xCED.split('/');
            var dateED = new Date (aED[2],aED[1],aED[0]);
            var ddED = dateED.getDate();
            var mmED = dateED.getMonth(); //January is 0!
            var yyyyED = dateED.getFullYear();
            if(ddED<10){
                ddED='0'+ddED;
            } 
            if(mmED<10){
                mmED='0'+mmED;
            } 
                
            if ($('#<%=hddMasterLG.ClientID%>').val() == "TH") {
                yyyyED = (parseInt(yyyyED)-543).toString();
            }
            var todayED = yyyyED+mmED+ddED;
            if (todayED < todaySD)
            {
                $('#<%=txtConstructionEndDate.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
                scrollAndFocus('#<%=txtConstructionEndDate.ClientID%>');
                OpenDialogError('<%=grtt("resConstructionEndDate") %>' + ' ' + '<%=grtt("resLeastDate") %>' + ' ' + '<%=grtt("resConstructionStartDate") %>');
                    return;
                }
            }



            var sSaleStatus = $('#<%=ddlSaleStatus.ClientID%>').val();
    if (sSaleStatus == "1"){
        if ($('#<%=txtSaleStartDate.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtSaleStartDate.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "";
            scrollAndFocus('#<%=txtSaleStartDate.ClientID%>');
                OpenDialogError('<%=grtt("resPleaseEnter") %>' + ' ' + '<%=grtt("resSaleStartDate") %>');
                return;
            } else {

                $('#<%=txtSaleStartDate.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "none";

            }

        }

        if (sSaleStatus == "2"){

            if ($('#<%=txtSaleEndDate.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtSaleEndDate.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "";
                scrollAndFocus('#<%=txtSaleEndDate.ClientID%>');
                OpenDialogError('<%=grtt("resPleaseEnter") %>' + ' ' + '<%=grtt("resSaleEndDate") %>');
                return;
            } else {
                $('#<%=txtSaleEndDate.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "none";
                        
            }

        }

        var xSSD =$('#<%=txtSaleStartDate.ClientID%>').val().replace(" ", "");
    var xSED =$('#<%=txtSaleEndDate.ClientID%>').val().replace(" ", "");
    if (xSSD != "" && xSED != ""){
        var aSD = xSSD.split('/');
        var dateSD = new Date (aSD[2],aSD[1],aSD[0]);
        var ddSD = dateSD.getDate();
        var mmSD = dateSD.getMonth(); //January is 0!
        var yyyySD = dateSD.getFullYear();
        if(ddSD<10){
            ddSD='0'+ddSD;
        } 
        if(mmSD<10){
            mmSD='0'+mmSD;
        } 
                
        if ($('#<%=hddMasterLG.ClientID%>').val() == "TH") {
                yyyySD = (parseInt(yyyySD)-543).toString();
            }
            var todaySD = yyyySD+mmSD+ddSD;
                
            var aED = xSED.split('/');
            var dateED = new Date (aED[2],aED[1],aED[0]);
            var ddED = dateED.getDate();
            var mmED = dateED.getMonth(); //January is 0!
            var yyyyED = dateED.getFullYear();
            if(ddED<10){
                ddED='0'+ddED;
            } 
            if(mmED<10){
                mmED='0'+mmED;
            } 
                
            if ($('#<%=hddMasterLG.ClientID%>').val() == "TH") {
                yyyyED = (parseInt(yyyyED)-543).toString();
            }
            var todayED = yyyyED+mmED+ddED;
            if (todayED < todaySD)
            {
                $('#<%=txtSaleEndDate.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "";
                scrollAndFocus('#<%=txtSaleEndDate.ClientID%>');
                OpenDialogError('<%=grtt("resSaleEndDate") %>' + ' ' + '<%=grtt("resLeastDate") %>' + ' ' + '<%=grtt("resSaleStartDate") %>');
                    return;
                }


            }

            CallSaveData();
        }

        function CheckProductCode() {
            var txtFRePrjNm = document.getElementById("<%= txtFRePrjNm.ClientID%>");
            var txtCodeMat = document.getElementById("<%= txtCodeMat.ClientID%>");
            var TempString;
            if (txtFRePrjNm.value != "" && txtCodeMat.value != "") {
                TempString = txtCodeMat.value.split(" - ");
                txtCodeMat.value = TempString[0] + " - " + txtFRePrjNm.value;
            }
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


        function Check_Key_Decimal(txtMoney, e)//check key number&dot only and decimal 2 digit
        {

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
                    return true;
                }
            } else {
                return true;
            }
        }

        function Check_Format_Number(txtMoney)//check key number
        {
            var Money = document.getElementById(txtMoney.id).value;

            if (Money != "") {
                document.getElementById(txtMoney.id).value = Money;
            }

            return true;
        }

        function Check_Format2Digit(txtMoney)//check key number&dot only and decimal 2 digit
        {
            var Money = document.getElementById(txtMoney.id).value;
            if (Money != "") {
                document.getElementById(txtMoney.id).value = numberWithCommas(parseFloat(numberWithCommasValue(Money)).toFixed(2));
            }
            return true;
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

        function numberWithCommasValue(valWithComma) {
            return valWithComma.toString().replace(/,/g, '');
        }

        function roundToTwo(num) {
            return +(Math.round(num + "e+2") + "e-2");
        }

        function getAdjustedDate(adjustment) {
            var today = new Date();
            today.setDate(today.getDate() + adjustment);

            var dd = today.getDate() - 1;
            var mm = today.getMonth() + 1; //January is 0!

            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            };

            if (mm < 10) {
                mm = '0' + mm
            };

            return mm + '/' + dd + '/' + yyyy;
        };

    </script>
</asp:Content>

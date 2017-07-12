<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="MST_Sales.aspx.vb" Inherits="SPW.Web.UI.MST_Sales" EnableEventValidation="false" %>

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

        @media only screen and (max-width: 1000px) {
            /*.hide-columns table td:nth-child(5),
            .hide-columns table th:nth-child(5),
            .hide-columns table td:nth-child(6),
            .hide-columns table th:nth-child(6) {
                display: none;
            }*/
        }

        @media only screen and (max-width: 800px) {
            /*.hide-columns table td:nth-child(5),
            .hide-columns table th:nth-child(5),
            .hide-columns table td:nth-child(6),
            .hide-columns table th:nth-child(6),
            .hide-columns table td:nth-child(7),
            .hide-columns table th:nth-child(7),
            .hide-columns table td:nth-child(8),
            .hide-columns table th:nth-child(8) {
                display: none;
            }*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <asp:HiddenField ID="hddpLG" runat="server" />
                <asp:HiddenField ID="hddParameterMenuID" runat="server" />
                <asp:HiddenField ID="hddReloadGrid" runat="server" />
                <asp:HiddenField ID="hddpSortBy" runat="server" />
                <asp:HiddenField ID="hddpSortType" runat="server" />
                <asp:HiddenField ID="hddpPagingDefault" runat="server" />
                <asp:HiddenField ID="hddKeyID" runat="server" />
                <asp:HiddenField ID="hddpPageInfo" runat="server" />
                <asp:HiddenField ID="hddpSearch" runat="server" />
                <asp:HiddenField ID="hddpFlagSearch" runat="server" />
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
                <asp:HiddenField ID="hddCheckUsedMaster" runat="server" />

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
                                                                        <a id="btnAddHref" runat="server" href="javascript:CallAddData();" class="btn btn-info glyph-icon icon-plus tooltip-button" visible='<%#Me.GetPermission().isAdd %>'></a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-lg-1">
                                                    </div>
                                                    <div class="col-lg-11">
                                                        <div class="row">
                                                            <div class="col-lg-8 col-md-12 col-sm-12">
                                                                <div class="row">
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsSalesGroup" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-8 col-md-12 col-sm-12">
                                                                        <asp:DropDownList ID="ddlsSalesGroup" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsDivision" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-8 col-md-12 col-sm-12">
                                                                        <asp:DropDownList ID="ddlsDivision" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsDepartment" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-8 col-md-12 col-sm-12">
                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="ddlsDepartment" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsSection" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-8 col-md-12 col-sm-12">
                                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="ddlsSection" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsZone" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-8 col-md-12 col-sm-12">
                                                                        <asp:DropDownList ID="ddlsZone" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsActive" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-8 col-md-12 col-sm-12">
                                                                        <asp:RadioButtonList ID="rdbsActive" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                <a href="javascript:CallLoaddata();" class="btn btn-info" title="Search Data">

                                                                    <i class="glyph-icon icon-search"></i>

                                                                    <asp:Label ID="lblsSearch" runat="server"></asp:Label>

                                                                </a>
                                                                <a href="javascript:CallClearFilter();" class="btn btn-danger" title="Cancel Filter">

                                                                    <i class="glyph-icon icon-close"></i>

                                                                    <asp:Label ID="lblsCancel2" runat="server"></asp:Label>

                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                    <div class="row" style="overflow-x: auto; overflow-y: hidden;">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <div>
                                                                <%--class="hide-columns"--%>
                                                                <%
                                                                    Dim lcSalesMan As New List(Of SalesMan_ViewModel)
                                                                    If hddReloadGrid.Value <> String.Empty Then
                                                                        If GetDataSaleMan() IsNot Nothing Then
                                                                            lcSalesMan = GetDataSaleMan()
                                                                %>
                                                                <table id="grdView" class="table table-striped table-bordered table-hover table-condensed" style="width: 100%">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                <asp:Label ID="TextHd10" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                <asp:Label ID="TextHd11" runat="server"></asp:Label></th>
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
                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                <asp:Label ID="TextHd8" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                <asp:Label ID="TextHd7" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                <asp:Label ID="TextHd9" runat="server"></asp:Label></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tfoot style="display: none;">
                                                                        <tr>
                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                <asp:Label ID="TextFt10" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                <asp:Label ID="TextFt11" runat="server"></asp:Label></th>
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
                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                <asp:Label ID="TextFt8" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                <asp:Label ID="TextFt7" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                <asp:Label ID="TextFt9" runat="server"></asp:Label></th>
                                                                        </tr>
                                                                    </tfoot>
                                                                    <tbody>
                                                                        <%
                                                                            Dim i As Integer = 0
                                                                            For Each sublcSalesMan As SalesMan_ViewModel In lcSalesMan
                                                                                Dim strb As New StringBuilder()
                                                                        %>
                                                                        <tr>
                                                                            <%
                                                                                strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-edit'  href='javascript:CallEditData(&#39;" + IIf(sublcSalesMan.SalesManCode <> String.Empty, sublcSalesMan.SalesManCode, String.Empty) + "&#39;);'></a></td>")
                                                                                Dim arr As String = hddCheckUsedMaster.Value
                                                                                If arr.IndexOf(sublcSalesMan.SalesManCode) = -1 Then
                                                                                    strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-danger glyph-icon icon-trash' href='javascript:ConfirmDelete(&#39;" + IIf(sublcSalesMan.SalesManCode <> String.Empty, sublcSalesMan.SalesManCode, String.Empty) + "&#39;);'></a></td>")
                                                                                Else
                                                                                    strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                                End If
                                                                                strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                                strb.Append("<td style='width:10%;'>" + IIf(sublcSalesMan.SalesManCode <> String.Empty, sublcSalesMan.SalesManCode, String.Empty) + "</td>")
                                                                                strb.Append("<td style='width:10%;'>" + IIf(sublcSalesMan.NameEN <> String.Empty, sublcSalesMan.NameEN, String.Empty) + "</td>")
                                                                                strb.Append("<td style='width:10%;'>" + IIf(sublcSalesMan.NameTH <> String.Empty, sublcSalesMan.NameTH, String.Empty) + "</td>")
                                                                                strb.Append("<td style='width:10%;'>" + IIf(sublcSalesMan.TypeDescription <> String.Empty, sublcSalesMan.TypeDescription, String.Empty) + "</td>")
                                                                                strb.Append("<td style='width:10%;'>" + IIf(Me.WebCulture.ToString.ToUpper = "TH", sublcSalesMan.DivisionDescriptionTH, sublcSalesMan.DivisionDescriptionEN) + "</td>")
                                                                                strb.Append("<td style='width:10%;'>" + IIf(sublcSalesMan.DepartmentDescription <> String.Empty, sublcSalesMan.DepartmentDescription, String.Empty) + "</td>")
                                                                                strb.Append("<td style='width:10%;'>" + IIf(sublcSalesMan.SectionDescription <> String.Empty, sublcSalesMan.SectionDescription, String.Empty) + "</td>")
                                                                                strb.Append("<td style='width:10%;'>" + IIf(sublcSalesMan.ZoneDescription <> String.Empty, sublcSalesMan.ZoneDescription, String.Empty) + "</td>")
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
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnDialog" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12">
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
                                                                                <%If (hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit) Then %>
                                                                                <a id="btnSaveData" runat="server" href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button"></a>
                                                                                <%End if %>
                                                                                <a id="btnCancelData" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="panel-body <%=IIf((hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit), "", "disabled") %>">
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaSaleManCode" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaSaleManCode" autocomplete="off" runat="server" CssClass="form-control" MaxLength="6" onkeypress="return removespacial(this, event);"></asp:TextBox>
                                                                            <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: center;">
                                                                            <asp:Label ID="lblaUserID" runat="server"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaUserID" autocomplete="off" runat="server" CssClass="form-control" MaxLength="5" onkeypress="return removespacial(this, event);"></asp:TextBox>
                                                                            <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaNameEn" runat="server"></asp:Label><asp:Label ID="Label3" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-7 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaNameEN" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="40" onkeypress="return removespacial(this, event);" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipNameEN") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaNameTH" runat="server"></asp:Label><asp:Label ID="Label4" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-7 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaNameTH" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="40" onkeypress="return removespacial(this, event);" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipNameTH") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage4" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaSalesGroup" runat="server"></asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-7 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaSalesGroup" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage5" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaDivision" runat="server"></asp:Label><asp:Label ID="Label6" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-7 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaDivision" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage6" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaDepartment" runat="server"></asp:Label><asp:Label ID="Label7" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-7 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaDepartment" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage7" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaSection" runat="server"></asp:Label><asp:Label ID="Label9" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-7 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaSection" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage10" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaZone" runat="server"></asp:Label><asp:Label ID="Label8" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-7 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaZone" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage8" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaActive" runat="server"></asp:Label><%--<asp:Label ID="Label9" runat="server" Text="*" ForeColor="red"></asp:Label>--%>
                                                                        </div>
                                                                        <div class="col-lg-7 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:RadioButtonList ID="rdbaActive" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaProject" runat="server"></asp:Label><%--<asp:Label ID="Label9" runat="server" Text="*" ForeColor="red"></asp:Label>--%>
                                                                        </div>
                                                                        <div class="col-lg-7 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaProject" style="display:none;" autocomplete="off" runat="server" CssClass="form-control" MaxLength="60" onkeypress="return removespacial(this, event);"></asp:TextBox>
                                                                            <asp:Label ID="lblMassage9" runat="server" ForeColor="Red"></asp:Label>
                                                                           <%-- <div class="form-group">
                                                                                <div class="col-lg-10 col-md-12 col-sm-12">--%>

                                                                                    <div class="ms-container">
                                                                                        <div style="float: left;"><%=grtt("resAll") %></div>
                                                                                        <div style="float: right;"><%=grtt("resSelectedProject") %></div>
                                                                                    </div>

                                                                                    <% 
                Dim chkAdd As Boolean = False
                Dim blProject As cSalesMan = New cSalesMan
                Dim lcProject As List(Of ED01PROJ) = blProject.GetProjectAll()
                If lcProject IsNot Nothing Then %>
                                                                                    <div class="ms-container">
                                                                                        <select multiple="multiple" class="multi-select" name="dualListboxProject">
                                                                                            <%  For Each subProject As ED01PROJ In lcProject
                    chkAdd = False
                    If dtProject IsNot Nothing Then
                        For i As Integer = 0 To dtProject.Rows.Count - 1
                            If subProject.FREPRJNO.ToString = dtProject.Rows(i)("FREPRJNO").ToString Then
                                                                                            %>
                                                                                            <option selected="selected" value="<%=dtProject.Rows(i)("FREPRJNO").ToString %>">
                                                                                                <%--<%=dtProject.Rows(i)("FREPRJNM").ToString %>--%>
                                                                                                <%=subProject.FREPRJNM.ToString %>
                                                                                            </option>
                                                                                            <% 
                        chkAdd = True
                        Exit For
                    End If
                Next
                If chkAdd = False Then
                                                                                            %>
                                                                                            <option value="<%=subProject.FREPRJNO.ToString %>">
                                                                                                <%=subProject.FREPRJNM.ToString %>
                                                                                            </option>
                                                                                            <%  
                    End If
                Else
                                                                                            %>
                                                                                            <option value="<%=subProject.FREPRJNO.ToString %>">
                                                                                                <%=subProject.FREPRJNM.ToString %>
                                                                                            </option>
                                                                                            <%  
                End If %>

                                                                                            <%  Next %>
                                                                                        </select>
                                                                                        <%   End If %>
                                                                                        <i class="glyph-icon icon-exchange"></i>
                                                                                    </div>
                                                                               <%-- </div>
                                                                            </div>--%>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel-body">
                                                                        <div class="row">
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
                                <button id="btnDeleteConfrim" runat="server" type="button" class="btn btn-danger" onclick="javascript:CallDeleteData();"></button>
                                <button id="btnCancelConfrim" runat="server" type="button" class="btn btn-default" data-dismiss="modal"></button>
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
                <asp:Button ID="btnClear" runat="server" CssClass="hide" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnClear" />
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
    <script src="js/moment-with-locales.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SetInitial();
            RegisUpdatePanelLoaded();
        });

        function RegisUpdatePanelLoaded() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endUpdatePanelRequestHandler);
        }
        function endUpdatePanelRequestHandler(sender, args) {
            SetInitial();
        }
        function SetInitial() {
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
            $(".multi-select").multiSelect();
        }

        function showOverlay() {
            $("#overlay").modal();
        }

        function CallCancel() {
            showOverlay();
            __doPostBack('<%= btnCancel.UniqueID%>');
        }

        function CallLoaddata() {
            showOverlay();
            __doPostBack('<%= btnSearch.UniqueID%>');
        }

        function CallClearFilter() {
            showOverlay();
            __doPostBack('<%= btnClear.UniqueID%>');
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

        function CallSaveData() {
            showOverlay();
            __doPostBack('<%= btnSave.UniqueID%>');
            return;
        }

        function CheckData() {
           <%-- if ($('#<%=txtaSaleManCode.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaSaleManCode.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                return;
            } else {
                $('#<%=txtaSaleManCode.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }--%>

            if ($('#<%=txtaUserID.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaUserID.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                scrollAndFocus('#<%=txtaUserID.ClientID%>');
                return;
            } else {
                $('#<%=txtaUserID.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
            }

            if ($('#<%=txtaNameEN.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaNameEN.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
                scrollAndFocus('#<%=txtaNameEN.ClientID%>');
                return;
            } else {
                $('#<%=txtaNameEN.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";
            }

            if ($('#<%=txtaNameTH.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaNameTH.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "";
                scrollAndFocus('#<%=txtaNameTH.ClientID%>');
                return;
            } else {
                $('#<%=txtaNameTH.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "none";
            }

            if ($('#<%=ddlaSalesGroup.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaSalesGroup.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "";
                scrollAndFocus('#<%=ddlaSalesGroup.ClientID%>');
                return;
            } else {
                $('#<%=ddlaSalesGroup.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "none";
            }

            if ($('#<%=ddlaDivision.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaDivision.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "";
                scrollAndFocus('#<%=ddlaDivision.ClientID%>');
                return;
            } else {
                $('#<%=ddlaDivision.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "none";
            }

            if ($('#<%=ddlaDepartment.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaDepartment.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage7').style.display = "";
                scrollAndFocus('#<%=ddlaDepartment.ClientID%>');
                return;
            } else {
                $('#<%=ddlaDepartment.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage7').style.display = "none";
            }

            if ($('#<%=ddlaSection.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaSection.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage10').style.display = "";
                scrollAndFocus('#<%=ddlaSection.ClientID%>');
                return;
            } else {
                $('#<%=ddlaSection.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage10').style.display = "none";
            }

            if ($('#<%=ddlaZone.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaZone.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage8').style.display = "";
                scrollAndFocus('#<%=ddlaZone.ClientID%>');
                return;
            } else {
                $('#<%=ddlaZone.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage8').style.display = "none";
            }


            <%--if ($('#<%=txtaProject.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaProject.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage9').style.display = "";
                return false;
            } else {
                $('#<%=txtaProject.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage9').style.display = "none";
            }--%>

            
            $('#<%=txtaProject.ClientID%>').val($('[name="dualListboxProject"]').val());

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
    </script>
</asp:Content>

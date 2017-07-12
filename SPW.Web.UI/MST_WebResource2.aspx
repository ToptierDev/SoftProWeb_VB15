<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="MST_WebResource2.aspx.vb" Inherits="SPW.Web.UI.MST_WebResource2" EnableEventValidation="false" %>

<%@ Import Namespace="SPW.Helper" %>
<%@ Import Namespace="SPW.BLL" %>
<%@ Import Namespace="SPW.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Button ID="btnEdit" runat="server" CssClass="hide" />
    <asp:HiddenField ID="hddpEdit" runat="server" />
    <asp:HiddenField ID="hddpSortBy" runat="server" />
    <asp:HiddenField ID="hddpSortType" runat="server" />
    <asp:HiddenField ID="hddpPagingDefault" runat="server" />
    <asp:HiddenField ID="hddpPageInfo" runat="server" />
    <asp:HiddenField ID="hddpSearch" runat="server" />
    <asp:HiddenField ID="hddpFlagSearch" runat="server" />
    <asp:Panel ID="Panel1" runat="server">
        <asp:HiddenField ID="hddParameterMenuID" runat="server" />
        <asp:HiddenField ID="hddReloadGrid" runat="server" />
        <asp:HiddenField ID="hddMSGSaveData" runat="server" />
        <asp:HiddenField ID="hddMSGCancelData" runat="server" />
        <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
        <asp:HiddenField ID="hddMSGEditData" runat="server" />
        <asp:HiddenField ID="hddMSGAddData" runat="server" />
        <asp:HiddenField ID="hddsMenu" runat="server" />
        <asp:HiddenField ID="hddsSubMenu" runat="server" />
        <asp:HiddenField ID="hddaMenu" runat="server" />
        <asp:HiddenField ID="hddaSubMenu" runat="server" />
        <asp:HiddenField ID="hddpLG" runat="server" />
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
                                                                <a id="hrefAdd" runat="server" href="javascript:CallAddData();" class="btn btn-info glyph-icon icon-plus tooltip-button" Visible='<%#Me.GetPermission().isAdd %>'></a>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <div class="col-lg-12">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                <div class="row">
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsModule" runat="server" Text="Label"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-12 col-sm-12">
                                                                        <asp:DropDownList ID="ddlsModule" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-lg-1 col-md-12 col-sm-12">
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsType" runat="server" Text="Label"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-12 col-sm-12">
                                                                        <asp:DropDownList ID="ddlsType" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsMenu" runat="server" Text="Label"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-12 col-sm-12">
                                                                        <asp:DropDownList ID="ddlsMenu" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-lg-1 col-md-12 col-sm-12">
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsSearchTH" runat="server" Text="Label"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-12 col-sm-12">
                                                                        <asp:TextBox ID="txtsSearchTH" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsSubMenu" runat="server" Text="Label"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-12 col-sm-12">
                                                                        <asp:DropDownList ID="ddlsSubMenu" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-lg-1 col-md-12 col-sm-12">
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsSearchEN" runat="server" Text="Label"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-12 col-sm-12">
                                                                        <asp:TextBox ID="txtsSearchEN" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsSubMenu1" runat="server" Text="Label"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-4 col-md-12 col-sm-12">
                                                                        <asp:DropDownList ID="ddlsSubMenu1" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-lg-1 col-md-12 col-sm-12">
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblsBaseMassage" runat="server" Text="Label"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-3 col-md-12 col-sm-12">
                                                                        <asp:DropDownList ID="ddlsBaseMassage" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                <a href="javascript:CallLoaddata();" class="btn  btn-info">
                                                                    <i class="glyph-icon icon-search"></i>
                                                                    <%=grtt("resSearch") %>
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                    &nbsp;
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <div class="row" style="overflow-x: auto; overflow-y: hidden;">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <%
    Dim lcWebResource As New List(Of WebResource_ViewModel)
    If hddReloadGrid.Value <> String.Empty Then
        If LoadDataWebResource() IsNot Nothing Then
            lcWebResource = LoadDataWebResource()
                                                            %>
                                                            <table id="grdView" class="table table-striped table-bordered table-hover" style="width: 100%">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResource("col_edit", "Text", "1")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("No")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("MenuName")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("ResourceName")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("ResourceType")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("ResourceValueEN")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("ResourceValueLC")%></th>
                                                                    </tr>
                                                                </thead>
                                                                <tfoot style="display: none;">
                                                                    <tr>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResource("col_edit", "Text", "1")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("No")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("MenuName")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("ResourceName") %></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("ResourceType")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("ResourceValueEN")%></th>
                                                                        <th style="text-align: center; vertical-align: text-top;"><%#GetResourceTypeText("ResourceValueLC")%></th>
                                                                    </tr>
                                                                </tfoot>
                                                                <tbody>
                                                                    <%
                        For Each sublcWebResource As WebResource_ViewModel In lcWebResource
                                                                    %>
                                                                    <tr>
                                                                        <td style="width: 5%; text-align: center;"><a class="btn btn-info glyph-icon icon-edit" href="javascript:EditData('<%=IIf(sublcWebResource.ResourceID <> String.Empty, sublcWebResource.ResourceID, String.Empty)%>');"></a></td>
                                                                        <td style="width: 5%"></td>
                                                                        <td style="width: 15%"><%=IIf(Me.WebCulture = "TH", sublcWebResource.MenuNameLC, sublcWebResource.MenuNameEN)%></td>
                                                                        <td style="width: 20%"><%=IIf(sublcWebResource.ResourceName <> String.Empty, sublcWebResource.ResourceName, String.Empty)%></td>
                                                                        <td style="width: 5%"><%=IIf(sublcWebResource.ResourceType <> String.Empty, sublcWebResource.ResourceType, String.Empty)%></td>
                                                                        <td style="width: 25%"><%=IIf(sublcWebResource.ResourceValueEN <> String.Empty, sublcWebResource.ResourceValueEN, String.Empty)%></td>
                                                                        <td style="width: 25%"><%=IIf(sublcWebResource.ResourceValueLC <> String.Empty, sublcWebResource.ResourceValueLC, String.Empty)%></td>
                                                                    </tr>
                                                                    <%
    Next
                                                                    %>
                                                                </tbody>
                                                            </table>
                                                            <% 
                            End If
                        End If
                                                            %>
                                                        </div>
                                                    </div>


                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                                    <%--   <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnSave" />--%>
                                                </Triggers>
                                            </asp:UpdatePanel>

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
                                                                        <%If (hddpEdit.Value = String.Empty And Me.GetPermission.isAdd) Or (hddpEdit.Value <> String.Empty And Me.GetPermission.isEdit) Then %>
                                                                        <a id="hrefSave" runat="server" href="javascript:CheckData();" class="btn btn-info tooltip-button glyph-icon icon-save"></a>
                                                                        <%End if %>
                                                                        <a id="hrefCencel" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="panel-body <%=IIf((hddpEdit.Value = String.Empty And Me.GetPermission.isAdd) Or (hddpEdit.Value <> String.Empty And Me.GetPermission.isEdit), "", "disabled") %>">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="row" id="rowBaseMassage" runat="server">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaBaseMassage" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-12 col-sm-12">
                                                                            <asp:DropDownList ID="ddlaBaseMassage" runat="server" CssClass="chosen-select" Enabled="false"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12">
                                                                            <asp:CheckBox ID="chksCopyToMenu" runat="server" AutoPostBack="true" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="br1" runat="server">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            &nbsp;
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="rowModule" runat="server">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaModule" runat="server" Text="Label"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-12 col-sm-12">
                                                                            <asp:DropDownList ID="ddlaModule" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="br2" runat="server">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            &nbsp;
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="rowMenu" runat="server">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaMenu" runat="server"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaMenu" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="br3" runat="server">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            &nbsp;
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="rowSubMenu" runat="server">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaSubMenu" runat="server"></asp:Label><asp:Label ID="Label7" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaSubMenu" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="br4" runat="server">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            &nbsp;
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="rowSubMenu1" runat="server">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaSubMenu1" runat="server"></asp:Label><asp:Label ID="Label8" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaSubMenu1" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage7" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="br5" runat="server">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            &nbsp;
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    <asp:Label ID="lblaResourceName" runat="server"></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                    <asp:TextBox ID="txtaResourceName" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="50" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipResourceName") %>'></asp:TextBox>
                                                                    <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    <asp:Label ID="lblaType" runat="server"></asp:Label><asp:Label ID="Label4" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                    <asp:DropDownList ID="ddlaType" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                    <asp:Label ID="lblMassage4" runat="server" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    <asp:Label ID="lblaResourceValueEN" runat="server"></asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                    <asp:TextBox ID="txtaResourceValueEN" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="255" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipResourceValueEN") %>'></asp:TextBox>
                                                                    <asp:Label ID="lblMassage5" runat="server" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    <asp:Label ID="lblaResourceValueLC" runat="server"></asp:Label><asp:Label ID="Label6" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                    <asp:TextBox ID="txtaResourceValueLC" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="255" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipResourceValueLC") %>'></asp:TextBox>
                                                                    <asp:Label ID="lblMassage6" runat="server" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <asp:HiddenField ID="hddID" runat="server" />
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
        <asp:Button ID="btnSearch" runat="server" CssClass="hide" />
        <asp:Button ID="btnAdd" runat="server" CssClass="hide" />
        <asp:Button ID="btnSave" runat="server" CssClass="hide" />
        <asp:Button ID="btnCancel" runat="server" CssClass="hide" />
    </asp:Panel>

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
            if ($('#grdView') != null) {
                if (!$.fn.DataTable.isDataTable('#grdView')) {
                    var t = $('#grdView').DataTable({
                        "order": [[$('#<%=hddpSortBy.ClientID%>').val(), $('#<%=hddpSortType.ClientID%>').val()]],
                        "pageLength": parseInt($('#<%=hddpPagingDefault.ClientID%>').val()),
                        "columnDefs": [{
                            "searchable": false,
                            "orderable": false,
                            "targets": [0 ,1]
                        }]
                    });

                    t.on('order.dt search.dt', function () {
                        t.column(1, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
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

        function showOverlay() {
            $("#overlay").modal();
        }
        function closeOverlay() {
            $("#overlay").modal('hide');
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

        function CheckData() {
            if (document.getElementById('<%=br1.ClientID%>').style.display == "none") {
                if ($('#<%=ddlaMenu.ClientID%>').val().replace(" ", "") == "") {
                    $('#<%=ddlaMenu.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                    scrollAndFocus('#<%=ddlaMenu.ClientID%>');
                return;
            } else {
                $('#<%=ddlaMenu.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }
            if ($('#<%=ddlaSubMenu.ClientID%>').val().replace(" ", "") == "") {
                    $('#<%=ddlaSubMenu.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                scrollAndFocus('#<%=ddlaSubMenu.ClientID%>');
                return;
            } else {
                $('#<%=ddlaSubMenu.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
            }
            var ddlaSubMenu1 = document.getElementById('<%=ddlaSubMenu1.ClientID %>');
                if (ddlaSubMenu1.options.length > 1) {
                    if ($('#<%=ddlaSubMenu1.ClientID%>').val().replace(" ", "") == "") {
                    $('#<%=ddlaSubMenu1.ClientID%>').addClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage7').style.display = "";
                        scrollAndFocus('#<%=ddlaSubMenu1.ClientID%>');
                    return;
                } else {
                    $('#<%=ddlaSubMenu1.ClientID%>').removeClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage7').style.display = "none";
                }
            }
        }
        if ($('#<%=txtaResourceName.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaResourceName.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
            scrollAndFocus('#<%=txtaResourceName.ClientID%>');
                return;
            } else {
                $('#<%=txtaResourceName.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";
            }
            if ($('#<%=ddlaType.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaType.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "";
                scrollAndFocus('#<%=ddlaType.ClientID%>');
                return;
            } else {
                $('#<%=ddlaType.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "none";
            }
            if ($('#<%=txtaResourceValueEN.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaResourceValueEN.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "";
                scrollAndFocus('#<%=txtaResourceValueEN.ClientID%>');
                return;
            } else {
                $('#<%=txtaResourceValueEN.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "none";
            }

            if ($('#<%=txtaResourceValueLC.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaResourceValueLC.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "";
                scrollAndFocus('#<%=txtaResourceValueLC.ClientID%>');
                return;
            } else {
                $('#<%=txtaResourceValueLC.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "none";
            }




            showOverlay();

            __doPostBack('<%= btnSave.UniqueID%>');
            return;
        }

        function EditData(pKey) {
            showOverlay(); 19
            var hddpEdit = document.getElementById('<%=hddpEdit.ClientID%>')
            if (pKey != "") {
                hddpEdit.value = pKey;
            }
            showOverlay();

            __doPostBack('<%= btnEdit.UniqueID%>');

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
    </script>
</asp:Content>

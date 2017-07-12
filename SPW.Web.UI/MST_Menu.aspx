<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="MST_Menu.aspx.vb" Inherits="SPW.Web.UI.MST_Menu" EnableEventValidation="false" %>

<%@ Import Namespace="SPW.Helper" %>
<%@ Import Namespace="SPW.BLL" %>
<%@ Import Namespace="SPW.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modal-dialog {
           position: fixed; 
           top:35%; 
           left:40%; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <asp:HiddenField ID="hddpSearch" runat="server"/>
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
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
                                                            <a id="btnMSGAddData" runat="server" href="javascript:CallAddData();" class="btn btn-info glyph-icon icon-plus tooltip-button" Visible='<%#Me.GetPermission().isAdd %>'>
                                                          
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="row">
                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                    &nbsp;
                                                                </div>
                                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblsModule" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-10 col-md-12 col-sm-12">
                                                                            <asp:DropDownList ID="ddlsModule" runat="server" CssClass="chosen-select" onchange="CallLoaddata();"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                    
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" style="overflow-x:auto;overflow-y:hidden;">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <%
                                                                Dim lcMenu As New List(Of Menu_ViewModel)
                                                                If hddReloadGrid.Value <> String.Empty Then
                                                                    If GetDataMenu() IsNot Nothing Then
                                                                        lcMenu = GetDataMenu()
                                                            %>
                                                                <table id="grdView" class="table table-striped table-bordered table-hover" style="width: 100%">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                <asp:Label ID="TextHd11" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                <asp:Label ID="TextHd12" runat="server"></asp:Label></th>
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
                                                                                <asp:Label ID="TextHd7" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                <asp:Label ID="TextHd8" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                <asp:Label ID="TextHd9" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                <asp:Label ID="TextHd10" runat="server"></asp:Label></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tfoot style="display: none;">
                                                                        <tr>
                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                <asp:Label ID="TextFt11" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                <asp:Label ID="TextFt12" runat="server"></asp:Label></th>
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
                                                                                <asp:Label ID="TextFt7" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                <asp:Label ID="TextFt8" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                <asp:Label ID="TextFt9" runat="server"></asp:Label></th>
                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                <asp:Label ID="TextFt10" runat="server"></asp:Label></th>
                                                                        </tr>
                                                                    </tfoot>
                                                                    <tbody>
                                                                        <%
                        Dim i As Integer = 0
                        For Each sublcMenu As Menu_ViewModel In lcMenu
                            Dim strb As New StringBuilder()
                                                                        %>
                                                                        <tr>
                                                                            
                                                                                <% 
                                                                                    strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-typicons-edit' href='javascript:CallEditData(&#39;" + Convert.ToString(sublcMenu.MenuID) + "&#39;);'></a></td>")
                                                                                    strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-danger glyph-icon icon-typicons-trash' href='javascript:ConfirmDelete(&#39;" + Convert.ToString(sublcMenu.MenuID) + "&#39;,&#39;" + IIf(Me.WebCulture = "TH", IIf(sublcMenu.MenuNameLC <> String.Empty, sublcMenu.MenuNameLC, String.Empty), IIf(sublcMenu.MenuNameEN <> String.Empty, sublcMenu.MenuNameEN, String.Empty)) + "&#39;);'></a></td>")
                                                                                    strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                                    strb.Append("<td style='width:5%;'>" + Convert.ToString(sublcMenu.MenuID) + "</td>")
                                                                                    strb.Append("<td style='width:5%;'>" + Convert.ToString(IIf(Me.WebCulture.ToUpper = "TH", sublcMenu.ModuleNameTH, sublcMenu.ModuleNameEN)) + "</td>")
                                                                                    strb.Append("<td style='width:15%;'>" + IIf(sublcMenu.MenuNameLC <> String.Empty, sublcMenu.MenuNameLC, String.Empty) + "</td>")
                                                                                    strb.Append("<td style='width:15%;'>" + IIf(sublcMenu.MenuNameEN <> String.Empty, sublcMenu.MenuNameEN, String.Empty) + "</td>")
                                                                                    strb.Append("<td style='width:22%;'>" + IIf(sublcMenu.MenuLocation <> String.Empty, sublcMenu.MenuLocation, String.Empty) + "</td>")
                                                                                    strb.Append("<td style='width:5%;'>" + IIf(sublcMenu.MenuType <> String.Empty, sublcMenu.MenuType, String.Empty) + "</td>")
                                                                                    strb.Append("<td style='width:5%;'>" + Convert.ToString(sublcMenu.ParentID) + "</td>")
                                                                                    strb.Append("<td style='width:5%;'>" + Convert.ToString(sublcMenu.Sequence) + "</td>")
                                                                                    strb.Append("<td style='width:10%;'>" + IIf(sublcMenu.EnableFlag = 1, GetResource("vActive", "Text", hddParameterMenuID.Value), GetResource("vInActive", "Text", hddParameterMenuID.Value)) + "</td>")

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
                                                                                <asp:Button ID="btnSaveTemp" runat="server" OnClientClick="javascript:CheckData();return false" CssClass="hide"/>
                                                                                <%If (hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit) Then %>
                                                                                <a id="btnMSGSaveData" runat="server" href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button">
                                                                                  
                                                                                </a>
                                                                                <%End if %>
                                                                                <a id="btnMSGCancelData" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button">
                                                                                
                                                                                </a>  
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="panel-body <%=IIf((hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit), "", "disabled") %>">
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaMenuID" runat="server"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaMenuID" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="38" onKeyPress="return Check_Key_Decimal(this,event)" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipMenuID") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaMenuNameLC" runat="server"></asp:Label><asp:Label ID="Label4" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaMenuNameLC" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="100" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipMenuNameLC") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaMenuNameEN" runat="server"></asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaMenuNameEN" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="100" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipMenuNameEN") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaMenuLocation" runat="server"></asp:Label><asp:Label ID="Label6" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaMenuLocation" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="255" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipMenuLocation") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage4" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaMenuType" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:RadioButtonList ID="rbtaMenuType" runat="server" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Selected="True" Value="FRM" Text="&nbsp;Form&nbsp;"></asp:ListItem>
                                                                                <asp:ListItem Value="RPT" Text="&nbsp;Report&nbsp;"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                            <asp:Label ID="lblMassage5" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaModule" runat="server"></asp:Label><asp:Label ID="Label3" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaModule" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage9" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-5 col-sm-5">
                                                                            <asp:Label ID="lblaParentID" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-7 col-sm-7" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaParentID" autocomplete="off" runat="server" CssClass="form-control" MaxLength="38" onKeyPress="return Check_Key_Decimal(this,event)"></asp:TextBox>
                                                                            <asp:Label ID="lblMassage6" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-5 col-sm-5">
                                                                            <asp:Label ID="lblaSequence" runat="server"></asp:Label><asp:Label ID="Label7" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-7 col-sm-7" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaSequence" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="38" onKeyPress="return Check_Key_Decimal(this,event)" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipSequence") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage7" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-5 col-sm-5">
                                                                            <asp:Label ID="lblaStatus" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-7 col-sm-7" style="text-align: left;">
                                                                            <asp:CheckBox ID="chkStatus" runat="server" />
                                                                            <asp:Label ID="lblMassage8" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%--<div class="panel-footer">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12" style="text-align: center;">
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
                                <asp:TextBox ID="lblBodydelete" runat="server" BackColor="White" BorderStyle="None" BorderWidth="0" Enabled="false" style="width:100%"></asp:TextBox>
                            </div>
                            <div class="modal-footer">
                                <button id="btnMSGDeleteData" runat="server" type="button" class="btn btn-danger" onclick="javascript:CallDeleteData();"></button>
                                <button id="btnMSGCancelDataS" runat="server" type="button" class="btn btn-default" data-dismiss="modal"></button>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnAdd" runat="server" CssClass="hide" />
                <asp:Button ID="btnEdit" runat="server" CssClass="hide" />
                <asp:Button ID="btnDelete" runat="server" CssClass="hide" />
                <asp:Button ID="btnSave" runat="server" CssClass="hide" />
                <asp:Button ID="btnCancel" runat="server" CssClass="hide" />
                <asp:Button ID="btnSearch" runat="server" CssClass="hide" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="modal fade .bs-example-modal-sm" id="overlay" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000; opacity: 0.6;">
            <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #000; font-size: 36px; left: 40%; top: 30%;">
                <img src="image/loading.gif" alt="Loading ..." style="height: 200px" /></span>
        </div>  
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            SetInitial();
        });

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
                            "targets": [0, 1, 2, 4]
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

        function showOverlay() {
            $("#overlay").modal();
        }

        function CallLoaddata() {
            showOverlay();
            __doPostBack('<%= btnSearch.UniqueID%>');
        }

        function CallCancel() {
            showOverlay();
            __doPostBack('<%= btnCancel.UniqueID%>');
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

        function ConfirmDelete(pKey,Name) {
            var hddKeyID = document.getElementById("<%= hddKeyID.ClientID%>");
            var lblBodydelete = document.getElementById("<%= lblBodydelete.ClientID%>");

            hddKeyID.value = pKey;
            lblBodydelete.value = lblBodydelete.defaultValue + " " + pKey + " ?";

            $("#DeleteModal").modal();
        }

        function CheckData() {
            if ($('#<%=txtaMenuID.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaMenuID.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                scrollAndFocus('#<%=txtaMenuID.ClientID%>');
                return false;
            } else {
                $('#<%=txtaMenuID.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }
            if ($('#<%=txtaMenuNameLC.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaMenuNameLC.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                scrollAndFocus('#<%=txtaMenuNameLC.ClientID%>');
                return;
            } else {
                $('#<%=txtaMenuNameLC.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
            }
            if ($('#<%=txtaMenuNameEN.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaMenuNameEN.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
                scrollAndFocus('#<%=txtaMenuNameEN.ClientID%>');
                return;
            } else {
                $('#<%=txtaMenuNameEN.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";
            }
            if ($('#<%=txtaMenuLocation.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaMenuLocation.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "";
                scrollAndFocus('#<%=txtaMenuLocation.ClientID%>');
                return;
            } else {
                $('#<%=txtaMenuLocation.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "none";
            }
            if ($('#<%=txtaSequence.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaSequence.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage7').style.display = "";
                scrollAndFocus('#<%=txtaSequence.ClientID%>');
                return;
            } else {
                $('#<%=txtaSequence.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage7').style.display = "none";
            }
            if ($('#<%=ddlaModule.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaModule.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage9').style.display = "";
                scrollAndFocus('#<%=ddlaModule.ClientID%>');
                return;
            } else {
                $('#<%=ddlaModule.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage9').style.display = "none";
            }

            showOverlay();
            __doPostBack('<%= btnSave.UniqueID%>');
            return;
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
    </script>
</asp:Content>

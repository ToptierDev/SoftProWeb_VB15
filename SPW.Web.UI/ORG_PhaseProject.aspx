<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ORG_PhaseProject.aspx.vb" Inherits="SPW.Web.UI.ORG_PhaseProject" EnableEventValidation="false" %>

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

        .grdView td:nth-child(5), .grdView th:nth-child(5) {
            <%=IIF(Me.GetPermission().isDelete,"","display:none;" )%>;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:DatePicker runat="server" ID="ucDatePicker" />
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
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
                <asp:HiddenField ID="hddMasterLG" runat="server" />
                <asp:HiddenField ID="hddChkPhaseDelete" runat="server" />
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
                                <asp:Panel ID="pnMain" runat="server">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12" style="margin-left: 2px;">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <h3>
                                                                            <asp:Label ID="lblMain3" runat="server"></asp:Label>
                                                                        </h3>
                                                                    </td>
                                                                    <td style="text-align: right;">
                                                                        <asp:Button ID="btnSaveTemp" runat="server" OnClientClick="javascript:CallSave();return false" CssClass="hide" />
                                                                        <a id="btnMSGSaveData" runat="server" href="javascript:CallSave();" class="btn glyph-icon icon-save btn-info tooltip-button" visible='<%#Me.GetPermission().isEdit %>'></a>
                                                                        <a id="btnMSGCancelData" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="row">
                                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblsProject" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-10 col-md-12 col-sm-12">
                                                                            <asp:DropDownList ID="ddlsProject" runat="server" CssClass="chosen-select" AutoPostBack="true" onChange="CallReload();"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                    <%--<table>
                                                                    <tr>
                                                                        <td>
                                                                            <div class="content-box remove-border dashboard-buttons clearfix">
                                                                                <a href="javascript:CallSave();" class="btn vertical-button remove-border btn-info" title="Save Data">
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
                                                                </table>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row <%=IIf(Me.GetPermission.isEdit, "", "disabled") %>">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="grdView" runat="server" CssClass="table table-hover table-striped table-bordered table-responsive grdView"
                                                                        EmptyDataText="No Items." EmptyDataRowStyle-CssClass="gridview-nodata" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" Style="width: 85%">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("gPhaseCode", "Text", hddParameterMenuID.Value)%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtgPhaseCode" autocomplete="off" runat="server" CssClass="form-control" Style="height: 28px; vertical-align: top;" MaxLength="3"></asp:TextBox>
                                                                                    <asp:HiddenField ID="hddID" runat="server" />
                                                                                    <asp:HiddenField ID="hddFlagSetAdd" runat="server" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle />
                                                                                <ItemStyle HorizontalAlign="left" Width="10%" Height="15px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("gStartDate", "Text", hddParameterMenuID.Value)%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtgStartDate" autocomplete="off" runat="server" data-date-format="dd/mm/yyyy" MaxLength="10" Style="height: 28px; vertical-align: top;" onKeyPress="return Check_Key_Date(this,event)"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle />
                                                                                <ItemStyle HorizontalAlign="center" Width="20%" Height="15px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("gEndDate", "Text", hddParameterMenuID.Value)%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtgEndDate" autocomplete="off" runat="server" data-date-format="dd/mm/yyyy" MaxLength="10" Style="height: 28px; vertical-align: top;" onKeyPress="return Check_Key_Date(this,event)"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle />
                                                                                <ItemStyle HorizontalAlign="center" Width="20%" Height="15px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("gDesc", "Text", hddParameterMenuID.Value)%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtgDesc" autocomplete="off" runat="server" class="form-control" Style="height: 28px; vertical-align: top;" MaxLength="50"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle />
                                                                                <ItemStyle HorizontalAlign="center" Width="30%" Height="15px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("col_delete", "Text", "1")%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="btnDelete" ImageUrl="~/image/delete.jpg" Style="width: 30px; height: 30px; margin-left: 10px;" ToolTip="Click to Delete" />
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
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <h3>
                                                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                        </h3>
                                                                    </td>
                                                                    <td style="text-align: right;">
                                                                        <a id="A1" runat="server" href="javascript:CallSave();" class="btn glyph-icon icon-save btn-info tooltip-button"  Visible='<%#Me.GetPermission().isEdit %>'>
                                                                         
                                                                        </a>
                                                                        <a id="A2" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button">
                                                                          
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </table>
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

                <asp:Button ID="btnReloadGrid" runat="server" CssClass="hide" />
                <asp:Button ID="btnSave" runat="server" CssClass="hide" />
                <asp:Button ID="btnCancel" runat="server" CssClass="hide" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnReloadGrid" />
            <asp:PostBackTrigger ControlID="btnCancel" />
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

        function Check_Key_Date(txt, e) {
            var Dates = document.getElementById(txt.id);
            var key;
            if (document.all) {
                key = window.event.keyCode;
            }
            else {
                key = e.which;
            }

            if (key != 47) {
                if (key < 48 || key > 57) {
                    if (key == 0 || key == 8) {
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    if (String(Dates.value).length > 9) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            } else {
                if (key == 47) {
                    if (Dates.value.indexOf("/") > -1) {
                        if (Dates.value.split("/").length > 2) {
                            return false;
                        }
                    } else {
                        return true;
                    }
                }
            }
        }

        function SetInitial() {
            <%-- var hddMasterLG = document.getElementById("<%= hddMasterLG.ClientID%>");
            if (hddMasterLG.value == "TH") {
                $('.datepicker').datepicker({
                    language: 'th-th',
                    isRTL: false,
                    format: 'dd/mm/yyyy',
                    autoclose: true
                });
            } else {
                $('.datepicker').datepicker({
                    language: 'en',
                    isRTL: false,
                    format: 'dd/mm/yyyy',
                    autoclose: true
                });
            }--%>
            attachDatePicker();
        }

        function showOverlay() {
            $("#overlay").modal();
        }

        function CallReload() {
            showOverlay();
            __doPostBack('<%= btnReloadGrid.UniqueID%>');
            return;
        }

        function CallCancel() {
            showOverlay();
            __doPostBack('<%= btnCancel.UniqueID%>');
            return;
        }

        function CallSave() {
            if ($('#<%=ddlsProject.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlsProject.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                return;
            } else {
                $('#<%=ddlsProject.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }

            showOverlay();

            __doPostBack('<%= btnSave.UniqueID%>');
            return false;
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

         function Check_Key_Date(txt, e) {
             var Dates = document.getElementById(txt.id);
             var key;
             if (document.all) {
                 key = window.event.keyCode;
             }
             else {
                 key = e.which;
             }

             //key != 46 && 
             if (key != 47) {
                 if (key < 48 || key > 57) {
                     if (key == 0 || key == 8) {
                         return true;
                     } else {
                         return false;
                     }
                 } else {
                     if (String(Dates.value).length > 9) {
                         return false;
                     }
                     else {
                         return true;
                     }
                 }
             } else {
                 if (key == 47) {
                     if (Dates.value.indexOf("/") > -1) {
                         if (Dates.value.split("/").length > 2) {
                             return false;
                         }
                     } else {
                         return true;
                     }
                 }
             }
         }
    </script>
</asp:Content>

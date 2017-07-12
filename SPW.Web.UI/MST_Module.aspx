<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="MST_Module.aspx.vb" Inherits="SPW.Web.UI.MST_Module" EnableEventValidation="false" %>

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
                <asp:HiddenField ID="hddpPageInfo" runat="server"/>
                <asp:HiddenField ID="hddpSearch" runat="server"/>
                <asp:HiddenField ID="hddpClientID" runat="server" />
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
                                <asp:Panel ID="pnMain" runat="server">
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
                                                            <a id="btnMSGAddData" runat="server" href="javascript:CallAddData();" 
                                                                class="btn btn-info glyph-icon icon-plus tooltip-button" Visible='<%#Me.GetPermission().isAdd %>'>
                                                            
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row" style="overflow-x:auto;overflow-y:hidden;">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <%
                                                                Dim lc As New List(Of CoreModule)
                                                                If hddReloadGrid.Value <> String.Empty Then
                                                                    If GetData() IsNot Nothing Then
                                                                        lc = GetData()
                                                            %>
                                                            <table id="grdView" class="table table-striped table-bordered table-hover" style="width: 100%">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd5" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd6" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd1" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd2" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd3" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd4" runat="server"></asp:Label></th>
                                                                    </tr>
                                                                </thead>
                                                                <tfoot style="display: none;">
                                                                    <tr>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt5" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt6" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt1" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt2" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt3" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt4" runat="server"></asp:Label></th>
                                                                    </tr>
                                                                </tfoot>
                                                                <tbody>
                                                                    <%
                        Dim i As Integer = 0
                        For Each sublc As CoreModule In lc
                            Dim strb As New StringBuilder()
                                                                    %>
                                                                    <tr>
                                                                        <%
                                                                            strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-typicons-edit' href='javascript:CallEditData(&#39;" + IIf(CStr(sublc.ModuleID) <> String.Empty, CStr(sublc.ModuleID), String.Empty) + "&#39;);'></a></td>")
                                                                            strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-danger glyph-icon icon-typicons-trash' href='javascript:ConfirmDelete(&#39;" + IIf(CStr(sublc.ModuleID) <> String.Empty, CStr(sublc.ModuleID), String.Empty) + "&#39;);'></a></td>")
                                                                            strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                            strb.Append("<td style='width:20%;'>" + IIf(CStr(sublc.ModuleID) <> String.Empty, CStr(sublc.ModuleID), String.Empty) + "</td>")
                                                                            strb.Append("<td style='width:30%;'>" + IIf(sublc.ModuleNameEN <> String.Empty, sublc.ModuleNameEN, String.Empty) + "</td>")
                                                                            strb.Append("<td style='width:30%;'>" + IIf(sublc.ModuleNameTH <> String.Empty, sublc.ModuleNameTH, String.Empty) + "</td>")

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
                                                                            <asp:Label ID="lblaCode" runat="server"></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaCode" autocomplete="off" runat="server" CssClass="form-control tooltip-button" onKeyPress="return Check_Key_Decimal(this,event)" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipCode") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaNameEN" runat="server"></asp:Label><asp:Label ID="Label3" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaNameEN" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="255" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipNameEN") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaNameTH" runat="server"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaNameTH" runat="server" autocomplete="off" CssClass="form-control tooltip-button" MaxLength="255" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipNameTH") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
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
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <asp:PostBackTrigger ControlID="btnSave" />
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

                    if ($('#<%=hddpPageInfo.ClientID%>').val() != "")
                    {
                        t.page(parseInt($('#<%=hddpPageInfo.ClientID%>').val())).draw(false);
                    }

                    t.on('search.dt keyup', function () {
                        var values = $('.dataTables_filter input').val();
                        hddpSearch.value = values;

                        var info = t.page.info();
                        hddpPageInfo.value = info.page;
                    })

                    if ($('#<%=hddpSearch.ClientID%>').val() != "")
                    {
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

        function FocusSet(txt) {
            var txt = document.getElementById(txt);
            txt.focus();
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
            if ($('#<%=txtaCode.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaCode.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                scrollAndFocus('#<%=txtaCode.ClientID%>');
                return;
            } else {
                $('#<%=txtaCode.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }
            if ($('#<%=txtaNameEN.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaNameEN.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                scrollAndFocus('#<%=txtaNameEN.ClientID%>');
                return;
            } else {
                $('#<%=txtaNameEN.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
            }
            if ($('#<%=txtaNameTH.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaNameTH.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
                scrollAndFocus('#<%=txtaNameTH.ClientID%>');
                return;
            } else {
                $('#<%=txtaNameTH.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";
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

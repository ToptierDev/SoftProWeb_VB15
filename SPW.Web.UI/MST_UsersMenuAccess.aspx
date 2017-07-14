<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="MST_UsersMenuAccess.aspx.vb" Inherits="SPW.Web.UI.MST_UsersMenuAccess" EnableEventValidation="false" %>

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
                <asp:HiddenField ID="hddpSearch" runat="server" />
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
                <asp:HiddenField ID="hddpLG" runat="server" />
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
                                                                        <asp:Button ID="btnSaveTemp" runat="server" OnClientClick="javascript:CallSave()" CssClass="hide" />
                                                                        <a id="btnMSGSaveData" runat="server" href="javascript:CallSave();" class="btn glyph-icon icon-save btn-info tooltip-button" visible='<%#Me.GetPermission().isEdit %>'></a>
                                                                        <a id="btnMSGCancelData" runat="server" href="javascript:CallReload();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
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
                                                                            <asp:Label ID="lblsUserId" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-10 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtsUserID" autocomplete="off" runat="server" BackColor="#ffe0c0" CssClass="form-control" onkeypress="return AutocompletedUserID(this,event);" onClick="AutocompletedUserID(this,event);"></asp:TextBox>
                                                                            <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblsModule" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-10 col-md-12 col-sm-12">
                                                                            <asp:DropDownList ID="ddlsModule" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-lg-6 col-md-12 col-sm-12">
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <label><%=GetResourceTypeText("resMenuType") %></label>
                                                                        </div>
                                                                        <div class="col-lg-10 col-md-12 col-sm-12">
                                                                            <label>
                                                                                <input type="radio" value="FRM" name="menuType" checked="checked" /><% =GetResourceTypeText("resMenuTypeFRM")%>
                                                                            </label>
                                                                            <label>
                                                                                <input type="radio" value="RPT" name="menuType" /><% =GetResourceTypeText("resMenuTypeRPT")%>
                                                                            </label>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">

                                                            <asp:HiddenField ID="hddSelectedMenu" runat="server" />
                                                            <asp:HiddenField ID="hddSelectedMenuAdd" runat="server" />
                                                            <asp:HiddenField ID="hddSelectedMenuEdit" runat="server" />
                                                            <asp:HiddenField ID="hddSelectedMenuDelete" runat="server" />
                                                            <asp:HiddenField ID="hddSelectedMenuApprove" runat="server" />
                                                            <asp:HiddenField ID="hddSelectedMenuPrint" runat="server" />

                                                            <div class="row grdFRMContainer <%IIf(GetPermission().isEdit, "", "disabled") %>" style="display: none;">
                                                                <div class="col-lg-12 col-md-12 col-sm-12">

                                                                    <asp:GridView ID="grdView" runat="server" CssClass="table table-hover table-striped table-bordered table-responsive"
                                                                        EmptyDataText="No Items." EmptyDataRowStyle-CssClass="gridview-nodata" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" Style="width: 80%">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <table style="text-align: center; width: 2%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:CheckBox ID="chkAll" runat="server" CssClass="chkHeaderFRM" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkMenu" CssClass="chkItemFRM" runat="server" />
                                                                                    <asp:HiddenField ID="hddMenuID" Value='<%#Bind("menuID") %>' runat="server" />
                                                                                    <asp:HiddenField ID="hddParentMenuID" Value='<%#Bind("parentID") %>' runat="server" />
                                                                                    <%--<asp:HiddenField ID="hddModule" Value='<%#Bind("ModuleID") %>' runat="server" />--%>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Font-Bold="false" />
                                                                                <ItemStyle HorizontalAlign="left" Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("Menu", "Text")%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMenuName" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle />
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("SubMenu", "Text")%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSubMenuName" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle />
                                                                                <ItemStyle Wrap="false" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("resAdd", "Text")%>
                                                                                    <br />
                                                                                    <asp:CheckBox ID="chkAllAdd" runat="server" CssClass="chkHeaderFRMAdd" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="chkAdd" CssClass="chkItemFRM chkItemFRMAdd" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="center" />
                                                                                <ItemStyle HorizontalAlign="center" Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("resEdit", "Text")%>
                                                                                    <br />
                                                                                    <asp:CheckBox ID="chkAllEdit" runat="server" CssClass="chkHeaderFRMEdit" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="chkEdit" CssClass="chkItemFRM chkItemFRMEdit" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="center" />
                                                                                <ItemStyle HorizontalAlign="center" Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("resDelete", "Text")%>
                                                                                    <br />
                                                                                    <asp:CheckBox ID="chkAllDelete" runat="server" CssClass="chkHeaderFRMDelete" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="chkDelete" CssClass="chkItemFRM chkItemFRMDelete" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="center"  />
                                                                                <ItemStyle HorizontalAlign="center" Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("resApprove", "Text")%>
                                                                                    <br />
                                                                                    <asp:CheckBox ID="chkAllApprove" runat="server" CssClass="chkHeaderFRMApprove" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="chkApprove" CssClass="chkItemFRM chkItemFRMApprove" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="center"  />
                                                                                <ItemStyle HorizontalAlign="center" Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("resPrint", "Text")%>
                                                                                    <br />
                                                                                    <asp:CheckBox ID="chkAllPrint" runat="server" CssClass="chkHeaderFRMPrint" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="chkPrint" CssClass="chkItemFRM chkItemFRMPrint" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="center" />
                                                                                <ItemStyle HorizontalAlign="center" Width="2%" />
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:HiddenField ID="hddCheckAll" runat="server" />

                                                                </div>
                                                            </div>

                                                            <div class="row  grdRPTContainer <%IIf(GetPermission().isEdit, "", "disabled") %>" style="display: none;">
                                                                <div class="col-lg-12 col-md-12 col-sm-12">

                                                                    <asp:GridView ID="grdViewRPT" runat="server" CssClass="table table-hover table-striped table-bordered table-responsive"
                                                                        EmptyDataText="No Items." EmptyDataRowStyle-CssClass="gridview-nodata" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" Style="width: 80%">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <table style="text-align: center; width: 2%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:CheckBox ID="chkAll" runat="server" CssClass="chkHeaderRPT" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkMenu" runat="server" CssClass="chkItemRPT" />
                                                                                    <asp:HiddenField ID="hddMenuID" Value='<%#Bind("menuID") %>' runat="server" />
                                                                                    <asp:HiddenField ID="hddParentMenuID" Value='<%#Bind("parentID") %>' runat="server" />
                                                                                    <%--<asp:HiddenField ID="hddModule" Value='<%#Bind("ModuleID") %>' runat="server" />--%>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Font-Bold="false" />
                                                                                <ItemStyle HorizontalAlign="left" Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <%--<asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("ModuuleName", "Text", hddParameterMenuID.Value)%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSystemName" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle />
                                                                                <ItemStyle Wrap="false" />
                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("Menu", "Text")%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMenuName" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle />
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("SubMenu", "Text")%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSubMenuName" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle />
                                                                                <ItemStyle Wrap="false" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <%# GetResource("resPrint", "Text")%>
                                                                                    <br />
                                                                                    <asp:CheckBox ID="chkAllPrint" runat="server" CssClass="chkHeaderRPTPrint" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" ID="chkPrint" CssClass="chkItemRPT chkItemRPTPrint" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle />
                                                                                <ItemStyle HorizontalAlign="center" Width="2%" />
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:HiddenField ID="hddCheckAllRPT" runat="server" />

                                                                </div>
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
                <asp:Button ID="btnSave" runat="server" CssClass="hide" />
                <asp:Button ID="btnCancel" runat="server" CssClass="hide" />
                <asp:Button ID="btnReload" runat="server" CssClass="hide" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <%--   <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnReload" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnReload" EventName="Click" />
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
        function $$(id, context) {
            var el = $("#" + id, context);
            if (el.length < 1)
                el = $("[id$=_" + id + "]", context);
            return el;
        }

        $(document).ready(function () {
            SetInitial();
            RegisUpdatePanelLoaded();

        });

        function RegisUpdatePanelLoaded() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endUpdatePanelRequestHandler);
        }
        function endUpdatePanelRequestHandler(sender, args) {
            // Do your stuff
            checkMenuType();
            selectCurrentMenuAccess();
            console.log($$('hddSelectedMenu').val())
            console.log($$('hddSelectedMenuAdd').val())
            console.log($$('hddSelectedMenuEdit').val())
            console.log($$('hddSelectedMenuDelete').val())
            console.log($$('hddSelectedMenuApprove').val())
            console.log($$('hddSelectedMenuPrint').val())

            console.log('init change');

            $('input[data-value]').change(function () {
                console.log('change');
                $this = $(this);
                var menuid = $this.attr('data-value');
                var checked = $this.prop('checked');
                $('input[data-chkadd=' + menuid + ']').prop('checked', checked);
                $('input[data-chkedit=' + menuid + ']').prop('checked', checked);
                $('input[data-chkdelete=' + menuid + ']').prop('checked', checked);
                $('input[data-chkapprove=' + menuid + ']').prop('checked', checked);
                $('input[data-chkprint=' + menuid + ']').prop('checked', checked);
            });

            var headerChkFRM = $(".chkHeaderFRM input");
            var itemChkFRM = $(".chkItemFRM input");
            var headerChkFRMAdd = $(".chkHeaderFRMAdd input");
            var itemChkFRMAdd = $(".chkItemFRMAdd input");
            var headerChkFRMEdit = $(".chkHeaderFRMEdit input");
            var itemChkFRMEdit = $(".chkItemFRMEdit input");
            var headerChkFRMDelete = $(".chkHeaderFRMDelete input");
            var itemChkFRMDelete = $(".chkItemFRMDelete input");
            var headerChkFRMApprove = $(".chkHeaderFRMApprove input");
            var itemChkFRMApprove = $(".chkItemFRMApprove input");
            var headerChkFRMPrint = $(".chkHeaderFRMPrint input");
            var itemChkFRMPrint = $(".chkItemFRMPrint input");
            
            headerChkFRM.click(function () {
                itemChkFRM.prop('checked', this.checked);
                headerChkFRMAdd.prop('checked', this.checked);
                headerChkFRMEdit.prop('checked', this.checked);
                headerChkFRMDelete.prop('checked', this.checked);
                headerChkFRMApprove.prop('checked', this.checked);
                headerChkFRMPrint.prop('checked', this.checked);
            });

            itemChkFRM.click(function () {
                if (this.checked == false) { headerChkFRM[0].checked = false; }
            });
           
            headerChkFRMAdd.click(function () {
                itemChkFRMAdd.prop('checked', this.checked);
            });

            itemChkFRMAdd.click(function () {
                if (this.checked == false) { headerChkFRMAdd[0].checked = false; }
            });

            headerChkFRMEdit.click(function () {
                itemChkFRMEdit.prop('checked', this.checked);
            });

            itemChkFRMEdit.click(function () {
                if (this.checked == false) { headerChkFRMEdit[0].checked = false; }
            });

            headerChkFRMDelete.click(function () {
                itemChkFRMDelete.prop('checked', this.checked);
            });

            itemChkFRMDelete.click(function () {
                if (this.checked == false) { headerChkFRMDelete[0].checked = false; }
            });

            headerChkFRMApprove.click(function () {
                itemChkFRMApprove.prop('checked', this.checked);
            });

            itemChkFRMApprove.click(function () {
                if (this.checked == false) { headerChkFRMApprove[0].checked = false; }
            });

            headerChkFRMPrint.click(function () {
                itemChkFRMPrint.prop('checked', this.checked);
            });

            itemChkFRMPrint.click(function () {
                if (this.checked == false) { headerChkFRMPrint[0].checked = false; }
            });
            
            var headerChkRPT = $(".chkHeaderRPT input");
            var itemChkRPT = $(".chkItemRPT input");
            var headerChkRPTPrint = $(".chkHeaderRPTPrint input");
            var itemChkRPTPrint = $(".chkItemRPTPrint input");

            headerChkRPT.click(function () {
                itemChkRPT.prop('checked', this.checked);
                headerChkRPTPrint.prop('checked', this.checked);
            });

            itemChkRPT.click(function () {
                if (this.checked == false) { headerChkRPT[0].checked = false; }
            });

            headerChkRPTPrint.click(function () {
                itemChkRPTPrint.prop('checked', this.checked);
            });

            itemChkRPTPrint.click(function () {
                if (this.checked == false) { headerChkRPTPrint[0].checked = false; }
            });

            $("#overlay").modal('hide');

        }

        function SetInitial() {

            $$('ddlsModule').change(function () {
                CallReload();
                $('select').trigger('chosen:updated');
            });

            $('input[type=radio][name=menuType]').change(function () {
                checkMenuType()
            });


        }

        function checkMenuType() {
            if ($('input[type=radio][name=menuType]:checked').val() == 'FRM') {
                $('.grdFRMContainer').show('fade');
                $('.grdRPTContainer').hide();
            }
            else {
                $('.grdFRMContainer').hide();
                $('.grdRPTContainer').show('fade');
            }
        }

        //Auto Completed
        function AutocompletedUserID(txt, e) {
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
            $("#<%=txtsUserID.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("./API/Api_UserMenuAccess.asmx/GetUserID")%>',
                        data: "{ 'ptKeyID': '" + request.term + "', 'ptLG': '" + hddpLG.value + "'  }",
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
                    $("#<%=txtsUserID.ClientID%>").val(i.item.label);
                    if ($('#<%=ddlsModule.ClientID%>').val() != "") {
                        CallReload();
                    }
                },
                minLength: 0,
                matchContains: true
            }).on('click', function () { $(this).keydown(); });

        }

        function showOverlay() {
            $("#overlay").modal();
        }

        function CallReload() {
            if ($$('ddlsModule').val() != '') {
                checkMenuType();
                showOverlay();
                __doPostBack('<%= btnReload.UniqueID%>');
            } else {
                $('.grdFRMContainer').hide('fade');
                $('.grdRPTContainer').hide('fade');
            }

            return;
        }

        function CallCancel() {
            showOverlay();
            __doPostBack('<%= btnCancel.UniqueID%>');
            return;
        }

        function CallSave() {
            if ($('#<%=txtsUserID.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtsUserID.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                return;
            } else {
                $('#<%=txtsUserID.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }
            if ($('#<%=ddlsModule.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlsModule.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                return;
            } else {
                $('#<%=ddlsModule.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
            }


            $$('hddSelectedMenu').val(JSON.stringify(getSelectedMenuAccess()));
            $$('hddSelectedMenuAdd').val(JSON.stringify(getSelectedMenuAccessAdd()));
            $$('hddSelectedMenuEdit').val(JSON.stringify(getSelectedMenuAccessEdit()));
            $$('hddSelectedMenuDelete').val(JSON.stringify(getSelectedMenuAccessDelete()));
            $$('hddSelectedMenuApprove').val(JSON.stringify(getSelectedMenuAccessApprove()));
            $$('hddSelectedMenuPrint').val(JSON.stringify(getSelectedMenuAccessPrint()));

            showOverlay();

            __doPostBack('<%= btnSave.UniqueID%>');
            //return false;
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

        function changCheckAll() {
            var checked;
            var Count = document.getElementById("<%= hddCheckAll.ClientID %>");
            if (Count.value == "") {
                Count.value = "1";
            }
            else {
                Count.value = parseInt(Count.value) + 1;
            }
            if (isEven(Count.value)) {
                checked = false;
            } else if (isOdd(Count.value)) {
                checked = true;
            }
            var GridView = document.getElementById("<%= grdView.ClientID %>");
            for (var i = 0; i < GridView.rows.length; i++) {
                inputList = GridView.rows[i].getElementsByTagName("input");
                if (inputList.length != 0) {
                    for (var j = 0; j < inputList.length; j++) {
                        if (inputList[j] != undefined) {
                            if (inputList[j].type == "checkbox") {
                                inputList[j].checked = checked;
                            }
                        }
                    }
                }
            }
        }

        function isEven(n) {

            if (n % 2 == 0) {
                return true;
            } else {
                return false;
            }
        }

        function isOdd(n) {
            if (Math.abs(n % 2) == 1) {
                return true;
            } else {
                return false;
            }
        }

        function selectCurrentMenuAccess() {
            var lstSelectedMenu = $.parseJSON($$('hddSelectedMenu').val());
            var lstSelectedMenuAdd = $.parseJSON($$('hddSelectedMenuAdd').val());
            var lstSelectedMenuEdit = $.parseJSON($$('hddSelectedMenuEdit').val());
            var lstSelectedMenuDelete = $.parseJSON($$('hddSelectedMenuDelete').val());
            var lstSelectedMenuApprove = $.parseJSON($$('hddSelectedMenuApprove').val());
            var lstSelectedMenuPrint = $.parseJSON($$('hddSelectedMenuPrint').val());

            $('input[type=checkbox][data-value]').each(function () {
                var $chk = $(this);
                var menuID = $chk.attr('data-value');
                lstSelectedMenu.forEach(function (item) {
                    if (+menuID == +item) {
                        $chk.prop('checked', true);
                    }
                });

            });

            $('input[type=checkbox][data-chkadd]').each(function () {
                var $chk = $(this);
                var menuID = $chk.attr('data-chkadd');
                lstSelectedMenuAdd.forEach(function (item) {
                    if (+menuID == +item) {
                        $chk.prop('checked', true);
                    }
                });

            });

            $('input[type=checkbox][data-chkedit]').each(function () {
                var $chk = $(this);
                var menuID = $chk.attr('data-chkedit');
                lstSelectedMenuEdit.forEach(function (item) {
                    if (+menuID == +item) {
                        $chk.prop('checked', true);
                    }
                });

            });

            $('input[type=checkbox][data-chkdelete]').each(function () {
                var $chk = $(this);
                var menuID = $chk.attr('data-chkdelete');
                lstSelectedMenuDelete.forEach(function (item) {
                    if (+menuID == +item) {
                        $chk.prop('checked', true);
                    }
                });

            });

            $('input[type=checkbox][data-chkapprove]').each(function () {
                var $chk = $(this);
                var menuID = $chk.attr('data-chkapprove');
                lstSelectedMenuApprove.forEach(function (item) {
                    if (+menuID == +item) {
                        $chk.prop('checked', true);
                    }
                });

            });

            $('input[type=checkbox][data-chkprint]').each(function () {
                var $chk = $(this);
                var menuID = $chk.attr('data-chkprint');
                lstSelectedMenuPrint.forEach(function (item) {
                    if (+menuID == +item) {
                        $chk.prop('checked', true);
                    }
                });

            });

            //$$('hddSelectedMenu').val()

        }

        function getSelectedMenuAccess() {
            var arrSelectedMenu = []
            $('input[type=checkbox][data-value]:checked').each(function () {
                arrSelectedMenu.push($(this).attr('data-value'));
            });
            console.log(arrSelectedMenu);
            return arrSelectedMenu;
        }

        function getSelectedMenuAccessAdd() {
            var arrSelectedMenuAdd = []
            $('input[type=checkbox][data-chkadd]:checked').each(function () {
                arrSelectedMenuAdd.push($(this).attr('data-chkadd'));
            });
            console.log(arrSelectedMenuAdd);
            return arrSelectedMenuAdd;
        }

        function getSelectedMenuAccessEdit() {
            var arrSelectedMenuEdit = []
            $('input[type=checkbox][data-chkedit]:checked').each(function () {
                arrSelectedMenuEdit.push($(this).attr('data-chkedit'));
            });
            console.log(arrSelectedMenuEdit);
            return arrSelectedMenuEdit;
        }

        function getSelectedMenuAccessDelete() {
            var arrSelectedMenuDelete = []
            $('input[type=checkbox][data-chkdelete]:checked').each(function () {
                arrSelectedMenuDelete.push($(this).attr('data-chkdelete'));
            });
            console.log(arrSelectedMenuDelete);
            return arrSelectedMenuDelete;
        }

        function getSelectedMenuAccessApprove() {
            var arrSelectedMenuApprove = []
            $('input[type=checkbox][data-chkapprove]:checked').each(function () {
                arrSelectedMenuApprove.push($(this).attr('data-chkapprove'));
            });
            console.log(arrSelectedMenuApprove);
            return arrSelectedMenuApprove;
        }

        function getSelectedMenuAccessPrint() {
            var arrSelectedMenuPrint = []
            $('input[type=checkbox][data-chkprint]:checked').each(function () {
                arrSelectedMenuPrint.push($(this).attr('data-chkprint'));
            });
            console.log(arrSelectedMenuPrint);
            return arrSelectedMenuPrint;
        }
    </script>
</asp:Content>

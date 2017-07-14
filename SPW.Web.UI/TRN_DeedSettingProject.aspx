<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TRN_DeedSettingProject.aspx.vb" Inherits="SPW.Web.UI.TRN_DeedSettingProject" EnableEventValidation="false" %>

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

        .paddingZero{
            padding: 0.5px 0.5px;
        }

         .grdView td:nth-child(13),.grdView th:nth-child(13) {
           
           <%=IIF(Me.GetPermission().isDelete,"","display:none;" )%>
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
                <asp:HiddenField ID="hddpClientID" runat="server" />
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
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
                                                                        <a id="btnMSGSaveData" runat="server" href="javascript:CallSave();" class="btn glyph-icon icon-save btn-info tooltip-button" Visible='<%#Me.GetPermission().isEdit %>'></a>
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
                                                                            <asp:DropDownList ID="ddlsProject" AppendDataBoundItems="True" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblsDeedTotal" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtsDeedTotal" autocomplete="off" runat="server" CssClass="form-control" Enabled="true" BackColor="#F5F5F5" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row <%=IIf(Me.GetPermission.isEdit, "", "disabled") %>" style="overflow-x:auto;overflow-y:hidden;">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <%--<div style="width: 100%; height:380px;overflow: auto">--%>
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="grdView" runat="server" CssClass="table table-hover table-striped table-bordered table-responsive grdView"
                                                                            EmptyDataText="No Items." EmptyDataRowStyle-CssClass="gridview-nodata" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" style="width:100%">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFASSETNO", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgFASSETNO" autocomplete="off" BackColor="#ffe0c0" runat="server" CssClass="form-control paddingZero" Style="height: 28px; vertical-align: top;" MaxLength="10"></asp:TextBox>
                                                                                        <asp:HiddenField ID="hddID" runat="server" />
                                                                                        <asp:HiddenField ID="hddFlagSetAdd" runat="server" />
                                                                                        
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="left" Width="10%" Height="15px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gPCPIECE", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Textbox ID="txtgPCPIECE" autocomplete="off" runat="server" class="form-control paddingZero" Style="height: 28px; vertical-align: top;" Enabled="true" BackColor="#F5F5F5" ReadOnly="true"></asp:Textbox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="left" Width="6%" Height="15px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFQTY", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Textbox ID="txtgFQTY" autocomplete="off" runat="server" class="form-control text-right paddingZero" onKeyPress="return Check_Key_Decimal(this,event)" Style="height: 28px; vertical-align: top;" Enabled="true" BackColor="#F5F5F5" ReadOnly="true"></asp:Textbox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="left" Width="7%" Height="15px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFMORTGBK", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Textbox ID="txtgFMORTGBK" autocomplete="off" runat="server" class="form-control paddingZero" Style="height: 28px; vertical-align: top;" Enabled="true" BackColor="#F5F5F5" ReadOnly="true"></asp:Textbox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="left" Width="7%" Height="15px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFMAINCSTR", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgFMAINCSTR" autocomplete="off" runat="server" data-date-format="dd/mm/yyyy" MaxLength="10" Style="height: 28px; vertical-align: top;" onKeyPress="return Check_Key_Date(this,event)"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="center" Width="9%" Height="15px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFMAINCEND", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgFMAINCEND" autocomplete="off" runat="server" data-date-format="dd/mm/yyyy" MaxLength="10" Style="height: 28px; vertical-align: top;" onKeyPress="return Check_Key_Date(this,event)"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="center" Width="9%" Height="15px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFQTYADJPLUS", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 100%">
                                                                                            <tr>
                                                                                                <td style="width: 100%">
                                                                                                    <asp:TextBox ID="txtgFQTYADJPLUS1" autocomplete="off" runat="server" class="form-control text-right paddingZero" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return checkFormatSplit(this);" Style="height: 28px; vertical-align: top;"></asp:TextBox>
                                                                                                </td>
                                                                                                <%--<td>-
                                                                                                </td>
                                                                                                <td style="width: 33%">
                                                                                                    <asp:TextBox ID="txtgFQTYADJPLUS2" autocomplete="off" runat="server" class="form-control text-right paddingZero" onKeyPress="return Check_Key_Decimal3(this,event)" Style="height: 28px; vertical-align: top;" MaxLength="1"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>-
                                                                                                </td>
                                                                                                <td style="width: 33%">
                                                                                                    <asp:TextBox ID="txtgFQTYADJPLUS3" autocomplete="off" runat="server" class="form-control text-right paddingZero" onKeyPress="return Check_Key_Decimal(this,event)" Style="height: 28px; vertical-align: top;" MaxLength="2"></asp:TextBox>
                                                                                                </td>--%>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="center" Width="7%" Height="15px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFQTYADJNPLUS", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <table style="width: 100%">
                                                                                            <tr>
                                                                                                <td style="width: 100%">
                                                                                                    <asp:TextBox ID="txtgFQTYADJNPLUS1" autocomplete="off" runat="server" class="form-control text-right paddingZero" onKeyPress="return Check_Key_Decimal(this,event)" onBlur="return checkFormatSplit(this);" Style="height: 28px; vertical-align: top;"></asp:TextBox>
                                                                                                </td>
                                                                                                <%--<td>-
                                                                                                </td>
                                                                                                <td style="width: 33%">
                                                                                                    <asp:TextBox ID="txtgFQTYADJNPLUS2" autocomplete="off" runat="server" class="form-control text-right" onKeyPress="return Check_Key_Decimal3(this,event)" Style="height: 28px; vertical-align: top;" MaxLength="1"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>-
                                                                                                </td>
                                                                                                <td style="width: 33%">
                                                                                                    <asp:TextBox ID="txtgFQTYADJNPLUS3" autocomplete="off" runat="server" class="form-control text-right" onKeyPress="return Check_Key_Decimal(this,event)" Style="height: 28px; vertical-align: top;" MaxLength="2"></asp:TextBox>
                                                                                                </td>--%>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="center" Width="7%" Height="15px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFPCLNDNO", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Textbox ID="txtgFPCLNDNO" autocomplete="off" runat="server" class="form-control paddingZero" Style="height: 28px; vertical-align: top;" Enabled="true" BackColor="#F5F5F5" ReadOnly="true"></asp:Textbox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="center" Width="6%" Height="10px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFPCWIDTH", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Textbox ID="txtgFPCWIDTH" autocomplete="off" runat="server" class="form-control paddingZero" Style="height: 28px; vertical-align: top;" Enabled="true" BackColor="#F5F5F5" ReadOnly="true"></asp:Textbox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="center" Width="6%" Height="10px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFPCBETWEEN", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgFPCBETWEEN" autocomplete="off" runat="server" class="form-control paddingZero" Style="height: 28px; vertical-align: top;" Enabled="true" BackColor="#F5F5F5" ReadOnly="true"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="center" Width="11%" Height="10px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("gFPCNOTE", "Text", hddParameterMenuID.Value)%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtgFPCNOTE" autocomplete="off" runat="server" class="form-control paddingZero" Style="height: 28px; vertical-align: top;"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="center" Width="10%" Height="15px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <%# GetResource("col_delete", "Text", "1")%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="btnDelete" ImageUrl="~/image/delete.jpg" Style="width: 30px; height: 30px; margin-left: 10px;" ToolTip="Click to Delete" />
                                                                                        <asp:HiddenField ID="hddFlagSelect" runat="server" />
                                                                                        <asp:HiddenField ID="hddgPCPIECE" runat="server" />
                                                                                        <asp:HiddenField ID="hddgFQTY" runat="server" />
                                                                                        <asp:HiddenField ID="hddgFMORTGBK" runat="server" />
                                                                                        <asp:HiddenField ID="hddgFPCLNDNO" runat="server" />
                                                                                        <asp:HiddenField ID="hddgFPCWIDTH" runat="server" />
                                                                                        <asp:HiddenField ID="hddgFPCBETWEEN" runat="server" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" VerticalAlign="Middle" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <asp:Button ID="btnGridAdd" runat="server" CssClass="hide" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            <%--</div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>&nbsp;
                                                                    </td>
                                                                    <td style="text-align: right;">
                                                                        <a id="btnMSGSaveData1" runat="server" href="javascript:CallSave();" class="btn glyph-icon icon-save btn-info tooltip-button" Visible='<%#Me.GetPermission().isEdit %>'></a>
                                                                        <a id="btnMSGCancelData1" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
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
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnReloadGrid" />
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
            var hddMasterLG = document.getElementById("<%= hddMasterLG.ClientID%>");
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
            }
            CalQTY();
        }

      
        function AutocompletedDeed(txtgFASSETNO,
                                   txtgPCPIECE,
                                   txtgFQTY,
                                   txtgFMORTGBK,
                                   txtgFMAINCSTR,
                                   txtgFMAINCEND,
                                   txtgFQTYADJPLUS1,
                                   txtgFQTYADJNPLUS1,
                                   txtgFPCLNDNO,
                                   txtgFPCWIDTH,
                                   txtgFPCBETWEEN,
                                   txtgFPCNOTE,
                                   IDs,
                                   hddFlagSelect,
                                   hddgPCPIECE,
                                   hddgFQTY,
                                   hddgFMORTGBK,
                                   hddgFPCLNDNO,
                                   hddgFPCWIDTH,
                                   hddgFPCBETWEEN,
                                   e) {
            var txtgFASSETNO = document.getElementById(txtgFASSETNO);
            var txtgPCPIECE = document.getElementById(txtgPCPIECE);
            var txtgFQTY = document.getElementById(txtgFQTY);
            var txtgFMORTGBK = document.getElementById(txtgFMORTGBK);
            var txtgFMAINCSTR = document.getElementById(txtgFMAINCSTR);
            var txtgFMAINCEND = document.getElementById(txtgFMAINCEND);
            var txtgFQTYADJPLUS1 = document.getElementById(txtgFQTYADJPLUS1);
            //var txtgFQTYADJPLUS2 = document.getElementById(txtgFQTYADJPLUS2);
            //var txtgFQTYADJPLUS3 = document.getElementById(txtgFQTYADJPLUS3);
            var txtgFQTYADJNPLUS1 = document.getElementById(txtgFQTYADJNPLUS1);
            //var txtgFQTYADJNPLUS2 = document.getElementById(txtgFQTYADJNPLUS2);
            //var txtgFQTYADJNPLUS3 = document.getElementById(txtgFQTYADJNPLUS3);
            var txtgFPCLNDNO = document.getElementById(txtgFPCLNDNO);
            var txtgFPCWIDTH = document.getElementById(txtgFPCWIDTH);
            var txtgFPCBETWEEN = document.getElementById(txtgFPCBETWEEN);
            var txtgFPCNOTE = document.getElementById(txtgFPCNOTE);
            var hddFlagSelect = document.getElementById(hddFlagSelect);
            var hddgPCPIECE = document.getElementById(hddgPCPIECE);
            var hddgFQTY = document.getElementById(hddgFQTY);
            var hddgFMORTGBK = document.getElementById(hddgFMORTGBK);
            var hddgFPCLNDNO = document.getElementById(hddgFPCLNDNO);
            var hddgFPCWIDTH = document.getElementById(hddgFPCWIDTH);
            var hddgFPCBETWEEN = document.getElementById(hddgFPCBETWEEN);
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
            
            $(txtgFASSETNO).autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("./API/Api_DeedSettingProject.asmx/GetDeed")%>',
                        data: "{ 'ptKeyID': '" + request.term + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('|')[0],
                                    val: item
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
                    hddFlagSelect.value = "1";
                    $(txtgFASSETNO).val(i.item.val.split('|')[0]);
                    $(txtgPCPIECE).val(i.item.val.split('|')[1]);
                    $(hddgPCPIECE).val(i.item.val.split('|')[1]);
                    $(txtgFQTY).val(i.item.val.split('|')[2]);
                    $(hddgFQTY).val(i.item.val.split('|')[2]);
                    $(txtgFMORTGBK).val(i.item.val.split('|')[3]);
                    $(hddgFMORTGBK).val(i.item.val.split('|')[3]);

                    //date
                    FMAINCSTR = i.item.val.split("|")[4];
                    FMAINCEND = i.item.val.split('|')[5]
                    if ($('[id$=hddMasterLG]').val() == "TH") {
                        FMAINCSTR = toThDate(FMAINCSTR);
                        FMAINCEND = toThDate(FMAINCEND);
                    }
                    $(txtgFMAINCSTR).val(FMAINCSTR);
                    $(txtgFMAINCEND).val(FMAINCEND);
                    


                    $(txtgFQTYADJPLUS1).val(i.item.val.split('|')[6] + '-' + i.item.val.split('|')[7] + '-' + i.item.val.split('|')[8]);
                    //$(txtgFQTYADJPLUS2).val(i.item.val.split('|')[7]);
                    //$(txtgFQTYADJPLUS3).val(i.item.val.split('|')[8]);
                    $(txtgFQTYADJNPLUS1).val(i.item.val.split('|')[9] + '-' + i.item.val.split('|')[10] + '-' + i.item.val.split('|')[11]);
                    //$(txtgFQTYADJNPLUS2).val(i.item.val.split('|')[10]);
                    //$(txtgFQTYADJNPLUS3).val(i.item.val.split('|')[11]);

                    $(txtgFPCLNDNO).val(i.item.val.split('|')[12]);
                    $(hddgFPCLNDNO).val(i.item.val.split('|')[12]);
                    $(txtgFPCWIDTH).val(i.item.val.split('|')[13]);
                    $(hddgFPCWIDTH).val(i.item.val.split('|')[13]);
                    $(txtgFPCBETWEEN).val(i.item.val.split('|')[14]);
                    $(hddgFPCBETWEEN).val(i.item.val.split('|')[14]);
                    $(txtgFPCNOTE).val(i.item.val.split('|')[15]);
                    CalQTY();
                    var chkBool = true;
                    var pDup;
                    var grid = document.getElementById("<%= grdView.ClientID %>");
                    if (grid != null) {
                        var count = grid.rows.length;
                        if (count > 2) {
                            for (var i = 0; i < grid.rows.length; i++) {
                                inputs = grid.rows[i].getElementsByTagName("input");
                                for (var j = 0; j < inputs.length; j++) {
                                    if (inputs[j].type == "text") {
                                        if (j == "0") {
                                            if (inputs[1].value != IDs) {
                                                if (txtgFASSETNO.value == inputs[j].value) {
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
                        hddFlagSelect.value = "";
                        txtgFASSETNO.value = "";
                        txtgPCPIECE.value = "";
                        txtgFQTY.value = "";
                        txtgFMORTGBK.value = "";
                        txtgFMAINCSTR.value = "";
                        txtgFMAINCEND.value = "";
                        txtgFQTYADJPLUS1.value = "";
                        //txtgFQTYADJPLUS2.value = "";
                        //txtgFQTYADJPLUS3.value = "";
                        txtgFQTYADJNPLUS1.value = "";
                        //txtgFQTYADJNPLUS2.value = "";
                        //txtgFQTYADJNPLUS3.value = "";
                        txtgFPCLNDNO.value = "";
                        txtgFPCWIDTH.value = "";
                        txtgFPCBETWEEN.value = "";
                        txtgFPCNOTE.value = "";
                        OpenDialogError("<%# Me.GetResource("msg_duplicate_table", "MSG") %>" + pDup);
                        return false;
                    }

                },
                minLength: 0,
                matchContains: true
            }).on('click', function () { $(this).keydown(); });

        }

        function CalQTY() {
            var txtsDeedTotal = document.getElementById("<%= txtsDeedTotal.ClientID%>");
            
            var TempFQTY = "";
            var TempFQTY1 = "0";
            var TempFQTY2 = "0";
            var TempFQTY3 = "0";
            var TempFQTY4 = "0";
            var TotalVa = "";
            var TempTotalPrice = "0";
            var grid = document.getElementById("<%= grdView.ClientID %>");
            if (grid != null) {
                var count = grid.rows.length;
                if (count > 2) {
                    for (var i = 0; i < grid.rows.length; i++) {
                        inputs = grid.rows[i].getElementsByTagName("input");
                        for (var j = 0; j < inputs.length; j++) {
                            if (inputs[j].type == "text") {
                                if (j == "4") {
                                    if (inputs[j].value != "")
                                    {
                                        TepFQTY = inputs[j].value
                                        TempFQTY1 = TepFQTY.split('-')[0];
                                        TempFQTY2 = TepFQTY.split('-')[1];
                                        TempFQTY3 = TepFQTY.split('-')[2];
                                        if (TempFQTY1 != "") {
                                            TempFQTY1 = parseInt(TempFQTY1) * 400;
                                        }
                                        if (TempFQTY2 != "") {
                                            TempFQTY2 = parseInt(TempFQTY2) * 100;
                                        }
                                        if (TempFQTY3 != "") {
                                            TempFQTY3 = parseInt(TempFQTY3);
                                        }
                                        if (TotalVa == "") {
                                            TotalVa = parseInt(TempFQTY1) + parseInt(TempFQTY2) + parseInt(TempFQTY3);
                                        } else {
                                            TotalVa = parseInt(TotalVa) + parseInt(TempFQTY1) + parseInt(TempFQTY2) + parseInt(TempFQTY3);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (TotalVa != "") {
                TempFQTY1 = parseInt(TotalVa) / 400;
                try {
                    var TempSplit1 = TempFQTY1.toString();
                    TempFQTY1 = TempSplit1.split('.')[0];
                    TempTotalPrice = TempFQTY1;
                    TempFQTY1 = parseInt(TempFQTY1) * 400;
                }catch(ex){
                    TempTotalPrice = TempFQTY1;
                    TempFQTY1 = parseInt(TempFQTY1) * 400;
                }
                TempFQTY2 = parseInt(TotalVa) - parseInt(TempFQTY1);
                TempFQTY3 = parseInt(TempFQTY2) / 100;
                try {
                    var TempSplit2 = TempFQTY3.toString();
                    TempFQTY3 = TempSplit2.split('.')[0];
                    TempTotalPrice = TempTotalPrice + "-" + TempFQTY3;
                    TempFQTY3 = parseInt(TempFQTY3) * 100;
                } catch (ex) {
                    TempTotalPrice = TempTotalPrice & "-" & TempFQTY3;
                    TempFQTY3 = parseInt(TempFQTY3) * 100;
                }
                TempFQTY4 = parseInt(TempFQTY2) - parseInt(TempFQTY3);
                TempTotalPrice = TempTotalPrice + "-" + TempFQTY4;
                if (TempTotalPrice != "NaN-NaN-NaN") {
                    txtsDeedTotal.value = TempTotalPrice;
                } else {
                    txtsDeedTotal.value = "";
                }
            }

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

         function checkFormatSplit(txt) {
             var txt = document.getElementById(txt.id);
             var Temp1 = "0";
             var Temp2 = "0";
             var Temp3 = "0";
             var arr = txt.value.split("-");
             if (txt.value != "") {
                 Temp1 = txt.value.split("-")[0];
                 if (Temp1 != ""){
                     Temp1 = fnFormatMoney0(parseFloat(replaceAll(Temp1, ',', '')));
                 }
                 try {
                     Temp2 = txt.value.split("-")[1];
                     if (Temp2 != undefined && Temp2 != "") {
                         if (parseInt(Temp2) > 3) {
                             Temp2 = "3";
                         }
                     } else {
                         Temp2 = "0";
                     }
                 } catch (ex) {
                     Temp2 = "0";
                 }
                 if (Temp2 != "") {
                     Temp2 = fnFormatMoney0(parseFloat(replaceAll(Temp2, ',', '')));
                 }
                 try {
                     Temp3 = txt.value.split("-")[2];
                     if (Temp3 != undefined && Temp3 != "") {
                         if (parseInt(Temp3) > 99) {
                             Temp3 = "99";
                         }
                     } else {
                         Temp3 = "0";
                     }
                 } catch (ex) {
                     Temp3 = "0";
                 }
                 if (Temp3 != "") {
                     Temp3 = fnFormatMoney0(parseFloat(replaceAll(Temp3, ',', '')));
                 }
             }
             txt.value = Temp1 + "-" + Temp2 + "-" + Temp3;

             var pRow = txt.id.substring(txt.id.length - 1);
             var pType;
             if (txt.id.indexOf("txtgFQTYADJPLUS1") > 0) {
                 pType = "1";
             } else {
                 pType = "2";
             }
             var grid = document.getElementById("<%= grdView.ClientID %>");
             if (grid != null) {
                var count = grid.rows.length;
                if (count > 2) {
                    for (var i = 0; i < grid.rows.length; i++) {
                        inputs = grid.rows[i].getElementsByTagName("input");
                        if (i == parseInt(pRow) + 1) {
                            for (var j = 0; j < inputs.length; j++) {
                                if (inputs[j].type == "text") {
                                    if (pType == "1") {
                                        if (j == "9") {
                                            inputs[j].value = "0-0-0";
                                        }
                                    } else if (pType == "2") {
                                        if (j == "8") {
                                            inputs[j].value = "0-0-0";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
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

         function Check_Key_Decimal3(txtMoney, e)//check key number&dot only and decimal 4 digit
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
                 if (key > 48 && key < 52) {
                     return true;
                 } else {
                     return false;
                 }
             }
             else {
                 return false;
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
         function toThDate(sDate) {
             if (sDate == '') {
                 return '';
             } else {
                 return sDate.substr(0, 6) + (+(sDate.substr(sDate.length - 4)) + 543);

             }
         }
    </script>
</asp:Content>

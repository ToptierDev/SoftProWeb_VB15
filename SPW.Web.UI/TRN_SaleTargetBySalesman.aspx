<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TRN_SaleTargetBySalesman.aspx.vb" Inherits="SPW.Web.UI.TRN_SaleTargetBySalesman" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modal-dialog {
            position: fixed;
            top: 35%;
            left: 40%;
        }
        .Text-Right {
            text-align:right;
        }
        .trclick tr.selected > td:nth-child(1),
        .trclick tr.selected > td:nth-child(2),
        .trclick tr.selected > td:nth-child(3){
            background-color: #c7c7fa;
        }

        .trclick tr.selected > td:nth-child(1) input,
        .trclick tr.selected > td:nth-child(2) input,
        .trclick tr.selected > td:nth-child(3) input{
            border-color:#2632ef !important;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <asp:HiddenField ID="hddParameterMenuID" runat="server" />
                <asp:HiddenField ID="hddKeyID" runat="server" />
                <asp:HiddenField ID="hddaTotalRoom" runat="server" />
                <asp:HiddenField ID="hddaTotalCash" runat="server" />
                <asp:HiddenField ID="hddsProject" runat="server" />
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
                                <asp:Panel ID="pnMain" runat="server">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12">
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
                                                                        <asp:Button ID="btnSaveTemp" runat="server" OnClientClick="javascript:CallSave();return false" CssClass="hide"/>
                                                                        <a href="javascript:CallSave();" class="btn glyph-icon icon-save btn-info <%=IIf(Me.GetPermission().isEdit, "", "hide") %>" title="Save Data">
                                                                        </a>
                                                                        <a href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger" title="Cancel Data">
                                                                         </a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-lg-12">
                                                        <div class="row">
                                                            <div class="col-lg-9 col-md-12 col-sm-12">
                                                                <div class="row">
                                                                    <div class="col-lg-1 col-md-1 col-sm-1">
                                                                        &nbsp;
                                                                    </div>
                                                                    <div class="col-lg-1 col-md-1 col-sm-1">
                                                                        <asp:Label ID="lblsYear" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-3 col-sm-3">
                                                                        <asp:Textbox autocomplete="off" ID="txtsYear" runat="server" CssClass="form-control text-center" style="width:70%" onKeyPress="return Check_Key_Decimal(this,event)" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-2 col-md-1 col-sm-1">
                                                                        <asp:Label ID="lblsProject" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-6 col-md-7 col-sm-7">                                                                        
                                                                        <asp:DropDownList ID="ddlsProject" AppendDataBoundItems="True" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                        <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-lg-4 col-md-5 col-sm-5">
                                                                        &nbsp;
                                                                    </div>
                                                                     <div class="col-lg-2 col-md-1 col-sm-1">
                                                                        <asp:Label ID="lblsSalesman" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-6 col-md-7 col-sm-7">
                                                                        <asp:DropDownList ID="ddlsSaleMan" AppendDataBoundItems="True" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                        <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: center;">
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
                                                        <hr style="width: 100%; border: 0.2px dashed; border-color: #cccccc" />
                                                         
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Label ID="lblaPeriod" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Label ID="lblaTotalRoom" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Label ID="lblaTotalCash" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div class="<%=IIf(Me.GetPermission.isEdit, "", "disabled") %> trclick">
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod1" runat="server" BorderStyle="None" Enabled="false" Text="1" Style="text-align: center; width: 10px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom1" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash1" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Label ID="lblaTotalQTY" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width:30%;text-align:center;">
                                                                     <asp:Textbox autocomplete="off" ID="txtaTotalRoom" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" Enabled="false" BackColor="#F5F5F5"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod2" runat="server" BorderStyle="None" Enabled="false" Text="2" Style="text-align: center; width: 10px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom2" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash2" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Label ID="lblaTotalAmount" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width:30%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" Enabled="false" BackColor="#F5F5F5"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod3" runat="server" BorderStyle="None" Enabled="false" Text="3" Style="text-align: center; width: 10px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom3" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash3" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod4" runat="server" BorderStyle="None" Enabled="false" Text="4" Style="text-align: center; width: 10px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom4" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash4" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod5" runat="server" BorderStyle="None" Enabled="false" Text="5" Style="text-align: center; width: 10px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom5" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash5" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod6" runat="server" BorderStyle="None" Enabled="false" Text="6" Style="text-align: center; width: 10px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom6" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash6" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod7" runat="server" BorderStyle="None" Enabled="false" Text="7" Style="text-align: center; width: 10px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom7" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash7" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod8" runat="server" BorderStyle="None" Enabled="false" Text="8" Style="text-align: center; width: 10px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom8" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash8" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod9" runat="server" BorderStyle="None" Enabled="false" Text="9" Style="text-align: center; width: 10px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom9" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash9" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod10" runat="server" BorderStyle="None" Enabled="false" Text="10" Style="text-align: center; width: 16px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom10" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash10" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod11" runat="server" BorderStyle="None" Enabled="false" Text="11" Style="text-align: center; width: 16px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom11" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash11" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td style="width:10%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaPeriod12" runat="server" BorderStyle="None" Enabled="false" Text="12" Style="text-align: center; width: 16px;background-color:transparent;"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalRoom12" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key(this,event);" onblur="return fnCalTotal();" onkeyup="callCheck15digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:20%;text-align:center;">
                                                                    <asp:Textbox autocomplete="off" ID="txtaTotalCash12" runat="server" CssClass="form-control Text-Right" style="Width:99%" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return fnCalTotal();" onkeyup="callCheck18digit(this,event);"></asp:TextBox>
                                                                </td>
                                                                <td style="width:50%;text-align:center;">
                                                                    &nbsp;
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
            </asp:Panel>
            <asp:Button ID="btnSave" runat="server" CssClass="hide" />
            <asp:Button ID="btnCancel" runat="server" CssClass="hide" />
            <asp:Button ID="btnReload" runat="server" CssClass="hide" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlsSaleMan" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnReload" />
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
            RegisUpdatePanelLoaded();
        });

        function RegisUpdatePanelLoaded() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endUpdatePanelRequestHandler);
        }
        function endUpdatePanelRequestHandler(sender, args) {
            SetInitial();
        }

        function SetInitial() {
            $("tbody tr").click(function () {
                $('.trclick tr').removeClass("selected");
                $(this).addClass("selected");
            });
        }

        function showOverlay() {
            $("#overlay").modal();
        }

        function CallSave() {
            if ($('#<%=txtsYear.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtsYear.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                return;
            } else {
                $('#<%=txtsYear.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }

            if ($('#<%=ddlsProject.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlsProject.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                return;
            } else {
                $('#<%=ddlsProject.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
            }

            if ($('#<%=ddlsSaleMan.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlsSaleMan.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
                return;
            } else {
                $('#<%=ddlsSaleMan.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";
            }

            showOverlay();

            __doPostBack('<%= btnSave.UniqueID%>');
        }

        

        

        function CallReload() {
            showOverlay();
            __doPostBack('<%= btnReload.UniqueID%>');
        }

        function CallCancel() {
            showOverlay();
            __doPostBack('<%= btnCancel.UniqueID%>');
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

        function setNextFocus(event, down, up, right, left) {
            var KeyID = event.keyCode;
            if (KeyID == 13 || KeyID == 40) {//down
                if (down != "") {
                    down = document.getElementById(down);
                    down.focus();
                    down.value = "";
                }
            } else if (KeyID == 38) {//up
                if (up != "") {
                    up = document.getElementById(up);
                    up.focus();
                    up.value = "";
                }
            } else if (KeyID == 39) {//right
                if (right != "") {
                    right = document.getElementById(right);
                    right.focus();
                    right.value = "";
                }
            } else if (KeyID == 37) {//left
                if (left != "") {
                    left = document.getElementById(left);
                    left.focus();
                    left.value = "";
                }
            }
        }

        function fnCalTotal() {
           
            var txtaTotalRoom1 = document.getElementById("<%= txtaTotalRoom1.ClientID%>");
            var txtaTotalCash1 = document.getElementById("<%= txtaTotalCash1.ClientID%>");
            
            var txtaTotalRoom2 = document.getElementById("<%= txtaTotalRoom2.ClientID%>");
            var txtaTotalCash2 = document.getElementById("<%= txtaTotalCash2.ClientID%>");
            
            var txtaTotalRoom3 = document.getElementById("<%= txtaTotalRoom3.ClientID%>");
            var txtaTotalCash3 = document.getElementById("<%= txtaTotalCash3.ClientID%>");
            
            var txtaTotalRoom4 = document.getElementById("<%= txtaTotalRoom4.ClientID%>");
            var txtaTotalCash4 = document.getElementById("<%= txtaTotalCash4.ClientID%>");
            
            var txtaTotalRoom5 = document.getElementById("<%= txtaTotalRoom5.ClientID%>");
            var txtaTotalCash5 = document.getElementById("<%= txtaTotalCash5.ClientID%>");
            
            var txtaTotalRoom6 = document.getElementById("<%= txtaTotalRoom6.ClientID%>");
            var txtaTotalCash6 = document.getElementById("<%= txtaTotalCash6.ClientID%>");
            
            var txtaTotalRoom7 = document.getElementById("<%= txtaTotalRoom7.ClientID%>");
            var txtaTotalCash7 = document.getElementById("<%= txtaTotalCash7.ClientID%>");
            
            var txtaTotalRoom8 = document.getElementById("<%= txtaTotalRoom8.ClientID%>");
            var txtaTotalCash8 = document.getElementById("<%= txtaTotalCash8.ClientID%>");
            
            var txtaTotalRoom9 = document.getElementById("<%= txtaTotalRoom9.ClientID%>");
            var txtaTotalCash9 = document.getElementById("<%= txtaTotalCash9.ClientID%>");
            
            var txtaTotalRoom10 = document.getElementById("<%= txtaTotalRoom10.ClientID%>");
            var txtaTotalCash10 = document.getElementById("<%= txtaTotalCash10.ClientID%>");
            
            var txtaTotalRoom11 = document.getElementById("<%= txtaTotalRoom11.ClientID%>");
            var txtaTotalCash11 = document.getElementById("<%= txtaTotalCash11.ClientID%>");
            
            var txtaTotalRoom12 = document.getElementById("<%= txtaTotalRoom12.ClientID%>");
            var txtaTotalCash12 = document.getElementById("<%= txtaTotalCash12.ClientID%>");

            var txtaTotalRoom = document.getElementById("<%= txtaTotalRoom.ClientID%>");
            var txtaTotalCash = document.getElementById("<%= txtaTotalCash.ClientID%>");

            var hddaTotalRoom = document.getElementById("<%= hddaTotalRoom.ClientID%>");
            var hddaTotalCash = document.getElementById("<%= hddaTotalCash.ClientID%>");

            var strTotalRoom = 0;
            var strTotalCash = 0;
            
            if (txtaTotalRoom1.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom1.value))
                
                txtaTotalRoom1.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom1.value, ',', '')));
            }
            if (txtaTotalCash1.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash1.value))
                txtaTotalCash1.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash1.value, ',', '')));
            }

            if (txtaTotalRoom2.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom2.value))
               
                txtaTotalRoom2.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom2.value, ',', '')));
            }
            if (txtaTotalCash2.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash2.value))
                txtaTotalCash2.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash2.value, ',', '')));
            }

            if (txtaTotalRoom3.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom3.value))
                
                txtaTotalRoom3.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom3.value, ',', '')));
            }
            if (txtaTotalCash3.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash3.value))
                txtaTotalCash3.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash3.value, ',', '')));
            }

            if (txtaTotalRoom4.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom4.value))
                
                txtaTotalRoom4.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom4.value, ',', '')));
            }
            if (txtaTotalCash4.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash4.value))
                txtaTotalCash4.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash4.value, ',', '')));
            }

            if (txtaTotalRoom5.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom5.value))
                
                txtaTotalRoom5.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom5.value, ',', '')));
            }
            if (txtaTotalCash5.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash5.value))
                txtaTotalCash5.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash5.value, ',', '')));
            }

            if (txtaTotalRoom6.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom6.value))
                
                txtaTotalRoom6.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom6.value, ',', '')));
            }
            if (txtaTotalCash6.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash6.value))
                txtaTotalCash6.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash6.value, ',', '')));
            }

            if (txtaTotalRoom7.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom7.value))
                
                txtaTotalRoom7.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom7.value, ',', '')));
            }
            if (txtaTotalCash7.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash7.value))
                txtaTotalCash7.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash7.value, ',', '')));
            }

            if (txtaTotalRoom8.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom8.value))
               
                txtaTotalRoom8.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom8.value, ',', '')));
            }
            if (txtaTotalCash8.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash8.value))
                txtaTotalCash8.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash8.value, ',', '')));
            }

            if (txtaTotalRoom9.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom9.value))
               
                txtaTotalRoom9.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom9.value, ',', '')));
            }
            if (txtaTotalCash9.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash9.value))
                txtaTotalCash9.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash9.value, ',', '')));
            }

            if (txtaTotalRoom10.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom10.value))
                
                txtaTotalRoom10.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom10.value, ',', '')));
            }
            if (txtaTotalCash10.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash10.value))
                txtaTotalCash10.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash10.value, ',', '')));
            }

            if (txtaTotalRoom11.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom11.value))
                
                txtaTotalRoom11.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom11.value, ',', '')));
            }
            if (txtaTotalCash11.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash11.value))
                txtaTotalCash11.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash11.value, ',', '')));
            }

            if (txtaTotalRoom12.value != "") {
                strTotalRoom = parseFloat(strTotalRoom) + parseFloat(numberWithCommasValue(txtaTotalRoom12.value))
                
                txtaTotalRoom12.value = fnFormatZero(parseFloat(replaceAll(txtaTotalRoom12.value, ',', '')));
            }
            if (txtaTotalCash12.value != "") {
                strTotalCash = parseFloat(strTotalCash) + parseFloat(numberWithCommasValue(txtaTotalCash12.value))
                txtaTotalCash12.value = fnFormatMoney(parseFloat(replaceAll(txtaTotalCash12.value, ',', '')));
            }

            if (strTotalRoom != "") {
                txtaTotalRoom.value = fnFormatZero(parseFloat(strTotalRoom));
                hddaTotalRoom.value = fnFormatZero(parseFloat(strTotalRoom));
            } else {
                txtaTotalRoom.value = "";
                hddaTotalRoom.value = "";
            }
            if (strTotalCash != "") {
                txtaTotalCash.value = fnFormatMoney(parseFloat(strTotalCash));
                hddaTotalCash.value = fnFormatMoney(parseFloat(strTotalCash));
            } else {
                txtaTotalCash.value = "";
                hddaTotalCash.value = "";
            }

        }

        function setFormat(txt, e) {
            var txt = document.getElementById(txt.id);
            if (txt.value == "") {
                txt.value = 0;
            }
            txt.value = fnFormatMoney(parseFloat(replaceAll(txt.value, ',', '')));

            if (txt.value == "0") {
                txt.value = "";
            }
        }

        function replaceAll(str, find, replace) {
            if (str != "" && str != undefined) {
                return str.replace(new RegExp(find, 'g'), replace);
            } else {
                return "";
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

        function fnFormatZero(values) {
            return values.toFixed(0).replace(/./g, function (c, i, a) { return i && c !== "." && ((a.length - i) % 3 === 0) ? ',' + c : c; });
        }

        function fnFormatMoney(values) {
            return values.toFixed(2).replace(/./g, function (c, i, a) { return i && c !== "." && ((a.length - i) % 3 === 0) ? ',' + c : c;});
        }

        function numberWithCommasValue(valWithComma) {
            return valWithComma.toString().replace(/,/g, '');
        }

        function numberWithCommas(x) {
            if (x) {
                x = numberWithCommasValue(x);
                return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            }
            return '0';
        }

        function Check_Key(txt, e) {
            var txt = document.getElementById(txt.id);
            if (txt != "") {
                if (replaceAll(txt.value, ",", "").length >= 15) {
                    return false;
                }
            }
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

        function Check_Key_Decimal(txt, e)
        {
            var txt = document.getElementById(txt.id);
            if (txt.value != "") {
                var array1 = txt.value.split(".")[0];
                var array2 = txt.value.split(".")[1];
                if (array1 != "" && array2 == undefined) {
                    array1 = replaceAll(array1, ",", "").length;
                    if (array1 >= 15) {
                        return false;
                    }
                } else if (array1 != "" && (array2 != "" && array2 != undefined)) {
                    var chkLength = replaceAll(array1, ",", "").length + replaceAll(array2, ",", "").length;
                    if (chkLength >= 18) {
                        return false;
                    }
                }
            }
            var key;
            if (document.all) {
                key = window.event.keyCode;
            }
            else {
                key = e.which;
            }
            if (key == 13) {
                return false;
            }
            if (key == 47 ||
                key == 91 ||
                key == 38 ||
                key == 92 ||
                key == 35 ||
                key == 43 ||
                key == 40 ||
                key == 41 ||
                key == 36 ||
                key == 126 ||
                key == 37 ||
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
            }
            if (key != 46) {
                if (key < 48 || key > 57) {
                    if (key == 0 || key == 8) {
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    if (String(txt.value).split(".").length > 1) {
                        if (String(txt.value).split(".")[1].length > 3) {
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
                if (txt.value.indexOf(".") > -1) {
                    return false;
                } else {
                    return true;
                }
            }
        }

        function callCheck15digit(txt, e) {
            var txt = document.getElementById(txt.id);
            if (txt.value.length > 15) {
                txt.value = txt.value.substring(0, 15);
            }
        }

        function callCheck18digit(txt, e) {
            var txt = document.getElementById(txt.id);
            if (txt.value != "") {
                var array1 = txt.value.split(".")[0];
                var array2 = txt.value.split(".")[1];
                if (array1 != "" && array2 == undefined) {
                    array1 = replaceAll(array1, ",", "").length;
                    if (array1 > 15) {
                        txt.value = txt.value.substring(0, 15);
                    }
                } else if (array1 != "" && (array2 != "" && array2 != undefined)) {
                    var chkLength = replaceAll(array1, ",", "").length + replaceAll(array2, ",", "").length;
                    if (chkLength > 18) {
                        txt.value = txt.value.substring(0, 18);
                    }
                }
            }
        }
    </script>
</asp:Content>

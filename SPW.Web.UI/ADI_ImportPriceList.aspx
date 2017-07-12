<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ADI_ImportPriceList.aspx.vb" Inherits="SPW.Web.UI.ADI_ImportPriceList"
    EnableEventValidation="false" %>

<%@ Import Namespace="SPW.Helper" %>
<%@ Import Namespace="SPW.BLL" %>
<%@ Import Namespace="SPW.DAL" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modal-dialog {
            position: fixed;
            top: 35%;
            left: 40%;
        }

        .imgAttachedFile {
            cursor: pointer;
        }

        .fileUpload {
            position: relative;
            overflow: hidden;
            margin: 0;
        }

            .fileUpload input.upload {
                position: absolute;
                top: 0;
                right: 0;
                margin: 0;
                padding: 0;
                font-size: 20px;
                cursor: pointer;
                opacity: 0;
                filter: alpha(opacity=0);
            }

        .normalfont {
            font-weight: normal !important;
        }

        .rbdchk {
            float: left;
            min-width: 200px;
            /* min-height: 30px;
            white-space: nowrap;
            font-size: 9pt;*/
            padding-left: 2px;
        }

            .rbdchk input[type=radio] + label {
                white-space: nowrap;
                padding: 8px;
            }

            .rbdchk tr td:first-child label {
                border-radius: 5px 0px 0px 0px;
            }

            .rbdchk tr td:last-child label {
                border-radius: 0px 5px 0px 0px;
            }
        /*
          Hide radio button (the round disc)
          we will use just the label to create pushbutton effect
        */
        input[type=radio] {
            display: none;
            margin: 5px;
            min-width: 200px;
            white-space: nowrap; /*font-size: 9pt;*/
            font-size: 9pt;
        }

            /*
          Change the look'n'feel of labels (which are adjacent to radiobuttons).
          Add some margin, padding to label
        */
            input[type=radio] + label {
                display: inline-block;
                margin: -2px;
                padding: 3px 9px;
                background-color: #e7e7e7;
                border-color: #ddd;
                color: black;
            }
            /*
         Change background color for label next to checked radio button
         to make it look like highlighted button
        */
            input[type=radio]:checked + label {
                background-image: none;
                background-color: #1C82E1;
                color: white;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <asp:HiddenField ID="hddParameterMenuID" runat="server" />
                <asp:HiddenField ID="hddReloadGrid" runat="server" />
                <asp:HiddenField ID="hddReloadGridEdit" runat="server" />
                <asp:HiddenField ID="hddpSortBy" runat="server" />
                <asp:HiddenField ID="hddpSortType" runat="server" />
                <asp:HiddenField ID="hddpPagingDefault" runat="server" />
                <asp:HiddenField ID="hddpPageInfo" runat="server" />
                <asp:HiddenField ID="hddpSearch" runat="server" />
                <asp:HiddenField ID="hddMSGImportData" runat="server" />
                <asp:HiddenField ID="hddMSGApproveData" runat="server" />
                <asp:HiddenField ID="hddMSGImportDataNo" runat="server" />
                <asp:HiddenField ID="hddMSGApproveDataNo" runat="server" />
                <asp:HiddenField ID="hddMSGPleaseImport" runat="server" />
                <asp:HiddenField ID="hddMSGVerify" runat="server" />
                <asp:HiddenField ID="hddMSGOKData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddChkVerifyModal" runat="server" />
                <asp:HiddenField ID="hddpFlagSearch" runat="server" />
                <asp:HiddenField ID="hddpLG" runat="server" />
                <asp:HiddenField ID="hddpAutoCodeProjectTab1" runat="server" />
                <asp:HiddenField ID="hddpAutoNameProjectTab1" runat="server" />
                <asp:HiddenField ID="hddpAutoCodePhaseTab1" runat="server" />
                <asp:HiddenField ID="hddpAutoNamePhaseTab1" runat="server" />
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
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12" style="margin-left: 2px;">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <h3>
                                                            <asp:Label ID="lblMain3" runat="server"></asp:Label>
                                                        </h3>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-lg-1 col-md-12 col-sm-12">&nbsp;</div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                <asp:Label ID="lblsProject" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-8 col-md-12 col-sm-12">
                                                                <asp:DropDownList ID="ddlsProject" AppendDataBoundItems="True" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-lg-1 col-md-12 col-sm-12">&nbsp;</div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                <asp:Label ID="lblsPhase" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-8 col-md-12 col-sm-12">
                                                                <asp:DropDownList ID="ddlsPhase" runat="server" AppendDataBoundItems="True"  CssClass="chosen-select"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                            </div>
                                                        </div>
                                                        <br />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                    <ContentTemplate>

                                                        <div class="row">
                                                            <div class="col-lg-1 col-md-12 col-sm-12">&nbsp;</div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                <asp:Label ID="lblsPublicUtility" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                <asp:TextBox ID="txtsPublicUtility" Style="text-align: right;" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30" onKeyPress="return Check_Key(event);" onBlur="return Check_Format_Number(this);"></asp:TextBox>
                                                            </div>
                                                            <div class="col-lg-1 col-md-12 col-sm-12">
                                                                <asp:Label ID="lblsPublicUtilityM" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: right;">
                                                                <asp:Label ID="lblsPublicUtilityPerMonth" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: right;">
                                                                <asp:TextBox ID="txtsPublicUtilityPerMonth" Style="text-align: right;" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30" onKeyPress="return Check_Key_Decimal(this,event)" onBlur="return Check_Format2Digit(this);"></asp:TextBox>
                                                            </div>
                                                            <div class="col-lg-1 col-md-12 col-sm-12">
                                                                <asp:Label ID="lblsPublicUtilityPerMonthM" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-lg-1 col-md-12 col-sm-12">&nbsp;</div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                <asp:Label ID="lblsDownPayment" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                <asp:TextBox ID="txtsDownPayment" Style="text-align: right;" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30" onKeyPress="return Check_Key_Decimal(this,event)" onBlur="return Check_Format2Digit(this);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-lg-1 col-md-12 col-sm-12">&nbsp;</div>
                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                <asp:Label ID="lblsFileName" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-lg-8 col-md-12 col-sm-12">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="vertical-align: top; width: 100%">
                                                                    <tr>
                                                                        <td style="text-align: left; width: 59%;">
                                                                            <input id="txtUploadFile" runat="server" placeholder="Choose File" disabled="disabled" class="form-control" />
                                                                        </td>
                                                                        <td style="text-align: left; width: 1%;">&nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="text-align: left; width: 10%;">
                                                                            <div class="fileUpload btn btn-blue-alt">
                                                                                <span>
                                                                                    <asp:Label ID="lblsBrowse" runat="server"></asp:Label></span>
                                                                                <asp:FileUpload ID="fileUploadUF" runat="server" CssClass="upload" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" />
                                                                            </div>
                                                                        </td>
                                                                        <td style="text-align: left; width: 11%;">&nbsp;</td>
                                                                        <td style="text-align: left; width: 9%;">
                                                                            <asp:Button ID="btnRefresh" runat="server" Text="Verify" CssClass="btn btn-blue-alt" OnClientClick="showOverlay();" />
                                                                            <asp:Button ID="btnClearRefresh" runat="server" Text="Verify" CssClass="hide" />
                                                                        </td>
                                                                        <td style="text-align: left; width: 1%;">&nbsp;</td>
                                                                        <td style="text-align: left; width: 9%;">
                                                                            <asp:Button ID="btnImport" runat="server" Text="Upload" CssClass="btn btn-blue-alt" OnClientClick="showOverlay();" Visible='<%#Me.GetPermission().isAdd %>' />
                                                                            <asp:Button ID="btnApproveData" runat="server" Text="Upload" CssClass="hide" />

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <asp:Panel ID="pnComment" runat="server" Style="display: none;">
                                                            <div class="row">
                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                    <br />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                    <div class="ui-helper-clearfix" style="width: 100%;">
                                                                        <div class="ui-widget-content" style="width: 100%; margin-left: 5px; padding: 5px 5px 5px 5px;">
                                                                            <table border="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblColor1" runat="server" BackColor="Pink" ForeColor="Pink" Text="Color" Font-Bold="True"></asp:Label>&nbsp;<asp:Label
                                                                                            ID="lblValidate1" runat="server" Text="" Font-Size="Medium"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblColor2" runat="server" BackColor="SaddleBrown" ForeColor="SaddleBrown" Font-Bold="True" Text="Color"></asp:Label>&nbsp;<asp:Label
                                                                                            ID="lblValidate2" runat="server" Text="" Font-Size="Medium"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lblColor3" runat="server" BackColor="Yellow" ForeColor="Yellow" Font-Bold="True" Text="Color"></asp:Label>&nbsp;<asp:Label
                                                                                            ID="lblValidate3" runat="server" Text="" Font-Size="Medium"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="Label1" runat="server" BackColor="Red" ForeColor="Red" Font-Bold="True" Text="Color"></asp:Label>&nbsp;<asp:Label
                                                                                            ID="lblValidate4" runat="server" Text="" Font-Size="Medium"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <div class="modal fade .bs-example-modal-sm" id="ImportPriceListModal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                                                            <div class="modal-dialog modal-sm">
                                                                <div class="modal-content">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                        <h4 class="modal-title">
                                                                            <asp:Label ID="lblHeaderImportPriceList" runat="server" ForeColor="blue"></asp:Label></h4>
                                                                    </div>
                                                                    <div class="modal-body">
                                                                        <asp:Label ID="lblBodyImportPriceList" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-primary" data-dismiss="modal"><% Response.Write(hddMSGOKData.Value) %></button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal fade .bs-example-modal-sm" id="VerifyModal2" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                                                            <div class="modal-dialog modal-sm">
                                                                <div class="modal-content">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                        <h4 class="modal-title">
                                                                            <asp:Label ID="lblHeaderVerify2" runat="server" ForeColor="blue"></asp:Label></h4>
                                                                    </div>
                                                                    <div class="modal-body">
                                                                        <asp:Label ID="lblBodyVerify2" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-primary" data-dismiss="modal"><% Response.Write(hddMSGOKData.Value) %></button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal fade .bs-example-modal-sm" id="VerifyModal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                                                            <div class="modal-dialog modal-sm">
                                                                <div class="modal-content">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                        <h4 class="modal-title">
                                                                            <asp:Label ID="lblHeaderVerify" runat="server" ForeColor="blue"></asp:Label></h4>
                                                                    </div>
                                                                    <div class="modal-body">
                                                                        <asp:Label ID="lblBodyVerify" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-primary" data-dismiss="modal"><% Response.Write(hddMSGOKData.Value) %></button><%-- onclick="javascript:CallImportData();"--%>
                                                                        <button type="button" class="btn btn-default" onclick="javascript:CallClearVerify();" data-dismiss="modal"><% Response.Write(hddMSGCancelData.Value) %></button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal fade .bs-example-modal-sm" id="VerifyModal3" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                                                            <div class="modal-dialog modal-sm">
                                                                <div class="modal-content">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                        <h4 class="modal-title">
                                                                            <asp:Label ID="lblHeaderVerify3" runat="server" ForeColor="blue"></asp:Label></h4>
                                                                    </div>
                                                                    <div class="modal-body">
                                                                        <asp:Label ID="lblBodyVerify3" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-primary" data-dismiss="modal"><% Response.Write(hddMSGOKData.Value) %></button><%-- onclick="javascript:CallImportData();"--%>
                                                                        <button type="button" class="btn btn-default" onclick="javascript:CallClearVerify();" data-dismiss="modal"><% Response.Write(hddMSGCancelData.Value) %></button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal fade .bs-example-modal-sm" id="VerifyModal4" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                                                            <div class="modal-dialog modal-sm">
                                                                <div class="modal-content">
                                                                    <div class="modal-header">
                                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                        <h4 class="modal-title">
                                                                            <asp:Label ID="lblHeaderVerify4" runat="server" ForeColor="blue"></asp:Label></h4>
                                                                    </div>
                                                                    <div class="modal-body">
                                                                        <asp:Label ID="lblBodyVerify4" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-primary" data-dismiss="modal"><% Response.Write(hddMSGOKData.Value) %></button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                                <asp:Panel ID="divRequestType" runat="server" CssClass="rbdchk">
                                                                    <asp:RadioButtonList ID="rdblZone" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                                <asp:GridView ID="grdImport" runat="server" CssClass="table table-hover table-striped table-bordered table-responsive"
                                                                    EmptyDataText="No Items." EmptyDataRowStyle-CssClass="gridview-nodata" AutoGenerateColumns="False" Style="width: 100%">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGProductCode", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "ProjectCode")%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="left" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGZone", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <input type="hidden" data-zone="<%# DataBinder.Eval(Container.DataItem, "Zone") %>"></input>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Zone")%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="left" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGType", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: left; width: 75px;"><%# DataBinder.Eval(Container.DataItem, "Type")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="left" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGUnitNo", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "UnitNo")%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="left" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGBookingAmount", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "BookingAmount")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGContractAmount", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "ContractAmount")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGPeriodDown", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "PeriodDown")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGLandPrice", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "LandPrice")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGCostPrice", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "CostPrice")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGGPTarget", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "GPTarget")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGStandardPrice", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "StandardPrice")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGLocationPrice", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "LocationPrice")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGTargetPrice", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "TargetPrice")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGActualPrice", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "ActualPrice")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGPromotionPrice", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "PromotionPrice")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGPriceList", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div style="text-align: right; width: 100%"><%# DataBinder.Eval(Container.DataItem, "PriceList")%></div>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <%# GetWebMessage("lblGRemark", "Text", hddParameterMenuID.Value)%>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="hddUnitNo" runat="server" Value='<%#Eval("UnitNo") %>' />
                                                                                <asp:HiddenField ID="hddRemark" runat="server" Value='<%#Eval("Remark") %>' />
                                                                                <asp:TextBox ID="txtRemark" runat="server" Width="70px" CssClass="form-control" MaxLength="50" Text='<%#Eval("Remark") %>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="left" Height="15px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="False">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "chk_status")%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle />
                                                                            <ItemStyle HorizontalAlign="Right" Width="12%" Height="15px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataRowStyle CssClass="gridview-nodata" />
                                                                    <HeaderStyle BackColor="Green" ForeColor="White" />
                                                                    <RowStyle BackColor="White" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <br />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnRefresh" />
                                                        <asp:PostBackTrigger ControlID="btnImport" />
                                                        <asp:PostBackTrigger ControlID="btnClearRefresh" />
                                                        <asp:PostBackTrigger ControlID="btnApproveData" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
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
        });

        function SetInitial() {

            $('#<%=fileUploadUF.ClientID%>').change(function (e) {
                document.getElementById("<%=txtUploadFile.ClientID%>").value = this.value;
            });

            $('.rbdchk input[type=radio]').change(function () {
                zoneHideShow(this.value);
            })

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

        function showOverlay() {
            $("#overlay").modal();
        }

        function CallImportData() {
            showOverlay();
            __doPostBack('<%= btnImport.UniqueID%>');
        }

        function CallApproveData() {
            __doPostBack('<%= btnApproveData.UniqueID%>');
        }


        function CallVerifyData() {
            __doPostBack('<%= btnRefresh.UniqueID%>');
        }

        function CallClearVerify() {
            __doPostBack('<%= btnClearRefresh.UniqueID%>');
        }

        function ConfirmVerify() {
            $("#VerifyModal").modal();
        }

        function ConfirmVerify2() {
            $("#VerifyModal2").modal();

        }

        function ConfirmVerify3() {
            $("#VerifyModal3").modal();

        }

        function ConfirmVerify4() {
            $("#VerifyModal4").modal();

        }

        function ConfirmImportPriceList() {
            $("#ImportPriceListModal").modal();
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


        function zoneHideShow(szone) {
            $("[data-zone]").parent().parent().hide();
            $("[data-zone=" + szone + "]").parent().parent().show();
            console.log(szone);
        }



    </script>
</asp:Content>

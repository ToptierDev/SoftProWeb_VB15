<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ORG_Zone.aspx.vb" Inherits="SPW.Web.UI.ORG_Zone" EnableEventValidation="false" %>

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

        .FixImage {
            background-repeat: no-repeat;
            background-attachment: fixed;
            max-width: 500px;
            max-height: 350px;
            background-size: cover;
        }

        .img-attach {
            background-repeat: no-repeat;
            background-attachment: fixed;
            width: 100%;
            max-height: 350px;
            background-size: cover;
        }
           .img-responsive {
         
           width: auto;
    max-width: 90%;
    max-height: 350px;
    margin: auto;
    position: absolute;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
        }
        .MultiFile-label {
        }
        .multifile-imagecontainer{
            padding: 5px;
    border: 2px outset #6588b7;
    height:400px
        }
       .MultiFile-label.col-sm-6 {
            margin-top: 17px;
            }
       .MultiFile-label a.btn{
           float:right;
           margin-bottom:5px;
               z-index: 1;
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
                <asp:HiddenField ID="hddpCheckData" runat="server" />
                <asp:HiddenField ID="hddpKeyIDPicture" runat="server" />
                <asp:HiddenField ID="hddpNamePicture" runat="server" />
                <asp:HiddenField ID="hddMSGAddFile" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteFile" runat="server" />
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
                <asp:HiddenField ID="hddpMasterData" runat="server" />
                <asp:HiddenField ID="hddpMSGDup" runat="server" />
                <asp:HiddenField ID="hddpTypeBrownser" runat="server" />
                <asp:HiddenField ID="hddsProject" runat="server" />
                <asp:HiddenField ID="hddaProject" runat="server" />
                <asp:HiddenField ID="hddpUnitUsed" runat="server" />
                <asp:HiddenField ID="hddIDRealTime" runat="server" />
                <asp:HiddenField ID="hddCheckUsedMaster" runat="server" />
                <asp:HiddenField ID="hddCheckProjectPriceList" runat="server" />
                <asp:HiddenField ID="hddMSGCheckProjectPriceList" runat="server" />
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
                                                            <a id="btnAddData" class="btn btn-info glyph-icon icon-plus tooltip-button" runat="server" href="javascript:CallAddData();" Visible='<%#Me.GetPermission().isAdd %>'></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-lg-1 col-md-12 col-sm-12">&nbsp;</div>
                                                        <div class="col-lg-11 col-md-12 col-sm-12">
                                                            <div class="row">
                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                    <div class="row">
                                                                        <div class="col-lg-1 col-md-12 col-sm-12">
                                                                            &nbsp;
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblsProject" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-8 col-md-12 col-sm-12">
                                                                            <asp:DropDownList ID="ddlsProject" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>

                                                                            <asp:Label ID="lblMassage4" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-1 col-md-12 col-sm-12">
                                                                            &nbsp;
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblsPhaseCode" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-12 col-sm-12">
                                                                            <asp:DropDownList ID="ddlsPhase" runat="server" CssClass="chosen-select"></asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">

                                                                    <a href="javascript:CallLoaddata();" class="btn  btn-info">
                                                                        <i class="glyph-icon icon-search"></i>
                                                                        <asp:Label ID="lblsSearch" runat="server"></asp:Label>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            &nbsp;
                                                        </div>
                                                    </div>
                                                    <div class="row" style="overflow-x:auto;overflow-y:hidden;">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <%
                                                                Dim lc As New List(Of Zone_ViewModel)
                                                                If hddReloadGrid.Value <> String.Empty Then
                                                                    If GetDataZone() IsNot Nothing Then
                                                                        lc = GetDataZone()
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
                        For Each sublc As Zone_ViewModel In lc
                            Dim strb As New StringBuilder()
                                                                    %>
                                                                    <tr>
                                                                        <%
                                                                            Try
                                                                                strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-edit'  href='javascript:CallEditData(&#39;" + IIf(sublc.ZoneCode <> String.Empty, sublc.FREPRJNO & "|" & sublc.PhaseCode & "|" & sublc.ZoneCode, String.Empty) + "&#39;);'></a></td>")
                                                                                Dim arr As String = hddCheckUsedMaster.Value
                                                                                Dim pChecktd As String = String.Empty
                                                                                For Each m As String In arr.Split(",")
                                                                                    Dim strPhase As String = String.Empty
                                                                                    Dim strZone As String = String.Empty
                                                                                    strPhase = m.Split("|")(0)
                                                                                    strZone = m.Split("|")(1)
                                                                                    If strZone = sublc.ZoneCode And strPhase = sublc.PhaseCode Then
                                                                                        strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                                        pChecktd = "1"
                                                                                        Exit For
                                                                                    End If
                                                                                Next
                                                                                If pChecktd = String.Empty Then
                                                                                    strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-danger glyph-icon icon-trash' href='javascript:ConfirmDelete(&#39;" + IIf(sublc.ZoneCode <> String.Empty, sublc.FREPRJNO & "|" & sublc.PhaseCode & "|" & sublc.ZoneCode, String.Empty) + "&#39;);'></a></td>")
                                                                                End If
                                                                                strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                                strb.Append("<td style='width:20%;'>" + IIf(sublc.PhaseCode <> String.Empty, sublc.PhaseCode, String.Empty) + "</td>")
                                                                                strb.Append("<td style='width:30%;'>" + IIf(sublc.ZoneCode <> String.Empty, sublc.ZoneCode, String.Empty) + "</td>")

                                                                                HttpContext.Current.Response.Write(strb.ToString())
                                                                            Catch ex As Exception

                                                                            End Try

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
                                                                                <a id="btnSaveData" runat="server" href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button"></a>
                                                                                <%End if %>
                                                                                <a id="btnCancelData" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="panel-body <%=IIf((hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit), "", "disabled") %>">
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12"></div>
                                                                        <div class="col-lg-6 col-md-12 col-sm-12">
                                                                            <div class="row">
                                                                                <div class="col-lg-1 col-md-12 col-sm-12">
                                                                                    &nbsp;
                                                                                </div>
                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaProject" runat="server"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12">

                                                                                    <asp:DropDownList ID="ddlaProject" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>

                                                                                    <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-1 col-md-12 col-sm-12">
                                                                                    &nbsp;
                                                                                </div>
                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaPhaseCode" runat="server"></asp:Label><asp:Label ID="Label4" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:DropDownList ID="ddlaPhase" runat="server" CssClass="chosen-select"></asp:DropDownList>

                                                                                    <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-1 col-md-12 col-sm-12">
                                                                                    &nbsp;
                                                                                </div>
                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaZoneCode" runat="server"></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtaZoneCode" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="2" AutoPostBack="true" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipZoneCode") %>'></asp:TextBox>
                                                                                    <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <%--<div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <a id="btnAddPicture" runat="server" href="javascript:CallAddPicture();" class="btn btn-info tooltip-button">

                                                                                            <i class="glyph-icon icon-image"></i>
                                                                                            <asp:Label ID="lblsPicture" runat="server"></asp:Label>
                                                                                        </a>
                                                                                    </td>
                                                                                    <td>
                                                                                        <a id="btnDeletePictures" runat="server" href="javascript:CallDeletePicture();" class="btn  btn-danger tooltip-button">

                                                                                            <i class="glyph-icon icon-trash"></i>


                                                                                            <asp:Label ID="lblsDeletePicture" runat="server"></asp:Label>

                                                                                        </a>
                                                                                    </td>
                                                                                   
                                                                                </tr>
                                                                            </table>

                                                                        </div>--%>
                                                                    </div>
                                                                    <hr style="width: 100%; border: 0.2px solid; border-color: #1c82e1" />
                                                                    <asp:Panel ID="Panel2" runat="server" DefaultButton="btnCallAddDataUnitTemp">
                                                                        <div class="row">
                                                                            <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                <div class="row">
                                                                                    <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                        <asp:Label ID="lblaUnitFrom" runat="server" Text="Label"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                        <asp:TextBox ID="txtaUnitFrom" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <asp:Label ID="lblMassage5" runat="server" ForeColor="Red"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                        <asp:Button ID="btnCallAddDataUnitTemp" runat="server" OnClientClick="CallAddDataUnit();" CssClass="hide" />
                                                                                        <a href="javascript:CallAddDataUnit();" class="btn btn-success glyph-icon icon-plus"></a>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                        <asp:Label ID="lblaUnitTo" runat="server" Text="Label"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-lg-4 col-md-12 col-sm-12">
                                                                                        <asp:TextBox ID="txtaUnitTo" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                        <asp:Label ID="lblMassage6" runat="server" ForeColor="Red"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-6 col-md-12 col-sm-12">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:GridView ID="grdView2" runat="server" CssClass="table table-hover table-striped table-bordered table-responsive"
                                                                                        EmptyDataText="No Items." EmptyDataRowStyle-CssClass="gridview-nodata" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" Style="width: 100%">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <%# GetResource("gUnit", "Text", hddParameterMenuID.Value)%>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtgUnit" autocomplete="off" runat="server" CssClass="form-control" Style="height: 28px; vertical-align: top;" MaxLength="10"></asp:TextBox>
                                                                                                    <asp:HiddenField ID="hddID" runat="server" />
                                                                                                    <asp:HiddenField ID="hddFlagSetAdd" runat="server" />
                                                                                                    <asp:HiddenField ID="hddFlagDup" runat="server" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle />
                                                                                                <ItemStyle HorizontalAlign="left" Width="24%" Height="15px" />
                                                                                            </asp:TemplateField>
                                                                                            <%-- <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <%# GetResource("gLeft", "Text", hddParameterMenuID.Value)%>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtgLeft" autocomplete="off" runat="server" CssClass="form-control text-right" Style="height: 28px; vertical-align: top;" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat(this,event);"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle />
                                                                                                <ItemStyle HorizontalAlign="right" Width="25%" Height="15px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <%# GetResource("gRight", "Text", hddParameterMenuID.Value)%>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtgRight" autocomplete="off" runat="server" CssClass="form-control text-right" Style="height: 28px; vertical-align: top;" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat(this,event);"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle />
                                                                                                <ItemStyle HorizontalAlign="right" Width="25%" Height="15px" />
                                                                                            </asp:TemplateField>--%>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <%# GetResource("gStatus", "Text", hddParameterMenuID.Value)%>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtgStatus" autocomplete="off" runat="server" Enabled="false" Style="height: 28px; vertical-align: top; width: 100%; border: 0; background-color: transparent;" MaxLength="1"></asp:TextBox>
                                                                                                    <asp:HiddenField ID="hddgStatus" runat="server" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle />
                                                                                                <ItemStyle HorizontalAlign="left" Width="24%" Height="15px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <%# GetResource("col_delete", "Text", "1")%>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="btnDelete" ImageUrl="~/image/delete.jpg" Style="width: 30px; height: 30px; margin-left: 5px; margin-top: 7px;" />
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
                                                                        <div class="col-lg-6 col-md-12 col-sm-12">
                                                                            <div class="panel panel-primary">
                                                                                <div class="panel-heading">
                                                                                    <asp:Label ID="lblTextPicture" runat="server" Text="Label"></asp:Label>
                                                                                </div>
                                                                                <div class="panel-body">
                                                                                    <div class="row"></div>
                                                                                    <div class="MultiFile-label  col-sm-12" style="text-align: left;">
                                                                                        <div class="multifile-imagecontainer">
                                                                                            <a class="btn btn-info"
                                                                                                id="btnAddFileUpload" onclick="triggerClick('FileUpload'); ">
                                                                                                <i class="glyph-icon  icon-image"></i><%=grtt("resAddPicture") %></a>
                                                                                            <a class="btn btn-danger" style="display: none;"
                                                                                                id="btnClearFileUpload" for="FileUpload" onclick="fn_clearValue(this); ">
                                                                                                <i class="glyph-icon  icon-close"></i><%=grtt("resClearPicture") %></a>
                                                                                            <asp:CheckBox runat="server" for="FileUpload" ID="chkDelete" CssClass="hidden" />
                                                                                            <asp:Image ID="imga" for="FileUpload" runat="server"
                                                                                                data-default="image/no_image_icon.png"
                                                                                                ImageUrl="image/no_image_icon.png" class="img-responsive" />
                                                                                            <asp:FileUpload ID="FileUpload"
                                                                                                class="hidden"
                                                                                                runat="server"
                                                                                                accept="image/*;"
                                                                                                onchange="PreviewImage(this);"></asp:FileUpload>
                                                                                        </div>
                                                                                    </div>


                                                                                </div>
                                                                            </div>
                                                                            <%--<asp:FileUpload ID="FileUpload1" runat="server" CssClass="hide"></asp:FileUpload>--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="panel-footer">
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
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
                                <button id="btnConfrimDelete" runat="server" type="button" class="btn btn-danger" onclick="javascript:CallDeleteData();"></button>
                                <button id="btnConfrimCancel" runat="server" type="button" class="btn btn-default" data-dismiss="modal"></button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade .bs-example-modal-sm" id="ConfirmModal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="H4">
                                    <asp:Label ID="lblHeaderSave" runat="server" ForeColor="red"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblBodySave" runat="server"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <button id="btnCallSaveOK" runat="server" type="button" class="btn btn-success" onclick="javascript:CallSaveData();"></button>
                                <button id="btnCallSaveCancel" runat="server" type="button" class="btn btn-default" data-dismiss="modal"></button>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnCancel" runat="server" CssClass="hide" />
                <asp:Button ID="btnSearch" runat="server" CssClass="hide" />
                <asp:Button ID="btnAdd" runat="server" CssClass="hide" />
                <asp:Button ID="btnEdit" runat="server" CssClass="hide" />
                <asp:Button ID="btnDelete" runat="server" CssClass="hide" />
                <asp:Button ID="btnSave" runat="server" CssClass="hide" />
                <%--<asp:Button ID="btnFileUpload" runat="server" CssClass="hide" />
                <asp:Button ID="btnDeletePicture" runat="server" CssClass="hide" />--%>
                <asp:Button ID="btnAddDataUnit" runat="server" CssClass="hide" />
                <asp:Button ID="btnReloadProject" runat="server" CssClass="hide" />
                <asp:Button ID="btnReloadPhase" runat="server" CssClass="hide" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <%--<asp:PostBackTrigger ControlID="btnFileUpload" />
            <asp:PostBackTrigger ControlID="btnDeletePicture" />--%>
            <asp:PostBackTrigger ControlID="btnAddDataUnit" />
            <asp:PostBackTrigger ControlID="btnReloadProject" />
            <asp:PostBackTrigger ControlID="btnReloadPhase" />
            <asp:PostBackTrigger ControlID="btnCallAddDataUnitTemp" />
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

        function showOverlay() {
            $("#overlay").modal();
        }

        function closeOverlay() {
            $("#overlay").modal('hide');
        }

        function CallProjectReload() {
            showOverlay();
            __doPostBack('<%= btnReloadProject.UniqueID%>');
        }

        function CallLoadHrefNewtab(Urls) {
            window.open(Urls, '_blank');
        }

        function CallPhaseReload() {
            showOverlay();
            __doPostBack('<%= btnReloadPhase.UniqueID%>');
        }

        function CallCancel() {
            showOverlay();
            __doPostBack('<%= btnCancel.UniqueID%>');
        }

        function CallLoaddata() {
            if ($('#<%=ddlsProject.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlsProject.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "";
                return;
            } else {
                $('#<%=ddlsProject.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "none";
            }
            showOverlay();
            __doPostBack('<%= btnSearch.UniqueID%>');
        }

        function CallAddData() {
            showOverlay();
            __doPostBack('<%= btnAdd.UniqueID%>');
        }

        function CallAddDataUnit() {
            if ($('#<%=ddlaProject.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaProject.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                return;
            } else {
                $('#<%=ddlaProject.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }
            if ($('#<%=txtaUnitFrom.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaUnitFrom.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "";
                return;
            } else {
                $('#<%=txtaUnitFrom.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "none";
            }
            if ($('#<%=txtaUnitTo.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaUnitTo.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "";
                return;
            } else {
                $('#<%=txtaUnitTo.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "none";
            }
            showOverlay();
            __doPostBack('<%= btnAddDataUnit.UniqueID%>');
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
            
            lblBodydelete.value = lblBodydelete.defaultValue + " " + pKey.split("|")[2] + " ?";

            $("#DeleteModal").modal();
        }

        function CallSaveData() {
            showOverlay();
            __doPostBack('<%= btnSave.UniqueID%>');
            return;
        }

        function CheckData() {
            if ($('#<%=ddlaProject.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaProject.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                scrollAndFocus('#<%=ddlaProject.ClientID%>');
                return;
            } else {
                $('#<%=ddlaProject.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }
            if ($('#<%=ddlaPhase.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaPhase.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                scrollAndFocus('#<%=ddlaPhase.ClientID%>');
                return;
            } else {
                $('#<%=ddlaPhase.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
            }
            if ($('#<%=txtaZoneCode.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaZoneCode.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
                scrollAndFocus('#<%=txtaZoneCode.ClientID%>');
                return;
            } else {
                $('#<%=txtaZoneCode.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";
            }
            var chk = true;
            var hddpUnitUsed = document.getElementById("<%= hddpUnitUsed.ClientID%>");
            var grid = document.getElementById("<%= grdView2.ClientID %>");
            if (grid != null) {
                var count = grid.rows.length;
                if (count != null) {
                    if (count > 2) {
                        for (var i = 0; i < grid.rows.length; i++) {
                            inputs = grid.rows[i].getElementsByTagName("input");
                            for (var j = 0; j < inputs.length; j++) {
                                if (inputs[j].type == "text") {
                                    if (j == "0") {
                                        if (inputs[j].value != "") {
                                            var str_array = hddpUnitUsed.value;
                                            var chkIndex = str_array.indexOf(inputs[j].value);
                                            if (chkIndex != -1) {
                                                var str_arrays = hddpUnitUsed.value.split(',');
                                                for (var ii = 0; ii < str_arrays.length; ii++) {
                                                    if (str_arrays[ii] == inputs[j].value) {
                                                        chk = false;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (chk == false) {
                $("#ConfirmModal").modal();
            } else {
                CallSaveData();
            }

        }

        <%--function CallAddPicture() {
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
    }--%>

    <%--function CallDeletePicture() {
        showOverlay();
        __doPostBack('<%= btnDeletePicture.UniqueID%>');
    }--%>

    function addRowGridView(txtClientID) {
            var hddpClientID = document.getElementById("<%= hddpClientID.ClientID%>");
            hddpClientID.value = txtClientID
            __doPostBack('<%= btnGridAdd.UniqueID%>');
    }

    function CheckDataDup(txt, IDs, txtstatus) {
        var chk = false;
        var txt = document.getElementById(txt);
        var txtstatus = document.getElementById(txtstatus);
        if (txt.value != "") {
            var hddpMSGDup = document.getElementById("<%= hddpMSGDup.ClientID%>");
            var hddpCheckData = document.getElementById("<%= hddpCheckData.ClientID%>");
            var hddpMasterData = document.getElementById("<%= hddpMasterData.ClientID%>");
            var hddCheckProjectPriceList = document.getElementById("<%= hddCheckProjectPriceList.ClientID%>");
            var hddMSGCheckProjectPriceList = document.getElementById("<%= hddMSGCheckProjectPriceList.ClientID%>");
            hddpCheckData.value = txt.value;
            if (hddpMasterData.value != "") {
                var str_array = hddpMasterData.value.split(',');
                for (var i = 0; i < str_array.length; i++) {
                    if (str_array[i].split("|")[0] == hddpCheckData.value) {
                        chk = true;
                        break;
                    }
                }
            }
            if (chk == false) {
                txt.value = "";
                txtstatus.value = "";
                OpenDialogError(hddpMSGDup.value);
                return;
            }

            chk = true;
            if (hddCheckProjectPriceList.value != "") {
                var str_array = hddCheckProjectPriceList.value.split(',');
                for (var i = 0; i < str_array.length; i++) {
                    if (str_array[i].split("|")[0] == txt.value) {
                        chk = false;
                        break;
                    }
                }
            }
            if (chk == false) {
                txt.value = "";
                txtstatus.value = "";
                OpenDialogError(hddMSGCheckProjectPriceList.value);
                return;
            }

            chk = true;
            var pDup;
            var grid = document.getElementById("<%= grdView2.ClientID %>");
            if (grid != null) {
                var count = grid.rows.length;
                if (count != null) {
                    if (count > 2) {
                        for (var i = 0; i < grid.rows.length; i++) {
                            inputs = grid.rows[i].getElementsByTagName("input");
                            for (var j = 0; j < inputs.length; j++) {
                                if (inputs[j].type == "text") {
                                    if (j == "0") {
                                        if (i != IDs) {
                                            if (inputs[j].value == hddpCheckData.value) {
                                                chk = false;
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
            }
            if (chk == false) {
                txt.value = "";
                txtstatus.value = "";
                OpenDialogError("<%# Me.GetResource("msg_duplicate_table", "MSG") %>" + pDup);
                return;
            }

            var grids = document.getElementById("<%= grdView2.ClientID %>");
            if (grids != null) {
                var count = grids.rows.length;
                if (count != null) {
                    if (count > 2) {
                        inputst = grids.rows[IDs].getElementsByTagName("input");
                        for (var j = 0; j < inputst.length; j++) {
                            if (inputst[j].type == "text") {
                                if (j == "0") {
                                    if (hddpMasterData.value != "") {
                                        var str_array = hddpMasterData.value.split(',');
                                        for (var i = 0; i < str_array.length; i++) {
                                            if (str_array[i].split("|")[0] == inputst[0].value) {
                                                //if (str_array[i].split("|")[1] != "") {
                                                //    inputst[4].value = fnFormatMoney(parseFloat(replaceAll(str_array[i].split("|")[1], ',', '')));
                                                //}
                                                //if (str_array[i].split("|")[2] != "") {
                                                //    inputst[5].value = fnFormatMoney(parseFloat(replaceAll(str_array[i].split("|")[2], ',', '')));;
                                                //}
                                                if (str_array[i].split("|")[3] != "") {
                                                    inputst[4].value = str_array[i].split("|")[3];
                                                    inputst[5].value = str_array[i].split("|")[3];
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
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

        function setFormat(txt, e) {
            var txt = document.getElementById(txt.id);
            if (txt.value != "") {
                txt.value = fnFormatMoney(parseFloat(replaceAll(txt.value, ',', '')));
            }

            //if (txt.value == "0.00") {
            //    txt.value = "";
            //}
        }

        function replaceAll(str, find, replace) {
            return str.replace(new RegExp(find, 'g'), replace);
        }

        function fnFormatMoney(values) {
            return values.toFixed(2).replace(/./g, function (c, i, a) { return i && c !== "." && ((a.length - i) % 3 === 0) ? ',' + c : c; });
        }

    </script>
    <script>

        function PreviewImage(el) {


            if (typeof FileReader !== "undefined") {
                //type check ที่type fileแล้ว

                var filename = el.value;
                var valid_extensions = /(\.jpg|\.jpeg|\.gif|\.png)$/i;

                if (filename == "") {
                    return false;
                }
                if (valid_extensions.test(filename)) {
                    console.log(filename);
                }
                else {
                    alert('รองรับเฉพาะไฟล์ .jpg .gif .png');
                    return false;
                }

                var size = el.files[0].size;
                console.log(size);
                // check file size
                if (size > 5000000)//5mb > 5000000
                {
                    alert('ไฟล์รูปเกิน 5mb กรุณาอัปไฟล์ใหม่');
                    el.value = "";
                    return false;
                } else {
                    ShowPreview(el);
                }
            }

        };



        function ShowPreview(el) {
            oFReader = new FileReader();
            oFReader.readAsDataURL(el.files[0]);

            oFReader.onload = function (oFREvent) {
                var uploadId = getIDFromAspDOM(el);
                $('img[for=' + uploadId + ']').attr('src', oFREvent.target.result);
                $('#btnAdd' + uploadId).hide();
                $('#btnClear' + uploadId).show();
                $('span[for=' + uploadId + ']').children('input[type = checkbox]').prop('checked', false);
            };

        };
        function triggerClick(id) {
            $$(id).trigger('click');

        }

        function fn_clearValue(el) {
            uploadId = $(el).attr('for');
            $('#btnAdd' + uploadId).show();
            $('#btnClear' + uploadId).hide();
            defaultSrc = $('img[for=' + uploadId + ']').attr('data-default');
            $('img[for=' + uploadId + ']').attr('src', defaultSrc);

            document.getElementById("<%=imga.ClientID%>").removeAttribute("onclick");

            $$(uploadId).val('');
            console.log(uploadId);
            $('span[for=' + uploadId + ']').children('input[type = checkbox]').prop('checked', true);
            return false;
        }

        function $$(id, context) {
            var el = $("#" + id, context);
            if (el.length < 1)
                el = $("[id$=_" + id + "]", context);
            return el;
        }
        function getIDFromAspDOM(el) {
            if ($(el).attr('id').indexOf("_") != -1) {
                result = $(el).attr('id').substring($(el).attr('id').lastIndexOf("_") + 1);

            }
            else
                result = $(el).attr('id');

            return result;
        }
        function getIDFromAspID(elid) {
            if (elid.indexOf("_") != -1) {
                result = elid.substring(elid.lastIndexOf("_") + 1);

            }
            else
                result = elid;

            return result;
        }


        function checkExistImage() {
            if ($$('imgaPicture').attr('src') != $$('imgaPicture').attr('data-default')) {
                $('#btnAddFileUploadPicture').hide();
                $('#btnClearFileUploadPicture').show();
            }
            if ($$('imga').attr('src') != $$('imga').attr('data-default')) {
                $('#btnAddFileUpload').hide();
                $('#btnClearFileUpload').show();
            }

        }
        $(function () {
            checkExistImage();

        });
        function endRequestHandler(sender, args) {
            $('select').chosen();
            $(".chosen-search").append('<i class="glyph-icon icon-search"></i>');
            $(".chosen-single div").html('<i class="glyph-icon icon-caret-down"></i>');
            checkExistImage();
        }

    </script>
</asp:Content>

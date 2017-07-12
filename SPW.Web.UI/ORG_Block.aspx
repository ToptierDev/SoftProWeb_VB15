<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ORG_Block.aspx.vb" Inherits="SPW.Web.UI.ORG_Block" EnableEventValidation="false" %>

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
                <asp:HiddenField ID="hddpClientID" runat="server" />
                <asp:HiddenField ID="hddpCheckData" runat="server" />
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
                <asp:HiddenField ID="hddpMasterData" runat="server" />
                <asp:HiddenField ID="hddpMSGDup" runat="server" />
                <asp:HiddenField ID="hddsProject" runat="server" />
                <asp:HiddenField ID="hddaProject" runat="server" />
                <asp:HiddenField ID="hddsPhase" runat="server" />
                <asp:HiddenField ID="hddaPhase" runat="server" />
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
                                                            <a id="btnAddData" class="btn btn-info glyph-icon icon-plus tooltip-button" runat="server" href="javascript:CallAddData();" Visible='<%#Me.GetPermission().isAdd %>'>
                                                              
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-lg-9 col-md-12 col-sm-12">
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    &nbsp;
                                                                </div>
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    <asp:Label ID="lblsProject" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                    <asp:DropDownList ID="ddlsProject" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                    <asp:Label ID="lblMassage7" runat="server" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    &nbsp;
                                                                </div>
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    <asp:Label ID="lblsPhase" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                    <asp:DropDownList ID="ddlsPhase" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    &nbsp;
                                                                </div>
                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                    <asp:Label ID="lblsZoneCode" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                    <asp:DropDownList ID="ddlsZone" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                            
                                                                    <a href="javascript:CallLoaddata();" class="btn  btn-info">
                                                                       <i class="glyph-icon icon-search"></i>
                                                                            <asp:Label ID="lblsSearch" runat="server"></asp:Label>
                                                                    </a>
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
                                                                Dim lc As New List(Of Block_ViewModel)
                                                                If hddReloadGrid.Value <> String.Empty Then
                                                                    If GetDataBlock() IsNot Nothing Then
                                                                        lc = GetDataBlock()
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
                        For Each sublc As Block_ViewModel In lc
                            Dim strb As New StringBuilder()
                                                                    %>
                                                                    <tr>
                                                                        <%
                                                                            Try
                                                                                strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-edit'  href='javascript:CallEditData(&#39;" + IIf(sublc.BlockCode <> String.Empty, sublc.ProjectCode & "|" & sublc.PhaseCode & "|" & sublc.ZoneCode & "|" & sublc.BlockCode, String.Empty) + "&#39;);'></a></td>")

                                                                                Dim arr As String = hddCheckUsedMaster.Value
                                                                                Dim pChecktd As String = String.Empty
                                                                                For Each m As String In arr.Split(",")
                                                                                    Dim strPhase As String = String.Empty
                                                                                    Dim strZone As String = String.Empty
                                                                                    Dim strBlock As String = String.Empty
                                                                                    strPhase = m.Split("|")(0)
                                                                                    strZone = m.Split("|")(1)
                                                                                    strBlock = m.Split("|")(2)
                                                                                    If strZone = sublc.ZoneCode And strPhase = sublc.PhaseCode And strBlock = sublc.BlockCode Then
                                                                                        strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                                        pChecktd = "1"
                                                                                        Exit For
                                                                                    End If
                                                                                Next

                                                                                If pChecktd = String.Empty Then
                                                                                    strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-danger glyph-icon icon-trash' href='javascript:ConfirmDelete(&#39;" + IIf(sublc.BlockCode <> String.Empty, sublc.ProjectCode & "|" & sublc.PhaseCode & "|" & sublc.ZoneCode & "|" & sublc.BlockCode, String.Empty) + "&#39;);'></a></td>")
                                                                                End If

                                                                                strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                                strb.Append("<td style='width:20%;'>" + IIf(sublc.PhaseCode <> String.Empty, sublc.PhaseCode, String.Empty) + "</td>")
                                                                                strb.Append("<td style='width:30%;'>" + IIf(sublc.ZoneCode <> String.Empty, sublc.ZoneCode, String.Empty) + "</td>")
                                                                                strb.Append("<td style='width:30%;'>" + IIf(sublc.BlockCode <> String.Empty, sublc.BlockCode, String.Empty) + "</td>")
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
                                                                                <asp:Button ID="btnSaveTemp" runat="server" OnClientClick="javascript:CheckData();return false" CssClass="hide"/>
                                                                                <%If (hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit) Then %>
                                                                                <a id="btnSaveData" runat="server" href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button">
                                                                                  
                                                                                </a>
                                                                                <%End if %>
                                                                                <a id="btnCancelData" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button">
                                                                                
                                                                                </a>  
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="panel-body <%=IIf((hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit), "", "disabled") %>">
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaProject" runat="server"></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaProject" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaPhase" runat="server"></asp:Label><asp:Label ID="Label3" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaPhase" runat="server" CssClass="chosen-select" AutoPostBack="true"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaZone" runat="server"></asp:Label><asp:Label ID="Label4" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:DropDownList ID="ddlaZone" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                            <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaBlock" runat="server"></asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                            <asp:TextBox ID="txtaBlock" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="2" AutoPostBack="true" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipBlock") %>'></asp:TextBox>
                                                                            <asp:Label ID="lblMassage4" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
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
                                                                                    <asp:Button ID="btnCallAddDataUnitTemp" runat="server" OnClientClick="CallAddDataUnit();" CssClass="hide"/>
                                                                                    <a class="btn btn-success glyph-icon icon-plus" href="javascript:CallAddDataUnit();">
                                                                                     
                                                                                    </a>
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
                                                                        <div class="col-lg-8 col-md-12 col-sm-12  <%=IIf((hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit), "", "disabled") %>">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:GridView ID="grdView2" runat="server" CssClass="table table-hover table-striped table-bordered table-responsive"
                                                                                        EmptyDataText="No Items." EmptyDataRowStyle-CssClass="gridview-nodata" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" Style="width: 100%">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <%# GetWebMessage("gUnit", "Text", hddParameterMenuID.Value)%>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtgUnit" autocomplete="off" runat="server" CssClass="form-control" Style="height: 28px; vertical-align: top;" MaxLength="10"></asp:TextBox>
                                                                                                    <asp:HiddenField ID="hddID" runat="server" />
                                                                                                    <asp:HiddenField ID="hddFlagSetAdd" runat="server" />
                                                                                                    <asp:HiddenField ID="hddFlagDup" runat="server" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle />
                                                                                                <ItemStyle HorizontalAlign="left" Width="20%" Height="15px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <%# GetWebMessage("gStyleCode", "Text", hddParameterMenuID.Value)%>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtgStyleCode" autocomplete="off" runat="server" CssClass="form-control" Style="height: 28px; vertical-align: top;border:0;background-color:transparent;" Enabled="false" MaxLength="25"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle />
                                                                                                <ItemStyle HorizontalAlign="right" Width="40%" Height="15px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <%# GetWebMessage("gAddress", "Text", hddParameterMenuID.Value)%>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox ID="txtgAddress" autocomplete="off" runat="server" CssClass="form-control" Style="height: 28px; vertical-align: top;border:0;background-color:transparent;" Enabled="false" MaxLength="10"></asp:TextBox>
                                                                                                    <asp:HiddenField ID="hddgStyleCode" runat="server" />
                                                                                                    <asp:HiddenField ID="hddgAddress" runat="server" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle />
                                                                                                <ItemStyle HorizontalAlign="right" Width="40%" Height="15px" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <%# GetWebMessage("col_delete", "Text", "1")%>
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="btnDelete" ImageUrl="~/image/delete.jpg" Style="width: 30px; height: 30px; margin-left: 5px; margin-top: 7px;" ToolTip="Click to Delete" />
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
                                <button id="btnCallSaveCancel" runat="server"  type="button" class="btn btn-default" data-dismiss="modal"></button>
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
                <asp:Button ID="btnAddDataUnit" runat="server" CssClass="hide" />
                <asp:Button ID="btnReloadGrid" runat="server" CssClass="hide" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnAddDataUnit" />
            <asp:PostBackTrigger ControlID="btnReloadGrid" />
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

        function CallCancel() {
            showOverlay();
            __doPostBack('<%= btnCancel.UniqueID%>');
         }

        function CallLoaddata() {
            if ($('#<%=ddlsProject.ClientID%>').val().replace(" ", "") == "") {
                 $('#<%=ddlsProject.ClientID%>').addClass("parsley-error")
                 document.getElementById('ContentPlaceHolder1_lblMassage7').style.display = "";
                 return;
             } else {
                 $('#<%=ddlsProject.ClientID%>').removeClass("parsley-error")
                 document.getElementById('ContentPlaceHolder1_lblMassage7').style.display = "none";
             }
            showOverlay();
            __doPostBack('<%= btnSearch.UniqueID%>');
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

        function CallReloadGrid() {
            showOverlay();
            __doPostBack('<%= btnReloadGrid.UniqueID%>');
        }

        function ConfirmDelete(pKey) {
            var hddKeyID = document.getElementById("<%= hddKeyID.ClientID%>");
            var lblBodydelete = document.getElementById("<%= lblBodydelete.ClientID%>");

            hddKeyID.value = pKey;
            lblBodydelete.value = lblBodydelete.defaultValue + " " + pKey.split("|")[3] + " ?";

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
            if ($('#<%=ddlaZone.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlaZone.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
                scrollAndFocus('#<%=ddlaZone.ClientID%>');
                return;
            } else {
                $('#<%=ddlaZone.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";
            }
            if ($('#<%=txtaBlock.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaBlock.ClientID%>').addClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "";
                scrollAndFocus('#<%=txtaBlock.ClientID%>');
                    return;
            } else {
                $('#<%=txtaBlock.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "none";
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

         function CallAddDataUnit() {
            if ($('#<%=ddlaProject.ClientID%>').val().replace(" ", "") == "") {
                 $('#<%=ddlaProject.ClientID%>').addClass("parsley-error")
                 document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                scrollAndFocus('#<%=ddlaProject.ClientID%>');
                 return;
             } else {
                 $('#<%=ddlaProject.ClientID%>').removeClass("parsley-error")
                 document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
             }
             if ($('#<%=txtaUnitFrom.ClientID%>').val().replace(" ", "") == "") {
                 $('#<%=txtaUnitFrom.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "";
                scrollAndFocus('#<%=txtaUnitFrom.ClientID%>');
                return;
            } else {
                $('#<%=txtaUnitFrom.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "none";
             }
             if ($('#<%=txtaUnitTo.ClientID%>').val().replace(" ", "") == "") {
                 $('#<%=txtaUnitTo.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "";
                 scrollAndFocus('#<%=txtaUnitTo.ClientID%>');
                return;
            } else {
                $('#<%=txtaUnitTo.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "none";
            }
            showOverlay();
            __doPostBack('<%= btnAddDataUnit.UniqueID%>');
         }

        function CheckDataDup(txt, IDs,txtgStyleCode,txtgAddress,hddgStyleCode,hddgAddress) {
            var chk = false;
            var txt = document.getElementById(txt);
            var hddCheckProjectPriceList = document.getElementById("<%= hddCheckProjectPriceList.ClientID%>");
            var hddMSGCheckProjectPriceList = document.getElementById("<%= hddMSGCheckProjectPriceList.ClientID%>");
            if (txt.value != "")
            {
                var hddpMSGDup = document.getElementById("<%= hddpMSGDup.ClientID%>");
                var hddpCheckData = document.getElementById("<%= hddpCheckData.ClientID%>");
                var hddpMasterData = document.getElementById("<%= hddpMasterData.ClientID%>");
                var txtgStyleCode = document.getElementById(txtgStyleCode);
                var txtgAddress = document.getElementById(txtgAddress);
                var hddgStyleCode = document.getElementById(hddgStyleCode);
                var hddgAddress = document.getElementById(hddgAddress);
                txtgStyleCode.value = "";
                txtgAddress.value = "";
                hddpCheckData.value = txt.value;
                if (hddpMasterData.value != "") {
                    var str_array = hddpMasterData.value.split(',');
                    for (var i = 0; i < str_array.length; i++) {
                        if (str_array[i].split('|')[0] == hddpCheckData.value) {
                            txtgStyleCode.value = str_array[i].split('|')[1];
                            txtgAddress.value = str_array[i].split('|')[2];
                            hddgStyleCode.value = str_array[i].split('|')[1];
                            hddgAddress.value = str_array[i].split('|')[2];
                            chk = true;
                            break;
                        }
                    }
                }
                if (chk == false) {
                    txt.value = "";
                    txtgStyleCode.value = "";
                    txtgAddress.value = "";
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
                    txtgStyleCode.value = "";
                    txtgAddress.value = "";
                    OpenDialogError(hddMSGCheckProjectPriceList.value);
                    return;
                }

                chk = true;
                var pDup;
                var grid = document.getElementById("<%= grdView2.ClientID %>");
                if (grid != null) {
                    var count = grid.rows.length;
                    if (count != null) {
                        if (count > 2)
                        {
                            for (var i = 0; i < grid.rows.length; i++) {
                                inputs = grid.rows[i].getElementsByTagName("input");
                                for (var j = 0; j < inputs.length; j++) {
                                    if (inputs[j].type == "text") {
                                        if (j == "0") {
                                            if (i != IDs){
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
                    txtgStyleCode.value = "";
                    txtgAddress.value = "";
                    OpenDialogError("<%# Me.GetResource("msg_duplicate_table", "MSG", "1") %>" + pDup);
                    return;
                }
            }
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
    </script>
</asp:Content>

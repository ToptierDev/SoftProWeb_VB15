<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ORG_SetTypeHouse.aspx.vb" Inherits="SPW.Web.UI.ORG_SetTypeHouse" EnableEventValidation="false" %>

<%@ Import Namespace="SPW.Helper" %>
<%@ Import Namespace="SPW.BLL" %>
<%@ Import Namespace="SPW.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta content="IE=9" http-equiv="X-UA-Compatible">
    <style type="text/css">
        .modal-dialog {
            position: fixed;
            top: 35%;
            left: 40%;
        }

        .FixImage {
            background-repeat: no-repeat;
            background-attachment: fixed;
            max-width: 400px;
            max-height: 350px;
            background-size: cover;
        }

        .FixBTNImage {
            background-repeat: no-repeat;
            background-attachment: fixed;
            max-width: 90px;
            max-height: 90px;
            background-size: cover;
            text-align: center;
            vertical-align: middle;
        }

        .CssdivImage {
            float: left;
            width: 100px;
            height: 100px;
            white-space: nowrap;
            text-align: center;
            vertical-align: middle;
        }

        input.multi {
            display: none;
        }

        .img-responsive {
         
           width: auto;
    max-width: 90%;
    max-height: 300px;
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
    height:350px
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
    <script>
        var resLanguage = {};
        $(function () {
            setLanguage();
        })
        function setLanguage() {
            resLanguage.resDeleteImage = '<%=grtt("resDeletePicture")%>';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" >
                <asp:HiddenField ID="hddParameterMenuID" runat="server" />
                <asp:HiddenField ID="hddReloadGrid" runat="server" />
                <asp:HiddenField ID="hddpSortBy" runat="server" />
                <asp:HiddenField ID="hddpSortType" runat="server" />
                <asp:HiddenField ID="hddpPagingDefault" runat="server" />
                <asp:HiddenField ID="hddKeyID" runat="server" />
                <asp:HiddenField ID="hddpPageInfo" runat="server" />
                <asp:HiddenField ID="hddpSearch" runat="server" />
                <asp:HiddenField ID="hddpClientID" runat="server" />
                <asp:HiddenField ID="hddpKeyIDPicture" runat="server" />
                <asp:HiddenField ID="hddpNamePicture" runat="server" />
                <asp:HiddenField ID="hddpIdMax" runat="server" />
                <asp:HiddenField ID="hddMSGAddFile" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteFile" runat="server" />
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
                <asp:HiddenField ID="hddpTypeBrownser" runat="server" />
                <asp:HiddenField ID="hddCheckUsedMaster" runat="server" />
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
                                                            <a id="btnhrefAdd" runat="server" href="javascript:CallAddData();" class="btn btn-info glyph-icon icon-plus tooltip-button" Visible='<%#Me.GetPermission().isAdd %>'></a>
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
                                                                    <asp:Label ID="lblsTypeHouse" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                    <%--<asp:TextBox ID="txtsTypeHouse" autocomplete="off" runat="server" BackColor="#ffe0c0" CssClass="form-control" onkeypress="return AutocompletedTypeHouse(this,event);" onClick="AutocompletedTypeHouse(this,event);" Style="display: none;"></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="ddlsTypeHouse" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                    <asp:Label ID="lblMassage6" runat="server" ForeColor="Red"></asp:Label>
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
                                                                Dim lc As New List(Of SetTypeHouse_ViewModel)
                                                                If hddReloadGrid.Value <> String.Empty Then
                                                                    If GetDataSetTypeHouse() IsNot Nothing Then
                                                                        lc = GetDataSetTypeHouse()
                                                            %>
                                                            <table id="grdView" class="table table-striped table-bordered table-hover" style="width: 100%">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd6" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd7" runat="server"></asp:Label></th>
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
                                                                    </tr>
                                                                </thead>
                                                                <tfoot style="display: none;">
                                                                    <tr>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt6" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt7" runat="server"></asp:Label></th>
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
                                                                    </tr>
                                                                </tfoot>
                                                                <tbody>
                                                                    <%
                        Dim i As Integer = 0
                        For Each sublc As SetTypeHouse_ViewModel In lc
                            Dim strb As New StringBuilder()
                                                                    %>
                                                                    <tr>
                                                                        <%
                                                                            strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-edit'  href='javascript:CallEditData(&#39;" + IIf(sublc.TypeHouseCode <> String.Empty, sublc.TypeHouseCode, String.Empty) + "&#39;);'></a></td>")
                                                                            Dim arr As String = hddCheckUsedMaster.Value
                                                                            If arr.IndexOf(sublc.TypeHouseCode) = -1 Then
                                                                                strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-danger glyph-icon icon-trash' href='javascript:ConfirmDelete(&#39;" + IIf(sublc.TypeHouseCode <> String.Empty, sublc.TypeHouseCode, String.Empty) + "&#39;);'></a></td>")
                                                                            Else
                                                                                strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                            End If
                                                                            strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                            strb.Append("<td style='width:20%;'>" + IIf(sublc.TypeDescription <> String.Empty, sublc.TypeDescription, String.Empty) + "</td>")
                                                                            strb.Append("<td style='width:30%;'>" + IIf(sublc.TypeHouseCode <> String.Empty, sublc.TypeHouseCode, String.Empty) + "</td>")
                                                                            strb.Append("<td style='width:30%;'>" + IIf(sublc.TypeHouseName <> String.Empty, sublc.TypeHouseName, String.Empty) + "</td>")
                                                                            strb.Append("<td style='width:20%;'>" + IIf(sublc.TypeCreate <> String.Empty, sublc.TypeCreate, String.Empty) + "</td>")

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
                                                                                <asp:Button ID="btnSaveTemp" runat="server" OnClientClick="javascript:CheckData();return false" CssClass="hide" />
                                                                                <%If (hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit) Then %>
                                                                                <a id="btnhrefSave" runat="server" href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button"></a>
                                                                                <%End if %>
                                                                                <a id="btnhrefCancel" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaTypeHouseCode" runat="server"></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-12 col-sm-12">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="width: 70%;">
                                                                                        <asp:TextBox ID="txtaTypeHouseCode" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="30" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipTypeHouseCode") %>'></asp:TextBox>
                                                                                        <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                                    </td>
                                                                                    <td style="width: 30%;">
                                                                                        <asp:DropDownList ID="ddlaTypeCreate" runat="server" CssClass="chosen-select"></asp:DropDownList>

                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div class="col-lg-1 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaTypeHouse" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-12 col-sm-12">
                                                                            <%--<asp:TextBox ID="txtaTypeHouse" autocomplete="off" runat="server" BackColor="#ffe0c0" CssClass="form-control" onkeypress="return AutocompletedTypeHouseAdd(this,event);" onClick="AutocompletedTypeHouseAdd(this,event);"  Style="display: none;"></asp:TextBox>--%>
                                                                            <asp:DropDownList ID="ddlaTypeHouse" runat="server" CssClass="chosen-select"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaNameEN" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaNameEN" autocomplete="off" runat="server" CssClass="form-control" MaxLength="80"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaNameTH" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaNameTH" autocomplete="off" runat="server" CssClass="form-control" MaxLength="80"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaNames" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaNames" autocomplete="off" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: center;">
                                                                            <asp:CheckBox ID="chkaBOI" runat="server" />
                                                                        </div>
                                                                        <div class="col-lg-1 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaModel" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaModel" autocomplete="off" runat="server" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <hr style="width: 100%; border: 0.2px dashed; border-color: #cccccc" />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblsUnit" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaUnitEN" autocomplete="off" runat="server" CssClass="form-control" placeholder="(English)" MaxLength="10"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaUnitThai" autocomplete="off" runat="server" CssClass="form-control" placeholder="(Thai)" MaxLength="10"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaSQW" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaSQW" autocomplete="off" runat="server" CssClass="form-control Text_right" onKeyPress="return Check_Key_Decimal(this,event);" onblur="CalRecord();return setFormat(this,event);"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <%--<table style="width:100%;">
                                                                                <tr>
                                                                                    <td style="width:30%;">
                                                                                        &nbsp;
                                                                                    </td>
                                                                                    <td style="width:70%;">
                                                                                        <asp:Label ID="lblaSQM" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>--%>
                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                &nbsp;
                                                                            </div>
                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                <asp:Label ID="lblaSQM" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaSQM" autocomplete="off" runat="server" CssClass="form-control Text_right" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat(this,event);"></asp:TextBox>
                                                                        </div>
                                                                        <%--<div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="width: 30%;">
                                                                                        <asp:TextBox ID="txtaSQM2" autocomplete="off" runat="server" CssClass="form-control Text_right" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat0(this,event);"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 70%;">
                                                                                        <asp:Label ID="lblaSQM2" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>--%>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaBSQM" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaBSQM" autocomplete="off" runat="server" CssClass="form-control Text_right" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat(this,event);"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                &nbsp;
                                                                            </div>
                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                <asp:Label ID="lblaPSQM" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaPSQM" autocomplete="off" runat="server" CssClass="form-control Text_right" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat(this,event);"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                &nbsp;
                                                                            </div>
                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                <asp:Label ID="lblaMeter" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaMeter" autocomplete="off" runat="server" CssClass="form-control Text_right" MaxLength="50"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:Label ID="lblaBedRoom" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaBedRoom" autocomplete="off" runat="server" CssClass="form-control Text_right" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat0(this,event);"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                &nbsp;
                                                                            </div>
                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                <asp:Label ID="lblaBathRoom" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <asp:TextBox ID="txtaBathRoom" autocomplete="off" runat="server" CssClass="form-control Text_right" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat0(this,event);"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                &nbsp;
                                                                            </div>
                                                                            <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                <asp:Label ID="lblaMeterWater" runat="server"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            <table>
                                                                                <tr>
                                                                                    <td style="width: 80%">
                                                                                        <asp:TextBox ID="txtaMeterWater" autocomplete="off" runat="server" CssClass="form-control Text_right" onKeyPress="return Check_Key_Decimal(this,event)" onblur="return setFormat0(this,event);"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 20%">
                                                                                        <asp:Label ID="lblaNew" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                    <hr style="width: 100%; border: 0.2px dashed; border-color: #cccccc" />
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <div class="content-box tabs" style="border: none;">
                                                                                <h3 class="content-box-header bg-blue-alt">
                                                                                    <span>&nbsp;</span>
                                                                                    <ul style="border: none;">
                                                                                        <li style="border: none;">
                                                                                            <a href="#tabs-example-1" style="border: none;">
                                                                                                <asp:Label ID="lblMain6" runat="server"></asp:Label>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li style="border: none;">
                                                                                            <a href="#tabs-example-2" style="border: none;">
                                                                                                <asp:Label ID="lblMain7" runat="server"></asp:Label>
                                                                                            </a>
                                                                                        </li>
                                                                                    </ul>
                                                                                    <h3></h3>
                                                                                    <div id="tabs-example-1">  
                                                                                                <h3><%=grtt("resCurrentPicture") %></h3>
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <div class="row">
                                                                                                    <asp:Repeater ID="rptImage" runat="server">
                                                                                                        <ItemTemplate>
                                                                                                            <div class="MultiFile-label col-sm-6 ">
                                                                                                                <div class="multifile-imagecontainer ">

                                                                                                                    <a class="btn btn-danger" onclick="HidePicture(this);"><i class="glyph-icon icon-trash"></i><%=grtt("resDeletePicture") %></a>
                                                                                                                   
                                                                                                                    <input type="checkbox" id="chkDeleteImage" runat="server" class="hide" />
                                                                                                                     <br />
                                                                                                                    <asp:Image ID="imgEdit" runat="server" CssClass="img-responsive " onclick="window.open($(this).attr('src'), '_blank');"/>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:Repeater>
                                                                                                </div>
                                                                                                <br />
                                                                                                <hr class="width-100" />
                                                                                                <h3><%=grtt("resNewPicture") %></h3>
                                                                                                <div class="row">
                                                                                                    <asp:FileUpload ID="fileUploadMulti" runat="server" class="multi nutClick" accept=".jpg,.jpeg,.gif,.png" />


                                                                                                      <div class="MultiFile-label col-sm-6">
                                                                                                                <div class="multifile-imagecontainer">
                                                                                                        <img src="image/addpicture.png" class="img-responsive" onclick="$('.nutClick').trigger('click');" />
                                                                                                    </div>   </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>

                                                                                        <%--<div class="row float-right">
                                                                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="hide" />
                                                                                                <a id="btnhrefAddPicture" runat="server" class="btn btn-info tooltip-button" href="javascript:CallAddPicture();"><i class="glyph-icon icon-image"></i>
                                                                                                <asp:Label ID="lblsPicture" runat="server"></asp:Label>
                                                                                                </a><a id="btnhrefAddDelete" runat="server" class="btn btn-danger tooltip-button" href="javascript:CallDeletePicture();"><i class="glyph-icon icon-trash"></i>
                                                                                                <asp:Label ID="lblsDeletePicture" runat="server"></asp:Label>
                                                                                                </a>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                                <table style="width: 100%;">
                                                                                                    <tr>
                                                                                                        <td align="center" style="vertical-align: middle;">
                                                                                                            <table style="width: 400px; height: 350px;">
                                                                                                                <tr>
                                                                                                                    <td style="text-align: center; vertical-align: middle;">
                                                                                                                        <asp:ImageButton ID="imgPic" runat="server" CssClass="FixImage" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </div>
                                                                                        </div>
                                                                                        <hr />
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                                <asp:Panel ID="pnImage" runat="server">
                                                                                                    <asp:Repeater ID="repeaterImageID" runat="server">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Panel ID="divImageID" runat="server" CssClass="CssdivImage">
                                                                                                                <asp:ImageButton ID="btnImg" runat="server" CssClass="FixBTNImage" />
                                                                                                            </asp:Panel>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:Repeater>
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>--%>
                                                                                    </div>
                                                                                    <div id="tabs-example-2">
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                                <asp:TextBox ID="txtaNote" runat="server" CssClass="form-control" MaxLength="80" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </h3>
                                                                            </div>
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
                                <asp:TextBox ID="lblBodydelete" runat="server" BackColor="White" BorderStyle="None" BorderWidth="0" Enabled="false" Style="width: 100%"></asp:TextBox>
                            </div>
                            <div class="modal-footer">
                                <button id="btnConfrimDelete" runat="server" type="button" class="btn btn-danger" onclick="javascript:CallDeleteData();"></button>
                                <button id="btnConfrimCancel" runat="server" type="button" class="btn btn-default" data-dismiss="modal"></button>
                            </div>
                        </div>
                    </div>
                </div>
            <asp:Button ID="btnSearch" runat="server" CssClass="hide" />
            <asp:Button ID="btnAdd" runat="server" CssClass="hide" />
            <asp:Button ID="btnEdit" runat="server" CssClass="hide" />
            <asp:Button ID="btnDelete" runat="server" CssClass="hide" />
            <asp:Button ID="btnSave" runat="server" CssClass="hide" />
            <%--                <asp:Button ID="btnFileUpload" runat="server" CssClass="hide" />
                <asp:Button ID="btnDeletePicture" runat="server" CssClass="hide" />--%>
            <asp:Button ID="btnCancel" runat="server" CssClass="hide" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <%--            <asp:PostBackTrigger ControlID="btnFileUpload" />
            <asp:PostBackTrigger ControlID="btnDeletePicture" />--%>
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
    <script src="Scripts_custom/jquery.MultiFile_customimage.js?v=<%=Me.assetVersion %>"></script>

    <script type="text/javascript">
     
        $(document).ready(function () {
            SetInitial();
        });
        function SetInitial() {
            var isEdit = <%#Me.GetPermission().isEdit.ToString.ToLower %>;
            var isAdd = <%#Me.GetPermission().isAdd.ToString.ToLower %>;
            var hddKeyID = document.getElementById('<%=hddKeyID.ClientID%>')
            if (isEdit == false && hddKeyID.value != ""){
                $("input").addClass('disabled');
                $("select").attr('disabled', 'disabled');
            }else if(isAdd == true && hddKeyID.value == ""){
                $("input").removeClass('disabled');
                $("select").removeAttr('disabled');
            }

            $(function () {
                "use strict";
                $(".tabs").tabs();
            });

            $(function () {
                "use strict";
                $(".tabs-hover").tabs({
                    event: "mouseover"
                });
            });

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

        //Auto Completed
        <%--  function AutocompletedTypeHouse(txt, e) {
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
            $("#<%=txtsTypeHouse.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("./API/Api_SetTypeHouse.asmx/GetTypeHouse")%>',
                        data: "{ 'ptKeyID': '" + request.term + "' }",
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
                    $("#<%=txtsTypeHouse.ClientID%>").val(i.item.label);
                },
                minLength: 0,
                matchContains: true
            }).on('click', function () { $(this).keydown(); });

            }--%>

        <%--    function AutocompletedTypeHouseAdd(txt, e) {
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
                $("#<%=txtaTypeHouse.ClientID%>").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("./API/Api_SetTypeHouse.asmx/GetTypeHouse")%>',
                            data: "{ 'ptKeyID': '" + request.term + "' }",
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
                        $("#<%=txtaTypeHouse.ClientID%>").val(i.item.label);
                    },
                    minLength: 0,
                    matchContains: true
                }).on('click', function () { $(this).keydown(); });

                }--%>

        <%--function AutocompletedTypeCreateAdd(txt, e) {
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
            $("#<%=txtaTypeCreate.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("./API/Api_SetTypeHouse.asmx/GetTypeCreate")%>',
                        data: "{ 'ptKeyID': '" + request.term + "' }",
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
                    $("#<%=txtaTypeCreate.ClientID%>").val(i.item.label);
                },
                minLength: 0,
                matchContains: true
            }).on('click', function () { $(this).keydown(); });

        }--%>

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

        function CallLoaddata() {

         <%--   if ($('#<%=ddlsTypeHouse.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=ddlsTypeHouse.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "";
                return;
            } else {
                $('#<%=ddlsTypeHouse.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "none";
            }--%>
            document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "none";

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
            if ($('#<%=txtaTypeHouseCode.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaTypeHouseCode.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                scrollAndFocus('#<%=txtaTypeHouseCode.ClientID%>');
                return;
            } else {
                $('#<%=txtaTypeHouseCode.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }


            CallSaveData();
        }

        function CalRecord() {
            var txtaSQW = document.getElementById("<%= txtaSQW.ClientID%>");
            var txtaSQM = document.getElementById("<%= txtaSQM.ClientID%>");
            if (txtaSQW.value == "") {
                txtaSQW.value = "0.00"
            }
            txtaSQM.value = fnFormatMoney(parseFloat(replaceAll(txtaSQW.value, ',', '')) * 4);
            if (txtaSQW.value == "0.00") {
                txtaSQW.value = ""
            }
            if (txtaSQM.value == "0.00") {
                txtaSQM.value = ""
            }
        }

        <%--function SetImageURL(strPath, pID, pName) {
            $('#<%=imgPic.ClientID%>').attr("src", "");
            $('#<%=imgPic.ClientID%>').attr("src", strPath);
            var hddpKeyIDPicture = document.getElementById("<%= hddpKeyIDPicture.ClientID%>");
            var hddpNamePicture = document.getElementById("<%= hddpNamePicture.ClientID%>");
            hddpKeyIDPicture.value = pID;
            hddpNamePicture.value = pName;
            $('#<%=imgPic.ClientID%>').attr("onClick", "CallLoadHrefNewtab('" + strPath + "');");
            return false
        }--%>

        function CallLoadHrefNewtab(Urls) {
            window.open(Urls, '_blank');
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

        function HidePicture(el) {
            $(el).next().prop('checked', true).parent().parent().hide();
        }
    </script>
</asp:Content>

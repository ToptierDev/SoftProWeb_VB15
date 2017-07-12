<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="MST_UserInformation.aspx.vb" Inherits="SPW.Web.UI.MST_UserInformation"
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

        .img-attach {
            background-repeat: no-repeat;
            background-attachment: fixed;
            width: 100%;
            max-height: 150px;
            background-size: cover;
        }
               input.multi {
            display: none;
        }

        .img-responsive {
         
           width: auto;
    max-width: 90%;
    max-height: 200px;
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
    height:250px
        }
       .MultiFile-label.col-sm-6 {
            margin-top: 17px;
            }
       .MultiFile-label a.btn{
           float:right;
           margin-bottom:5px;
               z-index: 1;
       }

       select.chosen-select.disabled-aslabel[disabled] + div.chosen-container {
            border: 0;
        }

        select.chosen-select.disabled-aslabel[disabled] + div.chosen-container span {
            color: black;
            margin-left: -10px;
            cursor: default;
            font-weight: bold;
        }

        select.chosen-select.disabled-aslabel[disabled].disabled-aslargelabel + div.chosen-container span {
            font-size: 20px;
        }

        select.chosen-select.disabled-aslabel[disabled] + div.chosen-container div {
            display: none;
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
                <asp:HiddenField ID="hddKeyID" runat="server" />
                <asp:HiddenField ID="hddUserID" runat="server" />
                <asp:HiddenField ID="hddIDCardNo" runat="server" />
                <asp:HiddenField ID="hddpPageInfo" runat="server" />
                <asp:HiddenField ID="hddpSearch" runat="server" />
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
                <asp:HiddenField ID="hddpFlagSearch" runat="server" />
                <asp:HiddenField ID="hddpAutoName1" runat="server" />
                <asp:HiddenField ID="hddpAutoCode1" runat="server" />
                <asp:HiddenField ID="hddpAutoName2" runat="server" />
                <asp:HiddenField ID="hddpAutoCode2" runat="server" />
                <asp:HiddenField ID="hddMsgIDCardNotEmp" runat="server" />
                <asp:HiddenField ID="hddMsgIDCardUseUser" runat="server" />
                <asp:HiddenField ID="hddMsgIDCardNotSave" runat="server" />
                <asp:HiddenField ID="hddMsgEmployeeNotEmp" runat="server" />
                <asp:HiddenField ID="hddMsgEmployeeUseUser" runat="server" />
                <asp:HiddenField ID="hddMsgEmployeeNotSave" runat="server" />
                <asp:HiddenField ID="hddMsgPassword" runat="server" />
                <asp:HiddenField ID="hddProject" runat="server" />
                <asp:HiddenField ID="hddCompany" runat="server" />
                <asp:HiddenField ID="hddpTypeBrownser" runat="server" />
                <asp:HiddenField ID="hddpFlagRegister" runat="server" />
                <asp:HiddenField ID="hddpEmployeeID" runat="server" />
                <asp:HiddenField ID="hddpLG" runat="server" />
                <asp:HiddenField ID="hddParameterMenuID2" runat="server" />
                <asp:HiddenField ID="hddaSection" runat="server" />
                <asp:HiddenField ID="hddaComID" runat="server" />
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
                                                        <div class="col-lg-1 col-md-1 col-sm-1  " style="text-align: right;">
                                                            <a href="javascript:CallAddData();" class="btn btn-info glyph-icon icon-typicons-plus-outline tooltip-button <%=IIf(Me.GetPermission().isAdd, "", "hide") %>" title="<% Response.Write(hddMSGAddData.Value) %>"></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-10" style="display: none;">
                                                        <div class="row">
                                                            <div class="col-lg-8 col-md-12 col-sm-12">
                                                                <div class="row">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblsKeyword" runat="server" Text="Label"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtsKeyword" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <div class="col-lg-2 col-md-12 col-sm-12"></div>
                                                                                <div class="col-lg-10 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblsKeywordDetail" runat="server" Text="Label" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-md-12 col-sm-12">

                                                                <a href="javascript:CallLoaddata();" class="btn  btn-info">
                                                                    <i class="glyph-icon icon-search"></i>
                                                                    <asp:Label ID="lblsSearch" runat="server"></asp:Label>
                                                                </a>
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

                                                                Dim lcCusInfo As New List(Of CoreUser)
                                                                If hddReloadGrid.Value <> String.Empty Then
                                                                    If GetDataUserInfo() IsNot Nothing Then
                                                                        lcCusInfo = GetDataUserInfo()

                                                            %>
                                                            <table id="grdView" class="table table-striped table-bordered table-hover" style="width: 100%">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd8" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd9" runat="server"></asp:Label></th>
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
                                                                    </tr>
                                                                </thead>
                                                                <tfoot style="display: none;">
                                                                    <tr>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt8" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt9" runat="server"></asp:Label></th>
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
                                                                    </tr>
                                                                </tfoot>
                                                                <tbody>
                                                                    <%


                                                                        Dim lstDep As List(Of BD11DEPT) = GetDataDep()
                                                                        Dim lstDiv As List(Of CoreDivision) = GetDataDiv()
                                                                        Dim lstPos As List(Of CorePosition) = GetDataPos()
                                                                        Dim lcDep As BD11DEPT = Nothing
                                                                        Dim lcDiv As CoreDivision = Nothing
                                                                        Dim lcPos As CorePosition = Nothing
                                                                        Dim DepName As String = String.Empty
                                                                        Dim DivName As String = String.Empty
                                                                        Dim PosName As String = String.Empty
                                                                        Dim i As Integer = 0
                                                                        For Each sublcCusInfo As CoreUser In lcCusInfo
                                                                            Dim strb As New StringBuilder()
                                                                            DepName = String.Empty
                                                                            DivName = String.Empty
                                                                            PosName = String.Empty
                                                                    %>
                                                                    <tr>
                                                                        <%
                                                                            strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-typicons-edit' href='javascript:CallEditData(&#39;" + Convert.ToString(sublcCusInfo.UserID) + "&#39;);'></a></td>")
                                                                            strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-danger glyph-icon icon-typicons-trash' href='javascript:ConfirmDelete(&#39;" + Convert.ToString(sublcCusInfo.UserID) + "&#39;,&#39;" + Convert.ToString(sublcCusInfo.UserID) + "&#39;);'></a></td>")
                                                                            strb.Append("<td style='width:1%;text-align: center;'></td>")
                                                                            strb.Append("<td style='width:12%;'>" + IIf(sublcCusInfo.UserID <> String.Empty, sublcCusInfo.UserID, String.Empty) + "</td>")
                                                                            strb.Append("<td style='width:10%;'>" + Convert.ToString(sublcCusInfo.EmployeeNo) + "</td>")
                                                                            Dim sFullName As String = ""
                                                                            If Me.WebCulture = "TH" Then
                                                                                sFullName = IIf(sublcCusInfo.TitleThai <> String.Empty, sublcCusInfo.TitleThai, String.Empty)
                                                                                If sFullName <> "" Then sFullName = sFullName & IIf(sublcCusInfo.NameThai <> String.Empty, sublcCusInfo.NameThai, String.Empty)
                                                                                If sFullName <> "" Then sFullName = sFullName & " " & IIf(sublcCusInfo.LastnameThai <> String.Empty, sublcCusInfo.LastnameThai, String.Empty)
                                                                            Else
                                                                                sFullName = IIf(sublcCusInfo.TitleEng <> String.Empty, sublcCusInfo.TitleEng, String.Empty)
                                                                                If sFullName <> "" Then sFullName = sFullName & IIf(sublcCusInfo.NameEng <> String.Empty, sublcCusInfo.NameEng, String.Empty)
                                                                                If sFullName <> "" Then sFullName = sFullName & " " & IIf(sublcCusInfo.LastnameEng <> String.Empty, sublcCusInfo.LastnameEng, String.Empty)
                                                                            End If
                                                                            strb.Append("<td style='width:39%;'>" + IIf(sFullName <> String.Empty, sFullName, String.Empty) + "</td>")
                                                                            If sublcCusInfo.Department <> String.Empty Then
                                                                                If lstDep IsNot Nothing Then
                                                                                    lcDep = lstDep.Where(Function(s) s.FDPCODE = sublcCusInfo.Department.ToString).SingleOrDefault
                                                                                    If lcDep IsNot Nothing Then
                                                                                        DepName = lcDep.FDPNAME.ToString
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                            If sublcCusInfo.Division <> String.Empty Then
                                                                                If lstDiv IsNot Nothing Then
                                                                                    lcDiv = lstDiv.Where(Function(s) s.DivisionCode = sublcCusInfo.Division.ToString).SingleOrDefault
                                                                                    If lcDiv IsNot Nothing Then
                                                                                        DivName = lcDiv.DivisionName.ToString
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                            If sublcCusInfo.Position <> String.Empty Then
                                                                                If lstPos IsNot Nothing Then
                                                                                    lcPos = lstPos.Where(Function(s) s.PositionCode = sublcCusInfo.Position.ToString).SingleOrDefault
                                                                                    If lcPos IsNot Nothing Then
                                                                                        PosName = lcPos.PositionName.ToString
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                            strb.Append("<td style='width:12%;'>" + IIf(DepName <> String.Empty, DepName, IIf(sublcCusInfo.Department <> String.Empty, sublcCusInfo.Department, String.Empty)) + "</td>")
                                                                            strb.Append("<td style='width:12%;'>" + IIf(DivName <> String.Empty, DivName, IIf(sublcCusInfo.Division <> String.Empty, sublcCusInfo.Division, String.Empty)) + "</td>")
                                                                            strb.Append("<td style='width:12%;'>" + IIf(PosName <> String.Empty, PosName, IIf(sublcCusInfo.Position <> String.Empty, sublcCusInfo.Position, String.Empty)) + "</td>")

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
                                <asp:Panel ID="pnDialog" runat="server" Style="display: none;">
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
                                                                                    <a href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button" title="<% Response.Write(hddMSGSaveData.Value) %>"></a>
                                                                                <%End if %>
                                                                                <a href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button" title="<% Response.Write(hddMSGCancelData.Value) %>"></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:HiddenField ID="hddaImgPicture" runat="server" />
                                                                        <div class="panel-body">
                                                                            <div class="example-box-wrapper">
                                                                                <ul class="nav-responsive nav nav-tabs">
                                                                                    <li id="liTab1" runat="server" class="active"><a href="#tabUSER1" data-toggle="tab">
                                                                                        <asp:Label ID="lblTabUSER1" runat="server" Text=""></asp:Label></a></li>
                                                                                    <li id="liTab2" runat="server"><a href="#tabUSER2" data-toggle="tab">
                                                                                        <asp:Label ID="lblTabUSER2" runat="server" Text=""></asp:Label></a></li>
                                                                                    <li id="liTab3" runat="server"><a href="#tabUSER3" data-toggle="tab">
                                                                                        <asp:Label ID="lblTabUSER3" runat="server" Text=""></asp:Label></a></li>
                                                                                </ul>
                                                                                <div class="tab-content">
                                                                                    <div class="tab-pane active" id="tabUSER1">
                                                                                        <div class="panel-body">
                                                                                            <div class="row">
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <asp:Label ID="lblaUserID" runat="server"></asp:Label><asp:Label ID="Label10" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtaUserID" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="10" TabIndex="0" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipUserID") %>'></asp:TextBox>
                                                                                                    <asp:Label ID="lblMassage9" runat="server" ForeColor="Red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:Label ID="lblaDepartment" runat="server"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:DropDownList ID="ddlaDepartment" CssClass="chosen-select" runat="server" TabIndex="11"></asp:DropDownList>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <asp:Label ID="lblaIDCardNo" runat="server"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:Panel ID="PanelIDCardNo" runat="server" DefaultButton="btnReload1">
                                                                                                        <asp:TextBox ID="txtaIDCardNo" autocomplete="off" runat="server" BackColor="#FFE0C0" CssClass="form-control tooltip-button" MaxLength="30" onblur="CallReload1();" TabIndex="0" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipIDCardNo") %>'></asp:TextBox>
                                                                                                        <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                                                    </asp:Panel>
                                                                                                </div>
                                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:Label ID="lblaDivision" runat="server"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:DropDownList ID="ddlaDivision" CssClass="chosen-select" runat="server" TabIndex="12"></asp:DropDownList>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <asp:Label ID="lblaEmployeeNo" runat="server"></asp:Label><asp:Label ID="Label8" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:Panel ID="PanelEmployeeNo" runat="server" DefaultButton="btnReload2">
                                                                                                        <asp:TextBox ID="txtaEmployeeNo" autocomplete="off" runat="server" BackColor="#FFE0C0" CssClass="form-control tooltip-button" MaxLength="30" onblur="CallReload2();" TabIndex="1" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipEmployeeNo") %>'></asp:TextBox>
                                                                                                        <asp:HiddenField ID="hddaEmployeeNo" runat="server" />
                                                                                                        <asp:Label ID="lblMassage8" runat="server" ForeColor="Red"></asp:Label>
                                                                                                    </asp:Panel>
                                                                                                </div>
                                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:Label ID="lblaPosition" runat="server"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:DropDownList ID="ddlaPosition" CssClass="chosen-select" runat="server" TabIndex="13"></asp:DropDownList>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                                                                    <ContentTemplate>
                                                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                            <asp:Label ID="lblaPassword" runat="server"></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                            <asp:TextBox ID="txtaPassword" autocomplete="off" runat="server" placeholder="Enter your password" CssClass="form-control tooltip-button" MaxLength="100" TextMode="Password" TabIndex="2" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipPassword") %>'></asp:TextBox>
                                                                                                            <asp:HiddenField ID="hddaPassword" runat="server" />
                                                                                                            <asp:HiddenField ID="hddaPasswordOld" runat="server" />
                                                                                                            <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                            &nbsp;
                                                                                                        </div>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:Label ID="lblaPhoneNo" runat="server"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtaPhoneNo" autocomplete="off" runat="server" CssClass="form-control" MaxLength="100" TabIndex="14"></asp:TextBox>
                                                                                                </div>

                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                                                                    <ContentTemplate>
                                                                                                        <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                            <asp:Label ID="lblaConfrimPassword" runat="server"></asp:Label><asp:Label ID="Label3" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                                        </div>
                                                                                                        <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                            <asp:TextBox ID="txtaConfrimPassword" autocomplete="off" runat="server" placeholder="Enter your confrim password" CssClass="form-control tooltip-button" MaxLength="100" TextMode="Password" TabIndex="3" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipConfrimPassword") %>'></asp:TextBox>
                                                                                                            <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
                                                                                                        </div>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:Label ID="lblaExtension" runat="server"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtaExtension" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30" TabIndex="15"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <asp:Label ID="lblaTitleEng" runat="server"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtaTitleEng" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30" TabIndex="4"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:Label ID="lblaEmail" runat="server"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtaEmail" autocomplete="off" runat="server" CssClass="form-control" MaxLength="100" TabIndex="16"></asp:TextBox>
                                                                                                </div>

                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <asp:Label ID="lblaNameEng" runat="server"></asp:Label><asp:Label ID="Label4" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtaNameEng" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="100" TabIndex="5" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipNameEng") %>'></asp:TextBox>
                                                                                                    <asp:Label ID="lblMassage4" runat="server" ForeColor="Red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:Label ID="lblaResignDate" runat="server"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:CheckBox ID="chbaResignDate" runat="server" TabIndex="17" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <asp:Label ID="lblaLastnameEng" runat="server"></asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtaLastnameEng" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="100" TabIndex="6" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipLastnameEng") %>'></asp:TextBox>
                                                                                                    <asp:Label ID="lblMassage5" runat="server" ForeColor="Red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <asp:Label ID="lblaTitleThai" runat="server"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtaTitleThai" autocomplete="off" runat="server" CssClass="form-control" MaxLength="30" TabIndex="7"></asp:TextBox>
                                                                                                </div>
                                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <asp:Label ID="lblaNameThai" runat="server"></asp:Label><asp:Label ID="Label6" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtaNameThai" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="100" TabIndex="8" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipNameThai") %>'></asp:TextBox>
                                                                                                    <asp:Label ID="lblMassage6" runat="server" ForeColor="Red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <asp:Label ID="lblaTrusteeLike" runat="server" Style="display: none;"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: center;">
                                                                                                    <asp:TextBox ID="txtaTrusteeLike" autocomplete="off" runat="server" CssClass="form-control" MaxLength="255" Style="display: none;"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <asp:Label ID="lblaLastnameThai" runat="server"></asp:Label><asp:Label ID="Label7" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtaLastnameThai" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="100" TabIndex="9" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipLastnameThai") %>'></asp:TextBox>
                                                                                                    <asp:Label ID="lblMassage7" runat="server" ForeColor="Red"></asp:Label>
                                                                                                </div>
                                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    &nbsp;
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                    <%=grtt("resPicture") %>
                                                                                                </div>

                                                                                                <div class="MultiFile-label col-lg-4 col-md-12 col-sm-12">
                                                                                                    <div class="multifile-imagecontainer">
                                                                                                         <a class="btn btn-info"
                                                                                                                    id="btnAddFileUploadPicture" onclick="triggerClick('FileUploadPicture'); ">

                                                                                                                    <i class="glyph-icon  icon-image"></i><%=grtt("resAddImage") %></a>

                                                                                                                <a class="btn btn-danger" style="display: none;"
                                                                                                                    id="btnClearFileUploadPicture" for="FileUploadPicture" onclick="fn_clearValue(this); ">
                                                                                                                    <i class="glyph-icon  icon-close"></i><%=grtt("resClearImage") %></a>
                                                                                                           <asp:Image ID="imgaPicture" for="FileUploadPicture" runat="server"
                                                                                                                        data-default="image/no_image_icon.png"
                                                                                                                        ImageUrl="image/no_image_icon.png" class="img-responsive"   />
                                                                                                                    <asp:FileUpload ID="FileUploadPicture"
                                                                                                                        class="hidden"
                                                                                                                        runat="server"
                                                                                                                        accept="image/*;"
                                                                                                                        onchange="PreviewImage(this);"></asp:FileUpload>
                                                                                                                <asp:CheckBox runat="server" for="FileUploadPicture" ID="chkDeletePicture" CssClass="hidden" />
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <%=grtt("resSignature") %>
                                                                                                </div>
                                                                                                <div class="MultiFile-label col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                    <div class="multifile-imagecontainer">
                                                                                                        <a class="btn btn-info"
                                                                                                            id="btnAddFileUploadSignature" onclick="triggerClick('FileUploadSignature'); ">

                                                                                                            <i class="glyph-icon  icon-image"></i><%=grtt("resAddSignature") %></a>

                                                                                                        <a class="btn btn-danger" style="display: none;"
                                                                                                            id="btnClearFileUploadSignature" for="FileUploadSignature" onclick="fn_clearValue(this); ">
                                                                                                            <i class="glyph-icon  icon-close"></i><%=grtt("resClearSignature") %></a>
                                                                                                        <asp:CheckBox runat="server" for="FileUploadSignature" ID="chkDeleteSignature" CssClass="hidden" />
                                                                                                        <asp:Image ID="imgaSignature" for="FileUploadSignature" runat="server"
                                                                                                            data-default="image/no_signature_icon.png"
                                                                                                            ImageUrl="image/no_signature_icon.png" class="img-responsive" />
                                                                                                        <asp:FileUpload ID="FileUploadSignature"
                                                                                                            class="hidden"
                                                                                                            runat="server"
                                                                                                            accept="image/*;"
                                                                                                            onchange="PreviewImage(this);"></asp:FileUpload>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="tab-pane " id="tabUSER2">
                                                                                        <div class="panel-body">
                                                                                            <div class="row">
                                                                                                <div class="form-group">
                                                                                                    <div class="col-lg-10 col-md-12 col-sm-12">

                                                                                                        <div class="ms-container">
                                                                                                            <div style="float: left;"><%=grtt("resAll") %></div>
                                                                                                            <div style="float: right;"><%=grtt("resSelectedProject") %></div>
                                                                                                        </div>

                                                                                                        <% 
                                                                                                            Dim chkAdd As Boolean = False
                                                                                                            Dim blProject As cUserInformation = New cUserInformation
                                                                                                            Dim lcProject As List(Of ED01PROJ) = blProject.GetProjectAll()
                                                                                                            If lcProject IsNot Nothing Then %>
                                                                                                        <div class="ms-container">
                                                                                                            <select multiple="multiple" class="multi-select" name="dualListboxProject">
                                                                                                                <%  For Each subProject As ED01PROJ In lcProject
                            chkAdd = False
                            If dtProject IsNot Nothing Then
                                For i As Integer = 0 To dtProject.Rows.Count - 1
                                    If subProject.FREPRJNO.ToString = dtProject.Rows(i)("FREPRJNO").ToString Then
                                                                                                                %>
                                                                                                                <option selected="selected" value="<%=dtProject.Rows(i)("FREPRJNO").ToString %>">
                                                                                                                    <%=dtProject.Rows(i)("FREPRJNM").ToString %>
                                                                                                                </option>
                                                                                                                <% 
                                chkAdd = True
                                Exit For
                            End If
                        Next
                        If chkAdd = False Then
                                                                                                                %>
                                                                                                                <option value="<%=subProject.FREPRJNO.ToString %>">
                                                                                                                    <%=subProject.FREPRJNM.ToString %>
                                                                                                                </option>
                                                                                                                <%  
                                                                                                                        End If
                                                                                                                    Else
                                                                                                                %>
                                                                                                                <option value="<%=subProject.FREPRJNO.ToString %>">
                                                                                                                    <%=subProject.FREPRJNM.ToString %>
                                                                                                                </option>
                                                                                                                <%  
                                                                                                                    End If %>

                                                                                                                <%  Next %>
                                                                                                            </select>
                                                                                                            <%   End If %>
                                                                                                            <i class="glyph-icon icon-exchange"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="tab-pane " id="tabUSER3">
                                                                                        <div class="panel-body">
                                                                                            <div class="row">
                                                                                                <div class="form-group">
                                                                                                    <div class="col-lg-10 col-md-12 col-sm-12">

                                                                                                           <div class="ms-container">
                                                                                                            <div style="float: left;"><%=grtt("resAll") %></div>
                                                                                                            <div style="float: right;"><%=grtt("resSelectedCompany") %></div>
                                                                                                        </div>

                                                                                                        <% 
                                                                                                            Dim chkAddCompany As Boolean = False
                                                                                                            Dim blCompany As cUserInformation = New cUserInformation
                                                                                                            Dim lcCompany As List(Of BD10DIVI) = blCompany.GetCompanyAll()
                                                                                                            If lcCompany IsNot Nothing Then %>
                                                                                                        <div class="ms-container">
                                                                                                            <select multiple="multiple" class="multi-select" name="dualListboxCompany">
                                                                                                                <%  For Each subCompany As BD10DIVI In lcCompany
                                                                                                                        chkAddCompany = False
                                                                                                                        If dtCompany IsNot Nothing Then
                                                                                                                            For i As Integer = 0 To dtCompany.Rows.Count - 1
                                                                                                                                If subCompany.COMID.ToString = dtCompany.Rows(i)("COMID").ToString Then
                                                                                                                %>
                                                                                                                <option selected="selected" value="<%=dtCompany.Rows(i)("COMID").ToString %>">
                                                                                                                    <%   If Me.WebCulture = "TH" Then %>
                                                                                                                    <%=dtCompany.Rows(i)("FDIVNAMET").ToString %>
                                                                                                                    <%   else  %>
                                                                                                                    <%=dtCompany.Rows(i)("FDIVNAME").ToString %>
                                                                                                                    <%   End If %>                                                                                                                    
                                                                                                                </option>
                                                                                                                <% 
                                                                                                                            chkAddCompany = True
                                                                                                                            Exit For
                                                                                                                        End If
                                                                                                                    Next
                                                                                                                    If chkAddCompany = False Then
                                                                                                                %>
                                                                                                                <option value="<%=subCompany.COMID.ToString %>">
                                                                                                                    <%   If Me.WebCulture = "TH" Then %>
                                                                                                                    <%=subCompany.FDIVNAMET.ToString %>
                                                                                                                    <%   else  %>
                                                                                                                    <%=subCompany.FDIVNAME.ToString %>
                                                                                                                    <%   End If %>

                                                                                                                </option>
                                                                                                                <%  
                                                                                                                        End If
                                                                                                                    Else
                                                                                                                %>
                                                                                                                <option value="<%=subCompany.FDIVNAME.ToString %>">
                                                                                                                    <%   If Me.WebCulture = "TH" Then %>
                                                                                                                    <%=subCompany.FDIVNAMET.ToString %>
                                                                                                                    <%   else  %>
                                                                                                                    <%=subCompany.FDIVNAME.ToString %>
                                                                                                                    <%   End If %>
                                                                                                                </option>
                                                                                                                <%  
                                                                                                                End If %>

                                                                                                                <%  Next %>
                                                                                                            </select>
                                                                                                            <%   End If %>
                                                                                                            <i class="glyph-icon icon-exchange"></i>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <asp:Button ID="btnReload1" runat="server" CssClass="hide" />
                                                                        <asp:Button ID="btnReload2" runat="server" CssClass="hide" />

                                                                    </ContentTemplate>
                                                                    <%--  <Triggers>
                                                                                            <asp:PostBackTrigger ControlID="btnReload1" />
                                                                                            <asp:PostBackTrigger ControlID="btnReload2" />
                                                                                        </Triggers>--%>
                                                                </asp:UpdatePanel>
                                                                <div class="panel-heading">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <h3 class="panel-title">
                                                                                    <asp:Label ID="Label9" runat="server"></asp:Label>
                                                                                </h3>
                                                                            </td>
                                                                            <td style="text-align: right;">
                                                                                <%If (hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit) Then %>
                                                                                    <a href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button" title="<% Response.Write(hddMSGSaveData.Value) %>"></a>
                                                                                <%End if %>
                                                                                <a href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button" title="<% Response.Write(hddMSGCancelData.Value) %>"></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
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
                                <asp:Label ID="lblBodydelete" runat="server"></asp:Label><asp:TextBox ID="txtKeyID" runat="server" BorderStyle="None" BorderWidth="0" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" onclick="javascript:CallDeleteData();"><% Response.Write(hddMSGDeleteData.Value) %></button>
                                <button type="button" class="btn btn-default" data-dismiss="modal"><% Response.Write(hddMSGCancelData.Value) %></button>
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
            <asp:PostBackTrigger ControlID="btnSearch" />
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
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="Script/jquery-ui.js"></script>
    <script src="js/moment-with-locales.min.js"></script>
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
                $(".multifile-imagecontainer .btn").addClass('hide');
            }else if(isAdd == true && hddKeyID.value == ""){
                $("input").removeClass('disabled');
                $("select").removeAttr('disabled');
                $(".multifile-imagecontainer .btn").removeClass('hide');
            }
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

                    if ($('#<%=hddpPageInfo.ClientID%>').val() != "") {
                        t.search($('#<%=hddpSearch.ClientID%>').val()).draw(false);
                    }

                    t.on('length.dt', function (e, settings, len) {
                        hddpPagingDefault.value = len;

                        var info = t.page.info();
                        hddpPageInfo.value = info.page;
                    });
                }
            }

            $(".multi-select").multiSelect();
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

        function ConfirmDelete(pKey, Name) {
            var hddKeyID = document.getElementById("<%= hddKeyID.ClientID%>");
            var txtKeyID = document.getElementById("<%= txtKeyID.ClientID%>");

            hddKeyID.value = pKey;
            txtKeyID.value = pKey + " ?";

            $("#DeleteModal").modal();
        }

        function CheckData() {
            var chkSave = 'Y';
            if ($('#<%=txtaUserID.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaUserID.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage9').style.display = "";
                $('.nav-tabs a:first').tab('show');
                scrollAndFocus('#<%=txtaUserID.ClientID%>');
                return;
            } else {
                $('#<%=txtaUserID.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage9').style.display = "none";
            }
            if ($('#<%=txtaIDCardNo.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaIDCardNo.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                $('.nav-tabs a:first').tab('show');
                scrollAndFocus('#<%=txtaIDCardNo.ClientID%>');
                return;
            } else {
                $('#<%=txtaIDCardNo.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }
            if ($('#<%=txtaEmployeeNo.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaEmployeeNo.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage8').style.display = "";
                $('.nav-tabs a:first').tab('show');
                scrollAndFocus('#<%=txtaEmployeeNo.ClientID%>');
                return;
            } else {
                $('#<%=txtaEmployeeNo.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage8').style.display = "none";
            }

            if ($('#<%=hddKeyID.ClientID%>').val().replace(" ", "") == "") {

                if ($('#<%=txtaPassword.ClientID%>').val().replace(" ", "") == "") {
                    $('#<%=txtaPassword.ClientID%>').addClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                    $('.nav-tabs a:first').tab('show');
                    scrollAndFocus('#<%=txtaPassword.ClientID%>');
                    return;
                } else {
                    $('#<%=txtaPassword.ClientID%>').removeClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
                }
                if ($('#<%=txtaConfrimPassword.ClientID%>').val().replace(" ", "") == "") {
                    $('#<%=txtaConfrimPassword.ClientID%>').addClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
                    $('.nav-tabs a:first').tab('show');
                    scrollAndFocus('#<%=txtaConfrimPassword.ClientID%>');
                    return;
                } else {
                    $('#<%=txtaConfrimPassword.ClientID%>').removeClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";
                }
                if ($('#<%=txtaPassword.ClientID%>').val().replace(" ", "") != $('#<%=txtaConfrimPassword.ClientID%>').val().replace(" ", "")) {
                    chkSave = 'N';
                    OpenDialogError($('#<%=hddMsgPassword.ClientID%>').val().replace(" ", ""));
                }
            }

            if ($('#<%=txtaNameEng.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaNameEng.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "";
                $('.nav-tabs a:first').tab('show');
                scrollAndFocus('#<%=txtaNameEng.ClientID%>');
                return;
            } else {
                $('#<%=txtaNameEng.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "none";
            }
            if ($('#<%=txtaLastnameEng.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaLastnameEng.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "";
                $('.nav-tabs a:first').tab('show');
                scrollAndFocus('#<%=txtaLastnameEng.ClientID%>');
                return;
            } else {
                $('#<%=txtaLastnameEng.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "none";
            }
            if ($('#<%=txtaNameThai.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaNameThai.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "";
                $('.nav-tabs a:first').tab('show');
                scrollAndFocus('#<%=txtaNameThai.ClientID%>');
                return;
            } else {
                $('#<%=txtaNameThai.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "none";
            }
            if ($('#<%=txtaLastnameThai.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaLastnameThai.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage7').style.display = "";
                $('.nav-tabs a:first').tab('show');
                scrollAndFocus('#<%=txtaLastnameThai.ClientID%>');
                return;
            } else {
                $('#<%=txtaLastnameThai.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage7').style.display = "none";
            }

            $('#<%=hddaPassword.ClientID%>').val($('#<%=txtaPassword.ClientID%>').val());
            $('#<%=hddProject.ClientID%>').val($('[name="dualListboxProject"]').val());
            $('#<%=hddCompany.ClientID%>').val($('[name="dualListboxCompany"]').val());
            $('#<%=hddaImgPicture.ClientID%>').val($('#<%=hddaImgPicture.ClientID%>').val());

            if (chkSave == 'Y') {
                CallSaveData();
            }
        }


        function CallSaveData() {
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


        function CallReload1() {
            $('#<%=hddaPassword.ClientID%>').val($('#<%=txtaPassword.ClientID%>').val());
            $('#<%=hddProject.ClientID%>').val($('[name="dualListboxProject"]').val());
            $('#<%=hddCompany.ClientID%>').val($('[name="dualListboxCompany"]').val());
            //showOverlay();
            if ($('#<%=txtaIDCardNo.ClientID%>').val().replace(" ","") != "")
            {
                __doPostBack('<%= btnReload1.UniqueID%>');
            }
            else
            {
                $('#<%=txtaEmployeeNo.ClientID%>').focus();
                scrollAndFocus('#<%=txtaEmployeeNo.ClientID%>');
            }
            return;
        }

        function CallReload2() {
            $('#<%=hddaPassword.ClientID%>').val($('#<%=txtaPassword.ClientID%>').val());
            $('#<%=hddProject.ClientID%>').val($('[name="dualListboxProject"]').val());
            $('#<%=hddCompany.ClientID%>').val($('[name="dualListboxCompany"]').val());
            //showOverlay();
            if ($('#<%=txtaEmployeeNo.ClientID%>').val().replace(" ","") != "")
            {
                __doPostBack('<%= btnReload2.UniqueID%>');
            }
            else
            {
                $('#<%=txtaPassword.ClientID%>').focus();
                scrollAndFocus('#<%=txtaPassword.ClientID%>');
            }
            return;
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
            if ($$('imgaSignature').attr('src') != $$('imgaSignature').attr('data-default')) {
                $('#btnAddFileUploadSignature').hide();
                $('#btnClearFileUploadSignature').show();
            }

        }
        $(function () {
            checkExistImage();

        });
        function endRequestHandler(sender, args) {
            $('select.chosen-select').chosen();
            $(".chosen-search").append('<i class="glyph-icon icon-search"></i>');
            $(".chosen-single div").html('<i class="glyph-icon icon-caret-down"></i>');
            checkExistImage();
        }

    </script>
</asp:Content>

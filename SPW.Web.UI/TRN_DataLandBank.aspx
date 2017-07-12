<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TRN_DataLandBank.aspx.vb" Inherits="SPW.Web.UI.TRN_DataLandBank" EnableEventValidation="false" %>

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

        [disabled].form-control {
            background: #FFFFFF;
        }

        .formEdit .readonly-edit {
            background: #eee;
        }

        .formAdd .readonly-edit {
            background: #FFFFFF;
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
    <uc2:DatePicker runat="server" />
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
                <asp:HiddenField ID="hddMSGSaveData" runat="server" />
                <asp:HiddenField ID="hddMSGCancelData" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteData" runat="server" />
                <asp:HiddenField ID="hddMSGEditData" runat="server" />
                <asp:HiddenField ID="hddMSGAddData" runat="server" />
                <asp:HiddenField ID="hddpAutoCode" runat="server" />
                <asp:HiddenField ID="hddpAutoName" runat="server" />
                <asp:HiddenField ID="hddpKeyIDPicture" runat="server" />
                <asp:HiddenField ID="hddpNamePicture" runat="server" />
                <asp:HiddenField ID="hddpIdMax" runat="server" />
                <asp:HiddenField ID="hddMSGAddFile" runat="server" />
                <asp:HiddenField ID="hddMSGDeleteFile" runat="server" />
                <asp:HiddenField ID="hddpTypeBrownser" runat="server" />
                <asp:HiddenField ID="hddCityPro" runat="server" />
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
                                <script>var dataSource=[];
                                    var dataSourceColumnDefine=[];
                                </script>
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
                                                            <a id="btnMSGAddData" runat="server" href="javascript:CallAddData();" class="btn btn-info glyph-icon icon-plus tooltip-button" Visible='<%#Me.GetPermission().isAdd %>'></a>
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
                                                                    <asp:Label ID="lblsKeyword" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="col-lg-8 col-md-12 col-sm-12">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtsKeyword" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                <asp:Label ID="lblMassage6" runat="server" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblsDescriptionKeyword" runat="server" ForeColor="red"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                            <a href="javascript:CallLoaddata();" class="btn btn-info" title="Search Data">

                                                                <i class="glyph-icon icon-search"></i>

                                                                <asp:Label ID="lblsSearch" runat="server"></asp:Label>

                                                            </a>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row" style="overflow-x:auto;overflow-y:hidden;">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">

                                                            <%
                                                                Dim lc As New List(Of FD11PROP)
                                                                If hddReloadGrid.Value <> String.Empty Then
                                                                    If GetData() IsNot Nothing Then
                                                                        lc = GetData()
                                                            %>
                                                            <table id="grdView" class="table table-striped table-bordered table-hover" style="width: 100%">
                                                                <thead>
                                                                    <tr>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd9" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd10" runat="server"></asp:Label></th>
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
                                                                        <th style="text-align: center; vertical-align: text-top;">
                                                                            <asp:Label ID="TextHd8" runat="server"></asp:Label></th>
                                                                    </tr>
                                                                </thead>
                                                                <tfoot style="display: none;">
                                                                    <tr>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt9" runat="server"></asp:Label></th>
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt10" runat="server"></asp:Label></th>
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
                                                                        <th style="text-align: center; text-anchor: middle;">
                                                                            <asp:Label ID="TextFt8" runat="server"></asp:Label></th>
                                                                    </tr>
                                                                </tfoot>
                                                                <%--   <tbody>
                                                                    <%
                        Dim i As Integer = 0
                        For Each sublc As FD11PROP In lc
                            Dim strb As New StringBuilder()
                                                                    %>
                                                                    <tr>
                                                                        <td style="width: 1%; text-align: center;"></td>
                                                                        <%
                        strb.Append("<td style='width:14%;'>" + IIf(sublc.FASSETNO <> String.Empty, sublc.FASSETNO, String.Empty) + "</td>")
                        strb.Append("<td style='width:13%;'>" + IIf(sublc.FPCPIECE <> String.Empty, sublc.FPCPIECE, String.Empty) + "</td>")
                        strb.Append("<td style='width:20%;'>" + IIf(sublc.FASSETNM <> String.Empty, sublc.FASSETNM, String.Empty) + "</td>")
                        strb.Append("<td style='width:10%;'>" + IIf(sublc.FPCINST3 <> String.Empty, sublc.FPCINST3, String.Empty) + "</td>")
                        strb.Append("<td style='width:10%;'>" + IIf(sublc.FASCOLOR <> String.Empty, sublc.FASCOLOR, String.Empty) + "</td>")
                        strb.Append("<td style='width:20%;'>" + IIf(sublc.FPCLANDOWN <> String.Empty, sublc.FPCLANDOWN, String.Empty) + "</td>")
                        strb.Append("<td style='width:10%;'>" + IIf(sublc.FPCLANDOWTL <> String.Empty, sublc.FPCLANDOWTL, String.Empty) + "</td>")
                        strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-edit'  href='javascript:CallEditData(&#39;" + IIf(sublc.FASSETNO <> String.Empty, sublc.FASSETNO, String.Empty) + "&#39;);'></a></td>")
                        strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-danger glyph-icon icon-trash' href='javascript:ConfirmDelete(&#39;" + IIf(sublc.FASSETNO <> String.Empty, sublc.FASSETNO, String.Empty) + "&#39;);'></a></td>")
                        HttpContext.Current.Response.Write(strb.ToString())
                                                                        %>
                                                                    </tr>
                                                                    <%
                        Next
                                                                    %>
                                                                </tbody>--%>
                                                            </table>
                                                            <script>
                                                             <%                                                 
                                                                Dim js = New System.Web.Script.Serialization.JavaScriptSerializer
                                                                js.MaxJsonLength = 999999999
                                                             %>  
                                                                dataSource= <%=js.Serialize(lc) %>;
                                                                var resAll='<%=grtt("resAll")%>';
                                                                var culture ='<%=Me.WebCulture.ToUpper%>';
                                                                for(i=0;i<dataSource.length;i++){
                                                                    var d= dataSource[i];
                                                                    d.EditButton='<a class="btn btn-info glyph-icon icon-edit"';
                                                                    d.EditButton+=' href="javascript:CallEditData(&#39;'+ d.FASSETNO+'&#39;)"></a>';
                                                                    d.DeleteButton='<a class="btn btn-danger glyph-icon icon-trash"';
                                                                    d.DeleteButton+=' href="javascript:ConfirmDelete(&#39;' + d.FASSETNO+ '&#39;)"></a>';
                                                
                                                                };
                                                                console.log(dataSource);
                                                                dataSourceColumnDefine=   [
                                                                         { "data": "EditButton" },
                                                                         { "data": "DeleteButton" },
                                                                         { "data": "FASSETNO" },
                                                                         { "data": "FASSETNO" },
                                                                         { "data": "FPCPIECE" },
                                                                     { "data": "FASSETNM" },
                                                                         { "data": "FPCINST3" },
                                                                         { "data": "FASCOLOR" },
                                                                         { "data": "FPCLANDOWN" },
                                                                         { "data": "FPCLANDOWTL" }
                                                                ];
                                                          

                                                                function isEmpty(obj) {
                                                                    return undefinedToEmpty(obj) == '';
                                                                }
                                                                function undefinedToEmpty(obj) {
                                                                    if (obj == undefined || obj == null) {
                                                                        return '';
                                                                    } else {
                                                                        return obj;
                                                                    }
                                                                }


                                                            </script>

                                                            <style>
                                                                #grdView td:nth-child(1) { /* first column */
                                                                    text-align: center;
                                                                    width: 1%;
                                                                }

                                                                #grdView td:nth-child(2) {
                                                                    text-align: center;
                                                                    width: 1%;
                                                                }

                                                                #grdView td:nth-child(3) {
                                                                    text-align: center;
                                                                    width: 1%;
                                                                }

                                                                #grdView td:nth-child(4) { /* second column */
                                                                    width: 14%;
                                                                }

                                                                #grdView td:nth-child(5) {
                                                                    width: 13%;
                                                                }

                                                                #grdView td:nth-child(6) {
                                                                    width: 20%;
                                                                }

                                                                #grdView td:nth-child(7) {
                                                                    width: 10%;
                                                                }

                                                                #grdView td:nth-child(8) {
                                                                    width: 10%;
                                                                }

                                                                #grdView td:nth-child(9) {
                                                                    width: 20%;
                                                                }

                                                                #grdView td:nth-child(10) {
                                                                    width: 10%;
                                                                }

                                                               
                                                            </style>
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
                                                                                <a id="btnMSGSaveData" runat="server" href="javascript:CheckData();" class="btn glyph-icon icon-save btn-info tooltip-button"></a>
                                                                                <%End if %>
                                                                                <a id="btnMSGCancelData" runat="server" href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger tooltip-button"></a>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFASSETNO" runat="server" Text=""></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                    <asp:TextBox ID="txtaFASSETNO" autocomplete="off" runat="server" CssClass="form-control readonly-edit tooltip-button" MaxLength="10" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipFASSETNO") %>'></asp:TextBox>
                                                                                    <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:CheckBox ID="chkaFASSETBJ2" runat="server" class="checkbox-inline" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFASSETOBJ" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:DropDownList ID="ddlaFASSETOBJ" runat="server" CssClass="chosen-select disabled-aslabel"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFASSETNM" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFASSETNM" autocomplete="off" runat="server" CssClass="form-control" MaxLength="120"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFENDATE" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                    <asp:TextBox ID="txtaFENDATE" autocomplete="off" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    &nbsp;
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFDESC1" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFPCPIECE" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCPIECE" autocomplete="off" runat="server" CssClass="form-control" Font-Bold="true" MaxLength="10"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFPCINST" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCINST" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFPCLNDNO" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCLNDNO" autocomplete="off" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    &nbsp;
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCINST2" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFPCBETWEEN" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCBETWEEN" autocomplete="off" runat="server" CssClass="form-control" MaxLength="35"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFPCTABON" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCTABON" autocomplete="off" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFPCWIDTH" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCWIDTH" autocomplete="off" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFPCAMPHE" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCAMPHE" autocomplete="off" BackColor="#ffe0c0" runat="server" CssClass="form-control" onkeypress="return AutocompletedPostal(this,event);" onClick="AutocompletedPostal(this,event);" MaxLength="60"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFPCINST3" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCINST3" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFPCPROVINCE" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCPROVINCE" autocomplete="off" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFASCOLOR" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFASCOLOR" autocomplete="off" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFPCLANDOWN" runat="server" Text=""></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCLANDOWN" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    <asp:Label ID="lblaFQTY" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <table style="width: 100%">
                                                                                        <tr>
                                                                                            <td style="width: 100%">
                                                                                                <asp:TextBox ID="txtaFQTY1" autocomplete="off" runat="server" CssClass="form-control text-right" onKeyPress="return Check_Key_Decimal(this,event)" Style="width: 98%" Font-Bold="true"></asp:TextBox>
                                                                                            </td>
                                                                                            <%--<td>&nbsp;-&nbsp;
                                                                                            </td>
                                                                                            <td style="width: 33%">
                                                                                                <asp:TextBox ID="txtaFQTY2" autocomplete="off" runat="server" CssClass="form-control text-right" onKeyPress="return Check_Key_Decimal3(this,event)" Style="width: 98%" Font-Bold="true" MaxLength="1"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>&nbsp;-&nbsp;
                                                                                            </td>
                                                                                            <td style="width: 33%">
                                                                                                <asp:TextBox ID="txtaFQTY3" autocomplete="off" runat="server" CssClass="form-control text-right" onKeyPress="return Check_Key_Decimal(this,event)" Style="width: 98%" Font-Bold="true" MaxLength="2"></asp:TextBox>
                                                                                            </td>--%>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                    &nbsp;
                                                                                </div>
                                                                                <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                    <asp:TextBox ID="txtaFPCLANDOWN2" autocomplete="off" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                                            <div class="content-box tabs" style="border: none;">
                                                                                <h3 class="content-box-header bg-blue-alt">
                                                                                    <span>&nbsp;</span>
                                                                                    <ul style="border: none;">
                                                                                        <li style="border: none;">
                                                                                            <a href="#tabUSER1" style="border: none;">
                                                                                                <asp:Label ID="lblTabUSER1" runat="server" Text=""></asp:Label>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li style="border: none;" class="active">
                                                                                            <a href="#tabUSER2" style="border: none;">
                                                                                                <asp:Label ID="lblTabUSER2" runat="server" Text=""></asp:Label>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li style="border: none;">
                                                                                            <a href="#tabUSER3" style="border: none;">
                                                                                                <asp:Label ID="lblTabUSER3" runat="server" Text=""></asp:Label>
                                                                                            </a>
                                                                                        </li>
                                                                                    </ul>
                                                                                    <h3></h3>
                                                                                    <div id="tabUSER1">
                                                                                        <div class="row">
                                                                                            <div class="col-sm-12">
                                                                                                <h3><%=grtt("resCurrentPicture") %></h3>
                                                                                                <div class="row">
                                                                                                    <asp:Repeater ID="rptImage" runat="server">
                                                                                                        <ItemTemplate>
                                                                                                            <div class="MultiFile-label col-sm-6 ">
                                                                                                                <div class="multifile-imagecontainer ">
                                                                                                                    <a class="btn btn-danger" onclick="HidePicture(this);"><i class="glyph-icon icon-trash"></i><%=grtt("resDeletePicture") %></a>
                                                                                                                    <input type="checkbox" id="chkDeleteImage" runat="server" class="hide" />
                                                                                                                    <br />
                                                                                                                   
                                                                                                                    <asp:Image ID="imgEdit" runat="server" CssClass="img-responsive" onclick="window.open($(this).attr('src'), '_blank');"  />
                                                                                                                        </a>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:Repeater>
                                                                                                </div>
                                                                                                <br />
                                                                                                <asp:Panel ID="imgAdd" runat="server">
                                                                                                <h3><%=grtt("resNewPicture") %></h3>
                                                                                                <div class="row">
                                                                                                    <asp:FileUpload ID="fileUploadMulti" runat="server" class="multi nutClick" accept=".jpg,.jpeg,.gif,.png" />


                                                                                                    <div class="MultiFile-label col-sm-6">
                                                                                                        <div class="multifile-imagecontainer">
                                                                                                            <img src="image/addpicture.png" class="img-responsive" onclick="$('.nutClick').trigger('click');" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                </asp:Panel>
                                                                                            </div>
                                                                                        </div>
                                                                                        <%--<div class="panel-body">
                                                                                            <div class="row float-right">
                                                                                                   <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="hide" />
                                                                                                        <a id="btnhrefAddPicture" runat="server" class="btn btn-info tooltip-button" href="javascript:CallAddPicture();">
                                                                                                         <i class="glyph-icon icon-image"></i>
                                                                                                            <asp:Label ID="lblsPicture" runat="server"></asp:Label>
                                                                                                        </a>
                                                                                               
                                                                                                        <a id="btnhrefAddDelete" runat="server" class="btn btn-danger tooltip-button" href="javascript:CallDeletePicture();">
                                                                                                            <i class="glyph-icon icon-trash"></i>
                                                                                                            <asp:Label ID="lblsDeletePicture" runat="server"></asp:Label>
                                                                                                </a>
                                                                                               
                                                                                                </div>

                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="col-lg-12 col-md-12 col-sm-12">
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
                                                                                                    </div>
                                                                                                </div>--%>
                                                                                    </div>
                                                                                    <div id="tabUSER2">
                                                                                        <div class="panel-body">
                                                                                            <div class="row">
                                                                                                <div class="col-lg-7 col-md-7 col-sm-7">
                                                                                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                        <asp:Label ID="lblaFMKPRCU" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                        <asp:TextBox ID="txtaFMKPRCU" runat="server" autocomplete="off" CssClass="form-control text-right" onKeyPress="return Check_Key_Decimal(this,event)"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:DropDownList ID="ddlaFMCOMPCOMP" runat="server" CssClass="chosen-select disabled-aslabel">
                                                                                                        </asp:DropDownList>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:Label ID="lblaFASSPRCU" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                        <asp:TextBox ID="txtaFASSPRCU" runat="server" autocomplete="off" CssClass="form-control text-right" onblur="return setFormat(this,event);" onKeyPress="return Check_Key_Decimal(this,event)"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-7 col-md-7 col-sm-7">
                                                                                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                        <asp:Label ID="lblaFKPRCA" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                                        <asp:TextBox ID="txtaFKPRCA" runat="server" autocomplete="off" CssClass="form-control text-right" onblur="return setFormat(this,event);" onKeyPress="return Check_Key_Decimal(this,event)"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:Label ID="lblaFASSPRCA" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:TextBox ID="txtaFASSPRCA" runat="server" autocomplete="off" CssClass="form-control text-right" onblur="return setFormat(this,event);" onKeyPress="return Check_Key_Decimal(this,event)"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-7 col-md-7 col-sm-7">
                                                                                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                        <asp:Label ID="lblaFMKPRCBY" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                                        <asp:TextBox ID="txtaFMKPRCBY" runat="server" autocomplete="off" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:Label ID="lblaFASSWHO" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                                        <asp:TextBox ID="txtaFASSWHO" runat="server" autocomplete="off" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-7 col-md-7 col-sm-7">
                                                                                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                        <asp:Label ID="lblaCLANDOWNTL" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-8 col-md-8 col-sm-8">
                                                                                                        <asp:TextBox ID="txtaCLANDOWNTL" runat="server" autocomplete="off" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:Label ID="lblaFASSPRCDT" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:TextBox ID="txtaFASSPRCDT" runat="server" autocomplete="off" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" onKeyPress="return Check_Key_Date(this,event)"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-7 col-md-7 col-sm-7">
                                                                                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                        <asp:Label ID="lblaFMKPRCDT" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:TextBox ID="txtaFMKPRCDT" runat="server" autocomplete="off" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" onKeyPress="return Check_Key_Date(this,event)"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:Label ID="lblaFASSCHG" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:TextBox ID="txtaFASSCHG" runat="server" autocomplete="off" CssClass="form-control text-right" onblur="return setFormat(this,event);" onKeyPress="return Check_Key_Decimal(this,event)"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="row">
                                                                                                <div class="col-lg-7 col-md-7 col-sm-7">
                                                                                                    <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                        <asp:Label ID="lblaFASSETST" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                                                                                        <asp:DropDownList ID="ddlaFASSETST" runat="server" CssClass="chosen-select disabled-aslabel">
                                                                                                        </asp:DropDownList>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                                    <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                        <asp:Label ID="lblaFASSETST2" runat="server" Text=""></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-6 col-md-6 col-sm-6">
                                                                                                        <asp:DropDownList ID="ddlaFASSETST2" runat="server" CssClass="chosen-select">
                                                                                                        </asp:DropDownList>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <br />
                                                                                            <div class="panel panel-primary">
                                                                                                <div class="panel-body">
                                                                                                    <div class="row">
                                                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                                                            <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                                <asp:Label ID="lblaFAGRNO" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                                <asp:TextBox ID="txtaFAGRNO" runat="server" autocomplete="off" BackColor="#F5F5F5" CssClass="form-control" Enabled="false" MaxLength="16"></asp:TextBox>
                                                                                                            </div>
                                                                                                            <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                                <asp:TextBox ID="txtaFAGRNO2" runat="server" autocomplete="off" BackColor="#F5F5F5" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                                            <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                                <asp:CheckBox ID="chkaFMORTGYN" runat="server" class="checkbox-inline" />
                                                                                                            </div>
                                                                                                            <div class="col-lg-7 col-md-7 col-sm-7">
                                                                                                                <asp:TextBox ID="txtaFMORTGYN" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <br />
                                                                                                    <div class="row">
                                                                                                        <div class="col-lg-7 col-md-7 col-sm-7">
                                                                                                            <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                                <asp:Label ID="lblaFSTDATE" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                                <asp:TextBox ID="txtaFSTDATE" runat="server" autocomplete="off" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" onKeyPress="return Check_Key_Date(this,event)"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="col-lg-5 col-md-5 col-sm-5">
                                                                                                            <div class="col-lg-3 col-md-3 col-sm-3">
                                                                                                                <asp:Label ID="lblaFMORTGT" runat="server" Text=""></asp:Label>
                                                                                                            </div>
                                                                                                            <div class="col-lg-4 col-md-4 col-sm-4">
                                                                                                                <asp:TextBox ID="txtaFMORTGT" runat="server" autocomplete="off" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10" onKeyPress="return Check_Key_Date(this,event)"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div id="tabUSER3">
                                                                                        <div class="panel-body">
                                                                                            <div class="row">
                                                                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                                    <asp:TextBox ID="txtaNote" runat="server" CssClass="form-control" MaxLength="80" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <br />
                                                                                    <h3></h3>
                                                                                </h3>
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
                                <button id="btnMSGDeleteData" runat="server" type="button" class="btn btn-danger" onclick="javascript:CallDeleteData();"></button>
                                <button id="btnMSGCancelDataS" runat="server" type="button" class="btn btn-default" data-dismiss="modal"></button>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnAdd" runat="server" CssClass="hide" />
                <asp:Button ID="btnSearch" runat="server" CssClass="hide" />
                <asp:Button ID="btnEdit" runat="server" CssClass="hide" />
                <asp:Button ID="btnDelete" runat="server" CssClass="hide" />
                <asp:Button ID="btnSave" runat="server" CssClass="hide" />
                <asp:Button ID="btnCancel" runat="server" CssClass="hide" />
                <asp:Button ID="btnReload" runat="server" CssClass="hide" />
                <%--                <asp:Button ID="btnFileUpload" runat="server" CssClass="hide" />
                <asp:Button ID="btnDeletePicture" runat="server" CssClass="hide" />--%>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnEdit" />
            <asp:PostBackTrigger ControlID="btnDelete" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnReload" />
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
                        { "targets": [1], "visible": isDelete }],
                        autoWidth: false,
                        data: dataSource,
                        columns:dataSourceColumnDefine
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


            <%--$('#<%=FileUpload1.ClientID%>').change(function (e) {

                if ($('#<%=FileUpload1.ClientID%>').val() != "") {
                     __doPostBack('<%= btnFileUpload.UniqueID%>');
                }
             });--%>
        }

        function AutocompletedPostal(txtFrelocat3, e) {
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
            $("#<%=txtaFPCAMPHE.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("./API/Api_DataLandBank.asmx/GetAuto")%>',
                        data: "{ 'ptPostal': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
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
                    $("#<%=hddpAutoName.ClientID%>").val(i.item.val);

                    PostbackSelectPostal();
                },
                minLength: 0,
                matchContains: true
            }).on('click', function () { $(this).keydown(); });

            }

            function PostbackSelectPostal() {
                __doPostBack('<%= btnReload.UniqueID%>');
            }

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

            function CallLoaddata() {
                <%--if ($('#<%=txtsKeyword.ClientID%>').val().replace(" ", "") == "") {
                    $('#<%=txtsKeyword.ClientID%>').addClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "";
                    return;
                } else {
                    $('#<%=txtsKeyword.ClientID%>').removeClass("parsley-error")
                    document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "none";
                }--%>
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

            }

            function CalQTY() {
                var txtaFQTY1 = document.getElementById("<%= txtaFQTY1.ClientID%>");
            <%--var txtaFQTY2 = document.getElementById("<%= txtaFQTY2.ClientID%>");
            var txtaFQTY3 = document.getElementById("<%= txtaFQTY3.ClientID%>");--%>
                var txtaFMKPRCU = document.getElementById("<%= txtaFMKPRCU.ClientID%>");
                var ddlaFMCOMPCOMP = document.getElementById("<%= ddlaFMCOMPCOMP.ClientID%>");
                var txtaFKPRCA = document.getElementById("<%= txtaFKPRCA.ClientID%>");
                var TempFQTY1 = "0";
                var TempFQTY2 = "0";
                var TempFQTY3 = "0";
                var TotalVa = "0";
                var TempPrice = "0";

                if (txtaFQTY1.value != "") {
                    TempFQTY1 = txtaFQTY1.value.split("-")[0];
                    TempFQTY2 = txtaFQTY1.value.split("-")[1];
                    TempFQTY3 = txtaFQTY1.value.split("-")[2];
                    if (TempFQTY1 == "") {
                        TempFQTY1 = "0";
                    }
                    if (TempFQTY2 == "") {
                        TempFQTY2 = "0";
                    }
                    if (TempFQTY3 == "") {
                        TempFQTY3 = "0";
                    }
                }


                if (TempFQTY1 != "")
                {
                    TempFQTY1 = fnFormatMoney0(parseInt(TempFQTY1) * 400);
                }
                if (TempFQTY2 != "") {
                    TempFQTY2 = fnFormatMoney0(parseInt(TempFQTY2) * 100);
                }
                if (TempFQTY3 != "") {
                    TempFQTY3 = fnFormatMoney0(parseInt(TempFQTY3));
                }

                TotalVa = parseInt(replaceAll(TempFQTY1, ',', '')) + parseInt(replaceAll(TempFQTY2, ',', '')) + parseInt(replaceAll(TempFQTY3, ',', ''));

                if (txtaFMKPRCU.value != "") {
                    TempPrice = txtaFMKPRCU.value;
                }

                if (ddlaFMCOMPCOMP.value == "202") {
                    TotalVa = parseInt(TotalVa) / 400
                    txtaFKPRCA.value = fnFormatMoney(parseFloat(replaceAll(TempPrice, ',', '')) * TotalVa);
                } else if (ddlaFMCOMPCOMP.value == "102") {
                    txtaFKPRCA.value = fnFormatMoney(parseFloat(replaceAll(TempPrice, ',', '')) * TotalVa);
                }

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
            if ($('#<%=txtaFASSETNO.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaFASSETNO.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                scrollAndFocus('#<%=txtaFASSETNO.ClientID%>');
                return;
            } else {
                $('#<%=txtaFASSETNO.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }

            CallSaveData();
        }


        <%--function CallAddPicture() {
            $("#<%=FileUpload1.ClientID%>").trigger('click');
            <%--var hddpTypeBrownser = document.getElementById("<%= hddpTypeBrownser.ClientID%>");
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

        <%--function SetImageURL(strPath, pID, pName) {
            $('#<%=imgPic.ClientID%>').attr("src", "");
            $('#<%=imgPic.ClientID%>').attr("src", strPath);
            var hddpKeyIDPicture = document.getElementById("<%= hddpKeyIDPicture.ClientID%>");
            var hddpNamePicture = document.getElementById("<%= hddpNamePicture.ClientID%>");
            hddpKeyIDPicture.value = pID;
            hddpNamePicture.value = pName;
            $('#<%=imgPic.ClientID%>').attr("onClick", "CallLoadHrefNewtab('" + strPath + "');");
            return false
        }

        function CallLoadHrefNewtab(Urls) {
            window.open(Urls, '_blank');
        }--%>

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
        function HidePicture(el) {
            $(el).next().prop('checked', true).parent().parent().hide();
        }
    </script>
</asp:Content>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TRN_CustomerInformation.aspx.vb" Inherits="SPW.Web.UI.TRN_CustomerInformation"
    EnableEventValidation="false" %>

<%@ Register Src="Usercontrol/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc2" %>
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

        .img-responsive {
            width: auto;
            max-width: 90%;
            max-height: 150px;
            margin: auto;
            position: absolute;
            top: 35px;
            bottom: 0;
            left: 0;
            right: 0;
        }

        .MultiFile-label {
        }

        .multifile-imagecontainer {
            padding: 5px;
            border: 2px outset #6588b7;
            height: 200px;
        }

        .MultiFile-label.col-sm-6 {
            margin-top: 17px;
        }

        .MultiFile-label a.btn {
            float: right;
            margin-bottom: 5px;
            z-index: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:DatePicker runat="server" ID="uc2Date" />
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
                <asp:HiddenField ID="hddpAutoName3" runat="server" />
                <asp:HiddenField ID="hddpAutoCode3" runat="server" />
                <asp:HiddenField ID="hddMsgMobileFormat" runat="server" />
                <asp:HiddenField ID="hddCulture" runat="server" />
                <asp:HiddenField ID="hddCheckUsedCustomer" runat="server" />
                <asp:HiddenField ID="hddCheckDeletePicture" runat="server" />
                <asp:HiddenField ID="hddCheckPeopleID" runat="server" />
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
                                                            <a href="javascript:CallAddData();" class="btn btn-info glyph-icon icon-plus tooltip-button <%=IIf(Me.GetPermission().isAdd, "", "hide") %>" title="<% Response.Write(hddMSGAddData.Value) %>"></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-lg-2">
                                                    </div>
                                                    <div class="col-lg-10">
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
                                                                                    <asp:Label ID="lblMassage6" runat="server" ForeColor="Red"></asp:Label>
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
                                                    <div class="row" style="overflow-x: auto; overflow-y: hidden;">
                                                        <div class="col-lg-12 col-md-12 col-sm-12">
                                                            <script>var dataSource=[];
                                                                var dataSourceColumnDefine=[];
                                                            </script>
                                                            <%
                                                                Dim lcCusInfo As New List(Of OD50RCVD)
                                                                If hddReloadGrid.Value <> String.Empty Then
                                                                    If GetDataCustomerInfo() IsNot Nothing Then
                                                                        lcCusInfo = GetDataCustomerInfo()
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
                                                                <%--  <tbody>
                                                                    <%
                                                                        Dim i As Integer = 0
                                                                        For Each sublcCusInfo As OD50RCVD In lcCusInfo
                                                                            Dim strb As New StringBuilder()
                                                                    %>
                                                                    <tr>
                                                                        <td style="width: 1%; text-align: center;"></td>
                                                                        <%
                                                                            strb.Append("<td style='width:23%;'>" + Convert.ToString(sublcCusInfo.FCONTCODE) + "</td>")
                                                                            If Me.WebCulture = "TH" Then
                                                                                strb.Append("<td style='width:37%;'>" + IIf(sublcCusInfo.FCONTTNM <> String.Empty, sublcCusInfo.FCONTTNM, String.Empty) + "</td>")
                                                                            Else
                                                                                strb.Append("<td style='width:37%;'>" + IIf(sublcCusInfo.FCONTENM <> String.Empty, sublcCusInfo.FCONTENM, String.Empty) + "</td>")
                                                                            End If
                                                                            strb.Append("<td style='width:18%;'>" + IIf(sublcCusInfo.FTELNO <> String.Empty, sublcCusInfo.FTELNO, String.Empty) + "</td>")
                                                                            strb.Append("<td style='width:19%;'>" + IIf(sublcCusInfo.FPEOPLEID <> String.Empty, sublcCusInfo.FPEOPLEID, String.Empty) + "</td>")
                                                                            strb.Append("<td style='width:1%;text-align: center;'><a id='btnEditTemp' class='btn btn-info glyph-icon icon-typicons-edit' href='javascript:CallEditData(&#39;" + Convert.ToString(sublcCusInfo.FCONTCODE) + "&#39;);'></a></td>")
                                                                            strb.Append("<td style='width:1%;text-align: center;'><a id='btnDeleteTemp' class='btn btn-info glyph-icon icon-typicons-trash' href='javascript:ConfirmDelete(&#39;" + Convert.ToString(sublcCusInfo.FCONTCODE) + "&#39;,&#39;" + Convert.ToString(sublcCusInfo.FCONTCODE) + "&#39;);'></a></td>")
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
                                                                dataSource= <%=js.Serialize(lcCusInfo) %>;
                                                                var resAll='<%=grtt("resAll")%>';
                                                                var culture ='<%=Me.WebCulture.ToUpper%>';
                                                                var hddCheckUsedCustomer =document.getElementById('<%=hddCheckUsedCustomer.ClientID%>');
                                                                for(i=0;i<dataSource.length;i++){
                                                                    var d= dataSource[i];
                                                                    if(culture=='TH'){
                                                                        d.FCONTENM=d.FCONTTNM
                                                                        d.FPDNAMETYPE=d.FPDNAMETYPET
                                                                    }
                                                                    if (d.FPEOPLEID != null)
                                                                    {
                                                                        d.FPEOPLEID = d.FPEOPLEID.replace(" ", "");
                                                                    }
                                                                    
                                                                    d.EditButton='<a class="btn btn-info glyph-icon icon-edit"';
                                                                    d.EditButton+=' href="javascript:CallEditData(&#39;'+ d.FCONTCODE+'&#39;)"></a>';
                                                                    //var n = hddCheckUsedCustomer.value.search(d.FCONTCODE);
                                                                    var arr = "[" + hddCheckUsedCustomer.value + "]";
                                                                    if (arr.indexOf(d.FCONTCODE) == "-1"){
                                                                        d.DeleteButton='<a class="btn btn-danger glyph-icon icon-trash"';
                                                                        d.DeleteButton+=' href="javascript:ConfirmDelete(&#39;' + d.FCONTCODE+ '&#39;,&#39;' + d.FCONTCODE+ '&#39;)"></a>';
                                                                    } else {
                                                                        d.DeleteButton='';
                                                                    }
                                                                };
                                                                console.log(dataSource);
                                                                dataSourceColumnDefine=   [
                                                                         { "data": "EditButton" },
                                                                         { "data": "DeleteButton" },
                                                                         { "data": "FCONTCODE" },
                                                                         { "data": "FCONTCODE" },
                                                                         { "data": "FCONTENM" },
                                                                         { "data": "FTELNO" },
                                                                         { "data": "FPEOPLEID" }
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
                                                                    width: 23%;
                                                                }

                                                                #grdView td:nth-child(5) {
                                                                    width: 37%;
                                                                }

                                                                #grdView td:nth-child(6) {
                                                                    width: 18%;
                                                                }

                                                                #grdView td:nth-child(7) {
                                                                    width: 19%;
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
                                                                <div class="panel-body <%=IIf((hddKeyID.Value = String.Empty And Me.GetPermission.isAdd) Or (hddKeyID.Value <> String.Empty And Me.GetPermission.isEdit), "", "disabled") %>">
                                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                                        <div class="col-lg-8 col-md-12 col-sm-12">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaFCONTCODE" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:TextBox ID="txtaFCONTCODE" autocomplete="off" runat="server" CssClass="form-control" MaxLength="8" Enabled="false" BackColor="#F5F5F5"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:CheckBox ID="chbaFPRECODE" runat="server" onchange="EnableFCONTTNM3();" />
                                                                                </div>
                                                                                <div class="col-lg-4 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 55%; text-align: right;">
                                                                                                <asp:Label ID="lblaFSTDATE" runat="server"></asp:Label><asp:Label ID="Label3" runat="server" Text="*" ForeColor="red"></asp:Label>&nbsp;
                                                                                            </td>
                                                                                            <td style="width: 45%;">
                                                                                                <asp:TextBox ID="txtaFSTDATEEdit" autocomplete="off" runat="server" CssClass="form-control" MaxLength="8" Enabled="false" BackColor="#F5F5F5"></asp:TextBox>
                                                                                                <asp:TextBox ID="txtaFSTDATE" autocomplete="off" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <asp:Label ID="lblMassage5" runat="server" ForeColor="Red"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaFCONTTNM" runat="server"></asp:Label><asp:Label ID="Label4" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:HiddenField ID="hddFPRELEN" runat="server" />
                                                                                    <table style="width: 100%;">
                                                                                        <tr style="width: 100%;">
                                                                                            <td style="width: 25%;">
                                                                                                <asp:TextBox ID="txtaFCONTTNM1" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="40" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipFCONTTNM1") %>'></asp:TextBox>
                                                                                            </td>
                                                                                            <td runat="server" id="tdFCONTTNM2" style="width: 38%;">
                                                                                                <asp:TextBox ID="txtaFCONTTNM2" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="55" onKeyPress="return Check_KeySpaceOnly(event);" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipFCONTTNM2") %>'></asp:TextBox>
                                                                                            </td>
                                                                                            <td runat="server" id="tdFCONTTNM3" style="width: 37%;">
                                                                                                <asp:TextBox ID="txtaFCONTTNM3" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="55" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipFCONTTNM3") %>'></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaFCONTENM" runat="server"></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:TextBox ID="txtaFCONTENM" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="150" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipFCONTENM") %>'></asp:TextBox>
                                                                                    <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaFADD1" runat="server"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">

                                                                                    <asp:TextBox ID="txtaFADD1" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="255" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipFADD1") %>'></asp:TextBox>


                                                                                    <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:TextBox ID="txtaFADD2" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaFADD3" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:TextBox ID="txtaFADD3" autocomplete="off" runat="server" BackColor="#ffe0c0" CssClass="form-control" MaxLength="50" onkeypress="return AutocompletedPostal1(this,event);" onClick="AutocompletedPostal1(this,event);"></asp:TextBox>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                <ContentTemplate>
                                                                                    <div class="row">
                                                                                        <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                            <asp:Label ID="lblaFPROVINCE" runat="server"></asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="red"></asp:Label>
                                                                                        </div>
                                                                                        <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">
                                                                                            <table style="width: 100%;">
                                                                                                <tr>
                                                                                                    <td style="width: 75%;">
                                                                                                        <asp:TextBox ID="txtaFPROVINCE" autocomplete="off" runat="server" CssClass="form-control tooltip-button" MaxLength="25" data-toggle="tooltip" data-placement="right" data-original-title='<%# grtt("resTooltipFPROVINCE") %>'></asp:TextBox></td>
                                                                                                    <td style="width: 2%;"></td>
                                                                                                    <td style="width: 23%;">
                                                                                                        <asp:TextBox ID="txtaFPOSTAL" autocomplete="off" runat="server" CssClass="form-control" MaxLength="5" onKeyPress="return Check_Key(event);"></asp:TextBox></td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <asp:HiddenField ID="hddaFPROVCD" runat="server" />
                                                                                            <asp:HiddenField ID="hddaFCITYCD" runat="server" />
                                                                                            <asp:Label ID="lblMassage4" runat="server" ForeColor="Red"></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:Label ID="lblaFBIRTH" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lblaFBIRTH2" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:TextBox ID="txtaFBIRTH" autocomplete="off" onblur="getAgeToBirthDay();" onKeyPress="return Check_Key_Date(this,event)" runat="server" class="datepicker form-control" data-date-format="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:Label ID="lblaAge" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-2 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:TextBox ID="txtaAge" autocomplete="off" runat="server" class="form-control" MaxLength="5" onKeyPress="return Check_Key(event);"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: right;">
                                                                                    <asp:CheckBox ID="chbaIsThailandor" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:Label ID="lblaFPEOPLEID" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:TextBox ID="txtaFPEOPLEID" autocomplete="off" runat="server" CssClass="form-control" onKeyPress="return Check_KeySpace(event);" onKeyUp="callCheck13digit();"></asp:TextBox>
                                                                                    <asp:Label ID="lblsMessageFPEOPLEID" runat="server" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:Label ID="lblaTaxNo" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:TextBox ID="txtaTaxNo" autocomplete="off" runat="server" CssClass="form-control" onKeyPress="return Check_KeySpace(event);" onKeyUp="callCheck13digit2();"></asp:TextBox>
                                                                                    <asp:Label ID="lblsMessageTaxNo" runat="server" ForeColor="red"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-lg-4 col-md-12 col-sm-12">
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:Label ID="lblaFTELNO" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:TextBox ID="txtaFTELNO" autocomplete="off" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:Label ID="lblaFMOBILE" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 4%;">1.</td>
                                                                                            <td style="width: 96%;">
                                                                                                <asp:TextBox ID="txtaFMOBILE1" autocomplete="off" runat="server" CssClass="form-control" MaxLength="40" onKeyPress="return Check_Key(event);"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <table style="width: 100%;">
                                                                                        <tr>
                                                                                            <td style="width: 4%;">2.</td>
                                                                                            <td style="width: 96%;">
                                                                                                <asp:TextBox ID="txtaFMOBILE2" autocomplete="off" runat="server" CssClass="form-control" MaxLength="10" onKeyPress="return Check_Key(event);"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:Label ID="lblaFFAX" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:TextBox ID="txtaFFAX" autocomplete="off" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:Label ID="lblaFEMAIL" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12" style="text-align: left;">
                                                                                    <asp:TextBox ID="txtaFEMAIL" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-12 col-sm-12">
                                                                                    <asp:Label ID="lblaFFACEBOOK" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-12 col-sm-12">
                                                                                    <asp:TextBox ID="txtaFFACEBOOK" autocomplete="off" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">

                                                                                <div class="col-lg-3 col-md-3 col-sm-12">
                                                                                    <asp:Label ID="lblaFLINE" runat="server"></asp:Label>
                                                                                </div>
                                                                                <div class="col-lg-9 col-md-9 col-sm-12">
                                                                                    <asp:TextBox ID="txtaFLINE" autocomplete="off" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div class="col-lg-3 col-md-3 col-sm-12">
                                                                                    &nbsp;
                                                                                </div>
                                                                                <div class="MultiFile-label col-lg-9 col-md-9 col-sm-12" style="vertical-align: top; text-align: left;">
                                                                                    <div class="multifile-imagecontainer">
                                                                                        <a class="btn btn-info"
                                                                                            id="btnAddFileUpload" onclick="triggerClick('FileUpload'); ">
                                                                                            <i class="glyph-icon  icon-image"></i><%=grtt("resAddLineQRCode") %></a>
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
                                                                        <br />
                                                                        <div class="row">
                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                            </div>
                                                                            <div class="col-lg-6 col-md-12 col-sm-12">
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                <div class="panel panel-primary">
                                                                                    <div class="panel-heading">
                                                                                        <h3>
                                                                                            <asp:Label ID="lblPerSon" runat="server" Text=""></asp:Label>
                                                                                        </h3>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <div class="row">
                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                <asp:Label ID="lblaFHADD1" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">

                                                                                                <asp:TextBox ID="txtaFHADD1" autocomplete="off" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:Label ID="lblaFSEX" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <table style="border: 1px solid gray; width: 80%;">
                                                                                                    <tr>
                                                                                                        <td>&nbsp;&nbsp;</td>
                                                                                                        <td>
                                                                                                            <asp:RadioButtonList ID="rbtaFSEX" runat="server" RepeatDirection="Horizontal">
                                                                                                            </asp:RadioButtonList></td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </div>
                                                                                        </div>
                                                                                        <br />
                                                                                        <div class="row">
                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                <asp:Label ID="lblaFHADD2" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:TextBox ID="txtaFHADD2" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                            </div>
                                                                                            <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                            </div>
                                                                                        </div>
                                                                                        <br />
                                                                                        <div class="row">
                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                <asp:Label ID="lblaFHADD3" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:TextBox ID="txtaFHADD3" autocomplete="off" runat="server" BackColor="#ffe0c0" CssClass="form-control" MaxLength="50" onkeypress="return AutocompletedPostal2(this,event);" onClick="AutocompletedPostal2(this,event);"></asp:TextBox>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                            <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:Label ID="lblaFHTEL" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:TextBox ID="txtaFHTEL" autocomplete="off" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                        <br />
                                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="row">
                                                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                        <asp:Label ID="lblaFHPROVINCE" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                        <asp:TextBox ID="txtaFHPROVINCE" autocomplete="off" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                                                                                        <asp:HiddenField ID="hddaFHPROVCD" runat="server" />
                                                                                                        <asp:HiddenField ID="hddaFHCITYCD" runat="server" />
                                                                                                    </div>
                                                                                                    <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                        <asp:Label ID="lblaFHPOSTAL" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                        <asp:TextBox ID="txtaFHPOSTAL" autocomplete="off" runat="server" CssClass="form-control" MaxLength="5" onKeyPress="return Check_Key(event);"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                <div class="panel panel-primary">
                                                                                    <div class="panel-heading">
                                                                                        <h3>
                                                                                            <asp:Label ID="lblOffice" runat="server" Text=""></asp:Label>
                                                                                        </h3>
                                                                                    </div>
                                                                                    <div class="panel-body">
                                                                                        <div class="row">
                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                <asp:Label ID="lblaFOFFNAME" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:TextBox ID="txtaFOFFNAME" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:Label ID="lblaFPOSITION" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:TextBox ID="txtaFPOSITION" autocomplete="off" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                        <br />
                                                                                        <div class="row">
                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                <asp:Label ID="lblaFOFFADD1" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:TextBox ID="txtaFOFFADD1" autocomplete="off" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                            </div>
                                                                                            <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                            </div>
                                                                                        </div>
                                                                                        <br />
                                                                                        <div class="row">
                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                <asp:Label ID="lblaFOFFADD2" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:TextBox ID="txtaFOFFADD2" autocomplete="off" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                            </div>
                                                                                            <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                            </div>
                                                                                        </div>
                                                                                        <br />
                                                                                        <div class="row">
                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                <asp:Label ID="lblaFOFFADD3" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:TextBox ID="txtaFOFFADD3" autocomplete="off" BackColor="#ffe0c0" runat="server" CssClass="form-control" MaxLength="50" onkeypress="return AutocompletedPostal3(this,event);" onClick="AutocompletedPostal3(this,event);"></asp:TextBox>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                            <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:Label ID="lblaFOFFTEL" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:TextBox ID="txtaFOFFTEL" autocomplete="off" runat="server" CssClass="form-control" MaxLength="40"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                        <br />
                                                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <div class="row">
                                                                                                    <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                        <asp:Label ID="lblaFOFFPROV" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                        <asp:TextBox ID="txtaFOFFPROV" autocomplete="off" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                                                                                        <asp:HiddenField ID="hddaFOFFPROVCD" runat="server" />
                                                                                                        <asp:HiddenField ID="hddaFOFFCITYCD" runat="server" />
                                                                                                    </div>
                                                                                                    <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                        <asp:Label ID="lblaFOFFZIP" runat="server"></asp:Label>
                                                                                                    </div>
                                                                                                    <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                        <asp:TextBox ID="txtaFOFFZIP" autocomplete="off" runat="server" CssClass="form-control" MaxLength="5" onKeyPress="return Check_Key(event);"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                        <br />
                                                                                        <div class="row">
                                                                                            <div class="col-lg-2 col-md-12 col-sm-12">
                                                                                                <asp:Label ID="lblaFOFFICETYPE" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <div class="col-lg-6 col-md-12 col-sm-12" style="text-align: left;">
                                                                                                <asp:TextBox ID="txtaFOFFICETYPE" autocomplete="off" runat="server" CssClass="form-control" MaxLength="120"></asp:TextBox>
                                                                                            </div>
                                                                                            <div class="col-lg-1 col-md-12 col-sm-12" style="text-align: left;">
                                                                                            </div>
                                                                                            <div class="col-lg-3 col-md-12 col-sm-12" style="text-align: left;">
                                                                                            </div>
                                                                                        </div>
                                                                                        <br />
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                                                                <%
                                                                                                    Dim dsStatusContCode As New DataSet
                                                                                                    If hddReloadGridEdit.Value <> String.Empty Then
                                                                                                        If GetDataStatusContCode() IsNot Nothing Then
                                                                                                            dsStatusContCode = GetDataStatusContCode()
                                                                                                %>
                                                                                                <table id="grdView2" class="table table-striped table-bordered table-hover" style="width: 100%">
                                                                                                    <thead>
                                                                                                        <tr>
                                                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                                                <asp:Label ID="TextHd2_1" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                                                <asp:Label ID="TextHd2_2" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                                                <asp:Label ID="TextHd2_3" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                                                <asp:Label ID="TextHd2_4" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                                                <asp:Label ID="TextHd2_5" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                                                <asp:Label ID="TextHd2_6" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; vertical-align: text-top;">
                                                                                                                <asp:Label ID="TextHd2_7" runat="server"></asp:Label></th>
                                                                                                        </tr>
                                                                                                    </thead>
                                                                                                    <tfoot style="display: none;">
                                                                                                        <tr>
                                                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                                                <asp:Label ID="TextFt2_1" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                                                <asp:Label ID="TextFt2_2" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                                                <asp:Label ID="TextFt2_3" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                                                <asp:Label ID="TextFt2_4" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                                                <asp:Label ID="TextFt2_5" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                                                <asp:Label ID="TextFt2_6" runat="server"></asp:Label></th>
                                                                                                            <th style="text-align: center; text-anchor: middle;">
                                                                                                                <asp:Label ID="TextFt2_7" runat="server"></asp:Label></th>
                                                                                                        </tr>
                                                                                                    </tfoot>
                                                                                                    <tbody>
                                                                                                        <%
                        Dim i As Integer = 0

                        For Each drStatusContCode As DataRow In dsStatusContCode.Tables(0).Rows
                            Dim strb As New StringBuilder()
                                                                                                        %>
                                                                                                        <tr>
                                                                                                            <%
                        i = i + 1
                        strb.Append("<td style='width: 1%; text-align: center;'>" + i.ToString + "</td>")
                        strb.Append("<td style='width:19%;'>" + IIf(drStatusContCode("FREPRJNO").ToString <> String.Empty, drStatusContCode("FREPRJNO").ToString, String.Empty) + "</td>")
                        strb.Append("<td style='width:15%;'>" + IIf(drStatusContCode("FREPHASE").ToString <> String.Empty, drStatusContCode("FREPHASE").ToString, String.Empty) + "</td>")
                        strb.Append("<td style='width:15%;'>" + IIf(drStatusContCode("FSERIALNo").ToString <> String.Empty, drStatusContCode("FSERIALNo").ToString, String.Empty) + "</td>")
                        strb.Append("<td style='width:15%;'>" + IIf(drStatusContCode("FADDRNO").ToString <> String.Empty, drStatusContCode("FADDRNO").ToString, String.Empty) + "</td>")
                        strb.Append("<td style='width:20%;'>" + IIf(drStatusContCode("FAGRNO").ToString <> String.Empty, drStatusContCode("FAGRNO").ToString, String.Empty) + "</td>")
                        strb.Append("<td style='width:15%;'>" + IIf(drStatusContCode("FStatus").ToString <> String.Empty, drStatusContCode("FStatus").ToString, String.Empty) + "</td>")
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
                            hddReloadGridEdit.Value = String.Empty
                        End If
                                                                                                %>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="panel-heading">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <h3 class="panel-title"></h3>
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
                                <asp:HiddenField ID="hddBodydelete" runat="server" />
                                <asp:TextBox ID="lblBodydelete" runat="server" BackColor="White" BorderStyle="None" BorderWidth="0" Enabled="false" Style="width: 100%"></asp:TextBox>
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnReload1" runat="server" CssClass="hide" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnReload2" runat="server" CssClass="hide" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnReload3" runat="server" CssClass="hide" />
                    </ContentTemplate>
                </asp:UpdatePanel>
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

            $('.datepicker').on('changeDate', function (ev) {
                $(this).datepicker('hide');
                if ($('#<%=txtaFBIRTH.ClientID%>').val() != "") {
                    var dayBirth = $(this).val();
                    var getdayBirth = dayBirth.split("/");
                    var YB = '';

                    if ($('#<%=hddCulture.ClientID%>').val() == "th") {
                        YB = getdayBirth[2] - 543;
                    }
                    else {
                        YB = getdayBirth[2];
                    }

                    //var YB = getdayBirth[2];  //-543
                    var MB = getdayBirth[1];
                    var DB = getdayBirth[0];

                    var setdayBirth = moment(YB + "-" + MB + "-" + DB);
                    var setNowDate = moment();
                    var yearData = setNowDate.diff(setdayBirth, 'years', true); // ข้อมูลปีแบบทศนิยม  
                    var yearFinal = Math.round(setNowDate.diff(setdayBirth, 'years', true), 0); // ปีเต็ม  
                    var yearReal = setNowDate.diff(setdayBirth, 'years'); // ปีจริง  
                    var monthDiff = Math.floor((yearData - yearReal) * 12); // เดือน  
                    var str_year_month = yearReal + " ปี " + monthDiff + " เดือน"; // ต่อวันเดือนปี  
                    $('#<%=txtaAge.ClientID%>').val(yearReal);
                    $('#<%=txtaFBIRTH.ClientID%>').val(dayBirth);
                }
            });
        }

        var getDaysInMonth = function (month, year) {
            // Here January is 1 based  
            //Day 0 is the last day in the previous month  
            return new Date(year, month, 0).getDate();
            // Here January is 0 based  
            // return new Date(year, month+1, 0).getDate();  
        }

        function getAgeToBirthDay() {
            //alert($('#<%=txtaFBIRTH.ClientID%>').val());
            if ($('#<%=txtaFBIRTH.ClientID%>').val() != "") {
                var dayBirth = $('#<%=txtaFBIRTH.ClientID%>').val();
                var getdayBirth = dayBirth.split("/");
                var YB = '';

                if ($('#<%=hddCulture.ClientID%>').val() == "th") {
                    YB = getdayBirth[2] - 543;
                }
                else {
                    YB = getdayBirth[2];
                }
                //var YB = getdayBirth[2];  //-543
                var MB = getdayBirth[1];
                var DB = getdayBirth[0];

                if (MB.Length == 1) {
                    MB = '0' + MB;
                }
                if (DB.Length == 1) {
                    DB = '0' + DB;
                }

                if (parseInt(MB) > 12) {
                    MB = '12';
                    if (parseInt(DB) > 31) {
                        DB = '31';
                    }
                }
                else {
                    var dayInMon = getDaysInMonth(MB, YB)
                    if (parseInt(DB) > dayInMon) {
                        DB = dayInMon;
                    }
                }

                $('#<%=txtaFBIRTH.ClientID%>').val(DB + "/" + MB + "/" + YB)

                var setdayBirth = moment(YB + "-" + MB + "-" + DB);
                var setNowDate = moment();
                var yearData = setNowDate.diff(setdayBirth, 'years', true); // ข้อมูลปีแบบทศนิยม  
                var yearFinal = Math.round(setNowDate.diff(setdayBirth, 'years', true), 0); // ปีเต็ม  
                var yearReal = setNowDate.diff(setdayBirth, 'years'); // ปีจริง  
                var monthDiff = Math.floor((yearData - yearReal) * 12); // เดือน  
                var str_year_month = yearReal + " ปี " + monthDiff + " เดือน"; // ต่อวันเดือนปี  
                $('#<%=txtaAge.ClientID%>').val(yearReal);
                $('#<%=txtaFBIRTH.ClientID%>').val(dayBirth);
            }
            else {
                $('#<%=txtaAge.ClientID%>').val('');
            }
        }

        function checkFormatMobile1() {
            if ($('#<%=txtaFMOBILE1.ClientID%>').val() != "") {
                if ($('#<%=txtaFMOBILE1.ClientID%>').val().Length > 10) {
                    $('#<%=txtaFMOBILE1.ClientID%>').focus();
                    alert($('#<%=hddMsgMobileFormat.ClientID%>').val());
                }
            }
        }
        function checkFormatMobile2() {
            if ($('#<%=txtaFMOBILE2.ClientID%>').val() != "") {
                if ($('#<%=txtaFMOBILE2.ClientID%>').val().Length > 10) {
                    $('#<%=txtaFMOBILE2.ClientID%>').focus();
                    alert($('#<%=hddMsgMobileFormat.ClientID%>').val());
                }
            }
        }



        function AutocompletedPostal1(txtaFADD3, e) {
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
            $("#<%=txtaFADD3.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("./API/Api_Project.asmx/GetAuto")%>',
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
                    $("#<%=hddpAutoName1.ClientID%>").val(i.item.val);
                    <%--var txtaFADD3 = document.getElementById('<%=txtaFADD3.ClientID%>');
                    var txtaFPROVINCE = document.getElementById('<%=txtaFPROVINCE.ClientID%>');
                    var txtaFPOSTAL = document.getElementById('<%=txtaFPOSTAL.ClientID%>');
                    var hddpAutoCode1 = document.getElementById('<%=hddpAutoCode1.ClientID%>');
                    var hddpAutoName1 = document.getElementById('<%=hddpAutoName1.ClientID%>');
                    var hddaFPROVCD = document.getElementById('<%=hddaFPROVCD.ClientID%>');
                    var hddaFCITYCD = document.getElementById('<%=hddaFCITYCD.ClientID%>');
                    hddpAutoCode1.value = "";
                    txtaFADD3.value = "";
                    txtaFPROVINCE.value = "";
                    txtaFPOSTAL.value = "";
                    if(hddpAutoName1.value != ""){
                        txtaFADD3.value = hddpAutoName1.value.split(" ")[0];
                        txtaFPROVINCE.value = hddpAutoName1.value.split(" ")[1];
                        txtaFPOSTAL.value = hddpAutoName1.value.split(" ")[2];
                    }else{
                        if(txtaFADD3.value != ""){
                            hddpAutoCode1.value = txtaFADD3.value;
                            hddpAutoName1a.value = txtaFADD3.value + " " + txtaFPROVINCE.value + " " + txtaFPOSTAL.value;
                        }
                    }
                    hddaFPROVCD.value = "";
                    hddaFCITYCD.value = "";--%>
                    PostbackSelectPostal1();
                },
                minLength: 0,
                matchContains: true
            }).on('click', function () { $(this).keydown(); });

        }

        function PostbackSelectPostal1() {
            __doPostBack('<%= btnReload1.UniqueID%>');
        }


        function AutocompletedPostal2(txtaFHADD3, e) {
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
            $("#<%=txtaFHADD3.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("./API/Api_Project.asmx/GetAuto")%>',
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
                    $("#<%=hddpAutoName2.ClientID%>").val(i.item.val);
                    PostbackSelectPostal2();
                },
                minLength: 0,
                matchContains: true
            }).on('click', function () { $(this).keydown(); });

            }

            function PostbackSelectPostal2() {
                __doPostBack('<%= btnReload2.UniqueID%>');
            }


            function AutocompletedPostal3(txtaFOFFADD3, e) {
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
                $("#<%=txtaFOFFADD3.ClientID%>").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%=ResolveUrl("./API/Api_Project.asmx/GetAuto")%>',
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
                        $("#<%=hddpAutoName3.ClientID%>").val(i.item.val);

                        PostbackSelectPostal3();
                    },
                    minLength: 0,
                    matchContains: true
                }).on('click', function () { $(this).keydown(); });

                }

                function PostbackSelectPostal3() {
                    __doPostBack('<%= btnReload3.UniqueID%>');
                }

                function showOverlay() {
                    $("#overlay").modal();
                }


                function CallLoaddata() {
                    if ($('#<%=txtsKeyword.ClientID%>').val().replace(" ", "") == "") {
                            $('#<%=txtsKeyword.ClientID%>').addClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "";
                            return;
                        } else {
                            $('#<%=txtsKeyword.ClientID%>').removeClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblMassage6').style.display = "none";
                        }
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
                        var hddBodydelete = document.getElementById("<%= hddBodydelete.ClientID%>");
                        var lblBodydelete = document.getElementById("<%= lblBodydelete.ClientID%>");
                  
                        hddKeyID.value = pKey;
                        lblBodydelete.value = hddBodydelete.value + " " + pKey + " ?";

                        $("#DeleteModal").modal();
                    }
                    function Check_KeySpace(e) {
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

                    function Check_KeySpaceOnly(e) {
                        var key;
                        if (document.all) {
                            key = window.event.keyCode;
                        } else {
                            key = e.which;
                        }
                        if (key != 32) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                    function removeString(txt) {
                        var regexp  = /\d/g;
                        var numb = txt.match(regexp);
                        numb = numb.join("");
                        return numb;
                    }
                    function callCheck13digit(){
                        var txt = document.getElementById('<%=txtaFPEOPLEID.ClientID%>');
                        if (txt.value != "") {
                            txt.value = removeString(txt.value);
                            txt.value = txt.value.replace(/\s/g,'');
                            if (txt.value.length <= 12) {
                                $('#<%=txtaFPEOPLEID.ClientID%>').addClass("parsley-error");
                                document.getElementById('ContentPlaceHolder1_lblsMessageFPEOPLEID').style.display = "";
                                return;
                            } else {
                                $('#<%=txtaFPEOPLEID.ClientID%>').removeClass("parsley-error")
                                document.getElementById('ContentPlaceHolder1_lblsMessageFPEOPLEID').style.display = "none";
                            }
                        }else{
                            $('#<%=txtaFPEOPLEID.ClientID%>').removeClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblsMessageFPEOPLEID').style.display = "none";
                        }
                        if (txt.value.length >= 13){
                            txt.value = txt.value.substring(0, 13);
                        }
                    }
                    function callCheck13digit2(){
                        var txt = document.getElementById('<%=txtaTaxNo.ClientID%>');
                        if (txt.value != "") {
                            txt.value = removeString(txt.value);
                            txt.value = txt.value.replace(/\s/g,'');
                            if (txt.value.length <= 12) {
                                $('#<%=txtaTaxNo.ClientID%>').addClass("parsley-error")
                                document.getElementById('ContentPlaceHolder1_lblsMessageTaxNo').style.display = "";
                                return;
                            } else {
                                $('#<%=txtaTaxNo.ClientID%>').removeClass("parsley-error")
                                document.getElementById('ContentPlaceHolder1_lblsMessageTaxNo').style.display = "none";
                            }
                        }else{
                            $('#<%=txtaTaxNo.ClientID%>').removeClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblsMessageTaxNo').style.display = "none";
                        }
                        if (txt.value.length >= 13){
                            txt.value = txt.value.substring(0, 13);
                        }
                    }
                    function CheckData() {
                        if ($('#<%=txtaFPEOPLEID.ClientID%>').val().replace(" ", "") != "") {
                            if ($('#<%=txtaFPEOPLEID.ClientID%>').val().length < 13) {
                                $('#<%=txtaFPEOPLEID.ClientID%>').addClass("parsley-error")
                                document.getElementById('ContentPlaceHolder1_lblsMessageFPEOPLEID').style.display = "";                                
                                scrollAndFocus('#<%=txtaFPEOPLEID.ClientID%>');
                                return;
                            } else {
                                $('#<%=txtaFPEOPLEID.ClientID%>').removeClass("parsley-error")
                                document.getElementById('ContentPlaceHolder1_lblsMessageFPEOPLEID').style.display = "none";
                            }
                            if ($('#<%=hddKeyID.ClientID%>').val() ==""){
                                var arr = "[" + $('#<%=hddCheckPeopleID.ClientID%>').val() + "]";
                                if (arr.indexOf($('#<%=txtaFPEOPLEID.ClientID%>').val()) != "-1"){
                                    OpenDialogError('<%= GetResource("resMSGDupPeopleID", "MSG", "1")%>');                             
                                    scrollAndFocus('#<%=txtaFPEOPLEID.ClientID%>');
                                    return;
                                }
                            }
                        }else{
                            $('#<%=txtaFPEOPLEID.ClientID%>').removeClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblsMessageFPEOPLEID').style.display = "none";
                        }
                        if ($('#<%=txtaTaxNo.ClientID%>').val().replace(" ", "") != "") {
                            if ($('#<%=txtaTaxNo.ClientID%>').val().length < 13) {
                                $('#<%=txtaTaxNo.ClientID%>').addClass("parsley-error")
                                document.getElementById('ContentPlaceHolder1_lblsMessageTaxNo').style.display = "";                           
                                scrollAndFocus('#<%=txtaTaxNo.ClientID%>');
                                return;
                            } else {
                                $('#<%=txtaTaxNo.ClientID%>').removeClass("parsley-error")
                                document.getElementById('ContentPlaceHolder1_lblsMessageTaxNo').style.display = "none";
                            }
                        }else{
                            $('#<%=txtaTaxNo.ClientID%>').removeClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblsMessageTaxNo').style.display = "none";
                        }


                        if ($('#<%=txtaFSTDATE.ClientID%>').val().replace(" ", "") == "") {
                            $('#<%=txtaFSTDATE.ClientID%>').addClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "";                    
                            scrollAndFocus('#<%=txtaFSTDATE.ClientID%>');
                            return;
                        } else {
                            $('#<%=txtaFSTDATE.ClientID%>').removeClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblMassage5').style.display = "none";
                        }
                        var chbaFPRECODE = document.getElementById('<%=chbaFPRECODE.ClientID%>');
                        if ($('#<%=txtaFCONTTNM1.ClientID%>').val().replace(" ", "") == "" ||
                            $('#<%=txtaFCONTTNM2.ClientID%>').val().replace(" ", "") == "") {
                            $('#<%=txtaFCONTTNM1.ClientID%>').addClass("parsley-error")
                            $('#<%=txtaFCONTTNM2.ClientID%>').addClass("parsley-error")     
                            if ($('#<%=txtaFCONTTNM1.ClientID%>').val().replace(" ", "") == "")
                            {
                                 scrollAndFocus('#<%=txtaFCONTTNM1.ClientID%>');
                            }
                            else if($('#<%=txtaFCONTTNM2.ClientID%>').val().replace(" ", "") == "")
                            {
                                 scrollAndFocus('#<%=txtaFCONTTNM2.ClientID%>');
                            }

                               
                            if (chbaFPRECODE.checked && $('#<%=txtaFCONTTNM3.ClientID%>').val().replace(" ", "") == ""){
                                $('#<%=txtaFCONTTNM3.ClientID%>').addClass("parsley-error")                  
                                scrollAndFocus('#<%=txtaFCONTTNM3.ClientID%>');
                            }
                            document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                            return;
                        } else {
                            $('#<%=txtaFCONTTNM1.ClientID%>').removeClass("parsley-error")
                            $('#<%=txtaFCONTTNM2.ClientID%>').removeClass("parsley-error")
                            if (chbaFPRECODE.checked && $('#<%=txtaFCONTTNM3.ClientID%>').val().replace(" ", "") == ""){
                                $('#<%=txtaFCONTTNM3.ClientID%>').removeClass("parsley-error")    
                            }
                            document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
                        }
                        if ($('#<%=txtaFCONTENM.ClientID%>').val().replace(" ", "") == "") {
                            $('#<%=txtaFCONTENM.ClientID%>').addClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";         
                            scrollAndFocus('#<%=txtaFCONTENM.ClientID%>');
                            return;
                        } else {
                            $('#<%=txtaFCONTENM.ClientID%>').removeClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
                        }
                        if ($('#<%=txtaFADD1.ClientID%>').val().replace(" ", "") == "") {
                            $('#<%=txtaFADD1.ClientID%>').addClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";      
                            scrollAndFocus('#<%=txtaFADD1.ClientID%>');
                            return;
                        } else {
                            $('#<%=txtaFADD1.ClientID%>').removeClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";
                        }
                        if ($('#<%=txtaFPROVINCE.ClientID%>').val().replace(" ", "") == "") {
                            $('#<%=txtaFPROVINCE.ClientID%>').addClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "";    
                            scrollAndFocus('#<%=txtaFPROVINCE.ClientID%>');
                            return;
                        } else {
                            $('#<%=txtaFPROVINCE.ClientID%>').removeClass("parsley-error")
                            document.getElementById('ContentPlaceHolder1_lblMassage4').style.display = "none";
                        }
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
                        var hddCheckDeletePicture = document.getElementById('<%=hddCheckDeletePicture.ClientID%>');
                        hddCheckDeletePicture.value = "";
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
                        var hddCheckDeletePicture = document.getElementById('<%=hddCheckDeletePicture.ClientID%>');
                        hddCheckDeletePicture.value = "1";
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
                        if($$('imga').attr('src')==undefined){
                            $$('imga').attr('src',$$('imga').attr('data-default'));
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

                    function CallLoadHrefNewtab(Urls) {
                        window.open(Urls, '_blank');
                    }
                    function EnableFCONTTNM3() {
                        var chbaFPRECODE = document.getElementById('<%=chbaFPRECODE.ClientID%>');
                        var txtaFCONTTNM3 = document.getElementById('<%=txtaFCONTTNM3.ClientID%>');
                        var lblaFBIRTH = document.getElementById('<%=lblaFBIRTH.ClientID%>');
                        var lblaFBIRTH2 = document.getElementById('<%=lblaFBIRTH2.ClientID%>');

                        if (chbaFPRECODE.checked == true){
                            txtaFCONTTNM3.value = "";
                            txtaFCONTTNM3.style.display = '';
                            lblaFBIRTH.style.display = '';
                            lblaFBIRTH2.style.display = 'none';
                        }else{
                            txtaFCONTTNM3.value = "";
                            txtaFCONTTNM3.style.display = 'none';
                            lblaFBIRTH.style.display = 'none';
                            lblaFBIRTH2.style.display = '';
                        }
                    }
    </script>

</asp:Content>

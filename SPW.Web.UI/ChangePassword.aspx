<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ChangePassword.aspx.vb" Inherits="SPW.Web.UI.ChangePassword" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server">
                <asp:HiddenField ID="hddParameterMenuID" runat="server" />
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
                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <h3>
                                                                <asp:Label ID="lblMain3" runat="server"></asp:Label>
                                                            </h3>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:Button ID="btnSaveTemp" runat="server" OnClientClick="javascript:CheckData();return false" CssClass="hide"/>
                                                            <a href="javascript:CheckData();" class="glyph-icon icon-save btn btn-info" title="Save Data">
                                                            
                                                            </a>
                                                            <a href="javascript:CallCancel();" class="btn glyph-icon icon-close btn-danger" title="Cancel Data">
                                                          
                                                            </a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-12 col-sm-12">
                                                        <asp:TextBox ID="txtaOldPassword" autocomplete="off" runat="server" class="form-control" placeholder="Enter your old password" TextMode="Password"></asp:TextBox>
                                                        <asp:Label ID="lblMassage1" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:Label ID="lblPasswordNotPass" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                                <hr style="width: 100%; border: 0.2px dashed; border-color: #1c82e1" />
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-12 col-sm-12">
                                                        <asp:TextBox ID="txtaNewPassword" autocomplete="off" runat="server" class="form-control" placeholder="Enter your new password" TextMode="Password"></asp:TextBox>
                                                        <asp:Label ID="lblMassage2" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                                <hr style="width: 100%; border: 0.2px dashed; border-color: #1c82e1" />
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-12 col-sm-12">
                                                        <asp:TextBox ID="txtaConfirmPassword" autocomplete="off" runat="server" class="form-control" placeholder="Enter your confirm password" TextMode="Password"></asp:TextBox>
                                                        <asp:Label ID="lblMassage3" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:Label ID="lblConfirmNot" runat="server" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="panel-footer">
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-12 col-sm-12">
                                                    </div>
                                                    <div class="col-lg-4 col-md-12 col-sm-12">
                                                        
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
                <asp:Button ID="btnSave" runat="server" Class="hide" />
                <asp:Button ID="btnCancel" runat="server" Class="hide" />
                <asp:Timer ID="Timer1" runat="server" Interval="3000" Enabled="False"></asp:Timer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade .bs-example-modal-sm" id="overlay" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000; opacity: 0.6;">
            <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #000; font-size: 36px; left: 40%; top: 30%;">
                <img src="image/loading.gif" alt="Loading ..." style="height: 200px" /></span>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            SetInitial();
        });

        function SetInitial() {

        }

        function showOverlay() {
            $("#overlay").modal();
        }

        function CheckData() {
            document.getElementById('ContentPlaceHolder1_lblConfirmNot').style.display = "none";
            document.getElementById('ContentPlaceHolder1_lblPasswordNotPass').style.display = "none";
            if ($('#<%=txtaOldPassword.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaOldPassword.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "";
                return;
            } else {
                $('#<%=txtaOldPassword.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage1').style.display = "none";
            }
            if ($('#<%=txtaNewPassword.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaNewPassword.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "";
                return;
            } else {
                $('#<%=txtaNewPassword.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage2').style.display = "none";
            }
            if ($('#<%=txtaConfirmPassword.ClientID%>').val().replace(" ", "") == "") {
                $('#<%=txtaConfirmPassword.ClientID%>').addClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "";
                return;
            } else {
                $('#<%=txtaConfirmPassword.ClientID%>').removeClass("parsley-error")
                document.getElementById('ContentPlaceHolder1_lblMassage3').style.display = "none";
            }

            if ($('#<%=txtaConfirmPassword.ClientID%>').val().replace(" ", "") != $('#<%=txtaNewPassword.ClientID%>').val().replace(" ", "")) {
                document.getElementById('ContentPlaceHolder1_lblConfirmNot').style.display = "";
                return;
            }
            showOverlay();
            __doPostBack('<%= btnSave.UniqueID%>');
            return;
        }

        function CallCancel() {
            showOverlay();
            __doPostBack('<%= btnCancel.UniqueID%>');
        }

        function OpenDialogText() {
            document.getElementById('ContentPlaceHolder1_lblPasswordNotPass').style.display = "";
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

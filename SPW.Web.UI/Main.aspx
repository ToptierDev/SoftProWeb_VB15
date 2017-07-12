<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Main.aspx.vb" Inherits="SPW.Web.UI.Main" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/image/PictureDashboard[6055].jpg" Style="width: 100%; height: 100%;" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

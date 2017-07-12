<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="MST_ForgetPassword.aspx.vb" Inherits="SPW.Web.UI.MST_ForgetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        
        <div class="col-md-12">
            <asp:Label runat="server" ID="lbErrorMessage" ForeColor="Red"></asp:Label>
            </div>
       
    
        <div class="col-md-12">
            UserName
            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        <div class="col-md-12">
            Email
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ></asp:TextBox>
            </div>

          <div class="col-md-8 col-md-offset-4 ">
          
        <asp:Button ID="btnResetPassword" runat="server" 
            CssClass="btn btn-info"
            OnClientClick="return validate()" Text="ResetPassword" />
              </div>
        </div>
    <script>
        function validate()
        {
            if ($('[id$=txtEmail]').val() == '' || $('[id$=txtUserName]').val() == '') {
                alert('กรุณากรอกข้อมูลให้ครบ');
                return false;
            } 
            return true;
        }

    </script>
</asp:Content>

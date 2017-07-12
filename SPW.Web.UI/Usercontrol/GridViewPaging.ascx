<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="GridViewPaging.ascx.vb" Inherits="SPW.Web.UI.GridViewPaging" %>
<style type="text/css">
    .navigationButton
    {
        -webkit-box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        -moz-box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        border-bottom-color: #333;
        border: 1px solid #ffffff;
        background-color: #ffffff;
        border-radius: 5px;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        color: #0d76c3;
        font-family: tahoma;
        font-size: 7pt;
        text-shadow: #b2e2f5 0 1px 0;
        padding: 5px;
        cursor: pointer;      
    }
    .tablePaging
    {
        font-family: "tahoma";
        width: 100%;
        border-collapse: collapse;
    }
    .tablePaging td
    {
        font-size: 1em;
        border: 1px solid #ffffff;
        padding: 3px 7px 2px 7px;
        background-color: #b1dbfa;
        font-size: 8pt;
    }
    .ui-widget-header {
	    color: Black;
        height: 35px;
	    font-weight: bold;
	    -webkit-box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        -moz-box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        border-bottom-color: #333;
        border: 1px solid #ffffff;
        background-color: Gainsboro;
        border-radius: 7px;
        -moz-border-radius: 7px;
        -webkit-border-radius: 7px;
        font-family: tahoma;
        font-size: 10pt;
        padding: 5px;
        cursor: pointer;      
    }
</style>
<table class="ui-widget-header" runat="server" id="tblpagging" style="width:100%;">
    <tr>
        <td style="width: 10%; text-align: center;" class="hidden-xs">
            <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>
        </td>
        <td style="width: 10%; text-align: center;">
            <asp:DropDownList ID="PageRowSize" runat="server" AutoPostBack="true" ForeColor="Black">
                <asp:ListItem >15</asp:ListItem>
                <asp:ListItem Selected="True">25</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 20%; text-align: center;" class="hidden-xs">
            <asp:Label ID="RecordDisplaySummary" runat="server"></asp:Label>
        </td>
        <td style="width: 20%; text-align: center;" class="hidden-xs">
            <asp:Label ID="PageDisplaySummary" runat="server"></asp:Label>
        </td>
        <td style="width: 40%; text-align: center;">
            <asp:Button ID="First"  Font-Bold=" true" runat="server" Text="<<" Width="35px" OnClick="First_Click" CssClass="navigationButton" />&nbsp;
            <asp:Button ID="Previous" runat="server"  Font-Bold="true" Text="<" Width="35px" OnClick="Previous_Click" CssClass="navigationButton" />&nbsp;
            <asp:TextBox ID="SelectedPageNo" runat="server" BorderColor="#DFE8F1"  OnKeyPress="return chkNumber(this)" BorderStyle="Solid" BorderWidth="1px" Width="50px" Style="text-align: center;height: 25px;color:black;" MaxLength="8"></asp:TextBox>&nbsp;
            <asp:Button ID="GO" runat="server" Font-Bold=" true" Text="GO" Width="35px" OnClick="GO_Click" CssClass="navigationButton" />&nbsp;
            <asp:Button ID="Next" runat="server" Font-Bold="true" Text=">" Width="35px" OnClick="Next_Click" CssClass="navigationButton" />&nbsp;
            <asp:Button ID="Last" runat="server" Font-Bold="true" Text=">>" Width="35px" OnClick="Last_Click"
                CssClass="navigationButton" />
        </td>
    </tr>
    <tr id="trErrorMessage" runat="server" visible="false">
        <td colspan="4" style="background-color: #e9e1e1;">
            <asp:Label ID="GridViewPagingError" runat="server" Font-Names="Verdana" Font-Size="9pt"
                ForeColor="Red"></asp:Label>
            <asp:HiddenField ID="hddTotalRows" runat="server" Value="0" />
            <asp:HiddenField ID="hddCurrentPage" runat="server" Value="0" />
        </td>
    </tr>
</table>
<script type="text/javascript">

    function chkNumber(ele) {

        var vchar = String.fromCharCode(event.keyCode);

        if ((vchar < '0' || vchar > '9') && (vchar != '.')) return false;
        ele.onKeyPress = vchar;

    }
</script>
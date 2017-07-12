<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="UTL_Language.aspx.vb" Inherits="SPW.Web.UI.UTL_Language" %>

<%@ Register Src="~/Usercontrol/AngularJSScript.ascx" TagPrefix="uc1" TagName="AngularJSScript" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <uc1:AngularJSScript runat="server" ID="AngularJSScript" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class=" form-control-container" ng-app="myApp" ng-controller="mainController" >

        <div class="row">
            <div class="col-sm-2"><%=grtt("resResourceName") %></div>
            <div class="col-sm-4">
                <input ng-model="txtResourceName" /></div>
            <div class="col-sm-2"><%=grtt("resResourceValueLC") %></div>
            <div class="col-sm-4">
                <input ng-model="txtResourceValueLC" /></div>
            <div class="col-sm-2"><%=grtt("resResourceValueEN") %></div>
            <div class="col-sm-4">
                <input ng-model="txtResourceValueEN" /></div>
            <div class="col-sm-2"><%=grtt("resResourceType") %></div>
            <div class="col-sm-4">
                <input ng-model="txtResourceType" /></div>
            <div class="col-sm-2"><%=grtt("resMenuID") %></div>
            <div class="col-sm-4">
                <input ng-model="txtMenuID" /></div>
            <div class="col-sm-6">
                <a class="btn btn-info" ng-click="search()">
                    <i class="glyph-icon icon-search"></i><%=grtt("resSearch") %></a>
           
                <a class="btn btn-info" ng-click="searchUnset()">
                    <i class="glyph-icon icon-search"></i><%=grtt("resUnsetResource") %></a>
            </div>
        </div>
        <div class="row">
        <a class="btn btn-info" ng-click="save()"><%=grtt("resSave") %></a>
        <a class="btn btn-info" ng-click="cleanResource()">Clear empty resource</a>

          <table >
                                                <tr>
                                                    <th><%=grtt("resResourceName") %></th>
                                                    <th><%=grtt("resResourceValueLC") %></th>
                                                    <th><%=grtt("resResourceValueEN") %></th>
                                                    <th><%=grtt("resResourceType") %></th>
                                                    <th><%=grtt("resMenuID") %></th>

                                                </tr>
                                                <tr ng-repeat="s in searchResult" ng-click="$last && addNewRow()">

                                                    <td>
                                                        <input type="text" ng-model="s.ResourceName"  />
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="s.ResourceValueLC"  />
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="s.ResourceValueEN"  />
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="s.ResourceType"  />
                                                    </td>
                                                    <td>
                                                        <input type="text" ng-model="s.MenuID"  />
                                                    </td>
                                                 
                                                </tr>
                                            </table>
        
        </div>


      </div> <script type="text/javascript">
        var rootHost = window.location.origin + '<%=Request.ApplicationPath %>';
        var culture = '<%= CultureDate%>';
        var systemDateString = '<%=Me.ToSystemDateString(DateTime.Now)%>';
    </script>
    <script src="Scripts_ngapp/UTL_Language.js?v=<%=Me.assetVersion %>"></script>
    <script>
        function OpenDialogError(Msg) {
            noty({
                text: '<i class="glyph-icon icon-times-circle mrg5R"></i> ' + Msg,
                type: 'error',
                dismissQueue: true,
                theme: 'agileUI',
                layout: 'center'
            });
            //closeOverlay();
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

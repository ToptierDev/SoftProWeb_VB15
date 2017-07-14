<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AngularJSScript.ascx.vb" Inherits="SPW.Web.UI.AngularJSScript" %>

<%          Dim version = ConfigurationManager.AppSettings("assetversion") %>
<%--   <link rel="stylesheet" href="https://cdn.gitcdn.link/cdn/angular/bower-material/v1.1.4/angular-material.css" />
    <link rel="stylesheet" href="https://material.angularjs.org/1.1.4/docs.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-animate.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-route.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-aria.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular-messages.min.js"></script>
    <script src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/t-114/svg-assets-cache.js"></script>
    <script src="https://cdn.gitcdn.link/cdn/angular/bower-material/v1.1.4/angular-material.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.6/moment.js"></script>
    
    <link rel="stylesheet" href="https://unpkg.com/ng-table@2.0.2/bundles/ng-table.min.css" />
    <script src="https://unpkg.com/ng-table@2.0.2/bundles/ng-table.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/angular-i18n/1.6.4/angular-locale_th-th.js"></script>
    <script src="http://assisrafael.github.io/angular-input-masks/javascripts/masks.js"></script>--%>

   <link rel="stylesheet" href="Scripts/angular-material.css" />
    <link rel="stylesheet" href="Scripts/docs.css" />
    <script src="Scripts/angular.js"></script>
    <script src="Scripts/angular-animate.min.js"></script>
    <script src="Scripts/angular-route.min.js"></script>
    <script src="Scripts/angular-aria.min.js"></script>
    <script src="Scripts/angular-messages.min.js"></script>
    <script src="Scripts/svg-assets-cache.js"></script>
    <script src="Scripts/angular-material.js"></script>
    <script src="Scripts/moment.js"></script>
    
    <link rel="stylesheet" href="Scripts/ng-table.min.css" />
    <script src="Scripts/ng-table.min.js"></script>
    <script src="Scripts/angular-locale_th-th.js"></script>
   <%-- <script src="Scripts/masks.js"></script>--%>
    <script src="Scripts/dynamic-number.js"></script>
<link href="Style/ng-custom.css?v=<%=version %>" rel="stylesheet" />
  <script data-require="ui-bootstrap@*" data-semver="0.10.0" src="http://angular-ui.github.io/bootstrap/ui-bootstrap-tpls-0.10.0.js"></script>
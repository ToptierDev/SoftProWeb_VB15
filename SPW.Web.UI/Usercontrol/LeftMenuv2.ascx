<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="LeftMenuv2.ascx.vb" Inherits="SPW.Web.UI.LeftMenuv2" %>
<%@ Import Namespace="SPW.Helper" %>
<%@ Import Namespace="SPW.BLL" %>
<%@ Import Namespace="SPW.DAL" %>
<div id="page-sidebar" style="margin-top: 76px; position: fixed;">
    <div class="scroll-sidebar">
        <ul id="sidebar-menu">
            <%         
                Dim oBasePage As New BasePage()
                If oBasePage.MasterMenu IsNot Nothing Then
                    Dim oMenuDefault = oBasePage.MasterMenu.Where(Function(q) q.MenuID = 1 And q.EnableFlag = 1).FirstOrDefault
                    If oMenuDefault Is Nothing Then
                        oBasePage.MasterMenu(Nothing)
                        If Request.QueryString("pFlag") Is Nothing Then
                            Call Redirect("Default.aspx")
                        End If
                    End If

            %>
            <li>
                <a href="Main.aspx" class="groupSideBar tooltip-button" data-toggle="tooltip" data-placement="right" data-original-title="<%=IIf(oBasePage.WebCulture.ToUpper = "TH", "หน้าหลัก", "Home") %>">
                    <%--<i class='glyph-icon icon-data32'></i>--%>
                    <% 
                        Response.Write("<i class='" & oMenuDefault.MenuIcon & "'></i>")
                    %>

                    <span>
                        <% 
                            If oBasePage.WebCulture = "TH" Then
                                Response.Write(oMenuDefault.MenuNameLC.ToString())
                            Else
                                Response.Write(oMenuDefault.MenuNameEN.ToString())
                            End If
                        %>
                    </span>
                </a>
                <!-- .sidebar-submenu -->
            </li>
            <%--<hr style="margin-top: 0px; margin-bottom: 0px;" />--%>
            <%
                End If
            %>
            <%
                If (Not oBasePage.MasterMenu Is Nothing) Then
                    Dim oMenuList = oBasePage.MasterMenu
                    Dim oModuleList = oBasePage.MasterMenu
                    Dim possibleMenuLists As List(Of Integer?) = oModuleList.Where(Function(s) s.ModuleID IsNot Nothing).OrderBy(Function(s) s.ModuleID).Select(Function(s) s.ModuleID).Distinct.ToList()
                    For Each oModule In oModuleList.Where(Function(s) possibleMenuLists.Contains(s.ModuleID)).OrderBy(Function(s) s.ModuleID).Select(Function(s) s.ModuleID).Distinct
                        If oModule IsNot Nothing Then
                            If oBasePage.WebCulture = "TH" Then
                                Dim possibleMeoduleNameLists As MasterMenu_ViewModel = oModuleList.Where(Function(s) s.ModuleID = oModule And s.ModuleID IsNot Nothing).FirstOrDefault
                                'Response.Write("<li class='header'><span>" & IIf(possibleMeoduleNameLists.ModuleNameLC.ToString().Length > 29, Left(possibleMeoduleNameLists.ModuleNameLC.ToString(), 25) & "...", possibleMeoduleNameLists.ModuleNameLC.ToString()) & "</span></li>")
            %>
            <li class="<% Response.Write(IIf(oBasePage.GetCurrentWebModulePage = CStr(oModule), "Tests", ""))%>">
                <%
                        Response.Write("<a href='#' class='tooltip-button' data-toggle='tooltip' data-placement='right' data-original-title='" & IIf(oBasePage.WebCulture.ToUpper = "TH", possibleMeoduleNameLists.ModuleNameLC.ToString(), possibleMeoduleNameLists.ModuleNameLC.ToString()) & "'><span class='fontModule'>" & IIf(possibleMeoduleNameLists.ModuleNameLC.ToString().Length > 25, Left(possibleMeoduleNameLists.ModuleNameLC.ToString(), 20) & "...", possibleMeoduleNameLists.ModuleNameLC.ToString()) & "</span><i class='" & possibleMeoduleNameLists.MenuIcon & "'></i></a>")
                    Else
                        Dim possibleMeoduleNameLists As MasterMenu_ViewModel = oModuleList.Where(Function(s) s.ModuleID = oModule And s.ModuleID IsNot Nothing).FirstOrDefault
                        'Response.Write("<li class='header'><span>" & IIf(possibleMeoduleNameLists.ModuleNameEN.ToString().Length > 29, Left(possibleMeoduleNameLists.ModuleNameEN.ToString(), 25) & "...", possibleMeoduleNameLists.ModuleNameEN.ToString()) & "</span></li>")
                %>
            <li class="<% Response.Write(IIf(oBasePage.GetCurrentWebModulePage = CStr(oModule), "Tests", ""))%>">
                <%
                        Response.Write("<a href='#' class='tooltip-button' data-toggle='tooltip' data-placement='right' data-original-title='" & IIf(oBasePage.WebCulture.ToUpper = "EN", possibleMeoduleNameLists.ModuleNameEN.ToString(), possibleMeoduleNameLists.ModuleNameEN.ToString()) & "'><span class='fontModule'>" & IIf(possibleMeoduleNameLists.ModuleNameEN.ToString().Length > 15, Left(possibleMeoduleNameLists.ModuleNameEN.ToString(), 12) & "...", possibleMeoduleNameLists.ModuleNameEN.ToString()) & "</span><i class='" & possibleMeoduleNameLists.MenuIcon & "'></i></a>")
                    End If
                    Dim oHeaderList = oBasePage.MasterMenu
                    Dim possibleHeaderLists As List(Of Integer?) = oHeaderList.Where(Function(s) s.ModuleID = oModule And s.ModuleID IsNot Nothing And s.ParentID IsNot Nothing).Select(Function(s) s.ParentID).Distinct.ToList()

                %>
                <!-- menu level 2 -->
                <div class="sidebar-submenu <% Response.Write(IIf(oBasePage.GetCurrentWebModulePage = CStr(oModule), "Openmenu", ""))%>">
                    <ul id="sidebar-menus">
                        <%
                            For Each oMasterMenu In oMenuList.Where(Function(q) possibleHeaderLists.Contains(q.MenuID) And q.ParentID Is Nothing And q.MenuID <> 1 And q.EnableFlag = 1)

                        %>

                        <li>
                            <%
                                If oMasterMenu.MenuLocation = "#" Then
                                    Response.Write("<a href='#' class='nogyp tooltip-button' data-toggle='tooltip' data-placement='right' data-original-title='" & IIf(oBasePage.WebCulture.ToUpper = "TH", oMasterMenu.MenuNameLC.ToString(), oMasterMenu.MenuNameEN.ToString()) & "'>")
                                Else
                                    If oBasePage.WebCulture.ToUpper = "TH" Then
                                        'If oMasterMenu.MenuNameLC.ToString.Length > 29 Then
                                        Response.Write("<a href='" & oMasterMenu.MenuLocation & "' class='groupSideBar tooltip-button'  data-toggle='tooltip' data-placement='right' data-original-title='" & IIf(oBasePage.WebCulture.ToUpper = "TH", oMasterMenu.MenuNameLC.ToString(), oMasterMenu.MenuNameEN.ToString()) & "'>")
                                        'Else
                                        '    Response.Write("<a href='" & oMasterMenu.MenuLocation & "' class='groupSideBar' style='margin-left:10px;'>")
                                        'End If
                                    Else
                                        'If oMasterMenu.MenuNameEN.ToString.Length > 29 Then
                                        Response.Write("<a href='" & oMasterMenu.MenuLocation & "' class='groupSideBar tooltip-button'  data-toggle='tooltip' data-placement='right' data-original-title='" & IIf(oBasePage.WebCulture.ToUpper = "TH", oMasterMenu.MenuNameLC.ToString(), oMasterMenu.MenuNameEN.ToString()) & "'>")
                                        'Else
                                        '    Response.Write("<a href='" & oMasterMenu.MenuLocation & "' class='groupSideBar' style='margin-left:10px;'>")
                                        'End If
                                    End If
                                End If

                                Response.Write("<i class='" & oMasterMenu.MenuIcon & "'></i>")

                                If oBasePage.WebCulture.ToUpper = "TH" Then
                                    Response.Write("<span>" & IIf(oMasterMenu.MenuNameLC.ToString().Length > 25, Left(oMasterMenu.MenuNameLC.ToString(), 21) & "...", oMasterMenu.MenuNameLC.ToString()) & "</span></a>")
                                Else
                                    Response.Write("<span>" & IIf(oMasterMenu.MenuNameEN.ToString().Length > 25, Left(oMasterMenu.MenuNameEN.ToString(), 21) & "...", oMasterMenu.MenuNameEN.ToString()) & "</span></a>")
                                End If
                            %>
                            <%
                                If oMasterMenu.MenuLocation = "#" Then
                            %>
                            <!-- menu level 3 -->
                            <div class="sidebar-submenu <% Response.Write(IIf(oBasePage.GetCurrentWebPage = CStr(oMasterMenu.MenuID), "Openmenu", ""))%>">
                                <ul>
                                    <%
                                        For Each oParentMenu In oMenuList.Where(Function(q) q.ModuleID = oModule And q.ParentID.Equals(oMasterMenu.MenuID) And q.EnableFlag = 1)
                                    %>
                                    <li>
                                        <%
                                            If oParentMenu.MenuLocation = "#" Then
                                                Response.Write("<a href='#'  class='tooltip-button' data-toggle='tooltip' data-placement='right' data-original-title='" & IIf(oBasePage.WebCulture.ToUpper = "TH", oParentMenu.MenuNameLC.ToString(), oParentMenu.MenuNameEN.ToString()) & "'>")

                                                If oBasePage.WebCulture.ToUpper = "TH" Then
                                                    Response.Write("<span>" & IIf(oParentMenu.MenuNameLC.ToString().Length > 23, Left(oParentMenu.MenuNameLC.ToString(), 18) & "...", oParentMenu.MenuNameLC.ToString()) & "</span></a>")
                                                Else
                                                    Response.Write("<span>" & IIf(oParentMenu.MenuNameEN.ToString().Length > 23, Left(oParentMenu.MenuNameEN.ToString(), 18) & "...", oParentMenu.MenuNameEN.ToString()) & "</span></a>")
                                                End If
                                        %>
                                        <div class="sidebar-submenu">
                                            <ul>
                                                <%
                                                    For Each oParentMenu2 In oMenuList.Where(Function(q) q.ModuleID = oModule And q.ParentID.Equals(oParentMenu.MenuID) And q.EnableFlag = 1)
                                                %>
                                                <li>
                                                    <%

                                                        If oBasePage.WebCulture.ToUpper = "TH" Then
                                                            'If oParentMenu2.MenuNameLC.ToString.Length > 21 Then
                                                            Response.Write("<a href='" & oParentMenu2.MenuLocation & "' class='groupSideBar tooltip-button' data-toggle='tooltip' data-placement='right' data-original-title='" & IIf(oBasePage.WebCulture.ToUpper = "TH", oParentMenu2.MenuNameLC.ToString(), oParentMenu2.MenuNameEN.ToString()) & "'>")
                                                            'Else
                                                            '    Response.Write("<a href='" & oParentMenu2.MenuLocation & "' class='groupSideBar tooltip-button' style='margin-left:20px;'>")
                                                            'End If
                                                        Else
                                                            'If oParentMenu2.MenuNameEN.ToString.Length > 21 Then
                                                            Response.Write("<a href='" & oParentMenu2.MenuLocation & "' class='groupSideBar tooltip-button' data-toggle='tooltip' data-placement='right' data-original-title='" & IIf(oBasePage.WebCulture.ToUpper = "TH", oParentMenu2.MenuNameLC.ToString(), oParentMenu2.MenuNameEN.ToString()) & "'>")
                                                            'Else
                                                            '    Response.Write("<a href='" & oParentMenu2.MenuLocation & "' class='groupSideBar tooltip-button' style='margin-left:20px;'>")
                                                            'End If
                                                        End If
                                                        If oBasePage.WebCulture.ToUpper = "TH" Then
                                                            Response.Write("<span>" & IIf(oParentMenu2.MenuNameLC.ToString().Length > 25, Left(oParentMenu2.MenuNameLC.ToString(), 21) & "...", oParentMenu2.MenuNameLC.ToString()) & "</span></a>")
                                                        Else
                                                            Response.Write("<span>" & IIf(oParentMenu2.MenuNameEN.ToString().Length > 25, Left(oParentMenu2.MenuNameEN.ToString(), 21) & "...", oParentMenu2.MenuNameEN.ToString()) & "</span></a>")
                                                        End If
                                                    %>
                                                </li>
                                                <%
                                                    Next
                                                %>
                                            </ul>
                                        </div>
                                        <%
                                            Else
                                                If oBasePage.WebCulture.ToUpper = "TH" Then
                                                    'If oParentMenu.MenuNameLC.ToString.Length > 25 Then
                                                    Response.Write("<a href='" & oParentMenu.MenuLocation & "' class='groupSideBar tooltip-button'  data-toggle='tooltip' data-placement='right' data-original-title='" & IIf(oBasePage.WebCulture.ToUpper = "TH", oParentMenu.MenuNameLC.ToString(), oParentMenu.MenuNameEN.ToString()) & "'>")
                                                    'Else
                                                    '    Response.Write("<a href='" & oParentMenu.MenuLocation & "' class='groupSideBar tooltip-button' style='margin-left:10px;'>")
                                                    'End If
                                                Else
                                                    'If oParentMenu.MenuNameEN.ToString.Length > 25 Then
                                                    Response.Write("<a href='" & oParentMenu.MenuLocation & "' class='groupSideBar tooltip-button'  data-toggle='tooltip' data-placement='right' data-original-title='" & IIf(oBasePage.WebCulture.ToUpper = "TH", oParentMenu.MenuNameLC.ToString(), oParentMenu.MenuNameEN.ToString()) & "'>")
                                                    'Else
                                                    '    Response.Write("<a href='" & oParentMenu.MenuLocation & "' class='groupSideBar tooltip-button' style='margin-left:10px;'>")
                                                    'End If
                                                End If

                                                If oBasePage.WebCulture.ToUpper = "TH" Then
                                                    Response.Write("<span>" & IIf(oParentMenu.MenuNameLC.ToString().Length > 25, Left(oParentMenu.MenuNameLC.ToString(), 21) & "...", oParentMenu.MenuNameLC.ToString()) & "</span></a>")
                                                Else
                                                    Response.Write("<span>" & IIf(oParentMenu.MenuNameEN.ToString().Length > 25, Left(oParentMenu.MenuNameEN.ToString(), 21) & "...", oParentMenu.MenuNameEN.ToString()) & "</span></a>")
                                                End If
                                            End If
                                        %>
                                    </li>
                                    <%
                                        Next
                                    %>
                                </ul>
                            </div>
                        </li>

                        <%
                                End If
                            Next
                        %>
                    </ul>
                </div>
                <!-- here -->
                <%
                    End If
                %>
            </li>
            <%


                    Next
                End If

            %>
            <!-- .sidebar-submenu -->
            <li>
                <a href="http://192.168.1.8/HTMLMapinfo/index.aspx" class="groupSideBar">
                    <i class="glyph-icon icon-map32"></i>
                    <span>Display Map
                    </span>
                </a>
            </li>
            <!-- #sidebar-menu -->
        </ul>
    </div>
</div>
<script type="text/javascript">
    function OpenModuleSpecial() {
        if ($("#sidebar-menu li").hasClass("sfHover")) {
            $("#sidebar-menu li").hide();
            $("#sidebar-menu li.sfHover").show();
            $("#sidebar-menu li.sfHover li").show();
        } else {
            $("#sidebar-menu li").show();
        }
    }
</script>

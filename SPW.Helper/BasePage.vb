Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Globalization
Imports System.Threading
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.DAL
Imports System.Configuration

Public Class BasePage
    Inherits System.Web.UI.Page
    Public CultureDate As String
    Public MenuID As Integer
    Public oCoreMenu As CoreMenu
    Public oCoreUsersMenu As CoreUsersMenu
    Public assetVersion As String
    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        If CurrentUser() Is Nothing Then
            Session.Abandon()

            If Request.QueryString("pFlag") Is Nothing Then
                Call Redirect("Default.aspx")
            End If
        Else
            Dim sPagePath = System.Web.HttpContext.Current.Request.Url.AbsolutePath
            Dim oFileInfo = New System.IO.FileInfo(sPagePath)
            Dim sPageName = oFileInfo.Name
            setMenuIDByMenuLocation(sPageName)
            '' setMenuIDByMenuLocation(HttpContext.Current.Request.Url.AbsolutePath.Substring(1))
        End If
        If WebCulture().ToLower = "th" Then
            CultureDate = "th-TH"
        Else
            CultureDate = "en-US"
        End If
        assetVersion = ConfigurationManager.AppSettings("assetversion")

        MyBase.OnLoad(e)
    End Sub
    Public Function setMenuIDByMenuLocation(ByVal strManuLocation As String) As Boolean
        Try
            Using db As New PNSDBWEBEntities
                Dim lcMenu As CoreMenu = db.CoreMenus.Where(Function(s) s.EnableFlag = 1 _
                                                                   And s.MenuLocation = strManuLocation.Replace("/", "")).FirstOrDefault
                MenuID = lcMenu.MenuID
                oCoreMenu = lcMenu
                Dim userID = CurrentUser().UserID
                oCoreUsersMenu = db.CoreUsersMenus.Where(Function(s) s.MenuID = MenuID And s.UserID = userID).FirstOrDefault()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Protected Overrides Sub InitializeCulture()
        MyBase.InitializeCulture()
    End Sub

    Public Function GetResourceObject(ByVal strResourceName As String,
                                      ByVal strResourceType As String,
                                      ByVal strResourceMenuID As String) As CoreWebResource
        Try
            Using db As New PNSDBWEBEntities
                Dim oResource = db.CoreWebResources.Where(Function(s) s.ResourceName.ToLower() = strResourceName.ToLower() And s.ResourceType.ToLower() = strResourceType.ToLower() And s.MenuID = strResourceMenuID).FirstOrDefault
                Return oResource
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GetMenuName() As String
        If (oCoreMenu IsNot Nothing) Then
            If WebCulture.ToLower = "th" Then
                Return oCoreMenu.MenuNameLC
            Else
                Return oCoreMenu.MenuNameEN
            End If
        End If
        Return "Not Set"
    End Function
    Public Function GetPermission() As CoreUsersMenu
        If (oCoreUsersMenu IsNot Nothing) Then
            Return oCoreUsersMenu
        Else
            Return New CoreUsersMenu()
        End If

    End Function

    Public Function GetNavigator() As String
        If (oCoreMenu IsNot Nothing) Then
            Dim lstMenu As New List(Of CoreMenu)
            Dim oModule As CoreModule
            Dim seperator = " / "
            Using db As New PNSDBWEBEntities
                oModule = db.CoreModules.Where(Function(s) s.ModuleID = oCoreMenu.ModuleID).FirstOrDefault
            End Using
            '  lstMenu.Add(oCoreMenu)
            getAllParentName(oCoreMenu, lstMenu)

            Dim sMenu = ""
            If WebCulture.ToLower = "th" Then
                sMenu += oModule.ModuleNameTH
            Else
                sMenu += oModule.ModuleNameEN
            End If
            sMenu += seperator
            For i As Integer = lstMenu.Count - 1 To 0 Step -1
                If WebCulture.ToLower = "th" Then
                    sMenu += lstMenu(i).MenuNameLC
                Else
                    sMenu += lstMenu(i).MenuNameEN
                End If
                If (i <> 0) Then
                    sMenu += seperator
                End If
            Next
            Return sMenu
        End If
        Return "Not Set"
    End Function

    Public Function getAllParentName(ByVal oCoreMenu As CoreMenu, ByRef lstMenu As List(Of CoreMenu)) As CoreMenu
        If (IsNothing(oCoreMenu.ParentID)) Then

            Return oCoreMenu
        Else
            Using db As New PNSDBWEBEntities
                Dim m As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuID = oCoreMenu.ParentID).FirstOrDefault
                oCoreMenu = m
            End Using
            lstMenu.Add(oCoreMenu)
            Return getAllParentName(oCoreMenu, lstMenu)
        End If


    End Function


    ''' <summary>
    ''' shot function for clean code in aspx
    ''' </summary>
    ''' <param name="strResourceName"></param>
    ''' <returns></returns>
    Public Function grtt(ByVal strResourceName As String) As String
        Return GetResource(strResourceName, "Text", MenuID)
    End Function
    Public Function GetResourceTypeText(ByVal strResourceName As String) As String
        Return GetResource(strResourceName, "Text", MenuID)
    End Function
    Public Function GetResource(ByVal strResourceName As String,
                                ByVal strResourceType As String) As String
        Return GetResource(strResourceName, strResourceType, MenuID)
    End Function


    Public Function GetResource(ByVal strResourceName As String,
                                ByVal strResourceType As String,
                                ByVal strResourceMenuID As String) As String

        Try


            Dim ctx As PNSDBWEBEntities = PNSDBWEBEntities.Context
            ' Dim resultsList As List(Of CoreWebResource) ' = ctx.CoreWebResources.ToList()
            Dim resource As CoreWebResource
            resource = ctx.CoreWebResources.Where(Function(s) s.ResourceName.ToLower() = strResourceName.ToLower() _
                                         And s.ResourceType.ToLower() = strResourceType.ToLower() _
                                         And (s.MenuID = strResourceMenuID _
                                         Or s.MenuID = 99999)).OrderBy(Function(s) s.MenuID).FirstOrDefault()
            If resource IsNot Nothing Then
                If WebCulture() IsNot Nothing Then
                    If WebCulture.ToLower = "th" Then
                        If Not IsDBNull(resource.ResourceValueLC) Then
                            Return resource.ResourceValueLC
                        End If
                    ElseIf WebCulture.ToLower = "en" Then
                        If Not IsDBNull(resource.ResourceValueLC) Then
                            Return resource.ResourceValueEN
                        End If
                    End If
                Else
                    If Not IsDBNull(resource.ResourceValueLC) Then
                        Return resource.ResourceValueLC
                    End If
                End If
            Else
                'Throw New Exception(String.Format("No resource name [{0}]", strResourceName))

                HelperLog.ErrorLog("0", "99999", Request.UserHostAddress(), "GetResource", New Exception(strResourceName))
                Return strResourceName
            End If
        Catch ex As Exception
            HelperLog.ErrorLog("0", "99999", Request.UserHostAddress(), "GetResource", ex)
            Return strResourceName
        End Try
    End Function


    Public Function GetWebMessage(ByVal strResourceName As String,
                                  ByVal strResourceType As String,
                                  ByVal strResourceMenuID As String) As String
        Return GetResource(strResourceName, strResourceType, strResourceMenuID)
    End Function

    Public Function WebCulture() As String
        If Session("PRR.application.language") Is Nothing Then
            Session.Add("PRR.application.language", "th")
        End If



        Return Session("PRR.application.language").ToString
    End Function
    Public Function WebCulture(ByVal ptLang$) As Boolean
        Try

            If Session("PRR.application.language") Is Nothing Then
                Session.Add("PRR.application.language", ptLang)
            Else
                Session("PRR.application.language") = ptLang
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function WebCompany() As String
        Return Session("PRR.application.Company").ToString
    End Function
    Public Function WebCompany(ByVal ptKey$) As Boolean
        Try
            If Session("PRR.application.Company") Is Nothing Then
                Session.Add("PRR.application.Company", ptKey)
            Else
                Session("PRR.application.Company") = ptKey
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function WebModule() As String
        Return Session("PRR.application.Module").ToString
    End Function
    Public Function WebModule(ByVal ptKey$) As Boolean
        Try
            If Session("PRR.application.Module") Is Nothing Then
                Session.Add("PRR.application.Module", ptKey)
            Else
                Session("PRR.application.Module") = ptKey
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CurrentUser() As CoreUser
        If Session("PRR.application.user_authen") Is Nothing Then
            Dim AutoLoginForTest As String = String.Empty
            Try
                AutoLoginForTest = ConfigurationManager.AppSettings("AutoLoginForTest")
            Catch ex As Exception

            End Try
            ''################# only for dev, please remove on production"
            'begin "
            If AutoLoginForTest = "true" Then
                Dim bl As New BLL.cLogin
                Dim lc As New CoreUser
                Dim blMenu As New BLL.cMenu
                ' lc = bl.LoginUser("adm", "toptier1234")
                lc = bl.LoginUserByAdmin("amd")
                If lc IsNot Nothing Then
                    Call SetCurrentUser(lc)
                    Call WebCompany("")
                    Call WebCulture("TH")
                    Call MasterMenu(blMenu.GetMenu("amd", "", ""))
                    Return Session("PRR.application.user_authen")
                End If
            End If
            ''################# end add
            Session.Abandon()
            If Request.QueryString("pFlag") Is Nothing Then
                Call Redirect("Default.aspx")
            End If
        End If
        Return Session("PRR.application.user_authen")
    End Function

    Public Function SetCurrentUser(ByVal poUser As CoreUser) As Boolean
        Try
            Session.Add("PRR.application.user_authen", poUser)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SetCurrentCompany(ByVal poCompany As BD10DIVI) As Boolean
        Try
            Session.Add("PRR.application.company_authen", poCompany)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetCurrentWebPage() As String
        Try
            Dim tWebPageList = HttpContext.Current.Request.Url.AbsolutePath.Split("/")
            Dim tWebPage = tWebPageList(tWebPageList.Count - 1).ToUpper
            Using db As New PNSDBWEBEntities
                Dim lc As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuLocation.ToUpper = tWebPage).FirstOrDefault
                If lc IsNot Nothing Then
                    tWebPage = lc.MenuID
                    Dim lcSubmenuLevel1 As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuID = lc.ParentID).FirstOrDefault
                    If lcSubmenuLevel1 IsNot Nothing Then
                        Dim lcHeaderMenu As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuID = lcSubmenuLevel1.ParentID).FirstOrDefault
                        If lcHeaderMenu IsNot Nothing Then
                            tWebPage = lcHeaderMenu.MenuID
                        End If
                    End If
                End If
            End Using
            Return tWebPage
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetCurrentWebModulePage() As String
        Try
            Dim tWebPageList = HttpContext.Current.Request.Url.AbsolutePath.Split("/")
            Dim tWebPage = tWebPageList(tWebPageList.Count - 1).ToUpper
            Using db As New PNSDBWEBEntities
                Dim lc As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuLocation.ToUpper = tWebPage).FirstOrDefault
                If lc IsNot Nothing Then
                    tWebPage = lc.ModuleID
                End If
            End Using
            Return tWebPage
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetCurrentMenuID() As String
        Try
            Dim tWebPageList = HttpContext.Current.Request.Url.AbsolutePath.Split("/")
            Dim tWebPage = tWebPageList(tWebPageList.Count - 1).ToUpper
            Dim strMenu1 As String = "0"
            Dim strMenu2 As String = "0"
            Using db As New PNSDBWEBEntities
                Dim lc As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuLocation.ToUpper = tWebPage).FirstOrDefault
                If lc IsNot Nothing Then
                    If lc.ParentID IsNot Nothing Then
                        strMenu1 = lc.ParentID
                        Dim lst As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuID = strMenu1).FirstOrDefault
                        If lst IsNot Nothing Then
                            If lst.ParentID IsNot Nothing Then
                                strMenu2 = lst.ParentID
                            End If
                        End If
                    End If
                End If
                If strMenu2 = "0" And strMenu1 <> "0" Then
                    Return strMenu2 & "|" & strMenu1
                ElseIf strMenu2 <> "0" And strMenu1 <> "0" Then
                    Return strMenu1 & "|" & strMenu2
                Else
                    Return strMenu1 & "|" & strMenu2
                End If
            End Using
            Return tWebPage
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetCurrentUser() As CoreMenu
        Try
            Return Session("PRR.application.user_authen")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function MasterMenu() As List(Of MasterMenu_ViewModel)
        Try
            Return Session("PRR.application.master_menu")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function MasterMenu(ByVal poCoreMenuList As List(Of MasterMenu_ViewModel)) As Boolean
        Try
            Session.Add("PRR.application.master_menu", poCoreMenuList)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Property CurrentPage() As Integer
        Get
            If ViewState("PRR.PageSize") IsNot Nothing Then
                Return ViewState("PRR.CurrentPage")
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            ViewState.Add("PRR.CurrentPage", value)
        End Set
    End Property

    Public Function ClearMessageList() As Boolean
        Try
            Session("PRR.application.messageList") = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Property TotalRow() As Integer
        Get
            If ViewState("PRR.TotalRow") IsNot Nothing Then
                Return ViewState("PRR.TotalRow")
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            ViewState.Add("PRR.TotalRow", value)
        End Set
    End Property

    Public Property PageSize() As Integer
        Get
            If ViewState("PRR.PageSize") IsNot Nothing Then
                If ViewState("PRR.PageSize") = 0 Then
                    If TotalRow = 0 Then
                        Return 25
                    End If
                    Return TotalRow
                Else
                    Return ViewState("PRR.PageSize")
                End If
            Else
                Return 25
            End If
        End Get
        Set(ByVal value As Integer)
            ViewState.Add("PRR.PageSize", value)
        End Set
    End Property

    Public Property PageSizeAll() As Integer
        Get
            If ViewState("PRR.PageSize") IsNot Nothing Then
                If ViewState("PRR.PageSize") = 0 Then
                    If TotalRow = 0 Then
                        Return 0
                    End If
                    Return TotalRow
                Else
                    Return ViewState("PRR.PageSize")
                End If
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            ViewState.Add("PRR.PageSize", value)
        End Set
    End Property

    Public Property SortType() As Byte
        Get
            If ViewState("PRR.SortType") IsNot Nothing Then
                Return ViewState("PRR.SortType")
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Byte)
            ViewState.Add("PRR.SortType", value)
        End Set
    End Property

    Public Property SortBy() As String
        Get
            If ViewState("PRR.SortBy") IsNot Nothing Then
                Return ViewState("PRR.SortBy")
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState.Add("PRR.SortBy", value)
        End Set
    End Property
    Public Sub ClearSessionPageLoad(ByVal UrlComming As String)
        If UrlComming <> "MST_Department.aspx" Then
            Session.Remove("MST_Department_PageInfo")
            Session.Remove("MST_Department_Search")
            Session.Remove("MST_Department_pageLength")
        End If
        If UrlComming <> "MST_Division.aspx" Then
            Session.Remove("MST_Division_PageInfo")
            Session.Remove("MST_Division_Search")
            Session.Remove("MST_Division_pageLength")
        End If
        If UrlComming <> "MST_Menu.aspx" Then
            Session.Remove("MST_Menu_PageInfo")
            Session.Remove("MST_Menu_Search")
            Session.Remove("MST_Menu_pageLength")
        End If
        If UrlComming <> "MST_SaleManGroup.aspx" Then
            Session.Remove("MST_SaleManGroup_PageInfo")
            Session.Remove("MST_SaleManGroup_Search")
            Session.Remove("MST_SaleManGroup_pageLength")
        End If
        If UrlComming <> "MST_Sales.aspx" Then
            Session.Remove("MST_Sales_PageInfo")
            Session.Remove("MST_Sales_Search")
            Session.Remove("MST_Sales_pageLength")
        End If
        If UrlComming <> "MST_WebResource.aspx" Then
            Session.Remove("MST_WebResource_PageInfo")
            Session.Remove("MST_WebResource_Search")
            Session.Remove("MST_WebResource_pageLength")
        End If
        If UrlComming <> "MST_Zone.aspx" Then
            Session.Remove("MST_Zone_PageInfo")
            Session.Remove("MST_Zone_Search")
            Session.Remove("MST_Zone_pageLength")
        End If
        If UrlComming <> "INT_Promotion.aspx" Then
            Session.Remove("INT_Promotion_PageInfo")
            Session.Remove("INT_Promotion_Search")
            Session.Remove("INT_Promotion_pageLength")
        End If
        If UrlComming <> "ORG_TypeHouse.aspx" Then
            Session.Remove("ORG_TypeHouse_PageInfo")
            Session.Remove("ORG_TypeHouse_Search")
            Session.Remove("ORG_TypeHouse_pageLength")
        End If
        If UrlComming <> "ORG_PhaseProject.aspx" Then
            Session.Remove("ORG_PhaseProject_PageInfo")
            Session.Remove("ORG_PhaseProject_Search")
            Session.Remove("ORG_PhaseProject_pageLength")
        End If
        If UrlComming <> "ORG_Block.aspx" Then
            Session.Remove("ORG_Block_PageInfo")
            Session.Remove("ORG_Block_Search")
            Session.Remove("ORG_Block_pageLength")
        End If
        If UrlComming <> "ORG_SetTypeHouse.aspx" Then
            Session.Remove("ORG_SetTypeHouse_PageInfo")
            Session.Remove("ORG_SetTypeHouse_Search")
            Session.Remove("ORG_SetTypeHouse_pageLength")
        End If
        If UrlComming <> "ORG_CreateType.aspx" Then
            Session.Remove("ORG_CreateType_PageInfo")
            Session.Remove("ORG_CreateType_Search")
            Session.Remove("ORG_CreateType_pageLength")
        End If
        If UrlComming <> "PRD_ProductStore.aspx" Then
            Session.Remove("PRD_ProductStore_PageInfo")
            Session.Remove("PRD_ProductStore_Search")
            Session.Remove("PRD_ProductStore_pageLength")
        End If
        If UrlComming <> "TRN_CustomerInformation.aspx" Then
            Session.Remove("TRN_CustomerInformation_PageInfo")
            Session.Remove("TRN_CustomerInformation_Search")
            Session.Remove("TRN_CustomerInformation_pageLength")
        End If
        If UrlComming <> "MST_UsersMenuAccess.aspx" Then
            Session.Remove("MST_UsersMenuAccess_PageInfo")
            Session.Remove("MST_UsersMenuAccess_Search")
            Session.Remove("MST_UsersMenuAccess_pageLength")
        End If
        If UrlComming <> "MST_UserInformation.aspx" Then
            Session.Remove("MST_UserInformation_PageInfo")
            Session.Remove("MST_UserInformation_Search")
            Session.Remove("MST_UserInformation_pageLength")
        End If
        If UrlComming <> "MST_UserInformation.aspx" Then
            Session.Remove("MST_UserInformation_PageInfo")
            Session.Remove("MST_UserInformation_Search")
            Session.Remove("MST_UserInformation_pageLength")
        End If
        If UrlComming <> "MST_Module.aspx" Then
            Session.Remove("MST_Module_PageInfo")
            Session.Remove("MST_Module_Search")
            Session.Remove("MST_Module_pageLength")
        End If
        If UrlComming <> "TRN_DataLandBank.aspx" Then
            Session.Remove("TRN_DataLandBank_PageInfo")
            Session.Remove("TRN_DataLandBank_Search")
            Session.Remove("TRN_DataLandBank_pageLength")
        End If
        If UrlComming <> "TRN_DataLandBank.aspx" Then
            Session.Remove("TRN_DataLandBank_PageInfo")
            Session.Remove("TRN_DataLandBank_Search")
            Session.Remove("TRN_DataLandBank_pageLength")
        End If
        If UrlComming <> "TRN_DeedSettingProject.aspx" Then
            Session.Remove("TRN_DeedSettingProject_PageInfo")
            Session.Remove("TRN_DeedSettingProject_Search")
            Session.Remove("TRN_DeedSettingProject_pageLength")
        End If
        If UrlComming <> "ADI_ImportPriceList.aspx" Then
            Session.Remove("ADI_ImportPriceList_PageInfo")
            Session.Remove("ADI_ImportPriceList_Search")
            Session.Remove("ADI_ImportPriceList_pageLength")
        End If
        If UrlComming <> "ADI_ImportStandardTargetPrice.aspx" Then
            Session.Remove("ADI_ImportStandardTargetPrice_PageInfo")
            Session.Remove("ADI_ImportStandardTargetPrice_Search")
            Session.Remove("ADI_ImportStandardTargetPrice_pageLength")
        End If
    End Sub
    Public Sub Redirect(ByVal url As String, Optional ByVal hasErrored As Boolean = False)

        If Not hasErrored Then
            HttpContext.Current.Server.ClearError()
            HttpContext.Current.Response.Redirect(url, False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        Else
            HttpContext.Current.Server.ClearError()
            HttpContext.Current.Response.Redirect(url, False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End If
    End Sub

    Public Function ToStringByCulture(ByVal pDate As DateTime, Optional ByVal pDateFormat As String = "dd/MM/yyyy") As String
        If pDate <> Nothing Then
            Return pDate.ToString(pDateFormat, New CultureInfo(CultureDate))
        End If
        Return String.Empty
    End Function

    Public Function ToSystemDate(ByVal pDate As String, Optional ByVal pDateFormat As String = "dd/MM/yyyy") As String
        If IsNothing(pDate) Or pDate = "" Then
            Return String.Empty
        End If
        Return DateTime.ParseExact(pDate, pDateFormat, New CultureInfo(CultureDate))
    End Function
    Public Function ToSystemDateString(ByVal pDate As String, Optional ByVal pDateFormat As String = "dd/MM/yyyy") As String
        If IsNothing(pDate) Or pDate = "" Then
            Return ""
        End If
        Dim a = DateTime.ParseExact(pDate, pDateFormat, New CultureInfo(CultureDate))
        Return ToSystemDateString(a)
        'Dim b = a.ToString(pDateFormat, New CultureInfo("en-US"))
        '' Return DateTime.ParseExact(pDate, pDateFormat, New CultureInfo(CultureDate)).ToString(pDate, New CultureInfo("en-US"))
        'Return b
    End Function
    Public Function ToSystemDateString(ByVal pDate As DateTime, Optional ByVal pDateFormat As String = "dd/MM/yyyy") As String
        If IsNothing(pDate) Then
            Return ""
        End If
        Dim a = pDate.ToString(pDateFormat, New CultureInfo("en-US"))
        ' Return DateTime.ParseExact(pDate, pDateFormat, New CultureInfo(CultureDate)).ToString(pDate, New CultureInfo("en-US"))
        Return a
    End Function

End Class

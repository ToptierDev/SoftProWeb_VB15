Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class _Default
    Inherits System.Web.UI.Page
    Dim oBasePage As New BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call LoadInit()
            Call LoadDropdownlist()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();", True)
        End If

    End Sub

#Region "Loaddata"
    Public Sub LoadInit()
        lblMassage1.Style.Add("display", "none")
        lblMassage2.Style.Add("display", "none")
        lblMassage3.Style.Add("display", "none")
        'lblMassage4.Style.Add("display", "none")
        lblMassage5.Style.Add("display", "none")
        lblMassage1.Text = GetResource("msg_required", "MSG")
        lblMassage2.Text = GetResource("msg_required", "MSG")
        lblMassage3.Text = GetResource("msg_required", "MSG")
        'lblMassage4.Text = GetResource("msg_required", "MSG")
        lblMassage5.Text = GetResource("msg_required_check_userid", "MSG")
        lblsUserName.Text = GetResource("lblsUserName", "Text", "1")
        lblsPassword.Text = GetResource("lblsPassword", "Text", "1")
        lblsCompany.Text = GetResource("lblsCompany", "Text", "1")
        'lblsModule.Text = GetResource("lblsModule", "Text", "1")
        lblsLanguage.Text = GetResource("lblsLanguage", "Text", "1")
        hplRegister.Attributes.Add("onclick", "showOverlay();")

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
#End Region

#Region "Dropdownlist"
    Public Sub LoadDropdownlist()
        Call LoadWebLang(ddlaWebLang)
        ddlsCompany.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
        'ddlsModule.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
    End Sub
    Public Sub LoadWebLang(ByVal ddl As DropDownList)
        Try
            ddl.Items.Clear()
            ddl.Items.Insert(0, New ListItem("ไทย", "TH"))
            ddl.Items.Insert(1, New ListItem("English", "EN"))
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LoadCompany(ByVal ddl As DropDownList)
        Try
            Dim bl As New cLogin
            Dim lc As List(Of Login_ViewModel) = bl.LoadCompany(txtaUserName.Text)
            ddl.Items.Clear()
            ddl.DataTextField = "Name"
            ddl.DataValueField = "Code"
            ddl.DataSource = lc
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            If lc IsNot Nothing Then
                If lc.Count = 1 Then
                    ddl.SelectedIndex = 1
                ElseIf lc.Count > 1 Then
                    ddl.SelectedIndex = 1
                End If
            Else
                ddl.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LoadModule(ByVal ddl As DropDownList)
        Try
            Dim bl As New cLogin
            Dim lc As List(Of Login_ViewModel) = bl.LoadModule(txtaUserName.Text)
            ddl.Items.Clear()
            ddl.DataTextField = "Name"
            ddl.DataValueField = "Code"
            ddl.DataSource = lc
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            If lc IsNot Nothing Then
                If lc.Count = 1 Then
                    ddl.SelectedIndex = 1
                ElseIf lc.Count > 1 Then
                    ddl.SelectedIndex = 1
                End If
            Else
                ddl.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim PasswordAdmin As String = ConfigurationManager.AppSettings("AdminPassword")
        If txtaUserName.Text <> String.Empty And
           txtaPassword.Text <> String.Empty Then
            Dim bl As New cLogin
            Dim blMenu As New cMenu
            Dim lc As New CoreUser
            If txtaPassword.Text.ToUpper = PasswordAdmin.ToUpper Then
                lc = bl.LoginUserByAdmin(txtaUserName.Text)
            Else
                lc = bl.LoginUser(txtaUserName.Text,
                                  txtaPassword.Text)
            End If
            If lc IsNot Nothing Then
                Call oBasePage.SetCurrentUser(lc)
            Else
                txtaUserName.Text = String.Empty
                txtaPassword.Text = String.Empty
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogText();", True)
                Exit Sub
            End If
            Dim lcs As BD10DIVI = bl.GetCompany(ddlsCompany.SelectedValue)
            If lcs IsNot Nothing Then
                Call oBasePage.SetCurrentCompany(lcs)
            End If
            Call oBasePage.WebCompany(ddlsCompany.SelectedValue.ToString())
            'Call oBasePage.WebModule(ddlsModule.SelectedValue.ToString())
            Call oBasePage.WebCulture(ddlaWebLang.SelectedValue.ToString())
            'Call oBasePage.MasterMenu(blMenu.GetMenu(txtaUserName.Text, ddlsModule.SelectedValue, ""))
            Call oBasePage.MasterMenu(blMenu.GetMenu(txtaUserName.Text, "", ""))

            Call Redirect("Main.aspx")
        Else
            txtaUserName.Text = String.Empty
            txtaPassword.Text = String.Empty
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogText();", True)
        End If
    End Sub

    Protected Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        If txtaUserName.Text <> String.Empty Then
            txtaPassword.Focus()
            Dim bl As New cLogin
            Dim lc As New CoreUser
            lc = bl.LoginUserByAdmin(txtaUserName.Text)
            If lc IsNot Nothing Then
                Call LoadCompany(ddlsCompany)
                'Call LoadModule(ddlsModule)
            End If
        Else
            ddlsCompany.Items.Clear()
            'ddlsModule.Items.Clear()
            ddlsCompany.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            'ddlsModule.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
        End If
    End Sub
#End Region

#Region "WebResource"
    Public Function GetResource(ByVal strResourceName As String,
                                ByVal strResourceType As String) As String
        Return GetResource(strResourceName, strResourceType, "1")
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
                If Not IsDBNull(resource.ResourceValueEN) Then
                    Return resource.ResourceValueEN
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
#End Region
End Class
﻿Imports SPW.DAL
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
        lblMassage1.Text = GetWebMessage("msg_required", "MSG", "1")
        lblMassage2.Text = GetWebMessage("msg_required", "MSG", "1")
        lblMassage3.Text = GetWebMessage("msg_required", "MSG", "1")
        'lblMassage4.Text = GetWebMessage("msg_required", "MSG", "1")
        lblMassage5.Text = GetWebMessage("msg_required_check_userid", "MSG", "1")
        lblsUserName.Text = GetWebMessage("lblsUserName", "Text", "1")
        lblsPassword.Text = GetWebMessage("lblsPassword", "Text", "1")
        lblsCompany.Text = GetWebMessage("lblsCompany", "Text", "1")
        'lblsModule.Text = GetWebMessage("lblsModule", "Text", "1")
        lblsLanguage.Text = GetWebMessage("lblsLanguage", "Text", "1")
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
        ddlsCompany.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
        'ddlsModule.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
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
            ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
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
            ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
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
            ddlsCompany.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            'ddlsModule.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
        End If
    End Sub
#End Region

#Region "WebResource"
    Public Function GetWebMessage(ByVal messageName As String,
                                  ByVal messageType As String,
                                  ByVal messageMenuID As String) As String
        Dim ctx As PNSDBWEBEntities = PNSDBWEBEntities.Context
        Dim resource As CoreWebResource = ctx.CoreWebResources.Where(Function(s) s.ResourceName.ToLower() = messageName.ToLower() And s.ResourceType.ToLower() = messageType.ToLower() And s.MenuID = messageMenuID).SingleOrDefault
        If resource IsNot Nothing Then
            If Not IsDBNull(resource.ResourceValueEN) Then
                Return resource.ResourceValueEN
            End If
        Else
            Throw New Exception(String.Format("No resource name [{0}]", messageName))
        End If
        Return String.Empty
    End Function
#End Region
End Class
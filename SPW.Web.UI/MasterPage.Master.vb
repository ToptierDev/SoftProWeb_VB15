Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Threading
Imports System.Globalization
Imports SPW.BLL
Imports SPW.DAL
Public Class MasterPage
    Inherits System.Web.UI.MasterPage
    Public assetversion As String = ConfigurationManager.AppSettings("assetversion")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Page.DataBind()
            If Session("PRR.application.CheckSlideBar") = "0" Then
                hddCheckSideBar.Value = "checkClose"
            End If
            'Dim bl As AccessLogsBL = New AccessLogsBL()
            'bl.AccessLogsAdd(Me.CurrentUser.UserID, 0, System.IO.Path.GetFileName(HttpContext.Current.Request.Url.ToString), Request.UserHostAddress())
            Call LoadInit()
        Else
            If hddCheckSideBar.Value = "checkClose" Then
                Session.Add("PRR.application.CheckSlideBar", "0")
            Else
                Session.Remove("PRR.application.CheckSlideBar")
            End If
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "callLoadHide();", True)
        End If
    End Sub
#Region "Loaddata"
    Public Sub LoadInit()
        Dim lang As String = String.Empty
        If Session("PRR.application.language").ToLower() = "th" Then
            lang = "th-TH"
        Else
            lang = "en-US"
        End If
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(lang)
        Thread.CurrentThread.CurrentCulture = New CultureInfo(lang)

        If Session("PRR.application.user_authen") IsNot Nothing Then
            Call LoadUser()
        End If

        If Session("PRR.application.company_authen") IsNot Nothing Then
            Call LoadCompany()
        End If

        Dim strLG As String = String.Empty
        If Session("PRR.application.language") IsNot Nothing Then
            strLG = Session("PRR.application.language")
        End If
        If strLG = "TH" Then
            btnChangePassword.Text = GetResource("btnChangePassword", "Text", "1")
            btnLogout.Text = GetResource("btnLogout", "Text", "1")
        Else
            btnChangePassword.Text = GetResource("btnChangePassword", "Text", "1")
            btnLogout.Text = GetResource("btnLogout", "Text", "1")
        End If

        btnChangePassword.Attributes.Add("onclick", "showOverlay();")
        btnLogout.Attributes.Add("onclick", "showOverlay();")

    End Sub
    Public Sub LoadUser()
        Dim lc As CoreUser = Session("PRR.application.user_authen")
        If lc IsNot Nothing Then
            Dim strLG As String = String.Empty
            If Session("PRR.application.language") IsNot Nothing Then
                strLG = Session("PRR.application.language")
            End If
            If strLG = "TH" Then
                lblaUserID.Text = lc.EmployeeNo & "/" & lc.TitleThai & " " & lc.NameThai & " " & lc.LastnameThai
                lblaUserID1.Text = lc.TitleThai & " " & lc.NameThai & " " & lc.LastnameThai
                lblaRoleID.Text = lc.Position
            Else
                lblaUserID.Text = lc.EmployeeNo & "/" & lc.TitleEng & " " & lc.NameEng & " " & lc.LastnameEng
                lblaUserID1.Text = lc.TitleEng & " " & lc.NameEng & " " & lc.LastnameEng
                lblaRoleID.Text = lc.Position
            End If
            If lc.ImgFile IsNot Nothing Then
                Dim base64StringImg As String = Convert.ToBase64String(lc.ImgFile, 0, lc.ImgFile.Length)
                If base64StringImg <> String.Empty Then
                    Try
                        img1.ImageUrl = Convert.ToString("data:image/png;base64,") & base64StringImg
                        img2.ImageUrl = Convert.ToString("data:image/png;base64,") & base64StringImg
                    Catch ex As Exception
                        img1.ImageUrl = "~/image/no_image.jpg"
                        img2.ImageUrl = "~/image/no_image.jpg"
                    End Try
                Else
                    img1.ImageUrl = "~/image/no_image.jpg"
                    img2.ImageUrl = "~/image/no_image.jpg"
                End If
            Else
                img1.ImageUrl = "~/image/no_image.jpg"
                img2.ImageUrl = "~/image/no_image.jpg"
            End If

        Else
            lblaUserID.Text = String.Empty
            lblaUserID1.Text = String.Empty
            lblaRoleID.Text = String.Empty
            img1.ImageUrl = String.Empty
            img2.ImageUrl = String.Empty
        End If
    End Sub
    Public Sub LoadCompany()
        Dim lc As BD10DIVI = Session("PRR.application.company_authen")
        If lc IsNot Nothing Then
            Dim strLG As String = String.Empty
            If Session("PRR.application.language") IsNot Nothing Then
                strLG = Session("PRR.application.language")
            End If
            If strLG = "TH" Then
                lblaCompany.Text = lc.FDIVCODE & " - " & lc.FDIVNAMET
            Else
                lblaCompany.Text = lc.FDIVCODE & " - " & lc.FDIVNAME
            End If
        Else
            lblaCompany.Text = String.Empty
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
#End Region
#Region "Event"
    Protected Sub btnClearSession_Click(sender As Object, e As EventArgs) Handles btnClearSession.Click
        Try
            If Session("PRR.application.CheckSlideBar") IsNot Nothing Then
                Session.Remove("PRR.application.CheckSlideBar")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        Call Redirect("ChangePassword.aspx")
    End Sub
    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Session.RemoveAll()
        Call Redirect("Default.aspx")
    End Sub

    Protected Sub imgLGTH_Click(sender As Object, e As ImageClickEventArgs) Handles imgLGTH.Click
        Session.Remove("PRR.application.language")
        Session.Add("PRR.application.language", "EN")
        Response.Redirect(Request.RawUrl)
    End Sub
    Protected Sub imgLGEN_Click(sender As Object, e As ImageClickEventArgs) Handles imgLGEN.Click
        Session.Remove("PRR.application.language")
        Session.Add("PRR.application.language", "TH")
        Response.Redirect(Request.RawUrl)
    End Sub
#End Region

#Region "Call WebResource"
    Public Function GetResource(ByVal strResourceName As String,
                                ByVal strResourceType As String,
                                ByVal strResourceMenuID As String) As String
        Dim strLG As String = String.Empty
        If Session("PRR.application.language") IsNot Nothing Then
            strLG = Session("PRR.application.language")
        End If
        Dim ctx As PNSDBWEBEntities = PNSDBWEBEntities.Context
        Dim resultsList As List(Of CoreWebResource) = ctx.CoreWebResources.ToList()
        Dim resource As CoreWebResource
        resource = resultsList.Where(Function(s) s.ResourceName.ToLower() = strResourceName.ToLower() And s.ResourceType.ToLower() = strResourceType.ToLower() And s.MenuID = strResourceMenuID).FirstOrDefault
        If resource IsNot Nothing Then
            If strLG IsNot Nothing Then
                If strLG.ToLower = "th" Then
                    If Not IsDBNull(resource.ResourceValueLC) Then
                        Return resource.ResourceValueLC
                    End If
                ElseIf strLG.ToLower = "en" Then
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
            Throw New Exception(String.Format("No resource name [{0}]", strResourceName))
        End If
        Return String.Empty
    End Function
#End Region

End Class
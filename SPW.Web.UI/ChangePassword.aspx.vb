Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Timers
Imports System.Threading.Tasks
Public Class ChangePassword
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Call LoadInit()
                hddParameterMenuID.Value = HelperLog.LoadMenuID(Request.RawUrl.ToString)
                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub

#Region "Loaddata"
    Public Sub LoadInit()
        lblMassage1.Style.Add("display", "none")
        lblMassage2.Style.Add("display", "none")
        lblMassage3.Style.Add("display", "none")
        lblPasswordNotPass.Style.Add("display", "none")
        lblConfirmNot.Style.Add("display", "none")

        lblMain1.Text = Me.GetResource("main_label", "Text", "1")
        lblMain2.Text = Me.GetResource("HeaderPageChangePassword", "Text", "1")
        lblMain3.Text = Me.GetResource("HeaderPageChangePassword", "Text", "1")

        lblMassage1.Text = Me.GetResource("msg_required", "MSG")
        lblMassage2.Text = Me.GetResource("msg_required", "MSG")
        lblMassage3.Text = Me.GetResource("msg_required", "MSG")
        lblPasswordNotPass.Text = Me.GetResource("msg_required_Password_notyet", "MSG")
        lblConfirmNot.Text = Me.GetResource("msg_required_Password_notequal", "MSG")
    End Sub
    Public Sub Redirects(ByVal url As String, Optional ByVal hasErrored As Boolean = False)

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
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim strUserid As String = Me.CurrentUser.UserID.ToString
            If strUserid <> String.Empty And
               txtaOldPassword.Text <> String.Empty Then
                Dim bl As New cLogin
                Dim lc As CoreUser = bl.LoginUser(strUserid,
                                                  txtaOldPassword.Text)
                If lc Is Nothing Then
                    txtaOldPassword.Text = String.Empty
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogText();", True)
                    Exit Sub
                End If

                If bl.UpdatePassword(strUserid,
                                     txtaNewPassword.Text) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & Me.GetResource("msg_change_password_success", "MSG") & "');", True)
                    Timer1.Enabled = True
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_change_password_not_success", "MSG") & "');", True)
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Call Redirect("Main.aspx")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub
#End Region

#Region "Timer"
    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Session.RemoveAll()
        Call Redirect("Default.aspx")
    End Sub
#End Region
End Class
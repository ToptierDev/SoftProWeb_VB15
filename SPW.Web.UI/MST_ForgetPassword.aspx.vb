Imports System.Net.Mail
Imports SPW.DAL
Imports SPW.Helper

Public Class MST_ForgetPassword
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DataBind()
    End Sub

    Protected Sub btnResetPassword_Click(sender As Object, e As EventArgs) Handles btnResetPassword.Click
        lbErrorMessage.Visible = False
        If txtEmail.Text = "" Or txtUserName.Text = "" Then
            lbErrorMessage.Text = grtt("resAllFieldAreRequire")
            lbErrorMessage.Visible = True
            'please fill input
        Else
            Dim db As New PNSDBWEBEntities
            Dim u = db.CoreUsers.Where(Function(x) x.Email = txtEmail.Text And x.UserID = txtUserName.Text).FirstOrDefault
            Dim isExist = Not IsNothing(u)
            If (isExist) Then
                sendEmail()
            Else
                lbErrorMessage.Text = grtt("resUsernameOrEmailDoesNotExist")
                lbErrorMessage.Visible = True
            End If

        End If



    End Sub

    Private Sub sendEmail()
        Try
            Dim email = txtEmail.Text
            Dim userid = txtUserName.Text

            Dim SmtpServer As New SmtpClient("smtp.gmail.com", 587)
            SmtpServer.EnableSsl = True
            SmtpServer.Credentials = New Net.NetworkCredential("chanawee_s@toptier.co.th", "mawmeaw30")
            Dim mail As New MailMessage("chanawee_s@toptier.co.th", "chanawee_s@toptier.co.th", "SPW:Reset password", userid + email + generateEmailContent())
            mail.IsBodyHtml = True
            mail.CC.Add("panuwat@toptier.co.th")
            mail.CC.Add("chanawee_s@toptier.co.th")
            ' SmtpServer.Send(mail)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function generateEmailContent() As String


        Return "<br><b>Reset password content</b> test html format <div style='color:red'>รอบสุดท้าย เดี๋ยวรอบห</div>น้าเทสแบบไม่ส่งเมล"
    End Function


End Class
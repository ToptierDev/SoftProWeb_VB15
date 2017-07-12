Imports System.Net.Mail
Imports SPW.DAL
Imports SPW.Helper

Public Class UTL_ForgotPassword
    Inherits BasePage

    Dim db As New PNSDBWEBEntities
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRequestForReset_Click(sender As Object, e As EventArgs) Handles btnRequestForReset.Click
        lblMassage5.Visible = False
        If txtEmail.Text = "" Or txtUserName.Text = "" Then
            lblMassage5.Text = grtt("resAllFieldAreRequire")
            lblMassage5.Visible = True
            'please fill input
        Else
            Dim u = db.CoreUsers.Where(Function(x) x.Email = txtEmail.Text And x.UserID = txtUserName.Text).FirstOrDefault
            Dim isExist = Not IsNothing(u)
            If (isExist) Then
                setForgotPassword(u)

            Else
                lblMassage5.Text = grtt("resUsernameOrEmailDoesNotExist")
                lblMassage5.Visible = True
            End If

        End If



    End Sub
    Private Sub setForgotPassword(ByVal coreUser As CoreUser)
        Try
            Dim newPassword = genRandomPassword()
        coreUser.ResetPassword = newPassword
            coreUser.isForgetPassword = 1
            coreUser.ForgetPasswordDate = DateTime.Now
        db.Entry(coreUser).State = Data.EntityState.Modified
        db.SaveChanges()
            sendEmail(coreUser)

        Catch ex As Exception
            lblMassage5.Text = ex.GetBaseException().Message
            lblMassage5.Visible = True
        End Try
    End Sub


    Private Function genRandomPassword()
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 8
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString
    End Function

    Private Sub sendEmail(ByVal coreUser As CoreUser)
        Try
            Dim email = txtEmail.Text
            Dim userid = txtUserName.Text

            Dim SmtpServer As New SmtpClient("smtp.gmail.com", 587)
            SmtpServer.EnableSsl = True
            SmtpServer.Credentials = New Net.NetworkCredential("chanawee_s@toptier.co.th", "mawmeaw30")
            Dim mail As New MailMessage("chanawee_s@toptier.co.th",
                                        coreUser.Email,
                                        "SPW:Reset password for " + coreUser.UserID _
                                        , generateEmailContent(coreUser))
            mail.IsBodyHtml = True
            mail.CC.Add("panuwat@toptier.co.th")
            mail.CC.Add("chanawee_s@toptier.co.th")
            SmtpServer.Send(mail)
            Response.Redirect("UTL_ResetPassword.aspx?pFlag=ResetPassword")
        Catch ex As Exception
            lblMassage5.Text = ex.GetBaseException().Message
            lblMassage5.Visible = True
        End Try
    End Sub
    Private Function generateEmailContent(ByVal coreUser As CoreUser) As String
        Dim resetUrl = Request.Url.Scheme & "://" & Request.Url.Authority + Request.ApplicationPath + "/UTL_ResetPassword.aspx?pFlag=ResetPassword"
        Dim sHtml = ""
        sHtml += "You request for reset password at:<b>" + coreUser.ForgetPasswordDate.Value.ToString("dd/MM/yyyy HH:mm:ss") + "</b>"
        sHtml += "<br/>User ID:<b>" + coreUser.UserID + "</b>"
        sHtml += "<br/>Reset Key:<b style='color:red;'>" + coreUser.ResetPassword + "</b>"
        sHtml += "<br/>Reset url:<b><a href='" + resetUrl + "'>" + resetUrl + "</a>" + "</b>"
        sHtml += "<br/>*Reset Key Expire:<b>" + coreUser.ForgetPasswordDate.Value.AddHours(1).ToString("dd/MM/yyyy HH:mm:ss") + "</b>"
        ' Response.Write(sHtml)
        Return sHtml
    End Function
End Class
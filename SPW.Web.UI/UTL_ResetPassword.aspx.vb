Imports System.Security.Cryptography
Imports SPW.DAL
Imports SPW.Helper

Public Class UTL_ResetPassword
    Inherits BasePage
    Dim db As New PNSDBWEBEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnCreateNewPassword_Click(sender As Object, e As EventArgs) Handles btnCreateNewPassword.Click
        lblMassage5.Visible = False
        lblMassage5.ForeColor = Drawing.Color.Red
        If txtEmail.Text = "" _
            Or txtUserName.Text = "" _
            Or txtResetKey.Text = "" _
            Or txtNewPassword.Text = "" _
            Or txtConfirmPassword.Text = "" _
            Then
            lblMassage5.Text = grtt("resAllFieldAreRequire")
            lblMassage5.Visible = True
            'please fill input
        Else
            Dim u = db.CoreUsers.Where(Function(x) x.Email = txtEmail.Text And x.UserID = txtUserName.Text).FirstOrDefault

            If IsNothing(u) Then
                lblMassage5.Text = grtt("resUsernameOrEmailDoesNotExist")
                lblMassage5.Visible = True
            ElseIf IsNothing(u.isForgetPassword) Or u.isForgetPassword <> 1 Then
                lblMassage5.Text = grtt("resNoResetRequestForThisUser")
                lblMassage5.Visible = True
            ElseIf u.ResetPassword <> txtResetKey.Text Then
                lblMassage5.Text = grtt("resInvalidResetKey")
                lblMassage5.Visible = True
            ElseIf (u.ForgetPasswordDate - DateTime.Now).Value.TotalHours > 1 Then
                lblMassage5.Text = grtt("resResetKeyExpired")
                lblMassage5.Visible = True
            ElseIf txtNewPassword.Text <> txtConfirmPassword.Text Then
                lblMassage5.Text = grtt("resConfirmPasswordNotMatch")
                lblMassage5.Visible = True
            Else
                createNewPassword(u)
            End If

        End If



    End Sub
    Private Sub createNewPassword(ByVal coreUser As CoreUser)
        Try
            coreUser.ResetPassword = Nothing
            coreUser.isForgetPassword = Nothing
            coreUser.Password = MD5Generator(txtNewPassword.Text)
            db.Entry(coreUser).State = Data.EntityState.Modified
            db.SaveChanges()
            lblMassage5.Text = grtt("resSaveSuccess")
            lblMassage5.ForeColor = Drawing.Color.Green
            lblMassage5.Visible = True
            btnLogin.Visible = True
            btnCreateNewPassword.Visible = False
            panReset.Visible = False
        Catch ex As Exception
            lblMassage5.Text = ex.GetBaseException().Message
            lblMassage5.Visible = True
        End Try
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Response.Redirect("Default.aspx")
    End Sub
    Dim passphrase As String = "password"
    Public Function MD5Generator(ByVal ptUserPassword$) As String

        'Dim x As New System.Security.Cryptography.MD5CryptoServiceProvider()
        'Dim bs As Byte() = System.Text.Encoding.UTF8.GetBytes(ptUserPassword)
        'bs = x.ComputeHash(bs)
        'Dim s As New System.Text.StringBuilder()
        'For Each b As Byte In bs
        '    s.Append(b.ToString("x2").ToLower())
        'Next
        'Return s.ToString()
        Dim Results As Byte()
        Dim UTF8 As New System.Text.UTF8Encoding()
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(passphrase))
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        TDESAlgorithm.Key = TDESKey
        TDESAlgorithm.Mode = CipherMode.ECB
        TDESAlgorithm.Padding = PaddingMode.PKCS7
        Dim DataToEncrypt As Byte() = UTF8.GetBytes(ptUserPassword)
        Try
            Dim Encryptor As ICryptoTransform = TDESAlgorithm.CreateEncryptor()
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length)
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return Convert.ToBase64String(Results)
    End Function
End Class
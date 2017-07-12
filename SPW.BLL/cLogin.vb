Imports SPW.BLL
Imports SPW.DAL
Imports System.Security.Cryptography
Public Class cLogin

#Region "Defualt.aspx, ChangePassword.aspx"
    Public Function LoginUser(ByVal pUserID As String,
                              ByVal pPassword As String) As CoreUser

        Using db As New PNSDBWEBEntities
            Dim tPassword = MD5Generator(pPassword)
            Dim query = db.CoreUsers.Where(Function(s) s.UserID.ToUpper = pUserID.ToUpper And
                                                       s.Password.ToUpper = tPassword.ToUpper)

            Return query.FirstOrDefault
        End Using

    End Function

    Public Function LoginUserByAdmin(ByVal pUserID As String) As CoreUser

        Using db As New PNSDBWEBEntities
            Dim query = db.CoreUsers.Where(Function(s) s.UserID.ToUpper = pUserID.ToUpper)

            Return query.FirstOrDefault
        End Using

    End Function

    Public Function GetCompany(ByVal pCompany As String) As BD10DIVI

        Using db As New PNSWEBEntities
            Dim query = db.BD10DIVI.Where(Function(s) s.FDIVCODE.ToUpper = pCompany.ToUpper)

            Return query.FirstOrDefault
        End Using

    End Function

    Public Function UpdatePassword(ByVal pUserID As String,
                                   ByVal pNewpassword As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim tPassword = MD5Generator(pNewpassword)
            Dim lc As CoreUser = db.CoreUsers.Where(Function(s) s.UserID = pUserID).SingleOrDefault
            If lc IsNot Nothing Then
                lc.Password = tPassword
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

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


#Region "Dropdownlist"
    Public Function LoadCompany(ByVal pUserID As String) As List(Of Login_ViewModel)
        Using db As New PNSWEB_SoftProEntities
            Dim qury = From l In db.vw_BD10DIVI_joinCoreCompany.Where(Function(s) s.UserID = pUserID)
                       Select New Login_ViewModel With {
                               .Code = l.FDIVCODE,
                               .Name = l.FDIVCODE & " - " & l.FDIVNAMET
                       }

            Dim lists = qury.ToList()
            Return lists
        End Using
    End Function
    Public Function LoadModule(ByVal pUserID As String) As List(Of Login_ViewModel)
        Using db As New PNSDBWEBEntities
            Dim qury = From l In db.CoreUsersMenus.Where(Function(s) s.UserID = pUserID)
                       Group Join mMenu In db.CoreMenus.Where(Function(s) s.ModuleID IsNot Nothing) On l.MenuID Equals mMenu.MenuID
                       Into mMen = Group From mMenu In mMen.DefaultIfEmpty()
                       Group Join mModule In db.CoreModules On mMenu.ModuleID Equals mModule.ModuleID
                       Into mModul = Group From mModule In mModul.DefaultIfEmpty()
                       Select New Login_ViewModel With {
                               .Code = mModule.ModuleID,
                               .Name = mModule.ModuleID & " - " & mModule.ModuleNameEN
                       }
            qury = qury.Where(Function(s) s.Code <> "")

            Dim lists = qury.Distinct().ToList()
            Return lists
        End Using
    End Function
#End Region

#End Region
End Class

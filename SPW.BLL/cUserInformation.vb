Imports SPW.BLL
Imports SPW.DAL
Imports System.Text
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Security.Cryptography
Imports System.Configuration


Public Class cUserInformation

#Region "MST_UserInformation.aspx"
    Public Function HashSHA1(ByVal value As String) As String
        Dim x As New System.Security.Cryptography.MD5CryptoServiceProvider()
        Dim bs As Byte() = System.Text.Encoding.UTF8.GetBytes(value)
        bs = x.ComputeHash(bs)
        Dim s As New System.Text.StringBuilder()
        For Each b As Byte In bs
            s.Append(b.ToString("x2").ToLower())
        Next
        Return s.ToString()
    End Function

    Dim passphrase As String = "password"
    Public Function EncryptData(ByVal Message As String) As String
        Dim Results As Byte()
        Dim UTF8 As New System.Text.UTF8Encoding()
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(passphrase))
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        TDESAlgorithm.Key = TDESKey
        TDESAlgorithm.Mode = CipherMode.ECB
        TDESAlgorithm.Padding = PaddingMode.PKCS7
        Dim DataToEncrypt As Byte() = UTF8.GetBytes(Message)
        Try
            Dim Encryptor As ICryptoTransform = TDESAlgorithm.CreateEncryptor()
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length)
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return Convert.ToBase64String(Results)
    End Function

    Public Function DecryptString(ByVal Message As String) As String
        Dim Results As Byte()
        Dim UTF8 As New System.Text.UTF8Encoding()
        Dim HashProvider As New MD5CryptoServiceProvider()
        Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes(passphrase))
        Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()
        TDESAlgorithm.Key = TDESKey
        TDESAlgorithm.Mode = CipherMode.ECB
        TDESAlgorithm.Padding = PaddingMode.PKCS7
        Dim DataToDecrypt As Byte() = Convert.FromBase64String(Message)
        Try
            Dim Decryptor As ICryptoTransform = TDESAlgorithm.CreateDecryptor()
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length)
        Finally
            TDESAlgorithm.Clear()
            HashProvider.Clear()
        End Try
        Return UTF8.GetString(Results)
    End Function

    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal strUserID As String,
                             ByVal strCulture As String) As List(Of CoreUser)

        Dim ctx As New PNSDBWEBEntities
        Dim qury = From lc In ctx.CoreUsers.Where(Function(s) 1 = 1)

        If Not String.IsNullOrEmpty(fillter.Keyword) Then
            qury = qury.Where(Function(s) s.UserID.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.EmployeeNo.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.TitleEng.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.TitleThai.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.NameEng.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.NameThai.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.LastnameEng.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.LastnameThai.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.Division.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.Department.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.Position.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")))

        End If


        Select Case fillter.SortType '0-None , 1 -Asc , 2 - Desc
            Case 1
                If fillter.SortBy.ToLower = "employeecode" Then
                    qury = qury.OrderBy(Function(s) s.EmployeeNo)
                ElseIf fillter.SortBy.ToLower = "fullname" Then
                    If strCulture.ToUpper = "TH" Then
                        qury = qury.OrderBy(Function(s) s.TitleThai).OrderBy(Function(s) s.NameThai).OrderBy(Function(s) s.LastnameThai)
                    Else
                        qury = qury.OrderBy(Function(s) s.TitleEng).OrderBy(Function(s) s.NameEng).OrderBy(Function(s) s.LastnameEng)
                    End If
                ElseIf fillter.SortBy.ToLower = "division" Then
                    qury = qury.OrderBy(Function(s) s.Division)
                ElseIf fillter.SortBy.ToLower = "department" Then
                    qury = qury.OrderBy(Function(s) s.Department)
                ElseIf fillter.SortBy.ToLower = "position" Then
                    qury = qury.OrderBy(Function(s) s.Position)
                End If
                Exit Select
            Case 2
                If fillter.SortBy.ToLower = "employeecode" Then
                    qury = qury.OrderByDescending(Function(s) s.EmployeeNo)
                ElseIf fillter.SortBy.ToLower = "fullname" Then
                    If strCulture.ToUpper = "TH" Then
                        qury = qury.OrderByDescending(Function(s) s.TitleThai).OrderBy(Function(s) s.NameThai).OrderBy(Function(s) s.LastnameThai)
                    Else
                        qury = qury.OrderByDescending(Function(s) s.TitleEng).OrderBy(Function(s) s.NameEng).OrderBy(Function(s) s.LastnameEng)
                    End If
                ElseIf fillter.SortBy.ToLower = "division" Then
                    qury = qury.OrderByDescending(Function(s) s.Division)
                ElseIf fillter.SortBy.ToLower = "department" Then
                    qury = qury.OrderByDescending(Function(s) s.Department)
                ElseIf fillter.SortBy.ToLower = "position" Then
                    qury = qury.OrderByDescending(Function(s) s.Position)
                End If
                Exit Select
            Case Else
                qury = qury.OrderBy(Function(s) s.EmployeeNo)
                Exit Select
        End Select

        If qury IsNot Nothing Then
            TotalRow = qury.Count
        End If

        If (fillter.PageSize > 0 And fillter.Page >= 0) Then
            qury = qury.Skip((fillter.Page - 1) * fillter.PageSize).Take(fillter.PageSize)
        End If

        Return qury.ToList

    End Function

    Public Function GetUserByID(ByVal pUserID As String,
                                ByVal strUserID As String) As CoreUser

        Using db As New PNSDBWEBEntities
            Dim query = db.CoreUsers.Where(Function(s) s.UserID = pUserID)
            Return query.FirstOrDefault
        End Using

    End Function

    Public Function LoadEditUserInfo(ByVal pUserID As String) As CoreUser

        Using db As New PNSDBWEBEntities
            Dim query = db.CoreUsers.Where(Function(s) s.UserID = pUserID)

            Return query.FirstOrDefault
        End Using
    End Function

    Public Function UserAdd(ByVal pUserId As String,
                            ByVal pEmployeeNo As String,
                            ByVal pIDCardNo As String,
                            ByVal pPassword As String,
                            ByVal pTitleEng As String,
                            ByVal pTitleThai As String,
                            ByVal pNameEng As String,
                            ByVal pNameThai As String,
                            ByVal pLastnameEng As String,
                            ByVal pLastnameThai As String,
                            ByVal bPicture As Boolean,
                            ByVal bSignature As Boolean,
                            ByVal pImgFile As Byte(),
                            ByVal pImgSignature As Byte(),
                            ByVal pDepartment As String,
                            ByVal pDivision As String,
                            ByVal pPosition As String,
                            ByVal pPhoneNo As String,
                            ByVal pExtension As String,
                            ByVal pEmail As String,
                            ByVal pProject As String,
                            ByVal pCompany As String,
                            ByVal pResignDate As Boolean,
                            ByVal pUserUpdate As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim NameTh As String = ""
            Dim NameEn As String = ""

            Dim lc As CoreUser = New CoreUser
            lc.UserID = pUserId
            lc.EmployeeNo = pEmployeeNo
            lc.IDCardNo = pIDCardNo
            If pPassword <> String.Empty Then lc.Password = pPassword
            If pTitleEng <> String.Empty Then lc.TitleEng = pTitleEng
            If pTitleThai <> String.Empty Then lc.TitleThai = pTitleThai

            lc.NameEng = pNameEng
            lc.NameThai = pNameThai
            lc.LastnameEng = pLastnameEng
            lc.LastnameThai = pLastnameThai

            NameEn = pTitleEng + pNameEng + " " + pLastnameEng
            NameTh = pTitleThai + pNameThai + " " + pLastnameThai

            If pImgFile IsNot Nothing Then
                lc.ImgFile = pImgFile
            Else
                If bPicture = False Then lc.ImgFile = Nothing
            End If
            If pImgSignature IsNot Nothing Then
                lc.ImgSignature = pImgSignature
            Else
                If bSignature = False Then lc.ImgSignature = Nothing
            End If

            If pDepartment <> String.Empty Then lc.Department = pDepartment
            If pDivision <> String.Empty Then lc.Division = pDivision
            If pPosition <> String.Empty Then lc.Position = pPosition
            If pPhoneNo <> String.Empty Then lc.PhoneNo = pPhoneNo
            If pExtension <> String.Empty Then lc.Extension = pExtension
            If pEmail <> String.Empty Then lc.Email = pEmail

            If pResignDate = True Then
                lc.ResignDate = Nothing
            Else
                lc.ResignDate = Date.Now
            End If

            lc.RegisterDate = Date.Now

            db.CoreUsers.Add(lc)

            Dim lcProjects As List(Of CoreUsersProject) = db.CoreUsersProjects.Where(Function(s) s.UserID = pUserId).ToList
            For Each m In lcProjects
                db.CoreUsersProjects.Remove(m)
            Next
            If pProject <> String.Empty Then

                Dim sArr() As String = pProject.Split(",")
                For i As Integer = 0 To sArr.Length - 1
                    Dim addProject As New CoreUsersProject
                    If sArr(i).ToString <> String.Empty Then
                        addProject.FREPRJNO = sArr(i).ToString
                    Else
                        addProject.FREPRJNO = String.Empty
                    End If
                    addProject.UserID = pUserId

                    db.CoreUsersProjects.Add(addProject)
                Next
            End If

            Dim lcCompany As List(Of CoreUsersCompany) = db.CoreUsersCompanies.Where(Function(s) s.UserID = pUserId).ToList
            For Each m In lcCompany
                db.CoreUsersCompanies.Remove(m)
            Next
            If pCompany <> String.Empty Then
                Dim sArr() As String = pCompany.Split(",")
                For i As Integer = 0 To sArr.Length - 1
                    Dim addProject As New CoreUsersCompany
                    If sArr(i).ToString <> String.Empty Then
                        addProject.COMID = sArr(i).ToString
                    Else
                        addProject.COMID = String.Empty
                    End If
                    addProject.UserID = pUserId

                    db.CoreUsersCompanies.Add(addProject)
                Next
            End If

            'Add CoreUsersMenu จาก CoreRoleMenu
            Dim lstUsersMenu As List(Of CoreUsersMenu) = db.CoreUsersMenus.Where(Function(s) s.UserID = pUserId).ToList
            For Each u As CoreUsersMenu In lstUsersMenu
                db.CoreUsersMenus.Remove(u)
            Next

            Dim subqueryRoleMenu As List(Of CoreRoleMenu) = db.CoreRoleMenus.Where(Function(s) s.DivisionCode = pDivision And s.PositionCode = pPosition).ToList
            For Each m As CoreRoleMenu In subqueryRoleMenu
                Dim lsct As CoreUsersMenu = New CoreUsersMenu
                lsct.MenuID = m.MenuID
                lsct.UserID = pUserId
                lsct.isAdd = m.isAdd
                lsct.isApprove = m.isApprove
                lsct.isDelete = m.isDelete
                lsct.isEdit = m.isEdit
                lsct.isPrint = m.isPrint
                db.CoreUsersMenus.Add(lsct)
            Next

            db.SaveChanges()

            If pDivision.ToUpper = "SAL" Then
                Dim db2 As New PNSWEBEntities
                'Dim sMaxCode As LD01SMAN = From p In db2.LD01SMAN Select (Max(p.FSMCODE))
                Dim query = From p In db2.LD01SMAN
                            Select p.FSMCODE
                Dim maxID = query.Max()

                Dim lsSale As LD01SMAN = New LD01SMAN
                lsSale.FSMCODE = CInt(maxID + 1).ToString
                lsSale.FSMNAME = NameEn
                lsSale.FSMNAMET = NameTh
                lsSale.FSLROUTE = "Z001"
                lsSale.FDIVCODE = "01"
                If pDepartment <> String.Empty Then lsSale.FDPCODE = pDepartment
                lsSale.FSMTYPE = Nothing
                lsSale.FSECCODE = Nothing
                lsSale.FBASICCOM = 0
                lsSale.FCURMARG = 0
                lsSale.FSTDMARG = 0
                If pProject <> String.Empty Then lsSale.FREPRJNO = pProject
                lsSale.FUSERID = pUserId
                If pResignDate = True Then
                    lsSale.FSMSTATUS = 1
                Else
                    lsSale.FSMSTATUS = 0
                End If

                db2.LD01SMAN.Add(lsSale)
                db2.SaveChanges()
            End If

            Return True
        End Using

    End Function

    Public Function UserEdit(ByVal pUserId As String,
                            ByVal pEmployeeNo As String,
                            ByVal pIDCardNo As String,
                            ByVal pPassword As String,
                            ByVal pTitleEng As String,
                            ByVal pTitleThai As String,
                            ByVal pNameEng As String,
                            ByVal pNameThai As String,
                            ByVal pLastnameEng As String,
                            ByVal pLastnameThai As String,
                            ByVal bPicture As Boolean,
                            ByVal bSignature As Boolean,
                            ByVal pImgFile As Byte(),
                            ByVal pImgSignature As Byte(),
                            ByVal pDepartment As String,
                            ByVal pDivision As String,
                            ByVal pPosition As String,
                            ByVal pPhoneNo As String,
                            ByVal pExtension As String,
                            ByVal pEmail As String,
                            ByVal pProject As String,
                            ByVal pCompany As String,
                            ByVal pResignDate As Boolean,
                            ByVal pUserUpdate As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim NameTh As String = ""
            Dim NameEn As String = ""

            Dim lc As CoreUser = db.CoreUsers.Where(Function(s) s.UserID = pUserId).SingleOrDefault
            If lc IsNot Nothing Then
                lc.UserID = pUserId
                lc.EmployeeNo = pEmployeeNo
                lc.IDCardNo = pIDCardNo
                If pPassword <> String.Empty Then lc.Password = pPassword
                If pTitleEng <> String.Empty Then lc.TitleEng = pTitleEng
                If pTitleThai <> String.Empty Then lc.TitleThai = pTitleThai

                lc.NameEng = pNameEng
                lc.NameThai = pNameThai
                lc.LastnameEng = pLastnameEng
                lc.LastnameThai = pLastnameThai

                NameEn = pTitleEng + pNameEng + " " + pLastnameEng
                NameTh = pTitleThai + pNameThai + " " + pLastnameThai

                If pImgFile IsNot Nothing Then
                    lc.ImgFile = pImgFile
                Else
                    If bPicture = False Then lc.ImgFile = Nothing
                End If
                If pImgSignature IsNot Nothing Then
                    lc.ImgSignature = pImgSignature
                Else
                    If bSignature = False Then lc.ImgSignature = Nothing
                End If

                If pDepartment <> String.Empty Then lc.Department = pDepartment
                If pDivision <> String.Empty Then lc.Division = pDivision
                If pPosition <> String.Empty Then lc.Position = pPosition
                If pPhoneNo <> String.Empty Then lc.PhoneNo = pPhoneNo
                If pExtension <> String.Empty Then lc.Extension = pExtension
                If pEmail <> String.Empty Then lc.Email = pEmail

                If pResignDate = True Then
                    lc.ResignDate = Nothing
                Else
                    lc.ResignDate = Date.Now
                End If

                Dim lcProjects As List(Of CoreUsersProject) = db.CoreUsersProjects.Where(Function(s) s.UserID = pUserId).ToList
                For Each m In lcProjects
                    db.CoreUsersProjects.Remove(m)
                Next
                If pProject <> String.Empty Then

                    Dim sArr() As String = pProject.Split(",")
                    For i As Integer = 0 To sArr.Length - 1
                        Dim addProject As New CoreUsersProject
                        If sArr(i).ToString <> String.Empty Then
                            addProject.FREPRJNO = sArr(i).ToString
                        Else
                            addProject.FREPRJNO = String.Empty
                        End If
                        addProject.UserID = pUserId

                        db.CoreUsersProjects.Add(addProject)
                    Next
                End If

                Dim lcCompany As List(Of CoreUsersCompany) = db.CoreUsersCompanies.Where(Function(s) s.UserID = pUserId).ToList
                For Each m In lcCompany
                    db.CoreUsersCompanies.Remove(m)
                Next
                If pCompany <> String.Empty Then
                    Dim sArr() As String = pCompany.Split(",")
                    For i As Integer = 0 To sArr.Length - 1
                        Dim addProject As New CoreUsersCompany
                        If sArr(i).ToString <> String.Empty Then
                            addProject.COMID = sArr(i).ToString
                        Else
                            addProject.COMID = String.Empty
                        End If
                        addProject.UserID = pUserId

                        db.CoreUsersCompanies.Add(addProject)
                    Next
                End If

                'Add CoreUsersMenu จาก CoreRoleMenu
                Dim lstUsersMenu As List(Of CoreUsersMenu) = db.CoreUsersMenus.Where(Function(s) s.UserID = pUserId).ToList
                For Each u As CoreUsersMenu In lstUsersMenu
                    db.CoreUsersMenus.Remove(u)
                Next

                Dim subqueryRoleMenu As List(Of CoreRoleMenu) = db.CoreRoleMenus.Where(Function(s) s.DivisionCode = pDivision And s.PositionCode = pPosition).ToList
                For Each m As CoreRoleMenu In subqueryRoleMenu
                    Dim lsct As CoreUsersMenu = New CoreUsersMenu
                    lsct.MenuID = m.MenuID
                    lsct.UserID = pUserId
                    lsct.isAdd = m.isAdd
                    lsct.isApprove = m.isApprove
                    lsct.isDelete = m.isDelete
                    lsct.isEdit = m.isEdit
                    lsct.isPrint = m.isPrint
                    db.CoreUsersMenus.Add(lsct)
                Next

            End If

            db.SaveChanges()

            If pDivision.ToUpper = "SAL" Then
                Dim db2 As New PNSWEBEntities
                Dim lcSale As LD01SMAN = db2.LD01SMAN.Where(Function(s) s.FUSERID = pUserId).SingleOrDefault

                If lcSale IsNot Nothing Then
                    'lcSale.FSMCODE = CInt(maxID + 1).ToString
                    lcSale.FSMNAME = NameEn
                    lcSale.FSMNAMET = NameTh
                    'lcSale.FSLROUTE = "Z001"
                    'lcSale.FDIVCODE = "01"
                    If pDepartment <> String.Empty Then lcSale.FDPCODE = pDepartment
                    'lcSale.FSMTYPE = Nothing
                    'lcSale.FSECCODE = Nothing
                    'lcSale.FBASICCOM = 0
                    'lcSale.FCURMARG = 0
                    'lcSale.FSTDMARG = 0
                    If pProject <> String.Empty Then lcSale.FREPRJNO = pProject
                    lcSale.FUSERID = pUserId
                    If pResignDate = True Then
                        lcSale.FSMSTATUS = 1
                    Else
                        lcSale.FSMSTATUS = 0
                    End If
                Else
                    'Dim sMaxCode As LD01SMAN = From p In db2.LD01SMAN Select (Max(p.FSMCODE))
                    Dim query = From p In db2.LD01SMAN
                                Select p.FSMCODE
                    Dim maxID = query.Max()

                    Dim lsSale As LD01SMAN = New LD01SMAN
                    lsSale.FSMCODE = CInt(maxID + 1).ToString
                    lsSale.FSMNAME = NameEn
                    lsSale.FSMNAMET = NameTh
                    lsSale.FSLROUTE = "Z001"
                    lsSale.FDIVCODE = "01"
                    If pDepartment <> String.Empty Then lsSale.FDPCODE = pDepartment
                    lsSale.FSMTYPE = Nothing
                    lsSale.FSECCODE = Nothing
                    lsSale.FBASICCOM = 0
                    lsSale.FCURMARG = 0
                    lsSale.FSTDMARG = 0
                    If pProject <> String.Empty Then lsSale.FREPRJNO = pProject
                    lsSale.FUSERID = pUserId
                    If pResignDate = True Then
                        lsSale.FSMSTATUS = 1
                    Else
                        lsSale.FSMSTATUS = 0
                    End If

                    db2.LD01SMAN.Add(lsSale)
                End If

                db2.SaveChanges()
            End If

            Return True
        End Using

    End Function

    Public Function UserDelete(ByVal pUserID As String,
                           ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSDBWEBEntities

                Dim lst As List(Of CoreUsersMenu) = db.CoreUsersMenus.Where(Function(s) s.UserID = pUserID).ToList
                For Each u As CoreUsersMenu In lst
                    db.CoreUsersMenus.Remove(u)
                Next

                Dim lstPro As List(Of CoreUsersProject) = db.CoreUsersProjects.Where(Function(s) s.UserID = pUserID).ToList
                For Each u As CoreUsersProject In lstPro
                    db.CoreUsersProjects.Remove(u)
                Next

                Dim lstCompany As List(Of CoreUsersCompany) = db.CoreUsersCompanies.Where(Function(s) s.UserID = pUserID).ToList
                For Each u As CoreUsersCompany In lstCompany
                    db.CoreUsersCompanies.Remove(u)
                Next

                Dim lc As CoreUser = db.CoreUsers.Where(Function(s) s.UserID = pUserID).SingleOrDefault
                db.CoreUsers.Remove(lc)


                Dim db2 As New PNSWEBEntities
                Dim lstSale As List(Of LD01SMAN) = db2.LD01SMAN.Where(Function(s) s.FUSERID = pUserID).ToList
                For Each u As LD01SMAN In lstSale
                    db2.LD01SMAN.Remove(u)
                Next

                db.SaveChanges()
                db2.SaveChanges()

                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetDataUser(ByVal pCode As String, ByVal pIDCardNo As String) As List(Of CoreUser)

        Dim ctx As New PNSDBWEBEntities

        Dim qury = From lc In ctx.CoreUsers.Where(Function(s) 1 = 1)
        If pCode <> String.Empty AndAlso pIDCardNo <> String.Empty Then
            qury = qury.Where(Function(s) s.EmployeeNo = pCode AndAlso s.IDCardNo = pIDCardNo)
        ElseIf pCode <> String.Empty AndAlso pIDCardNo = String.Empty Then
            qury = qury.Where(Function(s) s.EmployeeNo = pCode)
        ElseIf pCode = String.Empty AndAlso pIDCardNo <> String.Empty Then
            qury = qury.Where(Function(s) s.IDCardNo = pIDCardNo)
        End If

        Return qury.ToList
    End Function

    Public Function GetDataEmployee(ByVal pCode As String, ByVal pIDCardNo As String) As List(Of EMPLOYEE_INFO)

        Dim ctx As New PNSDBWEBEntities

        Dim qury = From lc In ctx.EMPLOYEE_INFO.Where(Function(s) 1 = 1)
        If pCode <> String.Empty AndAlso pIDCardNo <> String.Empty Then
            qury = qury.Where(Function(s) s.Code = pCode AndAlso s.IDCardNo = pIDCardNo)
        ElseIf pCode <> String.Empty AndAlso pIDCardNo = String.Empty Then
            qury = qury.Where(Function(s) s.Code = pCode)
        ElseIf pCode = String.Empty AndAlso pIDCardNo <> String.Empty Then
            qury = qury.Where(Function(s) s.IDCardNo = pIDCardNo)
        End If

        Return qury.ToList
    End Function

    Public Function GetEmployee(ByVal pKey As String) As List(Of String)

        Using db As New PNSDBWEBEntities

            If pKey = String.Empty Then
                Dim qury = (From m In db.EMPLOYEE_INFO.OrderBy(Function(s) s.IDCardNo)
                            Select m.IDCardNo & "-" & m.Code).ToList()

                Dim lists = qury.ToList()
                Return lists
            ElseIf pKey <> String.Empty Then
                Dim qury = (From m In db.EMPLOYEE_INFO.OrderBy(Function(s) s.IDCardNo)
                            Select m.IDCardNo & "-" & m.Code) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKey.ToUpper.Replace(" ", ""))).ToList()

                Dim lists = qury.ToList()
                Return lists
            End If

            Return Nothing
        End Using

    End Function

    Public Function GetEmployeeNo(ByVal pKey As String) As List(Of String)

        Using db As New PNSDBWEBEntities

            If pKey = String.Empty Then
                Dim qury = (From m In db.EMPLOYEE_INFO.OrderBy(Function(s) s.Code)
                            Select m.Code & "-" & m.IDCardNo).ToList()

                Dim lists = qury.ToList()
                Return lists
            ElseIf pKey <> String.Empty Then
                Dim qury = (From m In db.EMPLOYEE_INFO.OrderBy(Function(s) s.Code)
                            Select m.Code & "-" & m.IDCardNo) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKey.ToUpper.Replace(" ", ""))).ToList()

                Dim lists = qury.ToList()
                Return lists
            End If

            Return Nothing
        End Using

    End Function


    ''Get Project All
    Public Function GetProjectAll() As List(Of ED01PROJ)

        Using db As New PNSWEBEntities
            Dim lc As List(Of ED01PROJ) = db.ED01PROJ.ToList
            Return lc
        End Using

    End Function


    ''Get Company All
    Public Function GetCompanyAll() As List(Of BD10DIVI)

        Using db As New PNSWEBEntities
            Dim lc As List(Of BD10DIVI) = db.BD10DIVI.ToList
            Return lc
        End Using

    End Function

    Public Function GetCompanyAllBD10DIVI() As List(Of BD10DIVI)

        Using db As New PNSWEBEntities
            Dim lc As List(Of BD10DIVI) = db.BD10DIVI.ToList
            Return lc
        End Using

    End Function


#Region "Dropdownlist"
    Public Function LoadDepartment() As List(Of BD11DEPT)
        Using db As New PNSWEBEntities
            Dim lc As List(Of BD11DEPT) = db.BD11DEPT.OrderBy(Function(s) s.FDPNAME).ToList
            Return lc
        End Using
    End Function

    Public Function LoadDivision() As List(Of CoreDivision)
        Using db As New PNSDBWEBEntities
            Dim lc As List(Of CoreDivision) = db.CoreDivisions.OrderBy(Function(s) s.DivisionName).ToList
            Return lc
        End Using
    End Function

    Public Function LoadPosition() As List(Of CorePosition)
        Using db As New PNSDBWEBEntities
            Dim lc As List(Of CorePosition) = db.CorePositions.OrderBy(Function(s) s.PositionName).ToList
            Return lc
        End Using
    End Function

#End Region

    Public Function GetDepartment(ByVal pFDPCODE As String) As BD11DEPT
        Using db As New PNSWEBEntities
            Dim lc As BD11DEPT = db.BD11DEPT.Where(Function(s) s.FDPCODE = pFDPCODE).SingleOrDefault
            Return lc
        End Using
    End Function

    Public Function GetDivision(ByVal pDivisionCode As String) As CoreDivision
        Using db As New PNSDBWEBEntities
            Dim lc As CoreDivision = db.CoreDivisions.Where(Function(s) s.DivisionCode = pDivisionCode).SingleOrDefault
            Return lc
        End Using
    End Function

    Public Function GetPosition(ByVal pPositionCode As String) As CorePosition
        Using db As New PNSDBWEBEntities
            Dim lc As CorePosition = db.CorePositions.Where(Function(s) s.PositionCode = pPositionCode).SingleOrDefault
            Return lc
        End Using
    End Function

#End Region


End Class

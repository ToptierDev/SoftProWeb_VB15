Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL
Public Class cUserMenuAccess

#Region "MST_UsersMenuAccess.aspx"


    Public Function GetUserMenuAccess(ByVal pUserID As String,
                                      ByVal pModule As String,
                                      ByVal pType As String,
                                      ByVal strUserID As String) As List(Of MenuAccess_ViewModel)

        Using db As New PNSDBWEBEntities
            Dim subquery = From lc In db.CoreMenus.Where(Function(s) s.EnableFlag = 1 And s.ParentID IsNot Nothing And s.MenuType = pType).Select(Function(w) w.ParentID)
            Dim qury = From mMenu In db.CoreMenus.Where(Function(s) s.EnableFlag = 1 And Not subquery.Contains(s.MenuID))
                       Group Join mUserMenu In db.CoreUsersMenus On mUserMenu.MenuID Equals mMenu.MenuID
                       Into mUserMen = Group From mUserMenu In mUserMen.DefaultIfEmpty()
                       Group Join mModule In db.CoreModules On mModule.ModuleID Equals mMenu.ModuleID
                       Into mMod = Group From mModule In mMod.DefaultIfEmpty()
                       Select New MenuAccess_ViewModel With {
                           .MenuID = mMenu.MenuID,
                           .ModuleID = mMenu.ModuleID,
                           .ModuleNameTH = mModule.ModuleNameTH,
                           .ModuleNameEN = mModule.ModuleNameEN,
                           .MenuNameLC = mMenu.MenuNameLC,
                           .MenuNameEN = mMenu.MenuNameEN,
                           .MenuLocation = mMenu.MenuLocation,
                           .MenuType = mMenu.MenuType,
                           .ParentID = mMenu.ParentID,
                           .Sequence = mMenu.Sequence,
                           .EnableFlag = mMenu.EnableFlag,
                           .EmployeeNo = mUserMenu.UserID
                       }
            If Not String.IsNullOrEmpty(pUserID) Then
                qury = qury.Where(Function(s) s.EmployeeNo = pUserID)
            End If
            If Not String.IsNullOrEmpty(pModule) Then
                qury = qury.Where(Function(s) s.ModuleID = pModule)
            End If
            If Not String.IsNullOrEmpty(pType) Then
                qury = qury.Where(Function(s) s.MenuType = pType)
            End If

            qury = qury.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)

            Dim lists = qury.ToList()
            Return lists
            'If pType = "FRM" Then
            '    Dim qury = From mMenu In db.VW_MenuFRM.Where(Function(s) s.ModuleID = pModule)
            '               Select New MenuAccess_ViewModel With {
            '                   .MenuID = mMenu.MenuID,
            '                   .ModuleID = mMenu.ModuleID,
            '                   .ModuleNameTH = mMenu.ModuleNameTH,
            '                   .ModuleNameEN = mMenu.ModuleNameEN,
            '                   .MenuNameLC = mMenu.MenuNameLC,
            '                   .MenuNameEN = mMenu.MenuNameEN,
            '                   .ParentID = mMenu.ParentMenuID,
            '                   .Sequence = mMenu.Sequence
            '               }
            '    Dim lists = qury.ToList()
            '    Return lists
            'Else
            '    Dim qury = From mMenu In db.VW_MenuRPT.Where(Function(s) s.ModuleID = pModule)
            '               Select New MenuAccess_ViewModel With {
            '                   .MenuID = mMenu.MenuID,
            '                   .ModuleID = mMenu.ModuleID,
            '                   .ModuleNameTH = mMenu.ModuleNameTH,
            '                   .ModuleNameEN = mMenu.ModuleNameEN,
            '                   .MenuNameLC = mMenu.MenuNameLC,
            '                   .MenuNameEN = mMenu.MenuNameEN,
            '                   .ParentID = mMenu.ParentMenuID,
            '                   .Sequence = mMenu.Sequence
            '               }
            '    Dim lists = qury.ToList()
            '    Return lists
            'End If
        End Using

    End Function

    Public Function GetUserMenuAccess(ByVal pModule As String,
                                      ByVal pType As String,
                                      ByVal strUserID As String) As List(Of MenuAccess_ViewModel)

        Using db As New PNSDBWEBEntities
            'Dim subquery = From lc In db.CoreMenus.Where(Function(s) s.EnableFlag = 1 And s.ParentID IsNot Nothing And s.MenuType = pType).Select(Function(w) w.ParentID)
            'Dim qury = From mMenu In db.CoreMenus.Where(Function(s) 1 = 1 And s.EnableFlag = 1 And Not subquery.Contains(s.MenuID))
            '           Group Join mModule In db.CoreModules On mModule.ModuleID Equals mMenu.ModuleID
            '           Into mMod = Group From mModule In mMod.DefaultIfEmpty()
            '           Select New MenuAccess_ViewModel With {
            '               .MenuID = mMenu.MenuID,
            '               .ModuleID = mMenu.ModuleID,
            '               .ModuleNameTH = mModule.ModuleNameTH,
            '               .ModuleNameEN = mModule.ModuleNameEN,
            '               .MenuNameLC = mMenu.MenuNameLC,
            '               .MenuNameEN = mMenu.MenuNameEN,
            '               .MenuLocation = mMenu.MenuLocation,
            '               .MenuType = mMenu.MenuType,
            '               .ParentID = mMenu.ParentID,
            '               .Sequence = mMenu.Sequence,
            '               .EnableFlag = mMenu.EnableFlag
            '           }
            If pType = "FRM" Then
                Dim qury = From mMenu In db.VW_MenuFRM.Where(Function(s) s.ModuleID = pModule And s.MenuID <> "99999")
                           Select New MenuAccess_ViewModel With {
                               .MenuID = mMenu.MenuID,
                               .ModuleID = mMenu.ModuleID,
                               .ModuleNameTH = mMenu.ModuleNameTH,
                               .ModuleNameEN = mMenu.ModuleNameEN,
                               .MenuNameLC = mMenu.MenuNameLC,
                               .MenuNameEN = mMenu.MenuNameEN,
                               .ParentID = mMenu.ParentMenuID,
                               .Sequence = mMenu.Sequence
                           }
                Dim lists = qury.ToList()
                Return lists
            Else
                Dim qury = From mMenu In db.VW_MenuRPT.Where(Function(s) s.ModuleID = pModule And s.MenuID <> "99999")
                           Select New MenuAccess_ViewModel With {
                               .MenuID = mMenu.MenuID,
                               .ModuleID = mMenu.ModuleID,
                               .ModuleNameTH = mMenu.ModuleNameTH,
                               .ModuleNameEN = mMenu.ModuleNameEN,
                               .MenuNameLC = mMenu.MenuNameLC,
                               .MenuNameEN = mMenu.MenuNameEN,
                               .ParentID = mMenu.ParentMenuID,
                               .Sequence = mMenu.Sequence
                           }
                Dim lists = qury.ToList()
                Return lists
            End If

            'If Not String.IsNullOrEmpty(pModule) Then
            '    qury = qury.Where(Function(s) s.ModuleID = pModule)
            'End If
            'If Not String.IsNullOrEmpty(pType) Then
            '    qury = qury.Where(Function(s) s.MenuType = pType)
            'End If

            'qury = qury.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)

            'Dim lists = qury.ToList()
            'Return lists
        End Using

    End Function

    Public Function GetWebMenuByID(ByVal id As String,
                                   ByVal strUserID As String) As CoreMenu
        Dim lc As CoreMenu = Nothing

        Using db As New PNSDBWEBEntities
            lc = db.CoreMenus.Where(Function(w) w.MenuID = id).FirstOrDefault
        End Using
        Return lc

    End Function
    Public Function Save(ByVal pUserKeyID As String,
                         ByVal lstCoreUserMenu As List(Of CoreUsersMenu),
                         ByVal pModule As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim allMenuInModule As List(Of Integer) = db.CoreMenus.Where(Function(s) s.ModuleID = pModule
                ).Select(Function(s) s.MenuID).ToList()

            Dim deleteUserMenu As List(Of CoreUsersMenu) =
                      db.CoreUsersMenus.Where(Function(r) _
                             allMenuInModule.Contains(r.MenuID) _
                            And r.UserID = pUserKeyID
                            ).ToList()
            db.CoreUsersMenus.RemoveRange(deleteUserMenu)


            db.CoreUsersMenus.AddRange(lstCoreUserMenu)
            db.SaveChanges()
            Return True
        End Using

    End Function
    Public Function Save_backup(ByVal pUserKeyID As String,
                         ByVal lc As List(Of MenuAccess_ViewModel),
                         ByVal lstCoreUserMenu As List(Of CoreUsersMenu),
                         ByVal pModule As String,
                         ByVal pUserId As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim subquery As List(Of CoreMenu) = db.CoreMenus.Where(Function(s) s.ModuleID = pModule).ToList
            For Each m As CoreMenu In subquery
                Dim lst As List(Of CoreUsersMenu) = db.CoreUsersMenus.Where(Function(s) s.UserID = pUserKeyID And s.MenuID = m.MenuID).ToList
                For Each u As CoreUsersMenu In lst
                    db.CoreUsersMenus.Remove(u)
                Next
            Next
            For Each u As MenuAccess_ViewModel In lc
                Dim ListUserCoreMenu As List(Of CoreMenu) = db.CoreMenus.Where(Function(s) s.MenuID = u.MenuID).ToList
                For Each us As CoreMenu In ListUserCoreMenu
                    Dim ListUserCoreMenus As List(Of CoreUsersMenu) = db.CoreUsersMenus.Where(Function(s) s.MenuID = us.ParentID And s.UserID = pUserKeyID).ToList
                    For Each uss As CoreUsersMenu In ListUserCoreMenus
                        db.CoreUsersMenus.Remove(uss)
                        db.SaveChanges()
                    Next
                    Dim ListUserCoreMenusCheck As List(Of CoreUsersMenu) = db.CoreUsersMenus.Where(Function(s) s.MenuID = us.ParentID And s.UserID = pUserKeyID).ToList
                    If ListUserCoreMenusCheck.Count = 0 Then
                        If us.ParentID IsNot Nothing Then
                            Dim lssct As CoreUsersMenu = New CoreUsersMenu
                            lssct.MenuID = us.ParentID
                            lssct.UserID = pUserKeyID
                            db.CoreUsersMenus.Add(lssct)
                            db.SaveChanges()
                        End If
                    End If
                Next
            Next
            If lc IsNot Nothing Then
                If lc.Count > 0 Then
                    For Each m As MenuAccess_ViewModel In lc
                        Dim lsct As CoreUsersMenu = New CoreUsersMenu
                        Dim ListUserCoreMenusCheck As List(Of CoreUsersMenu) = db.CoreUsersMenus.Where(Function(s) s.MenuID = m.MenuID And s.UserID = pUserKeyID).ToList
                        If ListUserCoreMenusCheck.Count = 0 Then
                            lsct.MenuID = m.MenuID
                            lsct.UserID = pUserKeyID
                            db.CoreUsersMenus.Add(lsct)
                        End If
                    Next
                End If
            End If

            db.SaveChanges()
            Return True
        End Using

    End Function
    Public Function getAllParentMenu(ByVal listMenuId As List(Of Integer))
        Dim listCoreMenu As List(Of CoreMenu)
        Using db As New PNSDBWEBEntities
            listCoreMenu = db.CoreMenus.Where(Function(s) listMenuId.Contains(s.MenuID)).ToList
        End Using

        Dim lstParentMenu As New List(Of CoreMenu)
        For Each oCoreMenu In listCoreMenu
            getAllParentMenu(oCoreMenu, lstParentMenu)
        Next
        Return lstParentMenu
    End Function

    Public Function getAllParentMenu(ByVal oCoreMenu As CoreMenu, ByRef lstParentMenu As List(Of CoreMenu)) As CoreMenu
        If (IsNothing(oCoreMenu.ParentID)) Then

            Return oCoreMenu
        Else
            If (lstParentMenu.Where(Function(x) x.MenuID = oCoreMenu.ParentID).Count > 0) Then
                Return oCoreMenu
            End If
            Using db As New PNSDBWEBEntities
                Dim m As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuID = oCoreMenu.ParentID).FirstOrDefault
                oCoreMenu = m
            End Using
            lstParentMenu.Add(oCoreMenu)
            Return getAllParentMenu(oCoreMenu, lstParentMenu)
        End If

    End Function

    Public Function GetCoreRoleMenuByDevisionAndPosition(ByVal pUserID As String) As List(Of Integer)

        Using db As New PNSDBWEBEntities
            Dim query = From mMenu In db.CoreUsersMenus.Where(Function(s) s.UserID = pUserID).Select(Function(s) s.MenuID.Value)


            Dim lists = query.ToList()
            Return lists
        End Using

    End Function

    Public Function GetCoreRoleMenuByDevisionAndPositionGetPermission(ByVal pUserID As String, ByVal ptype As String) As List(Of Integer)

        Using db As New PNSDBWEBEntities
            Dim query = From mMenu In db.CoreUsersMenus.Where(Function(s) s.UserID = pUserID).Select(Function(s) s.MenuID.Value)

            If ptype = "ADD" Then
                query = From mMenu In db.CoreUsersMenus.Where(Function(s) s.UserID = pUserID And s.isAdd = True).Select(Function(s) s.MenuID.Value)
            ElseIf ptype = "EDIT" Then
                query = From mMenu In db.CoreUsersMenus.Where(Function(s) s.UserID = pUserID And s.isEdit = True).Select(Function(s) s.MenuID.Value)
            ElseIf ptype = "DELETE" Then
                query = From mMenu In db.CoreUsersMenus.Where(Function(s) s.UserID = pUserID And s.isDelete = True).Select(Function(s) s.MenuID.Value)
            ElseIf ptype = "APPROVE" Then
                query = From mMenu In db.CoreUsersMenus.Where(Function(s) s.UserID = pUserID And s.isApprove = True).Select(Function(s) s.MenuID.Value)
            ElseIf ptype = "PRINT" Then
                query = From mMenu In db.CoreUsersMenus.Where(Function(s) s.UserID = pUserID And s.isPrint = True).Select(Function(s) s.MenuID.Value)
            End If

            Dim lists = query.ToList()
            Return lists
        End Using

    End Function



#Region "Dropdownlist"
    Public Function LoadModule(ByVal pLG As String) As List(Of CoreModule)
        Using db As New PNSDBWEBEntities
            Dim lc As List(Of CoreModule) = db.CoreModules.OrderBy(Function(s) s.ModuleID).ToList
            Return lc.Select(Function(s) New CoreModule With {.ModuleID = s.ModuleID, .ModuleNameEN = String.Format("{0} - {1}", s.ModuleID, IIf(pLG = "TH", s.ModuleNameTH, s.ModuleNameEN))}).ToList()
        End Using
    End Function
    Public Function LoadUserID(ByVal pKeyID As String,
                               ByVal pLG As String) As List(Of APIUserMenuAccess_ViewModel)
        Using db As New PNSDBWEBEntities
            If pLG = "TH" Then
                If pKeyID = String.Empty Then
                    Dim qury = From m In db.CoreUsers
                               Select New APIUserMenuAccess_ViewModel With {
                                  .Titles = m.TitleThai,
                                  .Description = m.NameThai & " " & m.LastnameThai,
                                  .KeyID = m.UserID
                               }
                    Dim lists = qury.ToList()
                    Return lists
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = From m In db.CoreUsers
                               Select New APIUserMenuAccess_ViewModel With {
                                  .Titles = m.TitleThai,
                                  .Description = m.NameThai & " " & m.LastnameThai,
                                  .KeyID = m.UserID
                               }
                    If Not String.IsNullOrEmpty(pKeyID) Then
                        qury = qury.Where(Function(s) (s.Titles.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", "")) Or
                                                       s.Description.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", "")) Or
                                                       s.KeyID.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))))
                    End If
                    Dim lists = qury.ToList()
                    Return lists
                End If
            Else
                If pKeyID = String.Empty Then
                    Dim qury = From m In db.CoreUsers
                               Select New APIUserMenuAccess_ViewModel With {
                                  .Titles = m.TitleEng,
                                  .Description = m.NameEng & " " & m.LastnameEng,
                                  .KeyID = m.UserID
                               }
                    Dim lists = qury.ToList()
                    Return lists
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = From m In db.CoreUsers
                               Select New APIUserMenuAccess_ViewModel With {
                                  .Titles = m.TitleEng,
                                  .Description = m.NameEng & " " & m.LastnameEng,
                                  .KeyID = m.UserID
                               }
                    If Not String.IsNullOrEmpty(pKeyID) Then
                        qury = qury.Where(Function(s) (s.Titles.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", "")) Or
                                                       s.Description.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", "")) Or
                                                       s.KeyID.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))))
                    End If
                    Dim lists = qury.ToList()
                    Return lists
                End If
            End If
        End Using
        Return Nothing
    End Function
#End Region
#End Region


End Class

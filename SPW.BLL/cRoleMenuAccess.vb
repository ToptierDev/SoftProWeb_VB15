
Imports System.Collections.Generic
    Imports System.Linq
    Imports System.Text
Imports SPW.BLL
Imports SPW.DAL
Public Class cRoleMenuAccess

#Region "MST_RoleMenuAccess.aspx"

    Public Function GetRoleMenuAccessFRM(ByVal pModule As String) As List(Of MenuAccess_ViewModel)

        Using db As New PNSDBWEBEntities
            Dim query = From mMenu In db.VW_MenuFRM.Where(Function(s) s.ModuleID = pModule And s.MenuID <> "99999")
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

            Dim lists = query.ToList()
            Return lists
        End Using

    End Function

    Public Function GetRoleMenuAccessRPT(ByVal pModule As String) As List(Of MenuAccess_ViewModel)

        Using db As New PNSDBWEBEntities
            Dim query = From mMenu In db.VW_MenuRPT.Where(Function(s) s.ModuleID = pModule And s.MenuID <> "99999")
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

            Dim lists = query.ToList()
            Return lists
        End Using

    End Function

    Public Function GetCoreRoleMenuByDevisionAndPosition(ByVal pDivisionID As String, ByVal pPositionID As String) As List(Of Integer)

        Using db As New PNSDBWEBEntities
            Dim query = From mMenu In db.CoreRoleMenus.Where(Function(s) s.DivisionCode = pDivisionID And s.PositionCode = pPositionID).Select(Function(s) s.MenuID.Value)


            Dim lists = query.ToList()
            Return lists
        End Using

    End Function

    Public Function GetCoreRoleMenuByDevisionAndPositionGetPermission(ByVal pDivisionID As String, ByVal pPositionID As String, ByVal ptype As String) As List(Of Integer)

        Using db As New PNSDBWEBEntities
            Dim query = From mMenu In db.CoreRoleMenus.Where(Function(s) s.DivisionCode = pDivisionID And s.PositionCode = pPositionID).Select(Function(s) s.MenuID.Value)

            If ptype = "ADD" Then
                query = From mMenu In db.CoreRoleMenus.Where(Function(s) s.DivisionCode = pDivisionID And s.PositionCode = pPositionID And s.isAdd = True).Select(Function(s) s.MenuID.Value)
            ElseIf ptype = "EDIT" Then
                query = From mMenu In db.CoreRoleMenus.Where(Function(s) s.DivisionCode = pDivisionID And s.PositionCode = pPositionID And s.isEdit = True).Select(Function(s) s.MenuID.Value)
            ElseIf ptype = "DELETE" Then
                query = From mMenu In db.CoreRoleMenus.Where(Function(s) s.DivisionCode = pDivisionID And s.PositionCode = pPositionID And s.isDelete = True).Select(Function(s) s.MenuID.Value)
            ElseIf ptype = "APPROVE" Then
                query = From mMenu In db.CoreRoleMenus.Where(Function(s) s.DivisionCode = pDivisionID And s.PositionCode = pPositionID And s.isApprove = True).Select(Function(s) s.MenuID.Value)
            ElseIf ptype = "PRINT" Then
                query = From mMenu In db.CoreRoleMenus.Where(Function(s) s.DivisionCode = pDivisionID And s.PositionCode = pPositionID And s.isPrint = True).Select(Function(s) s.MenuID.Value)
            End If

            Dim lists = query.ToList()
            Return lists
        End Using

    End Function



    Public Function Save(ByVal pCoreRoleMenu As List(Of CoreRoleMenu),
                         ByVal pModuleID As Integer,
                         ByVal pDivisionCode As String,
                         ByVal pPosition As String
                        ) As Boolean




        Using db As New PNSDBWEBEntities

                Dim allMenuInModule As List(Of Integer) = db.CoreMenus.Where(Function(s) s.ModuleID = pModuleID
                ).Select(Function(s) s.MenuID).ToList()
                Dim affectUser As List(Of String) =
                      db.CoreUsers.Where(Function(r) r.Division = pDivisionCode And r.Position = pPosition).Select(Function(r) r.UserID).ToList()


                Dim deleteRoleMenu As List(Of CoreRoleMenu) =
                      db.CoreRoleMenus.Where(Function(r) r.DivisionCode = pDivisionCode And r.PositionCode = pPosition _
                            And allMenuInModule.Contains(r.MenuID)).ToList()
                db.CoreRoleMenus.RemoveRange(deleteRoleMenu)


                Dim deleteUserMenu As List(Of CoreUsersMenu) =
                      db.CoreUsersMenus.Where(Function(r) _
                             allMenuInModule.Contains(r.MenuID) _
                            And affectUser.Contains(r.UserID)
                            ).ToList()
                db.CoreUsersMenus.RemoveRange(deleteUserMenu)

                If pCoreRoleMenu IsNot Nothing Then
                    Dim listUserMenu As New List(Of CoreUsersMenu)
                    For Each userid In affectUser
                    listUserMenu.AddRange(pCoreRoleMenu.Select(Function(x) New CoreUsersMenu With
                                                              {.MenuID = x.MenuID,
                                                              .UserID = userid,
                                                               .isAdd = x.isAdd,
                                                                .isEdit = x.isEdit,
                                                                .isDelete = x.isDelete,
                                                                .isApprove = x.isApprove,
                                                                .isPrint = x.isPrint}).ToList())
                Next
                    db.CoreRoleMenus.AddRange(pCoreRoleMenu)
                    db.CoreUsersMenus.AddRange(listUserMenu)
                End If

                'If pCoreRoleMenu IsNot Nothing Then
                '    If pCoreRoleMenu.Count > 0 Then

                '        For Each m As CoreRoleMenu In pCoreRoleMenu
                '            db.CoreRoleMenus.Add(m)
                '            For Each u As String In affectUser
                '                Dim userMenu As New CoreUsersMenu
                '                userMenu.MenuID = m.MenuID
                '                userMenu.UserID = u

                '                Dim ListUserCoreMenu As List(Of CoreMenu) = db.CoreMenus.Where(Function(s) s.MenuID = m.MenuID).ToList
                '                For Each us As CoreMenu In ListUserCoreMenu
                '                    Dim ListUserCoreMenus As List(Of CoreUsersMenu) = db.CoreUsersMenus.Where(Function(s) s.MenuID = us.ParentID And s.UserID = u).ToList
                '                    For Each uss As CoreUsersMenu In ListUserCoreMenus
                '                        db.CoreUsersMenus.Remove(uss)
                '                    Next
                '                    Dim ListUserCoreMenusCheck As List(Of CoreUsersMenu) = db.CoreUsersMenus.Where(Function(s) s.MenuID = us.ParentID And s.UserID = u).ToList
                '                    If ListUserCoreMenusCheck.Count = 0 Then
                '                        If us.ParentID IsNot Nothing Then
                '                            Dim lssct As CoreUsersMenu = New CoreUsersMenu
                '                            lssct.MenuID = us.ParentID
                '                            lssct.UserID = u
                '                            db.CoreUsersMenus.Add(lssct)
                '                        End If
                '                    End If
                '                Next
                '                db.CoreUsersMenus.Add(userMenu)
                '            Next
                '        Next

                '    End If
                'End If

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

#Region "Dropdownlist"

    Public Function LoadDivision(ByVal pLG As String) As List(Of CoreDivision)
        Using db As New PNSDBWEBEntities
            Dim lc As List(Of CoreDivision) = db.CoreDivisions.OrderBy(Function(s) s.DivisionName).ToList
            Return lc
        End Using
    End Function
    Public Function LoadPosition(ByVal pLG As String) As List(Of CorePosition)
        Using db As New PNSDBWEBEntities
            Dim lc As List(Of CorePosition) = db.CorePositions.OrderBy(Function(s) s.PositionName).ToList
            Return lc
        End Using
    End Function
    Public Function LoadModule(ByVal pLG As String) As List(Of CoreModule)
            Using db As New PNSDBWEBEntities
                Dim lc As List(Of CoreModule) = db.CoreModules.OrderBy(Function(s) s.ModuleID).ToList
                Return lc.Select(Function(s) New CoreModule With {.ModuleID = s.ModuleID, .ModuleNameEN = String.Format("{0} - {1}", s.ModuleID, IIf(pLG = "TH", s.ModuleNameTH, s.ModuleNameEN))}).ToList()
            End Using
        End Function

#End Region


#End Region


End Class

Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL
Public Class cMenu

#Region "Defualt.aspx"
    'Public Function GetMenu(ByVal pUserID As String,
    '                        ByVal pModule As String,
    '                        ByVal pUserRoleCode As String) As List(Of MasterMenu_ViewModel)

    '    Using db As New PNSDBWEBEntities
    '        Dim oUserMenuCount = db.CoreUsersMenus.Where(Function(q) q.EmployeeNo.Equals(pUserID)).Count
    '        Dim oMenuList As List(Of CoreMenu)
    '        If oUserMenuCount > 0 Then
    '            oMenuList = New List(Of CoreMenu)
    '            Dim oUserMenuAccessList = db.CoreUsersMenus.Where(Function(q) q.EmployeeNo.Equals(pUserID)).ToList
    '            For Each oUserMenuAccess In oUserMenuAccessList
    '                'Dim oMenu = db.CoreMenus.Where(Function(q) q.MenuID = oUserMenuAccess.MenuID And (q.ModuleID = pModule)).SingleOrDefault
    '                Dim oMenu = db.CoreMenus.Where(Function(q) q.MenuID = oUserMenuAccess.MenuID).SingleOrDefault
    '                If Not oMenu Is Nothing Then
    '                    oMenuList.Add(oMenu)
    '                End If
    '            Next
    '            Dim possibleMenu As List(Of CoreMenu) = oMenuList.ToList
    '            Dim possibleMenuLists As List(Of Integer?) = possibleMenu.Select(Function(s) s.ParentID).ToList()

    '            Dim oMenuListHome As List(Of CoreMenu) = db.CoreMenus.Where(Function(q) q.MenuID = 1).ToList
    '            For Each oMenuHome In oMenuListHome
    '                oMenuList.Add(oMenuHome)
    '            Next
    '            'Dim oMenuListCore As List(Of CoreMenu) = db.CoreMenus.Where(Function(q) q.MenuID = 42).ToList
    '            'For Each oMenuCore In oMenuListCore
    '            '    oMenuList.Add(oMenuCore)
    '            'Next

    '            Dim oMenuListNull = db.CoreMenus.Where(Function(q) q.ModuleID Is Nothing And possibleMenuLists.Contains(q.MenuID)).ToList
    '            For Each oMenu In oMenuListNull
    '                Dim oMenuListNull2 As CoreMenu = db.CoreMenus.Where(Function(q) q.MenuID = oMenu.ParentID).FirstOrDefault
    '                If oMenuListNull2 IsNot Nothing Then
    '                    oMenuList.Add(oMenuListNull2)
    '                End If
    '                oMenuList.Add(oMenu)
    '            Next


    '            oMenuList = oMenuList.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence).Distinct.ToList

    '        Else
    '            oMenuList = db.CoreMenus.Where(Function(q) q.ParentID Is Nothing).OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence).ToList
    '        End If
    '        Return oMenuList
    '        'Dim lcMenu As List(Of CoreMenu) = db.CoreMenus.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence).ToList

    '        'Return lcMenu
    '    End Using
    'End Function
    Public Function GetMenu(ByVal pUserID As String,
                            ByVal pModule As String,
                            ByVal pUserRoleCode As String) As List(Of MasterMenu_ViewModel)

        Using db As New PNSDBWEBEntities
            Dim oMenuList As List(Of MasterMenu_ViewModel)
            Dim oUserMenuCount = db.CoreUsersMenus.Where(Function(q) q.UserID.Equals(pUserID)).Count
            If oUserMenuCount > 0 Then
                Dim qury = From l In db.CoreUsersMenus.Where(Function(q) q.UserID.Equals(pUserID))
                           Group Join mMenu In db.CoreMenus On l.MenuID Equals mMenu.MenuID
                           Into mMen = Group From mMenu In mMen.DefaultIfEmpty()
                           Group Join mModule In db.CoreModules On mMenu.ModuleID Equals mModule.ModuleID
                           Into mModu = Group From mModule In mModu.DefaultIfEmpty()
                           Select New MasterMenu_ViewModel With {
                    .MenuID = l.MenuID,
                    .MenuNameLC = mMenu.MenuNameLC,
                    .MenuNameEN = mMenu.MenuNameEN,
                    .MenuLocation = mMenu.MenuLocation,
                    .ParentID = mMenu.ParentID,
                    .Sequence = mMenu.Sequence,
                    .MenuType = mMenu.MenuType,
                    .EnableFlag = mMenu.EnableFlag,
                    .SystemID = mMenu.SystemID,
                    .MenuIcon = mMenu.MenuIcon,
                    .ModuleID = mMenu.ModuleID,
                    .ModuleNameLC = mModule.ModuleNameTH,
                    .ModuleNameEN = mModule.ModuleNameEN
                }

                qury = qury.Distinct()
                qury = qury.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)

                oMenuList = qury.ToList()
                qury = From mMenu In db.CoreMenus.Where(Function(s) s.MenuID = 1)
                       Group Join mModule In db.CoreModules On mMenu.ModuleID Equals mModule.ModuleID
                       Into mModu = Group From mModule In mModu.DefaultIfEmpty()
                       Select New MasterMenu_ViewModel With {
                    .MenuID = mMenu.MenuID,
                    .MenuNameLC = mMenu.MenuNameLC,
                    .MenuNameEN = mMenu.MenuNameEN,
                    .MenuLocation = mMenu.MenuLocation,
                    .ParentID = mMenu.ParentID,
                    .Sequence = mMenu.Sequence,
                    .MenuType = mMenu.MenuType,
                    .EnableFlag = mMenu.EnableFlag,
                    .SystemID = mMenu.SystemID,
                    .MenuIcon = mMenu.MenuIcon,
                    .ModuleID = mMenu.ModuleID,
                    .ModuleNameLC = mModule.ModuleNameTH,
                    .ModuleNameEN = mModule.ModuleNameEN
                }

                qury = qury.Distinct()
                qury = qury.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)
                Dim list = qury.ToList()
                For Each oMenuHome In list
                    oMenuList.Add(oMenuHome)
                Next
                Return oMenuList.Where(Function(s) s.MenuID IsNot Nothing).ToList
            End If


            'Dim lcMenu As List(Of CoreMenu) = db.CoreMenus.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence).ToList

        End Using
    End Function
#End Region

#Region "MST_Menu.aspx"
    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal pModule As String,
                             ByVal strUserID As String) As List(Of Menu_ViewModel)

        Using db As New PNSDBWEBEntities
            Dim qury = From l In db.CoreMenus
                       Group Join mModule In db.CoreModules On mModule.ModuleID Equals l.ModuleID
                           Into mMod = Group From mModule In mMod.DefaultIfEmpty()
                       Select New Menu_ViewModel With {
                               .MenuID = l.MenuID,
                               .ModuleID = mModule.ModuleID,
                               .ModuleNameTH = mModule.ModuleNameTH,
                               .ModuleNameEN = mModule.ModuleNameEN,
                               .MenuNameLC = l.MenuNameLC,
                               .MenuNameEN = l.MenuNameEN,
                               .MenuLocation = l.MenuLocation,
                               .MenuType = l.MenuType,
                               .ParentID = l.ParentID,
                               .Sequence = l.Sequence,
                               .EnableFlag = l.EnableFlag
                           }
            If Not String.IsNullOrEmpty(pModule) Then
                qury = qury.Where(Function(s) s.ModuleID = pModule)
            End If

            qury = qury.OrderBy(Function(s) s.MenuID)

            Dim lists = qury.ToList()
            Return lists
        End Using
    End Function

    Public Function LoadEditMenu(ByVal id As String,
                                 ByVal pUserID As String) As CoreMenu

        Using db As New PNSDBWEBEntities
            Dim query = db.CoreMenus.Where(Function(s) s.MenuID = id)

            Return query.FirstOrDefault
        End Using
    End Function

    Public Function Delete(ByVal id As String,
                           ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSDBWEBEntities
                Dim lc As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuID = id).SingleOrDefault
                db.CoreMenus.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Edit(ByVal pID As String,
                         ByVal pMenuID As String,
                         ByVal pMenuNameLC As String,
                         ByVal pMenuNameEN As String,
                         ByVal pMenuLocation As String,
                         ByVal pMenuType As String,
                         ByVal pParentID As String,
                         ByVal pSequence As String,
                         ByVal pStatus As Integer,
                         ByVal pModule As String,
                         ByVal pUserId As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim lc As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuID = pID).SingleOrDefault
            If lc IsNot Nothing Then
                lc.MenuNameLC = pMenuNameLC
                lc.MenuNameEN = pMenuNameEN
                lc.MenuLocation = pMenuLocation
                If pParentID <> String.Empty Then
                    lc.ParentID = pParentID
                End If
                If pSequence <> String.Empty Then
                    lc.Sequence = pSequence
                End If
                If pModule <> String.Empty Then
                    lc.ModuleID = CInt(pModule)
                Else
                    lc.ModuleID = Nothing
                End If
                lc.MenuType = pMenuType
                lc.EnableFlag = pStatus
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Add(ByVal pID As String,
                        ByVal pMenuID As String,
                        ByVal pMenuNameLC As String,
                        ByVal pMenuNameEN As String,
                        ByVal pMenuLocation As String,
                        ByVal pMenuType As String,
                        ByVal pParentID As String,
                        ByVal pSequence As String,
                        ByVal pStatus As Integer,
                        ByVal pModule As String,
                        ByVal pUserId As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim lc As CoreMenu = New CoreMenu

            lc.MenuID = pMenuID
            lc.MenuNameLC = pMenuNameLC
            lc.MenuNameEN = pMenuNameEN
            lc.MenuLocation = pMenuLocation
            If pParentID <> String.Empty Then
                lc.ParentID = pParentID
            End If
            If pSequence <> String.Empty Then
                lc.Sequence = pSequence
            End If
            If pModule <> String.Empty Then
                lc.ModuleID = CInt(pModule)
            Else
                lc.ModuleID = Nothing
            End If
            lc.MenuType = pMenuType
            lc.EnableFlag = pStatus

            db.CoreMenus.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function

#Region "Dropdownlist"
    Public Function LoadModule(ByVal pLG As String) As List(Of CoreModule)
        Using db As New PNSDBWEBEntities
            Dim lc As List(Of CoreModule) = db.CoreModules.OrderBy(Function(s) s.ModuleID).ToList
            Return lc.Select(Function(s) New CoreModule With {.ModuleID = s.ModuleID, .ModuleNameEN = String.Format("{0} - {1}", s.ModuleID, IIf(pLG = "TH", s.ModuleNameTH, s.ModuleNameEN))}).ToList()
        End Using
    End Function
#End Region
#End Region

End Class

Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL
Public Class cWebResouce

#Region "MST_WebResource.aspx"
    Public Function LoaddataWebResource(ByVal fillter As FillterSearch,
                                        ByRef TotalRow As Integer,
                                        ByVal pMenu1 As String,
                                        ByVal pMenu2 As String,
                                        ByVal pMenu3 As String,
                                        ByVal pType As String,
                                        ByVal pModule As String,
                                        ByVal pSearchTH As String,
                                        ByVal pSearchEN As String,
                                        ByVal pBaseMassage As String) As List(Of WebResource_ViewModel)

        Using db As New PNSDBWEBEntities
            Dim qury = From l In db.CoreWebResources
                       Group Join m In db.CoreMenus On m.MenuID Equals l.MenuID
                       Into dgrop = Group From m In dgrop.DefaultIfEmpty()
                       Select New WebResource_ViewModel With {
                            .ResourceID = l.ResourceID,
                            .MenuNameLC = m.MenuNameLC,
                            .MenuNameEN = m.MenuNameEN,
                            .MenuID = l.MenuID,
                            .ModuleID = m.ModuleID,
                            .ResourceName = l.ResourceName,
                            .ResourceType = l.ResourceType,
                            .ResourceValueEN = l.ResourceValueEN,
                            .ResourceValueLC = l.ResourceValueLC,
                            .ParentID = m.ParentID
                       }
            If Not String.IsNullOrEmpty(fillter.Keyword) Then
                qury = qury.Where(Function(s) s.ResourceName.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.ResourceValueLC.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.ResourceValueEN.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")))
            End If
            If pBaseMassage = String.Empty Then
                If pMenu1 <> String.Empty _
               And pMenu2 = String.Empty _
               And pMenu3 = String.Empty Then
                    qury = qury.Where(Function(s) s.ParentID = pMenu1)
                End If

                If pMenu1 <> String.Empty _
                   And pMenu2 <> String.Empty _
                   And pMenu3 = String.Empty Then
                    qury = qury.Where(Function(s) s.MenuID = pMenu2)
                End If

                If pMenu1 <> String.Empty _
                   And pMenu2 <> String.Empty _
                   And pMenu3 <> String.Empty Then
                    qury = qury.Where(Function(s) s.MenuID = pMenu3)
                End If


                If pMenu1 = String.Empty _
                   And pMenu2 <> String.Empty _
                   And pMenu3 = String.Empty Then
                    qury = qury.Where(Function(s) s.MenuID = pMenu2)
                End If

                If pMenu1 = String.Empty _
                   And pMenu2 = String.Empty _
                   And pMenu3 <> String.Empty Then
                    qury = qury.Where(Function(s) s.MenuID = pMenu3)
                End If

                If pMenu1 = String.Empty _
                   And pMenu2 <> String.Empty _
                   And pMenu3 <> String.Empty Then
                    qury = qury.Where(Function(s) s.MenuID = pMenu3)
                End If
            Else
                qury = qury.Where(Function(s) s.MenuID = pBaseMassage)
            End If


            If pType <> String.Empty Then
                If pType.ToUpper = "TOOLTIP" Then
                    qury = qury.Where(Function(s) s.ResourceName.ToUpper.Replace(" ", "").Contains("RESTOOLTIP"))
                Else
                    qury = qury.Where(Function(s) s.ResourceType = pType)
                End If
            End If

            If pSearchTH <> String.Empty Then
                qury = qury.Where(Function(s) s.ResourceValueLC.ToUpper.Replace(" ", "").Contains(pSearchTH.ToUpper))
            End If

            If pSearchEN <> String.Empty Then
                qury = qury.Where(Function(s) s.ResourceValueEN.ToUpper.Replace(" ", "").Contains(pSearchEN.ToUpper))
            End If

            If pModule <> String.Empty Then
                qury = qury.Where(Function(s) s.ModuleID = pModule)
            End If

            Select Case fillter.SortType '0-None , 1 -Asc , 2 - Desc
                Case 1
                    If fillter.SortBy.ToUpper = "RESOURCENAME" Then
                        qury = qury.OrderBy(Function(s) s.ResourceName)
                    ElseIf fillter.SortBy.ToUpper = "RESOURCEVALUELC" Then
                        qury = qury.OrderBy(Function(s) s.ResourceValueLC)
                    ElseIf fillter.SortBy.ToUpper = "RESOURCEVALUEEN" Then
                        qury = qury.OrderBy(Function(s) s.ResourceValueEN)
                    ElseIf fillter.SortBy.ToUpper = "RESOURCETYPE" Then
                        qury = qury.OrderBy(Function(s) s.ResourceType)
                    End If
                Case 2
                    If fillter.SortBy.ToUpper = "RESOURCENAME" Then
                        qury = qury.OrderByDescending(Function(s) s.ResourceName)
                    ElseIf fillter.SortBy.ToUpper = "RESOURCEVALUELC" Then
                        qury = qury.OrderByDescending(Function(s) s.ResourceValueLC)
                    ElseIf fillter.SortBy.ToUpper = "RESOURCEVALUEEN" Then
                        qury = qury.OrderByDescending(Function(s) s.ResourceValueEN)
                    ElseIf fillter.SortBy.ToUpper = "RESOURCETYPE" Then
                        qury = qury.OrderByDescending(Function(s) s.ResourceType)
                    End If
                Case Else
                    qury = qury.OrderBy(Function(s) s.MenuID)
                    Exit Select
            End Select

            If qury IsNot Nothing Then
                TotalRow = qury.Count
            End If

            If (fillter.PageSize > 0 And fillter.Page >= 0) Then
                qury = qury.Skip((fillter.Page - 1) * fillter.PageSize).Take(fillter.PageSize)
            End If

            Dim lists = qury.ToList()
            Return lists
        End Using
    End Function
    Public Function LoadMenu1(ByVal pModule As String) As List(Of MasterMenu_ViewModel)

        Using db As New PNSDBWEBEntities
            'Dim lcMenu As List(Of vw_CoreMenuLevel1) = db.vw_CoreMenuLevel1.Where(Function(s) s.EnableFlag = 1).ToList()

            'If pModule <> String.Empty Then
            '    lcMenu = lcMenu.Where(Function(s) s.a_moduleID = pModule And s.ModuleID = pModule).ToList()
            'End If

            'lcMenu = lcMenu.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence).ToList()

            'Return lcMenu
            Dim query = From a In db.CoreMenus.Where(Function(s) s.ParentID Is Nothing And s.ModuleID IsNot Nothing)
                        Join b In db.CoreMenus.Where(Function(s) s.ModuleID IsNot Nothing) On a.MenuID Equals b.ParentID
                        Select New MasterMenu_ViewModel With {.MenuID = a.MenuID,
                                                            .MenuNameLC = a.MenuNameLC,
                                                            .MenuNameEN = a.MenuNameEN,
                                                            .MenuLocation = a.MenuLocation,
                                                            .ParentID = a.ParentID,
                                                            .Sequence = a.Sequence,
                                                            .MenuType = a.MenuType,
                                                            .EnableFlag = a.EnableFlag,
                                                            .SystemID = a.SystemID,
                                                            .MenuIcon = a.MenuIcon,
                                                            .ModuleID = a.ModuleID}
            If pModule <> String.Empty Then
                query = query.Where(Function(s) s.ModuleID = pModule)
            End If

            query = query.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)

            Dim lists = query.Distinct.ToList

            Return lists
        End Using
    End Function
    Public Function LoadMenu2(ByVal pMenu As String,
                              ByVal pModule As String) As List(Of MasterMenu_ViewModel)

        Using db As New PNSDBWEBEntities

            'Dim lcMenu As List(Of vw_CoreMenuLevel2) = db.vw_CoreMenuLevel2.Where(Function(s) s.EnableFlag = 1).ToList()

            'If pMenu <> String.Empty Then
            '    lcMenu = lcMenu.Where(Function(s) s.ParentID = pMenu).ToList()
            'End If

            'If pModule <> String.Empty Then
            '    lcMenu = lcMenu.Where(Function(s) s.a_moduleID = pModule And s.ModuleID = pModule).ToList()
            'End If

            'lcMenu = lcMenu.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence).ToList()

            'Return lcMenu
            Dim query = From a In db.CoreMenus.Where(Function(s) s.ModuleID IsNot Nothing)
                        Join b In db.CoreMenus.Where(Function(s) s.ModuleID IsNot Nothing) On a.MenuID Equals b.ParentID
                        Select New MasterMenu_ViewModel With {.MenuID = b.MenuID,
                                                            .MenuNameLC = b.MenuNameLC,
                                                            .MenuNameEN = b.MenuNameEN,
                                                            .MenuLocation = b.MenuLocation,
                                                            .ParentID = b.ParentID,
                                                            .Sequence = b.Sequence,
                                                            .MenuType = b.MenuType,
                                                            .EnableFlag = b.EnableFlag,
                                                            .SystemID = b.SystemID,
                                                            .MenuIcon = b.MenuIcon,
                                                            .ModuleID = b.ModuleID}

            If pMenu <> String.Empty Then
                query = query.Where(Function(s) s.ParentID = pMenu)
            End If

            If pModule <> String.Empty Then
                query = query.Where(Function(s) s.ModuleID = pModule)
            End If

            query = query.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)

            Dim lists = query.Distinct.ToList

            Return lists
        End Using
    End Function
    Public Function LoadMenu3(ByVal pMenu As String,
                              ByVal pModule As String) As List(Of MasterMenu_ViewModel)

        Using db As New PNSDBWEBEntities
            'Dim lcMenu As List(Of vw_CoreMenuLevel3) = db.vw_CoreMenuLevel3.Where(Function(s) s.EnableFlag = 1).ToList()

            'If pMenu <> String.Empty Then
            '    lcMenu = lcMenu.Where(Function(s) s.ParentID = pMenu).ToList()
            'End If

            'If pModule <> String.Empty Then
            '    lcMenu = lcMenu.Where(Function(s) s.ModuleID = pModule).ToList()
            'End If

            'lcMenu = lcMenu.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence).ToList()

            'Return lcMenu
            Dim query = From a In db.CoreMenus
                        Join b In db.CoreMenus On a.MenuID Equals b.ParentID
                        Join c In db.CoreMenus.Where(Function(s) s.ModuleID IsNot Nothing) On b.MenuID Equals c.ParentID
                        Select New MasterMenu_ViewModel With {.MenuID = c.MenuID,
                                                            .MenuNameLC = c.MenuNameLC,
                                                            .MenuNameEN = c.MenuNameEN,
                                                            .MenuLocation = c.MenuLocation,
                                                            .ParentID = c.ParentID,
                                                            .Sequence = c.Sequence,
                                                            .MenuType = c.MenuType,
                                                            .EnableFlag = c.EnableFlag,
                                                            .SystemID = c.SystemID,
                                                            .MenuIcon = c.MenuIcon,
                                                            .ModuleID = c.ModuleID}
            If pMenu <> String.Empty Then
                query = query.Where(Function(s) s.ParentID = pMenu)
            End If

            If pModule <> String.Empty Then
                query = query.Where(Function(s) s.ModuleID = pModule)
            End If

            query = query.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)

            Dim lists = query.Distinct.ToList

            Return lists
        End Using
    End Function

    Public Function LoadMenuEdit1(ByVal pModule As String,
                                  ByVal pPrarentID As String) As MasterMenu_ViewModel

        Using db As New PNSDBWEBEntities
            'Dim lcMenu As List(Of vw_CoreMenuLevel1) = db.vw_CoreMenuLevel1.Where(Function(s) s.EnableFlag = 1).ToList()

            'If pModule <> String.Empty Then
            '    lcMenu = lcMenu.Where(Function(s) s.a_moduleID = pModule And s.ModuleID = pModule).ToList()
            'End If

            'lcMenu = lcMenu.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence).ToList()

            'Return lcMenu
            Dim query = From a In db.CoreMenus.Where(Function(s) s.ParentID Is Nothing And s.ModuleID IsNot Nothing)
                        Join b In db.CoreMenus.Where(Function(s) s.ModuleID IsNot Nothing) On a.MenuID Equals b.ParentID
                        Select New MasterMenu_ViewModel With {.MenuID = a.MenuID,
                                                            .MenuNameLC = a.MenuNameLC,
                                                            .MenuNameEN = a.MenuNameEN,
                                                            .MenuLocation = a.MenuLocation,
                                                            .ParentID = a.ParentID,
                                                            .Sequence = a.Sequence,
                                                            .MenuType = a.MenuType,
                                                            .EnableFlag = a.EnableFlag,
                                                            .SystemID = a.SystemID,
                                                            .MenuIcon = a.MenuIcon,
                                                            .ModuleID = a.ModuleID}
            If pModule <> String.Empty Then
                query = query.Where(Function(s) s.ModuleID = pModule)
            End If

            If pPrarentID <> String.Empty Then
                query = query.Where(Function(s) s.ParentID = pPrarentID)
            End If

            query = query.OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)

            Dim lists = query.FirstOrDefault

            Return lists
        End Using
    End Function

    Public Function LoadMenuParent() As List(Of CoreMenu)

        Using db As New PNSDBWEBEntities
            Dim lcMenu As List(Of CoreMenu) = db.CoreMenus.Where(Function(s) s.ParentID IsNot Nothing And s.EnableFlag = 1) _
                                                          .OrderBy(Function(s) s.ParentID) _
                                                          .ThenBy(Function(s) s.Sequence).ToList()

            Return lcMenu
        End Using
    End Function
#Region "Autocompleted"
    Public Function LoadMenuNotParent(ByVal pKeyID As String,
                                      ByVal pLG As String) As List(Of String)

        Using db As New PNSDBWEBEntities
            If pLG = "TH" Then
                If pKeyID = String.Empty Then
                    Dim qury = (From m In db.CoreMenus.Where(Function(s) s.MenuLocation = "#" And s.EnableFlag = 1).OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)
                                Select m.MenuID & "-" & m.MenuNameLC).ToList()

                    Return qury.ToList()
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = (From m In db.CoreMenus.Where(Function(s) s.MenuLocation = "#" And s.EnableFlag = 1).OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)
                                Select m.MenuID & "-" & m.MenuNameLC) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                    Return qury.ToList()
                End If
            Else
                If pKeyID = String.Empty Then
                    Dim qury = (From m In db.CoreMenus.Where(Function(s) s.MenuLocation = "#" And s.EnableFlag = 1).OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)
                                Select m.MenuID & "-" & m.MenuNameEN).ToList()

                    Return qury.ToList()
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = (From m In db.CoreMenus.Where(Function(s) s.MenuLocation = "#" And s.EnableFlag = 1).OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)
                                Select m.MenuID & "-" & m.MenuNameEN) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                    Return qury.ToList()
                End If
            End If

            Return Nothing
        End Using
    End Function

    Public Function LoadSubMenuByMenu(ByVal pKeyID As String,
                                      ByVal pLG As String,
                                      ByVal pMenu As String) As List(Of String)

        Using db As New PNSDBWEBEntities
            Try
                If pLG = "TH" Then
                    If pKeyID = String.Empty Then
                        Dim qury = (From m In db.CoreMenus.Where(Function(s) s.ParentID = pMenu And s.EnableFlag = 1).OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)
                                    Select m.MenuID & "-" & m.MenuNameLC).ToList()

                        Return qury.ToList()
                    ElseIf pKeyID <> String.Empty Then
                        Dim qury = (From m In db.CoreMenus.Where(Function(s) s.ParentID = pMenu And s.EnableFlag = 1).OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)
                                    Select m.MenuID & "-" & m.MenuNameLC) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                        Return qury.ToList()
                    End If
                Else
                    If pKeyID = String.Empty Then
                        Dim qury = (From m In db.CoreMenus.Where(Function(s) s.ParentID = pMenu And s.EnableFlag = 1).OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)
                                    Select m.MenuID & "-" & m.MenuNameEN).ToList()

                        Return qury.ToList()
                    ElseIf pKeyID <> String.Empty Then
                        Dim qury = (From m In db.CoreMenus.Where(Function(s) s.ParentID = pMenu And s.EnableFlag = 1).OrderBy(Function(s) s.ParentID).ThenBy(Function(s) s.Sequence)
                                    Select m.MenuID & "-" & m.MenuNameEN) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                        Return qury.ToList()
                    End If
                End If

                Return Nothing
            Catch ex As Exception
                Return Nothing
            End Try
        End Using

    End Function
#End Region

    Public Function LoadMenuNotParentEdit(ByVal pID As String) As CoreMenu

        Using db As New PNSDBWEBEntities
            Dim lcMenu As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuID = pID And s.EnableFlag = 1) _
                                                          .OrderBy(Function(s) s.ParentID) _
                                                          .ThenBy(Function(s) s.Sequence).FirstOrDefault()

            Return lcMenu
        End Using
    End Function

    Public Function LoadEditWebResource(ByVal pID As String) As CoreWebResource

        Using db As New PNSDBWEBEntities
            Dim lcWebResource As CoreWebResource = db.CoreWebResources.Where(Function(s) s.ResourceID = pID).SingleOrDefault
            Return lcWebResource
        End Using
    End Function
    Public Function LoadMenuParentBySubMenu(ByVal pSubMenu As String) As CoreMenu

        Using db As New PNSDBWEBEntities

            Dim lcMenu As CoreMenu = db.CoreMenus.Where(Function(s) s.MenuID = pSubMenu).SingleOrDefault

            Return lcMenu

        End Using

    End Function

    Public Function LoadBaseMassage() As List(Of CoreMenu)

        Using db As New PNSDBWEBEntities

            Dim lcMenu As List(Of CoreMenu) = db.CoreMenus.Where(Function(s) s.MenuID = 1 Or s.MenuID = 99999).ToList

            Return lcMenu

        End Using

    End Function


    Public Function AddWebResource(ByVal pMenuID As String, _
                                   ByVal pResourceName As String, _
                                   ByVal pType As String, _
                                   ByVal pResourceValueEN As String, _
                                   ByVal pResourceValueLC As String, _
                                   ByVal pUserId As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim Add_WebResource As CoreWebResource = New CoreWebResource

            Add_WebResource.MenuID = pMenuID
            Add_WebResource.ResourceName = pResourceName
            If pType = "Tooltip" Then
                Add_WebResource.ResourceType = "Text"
            Else
                Add_WebResource.ResourceType = pType
            End If
            Add_WebResource.ResourceValueEN = pResourceValueEN
            Add_WebResource.ResourceValueLC = pResourceValueLC
            Add_WebResource.UpdateBy = pUserId
            Add_WebResource.UpdateDate = DateTime.Now

            db.CoreWebResources.Add(Add_WebResource)
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function EditWebResource(ByVal pID As String, _
                                    ByVal pMenuID As String, _
                                    ByVal pResourceName As String, _
                                    ByVal pType As String, _
                                    ByVal pResourceValueEN As String, _
                                    ByVal pResourceValueLC As String, _
                                    ByVal pUserId As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim Edit_WebResource As CoreWebResource = db.CoreWebResources.Where(Function(s) s.ResourceID = pID).SingleOrDefault
            If Edit_WebResource IsNot Nothing Then
                'Edit_WebResource.MenuID = pMenuID
                Edit_WebResource.ResourceName = pResourceName
                If pType = "Tooltip" Then
                    Edit_WebResource.ResourceType = "Text"
                Else
                    Edit_WebResource.ResourceType = pType
                End If
                Edit_WebResource.ResourceValueEN = pResourceValueEN
                Edit_WebResource.ResourceValueLC = pResourceValueLC
                If pMenuID <> String.Empty Then
                    Edit_WebResource.MenuID = CInt(pMenuID)
                End If
                Edit_WebResource.UpdateBy = pUserId
                Edit_WebResource.UpdateDate = DateTime.Now
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function
#End Region

#Region "Dropdownlist"
    Public Function LoadModule(ByVal pUserId As String) As List(Of CoreModule)
        Using db As New PNSDBWEBEntities
            Dim lc = db.CoreModules.OrderBy(Function(s) s.ModuleID)
            Return lc.ToList
        End Using
    End Function
#End Region
End Class

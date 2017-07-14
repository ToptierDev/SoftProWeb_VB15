Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Web.Script.Serialization
Public Class MST_UsersMenuAccess
    Inherits BasePage

    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.DataBind()
                hddpLG.Value = Me.WebCulture.ToUpper()
                hddParameterMenuID.Value = HelperLog.LoadMenuID("MST_UsersMenuAccess.aspx")
                Me.ClearSessionPageLoad("MST_UsersMenuAccess.aspx")
                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                'Call LoadRadioButton()
                Call GetParameter()
                Call Loaddata()

                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "AutocompletedUserID(this,event);", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        Call Redirect("MST_UsersMenuAccess.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)

        Session.Remove("MST_UsersMenuAccess_UserID")
        Session.Remove("MST_UsersMenuAccess_Module")
        Session.Remove("MST_UsersMenuAccess_Type")
        Session.Remove("MST_UsersMenuAccess_FS")
        Session.Add("MST_UsersMenuAccess_UserID", IIf(txtsUserID.Text <> String.Empty, txtsUserID.Text, ""))
        Session.Add("MST_UsersMenuAccess_Module", IIf(ddlsModule.SelectedIndex <> 0, ddlsModule.SelectedValue, ""))
        'Session.Add("MST_UsersMenuAccess_Type", IIf(rdblCheckType.SelectedValue <> String.Empty, rdblCheckType.SelectedValue, ""))
        Session.Add("MST_UsersMenuAccess_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("MST_UsersMenuAccess_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("MST_UsersMenuAccess_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("MST_UsersMenuAccess_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("MST_UsersMenuAccess_UserID") IsNot Nothing Then
            If Session("MST_UsersMenuAccess_UserID").ToString <> String.Empty Then
                Dim strUserID As String = Session("MST_UsersMenuAccess_UserID").ToString
                txtsUserID.Text = strUserID
            End If
        End If
        If Session("MST_UsersMenuAccess_Module") IsNot Nothing Then
            If Session("MST_UsersMenuAccess_Module").ToString <> String.Empty Then
                Dim strModule As String = Session("MST_UsersMenuAccess_Module").ToString
                Try
                    ddlsModule.SelectedValue = strModule
                Catch ex As Exception
                    ddlsModule.SelectedIndex = 0
                End Try
            End If
        End If
        'If Session("MST_UsersMenuAccess_Type") IsNot Nothing Then
        '    If Session("MST_UsersMenuAccess_Type").ToString <> String.Empty Then
        '        Dim strType As String = Session("MST_UsersMenuAccess_Type").ToString
        '        Try
        '            rdblCheckType.SelectedValue = strType
        '        Catch ex As Exception
        '            rdblCheckType.SelectedIndex = 0
        '        End Try
        '    End If
        'End If
        If Session("MST_UsersMenuAccess_FS") IsNot Nothing Then
            If Session("MST_UsersMenuAccess_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("MST_UsersMenuAccess_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
            End If
        End If
        If Session("MST_UsersMenuAccess_PageInfo") IsNot Nothing Then
            If Session("MST_UsersMenuAccess_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("MST_UsersMenuAccess_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("MST_UsersMenuAccess_Search") IsNot Nothing Then
            If Session("MST_UsersMenuAccess_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("MST_UsersMenuAccess_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("MST_UsersMenuAccess_pageLength") IsNot Nothing Then
            If Session("MST_UsersMenuAccess_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("MST_UsersMenuAccess_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If
    End Sub

#End Region

#Region "Dropdownlist"
    Public Sub LoadDropdownlist()
        Call LoadModule(ddlsModule,
                        "S")
    End Sub
    Public Sub LoadModule(ByVal ddl As DropDownList,
                          ByVal strType As String)

        Dim bl As cUserMenuAccess = New cUserMenuAccess
        Try
            Dim lc As List(Of CoreModule) = bl.LoadModule(Me.WebCulture.ToUpper)
            ddl.Items.Clear()
            ddl.DataTextField = "ModuleNameEN"
            ddl.DataValueField = "ModuleID"
            ddl.DataSource = lc
            ddl.DataBind()
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            ElseIf strType = "S" Then
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadModule", ex)
        End Try
    End Sub
#End Region

#Region "RadioButton"
    'Public Sub LoadRadioButton()
    '    Call LoadType(rdblCheckType)
    '    rdblCheckType.SelectedIndex = 0
    'End Sub
    'Public Sub LoadType(ByVal rdbl As RadioButtonList)
    '    rdbl.Items.Insert(0, New ListItem(GetResource("MenuType", "Text", hddParameterMenuID.Value), "FRM"))
    '    rdbl.Items.Insert(1, New ListItem(GetResource("ReportType", "Text", hddParameterMenuID.Value), "RPT"))
    'End Sub
#End Region

#Region "Event Dropdownlist"
    'Protected Sub ddlsModule_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsModule.SelectedIndexChanged
    '    Try
    '        Call LoadRedirec("")
    '    Catch ex As Exception
    '        HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "ddlsModule_SelectedIndexChanged", ex)
    '    End Try
    'End Sub
#End Region

#Region "Event Radiobuttonlist"
    'Protected Sub rdblCheckType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdblCheckType.SelectedIndexChanged
    '    Try
    '        Call LoadRedirec("")
    '    Catch ex As Exception
    '        HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "rdblCheckType_SelectedIndexChanged", ex)
    '    End Try
    'End Sub
#End Region

#Region "Loaddata"
    Protected Sub DefaultAllData()
        Try
            Dim PagingDefault As Integer = 0
            Dim lcPaging As CoreWebResource = Me.GetResourceObject("grdView", "Paging", hddParameterMenuID.Value)
            If lcPaging IsNot Nothing Then
                PagingDefault = CInt(lcPaging.ResourceValueEN)
                hddpPagingDefault.Value = lcPaging.ResourceValueEN
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "DefaultAllData", ex)
        End Try
    End Sub
    Protected Sub LoadGridDefault()
        Try
            Dim SortColumn As Integer = 0
            Dim SortTypeDefault As String = String.Empty
            Dim CountColumn As Integer = 0
            Dim lcSort As CoreWebResource = Me.GetResourceObject("grdView", "Sort", hddParameterMenuID.Value)
            If lcSort IsNot Nothing Then
                SortColumn = CInt(lcSort.ResourceValueEN)
                SortTypeDefault = lcSort.ResourceValueLC
            End If

            hddpSortBy.Value = SortColumn
            hddpSortType.Value = SortTypeDefault.ToLower
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadGridDefault", ex)
        End Try
    End Sub
    Public Sub LoadInit()
        lblMain1.Text = Me.GetResource("main_label", "Text", "1")
        lblMain2.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
        lblMain3.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)

        lblsUserId.Text = Me.GetResource("lblsUserId", "Text", hddParameterMenuID.Value)
        lblsModule.Text = Me.GetResource("lblsModule", "Text", hddParameterMenuID.Value)
        'lblsTypeMenu.Text = Me.GetResource("lblsTypeMenu", "Text", hddParameterMenuID.Value)

        lblMassage1.Text = Me.GetResource("msg_required", "MSG")
        lblMassage2.Text = Me.GetResource("msg_required", "MSG")

        hddMSGSaveData.Value = Me.GetResource("msg_save_data", "MSG")
        hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG")
        hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG")
        hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG")
        hddMSGAddData.Value = Me.GetResource("msg_add_data", "MSG")

        btnMSGSaveData.Title = hddMSGSaveData.Value
        btnMSGCancelData.Title = hddMSGCancelData.Value

        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try
            hddReloadGrid.Value = "1"
            Dim bl As cUserMenuAccess = New cUserMenuAccess

            Dim strUser As String = String.Empty
            Call SetCiterail(txtsUserID.Text, strUser)

            If strUser <> String.Empty And
               ddlsModule.SelectedIndex <> 0 Then

                grdView.DataSource = Nothing
                grdView.DataBind()

                grdViewRPT.DataSource = Nothing
                grdViewRPT.DataBind()


                Dim lst As List(Of MenuAccess_ViewModel) = Nothing

                lst = bl.GetUserMenuAccess(ddlsModule.SelectedValue,
                                                      "FRM",
                                                      Me.CurrentUser.UserID)
                If lst IsNot Nothing Then
                    grdView.DataSource = lst
                    grdView.DataBind()
                End If


                Dim lstRPT As List(Of MenuAccess_ViewModel) = Nothing

                lstRPT = bl.GetUserMenuAccess(ddlsModule.SelectedValue,
                                                      "RPT",
                                                      Me.CurrentUser.UserID)
                If lstRPT IsNot Nothing Then
                    grdViewRPT.DataSource = lstRPT
                    grdViewRPT.DataBind()
                End If


            End If


            Dim lstMenuID = bl.GetCoreRoleMenuByDevisionAndPosition(strUser)
            Dim lstMenuIDAdd = bl.GetCoreRoleMenuByDevisionAndPositionGetPermission(strUser, "ADD")
            Dim lstMenuIDEdit = bl.GetCoreRoleMenuByDevisionAndPositionGetPermission(strUser, "EDIT")
            Dim lstMenuIDDelete = bl.GetCoreRoleMenuByDevisionAndPositionGetPermission(strUser, "DELETE")
            Dim lstMenuIDApprove = bl.GetCoreRoleMenuByDevisionAndPositionGetPermission(strUser, "APPROVE")
            Dim lstMenuIDPrint = bl.GetCoreRoleMenuByDevisionAndPositionGetPermission(strUser, "PRINT")

            Dim serializer As New JavaScriptSerializer
            hddSelectedMenu.Value = serializer.Serialize(lstMenuID)
            hddSelectedMenuAdd.Value = serializer.Serialize(lstMenuIDAdd)
            hddSelectedMenuEdit.Value = serializer.Serialize(lstMenuIDEdit)
            hddSelectedMenuDelete.Value = serializer.Serialize(lstMenuIDDelete)
            hddSelectedMenuApprove.Value = serializer.Serialize(lstMenuIDApprove)
            hddSelectedMenuPrint.Value = serializer.Serialize(lstMenuIDPrint)

            Session.Remove("MST_UsersMenuAccess_UserID")
            Session.Remove("MST_UsersMenuAccess_Module")
            Session.Remove("MST_UsersMenuAccess_Type")
            Session.Remove("MST_UsersMenuAccess_FS")
            Session.Remove("MST_UsersMenuAccess_PageInfo")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)
            Session.Remove("MST_UsersMenuAccess_UserID")
            Session.Remove("MST_UsersMenuAccess_Module")
            Session.Remove("MST_UsersMenuAccess_Type")
            Session.Remove("MST_UsersMenuAccess_FS")
            Session.Remove("MST_UsersMenuAccess_PageInfo")
        End Try
    End Sub
    Public Sub SetCiterail(ByVal pValue As String,
                           ByRef pBValue As String)
        Try
            If pValue <> String.Empty Then
                pBValue = pValue.Split(" -")(0)
            End If
        Catch ex As Exception
            pBValue = String.Empty
        End Try
    End Sub
#End Region

#Region "GirdView"
    Dim parentId As String = ""
    Protected Sub grdView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim chkMenu As CheckBox = DirectCast(e.Row.FindControl("chkMenu"), CheckBox)

                Dim chkAdd As CheckBox = DirectCast(e.Row.FindControl("chkAdd"), CheckBox)
                Dim chkEdit As CheckBox = DirectCast(e.Row.FindControl("chkEdit"), CheckBox)
                Dim chkDelete As CheckBox = DirectCast(e.Row.FindControl("chkDelete"), CheckBox)
                Dim chkApprove As CheckBox = DirectCast(e.Row.FindControl("chkApprove"), CheckBox)
                Dim chkPrint As CheckBox = DirectCast(e.Row.FindControl("chkPrint"), CheckBox)

                Dim hddMenuID As HiddenField = DirectCast(e.Row.FindControl("hddMenuID"), HiddenField)
                Dim menuID As Integer = Convert.ToInt32(hddMenuID.Value)
                Dim hddParentMenuID As HiddenField = DirectCast(e.Row.FindControl("hddParentMenuID"), HiddenField)
                Dim lblMenuName As Label = DirectCast(e.Row.FindControl("lblMenuName"), Label)
                Dim lblSubMenuName As Label = DirectCast(e.Row.FindControl("lblSubMenuName"), Label)
                'Dim lblSystemName As Label = DirectCast(e.Row.FindControl("lblSystemName"), Label)

                If WebCulture.ToLower = "th" Then
                    lblMenuName.Text = DataBinder.Eval(e.Row.DataItem, "MENUNAMELC")
                    lblSubMenuName.Text = DataBinder.Eval(e.Row.DataItem, "MENUNAMELC")
                    'lblSystemName.Text = DataBinder.Eval(e.Row.DataItem, "ModuleNameTH")
                ElseIf WebCulture.ToLower = "en" Then
                    lblMenuName.Text = DataBinder.Eval(e.Row.DataItem, "MENUNAMEEN")
                    lblSubMenuName.Text = DataBinder.Eval(e.Row.DataItem, "MENUNAMEEN")
                    'lblSystemName.Text = DataBinder.Eval(e.Row.DataItem, "ModuleNameEN")
                End If

                If hddParentMenuID.Value = "" Then
                    lblMenuName.Visible = True
                    lblSubMenuName.Visible = False
                Else
                    If hddParentMenuID.Value = parentId Then
                        lblMenuName.Visible = False
                        lblSubMenuName.Visible = True
                    Else
                        lblMenuName.Visible = True
                        lblSubMenuName.Visible = True
                        Dim bl As cUserMenuAccess = New cUserMenuAccess
                        Dim lc As CoreMenu = bl.GetWebMenuByID(hddParentMenuID.Value,
                                                               CurrentUser.UserID)
                        If WebCulture.ToLower = "th" Then
                            lblMenuName.Text = lc.MenuNameLC
                        ElseIf WebCulture.ToLower = "en" Then
                            lblMenuName.Text = lc.MenuNameEN
                        End If
                    End If
                End If
                parentId = hddParentMenuID.Value


                chkMenu.InputAttributes.Add("data-value", menuID)
                chkAdd.InputAttributes.Add("data-chkadd", menuID)
                chkEdit.InputAttributes.Add("data-chkedit", menuID)
                chkDelete.InputAttributes.Add("data-chkdelete", menuID)
                chkApprove.InputAttributes.Add("data-chkapprove", menuID)
                chkPrint.InputAttributes.Add("data-chkprint", menuID)


                'Dim strUser As String = String.Empty
                'Call SetCiterail(txtsUserID.Text, strUser)
                'If strUser <> String.Empty Then
                '    Dim usermenu As List(Of MenuAccess_ViewModel) = (New cUserMenuAccess).GetUserMenuAccess(strUser,
                '                                                                                            ddlsModule.SelectedValue,
                '                                                                                            rdblCheckType.SelectedValue,
                '                                                                                            CurrentUser.UserID)
                '    If usermenu IsNot Nothing Then
                '        If usermenu.Count > 0 Then
                '            Dim u As MenuAccess_ViewModel = Nothing
                '            u = usermenu.Where(Function(s) s.MenuID = menuID).SingleOrDefault()
                '            If u IsNot Nothing Then
                '                chkMenu.Checked = True
                '            End If
                '        End If
                '    End If
                'End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "grdView_RowDataBound", ex)
        End Try
    End Sub
    Protected Sub gridView_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles grdView.PreRender
        Try
            MergeRows(grdView)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "gridView_PreRender", ex)
        End Try
    End Sub


    Dim parentIdRPT As String = ""
    Protected Sub grdViewRPT_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdViewRPT.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim chkMenu As CheckBox = DirectCast(e.Row.FindControl("chkMenu"), CheckBox)
                Dim chkPrint As CheckBox = DirectCast(e.Row.FindControl("chkPrint"), CheckBox)

                Dim hddMenuID As HiddenField = DirectCast(e.Row.FindControl("hddMenuID"), HiddenField)
                Dim menuID As Integer = Convert.ToInt32(hddMenuID.Value)
                Dim hddParentMenuID As HiddenField = DirectCast(e.Row.FindControl("hddParentMenuID"), HiddenField)
                Dim lblMenuName As Label = DirectCast(e.Row.FindControl("lblMenuName"), Label)
                Dim lblSubMenuName As Label = DirectCast(e.Row.FindControl("lblSubMenuName"), Label)
                'Dim lblSystemName As Label = DirectCast(e.Row.FindControl("lblSystemName"), Label)

                If WebCulture.ToLower = "th" Then
                    lblMenuName.Text = DataBinder.Eval(e.Row.DataItem, "MENUNAMELC")
                    lblSubMenuName.Text = DataBinder.Eval(e.Row.DataItem, "MENUNAMELC")
                    'lblSystemName.Text = DataBinder.Eval(e.Row.DataItem, "ModuleNameTH")
                ElseIf WebCulture.ToLower = "en" Then
                    lblMenuName.Text = DataBinder.Eval(e.Row.DataItem, "MENUNAMEEN")
                    lblSubMenuName.Text = DataBinder.Eval(e.Row.DataItem, "MENUNAMEEN")
                    'lblSystemName.Text = DataBinder.Eval(e.Row.DataItem, "ModuleNameEN")
                End If

                If hddParentMenuID.Value = "" Then
                    lblMenuName.Visible = True
                    lblSubMenuName.Visible = False
                Else
                    If hddParentMenuID.Value = parentId Then
                        lblMenuName.Visible = False
                        lblSubMenuName.Visible = True
                    Else
                        lblMenuName.Visible = True
                        lblSubMenuName.Visible = True
                        Dim bl As cUserMenuAccess = New cUserMenuAccess
                        Dim lc As CoreMenu = bl.GetWebMenuByID(hddParentMenuID.Value,
                                                               CurrentUser.UserID)
                        If WebCulture.ToLower = "th" Then
                            lblMenuName.Text = lc.MenuNameLC
                        ElseIf WebCulture.ToLower = "en" Then
                            lblMenuName.Text = lc.MenuNameEN
                        End If
                    End If
                End If
                parentId = hddParentMenuID.Value

                chkMenu.InputAttributes.Add("data-value", menuID)
                chkPrint.InputAttributes.Add("data-chkprint", menuID)


                'Dim strUser As String = String.Empty
                'Call SetCiterail(txtsUserID.Text, strUser)
                'If strUser <> String.Empty Then
                '    Dim usermenu As List(Of MenuAccess_ViewModel) = (New cUserMenuAccess).GetUserMenuAccess(strUser,
                '                                                                                            ddlsModule.SelectedValue,
                '                                                                                            rdblCheckType.SelectedValue,
                '                                                                                            CurrentUser.UserID)
                '    If usermenu IsNot Nothing Then
                '        If usermenu.Count > 0 Then
                '            Dim u As MenuAccess_ViewModel = Nothing
                '            u = usermenu.Where(Function(s) s.MenuID = menuID).SingleOrDefault()
                '            If u IsNot Nothing Then
                '                chkMenu.Checked = True
                '            End If
                '        End If
                '    End If
                'End If

            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "grdView_RowDataBound", ex)
        End Try
    End Sub

    Protected Sub gridViewRPT_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles grdViewRPT.PreRender
        Try
            MergeRows(grdView)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "gridView_PreRender", ex)
        End Try
    End Sub

    Public Sub MergeRows(ByVal gridView As GridView)
        For rowIndex As Integer = gridView.Rows.Count - 2 To 0 Step -1
            Dim row As GridViewRow = gridView.Rows(rowIndex)
            Dim previousRow As GridViewRow = gridView.Rows(rowIndex + 1)
            'gridView.Rows(0).Cells(1).RowSpan = gridView.Rows.Count
            'row.VerticalAlign = VerticalAlign.Top
            'previousRow.Cells(1).Visible = False
            If CType(row.FindControl("hddParentMenuID"), HiddenField).Value = CType(previousRow.FindControl("hddParentMenuID"), HiddenField).Value And CType(row.FindControl("hddParentMenuID"), HiddenField).Value <> "" Then
                row.Cells(1).RowSpan = If(previousRow.Cells(1).RowSpan < 2, 2, previousRow.Cells(1).RowSpan + 1)
                previousRow.Cells(1).Visible = False
                row.VerticalAlign = VerticalAlign.Top
            End If
        Next
    End Sub
#End Region

#Region "Function"
    Protected Sub MenuChecked(ByRef lcUsermenu As List(Of MenuAccess_ViewModel))
        Try
            Dim strUser As String = String.Empty
            Call SetCiterail(txtsUserID.Text, strUser)
            Dim usermenu As List(Of MenuAccess_ViewModel) = (New cUserMenuAccess).GetUserMenuAccess(strUser,
                                                                                                    ddlsModule.SelectedValue,
                                                                                                    "FRM",
                                                                                                    CurrentUser.UserID)
            If usermenu IsNot Nothing Then
                Dim Tempparentid As String = String.Empty
                lcUsermenu = usermenu
                Dim checkDelete As Integer = 0
                Dim checkDup As Integer = 0
                For Each vRow As GridViewRow In Me.grdView.Rows
                    Dim chkMenu As CheckBox = DirectCast(vRow.FindControl("chkMenu"), CheckBox)
                    Dim hddMenuID As HiddenField = DirectCast(vRow.FindControl("hddMenuID"), HiddenField)
                    Dim hddParentMenuID As HiddenField = DirectCast(vRow.FindControl("hddParentMenuID"), HiddenField)
                    If chkMenu.Checked Then
                        If hddParentMenuID.Value <> String.Empty Then
                            If Tempparentid = String.Empty Then
                                Tempparentid = hddParentMenuID.Value
                            Else
                                Tempparentid = Tempparentid & "|" & hddParentMenuID.Value
                            End If
                        End If
                    End If
                Next
                For Each vRow As GridViewRow In Me.grdView.Rows
                    Dim chkMenu As CheckBox = DirectCast(vRow.FindControl("chkMenu"), CheckBox)
                    Dim hddMenuID As HiddenField = DirectCast(vRow.FindControl("hddMenuID"), HiddenField)
                    Dim hddParentMenuID As HiddenField = DirectCast(vRow.FindControl("hddParentMenuID"), HiddenField)

                    If Not chkMenu.Checked Then
                        For Each r In lcUsermenu
                            If r.MenuID = hddMenuID.Value Then
                                lcUsermenu.Remove(r)
                                Exit For
                            End If
                        Next
                        For Each r In lcUsermenu
                            Dim bool As Boolean = False
                            If hddParentMenuID.Value <> String.Empty Then
                                If hddParentMenuID.Value = "3" Then
                                    Dim sss As String = String.Empty
                                End If
                                If r.MenuID = hddParentMenuID.Value Then
                                    For Each a As String In Tempparentid.Split("|")
                                        If bool = False Then
                                            If hddParentMenuID.Value = a Then
                                                bool = True
                                            End If
                                        End If
                                    Next
                                    If bool = False Then
                                        lcUsermenu.Remove(r)
                                    End If
                                    Exit For
                                End If
                            End If
                        Next
                    Else
                        If lcUsermenu.Where(Function(s) s.MenuID = hddMenuID.Value).Count = 0 Then
                            Dim u As MenuAccess_ViewModel = New MenuAccess_ViewModel
                            u.EmployeeNo = strUser
                            u.MenuID = Convert.ToInt32(hddMenuID.Value)
                            lcUsermenu.Add(u)
                        End If
                        If hddParentMenuID.Value <> String.Empty Then
                            If checkDup <> hddParentMenuID.Value Then
                                If lcUsermenu.Where(Function(s) s.MenuID = hddParentMenuID.Value).Count = 0 Then
                                    Dim u As MenuAccess_ViewModel = New MenuAccess_ViewModel
                                    u.EmployeeNo = strUser
                                    u.MenuID = Convert.ToInt32(hddParentMenuID.Value)
                                    lcUsermenu.Add(u)
                                    checkDup = hddParentMenuID.Value
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "MenuChecked", ex)
        End Try
    End Sub
#End Region

#Region "Event"
    'Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
    '    Try
    '        Dim dt As New DataTable
    '        Dim bl As cUserMenuAccess = New cUserMenuAccess

    '        Dim strUser As String = String.Empty
    '        Call SetCiterail(txtsUserID.Text, strUser)

    '        Dim lcUserMenu As New List(Of MenuAccess_ViewModel)
    '        Call MenuChecked(lcUserMenu)

    '        If bl.Save(strUser,
    '                   lcUserMenu,
    '                   ddlsModule.SelectedValue,
    '                   Me.CurrentUser.UserID) Then
    '            Call LoadRedirec("1")
    '        Else
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
    '        End If

    '    Catch ex As Exception
    '        HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSave_Click", ex)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
    '    End Try
    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim dt As New DataTable
            Dim bl As cUserMenuAccess = New cUserMenuAccess
            Dim strUser As String = String.Empty
            Call SetCiterail(txtsUserID.Text, strUser)

            Dim lstCoreUserMenu As New List(Of CoreUsersMenu)
            Dim serializer As New JavaScriptSerializer
            Dim lstMenu = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenu.Value)

            Dim lstMenuAdd = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenuAdd.Value)
            Dim lstMenuEdit = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenuEdit.Value)
            Dim lstMenuDelete = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenuDelete.Value)
            Dim lstMenuApprove = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenuApprove.Value)
            Dim lstMenuPrint = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenuPrint.Value)

            For Each menuID As Integer In lstMenu

                Dim userMenu As New [CoreUsersMenu]() With {
                         .UserID = strUser,
                         .MenuID = menuID,
                         .isAdd = lstMenuAdd.Contains(menuID),
                         .isEdit = lstMenuEdit.Contains(menuID),
                         .isDelete = lstMenuDelete.Contains(menuID),
                         .isApprove = lstMenuApprove.Contains(menuID),
                         .isPrint = lstMenuPrint.Contains(menuID)
                         }


                lstCoreUserMenu.Add(userMenu)
            Next
            Dim allSelectedParentMenu As List(Of CoreMenu) = bl.getAllParentMenu(lstMenu)
            For Each parentMenu In allSelectedParentMenu
                Dim userMenu As New [CoreUsersMenu]() With {
                         .UserID = strUser,
                         .MenuID = parentMenu.MenuID
                        }
                lstCoreUserMenu.Add(userMenu)
            Next



            If bl.Save(strUser,
                       lstCoreUserMenu,
                       ddlsModule.SelectedValue) Then
                Call Loaddata()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
            End If

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "btnSave_Click", ex)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
        End Try
    End Sub


    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnCancel_Click", ex)
        End Try
    End Sub

    Protected Sub btnReloadGrid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReload.Click
        Try
            'Call LoadRedirec("")
            Call Loaddata()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReloadGrid_Click", ex)
        End Try
    End Sub
#End Region

#Region "Control Validate"
    Public Sub ControlValidate()
        lblMassage1.Style.Add("display", "none")
        lblMassage2.Style.Add("display", "none")
    End Sub
#End Region
End Class
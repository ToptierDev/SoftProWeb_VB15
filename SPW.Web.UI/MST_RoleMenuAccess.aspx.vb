
Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Web.Script.Serialization

Public Class MST_RoleMenuAccess
    Inherits BasePage

    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            Me.setMenuIDByMenuLocation("MST_RoleMenuAccess.aspx")
            Dim chkTemp2 As String = ddlsPosition.SelectedValue
            If Not IsPostBack Then
                Page.DataBind()
                'Me.WebCulture.ToUpper()
                'MenuID = HelperLog.LoadMenuID("MST_RoleMenuAccess.aspx")

                'ถ้าไม่กระทบหน้าอื่น ให้เอาออก
                Me.ClearSessionPageLoad("MST_RoleMenuAccess.aspx")
                Call LoadInit()

                Call LoadDropdownlist()
                Call Loaddata()

                HelperLog.AccessLog(Me.CurrentUser.UserID, Me.MenuID, Request.UserHostAddress())
            Else



                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();", True)
            End If






        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub



#Region "Dropdownlist"
    Public Sub LoadDropdownlist()
        Call LoadModule(ddlsModule,
                        "S")
        Call LoadDivision(ddlsDivision, "S")
        Call LoadPosition(ddlsPosition, "S")
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
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            ElseIf strType = "S" Then
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "LoadModule", ex)
        End Try
    End Sub


    Public Sub LoadDivision(ByVal ddl As DropDownList,
                          ByVal strType As String)

        Dim bl As cRoleMenuAccess = New cRoleMenuAccess
        Try
            Dim lc As List(Of CoreDivision) = bl.LoadDivision(Me.WebCulture.ToUpper)
            ddl.Items.Clear()
            ddl.DataTextField = "DivisionName"
            ddl.DataValueField = "DivisionCode"
            ddl.DataSource = lc
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "LoadDivision", ex)
        End Try
    End Sub

    Public Sub LoadPosition(ByVal ddl As DropDownList,
                          ByVal strType As String)

        Dim bl As cRoleMenuAccess = New cRoleMenuAccess
        Try
            Dim lc As List(Of CorePosition) = bl.LoadPosition(Me.WebCulture.ToUpper)
            ddl.Items.Clear()
            ddl.DataTextField = "PositionName"
            ddl.DataValueField = "PositionCode"
            ddl.DataSource = lc
            ddl.DataBind()
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            ElseIf strType = "S" Then
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "LoadPosition", ex)
        End Try
    End Sub

#End Region



#Region "Event Dropdownlist"
    'Protected Sub ddlsModule_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsModule.SelectedIndexChanged
    '    Try
    '        Call Loaddata()
    '    Catch ex As Exception
    '        HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "ddlsModule_SelectedIndexChanged", ex)
    '    End Try
    'End Sub
#End Region


#Region "Loaddata"


    Public Sub LoadInit()



    End Sub
    Public Sub Loaddata()
        Try
            Dim bl As cRoleMenuAccess = New cRoleMenuAccess


            Dim lst As List(Of MenuAccess_ViewModel) = Nothing
            If ddlsModule.SelectedIndex <> 0 Then
                grdView.DataSource = Nothing
                grdView.DataBind()

                lst = bl.GetRoleMenuAccessFRM(ddlsModule.SelectedValue)
                If lst IsNot Nothing Then
                    grdView.DataSource = lst
                    grdView.DataBind()
                End If
            End If

            Dim lstRPT As List(Of MenuAccess_ViewModel) = Nothing
            If ddlsModule.SelectedIndex <> 0 Then
                grdViewRPT.DataSource = Nothing
                grdViewRPT.DataBind()

                lstRPT = bl.GetRoleMenuAccessRPT(ddlsModule.SelectedValue)
                If lst IsNot Nothing Then
                    grdViewRPT.DataSource = lstRPT
                    grdViewRPT.DataBind()
                End If
            End If

            Dim lstMenuID = bl.GetCoreRoleMenuByDevisionAndPosition(ddlsDivision.SelectedValue, ddlsPosition.SelectedValue)
            Dim lstMenuIDAdd = bl.GetCoreRoleMenuByDevisionAndPositionGetPermission(ddlsDivision.SelectedValue, ddlsPosition.SelectedValue, "ADD")
            Dim lstMenuIDEdit = bl.GetCoreRoleMenuByDevisionAndPositionGetPermission(ddlsDivision.SelectedValue, ddlsPosition.SelectedValue, "EDIT")
            Dim lstMenuIDDelete = bl.GetCoreRoleMenuByDevisionAndPositionGetPermission(ddlsDivision.SelectedValue, ddlsPosition.SelectedValue, "DELETE")
            Dim lstMenuIDApprove = bl.GetCoreRoleMenuByDevisionAndPositionGetPermission(ddlsDivision.SelectedValue, ddlsPosition.SelectedValue, "APPROVE")
            Dim lstMenuIDPrint = bl.GetCoreRoleMenuByDevisionAndPositionGetPermission(ddlsDivision.SelectedValue, ddlsPosition.SelectedValue, "PRINT")

            Dim serializer As New JavaScriptSerializer
            hddSelectedMenu.Value = serializer.Serialize(lstMenuID)
            hddSelectedMenuAdd.Value = serializer.Serialize(lstMenuIDAdd)
            hddSelectedMenuEdit.Value = serializer.Serialize(lstMenuIDEdit)
            hddSelectedMenuDelete.Value = serializer.Serialize(lstMenuIDDelete)
            hddSelectedMenuApprove.Value = serializer.Serialize(lstMenuIDApprove)
            hddSelectedMenuPrint.Value = serializer.Serialize(lstMenuIDPrint)

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "Loaddata", ex)

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


            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "grdView_RowDataBound", ex)
        End Try
    End Sub

    Protected Sub gridView_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles grdView.PreRender
        Try
            MergeRows(grdView)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "gridView_PreRender", ex)
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
                    If hddParentMenuID.Value = parentIdRPT Then
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
                parentIdRPT = hddParentMenuID.Value
                chkMenu.InputAttributes.Add("data-value", menuID)
                chkPrint.InputAttributes.Add("data-chkprint", menuID)


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



#Region "Event"



    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim dt As New DataTable
            Dim bl As cRoleMenuAccess = New cRoleMenuAccess


            Dim lstCoreRoleMenu As New List(Of CoreRoleMenu)
            Dim serializer As New JavaScriptSerializer
            Dim lstMenu = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenu.Value)

            Dim lstMenuAdd = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenuAdd.Value)
            Dim lstMenuEdit = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenuEdit.Value)
            Dim lstMenuDelete = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenuDelete.Value)
            Dim lstMenuApprove = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenuApprove.Value)
            Dim lstMenuPrint = serializer.Deserialize(Of List(Of Integer))(hddSelectedMenuPrint.Value)

            For Each menuID As Integer In lstMenu
                Dim roleMenu As New [CoreRoleMenu]() With {
                        .DivisionCode = ddlsDivision.SelectedValue,
                         .PositionCode = ddlsPosition.SelectedValue,
                         .MenuID = menuID,
                         .UpdateBy = CurrentUser.UserID,
                         .UpdateDate = DateTime.Now,
                         .isAdd = lstMenuAdd.Contains(menuID),
                         .isEdit = lstMenuEdit.Contains(menuID),
                         .isDelete = lstMenuDelete.Contains(menuID),
                         .isApprove = lstMenuApprove.Contains(menuID),
                         .isPrint = lstMenuPrint.Contains(menuID)
                         }


                lstCoreRoleMenu.Add(roleMenu)
            Next

            Dim allSelectedParentMenu As List(Of CoreMenu) = bl.getAllParentMenu(lstMenu)
            For Each parentMenu In allSelectedParentMenu
                Dim roleMenu As New [CoreRoleMenu]() With {
                       .DivisionCode = ddlsDivision.SelectedValue,
                        .PositionCode = ddlsPosition.SelectedValue,
                        .MenuID = parentMenu.MenuID,
                        .UpdateBy = CurrentUser.UserID,
                        .UpdateDate = DateTime.Now
                        }
                lstCoreRoleMenu.Add(roleMenu)
            Next

            If bl.Save(lstCoreRoleMenu,
                       ddlsModule.SelectedValue,
                       ddlsDivision.SelectedValue,
                       ddlsPosition.SelectedValue) Then
                Call Loaddata()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
            End If

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "btnSave_Click", ex)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            ' Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "btnCancel_Click", ex)
        End Try
    End Sub

    Protected Sub btnReloadGrid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReload.Click
        Try
            Call Loaddata()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "btnReloadGrid_Click", ex)
        End Try
    End Sub
#End Region





End Class
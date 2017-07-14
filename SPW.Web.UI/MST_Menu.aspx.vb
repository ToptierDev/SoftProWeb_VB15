Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO

Public Class MST_Menu
    Inherits BasePage
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.DataBind()
                Session.Remove("IDOCS.application.LoaddataMenu")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("MST_Menu.aspx")
                Me.ClearSessionPageLoad("MST_Menu.aspx")
                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()
                Call LoadData()

                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())

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
        Call Redirect("MST_Menu.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("MST_Menu_Module")
        Session.Remove("MST_Menu_FS")
        Session.Add("MST_Menu_Module", IIf(ddlsModule.SelectedIndex <> 0, ddlsModule.SelectedValue, ""))
        Session.Add("MST_Menu_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("MST_Menu_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("MST_Menu_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("MST_Menu_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("MST_Menu_Module") IsNot Nothing Then
            If Session("MST_Menu_Module").ToString <> String.Empty Then
                Dim strModule As String = Session("MST_Menu_Module").ToString
                Try
                    ddlsModule.SelectedValue = strModule
                Catch ex As Exception
                    ddlsModule.SelectedIndex = 0
                End Try
            End If
        End If
        If Session("MST_Menu_FS") IsNot Nothing Then
            If Session("MST_Menu_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("MST_Menu_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
                Dim strPageInfo As String = Session("MST_Menu_PageInfo").ToString
                If strPageInfo <> String.Empty Then
                    hddpPageInfo.Value = strPageInfo
                End If
            End If
        End If
        If Session("MST_Menu_PageInfo") IsNot Nothing Then
            If Session("MST_Menu_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("MST_Menu_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("MST_Menu_Search") IsNot Nothing Then
            If Session("MST_Menu_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("MST_Menu_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("MST_Menu_pageLength") IsNot Nothing Then
            If Session("MST_Menu_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("MST_Menu_pageLength").ToString
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
        Call LoadModule(ddlaModule,
                        "A")
    End Sub
    Public Sub LoadModule(ByVal ddl As DropDownList,
                          ByVal strType As String)
        Dim bl As cMenu = New cMenu
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
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select_all", "MSG"), ""))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDepartment", ex)
        End Try
    End Sub
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
    Protected Sub LoadInit()
        Try
            lblMain1.Text = Me.GetResource("main_label", "Text", "1")
            lblMain2.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
            lblMain3.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
            lblMain4.Text = Me.GetResource("HeaderDialog", "Text", hddParameterMenuID.Value)
            lblsModule.Text = Me.GetResource("ModuleName", "Text", hddParameterMenuID.Value)

            lblaMenuID.Text = Me.GetResource("lblaMenuID", "Text", hddParameterMenuID.Value)
            lblaMenuNameLC.Text = Me.GetResource("lblaMenuNameLC", "Text", hddParameterMenuID.Value)
            lblaMenuNameEN.Text = Me.GetResource("lblaMenuNameEN", "Text", hddParameterMenuID.Value)
            lblaMenuLocation.Text = Me.GetResource("lblaMenuLocation", "Text", hddParameterMenuID.Value)
            lblaMenuType.Text = Me.GetResource("lblaMenuType", "Text", hddParameterMenuID.Value)
            lblaParentID.Text = Me.GetResource("lblaParentID", "Text", hddParameterMenuID.Value)
            lblaSequence.Text = Me.GetResource("lblaMenuSequence", "Text", hddParameterMenuID.Value)
            lblaStatus.Text = Me.GetResource("lblaStatus", "Text", hddParameterMenuID.Value)
            lblaModule.Text = Me.GetResource("ModuleName", "Text", hddParameterMenuID.Value)

            TextHd1.Text = Me.GetResource("No", "Text", "1")
            TextHd2.Text = Me.GetResource("MenuID", "Text", hddParameterMenuID.Value)
            TextHd3.Text = Me.GetResource("ModuleName", "Text", hddParameterMenuID.Value)
            TextHd4.Text = Me.GetResource("MenuNameLC", "Text", hddParameterMenuID.Value)
            TextHd5.Text = Me.GetResource("MenuNameEN", "Text", hddParameterMenuID.Value)
            TextHd6.Text = Me.GetResource("MenuLocation", "Text", hddParameterMenuID.Value)
            TextHd7.Text = Me.GetResource("MenuType", "Text", hddParameterMenuID.Value)
            TextHd8.Text = Me.GetResource("ParentID", "Text", hddParameterMenuID.Value)
            TextHd9.Text = Me.GetResource("MenuSequence", "Text", hddParameterMenuID.Value)
            TextHd10.Text = Me.GetResource("Status", "Text", hddParameterMenuID.Value)
            TextHd11.Text = Me.GetResource("col_edit", "Text", "1")
            TextHd12.Text = Me.GetResource("col_delete", "Text", "1")

            TextFt1.Text = Me.GetResource("No", "Text", "1")
            TextFt2.Text = Me.GetResource("MenuID", "Text", hddParameterMenuID.Value)
            TextFt3.Text = Me.GetResource("ModuleName", "Text", hddParameterMenuID.Value)
            TextFt4.Text = Me.GetResource("MenuNameLC", "Text", hddParameterMenuID.Value)
            TextFt5.Text = Me.GetResource("MenuNameEN", "Text", hddParameterMenuID.Value)
            TextFt6.Text = Me.GetResource("MenuLocation", "Text", hddParameterMenuID.Value)
            TextFt7.Text = Me.GetResource("MenuType", "Text", hddParameterMenuID.Value)
            TextFt8.Text = Me.GetResource("ParentID", "Text", hddParameterMenuID.Value)
            TextFt9.Text = Me.GetResource("MenuSequence", "Text", hddParameterMenuID.Value)
            TextFt10.Text = Me.GetResource("Status", "Text", hddParameterMenuID.Value)
            TextFt11.Text = Me.GetResource("col_edit", "Text", "1")
            TextFt12.Text = Me.GetResource("col_delete", "Text", "1")

            'lblsSave.Text = Me.GetResource("btn_save", "Text", "1")
            'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

            lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG")
            lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG") & " " & Me.GetResource("MenuID", "Text", hddParameterMenuID.Value)

            hddMSGSaveData.Value = Me.GetResource("msg_save_data", "MSG")
            hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG")
            hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG")
            hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG")
            hddMSGAddData.Value = Me.GetResource("msg_add_data", "MSG")

            btnMSGAddData.Title = hddMSGAddData.Value
            btnMSGSaveData.Title = hddMSGSaveData.Value
            btnMSGCancelData.Title = hddMSGCancelData.Value
            btnMSGDeleteData.InnerText = hddMSGDeleteData.Value
            btnMSGCancelDataS.InnerText = hddMSGCancelData.Value
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadInit", ex)
        End Try
    End Sub
    Protected Sub LoadData()
        Try
            hddReloadGrid.Value = "1"
            Dim fillter As FillterSearch = New FillterSearch
            Dim bl As cMenu = New cMenu
            fillter.Keyword = ""
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType

            Dim lst As List(Of Menu_ViewModel) = bl.Loaddata(fillter,
                                                             Me.TotalRow,
                                                             ddlsModule.SelectedValue,
                                                             Me.CurrentUser.UserID)

            If lst IsNot Nothing Then
                Call SetDataMenu(lst)
            Else
                Call SetDataMenu(Nothing)
            End If
            Session.Remove("MST_Menu_Module")
            Session.Remove("MST_Menu_FS")
            Session.Remove("MST_Menu_PageInfo")

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadData", ex)
            Session.Remove("MST_Menu_FS")
            Session.Remove("MST_Menu_Module")
            Session.Remove("MST_Menu_PageInfo")
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            hddReloadGrid.Value = String.Empty
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            hddKeyID.Value = String.Empty
            txtaMenuID.Text = String.Empty
            txtaMenuNameLC.Text = String.Empty
            txtaMenuNameEN.Text = String.Empty
            txtaMenuLocation.Text = String.Empty
            rbtaMenuType.SelectedIndex = 0
            txtaParentID.Text = String.Empty
            txtaSequence.Text = String.Empty
            ddlaModule.SelectedIndex = 0
            chkStatus.Checked = True

            txtaMenuID.Enabled = True

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try
            Dim bl As cMenu = New cMenu
            Dim lc As CoreMenu = bl.LoadEditMenu(hddKeyID.Value,
                                                 CurrentUser.UserID)
            If lc IsNot Nothing Then
                hddKeyID.Value = lc.MenuID
                txtaMenuID.Text = lc.MenuID
                txtaMenuNameLC.Text = lc.MenuNameLC
                txtaMenuNameEN.Text = lc.MenuNameEN
                txtaMenuLocation.Text = lc.MenuLocation
                rbtaMenuType.SelectedValue = lc.MenuType
                Try
                    ddlaModule.SelectedValue = lc.ModuleID
                Catch ex As Exception
                    ddlaModule.SelectedIndex = 0
                End Try
                If lc.ParentID IsNot Nothing Then
                    txtaParentID.Text = lc.ParentID
                Else
                    txtaParentID.Text = String.Empty
                End If
                If lc.Sequence IsNot Nothing Then
                    txtaSequence.Text = lc.Sequence
                Else
                    txtaSequence.Text = String.Empty
                End If

                chkStatus.Checked = Convert.ToBoolean(Convert.ToInt32(lc.EnableFlag))

                txtaMenuID.Enabled = False

                Call OpenDialog()
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim bl As cMenu = New cMenu
            Dim lc As CoreMenu = bl.LoadEditMenu(hddKeyID.Value,
                                                 Me.CurrentUser.UserID)
            If lc IsNot Nothing Then
                If Not bl.Delete(hddKeyID.Value,
                                 Me.CurrentUser.UserID) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_cannot_delete_because ", "MSG") & "');", True)
                Else
                    Call LoadRedirec("2")
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_deletenotsuccess", "MSG") & "');", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnDelete_Click", ex)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim message As String = ""
            Dim succ As Boolean = True
            Dim bl As cMenu = New cMenu
            Dim data As CoreMenu = Nothing
            If hddKeyID.Value <> "" Then
                If Not bl.Edit(hddKeyID.Value,
                               txtaMenuID.Text,
                               txtaMenuNameLC.Text,
                               txtaMenuNameEN.Text,
                               txtaMenuLocation.Text,
                               rbtaMenuType.SelectedValue,
                               txtaParentID.Text,
                               txtaSequence.Text,
                               Convert.ToInt32(chkStatus.Checked),
                               ddlaModule.SelectedValue,
                               Me.CurrentUser.UserID) Then
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                Else
                    Call LoadRedirec("1")
                End If

            Else
                Dim lc As CoreMenu = bl.LoadEditMenu(txtaMenuID.Text,
                                                          Me.CurrentUser.UserID())
                If lc Is Nothing Then
                    If Not bl.Add(hddKeyID.Value,
                          txtaMenuID.Text,
                          txtaMenuNameLC.Text,
                          txtaMenuNameEN.Text,
                          txtaMenuLocation.Text,
                          rbtaMenuType.SelectedValue,
                          txtaParentID.Text,
                          txtaSequence.Text,
                          Convert.ToInt32(chkStatus.Checked),
                          ddlaModule.SelectedValue,
                          Me.CurrentUser.UserID) Then
                        Call OpenDialog()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                    Else

                        Call LoadRedirec("1")
                    End If
                Else
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_duplicate", "MSG") & "');", True)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSave_Click", ex)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Call LoadRedirec("")
    End Sub
#End Region

#Region "ControlPanel"
    Public Sub OpenMain()
        pnMain.Visible = True
        pnDialog.Visible = False
    End Sub
    Public Sub OpenDialog()
        pnMain.Visible = False
        pnDialog.Visible = True
    End Sub
#End Region

#Region "Session"
    Public Function GetDataMenu() As List(Of Menu_ViewModel)
        Try
            Return Session("IDOCS.application.LoaddataMenu")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataMenu(ByVal lstMenu As List(Of Menu_ViewModel)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataMenu", lstMenu)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

End Class
Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class ORG_PhaseProject
    Inherits BasePage
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.DataBind()
                hddParameterMenuID.Value = HelperLog.LoadMenuID("ORG_PhaseProject.aspx")
                Me.ClearSessionPageLoad("ORG_PhaseProject.aspx")
                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()
                Call Loaddata()

                Dim strLG As String = String.Empty
                If Session("PRR.application.language") IsNot Nothing Then
                    strLG = Session("PRR.application.language")
                End If
                hddMasterLG.Value = strLG

                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub

#Region "Dropdownlist"
    Public Sub LoadDropdownlist()
        Try
            Call LoadProject(ddlsProject, "S")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDropdownlist", ex)
        End Try
    End Sub
    Public Sub LoadProject(ByVal ddl As DropDownList,
                          ByVal strType As String)
        Dim bl As cPermission = New cPermission
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadProject(CurrentUser.UserID)
            If lc IsNot Nothing Then
                ddl.DataValueField = "FREPRJNO"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "FREPRJNM"
                Else
                    ddl.DataTextField = "FREPRJNM"
                End If
                ddl.DataSource = lc
                ddl.DataBind()
            End If
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex.GetBaseException)
        End Try
    End Sub
#End Region

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        Call Redirect("ORG_PhaseProject.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("ORG_PhaseProject_FS")
        Session.Add("ORG_PhaseProject_Project", IIf(ddlsProject.SelectedIndex <> 0, ddlsProject.SelectedValue, ""))
        Session.Add("ORG_PhaseProject_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("ORG_PhaseProject_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("ORG_PhaseProject_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("ORG_PhaseProject_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("ORG_PhaseProject_Project") IsNot Nothing Then
            If Session("ORG_PhaseProject_Project").ToString <> String.Empty Then
                Dim strProject As String = Session("ORG_PhaseProject_Project").ToString
                Try
                    ddlsProject.SelectedValue = strProject
                Catch ex As Exception
                    ddlsProject.SelectedIndex = 0
                End Try
            End If
        End If
        If Session("ORG_PhaseProject_FS") IsNot Nothing Then
            If Session("ORG_PhaseProject_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("ORG_PhaseProject_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG", "1") & "');", True)
                End If
            End If
        End If
        If Session("ORG_PhaseProject_PageInfo") IsNot Nothing Then
            If Session("ORG_PhaseProject_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("ORG_PhaseProject_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("ORG_PhaseProject_Search") IsNot Nothing Then
            If Session("ORG_PhaseProject_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("ORG_PhaseProject_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("ORG_PhaseProject_pageLength") IsNot Nothing Then
            If Session("ORG_PhaseProject_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("ORG_PhaseProject_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If
    End Sub

#End Region

#Region "LoadData"
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

        lblsProject.Text = Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value)

        'lblsSave.Text = Me.GetResource("lblsSave", "Text", hddParameterMenuID.Value)
        'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

        lblMassage1.Text = Me.GetResource("msg_required", "MSG", "1")

        hddMSGSaveData.Value = Me.GetResource("msg_save_data", "MSG", "1")
        hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG", "1")
        hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG", "1")
        hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG", "1")
        hddMSGAddData.Value = Me.GetResource("msg_add_data", "MSG", "1")

        btnMSGSaveData.Title = hddMSGSaveData.Value
        btnMSGCancelData.Title = hddMSGCancelData.Value

        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try
            hddReloadGrid.Value = "1"

            Dim dt As New DataTable
            Dim fillter As FillterSearch = New FillterSearch
            Dim bl As cPhaseProject = New cPhaseProject

            fillter.Keyword = ""
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType

            If ddlsProject.SelectedIndex <> 0 Then
                Dim lst As List(Of PhaseProject_ViewModel) = bl.Loaddata(fillter,
                                                                        Me.TotalRow,
                                                                        ddlsProject.SelectedValue,
                                                                        Me.CurrentUser.UserID)
                Dim lcChk As List(Of String) = bl.GetEd03Phase(ddlsProject.SelectedValue)
                If lcChk IsNot Nothing Then
                    For Each m As String In lcChk
                        If hddChkPhaseDelete.Value = String.Empty Then
                            hddChkPhaseDelete.Value = m
                        Else
                            hddChkPhaseDelete.Value = hddChkPhaseDelete.Value & "," & m
                        End If
                    Next
                End If
                If lst IsNot Nothing Then
                    If lst.Count > 0 Then
                        Call SetDatatable(dt,
                                          lst)
                        grdView.DataSource = dt
                        grdView.DataBind()
                    Else
                        grdView.DataSource = Nothing
                        grdView.DataBind()
                        CallLoadGridView()
                    End If
                Else
                    grdView.DataSource = Nothing
                    grdView.DataBind()
                    CallLoadGridView()
                End If
            End If
            Session.Remove("ORG_PhaseProject_Project")
            Session.Remove("ORG_PhaseProject_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)

            Session.Remove("ORG_PhaseProject_Project")
            Session.Remove("ORG_PhaseProject_FS")
        End Try
    End Sub
    Public Sub SetCiterail(ByVal pValue As String,
                           ByRef pBValue As String)
        Try
            If pValue <> String.Empty Then
                pBValue = pValue.Split("-")(0)
            End If
        Catch ex As Exception
            pBValue = String.Empty
        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim dt As New DataTable
            Dim bl As cPhaseProject = New cPhaseProject

            Call GetDatatable(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Select("PhaseCode ='' And FlagSetAdd ='0'").Length <> 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_required", "MSG", "1") & Me.GetResource("gPhaseCode", "Text", hddParameterMenuID.Value) & "');", True)
                        Exit Sub
                    End If
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Select("PhaseCode ='" & dt.Rows(i)("PhaseCode").ToString & "' And FlagSetAdd ='0'").Length > 1 Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_duplicate", "MSG", "1") & "');", True)
                            Exit Sub
                        End If
                    Next
                End If
            End If
            If Not bl.PhaseProjectAdd(ddlsProject.SelectedValue,
                                      dt,
                                      Me.CurrentUser.UserID) Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
            Else
                Call LoadRedirec("1")
            End If

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSave_Click", ex)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Call LoadRedirec("")

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnCancel_Click", ex)
        End Try
    End Sub

    Protected Sub btnReloadGrid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReloadGrid.Click
        Call LoadRedirec("")
    End Sub

    Protected Sub btnGridAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGridAdd.Click
        Try
            Call CallLoadGridView()

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "FocusSet('" & hddpClientID.Value & "');", True)

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnGridAdd_Click", ex)
        End Try
    End Sub
#End Region

#Region "GridView"
    Protected Sub grdView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.Header
                    'e.Row.Cells(4).Visible = Me.GetPermission().isDelete
                Case DataControlRowType.DataRow
                    Dim btnDelete As ImageButton = e.Row.FindControl("btnDelete")
                    btnDelete.CommandArgument = DataBinder.Eval(e.Row.DataItem, "ID")
                    Dim hddID As HiddenField = CType(e.Row.FindControl("hddID"), HiddenField)
                    Dim txtgPhaseCode As TextBox = CType(e.Row.FindControl("txtgPhaseCode"), TextBox)
                    Dim txtgStartDate As TextBox = CType(e.Row.FindControl("txtgStartDate"), TextBox)
                    Dim txtgEndDate As TextBox = CType(e.Row.FindControl("txtgEndDate"), TextBox)
                    Dim txtgDesc As TextBox = CType(e.Row.FindControl("txtgDesc"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.Row.FindControl("hddFlagSetAdd"), HiddenField)
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "ID")) Then
                        If DataBinder.Eval(e.Row.DataItem, "ID").ToString <> String.Empty Then
                            hddID.Value = DataBinder.Eval(e.Row.DataItem, "ID")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PhaseCode")) Then
                        If DataBinder.Eval(e.Row.DataItem, "PhaseCode").ToString <> String.Empty Then
                            txtgPhaseCode.Text = DataBinder.Eval(e.Row.DataItem, "PhaseCode")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "StartDate")) Then
                        If DataBinder.Eval(e.Row.DataItem, "StartDate").ToString <> String.Empty Then
                            'Try
                            '    txtgStartDate.Text = CDate(DataBinder.Eval(e.Row.DataItem, "StartDate")).ToString("dd/MM/yyyy")
                            'Catch ex As Exception
                            '    txtgStartDate.Text = DataBinder.Eval(e.Row.DataItem, "StartDate")
                            'End Try
                            txtgStartDate.Text = ToStringByCulture(DataBinder.Eval(e.Row.DataItem, "StartDate"))
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "EndDate")) Then
                        If DataBinder.Eval(e.Row.DataItem, "EndDate").ToString <> String.Empty Then
                            'Try
                            '    txtgEndDate.Text = CDate(DataBinder.Eval(e.Row.DataItem, "EndDate")).ToString("dd/MM/yyyy")
                            'Catch ex As Exception
                            '    txtgEndDate.Text = DataBinder.Eval(e.Row.DataItem, "EndDate")
                            'End Try
                            txtgEndDate.Text = ToStringByCulture(DataBinder.Eval(e.Row.DataItem, "EndDate"))
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "Description")) Then
                        If DataBinder.Eval(e.Row.DataItem, "Description").ToString <> String.Empty Then
                            txtgDesc.Text = DataBinder.Eval(e.Row.DataItem, "Description")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FlagSetAdd").ToString <> String.Empty Then
                            hddFlagSetAdd.Value = DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")
                        End If
                    End If

                    If hddFlagSetAdd.Value = "1" Then
                        txtgPhaseCode.Attributes.Add("onclick", "addRowGridView('" & txtgPhaseCode.ClientID & "');")
                        txtgStartDate.Attributes.Add("onclick", "addRowGridView('" & txtgStartDate.ClientID & "');")
                        txtgEndDate.Attributes.Add("onclick", "addRowGridView('" & txtgEndDate.ClientID & "');")
                        txtgDesc.Attributes.Add("onclick", "addRowGridView('" & txtgDesc.ClientID & "');")
                        txtgStartDate.Attributes.Add("class", "form-control")
                        txtgEndDate.Attributes.Add("class", "form-control")
                        btnDelete.Visible = False
                    ElseIf hddFlagSetAdd.Value = "0" Then
                        txtgPhaseCode.Attributes.Remove("onclick")
                        txtgStartDate.Attributes.Remove("onclick")
                        txtgEndDate.Attributes.Remove("onclick")
                        txtgDesc.Attributes.Remove("onclick")
                        txtgStartDate.Attributes.Add("class", "datepicker form-control")
                        txtgEndDate.Attributes.Add("class", "datepicker form-control enddate")
                        btnDelete.Visible = True
                    End If

                    If hddChkPhaseDelete.Value <> String.Empty Then
                        For Each s As String In hddChkPhaseDelete.Value.Split(",")
                            If txtgPhaseCode.Text = s Then
                                btnDelete.Visible = False
                                Exit For
                            End If
                        Next
                    End If
                    'e.Row.Cells(4).Visible = Me.GetPermission().isDelete
            End Select
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, hddParameterMenuID.Value, "grdView2_RowDataBound", ex)
        End Try
    End Sub
    Private Sub grdView_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdView.RowCommand
        Try
            Select Case e.CommandName
                Case "btnDelete"
                    Dim dt As New DataTable
                    Call GetDatatable(dt)
                    If dt IsNot Nothing Then
                        For Each i As DataRow In dt.Select("ID = '" & e.CommandArgument & "'")
                            dt.Rows.Remove(i)
                        Next
                        If dt.Rows.Count > 0 Then
                            grdView.DataSource = dt
                            grdView.DataBind()
                        Else
                            grdView.DataSource = Nothing
                            grdView.DataBind()
                        End If
                    End If

            End Select
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "grdView2_RowCommand", ex)
        End Try
    End Sub
#End Region

#Region "Datatable"
    Public Sub CallLoadGridView()
        Dim dt As New DataTable
        Call AddDataTable(dt)

        grdView.DataSource = dt
        grdView.DataBind()
    End Sub
    Public Sub CreateDatatable(ByRef dt As DataTable)
        dt.Columns.Add("ID")
        dt.Columns.Add("PhaseCode")
        dt.Columns.Add("StartDate")
        dt.Columns.Add("EndDate")
        dt.Columns.Add("Description")
        dt.Columns.Add("FlagSetAdd")
    End Sub
    Public Sub AddDataTable(ByRef dt As DataTable)
        Call CreateDatatable(dt)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            If grdView IsNot Nothing Then
                For Each e As GridViewRow In grdView.Rows
                    Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                    Dim txtgPhaseCode As TextBox = CType(e.FindControl("txtgPhaseCode"), TextBox)
                    Dim txtgStartDate As TextBox = CType(e.FindControl("txtgStartDate"), TextBox)
                    Dim txtgEndDate As TextBox = CType(e.FindControl("txtgEndDate"), TextBox)
                    Dim txtgDesc As TextBox = CType(e.FindControl("txtgDesc"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)

                    dr = dt.NewRow
                    strID = CInt(hddID.Value)
                    dr.Item("ID") = hddID.Value
                    dr.Item("PhaseCode") = txtgPhaseCode.Text
                    If txtgStartDate.Text <> String.Empty Then
                        dr.Item("StartDate") = ToSystemDate(txtgStartDate.Text)
                    End If
                    If txtgEndDate.Text <> String.Empty Then
                        dr.Item("EndDate") = ToSystemDate(txtgEndDate.Text)
                    End If
                    dr.Item("Description") = txtgDesc.Text
                    hddFlagSetAdd.Value = "0"
                    dr.Item("FlagSetAdd") = "0"
                    dt.Rows.Add(dr)
                    If hddFlagSetAdd.Value = "1" Then
                        dt.Rows.Remove(dr)
                    End If
                Next
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("PhaseCode") = String.Empty
                dr.Item("StartDate") = String.Empty
                dr.Item("EndDate") = String.Empty
                dr.Item("Description") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dt.Rows.Add(dr)
            Else
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("PhaseCode") = String.Empty
                dr.Item("StartDate") = String.Empty
                dr.Item("EndDate") = String.Empty
                dr.Item("Description") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dt.Rows.Add(dr)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "AddDataTable", ex)
        End Try
    End Sub
    Public Sub GetDatatable(ByRef dt As DataTable)
        Call CreateDatatable(dt)
        Try
            Dim dr As DataRow
            For Each e As GridViewRow In grdView.Rows
                Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                Dim txtgPhaseCode As TextBox = CType(e.FindControl("txtgPhaseCode"), TextBox)
                Dim txtgStartDate As TextBox = CType(e.FindControl("txtgStartDate"), TextBox)
                Dim txtgEndDate As TextBox = CType(e.FindControl("txtgEndDate"), TextBox)
                Dim txtgDesc As TextBox = CType(e.FindControl("txtgDesc"), TextBox)
                Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                dr = dt.NewRow
                dr.Item("ID") = hddID.Value
                dr.Item("PhaseCode") = txtgPhaseCode.Text
                dr.Item("StartDate") = ToSystemDate(txtgStartDate.Text)
                dr.Item("EndDate") = ToSystemDate(txtgEndDate.Text)
                dr.Item("Description") = txtgDesc.Text
                dr.Item("FlagSetAdd") = hddFlagSetAdd.Value
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
    Public Sub SetDatatable(ByRef dt As DataTable,
                            ByVal lst As List(Of PhaseProject_ViewModel))
        Call CreateDatatable(dt)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            For Each m As PhaseProject_ViewModel In lst
                dr = dt.NewRow
                strID = strID + 1
                dr.Item("ID") = strID
                dr.Item("PhaseCode") = m.PhaseCode
                dr.Item("StartDate") = m.StartDate
                dr.Item("EndDate") = m.EndDate
                dr.Item("Description") = m.Description
                dr.Item("FlagSetAdd") = "0"
                dt.Rows.Add(dr)
            Next
            dr = dt.NewRow
            dr.Item("ID") = strID + 1
            dr.Item("PhaseCode") = String.Empty
            dr.Item("StartDate") = String.Empty
            dr.Item("EndDate") = String.Empty
            dr.Item("FlagSetAdd") = "1"
            dt.Rows.Add(dr)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
#End Region

#Region "Control Validate"
    Public Sub ControlValidate()
        lblMassage1.Style.Add("display", "none")
    End Sub
#End Region
End Class
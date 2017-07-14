Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class MST_Department
    Inherits BasePage
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Page.DataBind()
                Session.Remove("IDOCS.application.LoaddataDept")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("MST_Department.aspx")
                Me.ClearSessionPageLoad("MST_Department.aspx")
                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call GetParameter()
                Call Loaddata()

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
        Call Redirect("MST_Department.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("MST_Department_FS")
        Session.Add("MST_Department_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("MST_Department_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("MST_Department_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("MST_Department_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("MST_Department_FS") IsNot Nothing Then
            If Session("MST_Department_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("MST_Department_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
            End If
        End If
        If Session("MST_Department_PageInfo") IsNot Nothing Then
            If Session("MST_Department_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("MST_Department_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("MST_Department_Search") IsNot Nothing Then
            If Session("MST_Department_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("MST_Department_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("MST_Department_pageLength") IsNot Nothing Then
            If Session("MST_Department_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("MST_Department_pageLength").ToString
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
        lblMain4.Text = Me.GetResource("HeaderDialog", "Text", hddParameterMenuID.Value)

        lblaDepartmentCode.Text = Me.GetResource("lblaDepartmentCode", "Text", hddParameterMenuID.Value)
        lblaDepartmentName.Text = Me.GetResource("lblaDepartmentName", "Text", hddParameterMenuID.Value)
        lblaMG.Text = Me.GetResource("lblaMG", "Text", hddParameterMenuID.Value)

        TextHd1.Text = Me.GetResource("No", "Text", "1")
        TextHd2.Text = Me.GetResource("DepartmentCode", "Text", hddParameterMenuID.Value)
        TextHd3.Text = Me.GetResource("DepartmentName", "Text", hddParameterMenuID.Value)
        TextHd4.Text = Me.GetResource("col_edit", "Text", "1")
        TextHd5.Text = Me.GetResource("col_delete", "Text", "1")

        TextFt1.Text = Me.GetResource("No", "Text", "1")
        TextFt2.Text = Me.GetResource("DepartmentCode", "Text", hddParameterMenuID.Value)
        TextFt3.Text = Me.GetResource("DepartmentName", "Text", hddParameterMenuID.Value)
        TextFt4.Text = Me.GetResource("col_edit", "Text", "1")
        TextFt5.Text = Me.GetResource("col_delete", "Text", "1")

        'lblsSave.Text = Me.GetResource("btn_save", "Text", "1")
        'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

        lblMassage1.Text = Me.GetResource("msg_required", "MSG")
        lblMassage2.Text = Me.GetResource("msg_required", "MSG")

        lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG")
        lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG") & " " & Me.GetResource("lblaDepartmentCode", "Text", hddParameterMenuID.Value)

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

        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try
            hddReloadGrid.Value = "1"

            Dim fillter As FillterSearch = New FillterSearch
            Dim bl As cDepartment = New cDepartment
            fillter.Keyword = ""
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType

            Dim lst As List(Of BD11DEPT) = bl.LoaddataDepartment(fillter,
                                                                 Me.TotalRow,
                                                                 Me.CurrentUser.UserID)
            If lst IsNot Nothing Then
                Call SetDataDept(lst)
            Else
                Call SetDataDept(Nothing)
            End If
            Session.Remove("MST_Department_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)
            Session.Remove("MST_Department_FS")
        End Try
    End Sub
#End Region

#Region "Event"

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            hddKeyID.Value = ""
            txtaDepartmentCode.Text = ""
            txtaDepartmentName.Text = ""

            txtaDepartmentCode.Enabled = True

            Call CallLoadGridView()

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try
            Dim code As String = Convert.ToString(hddKeyID.Value)
            Dim bl As cDepartment = New cDepartment
            Dim lc As BD11DEPT = bl.GetDepartmentByID(code,
                                                      Me.CurrentUser.UserID)
            If lc IsNot Nothing Then
                hddKeyID.Value = lc.FDPCODE
                txtaDepartmentCode.Text = lc.FDPCODE
                txtaDepartmentName.Text = lc.FDPNAME
                txtaMG.Text = lc.FDPMG

                txtaDepartmentCode.Enabled = False

                Dim lsts As List(Of BD12SECT) = bl.LoadSection(lc.FDPCODE)
                Dim dt As New DataTable
                If lsts IsNot Nothing Then
                    If lsts.Count > 0 Then
                        Call CreateDatatable(dt)
                        Call SetDatatable(dt,
                                          lsts)
                        grdView2.DataSource = dt
                        grdView2.DataBind()
                    End If
                End If
                If lsts.Count = 0 Then
                    Call CallLoadGridView()
                End If
            End If

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim code As String = Convert.ToString(hddKeyID.Value)
            Dim bl As cDepartment = New cDepartment
            Dim data As BD11DEPT = bl.GetDepartmentByID(code,
                                                        Me.CurrentUser.UserID)
            If data IsNot Nothing Then
                If Not bl.DepartmentDelete(code,
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
            Dim dt As New DataTable
            Dim succ As Boolean = True
            Dim bl As cDepartment = New cDepartment
            Dim data As BD11DEPT = Nothing
            If hddKeyID.Value <> "" Then
                Call CreateDatatable(dt)
                Call GetDatatable(dt)
                If Not bl.DepartmentsEdit(hddKeyID.Value,
                                          txtaDepartmentName.Text,
                                          txtaMG.Text,
                                          dt,
                                          Me.CurrentUser.UserID) Then
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                Else
                    Call LoadRedirec("1")
                End If
            Else

                Dim lc As BD11DEPT = bl.GetDepartmentByID(txtaDepartmentCode.Text,
                                                          Me.CurrentUser.UserID)
                If lc Is Nothing Then
                    Call CreateDatatable(dt)
                    Call GetDatatable(dt)
                    If Not bl.DepartmentAdd(txtaDepartmentCode.Text,
                                            txtaDepartmentName.Text,
                                            txtaMG.Text,
                                            dt,
                                            Me.CurrentUser.UserID) Then

                        Call OpenDialog()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                    Else
                        Call LoadRedirec("1")
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_duplicate", "MSG") & "');", True)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSave_Click", ex)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnCencel_Click", ex)
        End Try
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

#Region "Control Validate"
    Public Sub ControlValidate()
        lblMassage1.Style.Add("display", "none")
        lblMassage2.Style.Add("display", "none")
    End Sub
#End Region

#Region "Session"
    Public Function GetDataDept() As List(Of BD11DEPT)
        Try
            Return Session("IDOCS.application.LoaddataDept")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataDept(ByVal lcDept As List(Of BD11DEPT)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataDept", lcDept)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "GridView2"
    Protected Sub grdView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView2.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    Dim btnDelete As ImageButton = e.Row.FindControl("btnDelete")
                    btnDelete.CommandArgument = DataBinder.Eval(e.Row.DataItem, "ID")
                    Dim hddID As HiddenField = CType(e.Row.FindControl("hddID"), HiddenField)
                    Dim txtgSectionCode As TextBox = CType(e.Row.FindControl("txtgSectionCode"), TextBox)
                    Dim txtgName As TextBox = CType(e.Row.FindControl("txtgName"), TextBox)
                    Dim txtgManager As TextBox = CType(e.Row.FindControl("txtgManager"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.Row.FindControl("hddFlagSetAdd"), HiddenField)
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "ID")) Then
                        If DataBinder.Eval(e.Row.DataItem, "ID").ToString <> String.Empty Then
                            hddID.Value = DataBinder.Eval(e.Row.DataItem, "ID")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FSECCODE")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FSECCODE").ToString <> String.Empty Then
                            txtgSectionCode.Text = DataBinder.Eval(e.Row.DataItem, "FSECCODE")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FSECNAME")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FSECNAME").ToString <> String.Empty Then
                            txtgName.Text = DataBinder.Eval(e.Row.DataItem, "FSECNAME")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FSECMG")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FSECMG").ToString <> String.Empty Then
                            txtgManager.Text = DataBinder.Eval(e.Row.DataItem, "FSECMG")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FlagSetAdd").ToString <> String.Empty Then
                            hddFlagSetAdd.Value = DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")
                        End If
                    End If
                    If hddFlagSetAdd.Value = "1" Then
                        txtgSectionCode.Attributes.Add("onclick", "addRowGridView('" & txtgSectionCode.ClientID & "');")
                        txtgName.Attributes.Add("onclick", "addRowGridView('" & txtgName.ClientID & "');")
                        txtgManager.Attributes.Add("onclick", "addRowGridView('" & txtgManager.ClientID & "');")
                        btnDelete.Visible = False
                    ElseIf hddFlagSetAdd.Value = "0" Then
                        txtgSectionCode.Attributes.Remove("onclick")
                        txtgName.Attributes.Remove("onclick")
                        txtgManager.Attributes.Remove("onclick")
                        btnDelete.Visible = True
                    End If
            End Select
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, hddParameterMenuID.Value, "grdView2_RowDataBound", ex)
        End Try
    End Sub
    Private Sub grdView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdView2.RowCommand
        Try
            Select Case e.CommandName
                Case "btnDelete"
                    Dim dt As New DataTable
                    Call CreateDatatable(dt)
                    Call GetDatatable(dt)
                    If dt IsNot Nothing Then
                        For Each i As DataRow In dt.Select("ID= '" & e.CommandArgument & "'")
                            dt.Rows.Remove(i)
                        Next
                        If dt.Rows.Count > 0 Then
                            grdView2.DataSource = dt
                            grdView2.DataBind()
                        Else
                            grdView2.DataSource = Nothing
                            grdView2.DataBind()
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
        Call CreateDatatable(dt)
        Call AddDataTable(dt)

        grdView2.DataSource = dt
        grdView2.DataBind()
    End Sub
    Public Sub CreateDatatable(ByRef dt As DataTable)
        dt.Columns.Add("ID")
        dt.Columns.Add("FSECCODE")
        dt.Columns.Add("FSECNAME")
        dt.Columns.Add("FSECMG")
        dt.Columns.Add("FlagSetAdd")
    End Sub
    Public Sub AddDataTable(ByRef dt As DataTable)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            If grdView2 IsNot Nothing Then
                For Each e As GridViewRow In grdView2.Rows
                    Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                    Dim txtgSectionCode As TextBox = CType(e.FindControl("txtgSectionCode"), TextBox)
                    Dim txtgName As TextBox = CType(e.FindControl("txtgName"), TextBox)
                    Dim txtgManager As TextBox = CType(e.FindControl("txtgManager"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)

                    dr = dt.NewRow
                    strID = CInt(hddID.Value)
                    dr.Item("ID") = hddID.Value
                    dr.Item("FSECCODE") = txtgSectionCode.Text
                    dr.Item("FSECNAME") = txtgName.Text
                    dr.Item("FSECMG") = txtgManager.Text

                    hddFlagSetAdd.Value = "0"
                    dr.Item("FlagSetAdd") = "0"
                    dt.Rows.Add(dr)

                    If hddFlagSetAdd.Value = "1" Then
                        dt.Rows.Remove(dr)
                    End If
                Next
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("FSECCODE") = String.Empty
                dr.Item("FSECNAME") = String.Empty
                dr.Item("FSECMG") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dt.Rows.Add(dr)
            Else
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("FSECCODE") = String.Empty
                dr.Item("FSECNAME") = String.Empty
                dr.Item("FSECMG") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dt.Rows.Add(dr)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "AddDataTable", ex)
        End Try
    End Sub
    Public Sub GetDatatable(ByRef dt As DataTable)
        Try
            Dim dr As DataRow
            For Each e As GridViewRow In grdView2.Rows
                Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                Dim txtgSectionCode As TextBox = CType(e.FindControl("txtgSectionCode"), TextBox)
                Dim txtgName As TextBox = CType(e.FindControl("txtgName"), TextBox)
                Dim txtgManager As TextBox = CType(e.FindControl("txtgManager"), TextBox)
                Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                dr = dt.NewRow
                dr.Item("ID") = hddID.Value
                dr.Item("FSECCODE") = txtgSectionCode.Text
                dr.Item("FSECNAME") = txtgName.Text
                dr.Item("FSECMG") = txtgManager.Text
                dr.Item("FlagSetAdd") = hddFlagSetAdd.Value
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
    Public Sub SetDatatable(ByRef dt As DataTable,
                            ByVal lst As List(Of BD12SECT))
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            For Each m As BD12SECT In lst
                dr = dt.NewRow
                strID = strID + 1
                dr.Item("ID") = strID
                dr.Item("FSECCODE") = m.FSECCODE
                dr.Item("FSECNAME") = m.FSECNAME
                dr.Item("FSECMG") = m.FSECMG
                dr.Item("FlagSetAdd") = "0"
                dt.Rows.Add(dr)
            Next
            dr = dt.NewRow
            dr.Item("ID") = strID + 1
            dr.Item("FSECCODE") = String.Empty
            dr.Item("FSECNAME") = String.Empty
            dr.Item("FSECMG") = String.Empty
            dr.Item("FlagSetAdd") = "1"
            dt.Rows.Add(dr)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
#End Region

End Class
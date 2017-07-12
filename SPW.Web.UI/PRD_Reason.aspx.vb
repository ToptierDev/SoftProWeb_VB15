Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class PRD_Reason
    Inherits BasePage
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.DataBind()
                hddParameterMenuID.Value = HelperLog.LoadMenuID("PRD_Reason.aspx")
                Me.ClearSessionPageLoad("PRD_Reason.aspx")
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
            Call LoadReasonGroup(ddlsGroupReason, "S")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDropdownlist", ex)
        End Try
    End Sub
    Public Sub LoadReasonGroup(ByVal ddl As DropDownList,
                               ByVal strType As String)
        Try
            ddl.Items.Clear()
            If strType = "S" Then
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select_all", "MSG", "1"), ""))
            End If
            Dim Temp1 As String = Me.GetResource("resProblem", "Text", hddParameterMenuID.Value)
            Dim Temp2 As String = Me.GetResource("resSolving", "Text", hddParameterMenuID.Value)
            Dim Temp3 As String = Me.GetResource("resNotFinish", "Text", hddParameterMenuID.Value)
            Dim Temp4 As String = Me.GetResource("res4", "Text", hddParameterMenuID.Value)
            Dim Temp5 As String = Me.GetResource("res5", "Text", hddParameterMenuID.Value)
            Dim Temp6 As String = Me.GetResource("res6", "Text", hddParameterMenuID.Value)
            Dim Temp7 As String = Me.GetResource("resOtherReason", "Text", hddParameterMenuID.Value)
            Dim Temp8 As String = Me.GetResource("resCancelReserv", "Text", hddParameterMenuID.Value)
            Dim Temp9 As String = Me.GetResource("resSalesContactor", "Text", hddParameterMenuID.Value)
            ddl.Items.Insert(1, New ListItem(Temp1, Temp1.Split("-")(0)))
            ddl.Items.Insert(2, New ListItem(Temp2, Temp2.Split("-")(0)))
            ddl.Items.Insert(3, New ListItem(Temp3, Temp3.Split("-")(0)))
            ddl.Items.Insert(4, New ListItem(Temp4, Temp4.Split("-")(0)))
            ddl.Items.Insert(5, New ListItem(Temp5, Temp5.Split("-")(0)))
            ddl.Items.Insert(6, New ListItem(Temp6, Temp6.Split("-")(0)))
            ddl.Items.Insert(7, New ListItem(Temp7, Temp7.Split("-")(0)))
            ddl.Items.Insert(8, New ListItem(Temp8, Temp8.Split("-")(0)))
            ddl.Items.Insert(9, New ListItem(Temp9, Temp9.Split("-")(0)))

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex.GetBaseException)
        End Try
    End Sub
#End Region

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        Call Redirect("PRD_Reason.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("PRD_Reason_FS")
        Session.Add("PRD_Reason_ReasonGroup", IIf(ddlsGroupReason.SelectedIndex <> 0, ddlsGroupReason.SelectedValue, ""))
        Session.Add("PRD_Reason_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("PRD_Reason_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("PRD_Reason_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("PRD_Reason_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("PRD_Reason_ReasonGroup") IsNot Nothing Then
            If Session("PRD_Reason_ReasonGroup").ToString <> String.Empty Then
                Dim strReasonGroup As String = Session("PRD_Reason_ReasonGroup").ToString
                Try
                    ddlsGroupReason.SelectedValue = strReasonGroup
                Catch ex As Exception
                    ddlsGroupReason.SelectedIndex = 0
                End Try
            End If
        End If
        If Session("PRD_Reason_FS") IsNot Nothing Then
            If Session("PRD_Reason_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("PRD_Reason_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG", "1") & "');", True)
                End If
            End If
        End If
        If Session("PRD_Reason_PageInfo") IsNot Nothing Then
            If Session("PRD_Reason_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("PRD_Reason_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("PRD_Reason_Search") IsNot Nothing Then
            If Session("PRD_Reason_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("PRD_Reason_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("PRD_Reason_pageLength") IsNot Nothing Then
            If Session("PRD_Reason_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("PRD_Reason_pageLength").ToString
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

        lblsGroupReason.Text = Me.GetResource("lblsGroupReason", "Text", hddParameterMenuID.Value)

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
            Dim bl As cReason = New cReason

            fillter.Keyword = ""
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType

            If ddlsGroupReason.SelectedIndex <> 0 Then
                Dim lst As List(Of KD05RSCD) = bl.Loaddata(fillter,
                                                           Me.TotalRow,
                                                           ddlsGroupReason.SelectedValue,
                                                           Me.CurrentUser.UserID)
                Dim lcChk As List(Of String) = bl.GetKD05RSCD(ddlsGroupReason.SelectedValue)
                If lcChk IsNot Nothing Then
                    For Each m As String In lcChk
                        If hddChkReasonDelete.Value = String.Empty Then
                            hddChkReasonDelete.Value = m
                        Else
                            hddChkReasonDelete.Value = hddChkReasonDelete.Value & "," & m
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
            Session.Remove("PRD_Reason_Project")
            Session.Remove("PRD_Reason_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)

            Session.Remove("PRD_Reason_Project")
            Session.Remove("PRD_Reason_FS")
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
            Dim bl As cReason = New cReason

            Call GetDatatable(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Select("FRSCODE ='' And FlagSetAdd ='0'").Length <> 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_required", "MSG", "1") & Me.GetResource("gReasonCode", "Text", hddParameterMenuID.Value) & "');", True)
                        Exit Sub
                    End If
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Select("FRSCODE ='" & dt.Rows(i)("FRSCODE").ToString & "' And FlagSetAdd ='0'").Length > 1 Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_duplicate", "MSG", "1") & "');", True)
                            Exit Sub
                        End If
                    Next
                End If
            End If
            If Not bl.ReasonAdd(ddlsGroupReason.SelectedValue,
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

                Case DataControlRowType.DataRow
                    Dim btnDelete As ImageButton = e.Row.FindControl("btnDelete")
                    btnDelete.CommandArgument = DataBinder.Eval(e.Row.DataItem, "ID")
                    Dim hddID As HiddenField = CType(e.Row.FindControl("hddID"), HiddenField)
                    Dim txtgReasonCode As TextBox = CType(e.Row.FindControl("txtgReasonCode"), TextBox)
                    Dim txtgReasonDescription As TextBox = CType(e.Row.FindControl("txtgReasonDescription"), TextBox)
                    Dim txtgRemark As TextBox = CType(e.Row.FindControl("txtgRemark"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.Row.FindControl("hddFlagSetAdd"), HiddenField)
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "ID")) Then
                        If DataBinder.Eval(e.Row.DataItem, "ID").ToString <> String.Empty Then
                            hddID.Value = DataBinder.Eval(e.Row.DataItem, "ID")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FRSCODE")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FRSCODE").ToString <> String.Empty Then
                            txtgReasonCode.Text = DataBinder.Eval(e.Row.DataItem, "FRSCODE")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FRSDESC")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FRSDESC").ToString <> String.Empty Then
                            txtgReasonDescription.Text = DataBinder.Eval(e.Row.DataItem, "FRSDESC")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FSOLVMETH")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FSOLVMETH").ToString <> String.Empty Then
                            txtgRemark.Text = DataBinder.Eval(e.Row.DataItem, "FSOLVMETH")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FlagSetAdd").ToString <> String.Empty Then
                            hddFlagSetAdd.Value = DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")
                        End If
                    End If

                    If hddFlagSetAdd.Value = "1" Then
                        txtgReasonCode.Attributes.Add("onclick", "addRowGridView('" & txtgReasonCode.ClientID & "');")
                        txtgReasonDescription.Attributes.Add("onclick", "addRowGridView('" & txtgReasonDescription.ClientID & "');")
                        txtgRemark.Attributes.Add("onclick", "addRowGridView('" & txtgRemark.ClientID & "');")
                        btnDelete.Visible = False
                    ElseIf hddFlagSetAdd.Value = "0" Then
                        txtgReasonCode.Attributes.Remove("onclick")
                        txtgReasonCode.Attributes.Remove("onclick")
                        txtgRemark.Attributes.Remove("onclick")
                        btnDelete.Visible = True
                    End If

                    If hddChkReasonDelete.Value <> String.Empty Then
                        For Each s As String In hddChkReasonDelete.Value.Split(",")
                            If txtgReasonCode.Text = s Then
                                btnDelete.Visible = False
                                Exit For
                            End If
                        Next
                    End If
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
        dt.Columns.Add("FRSCODE")
        dt.Columns.Add("FRSDESC")
        dt.Columns.Add("FSOLVMETH")
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
                    Dim txtgReasonCode As TextBox = CType(e.FindControl("txtgReasonCode"), TextBox)
                    Dim txtgReasonDescription As TextBox = CType(e.FindControl("txtgReasonDescription"), TextBox)
                    Dim txtgRemark As TextBox = CType(e.FindControl("txtgRemark"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)

                    dr = dt.NewRow
                    strID = CInt(hddID.Value)
                    dr.Item("ID") = hddID.Value
                    dr.Item("FRSCODE") = txtgReasonCode.Text
                    dr.Item("FRSDESC") = txtgReasonDescription.Text
                    dr.Item("FSOLVMETH") = txtgRemark.Text

                    hddFlagSetAdd.Value = "0"
                    dr.Item("FlagSetAdd") = "0"
                    dt.Rows.Add(dr)
                    If hddFlagSetAdd.Value = "1" Then
                        dt.Rows.Remove(dr)
                    End If
                Next
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("FRSCODE") = String.Empty
                dr.Item("FRSDESC") = String.Empty
                dr.Item("FSOLVMETH") = String.Empty
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
                Dim txtgReasonCode As TextBox = CType(e.FindControl("txtgReasonCode"), TextBox)
                Dim txtgReasonDescription As TextBox = CType(e.FindControl("txtgReasonDescription"), TextBox)
                Dim txtgRemark As TextBox = CType(e.FindControl("txtgRemark"), TextBox)
                Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                dr = dt.NewRow
                dr.Item("ID") = hddID.Value
                dr.Item("FRSCODE") = txtgReasonCode.Text
                dr.Item("FRSDESC") = txtgReasonDescription.Text
                dr.Item("FSOLVMETH") = txtgRemark.Text
                dr.Item("FlagSetAdd") = hddFlagSetAdd.Value
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
    Public Sub SetDatatable(ByRef dt As DataTable,
                            ByVal lst As List(Of KD05RSCD))
        Call CreateDatatable(dt)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            For Each m As KD05RSCD In lst
                dr = dt.NewRow
                strID = strID + 1
                dr.Item("ID") = strID
                dr.Item("FRSCODE") = m.FRSCODE
                dr.Item("FRSDESC") = m.FRSDESC
                dr.Item("FSOLVMETH") = m.FSOLVMETH
                dr.Item("FlagSetAdd") = "0"
                dt.Rows.Add(dr)
            Next
            dr = dt.NewRow
            dr.Item("ID") = strID + 1
            dr.Item("FRSCODE") = String.Empty
            dr.Item("FRSDESC") = String.Empty
            dr.Item("FSOLVMETH") = String.Empty
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
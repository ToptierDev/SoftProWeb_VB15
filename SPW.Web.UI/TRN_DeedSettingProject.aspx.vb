Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class TRN_DeedSettingProject
    Inherits BasePage
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.DataBind()
                hddParameterMenuID.Value = HelperLog.LoadMenuID("TRN_DeedSettingProject.aspx")
                Me.ClearSessionPageLoad("TRN_DeedSettingProject.aspx")
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
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            End If
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
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadProject", ex.GetBaseException)
        End Try
    End Sub

#End Region

#Region "Event DropDownlist"
    Protected Sub ddlsProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsProject.SelectedIndexChanged
        'Call LoadPhase(ddlsPhase, "S", ddlsProject.SelectedValue)
        Call LoadRedirec("")
    End Sub

#End Region

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        Call Redirect("TRN_DeedSettingProject.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("TRN_DeedSettingProject_FS")
        Session.Add("TRN_DeedSettingProject_Project", IIf(ddlsProject.SelectedIndex <> 0, ddlsProject.SelectedValue, ""))
        Session.Add("TRN_DeedSettingProject_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("TRN_DeedSettingProject_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("TRN_DeedSettingProject_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("TRN_DeedSettingProject_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("TRN_DeedSettingProject_Project") IsNot Nothing Then
            If Session("TRN_DeedSettingProject_Project").ToString <> String.Empty Then
                Dim strProject As String = Session("TRN_DeedSettingProject_Project").ToString
                ddlsProject.SelectedValue = strProject
            End If
        End If
        If Session("TRN_DeedSettingProject_FS") IsNot Nothing Then
            If Session("TRN_DeedSettingProject_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("TRN_DeedSettingProject_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
            End If
        End If
        If Session("TRN_DeedSettingProject_PageInfo") IsNot Nothing Then
            If Session("TRN_DeedSettingProject_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("TRN_DeedSettingProject_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("TRN_DeedSettingProject_Search") IsNot Nothing Then
            If Session("TRN_DeedSettingProject_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("TRN_DeedSettingProject_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("TRN_DeedSettingProject_pageLength") IsNot Nothing Then
            If Session("TRN_DeedSettingProject_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("TRN_DeedSettingProject_pageLength").ToString
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
        lblsDeedTotal.Text = Me.GetResource("lblsDeedTotal", "Text", hddParameterMenuID.Value)

        'lblsSave.Text = Me.GetResource("lblsSave", "Text", hddParameterMenuID.Value)
        'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

        lblMassage1.Text = grtt("resPleaseSelect")

        hddMSGSaveData.Value = Me.GetResource("msg_save_data", "MSG")
        hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG")
        hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG")
        hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG")
        hddMSGAddData.Value = Me.GetResource("msg_add_data", "MSG")
        'hddpCheckinTable.Value = Me.GetResource("msg_duplicate_table", "MSG")

        btnMSGSaveData.Title = hddMSGSaveData.Value
        btnMSGCancelData.Title = hddMSGCancelData.Value

        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try
            hddReloadGrid.Value = "1"

            Dim dt As New DataTable
            Dim bl As cDeedSettingProject = New cDeedSettingProject

            Dim strProject As String = String.Empty
            'Call SetCiterail(ddlsProject.SelectedValue, strProject)
            strProject = ddlsProject.SelectedValue
            If strProject <> String.Empty Then
                Dim lst As List(Of FD11PROP) = bl.Loaddata(strProject,
                                                           Me.CurrentUser.UserID)
                If lst IsNot Nothing Then
                    If lst.Count > 0 Then
                        Call SetDatatable(dt,
                                          lst)
                        grdView.DataSource = dt
                        grdView.DataBind()
                    Else
                        Call AddDataTable(dt)
                        grdView.DataSource = Nothing
                        grdView.DataBind()
                        CallLoadGridView()
                    End If
                Else
                    Call AddDataTable(dt)
                    grdView.DataSource = Nothing
                    grdView.DataBind()
                    CallLoadGridView()
                End If

                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "CalQTY();", True)
            End If
            Session.Remove("TRN_DeedSettingProject_Project")
            Session.Remove("TRN_DeedSettingProject_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)

            Session.Remove("TRN_DeedSettingProject_Project")
            Session.Remove("TRN_DeedSettingProject_FS")
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
            Dim bl As cDeedSettingProject = New cDeedSettingProject
            Dim strProject As String = String.Empty
            'Call SetCiterail(ddlsProject.SelectedValue, strProject)
            strProject = ddlsProject.SelectedValue
            Call GetDatatable(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Select("FASSETNO ='' And FlagSetAdd ='0'").Length <> 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_required", "MSG") & Me.GetResource("gFASSETNO", "Text", hddParameterMenuID.Value) & "');", True)
                        Exit Sub
                    End If
                    If dt.Select("FlagSelect ='' And FlagSetAdd ='0'").Length <> 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("resgFASSETNOSelect", "Text", hddParameterMenuID.Value) & "');", True)
                        Exit Sub
                    End If
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Select("FASSETNO ='" & dt.Rows(i)("FASSETNO").ToString & "' And FlagSetAdd ='0'").Length > 1 Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_duplicate", "MSG") & "');", True)
                            Exit Sub
                        End If
                    Next
                End If
            End If
            If Not bl.Add(strProject,
                          dt,
                          Me.CurrentUser.UserID) Then

                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
            Else
                Call LoadRedirec("1")
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
                    Dim hddFlagSelect As HiddenField = CType(e.Row.FindControl("hddFlagSelect"), HiddenField)
                    Dim hddgPCPIECE As HiddenField = CType(e.Row.FindControl("hddgPCPIECE"), HiddenField)
                    Dim hddgFQTY As HiddenField = CType(e.Row.FindControl("hddgFQTY"), HiddenField)
                    Dim hddgFMORTGBK As HiddenField = CType(e.Row.FindControl("hddgFMORTGBK"), HiddenField)
                    Dim hddgFPCLNDNO As HiddenField = CType(e.Row.FindControl("hddgFPCLNDNO"), HiddenField)
                    Dim hddgFPCWIDTH As HiddenField = CType(e.Row.FindControl("hddgFPCWIDTH"), HiddenField)
                    Dim hddgFPCBETWEEN As HiddenField = CType(e.Row.FindControl("hddgFPCBETWEEN"), HiddenField)
                    Dim txtgFASSETNO As TextBox = CType(e.Row.FindControl("txtgFASSETNO"), TextBox)
                    Dim txtgPCPIECE As TextBox = CType(e.Row.FindControl("txtgPCPIECE"), TextBox)
                    Dim txtgFQTY As TextBox = CType(e.Row.FindControl("txtgFQTY"), TextBox)
                    Dim txtgFMORTGBK As TextBox = CType(e.Row.FindControl("txtgFMORTGBK"), TextBox)
                    Dim txtgFMAINCSTR As TextBox = CType(e.Row.FindControl("txtgFMAINCSTR"), TextBox)
                    Dim txtgFMAINCEND As TextBox = CType(e.Row.FindControl("txtgFMAINCEND"), TextBox)
                    Dim txtgFQTYADJPLUS1 As TextBox = CType(e.Row.FindControl("txtgFQTYADJPLUS1"), TextBox)
                    'Dim txtgFQTYADJPLUS2 As TextBox = CType(e.Row.FindControl("txtgFQTYADJPLUS2"), TextBox)
                    'Dim txtgFQTYADJPLUS3 As TextBox = CType(e.Row.FindControl("txtgFQTYADJPLUS3"), TextBox)
                    Dim txtgFQTYADJNPLUS1 As TextBox = CType(e.Row.FindControl("txtgFQTYADJNPLUS1"), TextBox)
                    'Dim txtgFQTYADJNPLUS2 As TextBox = CType(e.Row.FindControl("txtgFQTYADJNPLUS2"), TextBox)
                    'Dim txtgFQTYADJNPLUS3 As TextBox = CType(e.Row.FindControl("txtgFQTYADJNPLUS3"), TextBox)
                    Dim txtgFPCLNDNO As TextBox = CType(e.Row.FindControl("txtgFPCLNDNO"), TextBox)
                    Dim txtgFPCWIDTH As TextBox = CType(e.Row.FindControl("txtgFPCWIDTH"), TextBox)
                    Dim txtgFPCBETWEEN As TextBox = CType(e.Row.FindControl("txtgFPCBETWEEN"), TextBox)
                    Dim txtgFPCNOTE As TextBox = CType(e.Row.FindControl("txtgFPCNOTE"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.Row.FindControl("hddFlagSetAdd"), HiddenField)
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "ID")) Then
                        If DataBinder.Eval(e.Row.DataItem, "ID").ToString <> String.Empty Then
                            hddID.Value = DataBinder.Eval(e.Row.DataItem, "ID")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FASSETNO")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FASSETNO").ToString <> String.Empty Then
                            txtgFASSETNO.Text = DataBinder.Eval(e.Row.DataItem, "FASSETNO")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "PCPIECE")) Then
                        If DataBinder.Eval(e.Row.DataItem, "PCPIECE").ToString <> String.Empty Then
                            txtgPCPIECE.Text = DataBinder.Eval(e.Row.DataItem, "PCPIECE")
                            hddgPCPIECE.Value = DataBinder.Eval(e.Row.DataItem, "PCPIECE")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FQTY")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FQTY").ToString <> String.Empty Then
                            Dim TempQTY As String = DataBinder.Eval(e.Row.DataItem, "FQTY")
                            Dim TempQTY1 As String = "0"
                            Dim TempQTY2 As String = "0"
                            Dim TempQTY3 As String = "0"
                            Try
                                Call CalReverse(TempQTY,
                                                TempQTY1,
                                                TempQTY2,
                                                TempQTY3,
                                                String.Empty)
                                txtgFQTY.Text = String.Format("{0:N0}", CInt(TempQTY1)) & "-" &
                                                String.Format("{0:N0}", CInt(TempQTY2)) & "-" &
                                                String.Format("{0:N0}", CInt(TempQTY3))
                                hddgFQTY.Value = String.Format("{0:N0}", CInt(TempQTY1)) & "-" &
                                                String.Format("{0:N0}", CInt(TempQTY2)) & "-" &
                                                String.Format("{0:N0}", CInt(TempQTY3))
                            Catch ex As Exception
                                txtgFQTY.Text = DataBinder.Eval(e.Row.DataItem, "FQTY").ToString
                                hddgFQTY.Value = DataBinder.Eval(e.Row.DataItem, "FQTY").ToString
                            End Try
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FMORTGBK")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FMORTGBK").ToString <> String.Empty Then
                            txtgFMORTGBK.Text = DataBinder.Eval(e.Row.DataItem, "FMORTGBK")
                            hddgFMORTGBK.Value = DataBinder.Eval(e.Row.DataItem, "FMORTGBK")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FMAINCSTR")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FMAINCSTR").ToString <> String.Empty Then
                            'Try
                            '    txtgFMAINCSTR.Text = CDate(DataBinder.Eval(e.Row.DataItem, "FMAINCSTR")).ToString("dd/MM/yyyy")
                            'Catch ex As Exception
                            '    txtgFMAINCSTR.Text = DataBinder.Eval(e.Row.DataItem, "FMAINCSTR")
                            'End Try
                            txtgFMAINCSTR.Text = ToStringByCulture(DataBinder.Eval(e.Row.DataItem, "FMAINCSTR"))
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FMAINCEND")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FMAINCEND").ToString <> String.Empty Then
                            'Try
                            '    txtgFMAINCEND.Text = CDate(DataBinder.Eval(e.Row.DataItem, "FMAINCEND")).ToString("dd/MM/yyyy")
                            'Catch ex As Exception
                            '    txtgFMAINCEND.Text = DataBinder.Eval(e.Row.DataItem, "FMAINCEND")
                            'End Try
                            txtgFMAINCEND.Text = ToStringByCulture(DataBinder.Eval(e.Row.DataItem, "FMAINCEND"))
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FQTYADJPLUS1")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FQTYADJPLUS1").ToString <> String.Empty Then
                            txtgFQTYADJPLUS1.Text = DataBinder.Eval(e.Row.DataItem, "FQTYADJPLUS1")
                        End If
                    End If
                    'If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FQTYADJPLUS2")) Then
                    '    If DataBinder.Eval(e.Row.DataItem, "FQTYADJPLUS2").ToString <> String.Empty Then
                    '        txtgFQTYADJPLUS2.Text = DataBinder.Eval(e.Row.DataItem, "FQTYADJPLUS2")
                    '    End If
                    'End If
                    'If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FQTYADJPLUS3")) Then
                    '    If DataBinder.Eval(e.Row.DataItem, "FQTYADJPLUS3").ToString <> String.Empty Then
                    '        txtgFQTYADJPLUS3.Text = DataBinder.Eval(e.Row.DataItem, "FQTYADJPLUS3")
                    '    End If
                    'End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FQTYADJNPLUS1")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FQTYADJNPLUS1").ToString <> String.Empty Then
                            txtgFQTYADJNPLUS1.Text = DataBinder.Eval(e.Row.DataItem, "FQTYADJNPLUS1")
                        End If
                    End If
                    'If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FQTYADJNPLUS2")) Then
                    '    If DataBinder.Eval(e.Row.DataItem, "FQTYADJNPLUS2").ToString <> String.Empty Then
                    '        txtgFQTYADJNPLUS2.Text = DataBinder.Eval(e.Row.DataItem, "FQTYADJNPLUS2")
                    '    End If
                    'End If
                    'If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FQTYADJNPLUS3")) Then
                    '    If DataBinder.Eval(e.Row.DataItem, "FQTYADJNPLUS3").ToString <> String.Empty Then
                    '        txtgFQTYADJNPLUS3.Text = DataBinder.Eval(e.Row.DataItem, "FQTYADJNPLUS3")
                    '    End If
                    'End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FPCLNDNO")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FPCLNDNO").ToString <> String.Empty Then
                            txtgFPCLNDNO.Text = DataBinder.Eval(e.Row.DataItem, "FPCLNDNO")
                            hddgFPCLNDNO.Value = DataBinder.Eval(e.Row.DataItem, "FPCLNDNO")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FPCWIDTH")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FPCWIDTH").ToString <> String.Empty Then
                            txtgFPCWIDTH.Text = DataBinder.Eval(e.Row.DataItem, "FPCWIDTH")
                            hddgFPCWIDTH.Value = DataBinder.Eval(e.Row.DataItem, "FPCWIDTH")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FPCBETWEEN")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FPCBETWEEN").ToString <> String.Empty Then
                            txtgFPCBETWEEN.Text = DataBinder.Eval(e.Row.DataItem, "FPCBETWEEN")
                            hddgFPCBETWEEN.Value = DataBinder.Eval(e.Row.DataItem, "FPCBETWEEN")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FPCNOTE")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FPCNOTE").ToString <> String.Empty Then
                            txtgFPCNOTE.Text = DataBinder.Eval(e.Row.DataItem, "FPCNOTE")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FlagSetAdd").ToString <> String.Empty Then
                            hddFlagSetAdd.Value = DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FlagSelect")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FlagSelect").ToString <> String.Empty Then
                            hddFlagSelect.Value = DataBinder.Eval(e.Row.DataItem, "FlagSelect")
                        End If
                    End If
                    If hddFlagSetAdd.Value = "1" Then
                        txtgFASSETNO.Attributes.Add("onclick", "addRowGridView('" & txtgFASSETNO.ClientID & "');")
                        txtgFASSETNO.Attributes.Remove("onkeypress")
                        txtgFASSETNO.BackColor = Drawing.Color.White
                        txtgPCPIECE.BackColor = Drawing.Color.White
                        txtgFQTY.BackColor = Drawing.Color.White
                        txtgFMORTGBK.BackColor = Drawing.Color.White
                        txtgFMAINCSTR.BackColor = Drawing.Color.White
                        txtgFMAINCEND.BackColor = Drawing.Color.White
                        txtgFQTYADJPLUS1.BackColor = Drawing.Color.White
                        'txtgFQTYADJPLUS2.BackColor = Drawing.Color.White
                        'txtgFQTYADJPLUS3.BackColor = Drawing.Color.White
                        txtgFQTYADJNPLUS1.BackColor = Drawing.Color.White
                        'txtgFQTYADJNPLUS2.BackColor = Drawing.Color.White
                        'txtgFQTYADJNPLUS3.BackColor = Drawing.Color.White
                        txtgFPCLNDNO.BackColor = Drawing.Color.White
                        txtgFPCWIDTH.BackColor = Drawing.Color.White
                        txtgFPCBETWEEN.BackColor = Drawing.Color.White
                        txtgFPCNOTE.BackColor = Drawing.Color.White
                        txtgPCPIECE.Attributes.Add("onclick", "addRowGridView('" & txtgPCPIECE.ClientID & "');")
                        txtgFQTY.Attributes.Add("onclick", "addRowGridView('" & txtgFQTY.ClientID & "');")
                        txtgFMORTGBK.Attributes.Add("onclick", "addRowGridView('" & txtgFMORTGBK.ClientID & "');")
                        txtgFMAINCSTR.Attributes.Add("onclick", "addRowGridView('" & txtgFMAINCSTR.ClientID & "');")
                        txtgFMAINCEND.Attributes.Add("onclick", "addRowGridView('" & txtgFMAINCEND.ClientID & "');")
                        txtgFQTYADJPLUS1.Attributes.Add("onclick", "addRowGridView('" & txtgFQTYADJPLUS1.ClientID & "');")
                        'txtgFQTYADJPLUS2.Attributes.Add("onclick", "addRowGridView('" & txtgFQTYADJPLUS2.ClientID & "');")
                        'txtgFQTYADJPLUS3.Attributes.Add("onclick", "addRowGridView('" & txtgFQTYADJPLUS3.ClientID & "');")
                        txtgFQTYADJNPLUS1.Attributes.Add("onclick", "addRowGridView('" & txtgFQTYADJNPLUS1.ClientID & "');")
                        'txtgFQTYADJNPLUS2.Attributes.Add("onclick", "addRowGridView('" & txtgFQTYADJNPLUS2.ClientID & "');")
                        'txtgFQTYADJNPLUS3.Attributes.Add("onclick", "addRowGridView('" & txtgFQTYADJNPLUS3.ClientID & "');")
                        txtgFPCLNDNO.Attributes.Add("onclick", "addRowGridView('" & txtgFPCLNDNO.ClientID & "');")
                        txtgFPCWIDTH.Attributes.Add("onclick", "addRowGridView('" & txtgFPCWIDTH.ClientID & "');")
                        txtgFPCBETWEEN.Attributes.Add("onclick", "addRowGridView('" & txtgFPCBETWEEN.ClientID & "');")
                        txtgFPCNOTE.Attributes.Add("onclick", "addRowGridView('" & txtgFPCNOTE.ClientID & "');")
                        txtgFMAINCSTR.Attributes.Add("class", "form-control paddingZero")
                        txtgFMAINCEND.Attributes.Add("class", "form-control paddingZero")
                        btnDelete.Visible = False
                    ElseIf hddFlagSetAdd.Value = "0" Then
                        txtgFASSETNO.Attributes.Remove("onclick")
                        txtgFASSETNO.Attributes.Add("onkeypress", "return AutocompletedDeed('" & txtgFASSETNO.ClientID & "'," &
                                                                                            "'" & txtgPCPIECE.ClientID & "'," &
                                                                                            "'" & txtgFQTY.ClientID & "'," &
                                                                                            "'" & txtgFMORTGBK.ClientID & "'," &
                                                                                            "'" & txtgFMAINCSTR.ClientID & "'," &
                                                                                            "'" & txtgFMAINCEND.ClientID & "'," &
                                                                                            "'" & txtgFQTYADJPLUS1.ClientID & "'," &
                                                                                            "'" & txtgFQTYADJNPLUS1.ClientID & "'," &
                                                                                            "'" & txtgFPCLNDNO.ClientID & "'," &
                                                                                            "'" & txtgFPCWIDTH.ClientID & "'," &
                                                                                            "'" & txtgFPCBETWEEN.ClientID & "'," &
                                                                                            "'" & txtgFPCNOTE.ClientID & "'," &
                                                                                            "'" & hddID.Value & "'," &
                                                                                            "'" & hddFlagSelect.ClientID & "'," &
                                                                                            "'" & hddgPCPIECE.ClientID & "'," &
                                                                                            "'" & hddgFQTY.ClientID & "'," &
                                                                                            "'" & hddgFMORTGBK.ClientID & "'," &
                                                                                            "'" & hddgFPCLNDNO.ClientID & "'," &
                                                                                            "'" & hddgFPCWIDTH.ClientID & "'," &
                                                                                            "'" & hddgFPCBETWEEN.ClientID & "'," &
                                                                                            "event);")
                        txtgPCPIECE.Attributes.Remove("onclick")
                        txtgFQTY.Attributes.Remove("onclick")
                        txtgFMORTGBK.Attributes.Remove("onclick")
                        txtgFMAINCSTR.Attributes.Remove("onclick")
                        txtgFMAINCEND.Attributes.Remove("onclick")
                        txtgFQTYADJPLUS1.Attributes.Remove("onclick")
                        'txtgFQTYADJPLUS2.Attributes.Remove("onclick")
                        'txtgFQTYADJPLUS3.Attributes.Remove("onclick")
                        txtgFQTYADJNPLUS1.Attributes.Remove("onclick")
                        'txtgFQTYADJNPLUS2.Attributes.Remove("onclick")
                        'txtgFQTYADJNPLUS3.Attributes.Remove("onclick")
                        txtgFPCLNDNO.Attributes.Remove("onclick")
                        txtgFPCWIDTH.Attributes.Remove("onclick")
                        txtgFPCBETWEEN.Attributes.Remove("onclick")
                        txtgFPCNOTE.Attributes.Remove("onclick")
                        txtgFMAINCSTR.Attributes.Add("class", "datepicker form-control paddingZero")
                        txtgFMAINCEND.Attributes.Add("class", "datepicker form-control paddingZero")
                        btnDelete.Visible = True
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
        dt.Columns.Add("FASSETNO")
        dt.Columns.Add("PCPIECE")
        dt.Columns.Add("FQTY")
        dt.Columns.Add("FMORTGBK")
        dt.Columns.Add("FMAINCSTR")
        dt.Columns.Add("FMAINCEND")
        dt.Columns.Add("FQTYADJPLUS")
        dt.Columns.Add("FQTYADJPLUS1")
        'dt.Columns.Add("FQTYADJPLUS2")
        'dt.Columns.Add("FQTYADJPLUS3")
        dt.Columns.Add("FQTYADJNPLUS")
        dt.Columns.Add("FQTYADJNPLUS1")
        'dt.Columns.Add("FQTYADJNPLUS2")
        'dt.Columns.Add("FQTYADJNPLUS3")
        dt.Columns.Add("FPCLNDNO")
        dt.Columns.Add("FPCWIDTH")
        dt.Columns.Add("FPCBETWEEN")
        dt.Columns.Add("FPCNOTE")
        dt.Columns.Add("FlagSetAdd")
        dt.Columns.Add("FlagSelect")
    End Sub
    Public Sub AddDataTable(ByRef dt As DataTable)
        Call CreateDatatable(dt)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            If grdView IsNot Nothing Then
                For Each e As GridViewRow In grdView.Rows
                    Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                    Dim hddFlagSelect As HiddenField = CType(e.FindControl("hddFlagSelect"), HiddenField)
                    Dim hddgPCPIECE As HiddenField = CType(e.FindControl("hddgPCPIECE"), HiddenField)
                    Dim hddgFQTY As HiddenField = CType(e.FindControl("hddgFQTY"), HiddenField)
                    Dim hddgFMORTGBK As HiddenField = CType(e.FindControl("hddgFMORTGBK"), HiddenField)
                    Dim hddgFPCLNDNO As HiddenField = CType(e.FindControl("hddgFPCLNDNO"), HiddenField)
                    Dim hddgFPCWIDTH As HiddenField = CType(e.FindControl("hddgFPCWIDTH"), HiddenField)
                    Dim hddgFPCBETWEEN As HiddenField = CType(e.FindControl("hddgFPCBETWEEN"), HiddenField)
                    Dim txtgFASSETNO As TextBox = CType(e.FindControl("txtgFASSETNO"), TextBox)
                    Dim txtgPCPIECE As TextBox = CType(e.FindControl("txtgPCPIECE"), TextBox)
                    Dim txtgFQTY As TextBox = CType(e.FindControl("txtgFQTY"), TextBox)
                    Dim txtgFMORTGBK As TextBox = CType(e.FindControl("txtgFMORTGBK"), TextBox)
                    Dim txtgFMAINCSTR As TextBox = CType(e.FindControl("txtgFMAINCSTR"), TextBox)
                    Dim txtgFMAINCEND As TextBox = CType(e.FindControl("txtgFMAINCEND"), TextBox)
                    Dim txtgFQTYADJPLUS1 As TextBox = CType(e.FindControl("txtgFQTYADJPLUS1"), TextBox)
                    'Dim txtgFQTYADJPLUS2 As TextBox = CType(e.FindControl("txtgFQTYADJPLUS2"), TextBox)
                    'Dim txtgFQTYADJPLUS3 As TextBox = CType(e.FindControl("txtgFQTYADJPLUS3"), TextBox)
                    Dim txtgFQTYADJNPLUS1 As TextBox = CType(e.FindControl("txtgFQTYADJNPLUS1"), TextBox)
                    'Dim txtgFQTYADJNPLUS2 As TextBox = CType(e.FindControl("txtgFQTYADJNPLUS2"), TextBox)
                    'Dim txtgFQTYADJNPLUS3 As TextBox = CType(e.FindControl("txtgFQTYADJNPLUS3"), TextBox)
                    Dim txtgFPCLNDNO As TextBox = CType(e.FindControl("txtgFPCLNDNO"), TextBox)
                    Dim txtgFPCWIDTH As TextBox = CType(e.FindControl("txtgFPCWIDTH"), TextBox)
                    Dim txtgFPCBETWEEN As TextBox = CType(e.FindControl("txtgFPCBETWEEN"), TextBox)
                    Dim txtgFPCNOTE As TextBox = CType(e.FindControl("txtgFPCNOTE"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)

                    dr = dt.NewRow
                    strID = CInt(hddID.Value)
                    dr.Item("ID") = hddID.Value
                    dr.Item("FASSETNO") = txtgFASSETNO.Text
                    dr.Item("PCPIECE") = hddgPCPIECE.Value
                    dr.Item("FQTY") = hddgFQTY.Value
                    dr.Item("FMORTGBK") = hddgFMORTGBK.Value
                    dr.Item("FMAINCSTR") = ToSystemDate(txtgFMAINCSTR.Text)
                    dr.Item("FMAINCEND") = ToSystemDate(txtgFMAINCEND.Text)
                    dr.Item("FQTYADJPLUS1") = txtgFQTYADJPLUS1.Text
                    'dr.Item("FQTYADJPLUS2") = txtgFQTYADJPLUS2.Text
                    'dr.Item("FQTYADJPLUS3") = txtgFQTYADJPLUS3.Text
                    dr.Item("FQTYADJNPLUS1") = txtgFQTYADJNPLUS1.Text
                    'dr.Item("FQTYADJNPLUS2") = txtgFQTYADJNPLUS2.Text
                    'dr.Item("FQTYADJNPLUS3") = txtgFQTYADJNPLUS3.Text
                    dr.Item("FPCLNDNO") = hddgFPCLNDNO.Value
                    dr.Item("FPCWIDTH") = hddgFPCWIDTH.Value
                    dr.Item("FPCBETWEEN") = hddgFPCBETWEEN.Value
                    dr.Item("FPCNOTE") = txtgFPCNOTE.Text
                    hddFlagSetAdd.Value = "0"
                    dr.Item("FlagSetAdd") = "0"
                    dr.Item("FlagSelect") = hddFlagSelect.Value
                    dt.Rows.Add(dr)
                    If hddFlagSetAdd.Value = "1" Then
                        dt.Rows.Remove(dr)
                    End If
                Next
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("FASSETNO") = String.Empty
                dr.Item("PCPIECE") = String.Empty
                dr.Item("FQTY") = String.Empty
                dr.Item("FMORTGBK") = String.Empty
                dr.Item("FMAINCSTR") = String.Empty
                dr.Item("FMAINCEND") = String.Empty
                dr.Item("FQTYADJPLUS1") = String.Empty
                'dr.Item("FQTYADJPLUS2") = String.Empty
                'dr.Item("FQTYADJPLUS3") = String.Empty
                dr.Item("FQTYADJNPLUS1") = String.Empty
                'dr.Item("FQTYADJNPLUS2") = String.Empty
                'dr.Item("FQTYADJNPLUS3") = String.Empty
                dr.Item("FPCLNDNO") = String.Empty
                dr.Item("FPCWIDTH") = String.Empty
                dr.Item("FPCBETWEEN") = String.Empty
                dr.Item("FPCNOTE") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dr.Item("FlagSelect") = ""
                dt.Rows.Add(dr)
            Else
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("FASSETNO") = String.Empty
                dr.Item("PCPIECE") = String.Empty
                dr.Item("FQTY") = String.Empty
                dr.Item("FMORTGBK") = String.Empty
                dr.Item("FMAINCSTR") = String.Empty
                dr.Item("FMAINCEND") = String.Empty
                dr.Item("FQTYADJPLUS1") = String.Empty
                'dr.Item("FQTYADJPLUS2") = String.Empty
                'dr.Item("FQTYADJPLUS3") = String.Empty
                dr.Item("FQTYADJNPLUS1") = String.Empty
                'dr.Item("FQTYADJNPLUS2") = String.Empty
                'dr.Item("FQTYADJNPLUS3") = String.Empty
                dr.Item("FPCLNDNO") = String.Empty
                dr.Item("FPCWIDTH") = String.Empty
                dr.Item("FPCBETWEEN") = String.Empty
                dr.Item("FPCNOTE") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dr.Item("FlagSelect") = ""
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
                Dim hddFlagSelect As HiddenField = CType(e.FindControl("hddFlagSelect"), HiddenField)
                Dim hddgPCPIECE As HiddenField = CType(e.FindControl("hddgPCPIECE"), HiddenField)
                Dim hddgFQTY As HiddenField = CType(e.FindControl("hddgFQTY"), HiddenField)
                Dim hddgFMORTGBK As HiddenField = CType(e.FindControl("hddgFMORTGBK"), HiddenField)
                Dim hddgFPCLNDNO As HiddenField = CType(e.FindControl("hddgFPCLNDNO"), HiddenField)
                Dim hddgFPCWIDTH As HiddenField = CType(e.FindControl("hddgFPCWIDTH"), HiddenField)
                Dim hddgFPCBETWEEN As HiddenField = CType(e.FindControl("hddgFPCBETWEEN"), HiddenField)
                Dim txtgFASSETNO As TextBox = CType(e.FindControl("txtgFASSETNO"), TextBox)
                Dim txtgPCPIECE As TextBox = CType(e.FindControl("txtgPCPIECE"), TextBox)
                Dim txtgFQTY As TextBox = CType(e.FindControl("txtgFQTY"), TextBox)
                Dim txtgFMORTGBK As TextBox = CType(e.FindControl("txtgFMORTGBK"), TextBox)
                Dim txtgFMAINCSTR As TextBox = CType(e.FindControl("txtgFMAINCSTR"), TextBox)
                Dim txtgFMAINCEND As TextBox = CType(e.FindControl("txtgFMAINCEND"), TextBox)
                Dim txtgFQTYADJPLUS1 As TextBox = CType(e.FindControl("txtgFQTYADJPLUS1"), TextBox)
                'Dim txtgFQTYADJPLUS2 As TextBox = CType(e.FindControl("txtgFQTYADJPLUS2"), TextBox)
                'Dim txtgFQTYADJPLUS3 As TextBox = CType(e.FindControl("txtgFQTYADJPLUS3"), TextBox)
                Dim txtgFQTYADJNPLUS1 As TextBox = CType(e.FindControl("txtgFQTYADJNPLUS1"), TextBox)
                'Dim txtgFQTYADJNPLUS2 As TextBox = CType(e.FindControl("txtgFQTYADJNPLUS2"), TextBox)
                'Dim txtgFQTYADJNPLUS3 As TextBox = CType(e.FindControl("txtgFQTYADJNPLUS3"), TextBox)
                Dim txtgFPCLNDNO As TextBox = CType(e.FindControl("txtgFPCLNDNO"), TextBox)
                Dim txtgFPCWIDTH As TextBox = CType(e.FindControl("txtgFPCWIDTH"), TextBox)
                Dim txtgFPCBETWEEN As TextBox = CType(e.FindControl("txtgFPCBETWEEN"), TextBox)
                Dim txtgFPCNOTE As TextBox = CType(e.FindControl("txtgFPCNOTE"), TextBox)
                Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                dr = dt.NewRow
                dr.Item("ID") = hddID.Value
                dr.Item("FASSETNO") = txtgFASSETNO.Text
                dr.Item("PCPIECE") = hddgPCPIECE.Value
                dr.Item("FQTY") = hddgFQTY.Value
                dr.Item("FMORTGBK") = hddgFMORTGBK.Value
                dr.Item("FMAINCSTR") = ToSystemDate(txtgFMAINCSTR.Text)
                dr.Item("FMAINCEND") = ToSystemDate(txtgFMAINCEND.Text)
                dr.Item("FQTYADJPLUS1") = txtgFQTYADJPLUS1.Text
                'dr.Item("FQTYADJPLUS2") = txtgFQTYADJPLUS2.Text
                'dr.Item("FQTYADJPLUS3") = txtgFQTYADJPLUS3.Text
                dr.Item("FQTYADJNPLUS1") = txtgFQTYADJNPLUS1.Text
                'dr.Item("FQTYADJNPLUS2") = txtgFQTYADJNPLUS2.Text
                'dr.Item("FQTYADJNPLUS3") = txtgFQTYADJNPLUS3.Text
                dr.Item("FPCLNDNO") = hddgFPCLNDNO.Value
                dr.Item("FPCWIDTH") = hddgFPCWIDTH.Value
                dr.Item("FPCBETWEEN") = hddgFPCBETWEEN.Value
                dr.Item("FPCNOTE") = txtgFPCNOTE.Text
                dr.Item("FlagSetAdd") = hddFlagSetAdd.Value
                dr.Item("FlagSelect") = hddFlagSelect.Value
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
    Public Sub SetDatatable(ByRef dt As DataTable,
                            ByVal lst As List(Of FD11PROP))
        Call CreateDatatable(dt)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            For Each m As FD11PROP In lst
                dr = dt.NewRow
                strID = strID + 1
                dr.Item("ID") = strID
                dr.Item("FASSETNO") = m.FASSETNO
                dr.Item("PCPIECE") = m.FPCPIECE
                Dim strFQTY As String = String.Empty
                If m.FQTY IsNot Nothing Then
                    strFQTY = String.Format("{0:N2}", CDec(m.FQTY))
                End If
                dr.Item("FQTY") = strFQTY
                dr.Item("FMORTGBK") = m.FMORTGBK
                Dim TempDate As String = IIf(m.FMAINCSTR IsNot Nothing, m.FMAINCSTR, String.Empty)
                If TempDate <> String.Empty Then
                    dr.Item("FMAINCSTR") = TempDate 'CDate(TempDate.Split(" ")(0)).ToString("dd/MM/yyyy")
                End If
                TempDate = IIf(m.FMAINCSTR IsNot Nothing, m.FMAINCEND, String.Empty)
                If TempDate <> String.Empty Then
                    dr.Item("FMAINCEND") = TempDate 'CDate(TempDate.Split(" ")(0)).ToString("dd/MM/yyyy")
                End If
                Dim Temp1 As String = "0"
                Dim Temp2 As String = "0"
                Dim Temp3 As String = "0"
                If m.FQTYADJ IsNot Nothing Then
                    If CStr(m.FQTYADJ).IndexOf("-") <> 0 Then
                        Call CalReverse(m.FQTYADJ,
                                        Temp1,
                                        Temp2,
                                        Temp3,
                                        String.Empty)
                        dr.Item("FQTYADJPLUS1") = String.Format("{0:N0}", CInt(Temp1)) & "-" &
                                                  String.Format("{0:N0}", CInt(Temp2)) & "-" &
                                                  String.Format("{0:N0}", CInt(Temp3))
                        'dr.Item("FQTYADJPLUS2") = String.Format("{0:N0}", CInt(Temp2))
                        'dr.Item("FQTYADJPLUS3") = String.Format("{0:N0}", CInt(Temp3))
                        dr.Item("FQTYADJNPLUS1") = "0-0-0"
                        'dr.Item("FQTYADJNPLUS2") = "0"
                        'dr.Item("FQTYADJNPLUS3") = "0"
                    Else
                        Call CalReverse(m.FQTYADJ,
                                        Temp1,
                                        Temp2,
                                        Temp3,
                                        "-")
                        dr.Item("FQTYADJPLUS1") = "0-0-0"
                        'dr.Item("FQTYADJPLUS2") = "0"
                        'dr.Item("FQTYADJPLUS3") = "0"
                        dr.Item("FQTYADJNPLUS1") = String.Format("{0:N0}", CInt(Temp1)) & "-" &
                                                  String.Format("{0:N0}", CInt(Temp2)) & "-" &
                                                  String.Format("{0:N0}", CInt(Temp3))
                        'dr.Item("FQTYADJNPLUS2") = String.Format("{0:N0}", CInt(Temp2))
                        'dr.Item("FQTYADJNPLUS3") = String.Format("{0:N0}", CInt(Temp3))
                    End If
                End If
                dr.Item("FPCLNDNO") = m.FPCLNDNO
                dr.Item("FPCWIDTH") = m.FPCWIDTH
                dr.Item("FPCBETWEEN") = m.FPCBETWEEN
                dr.Item("FPCNOTE") = m.FPCNOTE
                dr.Item("FlagSetAdd") = "0"
                dr.Item("FlagSelect") = "1"
                dt.Rows.Add(dr)
            Next
            dr = dt.NewRow
            dr.Item("ID") = strID + 1
            dr.Item("FASSETNO") = String.Empty
            dr.Item("PCPIECE") = String.Empty
            dr.Item("FQTY") = String.Empty
            dr.Item("FMORTGBK") = String.Empty
            dr.Item("FMAINCSTR") = String.Empty
            dr.Item("FMAINCEND") = String.Empty
            dr.Item("FQTYADJPLUS1") = String.Empty
            'dr.Item("FQTYADJPLUS2") = String.Empty
            'dr.Item("FQTYADJPLUS3") = String.Empty
            dr.Item("FQTYADJNPLUS1") = String.Empty
            'dr.Item("FQTYADJNPLUS2") = String.Empty
            'dr.Item("FQTYADJNPLUS3") = String.Empty
            dr.Item("FPCLNDNO") = String.Empty
            dr.Item("FPCWIDTH") = String.Empty
            dr.Item("FPCBETWEEN") = String.Empty
            dr.Item("FPCNOTE") = String.Empty
            dr.Item("FlagSetAdd") = "1"
            dr.Item("FlagSelect") = ""
            dt.Rows.Add(dr)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "SetDatatable", ex)
        End Try
    End Sub
#End Region

#Region "Control Validate"
    Public Sub ControlValidate()
        lblMassage1.Style.Add("display", "none")
    End Sub
#End Region

#Region "CalReverse"
    Public Sub CalReverse(ByVal pTotal As String,
                          ByRef pParam1 As String,
                          ByRef pParam2 As String,
                          ByRef pParam3 As String,
                          ByVal pType As String)
        If pType = "-" Then
            pTotal = CDec(pTotal) * -1
        End If
        Dim TempQTY1 As String = CDec(pTotal) / 400
        If TempQTY1.IndexOf(".") > 0 Then
            TempQTY1 = TempQTY1.Split(".")(0)
            pParam1 = TempQTY1
            TempQTY1 = CInt(TempQTY1) * 400
        Else
            pParam1 = TempQTY1
            TempQTY1 = CInt(TempQTY1) * 400
        End If
        Dim TempQTY2 As String = CDec(pTotal) - TempQTY1
        Dim TempQTY3 As String = CDec(TempQTY2) / 100
        If TempQTY3.IndexOf(".") > 0 Then
            TempQTY3 = TempQTY3.Split(".")(0)
            pParam2 = TempQTY3
            TempQTY3 = CInt(TempQTY3) * 100
        Else
            pParam2 = TempQTY3
            TempQTY3 = CInt(TempQTY3) * 100
        End If
        Dim TempQTY4 As String = CDec(TempQTY2) - TempQTY3
        TempQTY4 = TempQTY4.Split(".")(0)
        pParam3 = TempQTY4
    End Sub
#End Region
End Class
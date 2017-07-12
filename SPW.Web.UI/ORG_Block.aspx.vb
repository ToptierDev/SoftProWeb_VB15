Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class ORG_Block
    Inherits BasePage
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.DataBind()
                Session.Remove("IDOCS.application.LoaddataBlock")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("ORG_Block.aspx")
                Me.ClearSessionPageLoad("ORG_Block.aspx")
                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()
                Call getUsedMaster()
                Call Loaddata()

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
            Call LoadProject(ddlaProject, "A")
            Call LoadPhase(ddlsPhase, "S", ddlsProject.SelectedValue)
            Call LoadPhase(ddlaPhase, "A", ddlaProject.SelectedValue)
            Call LoadZone(ddlsZone, "S", ddlsProject.SelectedValue, ddlsPhase.SelectedValue)
            Call LoadZone(ddlaZone, "A", ddlaProject.SelectedValue, ddlaPhase.SelectedValue)
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
    Public Sub LoadPhase(ByVal ddl As DropDownList,
                         ByVal strType As String,
                         ByVal pProject As String)
        Dim bl As cBlock = New cBlock
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadPhase(pProject)
            If lc IsNot Nothing Then
                ddl.DataValueField = "FREPHASE"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "FREPHASE"
                Else
                    ddl.DataTextField = "FREPHASE"
                End If
                ddl.DataSource = lc
                ddl.DataBind()
            End If
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select_all", "MSG", "1"), ""))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex.GetBaseException)
        End Try
    End Sub
    Public Sub LoadZone(ByVal ddl As DropDownList,
                        ByVal strType As String,
                        ByVal pProject As String,
                        ByVal pPhase As String)
        Dim bl As cBlock = New cBlock
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadZone(pProject,
                                 pPhase)
            If lc IsNot Nothing Then
                'ddl.DataValueField = "FREZONE"
                'If Me.CurrentPage.ToString.ToUpper = "EN" Then
                '    ddl.DataTextField = "FREZONE"
                'Else
                '    ddl.DataTextField = "FREZONE"
                'End If
                ddl.DataSource = lc
                ddl.DataBind()
            End If
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select_all", "MSG", "1"), ""))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex.GetBaseException)
        End Try
    End Sub
#End Region

#Region "Event DropDownlist"
    Protected Sub ddlsProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsProject.SelectedIndexChanged
        Call LoadPhase(ddlsPhase, "S", ddlsProject.SelectedValue)
        Call LoadZone(ddlsZone, "S", ddlsProject.SelectedValue, ddlsPhase.SelectedValue)
    End Sub
    Protected Sub ddlaProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlaProject.SelectedIndexChanged
        Call LoadPhase(ddlaPhase, "A", ddlaProject.SelectedValue)
        Call LoadZone(ddlaZone, "A", ddlaProject.SelectedValue, ddlaPhase.SelectedValue)
    End Sub
    Protected Sub ddlsPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsPhase.SelectedIndexChanged
        Call LoadZone(ddlsZone, "S", ddlsProject.SelectedValue, ddlsPhase.SelectedValue)
    End Sub
    Protected Sub ddlaPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlaPhase.SelectedIndexChanged
        Call LoadZone(ddlaZone, "A", ddlaProject.SelectedValue, ddlaPhase.SelectedValue)
    End Sub
#End Region

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        Call Redirect("ORG_Block.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("ORG_Block_Project")
        Session.Remove("ORG_Block_Phase")
        Session.Remove("ORG_Block_Zone")
        Session.Remove("ORG_Block_FS")
        Session.Add("ORG_Block_Project", IIf(ddlsProject.SelectedIndex <> 0, ddlsProject.SelectedValue, ""))
        Session.Add("ORG_Block_Phase", IIf(ddlsPhase.SelectedIndex <> 0, ddlsPhase.SelectedValue, ""))
        Session.Add("ORG_Block_Zone", IIf(ddlsZone.SelectedIndex <> 0, ddlsZone.SelectedValue, ""))
        Session.Add("ORG_Block_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("ORG_Block_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("ORG_Block_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("ORG_Block_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("ORG_Block_Project") IsNot Nothing Then
            If Session("ORG_Block_Project").ToString <> String.Empty Then
                Dim strProject As String = Session("ORG_Block_Project").ToString
                Try
                    ddlsProject.SelectedValue = strProject

                Catch ex As Exception
                    ddlsProject.SelectedIndex = 0
                End Try
            End If
        End If
        If ddlsProject.SelectedIndex <> 0 Then
            Call LoadPhase(ddlsPhase, "S", ddlsProject.SelectedValue)
            Call LoadZone(ddlsZone, "S", ddlsProject.SelectedValue, ddlsPhase.SelectedValue)
        End If
        If Session("ORG_Block_Phase") IsNot Nothing Then
            If Session("ORG_Block_Phase").ToString <> String.Empty Then
                Dim strPhase As String = Session("ORG_Block_Phase").ToString
                Try
                    ddlsPhase.SelectedValue = strPhase
                    Call LoadZone(ddlsZone, "S", ddlsProject.SelectedValue, ddlsPhase.SelectedValue)
                Catch ex As Exception
                    ddlsPhase.SelectedIndex = 0
                End Try
            End If
        End If
        If ddlsPhase.SelectedIndex <> 0 Then
            Call LoadZone(ddlsZone, "S", ddlsProject.SelectedValue, ddlsPhase.SelectedValue)
        End If
        If Session("ORG_Block_Zone") IsNot Nothing Then
            If Session("ORG_Block_Zone").ToString <> String.Empty Then
                Dim strZone As String = Session("ORG_Block_Zone").ToString
                Try
                    ddlsZone.SelectedValue = strZone
                Catch ex As Exception
                    ddlsZone.SelectedIndex = 0
                End Try
            End If
        End If
        If Session("ORG_Block_FS") IsNot Nothing Then
            If Session("ORG_Block_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("ORG_Block_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG", "1") & "');", True)
                End If
            End If
        End If
        If Session("ORG_Block_PageInfo") IsNot Nothing Then
            If Session("ORG_Block_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("ORG_Block_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("ORG_Block_Search") IsNot Nothing Then
            If Session("ORG_Block_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("ORG_Block_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("ORG_Block_pageLength") IsNot Nothing Then
            If Session("ORG_Block_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("ORG_Block_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If
    End Sub

#End Region

#Region "Event Textbox"
    Protected Sub txtaBlock_TextChanged(sender As Object, e As EventArgs) Handles txtaBlock.TextChanged
        Try
            hddReloadGrid.Value = String.Empty

            If ddlaProject.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_please_select", "MSG", "1") & " " & GetResource("lblsProject", "Text", hddParameterMenuID.Value) & "');", True)
                Exit Sub
            End If
            If ddlaPhase.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_please_select", "MSG", "1") & " " & GetResource("PhaseCode", "Text", hddParameterMenuID.Value) & "');", True)
                Exit Sub
            End If
            If ddlaZone.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_please_select", "MSG", "1") & " " & GetResource("ZoneCode", "Text", hddParameterMenuID.Value) & "');", True)
                Exit Sub
            End If

            Dim dt As New DataTable
            Dim bl As New cBlock
            Dim lst As List(Of Block_ViewModel) = bl.LoadBlockMaster(ddlaProject.SelectedValue,
                                                                     ddlaPhase.SelectedValue,
                                                                     ddlaZone.SelectedValue,
                                                                     txtaBlock.Text)
            If lst IsNot Nothing Then
                If lst.Count > 0 Then
                    txtaBlock.Text = String.Empty
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_block_already", "MSG", "1") & "');", True)
                    Exit Sub
                End If
            Else
                txtaBlock.Text = String.Empty
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_block_already", "MSG", "1") & "');", True)
                Exit Sub
            End If
            Dim lsts As List(Of ED03UNIT) = bl.LoadED03UNITCheckData(ddlaProject.SelectedValue)
            If lsts IsNot Nothing Then
                If lsts.Count > 0 Then
                    For Each i As ED03UNIT In lsts
                        If hddpMasterData.Value = String.Empty Then
                            hddpMasterData.Value = i.FSERIALNO
                        Else
                            hddpMasterData.Value = hddpMasterData.Value & "," & i.FSERIALNO
                        End If
                    Next
                End If
            End If

            hddpUnitUsed.Value = String.Empty
            Dim lstsUsed As List(Of ED03UNIT) = bl.LoadED03UNITCheckDataUsed(ddlaProject.SelectedValue,
                                                                             txtaBlock.Text)
            If lstsUsed IsNot Nothing Then
                If lstsUsed.Count > 0 Then
                    For Each i As ED03UNIT In lstsUsed
                        If hddpUnitUsed.Value = String.Empty Then
                            hddpUnitUsed.Value = i.FSERIALNO
                        Else
                            hddpUnitUsed.Value = hddpUnitUsed.Value & "," & i.FSERIALNO
                        End If
                    Next
                End If
            End If

            hddCheckProjectPriceList.Value = String.Empty
            Call getUsedProjectPriceListAdd()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "txtaBlock_TextChanged", ex)
        End Try

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

        lblsProject.Text = Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value)
        lblsPhase.Text = Me.GetResource("lblsPhase", "Text", hddParameterMenuID.Value)
        lblsZoneCode.Text = Me.GetResource("lblsZoneCode", "Text", hddParameterMenuID.Value)

        lblaProject.Text = Me.GetResource("lblaProject", "Text", hddParameterMenuID.Value)
        lblaPhase.Text = Me.GetResource("lblaPhase", "Text", hddParameterMenuID.Value)
        lblaZone.Text = Me.GetResource("lblaZone", "Text", hddParameterMenuID.Value)
        lblaBlock.Text = Me.GetResource("lblaBlock", "Text", hddParameterMenuID.Value)
        lblaUnitFrom.Text = Me.GetResource("lblaUnitFrom", "Text", hddParameterMenuID.Value)
        lblaUnitTo.Text = Me.GetResource("lblaUnitTo", "Text", hddParameterMenuID.Value)

        TextHd1.Text = Me.GetResource("No", "Text", "1")
        TextHd2.Text = Me.GetResource("PhaseCode", "Text", hddParameterMenuID.Value)
        TextHd3.Text = Me.GetResource("ZoneCode", "Text", hddParameterMenuID.Value)
        TextHd4.Text = Me.GetResource("BlockCode", "Text", hddParameterMenuID.Value)
        TextHd5.Text = Me.GetResource("col_edit", "Text", "1")
        TextHd6.Text = Me.GetResource("col_delete", "Text", "1")

        TextFt1.Text = Me.GetResource("No", "Text", "1")
        TextFt2.Text = Me.GetResource("PhaseCode", "Text", hddParameterMenuID.Value)
        TextFt3.Text = Me.GetResource("ZoneCode", "Text", hddParameterMenuID.Value)
        TextFt4.Text = Me.GetResource("BlockCode", "Text", hddParameterMenuID.Value)
        TextFt5.Text = Me.GetResource("col_edit", "Text", "1")
        TextFt6.Text = Me.GetResource("col_delete", "Text", "1")

        lblsSearch.Text = Me.GetResource("lblsSearch", "Text", hddParameterMenuID.Value)
        'lblsSave.Text = Me.GetResource("btn_save", "Text", "1")
        'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

        lblMassage1.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage2.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage3.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage4.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage5.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage6.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage7.Text = grtt("resPleaseSelect")

        lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG", "1")
        lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG", "1")

        lblHeaderSave.Text = Me.GetResource("msg_header_save", "MSG", "1")
        lblBodySave.Text = Me.GetResource("msg_body_save", "MSG", "1")

        btnSaveData.Title = Me.GetResource("msg_save_data", "MSG", "1")
        btnCancelData.Title = Me.GetResource("msg_cancel_data", "MSG", "1")
        hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG", "1")
        hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG", "1")
        btnAddData.Title = Me.GetResource("msg_add_data", "MSG", "1")
        btnConfrimDelete.InnerText = hddMSGDeleteData.Value
        btnConfrimCancel.InnerText = Me.GetResource("msg_cancel_data", "MSG", "1")
        hddpMSGDup.Value = Me.GetResource("msg_check_master", "MSG", "1")
        hddMSGCheckProjectPriceList.Value = Me.GetResource("resUsedED11PAJ1", "MSG", "1")
        'hddpMSGDupInTable.Value = Me.GetResource("msg_duplicate_table", "MSG", "1")
        btnCallSaveOK.InnerText = Me.GetResource("msg_save_data", "MSG", "1")
        btnCallSaveCancel.InnerText = Me.GetResource("msg_cancel_data", "MSG", "1")


        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try
            hddReloadGrid.Value = "1"
            Dim bl As cBlock = New cBlock
            If ddlsProject.SelectedIndex <> 0 Then
                Dim lc As List(Of Block_ViewModel) = bl.Loaddata(ddlsProject.SelectedValue,
                                                                 ddlsPhase.SelectedValue,
                                                                 ddlsZone.SelectedValue,
                                                                 Me.CurrentUser.UserID)
                If lc IsNot Nothing Then
                    Call SetDataBlock(lc)
                Else
                    Call SetDataBlock(Nothing)
                End If
            End If

            Session.Remove("ORG_Block_Project")
            Session.Remove("ORG_Block_Phase")
            Session.Remove("ORG_Block_Zone")
            Session.Remove("ORG_Block_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)

            Session.Remove("ORG_Block_Project")
            Session.Remove("ORG_Block_Phase")
            Session.Remove("ORG_Block_Zone")
            Session.Remove("ORG_Block_FS")
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

#Region "GridView"
    Protected Sub grdView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView2.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    If hddIDRealTime.Value = String.Empty Then
                        hddIDRealTime.Value = "1"
                    Else
                        hddIDRealTime.Value = CInt(hddIDRealTime.Value) + 1
                    End If
                    Dim btnDelete As ImageButton = e.Row.FindControl("btnDelete")
                    btnDelete.CommandArgument = DataBinder.Eval(e.Row.DataItem, "ID")
                    Dim hddID As HiddenField = CType(e.Row.FindControl("hddID"), HiddenField)
                    Dim hddgStyleCode As HiddenField = CType(e.Row.FindControl("hddgStyleCode"), HiddenField)
                    Dim hddgAddress As HiddenField = CType(e.Row.FindControl("hddgAddress"), HiddenField)
                    Dim txtgUnit As TextBox = CType(e.Row.FindControl("txtgUnit"), TextBox)
                    Dim txtgStyleCode As TextBox = CType(e.Row.FindControl("txtgStyleCode"), TextBox)
                    Dim txtgAddress As TextBox = CType(e.Row.FindControl("txtgAddress"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.Row.FindControl("hddFlagSetAdd"), HiddenField)
                    'Dim hddFlagDup As HiddenField = CType(e.Row.FindControl("hddFlagDup"), HiddenField)
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "ID")) Then
                        If DataBinder.Eval(e.Row.DataItem, "ID").ToString <> String.Empty Then
                            hddID.Value = DataBinder.Eval(e.Row.DataItem, "ID")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FSERIALNO")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FSERIALNO").ToString <> String.Empty Then
                            txtgUnit.Text = DataBinder.Eval(e.Row.DataItem, "FSERIALNO")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FPDCODE")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FPDCODE").ToString <> String.Empty Then
                            txtgStyleCode.Text = DataBinder.Eval(e.Row.DataItem, "FPDCODE")
                            hddgStyleCode.Value = DataBinder.Eval(e.Row.DataItem, "FPDCODE")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FADDRNO")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FADDRNO").ToString <> String.Empty Then
                            txtgAddress.Text = DataBinder.Eval(e.Row.DataItem, "FADDRNO")
                            hddgAddress.Value = DataBinder.Eval(e.Row.DataItem, "FADDRNO")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FlagSetAdd").ToString <> String.Empty Then
                            hddFlagSetAdd.Value = DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")
                        End If
                    End If
                    'If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FlagDup")) Then
                    '    If DataBinder.Eval(e.Row.DataItem, "FlagDup").ToString <> String.Empty Then
                    '        hddFlagDup.Value = DataBinder.Eval(e.Row.DataItem, "FlagDup")
                    '    End If
                    'End If
                    'If hddFlagDup.Value = "1" Then
                    '    txtgUnit.CssClass = "form-control parsley-error"
                    'Else
                    '    txtgUnit.CssClass = "form-control"
                    'End If
                    If hddFlagSetAdd.Value = "1" Then
                        txtgUnit.Attributes.Remove("onblur")
                        txtgUnit.Attributes.Add("onclick", "addRowGridView('" & txtgUnit.ClientID & "');")
                        txtgStyleCode.Attributes.Add("onclick", "addRowGridView('" & txtgStyleCode.ClientID & "');")
                        txtgAddress.Attributes.Add("onclick", "addRowGridView('" & txtgAddress.ClientID & "');")
                        btnDelete.Visible = False
                    ElseIf hddFlagSetAdd.Value = "0" Then
                        txtgUnit.Attributes.Add("onblur", "CheckDataDup('" & txtgUnit.ClientID & "','" & hddIDRealTime.Value & "','" & txtgStyleCode.ClientID & "','" & txtgAddress.ClientID & "','" & hddgStyleCode.ClientID & "','" & hddgAddress.ClientID & "');")
                        txtgUnit.Attributes.Remove("onclick")
                        txtgStyleCode.Attributes.Remove("onclick")
                        txtgAddress.Attributes.Remove("onclick")
                        btnDelete.Visible = True
                    End If

                    For Each m As String In hddCheckProjectPriceList.Value.Split(",")
                        If txtgUnit.Text = m Then
                            txtgUnit.Enabled = False
                            btnDelete.Visible = False
                        End If
                    Next
            End Select
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, hddParameterMenuID.Value, "grdView2_RowDataBound", ex)
        End Try
    End Sub
    Private Sub grdView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdView2.RowCommand
        Try
            Dim dts As New DataTable
            Dim dr As DataRow
            Call CreateDatatableDelete(dts)
            Select Case e.CommandName
                Case "btnDelete"
                    Dim dt As New DataTable
                    Call CreateDatatable(dt)
                    Call GetDatatable(dt)
                    If dt IsNot Nothing Then
                        If Session("ORG_Block_Datatable_Delete") IsNot Nothing Then
                            dts = Session("ORG_Block_Datatable_Delete")
                        End If
                        For Each i As DataRow In dt.Select("ID= '" & e.CommandArgument & "'")
                            dr = dts.NewRow
                            dr.Item("FSERIALNO") = i.Item("FSERIALNO").ToString
                            dts.Rows.Add(dr)
                            dt.Rows.Remove(i)
                        Next

                        Session.Add("ORG_Block_Datatable_Delete", Nothing)
                        If dts IsNot Nothing Then
                            If dts.Rows.Count > 0 Then
                                Session.Add("ORG_Block_Datatable_Delete", dts)
                            End If
                        End If
                        If dt.Rows.Count > 0 Then
                            hddIDRealTime.Value = String.Empty
                            grdView2.DataSource = dt
                            grdView2.DataBind()
                        Else
                            hddIDRealTime.Value = String.Empty
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

#Region "Event"
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSearch_Click", ex)
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Dim bl As New cBlock
            hddKeyID.Value = ""
            hddpUnitUsed.Value = String.Empty
            txtaBlock.Text = String.Empty

            Try
                If ddlsProject.SelectedIndex <> 0 Then
                    ddlaProject.SelectedValue = ddlsProject.SelectedValue
                    Call LoadPhase(ddlaPhase, "A", ddlaProject.SelectedValue)
                    If ddlsPhase.SelectedIndex <> 0 Then
                        ddlaPhase.SelectedValue = ddlsPhase.SelectedValue
                        Call LoadZone(ddlaZone, "A", ddlaProject.SelectedValue, ddlaPhase.SelectedValue)
                        If ddlsZone.SelectedIndex <> 0 Then
                            ddlaZone.SelectedValue = ddlsZone.SelectedValue
                        Else
                            ddlaZone.SelectedIndex = 0
                        End If
                    Else
                        ddlaPhase.SelectedIndex = 0
                    End If
                Else
                    ddlaProject.SelectedIndex = 0
                End If
            Catch ex As Exception
                ddlaProject.SelectedIndex = 0
                ddlaPhase.SelectedIndex = 0
                ddlaZone.SelectedIndex = 0
            End Try

            hddpMasterData.Value = String.Empty
            Dim lsts As List(Of ED03UNIT) = bl.LoadED03UNITCheckData(ddlaProject.SelectedValue)
            If lsts IsNot Nothing Then
                If lsts.Count > 0 Then
                    For Each i As ED03UNIT In lsts
                        If hddpMasterData.Value = String.Empty Then
                            hddpMasterData.Value = i.FSERIALNO + "|" + i.FPDCODE + "|" + i.FADDRNO
                        Else
                            hddpMasterData.Value = hddpMasterData.Value & "," & i.FSERIALNO + "|" + i.FPDCODE + "|" + i.FADDRNO
                        End If
                    Next
                End If
            End If

            hddIDRealTime.Value = String.Empty
            grdView2.DataSource = Nothing
            grdView2.DataBind()
            CallLoadGridView()

            ddlaProject.Enabled = True
            ddlaPhase.Enabled = True
            ddlaZone.Enabled = True
            txtaBlock.Enabled = True

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try

            Dim bl As New cBlock
            Dim dt As New DataTable
            Dim strProjectCode As String = String.Empty
            Dim strPhaseCode As String = String.Empty
            Dim strZoneCode As String = String.Empty
            Dim strBlock As String = String.Empty
            If hddKeyID.Value <> String.Empty Then
                Try
                    strProjectCode = hddKeyID.Value.Split("|")(0).Replace(" ", "")
                    strPhaseCode = hddKeyID.Value.Split("|")(1).Replace(" ", "")
                    strZoneCode = hddKeyID.Value.Split("|")(2).Replace(" ", "")
                    strBlock = hddKeyID.Value.Split("|")(3).Replace(" ", "")
                Catch ex As Exception

                End Try
            End If
            Try
                If strProjectCode <> String.Empty Then
                    ddlaProject.SelectedValue = strProjectCode
                    Call LoadPhase(ddlaPhase, "A", ddlaProject.SelectedValue)
                    If strPhaseCode <> String.Empty Then
                        ddlaPhase.SelectedValue = strPhaseCode
                        Call LoadZone(ddlaZone, "A", ddlaProject.SelectedValue, ddlaPhase.SelectedValue)
                        If strZoneCode <> String.Empty Then
                            ddlaZone.SelectedValue = strZoneCode
                        Else
                            ddlaZone.SelectedIndex = 0
                        End If
                    Else
                        ddlaPhase.SelectedIndex = 0
                    End If
                Else
                    ddlaProject.SelectedIndex = 0
                End If
            Catch ex As Exception
                ddlaProject.SelectedIndex = 0
                ddlaPhase.SelectedIndex = 0
                ddlaZone.SelectedIndex = 0
            End Try

            txtaBlock.Text = strBlock

            Call getUsedProjectPriceListEdit()

            Dim lst As List(Of ED03UNIT) = bl.LoadED03UNIT(strProjectCode,
                                                           strPhaseCode,
                                                           strZoneCode,
                                                           txtaBlock.Text)
            If lst IsNot Nothing Then
                If lst.Count > 0 Then
                    Call SetDatatable(dt,
                                      lst)
                    hddIDRealTime.Value = String.Empty
                    grdView2.DataSource = dt
                    grdView2.DataBind()
                Else
                    hddIDRealTime.Value = String.Empty
                    grdView2.DataSource = Nothing
                    grdView2.DataBind()
                    CallLoadGridView()
                End If
            Else
                hddIDRealTime.Value = String.Empty
                grdView2.DataSource = Nothing
                grdView2.DataBind()
                CallLoadGridView()
            End If

            hddpMasterData.Value = String.Empty
            Dim lsts As List(Of ED03UNIT) = bl.LoadED03UNITCheckData(strProjectCode)
            If lsts IsNot Nothing Then
                If lsts.Count > 0 Then
                    For Each i As ED03UNIT In lsts
                        If hddpMasterData.Value = String.Empty Then
                            hddpMasterData.Value = i.FSERIALNO + "|" + i.FPDCODE + "|" + i.FADDRNO
                        Else
                            hddpMasterData.Value = hddpMasterData.Value & "," & i.FSERIALNO + "|" + i.FPDCODE + "|" + i.FADDRNO
                        End If
                    Next
                End If
            End If

            hddpUnitUsed.Value = String.Empty
            Dim lstsUsed As List(Of ED03UNIT) = bl.LoadED03UNITCheckDataUsed(strProjectCode,
                                                                             txtaBlock.Text)
            If lstsUsed IsNot Nothing Then
                If lstsUsed.Count > 0 Then
                    For Each i As ED03UNIT In lstsUsed
                        If hddpUnitUsed.Value = String.Empty Then
                            hddpUnitUsed.Value = i.FSERIALNO
                        Else
                            hddpUnitUsed.Value = hddpUnitUsed.Value & "," & i.FSERIALNO
                        End If
                    Next
                End If
            End If

            ddlaProject.Enabled = False
            ddlaPhase.Enabled = False
            ddlaZone.Enabled = False
            txtaBlock.Enabled = False

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Try
                Dim code As String = Convert.ToString(hddKeyID.Value)
                Dim bl As cBlock = New cBlock
                Dim strProjectCode As String = String.Empty
                Dim strPhaseCode As String = String.Empty
                Dim strZoneCode As String = String.Empty
                Dim strBlockCode As String = String.Empty
                If hddKeyID.Value <> String.Empty Then
                    Try
                        strProjectCode = hddKeyID.Value.Split("|")(0).Replace(" ", "")
                        strPhaseCode = hddKeyID.Value.Split("|")(1).Replace(" ", "")
                        strZoneCode = hddKeyID.Value.Split("|")(2).Replace(" ", "")
                        strBlockCode = hddKeyID.Value.Split("|")(3).Replace(" ", "")
                    Catch ex As Exception

                    End Try
                End If
                If code <> String.Empty Then
                    If Not bl.Delete(strProjectCode,
                                     strPhaseCode,
                                     strZoneCode,
                                     strBlockCode,
                                     Me.CurrentUser.UserID) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_cannot_delete_because ", "MSG", "1") & "');", True)
                    Else
                        Call LoadRedirec("2")
                    End If
                End If

            Catch ex As Exception
                HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnDelete_Click", ex)
            End Try
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnDelete_Click", ex)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim dt As New DataTable
            Dim bl As New cBlock

            If grdView2 IsNot Nothing Then
                Call CreateDatatable(dt)
                Call GetDatatable(dt)
                If Not bl.Save(ddlaProject.SelectedValue,
                               ddlaPhase.SelectedValue,
                               ddlaZone.SelectedValue,
                               txtaBlock.Text,
                               dt,
                               Session("ORG_Block_Datatable_Delete")) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                Else
                    Call LoadRedirec("1")
                End If
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

    Protected Sub btnReloadGrid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReloadGrid.Click
        Try
            hddIDRealTime.Value = String.Empty
            grdView2.DataSource = Nothing
            grdView2.DataBind()
            CallLoadGridView()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReloadGrid_Click", ex)
        End Try
    End Sub
    Protected Sub btnAddDataUnit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddDataUnit.Click
        Try
            hddReloadGrid.Value = String.Empty


            Dim dt As New DataTable
            Dim bl As New cBlock
            Dim lst As List(Of ED03UNIT) = bl.LoadED03UNITBetween(ddlaProject.SelectedValue,
                                                                  txtaUnitFrom.Text,
                                                                  txtaUnitTo.Text)
            Dim pDup As String = String.Empty
            Dim pUsed As String = String.Empty
            If lst IsNot Nothing Then
                If lst.Count > 0 Then
                    Call SetDatatableDataUnit(dt,
                                              lst)
                    txtaUnitFrom.Text = String.Empty
                    txtaUnitTo.Text = String.Empty
                    If dt.Select("FlagDup = '2'").Length > 0 Then
                        For Each i As DataRow In dt.Select("FlagDup= '2'")
                            If pUsed = String.Empty Then
                                pUsed = i.Item("FSERIALNO").ToString
                            Else
                                pUsed = pUsed & "," & i.Item("FSERIALNO").ToString
                            End If
                            dt.Rows.Remove(i)
                        Next

                        hddIDRealTime.Value = String.Empty
                        grdView2.DataSource = dt
                        grdView2.DataBind()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("resUsedED11PAJ1", "MSG", "1") & " " & pUsed & "');", True)
                        Exit Sub
                    End If

                    If dt.Select("FlagDup = '1'").Length > 0 Then
                        For Each i As DataRow In dt.Select("FlagDup= '1'")
                            If pDup = String.Empty Then
                                pDup = i.Item("FSERIALNO").ToString
                            Else
                                pDup = pDup & "," & i.Item("FSERIALNO").ToString
                            End If
                            dt.Rows.Remove(i)
                        Next

                        hddIDRealTime.Value = String.Empty
                        grdView2.DataSource = dt
                        grdView2.DataBind()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_duplicate_table", "MSG", "1") & pDup & "');", True)
                        Exit Sub
                    Else
                        hddIDRealTime.Value = String.Empty
                        grdView2.DataSource = dt
                        grdView2.DataBind()
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_not_found", "MSG", "1") & "');", True)
                    Exit Sub
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_not_found", "MSG", "1") & "');", True)
                Exit Sub
            End If

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
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
        lblMassage3.Style.Add("display", "none")
        lblMassage4.Style.Add("display", "none")
        lblMassage5.Style.Add("display", "none")
        lblMassage6.Style.Add("display", "none")
        lblMassage7.Style.Add("display", "none")
    End Sub
#End Region

#Region "Session"
    Public Function GetDataBlock() As List(Of Block_ViewModel)
        Try
            Return Session("IDOCS.application.LoaddataBlock")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataBlock(ByVal lcBlock As List(Of Block_ViewModel)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataBlock", lcBlock)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Datatable"
    Public Sub CallLoadGridView()
        Dim dt As New DataTable
        Call AddDataTable(dt)

        hddIDRealTime.Value = String.Empty
        grdView2.DataSource = dt
        grdView2.DataBind()
    End Sub
    Public Sub CreateDatatable(ByRef dt As DataTable)
        dt.Columns.Add("ID")
        dt.Columns.Add("FSERIALNO")
        dt.Columns.Add("FPDCODE")
        dt.Columns.Add("FADDRNO")
        dt.Columns.Add("FlagSetAdd")
        dt.Columns.Add("FlagDup")
    End Sub
    Public Sub AddDataTable(ByRef dt As DataTable)
        Call CreateDatatable(dt)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            If grdView2 IsNot Nothing Then
                For Each e As GridViewRow In grdView2.Rows
                    Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                    Dim hddgStyleCode As HiddenField = CType(e.FindControl("hddgStyleCode"), HiddenField)
                    Dim hddgAddress As HiddenField = CType(e.FindControl("hddgAddress"), HiddenField)
                    Dim txtgUnit As TextBox = CType(e.FindControl("txtgUnit"), TextBox)
                    Dim txtgStyleCode As TextBox = CType(e.FindControl("txtgStyleCode"), TextBox)
                    Dim txtgAddress As TextBox = CType(e.FindControl("txtgAddress"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                    Dim hddFlagDup As HiddenField = CType(e.FindControl("hddFlagDup"), HiddenField)

                    dr = dt.NewRow
                    strID = CInt(hddID.Value)
                    dr.Item("ID") = hddID.Value
                    dr.Item("FSERIALNO") = txtgUnit.Text
                    dr.Item("FPDCODE") = hddgStyleCode.Value
                    dr.Item("FADDRNO") = hddgAddress.Value

                    hddFlagSetAdd.Value = "0"
                    dr.Item("FlagSetAdd") = "0"
                    dr.Item("FlagDup") = hddFlagDup.Value

                    dt.Rows.Add(dr)
                    If hddFlagSetAdd.Value = "1" Then
                        dt.Rows.Remove(dr)
                    End If
                Next
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("FSERIALNO") = String.Empty
                dr.Item("FPDCODE") = String.Empty
                dr.Item("FADDRNO") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dr.Item("FlagDup") = String.Empty
                dt.Rows.Add(dr)
            Else
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("FSERIALNO") = String.Empty
                dr.Item("FPDCODE") = String.Empty
                dr.Item("FADDRNO") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dr.Item("FlagDup") = String.Empty
                dt.Rows.Add(dr)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "AddDataTable", ex)
        End Try
    End Sub
    Public Sub SetDatatable(ByRef dt As DataTable,
                            ByVal lc As List(Of ED03UNIT))
        Call CreateDatatable(dt)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            For Each ilc As ED03UNIT In lc
                dr = dt.NewRow
                strID = strID + 1
                dr.Item("ID") = strID
                dr.Item("FSERIALNO") = ilc.FSERIALNO
                dr.Item("FPDCODE") = ilc.FPDCODE
                dr.Item("FADDRNO") = ilc.FADDRNO
                dr.Item("FlagSetAdd") = "0"
                dr.Item("FlagDup") = String.Empty
                dt.Rows.Add(dr)
            Next
            dr = dt.NewRow
            dr.Item("ID") = strID + 1
            dr.Item("FSERIALNO") = String.Empty
            dr.Item("FPDCODE") = String.Empty
            dr.Item("FADDRNO") = String.Empty
            dr.Item("FlagSetAdd") = "1"
            dr.Item("FlagDup") = String.Empty
            dt.Rows.Add(dr)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "SetDatatable", ex)
        End Try
    End Sub
    Public Sub SetDatatableDataUnit(ByRef dt As DataTable,
                                    ByVal lc As List(Of ED03UNIT))
        Dim dr As DataRow
        Dim strID As Integer = 0
        Dim strUsedED11PAJ1 As List(Of String) = Nothing
        Call CreateDatatable(dt)
        Try
            If grdView2 IsNot Nothing Then
                For Each e As GridViewRow In grdView2.Rows
                    Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                    Dim hddgStyleCode As HiddenField = CType(e.FindControl("hddgStyleCode"), HiddenField)
                    Dim hddgAddress As HiddenField = CType(e.FindControl("hddgAddress"), HiddenField)
                    Dim txtgUnit As TextBox = CType(e.FindControl("txtgUnit"), TextBox)
                    Dim txtgStyleCode As TextBox = CType(e.FindControl("txtgStyleCode"), TextBox)
                    Dim txtgAddress As TextBox = CType(e.FindControl("txtgAddress"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                    Dim hddFlagDup As HiddenField = CType(e.FindControl("hddFlagDup"), HiddenField)
                    If hddFlagSetAdd.Value = "0" Then
                        If txtgUnit.Text <> String.Empty Or
                           txtgStyleCode.Text <> String.Empty Or
                           txtgAddress.Text <> String.Empty Then
                            dr = dt.NewRow
                            strID = CInt(hddID.Value)
                            dr.Item("ID") = hddID.Value
                            dr.Item("FSERIALNO") = txtgUnit.Text
                            dr.Item("FPDCODE") = hddgStyleCode.Value
                            dr.Item("FADDRNO") = hddgAddress.Value

                            hddFlagSetAdd.Value = "0"
                            dr.Item("FlagSetAdd") = "0"
                            dr.Item("FlagDup") = hddFlagDup.Value

                            dt.Rows.Add(dr)
                        End If
                    End If
                Next
            End If
            If hddCheckProjectPriceList.Value <> String.Empty Then
                strUsedED11PAJ1 = hddCheckProjectPriceList.Value.Split(",").ToList
            End If
            For Each ilc As ED03UNIT In lc
                If dt.Select("FSERIALNO = '" & ilc.FSERIALNO & "'").Length = 0 Then
                    dr = dt.NewRow
                    strID = strID + 1
                    dr.Item("ID") = strID
                    dr.Item("FSERIALNO") = ilc.FSERIALNO
                    dr.Item("FPDCODE") = ilc.FPDCODE
                    dr.Item("FADDRNO") = ilc.FADDRNO
                    dr.Item("FlagSetAdd") = "0"
                    dr.Item("FlagDup") = String.Empty
                    dt.Rows.Add(dr)
                Else
                    dr = dt.NewRow
                    strID = strID + 1
                    dr.Item("ID") = strID
                    dr.Item("FSERIALNO") = ilc.FSERIALNO
                    dr.Item("FPDCODE") = ilc.FPDCODE
                    dr.Item("FADDRNO") = ilc.FADDRNO
                    dr.Item("FlagSetAdd") = "0"
                    dr.Item("FlagDup") = "1"
                    dt.Rows.Add(dr)
                End If
            Next
            dr = dt.NewRow
            dr.Item("ID") = strID + 1
            dr.Item("FSERIALNO") = String.Empty
            dr.Item("FPDCODE") = String.Empty
            dr.Item("FADDRNO") = String.Empty
            dr.Item("FlagSetAdd") = "1"
            dr.Item("FlagDup") = String.Empty
            dt.Rows.Add(dr)

            If dt.AsEnumerable().Select(Function(s) s.Field(Of String)("FSERIALNO")).Where(Function(s) strUsedED11PAJ1.Contains(s)).Count > 0 Then
                For Each m As DataRow In dt.Rows
                    If strUsedED11PAJ1.Contains(m("FSERIALNO").ToString) Then
                        m("FlagDup") = "2"
                    End If
                Next
            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Sub GetDatatable(ByRef dt As DataTable)
        Try
            Dim dr As DataRow
            For Each e As GridViewRow In grdView2.Rows
                Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                Dim txtgUnit As TextBox = CType(e.FindControl("txtgUnit"), TextBox)
                Dim txtgStyleCode As TextBox = CType(e.FindControl("txtgStyleCode"), TextBox)
                Dim txtgAddress As TextBox = CType(e.FindControl("txtgAddress"), TextBox)
                Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                dr = dt.NewRow
                dr.Item("ID") = hddID.Value
                dr.Item("FSERIALNO") = txtgUnit.Text
                dr.Item("FPDCODE") = txtgStyleCode.Text
                dr.Item("FADDRNO") = txtgAddress.Text
                dr.Item("FlagSetAdd") = hddFlagSetAdd.Value
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
#End Region

#Region "DataTable Delete"
    Public Sub CreateDatatableDelete(ByRef dt As DataTable)
        dt.Columns.Add("FSERIALNO")
    End Sub
#End Region

#Region "Check Master Delete"
    Public Sub getUsedMaster()
        Try
            Dim bl As cBlock = New cBlock
            Dim lc As List(Of String) = bl.getUsedMaster(ddlsProject.SelectedValue,
                                                         ddlsPhase.SelectedValue,
                                                         ddlsZone.SelectedValue)
            hddCheckUsedMaster.Value = String.Empty
            If lc IsNot Nothing Then
                If lc.Count > 0 Then
                    hddCheckUsedMaster.Value = String.Join(",", lc)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "getUsedMaster", ex)
        End Try
    End Sub
    Public Sub getUsedProjectPriceListEdit()
        Try
            Dim bl As APIBL = New APIBL
            Dim lc As List(Of String) = bl.getUsedProjectPriceListEdit(ddlaProject.SelectedValue,
                                                                       ddlaPhase.SelectedValue,
                                                                       ddlaZone.SelectedValue)
            hddCheckProjectPriceList.Value = String.Empty
            If lc IsNot Nothing Then
                If lc.Count > 0 Then
                    hddCheckProjectPriceList.Value = String.Join(",", lc)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "getUsedProjectPriceList", ex)
        End Try
    End Sub
    Public Sub getUsedProjectPriceListAdd()
        Try
            Dim bl As APIBL = New APIBL
            'pRound = 1 Phase <> ปัจจุบัน
            'pRound = 2 Phase เดียวกัน คนละโซน
            Dim lc As List(Of String) = bl.getUsedProjectPriceListAdd(ddlaProject.SelectedValue,
                                                                      ddlaPhase.SelectedValue,
                                                                      ddlaZone.SelectedValue)

            If lc IsNot Nothing Then
                If lc.Count > 0 Then
                    If hddCheckProjectPriceList.Value = String.Empty Then
                        hddCheckProjectPriceList.Value = String.Join(",", lc)
                    Else
                        hddCheckProjectPriceList.Value += String.Join(",", lc)
                    End If
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "getUsedProjectPriceList", ex)
        End Try
    End Sub
#End Region

End Class
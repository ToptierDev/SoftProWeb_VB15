Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class ORG_Zone
    Inherits BasePage
    Private strPathServer As String = System.Configuration.ConfigurationManager.AppSettings("strPathServer").ToString
    Private strPathServerIMG As String = System.Configuration.ConfigurationManager.AppSettings("strPathServerIMG").ToString

    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                Page.DataBind()
                Session.Remove("IDOCS.application.LoaddataZone")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("ORG_Zone.aspx")
                Me.ClearSessionPageLoad("ORG_Zone.aspx")
                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()
                Call getUsedMaster()

                Call Loaddata()
                'Call DeleteAllFileTemp()

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
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex.GetBaseException)
        End Try
    End Sub
    Public Sub LoadPhase(ByVal ddl As DropDownList,
                         ByVal strType As String,
                         ByVal pProject As String)
        Dim bl As cZone = New cZone
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
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select_all", "MSG"), ""))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex)
        End Try
    End Sub
#End Region

#Region "Event DropDownlist"
    Protected Sub ddlsProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsProject.SelectedIndexChanged
        Call LoadPhase(ddlsPhase, "S", ddlsProject.SelectedValue)
    End Sub
    Protected Sub ddlaProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlaProject.SelectedIndexChanged
        Call LoadPhase(ddlaPhase, "A", ddlaProject.SelectedValue)
    End Sub

#End Region

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        Call SetParameter(FlagSave)
        Call Redirect("ORG_Zone.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("ORG_Zone_FS")
        Session.Add("ORG_Zone_Project", IIf(ddlsProject.SelectedIndex <> 0, ddlsProject.SelectedValue, ""))
        Session.Add("ORG_Zone_Zone", IIf(ddlsPhase.SelectedIndex <> 0, ddlsPhase.SelectedValue, ""))
        Session.Add("ORG_Zone_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("ORG_Zone_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("ORG_Zone_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("ORG_Zone_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("ORG_Zone_Project") IsNot Nothing Then
            If Session("ORG_Zone_Project").ToString <> String.Empty Then
                Dim strProject As String = Session("ORG_Zone_Project").ToString
                Try
                    ddlsProject.SelectedValue = strProject
                Catch ex As Exception
                    ddlsProject.SelectedIndex = 0
                End Try
            End If
        End If
        If ddlsProject.SelectedIndex <> 0 Then
            Call LoadPhase(ddlsPhase, "S", ddlsProject.SelectedValue)
        End If
        If Session("ORG_Zone_Zone") IsNot Nothing Then
            If Session("ORG_Zone_Zone").ToString <> String.Empty Then
                Dim strZone As String = Session("ORG_Zone_Zone").ToString
                Try
                    ddlsPhase.SelectedValue = strZone
                Catch ex As Exception
                    ddlsPhase.SelectedIndex = 0
                End Try
            End If
        End If
        If Session("ORG_Zone_FS") IsNot Nothing Then
            If Session("ORG_Zone_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("ORG_Zone_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
            End If
        End If
        If Session("ORG_Zone_PageInfo") IsNot Nothing Then
            If Session("ORG_Zone_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("ORG_Zone_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("ORG_Zone_Search") IsNot Nothing Then
            If Session("ORG_Zone_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("ORG_Zone_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("ORG_Zone_pageLength") IsNot Nothing Then
            If Session("ORG_Zone_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("ORG_Zone_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If
    End Sub

#End Region

#Region "Event Textbox"
    Protected Sub txtaZoneCode_TextChanged(sender As Object, e As EventArgs) Handles txtaZoneCode.TextChanged
        Try
            hddReloadGrid.Value = String.Empty
            If ddlaProject.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_please_select", "MSG") & " " & GetResource("lblsProject", "Text", hddParameterMenuID.Value) & "');", True)
                Exit Sub
            End If
            If ddlaPhase.SelectedIndex = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_please_select", "MSG") & " " & GetResource("PhaseCode", "Text", hddParameterMenuID.Value) & "');", True)
                Exit Sub
            End If
            If txtaZoneCode.Text = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_please_select", "MSG") & " " & GetResource("ZoneCode", "Text", hddParameterMenuID.Value) & "');", True)
                Exit Sub
            End If
            Dim dt As New DataTable
            Dim bl As New cZone
            Dim lst As List(Of Zone_ViewModel) = bl.LoadZoneMaster(ddlaProject.SelectedValue,
                                                                   ddlaPhase.SelectedValue,
                                                                   txtaZoneCode.Text)
            If lst IsNot Nothing Then
                If lst.Count > 0 Then
                    txtaZoneCode.Text = String.Empty
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_zone_already", "MSG") & "');", True)
                    Exit Sub
                End If
            Else
                txtaZoneCode.Text = String.Empty
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_zone_already", "MSG") & "');", True)
                Exit Sub
            End If

            hddpMasterData.Value = String.Empty
            Dim lsts As List(Of Zone_ViewModel) = bl.LoadED03UNITCheckData(ddlaProject.SelectedValue)
            If lsts IsNot Nothing Then
                If lsts.Count > 0 Then
                    For Each i As Zone_ViewModel In lsts
                        If hddpMasterData.Value = String.Empty Then
                            hddpMasterData.Value = i.FSERIALNO & "|" & i.FMLEFT & "|" & i.FMTOP & "|" & i.FRESTATUS
                        Else
                            hddpMasterData.Value = hddpMasterData.Value & "," & i.FSERIALNO & "|" & i.FMLEFT & "|" & i.FMTOP & "|" & i.FRESTATUS
                        End If
                    Next
                End If
            End If

            hddpUnitUsed.Value = String.Empty
            Dim lstsUsed As List(Of Zone_ViewModel) = bl.LoadED03UNITCheckDataUsed(ddlaProject.SelectedValue,
                                                                                   txtaZoneCode.Text)
            If lstsUsed IsNot Nothing Then
                If lstsUsed.Count > 0 Then
                    For Each i As Zone_ViewModel In lstsUsed
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
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "txtaZoneCode_TextChanged", ex)
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
        Try
            hddpTypeBrownser.Value = Request.Browser.Type.ToString.Substring(0, 2).ToLower
        Catch ex As Exception

        End Try
        lblMain1.Text = Me.GetResource("main_label", "Text", "1")
        lblMain2.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
        lblMain3.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
        lblMain4.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)

        lblsProject.Text = Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value)
        lblsPhaseCode.Text = Me.GetResource("PhaseCode", "Text", hddParameterMenuID.Value)
        lblTextPicture.Text = Me.GetResource("lblTextPicture", "Text", hddParameterMenuID.Value)

        lblaProject.Text = Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value)
        lblaPhaseCode.Text = Me.GetResource("PhaseCode", "Text", hddParameterMenuID.Value)
        lblaZoneCode.Text = Me.GetResource("lblsZoneCode", "Text", hddParameterMenuID.Value)
        lblaUnitFrom.Text = Me.GetResource("lblaUnitFrom", "Text", hddParameterMenuID.Value)
        lblaUnitTo.Text = Me.GetResource("lblaUnitTo", "Text", hddParameterMenuID.Value)

        TextHd1.Text = Me.GetResource("No", "Text", "1")
        TextHd2.Text = Me.GetResource("PhaseCode", "Text", hddParameterMenuID.Value)
        TextHd3.Text = Me.GetResource("ZoneCode", "Text", hddParameterMenuID.Value)
        TextHd4.Text = Me.GetResource("col_edit", "Text", "1")
        TextHd5.Text = Me.GetResource("col_delete", "Text", "1")

        TextFt1.Text = Me.GetResource("No", "Text", "1")
        TextFt2.Text = Me.GetResource("PhaseCode", "Text", hddParameterMenuID.Value)
        TextFt3.Text = Me.GetResource("ZoneCode", "Text", hddParameterMenuID.Value)
        TextFt4.Text = Me.GetResource("col_edit", "Text", "1")
        TextFt5.Text = Me.GetResource("col_delete", "Text", "1")

        lblsSearch.Text = Me.GetResource("lblsSearch", "Text", hddParameterMenuID.Value)
        'lblsDeletePicture.Text = Me.GetResource("lblsDeletePicture", "Text", hddParameterMenuID.Value)
        'lblsPicture.Text = Me.GetResource("lblsPicture", "Text", hddParameterMenuID.Value)
        'lblsSave.Text = Me.GetResource("btn_save", "Text", "1")
        'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

        lblMassage1.Text = Me.GetResource("msg_required", "MSG")
        lblMassage2.Text = Me.GetResource("msg_required", "MSG")
        lblMassage3.Text = Me.GetResource("msg_required", "MSG")
        lblMassage4.Text = grtt("resPleaseSelect")
        lblMassage5.Text = Me.GetResource("msg_required", "MSG")
        lblMassage6.Text = Me.GetResource("msg_required", "MSG")

        lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG")
        lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG") & " " & Me.GetResource("ZoneCode", "Text", hddParameterMenuID.Value)

        lblHeaderSave.Text = Me.GetResource("msg_header_save", "MSG")
        lblBodySave.Text = Me.GetResource("msg_body_save", "MSG")

        'btnAddPicture.Title = Me.GetResource("msg_add_file", "MSG")
        'btnDeletePictures.Title = Me.GetResource("msg_delete_file", "MSG")
        btnSaveData.Title = Me.GetResource("msg_save_data", "MSG")
        btnCancelData.Title = Me.GetResource("msg_cancel_data", "MSG")
        hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG")
        hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG")
        btnAddData.Title = Me.GetResource("msg_add_data", "MSG")
        btnConfrimDelete.InnerText = hddMSGDeleteData.Value
        btnConfrimCancel.InnerText = Me.GetResource("msg_cancel_data", "MSG")
        hddpMSGDup.Value = Me.GetResource("msg_check_master", "MSG")
        hddMSGCheckProjectPriceList.Value = Me.GetResource("resUsedED11PAJ1", "MSG")
        'hddpMSGDupInTable.Value = Me.GetResource("msg_duplicate_table", "MSG")
        btnCallSaveOK.InnerText = Me.GetResource("msg_save_data", "MSG")
        btnCallSaveCancel.InnerText = Me.GetResource("msg_cancel_data", "MSG")

        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try
            Dim bl As cZone = New cZone
            hddReloadGrid.Value = "1"

            If ddlsProject.SelectedIndex <> 0 Then
                Dim lc As List(Of Zone_ViewModel) = bl.Loaddata(ddlsProject.SelectedValue,
                                                                ddlsPhase.SelectedValue,
                                                                Me.CurrentUser.UserID)
                If lc IsNot Nothing Then
                    Call SetDataZone(lc)
                Else
                    Call SetDataZone(Nothing)
                End If
            End If

            Session.Remove("ORG_Zone_Project")
            Session.Remove("ORG_Zone_Zone")
            Session.Remove("ORG_Zone_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)

            Session.Remove("ORG_Zone_Project")
            Session.Remove("ORG_Zone_Zone")
            Session.Remove("ORG_Zone_FS")
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
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSearch_Click", ex)
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Dim bl As New cZone
            hddKeyID.Value = String.Empty
            hddpUnitUsed.Value = String.Empty
            hddCheckProjectPriceList.Value = String.Empty
            Try
                If ddlsProject.SelectedIndex <> 0 Then
                    ddlaProject.SelectedValue = ddlsProject.SelectedValue
                    Call LoadPhase(ddlaPhase, "A", ddlaProject.SelectedValue)
                    If ddlsPhase.SelectedIndex <> 0 Then
                        ddlaPhase.SelectedValue = ddlsPhase.SelectedValue
                    Else
                        ddlaPhase.SelectedIndex = 0
                    End If
                Else
                    ddlaProject.SelectedIndex = 0
                End If
            Catch ex As Exception
                ddlaPhase.SelectedIndex = 0
                ddlaProject.SelectedIndex = 0
            End Try

            hddpMasterData.Value = String.Empty
            Dim lsts As List(Of Zone_ViewModel) = bl.LoadED03UNITCheckData(ddlaProject.SelectedValue)
            If lsts IsNot Nothing Then
                If lsts.Count > 0 Then
                    For Each i As Zone_ViewModel In lsts
                        If hddpMasterData.Value = String.Empty Then
                            hddpMasterData.Value = i.FSERIALNO & "|" & i.FMLEFT & "|" & i.FMTOP & "|" & i.FRESTATUS
                        Else
                            hddpMasterData.Value = hddpMasterData.Value & "," & i.FSERIALNO & "|" & i.FMLEFT & "|" & i.FMTOP & "|" & i.FRESTATUS
                        End If
                    Next
                End If
            End If

            txtaZoneCode.Text = String.Empty
            txtaUnitFrom.Text = String.Empty
            txtaUnitTo.Text = String.Empty

            hddIDRealTime.Value = String.Empty
            grdView2.DataSource = Nothing
            grdView2.DataBind()
            CallLoadGridView()

            ddlaProject.Enabled = True
            ddlaPhase.Enabled = True
            txtaZoneCode.Enabled = True

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try
            Dim bl As New cZone
            Dim dt As New DataTable
            Dim strProjectCode As String = String.Empty
            Dim strPhaseCode As String = String.Empty
            Dim strZoneCode As String = String.Empty
            If hddKeyID.Value <> String.Empty Then
                Try
                    strProjectCode = hddKeyID.Value.Split("|")(0).Replace(" ", "")
                    strPhaseCode = hddKeyID.Value.Split("|")(1).Replace(" ", "")
                    strZoneCode = hddKeyID.Value.Split("|")(2).Replace(" ", "")
                Catch ex As Exception

                End Try
            End If

            Try
                If strProjectCode <> String.Empty Then
                    ddlaProject.SelectedValue = strProjectCode
                    Call LoadPhase(ddlaPhase, "A", ddlaProject.SelectedValue)
                    If strPhaseCode <> String.Empty Then
                        ddlaPhase.SelectedValue = strPhaseCode
                    Else
                        ddlaPhase.SelectedIndex = 0
                    End If
                Else
                    ddlaProject.SelectedIndex = 0
                End If
            Catch ex As Exception
                ddlaProject.SelectedIndex = 0
                ddlaPhase.SelectedIndex = 0
            End Try

            txtaZoneCode.Text = strZoneCode

            'Call LoadFileProcessToTemp()
            Call LoadFileProcess()

            Call getUsedProjectPriceListEdit()

            Dim lst As List(Of Zone_ViewModel) = bl.LoadED03UNIT(ddlaProject.SelectedValue,
                                                                 ddlaPhase.SelectedValue,
                                                                 txtaZoneCode.Text)
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
            Dim lsts As List(Of Zone_ViewModel) = bl.LoadED03UNITCheckData(ddlaProject.SelectedValue)
            If lsts IsNot Nothing Then
                If lsts.Count > 0 Then
                    For Each i As Zone_ViewModel In lsts
                        If hddpMasterData.Value = String.Empty Then
                            hddpMasterData.Value = i.FSERIALNO & "|" & i.FMLEFT & "|" & i.FMTOP & "|" & i.FRESTATUS
                        Else
                            hddpMasterData.Value = hddpMasterData.Value & "," & i.FSERIALNO & "|" & i.FMLEFT & "|" & i.FMTOP & "|" & i.FRESTATUS
                        End If
                    Next
                End If
            End If

            hddpUnitUsed.Value = String.Empty
            Dim lstsUsed As List(Of Zone_ViewModel) = bl.LoadED03UNITCheckDataUsed(ddlaProject.SelectedValue,
                                                                                   strZoneCode)
            If lstsUsed IsNot Nothing Then
                If lstsUsed.Count > 0 Then
                    For Each i As Zone_ViewModel In lstsUsed
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
            txtaZoneCode.Enabled = False

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim dt As New DataTable
            Dim bl As New cZone

            If grdView2 IsNot Nothing Then
                Call CreateDatatable(dt)
                Call GetDatatable(dt)
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        If Not bl.Save(ddlaProject.SelectedValue,
                                       ddlaPhase.SelectedValue,
                                       txtaZoneCode.Text,
                                       dt,
                                       Session("ORG_Zone_Datatable_Delete")) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                        Else
                            If FileUpload.HasFile Then
                                Call CopyFileTempToProcess()
                            Else
                                If chkDelete.Checked = True Then
                                    Dim strKeyID As String = ddlaProject.SelectedValue & "-" & ddlaPhase.SelectedValue & "-" & txtaZoneCode.Text & "-0"
                                    Call DeleteAllFile(strKeyID)
                                End If
                            End If
                            Call LoadRedirec("1")
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
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

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim code As String = Convert.ToString(hddKeyID.Value)
            Dim bl As cZone = New cZone
            Dim strProjectCode As String = String.Empty
            Dim strPhaseCode As String = String.Empty
            Dim strZoneCode As String = String.Empty
            If hddKeyID.Value <> String.Empty Then
                Try
                    strProjectCode = hddKeyID.Value.Split("|")(0).Replace(" ", "")
                    strPhaseCode = hddKeyID.Value.Split("|")(1).Replace(" ", "")
                    strZoneCode = hddKeyID.Value.Split("|")(2).Replace(" ", "")
                Catch ex As Exception

                End Try
            End If
            If code <> String.Empty Then
                If Not bl.Delete(strProjectCode,
                                 strPhaseCode,
                                 strZoneCode,
                                 Me.CurrentUser.UserID) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_cannot_delete_because ", "MSG") & "');", True)
                Else
                    Dim strKeyid As String = strProjectCode & "-" & strPhaseCode & "-" & strZoneCode & "-0"
                    'Call DeleteAllFile(strKeyid)
                    Call LoadRedirec("2")
                End If
            End If

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnDelete_Click", ex)
        End Try
    End Sub

    'Protected Const MAX_FILE_LENGTH_MB As Integer = 5
    'Protected Sub btnFileUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileUpload.Click
    '    hddpKeyIDPicture.Value = String.Empty
    '    hddpNamePicture.Value = String.Empty
    '    Dim dt As New DataTable
    '    If FileUpload1.HasFile Then
    '        Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)

    '        Dim fileType As String() = Split(".jpg,.jpeg,.gif,.png", ",")
    '        If Not fileType.Contains(Path.GetExtension(FileUpload1.FileName).ToLower) Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_jpg", "MSG") & "');", True)
    '            Exit Sub
    '        End If

    '        Dim len As Integer = FileUpload1.FileContent.Length
    '        If len > MAX_FILE_LENGTH_MB * 1000000 Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_notover", "MSG") & "');", True)
    '            Return
    '        End If

    '        If fileName <> String.Empty Then
    '            'If fileName.Split(".")(1).ToLower = "jpg" Then
    '            Call UploadFileTemp(fileName.Split(".")(1))
    '            Dim TempDate As String = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss")
    '            imgPic.ImageUrl = strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & "0." & fileName.Split(".")(1) & "?id=" & TempDate.Substring(TempDate.Length - 2)
    '            imgPic.Attributes.Add("onClick", "CallLoadHrefNewtab('" & strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & "0." & fileName.Split(".")(1) & "?id=" & TempDate.Substring(TempDate.Length - 2) & "');")
    '            'Else
    '            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_jpg", "MSG") & "');", True)
    '            'End If
    '        End If
    '    End If
    'End Sub

    'Protected Sub btnDeletePicture_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeletePicture.Click
    '    Call DeleteSigleFileTemp()
    '    imgPic.ImageUrl = String.Empty
    '    imgPic.Attributes.Remove("onClick")
    'End Sub

    Protected Sub btnGridAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGridAdd.Click
        Try
            Call CallLoadGridView()

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "FocusSet('" & hddpClientID.Value & "');", True)

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnGridAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnAddDataUnit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddDataUnit.Click
        Try
            hddReloadGrid.Value = String.Empty

            'If txtaUnitFrom.Text = String.Empty Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_please_select", "MSG") & " " & GetResource("lblaUnitFrom", "Text", hddParameterMenuID.Value) & "');", True)
            '    Exit Sub
            'End If
            'If txtaUnitTo.Text = String.Empty Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_please_select", "MSG") & " " & GetResource("lblaUnitTo", "Text", hddParameterMenuID.Value) & "');", True)
            '    Exit Sub
            'End If

            Dim dt As New DataTable
            Dim bl As New cZone
            Dim lst As List(Of Zone_ViewModel) = bl.LoadED03UNITBetween(ddlaProject.SelectedValue,
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

                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("resUsedED11PAJ1", "MSG") & " " & pUsed & "');", True)
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

                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_duplicate_table", "MSG") & " " & pDup & "');", True)
                        Exit Sub
                    Else
                        hddIDRealTime.Value = String.Empty
                        grdView2.DataSource = dt
                        grdView2.DataBind()
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_not_found", "MSG") & "');", True)
                    Exit Sub
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_not_found", "MSG") & "');", True)
                Exit Sub
            End If

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnReloadProject_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReloadProject.Click
        ddlaPhase.SelectedIndex = 0
        hddIDRealTime.Value = String.Empty
        grdView2.DataSource = Nothing
        grdView2.DataBind()
        CallLoadGridView()
    End Sub

    Protected Sub btnReloadPhase_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReloadPhase.Click
        txtaZoneCode.Text = String.Empty
        hddIDRealTime.Value = String.Empty
        grdView2.DataSource = Nothing
        grdView2.DataBind()
        CallLoadGridView()
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
                    Dim hddgStatus As HiddenField = CType(e.Row.FindControl("hddgStatus"), HiddenField)
                    Dim txtgUnit As TextBox = CType(e.Row.FindControl("txtgUnit"), TextBox)
                    'Dim txtgLeft As TextBox = CType(e.Row.FindControl("txtgLeft"), TextBox)
                    'Dim txtgRight As TextBox = CType(e.Row.FindControl("txtgRight"), TextBox)
                    Dim txtgStatus As TextBox = CType(e.Row.FindControl("txtgStatus"), TextBox)
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
                    'If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FMLEFT")) Then
                    '    If DataBinder.Eval(e.Row.DataItem, "FMLEFT").ToString <> String.Empty Then
                    '        txtgLeft.Text = String.Format("{0:N2}", CDec(DataBinder.Eval(e.Row.DataItem, "FMLEFT")))
                    '    End If
                    'End If
                    'If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FMTOP")) Then
                    '    If DataBinder.Eval(e.Row.DataItem, "FMTOP").ToString <> String.Empty Then
                    '        txtgRight.Text = String.Format("{0:N2}", CDec(DataBinder.Eval(e.Row.DataItem, "FMTOP")))
                    '    End If
                    'End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FRESTATUS")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FRESTATUS").ToString <> String.Empty Then
                            txtgStatus.Text = DataBinder.Eval(e.Row.DataItem, "FRESTATUS")
                            hddgStatus.Value = DataBinder.Eval(e.Row.DataItem, "FRESTATUS")
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
                        'txtgLeft.Attributes.Add("onclick", "addRowGridView('" & txtgLeft.ClientID & "');")
                        'txtgRight.Attributes.Add("onclick", "addRowGridView('" & txtgRight.ClientID & "');")
                        txtgStatus.Attributes.Add("onclick", "addRowGridView('" & txtgStatus.ClientID & "');")
                        btnDelete.Visible = False
                    ElseIf hddFlagSetAdd.Value = "0" Then
                        txtgUnit.Attributes.Add("onblur", "CheckDataDup('" & txtgUnit.ClientID & "'," &
                                                                       "'" & hddIDRealTime.Value & "'," &
                                                                       "'" & txtgStatus.ClientID & "'" &
                                                                       ");")
                        txtgUnit.Attributes.Remove("onclick")
                        'txtgLeft.Attributes.Remove("onclick")
                        'txtgRight.Attributes.Remove("onclick")
                        txtgStatus.Attributes.Remove("onclick")
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
                        If Session("ORG_Zone_Datatable_Delete") IsNot Nothing Then
                            dts = Session("ORG_Zone_Datatable_Delete")
                        End If
                        For Each i As DataRow In dt.Select("ID= '" & e.CommandArgument & "'")
                            dr = dts.NewRow
                            dr.Item("FSERIALNO") = i.Item("FSERIALNO").ToString
                            dts.Rows.Add(dr)
                            dt.Rows.Remove(i)
                        Next

                        Session.Add("ORG_Zone_Datatable_Delete", Nothing)
                        If dts IsNot Nothing Then
                            If dts.Rows.Count > 0 Then
                                Session.Add("ORG_Zone_Datatable_Delete", dts)
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
        'dt.Columns.Add("FMLEFT")
        'dt.Columns.Add("FMTOP")
        dt.Columns.Add("FRESTATUS")
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
                    Dim hddgStatus As HiddenField = CType(e.FindControl("hddgStatus"), HiddenField)
                    Dim txtgUnit As TextBox = CType(e.FindControl("txtgUnit"), TextBox)
                    'Dim txtgLeft As TextBox = CType(e.FindControl("txtgLeft"), TextBox)
                    'Dim txtgRgiht As TextBox = CType(e.FindControl("txtgRight"), TextBox)
                    Dim txtgStatus As TextBox = CType(e.FindControl("txtgStatus"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                    Dim hddFlagDup As HiddenField = CType(e.FindControl("hddFlagDup"), HiddenField)

                    dr = dt.NewRow
                    strID = CInt(hddID.Value)
                    dr.Item("ID") = hddID.Value
                    dr.Item("FSERIALNO") = txtgUnit.Text
                    'If txtgLeft.Text <> String.Empty Then
                    '    dr.Item("FMLEFT") = String.Format("{0:N2}", CDec(txtgLeft.Text))
                    'End If
                    'If txtgRgiht.Text <> String.Empty Then
                    '    dr.Item("FMTOP") = String.Format("{0:N2}", CDec(txtgRgiht.Text))
                    'End If
                    dr.Item("FRESTATUS") = hddgStatus.Value

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
                'dr.Item("FMLEFT") = String.Empty
                'dr.Item("FMTOP") = String.Empty
                dr.Item("FRESTATUS") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dr.Item("FlagDup") = String.Empty
                dt.Rows.Add(dr)
            Else
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("FSERIALNO") = String.Empty
                'dr.Item("FMLEFT") = String.Empty
                'dr.Item("FMTOP") = String.Empty
                dr.Item("FRESTATUS") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dt.Rows.Add(dr)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "AddDataTable", ex)
        End Try
    End Sub
    Public Sub SetDatatable(ByRef dt As DataTable,
                            ByVal lc As List(Of Zone_ViewModel))
        Call CreateDatatable(dt)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            For Each ilc As Zone_ViewModel In lc
                dr = dt.NewRow
                strID = strID + 1
                dr.Item("ID") = strID
                dr.Item("FSERIALNO") = ilc.FSERIALNO
                'If ilc.FMLEFT IsNot Nothing Then
                '    dr.Item("FMLEFT") = String.Format("{0:N2}", CDec(ilc.FMLEFT))
                'End If
                'If ilc.FMTOP IsNot Nothing Then
                '    dr.Item("FMTOP") = String.Format("{0:N2}", CDec(ilc.FMTOP))
                'End If
                dr.Item("FRESTATUS") = ilc.FRESTATUS
                dr.Item("FlagSetAdd") = "0"
                dr.Item("FlagDup") = String.Empty
                dt.Rows.Add(dr)
            Next
            dr = dt.NewRow
            dr.Item("ID") = strID + 1
            dr.Item("FSERIALNO") = String.Empty
            'dr.Item("FMLEFT") = String.Empty
            'dr.Item("FMTOP") = String.Empty
            dr.Item("FRESTATUS") = String.Empty
            dr.Item("FlagSetAdd") = "1"
            dr.Item("FlagDup") = String.Empty
            dt.Rows.Add(dr)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "SetDatatable", ex)
        End Try
    End Sub
    Public Sub SetDatatableDataUnit(ByRef dt As DataTable,
                                    ByVal lc As List(Of Zone_ViewModel))

        Dim dr As DataRow
        Dim strID As Integer = 0
        Dim strUsedED11PAJ1 As List(Of String) = Nothing
        Call CreateDatatable(dt)

        Try
            If grdView2 IsNot Nothing Then
                For Each e As GridViewRow In grdView2.Rows
                    Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                    Dim hddgStatus As HiddenField = CType(e.FindControl("hddgStatus"), HiddenField)
                    Dim txtgUnit As TextBox = CType(e.FindControl("txtgUnit"), TextBox)
                    'Dim txtgLeft As TextBox = CType(e.FindControl("txtgLeft"), TextBox)
                    'Dim txtgRgiht As TextBox = CType(e.FindControl("txtgRight"), TextBox)
                    Dim txtgStatus As TextBox = CType(e.FindControl("txtgStatus"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                    Dim hddFlagDup As HiddenField = CType(e.FindControl("hddFlagDup"), HiddenField)
                    If hddFlagSetAdd.Value = "0" Then
                        If txtgUnit.Text <> String.Empty Or
                           txtgStatus.Text <> String.Empty Then
                            dr = dt.NewRow
                            strID = CInt(hddID.Value)
                            dr.Item("ID") = hddID.Value
                            dr.Item("FSERIALNO") = txtgUnit.Text
                            'If txtgLeft.Text <> String.Empty Then
                            '    dr.Item("FMLEFT") = String.Format("{0:N2}", CDec(txtgLeft.Text))
                            'End If
                            'If txtgRgiht.Text <> String.Empty Then
                            '    dr.Item("FMTOP") = String.Format("{0:N2}", CDec(txtgRgiht.Text))
                            'End If
                            dr.Item("FRESTATUS") = hddgStatus.Value

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
            For Each ilc As Zone_ViewModel In lc
                If dt.Select("FSERIALNO = '" & ilc.FSERIALNO & "'").Length = 0 Then
                    dr = dt.NewRow
                    strID = strID + 1
                    dr.Item("ID") = strID
                    dr.Item("FSERIALNO") = ilc.FSERIALNO
                    'If ilc.FMLEFT IsNot Nothing Then
                    '    dr.Item("FMLEFT") = String.Format("{0:N2}", CDec(ilc.FMLEFT))
                    'End If
                    'If ilc.FMTOP IsNot Nothing Then
                    '    dr.Item("FMTOP") = String.Format("{0:N2}", CDec(ilc.FMTOP))
                    'End If
                    dr.Item("FRESTATUS") = ilc.FRESTATUS
                    dr.Item("FlagSetAdd") = "0"

                    dr.Item("FlagDup") = String.Empty
                    dt.Rows.Add(dr)
                ElseIf dt.Select("FSERIALNO = '" & ilc.FSERIALNO & "'").Length > 0 Then
                    dr = dt.NewRow
                    strID = strID + 1
                    dr.Item("ID") = strID
                    dr.Item("FSERIALNO") = ilc.FSERIALNO
                    'If ilc.FMLEFT IsNot Nothing Then
                    '    dr.Item("FMLEFT") = String.Format("{0:N2}", CDec(ilc.FMLEFT))
                    'End If
                    'If ilc.FMTOP IsNot Nothing Then
                    '    dr.Item("FMTOP") = String.Format("{0:N2}", CDec(ilc.FMTOP))
                    'End If
                    dr.Item("FRESTATUS") = ilc.FRESTATUS
                    dr.Item("FlagSetAdd") = "0"

                    dr.Item("FlagDup") = "1"
                    dt.Rows.Add(dr)
                End If
            Next
            dr = dt.NewRow
            dr.Item("ID") = strID + 1
            dr.Item("FSERIALNO") = String.Empty
            'dr.Item("FMLEFT") = String.Empty
            'dr.Item("FMTOP") = String.Empty
            dr.Item("FRESTATUS") = String.Empty
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
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "SetDatatable", ex)
        End Try
    End Sub
    Public Sub GetDatatable(ByRef dt As DataTable)
        Try
            Dim dr As DataRow
            For Each e As GridViewRow In grdView2.Rows
                Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                Dim txtgUnit As TextBox = CType(e.FindControl("txtgUnit"), TextBox)
                'Dim txtgLeft As TextBox = CType(e.FindControl("txtgLeft"), TextBox)
                'Dim txtgRgiht As TextBox = CType(e.FindControl("txtgRight"), TextBox)
                Dim txtgStatus As TextBox = CType(e.FindControl("txtgStatus"), TextBox)
                Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                Dim hddFlagDup As HiddenField = CType(e.FindControl("hddFlagDup"), HiddenField)
                dr = dt.NewRow
                dr.Item("ID") = hddID.Value
                dr.Item("FSERIALNO") = txtgUnit.Text
                'dr.Item("FMLEFT") = txtgLeft.Text
                'dr.Item("FMTOP") = txtgRgiht.Text
                dr.Item("FRESTATUS") = txtgStatus.Text
                dr.Item("FlagSetAdd") = hddFlagSetAdd.Value
                dr.Item("FlagDup") = hddFlagDup.Value
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

#Region "Session"
    Public Function GetDataZone() As List(Of Zone_ViewModel)
        Try
            Return Session("IDOCS.application.LoaddataZone")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataZone(ByVal lc As List(Of Zone_ViewModel)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataZone", lc)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

    '#Region "UploadFileTemp"
    '    Public Sub CrateFolderTemp()
    '        Dim strPath As String = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\")
    '        If (Not System.IO.Directory.Exists(strPath)) Then
    '            System.IO.Directory.CreateDirectory(strPath)
    '        End If
    '    End Sub
    '    Public Sub UploadFileTemp(ByVal strLFileName As String)
    '        Call CrateFolderTemp()
    '        Dim strID As Integer = 0
    '        Dim TempSave As String = String.Empty
    '        Dim TempDelete As String = String.Empty
    '        Dim objFSO As Object
    '        Dim objFolder As Object
    '        Dim objFile As Object

    '        Try
    '            objFSO = CreateObject("Scripting.FileSystemObject")
    '            objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\"))
    '            For Each objFile In objFolder.Files
    '                TempDelete = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\" & objFile.Name)
    '                If System.IO.File.Exists(TempDelete) Then
    '                    System.IO.File.Delete(TempDelete)
    '                End If
    '            Next objFile

    '            TempSave = Server.MapPath("Uploads/" & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/0." & strLFileName)
    '            FileUpload1.PostedFile.SaveAs(TempSave)
    '        Catch ex As Exception

    '        End Try


    '    End Sub
    '    Public Sub DeleteAllFileTemp()
    '        Dim objFSO As Object
    '        Dim objFolder As Object
    '        Dim objFile As Object

    '        Try
    '            objFSO = CreateObject("Scripting.FileSystemObject")
    '            objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\"))
    '            For Each objFile In objFolder.Files
    '                Dim TempDelete As String = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\" & objFile.Name)
    '                If System.IO.File.Exists(TempDelete) Then
    '                    System.IO.File.Delete(TempDelete)
    '                End If
    '            Next objFile
    '        Catch ex As Exception

    '        End Try

    '        Dim strPath As String = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\")

    '        If System.IO.Directory.Exists(strPath) Then
    '            System.IO.Directory.Delete(strPath)
    '        End If

    '    End Sub
    '    Public Sub DeleteSigleFileTemp()
    '        Dim objFSO As Object
    '        Dim objFolder As Object
    '        Dim objFile As Object
    '        Try
    '            objFSO = CreateObject("Scripting.FileSystemObject")
    '            objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\"))
    '            For Each objFile In objFolder.Files
    '                Dim TempDelete As String = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\" & objFile.Name)
    '                If System.IO.File.Exists(TempDelete) Then
    '                    System.IO.File.Delete(TempDelete)
    '                End If
    '            Next objFile
    '        Catch ex As Exception

    '        End Try
    '    End Sub
    '#End Region

#Region "UploadFileProcess"
    Public Sub CopyFileTempToProcess()
        Dim strKeyID As String = ddlaProject.SelectedValue & "-" & ddlaPhase.SelectedValue & "-" & txtaZoneCode.Text & "-0"
        Call DeleteAllFile(strKeyID)
        Call CrateFolder(strKeyID)
        Call UploadFile(strKeyID)
        'Call CopyFile(strKeyID)
    End Sub
    Public Sub CrateFolder(ByVal strKeyID As String)
        Dim strPath As String = Server.MapPath("IMG_SPW\" & hddParameterMenuID.Value & "\")
        If (Not System.IO.Directory.Exists(strPath)) Then
            System.IO.Directory.CreateDirectory(strPath)
        End If
    End Sub
    Public Sub DeleteAllFile(ByVal strKeyID As String)
        Dim objFSO As Object
        Dim objFolder As Object
        Dim objFile As Object
        Try
            objFSO = CreateObject("Scripting.FileSystemObject")
            objFolder = objFSO.GetFolder(Server.MapPath("IMG_SPW\" & hddParameterMenuID.Value & "\"))
            For Each objFile In objFolder.Files
                Dim strCheckFileName As String = objFile.Name.ToString.Split(".")(0)
                Dim TempDelete As String = Server.MapPath("IMG_SPW\" & hddParameterMenuID.Value & "\" & objFile.Name)
                If strKeyID = strCheckFileName Then
                    If System.IO.File.Exists(TempDelete) Then
                        System.IO.File.Delete(TempDelete)
                    End If
                End If
            Next objFile
        Catch ex As Exception

        End Try
    End Sub
    Public Sub CopyFile(ByVal strKeyID As String)
        Dim TempSave As String = String.Empty
        Dim TempPath As String = String.Empty
        Dim objFSO As Object
        Dim objFolder As Object
        Dim objFile As Object
        Try
            objFSO = CreateObject("Scripting.FileSystemObject")
            objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\"))
            For Each objFile In objFolder.Files
                TempSave = Server.MapPath("IMG_SPW/" & hddParameterMenuID.Value & "/" & strKeyID)
                TempPath = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\" & objFile.Name)
                If System.IO.File.Exists(TempPath) Then
                    System.IO.File.Copy(TempPath, TempSave & "." & objFile.Name.ToString.Split(".")(1))
                End If
            Next objFile

        Catch ex As Exception

        End Try

    End Sub

    Public Sub UploadFile(ByVal strKeyID As String)
        Dim TempSave As String = String.Empty
        Try
            TempSave = Server.MapPath("IMG_SPW/" & hddParameterMenuID.Value & "/" & strKeyID)
            FileUpload.PostedFile.SaveAs(TempSave & "." & FileUpload.FileName.ToString.Split(".")(1))
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LoadFileProcess()
        Dim strKeyID As String = ddlaProject.SelectedValue & "-" & ddlaPhase.SelectedValue & "-" & txtaZoneCode.Text & "-0"
        Dim objFSO As Object
        Dim objFolder As Object
        Dim objFile As Object
        Try
            objFSO = CreateObject("Scripting.FileSystemObject")
            objFolder = objFSO.GetFolder(Server.MapPath("IMG_SPW\" + hddParameterMenuID.Value + "\"))
            For Each objFile In objFolder.Files
                Dim strCheckName As String = objFile.Name.ToString.Split(".")(0)
                If strKeyID = strCheckName Then
                    Dim TempDate As String = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss")
                    imga.ImageUrl = strPathServerIMG & hddParameterMenuID.Value & "/" & strKeyID & "." & objFile.Name.ToString.Split(".")(1) & "?id=" & TempDate.Substring(TempDate.Length - 2)
                    imga.Attributes.Add("onClick", "CallLoadHrefNewtab('" & strPathServerIMG & hddParameterMenuID.Value & "/" & strKeyID & "." & objFile.Name.ToString.Split(".")(1) & "?id=" & TempDate.Substring(TempDate.Length - 2) & "');")
                End If
            Next objFile

        Catch ex As Exception

        End Try

    End Sub
#End Region

    '#Region "LoadFileForEdit"
    '    Public Sub LoadFileProcessToTemp()
    '        Call CrateFolderTemp()
    '        Call LoadFileProcess()
    '    End Sub
    '    Public Sub LoadFileProcess()

    '        Dim strKeyID As String = ddlaProject.SelectedValue & "-" & ddlaPhase.SelectedValue & "-" & txtaZoneCode.Text & "-0"
    '        Dim objFSO As Object
    '        Dim objFolder As Object
    '        Dim objFile As Object
    '        Try
    '            objFSO = CreateObject("Scripting.FileSystemObject")
    '            objFolder = objFSO.GetFolder(Server.MapPath("IMG_SPW\" + hddParameterMenuID.Value + "\"))
    '            For Each objFile In objFolder.Files
    '                Dim strCheckName As String = objFile.Name.ToString.Split(".")(0)
    '                If strKeyID = strCheckName Then
    '                    Dim ProcessPath As String = Server.MapPath("IMG_SPW/" + hddParameterMenuID.Value + "/" & objFile.Name)
    '                    Dim TempSave As String = Server.MapPath("Uploads/" + hddParameterMenuID.Value + "/" & Me.CurrentUser.UserID & "/0." & objFile.Name.ToString.Split(".")(1))
    '                    If Not System.IO.File.Exists(TempSave) Then
    '                        System.IO.File.Copy(ProcessPath, TempSave)
    '                    End If
    '                    Dim TempDate As String = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss")
    '                    imgPic.ImageUrl = strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & "0." & objFile.Name.ToString.Split(".")(1) & "?id=" & TempDate.Substring(TempDate.Length - 2)
    '                    imgPic.Attributes.Add("onClick", "CallLoadHrefNewtab('" & strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & "0." & objFile.Name.ToString.Split(".")(1) & "?id=" & TempDate.Substring(TempDate.Length - 2) & "');")
    '                End If
    '            Next objFile

    '        Catch ex As Exception

    '        End Try

    '    End Sub
    '#End Region

#Region "Check Master Delete"
    Public Sub getUsedMaster()
        Try
            Dim bl As cZone = New cZone
            Dim lc As List(Of String) = bl.getUsedMasterORG(ddlsProject.SelectedValue,
                                                            ddlsPhase.SelectedValue)
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
                                                                       txtaZoneCode.Text)
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
                                                                      txtaZoneCode.Text)

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
Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class MST_Zone
    Inherits BasePage
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Page.DataBind()
                Session.Remove("IDOCS.application.LoaddataZone")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("MST_Zone.aspx")
                Me.ClearSessionPageLoad("MST_Zone.aspx")

                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
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

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        Call Redirect("MST_Zone.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("MST_Zone_FS")
        Session.Add("MST_Zone_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("MST_Zone_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("MST_Zone_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("MST_Zone_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("MST_Zone_FS") IsNot Nothing Then
            If Session("MST_Zone_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("MST_Zone_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
            End If
        End If
        If Session("MST_Zone_PageInfo") IsNot Nothing Then
            If Session("MST_Zone_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("MST_Zone_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("MST_Zone_Search") IsNot Nothing Then
            If Session("MST_Zone_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("MST_Zone_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("MST_Zone_pageLength") IsNot Nothing Then
            If Session("MST_Zone_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("MST_Zone_pageLength").ToString
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

        lblaZoneCode.Text = Me.GetResource("lblaZoneCode", "Text", hddParameterMenuID.Value)
        lblaZoneName.Text = Me.GetResource("lblaZoneName", "Text", hddParameterMenuID.Value)
        lblaArea.Text = Me.GetResource("lblaArea", "Text", hddParameterMenuID.Value)

        TextHd1.Text = Me.GetResource("No", "Text", "1")
        TextHd2.Text = Me.GetResource("ZoneCode", "Text", hddParameterMenuID.Value)
        TextHd3.Text = Me.GetResource("ZoneArea", "Text", hddParameterMenuID.Value)
        TextHd4.Text = Me.GetResource("ZoneName", "Text", hddParameterMenuID.Value)
        TextHd5.Text = Me.GetResource("col_edit", "Text", "1")
        TextHd6.Text = Me.GetResource("col_delete", "Text", "1")

        TextFt1.Text = Me.GetResource("No", "Text", "1")
        TextFt2.Text = Me.GetResource("ZoneCode", "Text", hddParameterMenuID.Value)
        TextFt3.Text = Me.GetResource("ZoneArea", "Text", hddParameterMenuID.Value)
        TextFt4.Text = Me.GetResource("ZoneName", "Text", hddParameterMenuID.Value)
        TextFt5.Text = Me.GetResource("col_edit", "Text", "1")
        TextFt6.Text = Me.GetResource("col_delete", "Text", "1")

        'lblsSave.Text = Me.GetResource("btn_save", "Text", "1")
        'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

        lblMassage1.Text = Me.GetResource("msg_required", "MSG")
        lblMassage2.Text = Me.GetResource("msg_required", "MSG")
        lblMassage3.Text = Me.GetResource("msg_required", "MSG")

        lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG")
        lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG") & " " & Me.GetResource("ZoneCode", "Text", hddParameterMenuID.Value)

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
            Dim bl As cZone = New cZone
            fillter.Keyword = ""
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType

            Dim lst As List(Of LD07SLRT) = bl.LoaddataZone(fillter,
                                                       Me.TotalRow,
                                                       Me.CurrentUser.UserID)
            If lst IsNot Nothing Then
                Call SetDataZone(lst)
            Else
                Call SetDataZone(Nothing)
            End If
            Session.Remove("MST_Zone_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)
            Session.Remove("MST_Zone_FS")
        End Try
    End Sub
#End Region

#Region "Event"

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            hddKeyID.Value = ""
            txtaZoneCode.Text = ""
            txtaZoneName.Text = ""

            txtaZoneCode.Enabled = True
            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try
            Dim code As String = Convert.ToString(hddKeyID.Value)
            Dim bl As cZone = New cZone
            Dim lc As LD07SLRT = bl.GetZoneByID(code,
                                                Me.CurrentUser.UserID)
            If lc IsNot Nothing Then
                hddKeyID.Value = lc.FSLROUTE
                txtaZoneCode.Text = lc.FSLROUTE
                txtaZoneName.Text = lc.FSLROUTENM
                txtaArea.Text = lc.FSLAREA

                txtaZoneCode.Enabled = False
            End If

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim code As String = Convert.ToString(hddKeyID.Value)
            Dim bl As cZone = New cZone
            Dim data As LD07SLRT = bl.GetZoneByID(code,
                                                   Me.CurrentUser.UserID)
            If data IsNot Nothing Then
                If Not bl.ZoneDelete(code,
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
            Dim succ As Boolean = True
            Dim bl As cZone = New cZone
            Dim data As LD07SLRT = Nothing
            If hddKeyID.Value <> "" Then
                If Not bl.ZonesEdit(hddKeyID.Value,
                                    txtaZoneName.Text,
                                    txtaArea.Text,
                                    Me.CurrentUser.UserID) Then
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                Else
                    Call LoadRedirec("1")
                End If
            Else

                Dim lc As LD07SLRT = bl.GetZoneByID(txtaZoneCode.Text,
                                                         Me.CurrentUser.UserID)
                If lc Is Nothing Then
                    If Not bl.ZoneAdd(txtaZoneCode.Text,
                                      txtaZoneName.Text,
                                      txtaArea.Text,
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
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnCencel_Click", ex)
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
    End Sub
#End Region

#Region "Session"
    Public Function GetDataZone() As List(Of LD07SLRT)
        Try
            Return Session("IDOCS.application.LoaddataZone")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataZone(ByVal lcZone As List(Of LD07SLRT)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataZone", lcZone)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Check Master Delete"
    Public Sub getUsedMaster()
        Try
            Dim bl As cZone = New cZone
            Dim lc As List(Of String) = bl.getUsedMaster()
            hddCheckUsedMaster.Value = String.Empty
            If lc IsNot Nothing Then
                If lc.Count > 0 Then
                    hddCheckUsedMaster.Value = String.Join(",", lc)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadRadioButtonList", ex)
        End Try
    End Sub
#End Region

End Class
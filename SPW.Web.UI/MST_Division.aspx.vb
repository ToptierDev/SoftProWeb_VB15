Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class MST_Division
    Inherits BasePage
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Page.DataBind()
                Session.Remove("IDOCS.application.LoaddataDivision")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("MST_Division.aspx")
                Me.ClearSessionPageLoad("MST_Division.aspx")
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
        Call Redirect("MST_Division.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("MST_Division_FS")
        Session.Add("MST_Division_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("MST_Division_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("MST_Division_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("MST_Division_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("MST_Division_FS") IsNot Nothing Then
            If Session("MST_Division_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("MST_Division_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG", "1") & "');", True)
                End If
            End If
        End If
        If Session("MST_Division_PageInfo") IsNot Nothing Then
            If Session("MST_Division_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("MST_Division_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("MST_Division_Search") IsNot Nothing Then
            If Session("MST_Division_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("MST_Division_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("MST_Division_pageLength") IsNot Nothing Then
            If Session("MST_Division_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("MST_Division_pageLength").ToString
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

        'lblaDivisionCode.Text = Me.GetResource("lblaDivisionCode", "Text", hddParameterMenuID.Value)
        lblaDivisionNameTH.Text = Me.GetResource("lblaDivisionNameTH", "Text", hddParameterMenuID.Value)
        lblaDivisionNameEN.Text = Me.GetResource("lblaDivisionNameEN", "Text", hddParameterMenuID.Value)
        lblaMG.Text = Me.GetResource("lblaMG", "Text", hddParameterMenuID.Value)
        lblaAddress1.Text = Me.GetResource("lblaAddress1", "Text", hddParameterMenuID.Value)
        lblaAddress2.Text = Me.GetResource("lblaAddress2", "Text", hddParameterMenuID.Value)
        lblaAddress3.Text = Me.GetResource("lblaAddress3", "Text", hddParameterMenuID.Value)
        lblaPostal.Text = Me.GetResource("lblaPostal", "Text", hddParameterMenuID.Value)
        lblsTaxNo.Text = Me.GetResource("lblsTaxNo", "Text", hddParameterMenuID.Value)
        lblaSocialID.Text = Me.GetResource("lblaSocialID", "Text", hddParameterMenuID.Value)
        lblaSocialBranch.Text = Me.GetResource("lblaSocialBranch", "Text", hddParameterMenuID.Value)

        TextHd1.Text = Me.GetResource("No", "Text", "1")
        'TextHd2.Text = Me.GetResource("TextHd2", "Text", hddParameterMenuID.Value)
        TextHd2.Text = grtt("resComID")
        TextHd3.Text = Me.GetResource("TextHd3", "Text", hddParameterMenuID.Value)
        TextHd4.Text = Me.GetResource("TextHd4", "Text", hddParameterMenuID.Value)
        TextHd5.Text = Me.GetResource("TextHd5", "Text", hddParameterMenuID.Value)
        TextHd6.Text = Me.GetResource("TextHd6", "Text", hddParameterMenuID.Value)
        TextHd7.Text = Me.GetResource("TextHd7", "Text", hddParameterMenuID.Value)
        TextHd8.Text = Me.GetResource("TextHd8", "Text", hddParameterMenuID.Value)
        TextHd9.Text = Me.GetResource("col_edit", "Text", "1")
        TextHd10.Text = Me.GetResource("col_delete", "Text", "1")

        TextFt1.Text = Me.GetResource("No", "Text", "1")
        'TextFt2.Text = Me.GetResource("TextFt2", "Text", hddParameterMenuID.Value)
        TextFt2.Text = grtt("resComID")
        TextFt3.Text = Me.GetResource("TextFt3", "Text", hddParameterMenuID.Value)
        TextFt4.Text = Me.GetResource("TextFt4", "Text", hddParameterMenuID.Value)
        TextFt5.Text = Me.GetResource("TextFt5", "Text", hddParameterMenuID.Value)
        TextFt6.Text = Me.GetResource("TextFt6", "Text", hddParameterMenuID.Value)
        TextFt7.Text = Me.GetResource("TextFt7", "Text", hddParameterMenuID.Value)
        TextFt8.Text = Me.GetResource("TextFt8", "Text", hddParameterMenuID.Value)
        TextFt9.Text = Me.GetResource("col_edit", "Text", "1")
        TextFt10.Text = Me.GetResource("col_delete", "Text", "1")

        'lblsSave.Text = Me.GetResource("btn_save", "Text", "1")
        'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

        'lblMassage1.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage2.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage3.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage4.Text = Me.GetResource("msg_required", "MSG", "1")

        lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG", "1")
        lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG", "1") & " " & grtt("resComID")

        hddMSGSaveData.Value = Me.GetResource("msg_save_data", "MSG", "1")
        hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG", "1")
        hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG", "1")
        hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG", "1")
        hddMSGAddData.Value = Me.GetResource("msg_add_data", "MSG", "1")

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
            Dim bl As cDivision = New cDivision
            fillter.Keyword = ""
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType

            Dim lst As List(Of BD10DIVI) = bl.LoaddataDivision(fillter,
                                                               Me.TotalRow,
                                                               Me.CurrentUser.UserID)
            If lst IsNot Nothing Then
                Call SetDataDivision(lst)
            Else
                Call SetDataDivision(Nothing)
            End If
            Session.Remove("MST_Division_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)
            Session.Remove("MST_Division_FS")
        End Try
    End Sub
#End Region

#Region "Event"

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            'txtaDivisionCode.Text = String.Empty
            txtaComID.Text = String.Empty
            txtaDivisionNameTH.Text = String.Empty
            txtaDivisionNameEN.Text = String.Empty
            txtaMG.Text = String.Empty
            txtaAddress1.Text = String.Empty
            txtaAddress2.Text = String.Empty
            txtaAddress3.Text = String.Empty
            txtaPostal.Text = String.Empty
            txtsTaxNo.Text = String.Empty
            txtaSocialID.Text = String.Empty
            txtaSocialBranch.Text = String.Empty

            'txtaDivisionCode.Enabled = True

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try
            Dim code As String = Convert.ToString(hddKeyID.Value)
            Dim bl As cDivision = New cDivision
            Dim lc As BD10DIVI = bl.GetDivisionByID(code,
                                                    Me.CurrentUser.UserID)
            If lc IsNot Nothing Then
                'txtaDivisionCode.Text = lc.FDIVCODE
                txtaComID.Text = lc.COMID
                txtaDivisionNameTH.Text = lc.FDIVNAME
                txtaDivisionNameEN.Text = lc.FDIVNAMET
                txtaMG.Text = lc.FDIVMG
                txtaAddress1.Text = lc.FADD1
                txtaAddress2.Text = lc.FADD2
                txtaAddress3.Text = lc.FADD3
                txtaPostal.Text = lc.FPOSTAL
                txtsTaxNo.Text = lc.FCOMPTAXNO
                txtaSocialID.Text = lc.FSOCIALNO
                txtaSocialBranch.Text = lc.FSCBRANCH

                'txtaDivisionCode.Enabled = False
                txtaComID.Enabled = True
                If txtaComID.Text = String.Empty Then
                    txtaComID.Enabled = True
                Else
                    Dim lcs As List(Of CoreUsersCompany) = bl.GetDivisionUserCompanyByComID(txtaComID.Text,
                                                                                            Me.CurrentUser.UserID)
                    If lcs IsNot Nothing Then
                        If lcs.Count > 0 Then
                            txtaComID.Enabled = False
                        End If
                    End If
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
            Dim bl As cDivision = New cDivision
            Dim data As BD10DIVI = bl.GetDivisionByID(code,
                                                      Me.CurrentUser.UserID)
            If data IsNot Nothing Then
                If Not bl.DivisionDelete(code,
                                         Me.CurrentUser.UserID) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_cannot_delete_because ", "MSG", "1") & "');", True)
                Else
                    Call LoadRedirec("2")
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_deletenotsuccess", "MSG", "1") & "');", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnDelete_Click", ex)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim succ As Boolean = True
            Dim bl As cDivision = New cDivision
            Dim data As BD10DIVI = Nothing
            If hddKeyID.Value <> "" Then
                If Not bl.DivisionsEdit(hddKeyID.Value,
                                        txtaComID.Text,
                                        txtaDivisionNameEN.Text,
                                        txtaDivisionNameTH.Text,
                                        txtaMG.Text,
                                        txtaAddress1.Text,
                                        txtaAddress2.Text,
                                        txtaAddress3.Text,
                                        txtaPostal.Text,
                                        txtsTaxNo.Text,
                                        txtaSocialID.Text,
                                        txtaSocialBranch.Text,
                                        Me.CurrentUser.UserID) Then
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                Else
                    Call LoadRedirec("1")
                End If
            Else

                'Dim lc As BD10DIVI = bl.GetDivisionByID(txtaDivisionCode.Text,
                '                                        Me.CurrentUser.UserID)
                'If lc Is Nothing Then
                Dim lcs As BD10DIVI = bl.GetDivisionByComID(txtaComID.Text,
                                                                Me.CurrentUser.UserID)
                    If lcs Is Nothing Then
                    If Not bl.DivisionAdd("",
                                              txtaComID.Text,
                                              txtaDivisionNameEN.Text,
                                              txtaDivisionNameTH.Text,
                                              txtaMG.Text,
                                              txtaAddress1.Text,
                                              txtaAddress2.Text,
                                              txtaAddress3.Text,
                                              txtaPostal.Text,
                                              txtsTaxNo.Text,
                                              txtaSocialID.Text,
                                              txtaSocialBranch.Text,
                                              Me.CurrentUser.UserID) Then
                        'txtaDivisionCode.Text
                        Call OpenDialog()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                    Else
                        Call LoadRedirec("1")
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & grtt("resDuplicateCOMID") & "');", True)
                    End If
                'Else
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_duplicate", "MSG", "1") & "');", True)
                'End If
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
        'lblMassage1.Style.Add("display", "none")
        lblMassage2.Style.Add("display", "none")
        lblMassage3.Style.Add("display", "none")
        lblMassage4.Style.Add("display", "none")
    End Sub
#End Region

#Region "Session"
    Public Function GetDataDivision() As List(Of BD10DIVI)
        Try
            Return Session("IDOCS.application.LoaddataDivision")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataDivision(ByVal lcDivision As List(Of BD10DIVI)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataDivision", lcDivision)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Check Master Delete"
    Public Sub getUsedMaster()
        Try
            Dim bl As cDivision = New cDivision
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
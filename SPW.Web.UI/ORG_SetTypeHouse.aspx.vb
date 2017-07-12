Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Threading.Tasks
Public Class ORG_SetTypeHouse
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
                Session.Remove("IDOCS.application.LoaddataSetTypeHouse")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("ORG_SetTypeHouse.aspx")
                Me.ClearSessionPageLoad("ORG_SetTypeHouse.aspx")
                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()
                Call getUsedMaster()
                Call Loaddata()
                'Call DeleteAllFileTemp()
                Session.Remove("ORG_SetTypeHouse_Datatable")

                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())
                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "AutocompletedTypeHouse();", True)
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
            Call LoadTypeHouse(ddlsTypeHouse, "S")
            Call LoadTypeHouse(ddlaTypeHouse, "A")
            Call LoadCreateTypeHouse(ddlaTypeCreate, "S")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDropdownlist", ex)
        End Try
    End Sub
    Public Sub LoadCreateTypeHouse(ByVal ddl As DropDownList,
                                   ByVal strType As String)
        Dim bl As cSetTypeHouse = New cSetTypeHouse
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadTypeHouse()
            If lc IsNot Nothing Then
                ddl.DataValueField = "TypeHouseConsCode"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "TypeHouseConsDesc"
                Else
                    ddl.DataTextField = "TypeHouseConsDesc"
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
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex)
        End Try
    End Sub

    Public Sub LoadTypeHouse(ByVal ddl As DropDownList,
                             ByVal strType As String)
        Dim bl As cSetTypeHouse = New cSetTypeHouse
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadTypeHouseSD03TYPE()
            If lc IsNot Nothing Then
                ddl.DataValueField = "FTYCODE"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "FDESC"
                Else
                    ddl.DataTextField = "FDESC"
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
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex)
        End Try
    End Sub
#End Region

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        Call Redirect("ORG_SetTypeHouse.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("ORG_SetTypeHouse_TypeHouse")
        Session.Remove("ORG_SetTypeHouse_FS")
        'Session.Add("ORG_SetTypeHouse_TypeHouse", IIf(txtsTypeHouse.Text <> String.Empty, txtsTypeHouse.Text, ""))
        Session.Add("ORG_SetTypeHouse_TypeHouse", IIf(ddlsTypeHouse.SelectedValue <> String.Empty, ddlsTypeHouse.SelectedValue, ""))
        Session.Add("ORG_SetTypeHouse_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("ORG_SetTypeHouse_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("ORG_SetTypeHouse_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("ORG_SetTypeHouse_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("ORG_SetTypeHouse_TypeHouse") IsNot Nothing Then
            If Session("ORG_SetTypeHouse_TypeHouse").ToString <> String.Empty Then
                Dim strTypeHouse As String = Session("ORG_SetTypeHouse_TypeHouse").ToString
                'txtsTypeHouse.Text = strTypeHouse
                ddlsTypeHouse.SelectedValue = strTypeHouse
            End If
        End If
        If Session("ORG_SetTypeHouse_FS") IsNot Nothing Then
            If Session("ORG_SetTypeHouse_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("ORG_SetTypeHouse_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG", "1") & "');", True)
                End If
            End If
        End If
        If Session("ORG_SetTypeHouse_PageInfo") IsNot Nothing Then
            If Session("ORG_SetTypeHouse_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("ORG_SetTypeHouse_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("ORG_SetTypeHouse_Search") IsNot Nothing Then
            If Session("ORG_SetTypeHouse_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("ORG_SetTypeHouse_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("ORG_SetTypeHouse_pageLength") IsNot Nothing Then
            If Session("ORG_SetTypeHouse_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("ORG_SetTypeHouse_pageLength").ToString
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
        Try
            hddpTypeBrownser.Value = Request.Browser.Type.ToString.Substring(0, 2).ToLower
        Catch ex As Exception

        End Try
        lblMain1.Text = Me.GetResource("main_label", "Text", "1")
        lblMain2.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
        lblMain3.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
        lblMain4.Text = Me.GetResource("HeaderDialog", "Text", hddParameterMenuID.Value)
        lblMain6.Text = Me.GetResource("lblMain6", "Text", hddParameterMenuID.Value)
        lblMain7.Text = Me.GetResource("lblMain7", "Text", hddParameterMenuID.Value)

        lblsTypeHouse.Text = Me.GetResource("lblsTypeHouse", "Text", hddParameterMenuID.Value)

        lblaTypeHouseCode.Text = Me.GetResource("lblaTypeHouseCode", "Text", hddParameterMenuID.Value)
        lblaTypeHouse.Text = Me.GetResource("lblaTypeHouse", "Text", hddParameterMenuID.Value)
        lblaNameEN.Text = Me.GetResource("lblaNameEN", "Text", hddParameterMenuID.Value)
        lblaNameTH.Text = Me.GetResource("lblaNameTH", "Text", hddParameterMenuID.Value)
        lblaNames.Text = Me.GetResource("lblaNames", "Text", hddParameterMenuID.Value)
        chkaBOI.Text = "&nbsp;" & Me.GetResource("chkaBOI", "Text", hddParameterMenuID.Value)
        lblaModel.Text = Me.GetResource("lblaModel", "Text", hddParameterMenuID.Value)
        lblsUnit.Text = Me.GetResource("lblsUnit", "Text", hddParameterMenuID.Value)
        lblaSQW.Text = Me.GetResource("lblaSQW", "Text", hddParameterMenuID.Value)
        lblaSQM.Text = Me.GetResource("lblaSQM", "Text", hddParameterMenuID.Value)
        'lblaSQM2.Text = "&nbsp;" & Me.GetResource("lblaSQM2", "Text", hddParameterMenuID.Value)
        lblaBSQM.Text = Me.GetResource("lblaBSQM", "Text", hddParameterMenuID.Value)
        lblaPSQM.Text = Me.GetResource("lblaPSQM", "Text", hddParameterMenuID.Value)
        lblaMeter.Text = Me.GetResource("lblaMeter", "Text", hddParameterMenuID.Value)
        lblaBedRoom.Text = Me.GetResource("lblaBedRoom", "Text", hddParameterMenuID.Value)
        lblaBathRoom.Text = Me.GetResource("lblaBathRoom", "Text", hddParameterMenuID.Value)
        lblaMeterWater.Text = Me.GetResource("lblaMeterWater", "Text", hddParameterMenuID.Value)
        lblaNew.Text = "&nbsp;" & Me.GetResource("lblaNew", "Text", hddParameterMenuID.Value)

        TextHd1.Text = Me.GetResource("No", "Text", "1")
        TextHd2.Text = Me.GetResource("TypeDescription", "Text", hddParameterMenuID.Value)
        TextHd3.Text = Me.GetResource("TypeHouseCode", "Text", hddParameterMenuID.Value)
        TextHd4.Text = Me.GetResource("TypeHouseName", "Text", hddParameterMenuID.Value)
        TextHd5.Text = Me.GetResource("TypeCreate", "Text", hddParameterMenuID.Value)
        TextHd6.Text = Me.GetResource("col_edit", "Text", "1")
        TextHd7.Text = Me.GetResource("col_delete", "Text", "1")

        TextFt1.Text = Me.GetResource("No", "Text", "1")
        TextFt2.Text = Me.GetResource("TypeDescription", "Text", hddParameterMenuID.Value)
        TextFt3.Text = Me.GetResource("TypeHouseCode", "Text", hddParameterMenuID.Value)
        TextFt4.Text = Me.GetResource("TypeHouseName", "Text", hddParameterMenuID.Value)
        TextFt5.Text = Me.GetResource("TypeCreate", "Text", hddParameterMenuID.Value)
        TextFt6.Text = Me.GetResource("col_edit", "Text", "1")
        TextFt7.Text = Me.GetResource("col_delete", "Text", "1")

        lblsSearch.Text = Me.GetResource("lblsSearch", "Text", hddParameterMenuID.Value)
        'lblsPicture.Text = Me.GetResource("lblsPicture", "Text", hddParameterMenuID.Value)
        'lblsDeletePicture.Text = Me.GetResource("lblsDeletePicture", "Text", hddParameterMenuID.Value)
        'lblsSave.Text = Me.GetResource("btn_save", "Text", "1")
        'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

        lblMassage1.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage6.Text = grtt("resSelectAll")

        lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG", "1")
        lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG", "1") & " " & Me.GetResource("lblaTypeHouseCode", "Text", hddParameterMenuID.Value)

        txtaSQM.Enabled = False
        txtaSQM.BackColor = System.Drawing.ColorTranslator.FromHtml("#F5F5F5")

        hddMSGAddFile.Value = Me.GetResource("msg_add_file", "MSG", "1")
        hddMSGDeleteFile.Value = Me.GetResource("msg_delete_file", "MSG", "1")
        hddMSGSaveData.Value = Me.GetResource("msg_save_data", "MSG", "1")
        hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG", "1")
        hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG", "1")
        hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG", "1")
        hddMSGAddData.Value = Me.GetResource("msg_add_data", "MSG", "1")

        btnhrefAdd.Title = hddMSGAddData.Value
        btnhrefSave.Title = hddMSGSaveData.Value
        btnhrefCancel.Title = hddMSGCancelData.Value
        btnConfrimDelete.InnerText = hddMSGDeleteData.Value
        btnConfrimCancel.InnerText = hddMSGCancelData.Value
        'btnhrefAddPicture.Title = hddMSGAddFile.Value
        'btnhrefAddDelete.Title = hddMSGDeleteFile.Value

        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try
            hddReloadGrid.Value = "1"
            Dim fillter As FillterSearch = New FillterSearch
            Dim bl As cSetTypeHouse = New cSetTypeHouse
            fillter.Keyword = ""
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType

            Dim strTypeHouse As String = String.Empty
            'Call SetCiterail(txtsTypeHouse.Text, strTypeHouse)
            strTypeHouse = ddlsTypeHouse.SelectedValue
            'If strTypeHouse <> String.Empty Then
            Dim lst As List(Of SetTypeHouse_ViewModel) = bl.Loaddata(fillter,
                                                                          Me.TotalRow,
                                                                          Me.CurrentUser.UserID,
                                                                          strTypeHouse,
                                                                          Me.WebCulture.ToUpper)
                If lst IsNot Nothing Then
                    Call SetDataSetTypeHouse(lst)
                Else
                    Call SetDataSetTypeHouse(Nothing)
                End If
            'End If

            Session.Remove("ORG_SetTypeHouse_TypeHouse")
            Session.Remove("ORG_SetTypeHouse_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)

            Session.Remove("ORG_SetTypeHouse_TypeHouse")
            Session.Remove("ORG_SetTypeHouse_FS")
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
    Public Sub ClearText()
        txtaTypeHouseCode.Text = String.Empty

        ddlaTypeCreate.SelectedIndex = 0
        ddlaTypeHouse.SelectedIndex = 0
        'txtaTypeHouse.Text = String.Empty
        txtaNameEN.Text = String.Empty
        txtaNameTH.Text = String.Empty
        txtaNames.Text = String.Empty
        chkaBOI.Checked = False
        txtaModel.Text = String.Empty
        txtaUnitEN.Text = String.Empty
        txtaUnitThai.Text = String.Empty
        txtaSQW.Text = String.Empty
        txtaSQM.Text = String.Empty
        'txtaSQM2.Text = String.Empty
        txtaBSQM.Text = String.Empty
        txtaPSQM.Text = String.Empty
        txtaMeter.Text = String.Empty
        txtaBedRoom.Text = String.Empty
        txtaBathRoom.Text = String.Empty
        txtaMeterWater.Text = String.Empty

        txtaNote.Text = String.Empty
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
            Session.Remove("ORG_SetTypeHouse_Datatable")
            Call ClearText()

            txtaTypeHouseCode.Enabled = True

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try
            Session.Remove("ORG_SetTypeHouse_Datatable")
            Dim dt As New DataTable
            Call ClearText()
            Dim bl As cSetTypeHouse = New cSetTypeHouse
            Dim lc As SD05PDDS = bl.GetByID(hddKeyID.Value,
                                            Me.CurrentUser.UserID)
            If lc IsNot Nothing Then
                hddKeyID.Value = lc.FPDCODE
                txtaTypeHouseCode.Text = lc.FPDCODE
                Try
                    If lc.FDUTYCODE <> String.Empty Then
                        Try
                            ddlaTypeCreate.SelectedValue = lc.FDUTYCODE
                        Catch ex As Exception
                            ddlaTypeCreate.SelectedIndex = 0
                        End Try
                    End If
                Catch ex As Exception
                    'txtaTypeHouse.Text = String.Empty
                    ddlaTypeHouse.SelectedIndex = 0
                End Try
                Try
                    If lc.FTYCODE <> String.Empty Then
                        Dim lstSD As SD03TYPE = bl.LoadTypeHouseEdit(lc.FTYCODE)
                        If lstSD IsNot Nothing Then
                            'txtaTypeHouse.Text = lstSD.FTYCODE & "-" & lstSD.FDESC
                            ddlaTypeHouse.SelectedValue = lstSD.FTYCODE
                        Else
                            'txtaTypeHouse.Text = String.Empty
                            ddlaTypeHouse.SelectedIndex = 0
                        End If
                    End If
                Catch ex As Exception
                    'txtaTypeHouse.Text = String.Empty
                    ddlaTypeHouse.SelectedIndex = 0
                End Try
                txtaNameEN.Text = lc.FPDNAME
                txtaNameTH.Text = lc.FPDNAMET
                txtaNames.Text = lc.FPDSHORTNM
                Try
                    chkaBOI.Checked = IIf(lc.FPTYPE.ToUpper = "Y", True, False)
                Catch ex As Exception
                    chkaBOI.Checked = False
                End Try
                txtaModel.Text = lc.FMODEL
                txtaUnitEN.Text = lc.FUNITM
                txtaUnitThai.Text = lc.FUNITMT
                If Not IsDBNull(lc.FSTDAREA) Then
                    If lc.FSTDAREA IsNot Nothing Then
                        txtaSQW.Text = String.Format("{0:N2}", CDec(lc.FSTDAREA))
                        Dim SQWTempCal As Decimal = lc.FSTDAREA * 4
                        txtaSQM.Text = String.Format("{0:N2}", CDec(SQWTempCal))
                    End If
                End If
                If Not IsDBNull(lc.FSTDBUILT) Then
                    If lc.FSTDBUILT IsNot Nothing Then
                        txtaBSQM.Text = String.Format("{0:N2}", CDec(lc.FSTDBUILT))
                    End If
                End If
                If Not IsDBNull(lc.FPSQM) Then
                    If lc.FPSQM IsNot Nothing Then
                        txtaPSQM.Text = String.Format("{0:N2}", CDec(lc.FPSQM))
                    End If
                End If
                txtaMeter.Text = lc.FEMETERSZ
                If Not IsDBNull(lc.FNOBEDRM) Then
                    If lc.FNOBEDRM IsNot Nothing Then
                        txtaBedRoom.Text = String.Format("{0:N0}", CDec(lc.FNOBEDRM))
                    End If
                End If
                If Not IsDBNull(lc.FNOBATHRM) Then
                    If lc.FNOBATHRM IsNot Nothing Then
                        txtaBathRoom.Text = String.Format("{0:N0}", CDec(lc.FNOBATHRM))
                    End If
                End If
                If Not IsDBNull(lc.FNOBATHRM) Then
                    If lc.FNOBATHRM IsNot Nothing Then
                        txtaBathRoom.Text = String.Format("{0:N0}", CDec(lc.FNOBATHRM))
                    End If
                End If
                If Not IsDBNull(lc.FWMETERSZ) Then
                    If lc.FWMETERSZ IsNot Nothing Then
                        txtaMeterWater.Text = String.Format("{0:N0}", CDec(lc.FWMETERSZ))
                    End If
                End If
                Call LoadImageID()
                Dim lst As SD05PDSP = bl.GetByIDSD05PDSP(hddKeyID.Value,
                                                         Me.CurrentUser.UserID)
                If lst IsNot Nothing Then
                    txtaNote.Text = lst.FDESC
                End If
            End If

            txtaTypeHouseCode.Enabled = False

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim bl As cSetTypeHouse = New cSetTypeHouse
            Dim data As SD05PDDS = bl.GetByID(hddKeyID.Value,
                                              Me.CurrentUser.UserID)
            If data IsNot Nothing Then
                If Not bl.Delete(hddKeyID.Value,
                                 Me.CurrentUser.UserID) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_cannot_delete_because ", "MSG", "1") & "');", True)
                Else
                    'Call DeleteAllFile(hddKeyID.Value)
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
            Dim strTypeHouse As String = String.Empty
            'Call SetCiterail(txtaTypeHouse.Text, strTypeHouse)
            strTypeHouse = ddlaTypeHouse.SelectedValue
            Dim succ As Boolean = True
            Dim bl As cSetTypeHouse = New cSetTypeHouse
            Dim data As SD05PDDS = Nothing
            If hddKeyID.Value <> "" Then
                If Not bl.Edit(hddKeyID.Value,
                               ddlaTypeCreate.SelectedValue,
                               strTypeHouse,
                               txtaNameEN.Text,
                               txtaNameTH.Text,
                               txtaNames.Text,
                               chkaBOI.Checked,
                               txtaModel.Text,
                               txtaUnitEN.Text,
                               txtaUnitThai.Text,
                               txtaSQW.Text,
                               txtaSQM.Text,
                               txtaBSQM.Text,
                               txtaPSQM.Text,
                               txtaMeter.Text,
                               txtaBedRoom.Text,
                               txtaBathRoom.Text,
                               txtaMeterWater.Text,
                               txtaNote.Text,
                               Me.CurrentUser.UserID) Then
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                Else
                    Call CopyFileTempToProcess()
                    Session.Remove("ORG_SetTypeHouse_Datatable")
                    Call LoadRedirec("1")
                End If
            Else

                Dim lc As SD05PDDS = bl.GetByID(txtaTypeHouseCode.Text,
                                                Me.CurrentUser.UserID)
                If lc Is Nothing Then
                    If Not bl.Add(txtaTypeHouseCode.Text,
                                  ddlaTypeCreate.SelectedValue,
                                  strTypeHouse,
                                  txtaNameEN.Text,
                                  txtaNameTH.Text,
                                  txtaNames.Text,
                                  chkaBOI.Checked,
                                  txtaModel.Text,
                                  txtaUnitEN.Text,
                                  txtaUnitThai.Text,
                                  txtaSQW.Text,
                                  txtaSQM.Text,
                                  txtaBSQM.Text,
                                  txtaPSQM.Text,
                                  txtaMeter.Text,
                                  txtaBedRoom.Text,
                                  txtaBathRoom.Text,
                                  txtaMeterWater.Text,
                                  txtaNote.Text,
                                  Me.CurrentUser.UserID) Then

                        Call OpenDialog()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                    Else
                        Call CopyFileTempToProcess()
                        Session.Remove("ORG_SetTypeHouse_Datatable")
                        Call LoadRedirec("1")
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_duplicate", "MSG", "1") & "');", True)
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

    Protected Const MAX_FILE_LENGTH_MB As Integer = 5
    'Protected Sub btnFileUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileUpload.Click
    '    hddpKeyIDPicture.Value = String.Empty
    '    hddpNamePicture.Value = String.Empty
    '    Dim dt As New DataTable
    '    If FileUpload1.HasFile Then
    '        Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)

    '        Dim fileType As String() = Split(".jpg,.jpeg,.gif,.png", ",")
    '        If Not fileType.Contains(Path.GetExtension(FileUpload1.FileName).ToLower) Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_jpg", "MSG", "1") & "');", True)
    '            Exit Sub
    '        End If

    '        Dim len As Integer = FileUpload1.FileContent.Length
    '        If len > MAX_FILE_LENGTH_MB * 1000000 Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_notover", "MSG", "1") & "');", True)
    '            Return
    '        End If

    '        If fileName <> String.Empty Then
    '            'If fileName.Split(".")(1).ToLower = "jpg" Then
    '            'Call UploadFileTemp(fileName)
    '            Call AddDataTable(Session("ORG_SetTypeHouse_Datatable"),
    '                              fileName)
    '            Call LoadImageID()
    '            If Session("ORG_SetTypeHouse_Datatable") IsNot Nothing Then
    '                hddpKeyIDPicture.Value = fileName.Split(".")(0)
    '                hddpNamePicture.Value = fileName
    '                Dim TempDate As String = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss")
    '                imgPic.ImageUrl = strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & fileName & "?id" & TempDate.Substring(TempDate.Length - 2)
    '                imgPic.Attributes.Add("onClick", "CallLoadHrefNewtab('" & strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & fileName & "?id" & TempDate.Substring(TempDate.Length - 2) & "');")
    '            End If
    '            'Else
    '            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_jpg", "MSG", "1") & "');", True)
    '            'End If
    '        End If
    '    End If
    'End Sub
    'Protected Sub btnDeletePicture_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeletePicture.Click
    '    Dim dt As New DataTable
    '    Dim strFileName As Integer = 0
    '    Dim checkFlagFile As String = String.Empty
    '    Dim objFSO As Object
    '    Dim objFolder As Object
    '    Dim objFile As Object
    '    Dim strLFileName As String = String.Empty
    '    If Session("ORG_SetTypeHouse_Datatable") IsNot Nothing Then
    '        dt = Session("ORG_SetTypeHouse_Datatable")
    '    End If

    '    If dt IsNot Nothing Then
    '        If hddpKeyIDPicture.Value <> String.Empty Then
    '            For Each i As DataRow In dt.Select("ID= '" & hddpKeyIDPicture.Value & "'")
    '                dt.Rows.Remove(i)
    '            Next
    '            If dt.Rows.Count > 0 Then
    '                Session.Add("ORG_SetTypeHouse_Datatable", dt)
    '            Else
    '                Session.Add("ORG_SetTypeHouse_Datatable", Nothing)
    '            End If
    '            'Call DeleteSigleFileTemp(hddpNamePicture.Value)
    '            Call LoadImageID()
    '            If Session("ORG_SetTypeHouse_Datatable") IsNot Nothing Then
    '                Dim PreviosKetID As Integer = 0
    '                Dim tempKeyID As String = hddpKeyIDPicture.Value
    '                If tempKeyID <> String.Empty Then
    '                    Dim TempPath As String = String.Empty
    '                    Try
    '                        If CInt(tempKeyID) > 0 Then
    '                            strFileName = tempKeyID
    '                        End If
    '                        If strFileName <> 0 Then
    '                            For i As Integer = 0 To strFileName - 1
    '                                If CInt(tempKeyID) > 0 Then
    '                                    If strLFileName = String.Empty Then
    '                                        Try
    '                                            objFSO = CreateObject("Scripting.FileSystemObject")
    '                                            objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\"))
    '                                            For Each objFile In objFolder.Files
    '                                                Dim strCheckName As String = objFile.Name.ToString.Split(".")(0)
    '                                                If strCheckName = CInt(strFileName - 1) Then
    '                                                    strLFileName = objFile.Name.ToString.Split(".")(1)
    '                                                    Exit For
    '                                                End If
    '                                            Next objFile
    '                                        Catch ex As Exception

    '                                        End Try
    '                                    End If

    '                                    TempPath = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\" & strFileName & "." & strLFileName)
    '                                    If System.IO.File.Exists(TempPath) Then
    '                                        checkFlagFile = "1"
    '                                        PreviosKetID = strFileName
    '                                        Exit For
    '                                    End If
    '                                    strFileName -= 1
    '                                End If
    '                            Next
    '                        End If
    '                    Catch ex As Exception

    '                    End Try
    '                End If
    '                If strFileName = 0 Or checkFlagFile = String.Empty Then
    '                    PreviosKetID = CInt(dt.Rows(0)("ID").ToString)
    '                End If
    '                If strLFileName = String.Empty Then
    '                    Try
    '                        objFSO = CreateObject("Scripting.FileSystemObject")
    '                        objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\"))
    '                        For Each objFile In objFolder.Files
    '                            Dim strCheckName As String = objFile.Name.ToString.Split(".")(0)
    '                            If strCheckName = PreviosKetID Then
    '                                strLFileName = objFile.Name.ToString.Split(".")(1)
    '                                Exit For
    '                            End If
    '                        Next objFile
    '                    Catch ex As Exception

    '                    End Try
    '                End If

    '                hddpKeyIDPicture.Value = PreviosKetID
    '                hddpNamePicture.Value = PreviosKetID & "." & strLFileName
    '                Dim TempDate As String = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss")
    '                imgPic.ImageUrl = strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & PreviosKetID & "." & strLFileName & "?id=" & TempDate.Substring(TempDate.Length - 2)
    '                imgPic.Attributes.Add("onClick", "CallLoadHrefNewtab('" & strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & PreviosKetID & "." & strLFileName & "?id=" & TempDate.Substring(TempDate.Length - 2) & "');")
    '            Else
    '                imgPic.ImageUrl = String.Empty
    '                imgPic.Attributes.Remove("onClick")
    '            End If
    '        Else
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_select", "MSG", "1") & "');", True)
    '        End If
    '    End If
    'End Sub
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
        lblMassage6.Style.Add("display", "none")
    End Sub
#End Region

#Region "Session"
    Public Function GetDataSetTypeHouse() As List(Of SetTypeHouse_ViewModel)
        Try
            Return Session("IDOCS.application.LoaddataSetTypeHouse")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataSetTypeHouse(ByVal lcSetTypeHouse As List(Of SetTypeHouse_ViewModel)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataSetTypeHouse", lcSetTypeHouse)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Repeater"

#Region "Datatable"
    Public Sub CreateTable(ByRef dt As DataTable)
        dt.Columns.Add("ID")
        dt.Columns.Add("CallPath")
        dt.Columns.Add("FileName")
    End Sub
    Public Sub AddDataTable(ByVal dts As DataTable,
                            ByVal strFileName As String)
        Try
            Dim dt As New DataTable
            Call CreateTable(dt)
            Dim dr As DataRow
            Dim strID As Integer = 0
            If dts IsNot Nothing Then
                For i As Integer = 0 To dts.Rows.Count - 1
                    dr = dt.NewRow
                    dr.Item("ID") = strID
                    dr.Item("CallPath") = dts.Rows(i)("CallPath").ToString
                    dr.Item("FileName") = dts.Rows(i)("FileName").ToString
                    dt.Rows.Add(dr)
                    strID = strID + 1
                Next
                dr = dt.NewRow
                dr.Item("ID") = strID
                dr.Item("CallPath") = strPathServerIMG & hddParameterMenuID.Value & "/" & strFileName
                dr.Item("FileName") = strFileName
                dt.Rows.Add(dr)
            Else
                dr = dt.NewRow
                dr.Item("ID") = strID
                dr.Item("CallPath") = strPathServerIMG & hddParameterMenuID.Value & "/" & strFileName
                dr.Item("FileName") = strFileName
                dt.Rows.Add(dr)
            End If
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Session.Add("ORG_SetTypeHouse_Datatable", dt)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "AddDataTable", ex)
        End Try
    End Sub
#End Region

#Region "LoadRepeater"
    Private Sub LoadImageID()
        'Me.repeaterImageID.DataSource = Session("ORG_SetTypeHouse_Datatable")
        'Me.repeaterImageID.DataBind()
        Me.rptImage.DataSource = LoadRptImageDataSource()
        Me.rptImage.DataBind()
    End Sub

    Private Function LoadRptImageDataSource() As List(Of String)
        'oadcub
        Try
            Dim lstImage = New List(Of String)
            Dim files() As String = IO.Directory.GetFiles(Server.MapPath("IMG_SPW\" + hddParameterMenuID.Value + "\" + hddKeyID.Value + "\"))

            For Each file As String In files
                ' Do work, example
                lstImage.Add("IMG_SPW\" + hddParameterMenuID.Value + "\" + hddKeyID.Value + "\" & Path.GetFileName(file))
            Next

            Return lstImage
        Catch ex As Exception

        End Try

    End Function

    'Private Sub repeaterImageID_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repeaterImageID.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim div As Panel = e.Item.FindControl("divImageID")
    '        Dim btn As ImageButton = e.Item.FindControl("btnImg")
    '        Dim TempDate As String = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss")
    '        Dim script As String = "return SetImageURL('" & DataBinder.Eval(e.Item.DataItem, "CallPath") & "?id=" & TempDate.Substring(TempDate.Length - 2) & "','" & DataBinder.Eval(e.Item.DataItem, "ID") & "','" & DataBinder.Eval(e.Item.DataItem, "FileName") & "');"
    '        btn.Attributes("a-id") = DataBinder.Eval(e.Item.DataItem, "ID")
    '        btn.ImageUrl = DataBinder.Eval(e.Item.DataItem, "CallPath") & "?id=" & TempDate.Substring(TempDate.Length - 2)
    '        btn.Attributes.Add("onclick", script)
    '    End If
    'End Sub
    Private Sub rptImage_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptImage.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imgEdit As Image = e.Item.FindControl("imgEdit")
            imgEdit.ImageUrl = e.Item.DataItem.ToString
        End If
    End Sub
#End Region

#End Region

    '#Region "UploadFileTemp"
    '    Public Sub CrateFolderTemp()
    '        Dim strPath As String = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\")
    '        If (Not System.IO.Directory.Exists(strPath)) Then
    '            System.IO.Directory.CreateDirectory(strPath)
    '        End If
    '    End Sub
    '    Public Sub UploadFileTemp(ByRef strFileName As String)
    '        Call CrateFolderTemp()
    '        Dim strID As Integer = 0
    '        Dim TempSave As String = String.Empty
    '        Dim TempDelete As String = String.Empty

    '        Dim objFSO As Object
    '        Dim objFolder As Object
    '        Dim objFile As Object
    '        If Session("ORG_SetTypeHouse_Datatable") IsNot Nothing Then
    '            Try
    '                objFSO = CreateObject("Scripting.FileSystemObject")
    '                objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\"))
    '                For Each objFile In objFolder.Files
    '                    TempSave = Server.MapPath("Uploads/" & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & strID & "." & objFile.Name.ToString.Split(".")(1))
    '                    TempDelete = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\" & objFile.Name)
    '                    If System.IO.File.Exists(TempDelete) Then
    '                        If strID & "." & objFile.Name.ToString.Split(".")(1) <> objFile.Name Then
    '                            System.IO.File.Copy(TempDelete, TempSave)
    '                            System.IO.File.Delete(TempDelete)
    '                        End If
    '                    End If
    '                    strID += 1
    '                Next objFile
    '            Catch ex As Exception

    '            End Try
    '        End If

    '        TempSave = Server.MapPath("Uploads/" & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & strID & "." & strFileName.Split(".")(1))
    '        TempDelete = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\" & strID & "." & strFileName.Split(".")(1))
    '        If System.IO.File.Exists(TempDelete) Then
    '            System.IO.File.Delete(TempDelete)
    '            FileUpload1.PostedFile.SaveAs(TempSave)
    '        Else
    '            FileUpload1.PostedFile.SaveAs(TempSave)
    '        End If
    '        strFileName = strID & "." & strFileName.Split(".")(1)
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
    '    Public Sub DeleteSigleFileTemp(ByVal strName As String)
    '        Dim TempDelete As String = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\" & strName)

    '        If System.IO.File.Exists(TempDelete) Then
    '            System.IO.File.Delete(TempDelete)
    '        End If

    '    End Sub
    '#End Region

#Region "UploadFile"
    Public Sub CopyFileTempToProcess()
        Dim strKeyID As String = IIf(hddKeyID.Value <> String.Empty, hddKeyID.Value, txtaTypeHouseCode.Text)
        ' Call DeleteAllFile(strKeyID)
        Call DeleteSelectedFiles()
        Call CrateFolder(strKeyID)
        Call SaveFile(strKeyID)
    End Sub
    Public Sub CrateFolder(ByVal strKeyID As String)
        Dim strPath As String = Server.MapPath("IMG_SPW\" & hddParameterMenuID.Value & "\")
        If (Not System.IO.Directory.Exists(strPath)) Then
            System.IO.Directory.CreateDirectory(strPath)
        End If
    End Sub
    Public Sub DeleteAllFile(ByVal strKeyID As String)
        'oadcub
        Dim objFSO As Object
        Dim objFolder As Object
        Dim objFile As Object
        Try
            objFSO = CreateObject("Scripting.FileSystemObject")
            objFolder = objFSO.GetFolder(Server.MapPath("IMG_SPW\" & hddParameterMenuID.Value & "\"))
            For Each objFile In objFolder.Files
                Dim DatLenght As Integer = objFile.Name.ToString.Split("-").Length
                Dim strCheckFileName As String = objFile.Name.ToString.Split(".")(0).Replace("PD", "").Split("-")(DatLenght - 1)
                Dim strCheckName As String = objFile.Name.ToString.Split(".")(0).Replace("PD", "").Replace("-" & strCheckFileName, "")
                Dim TempDelete As String = Server.MapPath("IMG_SPW\" & hddParameterMenuID.Value & "\" & objFile.Name)
                If strKeyID.Replace("-" & strCheckFileName, "") = strCheckName Then
                    If System.IO.File.Exists(TempDelete) Then
                        System.IO.File.Delete(TempDelete)
                    End If
                End If
            Next objFile
        Catch ex As Exception

        End Try

    End Sub

    Public Sub DeleteSelectedFiles()
        For Each item In rptImage.Items
            If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                Dim chkDeleteImage As HtmlInputCheckBox = item.FindControl("chkDeleteImage")
                If chkDeleteImage.Checked Then
                    Dim img As Image = item.FindControl("imgEdit")
                    Dim path = Server.MapPath(img.ImageUrl)
                    If System.IO.File.Exists(path) Then
                        System.IO.File.Delete(path)
                    End If
                End If

            End If
        Next
    End Sub
    Public Sub SaveFile(ByVal strKeyID As String)
        Dim TempSave As String = Server.MapPath("IMG_SPW/" & hddParameterMenuID.Value & "/" & strKeyID & "/")
        If (Not System.IO.Directory.Exists(TempSave)) Then
            System.IO.Directory.CreateDirectory(TempSave)
        End If
        Try
            'check filename
            Dim fileCollection = Request.Files
            Dim strFileName As String = DateTime.Now.ToString("yyMMddhhmmssss")
            For i As Integer = 0 To fileCollection.Count - 1
                Dim uploadfile = fileCollection(i)
                If (uploadfile.ContentLength > 0) Then
                    Dim fileName = strFileName & i.ToString & Path.GetExtension(uploadfile.FileName)
                    uploadfile.SaveAs(TempSave & fileName)

                End If
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "SaveFile", ex.GetBaseException)
        End Try
    End Sub
#End Region

#Region "LoadFileForEdit"

#End Region

    ''' <summary>
    ''' oadcub#### Sample Multi file upload with preview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnUpload_Click1(sender As Object, e As EventArgs)
        Dim fileCollection = Request.Files
        For i As Integer = 0 To fileCollection.Count - 1
            Dim uploadfile = fileCollection(i)
            Dim fileName = Path.GetFileName(uploadfile.FileName)
            fileName = i.ToString + Path.GetExtension(uploadfile.FileName)
            If (uploadfile.ContentLength > 0) Then
                '  uploadfile.SaveAs(Server.MapPath("~/UploadFiles55/") + fileName)
            End If

        Next

    End Sub

#Region "Check Master Delete"
    Public Sub getUsedMaster()
        Try
            Dim bl As cSetTypeHouse = New cSetTypeHouse
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
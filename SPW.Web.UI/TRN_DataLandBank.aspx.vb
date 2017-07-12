Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class TRN_DataLandBank
    Inherits BasePage
    Private strPathServer As String = System.Configuration.ConfigurationManager.AppSettings("strPathServer").ToString
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.DataBind()
                Session.Remove("IDOCS.application.LoaddataDataLandBank")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("TRN_DataLandBank.aspx")
                Me.ClearSessionPageLoad("TRN_DataLandBank.aspx")

                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()
                Call Loaddata()
                'Call DeleteAllFileTemp()
                Session.Remove("TRN_DataLandBank_Datatable")

                If Request.QueryString("flagedit") IsNot Nothing Then
                    If Request.QueryString("flagedit").ToString <> String.Empty Then
                        hddKeyID.Value = Request.QueryString("flagedit").ToString
                        Panel1.Enabled = False
                        Panel1.CssClass = "formEdit"
                        imgAdd.Visible = False
                        btnMSGSaveData.Disabled = True
                        'btnhrefAddPicture.Disabled = True
                        'btnhrefAddDelete.Disabled = True
                        Call EditData()
                    End If
                Else
                    Call GetParameter()
                    Call Loaddata()
                End If

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
        If Request.QueryString("backurl") IsNot Nothing Then
            If Request.QueryString("backurl").ToString <> String.Empty Then
                If FlagSave = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                ElseIf FlagSave = "" Then
                    Call Redirect("ORG_Project.aspx?flagedit=" & Request.QueryString("backurl").ToString & "")
                End If
            End If
        Else
            SetParameter(FlagSave)
            Call Redirect("TRN_DataLandBank.aspx")
        End If
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("TRN_DataLandBank_Keyword")
        Session.Remove("TRN_DataLandBank_FS")
        Session.Add("TRN_DataLandBank_Keyword", IIf(txtsKeyword.Text <> String.Empty, txtsKeyword.Text, ""))
        Session.Add("TRN_DataLandBank_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("TRN_DataLandBank_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("TRN_DataLandBank_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("TRN_DataLandBank_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("TRN_DataLandBank_Keyword") IsNot Nothing Then
            If Session("TRN_DataLandBank_Keyword").ToString <> String.Empty Then
                Dim strKeyword As String = Session("TRN_DataLandBank_Keyword").ToString
                txtsKeyword.Text = strKeyword
            End If
        End If
        If Session("TRN_DataLandBank_FS") IsNot Nothing Then
            If Session("TRN_DataLandBank_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("TRN_DataLandBank_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG", "1") & "');", True)
                End If
            End If
        End If
        If Session("TRN_DataLandBank_PageInfo") IsNot Nothing Then
            If Session("TRN_DataLandBank_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("TRN_DataLandBank_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("TRN_DataLandBank_Search") IsNot Nothing Then
            If Session("TRN_DataLandBank_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("TRN_DataLandBank_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("TRN_DataLandBank_pageLength") IsNot Nothing Then
            If Session("TRN_DataLandBank_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("TRN_DataLandBank_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If
    End Sub

#End Region

#Region "Dropdownlist"
    Public Sub LoadDropdownlist()
        Call LoadStatus(ddlaFASSETOBJ)
        Call LoadPrice(ddlaFMCOMPCOMP)
        Call LoadFASSETST1(ddlaFASSETST)
        Call LoadFASSETST2(ddlaFASSETST2)
    End Sub
    Public Sub LoadStatus(ByVal ddl As DropDownList)
        Try
            ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            ddl.Items.Insert(1, New ListItem(GetWebMessage("ddlaFASSETOBJ1", "Text", hddParameterMenuID.Value), "3"))
            ddl.Items.Insert(2, New ListItem(GetWebMessage("ddlaFASSETOBJ2", "Text", hddParameterMenuID.Value), "1"))
            'ddl.Items.Insert(3, New ListItem(GetWebMessage("ddlaFASSETOBJ3", "Text", hddParameterMenuID.Value), "121"))
            'ddl.Items.Insert(4, New ListItem(GetWebMessage("ddlaFASSETOBJ4", "Text", hddParameterMenuID.Value), "122"))
            'ddl.Items.Insert(5, New ListItem(GetWebMessage("ddlaFASSETOBJ5", "Text", hddParameterMenuID.Value), "31"))

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadStatus", ex)
        End Try
    End Sub
    Public Sub LoadPrice(ByVal ddl As DropDownList)
        Try
            ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            ddl.Items.Insert(1, New ListItem(GetWebMessage("ddlaFMCOMPCOMP1", "Text", hddParameterMenuID.Value), "102"))
            ddl.Items.Insert(2, New ListItem(GetWebMessage("ddlaFMCOMPCOMP2", "Text", hddParameterMenuID.Value), "202"))

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadStatus", ex)
        End Try
    End Sub
    Public Sub LoadFASSETST1(ByVal ddl As DropDownList)
        Try
            ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            ddl.Items.Insert(1, New ListItem(GetWebMessage("ddlaFASSETST11", "Text", hddParameterMenuID.Value), "A"))
            ddl.Items.Insert(2, New ListItem(GetWebMessage("ddlaFASSETST12", "Text", hddParameterMenuID.Value), "B"))
            ddl.Items.Insert(3, New ListItem(GetWebMessage("ddlaFASSETST13", "Text", hddParameterMenuID.Value), "C"))

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadStatus", ex)
        End Try
    End Sub
    Public Sub LoadFASSETST2(ByVal ddl As DropDownList)
        Try
            ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            ddl.Items.Insert(1, New ListItem(GetWebMessage("ddlaFASSETST21", "Text", hddParameterMenuID.Value), "1"))
            ddl.Items.Insert(2, New ListItem(GetWebMessage("ddlaFASSETST22", "Text", hddParameterMenuID.Value), "2"))
            ddl.Items.Insert(3, New ListItem(GetWebMessage("ddlaFASSETST23", "Text", hddParameterMenuID.Value), "3"))

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadStatus", ex)
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

        lblsKeyword.Text = Me.GetResource("lblsKeyword", "Text", hddParameterMenuID.Value)
        lblsDescriptionKeyword.Text = Me.GetResource("lblsDescriptionKeyword", "Text", hddParameterMenuID.Value)

        lblsSearch.Text = Me.GetResource("lblsSearch", "Text", hddParameterMenuID.Value)
        'lblsPicture.Text = Me.GetResource("lblsPicture", "Text", hddParameterMenuID.Value)
        'lblsDeletePicture.Text = Me.GetResource("lblsDeletePicture", "Text", hddParameterMenuID.Value)

        lblaFASSETNO.Text = Me.GetResource("lblaFASSETNO", "Text", hddParameterMenuID.Value)
        chkaFASSETBJ2.Text = Me.GetResource("chkaFASSETBJ2", "Text", hddParameterMenuID.Value)
        lblaFASSETOBJ.Text = Me.GetResource("lblaFASSETOBJ", "Text", hddParameterMenuID.Value)
        lblaFASSETNM.Text = Me.GetResource("lblaFASSETNM", "Text", hddParameterMenuID.Value)
        lblaFENDATE.Text = Me.GetResource("lblaFENDATE", "Text", hddParameterMenuID.Value)
        lblaFPCPIECE.Text = Me.GetResource("lblaFPCPIECE", "Text", hddParameterMenuID.Value)
        lblaFPCINST.Text = Me.GetResource("lblaFPCINST", "Text", hddParameterMenuID.Value)
        lblaFPCLNDNO.Text = Me.GetResource("lblaFPCLNDNO", "Text", hddParameterMenuID.Value)
        lblaFPCBETWEEN.Text = Me.GetResource("lblaFPCBETWEEN", "Text", hddParameterMenuID.Value)
        lblaFPCTABON.Text = Me.GetResource("lblaFPCTABON", "Text", hddParameterMenuID.Value)
        lblaFPCWIDTH.Text = Me.GetResource("lblaFPCWIDTH", "Text", hddParameterMenuID.Value)
        lblaFPCAMPHE.Text = Me.GetResource("lblaFPCAMPHE", "Text", hddParameterMenuID.Value)
        lblaFPCINST3.Text = Me.GetResource("lblaFPCINST3", "Text", hddParameterMenuID.Value)
        lblaFPCPROVINCE.Text = Me.GetResource("lblaFPCPROVINCE", "Text", hddParameterMenuID.Value)
        lblaFASCOLOR.Text = Me.GetResource("lblaFASCOLOR", "Text", hddParameterMenuID.Value)
        lblaFPCLANDOWN.Text = Me.GetResource("lblaFPCLANDOWN", "Text", hddParameterMenuID.Value)
        lblaFQTY.Text = Me.GetResource("lblaFQTY", "Text", hddParameterMenuID.Value)
        lblaFMKPRCU.Text = Me.GetResource("lblaFMKPRCU", "Text", hddParameterMenuID.Value)
        lblaFASSPRCU.Text = Me.GetResource("lblaFASSPRCU", "Text", hddParameterMenuID.Value)
        lblaFKPRCA.Text = Me.GetResource("lblaFKPRCA", "Text", hddParameterMenuID.Value)
        lblaFASSPRCA.Text = Me.GetResource("lblaFASSPRCA", "Text", hddParameterMenuID.Value)
        lblaFMKPRCBY.Text = Me.GetResource("lblaFMKPRCBY", "Text", hddParameterMenuID.Value)
        lblaFASSWHO.Text = Me.GetResource("lblaFASSWHO", "Text", hddParameterMenuID.Value)
        lblaCLANDOWNTL.Text = Me.GetResource("lblaCLANDOWNTL", "Text", hddParameterMenuID.Value)
        lblaFASSPRCDT.Text = Me.GetResource("lblaFASSPRCDT", "Text", hddParameterMenuID.Value)
        lblaFMKPRCDT.Text = Me.GetResource("lblaFMKPRCDT", "Text", hddParameterMenuID.Value)
        lblaFASSCHG.Text = Me.GetResource("lblaFASSCHG", "Text", hddParameterMenuID.Value)
        lblaFASSETST.Text = Me.GetResource("lblaFASSETST", "Text", hddParameterMenuID.Value)
        lblaFASSETST2.Text = Me.GetResource("lblaFASSETST2", "Text", hddParameterMenuID.Value)
        lblaFAGRNO.Text = Me.GetResource("lblaFAGRNO", "Text", hddParameterMenuID.Value)
        chkaFMORTGYN.Text = Me.GetResource("chkaFMORTGYN", "Text", hddParameterMenuID.Value)
        lblaFSTDATE.Text = Me.GetResource("lblaFSTDATE", "Text", hddParameterMenuID.Value)
        lblaFMORTGT.Text = Me.GetResource("lblaFMORTGT", "Text", hddParameterMenuID.Value)

        TextHd1.Text = Me.GetResource("No", "Text", "1")
        TextHd2.Text = Me.GetResource("TextHd2", "Text", hddParameterMenuID.Value)
        TextHd3.Text = Me.GetResource("TextHd3", "Text", hddParameterMenuID.Value)
        TextHd4.Text = Me.GetResource("TextHd4", "Text", hddParameterMenuID.Value)
        TextHd5.Text = Me.GetResource("TextHd5", "Text", hddParameterMenuID.Value)
        TextHd6.Text = Me.GetResource("TextHd6", "Text", hddParameterMenuID.Value)
        TextHd7.Text = Me.GetResource("TextHd7", "Text", hddParameterMenuID.Value)
        TextHd8.Text = Me.GetResource("TextHd8", "Text", hddParameterMenuID.Value)
        TextHd9.Text = Me.GetResource("col_edit", "Text", "1")
        TextHd10.Text = Me.GetResource("col_delete", "Text", "1")

        TextFt1.Text = Me.GetResource("No", "Text", "1")
        TextFt2.Text = Me.GetResource("TextFt2", "Text", hddParameterMenuID.Value)
        TextFt3.Text = Me.GetResource("TextFt3", "Text", hddParameterMenuID.Value)
        TextFt4.Text = Me.GetResource("TextFt4", "Text", hddParameterMenuID.Value)
        TextFt5.Text = Me.GetResource("TextFt5", "Text", hddParameterMenuID.Value)
        TextFt6.Text = Me.GetResource("TextFt6", "Text", hddParameterMenuID.Value)
        TextFt7.Text = Me.GetResource("TextFt7", "Text", hddParameterMenuID.Value)
        TextFt8.Text = Me.GetResource("TextFt8", "Text", hddParameterMenuID.Value)
        TextFt9.Text = Me.GetResource("col_edit", "Text", "1")
        TextFt10.Text = Me.GetResource("col_delete", "Text", "1")

        lblMassage1.Text = Me.GetResource("msg_required", "MSG", "1")

        lblTabUSER1.Text = Me.GetResource("TabUSER1", "Text", hddParameterMenuID.Value)
        lblTabUSER2.Text = Me.GetResource("TabUSER2", "Text", hddParameterMenuID.Value)
        lblTabUSER3.Text = Me.GetResource("TabUSER3", "Text", hddParameterMenuID.Value)

        lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG", "1")
        lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG", "1") & " " & Me.GetResource("TextHd2", "Text", hddParameterMenuID.Value)

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

        Call SetScript()

        lblMassage6.Text = grtt("resPleaseEnter")
        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try
            hddReloadGrid.Value = "1"
            Dim bl As cDataLandBank = New cDataLandBank
            'If txtsKeyword.Text <> String.Empty Then
            Dim lst As List(Of FD11PROP) = bl.Loaddata(txtsKeyword.Text,
                                                       Me.CurrentUser.UserID)
            If lst IsNot Nothing Then
                Call SetData(lst)
            Else
                Call SetData(Nothing)
            End If
            'End If
            Session.Remove("TRN_DataLandBank_Keyword")
            Session.Remove("TRN_DataLandBank_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)
            Session.Remove("TRN_DataLandBank_Keyword")
            Session.Remove("TRN_DataLandBank_FS")
        End Try
    End Sub
#End Region

#Region "Function"
    Public Sub ClearText()
        txtaFASSETNO.Text = String.Empty
        chkaFASSETBJ2.Checked = False
        ddlaFASSETOBJ.SelectedIndex = 0
        txtaFASSETNM.Text = String.Empty
        txtaFENDATE.Text = ToStringByCulture(DateTime.Now)
        txtaFDESC1.Text = String.Empty
        txtaFPCPIECE.Text = String.Empty
        txtaFPCINST.Text = String.Empty
        txtaFPCLNDNO.Text = String.Empty
        txtaFPCINST2.Text = String.Empty
        txtaFPCBETWEEN.Text = String.Empty
        txtaFPCTABON.Text = String.Empty
        txtaFPCWIDTH.Text = String.Empty
        txtaFPCAMPHE.Text = String.Empty
        txtaFPCINST3.Text = String.Empty
        txtaFPCPROVINCE.Text = String.Empty
        txtaFASCOLOR.Text = String.Empty
        txtaFPCLANDOWN.Text = String.Empty
        txtaFQTY1.Text = String.Empty
        'txtaFQTY2.Text = String.Empty
        'txtaFQTY3.Text = String.Empty
        txtaFPCLANDOWN2.Text = String.Empty

        'imgPic.ImageUrl = String.Empty
        'repeaterImageID.DataSource = Nothing
        'repeaterImageID.DataBind()

        txtaFMKPRCU.Text = String.Empty
        ddlaFMCOMPCOMP.SelectedIndex = 0
        txtaFASSPRCU.Text = String.Empty
        txtaFKPRCA.Text = String.Empty
        txtaFASSPRCA.Text = String.Empty
        txtaFMKPRCBY.Text = String.Empty
        txtaFASSWHO.Text = String.Empty
        txtaCLANDOWNTL.Text = String.Empty
        txtaFASSPRCDT.Text = String.Empty
        txtaFMKPRCDT.Text = String.Empty
        txtaFASSCHG.Text = String.Empty
        ddlaFASSETST.SelectedIndex = 0
        ddlaFASSETST2.SelectedIndex = 0
        txtaFAGRNO.Text = String.Empty
        txtaFAGRNO2.Text = String.Empty
        chkaFMORTGYN.Checked = False
        txtaFMORTGYN.Text = String.Empty
        txtaFSTDATE.Text = String.Empty
        txtaFMORTGT.Text = String.Empty
        txtaNote.Text = String.Empty

        hddCityPro.Value = String.Empty

        Panel1.CssClass = "formAdd"
    End Sub
    Public Sub SetScript()
        txtaFQTY1.Attributes.Add("onblur", "checkFormatSplit(this);CalQTY();")
        'txtaFQTY2.Attributes.Add("onblur", "setFormat0(this,event);CalQTY();")
        'txtaFQTY3.Attributes.Add("onblur", "setFormat0(this,event);CalQTY();")
        txtaFMKPRCU.Attributes.Add("onblur", "setFormat(this,event);CalQTY();")
        ddlaFMCOMPCOMP.Attributes.Add("onchange", "CalQTY();")
    End Sub
    Public Sub EditData()
        Session.Remove("TRN_DataLandBank_Datatable")
        Dim dt As New DataTable
        Call ClearText()
        Dim bl As cDataLandBank = New cDataLandBank
        Dim lc As FD11PROP = bl.GetByID(hddKeyID.Value,
                                        Me.CurrentUser.UserID)

        Panel1.CssClass = "formEdit"
        If lc IsNot Nothing Then
            If lc.FASSETNO IsNot Nothing Then
                If lc.FASSETNO <> String.Empty Then
                    hddKeyID.Value = lc.FASSETNO
                    txtaFASSETNO.Text = lc.FASSETNO
                End If
            End If
            If lc.FASSETOBJ2 IsNot Nothing Then
                If lc.FASSETOBJ2 = "3" Then
                    chkaFASSETBJ2.Checked = True
                End If
            End If
            If lc.FASSETOBJ IsNot Nothing Then
                If lc.FASSETOBJ <> String.Empty Then
                    Try
                        ddlaFASSETOBJ.SelectedValue = lc.FASSETOBJ
                    Catch ex As Exception
                        ddlaFASSETOBJ.SelectedIndex = 0
                    End Try
                End If
            End If
            If lc.FASSETNM IsNot Nothing Then
                If lc.FASSETNM <> String.Empty Then
                    txtaFASSETNM.Text = lc.FASSETNM
                End If
            End If
            If Not IsDBNull(lc.FENDATE) Then
                If lc.FENDATE IsNot Nothing Then
                    Dim TempDate As String = lc.FENDATE
                    txtaFENDATE.Text = ToStringByCulture(TempDate)
                End If
            End If
            If lc.FDES1 IsNot Nothing Then
                If lc.FDES1 <> String.Empty Then
                    txtaFDESC1.Text = lc.FDES1
                End If
            End If
            If lc.FPCPIECE IsNot Nothing Then
                If lc.FPCPIECE <> String.Empty Then
                    txtaFPCPIECE.Text = lc.FPCPIECE
                End If
            End If
            If lc.FPCINST IsNot Nothing Then
                If lc.FPCINST <> String.Empty Then
                    txtaFPCINST.Text = lc.FPCINST
                End If
            End If
            If lc.FPCLNDNO IsNot Nothing Then
                If lc.FPCLNDNO <> String.Empty Then
                    txtaFPCLNDNO.Text = lc.FPCLNDNO
                End If
            End If
            If lc.FPCINST2 IsNot Nothing Then
                If lc.FPCINST2 <> String.Empty Then
                    txtaFPCINST2.Text = lc.FPCINST2
                End If
            End If
            If lc.FPCBETWEEN IsNot Nothing Then
                If lc.FPCBETWEEN <> String.Empty Then
                    txtaFPCBETWEEN.Text = lc.FPCBETWEEN
                End If
            End If
            If lc.FPCTAMBON IsNot Nothing Then
                If lc.FPCTAMBON <> String.Empty Then
                    txtaFPCTABON.Text = lc.FPCTAMBON
                End If
            End If
            If lc.FPCWIDTH IsNot Nothing Then
                If lc.FPCWIDTH <> String.Empty Then
                    txtaFPCWIDTH.Text = lc.FPCWIDTH
                End If
            End If
            If lc.FPCAMPHER IsNot Nothing Then
                If lc.FPCAMPHER <> String.Empty Then
                    txtaFPCAMPHE.Text = lc.FPCAMPHER
                End If
            End If
            If lc.FPCINST3 IsNot Nothing Then
                If lc.FPCINST3 <> String.Empty Then
                    txtaFPCINST3.Text = lc.FPCINST3
                End If
            End If
            If lc.FPCPROVINC IsNot Nothing Then
                If lc.FPCPROVINC <> String.Empty Then
                    txtaFPCPROVINCE.Text = lc.FPCPROVINC
                End If
            End If
            If lc.FASCOLOR IsNot Nothing Then
                If lc.FASCOLOR <> String.Empty Then
                    txtaFASCOLOR.Text = lc.FASCOLOR
                End If
            End If
            If lc.FPCLANDOWN IsNot Nothing Then
                If lc.FPCLANDOWN <> String.Empty Then
                    txtaFPCLANDOWN.Text = lc.FPCLANDOWN
                End If
            End If
            If Not IsDBNull(lc.FQTY) Then
                If lc.FQTY IsNot Nothing Then
                    Dim Temp1 As String = String.Empty
                    Dim Temp2 As String = String.Empty
                    Dim Temp3 As String = String.Empty
                    Call CalReverse(lc.FQTY,
                                    Temp1,
                                    Temp2,
                                    Temp3)
                    txtaFQTY1.Text = Temp1 & "-" & Temp2 & "-" & Temp3
                End If
            End If
            If lc.FPCLANDOWN2 IsNot Nothing Then
                If lc.FPCLANDOWN2 <> String.Empty Then
                    txtaFPCLANDOWN2.Text = lc.FPCLANDOWN2
                End If
            End If
            If Not IsDBNull(lc.FMKPRCU) Then
                If lc.FMKPRCU IsNot Nothing Then
                    txtaFMKPRCU.Text = String.Format("{0:N2}", CDec(lc.FMKPRCU))
                End If
            End If
            If lc.FLCOMPCOMP IsNot Nothing Then
                If lc.FLCOMPCOMP <> String.Empty Then
                    Try
                        ddlaFMCOMPCOMP.SelectedValue = lc.FLCOMPCOMP
                    Catch ex As Exception
                        ddlaFMCOMPCOMP.SelectedIndex = 0
                    End Try
                End If
            End If
            If Not IsDBNull(lc.FASSPRCU) Then
                If lc.FASSPRCU IsNot Nothing Then
                    txtaFASSPRCU.Text = String.Format("{0:N2}", CDec(lc.FASSPRCU))
                End If
            End If
            If Not IsDBNull(lc.FMKPRCA) Then
                If lc.FMKPRCA IsNot Nothing Then
                    txtaFKPRCA.Text = String.Format("{0:N2}", CDec(lc.FMKPRCA))
                End If
            End If
            If Not IsDBNull(lc.FASSPRCA) Then
                If lc.FASSPRCA IsNot Nothing Then
                    txtaFASSPRCA.Text = String.Format("{0:N2}", CDec(lc.FASSPRCA))
                End If
            End If
            If lc.FMKPRCBY IsNot Nothing Then
                If lc.FMKPRCBY <> String.Empty Then
                    txtaFMKPRCBY.Text = lc.FMKPRCBY
                End If
            End If
            If lc.FASSWHO IsNot Nothing Then
                If lc.FASSWHO <> String.Empty Then
                    txtaFASSWHO.Text = lc.FASSWHO
                End If
            End If
            If lc.FPCLANDOWTL IsNot Nothing Then
                If lc.FPCLANDOWTL <> String.Empty Then
                    txtaCLANDOWNTL.Text = lc.FPCLANDOWTL
                End If
            End If
            If Not IsDBNull(lc.FASSPRCDT) Then
                If lc.FASSPRCDT IsNot Nothing Then
                    Dim TempDate As String = lc.FASSPRCDT
                    txtaFASSPRCDT.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FMKPRCDT) Then
                If lc.FMKPRCDT IsNot Nothing Then
                    Dim TempDate As String = lc.FMKPRCDT
                    txtaFMKPRCDT.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FASSCHG) Then
                If lc.FASSCHG IsNot Nothing Then
                    txtaFASSCHG.Text = String.Format("{0:N2}", CDec(lc.FASSCHG))
                End If
            End If
            If lc.FASSETST IsNot Nothing Then
                If lc.FASSETST <> String.Empty Then
                    Dim strTempData As String = lc.FASSETST
                    Try
                        ddlaFASSETST.SelectedValue = strTempData.Substring(1, 1)
                    Catch ex As Exception
                        ddlaFASSETST.SelectedIndex = 0
                    End Try
                    Try
                        ddlaFASSETST2.SelectedValue = strTempData.Substring(0, 1)
                    Catch ex As Exception
                        ddlaFASSETST2.SelectedIndex = 0
                    End Try
                End If
            End If
            If lc.FAGRNO IsNot Nothing Then
                If lc.FAGRNO <> String.Empty Then
                    txtaFAGRNO.Text = lc.FAGRNO
                End If
            End If
            'If lc.FAGRNO2 IsNot Nothing Then
            '    If lc.FAGRNO2 <> String.Empty Then
            '        txtaFAGRNO2.Text = lc.FAGRNO2
            '    End If
            'End If
            If lc.FMORTGYN IsNot Nothing Then
                If lc.FMORTGYN = "Y" Then
                    chkaFMORTGYN.Checked = True
                ElseIf lc.FMORTGYN = "N" Then
                    chkaFMORTGYN.Checked = False
                End If
            End If
            'If lc.FMORTGYN IsNot Nothing Then
            '    If lc.FMORTGYN <> String.Empty Then
            '        txtaFMORTGYN.Text = lc.FMORTGYN
            '    End If
            'End If
            If Not IsDBNull(lc.FSTDATE) Then
                If lc.FSTDATE IsNot Nothing Then
                    Dim TempDate As String = lc.FSTDATE
                    txtaFSTDATE.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FMORTGDT) Then
                If lc.FMORTGDT IsNot Nothing Then
                    Dim TempDate As String = lc.FMORTGDT
                    txtaFMORTGT.Text = ToStringByCulture(TempDate)
                End If
            End If
            If lc.FPCNOTE IsNot Nothing Then
                If lc.FPCNOTE <> String.Empty Then
                    txtaNote.Text = lc.FPCNOTE
                End If
            End If

            If lc.FPCPROVCD And lc.FPCCITYCD Then
                hddCityPro.Value = lc.FPCPROVCD & lc.FPCCITYCD
            End If
            Call LoadImageID()
        End If

        txtaFASSETNO.Enabled = False

        Call OpenDialog()
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
    Protected Sub btnReload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReload.Click
        Try
            hddpAutoCode.Value = String.Empty
            If hddpAutoName.Value <> String.Empty Then
                hddCityPro.Value = hddpAutoName.Value.Split(" ")(0)
                txtaFPCAMPHE.Text = hddpAutoName.Value.Split(" ")(1)
                txtaFPCPROVINCE.Text = hddpAutoName.Value.Split(" ")(2)
                'txtFrepostal.Text = hddpAutoName.Value.Split(" ")(2)
            Else
                If txtaFPCAMPHE.Text <> String.Empty Then
                    hddpAutoCode.Value = txtaFPCAMPHE.Text
                    hddpAutoName.Value = txtaFPCAMPHE.Text & " " &
                                         txtaFPCPROVINCE.Text
                    hddCityPro.Value = hddpAutoName.Value.Split(" ")(0)
                    txtaFPCAMPHE.Text = hddpAutoName.Value.Split(" ")(1)
                    txtaFPCPROVINCE.Text = hddpAutoName.Value.Split(" ")(2)
                    'txtFrepostal.Text = hddpAutoName.Value.Split(" ")(2)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReload_Click", ex)
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Session.Remove("TRN_DataLandBank_Datatable")
            Call ClearText()
            Call OpenDialog()

            txtaFASSETNO.Enabled = True
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try
            Call EditData()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim succ As Boolean = True
            Dim bl As cDataLandBank = New cDataLandBank
            Dim pKeyCode As String = String.Empty
            If hddCityPro.Value <> String.Empty Then
                pKeyCode = hddCityPro.Value
            End If
            If hddKeyID.Value <> "" Then
                If Not bl.Edit(txtaFASSETNO.Text,
                               chkaFASSETBJ2.Checked,
                               ddlaFASSETOBJ.SelectedValue,
                               txtaFASSETNM.Text,
                               ToSystemDate(txtaFENDATE.Text),
                               txtaFDESC1.Text,
                               txtaFPCPIECE.Text,
                               txtaFPCINST.Text,
                               txtaFPCLNDNO.Text,
                               txtaFPCINST2.Text,
                               txtaFPCBETWEEN.Text,
                               txtaFPCTABON.Text,
                               txtaFPCWIDTH.Text,
                               txtaFPCAMPHE.Text,
                               txtaFPCINST3.Text,
                               txtaFPCPROVINCE.Text,
                               txtaFASCOLOR.Text,
                               txtaFPCLANDOWN.Text,
                               txtaFQTY1.Text,
                               "",
                               "",
                               txtaFPCLANDOWN2.Text,
                               txtaFMKPRCU.Text,
                               ddlaFMCOMPCOMP.SelectedValue,
                               txtaFASSPRCU.Text,
                               txtaFKPRCA.Text,
                               txtaFASSPRCA.Text,
                               txtaFMKPRCBY.Text,
                               txtaFASSWHO.Text,
                               txtaCLANDOWNTL.Text,
                               ToSystemDate(txtaFASSPRCDT.Text),
                               ToSystemDate(txtaFMKPRCDT.Text),
                               txtaFASSCHG.Text,
                               ddlaFASSETST.SelectedValue,
                               ddlaFASSETST2.SelectedValue,
                               txtaFAGRNO.Text,
                               txtaFAGRNO2.Text,
                               chkaFMORTGYN.Checked,
                               txtaFMORTGYN.Text,
                               ToSystemDate(txtaFSTDATE.Text),
                               ToSystemDate(txtaFMORTGT.Text),
                               txtaNote.Text,
                               pKeyCode,
                               Me.CurrentUser.UserID) Then
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                Else
                    Call CopyFileTempToProcess()
                    Session.Remove("TRN_DataLandBank_Datatable")
                    Call LoadRedirec("1")
                End If
            Else
                Dim lc As FD11PROP = bl.GetByID(txtaFASSETNO.Text,
                                                Me.CurrentUser.UserID)
                If lc Is Nothing Then
                    If Not bl.Add(txtaFASSETNO.Text,
                                  chkaFASSETBJ2.Checked,
                                  ddlaFASSETOBJ.SelectedValue,
                                  txtaFASSETNM.Text,
                                  ToSystemDate(txtaFENDATE.Text),
                                  txtaFDESC1.Text,
                                  txtaFPCPIECE.Text,
                                  txtaFPCINST.Text,
                                  txtaFPCLNDNO.Text,
                                  txtaFPCINST2.Text,
                                  txtaFPCBETWEEN.Text,
                                  txtaFPCTABON.Text,
                                  txtaFPCWIDTH.Text,
                                  txtaFPCAMPHE.Text,
                                  txtaFPCINST3.Text,
                                  txtaFPCPROVINCE.Text,
                                  txtaFASCOLOR.Text,
                                  txtaFPCLANDOWN.Text,
                                  txtaFQTY1.Text,
                                  "",
                                  "",
                                  txtaFPCLANDOWN2.Text,
                                  txtaFMKPRCU.Text,
                                  ddlaFMCOMPCOMP.SelectedValue,
                                  txtaFASSPRCU.Text,
                                  txtaFKPRCA.Text,
                                  txtaFASSPRCA.Text,
                                  txtaFMKPRCBY.Text,
                                  txtaFASSWHO.Text,
                                  txtaCLANDOWNTL.Text,
                                  ToSystemDate(txtaFASSPRCDT.Text),
                                  ToSystemDate(txtaFMKPRCDT.Text),
                                  txtaFASSCHG.Text,
                                  ddlaFASSETST.SelectedValue,
                                  ddlaFASSETST2.SelectedValue,
                                  txtaFAGRNO.Text,
                                  txtaFAGRNO2.Text,
                                  chkaFMORTGYN.Checked,
                                  txtaFMORTGYN.Text,
                                  ToSystemDate(txtaFSTDATE.Text),
                                  ToSystemDate(txtaFMORTGT.Text),
                                  txtaNote.Text,
                                  pKeyCode,
                                  Me.CurrentUser.UserID) Then
                        Call OpenDialog()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                    Else
                        Call CopyFileTempToProcess()
                        Session.Remove("TRN_DataLandBank_Datatable")
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
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim bl As cDataLandBank = New cDataLandBank
            Dim data As FD11PROP = bl.GetByID(hddKeyID.Value,
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
    '            Call UploadFileTemp(fileName)
    '            Call AddDataTable(Session("TRN_DataLandBank_Datatable"),
    '                              fileName)
    '            Call LoadImageID()
    '            If Session("TRN_DataLandBank_Datatable") IsNot Nothing Then
    '                hddpKeyIDPicture.Value = fileName.Split(".")(0)
    '                hddpNamePicture.Value = fileName
    '                Dim TempDate As String = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss")
    '                imgPic.ImageUrl = strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & fileName & "?id=" & TempDate.Substring(TempDate.Length - 2)
    '                imgPic.Attributes.Add("onClick", "CallLoadHrefNewtab('" & strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & fileName & "?id=" & TempDate.Substring(TempDate.Length - 2) & "');")
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
    '    If Session("TRN_DataLandBank_Datatable") IsNot Nothing Then
    '        dt = Session("TRN_DataLandBank_Datatable")
    '    End If

    '    If dt IsNot Nothing Then
    '        If hddpKeyIDPicture.Value <> String.Empty Then
    '            For Each i As DataRow In dt.Select("ID= '" & hddpKeyIDPicture.Value & "'")
    '                dt.Rows.Remove(i)
    '            Next
    '            If dt.Rows.Count > 0 Then
    '                Session.Add("TRN_DataLandBank_Datatable", dt)
    '            Else
    '                Session.Add("TRN_DataLandBank_Datatable", Nothing)
    '            End If
    '            Call DeleteSigleFileTemp(hddpNamePicture.Value)
    '            Call LoadImageID()
    '            If Session("TRN_DataLandBank_Datatable") IsNot Nothing Then
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
    Public Function GetData() As List(Of FD11PROP)
        Try
            Return Session("IDOCS.application.LoaddataDataLandBank")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetData(ByVal lc As List(Of FD11PROP)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataDataLandBank", lc)
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
                    dr.Item("CallPath") = strPathServer & hddParameterMenuID.Value + "/" & Me.CurrentUser.UserID & "/" & strID & "." & dts.Rows(i)("FileName").ToString.Split(".")(1)
                    dr.Item("FileName") = strID & "." & dts.Rows(i)("FileName").ToString.Split(".")(1)
                    dt.Rows.Add(dr)
                    strID = strID + 1
                Next
                dr = dt.NewRow
                dr.Item("ID") = strID
                dr.Item("CallPath") = strPathServer & hddParameterMenuID.Value + "/" & Me.CurrentUser.UserID & "/" & strID & "." & strFileName.Split(".")(1)
                dr.Item("FileName") = strID & "." & strFileName.Split(".")(1)
                dt.Rows.Add(dr)
            Else
                dr = dt.NewRow
                dr.Item("ID") = strID
                dr.Item("CallPath") = strPathServer & hddParameterMenuID.Value + "/" & Me.CurrentUser.UserID & "/" & strID & "." & strFileName.Split(".")(1)
                dr.Item("FileName") = strID & "." & strFileName.Split(".")(1)
                dt.Rows.Add(dr)
            End If
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Session.Add("TRN_DataLandBank_Datatable", dt)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "AddDataTable", ex)
        End Try
    End Sub
#End Region

#Region "LoadRepeater"
    Private Sub LoadImageID()
        'Me.repeaterImageID.DataSource = Session("TRN_DataLandBank_Datatable")
        'Me.repeaterImageID.DataBind()
        Me.rptImage.DataSource = LoadRptImageDataSource()
        Me.rptImage.DataBind()
    End Sub
    Private Function LoadRptImageDataSource() As List(Of String)
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
        'oadcub

    End Function
    'Private Sub repeaterImageID_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repeaterImageID.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '        Dim TempDate As String = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss")
    '        Dim div As Panel = e.Item.FindControl("divImageID")
    '        Dim btn As ImageButton = e.Item.FindControl("btnImg")
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
    '        If Session("TRN_DataLandBank_Datatable") IsNot Nothing Then
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

    '#Region "UploadFile"
    '    Public Sub CopyFileTempToProcess()
    '        Dim strKeyID As String = IIf(hddKeyID.Value <> String.Empty, hddKeyID.Value, txtaFASSETNO.Text)
    '        Call DeleteAllFile(strKeyID)
    '        Call CrateFolder(strKeyID)
    '        Call CopyFile(strKeyID)
    '    End Sub
    '    Public Sub CrateFolder(ByVal strKeyID As String)
    '        Dim strPath As String = Server.MapPath("IMG_SPW\" & hddParameterMenuID.Value & "\")
    '        If (Not System.IO.Directory.Exists(strPath)) Then
    '            System.IO.Directory.CreateDirectory(strPath)
    '        End If

    '        Dim strPath2 As String = Server.MapPath("IMG_SPW\" & hddParameterMenuID.Value & "\" & strKeyID & "\")
    '        If (Not System.IO.Directory.Exists(strPath2)) Then
    '            System.IO.Directory.CreateDirectory(strPath2)
    '        End If
    '    End Sub
    '    Public Sub DeleteAllFile(ByVal strKeyID As String)
    '        Dim objFSO As Object
    '        Dim objFolder As Object
    '        Dim objFile As Object
    '        Try
    '            objFSO = CreateObject("Scripting.FileSystemObject")
    '            objFolder = objFSO.GetFolder(Server.MapPath("IMG_SPW\" & hddParameterMenuID.Value & "\" & strKeyID & "\"))
    '            For Each objFile In objFolder.Files
    '                Dim TempDelete As String = Server.MapPath("IMG_SPW\" & hddParameterMenuID.Value & "\" & strKeyID & "\" & objFile.Name)
    '                If System.IO.File.Exists(TempDelete) Then
    '                    System.IO.File.Delete(TempDelete)
    '                End If
    '            Next objFile
    '        Catch ex As Exception

    '        End Try

    '    End Sub
    '    Public Sub CopyFile(ByVal strKeyID As String)
    '        Dim TempSave As String = String.Empty
    '        Dim TempPath As String = String.Empty
    '        Dim objFSO As Object
    '        Dim objFolder As Object
    '        Dim objFile As Object
    '        Try
    '            objFSO = CreateObject("Scripting.FileSystemObject")
    '            objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\"))
    '            Dim strFileName As Integer = 0
    '            For Each objFile In objFolder.Files
    '                TempSave = Server.MapPath("IMG_SPW/" & hddParameterMenuID.Value & "/" & strKeyID & "/")
    '                TempPath = Server.MapPath("Uploads\" & hddParameterMenuID.Value & "\" & Me.CurrentUser.UserID & "\" & objFile.Name)
    '                If System.IO.File.Exists(TempPath) Then
    '                    System.IO.File.Copy(TempPath, TempSave & strFileName & "." & objFile.Name.ToString.Split(".")(1))
    '                    strFileName += 1
    '                End If
    '            Next objFile

    '        Catch ex As Exception

    '        End Try
    '    End Sub
    '#End Region

    '#Region "LoadFileForEdit"
    '    Public Sub LoadFileProcessToTemp(ByVal strKeyID As String)
    '        Call CrateFolderTemp()
    '        Call LoadFileProcess(strKeyID)
    '    End Sub
    '    Public Sub LoadFileProcess(ByVal strKeyID As String)
    '        Dim objFSO As Object
    '        Dim objFolder As Object
    '        Dim objFile As Object
    '        Try
    '            objFSO = CreateObject("Scripting.FileSystemObject")
    '            objFolder = objFSO.GetFolder(Server.MapPath("IMG_SPW\" + hddParameterMenuID.Value + "\" + strKeyID + "\"))
    '            Dim strID As Integer = 0
    '            Dim strLNamefrist As String = String.Empty
    '            For Each objFile In objFolder.Files
    '                Dim ProcessPath As String = Server.MapPath("IMG_SPW/" + hddParameterMenuID.Value + "/" & hddKeyID.Value & "/" & strID & "." & objFile.Name.ToString.Split(".")(1))
    '                Dim TempSave As String = Server.MapPath("Uploads/" + hddParameterMenuID.Value + "/" & Me.CurrentUser.UserID & "/" & strID & "." & objFile.Name.ToString.Split(".")(1))
    '                If Not System.IO.File.Exists(TempSave) Then
    '                    System.IO.File.Copy(ProcessPath, TempSave)
    '                End If
    '                Call AddDataTable(Session("TRN_DataLandBank_Datatable"),
    '                                  strID & "." & objFile.Name.ToString.Split(".")(1))
    '                If strID = 0 Then
    '                    strLNamefrist = strID & "." & objFile.Name.ToString.Split(".")(1)
    '                End If
    '                strID = strID + 1
    '            Next objFile
    '            Call LoadImageID()
    '            If Session("TRN_DataLandBank_Datatable") IsNot Nothing Then
    '                hddpKeyIDPicture.Value = "0"
    '                hddpNamePicture.Value = strLNamefrist
    '                Dim TempDate As String = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss")
    '                imgPic.ImageUrl = strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & strLNamefrist & "?" & TempDate.Substring(TempDate.Length - 2)
    '                imgPic.Attributes.Add("onClick", "CallLoadHrefNewtab('" & strPathServer & hddParameterMenuID.Value & "/" & Me.CurrentUser.UserID & "/" & strLNamefrist & "?" & TempDate.Substring(TempDate.Length - 2) & "');")
    '            End If

    '        Catch ex As Exception

    '        End Try

    '    End Sub
    '#End Region

#Region "CalReverse"
    Public Sub CalReverse(ByVal pTotal As String,
                          ByRef pParam1 As String,
                          ByRef pParam2 As String,
                          ByRef pParam3 As String)
        If pTotal.IndexOf("-") = "0" Then
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

#Region "UploadFile"
    Public Sub CopyFileTempToProcess()
        Dim strKeyID As String = IIf(hddKeyID.Value <> String.Empty, hddKeyID.Value, txtaFASSETNO.Text)
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
End Class
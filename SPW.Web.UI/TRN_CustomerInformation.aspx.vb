Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class TRN_CustomerInformation
    Inherits BasePage

    Public clsCusInfo As ClsCustomer_Information = New ClsCustomer_Information
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
                Session.Remove("IDOCS.application.LoaddataStatusContCode")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("TRN_CustomerInformation.aspx")
                Me.ClearSessionPageLoad("TRN_CustomerInformation.aspx")

                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadRadioButtonList()
                Call GetParameter()
                Call getUsedCustomerCode()
                Call LoadData()

                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "AutocompletedPostal1(this,event);AutocompletedPostal2(this,event);AutocompletedPostal3(this,event);getAgeToBirthDay();", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();AutocompletedPostal1(this,event);AutocompletedPostal2(this,event);AutocompletedPostal3(this,event);getAgeToBirthDay();", True)
            End If


        Catch ex As Exception
            'HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        If hddpFlagSearch.Value = "1" Then
            Session.Remove("TRN_CustomerInformation_PageInfo")
            Session.Remove("TRN_CustomerInformation_Search")
            Session.Remove("TRN_CustomerInformation_pageLength")
            Session.Remove("TRN_CustomerInformation_keyword")
            Session.Add("TRN_CustomerInformation_keyword", IIf(txtsKeyword.Text <> String.Empty, txtsKeyword.Text, ""))
        End If
        Call Redirect("TRN_CustomerInformation.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("TRN_CustomerInformation_FS")
        Session.Add("TRN_CustomerInformation_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("TRN_CustomerInformation_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("TRN_CustomerInformation_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("TRN_CustomerInformation_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
        Session.Add("TRN_CustomerInformation_keyword", IIf(txtsKeyword.Text <> String.Empty, txtsKeyword.Text, ""))
    End Sub
    Public Sub GetParameter()
        If Session("TRN_CustomerInformation_FS") IsNot Nothing Then
            If Session("TRN_CustomerInformation_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("TRN_CustomerInformation_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG", "1") & "');", True)
                End If
                Dim strPageInfo As String = Session("TRN_CustomerInformation_PageInfo").ToString
                If strPageInfo <> String.Empty Then
                    hddpPageInfo.Value = strPageInfo
                End If

            End If
        End If

        If Session("TRN_CustomerInformation_keyword") IsNot Nothing Then
            txtsKeyword.Text = Session("TRN_CustomerInformation_keyword").ToString
        End If

        If Session("TRN_CustomerInformation_PageInfo") IsNot Nothing Then
            If Session("TRN_CustomerInformation_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("TRN_CustomerInformation_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("TRN_CustomerInformation_Search") IsNot Nothing Then
            If Session("TRN_CustomerInformation_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("TRN_CustomerInformation_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("TRN_CustomerInformation_pageLength") IsNot Nothing Then
            If Session("TRN_CustomerInformation_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("TRN_CustomerInformation_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If
    End Sub

#End Region

#Region "DropDownList"
    Public Sub LoadRadioButtonList()
        Try
            Call LoadSex(rbtaFSEX)
            rbtaFSEX.SelectedIndex = 0
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadRadioButtonList", ex)
        End Try
    End Sub
    Public Sub LoadSex(ByVal rbt As RadioButtonList)
        Try
            rbt.Items.Clear()
            rbt.Items.Insert(0, New ListItem("&nbsp;&nbsp;" & GetWebMessage("rbtaMale", "Text", hddParameterMenuID.Value) & "&nbsp;&nbsp;&nbsp;&nbsp;", "M"))
            rbt.Items.Insert(1, New ListItem("&nbsp;&nbsp;" & GetWebMessage("rbtaFemale", "Text", hddParameterMenuID.Value), "F"))

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadSex", ex)
        End Try
    End Sub

#End Region

#Region "Loaddata"
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
    Protected Sub LoadInit()
        Try
            lblMain1.Text = Me.GetResource("main_label", "Text", "1")
            lblMain2.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
            lblMain3.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
            lblMain4.Text = Me.GetResource("HeaderDialog", "Text", hddParameterMenuID.Value)

            lblsKeyword.Text = Me.GetResource("lblsKeyword", "Text", hddParameterMenuID.Value)
            lblsKeywordDetail.Text = Me.GetResource("lblsKeywordDetail", "Text", hddParameterMenuID.Value)
            lblsKeywordDetail.Text = String.Format(Me.GetResource("lblsKeywordDetail", "Text", hddParameterMenuID.Value),
                                                   Me.GetResource("FContCode", "Text", hddParameterMenuID.Value) &
                                                   ", " & Me.GetResource("FConName", "Text", hddParameterMenuID.Value) &
                                                   ", " & Me.GetResource("FTelNo", "Text", hddParameterMenuID.Value) &
                                                   ", " & Me.GetResource("FPeopleID", "Text", hddParameterMenuID.Value))

            lblsSearch.Text = Me.GetResource("lblsSearch", "Text", hddParameterMenuID.Value)

            lblaFCONTCODE.Text = Me.GetResource("lblaFCONTCODE", "Text", hddParameterMenuID.Value)
            chbaFPRECODE.Text = "&nbsp;" & Me.GetResource("chbaFPRECODE", "Text", hddParameterMenuID.Value)
            lblaFSTDATE.Text = Me.GetResource("lblaFSTDATE", "Text", hddParameterMenuID.Value)
            lblaFCONTTNM.Text = Me.GetResource("lblaFCONTTNM", "Text", hddParameterMenuID.Value)
            lblaFBIRTH.Text = Me.GetResource("lblaFBIRTH", "Text", hddParameterMenuID.Value)
            lblaFBIRTH2.Text = Me.GetResource("lblaFBIRTH2", "Text", hddParameterMenuID.Value)
            lblaAge.Text = Me.GetResource("lblaAge", "Text", hddParameterMenuID.Value)
            lblaFCONTENM.Text = Me.GetResource("lblaFCONTENM", "Text", hddParameterMenuID.Value)
            lblaFPEOPLEID.Text = Me.GetResource("lblaFPEOPLEID", "Text", hddParameterMenuID.Value)
            lblaTaxNo.Text = Me.GetResource("lblaTaxNo", "Text", hddParameterMenuID.Value)
            lblaFADD1.Text = Me.GetResource("lblaFADD1", "Text", hddParameterMenuID.Value)
            chbaIsThailandor.Text = "&nbsp;" & Me.GetResource("chbaIsThailandor", "Text", hddParameterMenuID.Value)
            lblaFTELNO.Text = Me.GetResource("lblaFTELNO", "Text", hddParameterMenuID.Value)
            lblaFADD3.Text = Me.GetResource("lblaFADD3", "Text", hddParameterMenuID.Value)
            lblaFMOBILE.Text = Me.GetResource("lblaFMOBILE", "Text", hddParameterMenuID.Value)
            lblaFPROVINCE.Text = Me.GetResource("lblaFPROVINCE", "Text", hddParameterMenuID.Value)
            lblaFFAX.Text = Me.GetResource("lblaFFAX", "Text", hddParameterMenuID.Value)
            lblaFLINE.Text = Me.GetResource("lblaFLINE", "Text", hddParameterMenuID.Value)
            lblaFFACEBOOK.Text = Me.GetResource("lblaFFACEBOOK", "Text", hddParameterMenuID.Value)
            lblaFEMAIL.Text = Me.GetResource("lblaFEMAIL", "Text", hddParameterMenuID.Value)

            lblPerSon.Text = Me.GetResource("lblPerSon", "Text", hddParameterMenuID.Value)
            lblaFHADD1.Text = Me.GetResource("lblaFHADD1", "Text", hddParameterMenuID.Value)
            lblaFSEX.Text = Me.GetResource("lblaFSEX", "Text", hddParameterMenuID.Value)
            lblaFHADD2.Text = Me.GetResource("lblaFHADD2", "Text", hddParameterMenuID.Value)
            lblaFHADD3.Text = Me.GetResource("lblaFHADD3", "Text", hddParameterMenuID.Value)
            lblaFHTEL.Text = Me.GetResource("lblaFHTEL", "Text", hddParameterMenuID.Value)
            lblaFHPROVINCE.Text = Me.GetResource("lblaFHPROVINCE", "Text", hddParameterMenuID.Value)
            lblaFHPOSTAL.Text = Me.GetResource("lblaFHPOSTAL", "Text", hddParameterMenuID.Value)

            lblOffice.Text = Me.GetResource("lblOffice", "Text", hddParameterMenuID.Value)
            lblaFOFFNAME.Text = Me.GetResource("lblaFOFFNAME", "Text", hddParameterMenuID.Value)
            lblaFPOSITION.Text = Me.GetResource("lblaFPOSITION", "Text", hddParameterMenuID.Value)
            lblaFOFFADD1.Text = Me.GetResource("lblaFOFFADD1", "Text", hddParameterMenuID.Value)
            lblaFOFFADD2.Text = Me.GetResource("lblaFOFFADD2", "Text", hddParameterMenuID.Value)
            lblaFOFFADD3.Text = Me.GetResource("lblaFOFFADD3", "Text", hddParameterMenuID.Value)
            lblaFOFFTEL.Text = Me.GetResource("lblaFOFFTEL", "Text", hddParameterMenuID.Value)
            lblaFOFFPROV.Text = Me.GetResource("lblaFOFFPROV", "Text", hddParameterMenuID.Value)
            lblaFOFFZIP.Text = Me.GetResource("lblaFOFFZIP", "Text", hddParameterMenuID.Value)
            lblaFOFFICETYPE.Text = Me.GetResource("lblaFOFFICETYPE", "Text", hddParameterMenuID.Value)

            TextHd1.Text = Me.GetResource("No", "Text", "1")
            TextHd2.Text = Me.GetResource("FContCode", "Text", hddParameterMenuID.Value)
            TextHd3.Text = Me.GetResource("FConName", "Text", hddParameterMenuID.Value)
            TextHd4.Text = Me.GetResource("FTelNo", "Text", hddParameterMenuID.Value)
            TextHd5.Text = Me.GetResource("FPeopleID", "Text", hddParameterMenuID.Value)
            TextHd6.Text = Me.GetResource("col_edit", "Text", "1")
            TextHd7.Text = Me.GetResource("col_delete", "Text", "1")

            TextFt1.Text = Me.GetResource("No", "Text", "1")
            TextFt2.Text = Me.GetResource("FContCode", "Text", hddParameterMenuID.Value)
            TextFt3.Text = Me.GetResource("FConName", "Text", hddParameterMenuID.Value)
            TextFt4.Text = Me.GetResource("FTelNo", "Text", hddParameterMenuID.Value)
            TextFt5.Text = Me.GetResource("FPeopleID", "Text", hddParameterMenuID.Value)
            TextFt6.Text = Me.GetResource("col_edit", "Text", "1")
            TextFt7.Text = Me.GetResource("col_delete", "Text", "1")

            lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG", "1")
            hddBodydelete.Value = Me.GetResource("msg_body_delete", "MSG", "1") & " " & Me.GetResource("FContCode", "Text", hddParameterMenuID.Value)

            hddMSGSaveData.Value = Me.GetResource("msg_save_data", "MSG", "1")
            hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG", "1")
            hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG", "1")
            hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG", "1")
            hddMSGAddData.Value = Me.GetResource("msg_add_data", "MSG", "1")

            hddMsgMobileFormat.Value = Me.GetResource("msg_mobile_format", "MSG", hddParameterMenuID.Value)

            TextHd2_1.Text = Me.GetResource("No", "Text", "1")
            TextHd2_2.Text = Me.GetResource("FREPRJNO", "Text", hddParameterMenuID.Value)
            TextHd2_3.Text = Me.GetResource("FREPHASE", "Text", hddParameterMenuID.Value)
            TextHd2_4.Text = Me.GetResource("FSERIALNO", "Text", hddParameterMenuID.Value)
            TextHd2_5.Text = Me.GetResource("FADDRNO", "Text", hddParameterMenuID.Value)
            TextHd2_6.Text = Me.GetResource("FAGRNO", "Text", hddParameterMenuID.Value)
            TextHd2_7.Text = Me.GetResource("FStatus", "Text", hddParameterMenuID.Value)

            TextFt2_1.Text = Me.GetResource("No", "Text", "1")
            TextFt2_2.Text = Me.GetResource("FREPRJNO", "Text", hddParameterMenuID.Value)
            TextFt2_3.Text = Me.GetResource("FREPHASE", "Text", hddParameterMenuID.Value)
            TextFt2_4.Text = Me.GetResource("FSERIALNO", "Text", hddParameterMenuID.Value)
            TextFt2_5.Text = Me.GetResource("FADDRNO", "Text", hddParameterMenuID.Value)
            TextFt2_6.Text = Me.GetResource("FAGRNO", "Text", hddParameterMenuID.Value)
            TextFt2_7.Text = Me.GetResource("FStatus", "Text", hddParameterMenuID.Value)

            hddCulture.Value = WebCulture().ToLower.ToString

            lblMassage1.Text = Me.GetResource("msg_required", "MSG", "1")
            lblMassage2.Text = Me.GetResource("msg_required", "MSG", "1")
            lblMassage3.Text = Me.GetResource("msg_required", "MSG", "1")
            lblMassage4.Text = Me.GetResource("msg_required", "MSG", "1")
            lblMassage5.Text = Me.GetResource("msg_required", "MSG", "1")
            lblMassage6.Text = grtt("resPleaseEnter")

            lblsMessageFPEOPLEID.Text = grtt("resPlease13Digit")
            lblsMessageTaxNo.Text = grtt("resPlease13Digit")
            Call LoadPeopleIDMaster()
            Call ControlValidate()

            Call clearText()

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadInit", ex)
        End Try
    End Sub
    Protected Sub LoadData()
        Try
            hddReloadGrid.Value = "1"
            Dim fillter As FillterSearch = New FillterSearch
            Dim bl As cCustomerInformation = New cCustomerInformation
            fillter.Keyword = txtsKeyword.Text
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType

            Dim lst As List(Of OD50RCVD) = bl.Loaddata(fillter,
                                                            Me.TotalRow,
                                                            Me.CurrentUser.UserID)

            If txtsKeyword.Text <> String.Empty Then
                If lst IsNot Nothing Then
                    Call SetDataCustomerInfo(lst)
                Else
                    Call SetDataCustomerInfo(Nothing)
                End If
            Else
                Call SetDataCustomerInfo(Nothing)
            End If

            Session.Remove("TRN_CustomerInformation_FS")
            Session.Remove("TRN_CustomerInformation_PageInfo")
            Session.Remove("TRN_CustomerInformation_keyword")


        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadData", ex)
            Session.Remove("TRN_CustomerInformation_FS")
            Session.Remove("TRN_CustomerInformation_PageInfo")
            Session.Remove("TRN_CustomerInformation_keyword")
        End Try
    End Sub

    Protected Sub LoadDataStatusContCode()
        Try

            hddReloadGridEdit.Value = "1"
            If hddKeyID.Value <> String.Empty Then
                Dim ds As DataSet = clsCusInfo.getStatusContCode(hddKeyID.Value)
                If ds IsNot Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Call SetDataStatusContCode(ds)
                    Else
                        Call SetDataStatusContCode(Nothing)
                    End If
                Else
                    Call SetDataStatusContCode(Nothing)
                End If
            Else
                Call SetDataStatusContCode(Nothing)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub clearText()
        txtaFCONTCODE.Text = String.Empty
        chbaFPRECODE.Checked = False
        lblaFBIRTH.Style.Add("display", "none")
        lblaFBIRTH2.Style.Add("display", "")
        txtaFCONTTNM3.Style.Add("display", "none")
        ' txtaFSTDATE.Text = Date.Now.ToString("dd/MM/yyyy") 'Date
        'txtaFSTDATEEdit.Text = Date.Now.ToString("dd/MM/yyyy") 'Date
        txtaFSTDATE.Text = ToStringByCulture(Date.Now)
        txtaFSTDATEEdit.Text = ToStringByCulture(Date.Now)
        hddFPRELEN.Value = String.Empty
        txtaFCONTTNM1.Text = String.Empty
        txtaFCONTTNM2.Text = String.Empty
        txtaFCONTTNM3.Text = String.Empty
        txtaFBIRTH.Text = String.Empty 'Date
        txtaAge.Text = String.Empty
        txtaFCONTENM.Text = String.Empty
        txtaFPEOPLEID.Text = String.Empty
        txtaFADD1.Text = String.Empty
        chbaIsThailandor.Checked = True
        txtaFADD2.Text = String.Empty
        txtaFTELNO.Text = String.Empty
        txtaFADD3.Text = String.Empty
        txtaFMOBILE1.Text = String.Empty
        txtaFMOBILE2.Text = String.Empty
        txtaFPROVINCE.Text = String.Empty
        txtaFPOSTAL.Text = String.Empty
        hddaFPROVCD.Value = String.Empty
        hddaFCITYCD.Value = String.Empty
        txtaFFAX.Text = String.Empty
        txtaFLINE.Text = String.Empty
        txtaFFACEBOOK.Text = String.Empty
        txtaFEMAIL.Text = String.Empty


        txtaFHADD1.Text = String.Empty
        rbtaFSEX.SelectedIndex = 0
        txtaFHADD2.Text = String.Empty
        txtaFHADD3.Text = String.Empty
        txtaFHTEL.Text = String.Empty
        txtaFHPROVINCE.Text = String.Empty
        hddaFHPROVCD.Value = String.Empty
        hddaFHCITYCD.Value = String.Empty
        txtaFHPOSTAL.Text = String.Empty

        txtaTaxNo.Text = String.Empty


        txtaFOFFNAME.Text = String.Empty
        txtaFPOSITION.Text = String.Empty
        txtaFOFFADD1.Text = String.Empty
        txtaFOFFADD2.Text = String.Empty
        txtaFOFFADD3.Text = String.Empty
        txtaFOFFTEL.Text = String.Empty
        txtaFOFFPROV.Text = String.Empty
        hddaFOFFPROVCD.Value = String.Empty
        hddaFOFFCITYCD.Value = String.Empty
        txtaFOFFZIP.Text = String.Empty
        txtaFOFFICETYPE.Text = String.Empty


    End Sub

    Public Sub LoadPeopleIDMaster()
        Dim bl As New cCustomerInformation
        hddCheckPeopleID.Value = String.Join(",", bl.LoadPeopleIDMaster())
    End Sub
#End Region

#Region "Function"
    Public Sub getUsedCustomerCode()
        Try
            Dim bl As cCustomerInformation = New cCustomerInformation
            Dim lc As List(Of String) = bl.getUsedCustomerCode
            hddCheckUsedCustomer.Value = String.Empty
            If lc IsNot Nothing Then
                If lc.Count > 0 Then
                    hddCheckUsedCustomer.Value = String.Join(",", lc)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadRadioButtonList", ex)
        End Try
    End Sub

    Public Sub Edit()
        Try
            txtaFSTDATE.Style.Add("display", "none")
            txtaFSTDATEEdit.Style.Add("display", "")


            Dim bl As cCustomerInformation = New cCustomerInformation
            Dim lc As OD50RCVD = bl.LoadEditCusInfo(hddKeyID.Value,
                                                    CurrentUser.UserID)
            If lc IsNot Nothing Then
                hddKeyID.Value = lc.FCONTCODE
                txtaFCONTCODE.Text = lc.FCONTCODE
                chbaFPRECODE.Checked = IIf(lc.FPRECODE IsNot Nothing, IIf(lc.FPRECODE = "0", True, False), False)
                If chbaFPRECODE.Checked Then
                    txtaFCONTTNM3.Style.Add("display", "")
                    lblaFBIRTH.Style.Add("display", "")
                    lblaFBIRTH2.Style.Add("display", "none")
                Else
                    lblaFBIRTH.Style.Add("display", "none")
                    lblaFBIRTH2.Style.Add("display", "")
                End If
                If Not IsDBNull(lc.FSTDATE) Then
                    If lc.FSTDATE IsNot Nothing Then
                        'Dim TempDate As String = lc.FSTDATE
                        'txtaFSTDATE.Text = CDate(TempDate.Split(" ")(0)).ToString("dd/MM/yyyy")
                        'txtaFSTDATEEdit.Text = CDate(TempDate.Split(" ")(0)).ToString("dd/MM/yyyy")
                        txtaFSTDATE.Text = ToStringByCulture(lc.FSTDATE.Value)
                        txtaFSTDATEEdit.Text = ToStringByCulture(lc.FSTDATE.Value)
                    End If
                End If
                hddFPRELEN.Value = lc.FPRELEN.ToString
                If Not IsDBNull(lc.FCONTTNM) Then
                    Try
                        txtaFCONTTNM1.Text = lc.FCONTTNM.Substring(0, hddFPRELEN.Value).ToString
                    Catch ex As Exception
                    End Try
                    Dim strTempFCONTTNM As String = lc.FCONTTNM.Substring(hddFPRELEN.Value, lc.FCONTTNM.Length - CInt(hddFPRELEN.Value)).ToString
                    Dim nameLength As Integer = 0
                    If chbaFPRECODE.Checked Then
                        Try
                            txtaFCONTTNM2.Text = strTempFCONTTNM.Split(" ")(0)
                            nameLength = strTempFCONTTNM.Split(" ")(0).Length
                        Catch ex As Exception
                            txtaFCONTTNM2.Text = lc.FCONTTNM.ToString
                        End Try
                        Try
                            txtaFCONTTNM3.Text = strTempFCONTTNM.Substring(nameLength)
                        Catch ex As Exception
                            txtaFCONTTNM3.Text = lc.FCONTTNM.ToString
                        End Try
                    Else
                        txtaFCONTTNM2.Text = strTempFCONTTNM
                    End If
                End If
                If Not IsDBNull(lc.FBIRTH) Then
                    If lc.FBIRTH IsNot Nothing Then
                        'Dim TempDate As String = lc.FBIRTH
                        'txtaFBIRTH.Text = CDate(TempDate.Split(" ")(0)).ToString("dd/MM/yyyy")
                        txtaFBIRTH.Text = ToStringByCulture(lc.FBIRTH.Value)
                    End If
                End If
                If txtaFBIRTH.Text = String.Empty Then
                    txtaAge.Text = ""
                Else
                    'Dim vPBirth As String = txtaFBIRTH.Text
                    'Dim vValue As Date = CDate(ToSystemDateString(vPBirth)) 'Date.ParseExact(txtaFBIRTH.Text, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo) 'System.Globalization.DateTimeFormatInfo.InvariantInfo,
                    'Dim vValueNow As Date = Date.Now 'CDate(ToSystemDate(Date.Now.ToString("dd/MM/yyyy"))) ' Date.ParseExact(Date.Now, "dd/MM/yyyy", New CultureInfo("en-US"))
                    'txtaAge.Text = Format(Year(vValueNow) - Year(vValue) + 1, "##0")
                End If
                txtaFCONTENM.Text = lc.FCONTENM
                If Not IsDBNull(lc.FPEOPLEID) Then
                    If lc.FPEOPLEID IsNot Nothing Then
                        txtaFPEOPLEID.Text = lc.FPEOPLEID.Replace(" ", "")
                    End If
                End If
                If Not IsDBNull(lc.FADD1) Then txtaFADD1.Text = lc.FADD1
                chbaIsThailandor.Checked = IIf(lc.IsThailandor = "1", True, False)
                If Not IsDBNull(lc.FADD2) Then txtaFADD2.Text = lc.FADD2
                If Not IsDBNull(lc.FTELNO) Then txtaFTELNO.Text = lc.FTELNO
                If Not IsDBNull(lc.FADD3) Then txtaFADD3.Text = lc.FADD3
                If Not IsDBNull(lc.FMOBILE) Then txtaFMOBILE1.Text = lc.FMOBILE
                If Not IsDBNull(lc.FMOBILE2) Then txtaFMOBILE2.Text = lc.FMOBILE2
                If Not IsDBNull(lc.FPROVINCE) Then txtaFPROVINCE.Text = lc.FPROVINCE
                If Not IsDBNull(lc.FPOSTAL) Then txtaFPOSTAL.Text = lc.FPOSTAL
                If Not IsDBNull(lc.FPROVCD) Then hddaFPROVCD.Value = lc.FPROVCD
                If Not IsDBNull(lc.FCITYCD) Then hddaFCITYCD.Value = lc.FCITYCD
                If Not IsDBNull(lc.FFAX) Then txtaFFAX.Text = lc.FFAX
                If Not IsDBNull(lc.FLINE) Then txtaFLINE.Text = lc.FLINE
                If Not IsDBNull(lc.FFACEBOOK) Then txtaFFACEBOOK.Text = lc.FFACEBOOK
                If Not IsDBNull(lc.FEMAIL) Then txtaFEMAIL.Text = lc.FEMAIL


                If Not IsDBNull(lc.FHADD1) Then txtaFHADD1.Text = lc.FHADD1
                If Not IsDBNull(lc.FSEX) Then rbtaFSEX.SelectedIndex = IIf(lc.FSEX = "M", 0, 1)
                If Not IsDBNull(lc.FHADD2) Then txtaFHADD2.Text = lc.FHADD2
                If Not IsDBNull(lc.FHADD3) Then txtaFHADD3.Text = lc.FHADD3
                If Not IsDBNull(lc.FHTEL) Then txtaFHTEL.Text = lc.FHTEL
                If Not IsDBNull(lc.FHPROVINCE) Then txtaFHPROVINCE.Text = lc.FHPROVINCE
                If Not IsDBNull(lc.FHPROVCD) Then hddaFHPROVCD.Value = lc.FHPROVCD
                If Not IsDBNull(lc.FHCITYCD) Then hddaFHCITYCD.Value = lc.FHCITYCD
                If Not IsDBNull(lc.FHPOSTAL) Then txtaFHPOSTAL.Text = lc.FHPOSTAL

                If Not IsDBNull(lc.FOFFNAME) Then txtaFOFFNAME.Text = lc.FOFFNAME
                If Not IsDBNull(lc.FPOSITION) Then txtaFPOSITION.Text = lc.FPOSITION
                If Not IsDBNull(lc.FOFFADD1) Then txtaFOFFADD1.Text = lc.FOFFADD1
                If Not IsDBNull(lc.FOFFADD2) Then txtaFOFFADD2.Text = lc.FOFFADD2
                If Not IsDBNull(lc.FOFFADD3) Then txtaFOFFADD3.Text = lc.FOFFADD3
                If Not IsDBNull(lc.FOFFTEL) Then txtaFOFFTEL.Text = lc.FOFFTEL
                If Not IsDBNull(lc.FOFFPROV) Then txtaFOFFPROV.Text = lc.FOFFPROV
                If Not IsDBNull(lc.FOFFPROVCD) Then hddaFOFFPROVCD.Value = lc.FOFFPROVCD
                If Not IsDBNull(lc.FOFFCITYCD) Then hddaFOFFCITYCD.Value = lc.FOFFCITYCD
                If Not IsDBNull(lc.FOFFZIP) Then txtaFOFFZIP.Text = lc.FOFFZIP
                If Not IsDBNull(lc.FOFFICETYPE) Then txtaFOFFICETYPE.Text = lc.FOFFICETYPE
                If Not IsDBNull(lc.FTAXNO) Then txtaTaxNo.Text = lc.FTAXNO

                Call LoadDataStatusContCode()
                Call LoadFileProcess()
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Event"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            hddpFlagSearch.Value = "1"
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSearch_Click", ex)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Call clearText()

            hddKeyID.Value = String.Empty
            txtaFSTDATE.Style.Add("display", "")
            txtaFSTDATEEdit.Style.Add("display", "none")

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try

            Call clearText()
            Call Edit()
            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim bl As cCustomerInformation = New cCustomerInformation
            Dim lc As OD50RCVD = bl.LoadEditCusInfo(hddKeyID.Value,
                                                 Me.CurrentUser.UserID)
            If lc IsNot Nothing Then
                If Not bl.Delete(hddKeyID.Value,
                                 Me.CurrentUser.UserID) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_cannot_delete_because ", "MSG", "1") & "');", True)
                Else
                    Dim strKeyID As String = txtaFCONTCODE.Text & "-0"
                    Call DeleteAllFile(strKeyID)
                    Call LoadRedirec("2")
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_deletenotsuccess", "MSG", "1") & "');", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnDelete_Click", ex)
        End Try
    End Sub

    ''''cSQL$ = "select * from od50rcvd where fcontcode like '" & Right(CStr(Year(MDate)), 2) & "%' order by fcontcode"
    ''''Set rstTempA = dbsDBF.OpenRecordset(cSQL$)
    ''''If rstTempA.RecordCount <> 0 Then
    ''''              rstTempA.MoveLast 'ล่าสุด
    ''''              cCode = Mid(rstTempA!FCONTCODE, 3, 6)
    ''''              If Not IsNumeric(cCode) Then
    ''''                  MsgBox "Can't assign new contactor code, please entry CONTACTOR CODE.", vbCritical
    ''''    Exit Sub
    ''''              Else
    ''''                  cCode = Right(CStr(Year(MDate)), 2) + Format(Val(cCode) + 1, "000000")
    ''''              End If
    ''''          Else
    ''''              cCode = Right(CStr(Year(MDate)), 2) + "000001"
    ''''          End If
    ''''          rstTempA.Close
    ''''          txtKey(0).Text = cCode

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim message As String = ""
            Dim succ As Boolean = True
            Dim bl As cCustomerInformation = New cCustomerInformation
            Dim data As OD50RCVD = Nothing

            hddaFPROVCD.Value = String.Empty
            If txtaFPROVINCE.Text <> String.Empty Then
                Dim lcProv1 As LD07AZIP = bl.LoadFPROVINCECode(txtaFPROVINCE.Text)
                If lcProv1 IsNot Nothing Then hddaFPROVCD.Value = lcProv1.FPROVCD
            End If

            hddaFHPROVCD.Value = String.Empty
            If txtaFHPROVINCE.Text <> String.Empty Then
                Dim lcProv2 As LD07AZIP = bl.LoadFPROVINCECode(txtaFHPROVINCE.Text)
                If lcProv2 IsNot Nothing Then hddaFHPROVCD.Value = lcProv2.FPROVCD
            End If

            hddaFOFFPROVCD.Value = String.Empty
            If txtaFOFFPROV.Text <> String.Empty Then
                Dim lcProv3 As LD07AZIP = bl.LoadFPROVINCECode(txtaFOFFPROV.Text)
                If lcProv3 IsNot Nothing Then hddaFOFFPROVCD.Value = lcProv3.FPROVCD
            End If

            hddaFCITYCD.Value = String.Empty
            If txtaFADD3.Text <> String.Empty Then
                Dim lcCity1 As LD07AZIP = bl.LoadFCITYCode(txtaFADD3.Text)
                If lcCity1 IsNot Nothing Then hddaFCITYCD.Value = lcCity1.FCITYCD
            End If

            hddaFHCITYCD.Value = String.Empty
            If txtaFHADD3.Text <> String.Empty Then
                Dim lcCity2 As LD07AZIP = bl.LoadFCITYCode(txtaFHADD3.Text)
                If lcCity2 IsNot Nothing Then hddaFHCITYCD.Value = lcCity2.FCITYCD
            End If

            hddaFOFFCITYCD.Value = String.Empty
            If txtaFOFFADD3.Text <> String.Empty Then
                Dim lcCity3 As LD07AZIP = bl.LoadFCITYCode(txtaFOFFADD3.Text)
                If lcCity3 IsNot Nothing Then hddaFOFFCITYCD.Value = lcCity3.FCITYCD
            End If

            hddFPRELEN.Value = IIf(txtaFCONTTNM1.Text <> String.Empty, txtaFCONTTNM1.Text.Length, "0")
            Dim pFCONTTNM As String = ""


            If hddKeyID.Value <> "" Then

                pFCONTTNM = txtaFCONTTNM1.Text
                pFCONTTNM += txtaFCONTTNM2.Text
                If txtaFCONTTNM3.Text <> String.Empty Then pFCONTTNM += " " & txtaFCONTTNM3.Text

                If Not bl.Edit(hddKeyID.Value,
                                "",
                                chbaFPRECODE.Checked.ToString,
                                hddFPRELEN.Value,
                                pFCONTTNM,
                                txtaFCONTENM.Text,
                                ToSystemDateString(txtaFBIRTH.Text),
                                rbtaFSEX.SelectedValue.ToString.ToUpper,
                                txtaFADD1.Text,
                                txtaFADD2.Text,
                                txtaFADD3.Text,
                                txtaFPROVINCE.Text,
                                txtaFPOSTAL.Text,
                                hddaFPROVCD.Value,
                                hddaFCITYCD.Value,
                                txtaFTELNO.Text,
                                txtaFFAX.Text,
                                txtaFEMAIL.Text,
                                txtaFMOBILE1.Text,
                                txtaFPEOPLEID.Text,
                                "",
                                "",
                                "",
                                txtaFHADD1.Text,
                                txtaFHADD2.Text,
                                txtaFHADD3.Text,
                                txtaFHPROVINCE.Text,
                                txtaFHPOSTAL.Text,
                                hddaFHPROVCD.Value,
                                hddaFHCITYCD.Value,
                                txtaFHTEL.Text,
                                txtaFOFFNAME.Text,
                                txtaFOFFADD1.Text,
                                txtaFOFFADD2.Text,
                                txtaFOFFADD3.Text,
                                txtaFOFFPROV.Text,
                                txtaFOFFZIP.Text,
                                hddaFOFFPROVCD.Value,
                                hddaFOFFCITYCD.Value,
                                txtaFOFFTEL.Text,
                                txtaFOFFICETYPE.Text,
                                txtaFPOSITION.Text,
                                "",
                                "",
                                ToSystemDateString(txtaFSTDATE.Text),
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "txtaFUPDDATE.Text",
                                "txtaFUPDBY.Text",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                chbaIsThailandor.Checked.ToString,
                                "",
                                "pStmDate",
                                "LastUpDate",
                                txtaFMOBILE2.Text,
                                txtaFLINE.Text,
                                txtaFFACEBOOK.Text,
                                txtaTaxNo.Text,
                                Me.CurrentUser.UserID) Then
                    'Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                Else
                    If FileUpload.HasFile Then
                        Call CopyFileTempToProcess()
                        Call LoadFileProcess()
                    ElseIf hddCheckDeletePicture.Value = "1" Then
                        Dim strKeyID As String = txtaFCONTCODE.Text & "-0"
                        Call DeleteAllFile(strKeyID)
                    End If
                    Call Edit()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                    'Call LoadRedirec("1")
                End If

            Else
                pFCONTTNM = txtaFCONTTNM1.Text
                pFCONTTNM += txtaFCONTTNM2.Text
                If txtaFCONTTNM3.Text <> String.Empty Then pFCONTTNM += " " & txtaFCONTTNM3.Text

                Dim lc As OD50RCVD = bl.GetOd50rcvdByName(pFCONTTNM,
                                                          txtaFCONTENM.Text)
                If lc Is Nothing Then
                    Dim MDate As Date = ToSystemDate(txtaFSTDATE.Text)

                    'Dim cCode As String = clsCusInfo.getOd50rcvd(Right(CStr(Year(MDate)), 2))
                    Dim sYear = Right(ToSystemDateString(MDate), 2)
                    Dim cCode As String = clsCusInfo.getOd50rcvd(sYear)
                    If cCode = "" Then
                        cCode = sYear + "000001"

                    Else
                        cCode = Mid(cCode, 3, 6)
                        If Not IsNumeric(cCode) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('Can''t assign new contactor code, please entry CONTACTOR CODE.');", True)
                            Exit Sub
                        Else
                            cCode = sYear + Format(Val(cCode) + 1, "000000")
                        End If
                    End If


                    If Not bl.Add(cCode,
                                "",
                                chbaFPRECODE.Checked.ToString,
                                hddFPRELEN.Value,
                                pFCONTTNM,
                                txtaFCONTENM.Text,
                                ToSystemDateString(txtaFBIRTH.Text),
                                rbtaFSEX.SelectedValue.ToString.ToUpper,
                                txtaFADD1.Text,
                                txtaFADD2.Text,
                                txtaFADD3.Text,
                                txtaFPROVINCE.Text,
                                txtaFPOSTAL.Text,
                                hddaFPROVCD.Value,
                                hddaFCITYCD.Value,
                                txtaFTELNO.Text,
                                txtaFFAX.Text,
                                txtaFEMAIL.Text,
                                txtaFMOBILE1.Text,
                                txtaFPEOPLEID.Text,
                                "",
                                "",
                                "",
                                txtaFHADD1.Text,
                                txtaFHADD2.Text,
                                txtaFHADD3.Text,
                                txtaFHPROVINCE.Text,
                                txtaFHPOSTAL.Text,
                                hddaFHPROVCD.Value,
                                hddaFHCITYCD.Value,
                                txtaFHTEL.Text,
                                txtaFOFFNAME.Text,
                                txtaFOFFADD1.Text,
                                txtaFOFFADD2.Text,
                                txtaFOFFADD3.Text,
                                txtaFOFFPROV.Text,
                                txtaFOFFZIP.Text,
                                hddaFOFFPROVCD.Value,
                                hddaFOFFCITYCD.Value,
                                txtaFOFFTEL.Text,
                                txtaFOFFICETYPE.Text,
                                txtaFPOSITION.Text,
                                "",
                                "",
                                ToSystemDateString(txtaFSTDATE.Text),
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                chbaIsThailandor.Checked.ToString,
                                "",
                                "pStmDate",
                                "LastUpDate",
                                txtaFMOBILE2.Text,
                                txtaFLINE.Text,
                                txtaFFACEBOOK.Text,
                                txtaTaxNo.Text,
                                Me.CurrentUser.UserID) Then

                        'Call OpenDialog()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                    Else
                        txtaFCONTCODE.Text = cCode

                        If FileUpload.HasFile Then
                            Call CopyFileTempToProcess()
                            Call LoadFileProcess()
                        ElseIf hddCheckDeletePicture.Value = "1" Then
                            Dim strKeyID As String = txtaFCONTCODE.Text & "-0"
                            Call DeleteAllFile(strKeyID)
                        End If
                        hddKeyID.Value = cCode
                        Call Edit()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                        'Call LoadRedirec("1")
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
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnCancel_Click", ex)
        End Try
    End Sub

    Protected Sub btnReload1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReload1.Click
        Try
            hddpAutoCode1.Value = String.Empty
            If hddpAutoName1.Value <> String.Empty Then
                txtaFADD3.Text = hddpAutoName1.Value.Split(" ")(0)
                txtaFPROVINCE.Text = hddpAutoName1.Value.Split(" ")(1)
                txtaFPOSTAL.Text = hddpAutoName1.Value.Split(" ")(2)
            Else
                If txtaFADD3.Text <> String.Empty Then
                    hddpAutoCode1.Value = txtaFADD3.Text
                    hddpAutoName1.Value = txtaFADD3.Text & " " &
                                         txtaFPROVINCE.Text & " " &
                                         txtaFPOSTAL.Text

                    txtaFADD3.Text = hddpAutoName1.Value.Split(" ")(0)
                    txtaFPROVINCE.Text = hddpAutoName1.Value.Split(" ")(1)
                    txtaFPOSTAL.Text = hddpAutoName1.Value.Split(" ")(2)
                End If
            End If
            hddaFPROVCD.Value = String.Empty
            hddaFCITYCD.Value = String.Empty
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReload1_Click", ex)
        End Try
    End Sub


    Protected Sub btnReload2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReload2.Click
        Try
            hddpAutoCode2.Value = String.Empty
            If hddpAutoName2.Value <> String.Empty Then
                txtaFHADD3.Text = hddpAutoName2.Value.Split(" ")(0)
                txtaFHPROVINCE.Text = hddpAutoName2.Value.Split(" ")(1)
                txtaFHPOSTAL.Text = hddpAutoName2.Value.Split(" ")(2)
            Else
                If txtaFHADD3.Text <> String.Empty Then
                    hddpAutoCode2.Value = txtaFHADD3.Text
                    hddpAutoName2.Value = txtaFHADD3.Text & " " &
                                         txtaFHPROVINCE.Text & " " &
                                         txtaFHPOSTAL.Text

                    txtaFHADD3.Text = hddpAutoName2.Value.Split(" ")(0)
                    txtaFHPROVINCE.Text = hddpAutoName2.Value.Split(" ")(1)
                    txtaFHPOSTAL.Text = hddpAutoName2.Value.Split(" ")(2)
                End If
            End If
            hddaFHPROVCD.Value = String.Empty
            hddaFHCITYCD.Value = String.Empty

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReload2_Click", ex)
        End Try
    End Sub

    Protected Sub btnReload3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReload3.Click
        Try
            hddpAutoCode3.Value = String.Empty
            If hddpAutoName3.Value <> String.Empty Then
                txtaFOFFADD3.Text = hddpAutoName3.Value.Split(" ")(0)
                txtaFOFFPROV.Text = hddpAutoName3.Value.Split(" ")(1)
                txtaFOFFZIP.Text = hddpAutoName3.Value.Split(" ")(2)
            Else
                If txtaFOFFADD3.Text <> String.Empty Then
                    hddpAutoCode3.Value = txtaFOFFADD3.Text
                    hddpAutoName3.Value = txtaFOFFADD3.Text & " " &
                                         txtaFOFFPROV.Text & " " &
                                         txtaFOFFZIP.Text

                    txtaFOFFADD3.Text = hddpAutoName3.Value.Split(" ")(0)
                    txtaFOFFPROV.Text = hddpAutoName3.Value.Split(" ")(1)
                    txtaFOFFZIP.Text = hddpAutoName3.Value.Split(" ")(2)
                End If
            End If
            hddaFOFFPROVCD.Value = String.Empty
            hddaFOFFCITYCD.Value = String.Empty
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReload3_Click", ex)
        End Try
    End Sub

#End Region

#Region "ControlPanel"
    Public Sub OpenMain()
        'pnMain.Visible = True
        'pnDialog.Visible = False
        pnMain.Style.Add("display", "")
        pnDialog.Style.Add("display", "none")
    End Sub
    Public Sub OpenDialog()
        'pnMain.Visible = False
        'pnDialog.Visible = True
        pnMain.Style.Add("display", "none")
        pnDialog.Style.Add("display", "")
    End Sub
#End Region

#Region "Session"
    Public Function GetDataCustomerInfo() As List(Of OD50RCVD)
        Try
            Return Session("IDOCS.application.LoaddataCustomerInformation")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataCustomerInfo(ByVal lstMenu As List(Of OD50RCVD)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataCustomerInformation", lstMenu)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function GetDataStatusContCode() As DataSet
        Try
            Return Session("IDOCS.application.LoaddataStatusContCode")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataStatusContCode(ByVal ds As DataSet) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataStatusContCode", ds)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Control Validate"
    Public Sub ControlValidate()
        lblMassage1.Style.Add("display", "none")
        lblMassage2.Style.Add("display", "none")
        lblMassage3.Style.Add("display", "none")
        lblMassage4.Style.Add("display", "none")
        lblMassage5.Style.Add("display", "none")
        lblMassage6.Style.Add("display", "none")
        lblsMessageFPEOPLEID.Style.Add("display", "none")
        lblsMessageTaxNo.Style.Add("display", "none")
    End Sub
#End Region

#Region "UploadFileProcess"
    Public Sub CopyFileTempToProcess()
        Dim strKeyID As String = txtaFCONTCODE.Text & "-0"
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
            imga.ImageUrl = ""
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
        Dim strKeyID As String = txtaFCONTCODE.Text & "-0"
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

End Class
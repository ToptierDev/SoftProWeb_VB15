Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO

Public Class INT_Promotion
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.DataBind()
                hddpLG.Value = Me.WebCulture.ToUpper
                hddParameterMenuID.Value = HelperLog.LoadMenuID("INT_Promotion.aspx")
                Me.ClearSessionPageLoad("INT_Promotion.aspx")

                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()
                Call Loaddata()

                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())
                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "AutocompletedPromotion(this,event);", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();AutocompletedFPD(this,event);", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        If hddpFlagSearch.Value = "1" Then
            Session.Remove("INT_Promotion_PageInfo")
            Session.Remove("INT_Promotion_Search")
            Session.Remove("INT_Promotion_pageLength")
        End If
        Call Redirect("INT_Promotion.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("INT_Promotion_Project")
        Session.Remove("INT_Promotion_Active")
        Session.Remove("INT_Promotion_FS")
        Session.Add("INT_Promotion_Project", IIf(txtsProject.Text <> String.Empty, txtsProject.Text, ""))
        Session.Add("INT_Promotion_Active", IIf(chksActive.Checked, "1", "0"))
        Session.Add("INT_Promotion_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("INT_Promotion_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("INT_Promotion_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("INT_Promotion_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        hddReloadGrid.Value = "1"
        If Session("INT_Promotion_Project") IsNot Nothing And
           Session("INT_Promotion_Active") IsNot Nothing Then
            If Session("INT_Promotion_Project").ToString <> String.Empty Then
                Dim strProject As String = Session("INT_Promotion_Project").ToString
                txtsProject.Text = strProject
            End If
            If Session("INT_Promotion_Active").ToString <> String.Empty Then
                Dim strActive As String = Session("INT_Promotion_Active").ToString
                If strActive = "1" Then
                    chksActive.Checked = True
                Else
                    chksActive.Checked = False
                End If
            End If

            If Session("INT_Promotion_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("INT_Promotion_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
            End If
        End If
        If Session("INT_Promotion_PageInfo") IsNot Nothing Then
            If Session("INT_Promotion_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("INT_Promotion_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("INT_Promotion_Search") IsNot Nothing Then
            If Session("INT_Promotion_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("INT_Promotion_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("INT_Promotion_pageLength") IsNot Nothing Then
            If Session("INT_Promotion_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("INT_Promotion_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If
    End Sub

#End Region

#Region "Dropdownlist"
    Public Sub LoadDropdownlist()
        'Call LoadProject(ddlsProject,
        '                 "S")

        Call LoadProject(ddlaProject,
                         "A")
    End Sub
    Public Sub LoadProject(ByVal ddl As DropDownList,
                           ByVal strType As String)
        Dim bl As cPromotion = New cPromotion
        Try
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select_all", "MSG"), ""))
            ElseIf strType = "S" Then
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select_all", "MSG"), ""))
            End If
            Dim lc = bl.LoadProject()
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
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDepartment", ex)
        End Try
    End Sub
#End Region

#Region "Loaddata"
    Public Sub LoadInit()
        chksActive.Checked = True

        lblMain1.Text = Me.GetResource("main_label", "Text", "1")
        lblMain2.Text = Me.GetResource("lblMain2", "Text", hddParameterMenuID.Value)
        lblMain3.Text = Me.GetResource("lblMain3", "Text", hddParameterMenuID.Value)
        lblMain4.Text = Me.GetResource("lblMain4", "Text", hddParameterMenuID.Value)

        lblsSearch.Text = Me.GetResource("lblsSearch", "Text", hddParameterMenuID.Value)

        lblsProject.Text = Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value)
        lblsActive.Text = Me.GetResource("lblsActive", "Text", hddParameterMenuID.Value)

        TextHd1.Text = Me.GetResource("TextHd1", "Text", hddParameterMenuID.Value)
        TextHd2.Text = Me.GetResource("TextHd2", "Text", hddParameterMenuID.Value)
        TextHd3.Text = Me.GetResource("TextHd3", "Text", hddParameterMenuID.Value)
        TextHd4.Text = Me.GetResource("TextHd4", "Text", hddParameterMenuID.Value)
        TextHd5.Text = Me.GetResource("TextHd5", "Text", hddParameterMenuID.Value)
        TextHd6.Text = Me.GetResource("TextHd6", "Text", hddParameterMenuID.Value)
        'TextHd7.Text = Me.GetResource("TextHd7", "Text", hddParameterMenuID.Value)
        TextHd8.Text = Me.GetResource("col_edit", "Text", "1")
        TextHd9.Text = Me.GetResource("col_delete", "Text", "1")

        TextFt1.Text = Me.GetResource("TextFt1", "Text", hddParameterMenuID.Value)
        TextFt2.Text = Me.GetResource("TextFt2", "Text", hddParameterMenuID.Value)
        TextFt3.Text = Me.GetResource("TextFt3", "Text", hddParameterMenuID.Value)
        TextFt4.Text = Me.GetResource("TextFt4", "Text", hddParameterMenuID.Value)
        TextFt5.Text = Me.GetResource("TextFt5", "Text", hddParameterMenuID.Value)
        TextFt6.Text = Me.GetResource("TextFt6", "Text", hddParameterMenuID.Value)
        'TextFt7.Text = Me.GetResource("TextFt7", "Text", hddParameterMenuID.Value)
        TextFt8.Text = Me.GetResource("col_edit", "Text", "1")
        TextFt9.Text = Me.GetResource("col_delete", "Text", "1")

        hddpActive.Value = GetResource("vActive", "Text", hddParameterMenuID.Value)
        hddpNotActive.Value = GetResource("vInActive", "Text", hddParameterMenuID.Value)
        hddpAll.Value = GetResource("vAll", "Text", hddParameterMenuID.Value)

        lblaPromotionCode.Text = Me.GetResource("lblaPromotionCode", "Text", hddParameterMenuID.Value)
        lblaPromotionAC.Text = Me.GetResource("lblaPromotionAC", "Text", hddParameterMenuID.Value)
        lblaApproveBy.Text = Me.GetResource("lblaApproveBy", "Text", hddParameterMenuID.Value)
        lblaFlagPromotionStatus.Text = Me.GetResource("lblaFlagPromotionStatus", "Text", hddParameterMenuID.Value)
        lblaFlagStandBooking.Text = Me.GetResource("lblaFlagStandBooking", "Text", hddParameterMenuID.Value)
        chkaFlagPromotionStatus.Text = Me.GetResource("chkaFlagPromotionStatus", "Text", hddParameterMenuID.Value)
        chkaFlagStandBooking.Text = Me.GetResource("chkaFlagStandBooking", "Text", hddParameterMenuID.Value)
        rdbFlagUtility.Text = Me.GetResource("rdbFlagUtility", "Text", hddParameterMenuID.Value)
        lblMain5.Text = Me.GetResource("lblMain5", "Text", hddParameterMenuID.Value)
        rdbFlagPR.Text = Me.GetResource("rdbFlagPR", "Text", hddParameterMenuID.Value)
        chkFlagVat.Text = Me.GetResource("chkFlagVat", "Text", hddParameterMenuID.Value)
        lblaPriceNotOver.Text = Me.GetResource("lblaPriceNotOver", "Text", hddParameterMenuID.Value)
        lblaPriceCashDiscount.Text = Me.GetResource("lblaPriceCashDiscount", "Text", hddParameterMenuID.Value)
        lblaProject.Text = Me.GetResource("lblaProject", "Text", hddParameterMenuID.Value)
        lblaFPDCode.Text = Me.GetResource("lblaFPDCode", "Text", hddParameterMenuID.Value)
        lblaDecsription.Text = Me.GetResource("lblaDecsription", "Text", hddParameterMenuID.Value)
        lblaTotalPrice.Text = Me.GetResource("lblaTotalPrice", "Text", hddParameterMenuID.Value)
        lblaVat.Text = Me.GetResource("lblaVat", "Text", hddParameterMenuID.Value)
        lblaTotalAll.Text = Me.GetResource("lblaTotalAll", "Text", hddParameterMenuID.Value)

        lblMassage1.Text = Me.GetResource("lblMassage1", "Text", hddParameterMenuID.Value)
        lblMassage2.Text = Me.GetResource("msg_pricenotover", "MSG")

        'lblsSave.Text = Me.GetResource("btnSaveTemp", "Text", hddParameterMenuID.Value)
        'lblsCancel.Text = Me.GetResource("btnCencel", "Text", hddParameterMenuID.Value)

        lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG")
        lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG") & " " & Me.GetResource("lblaPromotionCode", "Text", hddParameterMenuID.Value)

        hddMSGSaveData.Value = Me.GetResource("msg_save_data", "MSG")
        hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG")
        hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG")
        hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG")
        hddMSGAddData.Value = Me.GetResource("msg_add_data", "MSG")
        'hddpCheckinTable.Value = Me.GetResource("msg_duplicate_table", "MSG")

        btnMSGAddData.Title = hddMSGAddData.Value
        btnMSGSaveData.Title = hddMSGSaveData.Value
        btnMSGCancelData.Title = hddMSGCancelData.Value
        btnMSGDeleteData.InnerText = hddMSGDeleteData.Value
        btnMSGCancelDataS.InnerText = hddMSGCancelData.Value

        lblLGNameEN.Text = Me.GetResource("TextHd3", "Text", hddParameterMenuID.Value) & " English"
        lblLGNameTH.Text = Me.GetResource("TextHd3", "Text", hddParameterMenuID.Value) & " ภาษาไทย"

        lblMassage6.Text = grtt("resPleaseEnter")

        hddDeleteBeforeCheck.Value = grtt("resDeleteBeforeCheck")

        Call ControlValidate()
    End Sub
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
    Public Sub Loaddata()
        Try
            Dim bl As cPromotion = New cPromotion

            'Dim strProject As String = String.Empty
            'Call SetCiterail(txtsProject.Text, strProject)

            Dim lcPromotion As List(Of Promotion_ViewModel) = bl.Loaddata(txtsProject.Text,
                                                                          chksActive.Checked,
                                                                          Me.WebCulture.ToString.ToUpper)

                If lcPromotion IsNot Nothing Then
                    Call SetDataPromotion(lcPromotion)
                Else
                    Call SetDataPromotion(Nothing)
                End If

                Session.Remove("INT_Promotion_Project")
            Session.Remove("INT_Promotion_Active")
            Session.Remove("INT_Promotion_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)

            Session.Remove("INT_Promotion_Project")
            Session.Remove("INT_Promotion_Active")
            Session.Remove("INT_Promotion_FS")
        End Try
    End Sub
    Public Sub ClearText()
        txtaPromotionCode.Text = String.Empty
        txtaPromotionName.Text = String.Empty
        txtaPromotionAC.Text = "1134001"
        txtaApproveBy.Text = String.Empty
        chkaFlagPromotionStatus.Checked = False
        chkaFlagStandBooking.Checked = False
        rdbFlagUtility.Checked = False
        rdbFlagPR.Checked = False
        chkFlagVat.Checked = False
        txtaPriceNotOver.Text = String.Empty
        txtaPriceCashDiscount.Text = String.Empty
        ddlaProject.SelectedIndex = 0
        txtaFPDCode.Text = String.Empty
        txtaFPDName.Text = String.Empty
        txtaDecsription.Text = String.Empty
        txtaTotalPrice.Text = String.Empty
        txtaVat.Text = String.Empty
        txtaTotalAll.Text = String.Empty
        hddaProject.Value = String.Empty

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
            hddpFlagSearch.Value = "1"
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSearch_Click", ex)
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Call ClearText()
            Call CallLoadGridView()
            txtaPromotionCode.Enabled = True
            btnMSGApprove.Style.Add("display", "none")
            btnMSGUnApprove.Style.Add("display", "none")
            'txtaFPDCode.Attributes.Remove("disabled")
            'txtaFPDCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe0c0")

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try
            Call ClearText()
            Dim bl As cPromotion = New cPromotion
            Dim lc As Promotion_ViewModel = bl.LoadEdit(hddKeyID.Value,
                                                        Me.WebCulture.ToString.ToUpper)
            If lc IsNot Nothing Then
                If Not IsDBNull(lc.FPDCODE) Then
                    If lc.FPDCODE <> String.Empty Then
                        txtaPromotionCode.Text = lc.FPDCODE
                    End If
                End If
                txtaPromotionCode.Enabled = False


                If Not IsDBNull(lc.FPDNAMET) Then
                    If lc.FPDNAMET <> String.Empty Then
                        txtaPromotionNameT.Text = lc.FPDNAMET
                    End If
                End If
                If Not IsDBNull(lc.FPDNAME) Then
                    If lc.FPDNAME <> String.Empty Then
                        txtaPromotionName.Text = lc.FPDNAME
                    End If
                End If

                'FACCD Fix 1134001 Not Edit
                ''    If Not IsDBNull(lc.PromoAC) Then
                ''        If lc.PromoAC <> String.Empty Then
                ''            txtaPromotionAC.Text = lc.PromoAC
                ''        End If
                ''    End If
                'Approve Not Save in Page

                If Not IsDBNull(lc.FUPDBY) Then
                    If lc.FUPDBY <> String.Empty Then
                        txtaApproveBy.Text = lc.FUPDBY
                    End If
                End If

                Dim lcs As BD01STB1 = bl.CheckApprove(txtaPromotionCode.Text,
                                                              Me.CurrentUser.UserID)
                If lcs IsNot Nothing Then
                    If Not IsDBNull(lc.FUPDFLAG) Then
                        If lc.FUPDFLAG = "Y" Then
                            btnMSGApprove.Style.Add("display", "none")
                            btnMSGUnApprove.Style.Add("display", "")
                        Else
                            btnMSGApprove.Style.Add("display", "")
                            btnMSGUnApprove.Style.Add("display", "none")
                            ''ถ้าถูกนำไปใช้แล้วจะไม่สามารถเห็นไม่อนุมัติเลย
                        End If
                        ''ต้องเช็คด้วยว่ามีสิทธิ์เห็นปุ่มอนุมัติไหม
                    Else
                        btnMSGApprove.Style.Add("display", "")
                        btnMSGUnApprove.Style.Add("display", "none")
                        ''เพิ่มสิทธิ์การอนุมัติตรงนี้หละ
                        ''ถ้ามีสิทธิ์ก้อให้โชว์ปุ่มอนุมัติ
                    End If
                Else
                    btnMSGApprove.Style.Add("display", "none")
                    btnMSGUnApprove.Style.Add("display", "none")
                End If



                If Not IsDBNull(lc.FNOTUSE) Then
                    If lc.FNOTUSE = "N" Then
                        chkaFlagPromotionStatus.Checked = False
                    ElseIf lc.FNOTUSE = "Y" Then
                        chkaFlagPromotionStatus.Checked = True
                    End If
                End If

                'ยังไม่รู้
                '    If Not IsDBNull(lc.FlagStandardBooking) Then
                '        chkaFlagStandBooking.Checked = lc.FlagStandardBooking
                '    End If

                If Not IsDBNull(lc.FOPSEQ) Then
                    If lc.FOPSEQ = "00101" Then
                        rdbFlagUtility.Checked = False
                    ElseIf lc.FOPSEQ = "00102" Then
                        rdbFlagUtility.Checked = True
                    End If
                End If

                If Not IsDBNull(lc.FBATCHSZU) Then
                    If lc.FBATCHSZU = 1 Then
                        rdbFlagPR.Checked = True
                    ElseIf lc.FBATCHSZU = 0 Then
                        rdbFlagPR.Checked = False
                    End If
                End If
                If Not IsDBNull(lc.FPCOMPLETE) Then
                    If lc.FPCOMPLETE = 1 Then
                        chkFlagVat.Checked = True
                    ElseIf lc.FPCOMPLETE = 0 Then
                        chkFlagVat.Checked = False
                    End If
                End If
                If Not IsDBNull(lc.FSTDCOST) Then
                    If lc.FSTDCOST IsNot Nothing Then
                        txtaPriceNotOver.Text = String.Format("{0:N2}", CDec(lc.FSTDCOST))
                    End If
                End If
                If Not IsDBNull(lc.FSTDPRICE) Then
                    If lc.FSTDPRICE IsNot Nothing Then
                        txtaPriceCashDiscount.Text = String.Format("{0:N2}", CDec(lc.FSTDPRICE))
                    End If
                End If
                If Not IsDBNull(lc.FREPRJNO) Then
                    If lc.FREPRJNO <> String.Empty Then
                        Try
                            ddlaProject.SelectedValue = lc.FREPRJNO
                            hddaProject.Value = lc.FREPRJNO
                        Catch ex As Exception
                            ddlaProject.SelectedIndex = 0
                            hddaProject.Value = String.Empty
                        End Try
                        'Try
                        '    Dim lstProject As ED01PROJ = bl.LoadProjectEdit(lc.FREPRJNO)
                        '    If lstProject IsNot Nothing Then
                        '        If lstProject.FREPRJNO <> String.Empty Then
                        '            txtaProject.Text = lstProject.FREPRJNO & "-" & lstProject.FREPRJNM
                        '            hddaProject.Value = lstProject.FREPRJNO
                        '        End If
                        '    End If
                        'Catch ex As Exception
                        '    txtaProject.Text = String.Empty
                        '    hddaProject.Value = String.Empty
                        'End Try
                    End If
                End If
                If Not IsDBNull(lc.FPDCODETYPE) Then
                    If lc.FPDCODETYPE <> String.Empty Then
                        txtaFPDCode.Text = lc.FPDCODETYPE
                        If Me.WebCulture.ToUpper = "TH" Then
                            txtaFPDName.Text = IIf(lc.FPDNAMETYPET <> String.Empty, lc.FPDNAMETYPET, String.Empty)
                        Else
                            txtaFPDName.Text = IIf(lc.FPDNAMETYPE <> String.Empty, lc.FPDNAMETYPE, String.Empty)
                        End If
                    End If
                End If
                If Not IsDBNull(lc.FOPDESC) Then
                    If lc.FOPDESC <> String.Empty Then
                        txtaDecsription.Text = lc.FOPDESC
                    End If
                End If
                If lc.FPDCODE IsNot Nothing Then
                    If lc.FPDCODE <> String.Empty Then
                        Dim lsts As List(Of Promotion_ViewModel) = bl.LoadPromotionDetail(lc.FPDCODE,
                                                                                          rdbFlagUtility.Checked)
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
                End If

                Call LoadPdf()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "CalTotalAll();", True)
            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim bl As cPromotion = New cPromotion
            Dim lc As Promotion_ViewModel = bl.LoadEdit(hddKeyID.Value,
                                                    Me.WebCulture.ToString.ToUpper)
            If lc IsNot Nothing Then
                If Not bl.Delete(hddKeyID.Value) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_cannot_delete_because ", "MSG") & "');", True)
                Else
                    Call LoadRedirec("2")
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnDelete_Click", ex)
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            If Not SaveDraft() Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
            Else
                Call CopyFileTempToProcess()
                Call LoadRedirec("1")
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSave_Click", ex)
        End Try
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnCancel_Click", ex)
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
    Protected Sub btnReloadFPD_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReloadFPD.Click
        Try
            'txtaFPDCode.Attributes.Remove("enabled")
            'txtaFPDCode.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")

            hddaFPDCode.Value = String.Empty
            If hddaFPDName.Value <> String.Empty Then
                txtaFPDCode.Text = hddaFPDName.Value.Split(":")(0)
                txtaFPDName.Text = hddaFPDName.Value.Split(":")(1)
            Else
                If txtaPromotionCode.Text <> String.Empty Then
                    hddaFPDCode.Value = txtaFPDCode.Text
                    hddaFPDName.Value = txtaFPDCode.Text & ":" & txtaFPDName.Text

                    txtaFPDCode.Text = hddaFPDName.Value.Split(":")(0)
                    txtaFPDName.Text = hddaFPDName.Value.Split(":")(1)
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "CalTotalAll();", True)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReloadFPD_Click", ex)
        End Try
    End Sub
    Protected Sub btnApprove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApprove.Click
        Try
            Dim bl As cPromotion = New cPromotion
            If Not SaveDraft() Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
            Else
                If Not bl.Approve(txtaPromotionCode.Text,
                                  Me.CurrentUser.UserID) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                Else
                    Call CopyFileTempToProcess()
                    Call LoadRedirec("1")
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnApprove_Click", ex)
        End Try
    End Sub
    Protected Sub btnUnApprove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUnApprove.Click
        Try
            Dim bl As cPromotion = New cPromotion
            If Not SaveDraft() Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
            Else
                If Not bl.UnApprove(txtaPromotionCode.Text,
                                    Me.CurrentUser.UserID) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                Else
                    Call CopyFileTempToProcess()
                    Call LoadRedirec("1")
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnUnApprove_Click", ex)
        End Try
    End Sub
#End Region

#Region "Function"
    Function SaveDraft() As Boolean
        Try
            Dim strProject As String = String.Empty
            'Call SetCiterail(txtaProject.Text, strProject)
            strProject = ddlaProject.SelectedValue
            Dim dt As New DataTable
            Dim bl As cPromotion = New cPromotion
            Call CreateDatatable(dt)
            Call GetDatatable(dt)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Dim lstr As List(Of String) = dt.AsEnumerable().Select(Function(s) s.Field(Of String)("ProductCode")).Where(Function(s) s <> "").ToList()
                    If Not bl.chkProduct(lstr,
                                     rdbFlagUtility.Checked) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_producthavenomaster", "MSG") & "');", True)
                        Exit Function
                    End If
                End If
            End If
                If hddKeyID.Value = String.Empty Then
                Dim lc As Promotion_ViewModel = bl.LoadEdit(txtaPromotionCode.Text,
                                                            Me.WebCulture.ToString.ToUpper)
                If lc Is Nothing Then
                    If Not bl.Add(txtaPromotionCode.Text,
                                  txtaPromotionName.Text,
                                  txtaPromotionAC.Text,
                                  txtaApproveBy.Text,
                                  chkaFlagPromotionStatus.Checked,
                                  chkaFlagStandBooking.Checked,
                                  rdbFlagUtility.Checked,
                                  rdbFlagPR.Checked,
                                  chkFlagVat.Checked,
                                  txtaPriceNotOver.Text,
                                  txtaPriceCashDiscount.Text,
                                  strProject,
                                  txtaFPDCode.Text,
                                  txtaFPDName.Text,
                                  txtaDecsription.Text,
                                  dt,
                                  Me.CurrentUser.UserID,
                                  hddpLG.Value,
                                  txtaPromotionNameT.Text) Then
                        Return False
                    Else
                        Call CopyFileTempToProcess()
                        Return True
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_duplicate", "MSG") & "');", True)
                End If
            Else

                If Not bl.Edit(hddKeyID.Value,
                               txtaPromotionName.Text,
                               txtaPromotionAC.Text,
                               txtaApproveBy.Text,
                               chkaFlagPromotionStatus.Checked,
                               chkaFlagStandBooking.Checked,
                               rdbFlagUtility.Checked,
                               rdbFlagPR.Checked,
                               chkFlagVat.Checked,
                               txtaPriceNotOver.Text,
                               txtaPriceCashDiscount.Text,
                               strProject,
                               txtaFPDCode.Text,
                               txtaFPDName.Text,
                               txtaDecsription.Text,
                               dt,
                               Me.CurrentUser.UserID,
                               hddpLG.Value,
                               txtaPromotionNameT.Text) Then
                    Return False
                Else
                    Call CopyFileTempToProcess()
                    Return True
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "SaveDraft", ex)
            Return False
        End Try
    End Function
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

#Region "Session"
    Public Function GetDataPromotion() As List(Of Promotion_ViewModel)
        Try
            Return Session("IDOCS.application.LoaddataPromotion")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataPromotion(ByVal lcPromotion As List(Of Promotion_ViewModel)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataPromotion", lcPromotion)
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
        lblMassage6.Style.Add("display", "none")
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
        dt.Columns.Add("ProductCode")
        dt.Columns.Add("ProductName")
        dt.Columns.Add("UOM")
        dt.Columns.Add("QTY")
        dt.Columns.Add("UnitPrice")
        dt.Columns.Add("TotalPrice")
        dt.Columns.Add("FlagSetAdd")
    End Sub
    Public Sub AddDataTable(ByRef dt As DataTable)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            If dt IsNot Nothing Then
                For Each e As GridViewRow In grdView2.Rows
                    Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                    Dim txtgProductCode As TextBox = CType(e.FindControl("txtgProductCode"), TextBox)
                    Dim txtgProductName As TextBox = CType(e.FindControl("txtgProductName"), TextBox)
                    Dim txtgUOM As TextBox = CType(e.FindControl("txtgUOM"), TextBox)
                    Dim txtgQTY As TextBox = CType(e.FindControl("txtgQTY"), TextBox)
                    Dim txtgUnitPrice As TextBox = CType(e.FindControl("txtgUnitPrice"), TextBox)
                    Dim txtgTotalPrice As TextBox = CType(e.FindControl("txtgTotalPrice"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)

                    dr = dt.NewRow
                    strID = CInt(hddID.Value)
                    dr.Item("ID") = hddID.Value
                    dr.Item("ProductCode") = txtgProductCode.Text
                    dr.Item("ProductName") = txtgProductName.Text
                    dr.Item("UOM") = txtgUOM.Text
                    If txtgQTY.Text <> String.Empty Then
                        dr.Item("QTY") = String.Format("{0:N0}", CDec(txtgQTY.Text))
                    End If
                    If txtgUnitPrice.Text <> String.Empty Then
                        dr.Item("UnitPrice") = String.Format("{0:N2}", CDec(txtgUnitPrice.Text))
                    End If
                    If txtgTotalPrice.Text <> String.Empty Then
                        dr.Item("TotalPrice") = String.Format("{0:N2}", CDec(txtgTotalPrice.Text))
                    End If
                    hddFlagSetAdd.Value = "0"
                    dr.Item("FlagSetAdd") = "0"
                    dt.Rows.Add(dr)
                    'If txtgProductCode.Text = String.Empty And
                    '   txtgProductName.Text = String.Empty And
                    '   txtgUOM.Text = String.Empty And
                    '   txtgQTY.Text = String.Empty And
                    '   txtgUnitPrice.Text = String.Empty And
                    '   txtgTotalPrice.Text = String.Empty Then
                    '    dt.Rows.Remove(dr)
                    'End If
                    If hddFlagSetAdd.Value = "1" Then
                        dt.Rows.Remove(dr)
                    End If
                Next
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("ProductCode") = String.Empty
                dr.Item("ProductName") = String.Empty
                dr.Item("UOM") = String.Empty
                dr.Item("QTY") = String.Empty
                dr.Item("UnitPrice") = String.Empty
                dr.Item("TotalPrice") = String.Empty
                dr.Item("FlagSetAdd") = "1"
                dt.Rows.Add(dr)
            Else
                dr = dt.NewRow
                dr.Item("ID") = strID + 1
                dr.Item("ProductCode") = String.Empty
                dr.Item("ProductName") = String.Empty
                dr.Item("UOM") = String.Empty
                dr.Item("QTY") = String.Empty
                dr.Item("UnitPrice") = String.Empty
                dr.Item("TotalPrice") = String.Empty
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
                Dim txtgProductCode As TextBox = CType(e.FindControl("txtgProductCode"), TextBox)
                Dim txtgProductName As TextBox = CType(e.FindControl("txtgProductName"), TextBox)
                Dim txtgUOM As TextBox = CType(e.FindControl("txtgUOM"), TextBox)
                Dim txtgQTY As TextBox = CType(e.FindControl("txtgQTY"), TextBox)
                Dim txtgUnitPrice As TextBox = CType(e.FindControl("txtgUnitPrice"), TextBox)
                Dim txtgTotalPrice As TextBox = CType(e.FindControl("txtgTotalPrice"), TextBox)
                Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                dr = dt.NewRow
                dr.Item("ID") = hddID.Value
                dr.Item("ProductCode") = txtgProductCode.Text
                dr.Item("ProductName") = txtgProductName.Text
                dr.Item("UOM") = txtgUOM.Text
                dr.Item("QTY") = txtgQTY.Text
                dr.Item("UnitPrice") = txtgUnitPrice.Text
                dr.Item("TotalPrice") = txtgTotalPrice.Text
                dr.Item("FlagSetAdd") = hddFlagSetAdd.Value
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
    Public Sub SetDatatable(ByRef dt As DataTable,
                            ByVal lst As List(Of Promotion_ViewModel))
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            For Each m As Promotion_ViewModel In lst
                dr = dt.NewRow
                strID = strID + 1
                dr.Item("ID") = strID
                dr.Item("ProductCode") = m.FCOMPCODE
                dr.Item("ProductName") = IIf(Me.WebCulture = "TH", m.FCOMPNAMET, m.FCOMPNAME)
                dr.Item("UOM") = IIf(Me.WebCulture = "TH", m.FUNITMT2, m.FUNITMT)
                dr.Item("QTY") = m.FQTY
                dr.Item("UnitPrice") = m.FUCOST
                Try
                    dr.Item("TotalPrice") = m.FQTY * m.FUCOST
                Catch ex As Exception
                    dr.Item("TotalPrice") = String.Empty
                End Try
                dr.Item("FlagSetAdd") = "0"
                dt.Rows.Add(dr)
            Next
            dr = dt.NewRow
            dr.Item("ID") = strID + 1
            dr.Item("ProductCode") = String.Empty
            dr.Item("ProductName") = String.Empty
            dr.Item("UOM") = String.Empty
            dr.Item("QTY") = String.Empty
            dr.Item("UnitPrice") = String.Empty
            dr.Item("TotalPrice") = String.Empty
            dr.Item("FlagSetAdd") = "1"
            dt.Rows.Add(dr)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
#End Region

#Region "GridView2"
    Protected Sub grdView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView2.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    Dim btnDelete As ImageButton = e.Row.FindControl("btnDelete")
                    btnDelete.CommandArgument = DataBinder.Eval(e.Row.DataItem, "ID")
                    Dim hddID As HiddenField = CType(e.Row.FindControl("hddID"), HiddenField)
                    Dim txtgProductCode As TextBox = CType(e.Row.FindControl("txtgProductCode"), TextBox)
                    Dim txtgProductName As TextBox = CType(e.Row.FindControl("txtgProductName"), TextBox)
                    Dim txtgUOM As TextBox = CType(e.Row.FindControl("txtgUOM"), TextBox)
                    Dim txtgQTY As TextBox = CType(e.Row.FindControl("txtgQTY"), TextBox)
                    Dim txtgUnitPrice As TextBox = CType(e.Row.FindControl("txtgUnitPrice"), TextBox)
                    Dim txtgTotalPrice As TextBox = CType(e.Row.FindControl("txtgTotalPrice"), TextBox)
                    Dim hddFlagSetAdd As HiddenField = CType(e.Row.FindControl("hddFlagSetAdd"), HiddenField)
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "ID")) Then
                        If DataBinder.Eval(e.Row.DataItem, "ID").ToString <> String.Empty Then
                            hddID.Value = DataBinder.Eval(e.Row.DataItem, "ID")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "ProductCode")) Then
                        If DataBinder.Eval(e.Row.DataItem, "ProductCode").ToString <> String.Empty Then
                            txtgProductCode.Text = DataBinder.Eval(e.Row.DataItem, "ProductCode")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "ProductName")) Then
                        If DataBinder.Eval(e.Row.DataItem, "ProductName").ToString <> String.Empty Then
                            txtgProductName.Text = DataBinder.Eval(e.Row.DataItem, "ProductName")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "UOM")) Then
                        If DataBinder.Eval(e.Row.DataItem, "UOM").ToString <> String.Empty Then
                            txtgUOM.Text = DataBinder.Eval(e.Row.DataItem, "UOM")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "QTY")) Then
                        If DataBinder.Eval(e.Row.DataItem, "QTY").ToString <> String.Empty Then
                            txtgQTY.Text = String.Format("{0:N0}", CInt(DataBinder.Eval(e.Row.DataItem, "QTY")))
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "UnitPrice")) Then
                        If DataBinder.Eval(e.Row.DataItem, "UnitPrice").ToString <> String.Empty Then
                            txtgUnitPrice.Text = String.Format("{0:N2}", CDec(DataBinder.Eval(e.Row.DataItem, "UnitPrice")))
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "TotalPrice")) Then
                        If DataBinder.Eval(e.Row.DataItem, "TotalPrice").ToString <> String.Empty Then
                            txtgTotalPrice.Text = String.Format("{0:N2}", CDec(DataBinder.Eval(e.Row.DataItem, "TotalPrice")))
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FlagSetAdd").ToString <> String.Empty Then
                            hddFlagSetAdd.Value = DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")
                        End If
                    End If
                    If hddFlagSetAdd.Value = "1" Then
                        txtgProductCode.Attributes.Add("onclick", "addRowGridView('" & txtgProductCode.ClientID & "');")
                        txtgProductCode.Attributes.Remove("onkeypress")
                        txtgQTY.Attributes.Remove("onblur")
                        txtgUnitPrice.Attributes.Remove("onblur")
                        txtgProductCode.BackColor = Drawing.Color.White
                        txtgProductName.Attributes.Add("onclick", "addRowGridView('" & txtgProductName.ClientID & "');")
                        txtgUOM.Attributes.Add("onclick", "addRowGridView('" & txtgUOM.ClientID & "');")
                        txtgQTY.Attributes.Add("onclick", "addRowGridView('" & txtgQTY.ClientID & "');")
                        txtgUnitPrice.Attributes.Add("onclick", "addRowGridView('" & txtgUnitPrice.ClientID & "');")
                        txtgTotalPrice.Attributes.Add("onclick", "addRowGridView('" & txtgTotalPrice.ClientID & "');")
                        btnDelete.Visible = False
                    ElseIf hddFlagSetAdd.Value = "0" Then
                        txtgProductCode.Attributes.Remove("onclick")
                        txtgProductCode.Attributes.Add("onkeypress", "return AutocompletedProduct('" & txtgProductCode.ClientID & "'," &
                                                                                                  "'" & txtgProductName.ClientID & "'," &
                                                                                                  "'" & txtgUOM.ClientID & "'," &
                                                                                                  "'" & hddID.Value & "'," &
                                                                                                  "event);")
                        txtgQTY.Attributes.Add("onblur", "return CalTotalInGridAll('" & txtgQTY.ClientID & "'," &
                                                                                   "'" & txtgUnitPrice.ClientID & "'," &
                                                                                   "'" & txtgTotalPrice.ClientID & "');")
                        txtgUnitPrice.Attributes.Add("onblur", "return CalTotalInGridAll('" & txtgQTY.ClientID & "'," &
                                                                                         "'" & txtgUnitPrice.ClientID & "'," &
                                                                                         "'" & txtgTotalPrice.ClientID & "');")
                        txtgProductName.Attributes.Remove("onclick")
                        txtgUOM.Attributes.Remove("onclick")
                        txtgQTY.Attributes.Remove("onclick")
                        txtgUnitPrice.Attributes.Remove("onclick")
                        txtgTotalPrice.Attributes.Remove("onclick")
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

#Region "LoadRepeater"
    Private Sub LoadPdf()
        Me.rptPdf.DataSource = LoadrptPdfDataSource()
        Me.rptPdf.DataBind()
    End Sub

    Private Function LoadrptPdfDataSource() As List(Of String)
        'oadcub
        Try
            Dim lstImage = New List(Of String)
            Dim files() As String = IO.Directory.GetFiles(Server.MapPath("UPLOAD_PROMOTION\" + hddParameterMenuID.Value + "\" + hddKeyID.Value + "\"))

            For Each file As String In files
                ' Do work, example
                lstImage.Add("UPLOAD_PROMOTION\" + hddParameterMenuID.Value + "\" + hddKeyID.Value + "\" & Path.GetFileName(file))
            Next

            Return lstImage
        Catch ex As Exception

        End Try

    End Function

    Private Sub rptPdf_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptPdf.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim hplPdf As HyperLink = e.Item.FindControl("hplPdf")
            Dim fullPath = e.Item.DataItem.ToString
            hplPdf.NavigateUrl = fullPath
            Dim fileName = Path.GetFileName(fullPath)
            hplPdf.Text = String.Join("_", fileName.Split("_").Skip(2))

        End If
    End Sub
#End Region

#Region "UploadFile"
    Public Sub CopyFileTempToProcess()
        Dim strKeyID As String = IIf(hddKeyID.Value <> String.Empty, hddKeyID.Value, txtaPromotionCode.Text)
        ' Call DeleteAllFile(strKeyID)
        Call DeleteSelectedFiles()
        Call CrateFolder(strKeyID)
        Call SaveFile(strKeyID)
    End Sub
    Public Sub CrateFolder(ByVal strKeyID As String)
        Dim strPath As String = Server.MapPath("UPLOAD_PROMOTION\" & hddParameterMenuID.Value & "\")
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
            objFolder = objFSO.GetFolder(Server.MapPath("UPLOAD_PROMOTION\" & hddParameterMenuID.Value & "\"))
            For Each objFile In objFolder.Files
                Dim DatLenght As Integer = objFile.Name.ToString.Split("-").Length
                Dim strCheckFileName As String = objFile.Name.ToString.Split(".")(0).Replace("PD", "").Split("-")(DatLenght - 1)
                Dim strCheckName As String = objFile.Name.ToString.Split(".")(0).Replace("PD", "").Replace("-" & strCheckFileName, "")
                Dim TempDelete As String = Server.MapPath("UPLOAD_PROMOTION\" & hddParameterMenuID.Value & "\" & objFile.Name)
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
        For Each item In rptPdf.Items
            If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                Dim chkDeletePdf As HtmlInputCheckBox = item.FindControl("chkDeletePdf")
                If chkDeletePdf.Checked Then
                    Dim hplPdf As HyperLink = item.FindControl("hplPdf")
                    Dim path = Server.MapPath(hplPdf.NavigateUrl)
                    If System.IO.File.Exists(path) Then
                        System.IO.File.Delete(path)
                    End If
                    'Dim img As Image = item.FindControl("pdfEdit")
                    'Dim path = Server.MapPath(img.ImageUrl)
                    'If System.IO.File.Exists(path) Then
                    '    System.IO.File.Delete(path)
                    'End If
                End If

            End If
        Next
    End Sub
    Public Sub SaveFile(ByVal strKeyID As String)
        Dim TempSave As String = Server.MapPath("UPLOAD_PROMOTION/" & hddParameterMenuID.Value & "/" & strKeyID & "/")
        If (Not System.IO.Directory.Exists(TempSave)) Then
            System.IO.Directory.CreateDirectory(TempSave)
        End If
        Try
            'check filename
            Dim fileCollection = Request.Files
            Dim strFileName As String = DateTime.Now.ToString("yyMMddhhmmss")
            Dim iNo As Integer = 0
            For i As Integer = 0 To fileCollection.Count - 1
                Dim uploadfile = fileCollection(i)
                If (uploadfile.ContentLength > 0) Then
                    Dim fileName = strFileName & "_" & iNo & "_" & Path.GetFileName(uploadfile.FileName)
                    uploadfile.SaveAs(TempSave & fileName)
                    iNo = iNo + 1
                End If
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "SaveFile", ex.GetBaseException)
        End Try
    End Sub
#End Region
    ' oadcub#### Sample Multi file upload with preview

    Protected Sub btnTestUpload_Click(sender As Object, e As EventArgs)
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
End Class
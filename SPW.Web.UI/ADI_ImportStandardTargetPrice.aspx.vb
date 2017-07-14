Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Drawing

Imports NPOI.HSSF
Imports NPOI.HSSF.UserModel
Imports NPOI.XSSF
Imports NPOI.XSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.SS.UserModel.MissingCellPolicy
Imports NPOI.XWPF
Imports NPOI.XWPF.UserModel
Imports System.Runtime.InteropServices
Imports System.Drawing.Graphics
Imports System.Configuration

Public Class ADI_ImportStandardTargetPrice
    Inherits BasePage

    Public clsImportSTPrice As ClsADI_ImportStandardTargetPrice = New ClsADI_ImportStandardTargetPrice
    'Private strPathServer As String = System.Configuration.ConfigurationManager.AppSettings("strPathServer").ToString
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                Page.DataBind()
                hddpLG.Value = Me.WebCulture.ToUpper()
                hddParameterMenuID.Value = HelperLog.LoadMenuID("ADI_ImportStandardTargetPrice.aspx")
                Me.ClearSessionPageLoad("ADI_ImportStandardTargetPrice.aspx")

                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()

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
            Call LoadProject(ddlsProject2, "S")
            Call LoadPhase(ddlsPhase, "S", ddlsProject.SelectedValue)
            Call LoadPhase(ddlsPhase2, "S", ddlsProject2.SelectedValue)
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
    Public Sub LoadPhase(ByVal ddl As DropDownList,
                         ByVal strType As String,
                         ByVal pProject As String)
        Dim bl As cImportStandardTargetPrice = New cImportStandardTargetPrice
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
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadPhase", ex.GetBaseException)
        End Try
    End Sub
#End Region

#Region "Event DropDownlist"
    Protected Sub ddlsProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsProject.SelectedIndexChanged
        Call LoadPhase(ddlsPhase, "S", ddlsProject.SelectedValue)
    End Sub

    Protected Sub ddlsProject2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsProject2.SelectedIndexChanged
        Call LoadPhase(ddlsPhase2, "S", ddlsProject2.SelectedValue)
    End Sub

#End Region

#Region "ControlTab"
    Public Enum TabNameEnum
        IM1 = 0
        IM2 = 1
    End Enum
    Private Sub RegisterSetTabScript(ByVal tab As TabNameEnum)
        Dim index As Integer = tab
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "settab", String.Format("SetActiveTab({0});", index), True)
    End Sub

#End Region

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        If hddpFlagSearch.Value = "1" Then
            Session.Remove("ADI_ImportStandardTargetPrice_PageInfo")
            Session.Remove("ADI_ImportStandardTargetPrice_Search")
            Session.Remove("ADI_ImportStandardTargetPrice_pageLength")
            Session.Remove("ADI_ImportStandardTargetPrice_keyword")
        End If
        Call Redirect("ADI_ImportStandardTargetPrice.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("ADI_ImportStandardTargetPrice_FS")
        Session.Add("ADI_ImportStandardTargetPrice_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("ADI_ImportStandardTargetPrice_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("ADI_ImportStandardTargetPrice_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("ADI_ImportStandardTargetPrice_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
        Session.Add("ADI_ImportStandardTargetPrice_ddlsProject", IIf(ddlsProject.SelectedIndex <> 0, ddlsProject.SelectedValue, ""))
        Session.Add("ADI_ImportStandardTargetPrice_ddlsPhase", IIf(ddlsPhase.SelectedIndex <> 0, ddlsPhase.SelectedValue, ""))
        Session.Add("ADI_ImportStandardTargetPrice_ddlsProject2", IIf(ddlsProject2.SelectedIndex <> 0, ddlsProject2.SelectedValue, ""))
        Session.Add("ADI_ImportStandardTargetPrice_ddlsPhase2", IIf(ddlsPhase2.SelectedIndex <> 0, ddlsPhase2.SelectedValue, ""))

    End Sub
    Public Sub GetParameter()
        If Session("ADI_ImportStandardTargetPrice_FS") IsNot Nothing Then
            If Session("ADI_ImportStandardTargetPrice_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("ADI_ImportStandardTargetPrice_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
                Dim strPageInfo As String = Session("ADI_ImportStandardTargetPrice_PageInfo").ToString
                If strPageInfo <> String.Empty Then
                    hddpPageInfo.Value = strPageInfo
                End If

            End If
        End If


        If Session("ADI_ImportStandardTargetPrice_PageInfo") IsNot Nothing Then
            If Session("ADI_ImportStandardTargetPrice_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("ADI_ImportStandardTargetPrice_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("ADI_ImportStandardTargetPrice_Search") IsNot Nothing Then
            If Session("ADI_ImportStandardTargetPrice_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("ADI_ImportStandardTargetPrice_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("ADI_ImportStandardTargetPrice_pageLength") IsNot Nothing Then
            If Session("ADI_ImportStandardTargetPrice_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("ADI_ImportStandardTargetPrice_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If

        If Session("ADI_ImportStandardTargetPrice_ddlsProject") IsNot Nothing Then
            If Session("ADI_ImportStandardTargetPrice_ddlsProject").ToString <> String.Empty Then
                Dim strsProjectDefault As String = Session("ADI_ImportStandardTargetPrice_ddlsProject").ToString
                Try
                    ddlsProject.SelectedValue = strsProjectDefault
                Catch ex As Exception
                    ddlsProject.SelectedIndex = 0
                End Try
            End If
        End If
        If ddlsProject.SelectedIndex <> 0 Then
            Call LoadPhase(ddlsPhase, "S", ddlsProject.SelectedValue)
        End If
        If Session("ADI_ImportStandardTargetPrice_ddlsPhase") IsNot Nothing Then
            If Session("ADI_ImportStandardTargetPrice_ddlsPhase").ToString <> String.Empty Then
                Dim strsPhaseDefault As String = Session("ADI_ImportStandardTargetPrice_ddlsPhase").ToString
                If strsPhaseDefault <> String.Empty Then
                    Try
                        ddlsPhase.SelectedValue = strsPhaseDefault
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If

        If Session("ADI_ImportStandardTargetPrice_ddlsProject2") IsNot Nothing Then
            If Session("ADI_ImportStandardTargetPrice_ddlsProject2").ToString <> String.Empty Then
                Dim strsProjectDefault2 As String = Session("ADI_ImportStandardTargetPrice_ddlsProject2").ToString
                Try
                    ddlsProject2.SelectedValue = strsProjectDefault2
                Catch ex As Exception
                    ddlsProject2.SelectedIndex = 0
                End Try
            End If
        End If
        If ddlsProject2.SelectedIndex <> 0 Then
            Call LoadPhase(ddlsPhase2, "S", ddlsProject2.SelectedValue)
        End If
        If Session("ADI_ImportStandardTargetPrice_ddlsPhase2") IsNot Nothing Then
            If Session("ADI_ImportStandardTargetPrice_ddlsPhase2").ToString <> String.Empty Then
                Dim strsPhaseDefault2 As String = Session("ADI_ImportStandardTargetPrice_ddlsPhase2").ToString
                Try
                    ddlsPhase2.SelectedValue = strsPhaseDefault2
                Catch ex As Exception
                    ddlsPhase2.SelectedIndex = 0
                End Try
            End If
        End If


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

            lbltabIMPORT1.Text = Me.GetResource("TabIMPORT1", "Text", hddParameterMenuID.Value) 'นำเข้า
            lbltabIMPORT2.Text = Me.GetResource("TabIMPORT2", "Text", hddParameterMenuID.Value) 'อนุมัติ

            'Tab นำเข้า
            lblsProject.Text = Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value)
            lblsPhase.Text = Me.GetResource("lblsPhase", "Text", hddParameterMenuID.Value)
            lblsFileName.Text = Me.GetResource("lblsFileName", "Text", hddParameterMenuID.Value)
            lblsBrowse.Text = Me.GetResource("lblsBrowse", "Text", hddParameterMenuID.Value)
            btnRefresh.Text = Me.GetResource("btnRefresh", "Text", hddParameterMenuID.Value)
            btnImport.Text = Me.GetResource("btnImport", "Text", hddParameterMenuID.Value)
            lblsTarget.Text = Me.GetResource("lblsTarget", "Text", hddParameterMenuID.Value)
            lblsExportTemplate.Text = Me.GetResource("lblsExportTemplate", "Text", hddParameterMenuID.Value)

            hddMSGOKData.Value = Me.GetResource("msg_ok_data", "MSG")
            hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG")

            lblHeaderVerify.Text = Me.GetResource("HeaderVerify", "Text", hddParameterMenuID.Value)
            lblBodyVerify.Text = Me.GetResource("BodyVerify", "Text", hddParameterMenuID.Value)
            lblHeaderVerify2.Text = Me.GetResource("HeaderVerify", "Text", hddParameterMenuID.Value)
            lblBodyVerify2.Text = Me.GetResource("BodyVerify2", "Text", hddParameterMenuID.Value)
            lblHeaderImport.Text = Me.GetResource("HeaderImport", "Text", hddParameterMenuID.Value)
            lblBodyImport.Text = Me.GetResource("BodyImport", "Text", hddParameterMenuID.Value)

            'Dim bl As cImportStandardTargetPrice = New cImportStandardTargetPrice
            'If CurrentUser.Position IsNot Nothing Then
            '    Dim _PriceApprove As String = bl.GetPosition(CurrentUser.Position)
            '    If _PriceApprove = "0" OrElse _PriceApprove = "" Then 'คือห้าม Approve
            '        lblUserNotApprove.Text = Me.GetResource("usernotapprove", "Text", hddParameterMenuID.Value)
            '    Else '"1" คือ Approve ได้
            '        lblUserNotApprove.Text = ""
            '    End If
            'Else 'ไม่ Set Position คือห้าม Approve
            '    lblUserNotApprove.Text = Me.GetResource("usernotapprove", "Text", hddParameterMenuID.Value)
            'End If

            'Tab อนุมัติ
            lblsProject2.Text = Me.GetResource("lblsProject2", "Text", hddParameterMenuID.Value)
            lblsPhase2.Text = Me.GetResource("lblsPhase2", "Text", hddParameterMenuID.Value)
            chbItemNoApprove.Text = Me.GetResource("chbItemNoApprove", "Text", hddParameterMenuID.Value)
            btnRefresh2.Text = Me.GetResource("btnRefresh2", "Text", hddParameterMenuID.Value)
            btnApprove2.Text = Me.GetResource("btnApprove2", "Text", hddParameterMenuID.Value)

            hddMSGImportData.Value = Me.GetResource("msg_importdata", "MSG", hddParameterMenuID.Value)
            hddMSGApproveData.Value = Me.GetResource("msg_approvedata", "MSG", hddParameterMenuID.Value)
            hddMSGImportDataNo.Value = Me.GetResource("msg_importdata_no", "MSG", hddParameterMenuID.Value)
            hddMSGApproveDataNo.Value = Me.GetResource("msg_approvedata_no", "MSG", hddParameterMenuID.Value)
            hddMSGPleaseImport.Value = Me.GetResource("msg_pleaseimport", "MSG", hddParameterMenuID.Value)
            hddMSGVerify.Value = Me.GetResource("msg_verify", "MSG", hddParameterMenuID.Value)
            '
            lblValidate1.Text = Me.GetResource("msg_notempty", "MSG", hddParameterMenuID.Value)
            lblValidate2.Text = Me.GetResource("msg_noduplicate", "MSG", hddParameterMenuID.Value)
            lblValidate3.Text = Me.GetResource("msg_notnumber", "MSG", hddParameterMenuID.Value)
            lblValidate4.Text = Me.GetResource("msg_notprice", "MSG", hddParameterMenuID.Value)


            '1.ข้อมูลรหัสโครงการ และข้อมูลเลขที่แปลงต้องไม่เป็นค่าว่าง hddMSGNotEmpty.Value = Me.GetResource("msg_notempty", "MSG", hddParameterMenuID.Value)
            '2.ข้อมูลรหัสโครงการ และข้อมูลเลขที่แปลงห้ามซ้ำกัน hddMSGNoDuplica.Value = Me.GetResource("msg_noduplicate", "MSG", hddParameterMenuID.Value)
            '3.ไม่มีข้อมูลเลขที่แปลงในโครงการและเฟสนี้ hddMSGNotNumber.Value = Me.GetResource("msg_notnumber", "MSG", hddParameterMenuID.Value)
            '4.ข้อมูลราคาไม่ถูกต้อง hddMSGNotPrice.Value = Me.GetResource("msg_notprice", "MSG", hddParameterMenuID.Value)

            Call clearTextTab1()
            Call clearTextTab2()

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadInit", ex)
        End Try
    End Sub
    Public Sub clearTextTab1()
        hddChkVerifyModal.Value = ""
        ddlsProject.SelectedIndex = 0
        ddlsPhase.SelectedIndex = 0
        txtsTarget.Text = "0.00"
        grdImport.DataSource = Nothing
        grdImport.DataBind()
        btnImport.Attributes.Add("disabled", "")

        pnComment.Style.Add("display", "none")
    End Sub
    Public Sub clearTextTab2()
        ddlsProject2.SelectedIndex = 0
        ddlsPhase2.SelectedIndex = 0
        lblsSumPriceS.Text = ""
        lblsSumPriceG.Text = ""
        chbItemNoApprove.Checked = True
        grdApprove.DataSource = Nothing
        grdApprove.DataBind()
        'If lblUserNotApprove.Text = "" Then
        '    btnApprove2.Attributes.Remove("disabled")
        'Else
        '    btnApprove2.Attributes.Add("disabled", "")
        'End If
    End Sub

#End Region

#Region "Session"
    Public Function GetDataImportSTPrice() As List(Of CoreUser)
        Try
            Return Session("IDOCS.application.LoaddataImportStandardPriceAndTargetPrice")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataImportSTPrice(ByVal lstMenu As List(Of CoreUser)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataImportStandardPriceAndTargetPrice", lstMenu)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Tab 1"

#Region "Excel"

    Dim ColCell1En As String = "Project Code"
    Dim ColCell2En As String = "Unit No"
    Dim ColCell3En As String = "Lan Price / SQW"
    Dim ColCell4En As String = "Cost Price"
    Dim ColCell5En As String = "Standard Price"
    Dim ColCell6En As String = "Locate Price"
    Dim ColCell7En As String = "Target Price"

    Dim ColCell1Th As String = "รหัสโครงการ"
    Dim ColCell2Th As String = "เลขที่แปลง"
    Dim ColCell3Th As String = "ราคาที่ดินต่อตารางวา"
    Dim ColCell4Th As String = "ราคาต้นทุน"
    Dim ColCell5Th As String = "ราคามารตฐาน"
    Dim ColCell6Th As String = "ราคาตั้ง"
    Dim ColCell7Th As String = "ราคาเป้าหมาย"

    Dim ColCell1 As String = ""
    Dim ColCell2 As String = ""
    Dim ColCell3 As String = ""
    Dim ColCell4 As String = ""
    Dim ColCell5 As String = ""
    Dim ColCell6 As String = ""
    Dim ColCell7 As String = ""

    Public Function cellTextToNumber(ByVal cellText As String) As Integer
        Dim iNumber As Integer = 0
        Select Case cellText.ToLower().Trim
            Case "a"
                iNumber = 0
            Case "b"
                iNumber = 1
            Case "c"
                iNumber = 2
            Case "d"
                iNumber = 3
            Case "e"
                iNumber = 4
            Case "f"
                iNumber = 5
            Case "g"
                iNumber = 6
            Case "h"
                iNumber = 7
            Case "i"
                iNumber = 8
            Case "j"
                iNumber = 9
        End Select

        Return iNumber
    End Function

    Public Sub CropNumberAndText(ByVal UserKey As String, ByRef sVal As String, ByRef iVal As String)
        Dim Base As String = "0123456789"
        sVal = ""
        iVal = ""
        For Each Item In UserKey
            If Base.IndexOf(Item) >= 0 Then
                iVal += Item
            Else
                sVal += Item
            End If
        Next
    End Sub

    Protected Sub lblsExportTemplate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lblsExportTemplate.Click
        Try
            'If ddlsProject.SelectedValue = String.Empty Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value) & "');", True)
            'ElseIf ddlsPhase.SelectedValue = String.Empty Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsPhase", "Text", hddParameterMenuID.Value) & "');", True)
            'Else
            Call Redirect("ADI_ImportStandardTargetPriceTemplates.ashx?infoLang=" & hddpLG.Value)
            'End If
            Call RegisterSetTabScript(TabNameEnum.IM1)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnClearRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearRefresh.Click
        Try
            txtsTarget.Text = "0.00"
            Session.Remove("dtUploadSTPriceImport")
            btnImport.Attributes.Add("disabled", "")
            grdImport.DataSource = Nothing
            grdImport.DataBind()
            Call RegisterSetTabScript(TabNameEnum.IM1)
            UpdatePanelMain.Update()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnClearRefresh_Click", ex)
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
        Try
            hddChkVerifyModal.Value = ""
            'Me.lblUploadUF.Text = ""
            Session.Remove("dtUploadSTPriceImport")
            btnImport.Attributes.Remove("disabled")
            grdImport.DataSource = Nothing
            grdImport.DataBind()

            pnComment.Style.Add("display", "none")
            grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"

            Dim strMassage As String = String.Empty
            If ddlsProject.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value) & "');", True)
                Call RegisterSetTabScript(TabNameEnum.IM1)
                Exit Sub
            ElseIf ddlsPhase.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsPhase", "Text", hddParameterMenuID.Value) & "');", True)
                Call RegisterSetTabScript(TabNameEnum.IM1)
                Exit Sub
            End If

            If Me.fileUploadUF.HasFile Then
                Dim workbook As IWorkbook
                If Me.fileUploadUF.FileName.EndsWith(".xlsx") Then
                    workbook = New XSSFWorkbook(Me.fileUploadUF.PostedFile.InputStream)
                ElseIf Me.fileUploadUF.FileName.EndsWith(".xls") Then
                    workbook = New HSSFWorkbook(Me.fileUploadUF.PostedFile.InputStream)
                Else
                    Throw New Exception("Invalid file type.")
                End If

                'Dim datasheet = workbook.GetSheetAt(0)
                Dim datasheet = workbook.GetSheet("Sales & Marketing")
                Dim datasheet2 = workbook.GetSheet("Cost-Unit 093")
                Dim datasheet3 = workbook.GetSheet("status 300714")

                If datasheet Is Nothing Then
                    Throw New Exception("Sales & Marketing")
                End If



                'Document  Name
                Dim ColProjectCode As ICell = Nothing
                Dim ColUnitNo As ICell = Nothing
                Dim ColLanPriceSqw As ICell = Nothing
                Dim ColCostPrice As ICell = Nothing
                Dim ColStandardPrice As ICell = Nothing
                Dim ColLocatePrice As ICell = Nothing
                Dim ColTargetPrice As ICell = Nothing

                If hddpLG.Value = "TH" Then
                    ColCell1 = ColCell1Th
                    ColCell2 = ColCell2Th
                    ColCell3 = ColCell3Th
                    ColCell4 = ColCell4Th
                    ColCell5 = ColCell5Th
                    ColCell6 = ColCell6Th
                    ColCell7 = ColCell7Th
                Else
                    ColCell1 = ColCell1En.ToLower().Trim
                    ColCell2 = ColCell2En.ToLower().Trim
                    ColCell3 = ColCell3En.ToLower().Trim
                    ColCell4 = ColCell4En.ToLower().Trim
                    ColCell5 = ColCell5En.ToLower().Trim
                    ColCell6 = ColCell6En.ToLower().Trim
                    ColCell7 = ColCell7En.ToLower().Trim
                End If


                Dim bl As cImportStandardTargetPrice = New cImportStandardTargetPrice
                Dim listPhaseChk As List(Of ED03UNIT) = bl.GetPhaseCheck(ddlsProject.SelectedValue.Split("-")(0).ToString, ddlsPhase.SelectedValue.ToString)
                Dim dtPhaseChk As New DataTable
                Dim drPhaseChk() As DataRow = Nothing
                dtPhaseChk.Columns.Add("FREPRJNO")
                dtPhaseChk.Columns.Add("FREPHASE")
                dtPhaseChk.Columns.Add("FSERIALNO")
                dtPhaseChk.Columns.Add("FRESTATUS")
                If listPhaseChk IsNot Nothing Then
                    AddDatatableFilePhaseChk(dtPhaseChk, listPhaseChk)
                End If

                Dim fileNameAdded As String = ""
                Dim filePath As String = ""
                Dim _Filepath As String = ""
                Dim chkS As Boolean = True
                Dim _path As String = ""

                Dim vStatus As String
                Dim dt As New DataTable
                'Dim dtExcelDup As New DataTable
                Dim dtImport As New DataTable
                'Dim drExcelDup() As DataRow = Nothing
                Dim drCheckStatus() As DataRow = Nothing
                Dim bRemoveStatus As Boolean = True
                Dim dr As DataRow
                dt = createHeader()
                dt.Columns.Add("chk_status", GetType(String))
                dt.Columns.Add("FRESTATUS", GetType(String))


                Dim _UnitNo As String = ""
                Dim _Sheet2 As String = ""
                Dim _Sheet3 As String = ""
                Dim row2 As Integer = 0
                Dim row3 As Integer = 0
                Dim cell2 As Integer = 0
                Dim cell3 As Integer = 0
                Dim sVal As String = ""
                Dim iVal As String = ""
                Dim chkStep As Boolean = False
                For row As Integer = 0 To datasheet.PhysicalNumberOfRows - 1

                    'datasheet.GetRow(4).Cells(0)	{+'Cost-Unit 093'!B8}
                    'datasheet.GetRow(4).Cells(7)	{0}			
                    'datasheet.GetRow(4).Cells(8)	{0}			 
                    'datasheet.GetRow(4).Cells(9)	{* 1,000,000}
                    'datasheet.GetRow(4).Cells(19)	{1,000,000}	
                    'datasheet.GetRow(4).Cells(16)	{990,000}	    

                    If row > 3 Then 'เริ่มที่ Row 4

                        dr = dt.NewRow()

                        'If Not IsDBNull(datasheet.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK)) Then
                        '    If datasheet.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                        '        dr.Item("ProjectCode") = datasheet.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).ToString
                        '    End If
                        'End If
                        Try
                            dr.Item("ProjectCode") = ddlsProject.SelectedValue.Split("-")(0).ToString
                        Catch ex As Exception
                            dr.Item("ProjectCode") = ddlsProject.SelectedValue
                        End Try

                        If Not IsDBNull(datasheet.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK)) Then
                            If datasheet.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    _UnitNo = datasheet.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).StringCellValue
                                Catch ex As Exception
                                    _UnitNo = datasheet.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).NumericCellValue
                                End Try
                                'Try
                                '    _UnitNo = datasheet.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).ToString.Split("!")(1).ToString
                                '    chkStep = True
                                'Catch ex As Exception
                                '    _UnitNo = datasheet.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).ToString
                                '    chkStep = False
                                'End Try

                                'If chkStep = True Then
                                '    chkStep = False
                                '    Call CropNumberAndText(_UnitNo, sVal, iVal)
                                '    If iVal <> String.Empty Then
                                '        row2 = CInt(iVal) - 1
                                '        cell2 = cellTextToNumber(sVal)
                                '        If Not IsDBNull(datasheet2.GetRow(row2).GetCell(cell2, CREATE_NULL_AS_BLANK)) Then
                                '            If datasheet2.GetRow(row2).GetCell(cell2, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                '                Try
                                '                    _Sheet2 = datasheet2.GetRow(row2).GetCell(cell2, CREATE_NULL_AS_BLANK).ToString.Split("!")(1).ToString
                                '                    _UnitNo = _Sheet2
                                '                    chkStep = True
                                '                Catch ex As Exception
                                '                    _Sheet2 = datasheet2.GetRow(row2).GetCell(cell2, CREATE_NULL_AS_BLANK).ToString
                                '                    _UnitNo = _Sheet2
                                '                    chkStep = False
                                '                End Try
                                '            End If
                                '        End If
                                '    End If
                                'End If


                                'If chkStep = True Then
                                '    chkStep = False
                                '    Call CropNumberAndText(_UnitNo, sVal, iVal)
                                '    If iVal <> String.Empty Then
                                '        row3 = CInt(iVal) - 1
                                '        cell3 = cellTextToNumber(sVal)
                                '        If Not IsDBNull(datasheet3.GetRow(row3).GetCell(cell3, CREATE_NULL_AS_BLANK)) Then
                                '            If datasheet3.GetRow(row3).GetCell(cell3, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                '                _Sheet3 = datasheet3.GetRow(row3).GetCell(cell3, CREATE_NULL_AS_BLANK).ToString
                                '                _UnitNo = _Sheet3
                                '            End If
                                '        End If
                                '    End If
                                'End If


                                dr.Item("UnitNo") = _UnitNo
                            End If
                        End If

                        If Not IsDBNull(datasheet.GetRow(row).GetCell(7, CREATE_NULL_AS_BLANK)) Then
                            If datasheet.GetRow(row).GetCell(7, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item("LanPriceSqw") = datasheet.GetRow(row).GetCell(7, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item("LanPriceSqw") = datasheet.GetRow(row).GetCell(7, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item("LanPriceSqw") = datasheet.GetRow(row).GetCell(7, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If

                        If Not IsDBNull(datasheet.GetRow(row).GetCell(8, CREATE_NULL_AS_BLANK)) Then
                            If datasheet.GetRow(row).GetCell(8, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item("CostPrice") = datasheet.GetRow(row).GetCell(8, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item("CostPrice") = datasheet.GetRow(row).GetCell(8, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item("CostPrice") = datasheet.GetRow(row).GetCell(8, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If

                        If Not IsDBNull(datasheet.GetRow(row).GetCell(9, CREATE_NULL_AS_BLANK)) Then
                            If datasheet.GetRow(row).GetCell(9, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item("StandardPrice") = datasheet.GetRow(row).GetCell(9, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item("StandardPrice") = datasheet.GetRow(row).GetCell(9, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item("StandardPrice") = datasheet.GetRow(row).GetCell(9, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If

                        If Not IsDBNull(datasheet.GetRow(row).GetCell(19, CREATE_NULL_AS_BLANK)) Then
                            If datasheet.GetRow(row).GetCell(19, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item("LocatePrice") = datasheet.GetRow(row).GetCell(19, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item("LocatePrice") = datasheet.GetRow(row).GetCell(19, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item("LocatePrice") = datasheet.GetRow(row).GetCell(19, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If

                        If Not IsDBNull(datasheet.GetRow(row).GetCell(16, CREATE_NULL_AS_BLANK)) Then
                            If datasheet.GetRow(row).GetCell(16, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item("TargetPrice") = datasheet.GetRow(row).GetCell(16, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item("TargetPrice") = datasheet.GetRow(row).GetCell(16, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item("TargetPrice") = datasheet.GetRow(row).GetCell(16, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If


                        If IsDBNull(dr.Item("UnitNo")) Then 'IsDBNull(dr.Item("ProjectCode")) AndAlso
                            Exit For
                        Else
                            dt.Rows.Add(dr)
                        End If

                    End If

                Next

                If dt Is Nothing Or dt.Rows.Count = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & grtt("resDataNotFound") & "');", True)
                    Exit Sub
                End If

                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            With dt.Rows(i)
                                chkS = True
                                .Item("chk_status") = 0

                                If .Item("ProjectCode").ToString = String.Empty Then
                                    .Item("chk_status") = 1
                                    btnImport.Attributes.Add("disabled", "")
                                    chkS = False
                                    Exit For
                                ElseIf .Item("UnitNo").ToString = String.Empty Then
                                    .Item("chk_status") = 1
                                    btnImport.Attributes.Add("disabled", "")
                                    chkS = False
                                    Exit For
                                End If

                                If chkS = True Then
                                    If dt.Select("isnull(UnitNo,'')='" & .Item("UnitNo") & "'").Count > 1 Then
                                        .Item("chk_status") = 2
                                        btnImport.Attributes.Add("disabled", "")
                                        chkS = False
                                        Exit For
                                    End If
                                End If

                                If chkS = True Then
                                    If dtPhaseChk IsNot Nothing Then
                                        If dtPhaseChk.Rows.Count > 0 Then
                                            drPhaseChk = dtPhaseChk.Select("isnull(FREPRJNO,'')='" & .Item("ProjectCode") & "' and isnull(FSERIALNO,'')='" & .Item("UnitNo") & "' ")
                                            If drPhaseChk.Length = 0 Then
                                                .Item("chk_status") = 3
                                                btnImport.Attributes.Add("disabled", "")
                                                chkS = False
                                                Exit For
                                            Else
                                                '1   NULL	
                                                '2   1	Blank
                                                '3   2	จอง
                                                '4   3	ทำสัญญา
                                                '5       
                                                '6   4	โอนกรรมสิทธิ์
                                                '7   0	ไม่เปิดขาย
                                                .Item("FRESTATUS") = drPhaseChk(0)("FRESTATUS").ToString
                                                If drPhaseChk(0)("FRESTATUS").ToString = "2" OrElse
                                                    drPhaseChk(0)("FRESTATUS").ToString = "3" OrElse
                                                    drPhaseChk(0)("FRESTATUS").ToString = "4" Then
                                                    hddChkVerifyModal.Value = "Y"
                                                End If
                                            End If
                                        End If
                                    End If
                                End If


                                If chkS = True Then
                                    'เช็คเรื่องตัวเลข
                                    If .Item("LanPriceSqw").ToString = String.Empty Then
                                        .Item("LanPriceSqw") = "0.00"
                                    Else
                                        Try
                                            Dim _LanPriceSqw As Decimal = CDec(.Item("LanPriceSqw").ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If
                                    If .Item("CostPrice").ToString = String.Empty Then
                                        .Item("CostPrice") = "0.00"
                                    Else
                                        Try
                                            Dim _CostPrice As Decimal = CDec(.Item("CostPrice").ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If
                                    If .Item("StandardPrice").ToString = String.Empty Then
                                        .Item("StandardPrice") = "0.00"
                                    Else
                                        Try
                                            Dim _StandardPrice As Decimal = CDec(.Item("StandardPrice").ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If
                                    If .Item("LocatePrice").ToString = String.Empty Then
                                        .Item("LocatePrice") = "0.00"
                                    Else
                                        Try
                                            Dim _LocatePrice As Decimal = CDec(.Item("LocatePrice").ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If
                                    If .Item("TargetPrice").ToString = String.Empty Then
                                        .Item("TargetPrice") = "0.00"
                                    Else
                                        Try
                                            Dim _TargetPrice As Decimal = CDec(.Item("TargetPrice").ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If

                                    If .Item("LanPriceSqw").ToString <> String.Empty AndAlso .Item("LanPriceSqw").ToString <> "0.00" Then
                                        .Item("LanPriceSqw") = CDec(.Item("LanPriceSqw")).ToString("#,##0.00")
                                    End If
                                    If .Item("CostPrice").ToString <> String.Empty AndAlso .Item("CostPrice").ToString <> "0.00" Then
                                        .Item("CostPrice") = CDec(.Item("CostPrice")).ToString("#,##0.00")
                                    End If
                                    If .Item("StandardPrice").ToString <> String.Empty AndAlso .Item("StandardPrice").ToString <> "0.00" Then
                                        .Item("StandardPrice") = CDec(.Item("StandardPrice")).ToString("#,##0.00")
                                    End If
                                    If .Item("LocatePrice").ToString <> String.Empty AndAlso .Item("LocatePrice").ToString <> "0.00" Then
                                        .Item("LocatePrice") = CDec(.Item("LocatePrice")).ToString("#,##0.00")
                                    End If
                                    If .Item("TargetPrice").ToString <> String.Empty AndAlso .Item("TargetPrice").ToString <> "0.00" Then
                                        .Item("TargetPrice") = CDec(.Item("TargetPrice")).ToString("#,##0.00")
                                    End If

                                End If

                            End With
                        Next
                    End If
                End If


                If dt.Rows.Count > 0 Then
                    If dt.Select("chk_status<>0").Length > 0 Then
                        Dim dt2 As DataTable = New DataTable
                        Call createHeaderNospace2(dt2)
                        Call changeTypeData(dt2, dt)
                        Dim eDV As DataView = New DataView(dt)
                        eDV.RowFilter = "chk_status<>0"
                        Me.grdImport.DataSource = eDV
                    Else
                        Me.grdImport.DataSource = dt
                    End If
                Else
                    Me.grdImport.DataSource = dt
                End If
                grdImport.DataBind()

                Session.Add("dtUploadSTPriceImport", dt)
                drCheckStatus = dt.Select("chk_status in('1','2','3','4')")
                If drCheckStatus.Length = 0 Then
                    For Each row As DataRow In dt.Select("FRESTATUS in('2','3','4')")
                        row.Delete()
                        If bRemoveStatus = True Then bRemoveStatus = False
                    Next
                    If bRemoveStatus = True Then
                        btnImport.Attributes.Remove("disabled")
                        grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"
                        ' "Verify Data Success."

                        If hddChkVerifyModal.Value = "Y" Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "ConfirmVerify();", True)
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogSuccess('" & hddMSGVerify.Value & "');", True)
                        End If
                        Call RegisterSetTabScript(TabNameEnum.IM1)
                    Else
                        Session.Remove("dtUploadSTPriceImport")
                        Session.Add("dtUploadSTPriceImport", dt)
                        If dt.Rows.Count > 0 Then
                            btnImport.Attributes.Remove("disabled")
                            grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"
                            grdImport.DataSource = dt
                            grdImport.DataBind()
                            ' "Verify Data Success."

                            If hddChkVerifyModal.Value = "Y" Then
                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "ConfirmVerify();", True)
                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogSuccess('" & hddMSGVerify.Value & "');", True)
                            End If
                            Call RegisterSetTabScript(TabNameEnum.IM1)
                        Else
                            btnImport.Attributes.Add("disabled", "")
                            grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"
                            grdImport.DataSource = Nothing
                            grdImport.DataBind()

                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "ConfirmVerify2();", True)
                            Call RegisterSetTabScript(TabNameEnum.IM1)
                        End If
                    End If

                    'btnImport.Attributes.Remove("disabled")
                    'grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"
                    '' "Verify Data Success."

                    ''If hddChkVerifyModal.Value = "Y" Then
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "ConfirmVerify();", True)
                    ''Else
                    ''    ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogSuccess('" & hddMSGVerify.Value & "');", True)
                    ''End If
                    'Call RegisterSetTabScript(TabNameEnum.IM1)
                    ''Exit Sub

                Else
                    grdImport.CssClass = ""
                    '===== Set Color
                    '1.ข้อมูลรหัสโครงการ และข้อมูลเลขที่แปลงต้องไม่เป็นค่าว่าง hddMSGNotEmpty.Value = Me.GetResource("msg_notempty", "MSG", hddParameterMenuID.Value)
                    '2.ข้อมูลรหัสโครงการ และข้อมูลเลขที่แปลงห้ามซ้ำกัน hddMSGNoDuplica.Value = Me.GetResource("msg_noduplicate", "MSG", hddParameterMenuID.Value)
                    '3.ไม่มีข้อมูลเลขที่แปลงในโครงการและเฟสนี้ hddMSGNotNumber.Value = Me.GetResource("msg_notnumber", "MSG", hddParameterMenuID.Value)
                    '4.ข้อมูลราคาไม่ถูกต้อง hddMSGNotPrice.Value = Me.GetResource("msg_notprice", "MSG", hddParameterMenuID.Value)
                    For Each vRow As GridViewRow In Me.grdImport.Rows
                        vStatus = (TryCast(vRow.Cells(vRow.Cells.Count - 1).Controls(0), DataBoundLiteralControl).Text).Trim(New [Char]() {" "c, ControlChars.Cr, ControlChars.Lf})
                        'vStatus = vRow.Cells(vRow.Cells.Count - 1).Text
                        If vStatus = "1" Then
                            vRow.BackColor = Color.Pink
                        ElseIf vStatus = "2" Then
                            vRow.BackColor = Color.SaddleBrown
                        ElseIf vStatus = "3" Then
                            vRow.BackColor = Color.Yellow
                        ElseIf vStatus = "4" Then
                            vRow.BackColor = Color.Red
                        End If
                        'vRow.ForeColor = Color.White
                    Next
                    pnComment.Style.Add("display", "")
                    btnImport.Attributes.Add("disabled", "")
                    Call RegisterSetTabScript(TabNameEnum.IM1)
                    'Exit Sub
                End If
            Else
                '"Please select a file to import."
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & hddMSGPleaseImport.Value & "');", True)
                Call RegisterSetTabScript(TabNameEnum.IM1)
                'Exit Sub
            End If
        Catch ex As Exception
            Call RegisterSetTabScript(TabNameEnum.IM1)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & ex.Message.ToString() & "');", True)
        End Try
        'Call RegisterSetTabScript(TabNameEnum.IM1)
    End Sub

    Protected Sub btnApproveData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApproveData.Click
        Try
            txtsTarget.Text = "0.00"
            Session.Remove("dtUploadSTPriceImport")
            btnImport.Attributes.Add("disabled", "")
            grdImport.DataSource = Nothing
            grdImport.DataBind()

            chbItemNoApprove.Checked = True
            'If lblUserNotApprove.Text = "" Then
            '    btnApprove2.Attributes.Remove("disabled")
            'Else
            '    btnApprove2.Attributes.Add("disabled", "")
            'End If

            ddlsProject2.SelectedValue = ddlsProject.SelectedValue
            LoadPhase(ddlsPhase2, "S", ddlsProject.SelectedValue)
            ddlsPhase2.SelectedValue = ddlsPhase.SelectedValue
            Call LoadDataTab2()
            Call RegisterSetTabScript(TabNameEnum.IM2)

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnApproveData_Click", ex)
        End Try
    End Sub

    Protected Sub btnImport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImport.Click
        Try
            If ddlsProject.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value) & "');", True)
                Call RegisterSetTabScript(TabNameEnum.IM1)
                Exit Sub
            ElseIf ddlsPhase.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsPhase", "Text", hddParameterMenuID.Value) & "');", True)
                Call RegisterSetTabScript(TabNameEnum.IM1)
                Exit Sub
            End If
            Dim strMassage As String = ""
            Dim dtFileUploadSTPrice As New DataTable
            If Session("dtUploadSTPriceImport") IsNot Nothing Then
                dtFileUploadSTPrice = Session("dtUploadSTPriceImport")
            End If

            Dim succ As Boolean = True
            Dim bl As cImportStandardTargetPrice = New cImportStandardTargetPrice

            If dtFileUploadSTPrice IsNot Nothing Then
                If dtFileUploadSTPrice.Rows.Count > 0 Then
                    If Not bl.AddUploadAll(dtFileUploadSTPrice,
                                           txtsTarget.Text,
                                           Me.CurrentUser.UserID) Then
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & "Upload data fail." & "');", True)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & hddMSGImportDataNo.Value & "');", True)
                        Call RegisterSetTabScript(TabNameEnum.IM1)
                    Else
                        'txtsTarget.Text = "0.00"
                        Session.Remove("dtUploadSTPriceImport")
                        btnImport.Attributes.Add("disabled", "")
                        'grdImport.DataSource = Nothing
                        'grdImport.DataBind()

                        chbItemNoApprove.Checked = True
                        'If lblUserNotApprove.Text = "" Then
                        '    btnApprove2.Attributes.Remove("disabled")
                        'Else
                        '    btnApprove2.Attributes.Add("disabled", "")
                        'End If
                        Session.Remove("ADI_ImportStandardTargetPrice_ddlsProject")

                        ''ddlsProject2.SelectedValue = ddlsProject.SelectedValue
                        ''ddlsPhase2.SelectedValue = ddlsPhase.SelectedValue
                        ''Call LoadDataTab2()
                        'If lblUserNotApprove.Text = "" Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "ConfirmImport();", True)
                        'Else
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogSuccess('" & hddMSGImportData.Value & "');", True)
                        '    Call RegisterSetTabScript(TabNameEnum.IM1)
                        'End If
                        ''ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogSuccess('" & hddMSGImportData.Value & "');", True)
                        ''ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & "Upload Data Success." & "');", True)
                        ''Call RegisterSetTabScript(TabNameEnum.IM2)
                    End If
                Else
                    strMassage = "Please select a file to import."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & strMassage & "');", True)
                    Call RegisterSetTabScript(TabNameEnum.IM1)
                End If
            Else
                strMassage = "Please select a file to import."
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & strMassage & "');", True)
                Call RegisterSetTabScript(TabNameEnum.IM1)
            End If

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnImport_Click", ex)
        End Try
    End Sub

#End Region

#Region "Datatable"
    Dim dtED11COST As New DataTable
    Private Function createHeader() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("ProjectCode")
        dt.Columns.Add("UnitNo")
        dt.Columns.Add("LanPriceSqw")
        dt.Columns.Add("CostPrice")
        dt.Columns.Add("StandardPrice")
        dt.Columns.Add("LocatePrice")
        dt.Columns.Add("TargetPrice")
        Return dt
    End Function

    Private Function createHeaderTab2() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("ProjectCode")
        dt.Columns.Add("UnitNo")
        dt.Columns.Add("LanPriceSqw")
        dt.Columns.Add("CostPrice")
        dt.Columns.Add("StandardPrice")
        dt.Columns.Add("LocatePrice")
        dt.Columns.Add("TargetPrice")
        dt.Columns.Add("GP")
        dt.Columns.Add("ImportDate")
        dt.Columns.Add("Approve")
        dt.Columns.Add("Approvers")
        Return dt
    End Function

    Public Sub AddDatatableFilePhaseChk(ByRef dt As DataTable, ByVal lst As List(Of ED03UNIT))
        Try
            Dim dr As DataRow
            For Each u As ED03UNIT In lst
                dr = dt.NewRow
                dr.Item("FREPRJNO") = u.FREPRJNO
                dr.Item("FREPHASE") = u.FREPHASE
                dr.Item("FSERIALNO") = u.FSERIALNO
                dr.Item("FRESTATUS") = u.FRESTATUS
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            'HelperLog.ErrorLog(CurrentUser.USERID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub

    Private Sub createHeaderNospace2(ByRef dt As DataTable)
        dt.Columns.Add("ProjectCode")
        dt.Columns.Add("UnitNo")
        dt.Columns.Add("LanPriceSqw")
        dt.Columns.Add("CostPrice")
        dt.Columns.Add("StandardPrice")
        dt.Columns.Add("LocatePrice")
        dt.Columns.Add("TargetPrice")
    End Sub

    Private Sub changeTypeData(ByVal dt As DataTable,
                               ByRef vDS As DataTable)
        Try
            Dim dtcopy As New DataTable
            Dim dr As DataRow
            dtcopy = vDS

            ColCell1 = ColCell1En.ToLower().Trim
            ColCell2 = ColCell2En.ToLower().Trim
            ColCell3 = ColCell3En.ToLower().Trim
            ColCell4 = ColCell4En.ToLower().Trim
            ColCell5 = ColCell5En.ToLower().Trim
            ColCell6 = ColCell6En.ToLower().Trim
            ColCell7 = ColCell7En.ToLower().Trim

            Dim strChkStatus As String = "chk_status"

            For i As Integer = 0 To dtcopy.Rows.Count - 1
                dr = dt.NewRow()

                dr.Item(ColCell1.Replace(" ", "")) = dtcopy.Rows(i)(ColCell1.Replace(" ", "")).ToString
                dr.Item(ColCell2.Replace(" ", "")) = dtcopy.Rows(i)(ColCell2.Replace(" ", "")).ToString
                dr.Item(ColCell3.Replace(" ", "")) = dtcopy.Rows(i)(ColCell3.Replace(" ", "")).ToString
                dr.Item(ColCell4.Replace(" ", "")) = dtcopy.Rows(i)(ColCell4.Replace(" ", "")).ToString
                dr.Item(ColCell5.Replace(" ", "")) = dtcopy.Rows(i)(ColCell5.Replace(" ", "")).ToString
                dr.Item(ColCell6.Replace(" ", "")) = dtcopy.Rows(i)(ColCell6.Replace(" ", "")).ToString
                dr.Item(ColCell7.Replace(" ", "")) = dtcopy.Rows(i)(ColCell7.Replace(" ", "")).ToString
                dr.Item(strChkStatus.Replace(" ", "")) = dtcopy.Rows(i)(strChkStatus).ToString

                dt.Rows.Add(dr)
            Next
            vDS = New DataTable
            vDS = dt
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "changeTypeData", ex)
        End Try
    End Sub


#End Region

#End Region

#Region "Tab 2"

    Protected Sub btnRefresh2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh2.Click
        Try
            Session.Remove("dtUploadSTPriceImport")
            If ddlsProject2.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsProject2", "Text", hddParameterMenuID.Value) & "');", True)
            ElseIf ddlsPhase2.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsPhase2", "Text", hddParameterMenuID.Value) & "');", True)
            Else
                If chbItemNoApprove.Checked = True Then
                    If lblUserNotApprove.Text = "" Then
                        btnApprove2.Attributes.Remove("disabled")
                    Else
                        btnApprove2.Attributes.Add("disabled", "")
                    End If
                Else
                    btnApprove2.Attributes.Add("disabled", "")
                End If
                Call LoadDataTab2()
            End If

        Catch ex As Exception
            Call RegisterSetTabScript(TabNameEnum.IM2)
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnRefresh2_Click", ex)
        End Try
        Call RegisterSetTabScript(TabNameEnum.IM2)
    End Sub

    Protected Sub btnApprove2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApprove2.Click
        Try
            If ddlsProject2.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsProject2", "Text", hddParameterMenuID.Value) & "');", True)
            ElseIf ddlsPhase2.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsPhase2", "Text", hddParameterMenuID.Value) & "');", True)
            Else
                Dim dtFileUploadSTPrice As New DataTable
                If Session("dtUploadSTPriceImportTab2") IsNot Nothing Then
                    dtFileUploadSTPrice = Session("dtUploadSTPriceImportTab2")
                End If
                Dim succ As Boolean = True
                Dim bl As cImportStandardTargetPrice = New cImportStandardTargetPrice
                Dim strMassage As String = ""

                If dtFileUploadSTPrice IsNot Nothing Then
                    If dtFileUploadSTPrice.Rows.Count > 0 Then
                        If Not bl.UpdateApproveAll(dtFileUploadSTPrice,
                                           Me.CurrentUser.UserID) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMSGApproveDataNo.Value & "');", True)
                            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & "Approve data fail." & "');", True)
                            Call RegisterSetTabScript(TabNameEnum.IM2)
                        Else
                            Session.Remove("dtUploadSTPriceImport")
                            btnImport.Attributes.Add("disabled", "")
                            grdImport.DataSource = Nothing
                            grdImport.DataBind()

                            Session.Remove("dtUploadSTPriceImportTab2")
                            chbItemNoApprove.Checked = False
                            btnApprove2.Attributes.Add("disabled", "")

                            Call LoadDataTab2()

                            Session.Remove("ADI_ImportStandardTargetPrice_ddlsProject2")

                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & hddMSGApproveData.Value & "');", True)
                            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & "Approve Data Success." & "');", True)
                            Call RegisterSetTabScript(TabNameEnum.IM2)
                        End If
                    Else
                        Call RegisterSetTabScript(TabNameEnum.IM2)
                    End If
                Else
                    Call RegisterSetTabScript(TabNameEnum.IM2)
                End If
            End If
        Catch ex As Exception
            Call RegisterSetTabScript(TabNameEnum.IM2)
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnApprove2_Click", ex)
        End Try
        'Call RegisterSetTabScript(TabNameEnum.IM2)
    End Sub

    Protected Sub LoadDataTab2()
        Try
            Session.Remove("dtUploadSTPriceImportTab2")

            Dim lst As DataTable = clsImportSTPrice.GetED11COST(ddlsProject2.SelectedValue.Split("-")(0).ToString, ddlsPhase2.SelectedValue.ToString, chbItemNoApprove.Checked, Me.WebCulture.ToString.ToUpper)

            grdApprove.DataSource = lst
            grdApprove.DataBind()

            Session.Add("dtUploadSTPriceImportTab2", lst)

            lblsSumPriceS.Text = Me.GetResource("lblsSumPriceS", "Text", hddParameterMenuID.Value) & " : - "
            lblsSumPriceG.Text = Me.GetResource("lblsSumPriceG", "Text", hddParameterMenuID.Value) & " : - "

            If lst IsNot Nothing Then
                If lst.Rows.Count > 0 Then
                    lblsSumPriceS.Text = Me.GetResource("lblsSumPriceS", "Text", hddParameterMenuID.Value)
                    lblsSumPriceG.Text = Me.GetResource("lblsSumPriceG", "Text", hddParameterMenuID.Value)

                    Dim lstSumPriceS As DataTable = clsImportSTPrice.GetSumStandardPrice(ddlsProject2.SelectedValue.Split("-")(0).ToString, ddlsPhase2.SelectedValue.ToString, chbItemNoApprove.Checked)
                    Dim lstSumPriceG As DataTable = clsImportSTPrice.GetSumTargetPrice(ddlsProject2.SelectedValue.Split("-")(0).ToString, ddlsPhase2.SelectedValue.ToString, chbItemNoApprove.Checked)

                    If lstSumPriceS IsNot Nothing Then
                        If lstSumPriceS.Rows.Count > 0 Then lblsSumPriceS.Text = Me.GetResource("lblsSumPriceS", "Text", hddParameterMenuID.Value) & " : " & CDec(lstSumPriceS.Rows(0)("SumStandardPrice")).ToString("#,##0.00")
                    End If
                    If lstSumPriceG IsNot Nothing Then
                        If lstSumPriceG.Rows.Count > 0 Then lblsSumPriceG.Text = Me.GetResource("lblsSumPriceG", "Text", hddParameterMenuID.Value) & " : " & CDec(lstSumPriceG.Rows(0)("SumTargetPrice")).ToString("#,##0.00")
                    End If
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDataTab2", ex)
        End Try
    End Sub
#End Region

End Class
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


Public Class ADI_ImportPriceList
    Inherits BasePage

    Public clsImportPriceList As ClsADI_ImportPriceList = New ClsADI_ImportPriceList
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
                hddParameterMenuID.Value = HelperLog.LoadMenuID("ADI_ImportPriceList.aspx")
                Me.ClearSessionPageLoad("ADI_ImportPriceList.aspx")

                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()

                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())

            Else
                If rdblZone.SelectedValue <> "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();zoneHideShow('" & rdblZone.SelectedValue.ToString & "');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();", True)
                End If
            End If


        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub

#Region "Dropdownlist"
    Public Sub LoadDropdownlist()
        Try
            Call LoadProject(ddlsProject, "S")
            Call LoadPhase(ddlsPhase, "S", ddlsProject.SelectedValue)
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
        Dim bl As cImportPriceList = New cImportPriceList
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

#End Region


#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        If hddpFlagSearch.Value = "1" Then
            Session.Remove("ADI_ImportPriceList_PageInfo")
            Session.Remove("ADI_ImportPriceList_Search")
            Session.Remove("ADI_ImportPriceList_pageLength")
            Session.Remove("ADI_ImportPriceList_keyword")
        End If
        Call Redirect("ADI_ImportPriceList.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("ADI_ImportPriceList_FS")
        Session.Add("ADI_ImportPriceList_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("ADI_ImportPriceList_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("ADI_ImportPriceList_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("ADI_ImportPriceList_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
        Session.Add("ADI_ImportPriceList_ddlsProject", IIf(ddlsProject.SelectedIndex <> 0, ddlsProject.SelectedValue, ""))
        Session.Add("ADI_ImportPriceList_ddlsPhase", IIf(ddlsPhase.SelectedIndex <> 0, ddlsPhase.SelectedValue, ""))

    End Sub
    Public Sub GetParameter()
        If Session("ADI_ImportPriceList_FS") IsNot Nothing Then
            If Session("ADI_ImportPriceList_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("ADI_ImportPriceList_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
                Dim strPageInfo As String = Session("ADI_ImportPriceList_PageInfo").ToString
                If strPageInfo <> String.Empty Then
                    hddpPageInfo.Value = strPageInfo
                End If

            End If
        End If


        If Session("ADI_ImportPriceList_PageInfo") IsNot Nothing Then
            If Session("ADI_ImportPriceList_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("ADI_ImportPriceList_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("ADI_ImportPriceList_Search") IsNot Nothing Then
            If Session("ADI_ImportPriceList_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("ADI_ImportPriceList_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("ADI_ImportPriceList_pageLength") IsNot Nothing Then
            If Session("ADI_ImportPriceList_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("ADI_ImportPriceList_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If

        If Session("ADI_ImportPriceList_ddlsProject") IsNot Nothing Then
            If Session("ADI_ImportPriceList_ddlsProject").ToString <> String.Empty Then
                Dim strsProjectDefault As String = Session("ADI_ImportPriceList_ddlsProject").ToString
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
        If Session("ADI_ImportPriceList_ddlsPhase") IsNot Nothing Then
            If Session("ADI_ImportPriceList_ddlsPhase").ToString <> String.Empty Then
                Dim strsPhaseDefault As String = Session("ADI_ImportPriceList_ddlsPhase").ToString
                Try
                    ddlsPhase.SelectedValue = strsPhaseDefault
                Catch ex As Exception
                    ddlsPhase.SelectedIndex = 0
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
            'Tab นำเข้า
            lblsProject.Text = Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value)
            lblsPhase.Text = Me.GetResource("lblsPhase", "Text", hddParameterMenuID.Value)
            lblsFileName.Text = Me.GetResource("lblsFileName", "Text", hddParameterMenuID.Value)
            lblsBrowse.Text = Me.GetResource("lblsBrowse", "Text", hddParameterMenuID.Value)
            btnRefresh.Text = Me.GetResource("btnRefresh", "Text", hddParameterMenuID.Value)
            btnImport.Text = Me.GetResource("btnImport", "Text", hddParameterMenuID.Value)
            lblsPublicUtility.Text = Me.GetResource("lblsPublicUtility", "Text", hddParameterMenuID.Value) 'ค่าสาธารณูปโภค
            lblsPublicUtilityM.Text = Me.GetResource("lblsPublicUtilityM", "Text", hddParameterMenuID.Value) 'เดือน
            lblsPublicUtilityPerMonth.Text = Me.GetResource("lblsPublicUtilityPerMonth", "Text", hddParameterMenuID.Value) 'เดือนละ
            lblsPublicUtilityPerMonthM.Text = Me.GetResource("lblsPublicUtilityPerMonthM", "Text", hddParameterMenuID.Value) 'บาท
            lblsDownPayment.Text = Me.GetResource("lblsDownPayment", "Text", hddParameterMenuID.Value) 'เงินดาวน์ %


            hddMSGOKData.Value = Me.GetResource("msg_ok_data", "MSG")
            hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG")

            lblHeaderVerify.Text = Me.GetResource("HeaderVerify", "Text", hddParameterMenuID.Value)
            lblBodyVerify.Text = Me.GetResource("BodyVerify", "Text", hddParameterMenuID.Value)
            lblHeaderVerify2.Text = Me.GetResource("HeaderVerify", "Text", hddParameterMenuID.Value)
            lblBodyVerify2.Text = Me.GetResource("BodyVerify2", "Text", hddParameterMenuID.Value)
            lblHeaderVerify3.Text = Me.GetResource("HeaderVerify", "Text", hddParameterMenuID.Value)
            lblBodyVerify3.Text = Me.GetResource("BodyVerify3", "Text", hddParameterMenuID.Value)
            lblHeaderVerify4.Text = Me.GetResource("HeaderVerify", "Text", hddParameterMenuID.Value)
            lblBodyVerify4.Text = Me.GetResource("BodyVerify4", "Text", hddParameterMenuID.Value)
            lblHeaderImportPriceList.Text = Me.GetResource("lblHeaderImportPriceList", "Text", hddParameterMenuID.Value) 'Import Price List
            lblBodyImportPriceList.Text = Me.GetResource("lblBodyImportPriceList", "Text", hddParameterMenuID.Value) 'ระบุเปอร์เซนต์เงินดาวน์ก่อน

            hddMSGImportData.Value = Me.GetResource("msg_importdata", "MSG", hddParameterMenuID.Value)
            hddMSGApproveData.Value = Me.GetResource("msg_approvedata", "MSG", hddParameterMenuID.Value)
            hddMSGImportDataNo.Value = Me.GetResource("msg_importdata_no", "MSG", hddParameterMenuID.Value)
            hddMSGApproveDataNo.Value = Me.GetResource("msg_approvedata_no", "MSG", hddParameterMenuID.Value)
            hddMSGPleaseImport.Value = Me.GetResource("msg_pleaseimport", "MSG", hddParameterMenuID.Value)
            hddMSGVerify.Value = Me.GetResource("msg_verify", "MSG", hddParameterMenuID.Value)

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
        txtsPublicUtility.Text = "0" 'กี่เดือน
        txtsPublicUtilityPerMonth.Text = "0.00" 'กี่บาทต่อเดือน
        txtsDownPayment.Text = String.Empty
        rdblZone.Items.Clear()
        grdImport.DataSource = Nothing
        grdImport.DataBind()
        btnImport.Attributes.Add("disabled", "")

        pnComment.Style.Add("display", "none")
    End Sub
    Public Sub clearTextTab2()

    End Sub

#End Region

#Region "Session"
    Public Function GetDataImportSTPrice() As List(Of CoreUser)
        Try
            Return Session("IDOCS.application.LoaddataImportPriceList")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataImportSTPrice(ByVal lstMenu As List(Of CoreUser)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataImportPriceList", lstMenu)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Tab 1"

#Region "Excel"

    'รหัสโครงการ	    โซน     แบบบ้าน  เลขที่แปลง      เงินจอง           เงินทำสัญญา	        
    'Project Code	Zone	Type	Unit No	    Booking Amount	Contract Amount	    
    'งวดดาวน์	         ราคาที่ดิน	        ราคาต้นทุน	    ราคา GP	    ราคามารตฐาน	    ราคาสถานที่	    ราคาเป้าหมาย	    ราคา Actual	    ราคาโปรโมชั่น	        ราคาขาย
    'Period Down	 Land Price	    Cost Price	GP Price	Standard Price	Location Price	Target Price	Actual Price	Promotion Price	    Price List

    Dim ColProjectCode As String = "ProjectCode"
    Dim ColZone As String = "Zone"
    Dim ColType As String = "Type"
    Dim ColUnitNo As String = "UnitNo"
    Dim ColBookingAmount As String = "BookingAmount"
    Dim ColContractAmount As String = "ContractAmount"
    Dim ColPeriodDown As String = "PeriodDown"
    Dim ColLandPrice As String = "LandPrice"
    Dim ColCostPrice As String = "CostPrice"
    Dim ColGPTarget As String = "GPTarget"
    Dim ColStandardPrice As String = "StandardPrice"
    Dim ColLocationPrice As String = "LocationPrice"
    Dim ColTargetPrice As String = "TargetPrice"
    Dim ColActualPrice As String = "ActualPrice"
    Dim ColPromotionPrice As String = "PromotionPrice"
    Dim ColPriceList As String = "PriceList"
    Dim ColRemark As String = "Remark"
    Dim ColFtrnNO As String = "FtrnNO"


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

    Protected Sub btnClearRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearRefresh.Click
        Try
            txtsPublicUtility.Text = "0" 'กี่เดือน
            txtsPublicUtilityPerMonth.Text = "0.00" 'กี่บาทต่อเดือน
            txtsDownPayment.Text = String.Empty
            Session.Remove("dtUploadPriceListImport")
            Session.Remove("dtGroupZoneHead")
            btnImport.Attributes.Add("disabled", "")
            rdblZone.Items.Clear()
            grdImport.DataSource = Nothing
            grdImport.DataBind()
            'Call RegisterSetTabScript(TabNameEnum.IM1)
            UpdatePanelMain.Update()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnClearRefresh_Click", ex)
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
        Try
            hddChkVerifyModal.Value = ""
            'Me.lblUploadUF.Text = ""
            Session.Remove("dtUploadPriceListImport")
            Session.Remove("dtGroupZoneHead")
            btnImport.Attributes.Remove("disabled")
            rdblZone.Items.Clear()
            grdImport.DataSource = Nothing
            grdImport.DataBind()

            pnComment.Style.Add("display", "none")
            grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"

            Dim strMassage As String = String.Empty
            If ddlsProject.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value) & "');", True)
                'Call RegisterSetTabScript(TabNameEnum.IM1)
                Exit Sub
            ElseIf ddlsPhase.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsPhase", "Text", hddParameterMenuID.Value) & "');", True)
                'Call RegisterSetTabScript(TabNameEnum.IM1)
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

                Dim datasheet0 = workbook.GetSheet("Price list")
                Dim datasheet1 = workbook.GetSheet("ข้อมูลบ้าน")
                Dim datasheet2 = workbook.GetSheet("Sales & Marketing")
                Dim datasheet3 = workbook.GetSheet("Cost-Unit 093")
                Dim datasheet4 = workbook.GetSheet("status 300714")

                If datasheet0 Is Nothing Then
                    Throw New Exception("Price list")
                End If
                If datasheet1 Is Nothing Then
                    Throw New Exception("ข้อมูลบ้าน")
                End If
                If datasheet2 Is Nothing Then
                    Throw New Exception("Sales & Marketing")
                End If

                Dim bl As cImportPriceList = New cImportPriceList
                Dim listPhaseChk As DataTable = clsImportPriceList.GetPhaseCheck(ddlsProject.SelectedValue.Split("-")(0).ToString, ddlsPhase.SelectedValue.ToString)
                Dim dtPhaseChk As New DataTable
                Dim drPhaseChk() As DataRow = Nothing
                dtPhaseChk.Columns.Add("FREZONE")      'โซน
                dtPhaseChk.Columns.Add("FSERIALNO")    'แปลง
                dtPhaseChk.Columns.Add("FPDCODE")      'แบบบ้าน
                dtPhaseChk.Columns.Add("FREPRJNO")     'รหัสโครงการ
                dtPhaseChk.Columns.Add("FREPHASE")     'รหัส เฟส
                dtPhaseChk.Columns.Add("FRESTATUS")    'สถานะการขาย 
                dtPhaseChk.Columns.Add("FGPPERCENT")   'GP Target / ราคาGP
                'FREZONE, FSERIALNO, FPDCODE, FREPRJNO, FREPHASE,FRESTATUS
                'Select Case FREZONE,FSERIALNO,FPDCODE,FREPRJNO,FREPHASE,FRESTATUS from ED03UNIT
                If listPhaseChk IsNot Nothing Then
                    AddDatatableFilePhaseChk(dtPhaseChk, listPhaseChk)
                End If

                Dim fileNameAdded As String = ""
                Dim filePath As String = ""
                Dim _Filepath As String = ""
                Dim chkS As Boolean = True
                Dim _path As String = ""

                Dim vStatus As String = ""
                Dim dt As New DataTable
                Dim dtGroupZoneHead As New DataTable
                Dim dtImport As New DataTable
                Dim drCheckStatus() As DataRow = Nothing
                Dim bRemoveStatus As Boolean = True
                Dim drCheckZone() As DataRow = Nothing
                Dim bRemoveZone As Boolean = True
                Dim dr As DataRow
                Dim drGroupZoneHead As DataRow
                Dim drChkGroupZoneHead() As DataRow = Nothing
                dt = createHeader()
                dt.Columns.Add("chk_status", GetType(String))
                dt.Columns.Add("FRESTATUS", GetType(String))
                dtGroupZoneHead = createHeader()
                dtGroupZoneHead.Columns.Add("chk_status", GetType(String))
                dtGroupZoneHead.Columns.Add("FRESTATUS", GetType(String))


                Dim _UnitNo As String = ""
                Dim _Sheet1 As String = ""
                Dim _Sheet2 As String = ""
                Dim _Sheet3 As String = ""
                Dim _Sheet4 As String = ""
                Dim row1 As Integer = 0
                Dim row2 As Integer = 0
                Dim row3 As Integer = 0
                Dim row4 As Integer = 0
                Dim cell1 As Integer = 0
                Dim cell2 As Integer = 0
                Dim cell3 As Integer = 0
                Dim cell4 As Integer = 0
                Dim sVal As String = ""
                Dim iVal As String = ""
                Dim chkStep As Boolean = False

                For row As Integer = 0 To datasheet0.PhysicalNumberOfRows - 1

                    'datasheet0.GetRow(7).Cells(0)	{+'ข้อมูลบ้าน'!B16}	    ---->   ColUnitNo
                    'datasheet0.GetRow(7).Cells(7)	{5,000}             ---->   ColBookingAmount
                    'datasheet0.GetRow(7).Cells(8)	{20,000}            ---->   ColContractAmount
                    'datasheet0.GetRow(7).Cells(9)	{5}	                ---->   ColPeriodDown



                    If row > 6 Then 'เริ่มที่ Row 8 นับเริ่มจาก Row 0
                        dr = dt.NewRow()

                        Try
                            dr.Item(ColProjectCode) = ddlsProject.SelectedValue.Split("-")(0).ToString
                        Catch ex As Exception
                            dr.Item(ColProjectCode) = ddlsProject.SelectedValue
                        End Try

                        dr.Item(ColZone) = ""
                        dr.Item(ColType) = ""

                        If Not IsDBNull(datasheet0.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK)) Then
                            If datasheet0.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    _UnitNo = datasheet0.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).StringCellValue
                                Catch ex As Exception
                                    _UnitNo = datasheet0.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).NumericCellValue
                                End Try
                                'Try
                                '    _UnitNo = datasheet0.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).ToString.Split("!")(1).ToString
                                '    chkStep = True
                                'Catch ex As Exception
                                '    _UnitNo = datasheet0.GetRow(row).GetCell(0, CREATE_NULL_AS_BLANK).ToString
                                '    chkStep = False
                                'End Try

                                'If chkStep = True Then
                                '    chkStep = False
                                '    Call CropNumberAndText(_UnitNo, sVal, iVal)
                                '    If iVal <> String.Empty Then
                                '        row1 = CInt(iVal) - 1
                                '        cell1 = cellTextToNumber(sVal)
                                '        If Not IsDBNull(datasheet1.GetRow(row1).GetCell(cell1, CREATE_NULL_AS_BLANK)) Then
                                '            If datasheet1.GetRow(row1).GetCell(cell1, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                '                Try
                                '                    _Sheet1 = datasheet1.GetRow(row1).GetCell(cell1, CREATE_NULL_AS_BLANK).ToString.Split("!")(1).ToString
                                '                    _UnitNo = _Sheet1
                                '                    chkStep = True
                                '                Catch ex As Exception
                                '                    _Sheet1 = datasheet1.GetRow(row1).GetCell(cell1, CREATE_NULL_AS_BLANK).ToString
                                '                    _UnitNo = _Sheet1
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
                                '                Try
                                '                    _Sheet3 = datasheet3.GetRow(row3).GetCell(cell3, CREATE_NULL_AS_BLANK).ToString.Split("!")(1).ToString
                                '                    _UnitNo = _Sheet3
                                '                    chkStep = True
                                '                Catch ex As Exception
                                '                    _Sheet3 = datasheet3.GetRow(row3).GetCell(cell3, CREATE_NULL_AS_BLANK).ToString
                                '                    _UnitNo = _Sheet3
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
                                '        row4 = CInt(iVal) - 1
                                '        cell4 = cellTextToNumber(sVal)
                                '        If Not IsDBNull(datasheet4.GetRow(row4).GetCell(cell4, CREATE_NULL_AS_BLANK)) Then
                                '            If datasheet4.GetRow(row4).GetCell(cell4, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                '                _Sheet4 = datasheet4.GetRow(row4).GetCell(cell4, CREATE_NULL_AS_BLANK).ToString
                                '                _UnitNo = _Sheet4
                                '            End If
                                '        End If
                                '    End If
                                'End If


                                dr.Item(ColUnitNo) = _UnitNo
                            End If
                        End If

                        If Not IsDBNull(datasheet0.GetRow(row).GetCell(7, CREATE_NULL_AS_BLANK)) Then
                            If datasheet0.GetRow(row).GetCell(7, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item(ColBookingAmount) = datasheet0.GetRow(row).GetCell(7, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item(ColBookingAmount) = datasheet0.GetRow(row).GetCell(7, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item(ColBookingAmount) = datasheet0.GetRow(row).GetCell(7, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If



                        If Not IsDBNull(datasheet0.GetRow(row).GetCell(8, CREATE_NULL_AS_BLANK)) Then
                            If datasheet0.GetRow(row).GetCell(8, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item(ColContractAmount) = datasheet0.GetRow(row).GetCell(8, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item(ColContractAmount) = datasheet0.GetRow(row).GetCell(8, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item(ColContractAmount) = datasheet0.GetRow(row).GetCell(8, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If

                        If Not IsDBNull(datasheet0.GetRow(row).GetCell(9, CREATE_NULL_AS_BLANK)) Then
                            If datasheet0.GetRow(row).GetCell(9, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item(ColPeriodDown) = datasheet0.GetRow(row).GetCell(9, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item(ColPeriodDown) = datasheet0.GetRow(row).GetCell(9, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item(ColPeriodDown) = datasheet0.GetRow(row).GetCell(9, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If


                        'datasheet2.GetRow(row2).Cells(7)	{0}	            ---->   ColLandPrice
                        'datasheet2.GetRow(row2).Cells(8)	{0}		        ---->   ColCostPrice
                        'datasheet2.GetRow(row2).Cells(9)	{* 1,000,000}	---->   ColStandardPrice
                        'datasheet2.GetRow(row2).Cells(16)	{990,000}	    ---->   ColTargetPrice
                        'datasheet2.GetRow(row2).Cells(19)	{1,000,000}	    ---->   ColActualPrice , ColPriceList

                        If row2 = 0 Then row2 = 4 'เริ่มที่ Row 4
                        If Not IsDBNull(datasheet2.GetRow(row2).GetCell(7, CREATE_NULL_AS_BLANK)) Then
                            If datasheet2.GetRow(row2).GetCell(7, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item(ColLandPrice) = datasheet2.GetRow(row2).GetCell(7, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item(ColLandPrice) = datasheet2.GetRow(row2).GetCell(7, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item(ColLandPrice) = datasheet2.GetRow(row2).GetCell(7, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If

                        If Not IsDBNull(datasheet2.GetRow(row2).GetCell(8, CREATE_NULL_AS_BLANK)) Then
                            If datasheet2.GetRow(row2).GetCell(8, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item(ColCostPrice) = datasheet2.GetRow(row2).GetCell(8, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item(ColCostPrice) = datasheet2.GetRow(row2).GetCell(8, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item(ColCostPrice) = datasheet2.GetRow(row2).GetCell(8, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If

                        dr.Item(ColGPTarget) = ""

                        If Not IsDBNull(datasheet2.GetRow(row2).GetCell(9, CREATE_NULL_AS_BLANK)) Then
                            If datasheet2.GetRow(row2).GetCell(9, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item(ColStandardPrice) = datasheet2.GetRow(row2).GetCell(9, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item(ColStandardPrice) = datasheet2.GetRow(row2).GetCell(9, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item(ColStandardPrice) = datasheet2.GetRow(row2).GetCell(9, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If

                        dr.Item(ColLocationPrice) = "0.00"

                        If Not IsDBNull(datasheet2.GetRow(row2).GetCell(16, CREATE_NULL_AS_BLANK)) Then
                            If datasheet2.GetRow(row2).GetCell(16, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item(ColTargetPrice) = datasheet2.GetRow(row2).GetCell(16, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item(ColTargetPrice) = datasheet2.GetRow(row2).GetCell(16, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item(ColTargetPrice) = datasheet2.GetRow(row2).GetCell(16, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                            End If
                        End If


                        If Not IsDBNull(datasheet2.GetRow(row2).GetCell(19, CREATE_NULL_AS_BLANK)) Then
                            If datasheet2.GetRow(row2).GetCell(19, CREATE_NULL_AS_BLANK).ToString <> String.Empty Then
                                Try
                                    dr.Item(ColActualPrice) = datasheet2.GetRow(row2).GetCell(19, CREATE_NULL_AS_BLANK).NumericCellValue
                                Catch ex As Exception
                                    dr.Item(ColActualPrice) = datasheet2.GetRow(row2).GetCell(19, CREATE_NULL_AS_BLANK).StringCellValue
                                End Try
                                'dr.Item(ColActualPrice) = datasheet2.GetRow(row2).GetCell(19, CREATE_NULL_AS_BLANK).ToString.Replace("*", "").Replace(",", "")
                                dr.Item(ColPriceList) = dr.Item(ColActualPrice)
                            End If
                        End If
                        row2 = row2 + 1

                        dr.Item(ColPromotionPrice) = "0.00"
                        dr.Item(ColRemark) = ""

                        If IsDBNull(dr.Item(ColUnitNo)) Then
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
                                                .Item(ColZone) = drPhaseChk(0)("FREZONE").ToString
                                                .Item(ColType) = drPhaseChk(0)("FPDCODE").ToString
                                                Try
                                                    If drPhaseChk(0)("FGPPERCENT") IsNot Nothing Then
                                                        .Item(ColGPTarget) = drPhaseChk(0)("FGPPERCENT").ToString
                                                    End If
                                                Catch ex As Exception
                                                    .Item(ColGPTarget) = ""
                                                End Try
                                                .Item(ColGPTarget) = drPhaseChk(0)("FGPPERCENT").ToString
                                                If drPhaseChk(0)("FRESTATUS").ToString = "2" OrElse
                                                    drPhaseChk(0)("FRESTATUS").ToString = "3" OrElse
                                                    drPhaseChk(0)("FRESTATUS").ToString = "4" Then
                                                    hddChkVerifyModal.Value = "Y"
                                                End If

                                                If dtGroupZoneHead.Rows.Count = 0 Then
                                                    drGroupZoneHead = dtGroupZoneHead.NewRow()
                                                    drGroupZoneHead.Item(ColProjectCode) = .Item(ColProjectCode).ToString
                                                    drGroupZoneHead.Item(ColZone) = .Item(ColZone).ToString
                                                    drGroupZoneHead.Item(ColType) = .Item(ColType).ToString
                                                    drGroupZoneHead.Item(ColUnitNo) = .Item(ColUnitNo).ToString
                                                    drGroupZoneHead.Item(ColBookingAmount) = .Item(ColBookingAmount).ToString
                                                    drGroupZoneHead.Item(ColContractAmount) = .Item(ColContractAmount).ToString
                                                    drGroupZoneHead.Item(ColPeriodDown) = .Item(ColPeriodDown).ToString
                                                    drGroupZoneHead.Item(ColLandPrice) = .Item(ColLandPrice).ToString
                                                    drGroupZoneHead.Item(ColCostPrice) = .Item(ColCostPrice).ToString
                                                    drGroupZoneHead.Item(ColGPTarget) = .Item(ColGPTarget).ToString
                                                    drGroupZoneHead.Item(ColStandardPrice) = .Item(ColStandardPrice).ToString
                                                    drGroupZoneHead.Item(ColLocationPrice) = .Item(ColLocationPrice).ToString
                                                    drGroupZoneHead.Item(ColTargetPrice) = .Item(ColTargetPrice).ToString
                                                    drGroupZoneHead.Item(ColActualPrice) = .Item(ColActualPrice).ToString
                                                    drGroupZoneHead.Item(ColPriceList) = .Item(ColPriceList).ToString
                                                    drGroupZoneHead.Item(ColPromotionPrice) = .Item(ColPromotionPrice).ToString
                                                    drGroupZoneHead.Item(ColRemark) = .Item(ColRemark).ToString
                                                    drGroupZoneHead.Item("FRESTATUS") = .Item("FRESTATUS").ToString
                                                    drGroupZoneHead.Item("chk_status") = .Item("chk_status").ToString
                                                    dtGroupZoneHead.Rows.Add(drGroupZoneHead)
                                                Else
                                                    drChkGroupZoneHead = dtGroupZoneHead.Select("Zone='" & .Item(ColZone).ToString & "' ")
                                                    If drChkGroupZoneHead.Length = 0 Then
                                                        drGroupZoneHead = dtGroupZoneHead.NewRow()
                                                        drGroupZoneHead.Item(ColProjectCode) = .Item(ColProjectCode).ToString
                                                        drGroupZoneHead.Item(ColZone) = .Item(ColZone).ToString
                                                        drGroupZoneHead.Item(ColType) = .Item(ColType).ToString
                                                        drGroupZoneHead.Item(ColUnitNo) = .Item(ColUnitNo).ToString
                                                        drGroupZoneHead.Item(ColBookingAmount) = .Item(ColBookingAmount).ToString
                                                        drGroupZoneHead.Item(ColContractAmount) = .Item(ColContractAmount).ToString
                                                        drGroupZoneHead.Item(ColPeriodDown) = .Item(ColPeriodDown).ToString
                                                        drGroupZoneHead.Item(ColLandPrice) = .Item(ColLandPrice).ToString
                                                        drGroupZoneHead.Item(ColCostPrice) = .Item(ColCostPrice).ToString
                                                        drGroupZoneHead.Item(ColGPTarget) = .Item(ColGPTarget).ToString
                                                        drGroupZoneHead.Item(ColStandardPrice) = .Item(ColStandardPrice).ToString
                                                        drGroupZoneHead.Item(ColLocationPrice) = .Item(ColLocationPrice).ToString
                                                        drGroupZoneHead.Item(ColTargetPrice) = .Item(ColTargetPrice).ToString
                                                        drGroupZoneHead.Item(ColActualPrice) = .Item(ColActualPrice).ToString
                                                        drGroupZoneHead.Item(ColPriceList) = .Item(ColPriceList).ToString
                                                        drGroupZoneHead.Item(ColPromotionPrice) = .Item(ColPromotionPrice).ToString
                                                        drGroupZoneHead.Item(ColRemark) = .Item(ColRemark).ToString
                                                        drGroupZoneHead.Item("FRESTATUS") = .Item("FRESTATUS").ToString
                                                        drGroupZoneHead.Item("chk_status") = .Item("chk_status").ToString
                                                        dtGroupZoneHead.Rows.Add(drGroupZoneHead)
                                                    End If
                                                End If

                                            End If
                                        End If
                                    End If
                                End If


                                If chkS = True Then
                                    'เช็คเรื่องตัวเลข
                                    If .Item(ColBookingAmount).ToString = String.Empty Then
                                        .Item(ColBookingAmount) = "0.00"
                                    Else
                                        Try
                                            Dim _BookingAmount As Decimal = CDec(.Item(ColBookingAmount).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If
                                    If .Item(ColContractAmount).ToString = String.Empty Then
                                        .Item(ColContractAmount) = "0.00"
                                    Else
                                        Try
                                            Dim _ContractAmount As Decimal = CDec(.Item(ColContractAmount).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If
                                    If .Item(ColPeriodDown).ToString = String.Empty Then
                                        .Item(ColPeriodDown) = "0"
                                    Else
                                        Try
                                            Dim _PeriodDown As Decimal = CDec(.Item(ColPeriodDown).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If
                                    If .Item(ColLandPrice).ToString = String.Empty Then
                                        .Item(ColLandPrice) = "0.00"
                                    Else
                                        Try
                                            Dim _LandPrice As Decimal = CDec(.Item(ColLandPrice).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If
                                    If .Item(ColCostPrice).ToString = String.Empty Then
                                        .Item(ColCostPrice) = "0.00"
                                    Else
                                        Try
                                            Dim _CostPrice As Decimal = CDec(.Item(ColCostPrice).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If


                                    If .Item(ColGPTarget).ToString = String.Empty Then
                                        .Item(ColGPTarget) = "0.00"
                                    Else
                                        Try
                                            Dim _GPTarget As Decimal = CDec(.Item(ColGPTarget).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If

                                    If .Item(ColStandardPrice).ToString = String.Empty Then
                                        .Item(ColStandardPrice) = "0.00"
                                    Else
                                        Try
                                            Dim _StandardPrice As Decimal = CDec(.Item(ColStandardPrice).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If

                                    If .Item(ColLocationPrice).ToString = String.Empty Then
                                        .Item(ColLocationPrice) = "0.00"
                                    Else
                                        Try
                                            Dim _LocationPrice As Decimal = CDec(.Item(ColLocationPrice).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If

                                    If .Item(ColTargetPrice).ToString = String.Empty Then
                                        .Item(ColTargetPrice) = "0.00"
                                    Else
                                        Try
                                            Dim _TargetPrice As Decimal = CDec(.Item(ColTargetPrice).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If

                                    If .Item(ColActualPrice).ToString = String.Empty Then
                                        .Item(ColActualPrice) = "0.00"
                                    Else
                                        Try
                                            Dim _ActualPrice As Decimal = CDec(.Item(ColActualPrice).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If

                                    If .Item(ColPromotionPrice).ToString = String.Empty Then
                                        .Item(ColPromotionPrice) = "0.00"
                                    Else
                                        Try
                                            Dim _PromotionPrice As Decimal = CDec(.Item(ColPromotionPrice).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If

                                    If .Item(ColPriceList).ToString = String.Empty Then
                                        .Item(ColPriceList) = "0.00"
                                    Else
                                        Try
                                            Dim _PriceList As Decimal = CDec(.Item(ColPriceList).ToString)
                                        Catch ex As Exception
                                            .Item("chk_status") = 4
                                            btnImport.Attributes.Add("disabled", "")
                                            chkS = False
                                            Exit For
                                        End Try
                                    End If

                                    If .Item(ColBookingAmount).ToString <> String.Empty AndAlso .Item(ColBookingAmount).ToString <> "0.00" Then
                                        .Item(ColBookingAmount) = CDec(.Item(ColBookingAmount)).ToString("#,##0.00")
                                    End If
                                    If .Item(ColContractAmount).ToString <> String.Empty AndAlso .Item(ColContractAmount).ToString <> "0.00" Then
                                        .Item(ColContractAmount) = CDec(.Item(ColContractAmount)).ToString("#,##0.00")
                                    End If
                                    If .Item(ColPeriodDown).ToString <> String.Empty AndAlso .Item(ColPeriodDown).ToString <> "0" Then
                                        .Item(ColPeriodDown) = CDec(.Item(ColPeriodDown)).ToString("#,##0")
                                    End If
                                    If .Item(ColLandPrice).ToString <> String.Empty AndAlso .Item(ColLandPrice).ToString <> "0.00" Then
                                        .Item(ColLandPrice) = CDec(.Item(ColLandPrice)).ToString("#,##0.00")
                                    End If
                                    If .Item(ColCostPrice).ToString <> String.Empty AndAlso .Item(ColCostPrice).ToString <> "0.00" Then
                                        .Item(ColCostPrice) = CDec(.Item(ColCostPrice)).ToString("#,##0.00")
                                    End If
                                    If .Item(ColGPTarget).ToString <> String.Empty AndAlso .Item(ColGPTarget).ToString <> "0.00" Then
                                        .Item(ColGPTarget) = CDec(.Item(ColGPTarget)).ToString("#,##0.00")
                                    End If
                                    If .Item(ColStandardPrice).ToString <> String.Empty AndAlso .Item(ColStandardPrice).ToString <> "0.00" Then
                                        .Item(ColStandardPrice) = CDec(.Item(ColStandardPrice)).ToString("#,##0.00")
                                    End If
                                    If .Item(ColLocationPrice).ToString <> String.Empty AndAlso .Item(ColLocationPrice).ToString <> "0.00" Then
                                        .Item(ColLocationPrice) = CDec(.Item(ColLocationPrice)).ToString("#,##0.00")
                                    End If
                                    If .Item(ColTargetPrice).ToString <> String.Empty AndAlso .Item(ColTargetPrice).ToString <> "0.00" Then
                                        .Item(ColTargetPrice) = CDec(.Item(ColTargetPrice)).ToString("#,##0.00")
                                    End If
                                    If .Item(ColActualPrice).ToString <> String.Empty AndAlso .Item(ColActualPrice).ToString <> "0.00" Then
                                        .Item(ColActualPrice) = CDec(.Item(ColActualPrice)).ToString("#,##0.00")
                                    End If
                                    If .Item(ColPromotionPrice).ToString <> String.Empty AndAlso .Item(ColPromotionPrice).ToString <> "0.00" Then
                                        .Item(ColPromotionPrice) = CDec(.Item(ColPromotionPrice)).ToString("#,##0.00")
                                    End If
                                    If .Item(ColPriceList).ToString <> String.Empty AndAlso .Item(ColPriceList).ToString <> "0.00" Then
                                        .Item(ColPriceList) = CDec(.Item(ColPriceList)).ToString("#,##0.00")
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

                Session.Add("dtGroupZoneHead", dtGroupZoneHead)
                Session.Add("dtUploadPriceListImport", dt)
                drCheckStatus = dt.Select("chk_status in('1','2','3','4')")
                If drCheckStatus.Length = 0 Then
                    For Each row As DataRow In dt.Select("FRESTATUS in('2','3','4')")
                        row.Delete()
                        If bRemoveStatus = True Then bRemoveStatus = False
                    Next
                    'For Each rowH As DataRow In dtGroupZoneHead.Select("FRESTATUS in('2','3','4')")
                    '    rowH.Delete()
                    'Next

                    drCheckZone = dt.Select(ColZone & " =''")
                    If drCheckZone.Length > 0 Then
                        For Each row As DataRow In dt.Select(ColZone & " =''")
                            row.Delete()
                            If bRemoveZone = True Then bRemoveZone = False
                        Next
                        For Each rowH As DataRow In dtGroupZoneHead.Select(ColZone & " =''")
                            rowH.Delete()
                        Next

                        Session.Remove("dtGroupZoneHead")
                        Session.Add("dtGroupZoneHead", dtGroupZoneHead)
                        Session.Remove("dtUploadPriceListImport")
                        Session.Add("dtUploadPriceListImport", dt)
                        Call LoadZone(rdblZone, dtGroupZoneHead)

                        If dt.Rows.Count > 0 Then

                            btnImport.Attributes.Remove("disabled")
                            grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"
                            grdImport.Columns(16).Visible = True

                            If bRemoveZone = False Then
                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "zoneHideShow('" + rdblZone.SelectedValue + "');ConfirmVerify3();", True)
                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "zoneHideShow('" + rdblZone.SelectedValue + "');OpenDialogSuccess('" & hddMSGVerify.Value & "');", True)
                            End If
                        Else
                            btnImport.Attributes.Add("disabled", "")
                            grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"
                            grdImport.DataSource = Nothing
                            grdImport.DataBind()

                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "ConfirmVerify4();", True) '
                        End If


                    End If

                    If bRemoveZone = True Then
                        If bRemoveStatus = True Then

                            btnImport.Attributes.Remove("disabled")
                            grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"
                            grdImport.Columns(16).Visible = True
                            Call LoadZone(rdblZone, dtGroupZoneHead)
                            ' "Verify Data Success."

                            If hddChkVerifyModal.Value = "Y" Then
                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "zoneHideShow('" + rdblZone.SelectedValue + "');ConfirmVerify();", True)
                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "zoneHideShow('" + rdblZone.SelectedValue + "');OpenDialogSuccess('" & hddMSGVerify.Value & "');", True)
                            End If


                        Else
                            Session.Remove("dtGroupZoneHead")
                            Session.Add("dtGroupZoneHead", dtGroupZoneHead)
                            Session.Remove("dtUploadPriceListImport")
                            Session.Add("dtUploadPriceListImport", dt)
                            Call LoadZone(rdblZone, dtGroupZoneHead)
                            If dt.Rows.Count > 0 Then
                                btnImport.Attributes.Remove("disabled")
                                grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"
                                grdImport.DataSource = dt
                                grdImport.DataBind()
                                grdImport.Columns(16).Visible = True
                                ' "Verify Data Success."
                                If hddChkVerifyModal.Value = "Y" Then
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "zoneHideShow('" + rdblZone.SelectedValue + "');ConfirmVerify();", True)
                                Else
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "zoneHideShow('" + rdblZone.SelectedValue + "');OpenDialogSuccess('" & hddMSGVerify.Value & "');", True)
                                End If
                            Else
                                btnImport.Attributes.Add("disabled", "")
                                grdImport.CssClass = "table table-hover table-striped table-bordered table-responsive"
                                grdImport.DataSource = Nothing
                                grdImport.DataBind()

                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "ConfirmVerify2();", True) '
                            End If
                        End If
                    End If

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
                    'Call RegisterSetTabScript(TabNameEnum.IM1)
                    'Exit Sub

                    grdImport.Columns(16).Visible = False
                End If
            Else
                '"Please select a file to import."
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & hddMSGPleaseImport.Value & "');", True)
                'Call RegisterSetTabScript(TabNameEnum.IM1)
                'Exit Sub
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & ex.Message.ToString() & "');", True)
        End Try
    End Sub

    Public Sub LoadZone(ByVal rdbl As RadioButtonList, ByVal dtZone As DataTable)
        Dim _Zone As String = ""
        rdbl.Items.Clear()
        For i As Integer = 0 To dtZone.Rows.Count - 1
            If _Zone = "" Then _Zone = dtZone.Rows(i)("Zone").ToString
            rdbl.Items.Insert(i, New ListItem("Price No. " & i + 1, dtZone.Rows(i)("Zone").ToString))
        Next
        If _Zone <> String.Empty Then
            rdbl.SelectedIndex = 0
        End If
    End Sub

    Protected Sub btnApproveData_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApproveData.Click
        Try
            txtsPublicUtility.Text = "0" 'กี่เดือน
            txtsPublicUtilityPerMonth.Text = "0.00" 'กี่บาทต่อเดือน
            txtsDownPayment.Text = String.Empty
            Session.Remove("dtUploadPriceListImport")
            Session.Remove("dtGroupZoneHead")
            btnImport.Attributes.Add("disabled", "")
            rdblZone.Items.Clear()
            grdImport.DataSource = Nothing
            grdImport.DataBind()

            'chbItemNoApprove.Checked = True
            'If lblUserNotApprove.Text = "" Then
            '    btnApprove2.Attributes.Remove("disabled")
            'Else
            '    btnApprove2.Attributes.Add("disabled", "")
            'End If

            'txtsProject2.Text = ddlsProject.SelectedValue
            'txtsPhase2.Text = ddlsPhase.SelectedValue
            'Call LoadDataTab2()
            ''Call RegisterSetTabScript(TabNameEnum.IM2)

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnApproveData_Click", ex)
        End Try
    End Sub

    Protected Sub btnImport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImport.Click
        Try
            Dim strMassage As String = String.Empty
            If ddlsProject.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value) & "');", True)
                'Call RegisterSetTabScript(TabNameEnum.IM1)
                Exit Sub
            ElseIf ddlsPhase.SelectedValue = String.Empty Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & Me.GetResource("msg_please_select", "MSG") & " " & Me.GetResource("lblsPhase", "Text", hddParameterMenuID.Value) & "');", True)
                'Call RegisterSetTabScript(TabNameEnum.IM1)
                Exit Sub
            ElseIf txtsDownPayment.Text = String.Empty OrElse txtsDownPayment.Text = "0.00" OrElse txtsDownPayment.Text = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "ConfirmImportPriceList();", True)
                ''Call RegisterSetTabScript(TabNameEnum.IM1)
                Exit Sub
            End If

            Dim dtFileUploadSTPrice As New DataTable
            If Session("dtUploadPriceListImport") IsNot Nothing Then
                dtFileUploadSTPrice = Session("dtUploadPriceListImport")
            End If

            Dim dtGroupZoneHead As New DataTable
            If Session("dtGroupZoneHead") IsNot Nothing Then
                dtGroupZoneHead = Session("dtGroupZoneHead")
            End If

            Call changeTypeDataGrid(dtFileUploadSTPrice, grdImport)

            Dim succ As Boolean = True
            Dim bl As cImportPriceList = New cImportPriceList


            If dtFileUploadSTPrice IsNot Nothing Then
                If dtFileUploadSTPrice.Rows.Count > 0 Then
                    If Not bl.AddUploadAll(dtFileUploadSTPrice,
                                           dtGroupZoneHead,
                                           txtsPublicUtility.Text,
                                           txtsPublicUtilityPerMonth.Text,
                                           txtsDownPayment.Text,
                                           ddlsPhase.SelectedValue,
                                           Me.CurrentUser.UserID) Then

                        '    'ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & "Upload data fail." & "');", True)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & hddMSGImportDataNo.Value & "');", True)
                    Else
                        txtsPublicUtility.Text = "0" 'กี่เดือน
                        txtsPublicUtilityPerMonth.Text = "0.00" 'กี่บาทต่อเดือน
                        txtsDownPayment.Text = String.Empty
                        Session.Remove("dtUploadPriceListImport")
                        'Session.Remove("dtGroupZoneHead")
                        rdblZone.Items.Clear()
                        For i As Integer = 0 To dtGroupZoneHead.Rows.Count - 1
                            rdblZone.Items.Insert(i, New ListItem(dtGroupZoneHead.Rows(i)("FtrnNO").ToString, dtGroupZoneHead.Rows(i)("Zone").ToString))
                        Next
                        rdblZone.SelectedIndex = 0
                        btnImport.Attributes.Add("disabled", "")
                        Session.Remove("ADI_ImportPriceList_ddlsProject")
                        'rdblZone.Items.Clear()
                        'grdImport.DataSource = Nothing
                        'grdImport.DataBind()

                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "zoneHideShow('" + rdblZone.SelectedValue + "');OpenDialogSuccess('" & hddMSGImportData.Value & "');", True)
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & "Upload Data Success." & "');", True)
                    End If
                Else
                    strMassage = "Please select a file to import."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & strMassage & "');", True)
                    'Call RegisterSetTabScript(TabNameEnum.IM1)
                End If
            Else
                strMassage = "Please select a file to import."
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "select_upload", "OpenDialogError('" & strMassage & "');", True)
                'Call RegisterSetTabScript(TabNameEnum.IM1)
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
        dt.Columns.Add(ColProjectCode)
        dt.Columns.Add(ColZone)
        dt.Columns.Add(ColType)
        dt.Columns.Add(ColUnitNo)
        dt.Columns.Add(ColBookingAmount)
        dt.Columns.Add(ColContractAmount)
        dt.Columns.Add(ColPeriodDown)
        dt.Columns.Add(ColLandPrice)
        dt.Columns.Add(ColCostPrice)
        dt.Columns.Add(ColGPTarget)
        dt.Columns.Add(ColStandardPrice)
        dt.Columns.Add(ColLocationPrice)
        dt.Columns.Add(ColTargetPrice)
        dt.Columns.Add(ColActualPrice)
        dt.Columns.Add(ColPromotionPrice)
        dt.Columns.Add(ColPriceList)
        dt.Columns.Add(ColRemark)
        dt.Columns.Add(ColFtrnNO)
        Return dt
    End Function

    Private Function createHeaderGroupZone() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add(ColZone)
        Return dt
    End Function

    Public Sub AddDatatableFilePhaseChk(ByRef dt As DataTable, ByVal lst As DataTable)
        Try
            Dim dr As DataRow
            For i As Integer = 0 To lst.Rows.Count - 1
                dr = dt.NewRow
                dr.Item("FREZONE") = lst.Rows(i)("FREZONE").ToString
                dr.Item("FSERIALNO") = lst.Rows(i)("FSERIALNO").ToString
                dr.Item("FPDCODE") = lst.Rows(i)("FPDCODE").ToString
                dr.Item("FREPRJNO") = lst.Rows(i)("FREPRJNO").ToString
                dr.Item("FREPHASE") = lst.Rows(i)("FREPHASE").ToString
                dr.Item("FRESTATUS") = lst.Rows(i)("FRESTATUS").ToString
                dt.Rows.Add(dr)
            Next

        Catch ex As Exception
            'HelperLog.ErrorLog(CurrentUser.USERID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub

    Private Sub createHeaderNospace2(ByRef dt As DataTable)
        dt.Columns.Add(ColProjectCode)
        dt.Columns.Add(ColZone)
        dt.Columns.Add(ColType)
        dt.Columns.Add(ColUnitNo)
        dt.Columns.Add(ColBookingAmount)
        dt.Columns.Add(ColContractAmount)
        dt.Columns.Add(ColPeriodDown)
        dt.Columns.Add(ColLandPrice)
        dt.Columns.Add(ColCostPrice)
        dt.Columns.Add(ColGPTarget)
        dt.Columns.Add(ColStandardPrice)
        dt.Columns.Add(ColLocationPrice)
        dt.Columns.Add(ColTargetPrice)
        dt.Columns.Add(ColActualPrice)
        dt.Columns.Add(ColPromotionPrice)
        dt.Columns.Add(ColPriceList)
        dt.Columns.Add(ColRemark)
    End Sub

    Private Sub changeTypeData(ByVal dt As DataTable,
                               ByRef vDS As DataTable)
        Try
            Dim dtcopy As New DataTable
            Dim dr As DataRow
            dtcopy = vDS

            Dim strChkStatus As String = "chk_status"

            For i As Integer = 0 To dtcopy.Rows.Count - 1
                dr = dt.NewRow()

                dr.Item(ColProjectCode) = dtcopy.Rows(i)(ColProjectCode).ToString
                dr.Item(ColZone) = dtcopy.Rows(i)(ColZone).ToString
                dr.Item(ColType) = dtcopy.Rows(i)(ColType).ToString
                dr.Item(ColUnitNo) = dtcopy.Rows(i)(ColUnitNo).ToString
                dr.Item(ColBookingAmount) = dtcopy.Rows(i)(ColBookingAmount).ToString
                dr.Item(ColContractAmount) = dtcopy.Rows(i)(ColContractAmount).ToString
                dr.Item(ColPeriodDown) = dtcopy.Rows(i)(ColPeriodDown).ToString
                dr.Item(ColLandPrice) = dtcopy.Rows(i)(ColLandPrice).ToString
                dr.Item(ColCostPrice) = dtcopy.Rows(i)(ColCostPrice).ToString
                dr.Item(ColGPTarget) = dtcopy.Rows(i)(ColGPTarget).ToString
                dr.Item(ColStandardPrice) = dtcopy.Rows(i)(ColStandardPrice).ToString
                dr.Item(ColLocationPrice) = dtcopy.Rows(i)(ColLocationPrice).ToString
                dr.Item(ColTargetPrice) = dtcopy.Rows(i)(ColTargetPrice).ToString
                dr.Item(ColActualPrice) = dtcopy.Rows(i)(ColActualPrice).ToString
                dr.Item(ColPromotionPrice) = dtcopy.Rows(i)(ColPromotionPrice).ToString
                dr.Item(ColPriceList) = dtcopy.Rows(i)(ColPriceList).ToString
                dr.Item(ColRemark) = dtcopy.Rows(i)(ColRemark).ToString
                dr.Item(strChkStatus.Replace(" ", "")) = dtcopy.Rows(i)(strChkStatus).ToString

                dt.Rows.Add(dr)
            Next
            vDS = New DataTable
            vDS = dt
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "changeTypeData", ex)
        End Try
    End Sub

    Private Sub changeTypeDataGrid(ByRef dt As DataTable,
                              ByVal grdImport As GridView)
        Try
            For Each row As GridViewRow In grdImport.Rows
                Dim hddUnitNo As HiddenField = TryCast(row.Cells(0).Controls(0).FindControl("hddUnitNo"), HiddenField)
                Dim txtRemark As TextBox = TryCast(row.Cells(0).Controls(0).FindControl("txtRemark"), TextBox)

                ' Do something with the textBox's value
                Dim valueUnitNo As String = hddUnitNo.Value
                Dim valueRemark As String = txtRemark.Text
                If valueRemark <> String.Empty Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)(ColUnitNo).ToString = valueUnitNo Then
                            dt.Rows(i)(ColRemark) = valueRemark
                            Exit For
                        End If
                    Next
                End If
            Next

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "changeTypeData", ex)
        End Try
    End Sub


#End Region

#End Region


End Class
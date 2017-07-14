Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class ORG_Project
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
                Session.Remove("IDOCS.application.LoaddataProect")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("ORG_Project.aspx")
                Me.ClearSessionPageLoad("ORG_Project.aspx")

                Dim strLG As String = String.Empty
                If Session("PRR.application.language") IsNot Nothing Then
                    strLG = Session("PRR.application.language")
                End If
                hddMasterLG.Value = strLG

                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()
                Call Loaddata()
                Call DeleteAllFileTemp()
                Session.Remove("ORD_Project_DataTable_Document")

                If Request.QueryString("flagedit") IsNot Nothing Then
                    If Request.QueryString("flagedit").ToString <> String.Empty Then
                        hddKeyID.Value = Request.QueryString("flagedit").ToString
                        Call EditData()
                    End If
                Else
                    Call GetParameter()
                    Call Loaddata()
                End If

                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();checkNullShowAdd();", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub

#Region "Dropdownlist"
    Public Sub LoadDropdownlist()
        Call LoadProjectType(ddlProjectType)
        Call LoadConstructionStatus(ddlConstructionStatus)
        Call LoadSaleStatus(ddlSaleStatus)
    End Sub
    Public Sub LoadProjectType(ByVal ddl As DropDownList)

        Dim bl As cProject = New cProject
        Try

            ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))

            Dim lc As List(Of SD03TYPE) = bl.LoadProjectType()
            Dim _Code As String = ""
            Dim _Name As String = ""
            For i As Integer = 0 To lc.Count - 1
                _Code = lc(i).FTYCODE.ToString
                _Name = lc(i).FDESC.ToString

                If _Code.Length > 3 Then
                    _Code = Right(_Code, 3)
                End If

                ddl.Items.Insert(i + 1, New ListItem(String.Format("{0} - {1}", _Code, _Name), _Code))
            Next

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "LoadProjectType", ex)
        End Try
    End Sub

    Public Sub LoadConstructionStatus(ByVal ddl As DropDownList)
        Try
            'ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            'ddl.Items.Insert(1, New ListItem(grtt("resConstructionNo"), "0")) '+ ไม่กรอกวันที่เริ่มก่อสร้างและวันที่ก่อสร้างเสร็จ 0=ยังไม่เริ่มก่อสร้าง
            'ddl.Items.Insert(2, New ListItem(grtt("resConstructionStart"), "1")) '+ กรอกวันที่เริ่มก่อสร้าง 1=กำลังดำเนินการก่อสร้าง
            'ddl.Items.Insert(3, New ListItem(grtt("resConstructionEnd"), "2")) '+ กรอกวันที่ก่อสร้างเสร็จ 2=ก่อสร้างเสร็จแล้ว

            ddl.Items.Insert(0, New ListItem(grtt("resConstructionNo"), "0")) '+ ไม่กรอกวันที่เริ่มก่อสร้างและวันที่ก่อสร้างเสร็จ 0=ยังไม่เริ่มก่อสร้าง
            ddl.Items.Insert(1, New ListItem(grtt("resConstructionStart"), "1")) '+ กรอกวันที่เริ่มก่อสร้าง 1=กำลังดำเนินการก่อสร้าง
            ddl.Items.Insert(2, New ListItem(grtt("resConstructionEnd"), "2")) '+ กรอกวันที่ก่อสร้างเสร็จ 2=ก่อสร้างเสร็จแล้ว
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "LoadConstructionStatus", ex)
        End Try
    End Sub
    Public Sub LoadSaleStatus(ByVal ddl As DropDownList)
        Try
            'ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            'ddl.Items.Insert(1, New ListItem(grtt("resSaleNo"), "0")) '+ ไม่กรอกวันที่เริ่มการขายและวันที่การขายเสร็จ 0=ยังไม่เริ่มมีการขาย
            'ddl.Items.Insert(2, New ListItem(grtt("resSaleStart"), "1")) '+ กรอกวันที่เริ่มการขาย 1=กำลังดำเนินการขาย
            'ddl.Items.Insert(3, New ListItem(grtt("resSaleEnd"), "2")) '+ กรอกวันที่การขายเสร็จ 2=การขายเสร็จสิ้นแล้วแล้ว

            ddl.Items.Insert(0, New ListItem(grtt("resSaleNo"), "0")) '+ ไม่กรอกวันที่เริ่มการขายและวันที่การขายเสร็จ 0=ยังไม่เริ่มมีการขาย
            ddl.Items.Insert(1, New ListItem(grtt("resSaleStart"), "1")) '+ กรอกวันที่เริ่มการขาย 1=กำลังดำเนินการขาย
            ddl.Items.Insert(2, New ListItem(grtt("resSaleEnd"), "2")) '+ กรอกวันที่การขายเสร็จ 2=การขายเสร็จสิ้นแล้วแล้ว
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "LoadSaleStatus", ex)
        End Try
    End Sub

#End Region


#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        Call Redirect("ORG_Project.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("ORG_Project_FS")
        Session.Add("ORG_Project_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("ORG_Project_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("ORG_Project_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("ORG_Project_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("ORG_Project_FS") IsNot Nothing Then
            If Session("ORG_Project_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("ORG_Project_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
            End If
        End If
        If Session("ORG_Project_PageInfo") IsNot Nothing Then
            If Session("ORG_Project_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("ORG_Project_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("ORG_Project_Search") IsNot Nothing Then
            If Session("ORG_Project_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("ORG_Project_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("ORG_Project_pageLength") IsNot Nothing Then
            If Session("ORG_Project_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("ORG_Project_pageLength").ToString
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

        lblFreprjno.Text = Me.GetResource("lblFreprjno", "Text", hddParameterMenuID.Value)
        lblFRePrjNm.Text = Me.GetResource("lblFRePrjNm", "Text", hddParameterMenuID.Value)
        chbFboiyn.Text = Me.GetResource("chbFboiyn", "Text", hddParameterMenuID.Value)

        lblFrelocat1.Text = Me.GetResource("lblFrelocat1", "Text", hddParameterMenuID.Value)
        lblFrelocat3.Text = Me.GetResource("lblFrelocat3", "Text", hddParameterMenuID.Value)
        lblFreprovinc.Text = Me.GetResource("lblFreprovinc", "Text", hddParameterMenuID.Value)
        lblFtotarea.Text = Me.GetResource("lblFtotarea", "Text", hddParameterMenuID.Value)
        lblFnoofland.Text = Me.GetResource("lblFnoofland", "Text", hddParameterMenuID.Value)
        'lblFlandno.Text = Me.GetResource("lblFlandno", "Text", hddParameterMenuID.Value)

        lblFredesc1.Text = Me.GetResource("lblFredesc1", "Text", hddParameterMenuID.Value)
        chbFboiyn.Text = "&nbsp;" & Me.GetResource("chbFboiyn", "Text", hddParameterMenuID.Value)
        'lblFlandMudNo.Text = Me.GetResource("lblFlandMudNo", "Text", hddParameterMenuID.Value)
        lblCodeMat.Text = Me.GetResource("lblCodeMat", "Text", hddParameterMenuID.Value)
        lblFlanddoc.Text = Me.GetResource("lblFlanddoc", "Text", hddParameterMenuID.Value)
        lblDocument.Text = Me.GetResource("lblFlanddoc", "Text", hddParameterMenuID.Value)

        'lblLicenses.Text = Me.GetResource("lblLicenses", "Text", hddParameterMenuID.Value)
        lblNo.Text = Me.GetResource("lblNo", "Text", hddParameterMenuID.Value)
        lblDate.Text = Me.GetResource("lblDate", "Text", hddParameterMenuID.Value)
        lblEDate.Text = Me.GetResource("lblEDate", "Text", hddParameterMenuID.Value)
        lblLandAllocation.Text = Me.GetResource("lblLandAllocation", "Text", hddParameterMenuID.Value)
        lblConstructionPorject.Text = Me.GetResource("lblConstructionPorject", "Text", hddParameterMenuID.Value)
        lblBuildingHomes.Text = Me.GetResource("lblBuildingHomes", "Text", hddParameterMenuID.Value)
        lblCityPlan.Text = Me.GetResource("lblCityPlan", "Text", hddParameterMenuID.Value)
        lblCommercialLand.Text = Me.GetResource("lblCommercialLand", "Text", hddParameterMenuID.Value)
        lblGarbage.Text = Me.GetResource("lblGarbage", "Text", hddParameterMenuID.Value)
        lblContacts.Text = Me.GetResource("lblContacts", "Text", hddParameterMenuID.Value)
        lblDrainage.Text = Me.GetResource("lblDrainage", "Text", hddParameterMenuID.Value)
        lblExpandElectricity.Text = Me.GetResource("lblExpandElectricity", "Text", hddParameterMenuID.Value)
        lblPlumbingElectricity.Text = Me.GetResource("lblPlumbingElectricity", "Text", hddParameterMenuID.Value)
        lblScienceHill.Text = Me.GetResource("lblScienceHill", "Text", hddParameterMenuID.Value)
        lblBillboards.Text = Me.GetResource("lblBillboards", "Text", hddParameterMenuID.Value)
        lblFilling.Text = Me.GetResource("lblFilling", "Text", hddParameterMenuID.Value)
        lblaFLADNO.Text = Me.GetResource("lblaFLADNO", "Text", hddParameterMenuID.Value)
        lblaFPCLANDNO.Text = Me.GetResource("lblaFPCLANDNO", "Text", hddParameterMenuID.Value)

        lblSubChanod.Text = Me.GetResource("lblSubChanod", "Text", hddParameterMenuID.Value)

        TextHd1.Text = Me.GetResource("No", "Text", "1")
        TextHd2.Text = Me.GetResource("ProjectCode", "Text", hddParameterMenuID.Value)
        TextHd3.Text = Me.GetResource("ProjectName", "Text", hddParameterMenuID.Value)
        TextHd4.Text = Me.GetResource("col_edit", "Text", "1")
        TextHd5.Text = Me.GetResource("col_delete", "Text", "1")

        TextFt1.Text = Me.GetResource("No", "Text", "1")
        TextFt2.Text = Me.GetResource("ProjectCode", "Text", hddParameterMenuID.Value)
        TextFt3.Text = Me.GetResource("ProjectName", "Text", hddParameterMenuID.Value)
        TextFt4.Text = Me.GetResource("col_edit", "Text", "1")
        TextFt5.Text = Me.GetResource("col_delete", "Text", "1")

        'lblsSave.Text = Me.GetResource("btn_save", "Text", "1")
        'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

        lblMassage1.Text = Me.GetResource("msg_required", "MSG")
        lblMassage2.Text = Me.GetResource("msg_required", "MSG")
        lblMassage3.Text = Me.GetResource("msg_required", "MSG")
        lblMassage4.Text = Me.GetResource("msg_required", "MSG")
        lblMassage5.Text = Me.GetResource("msg_required", "MSG")

        lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG")
        lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG") & " " & Me.GetResource("lblFreprjno", "Text", hddParameterMenuID.Value)

        hddMSGAddFile.Value = Me.GetResource("msg_add_file", "MSG")
        hddMSGDeleteFile.Value = Me.GetResource("msg_delete_file", "MSG")
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
            Dim bl As cProject = New cProject
            fillter.Keyword = ""
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType

            Dim lst As List(Of vw_Project_join) = bl.Loaddata(fillter,
                                                              Me.TotalRow,
                                                              Me.CurrentUser.UserID)
            If lst IsNot Nothing Then
                Call SetDataProject(lst)
            Else
                Call SetDataProject(Nothing)
            End If

            Session.Remove("ORG_Project_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)

            Session.Remove("ORG_Project_FS")
        End Try
    End Sub
    Public Sub clearText()
        txtFreprjno.Text = String.Empty
        chbFboiyn.Checked = False
        txtFRePrjNm.Text = String.Empty
        txtFrelocat1.Text = String.Empty
        txtFrelocat2.Text = String.Empty
        'txtFprovcdFcitycd.Text = String.Empty
        txtFrelocat3.Text = String.Empty
        txtFreprovinc.Text = String.Empty
        txtFrepostal.Text = String.Empty
        txtFtotarea.Text = String.Empty
        txtFnoofland.Text = String.Empty
        'txtFlandno.Text = String.Empty
        'txtFlandMudNo.Text = String.Empty
        txtCodeMat.Text = String.Empty
        txtFredesc1.Text = String.Empty
        txtFredesc2.Text = String.Empty
        txtFredesc3.Text = String.Empty
        txtaFLADNO.Text = String.Empty
        txtaFPCLANDNO.Text = String.Empty
        txt2.Text = String.Empty
        txtDate0.Text = String.Empty
        txtDate13.Text = String.Empty
        txt11.Text = String.Empty
        txtDate1.Text = String.Empty
        txtDate14.Text = String.Empty
        txt12.Text = String.Empty
        txtDate2.Text = String.Empty
        txtDate15.Text = String.Empty
        txt13.Text = String.Empty
        txtDate3.Text = String.Empty
        txtDate16.Text = String.Empty
        txt14.Text = String.Empty
        txtDate4.Text = String.Empty
        txtDate17.Text = String.Empty
        txt15.Text = String.Empty
        txtDate5.Text = String.Empty
        txtDate18.Text = String.Empty
        txt16.Text = String.Empty
        txtDate6.Text = String.Empty
        txtDate19.Text = String.Empty
        txt17.Text = String.Empty
        txtDate7.Text = String.Empty
        txtDate20.Text = String.Empty
        txt18.Text = String.Empty
        txtDate8.Text = String.Empty
        txtDate21.Text = String.Empty
        txt19.Text = String.Empty
        txtDate9.Text = String.Empty
        txtDate22.Text = String.Empty
        txt20.Text = String.Empty
        txtDate10.Text = String.Empty
        txtDate23.Text = String.Empty
        txt21.Text = String.Empty
        txtDate11.Text = String.Empty
        txtDate24.Text = String.Empty
        txt22.Text = String.Empty
        txtDate12.Text = String.Empty
        txtDate25.Text = String.Empty

        hddFlanddoc.Value = String.Empty
        tdFlandoc.Visible = False
        tdFlandocUpload.Visible = False
        '1
        hddFREQDOC.Value = String.Empty
        tdFREQDOCAdd.Style.Add("display", "none")
        tdFREQDOC.Visible = False
        tdFREQDOCUpload.Visible = False
        '2
        hddFCONSTRDOC.Value = String.Empty
        tdFCONSTRDOCAdd.Style.Add("display", "none")
        tdFCONSTRDOC.Visible = False
        tdFCONSTRDOCUpload.Visible = False
        '3
        hddFCONSTRDOC1.Value = String.Empty
        tdFCONSTRDOC1Add.Style.Add("display", "none")
        tdFCONSTRDOC1.Visible = False
        tdFCONSTRDOC1Upload.Visible = False
        '4
        hddFREQDOC1.Value = String.Empty
        tdFREQDOC1Add.Style.Add("display", "none")
        tdFREQDOC1.Visible = False
        tdFREQDOC1Upload.Visible = False
        '5
        hddFREQDOC2.Value = String.Empty
        tdFREQDOC2Add.Style.Add("display", "none")
        tdFREQDOC2.Visible = False
        tdFREQDOC2Upload.Visible = False
        '6
        hddFREQDOC3.Value = String.Empty
        tdFREQDOC3Add.Style.Add("display", "none")
        tdFREQDOC3.Visible = False
        tdFREQDOC3Upload.Visible = False
        '7
        hddFREQDOC4.Value = String.Empty
        tdFREQDOC4Add.Style.Add("display", "none")
        tdFREQDOC4.Visible = False
        tdFREQDOC4Upload.Visible = False
        '8
        hddFREQDOC5.Value = String.Empty
        tdFREQDOC5Add.Style.Add("display", "none")
        tdFREQDOC5.Visible = False
        tdFREQDOC5Upload.Visible = False
        '9
        hddFREQDOC6.Value = String.Empty
        tdFREQDOC6Add.Style.Add("display", "none")
        tdFREQDOC6.Visible = False
        tdFREQDOC6Upload.Visible = False
        '10
        hddFREQDOC7.Value = String.Empty
        tdFREQDOC7Add.Style.Add("display", "none")
        tdFREQDOC7.Visible = False
        tdFREQDOC7Upload.Visible = False
        '11
        hddFREQDOC8.Value = String.Empty
        tdFREQDOC8Add.Style.Add("display", "none")
        tdFREQDOC8.Visible = False
        tdFREQDOC8Upload.Visible = False
        '12
        hddFREQDOC9.Value = String.Empty
        tdFREQDOC9Add.Style.Add("display", "none")
        tdFREQDOC9.Visible = False
        tdFREQDOC9Upload.Visible = False
        '13
        hddFREQDOC10.Value = String.Empty
        tdFREQDOC10Add.Style.Add("display", "none")
        tdFREQDOC10.Visible = False
        tdFREQDOC10Upload.Visible = False

        txtFtotarea.Enabled = False
        txtFnoofland.Enabled = False
        txtaFLADNO.Enabled = False
        txtaFPCLANDNO.Enabled = False


        ddlProjectType.SelectedIndex = 0
        txtProjectBrand.Text = String.Empty
        txtProjectStartDate.Text = String.Empty
        txtProjectEndDate.Text = String.Empty

        ddlConstructionStatus.SelectedIndex = 0
        txtConstructionStartDate.Text = String.Empty
        txtConstructionEndDate.Text = String.Empty

        ddlSaleStatus.SelectedIndex = 0
        txtSaleStartDate.Text = String.Empty
        txtSaleEndDate.Text = String.Empty

    End Sub
    Public Sub SetCiterail(ByVal pValue As String,
                           ByRef pBValue As String)
        Try
            If pValue <> String.Empty Then
                pBValue = pValue.Split(" - ")(0)
            End If
        Catch ex As Exception
            pBValue = String.Empty
        End Try
    End Sub
#End Region

#Region "Function"
    Public Sub EditData()
        Session.Remove("ORD_Project_DataTable_Document")
        Dim dt As New DataTable
        Dim dts As New DataTable
        Call clearText()
        Dim bl As cProject = New cProject
        Dim lc As ED01PROJ = bl.GetED01PROJByID(hddKeyID.Value,
                                                Me.CurrentUser.UserID)
        If lc IsNot Nothing Then

            txtFreprjno.Text = hddKeyID.Value
            chbFboiyn.Checked = IIf(lc.FBOIYN = "Y", True, False)
            txtFRePrjNm.Text = lc.FREPRJNM
            txtFrelocat1.Text = lc.FRELOCAT1
            txtFrelocat2.Text = lc.FRELOCAT2
            txtFrelocat3.Text = lc.FRELOCAT3
            txtFreprovinc.Text = lc.FREPROVINC
            txtFrepostal.Text = lc.FREPOSTAL

            txtaFLADNO.Text = lc.FLANDNO
            txtaFPCLANDNO.Text = lc.FPCLANDNO
            Try
                Dim lcCodemat As SD02GODN = bl.LoadCodeMatEdit(lc.FGDCODE)
                If lcCodemat IsNot Nothing Then
                    If lcCodemat.FGDCODE <> String.Empty Then
                        txtCodeMat.Text = lcCodemat.FGDCODE & " - " & lcCodemat.FDESC
                    End If
                End If

            Catch ex As Exception
                txtCodeMat.Text = String.Empty
            End Try

            txtFredesc1.Text = lc.FREDESC1
            txtFredesc2.Text = lc.FREDESC2
            txtFredesc3.Text = lc.FREDESC3
            '1
            txt2.Text = lc.FREQNO
            If Not IsDBNull(lc.FREQDATE) Then
                If lc.FREQDATE IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDATE
                    txtDate0.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDATE) Then
                If lc.FREQEDATE IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDATE
                    txtDate13.Text = ToStringByCulture(TempDate)
                End If
            End If
            '2
            txt11.Text = lc.FCONSTRNO
            If Not IsDBNull(lc.FCONSTRDT) Then
                If lc.FCONSTRDT IsNot Nothing Then
                    Dim TempDate As String = lc.FCONSTRDT
                    txtDate1.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FCONSTREDT) Then
                If lc.FCONSTREDT IsNot Nothing Then
                    Dim TempDate As String = lc.FCONSTREDT
                    txtDate14.Text = ToStringByCulture(TempDate)
                End If
            End If
            '3
            txt12.Text = lc.FCONSTRNO2
            If Not IsDBNull(lc.FCONSTRDT2) Then
                If lc.FCONSTRDT2 IsNot Nothing Then
                    Dim TempDate As String = lc.FCONSTRDT2
                    txtDate2.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FCONSTREDT2) Then
                If lc.FCONSTREDT2 IsNot Nothing Then
                    Dim TempDate As String = lc.FCONSTREDT2
                    txtDate15.Text = ToStringByCulture(TempDate)
                End If
            End If
            '4
            txt13.Text = lc.FREQNO1
            If Not IsDBNull(lc.FREQDT1) Then
                If lc.FREQDT1 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDT1
                    txtDate3.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDT1) Then
                If lc.FREQEDT1 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDT1
                    txtDate16.Text = ToStringByCulture(TempDate)
                End If
            End If
            '5
            txt14.Text = lc.FREQNO2
            If Not IsDBNull(lc.FREQDT2) Then
                If lc.FREQDT2 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDT2
                    txtDate4.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDT2) Then
                If lc.FREQEDT2 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDT2
                    txtDate17.Text = ToStringByCulture(TempDate)
                End If
            End If
            '6
            txt15.Text = lc.FREQNO3
            If Not IsDBNull(lc.FREQDT3) Then
                If lc.FREQDT3 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDT3
                    txtDate5.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDT3) Then
                If lc.FREQEDT3 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDT3
                    txtDate18.Text = ToStringByCulture(TempDate)
                End If
            End If
            '7
            txt16.Text = lc.FREQNO4
            If Not IsDBNull(lc.FREQDT4) Then
                If lc.FREQDT4 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDT4
                    txtDate6.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDT4) Then
                If lc.FREQEDT4 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDT4
                    txtDate19.Text = ToStringByCulture(TempDate)
                End If
            End If
            '8
            txt17.Text = lc.FREQNO5
            If Not IsDBNull(lc.FREQDT5) Then
                If lc.FREQDT5 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDT5
                    txtDate7.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDT5) Then
                If lc.FREQEDT5 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDT5
                    txtDate20.Text = ToStringByCulture(TempDate)
                End If
            End If
            '9
            txt18.Text = lc.FREQNO6
            If Not IsDBNull(lc.FREQDT6) Then
                If lc.FREQDT6 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDT6
                    txtDate8.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDT6) Then
                If lc.FREQEDT6 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDT6
                    txtDate21.Text = ToStringByCulture(TempDate)
                End If
            End If
            '10
            txt19.Text = lc.FREQNO7
            If Not IsDBNull(lc.FREQDT7) Then
                If lc.FREQDT7 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDT7
                    txtDate9.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDT7) Then
                If lc.FREQEDT7 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDT7
                    txtDate22.Text = ToStringByCulture(TempDate)
                End If
            End If
            '11
            txt20.Text = lc.FREQNO8
            If Not IsDBNull(lc.FREQDT8) Then
                If lc.FREQDT8 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDT8
                    txtDate10.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDT8) Then
                If lc.FREQEDT8 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDT8
                    txtDate23.Text = ToStringByCulture(TempDate)
                End If
            End If
            '12
            txt21.Text = lc.FREQNO9
            If Not IsDBNull(lc.FREQDT9) Then
                If lc.FREQDT9 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDT9
                    txtDate11.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDT9) Then
                If lc.FREQEDT9 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDT9
                    txtDate24.Text = ToStringByCulture(TempDate)
                End If
            End If
            '13
            txt22.Text = lc.FREQNO10
            If Not IsDBNull(lc.FREQDT10) Then
                If lc.FREQDT10 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQDT10
                    txtDate12.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FREQEDT10) Then
                If lc.FREQEDT10 IsNot Nothing Then
                    Dim TempDate As String = lc.FREQEDT10
                    txtDate25.Text = ToStringByCulture(TempDate)
                End If
            End If

            If Not IsDBNull(lc.FREQDOC) Then
                If lc.FREQDOC <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC,
                                      "FREQ",
                                      "FREQDOC")
                End If
            End If
            If Not IsDBNull(lc.FCONSTRDOC) Then
                If lc.FCONSTRDOC <> String.Empty Then
                    Call AddDataTable(lc.FCONSTRDOC,
                                      "FREQ",
                                      "FCONSTRDOC")
                End If
            End If
            If Not IsDBNull(lc.FCONSTRDOC1) Then
                If lc.FCONSTRDOC1 <> String.Empty Then
                    Call AddDataTable(lc.FCONSTRDOC1,
                                      "FREQ",
                                      "FCONSTRDOC1")
                End If
            End If
            If Not IsDBNull(lc.FREQDOC1) Then
                If lc.FREQDOC1 <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC1,
                                      "FREQ",
                                      "FREQDOC1")
                End If
            End If
            If Not IsDBNull(lc.FREQDOC2) Then
                If lc.FREQDOC2 <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC2,
                                      "FREQ",
                                      "FREQDOC2")
                End If
            End If
            If Not IsDBNull(lc.FREQDOC3) Then
                If lc.FREQDOC3 <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC3,
                                      "FREQ",
                                      "FREQDOC3")
                End If
            End If
            If Not IsDBNull(lc.FREQDOC4) Then
                If lc.FREQDOC4 <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC4,
                                      "FREQ",
                                      "FREQDOC4")
                End If
            End If
            If Not IsDBNull(lc.FREQDOC5) Then
                If lc.FREQDOC5 <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC5,
                                      "FREQ",
                                      "FREQDOC5")
                End If
            End If
            If Not IsDBNull(lc.FREQDOC6) Then
                If lc.FREQDOC6 <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC6,
                                      "FREQ",
                                      "FREQDOC6")
                End If
            End If
            If Not IsDBNull(lc.FREQDOC7) Then
                If lc.FREQDOC7 <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC7,
                                      "FREQ",
                                      "FREQDOC7")
                End If
            End If
            If Not IsDBNull(lc.FREQDOC8) Then
                If lc.FREQDOC8 <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC8,
                                      "FREQ",
                                      "FREQDOC8")
                End If
            End If
            If Not IsDBNull(lc.FREQDOC9) Then
                If lc.FREQDOC9 <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC9,
                                      "FREQ",
                                      "FREQDOC9")
                End If
            End If
            If Not IsDBNull(lc.FREQDOC10) Then
                If lc.FREQDOC10 <> String.Empty Then
                    Call AddDataTable(lc.FREQDOC10,
                                      "FREQ",
                                      "FREQDOC10")
                End If
            End If
            If Not IsDBNull(lc.FLANDDOC) Then
                If lc.FLANDDOC <> String.Empty Then
                    Call AddDataTable(lc.FLANDDOC,
                                      "FLAND",
                                      "FLANDDOC")
                End If
            End If
            If Session("ORD_Project_DataTable_Document") IsNot Nothing Then
                dts = Session("ORD_Project_DataTable_Document")
            End If
            If dts IsNot Nothing Then
                If dts.Rows.Count > 0 Then
                    For i As Integer = 0 To dts.Rows.Count - 1
                        Call LoadLinkbutton(dts.Rows(i)("FileName").ToString,
                                            dts.Rows(i)("Type").ToString,
                                            dts.Rows(i)("Column").ToString)
                    Next

                    Call CopyProcessToTemp()
                End If
            End If

            Dim strFtotarea As String = String.Empty
            Dim lsts As List(Of FD11PROP) = bl.LoadED01PROJLAND(lc.FREPRJNO)
            If lsts IsNot Nothing Then
                If lsts.Count > 0 Then
                    Call CreateDatatable(dt)
                    Call SetDatatable(dt,
                                      lsts,
                                      strFtotarea)

                    If dt.Rows.Count > 0 Then
                        txtFnoofland.Text = String.Format("{0:N0}", dt.Rows.Count)
                        Dim strFLADNO As String = String.Empty
                        Dim strFPCLANDNO As String = String.Empty
                        For i As Integer = 0 To dt.Rows.Count - 1
                            If dt.Rows(i)("FLANDNO").ToString <> String.Empty Then
                                If strFLADNO = String.Empty Then
                                    strFLADNO = dt.Rows(i)("FLANDNO").ToString
                                Else
                                    strFLADNO = strFLADNO & "," & dt.Rows(i)("FLANDNO").ToString
                                End If
                            End If
                            If dt.Rows(i)("FPCLANDNO").ToString <> String.Empty Then
                                If strFPCLANDNO = String.Empty Then
                                    strFPCLANDNO = dt.Rows(i)("FPCLANDNO").ToString
                                Else
                                    strFPCLANDNO = strFPCLANDNO & "," & dt.Rows(i)("FPCLANDNO").ToString
                                End If
                            End If
                        Next
                        txtaFLADNO.Text = strFLADNO
                        txtaFPCLANDNO.Text = strFPCLANDNO
                    End If
                    If strFtotarea <> String.Empty Then
                        txtFtotarea.Text = String.Format("{0:N2}", CDec(strFtotarea))
                    End If

                    grdView2.DataSource = dt
                    grdView2.DataBind()
                End If
            End If


            Try
                ddlProjectType.SelectedValue = lc.FTYPECODE
            Catch ex As Exception
                ddlProjectType.SelectedIndex = 0
            End Try
            txtProjectBrand.Text = lc.FBRAND

            If Not IsDBNull(lc.FPLANDATEST) Then
                If lc.FPLANDATEST IsNot Nothing Then
                    Dim TempDate As String = lc.FPLANDATEST
                    txtProjectStartDate.Text = ToStringByCulture(TempDate)
                End If
            End If
            If Not IsDBNull(lc.FPLANDATEFN) Then
                If lc.FPLANDATEFN IsNot Nothing Then
                    Dim TempDate As String = lc.FPLANDATEFN
                    txtProjectEndDate.Text = ToStringByCulture(TempDate)
                End If
            End If

            If Not IsDBNull(lc.FCONSTATUS) Then
                If lc.FCONSTATUS IsNot Nothing Then
                    ddlConstructionStatus.SelectedValue = lc.FCONSTATUS.ToString
                Else
                    ddlConstructionStatus.SelectedIndex = 0
                End If
            Else
                ddlConstructionStatus.SelectedIndex = 0
            End If

            If Not IsDBNull(lc.FCONDATESTR) Then
                If lc.FCONDATESTR IsNot Nothing Then
                    Dim TempDate As String = lc.FCONDATESTR
                    txtConstructionStartDate.Text = ToStringByCulture(TempDate)
                End If
            End If

            If Not IsDBNull(lc.FCONDATEFIN) Then
                If lc.FCONDATEFIN IsNot Nothing Then
                    Dim TempDate As String = lc.FCONDATEFIN
                    txtConstructionEndDate.Text = ToStringByCulture(TempDate)
                End If
            End If

            If Not IsDBNull(lc.FSALESTATUS) Then
                If lc.FSALESTATUS IsNot Nothing Then
                    ddlSaleStatus.SelectedValue = lc.FSALESTATUS.ToString
                Else
                    ddlSaleStatus.SelectedIndex = 0
                End If
            Else
                ddlSaleStatus.SelectedIndex = 0
            End If

            If Not IsDBNull(lc.FSALEDATESTR) Then
                If lc.FSALEDATESTR IsNot Nothing Then
                    Dim TempDate As String = lc.FSALEDATESTR
                    txtSaleStartDate.Text = ToStringByCulture(TempDate)
                End If
            End If

            If Not IsDBNull(lc.FSALEDATEFIN) Then
                If lc.FSALEDATEFIN IsNot Nothing Then
                    Dim TempDate As String = lc.FSALEDATEFIN
                    txtSaleEndDate.Text = ToStringByCulture(TempDate)
                End If
            End If

            If lsts.Count = 0 Then
                Call CallLoadGridView()
            End If

            txtFreprjno.Enabled = False

            'txtFprovcdFcitycd.Enabled = False
            'txtFprovcdFcitycd.BackColor = System.Drawing.ColorTranslator.FromHtml("#F5F5F5")
        End If
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

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Dim bl As cProject = New cProject

            Session.Remove("ORD_Project_DataTable_Document")
            hddKeyID.Value = ""
            Call clearText()

            Call CallLoadGridView()

            txtFreprjno.Enabled = True

            'txtFprovcdFcitycd.Enabled = True
            'txtFprovcdFcitycd.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")

            Call OpenDialog()
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

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim bl As cProject = New cProject
            Dim data As ED01PROJ = bl.GetED01PROJByID(hddKeyID.Value,
                                                      Me.CurrentUser.UserID)
            If data IsNot Nothing Then
                If Not bl.Delete(hddKeyID.Value,
                                 Me.CurrentUser.UserID) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_cannot_delete_because ", "MSG") & "');", True)
                Else
                    Call DeleteAllFile(hddKeyID.Value)
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
            Dim dt As New DataTable
            Dim dts As New DataTable
            Dim succ As Boolean = True
            Dim bl As cProject = New cProject
            Dim data As BD11DEPT = Nothing
            If Session("ORD_Project_DataTable_Document") IsNot Nothing Then
                dts = Session("ORD_Project_DataTable_Document")
            End If
            Dim strCodeMat As String = String.Empty
            Call SetCiterail(txtCodeMat.Text, strCodeMat)


            If hddKeyID.Value <> "" Then
                Call CreateDatatable(dt)
                Call GetDatatable(dt)
                If Not bl.Edit(hddKeyID.Value,
                               chbFboiyn.Checked,
                               txtFRePrjNm.Text,
                               txtFrelocat1.Text,
                               txtFrelocat2.Text,
                               txtFrelocat3.Text,
                               txtFreprovinc.Text,
                               txtFrepostal.Text,
                               txtFtotarea.Text,
                               txtFnoofland.Text,
                               strCodeMat,
                               txtFredesc1.Text,
                               txtFredesc2.Text,
                               txtFredesc3.Text,
                               txt2.Text,
                               ToSystemDate(txtDate0.Text),
                               ToSystemDate(txtDate13.Text),
                               txt11.Text,
                               ToSystemDate(txtDate1.Text),
                               ToSystemDate(txtDate14.Text),
                               txt12.Text,
                               ToSystemDate(txtDate2.Text),
                               ToSystemDate(txtDate15.Text),
                               txt13.Text,
                               ToSystemDate(txtDate3.Text),
                               ToSystemDate(txtDate16.Text),
                               txt14.Text,
                               ToSystemDate(txtDate4.Text),
                               ToSystemDate(txtDate17.Text),
                               txt15.Text,
                               ToSystemDate(txtDate5.Text),
                               ToSystemDate(txtDate18.Text),
                               txt16.Text,
                               ToSystemDate(txtDate6.Text),
                               ToSystemDate(txtDate19.Text),
                               txt17.Text,
                               ToSystemDate(txtDate7.Text),
                               ToSystemDate(txtDate20.Text),
                               txt18.Text,
                               ToSystemDate(txtDate8.Text),
                               ToSystemDate(txtDate21.Text),
                               txt19.Text,
                               ToSystemDate(txtDate9.Text),
                               ToSystemDate(txtDate22.Text),
                               txt20.Text,
                               ToSystemDate(txtDate10.Text),
                               ToSystemDate(txtDate23.Text),
                               txt21.Text,
                               ToSystemDate(txtDate11.Text),
                               ToSystemDate(txtDate24.Text),
                               txt22.Text,
                               ToSystemDate(txtDate12.Text),
                               ToSystemDate(txtDate25.Text),
                               dt,
                               dts,
                               Me.CurrentUser.UserID,
                               txtaFLADNO.Text,
                               txtaFPCLANDNO.Text,
                               ddlProjectType.SelectedValue,
                               txtProjectBrand.Text,
                               ToSystemDate(txtProjectStartDate.Text),
                               ToSystemDate(txtProjectEndDate.Text),
                               ddlConstructionStatus.SelectedValue,
                               ToSystemDate(txtConstructionStartDate.Text),
                               ToSystemDate(txtConstructionEndDate.Text),
                               ddlSaleStatus.SelectedValue,
                               ToSystemDate(txtSaleStartDate.Text),
                               ToSystemDate(txtSaleEndDate.Text)) Then

                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                Else
                    txtCodeMat.Text = strCodeMat
                    Call CopyFileTempToProcess()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                End If
            Else
                Dim lc As ED01PROJ = bl.GetED01PROJByID(txtFreprjno.Text,
                                                        Me.CurrentUser.UserID)
                If lc Is Nothing Then
                    Call CreateDatatable(dt)
                    Call GetDatatable(dt)
                    If Not bl.Add(txtFreprjno.Text,
                                  chbFboiyn.Checked,
                                  txtFRePrjNm.Text,
                                  txtFrelocat1.Text,
                                  txtFrelocat2.Text,
                                  txtFrelocat3.Text,
                                  txtFreprovinc.Text,
                                  txtFrepostal.Text,
                                  txtFtotarea.Text,
                                  txtFnoofland.Text,
                                  strCodeMat,
                                  txtFredesc1.Text,
                                  txtFredesc2.Text,
                                  txtFredesc3.Text,
                                  txt2.Text,
                                  ToSystemDate(txtDate0.Text),
                                  ToSystemDate(txtDate13.Text),
                                  txt11.Text,
                                  ToSystemDate(txtDate1.Text),
                                  ToSystemDate(txtDate14.Text),
                                  txt12.Text,
                                  ToSystemDate(txtDate2.Text),
                                  ToSystemDate(txtDate15.Text),
                                  txt13.Text,
                                  ToSystemDate(txtDate3.Text),
                                  ToSystemDate(txtDate16.Text),
                                  txt14.Text,
                                  ToSystemDate(txtDate4.Text),
                                  ToSystemDate(txtDate17.Text),
                                  txt15.Text,
                                  ToSystemDate(txtDate5.Text),
                                  ToSystemDate(txtDate18.Text),
                                  txt16.Text,
                                  ToSystemDate(txtDate6.Text),
                                  ToSystemDate(txtDate19.Text),
                                  txt17.Text,
                                  ToSystemDate(txtDate7.Text),
                                  ToSystemDate(txtDate20.Text),
                                  txt18.Text,
                                  ToSystemDate(txtDate8.Text),
                                  ToSystemDate(txtDate21.Text),
                                  txt19.Text,
                                  ToSystemDate(txtDate9.Text),
                                  ToSystemDate(txtDate22.Text),
                                  txt20.Text,
                                  ToSystemDate(txtDate10.Text),
                                  ToSystemDate(txtDate23.Text),
                                  txt21.Text,
                                  ToSystemDate(txtDate11.Text),
                                  ToSystemDate(txtDate24.Text),
                                  txt22.Text,
                                  ToSystemDate(txtDate12.Text),
                                  ToSystemDate(txtDate25.Text),
                                  dt,
                                  dts,
                                  Me.CurrentUser.UserID,
                                  txtaFLADNO.Text,
                                  txtaFPCLANDNO.Text,
                                  ddlProjectType.SelectedValue,
                                  txtProjectBrand.Text,
                                  ToSystemDate(txtProjectStartDate.Text),
                                  ToSystemDate(txtProjectEndDate.Text),
                                  ddlConstructionStatus.SelectedValue,
                                  ToSystemDate(txtConstructionStartDate.Text),
                                  ToSystemDate(txtConstructionEndDate.Text),
                                  ddlSaleStatus.SelectedValue,
                                  ToSystemDate(txtSaleStartDate.Text),
                                  ToSystemDate(txtSaleEndDate.Text)) Then

                        Call OpenDialog()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                    Else
                        hddKeyID.Value = txtFreprjno.Text
                        txtCodeMat.Text = strCodeMat
                        Call CopyFileTempToProcess()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_duplicate", "MSG") & "');", True)
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

    Protected Sub btnGridAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGridAdd.Click
        Try
            Call CallLoadGridView()

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "FocusSet('" & hddpClientID.Value & "');", True)

        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnGridAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnFileUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileUpload.Click
        Dim dt As New DataTable
        If Session("ORD_Project_DataTable_Document") IsNot Nothing Then
            dt = Session("ORD_Project_DataTable_Document")
        End If
        If FileUpload1.HasFile Then
            Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
            If fileName <> String.Empty Then
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        For Each i As DataRow In dt.Select("Column= '" & hddColumnDoc.Value & "'")
                            dt.Rows.Remove(i)
                        Next
                    End If
                End If
                Call AddDataTable(fileName,
                                  hddTypeDoc.Value,
                                  hddColumnDoc.Value)
                Call UploadFileTemp(fileName,
                                    hddTypeDoc.Value,
                                    hddColumnDoc.Value)
                Call LoadLinkbutton(fileName,
                                    hddTypeDoc.Value,
                                    hddColumnDoc.Value)
            End If
        End If
    End Sub

    Protected Sub btnDeleteFileUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteFileUpload.Click
        Dim dt As New DataTable
        If Session("ORD_Project_DataTable_Document") IsNot Nothing Then
            dt = Session("ORD_Project_DataTable_Document")
        End If

        If hddTypeDoc.Value <> String.Empty And
           hddColumnDoc.Value <> String.Empty Then
            Call DeleteSigleFileTemp(hddTypeDoc.Value,
                                     hddColumnDoc.Value)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each i As DataRow In dt.Select("Column= '" & hddColumnDoc.Value & "'")
                        dt.Rows.Remove(i)
                    Next
                End If
            End If
            If hddColumnDoc.Value = "FLANDDOC" Then
                hddFlanddoc.Value = String.Empty
                tdFlandoc.Visible = False
                tdFlandocUpload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC" Then
                hddFREQDOC.Value = String.Empty
                tdFREQDOC.Visible = False
                tdFREQDOCUpload.Visible = False
            ElseIf hddColumnDoc.Value = "FCONSTRDOC" Then
                hddFCONSTRDOC.Value = String.Empty
                tdFCONSTRDOC.Visible = False
                tdFCONSTRDOCUpload.Visible = False
            ElseIf hddColumnDoc.Value = "FCONSTRDOC1" Then
                hddFCONSTRDOC1.Value = String.Empty
                tdFCONSTRDOC1.Visible = False
                tdFCONSTRDOC1Upload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC1" Then
                'hddFREQDOC1.Attributes.Remove("onclick")
                hddFREQDOC1.Value = String.Empty
                tdFREQDOC1.Visible = False
                tdFREQDOC1Upload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC2" Then
                hddFREQDOC2.Value = String.Empty
                tdFREQDOC2.Visible = False
                tdFREQDOC2Upload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC3" Then
                hddFREQDOC3.Value = String.Empty
                tdFREQDOC3.Visible = False
                tdFREQDOC3Upload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC4" Then
                hddFREQDOC4.Value = String.Empty
                tdFREQDOC4.Visible = False
                tdFREQDOC4Upload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC5" Then
                hddFREQDOC5.Value = String.Empty
                tdFREQDOC5.Visible = False
                tdFREQDOC5Upload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC6" Then
                hddFREQDOC6.Value = String.Empty
                tdFREQDOC6.Visible = False
                tdFREQDOC6Upload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC7" Then
                hddFREQDOC7.Value = String.Empty
                tdFREQDOC7.Visible = False
                tdFREQDOC7Upload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC8" Then
                hddFREQDOC8.Value = String.Empty
                tdFREQDOC8.Visible = False
                tdFREQDOC8Upload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC9" Then
                hddFREQDOC9.Value = String.Empty
                tdFREQDOC9.Visible = False
                tdFREQDOC9Upload.Visible = False
            ElseIf hddColumnDoc.Value = "FREQDOC10" Then
                hddFREQDOC10.Value = String.Empty
                tdFREQDOC10.Visible = False
                tdFREQDOC10Upload.Visible = False
            End If
        End If
    End Sub

    Protected Sub btnReload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReload.Click
        Try
            hddpAutoCode.Value = String.Empty
            If hddpAutoName.Value <> String.Empty Then
                txtFrelocat3.Text = hddpAutoName.Value.Split(" ")(0)
                txtFreprovinc.Text = hddpAutoName.Value.Split(" ")(1)
                txtFrepostal.Text = hddpAutoName.Value.Split(" ")(2)
            Else
                If txtFrelocat3.Text <> String.Empty Then
                    hddpAutoCode.Value = txtFrelocat3.Text
                    hddpAutoName.Value = txtFrelocat3.Text & " " &
                                         txtFreprovinc.Text & " " &
                                         txtFrepostal.Text

                    txtFrelocat3.Text = hddpAutoName.Value.Split(" ")(0)
                    txtFreprovinc.Text = hddpAutoName.Value.Split(" ")(1)
                    txtFrepostal.Text = hddpAutoName.Value.Split(" ")(2)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReload_Click", ex)
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
    End Sub
#End Region

#Region "Session"
    Public Function GetDataSetProject() As List(Of vw_Project_join)
        Try
            Return Session("IDOCS.application.LoaddataProect")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataProject(ByVal lcProject As List(Of vw_Project_join)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataProect", lcProject)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "GridView"
    Protected Sub grdView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView2.RowDataBound
        Try
            Select Case e.Row.RowType
                Case DataControlRowType.DataRow
                    Dim btnEdit As ImageButton = e.Row.FindControl("btnEdit")
                    btnEdit.CommandArgument = DataBinder.Eval(e.Row.DataItem, "FASSETNO")
                    Dim hddID As HiddenField = CType(e.Row.FindControl("hddID"), HiddenField)
                    Dim txtgFASSETNO As Label = CType(e.Row.FindControl("txtgFASSETNO"), Label)
                    Dim txtgChanodNo As Label = CType(e.Row.FindControl("txtgChanodNo"), Label)
                    Dim txtgChanodMud As Label = CType(e.Row.FindControl("txtgChanodMud"), Label)
                    Dim txtgArea As Label = CType(e.Row.FindControl("txtgArea"), Label)
                    Dim hddFlagSetAdd As HiddenField = CType(e.Row.FindControl("hddFlagSetAdd"), HiddenField)
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "ID")) Then
                        If DataBinder.Eval(e.Row.DataItem, "ID").ToString <> String.Empty Then
                            hddID.Value = DataBinder.Eval(e.Row.DataItem, "ID")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FASSETNO")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FASSETNO").ToString <> String.Empty Then
                            txtgFASSETNO.Text = DataBinder.Eval(e.Row.DataItem, "FASSETNO")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FLANDNO")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FLANDNO").ToString <> String.Empty Then
                            txtgChanodNo.Text = DataBinder.Eval(e.Row.DataItem, "FLANDNO")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FPCLANDNO")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FPCLANDNO").ToString <> String.Empty Then
                            txtgChanodMud.Text = DataBinder.Eval(e.Row.DataItem, "FPCLANDNO")
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FTOTAREA")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FTOTAREA").ToString <> String.Empty Then
                            Dim Temp1 As String = String.Empty
                            Dim Temp2 As String = String.Empty
                            Dim Temp3 As String = String.Empty
                            Call CalReverse(DataBinder.Eval(e.Row.DataItem, "FTOTAREA").ToString,
                                            Temp1,
                                            Temp2,
                                            Temp3)
                            txtgArea.Text = Temp1 & "-" & Temp2 & "-" & Temp3
                        End If
                    End If
                    If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")) Then
                        If DataBinder.Eval(e.Row.DataItem, "FlagSetAdd").ToString <> String.Empty Then
                            hddFlagSetAdd.Value = DataBinder.Eval(e.Row.DataItem, "FlagSetAdd")
                        End If
                    End If
                    If hddFlagSetAdd.Value = "1" Then
                        txtgFASSETNO.Attributes.Add("onclick", "addRowGridView('" & txtgFASSETNO.ClientID & "');")
                        txtgChanodNo.Attributes.Add("onclick", "addRowGridView('" & txtgChanodNo.ClientID & "');")
                        txtgChanodMud.Attributes.Add("onclick", "addRowGridView('" & txtgChanodMud.ClientID & "');")
                        txtgArea.Attributes.Add("onclick", "addRowGridView('" & txtgArea.ClientID & "');")
                        btnEdit.Visible = False
                    ElseIf hddFlagSetAdd.Value = "0" Then
                        txtgFASSETNO.Attributes.Remove("onclick")
                        txtgChanodNo.Attributes.Remove("onclick")
                        txtgChanodMud.Attributes.Remove("onclick")
                        txtgArea.Attributes.Remove("onclick")
                        btnEdit.Visible = True
                    End If
            End Select
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, hddParameterMenuID.Value, "grdView2_RowDataBound", ex)
        End Try
    End Sub
    Private Sub grdView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdView2.RowCommand
        Try
            Select Case e.CommandName
                Case "btnEdit"
                    'Dim dt As New DataTable
                    'Call CreateDatatable(dt)
                    'Call GetDatatable(dt)
                    'If dt IsNot Nothing Then
                    '    For Each i As DataRow In dt.Select("ID= '" & e.CommandArgument & "'")
                    '        dt.Rows.Remove(i)
                    '    Next
                    '    If dt.Rows.Count > 0 Then
                    '        grdView2.DataSource = dt
                    '        grdView2.DataBind()
                    '    Else
                    '        grdView2.DataSource = Nothing
                    '        grdView2.DataBind()
                    '    End If
                    'End If
                    Call Redirect("TRN_DataLandBank.aspx?flagedit=" & e.CommandArgument & "&backurl=" & hddKeyID.Value & "")
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

        grdView2.DataSource = dt
        grdView2.DataBind()
    End Sub
    Public Sub CreateDatatable(ByRef dt As DataTable)
        dt.Columns.Add("ID")
        dt.Columns.Add("FASSETNO")
        dt.Columns.Add("FLANDNO")
        dt.Columns.Add("FPCLANDNO")
        dt.Columns.Add("FTOTAREA")
        dt.Columns.Add("FlagSetAdd")
    End Sub
    Public Sub AddDataTable(ByRef dt As DataTable)
        Call CreateDatatable(dt)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            If grdView2 IsNot Nothing Then
                For Each e As GridViewRow In grdView2.Rows
                    Dim hddID As HiddenField = CType(e.FindControl("hddID"), HiddenField)
                    Dim txtgFASSETNO As Label = CType(e.FindControl("txtgFASSETNO"), Label)
                    Dim txtgChanodNo As Label = CType(e.FindControl("txtgChanodNo"), Label)
                    Dim txtgChanodMud As Label = CType(e.FindControl("txtgChanodMud"), Label)
                    Dim txtgArea As Label = CType(e.FindControl("txtgArea"), Label)
                    Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)

                    dr = dt.NewRow
                    strID = CInt(hddID.Value)
                    dr.Item("ID") = hddID.Value
                    dr.Item("FASSETNO") = txtgFASSETNO.Text
                    dr.Item("FLANDNO") = txtgChanodNo.Text
                    dr.Item("FPCLANDNO") = txtgChanodMud.Text
                    dr.Item("FTOTAREA") = txtgArea.Text
                    dr.Item("FQTY") = txtgArea.Text

                    hddFlagSetAdd.Value = "0"
                    dr.Item("FlagSetAdd") = "0"

                    dt.Rows.Add(dr)
                    If hddFlagSetAdd.Value = "1" Then
                        dt.Rows.Remove(dr)
                    End If
                Next
                'dr = dt.NewRow
                'dr.Item("ID") = strID + 1
                'dr.Item("FLANDNO") = String.Empty
                'dr.Item("FPCLANDNO") = String.Empty
                'dr.Item("FTOTAREA") = String.Empty
                'dr.Item("FlagSetAdd") = "1"
                'dt.Rows.Add(dr)
            Else
                'dr = dt.NewRow
                'dr.Item("ID") = strID + 1
                'dr.Item("FLANDNO") = String.Empty
                'dr.Item("FPCLANDNO") = String.Empty
                'dr.Item("FTOTAREA") = String.Empty
                'dr.Item("FlagSetAdd") = "1"
                'dt.Rows.Add(dr)
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
                Dim txtgFASSETNO As Label = CType(e.FindControl("txtgFASSETNO"), Label)
                Dim txtgChanodNo As Label = CType(e.FindControl("txtgChanodNo"), Label)
                Dim txtgChanodMud As Label = CType(e.FindControl("txtgChanodMud"), Label)
                Dim txtgArea As Label = CType(e.FindControl("txtgArea"), Label)
                Dim hddFlagSetAdd As HiddenField = CType(e.FindControl("hddFlagSetAdd"), HiddenField)
                dr = dt.NewRow
                dr.Item("ID") = hddID.Value
                dr.Item("FASSETNO") = txtgFASSETNO.Text
                dr.Item("FLANDNO") = txtgChanodNo.Text
                dr.Item("FPCLANDNO") = txtgChanodMud.Text
                dr.Item("FTOTAREA") = txtgArea.Text
                dr.Item("FlagSetAdd") = hddFlagSetAdd.Value
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
    Public Sub SetDatatable(ByRef dt As DataTable,
                            ByVal lst As List(Of FD11PROP),
                            ByRef pFtotarea As String)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            Dim iTotalQTY As Integer = 0
            For Each m As FD11PROP In lst
                dr = dt.NewRow
                strID = strID + 1
                dr.Item("ID") = strID
                dr.Item("FASSETNO") = m.FASSETNO
                dr.Item("FLANDNO") = m.FPCPIECE
                dr.Item("FPCLANDNO") = m.FPCLNDNO
                dr.Item("FTOTAREA") = m.FQTY
                iTotalQTY += m.FQTY
                dr.Item("FlagSetAdd") = "0"
                dt.Rows.Add(dr)
            Next
            pFtotarea = iTotalQTY
            'dr = dt.NewRow
            'dr.Item("ID") = strID + 1
            'dr.Item("FLANDNO") = String.Empty
            'dr.Item("FPCLANDNO") = String.Empty
            'dr.Item("FTOTAREA") = String.Empty
            'dr.Item("FlagSetAdd") = "1"
            'dt.Rows.Add(dr)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "GetDatatable", ex)
        End Try
    End Sub
#End Region

#Region "FileUpload"
    Public Sub CreateTableFileUpload(ByRef dt As DataTable)
        dt.Columns.Add("ID")
        dt.Columns.Add("Type")
        dt.Columns.Add("Column")
        dt.Columns.Add("FileName")
        dt.Columns.Add("FilePath")
    End Sub
    Public Sub AddDataTable(ByVal strFileName As String,
                            ByVal strType As String,
                            ByVal strColumn As String)
        Dim dt As New DataTable
        Dim dts As New DataTable
        Call CreateTableFileUpload(dt)
        Try
            Dim dr As DataRow
            Dim strID As Integer = 0
            If Session("ORD_Project_DataTable_Document") IsNot Nothing Then
                dts = Session("ORD_Project_DataTable_Document")
            End If
            If dts IsNot Nothing Then
                For i As Integer = 0 To dts.Rows.Count - 1
                    dr = dt.NewRow
                    dr.Item("ID") = strID
                    dr.Item("Type") = dts.Rows(i)("Type").ToString
                    dr.Item("Column") = dts.Rows(i)("Column").ToString
                    dr.Item("FileName") = dts.Rows(i)("FileName").ToString
                    dr.Item("FilePath") = dts.Rows(i)("FilePath").ToString
                    dt.Rows.Add(dr)
                    strID = strID + 1
                Next
                dr = dt.NewRow
                dr.Item("ID") = strID
                dr.Item("Type") = strType
                dr.Item("Column") = strColumn
                dr.Item("FileName") = strFileName
                dr.Item("FilePath") = strPathServer &
                                        hddParameterMenuID.Value & "/" &
                                        Me.CurrentUser.UserID & "/" &
                                        strType & "/" &
                                        strColumn & "/" &
                                        strFileName
                dt.Rows.Add(dr)
            Else
                dr = dt.NewRow
                dr.Item("ID") = strID
                dr.Item("Type") = strType
                dr.Item("Column") = strColumn
                dr.Item("FileName") = strID & ".jpg"
                dr.Item("FilePath") = strPathServer &
                                        hddParameterMenuID.Value & "/" &
                                        Me.CurrentUser.UserID & "/" &
                                        strType & "/" &
                                        strColumn & "/" &
                                        strFileName
                dt.Rows.Add(dr)
            End If
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Session.Add("ORD_Project_DataTable_Document", dt)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

#Region "UploadFileTemp"
    Public Sub CrateFolderTemp(ByVal strType As String,
                               ByVal strColumn As String)
        Dim strPath As String = Server.MapPath("Uploads\" &
                                                hddParameterMenuID.Value & "\" &
                                                Me.CurrentUser.UserID & "\" &
                                                strType & "\" &
                                                strColumn & "\")
        If (Not System.IO.Directory.Exists(strPath)) Then
            System.IO.Directory.CreateDirectory(strPath)
        End If
    End Sub
    Public Sub UploadFileTemp(ByVal strFileName As String,
                              ByVal strType As String,
                              ByVal strColumn As String)
        Call CrateFolderTemp(strType,
                             strColumn)
        Dim strID As Integer = 0
        Dim objFSO As Object
        Dim objFolder As Object
        Dim objFile As Object
        Dim TempSave As String = String.Empty
        Dim TempDelete As String = String.Empty
        Dim TempDeleteAfter As String = String.Empty

        TempSave = Server.MapPath("Uploads/" &
                                  hddParameterMenuID.Value & "/" &
                                  Me.CurrentUser.UserID & "/" &
                                  strType & "/" &
                                  strColumn & "/" &
                                  strFileName)
        TempDelete = Server.MapPath("Uploads\" &
                                    hddParameterMenuID.Value & "\" &
                                    Me.CurrentUser.UserID & "\" &
                                    strType & "\" &
                                    strColumn & "\" &
                                    strFileName)
        objFSO = CreateObject("Scripting.FileSystemObject")
        objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" &
                                                    hddParameterMenuID.Value & "\" &
                                                    Me.CurrentUser.UserID & "\" &
                                                    strType & "\" &
                                                    strColumn & "\"))
        For Each objFile In objFolder.Files
            TempDeleteAfter = Server.MapPath("Uploads\" &
                                             hddParameterMenuID.Value & "\" &
                                             Me.CurrentUser.UserID & "\" &
                                             strType & "\" &
                                             strColumn & "\" &
                                             objFile.Name)
            If System.IO.File.Exists(TempDeleteAfter) Then
                System.IO.File.Delete(TempDeleteAfter)
            End If
        Next
        If System.IO.File.Exists(TempDelete) Then
            System.IO.File.Delete(TempDelete)
            FileUpload1.PostedFile.SaveAs(TempSave)
        Else
            FileUpload1.PostedFile.SaveAs(TempSave)
        End If

    End Sub
#End Region

#Region "DeleteFileTemp"
    Public Sub DeleteAllFileTemp()
        Dim dt As New DataTable
        Call CreateDataFolder(dt)
        Dim objFSO As Object
        Dim objFolder As Object
        Dim objFile As Object

        Try
            For i As Integer = 0 To dt.Rows.Count - 1
                Try
                    objFSO = CreateObject("Scripting.FileSystemObject")
                    objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" &
                                                                hddParameterMenuID.Value & "\" &
                                                                Me.CurrentUser.UserID & "\" &
                                                                dt.Rows(i)("Type").ToString & "\" &
                                                                dt.Rows(i)("Column").ToString & "\"))
                    For Each objFile In objFolder.Files
                        Dim TempDelete As String = Server.MapPath("Uploads\" &
                                                                  hddParameterMenuID.Value & "\" &
                                                                  Me.CurrentUser.UserID & "\" &
                                                                  dt.Rows(i)("Type").ToString & "\" &
                                                                  dt.Rows(i)("Column").ToString & "\" &
                                                                  objFile.Name)
                        If System.IO.File.Exists(TempDelete) Then
                            System.IO.File.Delete(TempDelete)
                        End If
                    Next objFile
                Catch ex As Exception

                End Try
            Next
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strPathColumn As String = Server.MapPath("Uploads\" &
                                                   hddParameterMenuID.Value & "\" &
                                                   Me.CurrentUser.UserID & "\" &
                                                   dt.Rows(i)("Type").ToString & "\" &
                                                   dt.Rows(i)("Column").ToString & "\")

                If System.IO.Directory.Exists(strPathColumn) Then
                    System.IO.Directory.Delete(strPathColumn)
                End If
            Next
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strPathType As String = Server.MapPath("Uploads\" &
                                                   hddParameterMenuID.Value & "\" &
                                                   Me.CurrentUser.UserID & "\" &
                                                   dt.Rows(i)("Type").ToString & "\")

                If System.IO.Directory.Exists(strPathType) Then
                    System.IO.Directory.Delete(strPathType)
                End If
            Next
            Dim strPathUser As String = Server.MapPath("Uploads\" &
                                                   hddParameterMenuID.Value & "\" &
                                                   Me.CurrentUser.UserID & "\")

            If System.IO.Directory.Exists(strPathUser) Then
                System.IO.Directory.Delete(strPathUser)
            End If
        Catch ex As Exception

        End Try

    End Sub
    Public Sub DeleteSigleFileTemp(ByVal strType As String,
                                   ByVal strColumn As String)
        Dim objFSO As Object
        Dim objFolder As Object
        Dim objFile As Object
        objFSO = CreateObject("Scripting.FileSystemObject")
        objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" &
                                                     hddParameterMenuID.Value & "\" &
                                                     Me.CurrentUser.UserID & "\" &
                                                     strType & "\" &
                                                     strColumn & "\"))
        For Each objFile In objFolder.Files
            Dim TempDelete As String = Server.MapPath("Uploads\" &
                                                       hddParameterMenuID.Value & "\" &
                                                       Me.CurrentUser.UserID & "\" &
                                                       strType & "\" &
                                                       strColumn & "\" &
                                                       objFile.Name)
            If System.IO.File.Exists(TempDelete) Then
                System.IO.File.Delete(TempDelete)
            End If
        Next objFile

    End Sub
#End Region

#Region "UploadProcess"
    Public Sub CopyFileTempToProcess()
        Dim strKeyID As String = IIf(hddKeyID.Value <> String.Empty, hddKeyID.Value, txtFreprjno.Text)
        Call DeleteAllFile(strKeyID)
        Call CopyFile(strKeyID)
    End Sub
    Public Sub CrateFolder(ByVal strKeyID As String,
                           ByVal strType As String,
                           ByVal strColumn As String)
        Dim strPath As String = Server.MapPath("Document\" &
                                                strType & "\" &
                                                strKeyID & "\" &
                                                strColumn & "\")
        If (Not System.IO.Directory.Exists(strPath)) Then
            System.IO.Directory.CreateDirectory(strPath)
        End If
    End Sub
    Public Sub CopyFile(ByVal strKeyID As String)
        Dim dt As New DataTable
        Dim TempSave As String = String.Empty
        Dim TempDelete As String = String.Empty
        Dim TempPath As String = String.Empty
        If Session("ORD_Project_DataTable_Document") IsNot Nothing Then
            dt = Session("ORD_Project_DataTable_Document")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Call CrateFolder(strKeyID,
                                         dt.Rows(i)("Type").ToString,
                                         dt.Rows(i)("Column").ToString)

                        TempSave = Server.MapPath("Document/" &
                                                   dt.Rows(i)("Type").ToString & "/" &
                                                   strKeyID & "/" &
                                                   dt.Rows(i)("Column").ToString & "/" &
                                                   dt.Rows(i)("FileName").ToString)
                        TempPath = Server.MapPath("Uploads\" &
                                                   hddParameterMenuID.Value & "\" &
                                                   Me.CurrentUser.UserID & "\" &
                                                   dt.Rows(i)("Type").ToString & "\" &
                                                   dt.Rows(i)("Column").ToString & "\" &
                                                   dt.Rows(i)("FileName").ToString)
                        If System.IO.File.Exists(TempPath) Then
                            System.IO.File.Copy(TempPath, TempSave)
                        End If
                    Next
                End If
            End If
        End If
    End Sub
#End Region

#Region "DeleteProcess"
    Public Sub DeleteAllFile(ByVal strKeyID As String)
        Dim dt As New DataTable
        Call CreateDataFolder(dt)
        Dim objFSO As Object
        Dim objFolder As Object
        Dim objFile As Object

        Try
            For i As Integer = 0 To dt.Rows.Count - 1
                Try
                    objFSO = CreateObject("Scripting.FileSystemObject")
                    objFolder = objFSO.GetFolder(Server.MapPath("Document\" &
                                                                dt.Rows(i)("Type").ToString & "\" &
                                                                strKeyID & "\" &
                                                                dt.Rows(i)("Column").ToString & "\"))
                    For Each objFile In objFolder.Files
                        Dim TempDelete As String = Server.MapPath("Document\" &
                                                                  dt.Rows(i)("Type").ToString & "\" &
                                                                  strKeyID & "\" &
                                                                  dt.Rows(i)("Column").ToString & "\" &
                                                                  objFile.Name)
                        If System.IO.File.Exists(TempDelete) Then
                            System.IO.File.Delete(TempDelete)
                        End If
                    Next objFile
                Catch ex As Exception

                End Try
            Next
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strPathColumn As String = Server.MapPath("Document\" &
                                                             dt.Rows(i)("Type").ToString & "\" &
                                                             strKeyID & "\" &
                                                             dt.Rows(i)("Column").ToString & "\")

                If System.IO.Directory.Exists(strPathColumn) Then
                    System.IO.Directory.Delete(strPathColumn)
                End If
            Next
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strPathKeyID As String = Server.MapPath("Document\" &
                                                             dt.Rows(i)("Type").ToString & "\" &
                                                             strKeyID & "\")

                If System.IO.Directory.Exists(strPathKeyID) Then
                    System.IO.Directory.Delete(strPathKeyID)
                End If
            Next
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim strPathType As String = Server.MapPath("Document\" &
                                                            dt.Rows(i)("Type").ToString & "\")

                If System.IO.Directory.Exists(strPathType) Then
                    System.IO.Directory.Delete(strPathType)
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "CopyProcessToTemp"
    Public Sub CopyProcessToTemp()
        Dim TempSave As String = String.Empty
        Dim TempPath As String = String.Empty
        Dim dt As New DataTable
        Dim strKeyID As String = IIf(hddKeyID.Value <> String.Empty, hddKeyID.Value, txtFreprjno.Text)

        If Session("ORD_Project_DataTable_Document") IsNot Nothing Then
            dt = Session("ORD_Project_DataTable_Document")
        End If
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Call CrateFolderTemp(dt.Rows(i)("Type").ToString,
                                         dt.Rows(i)("Column").ToString)
                    TempSave = Server.MapPath("Uploads/" &
                                              hddParameterMenuID.Value & "/" &
                                              Me.CurrentUser.UserID & "/" &
                                              dt.Rows(i)("Type").ToString & "/" &
                                              dt.Rows(i)("Column").ToString & "/" &
                                              dt.Rows(i)("FileName").ToString)
                    TempPath = Server.MapPath("Document\" &
                                                dt.Rows(i)("Type").ToString & "\" &
                                                strKeyID & "\" &
                                                dt.Rows(i)("Column").ToString & "\" &
                                                dt.Rows(i)("FileName").ToString)
                    If System.IO.File.Exists(TempPath) Then
                        System.IO.File.Copy(TempPath, TempSave)
                    End If
                Next
            End If
        End If
    End Sub
#End Region

#Region "LoadLinkbutton"
    Public Sub LoadLinkbutton(ByVal strFileName As String,
                              ByVal strType As String,
                              ByVal strColumn As String)
        If strColumn = "FLANDDOC" Then
            hddFlanddoc.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFlandoc.Visible = True
            tdFlandocUpload.Visible = True

            hrefFlandoc.Title = strFileName
        ElseIf strColumn = "FREQDOC" Then
            hddFREQDOC.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFREQDOCAdd.Style.Add("display", "")
            tdFREQDOC.Visible = True
            tdFREQDOCUpload.Visible = True

            hrefFREQDOC.Title = strFileName
        ElseIf strColumn = "FCONSTRDOC" Then
            hddFCONSTRDOC.Value = strPathServer &
                                    hddParameterMenuID.Value & "/" &
                                    Me.CurrentUser.UserID & "/" &
                                    strType & "/" &
                                    strColumn & "/" &
                                    strFileName
            tdFCONSTRDOCAdd.Style.Add("display", "")
            tdFCONSTRDOC.Visible = True
            tdFCONSTRDOCUpload.Visible = True

            hrefFCONSTRDOC.Title = strFileName
        ElseIf strColumn = "FCONSTRDOC1" Then
            hddFCONSTRDOC1.Value = strPathServer &
                                    hddParameterMenuID.Value & "/" &
                                    Me.CurrentUser.UserID & "/" &
                                    strType & "/" &
                                    strColumn & "/" &
                                    strFileName
            tdFCONSTRDOC1Add.Style.Add("display", "")
            tdFCONSTRDOC1.Visible = True
            tdFCONSTRDOC1Upload.Visible = True

            hrefFCONSTRDOC1.Title = strFileName
        ElseIf strColumn = "FREQDOC1" Then
            hddFREQDOC1.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFREQDOC1Add.Style.Add("display", "")
            tdFREQDOC1.Visible = True
            tdFREQDOC1Upload.Visible = True

            hrefFREQDOC1.Title = strFileName
        ElseIf strColumn = "FREQDOC2" Then
            hddFREQDOC2.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFREQDOC2Add.Style.Add("display", "")
            tdFREQDOC2.Visible = True
            tdFREQDOC2Upload.Visible = True

            hrefFREQDOC2.Title = strFileName
        ElseIf strColumn = "FREQDOC3" Then
            hddFREQDOC3.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFREQDOC3Add.Style.Add("display", "")
            tdFREQDOC3.Visible = True
            tdFREQDOC3Upload.Visible = True

            hrefFREQDOC3.Title = strFileName
        ElseIf strColumn = "FREQDOC4" Then
            hddFREQDOC4.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFREQDOC4Add.Style.Add("display", "")
            tdFREQDOC4.Visible = True
            tdFREQDOC4Upload.Visible = True

            hrefFREQDOC4.Title = strFileName
        ElseIf strColumn = "FREQDOC5" Then
            hddFREQDOC5.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFREQDOC5Add.Style.Add("display", "")
            tdFREQDOC5.Visible = True
            tdFREQDOC5Upload.Visible = True

            hrefFREQDOC5.Title = strFileName
        ElseIf strColumn = "FREQDOC6" Then
            hddFREQDOC6.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFREQDOC6Add.Style.Add("display", "")
            tdFREQDOC6.Visible = True
            tdFREQDOC6Upload.Visible = True

            hrefFREQDOC6.Title = strFileName
        ElseIf strColumn = "FREQDOC7" Then
            hddFREQDOC7.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFREQDOC7Add.Style.Add("display", "")
            tdFREQDOC7.Visible = True
            tdFREQDOC7Upload.Visible = True

            hrefFREQDOC7.Title = strFileName
        ElseIf strColumn = "FREQDOC8" Then
            hddFREQDOC8.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFREQDOC8Add.Style.Add("display", "")
            tdFREQDOC8.Visible = True
            tdFREQDOC8Upload.Visible = True

            hrefFREQDOC8.Title = strFileName
        ElseIf strColumn = "FREQDOC9" Then
            hddFREQDOC9.Value = strPathServer &
                                hddParameterMenuID.Value & "/" &
                                Me.CurrentUser.UserID & "/" &
                                strType & "/" &
                                strColumn & "/" &
                                strFileName
            tdFREQDOC9Add.Style.Add("display", "")
            tdFREQDOC9.Visible = True
            tdFREQDOC9Upload.Visible = True

            hrefFREQDOC9.Title = strFileName
        ElseIf strColumn = "FREQDOC10" Then
            hddFREQDOC10.Value = strPathServer &
                                    hddParameterMenuID.Value & "/" &
                                    Me.CurrentUser.UserID & "/" &
                                    strType & "/" &
                                    strColumn & "/" &
                                    strFileName
            tdFREQDOC10Add.Style.Add("display", "")
            tdFREQDOC10.Visible = True
            tdFREQDOC10Upload.Visible = True

            hrefFREQDOC10.Title = strFileName
        End If

    End Sub
#End Region

    Public Sub CreateDataFolder(ByRef dt As DataTable)
        dt.Columns.Add("Type")
        dt.Columns.Add("Column")
        Dim dr As DataRow
        dr = dt.NewRow
        dr.Item("Type") = "FLAND"
        dr.Item("Column") = "FLANDDOC"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FCONSTRDOC"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FCONSTRDOC1"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC1"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC2"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC3"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC4"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC5"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC6"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC7"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC8"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC9"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr.Item("Type") = "FREQ"
        dr.Item("Column") = "FREQDOC10"
        dt.Rows.Add(dr)
    End Sub

#End Region

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

End Class
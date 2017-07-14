Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO

Public Class MST_Sales
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Page.DataBind()
                hddpLG.Value = Me.WebCulture.ToString.ToUpper
                Session.Remove("IDOCS.application.LoaddataSalesMan")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("MST_Sales.aspx")
                Me.ClearSessionPageLoad("MST_Sales.aspx")

                Call LoadRadioButtonList()
                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
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

#Region "DropDownList"
    Public Sub LoadDropdownlist()
        Try
            Call LoadSaleGroup(ddlsSalesGroup, "S")
            Call LoadSaleGroup(ddlaSalesGroup, "A")
            Call LoadDivision(ddlsDivision, "S")
            Call LoadDivision(ddlaDivision, "A")
            Call LoadDepartment(ddlsDepartment, "S")
            Call LoadDepartment(ddlaDepartment, "A")
            Call LoadSection(ddlsSection, "S", ddlsDepartment.SelectedValue)
            Call LoadSection(ddlaSection, "A", ddlaDepartment.SelectedValue)
            Call LoadZone(ddlsZone, "S")
            Call LoadZone(ddlaZone, "A")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDropdownlist", ex)
        End Try
    End Sub
    Public Sub LoadSaleGroup(ByVal ddl As DropDownList,
                             ByVal strType As String)
        Dim bl As cSalesMan = New cSalesMan
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadSalsManGroup()
            If lc IsNot Nothing Then
                ddl.DataValueField = "FSMTYPE"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "FSMTYPEDS"
                Else
                    ddl.DataTextField = "FSMTYPEDS"
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
    Public Sub LoadDivision(ByVal ddl As DropDownList,
                            ByVal strType As String)
        Dim bl As cSalesMan = New cSalesMan
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadDivision()
            If lc IsNot Nothing Then
                ddl.DataValueField = "FDIVCODE"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "FDIVNAME"
                Else
                    ddl.DataTextField = "FDIVNAMET"
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
    Public Sub LoadDepartment(ByVal ddl As DropDownList,
                              ByVal strType As String)
        Dim bl As cSalesMan = New cSalesMan
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadDepartment()
            If lc IsNot Nothing Then
                ddl.DataValueField = "FDPCODE"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "FDPNAME"
                Else
                    ddl.DataTextField = "FDPNAME"
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
    Public Sub LoadSection(ByVal ddl As DropDownList,
                           ByVal strType As String,
                           ByVal strDepartment As String)
        Dim bl As cSalesMan = New cSalesMan
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadSection(strDepartment)
            If lc IsNot Nothing Then
                ddl.DataValueField = "FSECCODE"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "FSECNAME"
                Else
                    ddl.DataTextField = "FSECNAME"
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
    Public Sub LoadZone(ByVal ddl As DropDownList,
                        ByVal strType As String)
        Dim bl As cSalesMan = New cSalesMan
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadZone()
            If lc IsNot Nothing Then
                ddl.DataValueField = "FSLROUTE"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "FSLROUTENM"
                Else
                    ddl.DataTextField = "FSLROUTENM"
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

#Region "Event Dropdownlist"
    Protected Sub ddlsDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsDepartment.SelectedIndexChanged
        Call LoadSection(ddlsSection, "S", ddlsDepartment.SelectedValue)
    End Sub
    Protected Sub ddlaDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlaDepartment.SelectedIndexChanged
        Call LoadSection(ddlaSection, "A", ddlaDepartment.SelectedValue)
    End Sub
#End Region

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        If hddpFlagSearch.Value = "1" Then
            Session.Remove("MST_Sales_PageInfo")
            Session.Remove("MST_Sales_Search")
            Session.Remove("MST_Sales_pageLength")
        End If
        Call Redirect("MST_Sales.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        '    Dim strSalesGroup As String = String.Empty

        '    If txtsSalesGroup.Text <> String.Empty Then
        '        strSalesGroup = hddsSalesGroup.Value
        '    End If
        Session.Remove("MST_Sales_SalesGroup")
        Session.Remove("MST_Sales_Division")
        Session.Remove("MST_Sales_Department")
        Session.Remove("MST_Sales_Section")
        Session.Remove("MST_Sales_Zone")
        Session.Remove("MST_Sales_Active")
        Session.Remove("MST_Sales_FS")
        Session.Add("MST_Sales_SalesGroup", IIf(ddlsSalesGroup.SelectedIndex <> 0, ddlsSalesGroup.SelectedValue, ""))
        Session.Add("MST_Sales_Division", IIf(ddlsDivision.SelectedIndex <> 0, ddlsDivision.SelectedValue, ""))
        Session.Add("MST_Sales_Department", IIf(ddlsDepartment.SelectedIndex <> 0, ddlsDepartment.SelectedValue, ""))
        Session.Add("MST_Sales_Section", IIf(ddlsSection.SelectedIndex <> 0, ddlsSection.SelectedValue, ""))
        Session.Add("MST_Sales_Zone", IIf(ddlsZone.SelectedIndex <> 0, ddlsZone.SelectedValue, ""))
        Session.Add("MST_Sales_Active", IIf(rdbsActive.SelectedIndex <> 0, rdbsActive.SelectedValue, ""))
        Session.Add("MST_Sales_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("MST_Sales_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("MST_Sales_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("MST_Sales_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        hddReloadGrid.Value = "1"
        If (Session("MST_Sales_SalesGroup") IsNot Nothing And
           Session("MST_Sales_Division") IsNot Nothing And
           Session("MST_Sales_Department") IsNot Nothing And
           Session("MST_Sales_Section") IsNot Nothing And
           Session("MST_Sales_Zone") IsNot Nothing) Or
           Session("MST_Sales_Active") IsNot Nothing Then
            If Session("MST_Sales_SalesGroup").ToString <> String.Empty Then
                Dim strSalesGroup As String = Session("MST_Sales_SalesGroup").ToString
                Try
                    ddlsSalesGroup.SelectedValue = strSalesGroup
                Catch ex As Exception
                    ddlsSalesGroup.SelectedIndex = 0
                End Try
            End If
            If Session("MST_Sales_Division").ToString <> String.Empty Then
                Dim strDivision As String = Session("MST_Sales_Division").ToString
                Try
                    ddlsDivision.SelectedValue = strDivision
                Catch ex As Exception
                    ddlsDivision.SelectedIndex = 0
                End Try
            End If
            If Session("MST_Sales_Department").ToString <> String.Empty Then
                Dim strDepartment As String = Session("MST_Sales_Department").ToString
                Try
                    ddlsDepartment.SelectedValue = strDepartment
                Catch ex As Exception
                    ddlsDepartment.SelectedIndex = 0
                End Try
            End If
            If Session("MST_Sales_Section").ToString <> String.Empty Then
                Dim strSection As String = Session("MST_Sales_Section").ToString
                Try
                    ddlsSection.SelectedValue = strSection
                Catch ex As Exception
                    ddlsSection.SelectedIndex = 0
                End Try
            End If
            If Session("MST_Sales_Zone").ToString <> String.Empty Then
                Dim strZone As String = Session("MST_Sales_Zone").ToString
                Try
                    ddlsZone.SelectedValue = strZone
                Catch ex As Exception
                    ddlsZone.SelectedIndex = 0
                End Try
            End If
            If Session("MST_Sales_Active").ToString IsNot Nothing Then
                Dim strActive As String = Session("MST_Sales_Active").ToString
                Try
                    rdbsActive.SelectedValue = strActive
                Catch ex As Exception
                    rdbsActive.SelectedIndex = 0
                End Try
            End If

            If Session("MST_Sales_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("MST_Sales_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
            End If
        End If
        If Session("MST_Sales_PageInfo") IsNot Nothing Then
            If Session("MST_Sales_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("MST_Sales_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("MST_Sales_Search") IsNot Nothing Then
            If Session("MST_Sales_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("MST_Sales_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("MST_Sales_pageLength") IsNot Nothing Then
            If Session("MST_Sales_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("MST_Sales_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If
    End Sub
#End Region

#Region "Radio Button List"
    Public Sub LoadRadioButtonList()
        Call LoadActive(rdbsActive)
        Call LoadActive(rdbaActive)
    End Sub
    Public Sub LoadActive(ByVal rdb As RadioButtonList)
        rdb.Items.Insert(0, New ListItem(GetResource("Active", "Text", hddParameterMenuID.Value), "1"))
        rdb.Items.Insert(1, New ListItem(GetResource("Inactive", "Text", hddParameterMenuID.Value), "2"))
    End Sub
#End Region

#Region "Loaddata"
    Public Sub LoadInit()
        lblMain1.Text = Me.GetResource("main_label", "Text", "1")
        lblMain2.Text = Me.GetResource("lblMain2", "Text", hddParameterMenuID.Value)
        lblMain3.Text = Me.GetResource("lblMain3", "Text", hddParameterMenuID.Value)
        lblMain4.Text = Me.GetResource("lblMain4", "Text", hddParameterMenuID.Value)

        lblsSearch.Text = Me.GetResource("lblsSearch", "Text", hddParameterMenuID.Value)
        lblsCancel2.Text = Me.GetResource("msg_cancel_data", "MSG")

        lblsSalesGroup.Text = Me.GetResource("lblsSalesGroup", "Text", hddParameterMenuID.Value)
        lblsDivision.Text = Me.GetResource("lblsDivision", "Text", hddParameterMenuID.Value)
        lblsDepartment.Text = Me.GetResource("lblsDepartment", "Text", hddParameterMenuID.Value)
        lblsSection.Text = Me.GetResource("lblsSection", "Text", hddParameterMenuID.Value)
        lblsZone.Text = Me.GetResource("lblsZone", "Text", hddParameterMenuID.Value)
        lblsActive.Text = Me.GetResource("lblsActive", "Text", hddParameterMenuID.Value)

        TextHd1.Text = Me.GetResource("TextHd1", "Text", hddParameterMenuID.Value)
        TextHd2.Text = Me.GetResource("TextHd2", "Text", hddParameterMenuID.Value)
        TextHd3.Text = Me.GetResource("TextHd3", "Text", hddParameterMenuID.Value)
        TextHd4.Text = Me.GetResource("TextHd4", "Text", hddParameterMenuID.Value)
        TextHd5.Text = Me.GetResource("TextHd5", "Text", hddParameterMenuID.Value)
        TextHd6.Text = Me.GetResource("TextHd6", "Text", hddParameterMenuID.Value)
        TextHd7.Text = Me.GetResource("TextHd7", "Text", hddParameterMenuID.Value)
        TextHd8.Text = Me.GetResource("TextHd8", "Text", hddParameterMenuID.Value)
        TextHd9.Text = Me.GetResource("TextHd9", "Text", hddParameterMenuID.Value)
        TextHd10.Text = Me.GetResource("col_edit", "Text", "1")
        TextHd11.Text = Me.GetResource("col_delete", "Text", "1")

        TextFt1.Text = Me.GetResource("TextFt1", "Text", hddParameterMenuID.Value)
        TextFt2.Text = Me.GetResource("TextFt2", "Text", hddParameterMenuID.Value)
        TextFt3.Text = Me.GetResource("TextFt3", "Text", hddParameterMenuID.Value)
        TextFt4.Text = Me.GetResource("TextFt4", "Text", hddParameterMenuID.Value)
        TextFt5.Text = Me.GetResource("TextFt5", "Text", hddParameterMenuID.Value)
        TextFt6.Text = Me.GetResource("TextFt6", "Text", hddParameterMenuID.Value)
        TextFt7.Text = Me.GetResource("TextFt7", "Text", hddParameterMenuID.Value)
        TextFt8.Text = Me.GetResource("TextFt8", "Text", hddParameterMenuID.Value)
        TextFt9.Text = Me.GetResource("TextFt9", "Text", hddParameterMenuID.Value)
        TextFt10.Text = Me.GetResource("col_edit", "Text", "1")
        TextFt11.Text = Me.GetResource("col_delete", "Text", "1")

        lblaSaleManCode.Text = Me.GetResource("lblaSaleManCode", "Text", hddParameterMenuID.Value)
        lblaUserID.Text = Me.GetResource("lblaUserID", "Text", hddParameterMenuID.Value)
        lblaNameEn.Text = Me.GetResource("lblaNameEn", "Text", hddParameterMenuID.Value)
        lblaNameTH.Text = Me.GetResource("lblaNameTH", "Text", hddParameterMenuID.Value)
        lblaSalesGroup.Text = Me.GetResource("lblaSalesGroup", "Text", hddParameterMenuID.Value)
        lblaDivision.Text = Me.GetResource("lblaDivision", "Text", hddParameterMenuID.Value)
        lblaDepartment.Text = Me.GetResource("lblaDepartment", "Text", hddParameterMenuID.Value)
        lblaSection.Text = Me.GetResource("lblaSection", "Text", hddParameterMenuID.Value)
        lblaZone.Text = Me.GetResource("lblaZone", "Text", hddParameterMenuID.Value)
        lblaProject.Text = Me.GetResource("lblaProject", "Text", hddParameterMenuID.Value)
        lblaActive.Text = Me.GetResource("lblsActive", "Text", hddParameterMenuID.Value)

        lblMassage1.Text = Me.GetResource("lblMassage1", "Text", hddParameterMenuID.Value)
        lblMassage2.Text = Me.GetResource("lblMassage2", "Text", hddParameterMenuID.Value)
        lblMassage3.Text = Me.GetResource("lblMassage3", "Text", hddParameterMenuID.Value)
        lblMassage4.Text = Me.GetResource("lblMassage4", "Text", hddParameterMenuID.Value)
        lblMassage5.Text = Me.GetResource("lblMassage5", "Text", hddParameterMenuID.Value)
        lblMassage6.Text = Me.GetResource("lblMassage6", "Text", hddParameterMenuID.Value)
        lblMassage7.Text = Me.GetResource("lblMassage7", "Text", hddParameterMenuID.Value)
        lblMassage8.Text = Me.GetResource("lblMassage8", "Text", hddParameterMenuID.Value)
        lblMassage9.Text = Me.GetResource("lblMassage9", "Text", hddParameterMenuID.Value)
        lblMassage10.Text = Me.GetResource("lblMassage10", "Text", hddParameterMenuID.Value)

        'lblsSave.Text = Me.GetResource("btnSaveTemp", "Text", hddParameterMenuID.Value)
        'lblsCancel.Text = Me.GetResource("btnCencel", "Text", hddParameterMenuID.Value)

        hddMSGSaveData.Value = Me.GetResource("msg_save_data", "MSG")
        hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG")
        hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG")
        hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG")
        hddMSGAddData.Value = Me.GetResource("msg_add_data", "MSG")

        btnSaveData.Title = Me.GetResource("msg_save_data", "MSG")
        btnCancelData.Title = Me.GetResource("msg_cancel_data", "MSG")
        btnDeleteConfrim.InnerText = hddMSGDeleteData.Value
        btnCancelConfrim.InnerText = hddMSGCancelData.Value
        btnAddHref.Title = hddMSGAddData.Value

        lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG")
        lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG") & " " & Me.GetResource("lblaSaleManCode", "Text", hddParameterMenuID.Value)

        rdbsActive.SelectedIndex = 0

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

            Dim fillter As FillterSearch = New FillterSearch
            Dim bl As cSalesMan = New cSalesMan

            fillter.Keyword = String.Empty
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType

            Dim lcSalesMan As List(Of SalesMan_ViewModel) = bl.Loaddata(fillter,
                                                                        Me.TotalRow,
                                                                        ddlsSalesGroup.SelectedValue,
                                                                        ddlsDivision.SelectedValue,
                                                                        ddlsDepartment.SelectedValue,
                                                                        ddlsSection.SelectedValue,
                                                                        ddlsZone.SelectedValue,
                                                                        rdbsActive.SelectedValue,
                                                                        Me.WebCulture.ToString.ToUpper)

            If lcSalesMan IsNot Nothing Then
                Call SetDataSaleMan(lcSalesMan)
            Else
                Call SetDataSaleMan(Nothing)
            End If

            Session.Remove("MST_Sales_SalesGroup")
            Session.Remove("MST_Sales_Division")
            Session.Remove("MST_Sales_Department")
            Session.Remove("MST_Sales_Section")
            Session.Remove("MST_Sales_Zone")
            Session.Remove("MST_Sales_Active")
            Session.Remove("MST_Sales_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)
            Session.Remove("MST_Sales_SalesGroup")
            Session.Remove("MST_Sales_Division")
            Session.Remove("MST_Sales_Department")
            Session.Remove("MST_Sales_Section")
            Session.Remove("MST_Sales_Zone")
            Session.Remove("MST_Sales_Active")
            Session.Remove("MST_Sales_FS")
        End Try
    End Sub
    Public Sub ClearText()
        txtaSaleManCode.Text = String.Empty
        txtaUserID.Text = String.Empty
        txtaNameEN.Text = String.Empty
        txtaNameTH.Text = String.Empty
        ddlaSalesGroup.SelectedIndex = 0
        ddlaDivision.SelectedIndex = 0
        ddlaDepartment.SelectedIndex = 0
        ddlaSection.SelectedIndex = 0
        ddlaZone.SelectedIndex = 0
        txtaProject.Text = String.Empty
        rdbaActive.SelectedIndex = 0
    End Sub

    Sub getData()

        If txtaProject.Text <> String.Empty Then
            Call CreateDatatableProject(dtProject)
            Call ConvertDatatableProject(txtaProject.Text, dtProject)
        End If

    End Sub

    Public Sub ClearFilter()
        ddlsSalesGroup.SelectedIndex = 0
        ddlsDivision.SelectedIndex = 0
        ddlsDepartment.SelectedIndex = 0
        ddlsSection.SelectedIndex = 0
        ddlsZone.SelectedIndex = 0
        rdbsActive.SelectedIndex = 0
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

            txtaSaleManCode.Enabled = False

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try
            Call ClearText()
            Dim bl As cSalesMan = New cSalesMan
            Dim lc As SalesMan_ViewModel = bl.LoadEditSalesMan(hddKeyID.Value,
                                                               Me.WebCulture.ToString.ToUpper)
            If lc IsNot Nothing Then
                txtaSaleManCode.Text = lc.SalesManCode
                txtaUserID.Text = lc.UserID
                txtaNameEN.Text = lc.NameEN
                txtaNameTH.Text = lc.NameTH
                Try
                    If lc.TypeCode <> String.Empty Then
                        ddlaSalesGroup.SelectedValue = lc.TypeCode
                    End If
                Catch ex As Exception
                    ddlaSalesGroup.SelectedIndex = 0
                End Try
                Try
                    If lc.DivCode <> String.Empty Then
                        ddlaDivision.SelectedValue = lc.DivCode
                    End If
                Catch ex As Exception
                    ddlaDivision.SelectedIndex = 0
                End Try
                Try
                    If lc.DepCode <> String.Empty Then
                        ddlaDepartment.SelectedValue = lc.DepCode
                    End If
                Catch ex As Exception
                    ddlaDepartment.SelectedIndex = 0
                End Try
                Try
                    If lc.SectionCode <> String.Empty Then
                        ddlaSection.SelectedValue = lc.SectionCode
                    End If
                Catch ex As Exception
                    ddlaSection.SelectedIndex = 0
                End Try
                Try
                    If lc.ZoneCode <> String.Empty Then
                        ddlaZone.SelectedValue = lc.ZoneCode
                    End If
                Catch ex As Exception
                    ddlaZone.SelectedIndex = 0
                End Try
                If lc.status IsNot Nothing Then
                    If lc.status = 1 Then
                        rdbaActive.SelectedIndex = 0
                    Else
                        rdbaActive.SelectedIndex = 1
                    End If
                End If

                txtaProject.Text = lc.ProjectCode

                If txtaProject.Text <> String.Empty Then
                    Call CreateDatatableProject(dtProject)
                    Call ConvertDatatableProject(txtaProject.Text, dtProject)
                End If


                txtaSaleManCode.Enabled = False
            End If

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim bl As cSalesMan = New cSalesMan
            Dim lc As SalesMan_ViewModel = bl.LoadEditSalesMan(hddKeyID.Value,
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
            Dim bl As cSalesMan = New cSalesMan
            If hddKeyID.Value = String.Empty Then
                'Dim lc As SalesMan_ViewModel = bl.LoadEditSalesMan(txtaSaleManCode.Text,
                '                                                   Me.WebCulture.ToString.ToUpper)
                'If lc Is Nothing Then
                If Not bl.Add(txtaSaleManCode.Text,
                                txtaUserID.Text,
                                txtaNameEN.Text,
                                txtaNameTH.Text,
                                ddlaSalesGroup.SelectedValue,
                                ddlaDivision.SelectedValue,
                                ddlaDepartment.SelectedValue,
                                ddlaZone.SelectedValue,
                                ddlaSection.SelectedValue,
                                txtaProject.Text,
                                rdbaActive.SelectedValue,
                                Me.CurrentUser.UserID) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                Else
                    hddKeyID.Value = txtaSaleManCode.Text
                    Call getData()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                End If
                'Else
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_duplicate", "MSG") & "');", True)
                'End If
            Else
                If Not bl.Edit(hddKeyID.Value,
                               txtaUserID.Text,
                               txtaNameEN.Text,
                               txtaNameTH.Text,
                               ddlaSalesGroup.SelectedValue,
                               ddlaDivision.SelectedValue,
                               ddlaDepartment.SelectedValue,
                               ddlaZone.SelectedValue,
                               ddlaSection.SelectedValue,
                               txtaProject.Text,
                               rdbaActive.SelectedValue,
                               Me.CurrentUser.UserID) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                Else
                    Call getData()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
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
    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            Call ClearFilter()
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnCancel_Click", ex)
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

#Region "Session"
    Public Function GetDataSaleMan() As List(Of SalesMan_ViewModel)
        Try
            Return Session("IDOCS.application.LoaddataSalesMan")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataSaleMan(ByVal lcSalesMan As List(Of SalesMan_ViewModel)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataSalesMan", lcSalesMan)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region


#Region "Datatable"
    Public dtProject As New DataTable
    Public Sub CreateDatatableProject(ByRef dt As DataTable)
        dt = New DataTable
        dt.Columns.Add("FREPRJNO")
    End Sub

    Public Sub ConvertDatatableProject(ByVal lc As String,
                                ByRef dt As DataTable)
        Try

            Dim dr As DataRow
            Dim lcArr() As String = lc.Split(",")
            For i As Integer = 0 To lcArr.Length - 1
                dr = dt.NewRow
                dr.Item("FREPRJNO") = lcArr(i).ToString
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "ConvertDatatableProject", ex)
            Dim message As String = "OpenSaveDialog('" + GetResource("msg_alert", "MSG", "0") + "','" + ex.Message.ToString + "','n');"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "callScriptFunction", message, True)
        End Try
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
        lblMassage7.Style.Add("display", "none")
        lblMassage8.Style.Add("display", "none")
        lblMassage9.Style.Add("display", "none")
        lblMassage10.Style.Add("display", "none")
    End Sub
#End Region

#Region "Check Master Delete"
    Public Sub getUsedMaster()
        Try
            Dim bl As cSalesMan = New cSalesMan
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
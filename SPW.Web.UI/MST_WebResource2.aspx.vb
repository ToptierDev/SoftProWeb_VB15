Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class MST_WebResource2
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.setMenuIDByMenuLocation("MST_WebResource2.aspx")
        Try
            If Not IsPostBack Then
                Page.DataBind()
                hddpLG.Value = Me.WebCulture.ToUpper()
                Session.Remove("PRR.application.LoadDataWebResource")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("MST_WebResource2.aspx")
                Me.ClearSessionPageLoad("MST_WebResource2.aspx")

                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call Loaddata()
                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();closeOverlay();", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub

#Region "DropDownList"
    Public Sub LoadDropdownlist()
        Try
            Call LoadModule(ddlsModule, "S")
            Call LoadModule(ddlaModule, "A")
            Call LoadMenu1(ddlsMenu, "S", ddlsModule.SelectedValue)
            Call LoadMenu1(ddlaMenu, "A", ddlaModule.SelectedValue)
            Call LoadMenu2(ddlsSubMenu, "S", ddlsMenu.SelectedValue, ddlsModule.SelectedValue)
            Call LoadMenu2(ddlaSubMenu, "A", ddlaMenu.SelectedValue, ddlaModule.SelectedValue)
            Call LoadMenu3(ddlsSubMenu1, "S", ddlsSubMenu.SelectedValue, ddlaModule.SelectedValue)
            Call LoadMenu3(ddlaSubMenu1, "A", ddlaSubMenu.SelectedValue, ddlsModule.SelectedValue)
            Call LoadType(ddlsType, "S")
            Call LoadType(ddlaType, "S")
            Call LoadBaseMassage(ddlsBaseMassage, "S")
            Call LoadBaseMassage(ddlaBaseMassage, "A")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDropdownlist", ex)
        End Try
    End Sub
    Public Sub LoadModule(ByVal ddl As DropDownList,
                          ByVal strType As String)
        Dim bl As cWebResouce = New cWebResouce
        ddl.Items.Clear()
        Try
            Dim lc = bl.LoadModule(Me.CurrentUser.UserID)
            If lc IsNot Nothing Then
                ddl.DataValueField = "ModuleID"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "ModuleNameEN"
                Else
                    ddl.DataTextField = "ModuleNameTH"
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
    Public Sub LoadMenu1(ByVal ddl As DropDownList,
                         ByVal strType As String,
                         ByVal strModule As String)
        Dim bl As cWebResouce = New cWebResouce
        ddl.Items.Clear()
        Try
            If strModule <> String.Empty Then
                Dim lc = bl.LoadMenu1(strModule)
                If lc IsNot Nothing Then
                    ddl.DataValueField = "MenuID"
                    If Me.CurrentPage.ToString.ToUpper = "EN" Then
                        ddl.DataTextField = "MenuNameEN"
                    Else
                        ddl.DataTextField = "MenuNameLC"
                    End If
                    ddl.DataSource = lc
                    ddl.DataBind()
                End If
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
    Public Sub LoadMenu2(ByVal ddl As DropDownList,
                         ByVal strType As String,
                         ByVal strMenu1 As String,
                         ByVal strModule As String)
        Dim bl As cWebResouce = New cWebResouce
        ddl.Items.Clear()
        Try
            If strMenu1 <> String.Empty Or
               strModule <> String.Empty Then
                Dim lc = bl.LoadMenu2(strMenu1,
                                      strModule)
                If lc IsNot Nothing Then
                    ddl.DataValueField = "MenuID"
                    If Me.CurrentPage.ToString.ToUpper = "EN" Then
                        ddl.DataTextField = "MenuNameEN"
                    Else
                        ddl.DataTextField = "MenuNameLC"
                    End If
                    ddl.DataSource = lc
                    ddl.DataBind()
                End If
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
    Public Sub LoadMenu3(ByVal ddl As DropDownList,
                         ByVal strType As String,
                         ByVal strMenu3 As String,
                         ByVal strModule As String)
        Dim bl As cWebResouce = New cWebResouce
        ddl.Items.Clear()
        Try
            If strMenu3 <> String.Empty Or
               strModule <> String.Empty Then
                Dim lc = bl.LoadMenu3(strMenu3,
                                      strModule)
                If lc IsNot Nothing Then
                    ddl.DataValueField = "MenuID"
                    If Me.CurrentPage.ToString.ToUpper = "EN" Then
                        ddl.DataTextField = "MenuNameEN"
                    Else
                        ddl.DataTextField = "MenuNameLC"
                    End If
                    ddl.DataSource = lc
                    ddl.DataBind()
                End If
            End If
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select_all", "MSG"), ""))
                'ddl.Items.Insert(1, New ListItem(GetResource("main_label", "Text", "1"), "1"))
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex)
        End Try
    End Sub
    Public Sub LoadType(ByVal ddl As DropDownList,
                        ByVal strType As String)
        Try
            ddl.Items.Clear()
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select", "MSG"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetResource("msg_select_all", "MSG"), ""))
            End If
            ddl.Items.Insert(1, New ListItem("Text", "Text"))
            ddl.Items.Insert(2, New ListItem("MSG", "MSG"))
            ddl.Items.Insert(3, New ListItem("Paging", "Paging"))
            ddl.Items.Insert(4, New ListItem("Sort", "Sort"))
            ddl.Items.Insert(5, New ListItem("Tooltip", "Tooltip"))
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex)
        End Try
    End Sub
    Public Sub LoadBaseMassage(ByVal ddl As DropDownList,
                               ByVal strType As String)
        Dim bl As cWebResouce = New cWebResouce
        ddl.Items.Clear()
        Try

            Dim lc = bl.LoadBaseMassage()
            If lc IsNot Nothing Then
                ddl.DataValueField = "MenuID"
                If Me.CurrentPage.ToString.ToUpper = "EN" Then
                    ddl.DataTextField = "MenuNameEN"
                Else
                    ddl.DataTextField = "MenuNameLC"
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
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadBaseMassage", ex)
        End Try
    End Sub
#End Region

#Region "Event DropDownlist"
    Protected Sub ddlsModule_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsModule.SelectedIndexChanged
        Call LoadMenu1(ddlsMenu, "S", ddlsModule.SelectedValue)
        Call LoadMenu2(ddlsSubMenu, "S", ddlsMenu.SelectedValue, ddlsModule.SelectedValue)
        Call LoadMenu3(ddlsSubMenu1, "S", ddlsSubMenu.SelectedValue, ddlsModule.SelectedValue)

        ddlsBaseMassage.SelectedIndex = 0
    End Sub
    Protected Sub ddlsMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsMenu.SelectedIndexChanged
        Call LoadMenu2(ddlsSubMenu, "S", ddlsMenu.SelectedValue, ddlsModule.SelectedValue)
        Call LoadMenu3(ddlsSubMenu1, "S", ddlsSubMenu.SelectedValue, ddlsModule.SelectedValue)

        ddlsBaseMassage.SelectedIndex = 0
    End Sub
    Protected Sub ddlsSubMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsSubMenu.SelectedIndexChanged
        Call LoadMenu3(ddlsSubMenu1, "S", ddlsSubMenu.SelectedValue, ddlsModule.SelectedValue)

        ddlsBaseMassage.SelectedIndex = 0
    End Sub
    Protected Sub ddlsBaseMassage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsBaseMassage.SelectedIndexChanged
        ddlsModule.SelectedIndex = 0
        Call LoadMenu1(ddlsMenu, "S", ddlsModule.SelectedValue)
        Call LoadMenu2(ddlsSubMenu, "S", ddlsMenu.SelectedValue, ddlsModule.SelectedValue)
        Call LoadMenu3(ddlsSubMenu1, "S", ddlsSubMenu.SelectedValue, ddlsModule.SelectedValue)
    End Sub
    Protected Sub ddlaModule_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlaModule.SelectedIndexChanged
        Call LoadMenu1(ddlaMenu, "A", ddlaModule.SelectedValue)
        Call LoadMenu2(ddlaSubMenu, "A", ddlaMenu.SelectedValue, ddlaModule.SelectedValue)
        Call LoadMenu3(ddlaSubMenu1, "A", ddlaSubMenu.SelectedValue, ddlaModule.SelectedValue)
    End Sub
    Protected Sub ddlaMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlaMenu.SelectedIndexChanged
        Call LoadMenu2(ddlaSubMenu, "A", ddlaMenu.SelectedValue, ddlaModule.SelectedValue)
        Call LoadMenu3(ddlaSubMenu1, "A", ddlaSubMenu.SelectedValue, ddlaModule.SelectedValue)
    End Sub
    Protected Sub ddlaSubMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlaSubMenu.SelectedIndexChanged
        Call LoadMenu3(ddlaSubMenu1, "A", ddlaSubMenu.SelectedValue, ddlaModule.SelectedValue)
    End Sub
#End Region

#Region "Event CheckBox"
    Protected Sub chksCopyToMenu_CheckedChanged(sender As Object, e As EventArgs) Handles chksCopyToMenu.CheckedChanged
        If chksCopyToMenu.Checked Then
            rowBaseMassage.Attributes.Add("style", "display:none")
            rowModule.Attributes.Add("style", "display:block")
            rowMenu.Attributes.Add("style", "display:block")
            rowSubMenu.Attributes.Add("style", "display:block")
            rowSubMenu1.Attributes.Add("style", "display:block")
            br1.Attributes.Add("style", "display:none")
            br2.Attributes.Add("style", "display:block")
            br3.Attributes.Add("style", "display:block")
            br4.Attributes.Add("style", "display:block")
            br5.Attributes.Add("style", "display:block")
            ddlaBaseMassage.SelectedIndex = 0
            hddID.Value = String.Empty

            ddlaModule.Enabled = True
            ddlaMenu.Enabled = True
            ddlaSubMenu.Enabled = True
            ddlaSubMenu1.Enabled = True
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

        lblsMenu.Text = Me.GetResource("lblsMenu", "Text", hddParameterMenuID.Value)
        lblsSubMenu.Text = Me.GetResource("lblsSubMenu", "Text", hddParameterMenuID.Value)
        lblsSubMenu1.Text = Me.GetResource("lblsSubMenu1", "Text", hddParameterMenuID.Value)
        lblsType.Text = Me.GetResource("lblsType", "Text", hddParameterMenuID.Value)
        lblsModule.Text = Me.GetResource("lblsModule", "Text", hddParameterMenuID.Value)
        lblsSearchTH.Text = Me.GetResource("lblsSearchTH", "Text", hddParameterMenuID.Value)
        lblsSearchEN.Text = Me.GetResource("lblsSearchEN", "Text", hddParameterMenuID.Value)
        lblsBaseMassage.Text = Me.GetResource("lblsBaseMassage", "Text", hddParameterMenuID.Value)
        lblaBaseMassage.Text = Me.GetResource("lblaBaseMassage", "Text", hddParameterMenuID.Value)

        lblaMenu.Text = Me.GetResource("lblaMenu", "Text", hddParameterMenuID.Value)
        lblaSubMenu.Text = Me.GetResource("lblaSubMenu", "Text", hddParameterMenuID.Value)
        lblaSubMenu1.Text = Me.GetResource("lblaSubMenu1", "Text", hddParameterMenuID.Value)
        lblaType.Text = Me.GetResource("lblaType", "Text", hddParameterMenuID.Value)
        lblaResourceName.Text = Me.GetResource("lblaResourceName", "Text", hddParameterMenuID.Value)
        lblaResourceValueEN.Text = Me.GetResource("lblaResourceValueEN", "Text", hddParameterMenuID.Value)
        lblaResourceValueLC.Text = Me.GetResource("lblaResourceValueLC", "Text", hddParameterMenuID.Value)
        lblaModule.Text = Me.GetResource("lblaModule", "Text", hddParameterMenuID.Value)
        chksCopyToMenu.Text = Me.GetResource("chksCopyToMenu", "Text", hddParameterMenuID.Value)

        lblMassage1.Text = Me.GetResource("msg_required", "MSG")
        lblMassage2.Text = Me.GetResource("msg_required", "MSG")
        lblMassage3.Text = Me.GetResource("msg_required", "MSG")
        lblMassage4.Text = Me.GetResource("msg_required", "MSG")
        lblMassage5.Text = Me.GetResource("msg_required", "MSG")
        lblMassage6.Text = Me.GetResource("msg_required", "MSG")
        lblMassage7.Text = Me.GetResource("msg_required", "MSG")

        hddReloadGrid.Value = String.Empty

        hrefSave.Title = Me.GetResource("msg_save_data", "MSG")
        hrefCencel.Title = Me.GetResource("msg_cancel_data", "MSG")
        hrefAdd.Title = Me.GetResource("msg_add_data", "MSG")

        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try
            Dim fillter As FillterSearch = New FillterSearch
            Dim bl As cWebResouce = New cWebResouce
            fillter.Keyword = ""

            Dim lst As List(Of WebResource_ViewModel) = bl.LoaddataWebResource(fillter,
                                                                               Me.TotalRow,
                                                                               ddlsMenu.SelectedValue,
                                                                               ddlsSubMenu.SelectedValue,
                                                                               ddlsSubMenu1.SelectedValue,
                                                                               ddlsType.SelectedValue,
                                                                               ddlsModule.SelectedValue,
                                                                               txtsSearchTH.Text,
                                                                               txtsSearchEN.Text,
                                                                               ddlsBaseMassage.SelectedValue)

            If lst IsNot Nothing Then
                Call LoadDataWebResource(lst)
            Else
                Call LoadDataWebResource(Nothing)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)
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
#End Region

#Region "Event"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            hddReloadGrid.Value = "1"
            Call Loaddata()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSearch_Click", ex)
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Call ControlValidate()
            rowBaseMassage.Attributes.Add("style", "display:none")
            rowModule.Attributes.Add("style", "display:block")
            rowMenu.Attributes.Add("style", "display:block")
            rowSubMenu.Attributes.Add("style", "display:block")
            rowSubMenu1.Attributes.Add("style", "display:block")
            br1.Attributes.Add("style", "display:none")
            br2.Attributes.Add("style", "display:block")
            br3.Attributes.Add("style", "display:block")
            br4.Attributes.Add("style", "display:block")
            br5.Attributes.Add("style", "display:block")
            ddlaBaseMassage.SelectedIndex = 0
            Try
                If ddlsModule.SelectedIndex <> 0 Then
                    ddlaModule.SelectedValue = ddlsModule.SelectedValue
                Else
                    ddlaModule.SelectedIndex = 0
                End If
                Call LoadMenu1(ddlaMenu, "A", ddlaModule.SelectedValue)
                If ddlsMenu.SelectedIndex <> 0 Then
                    ddlaMenu.SelectedValue = ddlsMenu.SelectedValue
                Else
                    ddlaMenu.SelectedIndex = 0
                End If
                Call LoadMenu2(ddlaSubMenu, "A", ddlaMenu.SelectedValue, ddlaModule.SelectedValue)
                If ddlsSubMenu.SelectedIndex <> 0 Then
                    ddlaSubMenu.SelectedValue = ddlsSubMenu.SelectedValue
                Else
                    ddlaSubMenu.SelectedIndex = 0
                End If
                Call LoadMenu3(ddlaSubMenu1, "A", ddlaSubMenu.SelectedValue, ddlaModule.SelectedValue)
                If ddlsSubMenu1.SelectedIndex <> 0 Then
                    ddlaSubMenu1.SelectedValue = ddlsSubMenu1.SelectedValue
                Else
                    ddlaSubMenu1.SelectedIndex = 0
                End If
                If ddlaMenu.SelectedIndex <> 0 Then
                    ddlaSubMenu.Items.Insert(1, New ListItem(ddlaMenu.SelectedItem.Text, ddlaMenu.SelectedValue))
                End If
                If ddlsType.SelectedIndex <> 0 Then
                    ddlaType.SelectedValue = ddlsType.SelectedValue
                Else
                    ddlaType.SelectedIndex = 0
                End If
            Catch ex As Exception
                ddlaModule.SelectedIndex = 0
                ddlaMenu.SelectedIndex = 0
                ddlaSubMenu.SelectedIndex = 0
            End Try

            ddlaModule.Enabled = True
            ddlaMenu.Enabled = True
            ddlaSubMenu.Enabled = True
            ddlaSubMenu1.Enabled = True
            txtaResourceName.Enabled = True
            ddlaType.Enabled = True

            hddID.Value = String.Empty
            txtaResourceName.Text = String.Empty
            ddlaType.SelectedIndex = 0
            txtaResourceValueLC.Text = String.Empty
            txtaResourceValueEN.Text = String.Empty

            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub
    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If hddpEdit.Value <> String.Empty Then
            Dim bl As cWebResouce = New cWebResouce
            Dim lcWebresource As CoreWebResource = bl.LoadEditWebResource(hddpEdit.Value)
            If lcWebresource IsNot Nothing Then
                hddID.Value = lcWebresource.ResourceID

                Dim lcMenu As CoreMenu = bl.LoadMenuParentBySubMenu(lcWebresource.MenuID)

                Try
                    If lcMenu.ModuleID IsNot Nothing Then
                        ddlaModule.SelectedValue = lcMenu.ModuleID
                    Else
                        ddlaModule.SelectedIndex = 0
                    End If

                    Call LoadMenu3(ddlaSubMenu1, "A", ddlaSubMenu.SelectedValue, ddlaModule.SelectedValue)
                    If ddlaSubMenu1.Items.Count > 1 Then
                        If lcMenu.MenuID <> Nothing Then
                            ddlaSubMenu1.SelectedValue = lcMenu.MenuID
                        Else
                            ddlaSubMenu1.SelectedIndex = 0
                        End If

                        Call LoadMenu2(ddlaSubMenu, "A", ddlaMenu.SelectedValue, ddlaModule.SelectedValue)
                        If lcMenu.MenuID <> Nothing Then
                            ddlaSubMenu.SelectedValue = lcMenu.ParentID
                        Else
                            ddlaSubMenu.SelectedIndex = 0
                        End If

                        Call LoadMenu1(ddlaMenu, "A", ddlaModule.SelectedValue)
                        Dim lcMenu1 As CoreMenu = bl.LoadMenuNotParentEdit(lcMenu.ParentID)
                        ddlaMenu.SelectedIndex = 0
                        If lcMenu1 IsNot Nothing Then
                            If lcMenu1.ParentID IsNot Nothing Then
                                ddlaMenu.SelectedValue = lcMenu1.ParentID
                            End If
                        End If
                    Else
                        Call LoadMenu2(ddlaSubMenu, "A", ddlaMenu.SelectedValue, ddlaModule.SelectedValue)
                        If lcMenu.MenuID <> Nothing Then
                            ddlaSubMenu.SelectedValue = lcMenu.MenuID
                        Else
                            ddlaSubMenu.SelectedIndex = 0
                        End If
                        Call LoadMenu1(ddlaMenu, "A", ddlaModule.SelectedValue)
                        If lcMenu.ParentID IsNot Nothing Then
                            ddlaMenu.SelectedValue = lcMenu.ParentID
                        Else
                            ddlaMenu.SelectedIndex = 0
                        End If
                        'If ddlaMenu.SelectedIndex <> 0 Then
                        '    ddlaSubMenu.Items.Insert(1, New ListItem(ddlaMenu.SelectedItem.Text, ddlaMenu.SelectedValue))
                        'End If
                        'If lcMenu.MenuID = 99999 Or lcMenu.MenuID = 1 Then
                        '    ddlaMenu.SelectedValue = lcMenu.MenuID
                        'End If
                    End If
                Catch ex As Exception
                    ddlaModule.SelectedIndex = 0
                    ddlaMenu.SelectedIndex = 0
                    ddlaSubMenu.SelectedIndex = 0
                End Try

                txtaResourceName.Text = lcWebresource.ResourceName
                Try
                    ddlaType.SelectedValue = lcWebresource.ResourceType
                Catch ex As Exception
                    ddlaType.SelectedIndex = 0
                End Try
                txtaResourceValueLC.Text = lcWebresource.ResourceValueLC
                txtaResourceValueEN.Text = lcWebresource.ResourceValueEN

                ddlaModule.Enabled = False
                ddlaMenu.Enabled = False
                ddlaSubMenu.Enabled = False
                ddlaSubMenu1.Enabled = False
                txtaResourceName.Enabled = False
                ddlaType.Enabled = False

                chksCopyToMenu.Checked = False
                If lcWebresource.MenuID = 1 Or lcWebresource.MenuID = 99999 Then
                    Try
                        ddlaBaseMassage.SelectedIndex = lcWebresource.MenuID
                    Catch ex As Exception
                        ddlaBaseMassage.SelectedIndex = 0
                    End Try
                    rowBaseMassage.Attributes.Add("style", "display:block")
                    rowModule.Attributes.Add("style", "display:none")
                    rowMenu.Attributes.Add("style", "display:none")
                    rowSubMenu.Attributes.Add("style", "display:none")
                    rowSubMenu1.Attributes.Add("style", "display:none")
                    br1.Attributes.Add("style", "display:block")
                    br2.Attributes.Add("style", "display:none")
                    br3.Attributes.Add("style", "display:none")
                    br4.Attributes.Add("style", "display:none")
                    br5.Attributes.Add("style", "display:none")
                Else
                    ddlaBaseMassage.SelectedIndex = 0
                    rowBaseMassage.Attributes.Add("style", "display:none")
                    rowModule.Attributes.Add("style", "display:block")
                    rowMenu.Attributes.Add("style", "display:block")
                    rowSubMenu.Attributes.Add("style", "display:block")
                    rowSubMenu1.Attributes.Add("style", "display:block")
                    br1.Attributes.Add("style", "display:none")
                    br2.Attributes.Add("style", "display:block")
                    br3.Attributes.Add("style", "display:block")
                    br4.Attributes.Add("style", "display:block")
                    br5.Attributes.Add("style", "display:block")
                End If


            End If
            Call OpenDialog()
        End If
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Call OpenMain()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnCencel_Click", ex)
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            hddReloadGrid.Value = String.Empty
            Dim succ As Boolean = True
            Dim bl As cWebResouce = New cWebResouce
            Dim data As CoreWebResource = Nothing
            If hddID.Value <> "" And Not chksCopyToMenu.Checked Then
                If Not bl.EditWebResource(hddID.Value,
                                          IIf(ddlaSubMenu1.Items.Count > 1, ddlaSubMenu1.SelectedValue, ddlaSubMenu.SelectedValue),
                                          txtaResourceName.Text,
                                          ddlaType.SelectedValue,
                                          txtaResourceValueEN.Text,
                                          txtaResourceValueLC.Text,
                                          Me.CurrentUser.UserID) Then
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                Else
                    Call Loaddata()
                    Call OpenMain()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                End If
            Else
                If Not bl.AddWebResource(IIf(ddlaSubMenu1.Items.Count > 1, ddlaSubMenu1.SelectedValue, ddlaSubMenu.SelectedValue),
                                         txtaResourceName.Text,
                                         ddlaType.SelectedValue,
                                         txtaResourceValueEN.Text,
                                         txtaResourceValueLC.Text,
                                         Me.CurrentUser.UserID) Then
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                Else
                    Call Loaddata()
                    Call OpenMain()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSave_Click", ex)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
        End Try
    End Sub
#End Region

#Region "ControlPanel"
    Public Sub OpenMain()
        hddReloadGrid.Value = "1"
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
        lblMassage6.Style.Add("display", "none")
        lblMassage7.Style.Add("display", "none")
    End Sub
#End Region

#Region "Session"
    Public Function LoadDataWebResource() As List(Of WebResource_ViewModel)
        Try
            Return Session("PRR.application.LoadDataWebResource")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function LoadDataWebResource(ByVal pWebResource As List(Of WebResource_ViewModel)) As Boolean
        Try
            Session.Add("PRR.application.LoadDataWebResource", pWebResource)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region
End Class
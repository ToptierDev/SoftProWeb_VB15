Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Public Class MST_WebResource
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hddpLG.Value = Me.WebCulture.ToUpper()
                Session.Remove("PRR.application.LoadDataWebResource")
                hddParameterMenuID.Value = HelperLog.LoadMenuID("MST_WebResource.aspx")
                Me.ClearSessionPageLoad("MST_WebResource.aspx")

                Call LoadInit()
                Call DefaultAllData()
                Call LoadGridDefault()
                Call LoadDropdownlist()
                Call GetParameter()
                Call Loaddata()
                HelperLog.AccessLog(Me.CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress())
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "AutocompletedMenu(this,event);AutocompletedSubMenu(this,event);", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub

#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        Call SetParameter(FlagSave)
        If hddpFlagSearch.Value = "1" Then
            Session.Remove("MST_WebResource_PageInfo")
            Session.Remove("MST_WebResource_Search")
            Session.Remove("MST_WebResource_pageLength")
        End If
        Call Redirect("MST_WebResource.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Dim strParameter As String = String.Empty
        Session.Add("MST_WebResource_Menu", IIf(txtsMenu.Text <> String.Empty, hddsMenu.Value & "|" & txtsMenu.Text, ""))
        Session.Add("MST_WebResource_SubMenu", IIf(txtsSubMenu.Text <> String.Empty, hddsSubMenu.Value & "|" & txtsSubMenu.Text, ""))
        Session.Add("MST_WebResource_Type", IIf(ddlsType.SelectedIndex <> 0, ddlsType.SelectedValue, ""))
        Session.Add("MST_WebResource_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("MST_WebResource_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("MST_WebResource_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("MST_WebResource_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
    End Sub
    Public Sub GetParameter()
        If Session("MST_WebResource_Menu") IsNot Nothing And
           Session("MST_WebResource_SubMenu") IsNot Nothing And
           Session("MST_WebResource_Type") IsNot Nothing Then
            hddReloadGrid.Value = "1"
            If Session("MST_WebResource_Menu").ToString <> String.Empty Then
                Dim strMenu As String = Session("MST_WebResource_Menu").ToString
                txtsMenu.Text = strMenu.Split("|")(1)
                hddsMenu.Value = strMenu.Split("|")(0)
            End If
            If Session("MST_WebResource_SubMenu").ToString <> String.Empty Then
                Dim strSubMenu As String = Session("MST_WebResource_SubMenu").ToString
                txtsSubMenu.Text = strSubMenu.Split("|")(1)
                hddsSubMenu.Value = strSubMenu.Split("|")(0)
            End If
            If Session("MST_WebResource_Type").ToString <> String.Empty Then
                Dim strType As String = Session("MST_WebResource_Type").ToString
                Try
                    ddlsType.SelectedValue = strType
                Catch ex As Exception
                    ddlsType.SelectedIndex = 0
                End Try
            End If
            If Session("MST_WebResource_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("MST_WebResource_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');", True)
                End If
            End If
        End If
        If Session("MST_WebResource_PageInfo") IsNot Nothing Then
            If Session("MST_WebResource_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("MST_WebResource_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("MST_WebResource_Search") IsNot Nothing Then
            If Session("MST_WebResource_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("MST_WebResource_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("MST_WebResource_pageLength") IsNot Nothing Then
            If Session("MST_WebResource_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("MST_WebResource_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If
    End Sub

#End Region

#Region "DropDownList"
    Public Sub LoadDropdownlist()
        Try
            Call LoadType(ddlsType, "S")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDropdownlist", ex)
        End Try
    End Sub

    Public Sub LoadType(ByVal ddl As DropDownList,
                        ByVal strType As String)
        Try
            ddl.Items.Clear()
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select_all", "MSG", "1"), ""))
            End If
            ddl.Items.Insert(1, New ListItem("Text", "Text"))
            ddl.Items.Insert(2, New ListItem("MSG", "MSG"))
            ddl.Items.Insert(3, New ListItem("Paging", "Paging"))
            ddl.Items.Insert(4, New ListItem("Sort", "Sort"))
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadType", ex)
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

        lblsSearch.Text = Me.GetResource("lblsSearch", "Text", hddParameterMenuID.Value)
        lblsMenu.Text = Me.GetResource("lblsMenu", "Text", hddParameterMenuID.Value)
        lblsSubMenu.Text = Me.GetResource("lblsSubMenu", "Text", hddParameterMenuID.Value)
        lblsType.Text = Me.GetResource("lblsType", "Text", hddParameterMenuID.Value)
        'lblsKeyword.Text = Me.GetResource("lblsKeyword", "Text", hddParameterMenuID.Value)
        'lblsKeywordMsg.Text = Me.GetResource("lblsKeywordMsg", "Text", hddParameterMenuID.Value) & _
        '                      Me.GetResource("ResourceName", "Text", hddParameterMenuID.Value) & ", " & _
        '                      Me.GetResource("ResourceValueEN", "Text", hddParameterMenuID.Value) & ", " & _
        '                      Me.GetResource("ResourceValueLC", "Text", hddParameterMenuID.Value)

        lblaMenu.Text = Me.GetResource("lblaMenu", "Text", hddParameterMenuID.Value)
        lblaSubMenu.Text = Me.GetResource("lblaSubMenu", "Text", hddParameterMenuID.Value)
        lblaType.Text = Me.GetResource("lblaType", "Text", hddParameterMenuID.Value)
        lblaResourceName.Text = Me.GetResource("lblaResourceName", "Text", hddParameterMenuID.Value)
        lblaResourceValueEN.Text = Me.GetResource("lblaResourceValueEN", "Text", hddParameterMenuID.Value)
        lblaResourceValueLC.Text = Me.GetResource("lblaResourceValueLC", "Text", hddParameterMenuID.Value)

        'lblsSave.Text = Me.GetResource("btn_save", "Text", "1")
        'lblsCancel.Text = Me.GetResource("btn_cancel", "Text", "1")

        lblMassage1.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage2.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage3.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage4.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage5.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage6.Text = Me.GetResource("msg_required", "MSG", "1")

        hddReloadGrid.Value = String.Empty

        hrefSave.Title = Me.GetResource("msg_save_data", "MSG", "1")
        hrefCencel.Title = Me.GetResource("msg_cancel_data", "MSG", "1")
        hrefAdd.Title = Me.GetResource("msg_add_data", "MSG", "1")

        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try


            Dim fillter As FillterSearch = New FillterSearch
            Dim bl As cWebResouce = New cWebResouce
            fillter.Keyword = ""

            Dim lst As List(Of WebResource_ViewModel) = bl.LoaddataWebResource(fillter,
                                                                               Me.TotalRow,
                                                                               hddsMenu.Value,
                                                                               hddsSubMenu.Value,
                                                                               ddlsType.SelectedValue,
                                                                               "",
                                                                               "")

            If lst IsNot Nothing Then
                Call LoadDataWebResource(lst)
            Else
                Call LoadDataWebResource(Nothing)
            End If
            Session.Remove("MST_WebResource_Menu")
            Session.Remove("MST_WebResource_SubMenu")
            Session.Remove("MST_WebResource_Type")
            Session.Remove("MST_WebResource_FS")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)
            Session.Remove("MST_WebResource_Menu")
            Session.Remove("MST_WebResource_SubMenu")
            Session.Remove("MST_WebResource_Type")
            Session.Remove("MST_WebResource_FS")
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
            hddpFlagSearch.Value = "1"
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSearch_Click", ex)
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Call ControlValidate()

            Call LoadType(ddlaType,
                          "A")

            If txtsMenu.Text <> String.Empty Then
                txtaMenu.Text = txtsMenu.Text
                hddaMenu.Value = hddsMenu.Value
            Else
                txtaMenu.Text = String.Empty
                hddaMenu.Value = String.Empty
            End If

            If txtsSubMenu.Text <> String.Empty Then
                txtaSubMenu.Text = txtsSubMenu.Text
                hddaSubMenu.Value = hddsSubMenu.Value
            Else
                txtaSubMenu.Text = String.Empty
                hddaSubMenu.Value = String.Empty
            End If

            txtaMenu.Enabled = True
            txtaSubMenu.Enabled = True
            txtaResourceName.Enabled = True
            ddlaType.Enabled = True

            txtaMenu.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe0c0")
            txtaSubMenu.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe0c0")


            hddID.Value = String.Empty
            'ddlaMenu.SelectedValue = ddlsMenu.SelectedValue
            'If ddlaMenu.SelectedIndex <> 0 Then
            '    ddlaSubMenu.Items.Insert(1, New ListItem(ddlaMenu.SelectedItem.Text, ddlaMenu.SelectedValue))
            'End If
            'Try
            '    ddlaSubMenu.SelectedValue = ddlsSubMenu.SelectedValue
            'Catch ex As Exception
            '    ddlaSubMenu.SelectedIndex = 0
            'End Try
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


                Call LoadType(ddlaType,
                              "A")
                Dim lcMenu As CoreMenu = bl.LoadMenuParentBySubMenu(lcWebresource.MenuID)
                Try
                    If lcMenu.ParentID IsNot Nothing Then
                        Dim lcParentMenu As CoreMenu = bl.LoadMenuNotParentEdit(lcMenu.ParentID)
                        If hddpLG.Value = "TH" Then
                            txtaMenu.Text = lcParentMenu.MenuNameLC
                        Else
                            txtaMenu.Text = lcParentMenu.MenuNameEN
                        End If
                    End If
                    hddaMenu.Value = lcMenu.ParentID
                Catch ex As Exception
                    txtaMenu.Text = String.Empty
                    hddaMenu.Value = String.Empty
                End Try
                Try
                    If lcWebresource IsNot Nothing Then
                        If lcWebresource.MenuID <> Nothing Then
                            Dim lcParentMenu As CoreMenu = bl.LoadMenuNotParentEdit(lcWebresource.MenuID)
                            If lcParentMenu IsNot Nothing Then
                                If hddpLG.Value = "TH" Then
                                    txtaSubMenu.Text = lcParentMenu.MenuNameLC
                                Else
                                    txtaSubMenu.Text = lcParentMenu.MenuNameEN
                                End If
                            End If
                        End If
                    End If
                    hddaSubMenu.Value = lcWebresource.MenuID
                Catch ex As Exception
                    txtaSubMenu.Text = String.Empty
                    hddaSubMenu.Value = String.Empty
                End Try
                txtaResourceName.Text = lcWebresource.ResourceName
                Try
                    ddlaType.SelectedValue = lcWebresource.ResourceType
                Catch ex As Exception
                    ddlaType.SelectedIndex = 0
                End Try
                txtaResourceValueLC.Text = lcWebresource.ResourceValueLC
                txtaResourceValueEN.Text = lcWebresource.ResourceValueEN

                txtaMenu.Enabled = False
                txtaSubMenu.Enabled = False
                txtaResourceName.Enabled = False
                ddlaType.Enabled = False
            End If
            Call OpenDialog()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "closeOverlay();", True)
        End If
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Call LoadRedirec("")
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
            If hddID.Value <> "" Then
                If Not bl.EditWebResource(hddID.Value,
                                          hddsSubMenu.Value,
                                          txtaResourceName.Text,
                                          ddlaType.SelectedValue,
                                          txtaResourceValueEN.Text,
                                          txtaResourceValueLC.Text,
                                          Me.CurrentUser.UserID) Then
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                Else
                    Call LoadRedirec("1")
                End If
            Else
                If Not bl.AddWebResource(hddsSubMenu.Value,
                                         txtaResourceName.Text,
                                         ddlaType.SelectedValue,
                                         txtaResourceValueEN.Text,
                                         txtaResourceValueLC.Text,
                                         Me.CurrentUser.UserID) Then
                    Call OpenDialog()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
                Else
                    Call LoadRedirec("1")
                End If
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSave_Click", ex)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');", True)
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
        lblMassage6.Style.Add("display", "none")
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
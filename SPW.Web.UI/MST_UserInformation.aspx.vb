Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Drawing

Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Web.UI
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO.Ports
Imports System.Security.Cryptography
Imports System.Management
Imports System.Web.UI.ScriptManager
Imports System.Web.UI.ClientScriptManager

Public Class MST_UserInformation
    Inherits BasePage

    Public clsUserInfo As ClsUser_Information = New ClsUser_Information

    Private strPathServer As String = System.Configuration.ConfigurationManager.AppSettings("strPathServer").ToString
    Public Enum TypeName
        asc = 1
        desc = 2
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                Page.DataBind()
                hddParameterMenuID.Value = HelperLog.LoadMenuID("MST_UserInformation.aspx")
                Me.ClearSessionPageLoad("MST_UserInformation.aspx")

                Call LoadInit()
                Call DefaultAllData()
                Call LoadDropdownlist()
                Call LoadGridDefault()


                If Request.QueryString("pFlag") IsNot Nothing Then
                    If Request.QueryString("pFlag").ToString <> String.Empty Then
                        hddpFlagRegister.Value = Request.QueryString("pFlag").ToString
                        liTab2.Style.Add("display", "none")
                        liTab3.Style.Add("display", "none")
                        Call clearText()
                        Call OpenDialog()
                    End If
                Else
                    hddpEmployeeID.Value = Me.CurrentUser.UserID
                    liTab2.Style.Add("display", "")
                    liTab3.Style.Add("display", "")
                    Call GetParameter()
                    Call LoadData()
                End If

                If hddpFlagRegister.Value = String.Empty Then
                    hddpEmployeeID.Value = Me.hddpEmployeeID.Value
                Else
                    hddpEmployeeID.Value = "TEMP" & DateTime.Now.ToString("dd/MM/yy hh mm ss").Split(" ")(3)
                End If


                HelperLog.AccessLog(Me.hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress())
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();", True)
            End If


        Catch ex As Exception
            'HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub


#Region "SetParameter"
    Public Sub LoadRedirec(ByVal FlagSave As String)
        SetParameter(FlagSave)
        If hddpFlagSearch.Value = "1" Then
            Session.Remove("MST_UserInformation_PageInfo")
            Session.Remove("MST_UserInformation_Search")
            Session.Remove("MST_UserInformation_pageLength")
            Session.Remove("MST_UserInformation_keyword")
            Session.Add("MST_UserInformation_keyword", IIf(txtsKeyword.Text <> String.Empty, txtsKeyword.Text, ""))
        End If
        Call Redirect("MST_UserInformation.aspx")
    End Sub
    Public Sub SetParameter(ByVal FlagSave As String)
        Session.Remove("MST_UserInformation_FS")
        Session.Add("MST_UserInformation_FS", IIf(FlagSave <> String.Empty, FlagSave, ""))
        Session.Add("MST_UserInformation_PageInfo", IIf(hddpPageInfo.Value <> String.Empty, hddpPageInfo.Value, ""))
        Session.Add("MST_UserInformation_Search", IIf(hddpSearch.Value <> String.Empty, hddpSearch.Value, ""))
        Session.Add("MST_UserInformation_pageLength", IIf(hddpPagingDefault.Value <> String.Empty, hddpPagingDefault.Value, ""))
        Session.Add("MST_UserInformation_keyword", IIf(txtsKeyword.Text <> String.Empty, txtsKeyword.Text, ""))
    End Sub
    Public Sub GetParameter()
        If Session("MST_UserInformation_FS") IsNot Nothing Then
            If Session("MST_UserInformation_FS").ToString <> String.Empty Then
                Dim strFS As String = Session("MST_UserInformation_FS").ToString
                If strFS = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG") & "');", True)
                ElseIf strFS = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_deletesuccess", "MSG") & "');", True)
                End If
                Dim strPageInfo As String = Session("MST_UserInformation_PageInfo").ToString
                If strPageInfo <> String.Empty Then
                    hddpPageInfo.Value = strPageInfo
                End If

            End If
        End If

        If Session("MST_UserInformation_keyword") IsNot Nothing Then
            txtsKeyword.Text = Session("MST_UserInformation_keyword").ToString
        End If

        If Session("MST_UserInformation_PageInfo") IsNot Nothing Then
            If Session("MST_UserInformation_PageInfo").ToString <> String.Empty Then
                Dim strInfo As String = Session("MST_UserInformation_PageInfo").ToString
                If strInfo <> String.Empty Then
                    hddpPageInfo.Value = strInfo
                End If
            End If
        End If
        If Session("MST_UserInformation_Search") IsNot Nothing Then
            If Session("MST_UserInformation_Search").ToString <> String.Empty Then
                Dim strSearch As String = Session("MST_UserInformation_Search").ToString
                If strSearch <> String.Empty Then
                    hddpSearch.Value = strSearch
                End If
            End If
        End If
        If Session("MST_UserInformation_pageLength") IsNot Nothing Then
            If Session("MST_UserInformation_pageLength").ToString <> String.Empty Then
                Dim strPagingDefault As String = Session("MST_UserInformation_pageLength").ToString
                If strPagingDefault <> String.Empty Then
                    hddpPagingDefault.Value = strPagingDefault
                End If
            End If
        End If
    End Sub

#End Region

#Region "Dropdownlist"
    Public Sub LoadDropdownlist()
        Call LoadDepartment(ddlaDepartment)
        Call LoadDivision(ddlaDivision)
        Call LoadPosition(ddlaPosition)
    End Sub
    Public Sub LoadDepartment(ByVal ddl As DropDownList)

        Dim bl As cUserInformation = New cUserInformation
        Try
            Dim lc As List(Of BD11DEPT) = bl.LoadDepartment()
            ddl.Items.Clear()
            ddl.DataTextField = "FDPNAME"
            ddl.DataValueField = "FDPCODE"
            ddl.DataSource = lc
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("", ""))
            Session.Add("MST_UserInformation_Department", lc)

        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDepartment", ex)
        End Try
    End Sub

    Public Sub LoadDivision(ByVal ddl As DropDownList)

        Dim bl As cUserInformation = New cUserInformation
        Try
            Dim lc As List(Of CoreDivision) = bl.LoadDivision()
            ddl.Items.Clear()
            ddl.DataTextField = "DivisionName"
            ddl.DataValueField = "DivisionCode"
            ddl.DataSource = lc
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("", ""))
            Session.Add("MST_UserInformation_Division", lc)

        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDivision", ex)
        End Try
    End Sub

    Public Sub LoadPosition(ByVal ddl As DropDownList)

        Dim bl As cUserInformation = New cUserInformation
        Try
            Dim lc As List(Of CorePosition) = bl.LoadPosition()
            ddl.Items.Clear()
            ddl.DataTextField = "PositionName"
            ddl.DataValueField = "PositionCode"
            ddl.DataSource = lc
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("", ""))
            Session.Add("MST_UserInformation_Position", lc)

        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadPosition", ex)
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
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "DefaultAllData", ex)
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
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadGridDefault", ex)
        End Try
    End Sub
    Protected Sub LoadInit()
        Try
            lblMain1.Text = Me.GetResource("main_label", "Text", "1")
            lblMain2.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
            lblMain3.Text = Me.GetResource("HeaderPage", "Text", hddParameterMenuID.Value)
            lblMain4.Text = Me.GetResource("HeaderDialog", "Text", hddParameterMenuID.Value)

            lblsKeyword.Text = Me.GetResource("lblsKeyword", "Text", hddParameterMenuID.Value)
            lblsKeywordDetail.Text = String.Format(Me.GetResource("lblsKeywordDetail", "Text", hddParameterMenuID.Value),
                                                   Me.GetResource("UserID", "Text", hddParameterMenuID.Value) &
                                                   ", " & Me.GetResource("employeeno", "Text", hddParameterMenuID.Value) &
                                                   ", " & Me.GetResource("FUSERNAME", "Text", hddParameterMenuID.Value) &
                                                   ", " & Me.GetResource("FDIVCODE", "Text", hddParameterMenuID.Value) &
                                                   ", " & Me.GetResource("FDPCODE", "Text", hddParameterMenuID.Value) &
                                                   ", " & Me.GetResource("FPOSITION", "Text", hddParameterMenuID.Value))

            lblsSearch.Text = Me.GetResource("lblsSearch", "Text", hddParameterMenuID.Value)

            lblTabUSER1.Text = Me.GetResource("TabUSER1", "Text", hddParameterMenuID.Value) '"Info"
            lblTabUSER2.Text = Me.GetResource("TabUSER2", "Text", hddParameterMenuID.Value) '"Project"
            lblTabUSER3.Text = Me.GetResource("TabUSER3", "Text", hddParameterMenuID.Value) '"Company"

            lblaUserID.Text = Me.GetResource("lblaUserID", "Text", hddParameterMenuID.Value)
            lblaIDCardNo.Text = Me.GetResource("lblaIDCardNo", "Text", hddParameterMenuID.Value)
            lblaEmployeeNo.Text = Me.GetResource("lblaEmployeeNo", "Text", hddParameterMenuID.Value)
            lblaPassword.Text = Me.GetResource("lblaPassword", "Text", hddParameterMenuID.Value)
            lblaConfrimPassword.Text = Me.GetResource("lblaConfrimPassword", "Text", hddParameterMenuID.Value)
            lblaTitleEng.Text = Me.GetResource("lblaTitleEng", "Text", hddParameterMenuID.Value)
            lblaNameEng.Text = Me.GetResource("lblaNameEng", "Text", hddParameterMenuID.Value)
            lblaLastnameEng.Text = Me.GetResource("lblaLastnameEng", "Text", hddParameterMenuID.Value)
            lblaTitleThai.Text = Me.GetResource("lblaTitleThai", "Text", hddParameterMenuID.Value)
            lblaNameThai.Text = Me.GetResource("lblaNameThai", "Text", hddParameterMenuID.Value)
            lblaLastnameThai.Text = Me.GetResource("lblaLastnameThai", "Text", hddParameterMenuID.Value)


            lblaDivision.Text = Me.GetResource("lblaDivision", "Text", hddParameterMenuID.Value)
            lblaDepartment.Text = Me.GetResource("lblaDepartment", "Text", hddParameterMenuID.Value)
            lblaPosition.Text = Me.GetResource("lblaPosition", "Text", hddParameterMenuID.Value)
            lblaPhoneNo.Text = Me.GetResource("lblaPhoneNo", "Text", hddParameterMenuID.Value)
            lblaExtension.Text = Me.GetResource("lblaExtension", "Text", hddParameterMenuID.Value)
            lblaEmail.Text = Me.GetResource("lblaEmail", "Text", hddParameterMenuID.Value)
            lblaResignDate.Text = Me.GetResource("lblaResignDate", "Text", hddParameterMenuID.Value)
            lblaTrusteeLike.Text = Me.GetResource("lblaTrusteeLike", "Text", hddParameterMenuID.Value)

            TextHd1.Text = Me.GetResource("No", "Text", "1")
            TextHd2.Text = Me.GetResource("UserID", "Text", hddParameterMenuID.Value)
            TextHd3.Text = Me.GetResource("employeeno", "Text", hddParameterMenuID.Value)
            TextHd4.Text = Me.GetResource("FUSERNAME", "Text", hddParameterMenuID.Value)
            TextHd5.Text = Me.GetResource("FDPCODE", "Text", hddParameterMenuID.Value)
            TextHd6.Text = Me.GetResource("FDIVCODE", "Text", hddParameterMenuID.Value)
            TextHd7.Text = Me.GetResource("FPOSITION", "Text", hddParameterMenuID.Value)
            TextHd8.Text = Me.GetResource("col_edit", "Text", "1")
            TextHd9.Text = Me.GetResource("col_delete", "Text", "1")

            TextFt1.Text = Me.GetResource("No", "Text", "1")
            TextFt2.Text = Me.GetResource("UserID", "Text", hddParameterMenuID.Value)
            TextFt3.Text = Me.GetResource("employeeno", "Text", hddParameterMenuID.Value)
            TextFt4.Text = Me.GetResource("FUSERNAME", "Text", hddParameterMenuID.Value)
            TextFt5.Text = Me.GetResource("FDPCODE", "Text", hddParameterMenuID.Value)
            TextFt6.Text = Me.GetResource("FDIVCODE", "Text", hddParameterMenuID.Value)
            TextFt7.Text = Me.GetResource("FPOSITION", "Text", hddParameterMenuID.Value)
            TextFt8.Text = Me.GetResource("col_edit", "Text", "1")
            TextFt9.Text = Me.GetResource("col_delete", "Text", "1")

            lblHeaderDelete.Text = Me.GetResource("msg_header_delete", "MSG")
            lblBodydelete.Text = Me.GetResource("msg_body_delete", "MSG") & " " & Me.GetResource("employeeno", "Text", hddParameterMenuID.Value)

            hddMSGSaveData.Value = Me.GetResource("msg_save_data", "MSG")
            hddMSGCancelData.Value = Me.GetResource("msg_cancel_data", "MSG")
            hddMSGDeleteData.Value = Me.GetResource("msg_delete_data", "MSG")
            hddMSGEditData.Value = Me.GetResource("msg_edit_data", "MSG")
            hddMSGAddData.Value = Me.GetResource("msg_add_data", "MSG")

            hddMsgIDCardNotEmp.Value = Me.GetResource("msg_IDCardNotEmp", "MSG", hddParameterMenuID.Value)
            hddMsgIDCardUseUser.Value = Me.GetResource("msg_IDCardUseUser", "MSG", hddParameterMenuID.Value)
            hddMsgIDCardNotSave.Value = Me.GetResource("msg_IDCardNotSave", "MSG", hddParameterMenuID.Value)
            hddMsgEmployeeNotEmp.Value = Me.GetResource("msg_EmployeeNotEmp", "MSG", hddParameterMenuID.Value)
            hddMsgEmployeeUseUser.Value = Me.GetResource("msg_EmployeeUseUser", "MSG", hddParameterMenuID.Value)
            hddMsgEmployeeNotSave.Value = Me.GetResource("msg_EmployeeNotSave", "MSG", hddParameterMenuID.Value)

            hddMsgPassword.Value = Me.GetResource("msg_password", "MSG", hddParameterMenuID.Value)
            'Please entry your password again.

            lblMassage1.Text = Me.GetResource("msg_required", "MSG")
            lblMassage2.Text = Me.GetResource("msg_required", "MSG")
            lblMassage3.Text = Me.GetResource("msg_required", "MSG")
            lblMassage4.Text = Me.GetResource("msg_required", "MSG")
            lblMassage5.Text = Me.GetResource("msg_required", "MSG")
            lblMassage6.Text = Me.GetResource("msg_required", "MSG")
            lblMassage7.Text = Me.GetResource("msg_required", "MSG")
            lblMassage8.Text = Me.GetResource("msg_required", "MSG")
            lblMassage9.Text = Me.GetResource("msg_required", "MSG")

            Call clearText()
            Call ControlValidate()

        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadInit", ex)
        End Try
    End Sub
    Protected Sub LoadData()
        Try
            hddReloadGrid.Value = "1"
            Dim fillter As FillterSearch = New FillterSearch
            Dim bl As cUserInformation = New cUserInformation
            fillter.Keyword = txtsKeyword.Text
            fillter.Page = Me.CurrentPage + 1
            fillter.PageSize = Me.PageSizeAll
            fillter.SortBy = Me.SortBy
            fillter.SortType = Me.SortType


            'If txtsKeyword.Text <> String.Empty Then
            Dim lst As List(Of CoreUser) = bl.Loaddata(fillter,
                                                            Me.TotalRow,
                                                            Me.hddpEmployeeID.Value,
                                                            Me.WebCulture)

            If lst IsNot Nothing Then
                Call SetDataUserInfo(lst)
            Else
                Call SetDataUserInfo(Nothing)
            End If
            'End If

            Session.Remove("MST_UserInformation_FS")
            Session.Remove("MST_UserInformation_PageInfo")
            Session.Remove("MST_UserInformation_keyword")


        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadData", ex)
            Session.Remove("MST_UserInformation_FS")
            Session.Remove("MST_UserInformation_PageInfo")
            Session.Remove("MST_UserInformation_keyword")
        End Try
    End Sub
    Public Sub clearText()

        hddKeyID.Value = String.Empty
        hddUserID.Value = String.Empty
        hddIDCardNo.Value = String.Empty

        txtaUserID.Text = String.Empty
        txtaUserID.Enabled = True
        txtaUserID.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")

        txtaIDCardNo.Text = String.Empty
        txtaIDCardNo.Enabled = True
        txtaIDCardNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFE0C0")

        txtaEmployeeNo.Text = String.Empty
        txtaEmployeeNo.Enabled = True
        txtaEmployeeNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFE0C0")
        hddaEmployeeNo.Value = String.Empty

        hddaPasswordOld.Value = String.Empty
        hddaPassword.Value = String.Empty
        txtaPassword.Text = String.Empty
        txtaConfrimPassword.Text = String.Empty
        txtaTitleEng.Text = String.Empty
        txtaNameEng.Text = String.Empty
        txtaLastnameEng.Text = String.Empty
        txtaTitleThai.Text = String.Empty
        txtaNameThai.Text = String.Empty
        txtaLastnameThai.Text = String.Empty

        imgaPicture.ImageUrl = "image/no_image_icon.png"
        hddaImgPicture.Value = String.Empty

        imgaSignature.ImageUrl = "image/no_signature_icon.png"

        ddlaDivision.SelectedIndex = 0
        ddlaDepartment.SelectedIndex = 0
        ddlaPosition.SelectedIndex = 0
        txtaPhoneNo.Text = String.Empty
        txtaEmail.Text = String.Empty

        hddaSection.Value = String.Empty
        hddaComID.Value = String.Empty

        txtaTrusteeLike.Text = String.Empty
        txtaTrusteeLike.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFE0C0")

        hddpAutoName1.Value = String.Empty
        hddpAutoCode1.Value = String.Empty

        hddpAutoName2.Value = String.Empty
        hddpAutoCode2.Value = String.Empty

    End Sub
    Sub getData()

        If hddaPassword.Value <> String.Empty Then
            'txtaPassword.Text = hddaPassword.Value
            'txtaConfrimPassword.Text = hddaPassword.Value
            txtaPassword.Attributes.Add("value", hddaPassword.Value)
            txtaConfrimPassword.Attributes.Add("value", hddaPassword.Value)
        End If

        If hddUserID.Value <> String.Empty Then txtaUserID.Text = hddUserID.Value
        If hddIDCardNo.Value <> String.Empty Then txtaIDCardNo.Text = hddIDCardNo.Value
        If hddaEmployeeNo.Value <> String.Empty Then txtaEmployeeNo.Text = hddaEmployeeNo.Value

        If hddProject.Value <> String.Empty Then
            hddProject.Value = hddProject.Value.Replace("'", "")
            hddProject.Value = "'" & hddProject.Value.Replace(",", "','") & "'"
            Dim lcProject As DataTable = clsUserInfo.GetCoreUsersProjectByUse(hddProject.Value)
            Call CreateDatatableProject(dtProject)
            Call ConvertDatatableProject(lcProject, dtProject)
        End If

        If hddCompany.Value <> String.Empty Then
            hddCompany.Value = hddCompany.Value.Replace("'", "")
            hddCompany.Value = "'" & hddCompany.Value.Replace(",", "','") & "'"
            Dim lcCompany As DataTable = clsUserInfo.GetCoreUsersCompanyByUse(hddCompany.Value)
            Call CreateDatatableCompany(dtCompany)
            Call ConvertDatatableCompany(lcCompany, dtCompany)
        End If

    End Sub

    Public Shared Function GetText(ByVal Models As String) As String
        Dim sBuilder As New StringBuilder()
        Using hasher As MD5 = MD5.Create()
            Dim dbytes As Byte() = hasher.ComputeHash(Encoding.UTF8.GetBytes(Models))
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("X2"))
            Next n
        End Using
        Return sBuilder.ToString()
    End Function

#End Region

#Region "Event"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            hddpFlagSearch.Value = "1"
            Call LoadRedirec("")
        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSearch_Click", ex)
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Call clearText()
            Call OpenDialog()
        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Try

            txtaUserID.Text = String.Empty
            'txtaUserID.Attributes.Add("disabled", "")
            txtaUserID.Enabled = False
            txtaUserID.BackColor = System.Drawing.ColorTranslator.FromHtml("#FCFCFC")

            txtaIDCardNo.Text = String.Empty
            'txtaIDCardNo.Attributes.Add("disabled", "")
            txtaIDCardNo.Enabled = False
            txtaIDCardNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FCFCFC")

            'txtaEmployeeNo.Attributes.Add("disabled", "")
            txtaEmployeeNo.Enabled = False
            txtaEmployeeNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FCFCFC")

            Dim bl As cUserInformation = New cUserInformation
            Dim lc As CoreUser = bl.LoadEditUserInfo(hddKeyID.Value)

            If lc IsNot Nothing Then
                hddKeyID.Value = lc.UserID.ToString
                hddUserID.Value = lc.UserID.ToString
                txtaUserID.Text = lc.UserID.ToString
                txtaIDCardNo.Text = lc.IDCardNo.ToString
                hddIDCardNo.Value = lc.IDCardNo.ToString
                txtaEmployeeNo.Text = lc.EmployeeNo.ToString
                hddaEmployeeNo.Value = lc.EmployeeNo.ToString

                Dim _TestDecrypt As String = ""
                _TestDecrypt = bl.DecryptString(lc.Password.ToString)

                hddaPasswordOld.Value = _TestDecrypt
                hddaPassword.Value = _TestDecrypt
                txtaPassword.Text = _TestDecrypt
                txtaConfrimPassword.Text = _TestDecrypt

                If lc.TitleEng IsNot Nothing Then txtaTitleEng.Text = lc.TitleEng.ToString
                txtaNameEng.Text = lc.NameEng.ToString
                txtaLastnameEng.Text = lc.LastnameEng.ToString
                If lc.TitleThai IsNot Nothing Then txtaTitleThai.Text = lc.TitleThai.ToString
                txtaNameThai.Text = lc.NameThai.ToString
                txtaLastnameThai.Text = lc.LastnameThai.ToString

                If lc.ImgFile Is Nothing Then
                    imgaPicture.ImageUrl = "image/no_image_icon.png"
                Else
                    Dim base64StringImg As String = Convert.ToBase64String(lc.ImgFile, 0, lc.ImgFile.Length)
                    Try
                        imgaPicture.ImageUrl = Convert.ToString("data:image/png;base64,") & base64StringImg
                    Catch ex As Exception
                        imgaPicture.ImageUrl = "image/no_image_icon.png"
                    End Try
                End If

                If lc.ImgSignature Is Nothing Then
                    imgaSignature.ImageUrl = "image/no_signature_icon.png"
                Else
                    Dim base64StringSig As String = Convert.ToBase64String(lc.ImgSignature, 0, lc.ImgSignature.Length)
                    Try
                        imgaSignature.ImageUrl = Convert.ToString("data:image/png;base64,") & base64StringSig
                    Catch ex As Exception
                        imgaSignature.ImageUrl = "image/no_signature_icon.png"
                    End Try
                End If

                'If lc.ImgFile Is Nothing Then
                '    imgaImgFile.ImageUrl = "image/no_image_icon.png"
                '    hddpKeyIDImg.Value = String.Empty
                '    hddpNameImg.Value = String.Empty
                '    hddaImgPicture.Value = String.Empty
                'Else
                '    hddaImgPicture.Value = lc.ImgFile.ToString
                '    Dim base64StringImg As String = Convert.ToBase64String(lc.ImgFile, 0, lc.ImgFile.Length)
                '    Try
                '        imgaImgFile.ImageUrl = Convert.ToString("data:image/png;base64,") & base64StringImg
                '    Catch ex As Exception
                '        imgaImgFile.ImageUrl = "image/no_image_icon.png"
                '        hddpKeyIDImg.Value = String.Empty
                '        hddpNameImg.Value = String.Empty
                '        hddaImgPicture.Value = String.Empty
                '    End Try
                'End If

                'If lc.ImgSignature Is Nothing Then
                '    imgaImgSignature.ImageUrl = "image/no_signature_icon.png"
                '    hddpKeyIDPicture.Value = String.Empty
                '    hddpNamePicture.Value = String.Empty
                '    hddaImgPictureSignature.Value = String.Empty
                'Else
                '    hddaImgPictureSignature.Value = lc.ImgSignature.ToString
                '    Dim base64StringSig As String = Convert.ToBase64String(lc.ImgSignature, 0, lc.ImgSignature.Length)
                '    Try
                '        imgaImgSignature.ImageUrl = Convert.ToString("data:image/png;base64,") & base64StringSig
                '    Catch ex As Exception
                '        imgaImgSignature.ImageUrl = "image/no_signature_icon.png"
                '        hddpKeyIDPicture.Value = String.Empty
                '        hddpNamePicture.Value = String.Empty
                '        hddaImgPictureSignature.Value = String.Empty
                '    End Try
                'End If

                If lc.Division IsNot Nothing Then ddlaDivision.SelectedValue = lc.Division.ToString
                If lc.Department IsNot Nothing Then ddlaDepartment.SelectedValue = lc.Department.ToString
                If lc.Position IsNot Nothing Then ddlaPosition.SelectedValue = lc.Position.ToString
                If lc.PhoneNo IsNot Nothing Then txtaPhoneNo.Text = lc.PhoneNo.ToString
                If lc.Extension IsNot Nothing Then txtaExtension.Text = lc.Extension.ToString
                If lc.Email IsNot Nothing Then txtaEmail.Text = lc.Email.ToString

                Dim lcProject As DataTable = clsUserInfo.GetCoreUsersProject(hddKeyID.Value)
                Call CreateDatatableProject(dtProject)
                Call ConvertDatatableProject(lcProject, dtProject)

                Dim lcCompany As DataTable = clsUserInfo.GetCoreUsersCompany(hddKeyID.Value)
                Call CreateDatatableCompany(dtCompany)
                Call ConvertDatatableCompany(lcCompany, dtCompany)

                Call OpenDialog()

                txtaPassword.Attributes.Add("value", hddaPassword.Value)
                txtaConfrimPassword.Attributes.Add("value", hddaPassword.Value)

                If lc.ResignDate Is Nothing Then
                    chbaResignDate.Checked = True
                Else
                    chbaResignDate.Checked = False
                End If

                'txtaPassword.Text = hddaPassword.Value
                'txtaConfrimPassword.Text = hddaPassword.Value

            End If

        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "btnEdit_Click", ex)
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Try
            Dim bl As cUserInformation = New cUserInformation
            Dim lc As CoreUser = bl.LoadEditUserInfo(hddKeyID.Value)
            If lc IsNot Nothing Then
                If Not bl.UserDelete(hddKeyID.Value,
                                 Me.hddpEmployeeID.Value) Then
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_cannot_delete_because ", "MSG") & "');", True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_deletenotsuccess", "MSG") & "');", True)
                Else
                    Call LoadRedirec("2")
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_deletenotsuccess", "MSG") & "');", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "btnDelete_Click", ex)
        End Try
    End Sub

    Public Function GetBytes(str As String) As Byte()
        Dim bytes As Byte() = New Byte(str.Length * 2 - 1) {}
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length)
        Return bytes
    End Function

    Public Function GetString(bytes As Byte()) As String
        Dim chars As Char() = New Char(bytes.Length / 2 - 1) {}
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length)
        Return New String(chars)
    End Function
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click

        Try
            Dim message As String = ""
            Dim succ As Boolean = True
            Dim bl As cUserInformation = New cUserInformation
            Dim data As CoreUser = Nothing



            Dim _TestEecrypt As String = ""
            'Dim _TestDecrypt As String = ""
            '_TestEecrypt = bl.EncryptData("p@$$w0rd")
            '_TestDecrypt = bl.DecryptString(lc.Password.ToString)

            If hddaPasswordOld.Value <> txtaPassword.Text Then
                If txtaPassword.Text <> String.Empty Then
                    _TestEecrypt = bl.EncryptData(txtaPassword.Text)
                    hddaPassword.Value = _TestEecrypt
                End If
            ElseIf hddaPassword.Value = String.Empty Then
                _TestEecrypt = bl.EncryptData(hddaPasswordOld.Value)
                hddaPassword.Value = _TestEecrypt
            Else
                _TestEecrypt = bl.EncryptData(hddaPassword.Value)
                hddaPassword.Value = _TestEecrypt
            End If

            Dim bPicture As Boolean = False
            Dim bSignature As Boolean = False

            Dim imagesPicture As Byte() = Nothing
            Dim imagesSignature As Byte() = Nothing

            If chkDeletePicture.Checked = True Then
                imagesPicture = Nothing
            Else
                If FileUploadPicture.HasFile Then
                    Try
                        Dim StreemImg As Stream = (FileUploadPicture.PostedFile.InputStream)
                        Dim brsImg As New BinaryReader(StreemImg)
                        imagesPicture = brsImg.ReadBytes(CInt(StreemImg.Length))
                    Catch ex As Exception
                        imagesPicture = Nothing
                    End Try
                Else
                    If imgaPicture.ImageUrl <> "image/no_image_icon.png" And hddaImgPicture.Value <> String.Empty Then
                        Try
                            Dim image As System.Drawing.Image = System.Drawing.Image.FromFile(hddaImgPicture.Value)
                            Dim imageConverter As New ImageConverter()
                            Dim imageByte As Byte() = DirectCast(imageConverter.ConvertTo(image, GetType(Byte())), Byte())
                            imagesPicture = imageByte
                            hddaImgPicture.Value = String.Empty
                        Catch ex As Exception
                            imagesPicture = Nothing
                        End Try
                    End If
                End If
                If imgaPicture.ImageUrl <> "image/no_image_icon.png" Then bPicture = True
            End If

            If chkDeleteSignature.Checked = True Then
                imagesSignature = Nothing
            Else
                If FileUploadSignature.HasFile Then
                    Try
                        Dim StreemImg As Stream = (FileUploadSignature.PostedFile.InputStream)
                        Dim brsImg As New BinaryReader(StreemImg)
                        imagesSignature = brsImg.ReadBytes(CInt(StreemImg.Length))
                    Catch ex As Exception
                        imagesSignature = Nothing
                    End Try
                End If

                If imgaSignature.ImageUrl <> "image/no_signature_icon.png" Then bSignature = True
            End If


            If hddKeyID.Value <> "" Then

                If Not bl.UserEdit(hddKeyID.Value,
                                hddaEmployeeNo.Value,
                                hddIDCardNo.Value,
                                hddaPassword.Value,
                                txtaTitleEng.Text,
                                txtaTitleThai.Text,
                                txtaNameEng.Text,
                                txtaNameThai.Text,
                                txtaLastnameEng.Text,
                                txtaLastnameThai.Text,
                                bPicture,
                                bSignature,
                                imagesPicture,
                                imagesSignature,
                                ddlaDepartment.SelectedValue,
                                ddlaDivision.SelectedValue,
                                ddlaPosition.SelectedValue,
                                txtaPhoneNo.Text,
                                txtaExtension.Text,
                                txtaEmail.Text,
                                hddProject.Value,
                                hddCompany.Value,
                                chbaResignDate.Checked,
                                hddpEmployeeID.Value) Then
                    Call OpenDialog()
                    Call getData()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                    Call getData()
                Else
                    If hddpFlagRegister.Value = String.Empty Then
                        Call LoadRedirec("1")
                    Else
                        Call Redirect("Default.aspx")
                    End If
                End If
            Else

                Dim lstChkUserIDCard As List(Of CoreUser) = bl.GetDataUser("", txtaIDCardNo.Text.Replace(" ", ""))
                If lstChkUserIDCard IsNot Nothing Then
                    If lstChkUserIDCard.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgIDCardUseUser.Value & "');", True)
                        Exit Sub
                    End If
                End If
                Dim lstChkUserEmp As List(Of CoreUser) = bl.GetDataUser(txtaEmployeeNo.Text.Replace(" ", ""), "")
                If lstChkUserEmp IsNot Nothing Then
                    If lstChkUserEmp.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgEmployeeUseUser.Value & "');", True)
                        Exit Sub
                    End If
                End If
                Dim lstIDCard As List(Of EMPLOYEE_INFO) = bl.GetDataEmployee("", txtaIDCardNo.Text.Replace(" ", ""))
                If lstIDCard IsNot Nothing Then
                    If lstIDCard.Count = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgIDCardNotEmp.Value & "');", True)
                        Exit Sub
                    End If
                End If
                Dim lstEmp As List(Of EMPLOYEE_INFO) = bl.GetDataEmployee(txtaEmployeeNo.Text.Replace(" ", ""), "")
                If lstEmp IsNot Nothing Then
                    If lstEmp.Count = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgEmployeeNotEmp.Value & "');", True)
                        Exit Sub
                    End If
                End If

                'Dim lst As List(Of EMPLOYEE_INFO) = bl.GetDataEmployee("", txtaIDCardNo.Text)
                'If lst IsNot Nothing Then
                Dim lc As CoreUser = bl.GetUserByID(txtaUserID.Text,
                                                    hddpEmployeeID.Value)
                If lc Is Nothing Then
                    If Not bl.UserAdd(txtaUserID.Text,
                            txtaEmployeeNo.Text,
                            txtaIDCardNo.Text,
                            hddaPassword.Value,
                            txtaTitleEng.Text,
                            txtaTitleThai.Text,
                            txtaNameEng.Text,
                            txtaNameThai.Text,
                            txtaLastnameEng.Text,
                            txtaLastnameThai.Text,
                            bPicture,
                            bSignature,
                            imagesPicture,
                            imagesSignature,
                            ddlaDepartment.SelectedValue,
                            ddlaDivision.SelectedValue,
                            ddlaPosition.SelectedValue,
                            txtaPhoneNo.Text,
                            txtaExtension.Text,
                            txtaEmail.Text,
                            hddProject.Value,
                            hddCompany.Value,
                            chbaResignDate.Checked,
                            hddpEmployeeID.Value) Then
                        Call OpenDialog()
                        Call getData()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
                        Call getData()
                    Else
                        If hddpFlagRegister.Value = String.Empty Then
                            Call LoadRedirec("1")
                        Else
                            Call Redirect("Default.aspx")
                        End If
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_data_duplicate", "MSG") & "');", True)
                End If
                'Else
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgIDCardNo.Value & "');", True)
                'End If

            End If
        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSave_Click", ex)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG") & "');", True)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            If hddpFlagRegister.Value = String.Empty Then
                Call LoadRedirec("")
            Else
                Call Redirect("Default.aspx")
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "btnCancel_Click", ex)
        End Try
    End Sub

    Protected Sub btnReload1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReload1.Click
        Try

            Dim bl As cUserInformation = New cUserInformation
            Dim strPathImageUser As String = ""
            Dim strUrlImageUser As String = ""
            Try
                strPathImageUser = System.Configuration.ConfigurationManager.AppSettings("strPathImageUser").ToString
            Catch ex As Exception
            End Try
            Try
                strUrlImageUser = System.Configuration.ConfigurationManager.AppSettings("strUrlImageUser").ToString
            Catch ex As Exception
            End Try
            Dim bChk As Boolean = True
            If txtaIDCardNo.Text <> String.Empty Then
                Dim lstChkUser As List(Of CoreUser) = bl.GetDataUser("", txtaIDCardNo.Text.Replace(" ", ""))
                If lstChkUser IsNot Nothing Then
                    If lstChkUser.Count > 0 Then
                        Call clearText()
                        bChk = False
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgIDCardUseUser.Value & "');", True)
                    End If
                End If
                If bChk = True Then
                    Dim lst As List(Of EMPLOYEE_INFO) = bl.GetDataEmployee("", txtaIDCardNo.Text.Replace(" ", ""))
                    If lst IsNot Nothing Then
                        If lst.Count > 0 Then
                            hddpAutoCode1.Value = lst(0).Code.ToString
                            hddpAutoName1.Value = lst(0).IDCardNo.ToString

                            txtaIDCardNo.Text = lst(0).IDCardNo.ToString
                            txtaEmployeeNo.Text = lst(0).Code.ToString
                            txtaPassword.Text = String.Empty
                            txtaConfrimPassword.Text = String.Empty
                            If lst(0).TitleEng IsNot Nothing Then
                                txtaTitleEng.Text = lst(0).TitleEng.ToString
                            Else
                                txtaTitleEng.Text = String.Empty
                            End If
                            If lst(0).Name1 IsNot Nothing Then
                                txtaNameEng.Text = lst(0).Name1.ToString
                            Else
                                txtaNameEng.Text = String.Empty
                            End If
                            If lst(0).Lastname1 IsNot Nothing Then
                                txtaLastnameEng.Text = lst(0).Lastname1.ToString
                            Else
                                txtaLastnameEng.Text = String.Empty
                            End If
                            If lst(0).TitleThai IsNot Nothing Then
                                txtaTitleThai.Text = lst(0).TitleThai.ToString
                            Else
                                txtaTitleThai.Text = String.Empty
                            End If
                            If lst(0).Name2 IsNot Nothing Then
                                txtaNameThai.Text = lst(0).Name2.ToString
                            Else
                                txtaNameThai.Text = String.Empty
                            End If
                            If lst(0).Lastname2 IsNot Nothing Then
                                txtaLastnameThai.Text = lst(0).Lastname2.ToString
                            Else
                                txtaLastnameThai.Text = String.Empty
                            End If

                            Dim bChkImg As Boolean = False
                            Dim _jpg As String = txtaEmployeeNo.Text & ".jpg"
                            Dim _jpeg As String = txtaEmployeeNo.Text & ".jpeg"
                            Dim _Img As String = ""
                            Dim allfiles() As String = System.IO.Directory.GetFiles(strPathImageUser, _jpg, System.IO.SearchOption.AllDirectories)
                            _Img = _jpg
                            If allfiles Is Nothing Then
                                bChkImg = True
                            Else
                                If allfiles.Length = 0 Then bChkImg = True
                            End If
                            If bChkImg = True Then
                                allfiles = System.IO.Directory.GetFiles(strPathImageUser, _jpeg, System.IO.SearchOption.AllDirectories)
                                _Img = _jpeg
                            End If

                            Dim dr As DataRow = Nothing
                            If allfiles IsNot Nothing Then
                                For Each filx In allfiles
                                    hddaImgPicture.Value = filx

                                    If hddaImgPicture.Value = String.Empty Then
                                        imgaPicture.ImageUrl = "image/no_image_icon.png"
                                    Else
                                        Try
                                            imgaPicture.ImageUrl = strUrlImageUser & _Img
                                        Catch ex As Exception
                                            imgaPicture.ImageUrl = "image/no_image_icon.png"
                                        End Try
                                    End If

                                Next
                            Else
                                hddaImgPicture.Value = String.Empty
                                imgaPicture.ImageUrl = "image/no_image_icon.png"
                            End If

                            imgaSignature.ImageUrl = "image/no_signature_icon.png"

                            If lst(0).Division IsNot Nothing Then
                                ddlaDivision.SelectedValue = lst(0).Division.ToString
                            Else
                                ddlaDivision.SelectedValue = String.Empty
                            End If
                            If lst(0).Department IsNot Nothing Then
                                ddlaDepartment.SelectedValue = lst(0).Department.ToString
                            Else
                                ddlaDepartment.SelectedValue = String.Empty
                            End If
                            If lst(0).Sectionn IsNot Nothing Then
                                hddaSection.Value = lst(0).Sectionn.ToString
                            Else
                                hddaSection.Value = String.Empty
                            End If
                            If lst(0).COMID IsNot Nothing Then
                                hddaComID.Value = lst(0).COMID.ToString
                            Else
                                hddaComID.Value = String.Empty
                            End If
                            If lst(0).Positionn IsNot Nothing Then
                                ddlaPosition.SelectedValue = lst(0).Positionn.ToString
                            Else
                                ddlaPosition.SelectedValue = String.Empty
                            End If
                            If lst(0).PhoneNo IsNot Nothing Then
                                txtaPhoneNo.Text = lst(0).PhoneNo.ToString
                            Else
                                txtaPhoneNo.Text = String.Empty
                            End If
                            If lst(0).Extension IsNot Nothing Then
                                txtaExtension.Text = lst(0).Extension.ToString
                            Else
                                txtaExtension.Text = String.Empty
                            End If
                            If lst(0).Email IsNot Nothing Then
                                txtaEmail.Text = lst(0).Email.ToString
                            Else
                                txtaEmail.Text = String.Empty
                            End If
                            If lst(0).ResignDate Is Nothing Then
                                chbaResignDate.Checked = True
                            Else
                                chbaResignDate.Checked = False
                            End If

                            hddProject.Value = hddaSection.Value
                            If hddProject.Value <> String.Empty Then
                                hddProject.Value = "'" & hddProject.Value.Replace(",", "','") & "'"
                                Dim lcProject As DataTable = clsUserInfo.GetCoreUsersProjectByUse(hddProject.Value)
                                Call CreateDatatableProject(dtProject)
                                Call ConvertDatatableProject(lcProject, dtProject)
                            End If
                            hddCompany.Value = hddaComID.Value
                            If hddCompany.Value <> String.Empty Then
                                hddCompany.Value = "'" & hddCompany.Value.Replace(",", "','") & "'"
                                Dim lcCompany As DataTable = clsUserInfo.GetCoreUsersCompanyByUse(hddCompany.Value)
                                Call CreateDatatableCompany(dtCompany)
                                Call ConvertDatatableCompany(lcCompany, dtCompany)
                            End If

                        Else
                            Call clearText()
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgIDCardNotEmp.Value & "');", True)
                        End If
                    Else
                        Call clearText()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgIDCardNotEmp.Value & "');", True)
                    End If
                End If

            End If

            Call getData()
            'UpdatePanel3.DataBind()
        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReload1_Click", ex)
        End Try
    End Sub

    Protected Sub btnReload2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReload2.Click
        Try

            Dim bl As cUserInformation = New cUserInformation
            Dim strPathImageUser As String = ""
            Dim strUrlImageUser As String = ""
            Try
                strPathImageUser = System.Configuration.ConfigurationManager.AppSettings("strPathImageUser").ToString
            Catch ex As Exception
            End Try
            Try
                strUrlImageUser = System.Configuration.ConfigurationManager.AppSettings("strUrlImageUser").ToString
            Catch ex As Exception
            End Try
            Dim bChk As Boolean = True
            If txtaEmployeeNo.Text <> String.Empty Then
                Dim lstChkUser As List(Of CoreUser) = bl.GetDataUser(txtaEmployeeNo.Text.Replace(" ", ""), "")
                If lstChkUser IsNot Nothing Then
                    If lstChkUser.Count > 0 Then
                        Call clearText()
                        bChk = False
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgEmployeeUseUser.Value & "');", True)
                    End If
                End If
                If bChk = True Then
                    Dim lst As List(Of EMPLOYEE_INFO) = bl.GetDataEmployee(txtaEmployeeNo.Text.Replace(" ", ""), "")
                    If lst IsNot Nothing Then

                        If lst.Count > 0 Then

                            hddpAutoCode2.Value = lst(0).Code.ToString
                            hddpAutoName2.Value = lst(0).IDCardNo.ToString

                            txtaIDCardNo.Text = lst(0).IDCardNo.ToString
                            txtaEmployeeNo.Text = lst(0).Code.ToString
                            txtaPassword.Text = String.Empty
                            txtaConfrimPassword.Text = String.Empty
                            If lst(0).TitleEng IsNot Nothing Then
                                txtaTitleEng.Text = lst(0).TitleEng.ToString
                            Else
                                txtaTitleEng.Text = String.Empty
                            End If
                            If lst(0).Name1 IsNot Nothing Then
                                txtaNameEng.Text = lst(0).Name1.ToString
                            Else
                                txtaNameEng.Text = String.Empty
                            End If
                            If lst(0).Lastname1 IsNot Nothing Then
                                txtaLastnameEng.Text = lst(0).Lastname1.ToString
                            Else
                                txtaLastnameEng.Text = String.Empty
                            End If
                            If lst(0).TitleThai IsNot Nothing Then
                                txtaTitleThai.Text = lst(0).TitleThai.ToString
                            Else
                                txtaTitleThai.Text = String.Empty
                            End If
                            If lst(0).Name2 IsNot Nothing Then
                                txtaNameThai.Text = lst(0).Name2.ToString
                            Else
                                txtaNameThai.Text = String.Empty
                            End If
                            If lst(0).Lastname2 IsNot Nothing Then
                                txtaLastnameThai.Text = lst(0).Lastname2.ToString
                            Else
                                txtaLastnameThai.Text = String.Empty
                            End If

                            Dim bChkImg As Boolean = False
                            Dim _jpg As String = txtaEmployeeNo.Text & ".jpg"
                            Dim _jpeg As String = txtaEmployeeNo.Text & ".jpeg"
                            Dim _Img As String = ""
                            Dim allfiles() As String = System.IO.Directory.GetFiles(strPathImageUser, _jpg, System.IO.SearchOption.AllDirectories)
                            _Img = _jpg
                            If allfiles Is Nothing Then
                                bChkImg = True
                            Else
                                If allfiles.Length = 0 Then bChkImg = True
                            End If
                            If bChkImg = True Then
                                allfiles = System.IO.Directory.GetFiles(strPathImageUser, _jpeg, System.IO.SearchOption.AllDirectories)
                                _Img = _jpeg
                            End If

                            Dim dr As DataRow = Nothing
                            If allfiles IsNot Nothing Then
                                For Each filx In allfiles
                                    hddaImgPicture.Value = filx

                                    If hddaImgPicture.Value = String.Empty Then
                                        imgaPicture.ImageUrl = "image/no_image_icon.png"
                                    Else
                                        Try
                                            imgaPicture.ImageUrl = strUrlImageUser & _Img
                                        Catch ex As Exception
                                            imgaPicture.ImageUrl = "image/no_image_icon.png"
                                        End Try
                                    End If

                                Next
                            Else
                                hddaImgPicture.Value = String.Empty
                                imgaPicture.ImageUrl = "image/no_image_icon.png"
                            End If

                            imgaSignature.ImageUrl = "image/no_signature_icon.png"

                            If lst(0).Division IsNot Nothing Then
                                ddlaDivision.SelectedValue = lst(0).Division.ToString
                            Else
                                ddlaDivision.SelectedValue = String.Empty
                            End If
                            If lst(0).Department IsNot Nothing Then
                                ddlaDepartment.SelectedValue = lst(0).Department.ToString
                            Else
                                ddlaDepartment.SelectedValue = String.Empty
                            End If
                            If lst(0).Sectionn IsNot Nothing Then
                                hddaSection.Value = lst(0).Sectionn.ToString
                            Else
                                hddaSection.Value = String.Empty
                            End If
                            If lst(0).COMID IsNot Nothing Then
                                hddaComID.Value = lst(0).COMID.ToString
                            Else
                                hddaComID.Value = String.Empty
                            End If
                            If lst(0).Positionn IsNot Nothing Then
                                ddlaPosition.SelectedValue = lst(0).Positionn.ToString
                            Else
                                ddlaPosition.SelectedValue = String.Empty
                            End If
                            If lst(0).PhoneNo IsNot Nothing Then
                                txtaPhoneNo.Text = lst(0).PhoneNo.ToString
                            Else
                                txtaPhoneNo.Text = String.Empty
                            End If
                            If lst(0).Extension IsNot Nothing Then
                                txtaExtension.Text = lst(0).Extension.ToString
                            Else
                                txtaExtension.Text = String.Empty
                            End If
                            If lst(0).Email IsNot Nothing Then
                                txtaEmail.Text = lst(0).Email.ToString
                            Else
                                txtaEmail.Text = String.Empty
                            End If
                            If lst(0).ResignDate Is Nothing Then
                                chbaResignDate.Checked = True
                            Else
                                chbaResignDate.Checked = False
                            End If

                            hddProject.Value = hddaSection.Value
                            If hddProject.Value <> String.Empty Then
                                hddProject.Value = "'" & hddProject.Value.Replace(",", "','") & "'"
                                Dim lcProject As DataTable = clsUserInfo.GetCoreUsersProjectByUse(hddProject.Value)
                                Call CreateDatatableProject(dtProject)
                                Call ConvertDatatableProject(lcProject, dtProject)
                            End If
                            hddCompany.Value = hddaComID.Value
                            If hddCompany.Value <> String.Empty Then
                                hddCompany.Value = "'" & hddCompany.Value.Replace(",", "','") & "'"
                                Dim lcCompany As DataTable = clsUserInfo.GetCoreUsersCompanyByUse(hddCompany.Value)
                                Call CreateDatatableCompany(dtCompany)
                                Call ConvertDatatableCompany(lcCompany, dtCompany)
                            End If
                        Else
                            Call clearText()
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgEmployeeNotEmp.Value & "');", True)
                        End If
                    Else
                        Call clearText()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & hddMsgEmployeeNotEmp.Value & "');", True)
                    End If
                End If

            End If

            Call getData()
            'UpdatePanel3.DataBind()
        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReload1_Click", ex)
        End Try
    End Sub

    'Protected Const MAX_FILE_LENGTH_MB As Integer = 5
    'Dim sSignature As String = "Signature"
    'Dim sImg As String = "Img"

    'Protected Sub btnFileUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileUpload.Click
    '    hddpKeyIDPicture.Value = String.Empty
    '    hddpNamePicture.Value = String.Empty
    '    Dim dt As New DataTable
    '    If FileUpload1.HasFile Then
    '        Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)

    '        Dim fileType As String() = Split(".jpg,.jpeg,.gif,.png", ",")
    '        If Not fileType.Contains(Path.GetExtension(FileUpload1.FileName)) Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_jpg", "MSG") & "');", True)
    '            Exit Sub
    '        End If

    '        'Dim len As Integer = FileUpload1.FileContent.Length
    '        'If len > MAX_FILE_LENGTH_MB * 1000000 Then
    '        '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_notover", "MSG") & "');", True)
    '        '    Return
    '        'End If

    '        If fileName <> String.Empty Then
    '            Call UploadFileTemp(fileName, sSignature, FileUpload1)
    '            hddpKeyIDPicture.Value = fileName.Split(".")(0)
    '            hddpNamePicture.Value = fileName
    '            imgaImgSignature.ImageUrl = strPathServer & sSignature & "/" & hddParameterMenuID.Value & "/" & Me.hddpEmployeeID.Value & "/" & fileName
    '        End If
    '    End If

    '    Call getData()

    'End Sub
    'Protected Sub btnDeletePicture_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeletePicture.Click

    '    Dim strFileName As Integer = 0
    '    Dim checkFlagFile As String = String.Empty
    '    Dim objFSO As Object
    '    Dim objFolder As Object
    '    Dim objFile As Object
    '    Dim strLFileName As String = String.Empty

    '    If hddpKeyIDPicture.Value <> String.Empty Then

    '        Call DeleteSigleFileTemp(hddpNamePicture.Value, sSignature)
    '        Dim PreviosKetID As Integer = 0
    '        Dim tempKeyID As String = hddpKeyIDPicture.Value
    '        If tempKeyID <> String.Empty Then
    '            Dim TempPath As String = String.Empty
    '            Try
    '                If CInt(tempKeyID) > 0 Then
    '                    strFileName = tempKeyID
    '                End If
    '                If strFileName <> 0 Then
    '                    For i As Integer = 0 To strFileName - 1
    '                        If CInt(tempKeyID) > 0 Then
    '                            If strLFileName = String.Empty Then
    '                                Try
    '                                    objFSO = CreateObject("Scripting.FileSystemObject")
    '                                    objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & sSignature & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\"))
    '                                    For Each objFile In objFolder.Files
    '                                        Dim strCheckName As String = objFile.Name.ToString.Split(".")(0)
    '                                        If strCheckName = CInt(strFileName - 1) Then
    '                                            strLFileName = objFile.Name.ToString.Split(".")(1)
    '                                            Exit For
    '                                        End If
    '                                    Next objFile
    '                                Catch ex As Exception

    '                                End Try
    '                            End If

    '                            TempPath = Server.MapPath("Uploads\" & sSignature & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\" & strFileName & "." & strLFileName)
    '                            If System.IO.File.Exists(TempPath) Then
    '                                checkFlagFile = "1"
    '                                PreviosKetID = strFileName
    '                                Exit For
    '                            End If
    '                            strFileName -= 1
    '                        End If
    '                    Next
    '                End If
    '            Catch ex As Exception

    '            End Try
    '        End If
    '        'If strFileName = 0 Or checkFlagFile = String.Empty Then
    '        '    PreviosKetID = CInt(dt.Rows(0)("ID").ToString)
    '        'End If
    '        If strLFileName = String.Empty Then
    '            Try
    '                objFSO = CreateObject("Scripting.FileSystemObject")
    '                objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & sSignature & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\"))
    '                For Each objFile In objFolder.Files
    '                    Dim strCheckName As String = objFile.Name.ToString.Split(".")(0)
    '                    If strCheckName = PreviosKetID Then
    '                        strLFileName = objFile.Name.ToString.Split(".")(1)
    '                        Exit For
    '                    End If
    '                Next objFile
    '            Catch ex As Exception

    '            End Try
    '        End If

    '        If PreviosKetID <> 0 Then
    '            hddpKeyIDPicture.Value = PreviosKetID
    '            hddpNamePicture.Value = PreviosKetID & "." & strLFileName
    '            hddaImgPictureSignature.Value = String.Empty
    '            imgaImgSignature.ImageUrl = strPathServer & sSignature & "/" & hddParameterMenuID.Value & "/" & Me.hddpEmployeeID.Value & "/" & PreviosKetID & "." & strLFileName
    '        Else
    '            hddpKeyIDPicture.Value = String.Empty
    '            hddpNamePicture.Value = String.Empty
    '            hddaImgPictureSignature.Value = String.Empty
    '            imgaImgSignature.ImageUrl = "image/no_signature_icon.png"
    '        End If
    '    Else
    '        hddpKeyIDPicture.Value = String.Empty
    '        hddpNamePicture.Value = String.Empty
    '        hddaImgPictureSignature.Value = String.Empty
    '        imgaImgSignature.ImageUrl = "image/no_signature_icon.png"
    '    End If

    '    Call getData()

    'End Sub

    'Protected Sub btnFileUploadImg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFileUploadImg.Click
    '    hddpKeyIDImg.Value = String.Empty
    '    hddpNameImg.Value = String.Empty
    '    Dim dt As New DataTable
    '    If FileUploadImg.HasFile Then
    '        Dim fileName As String = Path.GetFileName(FileUploadImg.PostedFile.FileName)

    '        Dim fileType As String() = Split(".jpg,.jpeg,.gif,.png", ",")
    '        If Not fileType.Contains(Path.GetExtension(FileUploadImg.FileName)) Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_jpg", "MSG") & "');", True)
    '            Exit Sub
    '        End If

    '        'Dim len As Integer = FileUploadImg.FileContent.Length
    '        'If len > MAX_FILE_LENGTH_MB * 1000000 Then
    '        '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_pic_notover", "MSG") & "');", True)
    '        '    Return
    '        'End If

    '        If fileName <> String.Empty Then
    '            Call UploadFileTemp(fileName, sImg, FileUploadImg)
    '            hddpKeyIDImg.Value = fileName.Split(".")(0)
    '            hddpNameImg.Value = fileName
    '            imgaImgFile.ImageUrl = strPathServer & sImg & "/" & hddParameterMenuID.Value & "/" & Me.hddpEmployeeID.Value & "/" & fileName
    '        End If
    '    End If

    '    Call getData()

    'End Sub
    'Protected Sub btnDeleteImg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteImg.Click

    '    Dim strFileName As Integer = 0
    '    Dim checkFlagFile As String = String.Empty
    '    Dim objFSO As Object
    '    Dim objFolder As Object
    '    Dim objFile As Object
    '    Dim strLFileName As String = String.Empty

    '    If hddpKeyIDImg.Value <> String.Empty Then

    '        Call DeleteSigleFileTemp(hddpNameImg.Value, sImg)
    '        Dim PreviosKetID As Integer = 0
    '        Dim tempKeyID As String = hddpKeyIDImg.Value
    '        If tempKeyID <> String.Empty Then
    '            Dim TempPath As String = String.Empty
    '            Try
    '                If CInt(tempKeyID) > 0 Then
    '                    strFileName = tempKeyID
    '                End If
    '                If strFileName <> 0 Then
    '                    For i As Integer = 0 To strFileName - 1
    '                        If CInt(tempKeyID) > 0 Then
    '                            If strLFileName = String.Empty Then
    '                                Try
    '                                    objFSO = CreateObject("Scripting.FileSystemObject")
    '                                    objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & sImg & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\"))
    '                                    For Each objFile In objFolder.Files
    '                                        Dim strCheckName As String = objFile.Name.ToString.Split(".")(0)
    '                                        If strCheckName = CInt(strFileName - 1) Then
    '                                            strLFileName = objFile.Name.ToString.Split(".")(1)
    '                                            Exit For
    '                                        End If
    '                                    Next objFile
    '                                Catch ex As Exception

    '                                End Try
    '                            End If

    '                            TempPath = Server.MapPath("Uploads\" & sImg & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\" & strFileName & "." & strLFileName)
    '                            If System.IO.File.Exists(TempPath) Then
    '                                checkFlagFile = "1"
    '                                PreviosKetID = strFileName
    '                                Exit For
    '                            End If
    '                            strFileName -= 1
    '                        End If
    '                    Next
    '                End If
    '            Catch ex As Exception

    '            End Try
    '        End If


    '        If strLFileName = String.Empty Then
    '            Try
    '                objFSO = CreateObject("Scripting.FileSystemObject")
    '                objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & sImg & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\"))
    '                For Each objFile In objFolder.Files
    '                    Dim strCheckName As String = objFile.Name.ToString.Split(".")(0)
    '                    If strCheckName = PreviosKetID Then
    '                        strLFileName = objFile.Name.ToString.Split(".")(1)
    '                        Exit For
    '                    End If
    '                Next objFile
    '            Catch ex As Exception

    '            End Try
    '        End If

    '        If PreviosKetID <> 0 Then
    '            hddpKeyIDImg.Value = PreviosKetID
    '            hddpNameImg.Value = PreviosKetID & "." & strLFileName
    '            hddaImgPicture.Value = String.Empty
    '            imgaImgFile.ImageUrl = strPathServer & sImg & "/" & hddParameterMenuID.Value & "/" & Me.hddpEmployeeID.Value & "/" & PreviosKetID & "." & strLFileName
    '        Else
    '            hddpKeyIDImg.Value = String.Empty
    '            hddpNameImg.Value = String.Empty
    '            hddaImgPicture.Value = String.Empty
    '            imgaImgFile.ImageUrl = "image/no_image_icon.png"
    '        End If
    '    Else
    '        hddpKeyIDImg.Value = String.Empty
    '        hddpNameImg.Value = String.Empty
    '        hddaImgPicture.Value = String.Empty
    '        imgaImgFile.ImageUrl = "image/no_image_icon.png"
    '    End If

    '    Call getData()

    'End Sub

#End Region

#Region "ControlPanel"
    Public Sub OpenMain()
        pnMain.Style.Add("display", "")
        pnDialog.Style.Add("display", "none")
    End Sub
    Public Sub OpenDialog()
        pnMain.Style.Add("display", "none")
        pnDialog.Style.Add("display", "")
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
    End Sub
#End Region

#Region "Session"
    Public Function GetDataUserInfo() As List(Of CoreUser)
        Try
            Return Session("IDOCS.application.LoaddataUserInformation")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function SetDataUserInfo(ByVal lstMenu As List(Of CoreUser)) As Boolean
        Try
            Session.Add("IDOCS.application.LoaddataUserInformation", lstMenu)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetDataDep() As List(Of BD11DEPT)
        Try
            Return Session("MST_UserInformation_Department")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetDataDiv() As List(Of CoreDivision)
        Try
            Return Session("MST_UserInformation_Division")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetDataPos() As List(Of CorePosition)
        Try
            Return Session("MST_UserInformation_Position")
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region

#Region "Datatable"
    Public dtProject As New DataTable
    Public dtCompany As New DataTable
    Public Sub CreateDatatableProject(ByRef dt As DataTable)
        dt = New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("UserID")
        dt.Columns.Add("FREPRJNO")
        dt.Columns.Add("FREPRJNM")
    End Sub
    Public Sub ConvertDatatableProject(ByVal lc As DataTable,
                                ByRef dt As DataTable)
        Try

            Dim dr As DataRow
            For Each drUse As DataRow In lc.Rows
                dr = dt.NewRow
                dr.Item("ID") = drUse("ID").ToString
                dr.Item("UserID") = drUse("UserID").ToString
                dr.Item("FREPRJNO") = drUse("FREPRJNO").ToString
                dr.Item("FREPRJNM") = drUse("FREPRJNM").ToString
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "ConvertDatatableProject", ex)
            Dim message As String = "OpenSaveDialog('" + GetResource("msg_alert", "MSG", "0") + "','" + ex.Message.ToString + "','n');"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "callScriptFunction", message, True)
        End Try
    End Sub

    Public Sub CreateDatatableCompany(ByRef dt As DataTable)
        dt = New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("UserID")
        dt.Columns.Add("COMID")
        dt.Columns.Add("FDIVNAMET")
        dt.Columns.Add("FDIVNAME")
    End Sub
    Public Sub ConvertDatatableCompany(ByVal lc As DataTable,
                                ByRef dt As DataTable)
        Try

            Dim dr As DataRow
            For Each drUse As DataRow In lc.Rows
                dr = dt.NewRow
                dr.Item("ID") = drUse("ID").ToString
                dr.Item("UserID") = drUse("UserID").ToString
                dr.Item("COMID") = drUse("COMID").ToString
                dr.Item("FDIVNAMET") = drUse("FDIVNAMET").ToString
                dr.Item("FDIVNAME") = drUse("FDIVNAME").ToString
                dt.Rows.Add(dr)
            Next
        Catch ex As Exception
            HelperLog.ErrorLog(hddpEmployeeID.Value, hddParameterMenuID.Value, Request.UserHostAddress(), "ConvertDatatableCompany", ex)
            Dim message As String = "OpenSaveDialog('" + GetResource("msg_alert", "MSG", "0") + "','" + ex.Message.ToString + "','n');"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "callScriptFunction", message, True)
        End Try
    End Sub
#End Region

#Region "UploadFileTemp"

    'Public Sub CrateFolderTemp(ByVal _Type As String)
    '    Dim strPath As String = Server.MapPath("Uploads\" & _Type & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\")
    '    If (Not System.IO.Directory.Exists(strPath)) Then
    '        System.IO.Directory.CreateDirectory(strPath)
    '    End If
    'End Sub
    'Public Sub UploadFileTemp(ByRef strFileName As String, ByVal _Type As String, ByVal FileUploadTemp As FileUpload)
    '    Call CrateFolderTemp(_Type)
    '    Dim strID As Integer = 0
    '    Dim TempSave As String = String.Empty
    '    Dim TempDelete As String = String.Empty
    '    Dim sDate As String = Date.Now.ToString("yyyyMMddHHmmss")
    '    'Dim objFSO As Object
    '    'Dim objFolder As Object
    '    'Dim objFile As Object

    '    'Try
    '    '    objFSO = CreateObject("Scripting.FileSystemObject")
    '    '    objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & _Type & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\"))
    '    '    For Each objFile In objFolder.Files
    '    '        TempSave = Server.MapPath("Uploads/" & _Type & "/" & hddParameterMenuID.Value & "/" & Me.hddpEmployeeID.Value & "/" & strID & "." & objFile.Name.ToString.Split(".")(1))
    '    '        TempDelete = Server.MapPath("Uploads\" & _Type & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\" & objFile.Name)
    '    '        If System.IO.File.Exists(TempDelete) Then
    '    '            If strID & "." & objFile.Name.ToString.Split(".")(1) <> objFile.Name Then
    '    '                System.IO.File.Copy(TempDelete, TempSave)
    '    '                System.IO.File.Delete(TempDelete)
    '    '            End If
    '    '        End If
    '    '        strID += 1
    '    '    Next objFile
    '    'Catch ex As Exception
    '    'End Try

    '    TempSave = Server.MapPath("Uploads/" & _Type & "/" & hddParameterMenuID.Value & "/" & Me.hddpEmployeeID.Value & "/" & strID & sDate & "." & strFileName.Split(".")(1))
    '    TempDelete = Server.MapPath("Uploads\" & _Type & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\" & strID & sDate & "." & strFileName.Split(".")(1))
    '    If System.IO.File.Exists(TempDelete) Then
    '        System.IO.File.Delete(TempDelete)
    '        FileUploadTemp.PostedFile.SaveAs(TempSave)
    '    Else
    '        FileUploadTemp.PostedFile.SaveAs(TempSave)
    '    End If
    '    strFileName = strID & sDate & "." & strFileName.Split(".")(1)
    'End Sub
    'Public Sub DeleteAllFileTemp(ByVal _Type As String)
    '    Dim objFSO As Object
    '    Dim objFolder As Object
    '    Dim objFile As Object

    '    Try
    '        objFSO = CreateObject("Scripting.FileSystemObject")
    '        objFolder = objFSO.GetFolder(Server.MapPath("Uploads\" & _Type & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\"))
    '        For Each objFile In objFolder.Files
    '            Dim TempDelete As String = Server.MapPath("Uploads\" & _Type & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\" & objFile.Name)
    '            If System.IO.File.Exists(TempDelete) Then
    '                System.IO.File.Delete(TempDelete)
    '            End If
    '        Next objFile
    '    Catch ex As Exception

    '    End Try

    '    Dim strPath As String = Server.MapPath("Uploads\" & _Type & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\")

    '    If System.IO.Directory.Exists(strPath) Then
    '        System.IO.Directory.Delete(strPath)
    '    End If

    'End Sub
    'Public Sub DeleteSigleFileTemp(ByVal strName As String, ByVal _Type As String)
    '    Try
    '        Dim TempDelete As String = Server.MapPath("Uploads\" & _Type & "\" & hddParameterMenuID.Value & "\" & Me.hddpEmployeeID.Value & "\" & strName)

    '        If System.IO.File.Exists(TempDelete) Then
    '            System.IO.File.Delete(TempDelete)
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
#End Region


End Class
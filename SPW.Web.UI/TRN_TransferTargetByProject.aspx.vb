Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO

Public Class TRN_TransferTargetByProject
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hddParameterMenuID.Value = HelperLog.LoadMenuID("TRN_TransferTargetByProject.aspx")

                Call LoadInit()
                Call ClearText()
                'Call setTextFocus()
                Call LoadDropdownlist()
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
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
            Else
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select", "MSG", "1"), ""))
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

#End Region

#Region "Event DropDownlist"
    Protected Sub ddlsProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsProject.SelectedIndexChanged
        Try
            Dim strProject As String = String.Empty
            'Call SetCiterail(txtsProject.Text, strProject)
            strProject = ddlsProject.SelectedValue
            If strProject <> String.Empty Then
                Call Loaddata()
            Else
                Call ClearText()
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "ddlsProject_SelectedIndexChanged", ex)
        End Try
    End Sub

#End Region
#Region "Event Text"
    Protected Sub txtsYear_TextChanged(sender As Object, e As EventArgs) Handles txtsYear.TextChanged
        Try
            If txtsYear.Text <> String.Empty Then
                Call Loaddata()
            Else
                Call ClearText()
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "txtsYear_TextChanged", ex)
        End Try
    End Sub
#End Region

#Region "Loaddata"
    Public Sub LoadInit()
        lblMain1.Text = Me.GetResource("main_label", "Text", "1")
        lblMain2.Text = Me.GetResource("lblMain2", "Text", hddParameterMenuID.Value)
        lblMain3.Text = Me.GetResource("lblMain3", "Text", hddParameterMenuID.Value)

        'lblsSave.Text = Me.GetResource("lblsSave", "Text", hddParameterMenuID.Value)
        'lblsCancel.Text = Me.GetResource("lblsCancel", "Text", hddParameterMenuID.Value)
        lblsYear.Text = Me.GetResource("lblsYear", "Text", hddParameterMenuID.Value)
        lblsProject.Text = Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value)

        lblaPeriod.Text = Me.GetResource("lblaPeriod", "Text", hddParameterMenuID.Value)
        lblaTotalRoom.Text = Me.GetResource("lblaTotalRoom", "Text", hddParameterMenuID.Value)
        lblaTotalCash.Text = Me.GetResource("lblaTotalCash", "Text", hddParameterMenuID.Value)
        lblaTotalQTY.Text = Me.GetResource("lblaTotalQTY", "Text", hddParameterMenuID.Value)
        lblaTotalAmount.Text = Me.GetResource("lblaTotalAmount", "Text", hddParameterMenuID.Value)

        lblMassage1.Text = Me.GetResource("msg_required", "MSG", "1")
        lblMassage2.Text = grtt("resPleaseSelect")

        txtsYear.Text = Date.Today.Year.ToString()

        Call ControlValidate()
    End Sub
    Public Sub Loaddata()
        Try
            Call ClearText()

            Dim strProject As String = String.Empty
            'Call SetCiterail(txtsProject.Text, strProject)
            strProject = ddlsProject.SelectedValue

            Dim bl As cTransferTargetByProject = New cTransferTargetByProject
            Dim lcSalesMan As List(Of LD421PTR) = bl.Loaddata(strProject,
                                                              txtsYear.Text)
            If lcSalesMan IsNot Nothing Then
                If lcSalesMan.Count > 0 Then
                    For Each sublcSalesMan As LD421PTR In lcSalesMan
                        Try
                            txtaTotalRoom1.Text = IIf(sublcSalesMan.FTARGETQ1 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ1)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom1.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash1.Text = IIf(sublcSalesMan.FTARGET1 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET1)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash1.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom2.Text = IIf(sublcSalesMan.FTARGETQ2 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ2)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom2.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash2.Text = IIf(sublcSalesMan.FTARGET2 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET2)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash2.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom3.Text = IIf(sublcSalesMan.FTARGETQ3 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ3)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom3.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash3.Text = IIf(sublcSalesMan.FTARGET3 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET3)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash3.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom4.Text = IIf(sublcSalesMan.FTARGETQ4 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ4)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom4.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash4.Text = IIf(sublcSalesMan.FTARGET4 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET4)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash4.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom5.Text = IIf(sublcSalesMan.FTARGETQ5 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ5)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom5.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash5.Text = IIf(sublcSalesMan.FTARGET5 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET5)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash5.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom6.Text = IIf(sublcSalesMan.FTARGETQ6 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ6)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom6.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash6.Text = IIf(sublcSalesMan.FTARGET6 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET6)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash6.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom7.Text = IIf(sublcSalesMan.FTARGETQ7 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ7)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom7.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash7.Text = IIf(sublcSalesMan.FTARGET7 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET7)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash7.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom8.Text = IIf(sublcSalesMan.FTARGETQ8 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ8)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom8.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash8.Text = IIf(sublcSalesMan.FTARGET8 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET8)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash8.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom9.Text = IIf(sublcSalesMan.FTARGETQ9 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ9)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom9.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash9.Text = IIf(sublcSalesMan.FTARGET9 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET9)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash9.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom10.Text = IIf(sublcSalesMan.FTARGETQ10 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ10)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom10.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash10.Text = IIf(sublcSalesMan.FTARGET10 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET10)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash10.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom11.Text = IIf(sublcSalesMan.FTARGETQ11 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ11)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom11.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash11.Text = IIf(sublcSalesMan.FTARGET11 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET11)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash11.Text = String.Empty
                        End Try
                        Try
                            txtaTotalRoom12.Text = IIf(sublcSalesMan.FTARGETQ12 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGETQ12)), String.Empty)
                        Catch ex As Exception
                            txtaTotalRoom12.Text = String.Empty
                        End Try
                        Try
                            txtaTotalCash12.Text = IIf(sublcSalesMan.FTARGET12 IsNot Nothing, String.Format("{0:N2}", CDec(sublcSalesMan.FTARGET12)), String.Empty)
                        Catch ex As Exception
                            txtaTotalCash12.Text = String.Empty
                        End Try
                    Next
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "LodaCalTotal", "fnCalTotal();", True)
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "Loaddata", ex)
        End Try
    End Sub
    Public Sub ClearText()
        txtaTotalRoom1.Text = String.Empty
        txtaTotalCash1.Text = String.Empty

        txtaTotalRoom2.Text = String.Empty
        txtaTotalCash2.Text = String.Empty

        txtaTotalRoom3.Text = String.Empty
        txtaTotalCash3.Text = String.Empty

        txtaTotalRoom4.Text = String.Empty
        txtaTotalCash4.Text = String.Empty

        txtaTotalRoom5.Text = String.Empty
        txtaTotalCash5.Text = String.Empty

        txtaTotalRoom6.Text = String.Empty
        txtaTotalCash6.Text = String.Empty

        txtaTotalRoom7.Text = String.Empty
        txtaTotalCash7.Text = String.Empty

        txtaTotalRoom8.Text = String.Empty
        txtaTotalCash8.Text = String.Empty

        txtaTotalRoom9.Text = String.Empty
        txtaTotalCash9.Text = String.Empty

        txtaTotalRoom10.Text = String.Empty
        txtaTotalCash10.Text = String.Empty

        txtaTotalRoom11.Text = String.Empty
        txtaTotalCash11.Text = String.Empty

        txtaTotalRoom12.Text = String.Empty
        txtaTotalCash12.Text = String.Empty

        txtaTotalRoom.Text = String.Empty
        txtaTotalCash.Text = String.Empty
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
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            Dim strProject As String = String.Empty
            'Call SetCiterail(txtsProject.Text, strProject)
            strProject = ddlsProject.SelectedValue
            'If txtsYear.Text = String.Empty Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_required", "MSG", "1") & " " & Me.GetResource("lblsYear", "Text", hddParameterMenuID.Value) & "');", True)
            '    Exit Sub
            'End If
            'If ddlsProject.SelectedIndex = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_required", "MSG", "1") & " " & Me.GetResource("lblsProject", "Text", hddParameterMenuID.Value) & "');", True)
            '    Exit Sub
            'End If

            Dim dt As New DataTable
            Dim bl As cTransferTargetByProject = New cTransferTargetByProject
            dt = GetConvertDatatable()
            If Not bl.addData(dt,
                              strProject,
                              txtsYear.Text,
                              hddaTotalRoom.Value,
                              hddaTotalCash.Value,
                              Me.CurrentUser.UserID) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_savenotsuccess", "MSG", "1") & "');fnCalTotal();", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogSuccess('" & GetResource("msg_savesuccess", "MSG", "1") & "');fnCalTotal();", True)
            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnSave_Click", ex)
        End Try
    End Sub
    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Call Loaddata()
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnCancel_Click", ex)
        End Try
    End Sub

#End Region

#Region "Datatable"
    Public Sub CreateHeaderDatatable(ByRef dt As DataTable)
        dt.Columns.Add("Period")
        dt.Columns.Add("TotalRoom")
        dt.Columns.Add("TotalCash")
    End Sub
    Public Function GetConvertDatatable() As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Call CreateHeaderDatatable(dt)
        '1
        dr = dt.NewRow
        If txtaPeriod1.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod1.Text
        End If
        If txtaTotalRoom1.Text <> String.Empty And
           txtaTotalRoom1.Text <> "0" Then
            dr.Item("TotalRoom") = txtaTotalRoom1.Text
        End If
        If txtaTotalCash1.Text <> String.Empty And
           txtaTotalCash1.Text <> "0" Then
            dr.Item("TotalCash") = txtaTotalCash1.Text
        End If
        dt.Rows.Add(dr)

        '2
        dr = dt.NewRow
        If txtaPeriod2.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod2.Text
        End If
        If txtaTotalRoom2.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom2.Text
        End If
        If txtaTotalCash2.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash2.Text
        End If
        dt.Rows.Add(dr)

        '3
        dr = dt.NewRow
        If txtaPeriod3.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod3.Text
        End If
        If txtaTotalRoom3.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom3.Text
        End If
        If txtaTotalCash3.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash3.Text
        End If
        dt.Rows.Add(dr)

        '4
        dr = dt.NewRow
        If txtaPeriod4.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod4.Text
        End If
        If txtaTotalRoom4.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom4.Text
        End If
        If txtaTotalCash4.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash4.Text
        End If
        dt.Rows.Add(dr)

        '5
        dr = dt.NewRow
        If txtaPeriod5.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod5.Text
        End If
        If txtaTotalRoom5.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom5.Text
        End If
        If txtaTotalCash5.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash5.Text
        End If
        dt.Rows.Add(dr)

        '6
        dr = dt.NewRow
        If txtaPeriod6.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod6.Text
        End If
        If txtaTotalRoom6.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom6.Text
        End If
        If txtaTotalCash6.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash6.Text
        End If
        dt.Rows.Add(dr)

        '7
        dr = dt.NewRow
        If txtaPeriod7.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod7.Text
        End If
        If txtaTotalRoom7.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom7.Text
        End If
        If txtaTotalCash7.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash7.Text
        End If
        dt.Rows.Add(dr)

        '8
        dr = dt.NewRow
        If txtaPeriod8.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod8.Text
        End If
        If txtaTotalRoom8.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom8.Text
        End If
        If txtaTotalCash8.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash8.Text
        End If
        dt.Rows.Add(dr)

        '9
        dr = dt.NewRow
        If txtaPeriod9.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod9.Text
        End If
        If txtaTotalRoom9.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom9.Text
        End If
        If txtaTotalCash9.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash9.Text
        End If
        dt.Rows.Add(dr)

        '10
        dr = dt.NewRow
        If txtaPeriod10.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod10.Text
        End If
        If txtaTotalRoom10.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom10.Text
        End If
        If txtaTotalCash10.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash10.Text
        End If
        dt.Rows.Add(dr)

        '11
        dr = dt.NewRow
        If txtaPeriod11.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod11.Text
        End If
        If txtaTotalRoom11.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom11.Text
        End If
        If txtaTotalCash11.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash11.Text
        End If
        dt.Rows.Add(dr)

        '12
        dr = dt.NewRow
        If txtaPeriod12.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod12.Text
        End If
        If txtaTotalRoom12.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom12.Text
        End If
        If txtaTotalCash12.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash12.Text
        End If
        dt.Rows.Add(dr)

        Return dt
    End Function
#End Region

#Region "Control Validate"
    Public Sub ControlValidate()
        lblMassage1.Style.Add("display", "none")
        lblMassage2.Style.Add("display", "none")
    End Sub
#End Region

#Region "setTextFocus"
    Public Sub setTextFocus()
        txtaTotalRoom1.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom2.ClientID & "','','" & txtaTotalCash1.ClientID & "','');")
        txtaTotalRoom2.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom3.ClientID & "','" & txtaTotalRoom1.ClientID & "','" & txtaTotalCash2.ClientID & "','');")
        txtaTotalRoom3.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom4.ClientID & "','" & txtaTotalRoom2.ClientID & "','" & txtaTotalCash3.ClientID & "','');")
        txtaTotalRoom4.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom5.ClientID & "','" & txtaTotalRoom3.ClientID & "','" & txtaTotalCash4.ClientID & "','');")
        txtaTotalRoom5.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom6.ClientID & "','" & txtaTotalRoom4.ClientID & "','" & txtaTotalCash5.ClientID & "','');")
        txtaTotalRoom6.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom7.ClientID & "','" & txtaTotalRoom5.ClientID & "','" & txtaTotalCash6.ClientID & "','');")
        txtaTotalRoom7.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom8.ClientID & "','" & txtaTotalRoom6.ClientID & "','" & txtaTotalCash7.ClientID & "','');")
        txtaTotalRoom8.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom9.ClientID & "','" & txtaTotalRoom7.ClientID & "','" & txtaTotalCash8.ClientID & "','');")
        txtaTotalRoom9.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom10.ClientID & "','" & txtaTotalRoom8.ClientID & "','" & txtaTotalCash9.ClientID & "','');")
        txtaTotalRoom10.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom11.ClientID & "','" & txtaTotalRoom9.ClientID & "','" & txtaTotalCash10.ClientID & "','');")
        txtaTotalRoom11.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalRoom12.ClientID & "','" & txtaTotalRoom10.ClientID & "','" & txtaTotalCash11.ClientID & "','');")
        txtaTotalRoom12.Attributes.Add("onkeyup", "setNextFocus(event,'','" & txtaTotalRoom11.ClientID & "','" & txtaTotalCash12.ClientID & "','');")

        txtaTotalCash1.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash2.ClientID & "','','','" & txtaTotalRoom1.ClientID & "');")
        txtaTotalCash2.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash3.ClientID & "','" & txtaTotalCash1.ClientID & "','','" & txtaTotalRoom2.ClientID & "');")
        txtaTotalCash3.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash4.ClientID & "','" & txtaTotalCash2.ClientID & "','','" & txtaTotalRoom3.ClientID & "');")
        txtaTotalCash4.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash5.ClientID & "','" & txtaTotalCash3.ClientID & "','','" & txtaTotalRoom4.ClientID & "');")
        txtaTotalCash5.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash6.ClientID & "','" & txtaTotalCash4.ClientID & "','','" & txtaTotalRoom5.ClientID & "');")
        txtaTotalCash6.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash7.ClientID & "','" & txtaTotalCash5.ClientID & "','','" & txtaTotalRoom6.ClientID & "');")
        txtaTotalCash7.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash8.ClientID & "','" & txtaTotalCash6.ClientID & "','','" & txtaTotalRoom7.ClientID & "');")
        txtaTotalCash8.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash9.ClientID & "','" & txtaTotalCash7.ClientID & "','','" & txtaTotalRoom8.ClientID & "');")
        txtaTotalCash9.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash10.ClientID & "','" & txtaTotalCash8.ClientID & "','','" & txtaTotalRoom9.ClientID & "');")
        txtaTotalCash10.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash11.ClientID & "','" & txtaTotalCash9.ClientID & "','','" & txtaTotalRoom10.ClientID & "');")
        txtaTotalCash11.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash12.ClientID & "','" & txtaTotalCash10.ClientID & "','','" & txtaTotalRoom11.ClientID & "');")
        txtaTotalCash12.Attributes.Add("onkeyup", "setNextFocus(event,'','" & txtaTotalCash11.ClientID & "','','" & txtaTotalRoom12.ClientID & "');")
    End Sub
#End Region
End Class
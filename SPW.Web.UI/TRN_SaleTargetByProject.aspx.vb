Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO

Public Class TRN_SaleTargetByProject
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                hddParameterMenuID.Value = HelperLog.LoadMenuID("TRN_SaleTargetByProject.aspx")

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
        Call LoadProject(ddlsProject, "S")

    End Sub
    Public Sub LoadProject(ByVal ddl As DropDownList,
                           ByVal strType As String)
        Dim bl As cPermission = New cPermission
        Try
            If strType = "A" Then
                ddl.Items.Insert(0, New ListItem(GetWebMessage("msg_select_all", "MSG", "1"), ""))
            ElseIf strType = "S" Then
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
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "LoadDepartment", ex.GetBaseException)
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
            HelperLog.ErrorLog(CurrentUser.UserID, hddParameterMenuID.Value, Request.UserHostAddress(), "btnReload_Click", ex)
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
        lblaJusSun.Text = Me.GetResource("lblaJusSun", "Text", hddParameterMenuID.Value)

        lblaPeriod.Text = Me.GetResource("lblaPeriod", "Text", hddParameterMenuID.Value)
        lblaTotalRoom.Text = Me.GetResource("lblaTotalRoom", "Text", hddParameterMenuID.Value)
        lblaTotalCash.Text = Me.GetResource("lblaTotalCash", "Text", hddParameterMenuID.Value)
        lblaSupervisor.Text = Me.GetResource("lblaSupervisor", "Text", hddParameterMenuID.Value)
        lblaSaleMan.Text = Me.GetResource("lblaSaleMan", "Text", hddParameterMenuID.Value)
        lblaReserv.Text = Me.GetResource("lblaReserv", "Text", hddParameterMenuID.Value)
        lblaTransfer.Text = Me.GetResource("lblaTransfer", "Text", hddParameterMenuID.Value)
        lblaTotal.Text = Me.GetResource("lblaTotal", "Text", hddParameterMenuID.Value)

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
            Dim bl As cSalesTargetByProject = New cSalesTargetByProject
            Dim lcSalesMan As List(Of LD421PTY) = bl.Loaddata(strProject,
                                                              txtsYear.Text)
            If lcSalesMan IsNot Nothing Then
                If lcSalesMan.Count > 0 Then
                    For Each sublcSalesMan As LD421PTY In lcSalesMan
                        '1
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
                        If sublcSalesMan.FSUPCODE1 IsNot Nothing Then
                            txtaSupervisor1.Text = sublcSalesMan.FSUPCODE1
                        Else
                            txtaSupervisor1.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST1 IsNot Nothing Then
                            txtaSaleMan1.Text = sublcSalesMan.FSMLIST1
                        Else
                            txtaSaleMan1.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA1 IsNot Nothing Then
                            txtaReserv1.Text = sublcSalesMan.FCOMALTA1
                        Else
                            txtaReserv1.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB1 IsNot Nothing Then
                            txtaTransfer1.Text = sublcSalesMan.FCOMALTB1
                        Else
                            txtaTransfer1.Text = String.Empty
                        End If

                        '2
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
                        If sublcSalesMan.FSUPCODE2 IsNot Nothing Then
                            txtaSupervisor2.Text = sublcSalesMan.FSUPCODE2
                        Else
                            txtaSupervisor2.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST2 IsNot Nothing Then
                            txtaSaleMan2.Text = sublcSalesMan.FSMLIST2
                        Else
                            txtaSaleMan2.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA2 IsNot Nothing Then
                            txtaReserv2.Text = sublcSalesMan.FCOMALTA2
                        Else
                            txtaReserv2.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB2 IsNot Nothing Then
                            txtaTransfer2.Text = sublcSalesMan.FCOMALTB2
                        Else
                            txtaTransfer2.Text = String.Empty
                        End If

                        '3
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
                        If sublcSalesMan.FSUPCODE3 IsNot Nothing Then
                            txtaSupervisor3.Text = sublcSalesMan.FSUPCODE3
                        Else
                            txtaSupervisor3.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST3 IsNot Nothing Then
                            txtaSaleMan3.Text = sublcSalesMan.FSMLIST3
                        Else
                            txtaSaleMan3.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA3 IsNot Nothing Then
                            txtaReserv3.Text = sublcSalesMan.FCOMALTA3
                        Else
                            txtaReserv3.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB3 IsNot Nothing Then
                            txtaTransfer3.Text = sublcSalesMan.FCOMALTB3
                        Else
                            txtaTransfer3.Text = String.Empty
                        End If

                        '4
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
                        If sublcSalesMan.FSUPCODE4 IsNot Nothing Then
                            txtaSupervisor4.Text = sublcSalesMan.FSUPCODE4
                        Else
                            txtaSupervisor4.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST4 IsNot Nothing Then
                            txtaSaleMan4.Text = sublcSalesMan.FSMLIST4
                        Else
                            txtaSaleMan4.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA4 IsNot Nothing Then
                            txtaReserv4.Text = sublcSalesMan.FCOMALTA4
                        Else
                            txtaReserv4.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB4 IsNot Nothing Then
                            txtaTransfer4.Text = sublcSalesMan.FCOMALTB4
                        Else
                            txtaTransfer4.Text = String.Empty
                        End If

                        '5
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
                        If sublcSalesMan.FSUPCODE5 IsNot Nothing Then
                            txtaSupervisor5.Text = sublcSalesMan.FSUPCODE5
                        Else
                            txtaSupervisor5.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST5 IsNot Nothing Then
                            txtaSaleMan5.Text = sublcSalesMan.FSMLIST5
                        Else
                            txtaSaleMan5.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA5 IsNot Nothing Then
                            txtaReserv5.Text = sublcSalesMan.FCOMALTA5
                        Else
                            txtaReserv5.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB5 IsNot Nothing Then
                            txtaTransfer5.Text = sublcSalesMan.FCOMALTB5
                        Else
                            txtaTransfer5.Text = String.Empty
                        End If

                        '6
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
                        If sublcSalesMan.FSUPCODE6 IsNot Nothing Then
                            txtaSupervisor6.Text = sublcSalesMan.FSUPCODE6
                        Else
                            txtaSupervisor6.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST6 IsNot Nothing Then
                            txtaSaleMan6.Text = sublcSalesMan.FSMLIST6
                        Else
                            txtaSaleMan6.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA6 IsNot Nothing Then
                            txtaReserv6.Text = sublcSalesMan.FCOMALTA6
                        Else
                            txtaReserv6.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB6 IsNot Nothing Then
                            txtaTransfer6.Text = sublcSalesMan.FCOMALTB6
                        Else
                            txtaTransfer6.Text = String.Empty
                        End If

                        '7
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
                        If sublcSalesMan.FSUPCODE7 IsNot Nothing Then
                            txtaSupervisor7.Text = sublcSalesMan.FSUPCODE7
                        Else
                            txtaSupervisor7.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST7 IsNot Nothing Then
                            txtaSaleMan7.Text = sublcSalesMan.FSMLIST7
                        Else
                            txtaSaleMan7.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA7 IsNot Nothing Then
                            txtaReserv7.Text = sublcSalesMan.FCOMALTA7
                        Else
                            txtaReserv7.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB7 IsNot Nothing Then
                            txtaTransfer7.Text = sublcSalesMan.FCOMALTB7
                        Else
                            txtaTransfer7.Text = String.Empty
                        End If

                        '8
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
                        If sublcSalesMan.FSUPCODE8 IsNot Nothing Then
                            txtaSupervisor8.Text = sublcSalesMan.FSUPCODE8
                        Else
                            txtaSupervisor8.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST8 IsNot Nothing Then
                            txtaSaleMan8.Text = sublcSalesMan.FSMLIST8
                        Else
                            txtaSaleMan8.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA8 IsNot Nothing Then
                            txtaReserv8.Text = sublcSalesMan.FCOMALTA8
                        Else
                            txtaReserv8.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB8 IsNot Nothing Then
                            txtaTransfer8.Text = sublcSalesMan.FCOMALTB8
                        Else
                            txtaTransfer8.Text = String.Empty
                        End If

                        '9
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
                        If sublcSalesMan.FSUPCODE9 IsNot Nothing Then
                            txtaSupervisor9.Text = sublcSalesMan.FSUPCODE9
                        Else
                            txtaSupervisor9.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST9 IsNot Nothing Then
                            txtaSaleMan9.Text = sublcSalesMan.FSMLIST9
                        Else
                            txtaSaleMan9.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA9 IsNot Nothing Then
                            txtaReserv9.Text = sublcSalesMan.FCOMALTA9
                        Else
                            txtaReserv9.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB9 IsNot Nothing Then
                            txtaTransfer9.Text = sublcSalesMan.FCOMALTB9
                        Else
                            txtaTransfer9.Text = String.Empty
                        End If

                        '10
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
                        If sublcSalesMan.FSUPCODE10 IsNot Nothing Then
                            txtaSupervisor10.Text = sublcSalesMan.FSUPCODE10
                        Else
                            txtaSupervisor10.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST10 IsNot Nothing Then
                            txtaSaleMan10.Text = sublcSalesMan.FSMLIST10
                        Else
                            txtaSaleMan10.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA10 IsNot Nothing Then
                            txtaReserv10.Text = sublcSalesMan.FCOMALTA10
                        Else
                            txtaReserv10.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB10 IsNot Nothing Then
                            txtaTransfer10.Text = sublcSalesMan.FCOMALTB10
                        Else
                            txtaTransfer10.Text = String.Empty
                        End If

                        '11
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
                        If sublcSalesMan.FSUPCODE11 IsNot Nothing Then
                            txtaSupervisor11.Text = sublcSalesMan.FSUPCODE11
                        Else
                            txtaSupervisor11.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST11 IsNot Nothing Then
                            txtaSaleMan11.Text = sublcSalesMan.FSMLIST11
                        Else
                            txtaSaleMan11.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA11 IsNot Nothing Then
                            txtaReserv11.Text = sublcSalesMan.FCOMALTA11
                        Else
                            txtaReserv11.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB11 IsNot Nothing Then
                            txtaTransfer11.Text = sublcSalesMan.FCOMALTB11
                        Else
                            txtaTransfer11.Text = String.Empty
                        End If

                        '12
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
                        If sublcSalesMan.FSUPCODE12 IsNot Nothing Then
                            txtaSupervisor12.Text = sublcSalesMan.FSUPCODE12
                        Else
                            txtaSupervisor12.Text = String.Empty
                        End If
                        If sublcSalesMan.FSMLIST12 IsNot Nothing Then
                            txtaSaleMan12.Text = sublcSalesMan.FSMLIST12
                        Else
                            txtaSaleMan12.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTA12 IsNot Nothing Then
                            txtaReserv12.Text = sublcSalesMan.FCOMALTA12
                        Else
                            txtaReserv12.Text = String.Empty
                        End If
                        If sublcSalesMan.FCOMALTB12 IsNot Nothing Then
                            txtaTransfer12.Text = sublcSalesMan.FCOMALTB12
                        Else
                            txtaTransfer12.Text = String.Empty
                        End If
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
        txtaSupervisor1.Text = String.Empty
        txtaSaleMan1.Text = String.Empty
        txtaReserv1.Text = String.Empty
        txtaTransfer1.Text = String.Empty

        txtaTotalRoom2.Text = String.Empty
        txtaTotalCash2.Text = String.Empty
        txtaSupervisor2.Text = String.Empty
        txtaSaleMan2.Text = String.Empty
        txtaReserv2.Text = String.Empty
        txtaTransfer2.Text = String.Empty

        txtaTotalRoom3.Text = String.Empty
        txtaTotalCash3.Text = String.Empty
        txtaSupervisor3.Text = String.Empty
        txtaSaleMan3.Text = String.Empty
        txtaReserv3.Text = String.Empty
        txtaTransfer3.Text = String.Empty

        txtaTotalRoom4.Text = String.Empty
        txtaTotalCash4.Text = String.Empty
        txtaSupervisor4.Text = String.Empty
        txtaSaleMan4.Text = String.Empty
        txtaReserv4.Text = String.Empty
        txtaTransfer4.Text = String.Empty

        txtaTotalRoom5.Text = String.Empty
        txtaTotalCash5.Text = String.Empty
        txtaSupervisor5.Text = String.Empty
        txtaSaleMan5.Text = String.Empty
        txtaReserv5.Text = String.Empty
        txtaTransfer5.Text = String.Empty

        txtaTotalRoom6.Text = String.Empty
        txtaTotalCash6.Text = String.Empty
        txtaSupervisor6.Text = String.Empty
        txtaSaleMan6.Text = String.Empty
        txtaReserv6.Text = String.Empty
        txtaTransfer6.Text = String.Empty

        txtaTotalRoom7.Text = String.Empty
        txtaTotalCash7.Text = String.Empty
        txtaSupervisor7.Text = String.Empty
        txtaSaleMan7.Text = String.Empty
        txtaReserv7.Text = String.Empty
        txtaTransfer7.Text = String.Empty

        txtaTotalRoom8.Text = String.Empty
        txtaTotalCash8.Text = String.Empty
        txtaSupervisor8.Text = String.Empty
        txtaSaleMan8.Text = String.Empty
        txtaReserv8.Text = String.Empty
        txtaTransfer8.Text = String.Empty

        txtaTotalRoom9.Text = String.Empty
        txtaTotalCash9.Text = String.Empty
        txtaSupervisor9.Text = String.Empty
        txtaSaleMan9.Text = String.Empty
        txtaReserv9.Text = String.Empty
        txtaTransfer9.Text = String.Empty

        txtaTotalRoom10.Text = String.Empty
        txtaTotalCash10.Text = String.Empty
        txtaSupervisor10.Text = String.Empty
        txtaSaleMan10.Text = String.Empty
        txtaReserv10.Text = String.Empty
        txtaTransfer10.Text = String.Empty

        txtaTotalRoom11.Text = String.Empty
        txtaTotalCash11.Text = String.Empty
        txtaSupervisor11.Text = String.Empty
        txtaSaleMan11.Text = String.Empty
        txtaReserv11.Text = String.Empty
        txtaTransfer11.Text = String.Empty

        txtaTotalRoom12.Text = String.Empty
        txtaTotalCash12.Text = String.Empty
        txtaSupervisor12.Text = String.Empty
        txtaSaleMan12.Text = String.Empty
        txtaReserv12.Text = String.Empty
        txtaTransfer12.Text = String.Empty

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
            Dim bl As cSalesTargetByProject = New cSalesTargetByProject
            Dim strCheckData As String = String.Empty
            dt = GetConvertDatatable(strCheckData)
            If strCheckData = String.Empty Then
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
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
                    End If
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "callScriptFunction", "OpenDialogError('" & GetResource("msg_rete_notok", "MSG", "1") & strCheckData & "');fnCalTotal();", True)
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
        dt.Columns.Add("Supervisor")
        dt.Columns.Add("SaleMan")
        dt.Columns.Add("Reserv")
        dt.Columns.Add("Transfer")
    End Sub
    Public Function GetConvertDatatable(ByRef strCheckData As String) As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Call CreateHeaderDatatable(dt)
        '1
        dr = dt.NewRow
        If txtaPeriod1.Text <> String.Empty Then
            dr.Item("Period") = txtaPeriod1.Text
        End If
        If txtaTotalRoom1.Text <> String.Empty Then
            dr.Item("TotalRoom") = txtaTotalRoom1.Text
        End If
        If txtaTotalCash1.Text <> String.Empty Then
            dr.Item("TotalCash") = txtaTotalCash1.Text
        End If
        If txtaSupervisor1.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor1.Text
        End If
        If txtaSaleMan1.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan1.Text
        End If
        If txtaReserv1.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv1.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "1"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "1"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv1.Text
        End If
        If txtaTransfer1.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer1.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "1"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "1"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer1.Text
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
        If txtaSupervisor2.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor2.Text
        End If
        If txtaSaleMan2.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan2.Text
        End If
        If txtaReserv2.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv2.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "2"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "2"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv2.Text
        End If
        If txtaTransfer2.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer2.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "2"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "2"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer2.Text
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
        If txtaSupervisor3.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor3.Text
        End If
        If txtaSaleMan3.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan3.Text
        End If
        If txtaReserv3.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv3.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "3"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "3"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv3.Text
        End If
        If txtaTransfer3.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer3.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "3"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "3"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer3.Text
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
        If txtaSupervisor4.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor4.Text
        End If
        If txtaSaleMan4.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan4.Text
        End If
        If txtaReserv4.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv4.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "4"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "4"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv4.Text
        End If
        If txtaTransfer4.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer4.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "4"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "4"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer4.Text
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
        If txtaSupervisor5.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor5.Text
        End If
        If txtaSaleMan5.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan5.Text
        End If
        If txtaReserv5.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv5.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "5"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "5"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv5.Text
        End If
        If txtaTransfer5.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer5.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "5"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "5"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer5.Text
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
        If txtaSupervisor6.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor6.Text
        End If
        If txtaSaleMan6.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan6.Text
        End If
        If txtaReserv6.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv6.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "6"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "6"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv6.Text
        End If
        If txtaTransfer6.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer6.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "6"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "6"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer6.Text
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
        If txtaSupervisor7.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor7.Text
        End If
        If txtaSaleMan7.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan7.Text
        End If
        If txtaReserv7.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv7.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "7"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "7"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv7.Text
        End If
        If txtaTransfer7.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer7.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "7"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "7"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer7.Text
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
        If txtaSupervisor8.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor8.Text
        End If
        If txtaSaleMan8.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan8.Text
        End If
        If txtaReserv8.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv8.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "8"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "8"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv8.Text
        End If
        If txtaTransfer8.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer8.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "8"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "8"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer8.Text
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
        If txtaSupervisor9.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor9.Text
        End If
        If txtaSaleMan9.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan9.Text
        End If
        If txtaReserv9.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv9.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "9"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "9"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv9.Text
        End If
        If txtaTransfer9.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer9.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "9"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "9"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer9.Text
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
        If txtaSupervisor10.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor10.Text
        End If
        If txtaSaleMan10.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan10.Text
        End If
        If txtaReserv10.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv10.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "10"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "10"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv10.Text
        End If
        If txtaTransfer10.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer10.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "10"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "10"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer10.Text
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
        If txtaSupervisor11.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor11.Text
        End If
        If txtaSaleMan11.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan11.Text
        End If
        If txtaReserv11.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv11.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "11"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "11"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv11.Text
        End If
        If txtaTransfer11.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer11.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "11"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "11"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer11.Text
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
        If txtaSupervisor12.Text <> String.Empty Then
            dr.Item("Supervisor") = txtaSupervisor12.Text
        End If
        If txtaSaleMan12.Text <> String.Empty Then
            dr.Item("SaleMan") = txtaSaleMan12.Text
        End If
        If txtaReserv12.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaReserv12.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "12"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "12"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Reserv") = txtaReserv12.Text
        End If
        If txtaTransfer12.Text <> String.Empty Then
            Dim arrayTemp As String() = txtaTransfer12.Text.Split(",")
            If arrayTemp.Length <> 3 Then
                strCheckData = "12"
                Exit Function
            Else
                For Each e As String In arrayTemp
                    If e = String.Empty Then
                        strCheckData = "12"
                        Exit Function
                    End If
                Next
            End If
            dr.Item("Transfer") = txtaTransfer12.Text
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

        txtaTotalCash1.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash2.ClientID & "','','" & txtaSupervisor1.ClientID & "','" & txtaTotalRoom1.ClientID & "');")
        txtaTotalCash2.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash3.ClientID & "','" & txtaTotalCash1.ClientID & "','" & txtaSupervisor2.ClientID & "','" & txtaTotalRoom2.ClientID & "');")
        txtaTotalCash3.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash4.ClientID & "','" & txtaTotalCash2.ClientID & "','" & txtaSupervisor3.ClientID & "','" & txtaTotalRoom3.ClientID & "');")
        txtaTotalCash4.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash5.ClientID & "','" & txtaTotalCash3.ClientID & "','" & txtaSupervisor4.ClientID & "','" & txtaTotalRoom4.ClientID & "');")
        txtaTotalCash5.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash6.ClientID & "','" & txtaTotalCash4.ClientID & "','" & txtaSupervisor5.ClientID & "','" & txtaTotalRoom5.ClientID & "');")
        txtaTotalCash6.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash7.ClientID & "','" & txtaTotalCash5.ClientID & "','" & txtaSupervisor6.ClientID & "','" & txtaTotalRoom6.ClientID & "');")
        txtaTotalCash7.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash8.ClientID & "','" & txtaTotalCash6.ClientID & "','" & txtaSupervisor7.ClientID & "','" & txtaTotalRoom7.ClientID & "');")
        txtaTotalCash8.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash9.ClientID & "','" & txtaTotalCash7.ClientID & "','" & txtaSupervisor8.ClientID & "','" & txtaTotalRoom8.ClientID & "');")
        txtaTotalCash9.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash10.ClientID & "','" & txtaTotalCash8.ClientID & "','" & txtaSupervisor9.ClientID & "','" & txtaTotalRoom9.ClientID & "');")
        txtaTotalCash10.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash11.ClientID & "','" & txtaTotalCash9.ClientID & "','" & txtaSupervisor10.ClientID & "','" & txtaTotalRoom10.ClientID & "');")
        txtaTotalCash11.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTotalCash12.ClientID & "','" & txtaTotalCash10.ClientID & "','" & txtaSupervisor11.ClientID & "','" & txtaTotalRoom11.ClientID & "');")
        txtaTotalCash12.Attributes.Add("onkeyup", "setNextFocus(event,'','" & txtaTotalCash11.ClientID & "','" & txtaSupervisor12.ClientID & "','" & txtaTotalRoom12.ClientID & "');")

        txtaSupervisor1.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor2.ClientID & "','','" & txtaSaleMan1.ClientID & "','" & txtaTotalCash1.ClientID & "');")
        txtaSupervisor2.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor3.ClientID & "','" & txtaSupervisor1.ClientID & "','" & txtaSaleMan2.ClientID & "','" & txtaTotalCash2.ClientID & "');")
        txtaSupervisor3.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor4.ClientID & "','" & txtaSupervisor2.ClientID & "','" & txtaSaleMan3.ClientID & "','" & txtaTotalCash3.ClientID & "');")
        txtaSupervisor4.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor5.ClientID & "','" & txtaSupervisor3.ClientID & "','" & txtaSaleMan4.ClientID & "','" & txtaTotalCash4.ClientID & "');")
        txtaSupervisor5.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor6.ClientID & "','" & txtaSupervisor4.ClientID & "','" & txtaSaleMan5.ClientID & "','" & txtaTotalCash5.ClientID & "');")
        txtaSupervisor6.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor7.ClientID & "','" & txtaSupervisor5.ClientID & "','" & txtaSaleMan6.ClientID & "','" & txtaTotalCash6.ClientID & "');")
        txtaSupervisor7.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor8.ClientID & "','" & txtaSupervisor6.ClientID & "','" & txtaSaleMan7.ClientID & "','" & txtaTotalCash7.ClientID & "');")
        txtaSupervisor8.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor9.ClientID & "','" & txtaSupervisor7.ClientID & "','" & txtaSaleMan8.ClientID & "','" & txtaTotalCash8.ClientID & "');")
        txtaSupervisor9.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor10.ClientID & "','" & txtaSupervisor8.ClientID & "','" & txtaSaleMan9.ClientID & "','" & txtaTotalCash9.ClientID & "');")
        txtaSupervisor10.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor11.ClientID & "','" & txtaSupervisor9.ClientID & "','" & txtaSaleMan10.ClientID & "','" & txtaTotalCash10.ClientID & "');")
        txtaSupervisor11.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSupervisor12.ClientID & "','" & txtaSupervisor10.ClientID & "','" & txtaSaleMan11.ClientID & "','" & txtaTotalCash11.ClientID & "');")
        txtaSupervisor12.Attributes.Add("onkeyup", "setNextFocus(event,'','" & txtaSupervisor11.ClientID & "','" & txtaSaleMan12.ClientID & "','" & txtaTotalCash12.ClientID & "');")

        txtaSaleMan1.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan2.ClientID & "','','" & txtaReserv1.ClientID & "','" & txtaSupervisor1.ClientID & "');")
        txtaSaleMan2.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan3.ClientID & "','" & txtaSaleMan1.ClientID & "','" & txtaReserv2.ClientID & "','" & txtaSupervisor2.ClientID & "');")
        txtaSaleMan3.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan4.ClientID & "','" & txtaSaleMan2.ClientID & "','" & txtaReserv3.ClientID & "','" & txtaSupervisor3.ClientID & "');")
        txtaSaleMan4.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan5.ClientID & "','" & txtaSaleMan3.ClientID & "','" & txtaReserv4.ClientID & "','" & txtaSupervisor4.ClientID & "');")
        txtaSaleMan5.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan6.ClientID & "','" & txtaSaleMan4.ClientID & "','" & txtaReserv5.ClientID & "','" & txtaSupervisor5.ClientID & "');")
        txtaSaleMan6.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan7.ClientID & "','" & txtaSaleMan5.ClientID & "','" & txtaReserv6.ClientID & "','" & txtaSupervisor6.ClientID & "');")
        txtaSaleMan7.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan8.ClientID & "','" & txtaSaleMan6.ClientID & "','" & txtaReserv7.ClientID & "','" & txtaSupervisor7.ClientID & "');")
        txtaSaleMan8.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan9.ClientID & "','" & txtaSaleMan7.ClientID & "','" & txtaReserv8.ClientID & "','" & txtaSupervisor8.ClientID & "');")
        txtaSaleMan9.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan10.ClientID & "','" & txtaSaleMan8.ClientID & "','" & txtaReserv9.ClientID & "','" & txtaSupervisor9.ClientID & "');")
        txtaSaleMan10.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan11.ClientID & "','" & txtaSaleMan9.ClientID & "','" & txtaReserv10.ClientID & "','" & txtaSupervisor10.ClientID & "');")
        txtaSaleMan11.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaSaleMan12.ClientID & "','" & txtaSaleMan10.ClientID & "','" & txtaReserv11.ClientID & "','" & txtaSupervisor11.ClientID & "');")
        txtaSaleMan12.Attributes.Add("onkeyup", "setNextFocus(event,'','" & txtaSaleMan11.ClientID & "','" & txtaReserv12.ClientID & "','" & txtaSupervisor12.ClientID & "');")

        txtaReserv1.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv2.ClientID & "','','" & txtaTransfer1.ClientID & "','" & txtaSaleMan1.ClientID & "');")
        txtaReserv2.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv3.ClientID & "','" & txtaReserv1.ClientID & "','" & txtaTransfer2.ClientID & "','" & txtaSaleMan2.ClientID & "');")
        txtaReserv3.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv4.ClientID & "','" & txtaReserv2.ClientID & "','" & txtaTransfer3.ClientID & "','" & txtaSaleMan3.ClientID & "');")
        txtaReserv4.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv5.ClientID & "','" & txtaReserv3.ClientID & "','" & txtaTransfer4.ClientID & "','" & txtaSaleMan4.ClientID & "');")
        txtaReserv5.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv6.ClientID & "','" & txtaReserv4.ClientID & "','" & txtaTransfer5.ClientID & "','" & txtaSaleMan5.ClientID & "');")
        txtaReserv6.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv7.ClientID & "','" & txtaReserv5.ClientID & "','" & txtaTransfer6.ClientID & "','" & txtaSaleMan6.ClientID & "');")
        txtaReserv7.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv8.ClientID & "','" & txtaReserv6.ClientID & "','" & txtaTransfer7.ClientID & "','" & txtaSaleMan7.ClientID & "');")
        txtaReserv8.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv9.ClientID & "','" & txtaReserv7.ClientID & "','" & txtaTransfer8.ClientID & "','" & txtaSaleMan8.ClientID & "');")
        txtaReserv9.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv10.ClientID & "','" & txtaReserv8.ClientID & "','" & txtaTransfer9.ClientID & "','" & txtaSaleMan9.ClientID & "');")
        txtaReserv10.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv11.ClientID & "','" & txtaReserv9.ClientID & "','" & txtaTransfer10.ClientID & "','" & txtaSaleMan10.ClientID & "');")
        txtaReserv11.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaReserv12.ClientID & "','" & txtaReserv10.ClientID & "','" & txtaTransfer11.ClientID & "','" & txtaSaleMan11.ClientID & "');")
        txtaReserv12.Attributes.Add("onkeyup", "setNextFocus(event,'','" & txtaReserv11.ClientID & "','" & txtaTransfer12.ClientID & "','" & txtaSaleMan12.ClientID & "');")

        txtaTransfer1.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer2.ClientID & "','','','" & txtaReserv1.ClientID & "');")
        txtaTransfer2.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer3.ClientID & "','" & txtaTransfer1.ClientID & "','','" & txtaReserv2.ClientID & "');")
        txtaTransfer3.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer4.ClientID & "','" & txtaTransfer2.ClientID & "','','" & txtaReserv3.ClientID & "');")
        txtaTransfer4.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer5.ClientID & "','" & txtaTransfer3.ClientID & "','','" & txtaReserv4.ClientID & "');")
        txtaTransfer5.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer6.ClientID & "','" & txtaTransfer4.ClientID & "','','" & txtaReserv5.ClientID & "');")
        txtaTransfer6.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer7.ClientID & "','" & txtaTransfer5.ClientID & "','','" & txtaReserv6.ClientID & "');")
        txtaTransfer7.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer8.ClientID & "','" & txtaTransfer6.ClientID & "','','" & txtaReserv7.ClientID & "');")
        txtaTransfer8.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer9.ClientID & "','" & txtaTransfer7.ClientID & "','','" & txtaReserv8.ClientID & "');")
        txtaTransfer9.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer10.ClientID & "','" & txtaTransfer8.ClientID & "','','" & txtaReserv9.ClientID & "');")
        txtaTransfer10.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer11.ClientID & "','" & txtaTransfer9.ClientID & "','','" & txtaReserv10.ClientID & "');")
        txtaTransfer11.Attributes.Add("onkeyup", "setNextFocus(event,'" & txtaTransfer12.ClientID & "','" & txtaTransfer10.ClientID & "','','" & txtaReserv11.ClientID & "');")
        txtaTransfer12.Attributes.Add("onkeyup", "setNextFocus(event,'','" & txtaTransfer11.ClientID & "','','" & txtaReserv12.ClientID & "');")
    End Sub

#End Region
End Class
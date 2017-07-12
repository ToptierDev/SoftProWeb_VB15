Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Configuration
Imports Microsoft.Reporting.WebForms
Public Class RPT_Viewer
    Inherits BasePage
    Private gSQL As New ClsAccessLog_dataset
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call LoadInit()
            'Call LoadReport()
            Call LoadReport()
            HelperLog.AccessLog(Me.CurrentUser.FUSERID, "37", Request.UserHostAddress())
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "document.ready", "SetInitial();", True)
        End If
    End Sub

#Region "LoadData"
    Public Sub LoadInit()
        lblMain1.Text = Me.GetResource("main_label", "Text", "1")
        lblMain2.Text = "Report"


        'lblsMenu.Text = Me.GetResource("lblsMenu", "Text", "37")
        'lblsSubMenu.Text = Me.GetResource("lblsSubMenu", "Text", "37")
        'lblsUser.Text = Me.GetResource("lblsUser", "Text", "37")
        'btnSearch.Text = Me.GetResource("btnSearch", "Text", "37")
        'btnCancel.Text = Me.GetResource("btnCancel", "Text", "37")
    End Sub
    'Private Sub LoadReport()
    '    Try
    '        Dim rptDataSource As New ReportDataSource
    '        Dim ReportName As String = "AccessLogs_RPT"
    '        With Me.ReportViewer1.LocalReport
    '            .ReportPath = "Reports\" & ReportName & ".rdlc"
    '            .DataSources.Clear()
    '        End With
    '        Dim ds As New DataSet

    '        ds = gSQL.AccessLogs(hddMenu.Value, _
    '                             hddSubMenu.Value, _
    '                             hddUser.Value, _
    '                             "AccessLogs")
    '        rptDataSource = New ReportDataSource("AccessLogs", ds.Tables("AccessLogs"))
    '        ReportViewer1.LocalReport.DataSources.Add(rptDataSource)


    '        Dim parameters As New List(Of ReportParameter)
    '        Dim Myparam As ReportParameter
    '        'parameters.Add(New ReportParameter("LG", If(Me.WebCulture.ToUpper = "TH", "T", "E")))

    '        Myparam = New ReportParameter("LG", If(Me.WebCulture.ToUpper = "TH", "T", "E"))
    '        parameters.Add(Myparam)
    '        'parameters.Name = "LG"
    '        'parameters.Values.Add(IIf(Me.WebCulture.ToUpper = "TH", "T", "E"))
    '        ReportViewer1.LocalReport.SetParameters(parameters)

    '        If ds IsNot Nothing Then
    '            'If ds.Tables("AccessLogs").Rows.Count > 0 Then
    '            '    ReportViewer1.Visible = True
    '            'Else
    '            '    ReportViewer1.Visible = False
    '            'End If
    '            ReportViewer1.Visible = True
    '        Else
    '            ReportViewer1.Visible = False
    '        End If

    '        ReportViewer1.PromptAreaCollapsed = True
    '        ReportViewer1.ShowToolBar = True
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub LoadReport()
        Dim ReportUrl As String = String.Empty
        If Request.QueryString("ReportURL") IsNot Nothing Then
            ReportUrl = Request.QueryString("ReportURL").ToString()
        End If

        Dim strRptPath As String = System.Configuration.ConfigurationManager.AppSettings("ReportingService")
        Dim strFolder As String = System.Configuration.ConfigurationManager.AppSettings("ReportingServiceFolder")


        Dim year As String = DateTime.Now.ToString("yyyy", New CultureInfo("en-US"))
        Dim month As String = DateTime.Now.ToString("MM").Substring(1, 1)
        Dim reportfileName As String = ReportUrl


        ReportViewer1.ServerReport.ReportServerUrl = New Uri(strRptPath)
        ReportViewer1.ServerReport.ReportPath = String.Format("{0}{1}", strFolder, reportfileName)

        'Try
        '    Dim parameters As New List(Of ReportParameter)()
        '    parameters.Add(New ReportParameter("user", Me.CurrentUser.FUSERID))
        '    ReportViewer1.ServerReport.SetParameters(parameters)
        'Catch ex As Exception

        'End Try
        ReportViewer1.ShowToolBar = True

    End Sub
#End Region

#Region "Event"

#End Region

End Class
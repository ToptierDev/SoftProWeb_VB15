Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Timers
Imports System.Threading.Tasks
Public Class Main
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMain1.Text = Me.GetResource("main_label", "Text", "1")
        lblMain2.Text = Me.GetResource("main_label", "Text", "1")
        HelperLog.AccessLog(Me.CurrentUser.UserID, "1", Request.UserHostAddress())
    End Sub

End Class
Public Class DatePicker
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strLG As String = String.Empty
        If Session("PRR.application.language") IsNot Nothing Then
            strLG = Session("PRR.application.language")
        End If
        hddMasterLG.Value = strLG
    End Sub
End Class
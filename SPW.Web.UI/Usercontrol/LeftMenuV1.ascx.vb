Public Class LeftMenuV1
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Public Sub Redirect(ByVal url As String, Optional ByVal hasErrored As Boolean = False)

        If Not hasErrored Then
            HttpContext.Current.Server.ClearError()
            HttpContext.Current.Response.Redirect(url, False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        Else
            HttpContext.Current.Server.ClearError()
            HttpContext.Current.Response.Redirect(url, False)
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End If
    End Sub
End Class
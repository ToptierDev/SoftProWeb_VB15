Imports SPW.DAL

Public Class Log_ErrorLog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Using db As New PNSDBWEBEntities
            Dim lc = db.CoreErrorLogs.Where(Function(x) x.ErrorFunction <> "GetResource").OrderByDescending(Function(x) x.ID).Take(20).ToList()

            GridView1.DataSource = lc
            GridView1.DataBind()
        End Using

    End Sub

End Class
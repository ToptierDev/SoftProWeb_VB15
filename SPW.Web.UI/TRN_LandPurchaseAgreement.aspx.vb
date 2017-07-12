Imports SPW.Helper
Public Class TRN_LandPurchaseAgreement
    Inherits BasePage
    Public fd11propDataSource As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Me.setMenuIDByMenuLocation("TRN_LandPurchaseAgreement.aspx")


        Try
            If Not IsPostBack Then
                Page.DataBind()

                HelperLog.AccessLog(Me.CurrentUser.UserID, Me.MenuID, Request.UserHostAddress())
            Else

            End If
        Catch ex As Exception
            HelperLog.ErrorLog(CurrentUser.UserID, Me.MenuID, Request.UserHostAddress(), "Page_Load", ex)
        End Try
    End Sub

    'Public Sub setJsonDataSource()

    '    Using db As New PNSWEBEntities
    '        Dim lc As List(Of FD11PROP) = db.FD11PROP.OrderBy(Function(s) s.FASSETNO).ToList
    '        If pKeyword <> String.Empty Then
    '            lc = db.FD11PROP.Where(Function(s) s.FASSETNO.ToUpper.Replace(" ", "").Contains(pKeyword.ToUpper.Replace(" ", "")) Or
    '                                               s.FPCPIECE.ToUpper.Replace(" ", "").Contains(pKeyword.ToUpper.Replace(" ", ""))).ToList
    '        End If
    '        Return lc.ToList
    '    End Using

    'End Sub


End Class
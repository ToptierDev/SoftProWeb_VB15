﻿Imports SPW.Helper
Public Class TRN_UnitDefineProject
    Inherits BasePage

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

End Class
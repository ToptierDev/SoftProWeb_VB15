Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports SPW.DAL
Imports SPW.BLL

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
<ScriptService()>
Public Class Api_ClearSession
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Sub ClearSessionSideBar()
        Try
            If Session("PRR.application.CheckSlideBar") IsNot Nothing Then
                Session.Remove("PRR.application.CheckSlideBar")
            End If
        Catch ex As Exception

        End Try

    End Sub

End Class
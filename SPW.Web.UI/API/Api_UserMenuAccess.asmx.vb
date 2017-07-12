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
Public Class Api_UserMenuAccess
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetUserID(ByVal ptKeyID$,
                              ByVal ptLG$) As String()
        '
        Dim olc As New List(Of APIUserMenuAccess_ViewModel)
        Dim bl As New cUserMenuAccess
        If ptKeyID = "undefined" Then
            ptKeyID = String.Empty
        End If
        olc = bl.LoadUserID(ptKeyID,
                            ptLG)
        Dim otArray As New List(Of String)
        If olc IsNot Nothing Then
            For Each olst As APIUserMenuAccess_ViewModel In olc
                otArray.Add(String.Format("{0}|{1}", olst.KeyID & " - " & olst.Titles & " " & olst.Description, olst.KeyID & " - " & olst.Titles & " " & olst.Description))
            Next
        End If

        Return otArray.ToArray
    End Function

End Class
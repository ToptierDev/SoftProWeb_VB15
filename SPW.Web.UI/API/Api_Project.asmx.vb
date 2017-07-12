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
Public Class Api_Project
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetAuto(ByVal ptPostal$) As String()
        'LMT
        Dim olc As New List(Of APIProject_ViewModel)
        Dim bl As New APIBL
        If ptPostal = "undefined" Then
            ptPostal = String.Empty
        End If
        olc = bl.GetAuto(ptPostal)
        Dim otArray As New List(Of String)
        If olc IsNot Nothing Then
            For Each olst As APIProject_ViewModel In olc
                otArray.Add(String.Format("{0}-{1}", olst.Description & " " & olst.Postal, olst.Description & " " & olst.Postal))
            Next
        End If

        Return otArray.ToArray
    End Function

    <WebMethod()>
    Public Function GetCodeMat(ByVal ptKeyID$) As String()
        'LMT
        Dim olc As New List(Of String)
        Dim bl As New cProject
        If ptKeyID = "undefined" Then
            ptKeyID = String.Empty
        End If
        olc = bl.LoadCodeMat(ptKeyID)
        Dim otArray As New List(Of String)
        If olc IsNot Nothing Then
            For Each olst In olc
                otArray.Add(String.Format("{0}|{1}", olst, olst.Split("-")(0)))
            Next
        End If

        Return otArray.ToArray
    End Function

End Class
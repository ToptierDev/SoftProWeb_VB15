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
Public Class Api_WebResource
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetMenu(ByVal ptKeyID$,
                            ByVal ptLG$) As String()
        'LMT
        Dim olc As New List(Of String)
        Dim bl As New cWebResouce
        If ptKeyID = "undefined" Then
            ptKeyID = String.Empty
        End If
        olc = bl.LoadMenuNotParent(ptKeyID,
                                   ptLG)
        Dim otArray As New List(Of String)
        If olc IsNot Nothing Then
            For Each olst In olc
                otArray.Add(String.Format("{0}|{1}", olst.Split("-")(1), olst.Split("-")(0)))
            Next
        End If

        Return otArray.ToArray
    End Function

    <WebMethod()>
    Public Function GetSubMenu(ByVal ptKeyID$,
                               ByVal ptLG$,
                               ByVal ptMenu$) As String()
        'LMT
        Dim olc As New List(Of String)
        Dim bl As New cWebResouce
        If ptKeyID = "undefined" Then
            ptKeyID = String.Empty
        End If
        olc = bl.LoadSubMenuByMenu(ptKeyID,
                                   ptLG,
                                   ptMenu)
        Dim otArray As New List(Of String)
        If olc IsNot Nothing Then
            For Each olst In olc
                otArray.Add(String.Format("{0}|{1}", olst.Split("-")(1), olst.Split("-")(0)))
            Next
        End If

        Return otArray.ToArray
    End Function

End Class
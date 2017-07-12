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
Public Class Api_Block
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetProject(ByVal ptKeyID$) As String()
        'LMT
        Dim olc As New List(Of String)
        Dim bl As New cBlock
        If ptKeyID = "undefined" Then
            ptKeyID = String.Empty
        End If
        olc = bl.LoadProject(ptKeyID)
        Dim otArray As New List(Of String)
        If olc IsNot Nothing Then
            For Each olst In olc
                otArray.Add(String.Format("{0}|{1}", olst, olst.Split("-")(0)))
            Next
        End If

        Return otArray.ToArray
    End Function

    <WebMethod()>
    Public Function GetPhase(ByVal ptKeyID$,
                             ByVal ptProject$) As String()
        'LMT
        Dim olc As New List(Of String)
        Dim bl As New cBlock
        If ptKeyID = "undefined" Then
            ptKeyID = String.Empty
        End If
        olc = bl.LoadPhase(ptKeyID,
                           ptProject)
        Dim otArray As New List(Of String)
        If olc IsNot Nothing Then
            For Each olst In olc
                otArray.Add(String.Format("{0}|{1}", olst, olst))
            Next
        End If

        Return otArray.ToArray
    End Function

    <WebMethod()>
    Public Function GetZone(ByVal ptKeyID$,
                            ByVal ptProject$,
                            ByVal ptPhase$) As String()
        'LMT
        Dim olc As New List(Of String)
        Dim bl As New cBlock
        If ptKeyID = "undefined" Then
            ptKeyID = String.Empty
        End If
        olc = bl.LoadZone(ptKeyID,
                          ptProject,
                          ptPhase)
        Dim otArray As New List(Of String)
        If olc IsNot Nothing Then
            For Each olst In olc
                otArray.Add(String.Format("{0}|{1}", olst, olst))
            Next
        End If

        Return otArray.ToArray
    End Function

End Class
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
Public Class Api_DeedSettingProject
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetProject(ByVal ptKeyID$) As String()
        'LMT
        Dim olc As New List(Of String)
        Dim bl As New cDeedSettingProject
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
    Public Function GetDeed(ByVal ptKeyID$) As String()
        'LMT
        Dim olc As New List(Of DeedSettingProject_ViewModel)
        Dim bl As New cDeedSettingProject
        If ptKeyID = "undefined" Then
            ptKeyID = String.Empty
        End If
        olc = bl.LoadDeed(ptKeyID)
        Dim otArray As New List(Of String)
        If olc IsNot Nothing Then
            For Each olst As DeedSettingProject_ViewModel In olc
                Dim TempDate1 As String = String.Empty
                Dim TempDate2 As String = String.Empty
                Dim TempQTY As String = String.Empty
                Dim TempQTY1 As String = "0"
                Dim TempQTY2 As String = "0"
                Dim TempQTY3 As String = "0"
                Dim pParam1 As String = "0"
                Dim pParam2 As String = "0"
                Dim pParam3 As String = "0"
                Dim pParam4 As String = "0"
                Dim pParam5 As String = "0"
                Dim pParam6 As String = "0"
                If Not IsDBNull(olst.FMAINCSTR) Then
                    If olst.FMAINCSTR IsNot Nothing Then
                        TempDate1 = CDate(olst.FMAINCSTR.ToString.Split(" ")(0)).ToString("dd/MM/yyyy")
                    End If
                End If
                If Not IsDBNull(olst.FMAINCEND) Then
                    If olst.FMAINCEND IsNot Nothing Then
                        TempDate2 = CDate(olst.FMAINCEND.ToString.Split(" ")(0)).ToString("dd/MM/yyyy")
                    End If
                End If
                If Not IsDBNull(olst.FQTY) Then
                    If olst.FQTY IsNot Nothing Then
                        Call CalReverse(olst.FQTY,
                                        TempQTY1,
                                        TempQTY2,
                                        TempQTY3,
                                        String.Empty)
                        TempQTY = String.Format("{0:N0}", CDec(TempQTY1)) & "-" &
                                  String.Format("{0:N0}", CDec(TempQTY2)) & "-" &
                                  String.Format("{0:N0}", CDec(TempQTY3))

                    End If
                End If
                If Not IsDBNull(olst.FQTYADJPLUS) Then
                    If olst.FQTYADJPLUS IsNot Nothing Then
                        If olst.FQTYADJNPLUS.ToString.IndexOf("-") = "-1" Then
                            Call CalReverse(olst.FQTYADJPLUS,
                                        pParam1,
                                        pParam2,
                                        pParam3,
                                        String.Empty)
                            pParam1 = String.Format("{0:N0}", CInt(pParam1))
                            pParam2 = String.Format("{0:N0}", CInt(pParam2))
                            pParam3 = String.Format("{0:N0}", CInt(pParam3))
                        End If
                    End If
                End If
                If Not IsDBNull(olst.FQTYADJNPLUS) Then
                    If olst.FQTYADJNPLUS IsNot Nothing Then
                        If olst.FQTYADJNPLUS.ToString.IndexOf("-") = "0" Then
                            Call CalReverse(olst.FQTYADJNPLUS,
                                            pParam4,
                                            pParam5,
                                            pParam6,
                                            "-")
                            pParam4 = String.Format("{0:N0}", CInt(pParam4))
                            pParam5 = String.Format("{0:N0}", CInt(pParam5))
                            pParam6 = String.Format("{0:N0}", CInt(pParam6))
                        End If
                    End If
                End If
                otArray.Add(String.Format("{0}|{1}", olst.FASSETNO, olst.PCPIECE & "|" &
                                                                    TempQTY & "|" &
                                                                    olst.FMORTGBK & "|" &
                                                                    TempDate1 & "|" &
                                                                    TempDate2 & "|" &
                                                                    pParam1 & "|" &
                                                                    pParam2 & "|" &
                                                                    pParam3 & "|" &
                                                                    pParam4 & "|" &
                                                                    pParam5 & "|" &
                                                                    pParam6 & "|" &
                                                                    olst.FPCLNDNO & "|" &
                                                                    olst.FPCWIDTH & "|" &
                                                                    olst.FPCBETWEEN & "|" &
                                                                    olst.FPCNOTE))
            Next
        End If

        Return otArray.ToArray
    End Function


#Region "CalReverse"
    Public Sub CalReverse(ByVal pTotal As String,
                          ByRef pParam1 As String,
                          ByRef pParam2 As String,
                          ByRef pParam3 As String,
                          ByVal pType As String)
        If pType = "-" Then
            pTotal = CDec(pTotal) * -1
        End If
        Dim TempQTY1 As String = CDec(pTotal) / 400
        If TempQTY1.IndexOf(".") > 0 Then
            TempQTY1 = TempQTY1.Split(".")(0)
            pParam1 = TempQTY1
            TempQTY1 = CInt(TempQTY1) * 400
        Else
            pParam1 = TempQTY1
            TempQTY1 = CInt(TempQTY1) * 400
        End If
        Dim TempQTY2 As String = CDec(pTotal) - TempQTY1
        Dim TempQTY3 As String = CDec(TempQTY2) / 100
        If TempQTY3.IndexOf(".") > 0 Then
            TempQTY3 = TempQTY3.Split(".")(0)
            pParam2 = TempQTY3
            TempQTY3 = CInt(TempQTY3) * 100
        Else
            pParam2 = TempQTY3
            TempQTY3 = CInt(TempQTY3) * 100
        End If
        Dim TempQTY4 As String = CDec(TempQTY2) - TempQTY3
        pParam3 = TempQTY4
    End Sub
#End Region
End Class
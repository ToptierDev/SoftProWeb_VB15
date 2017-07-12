﻿Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cTransferTargetByProject

#Region "TRN_TransferTargetByProject.aspx"

    Public Function Loaddata(ByVal pProject As String,
                             ByVal pYear As String) As List(Of LD421PTR)

        Using db As New PNSWEBEntities
            Dim lcSalesTarget As List(Of LD421PTR) = db.LD421PTR.Where(Function(s) s.FREPRJNO = pProject _
                                                                               And s.FYEAR = pYear).OrderBy(Function(s) s.FREPRJNO).ToList


            Return lcSalesTarget.ToList
        End Using
    End Function

    Public Function addData(ByVal dt As DataTable,
                            ByVal pProject As String,
                            ByVal pYear As String,
                            ByVal pTargetq As String,
                            ByVal pTarget As String,
                            ByVal strUserID As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As List(Of LD421PTR) = db.LD421PTR.Where(Function(s) s.FREPRJNO = pProject _
                                                                    And s.FYEAR = pYear).ToList
            For Each e As LD421PTR In lc
                db.LD421PTR.Remove(e)
            Next
            If dt IsNot Nothing Then
                Dim m As New LD421PTR
                m.FYEAR = CInt(pYear)
                m.FREPRJNO = pProject
                m.FTYCODE = ""
                If pTargetq <> String.Empty Then
                    m.FYTARGETQ = CDbl(pTargetq)
                Else
                    m.FYTARGETQ = Nothing
                End If
                If pTarget <> String.Empty Then
                    m.FYTARGET = CDbl(pTarget)
                Else
                    m.FYTARGET = Nothing
                End If

                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim strPeriod As String = dt.Rows(i)("Period").ToString
                    If strPeriod = "1" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ1 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET1 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "2" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ2 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET2 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "3" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ3 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET3 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "4" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ4 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET4 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "5" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ5 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET5 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "6" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ6 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET6 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "7" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ7 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET7 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "8" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ8 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET8 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "9" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ9 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET9 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "10" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ10 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET10 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "11" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ11 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET11 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    ElseIf strPeriod = "12" Then
                        If dt.Rows(i)("TotalRoom").ToString <> String.Empty Then
                            m.FTARGETQ12 = CDbl(dt.Rows(i)("TotalRoom").ToString)
                        End If
                        If dt.Rows(i)("TotalCash").ToString <> String.Empty Then
                            m.FTARGET12 = CDbl(dt.Rows(i)("TotalCash").ToString)
                        End If
                    End If
                Next

                'm.FUSERID = strUserID
                m.FSTMDATE = DateTime.Now
                m.FSTMTIME = DateTime.Now.ToString.Split(" ")(1)

                db.LD421PTR.Add(m)
            End If
            db.SaveChanges()
            Return True
        End Using
        Return False
    End Function

#Region "Dropdownlist"
    Public Function LoadProject() As List(Of ED01PROJ)
        Using db As New PNSWEBEntities
            Dim lcProject As List(Of ED01PROJ) = db.ED01PROJ.OrderBy(Function(s) s.FREPRJNO).ToList
            Return lcProject.Select(Function(s) New ED01PROJ With {.FREPRJNO = s.FREPRJNO, .FREPRJNM = String.Format("{0} {1}", s.FREPRJNO, s.FREPRJNM)}).ToList()
        End Using
    End Function
#End Region

#End Region
End Class

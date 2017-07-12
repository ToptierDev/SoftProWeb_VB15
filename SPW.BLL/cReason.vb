Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cReason

#Region "PRD_Reason.aspx"
    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal pReasonGroup As String,
                             ByVal strUserID As String) As List(Of KD05RSCD)

        Using db As New PNSWEBEntities
            Dim lc As List(Of KD05RSCD) = db.KD05RSCD.ToList
            If Not String.IsNullOrEmpty(pReasonGroup) Then
                lc = lc.Where(Function(s) s.FRSGROUP = pReasonGroup).ToList
            End If

            lc = lc.OrderBy(Function(s) s.FRSCODE).ToList

            If lc IsNot Nothing Then
                TotalRow = lc.Count
            End If

            If (fillter.PageSize > 0 And fillter.Page >= 0) Then
                lc = lc.Skip((fillter.Page - 1) * fillter.PageSize).Take(fillter.PageSize)
            End If

            Dim lists = lc.ToList()
            Return lists
        End Using
    End Function

    Public Function GetKD05RSCD(ByVal pReasonCode As String) As List(Of String)

        'Using db As New PNSWEBEntities
        '    Dim lc As List(Of String) = db.KD05RSCD.Where(Function(s) s.FRSCODE = pReasonCode And s.FRSCODE IsNot Nothing).Select(Function(s) s.FRSCODE).Distinct.ToList
        '    Return lc
        'End Using
        Return Nothing
    End Function

    Public Function GetReasonByID(ByVal pReasonGroup As String,
                                  ByVal pReasonCode As String,
                                  ByVal strUserID As String) As KD05RSCD

        Using db As New PNSWEBEntities
            Dim lc As KD05RSCD = db.KD05RSCD.Where(Function(s) s.FRSCODE = pReasonCode And
                                                               s.FRSGROUP = pReasonCode).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function ReasonDelete(ByVal pReasonGroup As String,
                                 ByVal pReasonCode As String,
                                 ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As KD05RSCD = db.KD05RSCD.Where(Function(s) s.FRSCODE = pReasonCode And
                                                                   s.FRSGROUP = pReasonCode).SingleOrDefault
                db.KD05RSCD.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ReasonAdd(ByVal pReasonGroup As String,
                              ByVal dt As DataTable,
                              ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities

            Dim lst As List(Of KD05RSCD) = db.KD05RSCD.Where(Function(s) s.FRSGROUP = pReasonGroup).ToList
            For Each u As KD05RSCD In lst
                db.KD05RSCD.Remove(u)
            Next
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)("FlagSetAdd").ToString = "0" Then
                            Dim m As KD05RSCD = New KD05RSCD
                            m.FRSGROUP = pReasonGroup
                            m.FRSCODE = dt.Rows(i)("FRSCODE").ToString
                            m.FRSDESC = dt.Rows(i)("FRSDESC").ToString
                            m.FSOLVMETH = dt.Rows(i)("FSOLVMETH").ToString

                            db.KD05RSCD.Add(m)
                        End If
                    Next
                End If
            End If
            db.SaveChanges()
            Return True
        End Using
    End Function

#End Region

End Class

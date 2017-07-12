Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cDepartment

#Region "MST_Department.aspx"
    Public Function LoaddataDepartment(ByVal fillter As FillterSearch,
                                       ByRef TotalRow As Integer,
                                       ByVal strUserID As String) As List(Of BD11DEPT)
        Using db As New PNSWEBEntities
            Dim lc As List(Of BD11DEPT) = db.BD11DEPT.OrderBy(Function(s) s.FDPCODE).ToList
            Return lc.ToList
        End Using
    End Function

    Public Function GetDepartmentByID(ByVal id As String,
                                      ByVal strUserID As String) As BD11DEPT

        Using db As New PNSWEBEntities
            Dim lc As BD11DEPT = db.BD11DEPT.Where(Function(s) s.FDPCODE = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function LoadSection(ByVal pDepCode As String) As List(Of BD12SECT)

        Using db As New PNSWEBEntities
            Dim lc As List(Of BD12SECT) = db.BD12SECT.Where(Function(s) s.FDPCODE = pDepCode).ToList
            Return lc.ToList
        End Using

    End Function

    Public Function DepartmentDelete(ByVal id As String,
                                     ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As BD11DEPT = db.BD11DEPT.Where(Function(s) s.FDPCODE = id).SingleOrDefault
                Dim lst As List(Of BD12SECT) = db.BD12SECT.Where(Function(s) s.FDPCODE = id).ToList
                For Each u As BD12SECT In lst
                    db.BD12SECT.Remove(u)
                Next
                db.BD11DEPT.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DepartmentsEdit(ByVal pDepCode As String,
                                    ByVal pDescription As String,
                                    ByVal pMG As String,
                                    ByVal dt As DataTable,
                                    ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As BD11DEPT = db.BD11DEPT.Where(Function(s) s.FDPCODE = pDepCode).SingleOrDefault
            If lc IsNot Nothing Then
                lc.FDPNAME = pDescription
                lc.FDPMG = pMG
                Dim lst As List(Of BD12SECT) = db.BD12SECT.Where(Function(s) s.FDPCODE = pDepCode).ToList
                For Each u As BD12SECT In lst
                    db.BD12SECT.Remove(u)
                Next
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            Dim m As BD12SECT = New BD12SECT
                            If dt.Rows(i)("FlagSetAdd").ToString = "0" Then
                                m.FDPCODE = pDepCode

                                If dt.Rows(i)("FSECCODE").ToString <> String.Empty Then
                                    m.FSECCODE = dt.Rows(i)("FSECCODE").ToString
                                End If

                                If dt.Rows(i)("FSECNAME").ToString <> String.Empty Then
                                    m.FSECNAME = dt.Rows(i)("FSECNAME").ToString
                                End If

                                If dt.Rows(i)("FSECMG").ToString <> String.Empty Then
                                    m.FSECMG = dt.Rows(i)("FSECMG").ToString
                                End If
                                db.BD12SECT.Add(m)
                            End If
                        Next
                    End If
                End If
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function DepartmentAdd(ByVal pDepCode As String,
                                  ByVal pDescription As String,
                                  ByVal pMG As String,
                                  ByVal dt As DataTable,
                                  ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As BD11DEPT = New BD11DEPT
            lc.FDPCODE = pDepCode
            lc.FDPNAME = pDescription
            lc.FDPMG = pMG
            Dim lst As List(Of BD12SECT) = db.BD12SECT.Where(Function(s) s.FDPCODE = pDepCode).ToList
            For Each u As BD12SECT In lst
                db.BD12SECT.Remove(u)
            Next
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim m As BD12SECT = New BD12SECT
                        If dt.Rows(i)("FlagSetAdd").ToString = "0" Then
                            m.FDPCODE = pDepCode

                            If dt.Rows(i)("FSECCODE").ToString <> String.Empty Then
                                m.FSECCODE = dt.Rows(i)("FSECCODE").ToString
                            End If

                            If dt.Rows(i)("FSECNAME").ToString <> String.Empty Then
                                m.FSECNAME = dt.Rows(i)("FSECNAME").ToString
                            End If

                            If dt.Rows(i)("FSECMG").ToString <> String.Empty Then
                                m.FSECMG = dt.Rows(i)("FSECMG").ToString
                            End If
                            db.BD12SECT.Add(m)
                        End If
                    Next
                End If
            End If
            db.BD11DEPT.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function
#End Region

End Class

Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cPhaseProject

#Region "ORG_PhaseProject.aspx"
    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal pProject As String,
                             ByVal strUserID As String) As List(Of PhaseProject_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED02PHAS
                       Group Join mProject In db.ED01PROJ On mProject.FREPRJNO Equals l.FREPRJNO
                           Into mProj = Group From mProject In mProj.DefaultIfEmpty()
                       Select New PhaseProject_ViewModel With {
                               .ProjectCode = l.FREPRJNO,
                               .ProjectName = mProject.FREPRJNM,
                               .PhaseCode = l.FREPHASE,
                               .StartDate = l.FSTRDATE,
                               .EndDate = l.FENDDATE,
                               .Description = l.FREPHASEDS
                           }

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.ProjectCode = pProject)
            End If

            qury = qury.OrderBy(Function(s) s.ProjectCode)

            If qury IsNot Nothing Then
                TotalRow = qury.Count
            End If

            If (fillter.PageSize > 0 And fillter.Page >= 0) Then
                qury = qury.Skip((fillter.Page - 1) * fillter.PageSize).Take(fillter.PageSize)
            End If

            Dim lists = qury.ToList()
            Return lists
        End Using
    End Function

    Public Function GetEd03Phase(ByVal pPorjectCode As String) As List(Of String)

        Using db As New PNSWEBEntities
            Dim query = ((From a In db.ED04RECF.Where(Function(s) s.FREPRJNO = pPorjectCode And s.FREPHASE IsNot Nothing) Select a.FREPHASE) _
                        .Union _
                        (From a In db.ED03UNIT.Where(Function(s) s.FREPRJNO = pPorjectCode And s.FREPHASE IsNot Nothing) Select a.FREPHASE)).Where(Function(s) s <> "").Distinct.ToList

            Return query
        End Using

    End Function

    Public Function GetPhaseProjectByID(ByVal pPorjectCode As String,
                                        ByVal pPhaseCode As String,
                                        ByVal strUserID As String) As ED02PHAS

        Using db As New PNSWEBEntities
            Dim lc As ED02PHAS = db.ED02PHAS.Where(Function(s) s.FREPRJNO = pPorjectCode _
                                                           And s.FREPHASE = pPhaseCode).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function PhaseProjectDelete(ByVal pPorjectCode As String,
                                       ByVal pPhaseCode As String,
                                       ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As ED02PHAS = db.ED02PHAS.Where(Function(s) s.FREPRJNO = pPorjectCode _
                                                               And s.FREPHASE = pPhaseCode).SingleOrDefault
                db.ED02PHAS.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function PhaseProjectAdd(ByVal pPorjectCode As String,
                                    ByVal dt As DataTable,
                                    ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities

            Dim lst As List(Of ED02PHAS) = db.ED02PHAS.Where(Function(s) s.FREPRJNO = pPorjectCode).ToList
            For Each u As ED02PHAS In lst
                db.ED02PHAS.Remove(u)
            Next
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)("FlagSetAdd").ToString = "0" Then
                            Dim m As ED02PHAS = New ED02PHAS
                            m.FREPRJNO = pPorjectCode
                            m.FREPHASE = dt.Rows(i)("PhaseCode").ToString
                            If dt.Rows(i)("StartDate").ToString <> String.Empty Then
                                Dim TempDate As String = dt.Rows(i)("StartDate").ToString
                                'TempDate = TempDate.ToString.Split("/")(1) & "/" & TempDate.ToString.Split("/")(0) & "/" & TempDate.ToString.Split("/")(2)
                                m.FSTRDATE = CDate(TempDate)
                            End If
                            If dt.Rows(i)("EndDate").ToString <> String.Empty Then
                                Dim TempDate As String = dt.Rows(i)("EndDate").ToString
                                'TempDate = TempDate.ToString.Split("/")(1) & "/" & TempDate.ToString.Split("/")(0) & "/" & TempDate.ToString.Split("/")(2)
                                m.FENDDATE = CDate(TempDate)
                            End If
                            m.FREPHASEDS = dt.Rows(i)("Description").ToString
                            db.ED02PHAS.Add(m)
                        End If
                    Next
                End If
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

#Region "Dropdownlist"
    Public Function LoadProject() As List(Of ED01PROJ)
        Using db As New PNSWEBEntities
            Dim qury As List(Of ED01PROJ) = db.ED01PROJ.OrderBy(Function(s) s.FREPRJNO).ToList
            qury = qury.Select(Function(m) New ED01PROJ With {.FREPRJNO = m.FREPRJNO, .FREPRJNM = String.Format("{0} - {1}", m.FREPRJNO, m.FREPRJNM)}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function
    Public Function LoadProject(ByVal pKeyID As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                Dim qury = (From m In db.ED01PROJ.OrderBy(Function(s) s.FREPRJNO)
                            Select m.FREPRJNO & "-" & m.FREPRJNM).ToList()

                Return qury.ToList()
            ElseIf pKeyID <> String.Empty Then
                Dim qury = (From m In db.ED01PROJ.OrderBy(Function(s) s.FREPRJNO)
                            Select m.FREPRJNO & "-" & m.FREPRJNM) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                Return qury.ToList()
            End If
        End Using
        Return Nothing
    End Function
#End Region
#End Region

End Class

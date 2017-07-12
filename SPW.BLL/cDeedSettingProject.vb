Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cDeedSettingProject

#Region "TRN_DeedSettingProject.aspx"
    Public Function Loaddata(ByVal pProject As String,
                             ByVal strUserID As String) As List(Of FD11PROP)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.FD11PROP

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO = pProject And (s.FENTTYPE = "1" Or s.FENTTYPE = "3"))
            End If

            qury = qury.OrderBy(Function(s) s.FASSETNO)

            Dim lists = qury.ToList()
            Return lists
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

    Public Function Add(ByVal pPorjectCode As String,
                        ByVal dt As DataTable,
                        ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As List(Of FD11PROP) = db.FD11PROP.Where(Function(s) s.FREPRJNO = pPorjectCode And (s.FENTTYPE = "1" Or s.FENTTYPE = "3")).ToList
            For Each u As FD11PROP In lc
                u.FREPRJNO = Nothing
            Next
            Dim strFLANDNO As String = String.Empty
            Dim strCFLANDNO As String = String.Empty
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)("FlagSetAdd").ToString = "0" Then

                            Dim pFASSETNO As String = dt.Rows(i)("FASSETNO").ToString
                            Dim pPCPIECE As String = dt.Rows(i)("PCPIECE").ToString
                            Dim pFPCLNDNO As String = dt.Rows(i)("FPCLNDNO").ToString
                            If strFLANDNO = String.Empty Then
                                strFLANDNO = pPCPIECE
                            Else
                                strFLANDNO = strFLANDNO & "," & pPCPIECE
                            End If
                            If strCFLANDNO = String.Empty Then
                                strCFLANDNO = pFPCLNDNO
                            Else
                                strCFLANDNO = strCFLANDNO & "," & pFPCLNDNO
                            End If

                            Dim lst As FD11PROP = db.FD11PROP.Where(Function(s) s.FASSETNO = pFASSETNO).FirstOrDefault
                            If lst IsNot Nothing Then
                                lst.FREPRJNO = pPorjectCode

                                If dt.Rows(i)("FMAINCSTR").ToString <> String.Empty Then
                                    Dim TempDate As String = dt.Rows(i)("FMAINCSTR").ToString
                                    'TempDate = TempDate.ToString.Split("/")(1) & "/" & TempDate.ToString.Split("/")(0) & "/" & TempDate.ToString.Split("/")(2)
                                    lst.FMAINCSTR = CDate(TempDate)
                                End If

                                If dt.Rows(i)("FMAINCEND").ToString <> String.Empty Then
                                    Dim TempDate As String = dt.Rows(i)("FMAINCEND").ToString
                                    'TempDate = TempDate.ToString.Split("/")(1) & "/" & TempDate.ToString.Split("/")(0) & "/" & TempDate.ToString.Split("/")(2)
                                    lst.FMAINCEND = CDate(TempDate)
                                End If

                                Dim IntTempTotal1 As Integer = "0"
                                Dim IntTemp1 As String = "0"
                                Dim IntTemp2 As Integer = "0"
                                Dim IntTemp3 As Integer = "0"
                                If dt.Rows(i)("FQTYADJPLUS1").ToString <> String.Empty Then
                                    IntTemp1 = dt.Rows(i)("FQTYADJPLUS1").ToString
                                End If
                                'If dt.Rows(i)("FQTYADJPLUS2").ToString <> String.Empty Then
                                '    IntTemp2 = CInt(dt.Rows(i)("FQTYADJPLUS2").ToString)
                                'End If
                                'If dt.Rows(i)("FQTYADJPLUS3").ToString <> String.Empty Then
                                '    IntTemp3 = CInt(dt.Rows(i)("FQTYADJPLUS3").ToString)
                                'End If
                                If IntTemp1 <> String.Empty And IntTemp1 <> "0" Then
                                    IntTempTotal1 = CDec((CInt(IntTemp1.Split("-")(0)) * 400) + (CInt(IntTemp1.Split("-")(1)) * 100) + CInt(IntTemp1.Split("-")(2)))
                                End If

                                If IntTempTotal1 > 0 Then
                                    lst.FQTYADJ = CDec(IntTempTotal1)
                                End If
                                Dim IntTempTotal2 As Integer = "0"
                                Dim IntTemp4 As String = "0"
                                Dim IntTemp5 As Integer = "0"
                                Dim IntTemp6 As Integer = "0"
                                If dt.Rows(i)("FQTYADJNPLUS1").ToString <> String.Empty Then
                                    IntTemp4 = dt.Rows(i)("FQTYADJNPLUS1").ToString
                                End If
                                'If dt.Rows(i)("FQTYADJNPLUS2").ToString <> String.Empty Then
                                '    IntTemp5 = CInt(dt.Rows(i)("FQTYADJNPLUS2").ToString)
                                'End If
                                'If dt.Rows(i)("FQTYADJNPLUS3").ToString <> String.Empty Then
                                '    IntTemp6 = CInt(dt.Rows(i)("FQTYADJNPLUS3").ToString)
                                'End If
                                If IntTemp4 <> String.Empty And IntTemp4 <> "0" Then
                                    IntTempTotal2 = CDec((CInt(IntTemp4.Split("-")(0)) * 400) + (CInt(IntTemp4.Split("-")(1)) * 100) + CInt(IntTemp4.Split("-")(2)))
                                End If

                                If IntTempTotal2 > 0 Then
                                    lst.FQTYADJ = CDec(IntTempTotal2 * -1)
                                End If
                                If IntTempTotal1 = 0 And IntTempTotal2 = 0 Then
                                    lst.FQTYADJ = Nothing
                                End If
                                If dt.Rows(i)("FPCNOTE").ToString <> String.Empty Then
                                    lst.FPCNOTE = dt.Rows(i)("FPCNOTE").ToString
                                End If
                            End If
                        End If
                    Next
                    If strFLANDNO <> String.Empty Or
                       strCFLANDNO <> String.Empty Then
                        Dim lcst As ED01PROJ = db.ED01PROJ.Where(Function(s) s.FREPRJNO = pPorjectCode).FirstOrDefault
                        If lcst IsNot Nothing Then
                            If strFLANDNO <> String.Empty Then
                                lcst.FLANDNO = strFLANDNO
                            Else
                                lcst.FLANDNO = Nothing
                            End If
                            If strCFLANDNO <> String.Empty Then
                                lcst.FPCLANDNO = strCFLANDNO
                            Else
                                lcst.FPCLANDNO = Nothing
                            End If
                        End If
                    End If
                End If
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

#Region "Dropdownlist"

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

    Public Function LoadDeed(ByVal pKeyID As String) As List(Of DeedSettingProject_ViewModel)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                Dim qury = From l In db.FD11PROP.Where(Function(s) (s.FENTTYPE = "1" Or s.FENTTYPE = "3"))
                           Select New DeedSettingProject_ViewModel With
                           {
                                .FASSETNO = l.FASSETNO,
                                .PCPIECE = l.FPCPIECE,
                                .FQTY = l.FQTY,
                                .FMORTGBK = l.FMORTGBK,
                                .FMAINCSTR = l.FMAINCSTR,
                                .FMAINCEND = l.FMAINCEND,
                                .FQTYADJPLUS = l.FQTYADJ,
                                .FQTYADJNPLUS = l.FQTYADJ,
                                .FPCLNDNO = l.FPCLNDNO,
                                .FPCWIDTH = l.FPCWIDTH,
                                .FPCBETWEEN = l.FPCBETWEEN,
                                .FPCNOTE = l.FPCNOTE
                           }

                Return qury.Take(50).ToList()
            ElseIf pKeyID <> String.Empty Then
                Dim qury = From l In db.FD11PROP.Where(Function(s) (s.FENTTYPE = "1" Or s.FENTTYPE = "3") And
                                                      (s.FASSETNO.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))))
                           Select New DeedSettingProject_ViewModel With
                           {
                                .FASSETNO = l.FASSETNO,
                                .PCPIECE = l.FPCPIECE,
                                .FQTY = l.FQTY,
                                .FMORTGBK = l.FMORTGBK,
                                .FMAINCSTR = l.FMAINCSTR,
                                .FMAINCEND = l.FMAINCEND,
                                .FQTYADJPLUS = l.FQTYADJ,
                                .FQTYADJNPLUS = l.FQTYADJ,
                                .FPCLNDNO = l.FPCLNDNO,
                                .FPCWIDTH = l.FPCWIDTH,
                                .FPCBETWEEN = l.FPCBETWEEN,
                                .FPCNOTE = l.FPCNOTE
                           }

                qury = qury

                Return qury.Take(50).ToList()
            End If
        End Using
        Return Nothing
    End Function

    Public Function LoadProject() As List(Of ED01PROJ)
        Using db As New PNSWEBEntities
            Dim qury As List(Of ED01PROJ) = db.ED01PROJ.OrderBy(Function(s) s.FREPRJNO).ToList
            qury = qury.Select(Function(m) New ED01PROJ With {.FREPRJNO = m.FREPRJNO, .FREPRJNM = String.Format("{0} - {1}", m.FREPRJNO, m.FREPRJNM)}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function

#End Region
#End Region

End Class

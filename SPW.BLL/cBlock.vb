Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cBlock

#Region "ORG_Block.aspx"
    Public Function getUsedMaster(ByVal pProjectCode As String,
                                  ByVal pPhase As String,
                                  ByVal pZone As String) As List(Of String)

        Using db As New PNSWEBEntities
            Dim query = Nothing
            If pPhase <> String.Empty And pProjectCode <> String.Empty And pZone <> String.Empty Then
                query = (From a In db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProjectCode And s.FREPHASE = pPhase And s.FREZONE = pZone) Select a.FREPHASE & "|" & a.FREZONE & "|" & a.FREBLOCK).Where(Function(s) s <> "" And s IsNot Nothing).Distinct.ToList
            ElseIf pPhase <> String.Empty And pProjectCode <> String.Empty Then
                query = (From a In db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProjectCode And s.FREPHASE = pPhase) Select a.FREPHASE & "|" & a.FREZONE & "|" & a.FREBLOCK).Where(Function(s) s <> "" And s IsNot Nothing).Distinct.ToList
            ElseIf pProjectCode <> String.Empty Then
                query = (From a In db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProjectCode) Select a.FREPHASE & "|" & a.FREZONE & "|" & a.FREBLOCK).Where(Function(s) s <> "" And s IsNot Nothing).Distinct.ToList
            End If
            Return query
        End Using
    End Function

    Public Function Loaddata(ByVal pProject As String,
                             ByVal pPhaseCode As String,
                             ByVal pZone As String,
                             ByVal strUserID As String) As List(Of Block_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED04BLOK
                       Join m In db.ED04RECF On m.FREPHASE Equals l.FREPHASE And m.FREPRJNO.Trim Equals l.FREPRJNO.Trim And m.FREZONE.Trim Equals l.FREZONE.Trim
                       Select New Block_ViewModel With {
                               .ProjectCode = l.FREPRJNO,
                               .PhaseCode = l.FREPHASE,
                               .ZoneCode = l.FREZONE,
                               .BlockCode = l.FREBLOCK
                           }

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.ProjectCode.Trim = pProject)
            End If

            If Not String.IsNullOrEmpty(pPhaseCode) Then
                qury = qury.Where(Function(s) s.PhaseCode = pPhaseCode)
            End If

            If Not String.IsNullOrEmpty(pZone) Then
                qury = qury.Where(Function(s) s.ZoneCode = pZone)
            End If

            qury = qury.OrderBy(Function(s) s.BlockCode)


            Dim lists = qury.ToList()
            Return lists
        End Using
    End Function

    Public Function LoadBlockMaster(ByVal pProject As String,
                                    ByVal pPhase As String,
                                    ByVal pZone As String,
                                    ByVal pBlock As String) As List(Of Block_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED04BLOK
                       Join m In db.ED04RECF On m.FREPHASE Equals l.FREPHASE And m.FREPRJNO.Trim Equals l.FREPRJNO.Trim And m.FREZONE.Trim Equals l.FREZONE.Trim
                       Select New Block_ViewModel With {
                               .ProjectCode = l.FREPRJNO,
                               .PhaseCode = l.FREPHASE,
                               .ZoneCode = l.FREZONE,
                               .BlockCode = l.FREBLOCK
                           }

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.ProjectCode.Trim = pProject)
            End If

            If Not String.IsNullOrEmpty(pPhase) Then
                qury = qury.Where(Function(s) s.PhaseCode = pPhase)
            End If

            If Not String.IsNullOrEmpty(pZone) Then
                qury = qury.Where(Function(s) s.ZoneCode = pZone)
            End If

            If Not String.IsNullOrEmpty(pBlock) Then
                qury = qury.Where(Function(s) s.BlockCode = pBlock)
            End If

            Dim lists = qury.ToList
            Return lists
        End Using
    End Function

    Public Function LoadED03UNIT(ByVal pProject As String,
                                 ByVal pPhase As String,
                                 ByVal pZone As String,
                                 ByVal pBlock As String) As List(Of ED03UNIT)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED03UNIT

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProject)
            End If

            If Not String.IsNullOrEmpty(pPhase) Then
                qury = qury.Where(Function(s) s.FREPHASE = pPhase)
            End If

            If Not String.IsNullOrEmpty(pZone) Then
                qury = qury.Where(Function(s) s.FREZONE = pZone)
            End If

            If Not String.IsNullOrEmpty(pBlock) Then
                qury = qury.Where(Function(s) s.FREBLOCK = pBlock)
            End If

            Dim lists = qury.ToList()
            lists = lists.OrderBy(Function(s) s.FSERIALNO, New SemiNumericComparer).ToList()
            Return lists
        End Using
    End Function

    Public Function LoadED03UNITCheckData(ByVal pProject As String) As List(Of ED03UNIT)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED03UNIT

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProject)
            End If

            Dim lists = qury.ToList()
            lists = lists.OrderBy(Function(s) s.FSERIALNO, New SemiNumericComparer).ToList()
            Return lists
        End Using
    End Function

    Public Function LoadED03UNITCheckDataUsed(ByVal pProject As String,
                                              ByVal pBlock As String) As List(Of ED03UNIT)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED03UNIT.Where(Function(s) s.FREBLOCK IsNot Nothing And s.FREBLOCK <> String.Empty)

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProject)
            End If

            If Not String.IsNullOrEmpty(pBlock) Then
                qury = qury.Where(Function(s) s.FREBLOCK <> pBlock)
            End If

            Dim lists = qury.ToList()
            lists = lists.OrderBy(Function(s) s.FSERIALNO, New SemiNumericComparer).ToList()
            Return lists
        End Using
    End Function

    Public Function Save(ByVal pProject As String,
                         ByVal pPhase As String,
                         ByVal pZone As String,
                         ByVal pBlock As String,
                         ByVal dt As DataTable,
                         ByVal dts As DataTable) As Boolean
        Using db As New PNSWEBEntities
            If dts IsNot Nothing Then
                For i As Integer = 0 To dts.Rows.Count - 1
                    Dim strFSERIALNO As String = dts.Rows(i)("FSERIALNO").ToString
                    If strFSERIALNO <> String.Empty Then
                        Dim lst As ED03UNIT = db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                            s.FSERIALNO = strFSERIALNO).FirstOrDefault
                        lst.FREBLOCK = Nothing
                    End If
                Next
            End If

            Dim lcs As List(Of ED03UNIT) = db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                         s.FREPHASE = pPhase And
                                                                         s.FREZONE = pZone And
                                                                         s.FREBLOCK = pBlock).ToList
            If lcs IsNot Nothing Then
                For Each m As ED03UNIT In lcs
                    m.FREBLOCK = Nothing
                Next
            End If

            Dim countRow As Integer = 0
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)("FlagSetAdd").ToString = "0" Then
                            Dim strFSERIALNO As String = dt.Rows(i)("FSERIALNO").ToString
                            Dim strFPDCODE As String = dt.Rows(i)("FPDCODE").ToString
                            Dim strFADDRNO As String = dt.Rows(i)("FADDRNO").ToString
                            If strFSERIALNO <> String.Empty Then
                                Dim lst As ED03UNIT = db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                                    s.FSERIALNO = strFSERIALNO).FirstOrDefault
                                lst.FREPHASE = pPhase
                                lst.FREZONE = pZone
                                lst.FREBLOCK = pBlock
                                lst.FPDCODE = strFPDCODE
                                lst.FADDRNO = strFADDRNO
                            End If
                            countRow += 1
                        End If
                    Next
                End If
            End If

            Dim lstBj As List(Of ED04BLOK) = db.ED04BLOK.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                           s.FREPHASE = pPhase And
                                                                           s.FREZONE = pZone And
                                                                           s.FREBLOCK = pBlock).ToList
            If lstBj IsNot Nothing Then
                For Each m As ED04BLOK In lstBj
                    db.ED04BLOK.Remove(m)
                Next
            End If

            Dim lcj As ED04BLOK = New ED04BLOK
            lcj.FREPRJNO = pProject
            lcj.FREPHASE = pPhase
            lcj.FREZONE = pZone
            lcj.FREBLOCK = pBlock
            lcj.FQTY = CDbl(countRow)

            db.ED04BLOK.Add(lcj)

            db.SaveChanges()

            Dim lstC As List(Of String) = db.ED04BLOK.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                        s.FREPHASE = pPhase And
                                                                        s.FREZONE = pZone).Select(Function(s) s.FREBLOCK).Distinct.ToList
            If lstC IsNot Nothing Then
                For Each g In lstC
                    Dim l As ED04BLOK = db.ED04BLOK.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                      s.FREPHASE = pPhase And
                                                                      s.FREZONE = pZone And
                                                                      s.FREBLOCK = g).FirstOrDefault
                    Dim ls As List(Of ED03UNIT) = db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                                s.FREPHASE = pPhase And
                                                                                s.FREZONE = pZone And
                                                                                s.FREBLOCK = g).ToList
                    If l IsNot Nothing And ls.Count > 0 Then
                        Dim lstE As ED04BLOK = db.ED04BLOK.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                             s.FREPHASE = pPhase And
                                                                             s.FREZONE = pZone And
                                                                             s.FREBLOCK = g).FirstOrDefault
                        lstE.FQTY = ls.Count
                        'ElseIf l IsNot Nothing And ls.Count = 0 Then
                        '    Dim lstE As List(Of ED04BLOK) = db.ED04BLOK.Where(Function(s) s.FREPRJNO.Trim = pProject And
                        '                                                                  s.FREPHASE = pPhase And
                        '                                                                  s.FREZONE = pZone And
                        '                                                                  s.FREBLOCK = g).ToList
                        '    If lstE IsNot Nothing Then
                        '        For Each s As ED04BLOK In lstE
                        '            db.ED04BLOK.Remove(s)
                        '        Next
                        '    End If
                    End If
                Next
            End If

            db.SaveChanges()

            Return True
        End Using
    End Function

    Public Function Delete(ByVal pProject As String,
                           ByVal pPhase As String,
                           ByVal pZone As String,
                           ByVal pBlock As String,
                           ByVal pUserID As String) As Boolean
        Using db As New PNSWEBEntities
            Dim lst As List(Of ED03UNIT) = db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                         s.FREPHASE = pPhase And
                                                                         s.FREZONE = pZone And
                                                                         s.FREBLOCK = pBlock).ToList
            If lst IsNot Nothing Then
                For Each lc As ED03UNIT In lst
                    lc.FREBLOCK = Nothing
                Next
            End If
            Dim lstB As List(Of ED04BLOK) = db.ED04BLOK.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                          s.FREPHASE = pPhase And
                                                                          s.FREZONE = pZone And
                                                                          s.FREBLOCK = pBlock).ToList
            If lstB IsNot Nothing Then
                For Each m As ED04BLOK In lstB
                    db.ED04BLOK.Remove(m)
                Next
            End If
            db.SaveChanges()
        End Using
        Return True
    End Function

    Public Function LoadED03UNITBetween(ByVal pProject As String,
                                        ByVal pUnitFrom As String,
                                        ByVal pUnitTo As String) As List(Of ED03UNIT)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED03UNIT

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProject)
            End If

            If Not String.IsNullOrEmpty(pUnitFrom) And
               Not String.IsNullOrEmpty(pUnitTo) Then
                qury = qury.Where(Function(s) s.FSERIALNO >= pUnitFrom And s.FSERIALNO <= pUnitTo)
            End If

            Dim lists = qury.ToList()
            lists = lists.OrderBy(Function(s) s.FSERIALNO, New SemiNumericComparer).ToList()

            Return lists
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
    Public Function LoadPhase(ByVal pProject As String) As List(Of ED02PHAS)
        Using db As New PNSWEBEntities
            Dim qury As List(Of ED02PHAS) = db.ED02PHAS.Where(Function(s) s.FREPRJNO.Trim = pProject).OrderBy(Function(s) s.FREPHASE).ToList
            qury = qury.Select(Function(m) New ED02PHAS With {.FREPHASE = m.FREPHASE}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function
    Public Function LoadZone(ByVal pProject As String,
                             ByVal pPhase As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pPhase <> String.Empty Then
                Dim qury = (From m In db.ED04RECF.Where(Function(s) s.FREPRJNO.Trim = pProject And s.FREPHASE = pPhase And s.FREZONE IsNot Nothing).OrderBy(Function(s) s.FREZONE)
                            Select m.FREZONE).Distinct().ToList()

                Return qury.ToList()
            ElseIf pProject <> String.Empty Then
                Dim qury = (From m In db.ED04RECF.Where(Function(s) s.FREPRJNO.Trim = pProject And s.FREZONE IsNot Nothing).OrderBy(Function(s) s.FREZONE)
                            Select m.FREZONE).Distinct().ToList()

                Return qury.ToList()
            End If

        End Using
        Return Nothing
    End Function
    Public Function LoadProjectEdit(ByVal pID As String) As ED01PROJ
        Using db As New PNSWEBEntities
            Dim lc As ED01PROJ = db.ED01PROJ.Where(Function(s) s.FREPRJNO.Trim = pID).FirstOrDefault
            Return lc
        End Using
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

    Public Function LoadPhaseEdit(ByVal pProject As String,
                                  ByVal pID As String) As ED02PHAS
        Using db As New PNSWEBEntities
            Dim lc As ED02PHAS = db.ED02PHAS.Where(Function(s) s.FREPRJNO.Trim = pProject And s.FREPHASE = pID).FirstOrDefault
            Return lc
        End Using
    End Function
    Public Function LoadPhase(ByVal pKeyID As String,
                              ByVal pProject As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pProject = String.Empty Then
                Return Nothing
            Else
                If pKeyID = String.Empty Then
                    Dim qury = (From m In db.ED02PHAS.Where(Function(s) s.FREPRJNO.Trim = pProject).OrderBy(Function(s) s.FREPHASE)
                                Select m.FREPHASE).Distinct().ToList()

                    Return qury.ToList()
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = (From m In db.ED02PHAS.Where(Function(s) s.FREPRJNO.Trim = pProject).OrderBy(Function(s) s.FREPHASE)
                                Select m.FREPHASE) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).Distinct().ToList()

                    Return qury.ToList()
                End If
            End If
            Return Nothing
        End Using
    End Function
    Public Function LoadZoneEdit(ByVal pProject As String,
                                 ByVal pPhase As String,
                                 ByVal pID As String) As ED03UNIT
        Using db As New PNSWEBEntities
            Dim lc As ED03UNIT = db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And s.FREPHASE = pPhase And s.FREZONE = pID).FirstOrDefault
            Return lc
        End Using
    End Function
    Public Function LoadZone(ByVal pKeyID As String,
                             ByVal pProject As String,
                             ByVal pPhase As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pProject = String.Empty Or pPhase = String.Empty Then
                Return Nothing
            Else
                If pKeyID = String.Empty Then
                    Dim qury = (From m In db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And s.FREPHASE = pPhase And s.FREZONE IsNot Nothing).OrderBy(Function(s) s.FREZONE)
                                Select m.FREZONE).Distinct().ToList()

                    Return qury.ToList()
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = (From m In db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And s.FREPHASE = pPhase And s.FREZONE IsNot Nothing).OrderBy(Function(s) s.FREZONE)
                                Select m.FREZONE) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).Distinct().ToList()

                    Return qury.ToList()
                End If
            End If
            Return Nothing
        End Using
    End Function
#End Region
#End Region

End Class

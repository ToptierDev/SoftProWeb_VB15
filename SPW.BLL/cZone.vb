Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL
Public Class cZone

#Region "MST_Zone.aspx"
    Public Function LoaddataZone(ByVal fillter As FillterSearch,
                               ByRef TotalRow As Integer,
                               ByVal strUserID As String) As List(Of LD07SLRT)

        Using db As New PNSWEBEntities
            Dim lc As List(Of LD07SLRT) = db.LD07SLRT.OrderBy(Function(s) s.FSLROUTE).ToList
            Return lc.ToList
        End Using

    End Function

    Public Function GetZoneByID(ByVal id As String,
                                ByVal strUserID As String) As LD07SLRT

        Using db As New PNSWEBEntities
            Dim lc As LD07SLRT = db.LD07SLRT.Where(Function(s) s.FSLROUTE = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function ZoneDelete(ByVal id As String,
                               ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As LD07SLRT = db.LD07SLRT.Where(Function(s) s.FSLROUTE = id).SingleOrDefault
                db.LD07SLRT.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ZonesEdit(ByVal pZoneCode As String,
                              ByVal pDescription As String,
                              ByVal pArea As String,
                              ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As LD07SLRT = db.LD07SLRT.Where(Function(s) s.FSLROUTE = pZoneCode).SingleOrDefault
            If lc IsNot Nothing Then
                lc.FSLROUTENM = pDescription
                lc.FSLAREA = pArea
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function ZoneAdd(ByVal pZoneCode As String,
                            ByVal pDescription As String,
                            ByVal pArea As String,
                            ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As LD07SLRT = New LD07SLRT
            lc.FSLROUTE = pZoneCode
            lc.FSLROUTENM = pDescription
            lc.FSLAREA = pArea

            db.LD07SLRT.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function getUsedMaster() As List(Of String)

        Using db As New PNSWEBEntities
            Dim query = (From a In db.LD01SMAN Select a.FSLROUTE) _
                        .Where(Function(s) s <> "").Distinct.ToList


            Return query
        End Using
    End Function

#End Region

#Region "ORG_Zone.aspx"

    Public Function getUsedMasterORG(ByVal pProjectCode As String,
                                     ByVal pPhase As String) As List(Of String)

        Using db As New PNSWEBEntities
            If pProjectCode <> String.Empty Then
                Dim lcPhas As List(Of String) = db.ED02PHAS.Where(Function(s) s.FREPRJNO = pProjectCode).Select(Function(s) s.FREPHASE).Distinct.ToList

                Dim query = ((From a In db.ED04BLOK.Where(Function(s) s.FREPRJNO.Trim = pProjectCode And lcPhas.Contains(s.FREPHASE)) Select a.FREPHASE & "|" & a.FREZONE) _
                             .Union _
                             (From a In db.ED11PAJ1.Where(Function(s) s.FREPRJNO.Trim = pProjectCode And lcPhas.Contains(s.FREPHASE)) Select a.FREPHASE & "|" & a.FREZONE) _
                             .Union _
                             (From a In db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProjectCode And lcPhas.Contains(s.FREPHASE)) Select a.FREPHASE & "|" & a.FREZONE)).Where(Function(s) s <> "" And s IsNot Nothing).Distinct.ToList



                Return query
            ElseIf pProjectCode <> String.Empty And pPhase <> String.Empty Then
                Dim lcPhas As List(Of String) = db.ED02PHAS.Where(Function(s) s.FREPRJNO = pProjectCode And s.FREPHASE = pPhase).Select(Function(s) s.FREPHASE).Distinct.ToList

                Dim query = ((From a In db.ED04BLOK.Where(Function(s) s.FREPRJNO.Trim = pProjectCode And lcPhas.Contains(s.FREPHASE)) Select a.FREPHASE & "|" & a.FREZONE) _
                             .Union _
                             (From a In db.ED11PAJ1.Where(Function(s) s.FREPRJNO.Trim = pProjectCode And lcPhas.Contains(s.FREPHASE)) Select a.FREPHASE & "|" & a.FREZONE) _
                             .Union _
                             (From a In db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProjectCode And lcPhas.Contains(s.FREPHASE)) Select a.FREPHASE & "|" & a.FREZONE)).Where(Function(s) s <> "" And s IsNot Nothing).Distinct.ToList



                Return query
            End If

        End Using
    End Function

    Public Function Loaddata(ByVal pProject As String,
                             ByVal pPhaseCode As String,
                             ByVal strUserID As String) As List(Of Zone_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED04RECF
                       Join m In db.ED02PHAS On m.FREPHASE Equals l.FREPHASE And m.FREPRJNO.Trim Equals l.FREPRJNO.Trim
                       Select New Zone_ViewModel With {
                               .FREPRJNO = l.FREPRJNO,
                               .PhaseCode = l.FREPHASE,
                               .ZoneCode = l.FREZONE
                           }

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProject)
            End If

            If Not String.IsNullOrEmpty(pPhaseCode) Then
                qury = qury.Where(Function(s) s.PhaseCode = pPhaseCode)
            End If

            Dim lists = qury.ToList
            lists = lists.OrderBy(Function(s) s.FREPRJNO And s.FREPHASE And s.FREZONE, New SemiNumericComparer).Distinct.ToList()
            Return lists
        End Using
    End Function

    Public Function LoadZoneMaster(ByVal pProject As String,
                                   ByVal pPhaseCode As String,
                                   ByVal pZoneCode As String) As List(Of Zone_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED04RECF
                       Join m In db.ED02PHAS On m.FREPHASE Equals l.FREPHASE And m.FREPRJNO.Trim Equals l.FREPRJNO.Trim
                       Select New Zone_ViewModel With {
                               .FREPRJNO = l.FREPRJNO,
                               .PhaseCode = l.FREPHASE,
                               .ZoneCode = l.FREZONE
                           }

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProject)
            End If

            If Not String.IsNullOrEmpty(pPhaseCode) Then
                qury = qury.Where(Function(s) s.PhaseCode = pPhaseCode)
            End If

            If Not String.IsNullOrEmpty(pZoneCode) Then
                qury = qury.Where(Function(s) s.ZoneCode = pZoneCode)
            End If

            Dim lists = qury.ToList
            lists = lists.OrderBy(Function(s) s.FREPRJNO And s.FREPHASE And s.FREZONE, New SemiNumericComparer).Distinct.ToList()
            Return lists
        End Using
    End Function

    Public Function LoadED03UNIT(ByVal pProject As String,
                                 ByVal pPhaseCode As String,
                                 ByVal pZoneCode As String) As List(Of Zone_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED03UNIT
                       Group Join mStatus In db.ED03UNITSTATUS On mStatus.FRESTATUS Equals l.FRESTATUS
                       Into mStat = Group From mStatus In mStat.DefaultIfEmpty()
                       Select New Zone_ViewModel With {
                            .FREPRJNO = l.FREPRJNO,
                            .PhaseCode = l.FREPHASE,
                            .ZoneCode = l.FREZONE,
                            .FSERIALNO = l.FSERIALNO,
                            .FREPHASE = l.FREPHASE,
                            .FREZONE = l.FREZONE,
                            .FMLEFT = l.FMLEFT,
                            .FMTOP = l.FMTOP,
                            .FRESTATUS = mStatus.FRESTATUSDESC
                        }


            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProject)
            End If

            If Not String.IsNullOrEmpty(pPhaseCode) Then
                qury = qury.Where(Function(s) s.FREPHASE = pPhaseCode)
            End If

            If Not String.IsNullOrEmpty(pZoneCode) Then
                qury = qury.Where(Function(s) s.FREZONE = pZoneCode)
            End If

            Dim lists = qury.ToList
            lists = lists.OrderBy(Function(s) s.FREZONE, New SemiNumericComparer).ToList()
            Return lists
        End Using
    End Function

    Public Function LoadED03UNITBetween(ByVal pProject As String,
                                        ByVal pUnitFrom As String,
                                        ByVal pUnitTo As String) As List(Of Zone_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED03UNIT
                       Group Join mStatus In db.ED03UNITSTATUS On mStatus.FRESTATUS Equals l.FRESTATUS
                       Into mStat = Group From mStatus In mStat.DefaultIfEmpty()
                       Select New Zone_ViewModel With {
                            .FREPRJNO = l.FREPRJNO,
                            .PhaseCode = l.FREPHASE,
                            .ZoneCode = l.FREZONE,
                            .FSERIALNO = l.FSERIALNO,
                            .FREPHASE = l.FREPHASE,
                            .FREZONE = l.FREZONE,
                            .FMLEFT = l.FMLEFT,
                            .FMTOP = l.FMTOP,
                            .FRESTATUS = mStatus.FRESTATUSDESC
                        }

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProject)
            End If

            If Not String.IsNullOrEmpty(pUnitFrom) And
               Not String.IsNullOrEmpty(pUnitTo) Then
                qury = qury.Where(Function(s) s.FSERIALNO >= pUnitFrom And s.FSERIALNO <= pUnitTo)
            End If

            qury = qury.OrderBy(Function(s) s.FSERIALNO)

            Dim lists = qury.ToList()
            Return lists
        End Using
    End Function

    Public Function LoadED03UNITCheckData(ByVal pProject As String) As List(Of Zone_ViewModel)

        Using db As New PNSWEBEntities


            Dim qury = From l In db.ED03UNIT
                       Group Join mStatus In db.ED03UNITSTATUS On mStatus.FRESTATUS Equals l.FRESTATUS
                       Into mStat = Group From mStatus In mStat.DefaultIfEmpty()
                       Select New Zone_ViewModel With {
                            .FREPRJNO = l.FREPRJNO,
                            .PhaseCode = l.FREPHASE,
                            .ZoneCode = l.FREZONE,
                            .FSERIALNO = l.FSERIALNO,
                            .FREPHASE = l.FREPHASE,
                            .FREZONE = l.FREZONE,
                            .FMLEFT = l.FMLEFT,
                            .FMTOP = l.FMTOP,
                            .FRESTATUS = mStatus.FRESTATUSDESC
                        }

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProject)
            End If

            Dim lists = qury.ToList
            lists = lists.OrderBy(Function(s) s.FSERIALNO, New SemiNumericComparer).ToList()
            Return lists
        End Using
    End Function

    Public Function LoadED03UNITCheckDataUsed(ByVal pProject As String,
                                              ByVal pZone As String) As List(Of Zone_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.ED03UNIT.Where(Function(s) s.FREZONE IsNot Nothing And s.FREZONE <> String.Empty)
                       Group Join mStatus In db.ED03UNITSTATUS On mStatus.FRESTATUS Equals l.FRESTATUS
                       Into mStat = Group From mStatus In mStat.DefaultIfEmpty()
                       Select New Zone_ViewModel With {
                            .FREPRJNO = l.FREPRJNO,
                            .PhaseCode = l.FREPHASE,
                            .ZoneCode = l.FREZONE,
                            .FSERIALNO = l.FSERIALNO,
                            .FREPHASE = l.FREPHASE,
                            .FREZONE = l.FREZONE,
                            .FMLEFT = l.FMLEFT,
                            .FMTOP = l.FMTOP,
                            .FRESTATUS = mStatus.FRESTATUSDESC
                        }

            If Not String.IsNullOrEmpty(pProject) Then
                qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProject)
            End If
            If Not String.IsNullOrEmpty(pZone) Then
                qury = qury.Where(Function(s) s.FREZONE <> pZone)
            End If

            Dim lists = qury.ToList
            lists = lists.OrderBy(Function(s) s.FSERIALNO, New SemiNumericComparer).ToList()
            Return lists
        End Using
    End Function

    Public Function Save(ByVal pProject As String,
                         ByVal pPhase As String,
                         ByVal pZone As String,
                         ByVal dt As DataTable,
                         ByVal dts As DataTable) As Boolean
        Using db As New PNSWEBEntities
            If dts IsNot Nothing Then
                For i As Integer = 0 To dts.Rows.Count - 1
                    Dim strFSERIALNO As String = dts.Rows(i)("FSERIALNO").ToString
                    If strFSERIALNO <> String.Empty Then
                        Dim lst As ED03UNIT = db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                            s.FSERIALNO = strFSERIALNO).FirstOrDefault
                        lst.FREZONE = Nothing
                        lst.FREPHASE = Nothing
                        lst.FREBLOCK = Nothing
                    End If
                Next
            End If
            Dim lsts As List(Of ED03UNIT) = db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                          s.FREPHASE = pPhase And
                                                                          s.FREZONE = pZone).ToList
            For Each m As ED03UNIT In lsts
                m.FREZONE = Nothing
                m.FREPHASE = Nothing
                m.FREBLOCK = Nothing
            Next

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)("FlagSetAdd").ToString = "0" Then
                            Dim strFSERIALNO As String = dt.Rows(i)("FSERIALNO").ToString
                            'Dim strFMLEFT As String = dt.Rows(i)("FMLEFT").ToString
                            'Dim strFMTOP As String = dt.Rows(i)("FMTOP").ToString
                            Dim strFRESTATUS As String = dt.Rows(i)("FRESTATUS").ToString
                            If strFSERIALNO <> String.Empty Then
                                Dim lst As ED03UNIT = db.ED03UNIT.Where(Function(s) s.FREPRJNO = pProject And
                                                                                    s.FSERIALNO = strFSERIALNO).FirstOrDefault
                                lst.FREPHASE = pPhase
                                lst.FREZONE = pZone
                                'If strFMLEFT <> String.Empty Then
                                '    lst.FMLEFT = CDec(strFMLEFT)
                                'Else
                                '    lst.FMLEFT = Nothing
                                'End If
                                'If strFMTOP <> String.Empty Then
                                '    lst.FMTOP = CDec(strFMTOP)
                                'Else
                                '    lst.FMTOP = Nothing
                                'End If
                                'lst.FRESTATUS = strFRESTATUS
                            End If
                        End If
                    Next
                End If
            End If

            Dim lstMaster As ED04RECF = db.ED04RECF.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                      s.FREPHASE = pPhase And
                                                                      s.FREZONE = pZone).FirstOrDefault
            If lstMaster Is Nothing Then
                Dim penddingAdd As New ED04RECF
                penddingAdd.FREPRJNO = pProject
                penddingAdd.FREPHASE = pPhase
                penddingAdd.FREZONE = pZone
                penddingAdd.FAREAUPRC = 0
                penddingAdd.FPOVERAREA = 0
                penddingAdd.FPCORNER1 = 0
                penddingAdd.FPCORNER2 = 0
                penddingAdd.FPMROADA = 0
                penddingAdd.FPMROADB = 0
                penddingAdd.FPMROADC = 0
                penddingAdd.FPPUBLICA = 0
                penddingAdd.FPPUBLICB = 0
                penddingAdd.FPPUBLICC = 0
                penddingAdd.FMPUBLIC = 0
                penddingAdd.FPBOOKA = 0
                penddingAdd.FPBOOKB = 0
                penddingAdd.FPBOOKC = 0
                penddingAdd.FRMINBOOK = 0
                penddingAdd.FRCONTRACT = 0
                penddingAdd.FRDOWN = 0
                penddingAdd.FDOWNMON = 0
                penddingAdd.FRINTA = 0
                penddingAdd.FRINTB = 0
                penddingAdd.FRINTC = 0
                penddingAdd.FPCONTRACTA = 0
                penddingAdd.FPCONTRACTB = 0
                penddingAdd.FPCONTRACTC = 0
                penddingAdd.FYPAYMENT1 = 0
                penddingAdd.FYPAYMENT2 = 0
                penddingAdd.FYPAYMENT3 = 0
                penddingAdd.FYPAYMENT4 = 0
                penddingAdd.FMLEFT = 0
                penddingAdd.FMTOP = 0
                penddingAdd.FMWIDTH = 0
                penddingAdd.FMHEIGHT = 0
                penddingAdd.FZONEPIC = 0

                db.ED04RECF.Add(penddingAdd)
            End If

            db.SaveChanges()
        End Using
        Return True
    End Function
    Public Function Delete(ByVal pProject As String,
                           ByVal pPhase As String,
                           ByVal pZone As String,
                           ByVal pUserID As String) As Boolean
        Using db As New PNSWEBEntities
            'Dim lst As List(Of ED03UNIT) = db.ED03UNIT.Where(Function(s) s.FREPRJNO.Trim = pProject And
            '                                                             s.FREPHASE = pPhase And
            '                                                             s.FREZONE = pZone).ToList
            'If lst IsNot Nothing Then
            '    For Each lc As ED03UNIT In lst
            '        lc.FREZONE = Nothing
            '        lc.FREPHASE = Nothing
            '    Next
            'End If

            Dim lst As List(Of ED04RECF) = db.ED04RECF.Where(Function(s) s.FREPRJNO.Trim = pProject And
                                                                         s.FREPHASE = pPhase And
                                                                         s.FREZONE = pZone).ToList
            If lst IsNot Nothing Then
                For Each m As ED04RECF In lst
                    db.ED04RECF.Remove(m)
                Next
            End If
            db.SaveChanges()
        End Using
        Return True
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
    Public Function LoadProjectEdit(ByVal pProject As String) As ED01PROJ
        Using db As New PNSWEBEntities
            Dim lst As ED01PROJ = db.ED01PROJ.Where(Function(s) s.FREPRJNO.Trim = pProject).FirstOrDefault
            Return lst
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
    Public Function LoadPhase(ByVal pKeyID As String,
                              ByVal pProject As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pProject = String.Empty Then
                Return Nothing
            Else
                If pKeyID = String.Empty Then
                    Dim qury = (From m In db.ED02PHAS.Where(Function(s) s.FREPRJNO.Trim = pProject).OrderBy(Function(s) s.FREPHASE)
                                Select m.FREPHASE).ToList()

                    Return qury.ToList()
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = (From m In db.ED02PHAS.Where(Function(s) s.FREPRJNO.Trim = pProject).OrderBy(Function(s) s.FREPHASE)
                                Select m.FREPHASE) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                    Return qury.ToList()
                End If
            End If
            Return Nothing
        End Using
    End Function
#End Region
#End Region

End Class

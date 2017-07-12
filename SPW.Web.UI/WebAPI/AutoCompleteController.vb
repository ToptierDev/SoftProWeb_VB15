Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Description
Imports System.Web.Script.Serialization
Imports Newtonsoft
Imports SPW.DAL
Imports SPW.BLL

Namespace WebAPI
    Public Class AutoCompleteController
        Inherits System.Web.Http.ApiController

        Private db As New PNSDBWEBEntities
        Private dbw As New PNSWEBEntities
        Private dbv As New PNSWEB_SoftProEntities
        Function GetData(ByVal query As String, ByVal dataSource As String) As IHttpActionResult
            Dim serializer As New JavaScriptSerializer
            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try

                If (dataSource = "AD01VEN1_ALL") Then
                    Return Json(dbw.AD01VEN1.OrderBy(Function(x) x.FSUCODE).Select(Function(x) New With {.FSUCODE = x.FSUCODE, .FSUNAME = x.FSUNAME}).ToList())
                ElseIf (dataSource = "AD01VEN1") Then
                    Return Json(dbw.AD01VEN1.Where(Function(x) x.FSUCODE.StartsWith(query)).ToList())
                ElseIf (dataSource = "PROJECT_PHASE_ZONE_BLOCK_MASTER") Then
                    Dim q = query.Split("_")
                    Dim sUserID = q(0)
                    'ED01PROJ --project
                    'ED02PHAS --phase
                    'ED04RECF --zone
                    'ED04BLOK --block
                    Dim results As New ProjectPhaseZoneBlockMaster_ViewModel
                    'Dim projects = dbw.ED01PROJ.OrderBy(Function(x) x.FREPRJNO).ToList()
                    Dim bl As cPermission = New cPermission
                    Dim projects = bl.LoadProject(sUserID)

                    Dim phases = dbw.ED02PHAS.OrderBy(Function(x) x.FREPHASE).ToList()
                    Dim zones = dbw.ED04RECF.OrderBy(Function(x) x.FREZONE).ToList()
                    Dim blocks = dbw.ED04BLOK.OrderBy(Function(x) x.FREBLOCK).ToList()

                    results.projects = projects.Select(Function(x) New ProjectPhaseZoneBlockMaster_Project_ViewModel With {.FREPRJNO = x.FREPRJNO.Trim, .ED01PROJ = x}).ToList()
                    For Each pj In results.projects
                        pj.phases = phases.Where(Function(x) pj.FREPRJNO = x.FREPRJNO.Trim).Select(Function(x) New ProjectPhaseZoneBlockMaster_Phase_ViewModel With {.FREPHASE = x.FREPHASE, .ED02PHAS = x}).ToList()
                        For Each ph In pj.phases
                            ph.zones = zones.Where(Function(x) pj.FREPRJNO = x.FREPRJNO.Trim And ph.FREPHASE = x.FREPHASE).Select(Function(x) New ProjectPhaseZoneBlockMaster_Zone_ViewModel With {.FREZONE = x.FREZONE, .ED04RECF = x}).ToList()
                            For Each zn In ph.zones
                                zn.blocks = blocks.Where(Function(x) pj.FREPRJNO.Trim = x.FREPRJNO And ph.FREPHASE = x.FREPHASE And zn.FREZONE = x.FREZONE).Select(Function(x) New ProjectPhaseZoneBlockMaster_Block_ViewModel With {.FREBLOCK = x.FREBLOCK, .ED04BLOK = x}).ToList()
                            Next
                        Next
                    Next
                    returnResult.Datas.Data1 = results
                    Return Json(returnResult)

                ElseIf (dataSource = "FD11PROP") Then

                    Dim listAsset = dbv.vw_FD11PROP_NotInAD11INV1.[Select](Function(f) f.FASSETNO).ToList
                    Dim fd = dbw.FD11PROP.Where(Function(x) x.FASSETNO = query Or
                                               listAsset.Contains(x.FASSETNO))
                    Return Json(fd)

                ElseIf (dataSource = "BD10DIVI") Then
                    Return Json(dbw.BD10DIVI.Select(Function(x) New With {.FDIVCODE = x.FDIVCODE, .FDIVNAME = x.FDIVNAME, .FDIVNAMET = x.FDIVNAMET}).OrderBy(Function(x) x.FDIVCODE))
                ElseIf (dataSource = "ED03UNITSTATUS") Then
                    Return Json(dbw.ED03UNITSTATUS.Where(Function(x) x.FRESTATUS IsNot Nothing And x.FRESTATUS <> "") _
                                .Select(Function(x) New With {.FRESTATUS = x.FRESTATUS,
                                                              .FRESTATUSDESC = x.FRESTATUSDESC, .Display = x.FRESTATUS + "-" + x.FRESTATUSDESC}).OrderBy(Function(x) x.FRESTATUS))
                ElseIf (dataSource = "ED03UNITTYPE") Then ''#####################Add By Nattawit.kr 2017/05/15
                    Dim Data = (dbw.ED03UNITTYPE.Where(Function(x) x.FRETYPE IsNot Nothing And x.FRETYPE <> "") _
                                .Select(Function(x) New With {.FRETYPE = x.FRETYPE,
                                                              .FRESTYPEDESC = x.FRESTYPEDESC, .Display = x.FRETYPE + "-" + x.FRESTYPEDESC}).OrderBy(Function(x) x.FRETYPE)).ToList
                    returnResult.Datas.Data1 = Data
                    Return Json(returnResult)

                ElseIf (dataSource = "ED01PROJ") Then
                    Dim q = query.Split("_")
                    Dim sUserID = q(0)

                    Dim bl As cPermission = New cPermission
                    Return Json(bl.LoadProject(sUserID).Select(Function(x) New With
                                                  {
                       .FREPRJNO = x.FREPRJNO,
                      .FREPRJNM = x.FREPRJNM,
                      .FRELOCATE1 = x.FRELOCAT1,
                      .FRELOCATE2 = x.FRELOCAT2,
                      .FRELOCATE3 = x.FRELOCAT3,
                      .FREPROVINCE = x.FREPROVINC,
                      .FREPOSTAL = x.FREPOSTAL,
                      .FTOTALAREA = x.FTOTAREA,
                      .FNOOFLAND = x.FNOOFLAND,
                      .FLANDNO = x.FLANDNO}) _
                      .OrderBy(Function(x) x.FREPRJNO))

                    'Return Json(dbw.ED01PROJ.Select(Function(x) New With
                    '                                {
                    '                                .Display = x.FREPRJNO + "-" + x.FREPRJNM,
                    '    .FREPRJNO = x.FREPRJNO,
                    '    .FREPRJNM = x.FREPRJNM,
                    '    .FRELOCATE1 = x.FRELOCAT1,
                    '    .FRELOCATE2 = x.FRELOCAT2,
                    '    .FRELOCATE3 = x.FRELOCAT3,
                    '    .FREPROVINCE = x.FREPROVINC,
                    '    .FREPOSTAL = x.FREPOSTAL,
                    '    .FTOTALAREA = x.FTOTAREA,
                    '    .FNOOFLAND = x.FNOOFLAND,
                    '    .FLANDNO = x.FLANDNO}) _
                    '    .OrderBy(Function(x) x.FREPRJNO))

                ElseIf (dataSource = "ED03UNIT_ByPhase") Then
                    Dim q = query.Split("_")
                    Dim FREPRJNO = q(0)
                    Dim FREPHASE = q(1)

                    Dim data = dbw.ED03UNIT.Where(Function(x) x.FREPRJNO = FREPRJNO And x.FREPHASE = FREPHASE And x.FREZONE IsNot Nothing) _
                        .Select(Function(x) x.FREZONE).Distinct()


                    returnResult.Datas.Data1 = data
                    Return Json(returnResult)

                ElseIf (dataSource = "ED03UNIT_Available") Then

                    'FASSETNO_FREPRJNO_FSERIALNO
                    Dim q = query.Split("_")
                    Dim FASSETNO = q(0)
                    Dim FREPRJNO = q(1)
                    Dim FSERIALNO = q(2)
                    Dim lc = dbw.ED03UNIT.Where(Function(s) s.FSERIALNO = FSERIALNO And s.FREPRJNO = FREPRJNO).FirstOrDefault

                    If IsNothing(lc) Then
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "ไม่พบข้อมูลหมายเลขแปลงตามแผนผัง " & FSERIALNO
                        'returnResult.Datas.Data1 = Nothing
                        Return Json(returnResult)
                    End If

                    Dim ed03 = dbw.ED03UNIT.Where(Function(x) _
                                               x.FREPRJNO = FREPRJNO _
                                              And (x.FASSETNO Is Nothing Or x.FASSETNO = FASSETNO) _
                                              And x.FSERIALNO = FSERIALNO).Select(Function(x) New With
                                              {
                                                .FASSETNO = x.FASSETNO,
                                                .FPDCODE = x.FPDCODE,
                                                .FSERIALNO = x.FSERIALNO,
                                                .FADDRNO = x.FADDRNO
                                              }).FirstOrDefault
                    If (IsNothing(ed03)) Then
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "หมายเลขแปลงตามผัง : " & FSERIALNO & " ถูกใช้งานแล้ว"

                    Else
                        returnResult.Datas.Data1 = ed03
                    End If
                    Return Json(returnResult)


                ElseIf (dataSource = "list-available-ed03unit-by-freprjno") Then

                    'FASSETNO_FREPRJNO_FSERIALNO
                    Dim q = query.Split("_")
                    Dim FREPRJNO = q(0)

                    Dim ed03 = dbw.ED03UNIT.Where(Function(x) _
                                               x.FREPRJNO = FREPRJNO _
                                              And (x.FASSETNO Is Nothing)
                                              ).Select(Function(x) New With
                                              {
                                                .FASSETNO = x.FASSETNO,
                                                .FPDCODE = x.FPDCODE,
                                                .FSERIALNO = x.FSERIALNO,
                                                .FADDRNO = x.FADDRNO
                                              }).ToList()
                    If (IsNothing(ed03)) Then
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "ไม่พบข้อมูล"

                    Else
                        returnResult.Datas.Data1 = ed03
                    End If
                    Return Json(returnResult)


                ElseIf (dataSource = "ED03UNIT_Master") Then
                    'FASSETNO_FREPRJNO_FSERIALNO
                    Dim q = query.Split("_")
                    Dim FREPRJNO = q(0)
                    Dim FSERIALNO = q(1)

                    Dim ed03 As List(Of ED03UNIT) = dbw.ED03UNIT.Where(Function(s) s.FREPRJNO = FREPRJNO _
                                                                               And s.FSERIALNO = FSERIALNO).ToList
                    If ed03.Count > 0 Then
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "ยูนิต # : " & FSERIALNO & " ถูกใช้งานแล้ว"
                    Else
                        returnResult.Datas.Data1 = ed03.ToList()
                    End If
                    Return Json(returnResult)

                ElseIf (dataSource = "ED03UNIT_CHECKDUP_RUNNING") Then
                    'FASSETNO_FREPRJNO_FSERIALNO
                    Dim q = query.Split("_")
                    Dim FREPRJNO = q(0)
                    Dim START = q(1)
                    Dim COUNT = q(2)

                    Dim seperateIndex = 0
                    For i = 0 To START.Length - 1
                        Try
                            Dim a = START.Substring(i)
                            Integer.Parse(a)

                        Catch ex As Exception
                            seperateIndex = i
                        End Try
                    Next
                    Dim strStart = START.Substring(0, seperateIndex + 1)
                    Dim intStart = Integer.Parse(START.Substring(seperateIndex + 1))
                    Dim padleng = START.Substring(seperateIndex + 1).Length
                    Dim listUnit As New List(Of String)
                    For i As Integer = 0 To COUNT - 1
                        listUnit.Add(strStart + (intStart + i).ToString.PadLeft(padleng, "0"))

                    Next

                    Dim listEd03Exist = ED03UNITExists(listUnit, FREPRJNO)
                    If (listEd03Exist.Count > 0) Then
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "ยูนิต:" + String.Join(", ", listEd03Exist) + " ซ้ำ"
                        Return Json(returnResult)
                    End If



                    Return Json(returnResult)

                ElseIf (dataSource = "SD05PDDS_Master") Then

                    'FPDCODE
                    Dim q = query.Split("_")
                    Dim pFPDCODE = q(0)

                    Dim ed03 = (From m In dbw.SD05PDDS.Where(Function(s) s.FPDCODE = pFPDCODE)
                                Join t In dbw.SD03TYPE On m.FTYCODE Equals t.FTYCODE
                                Select (New With {Key .FPDCODE = m.FPDCODE,
                                                    .FPDNAME = m.FPDNAME,
                                                     .FAREA = m.FSTDAREA,
                                                     .FBUILTIN = m.FSTDBUILT,
                                                     .FDESC = t.FDESC
                                                })).FirstOrDefault
                    If IsNothing(ed03) Then
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "แบบบ้าน : " & pFPDCODE & " ไม่มีอยู่ในระบบ"
                    Else
                        returnResult.Datas.Data1 = ed03
                    End If
                    Return Json(returnResult)
                ElseIf (dataSource = "SD05PDDS_All") Then
                    'ข้อมูลแบบบ้านทั้งหมด
                    Dim ed03 = (From m In dbw.SD05PDDS
                                Join t In dbw.SD03TYPE On m.FTYCODE Equals t.FTYCODE
                                Where t.FENTTYPE = "1"
                                Select (New With {Key .FPDCODE = m.FPDCODE,
                                                    .FPDNAME = m.FPDNAME,
                                                     .FAREA = m.FSTDAREA,
                                                     .FBUILTIN = m.FSTDBUILT,
                                                     .FDESC = t.FDESC
                                                })).ToList()
                    If IsNothing(ed03) Then
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "ไม่พบข้อมูลแบบบ้าน"
                    Else
                        returnResult.Datas.Data1 = ed03
                    End If
                    Return Json(returnResult)
                ElseIf (dataSource = "UNITDUP_ProjectPrice") Then

                    'FASSETNO_FREPRJNO_FSERIALNO
                    Dim q = query.Split("_")
                    Dim pFREPRJNO = q(0)
                    Dim pFREPHASE = q(1)
                    Dim pFREZONE = q(2)
                    Dim pFSERIALNO = q(3)

                    'Dim ed03 = (From a In dbw.ED03UNIT
                    '            Join b In dbw.SD05PDDS On a.FPDCODE Equals b.FPDCODE
                    '            Where a.FREPRJNO = pFREPRJNO And a.FREZONE = pFREZONE And a.FREPHASE = pFREPHASE And a.FSERIALNO = pFSERIALNO
                    '            Select (New With {Key .FPDCODE = a.FPDCODE,
                    '                                 .FPDNAME = b.FPDNAME,
                    '                                 .FAREA = a.FAREA,
                    '                                 .FRESTATUS = a.FRESTATUS,
                    '                .FSERIALNO = a.FSERIALNO
                    '                            })).FirstOrDefault


                    Dim ed03 = (From a In dbw.ED03UNIT
                                Group Join b In dbw.SD05PDDS On a.FPDCODE Equals b.FPDCODE
Into dgrop = Group From b In dgrop.DefaultIfEmpty()
                                Where a.FREPRJNO = pFREPRJNO And a.FREZONE = pFREZONE And a.FREZONE = pFREZONE And a.FREPHASE = pFREPHASE And a.FSERIALNO = pFSERIALNO
                                Select (New With {Key .FPDCODE = a.FPDCODE,
.FPDNAME = b.FPDNAME,
.FAREA = a.FAREA,
.FRESTATUS = a.FRESTATUS,
.FSERIALNO = a.FSERIALNO
})).FirstOrDefault

                    If Not IsNothing(ed03) Then
                        ed03.FRESTATUS = IIf(IsNothing(ed03.FRESTATUS), "0", ed03.FRESTATUS)

                        If ed03.FRESTATUS = "2" Or ed03.FRESTATUS = "3" Or ed03.FRESTATUS = "4" Then
                            returnResult.ErrorView.IsError = True
                            returnResult.ErrorView.Message = "ยูนิต # : " & pFSERIALNO & " นี้ถูกขายไปเรียบร้อยแล้วไม่สามารถเพิ่มได้"
                            returnResult.Datas.Data1 = ed03
                        Else
                            returnResult.Datas.Data1 = ed03
                        End If
                    Else
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "ยูนิต # : " & pFSERIALNO & " ต้องอยู่ในโครงการ เฟส และโซนนี้"
                    End If
                    Return Json(returnResult)

                ElseIf (dataSource = "list-ed03unit-for-projectpricelist") Then

                    'FASSETNO_FREPRJNO_FSERIALNO
                    Dim q = query.Split("_")
                    Dim pFREPRJNO = q(0)
                    Dim pFREPHASE = q(1)
                    Dim pFREZONE = q(2)

                    Dim ed03 = (From a In dbw.ED03UNIT
                                Group Join b In dbw.SD05PDDS On a.FPDCODE Equals b.FPDCODE
Into dgrop = Group From b In dgrop.DefaultIfEmpty()
                                Where a.FREPRJNO = pFREPRJNO And a.FREZONE = pFREZONE And a.FREZONE = pFREZONE And a.FREPHASE = pFREPHASE _
                                    And Not (a.FRESTATUS = "2" Or a.FRESTATUS = "3" Or a.FRESTATUS = "4")
                                Select (New With {Key .FPDCODE = a.FPDCODE,
.FPDNAME = b.FPDNAME,
.FAREA = a.FAREA,
.FRESTATUS = a.FRESTATUS,
.FSERIALNO = a.FSERIALNO
})).ToList()

                    If Not IsNothing(ed03) Then
                        returnResult.Datas.Data1 = ed03
                    Else
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "ไม่พบข้อมูล"
                    End If
                    Return Json(returnResult)


                ElseIf (dataSource = "FD11PROP_setFPCPIECE") Then

                    'FASSETNO_FREPRJNO_FSERIALNO
                    Dim q = query.Split("_")
                    Dim pFREPRJNO = q(0)
                    Dim pFPCPIECE = q(1)

                    Dim fd11 As List(Of FD11PROP) = dbw.FD11PROP.Where(Function(s) s.FREPRJNO = pFREPRJNO _
                                                                               And s.FPCPIECE = pFPCPIECE).ToList
                    If fd11.Count > 0 Then
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "โฉนด # : " & pFPCPIECE & " ถูกใช้งานแล้ว"
                    Else
                        returnResult.Datas.Data1 = fd11.ToList()
                    End If
                    Return Json(returnResult)
                ElseIf (dataSource = "ED02PHAS") Then
                    Dim FREPRJNO = query
                    Dim data = dbw.ED02PHAS.Where(Function(x) x.FREPRJNO = FREPRJNO).ToList()
                    returnResult.Datas.Data1 = data
                    Return Json(returnResult)
                End If
            Catch ex As Exception

                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = ex.GetBaseException().Message
                returnResult.ErrorView.StackTrace = ex.StackTrace
                Return Json(returnResult)
            End Try

            returnResult.ErrorView.IsError = True
            returnResult.ErrorView.Message = "Data source not found"

            Return Json(returnResult)

        End Function





        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
                dbw.Dispose()
                dbv.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function AD01VEN1Exists(ByVal id As String) As Boolean
            Return dbw.AD01VEN1.Count(Function(e) e.FSUCODE = id) > 0
        End Function
        Private Function ED03UNITExists(ByVal listEd03 As List(Of String), ByVal FREPRJNO As String) As List(Of String)
            Return dbw.ED03UNIT.Where(Function(e) e.FREPRJNO = FREPRJNO And listEd03.Contains(e.FSERIALNO)).Select(Function(x) x.FSERIALNO).ToList()
        End Function
    End Class
End Namespace
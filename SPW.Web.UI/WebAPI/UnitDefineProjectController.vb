Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Description
Imports SPW.DAL

Namespace WebAPI
    Public Class UnitDefineProjectController
        Inherits System.Web.Http.ApiController

        Private db As New PNSWEBEntities
        Private dbView As New PNSWEB_SoftproEntities

        ' GET: api/ED03UNIT
        Function GetED03UNIT() As IQueryable(Of ED03UNIT)

            Return db.ED03UNIT
        End Function

        'Private Function generateUnit(ByVal start As String, ByVal finish As String) As List(Of String)
        '    Dim listUnit As List(Of String)
        '    If (start.Length <> finish.Length) Then
        '        Return listUnit
        '    End If
        '    Dim seperateStartIndex = 0
        '    For i = 0 To start.Length - 1
        '        Try
        '            Dim a = start.Substring(i)
        '            Integer.Parse(a)

        '        Catch ex As Exception
        '            seperateStartIndex = i
        '        End Try
        '    Next
        '    Dim seperateFinishIndex = 0
        '    For i = 0 To finish.Length - 1
        '        Try
        '            Dim a = finish.Substring(i)
        '            Integer.Parse(a)
        '        Catch ex As Exception
        '            seperateFinishIndex = i
        '        End Try
        '    Next
        '    If (seperateStartIndex <> seperateFinishIndex) Then
        '        Return listUnit
        '    End If

        '    Dim strStart = start.Substring(0, seperateStartIndex + 1)
        '    Dim intStart = Integer.Parse(start.Substring(seperateStartIndex + 1))
        '    Dim strFinish = start.Substring(0, seperateFinishIndex + 1)
        '    Dim intFinish = Integer.Parse(finish.Substring(seperateFinishIndex + 1))
        '    Dim padleng = start.Substring(seperateStartIndex + 1).Length
        '    If (strStart <> strFinish) Then
        '        Return listUnit
        '    End If


        '    listUnit = New List(Of String)
        '    Try
        '        Dim s = strStart(strStart.Length - 1)
        '        Dim f = strFinish(strFinish.Length - 1)
        '        While s <> f
        '            strStart = Mid(strStart, strFinish.Length - 1, 1)
        '            For i As Integer = intStart To intFinish
        '                listUnit.Add(strStart + (i).ToString.PadLeft(padleng, "0"))
        '            Next
        '            s = Inc(s)
        '        End While


        '        Return listUnit
        '    Catch ex As Exception
        '        Return listUnit
        '    End Try

        'End Function

        'Public Function Inc(ByVal c As Char)

        '    'Remember if input is uppercase for later
        '    Dim isUpper = Char.IsUpper(c)

        '    'Work in lower case for ease
        '    c = Char.ToLower(c)

        '    'Check input range
        '    If c < "a" Or c > "z" Then Throw New ArgumentOutOfRangeException

        '    'Do the increment
        '    c = Chr(Asc(c) + 1)

        '    'Check not left alphabet
        '    If c > "z" Then c = "a"

        '    'Check if input was upper case
        '    If isUpper Then c = Char.ToUpper(c)
        '    Return c
        'End Function


        ' GET: api/ED03UNIT/5
        <ResponseType(GetType(ED03UNIT))>
        Async Function GetED03UNIT(ByVal id As String, ByVal phase As String, ByVal zone As String, ByVal unitfrom As String, ByVal unitto As String) As Task(Of IHttpActionResult)
            Dim f As String = "0000000000"
            Dim t As String = "ZZZZZZZZZZ"
            Dim query =
                 dbView.vw_UnitDefineProject.Where(Function(s) _
                                                         s.FREPRJNO = id _
                                                         And (s.FREPHASE = phase Or phase Is Nothing) _
                                                         And (s.FREZONE = zone Or zone Is Nothing))

            If (unitfrom <> Nothing) Then
                Try
                    f = (Integer.Parse(unitfrom)).ToString().PadLeft(10, "0")
                    query = query.Where(Function(s) SqlServer.SqlFunctions.Replicate("0", 10 - s.FSERIALNO.ToString().Length) + s.FSERIALNO >= f)
                Catch ex As Exception
                    f = unitfrom.PadRight(10, "0")
                    query = query.Where(Function(s) s.FSERIALNO + SqlServer.SqlFunctions.Replicate("0", 10 - s.FSERIALNO.ToString().Length) >= f)
                End Try

            End If
            If (unitto <> Nothing) Then
                Try
                    t = (Integer.Parse(unitto)).ToString().PadLeft(10, "0")
                    query = query.Where(Function(s) SqlServer.SqlFunctions.Replicate("0", 10 - s.FSERIALNO.ToString().Length) + s.FSERIALNO <= t)

                Catch ex As Exception
                    t = unitto.PadRight(10, "Z")
                    query = query.Where(Function(s) s.FSERIALNO + SqlServer.SqlFunctions.Replicate("Z", 10 - s.FSERIALNO.ToString().Length) <= t)
                End Try
            End If


            Dim eD03UNIT As List(Of vw_UnitDefineProject) = Await query.OrderBy(Function(s) s.FSERIALNO).ToListAsync
            If IsNothing(eD03UNIT) Then
                Return NotFound()
            End If

            Dim viewModel As New TRN_UnitDefineProject_ViewModel
            viewModel.List_vwED03UNIT = eD03UNIT

            Return Ok(viewModel)
        End Function

        ' GET: api/ED03UNIT/5
        <ResponseType(GetType(ED03UNIT))>
        Async Function GetED03UNIT(ByVal id As String, ByVal culture As String) As Task(Of IHttpActionResult)
            Dim eD03UNIT As List(Of vw_UnitDefineProject) = Await dbView.vw_UnitDefineProject.Where(Function(s) s.FREPRJNO = id).ToListAsync
            If IsNothing(eD03UNIT) Then
                Return NotFound()
            End If

            Dim viewModel As New TRN_UnitDefineProject_ViewModel
            viewModel.List_vwED03UNIT = eD03UNIT
            Return Ok(viewModel)
        End Function

        ' PUT: api/ED03UNIT/5
        <ResponseType(GetType(Void))>
        Async Function PutED03UNIT(ByVal viewModel As TRN_UnitDefineProject_ViewModel) As Task(Of IHttpActionResult)
            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try
                If Not ModelState.IsValid Then
                    returnResult.ErrorView.IsError = True

                    returnResult.ErrorView.Message = "Bad Request"
                    returnResult.ErrorView.ErrorObject = ModelState
                    Return Json(returnResult)
                End If
                If viewModel IsNot Nothing Then
                    If viewModel.ED03UNIT IsNot Nothing Then
                        Dim eD03 As ED03UNIT = viewModel.ED03UNIT
                        If eD03.FREPRJNO = String.Empty And
                           eD03.FSERIALNO = String.Empty Then

                            Dim pendingadd As New ED03UNIT
                            pendingadd.FREPRJNO = eD03.FREPRJNO
                            pendingadd.FSERIALNO = eD03.FSERIALNO
                            pendingadd.FAREAUPRC = 0
                            pendingadd.FAREATPRC = 0
                            pendingadd.FBUILDPRC = 0
                            pendingadd.FPOVERAREA = 0
                            pendingadd.FTOVERAREA = 0
                            pendingadd.FTPRICE = 0
                            pendingadd.FASSPRCA = 0
                            pendingadd.FASSCHG = 0
                            pendingadd.FMLEFT = 0
                            pendingadd.FMTOP = 0
                            pendingadd.FEMETERSIZE = 0
                            pendingadd.FEMETERINSURE = 0
                            pendingadd.FEMETERINSTALL = 0
                            pendingadd.FWMETERSIZE = 0
                            pendingadd.FWMETERINSURE = 0
                            pendingadd.FWMETERINSTALL = 0
                            pendingadd.FTELNOINSTALL = 0
                            pendingadd.FSERVICE1 = 0
                            pendingadd.FSERVICE2 = 0
                            pendingadd.StmDate = Date.Now
                            pendingadd.StmTime = DateTime.Now.ToString("HH:mm:ss")

                            db.ED03UNIT.Add(pendingadd)
                            Await db.SaveChangesAsync()
                        End If
                    End If
                    If viewModel.List_vwED03UNIT IsNot Nothing Then
                        For Each eD03 As vw_UnitDefineProject In viewModel.List_vwED03UNIT
                            If eD03.e_FSERIALNO <> eD03.FSERIALNO Then
                                Dim lc = db.ED03UNIT.Where(Function(s) s.FSERIALNO = eD03.e_FSERIALNO And
                                                                       s.FREPRJNO = eD03.FREPRJNO).FirstOrDefault
                                If lc IsNot Nothing Then
                                    db.ED03UNIT.Remove(lc)

                                    Dim pendingadd As New ED03UNIT
                                    pendingadd.FREPRJNO = lc.FREPRJNO
                                    pendingadd.FRESTATUS = lc.FRESTATUS
                                    pendingadd.FSERIALNO = eD03.FSERIALNO
                                    pendingadd.FTYUNIT = lc.FTYUNIT
                                    pendingadd.FPDCODE = lc.FPDCODE
                                    pendingadd.FAREA = lc.FAREA
                                    pendingadd.FADDRNO = lc.FADDRNO
                                    pendingadd.FREPHASE = lc.FREPHASE
                                    pendingadd.FREZONE = lc.FREZONE
                                    pendingadd.FREBLOCK = lc.FREBLOCK
                                    pendingadd.FBUILTIN = lc.FBUILTIN
                                    pendingadd.FTOWER = lc.FTOWER
                                    pendingadd.FFLOOR = lc.FFLOOR
                                    pendingadd.FAREAUPRC = lc.FAREAUPRC
                                    pendingadd.FAREATPRC = lc.FAREATPRC
                                    pendingadd.FBUILDPRC = lc.FBUILDPRC
                                    pendingadd.FPOVERAREA = lc.FPOVERAREA
                                    pendingadd.FTOVERAREA = lc.FTOVERAREA
                                    pendingadd.FTPRICE = lc.FTPRICE
                                    pendingadd.FASSPRCA = lc.FASSPRCA
                                    pendingadd.FASSCHG = lc.FASSCHG
                                    pendingadd.FMLEFT = lc.FMLEFT
                                    pendingadd.FMTOP = lc.FMTOP
                                    pendingadd.FEMETERSIZE = lc.FEMETERSIZE
                                    pendingadd.FEMETERINSURE = lc.FEMETERINSURE
                                    pendingadd.FEMETERINSTALL = lc.FEMETERINSTALL
                                    pendingadd.FWMETERSIZE = lc.FWMETERSIZE
                                    pendingadd.FWMETERINSURE = lc.FWMETERINSURE
                                    pendingadd.FWMETERINSTALL = lc.FWMETERINSTALL
                                    pendingadd.FTELNOINSTALL = lc.FTELNOINSTALL
                                    pendingadd.FSERVICE1 = lc.FSERVICE1
                                    pendingadd.FSERVICE2 = lc.FSERVICE2
                                    pendingadd.StmDate = Date.Now
                                    pendingadd.StmTime = DateTime.Now.ToString("HH:mm:ss")

                                    db.ED03UNIT.Add(pendingadd)
                                    Await db.SaveChangesAsync()
                                End If
                            ElseIf eD03.FREPRJNO <> String.Empty And
                                   eD03.FSERIALNO <> String.Empty Then
                                Dim lc = db.ED03UNIT.Where(Function(s) s.FSERIALNO = eD03.FSERIALNO And
                                                                       s.FREPRJNO = eD03.FREPRJNO).FirstOrDefault
                                If lc IsNot Nothing Then
                                    If eD03.FRESTATUS <> String.Empty Then
                                        lc.FRESTATUS = eD03.FRESTATUS
                                    Else
                                        lc.FRESTATUS = Nothing
                                    End If
                                    If eD03.FSERIALNO <> String.Empty Then
                                        lc.FSERIALNO = eD03.FSERIALNO
                                    Else
                                        lc.FSERIALNO = Nothing
                                    End If
                                    If eD03.FTYUNIT <> String.Empty Then
                                        lc.FTYUNIT = eD03.FTYUNIT
                                    Else
                                        lc.FTYUNIT = Nothing
                                    End If
                                    If eD03.FPDCODE <> String.Empty Then
                                        lc.FPDCODE = eD03.FPDCODE
                                    Else
                                        lc.FPDCODE = Nothing
                                    End If
                                    If eD03.FAREA IsNot Nothing Then
                                        lc.FAREA = eD03.FAREA
                                    Else
                                        lc.FAREA = Nothing
                                    End If
                                    If eD03.FADDRNO <> String.Empty Then
                                        lc.FADDRNO = eD03.FADDRNO
                                    Else
                                        lc.FADDRNO = Nothing
                                    End If
                                    If eD03.FREPHASE <> String.Empty Then
                                        lc.FREPHASE = eD03.FREPHASE
                                    Else
                                        lc.FREPHASE = Nothing
                                    End If
                                    If eD03.FREZONE <> String.Empty Then
                                        lc.FREZONE = eD03.FREZONE
                                    Else
                                        lc.FREZONE = Nothing
                                    End If
                                    If eD03.FREBLOCK <> String.Empty Then
                                        lc.FREBLOCK = eD03.FREBLOCK
                                    Else
                                        lc.FREBLOCK = Nothing
                                    End If
                                    If eD03.FBUILTIN IsNot Nothing Then
                                        lc.FBUILTIN = eD03.FBUILTIN
                                    Else
                                        lc.FBUILTIN = Nothing
                                    End If
                                    If eD03.FTOWER <> String.Empty Then
                                        lc.FTOWER = eD03.FTOWER
                                    Else
                                        lc.FTOWER = Nothing
                                    End If
                                    If eD03.FFLOOR <> String.Empty Then
                                        lc.FFLOOR = eD03.FFLOOR
                                    Else
                                        lc.FFLOOR = Nothing
                                    End If

                                    If eD03.FASSETNO <> String.Empty Then
                                        Dim lcFD11PROP = db.FD11PROP.Where(Function(s) s.FASSETNO = eD03.FASSETNO).FirstOrDefault
                                        If lcFD11PROP IsNot Nothing Then
                                            'lcFD11PROP.FDPCODE = eD03.FPDCODE
                                            lcFD11PROP.FPCINST = eD03.FADDRNO
                                        End If
                                    End If

                                    Await db.SaveChangesAsync()
                                End If
                            End If
                        Next
                    End If
                End If
                'If Not id = eD03.FREPRJNO Then
                '    returnResult.ErrorView.IsError = True
                '    returnResult.ErrorView.Message = "Not Found"
                '    Return Json(returnResult)
                'End If


            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = ex.Message
                returnResult.ErrorView.StackTrace = ex.StackTrace
                Return Json(returnResult)
            End Try


            Return Json(returnResult)
        End Function


        ' POST: api/ED03UNIT
        '<ResponseType(GetType(ED03UNIT))>
        Async Function PostED03UNIT(ByVal viewModel As TRN_UnitDefineProject_ViewModel) As Task(Of IHttpActionResult)
            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try
                If Not ModelState.IsValid Then
                    Return BadRequest(ModelState)
                End If

                Dim listEd03Exist = ED03UNITExists(viewModel.List_ED03UNIT.Select(Function(x) x.FSERIALNO).ToList(), viewModel.List_ED03UNIT(0).FREPRJNO)
                If (listEd03Exist.Count > 0) Then
                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "ยูนิต:" + String.Join(", ", listEd03Exist) + " ซ้ำ"
                    Return Json(returnResult)
                End If

                If viewModel.List_ED03UNIT(0).FPDCODE <> String.Empty Then
                    Dim listSD05PDDSExist = SD05PDDSExists(viewModel.List_ED03UNIT(0).FPDCODE)
                    If (listSD05PDDSExist.Count = 0) Then
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "แบบบ้าน:" + String.Join(",", listEd03Exist) + " ไม่มีอยู่ในระบบ"
                        Return Json(returnResult)
                    End If
                End If

                Dim pFAREA As Double? = 0
                Dim pFBUILTIN As Double? = 0
                Dim pFPDCODE As String = viewModel.List_ED03UNIT(0).FPDCODE
                'If viewModel.List_ED03UNIT(0).FPDCODE <> String.Empty Then
                '    Dim lc As SD05PDDS = db.SD05PDDS.Where(Function(s) s.FPDCODE = pFPDCODE).FirstOrDefault
                '    pFAREA = lc.FSTDAREA
                '    pFBUILTIN = lc.FSTDBUILT
                'End If

                If (viewModel.List_ED03UNIT IsNot Nothing) Then
                    For Each i As ED03UNIT In viewModel.List_ED03UNIT
                        'i.FAREA = pFAREA
                        'i.FBUILTIN = pFBUILTIN
                        i.FAREAUPRC = 0
                        i.FAREATPRC = 0
                        i.FBUILDPRC = 0
                        i.FPOVERAREA = 0
                        i.FTOVERAREA = 0
                        i.FTPRICE = 0
                        i.FASSPRCA = 0
                        i.FASSCHG = 0
                        i.FMLEFT = 0
                        i.FMTOP = 0
                        i.FEMETERSIZE = 0
                        i.FEMETERINSURE = 0
                        i.FEMETERINSTALL = 0
                        i.FWMETERSIZE = 0
                        i.FWMETERINSURE = 0
                        i.FWMETERINSTALL = 0
                        i.FTELNOINSTALL = 0
                        i.FSERVICE1 = 0
                        i.FSERVICE2 = 0
                        i.StmDate = Date.Now
                        i.StmTime = DateTime.Now.ToString("HH:mm:ss")
                    Next
                    db.ED03UNIT.AddRange(viewModel.List_ED03UNIT)
                    Await db.SaveChangesAsync()
                End If

                Dim pProjectCode As String = viewModel.List_ED03UNIT(0).FREPRJNO
                Dim pFSERIALNO As List(Of String) = viewModel.List_ED03UNIT.Select(Function(x) x.FSERIALNO).ToList()
                viewModel.List_vwED03UNIT = dbView.vw_UnitDefineProject.Where(Function(s) pFSERIALNO.Contains(s.FSERIALNO) And s.FREPRJNO = pProjectCode).ToList

                returnResult.Datas.Data1 = viewModel.List_vwED03UNIT.OrderBy(Function(s) s.FSERIALNO).ToList()


            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = getLastInnerException(ex)
                returnResult.ErrorView.ErrorObject = ex.InnerException
                returnResult.ErrorView.StackTrace = ex.StackTrace
            End Try
            Return Json(returnResult)
        End Function

        ' DELETE: api/ED03UNIT/5
        <ResponseType(GetType(ED03UNIT))>
        Async Function DeleteED03UNIT(ByVal id As String,
                                      ByVal pProject As String) As Task(Of IHttpActionResult)
            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try
                Dim listid = id.Split("_")
                Dim eD03UNIT As List(Of ED03UNIT) = Await db.ED03UNIT.Where(Function(s) listid.Contains(s.FSERIALNO) And s.FREPRJNO = pProject).ToListAsync()
                If IsNothing(eD03UNIT) Then
                    Return NotFound()
                End If
                db.ED03UNIT.RemoveRange(eD03UNIT)
                Await db.SaveChangesAsync()
            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = ex.Message
                returnResult.ErrorView.ErrorObject = ex.InnerException
            End Try
            returnResult.Datas.Data1 = New With {.id = id.Replace("_", ", ")}
            Return Json(returnResult)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function ED03UNITExists(ByVal listEd03 As List(Of String), ByVal FREPRJNO As String) As List(Of String)
            Return db.ED03UNIT.Where(Function(e) e.FREPRJNO = FREPRJNO And listEd03.Contains(e.FSERIALNO)).Select(Function(x) x.FSERIALNO).ToList()
        End Function

        Private Function SD05PDDSExists(ByVal FPDCODE As String) As List(Of String)
            Return db.SD05PDDS.Where(Function(e) e.FPDCODE = FPDCODE).Select(Function(x) x.FPDCODE).ToList()
        End Function

        Private Function getLastInnerException(ByVal ex As Exception)
            If (ex.InnerException Is Nothing) Then
                Return ex.Message
            Else
                Return getLastInnerException(ex.InnerException)
            End If
        End Function
    End Class
End Namespace
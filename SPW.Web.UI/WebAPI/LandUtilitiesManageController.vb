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
    Public Class LandUtilitiesManageController
        Inherits System.Web.Http.ApiController

        Private db As New PNSWEBEntities
        Private dbv As New PNSWEB_SoftProEntities

        ' GET: api/LandUtilitiesManage
        Function Search(ByVal query As String, ByVal culture As String, ByVal mode As String) As IQueryable(Of TRN_LandUtilitiesManage_ViewModel)
            Return db.FD11PROP.Take(10)
        End Function

        ' GET: api/LandUtilitiesManage/5
        <ResponseType(GetType(TRN_LandUtilitiesManage_ViewModel))>
        Async Function GetById(ByVal id As String, ByVal unitfrom As String, ByVal unitto As String, ByVal pcpiecefrom As String, ByVal pcpieceto As String, ByVal culture As String) As Task(Of IHttpActionResult)
            Dim eD01PROJ As ED01PROJ = Await db.ED01PROJ.FindAsync(id)
            If IsNothing(eD01PROJ) Then
                Return NotFound()
            End If
            Dim viewModel As New TRN_LandUtilitiesManage_ViewModel
            viewModel.ED01PROJ = eD01PROJ
            viewModel.List_vwFD11PROP = Await dbv.vw_FD11PROP_JoinED03UNIT.Where(Function(x) x.FREPRJNO = id _
                  And (x.FSERNO >= unitfrom Or unitfrom Is Nothing) _
                                                         And (x.FSERNO <= unitto Or unitto Is Nothing) _
                                                         And (x.FPCPIECE >= pcpiecefrom Or pcpiecefrom Is Nothing) _
                                                         And (x.FPCPIECE <= pcpieceto Or pcpieceto Is Nothing)
            ).OrderBy(Function(x) x.FASSETNO).ToListAsync()


            Return Ok(viewModel)
        End Function

        ' PUT: api/LandUtilitiesManage/5
        '<ResponseType(GetType(Void))>
        'Async Function PutFD11PROP(ByVal id As String, ByVal viewModel As TRN_LandUtilitiesManage_ViewModel) As Task(Of IHttpActionResult)
        Async Function PutFD11PROP(ByVal viewModel As TRN_LandUtilitiesManage_ViewModel) As Task(Of IHttpActionResult)
            Dim returnResult As GeneralViewModels = New GeneralViewModels

            'Dim fd11prop = viewModel.FD11PROP
            Try
                If Not ModelState.IsValid Then
                    returnResult.ErrorView.IsError = True

                    returnResult.ErrorView.Message = "Bad Request"
                    returnResult.ErrorView.ErrorObject = ModelState
                    Return Json(returnResult)
                End If

                For Each fd11prop In viewModel.List_FD11PROP


                    'If Not id = fd11prop.FASSETNO Then
                    '    returnResult.ErrorView.IsError = True
                    '    returnResult.ErrorView.Message = "Not Found"
                    '    Return Json(returnResult)
                    'End If
                    'db.Entry(fd11prop).State = Data.EntityState.Modified
                    Dim pendingUpdate As FD11PROP = db.FD11PROP.Where(Function(s) s.FASSETNO = fd11prop.FASSETNO And s.FREPRJNO = fd11prop.FREPRJNO).SingleOrDefault
                    If pendingUpdate IsNot Nothing Then
                        If fd11prop.FPCPIECE <> String.Empty Then
                            pendingUpdate.FPCPIECE = fd11prop.FPCPIECE
                        Else
                            pendingUpdate.FPCPIECE = Nothing
                        End If
                        If fd11prop.FPCLNDNO <> String.Empty Then
                            pendingUpdate.FPCLNDNO = fd11prop.FPCLNDNO
                        Else
                            pendingUpdate.FPCLNDNO = Nothing
                        End If
                        If fd11prop.FPCWIDTH <> String.Empty Then
                            pendingUpdate.FPCWIDTH = fd11prop.FPCWIDTH
                        Else
                            pendingUpdate.FPCWIDTH = Nothing
                        End If
                        If fd11prop.FPCBETWEEN <> String.Empty Then
                            pendingUpdate.FPCBETWEEN = fd11prop.FPCBETWEEN
                        Else
                            pendingUpdate.FPCBETWEEN = Nothing
                        End If
                        If fd11prop.FPCBOOK <> String.Empty Then
                            pendingUpdate.FPCBOOK = fd11prop.FPCBOOK
                        Else
                            pendingUpdate.FPCBOOK = String.Empty
                        End If
                        If fd11prop.FPCPAGE <> String.Empty Then
                            pendingUpdate.FPCPAGE = fd11prop.FPCPAGE
                        Else
                            pendingUpdate.FPCPAGE = Nothing
                        End If
                        If fd11prop.FQTY IsNot Nothing Then
                            pendingUpdate.FQTY = fd11prop.FQTY
                        Else
                            pendingUpdate.FQTY = Nothing
                        End If
                        If fd11prop.FSERNO <> String.Empty Then
                            pendingUpdate.FSERNO = fd11prop.FSERNO
                        Else
                            pendingUpdate.FSERNO = Nothing
                        End If
                        If fd11prop.FMORTGYN <> String.Empty Then
                            pendingUpdate.FMORTGYN = fd11prop.FMORTGYN
                        Else
                            pendingUpdate.FMORTGYN = String.Empty
                        End If
                        'If fd11prop.FDPCODE <> String.Empty Then
                        '    pendingUpdate.FDPCODE = fd11prop.FDPCODE
                        'Else
                        '    pendingUpdate.FDPCODE = String.Empty
                        'End If
                        If fd11prop.FASSPRCA IsNot Nothing Then
                            pendingUpdate.FASSPRCA = fd11prop.FASSPRCA
                        Else
                            pendingUpdate.FASSPRCA = Nothing
                        End If
                        If fd11prop.FPCINST <> String.Empty Then
                            pendingUpdate.FPCINST = fd11prop.FPCINST
                        Else
                            pendingUpdate.FPCINST = Nothing
                        End If
                        If fd11prop.FPCNOTE <> String.Empty Then
                            pendingUpdate.FPCNOTE = fd11prop.FPCNOTE
                        Else
                            pendingUpdate.FPCNOTE = Nothing
                        End If
                        If fd11prop.FAREAOUT IsNot Nothing Then
                            pendingUpdate.FAREAOUT = fd11prop.FAREAOUT
                        Else
                            pendingUpdate.FAREAOUT = Nothing
                        End If
                        If fd11prop.FAREAIN IsNot Nothing Then
                            pendingUpdate.FAREAIN = fd11prop.FAREAIN
                        Else
                            pendingUpdate.FAREAIN = Nothing
                        End If
                        If fd11prop.FAREAPARK IsNot Nothing Then
                            pendingUpdate.FAREAPARK = fd11prop.FAREAPARK
                        Else
                            pendingUpdate.FAREAPARK = Nothing
                        End If
                        If fd11prop.FPWIEGHAREA IsNot Nothing Then
                            pendingUpdate.FPWIEGHAREA = fd11prop.FPWIEGHAREA
                        Else
                            pendingUpdate.FPWIEGHAREA = Nothing
                        End If
                        If fd11prop.FPAREAOUT IsNot Nothing Then
                            pendingUpdate.FPAREAOUT = fd11prop.FPAREAOUT
                        Else
                            pendingUpdate.FPAREAOUT = Nothing
                        End If
                        If fd11prop.FPAREAIN IsNot Nothing Then
                            pendingUpdate.FPAREAIN = fd11prop.FPAREAIN
                        Else
                            pendingUpdate.FPAREAIN = Nothing
                        End If
                        If fd11prop.FPAREAPARK IsNot Nothing Then
                            pendingUpdate.FPAREAPARK = fd11prop.FPAREAPARK
                        Else
                            pendingUpdate.FPAREAPARK = Nothing
                        End If
                        If fd11prop.FHBKDATE IsNot Nothing Then
                            pendingUpdate.FHBKDATE = fd11prop.FHBKDATE
                        Else
                            pendingUpdate.FHBKDATE = Nothing
                        End If
                    End If

                    Dim clearED03UNIT As List(Of ED03UNIT) = Await db.ED03UNIT.Where(Function(x) x.FASSETNO = fd11prop.FASSETNO And x.FREPRJNO = fd11prop.FREPRJNO).ToListAsync
                    If clearED03UNIT IsNot Nothing Then
                        For Each m As ED03UNIT In clearED03UNIT
                            m.FASSETNO = Nothing
                        Next
                    End If

                    Dim ED03UNIT As ED03UNIT = Await db.ED03UNIT.Where(Function(x) x.FSERIALNO = fd11prop.FSERNO And x.FREPRJNO = fd11prop.FREPRJNO).FirstOrDefaultAsync
                    If ED03UNIT IsNot Nothing Then
                        ED03UNIT.FASSETNO = fd11prop.FASSETNO
                    End If
                Next
                Await db.SaveChangesAsync()
            Catch ex As DbUpdateConcurrencyException

                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = getLastInnerException(ex)
                Return Json(returnResult)

            End Try
            Return Json(returnResult)
        End Function

        ' POST: api/LandUtilitiesManage
        <ResponseType(GetType(FD11PROP))>
        Async Function PostFD11PROP(ByVal viewModel As TRN_LandUtilitiesManage_ViewModel) As Task(Of IHttpActionResult)
            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try

                If Not ModelState.IsValid Then
                    Return BadRequest(ModelState)
                End If

                If (viewModel.List_FD11PROP(0).FPCPIECE <> "") Then
                    Dim listFpc = getExistsFpcpiece(viewModel.List_FD11PROP.Select(Function(x) If(String.IsNullOrEmpty(x.FPCPIECE), "", x.FPCPIECE)).ToList())
                    If listFpc.Count > 0 Then
                        returnResult.ErrorView.IsError = True
                        returnResult.ErrorView.Message = "โฉนด:" + String.Join(", ", listFpc) + " ซ้ำ"
                        Return Json(returnResult)
                    End If
                End If

                'เอาค่าMax
                Dim FREPRJNO = viewModel.List_FD11PROP(0).FREPRJNO
                Dim pRunning As Integer = 0
                Dim lc As FD11PROP = db.FD11PROP.Where(Function(s) _
                            s.FASSETNO.Substring(0, 5) = FREPRJNO _
                            And s.FASSETNO.Length = 10
                ).OrderByDescending(Function(s) s.FASSETNO).FirstOrDefault
                If lc IsNot Nothing Then
                    pRunning = lc.FASSETNO.Substring(6)
                End If


                'ตอน new Unit ไม่มีค่า
                'Dim listED03 = getExistsED03UNIT(listAssetNo)
                'If (listED03.Count > 0) Then
                '    returnResult.ErrorView.IsError = True
                '    returnResult.ErrorView.Message = "แปลง:" + String.Join(",", listED03) + " ถูกผูกแผนผังแล้ว"
                '    Return Json(returnResult)
                'End If

                'comment เพราะ gen assetno ใหม่ ไม่ต้องเชคซ้ำ
                'Dim listAssetNo = viewModel.List_FD11PROP.Select(Function(x) x.FASSETNO).ToList()
                'Dim listFD = getExistsAssetNo(listAssetNo)
                'If (listFD.Count > 0) Then
                '    returnResult.ErrorView.IsError = True
                '    returnResult.ErrorView.Message = "แปลง:" + String.Join(",", listFD) + " ซ้ำ"
                '    Return Json(returnResult)

                'End If


                If (viewModel.List_FD11PROP IsNot Nothing) Then
                    'set default value
                    For Each i As FD11PROP In viewModel.List_FD11PROP
                        pRunning += 1
                        i.FASSETNO = i.FREPRJNO + " " + pRunning.ToString.PadLeft(4, "0")
                        i.FINSAMT = 0
                        i.FINSPREM = 0
                        i.FTOTAGE = 0
                        i.FNOMONTH = 0
                        i.FOUTMONTH = 0
                        i.FQTYADJ = 0
                        i.FORGCOST = 0
                        i.FTOTCOST = 0
                        i.FVSALVAG = 0
                        i.FVDEPRE = 0
                        i.FVSALVAGAC = 0
                        i.FVDEPREAC = 0
                        i.FLACTST = 31
                        i.FRFINCOST = 0
                        i.FMKPRCU = 0
                        i.FMKPRCA = 0
                        i.FASSPRCU = 0
                        i.FASSPRCDT = Nothing
                        i.FASSCHG = 0
                        i.FENTTYPE = 2
                        Dim ed = db.ED03UNIT.Where(Function(x) x.FSERIALNO = i.FSERNO And x.FREPRJNO = i.FREPRJNO).FirstOrDefault
                        If (ed IsNot Nothing) Then
                            ed.FASSETNO = i.FASSETNO
                        End If
                    Next
                    db.FD11PROP.AddRange(viewModel.List_FD11PROP)

                    Await db.SaveChangesAsync()
                End If


                returnResult.Datas.Data1 = viewModel.List_FD11PROP.OrderBy(Function(x) x.FASSETNO).ToList()


            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = ex.GetBaseException().Message
                returnResult.ErrorView.ErrorObject = ex.GetBaseException()
                returnResult.ErrorView.StackTrace = ex.StackTrace

            End Try
            Return Json(returnResult)
        End Function

        ' DELETE: api/LandUtilitiesManage/5
        <ResponseType(GetType(FD11PROP))>
        Async Function DeleteFD11PROP(ByVal id As String) As Task(Of IHttpActionResult)
            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try
                Dim listid = id.Split("_")

                Dim fD11PROP As List(Of FD11PROP) = Await db.FD11PROP.Where(Function(s) listid.Contains(s.FASSETNO)).ToListAsync()

                If IsNothing(fD11PROP) Then
                    Return NotFound()
                End If
                Dim eD03UNIT As List(Of ED03UNIT) = Await db.ED03UNIT.Where(Function(s) listid.Contains(s.FASSETNO)).ToListAsync()
                If eD03UNIT IsNot Nothing Then
                    For Each ed In eD03UNIT
                        ed.FASSETNO = Nothing
                    Next
                End If


                'db.FD11PROP.Remove(fD11PROP)
                db.FD11PROP.RemoveRange(fD11PROP)
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

        Private Function FD11PROPExists(ByVal id As String) As Boolean
            Return db.FD11PROP.Count(Function(e) e.FASSETNO = id) > 0
        End Function
        Private Function getExistsFpcpiece(ByVal listFpcpiece As List(Of String)) As List(Of String)
            Return db.FD11PROP.Where(Function(e) e.FPCPIECE IsNot Nothing And listFpcpiece.Contains(e.FPCPIECE)).Select(Function(x) x.FPCPIECE).ToList()
        End Function

        Private Function getExistsED03UNIT(ByVal listAssetNo As List(Of String)) As List(Of String)
            Return db.ED03UNIT.Where(Function(e) listAssetNo.Contains(e.FASSETNO)).Select(Function(x) x.FASSETNO).ToList()
        End Function
        Private Function getExistsAssetNo(ByVal listAssetNo As List(Of String)) As List(Of String)
            Return db.FD11PROP.Where(Function(e) listAssetNo.Contains(e.FASSETNO)).Select(Function(x) x.FASSETNO).ToList()
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
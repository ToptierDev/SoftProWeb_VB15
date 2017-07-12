

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
        Public Class ProjectPriceListController
            Inherits System.Web.Http.ApiController

        Private db As New PNSWEBEntities
        Private dbv As New PNSWEB_SoftProEntities

        ' GET: api/ProjectPriceList
        Function GetAD11INV1(ByVal query As String, ByVal culture As String, ByVal mode As String) As Results.JsonResult(Of GeneralViewModels)
            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try
                If (query = "") Then
                    returnResult.Datas.Data1 = dbv.vw_ProjectPriceList_SearchResult
                    Return Json(returnResult)
                Else
                    Dim q = query.Split("_")
                    Dim FREPRJNO = q(0)
                    Dim FREPHASE = q(1)
                    Dim FREZONE = q(2)
                    If (mode.ToUpper() = "SEARCH") Then
                        Dim data = dbv.vw_ProjectPriceList_SearchResult.Where(Function(x) _
                                     (x.FREPRJNO = FREPRJNO Or FREPRJNO = "") _
                                    And (x.FREPHASE = FREPHASE Or FREPHASE = "") _
                                    And (x.FREZONE = FREZONE Or FREZONE = ""))
                        returnResult.Datas.Data1 = data
                    ElseIf (mode.ToUpper() = "NEW") Then

                        Dim viewModel As ProjectProceList_ViewModel = New ProjectProceList_ViewModel()
                        viewModel.ED04RECF = db.ED04RECF.Where(Function(x) (x.FREPRJNO = FREPRJNO) _
                    And (x.FREPHASE = FREPHASE) _
                    And (x.FREZONE = FREZONE)
                    ).FirstOrDefault
                        viewModel.ED11PAJ1 = New ED11PAJ1
                        viewModel.ED11PAJ1.FTRNNO = "TEMPFTRNNO"
                        'viewModel.List_ED11PAJ2 = Await db.ED11PAJ2.Where(Function(x) (x.FTRNNO = FTRNNO)
                        '    ).ToListAsync()

                        If IsNothing(viewModel.ED04RECF) Then
                            viewModel.ED04RECF = New ED04RECF
                            viewModel.ED04RECF.FREPRJNO = FREPRJNO
                            viewModel.ED04RECF.FREPHASE = FREPHASE
                            viewModel.ED04RECF.FREZONE = FREZONE

                        End If


                        viewModel.vw_List_ED11PAJ2 = New List(Of vw_ED11PAJ2_WithDetail)

                            returnResult.Datas.Data1 = viewModel

                        End If
                        Return Json(returnResult)
                End If
            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = getLastInnerException(ex)
            End Try
            Return Json(returnResult)
        End Function


        ' GET: api/ProjectPriceList/5
        '<ResponseType(GetType(LandPurchaseAgreement_ViewModel))>
        Async Function GetAD11INV1(ByVal query As String, ByVal culture As String) As Task(Of Results.JsonResult(Of GeneralViewModels))
            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try


                Dim q = query.Split("_")
                Dim FREPRJNO = q(0)
                Dim FREPHASE = q(1)
                Dim FREZONE = q(2)
                Dim FTRNNO = q(3)
                'New
                'Select Case* From ED04RECF Where FREPRJNO =? And FREPHASE =? And FREZone =?
                'Edit
                '                Select Case* From ED04RECF Where FREPRJNO =? And FREPHASE =? And FREZone =?
                'Select Case* From ED11PAJ1 Where FTRNNO =?  
                'Select Case* From ED11PAJ2 Where FTRNNO =?  

                Dim viewModel As ProjectProceList_ViewModel = New ProjectProceList_ViewModel()
                viewModel.ED04RECF = Await db.ED04RECF.Where(Function(x) (x.FREPRJNO = FREPRJNO Or FREPRJNO = "") _
                    And (x.FREPHASE = FREPHASE Or FREPHASE = "") _
                    And (x.FREZONE = FREZONE Or FREZONE = "")
                    ).FirstOrDefaultAsync()
                viewModel.ED11PAJ1 = Await db.ED11PAJ1.Where(Function(x) (x.FTRNNO = FTRNNO)
                    ).FirstOrDefaultAsync()
                viewModel.vw_List_ED11PAJ2 = Await dbv.vw_ED11PAJ2_WithDetail.Where(Function(x) (x.FTRNNO = FTRNNO Or FTRNNO = "") _
                                                                                          And (x.FREPRJNO = FREPRJNO) _
                                                                                          And (x.FREPHASE = FREPHASE) _
                                                                                          And (x.FREZONE = FREZONE)
                    ).ToListAsync()


                returnResult.Datas.Data1 = viewModel
            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = getLastInnerException(ex)
            End Try

            Return Json(returnResult)
        End Function

        ' PUT: api/LandPurchaseAgreement/5
        '<ResponseType(GetType(Void))>
        Async Function PutAD11INV1(ByVal id As String, ByVal userid As String, ByVal viewModel As ProjectProceList_ViewModel) As Task(Of IHttpActionResult)

            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try

                If Not ModelState.IsValid Then

                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "Bad Request"
                    returnResult.ErrorView.ErrorObject = ModelState
                    Return Json(returnResult)
                End If

                Dim ed04recf As ED04RECF = viewModel.ED04RECF
                Dim ed11paj1 As ED11PAJ1 = viewModel.ED11PAJ1
                Dim listEd11paj2 As List(Of ED11PAJ2) = viewModel.List_ED11PAJ2

                'ed04recf.FDOWNMON = ed11paj1.FMDOWN
                'ed04recf.FPPUBLICA = ed11paj1.FPPUBLICA
                'ed04recf.FPPUBLICB = ed11paj1.FPPUBLICB
                'ed04recf.FPPUBLICC = ed11paj1.FPPUBLICC
                'ed04recf.FPMROADA = ed11paj1.FPMROADA
                'ed04recf.FPMROADB = ed11paj1.FPMROADB
                'ed04recf.FPMROADC = ed11paj1.FPMROADC
                'ed04recf.FAREAUPRC = ed11paj1.FAREAUPRC
                'ed04recf.FPOVERAREA = ed11paj1.FPOVERAREA
                'ed04recf.FPCORNER1 = ed11paj1.FPCORNER1
                'ed04recf.FPCORNER2 = ed11paj1.FPCORNER2
                'ed04recf.FPPUBLICTY = ed11paj1.FPPUBLICTY

                ed04recf.FLASTUPD = Date.Now  'วันเวลาที่ Update
                ed04recf.FUPDBY = userid 'ผู้ Update


                Dim delete_listEd11paj2 = Await db.ED11PAJ2.Where(Function(x) x.FTRNNO = ed11paj1.FTRNNO).ToListAsync()

                db.ED11PAJ2.RemoveRange(delete_listEd11paj2)
                If (Not IsNothing(listEd11paj2)) Then
                    db.ED11PAJ2.AddRange(listEd11paj2)
                End If

                db.Entry(ed04recf).State = Data.EntityState.Modified
                db.Entry(ed11paj1).State = Data.EntityState.Modified
                Await db.SaveChangesAsync()

            Catch exEntity As Entity.Validation.DbEntityValidationException
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = exEntity.EntityValidationErrors(0).ValidationErrors(0).ErrorMessage
                returnResult.ErrorView.StackTrace = exEntity.StackTrace
            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = getLastInnerException(ex)
                returnResult.ErrorView.StackTrace = ex.StackTrace
            End Try
            Return Json(returnResult)
        End Function



        ' PUT: api/LandPurchaseAgreement/5
        '<ResponseType(GetType(Void))>
        Async Function PutAD11INV1ACTION(ByVal id As String, ByVal userid As String, ByVal action As String, ByVal viewModel As ProjectProceList_ViewModel) As Task(Of IHttpActionResult)

            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try

                If Not ModelState.IsValid Then

                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "Bad Request"
                    returnResult.ErrorView.ErrorObject = ModelState
                    Return Json(returnResult)
                End If

                Dim ed11paj1 As ED11PAJ1 = viewModel.ED11PAJ1

                If action = "approve" Then
                    ed11paj1.FMDATE = Date.Now 'วันเวลาที่ APPROVE
                    ed11paj1.FUPDFLAG = "Y" 'สถานะ APPROVE
                ElseIf action = "unapprove" Then
                    ed11paj1.FMDATE = Nothing 'วันเวลาที่ APPROVE
                    ed11paj1.FUPDFLAG = Nothing 'สถานะ APPROVE
                End If

                db.Entry(ed11paj1).State = Data.EntityState.Modified
                Await db.SaveChangesAsync()

            Catch exEntity As Entity.Validation.DbEntityValidationException
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = exEntity.EntityValidationErrors(0).ValidationErrors(0).ErrorMessage
                returnResult.ErrorView.StackTrace = exEntity.StackTrace
            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = getLastInnerException(ex)
                returnResult.ErrorView.StackTrace = ex.StackTrace
            End Try
            Return Json(returnResult)
        End Function

        ' POST: api/ProjectPriceList
        <ResponseType(GetType(ProjectProceList_ViewModel))>
        Async Function PostAD11INV1(ByVal userid As String, ByVal viewModel As ProjectProceList_ViewModel) As Task(Of IHttpActionResult)

            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try

                If Not ModelState.IsValid Then
                    returnResult.ErrorView.IsError = True

                    returnResult.ErrorView.Message = "Validation Fail"
                    returnResult.ErrorView.ErrorObject = ModelState.Values.SelectMany(Function(e) e.Errors.[Select](Function(er) er.ErrorMessage))

                    Return Json(returnResult)
                End If


                Dim sFtrnNO As String = ""
                Dim lst As List(Of ED11PAJ1) = db.ED11PAJ1.Where(Function(s) s.FTRNYEAR = viewModel.ED11PAJ1.FTRNYEAR And s.FTRNCD = viewModel.ED11PAJ1.FTRNCD).OrderByDescending(Function(s) s.FTRNNO).ToList
                Dim lcFtrnNO As ED11PAJ1 = lst.First()
                If lcFtrnNO IsNot Nothing Then
                    'PA1400001
                    sFtrnNO = lcFtrnNO.FTRNCD & viewModel.ED11PAJ1.FTRNYEAR.Substring(2, 2) & String.Format("{0:D5}", CInt(lcFtrnNO.FTRNNO.Substring(4) + 1))
                Else
                    sFtrnNO = lcFtrnNO.FTRNCD & viewModel.ED11PAJ1.FTRNYEAR.Substring(2, 2) & "00001"
                End If

                'set default value
                'viewModel.ED04RECF.FPCONTRACTA = 7
                viewModel.ED04RECF.FPCONTRACTB = 0
                viewModel.ED04RECF.FPCONTRACTC = 0
                viewModel.ED04RECF.FUPDNO = sFtrnNO
                viewModel.ED04RECF.FPCONTRACTB = 0
                viewModel.ED04RECF.FPCONTRACTC = 0
                viewModel.ED04RECF.FYPAYMENT1 = 10
                viewModel.ED04RECF.FYPAYMENT2 = 15
                viewModel.ED04RECF.FYPAYMENT3 = 20
                viewModel.ED04RECF.FYPAYMENT4 = 25
                viewModel.ED04RECF.FMLEFT = 0
                viewModel.ED04RECF.FMTOP = 0
                viewModel.ED04RECF.FMWIDTH = 0
                viewModel.ED04RECF.FMHEIGHT = 0
                viewModel.ED04RECF.FLASTUPD = Date.Now  'วันเวลาที่ Update
                viewModel.ED04RECF.FUPDBY = userid 'ผู้ Update

                viewModel.ED04RECF.FDOWNMON = viewModel.ED11PAJ1.FMDOWN
                viewModel.ED04RECF.FPPUBLICA = viewModel.ED11PAJ1.FPPUBLICA
                viewModel.ED04RECF.FPPUBLICB = viewModel.ED11PAJ1.FPPUBLICB
                viewModel.ED04RECF.FPPUBLICC = viewModel.ED11PAJ1.FPPUBLICC
                viewModel.ED04RECF.FPMROADA = viewModel.ED11PAJ1.FPMROADA
                viewModel.ED04RECF.FPMROADB = viewModel.ED11PAJ1.FPMROADB
                viewModel.ED04RECF.FPMROADC = viewModel.ED11PAJ1.FPMROADC
                viewModel.ED04RECF.FAREAUPRC = viewModel.ED11PAJ1.FAREAUPRC
                viewModel.ED04RECF.FPOVERAREA = viewModel.ED11PAJ1.FPOVERAREA
                viewModel.ED04RECF.FPCORNER1 = viewModel.ED11PAJ1.FPCORNER1
                viewModel.ED04RECF.FPCORNER2 = viewModel.ED11PAJ1.FPCORNER2
                viewModel.ED04RECF.FPPUBLICTY = viewModel.ED11PAJ1.FPPUBLICTY

                viewModel.ED11PAJ1.FDOWNDEC = 0
                viewModel.ED11PAJ1.FPPUBLIC2A = 0
                viewModel.ED11PAJ1.FTRNNO = sFtrnNO

                If (viewModel.List_ED11PAJ2 IsNot Nothing) Then
                    For Each item As ED11PAJ2 In viewModel.List_ED11PAJ2
                        item.FTARGETPRICE = 0
                        item.FTRNNO = sFtrnNO
                    Next
                    db.ED11PAJ2.AddRange(viewModel.List_ED11PAJ2)
                End If
                'db.ED04RECF.Add(viewModel.ED04RECF)

                If db.ED04RECF.Count(Function(e) e.FREPRJNO = viewModel.ED04RECF.FREPRJNO _
                   And e.FREPHASE = viewModel.ED04RECF.FREPHASE _
                   And e.FREZONE = viewModel.ED04RECF.FREZONE
                    ) > 0 Then
                    db.Entry(viewModel.ED04RECF).State = Data.EntityState.Modified

                Else
                    db.ED04RECF.Add(viewModel.ED04RECF)
                End If


                db.ED11PAJ1.Add(viewModel.ED11PAJ1)

                Await db.SaveChangesAsync()


            Catch exEntity As Entity.Validation.DbEntityValidationException
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = exEntity.EntityValidationErrors(0).ValidationErrors(0).ErrorMessage
                returnResult.ErrorView.StackTrace = exEntity.StackTrace
            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = getLastInnerException(ex)
                returnResult.ErrorView.StackTrace = ex.StackTrace
            End Try

            returnResult.Datas.Data1 = viewModel
            Return Json(returnResult)

            'Return CreatedAtRoute("DefaultApi", New With {.id = viewModel.AD11INV1.FVOUNO}, viewModel)
        End Function

        ' DELETE: api/ProjectPriceList/5
        <ResponseType(GetType(AD11INV1))>
            Async Function DeleteAD11INV1(ByVal id As String) As Task(Of IHttpActionResult)

                Dim returnResult As GeneralViewModels = New GeneralViewModels

                Try

                Dim ed11 As ED11PAJ1 = Await db.ED11PAJ1.FindAsync(id)
                Dim deleteQuery As IQueryable(Of ED11PAJ2) = db.ED11PAJ2.Where(Function(x) x.FTRNNO = id)
                If IsNothing(ed11) Then
                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "ไม่พบโฉนด"
                    Return Json(returnResult)
                End If

                db.ED11PAJ2.RemoveRange(deleteQuery)
                db.ED11PAJ1.Remove(ed11)
                Await db.SaveChangesAsync()

                Catch ex As Exception
                    returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = getLastInnerException(ex)
                Return returnResult
            End Try

                returnResult.Datas.Data1 = New With {.id = id}
                Return Json(returnResult)
            End Function

            Protected Overrides Sub Dispose(ByVal disposing As Boolean)
                If (disposing) Then
                    db.Dispose()
                End If
                MyBase.Dispose(disposing)
            End Sub

            Private Function AD11INV1Exists(ByVal id As String) As Boolean
                Return db.AD11INV1.Count(Function(e) e.FVOUNO = id) > 0
            End Function
            Private Function AD01VEN1Exists(ByVal id As String) As Boolean
                Return db.AD01VEN1.Count(Function(e) e.FSUCODE = id) > 0
            End Function

            Private Function FD11PROPExists(ByVal id As String) As Boolean

                Return db.AD11INV1.Count(Function(e) e.FASSETNO = id) > 0
            End Function

            Private Function FD11PROPAvailable(ByVal id As String) As Boolean

                Return db.FD11PROP.Count(Function(e) e.FASSETNO = id) > 0
            End Function
        Private Function AD01VEN1Available(ByVal id As String) As Boolean

            Return db.AD01VEN1.Count(Function(e) e.FSUCODE = id) > 0
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
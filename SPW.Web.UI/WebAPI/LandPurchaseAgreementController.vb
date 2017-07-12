Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Description
Imports System.Web.Http.Results
Imports SPW.DAL

Namespace WebAPI
    Public Class LandPurchaseAgreementController
        Inherits System.Web.Http.ApiController

        Private db As New PNSWEBEntities
        Private dbx As New PNSWEB_SoftProEntities
        ' GET: api/LandPurchaseAgreement
        ' Function GetAD11INV1(ByVal query As String, ByVal culture As String, ByVal mode As String) As IQueryable(Of vw_AD11INV1_SearchResult)
        Function GetAD11INV1(ByVal query As String, ByVal culture As String, ByVal mode As String) As IHttpActionResult
            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try
                If (query = "") Then
                    returnResult.Datas.Data1 = dbx.vw_AD11INV1_SearchResult.ToList()
                Else
                    returnResult.Datas.Data1 = dbx.vw_AD11INV1_SearchResult.Where(Function(x) x.FVOUNO.StartsWith(query) _
                    Or x.FASSETNO.StartsWith(query) _
                    Or x.FPCPIECE.StartsWith(query)).ToList()

                End If
            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = ex.GetBaseException().Message
                returnResult.ErrorView.ErrorObject = ex.GetBaseException()
                returnResult.ErrorView.StackTrace = ex.StackTrace
            End Try
            Return Json(returnResult)
        End Function

        ' GET: api/LandPurchaseAgreement/5
        <ResponseType(GetType(LandPurchaseAgreement_ViewModel))>
        Async Function GetAD11INV1(ByVal id As String, ByVal culture As String) As Task(Of IHttpActionResult)
            Dim aD11INV1 As AD11INV1 = Await db.AD11INV1.FindAsync(id)
            If IsNothing(aD11INV1) Then
                Return NotFound()
            End If
            Dim viewModel As New LandPurchaseAgreement_ViewModel
            viewModel.AD11INV1 = aD11INV1
            viewModel.AD01VEN1 = Await db.AD01VEN1.FindAsync(viewModel.AD11INV1.FSUCODE)
            viewModel.FD11PROP = Await db.FD11PROP.FindAsync(viewModel.AD11INV1.FASSETNO)
            viewModel.List_AD11INV3 = Await db.AD11INV3.Where(Function(x) x.FVOUNO = viewModel.AD11INV1.FVOUNO).ToListAsync()



            Return Ok(viewModel)
        End Function

        ' PUT: api/LandPurchaseAgreement/5
        '<ResponseType(GetType(Void))>
        Async Function PutAD11INV1(ByVal id As String, ByVal viewModel As LandPurchaseAgreement_ViewModel) As Task(Of IHttpActionResult)

            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try

                If Not ModelState.IsValid Then

                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "Bad Request"
                    returnResult.ErrorView.ErrorObject = ModelState
                    Return Json(returnResult)
                End If

                If Not (AD11INV1Exists(id)) Then
                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "Not Found"
                    Return Json(returnResult)
                End If

                Dim aD11INV1 = viewModel.AD11INV1
                If Not id = aD11INV1.FVOUNO Then
                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "Bad Request"
                    Return Json(returnResult)
                End If
                Dim aD11INV3 = viewModel.List_AD11INV3

                Dim deleteList_AD11INV3 = Await db.AD11INV3.Where(Function(x) x.FVOUNO = viewModel.AD11INV1.FVOUNO).ToListAsync()

                db.AD11INV3.RemoveRange(deleteList_AD11INV3)
                db.AD11INV3.AddRange(viewModel.List_AD11INV3)

                db.Entry(aD11INV1).State = Data.EntityState.Modified
                Await db.SaveChangesAsync()

            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = ex.GetBaseException().Message
                returnResult.ErrorView.ErrorObject = ex.GetBaseException()
                returnResult.ErrorView.StackTrace = ex.StackTrace
            End Try
            Return Json(returnResult)
        End Function

        ' POST: api/LandPurchaseAgreement
        <ResponseType(GetType(LandPurchaseAgreement_ViewModel))>
        Async Function PostAD11INV1(ByVal viewModel As LandPurchaseAgreement_ViewModel) As Task(Of IHttpActionResult)

            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Try

                If Not ModelState.IsValid Then
                    Return BadRequest(ModelState)
                End If
                If (AD11INV1Exists(viewModel.AD11INV1.FVOUNO)) Then
                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "VOUNO:" + viewModel.AD11INV1.FVOUNO + " already exists."
                    Return Json(returnResult)
                End If
                If (viewModel.AD11INV1.FASSETNO Is Nothing Or (Not FD11PROPAvailable(viewModel.AD11INV1.FASSETNO))) Then
                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "ASSETNO:" + viewModel.AD11INV1.FASSETNO + " ไม่มีในระบบ"
                    Return Json(returnResult)
                End If
                If (FD11PROPExists(viewModel.AD11INV1.FASSETNO)) Then
                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "ASSETNO:" + viewModel.AD11INV1.FASSETNO + " ผูกสัญญาแล้ว ไม่สามารถทำสัญญาใหม่ได้"
                    Return Json(returnResult)
                End If
                If (viewModel.AD11INV1.FSUCODE Is Nothing Or Not AD01VEN1Available(viewModel.AD11INV1.FSUCODE)) Then
                    returnResult.ErrorView.IsError = True
                    returnResult.ErrorView.Message = "SUCODE:" + viewModel.AD11INV1.FSUCODE + " ไม่มีในระบบ"
                    Return Json(returnResult)
                End If



                'set default value
                viewModel.AD11INV1.FVOUTYPE = "02"
                viewModel.AD11INV1.FAPTYPE = 1
                viewModel.AD11INV1.FDISCOUNT = 0
                viewModel.AD11INV1.FAVALUE = 0
                viewModel.AD11INV1.FPVAT = 0
                viewModel.AD11INV1.FAVAT = 0
                viewModel.AD11INV1.FCOMPLETE = 0
                viewModel.AD11INV1.FEXRATE = 0
                viewModel.AD11INV1.FPWTAX = 0
                viewModel.AD11INV1.FADVAMT = 0
                viewModel.AD11INV1.FCASHRET = 0
                If (viewModel.List_AD11INV3 IsNot Nothing) Then

                    For Each item As AD11INV3 In viewModel.List_AD11INV3
                        item.FPVAT = 0
                        item.FAVAT = 0
                    Next
                    db.AD11INV3.AddRange(viewModel.List_AD11INV3)
                End If
                db.AD11INV1.Add(viewModel.AD11INV1)

                Await db.SaveChangesAsync()


            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = ex.GetBaseException().Message
                returnResult.ErrorView.ErrorObject = ex.GetBaseException()
                returnResult.ErrorView.StackTrace = ex.StackTrace

            End Try


            returnResult.Datas.Data1 = New With {.id = viewModel.AD11INV1.FVOUNO}
            Return Json(returnResult)

            'Return CreatedAtRoute("DefaultApi", New With {.id = viewModel.AD11INV1.FVOUNO}, viewModel)
        End Function

        ' DELETE: api/LandPurchaseAgreement/5
        <ResponseType(GetType(AD11INV1))>
        Async Function DeleteAD11INV1(ByVal id As String) As Task(Of IHttpActionResult)

            Dim returnResult As GeneralViewModels = New GeneralViewModels

            Try

                Dim aD11INV1 As AD11INV1 = Await db.AD11INV1.FindAsync(id)
                Dim deleteQuery As IQueryable(Of AD11INV3) = db.AD11INV3.Where(Function(x) x.FVOUNO = id)
                If IsNothing(aD11INV1) Then
                    Return NotFound()
                End If

                db.AD11INV3.RemoveRange(deleteQuery)
                db.AD11INV1.Remove(aD11INV1)
                Await db.SaveChangesAsync()

            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = ex.GetBaseException().Message
                returnResult.ErrorView.ErrorObject = ex.GetBaseException()
            End Try

            returnResult.Datas.Data1 = New With {.id = id}
            Return Json(returnResult)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
                dbx.Dispose()
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
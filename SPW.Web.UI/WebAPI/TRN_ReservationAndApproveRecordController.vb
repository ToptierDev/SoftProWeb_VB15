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
    Public Class TRN_ReservationAndApproveRecordController
        Inherits System.Web.Http.ApiController

        Private db As New PNSWEBEntities

        ' GET: api/TRN_ReservationAndApproveRecord
        Function GetOD11BKT1() As Results.JsonResult(Of GeneralViewModels)
            Dim returnResult As GeneralViewModels = New GeneralViewModels
            Dim viewModel As New TRN_ReservationAndApproveRecord_ViewModel
            Try
                'SL01
                'รหัสผู้ขาย
                viewModel.OD50RCVD = db.OD50RCVD.FirstOrDefault()
                'SL02
                viewModel.OD11BKT1 = db.OD11BKT1.FirstOrDefault()
                viewModel.List_OD21LAGD = db.OD21LAGD.Take(2).ToList()
                viewModel.List_OD21LAGD2 = db.OD21LAGD2.Take(2).ToList()
                viewModel.List_OD21LAPM = db.OD21LAPM.Take(2).ToList()
                'SL03
                viewModel.BD24CRRG = db.BD24CRRG.FirstOrDefault()
                viewModel.List_RD26ORRG = db.RD26ORRG.Take(2).ToList()
                viewModel.List_REPRINTLOG = db.REPRINTLOGs.Take(2).ToList()



                returnResult.Datas.Data1 = viewModel
            Catch ex As Exception
                returnResult.ErrorView.IsError = True
                returnResult.ErrorView.Message = ex.GetBaseException.Message
            End Try

            Return Json(returnResult)
        End Function

        ' GET: api/TRN_ReservationAndApproveRecord/5
        <ResponseType(GetType(OD11BKT1))>
        Async Function GetOD11BKT1(ByVal id As String) As Task(Of IHttpActionResult)
            Dim oD11BKT1 As OD11BKT1 = Await db.OD11BKT1.FindAsync(id)
            If IsNothing(oD11BKT1) Then
                Return NotFound()
            End If

            Return Ok(oD11BKT1)
        End Function

        ' PUT: api/TRN_ReservationAndApproveRecord/5
        <ResponseType(GetType(Void))>
        Async Function PutOD11BKT1(ByVal id As String, ByVal oD11BKT1 As OD11BKT1) As Task(Of IHttpActionResult)
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = oD11BKT1.FTRNNO Then
                Return BadRequest()
            End If

            db.Entry(oD11BKT1).State = Entity.EntityState.Modified

            Try
                Await db.SaveChangesAsync()
            Catch ex As DbUpdateConcurrencyException
                If Not (OD11BKT1Exists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/TRN_ReservationAndApproveRecord
        <ResponseType(GetType(OD11BKT1))>
        Async Function PostOD11BKT1(ByVal oD11BKT1 As OD11BKT1) As Task(Of IHttpActionResult)
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.OD11BKT1.Add(oD11BKT1)

            Try
                Await db.SaveChangesAsync()
            Catch ex As DbUpdateException
                If (OD11BKT1Exists(oD11BKT1.FTRNNO)) Then
                    Return Conflict()
                Else
                    Throw
                End If
            End Try

            Return CreatedAtRoute("DefaultApi", New With {.id = oD11BKT1.FTRNNO}, oD11BKT1)
        End Function

        ' DELETE: api/TRN_ReservationAndApproveRecord/5
        <ResponseType(GetType(OD11BKT1))>
        Async Function DeleteOD11BKT1(ByVal id As String) As Task(Of IHttpActionResult)
            Dim oD11BKT1 As OD11BKT1 = Await db.OD11BKT1.FindAsync(id)
            If IsNothing(oD11BKT1) Then
                Return NotFound()
            End If

            db.OD11BKT1.Remove(oD11BKT1)
            Await db.SaveChangesAsync()

            Return Ok(oD11BKT1)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function OD11BKT1Exists(ByVal id As String) As Boolean
            Return db.OD11BKT1.Count(Function(e) e.FTRNNO = id) > 0
        End Function
    End Class
End Namespace
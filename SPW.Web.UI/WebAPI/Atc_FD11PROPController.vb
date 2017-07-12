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
    Public Class Atc_FD11PROPController
        Inherits System.Web.Http.ApiController

        Private db As New PNSWEBEntities

        ' GET: api/AD01VEN1
        Function GetAD01VEN1() As IQueryable(Of FD11PROP)

            '   Return db.FD11PROP _
            '.Where(Function(x) x.FASSETNO <> db.AD11INV1.Any(Function(y) y.FASSETNO = x.FASSETNO))

            Return db.FD11PROP.Where(Function(f) Not db.AD11INV1.[Select](Function(a) a.FASSETNO).Contains(f.FASSETNO)).[Select](Function(f) f)

            '   Return db.FD11PROP.Where(Function(x) x.FENTTYPE = 1 And x.FASSETNO)
        End Function

        ' GET: api/AD01VEN1/5
        <ResponseType(GetType(FD11PROP))>
        Async Function GetAD01VEN1(ByVal id As String) As Task(Of IHttpActionResult)
            Dim aD01VEN1 As FD11PROP = Await db.FD11PROP.FindAsync(id)
            If IsNothing(aD01VEN1) Then
                Return NotFound()
            End If

            Return Ok(aD01VEN1)
        End Function

        ' PUT: api/AD01VEN1/5
        <ResponseType(GetType(Void))>
        Async Function PutAD01VEN1(ByVal id As String, ByVal aD01VEN1 As FD11PROP) As Task(Of IHttpActionResult)
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = aD01VEN1.FSUCODE Then
                Return BadRequest()
            End If

            db.Entry(aD01VEN1).State = Data.EntityState.Modified

            Try
                Await db.SaveChangesAsync()
            Catch ex As DbUpdateConcurrencyException
                If Not (AD01VEN1Exists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/AD01VEN1
        <ResponseType(GetType(FD11PROP))>
        Async Function PostAD01VEN1(ByVal aD01VEN1 As FD11PROP) As Task(Of IHttpActionResult)
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.FD11PROP.Add(aD01VEN1)

            Try
                Await db.SaveChangesAsync()
            Catch ex As DbUpdateException
                If (AD01VEN1Exists(aD01VEN1.FSUCODE)) Then
                    Return Conflict()
                Else
                    Throw
                End If
            End Try

            Return CreatedAtRoute("DefaultApi", New With {.id = aD01VEN1.FSUCODE}, aD01VEN1)
        End Function

        ' DELETE: api/AD01VEN1/5
        <ResponseType(GetType(FD11PROP))>
        Async Function DeleteAD01VEN1(ByVal id As String) As Task(Of IHttpActionResult)
            Dim aD01VEN1 As FD11PROP = Await db.FD11PROP.FindAsync(id)
            If IsNothing(aD01VEN1) Then
                Return NotFound()
            End If

            db.FD11PROP.Remove(aD01VEN1)
            Await db.SaveChangesAsync()

            Return Ok(aD01VEN1)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function AD01VEN1Exists(ByVal id As String) As Boolean
            Return db.AD01VEN1.Count(Function(e) e.FSUCODE = id) > 0
        End Function
    End Class
End Namespace
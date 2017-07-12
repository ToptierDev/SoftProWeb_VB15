Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cSD02GODN

#Region "PRD_ProductStore.aspx"
    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal strUserID As String) As List(Of SD02GODN)

        Using db As New PNSWEBEntities
            Dim lc As List(Of SD02GODN) = db.SD02GODN.OrderBy(Function(s) s.FGDCODE).ToList
            Return lc.ToList
        End Using

    End Function

    Public Function GetByID(ByVal id As String,
                            ByVal strUserID As String) As SD02GODN

        Using db As New PNSWEBEntities
            Dim lc As SD02GODN = db.SD02GODN.Where(Function(s) s.FGDCODE = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function Delete(ByVal id As String,
                           ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As SD02GODN = db.SD02GODN.Where(Function(s) s.FGDCODE = id).SingleOrDefault
                db.SD02GODN.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Edit(ByVal pCode As String,
                         ByVal pDescription As String,
                         ByVal pShortName As String,
                         ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As SD02GODN = db.SD02GODN.Where(Function(s) s.FGDCODE = pCode).SingleOrDefault
            If lc IsNot Nothing Then
                lc.FDESC = pDescription
                lc.FSHDESC = pShortName
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Add(ByVal pCode As String,
                        ByVal pDescription As String,
                        ByVal pShortName As String,
                        ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As SD02GODN = New SD02GODN
            Dim pID As String = String.Empty
            Dim lcMax As SD02GODN = db.SD02GODN.Where(Function(s) s.FGDCODE <> "99").OrderByDescending(Function(s) CInt(s.FGDCODE)).FirstOrDefault
            If lcMax IsNot Nothing Then
                pID = CInt(lcMax.FGDCODE) + 1
            End If
            lc.FGDCODE = pID
            lc.FDESC = pDescription
            lc.FSHDESC = pShortName

            db.SD02GODN.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function getUsedMaster() As List(Of String)

        Using db As New PNSWEBEntities
            Dim query = (From a In db.ED01PROJ Select a.FGDCODE) _
                        .Where(Function(s) s <> "").Distinct.ToList


            Return query
        End Using
    End Function
#End Region

End Class

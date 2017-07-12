Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cZTypeHouseCons

#Region "ORG_CreateType.aspx"
    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal strUserID As String) As List(Of ZTypeHouseCon)

        Using db As New PNSWEBEntities
            Dim lc As List(Of ZTypeHouseCon) = db.ZTypeHouseCons.OrderBy(Function(s) s.TypeHouseConsCode).ToList
            Return lc.ToList
        End Using

    End Function

    Public Function GetByID(ByVal id As String,
                            ByVal strUserID As String) As ZTypeHouseCon

        Using db As New PNSWEBEntities
            Dim lc As ZTypeHouseCon = db.ZTypeHouseCons.Where(Function(s) s.TypeHouseConsCode = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function Delete(ByVal id As String,
                           ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As ZTypeHouseCon = db.ZTypeHouseCons.Where(Function(s) s.TypeHouseConsCode = id).SingleOrDefault
                db.ZTypeHouseCons.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Edit(ByVal pCode As String,
                         ByVal pDescription As String,
                         ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As ZTypeHouseCon = db.ZTypeHouseCons.Where(Function(s) s.TypeHouseConsCode = pCode).SingleOrDefault
            If lc IsNot Nothing Then
                lc.TypeHouseConsDesc = pDescription
                lc.UpdateBy = pUserId
                lc.UpdateDate = DateTime.Now
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Add(ByVal pCode As String,
                        ByVal pDescription As String,
                        ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As ZTypeHouseCon = New ZTypeHouseCon
            lc.TypeHouseConsCode = pCode
            lc.TypeHouseConsDesc = pDescription
            lc.UpdateBy = pUserId
            lc.UpdateDate = DateTime.Now

            db.ZTypeHouseCons.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function getUsedMaster() As List(Of String)

        Using db As New PNSWEBEntities
            Dim query = (From a In db.SD05PDDS Select a.FDUTYCODE) _
                        .Where(Function(s) s <> "").ToList


            Return query
        End Using
    End Function
#End Region

End Class

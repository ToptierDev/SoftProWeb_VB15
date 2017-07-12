Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cTypeHouse

#Region "ORG_TypeHouse.aspx"
    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal strUserID As String) As List(Of vw_TypeHouse_Join)

        Using db As New PNSWEB_SoftProEntities
            Dim lc As List(Of vw_TypeHouse_Join) = db.vw_TypeHouse_Join.OrderBy(Function(s) s.FTYCODE).ToList
            Return lc.ToList
        End Using
    End Function

    Public Function GetTypeByID(ByVal id As String,
                                ByVal strUserID As String) As SD03TYPE

        Using db As New PNSWEBEntities
            Dim lc As SD03TYPE = db.SD03TYPE.Where(Function(s) s.FTYCODE = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function TypeDelete(ByVal id As String,
                               ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As SD03TYPE = db.SD03TYPE.Where(Function(s) s.FTYCODE = id).SingleOrDefault
                db.SD03TYPE.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TypesEdit(ByVal pTypeCode As String,
                              ByVal pDescription As String,
                              ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As SD03TYPE = db.SD03TYPE.Where(Function(s) s.FTYCODE = pTypeCode).SingleOrDefault
            If lc IsNot Nothing Then
                lc.FDESC = pDescription
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function TypeAdd(ByVal pTypeCode As String,
                            ByVal pDescription As String,
                            ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As SD03TYPE = New SD03TYPE
            lc.FENTTYPE = "1"
            lc.FTYCODE = pTypeCode
            lc.FDESC = pDescription

            db.SD03TYPE.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function
#End Region

End Class

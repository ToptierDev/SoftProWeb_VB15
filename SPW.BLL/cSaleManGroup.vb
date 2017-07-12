Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cSaleManGroup

#Region "MST_SaleManGroup.aspx"
    Public Function LoaddataSalesManGroup(ByVal fillter As FillterSearch,
                                       ByRef TotalRow As Integer,
                                        ByVal strUserID As String) As List(Of LD08SMCT)

        Using db As New PNSWEBEntities
            Dim lc As List(Of LD08SMCT) = db.LD08SMCT.OrderBy(Function(s) s.FSMTYPE).ToList
            Return lc.ToList
        End Using

    End Function

    Public Function GetSalesManGroupByID(ByVal id As String,
                                        ByVal strUserID As String) As LD08SMCT

        Using db As New PNSWEBEntities
            Dim lc As LD08SMCT = db.LD08SMCT.Where(Function(s) s.FSMTYPE = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function SalesManGroupDelete(ByVal id As String,
                                        ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As LD08SMCT = db.LD08SMCT.Where(Function(s) s.FSMTYPE = id).SingleOrDefault
                db.LD08SMCT.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SalesManGroupsEdit(ByVal pTypeCode As String,
                                       ByVal pDescription As String,
                                       ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As LD08SMCT = db.LD08SMCT.Where(Function(s) s.FSMTYPE = pTypeCode).SingleOrDefault
            If lc IsNot Nothing Then
                lc.FSMTYPEDS = pDescription
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function SalesManGroupAdd(ByVal pTypeCode As String,
                                     ByVal pDescription As String,
                                     ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As LD08SMCT = New LD08SMCT
            lc.FSMTYPE = pTypeCode
            lc.FSMTYPEDS = pDescription

            db.LD08SMCT.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function getUsedMaster() As List(Of String)

        Using db As New PNSWEBEntities
            Dim query = (From a In db.LD01SMAN Select a.FSMTYPE) _
                        .Where(Function(s) s <> "").Distinct.ToList


            Return query
        End Using
    End Function
#End Region

End Class

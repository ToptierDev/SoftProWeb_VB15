Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cModule

#Region "MST_Module.aspx"
    Public Function Loaddata(ByVal fillter As FillterSearch,
                            ByRef TotalRow As Integer,
                            ByVal strUserID As String) As List(Of CoreModule)
        Using db As New PNSDBWEBEntities
            Dim lc As List(Of CoreModule) = db.CoreModules.OrderBy(Function(s) s.ModuleID).ToList
            Return lc.ToList
        End Using
    End Function

    Public Function GetByID(ByVal id As String,
                            ByVal strUserID As String) As CoreModule

        Using db As New PNSDBWEBEntities
            Dim lc As CoreModule = db.CoreModules.Where(Function(s) s.ModuleID = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function LoadSection(ByVal pCode As String) As List(Of CoreModule)

        Using db As New PNSDBWEBEntities
            Dim lc As List(Of CoreModule) = db.CoreModules.Where(Function(s) s.ModuleID = pCode).ToList
            Return lc.ToList
        End Using

    End Function

    Public Function Delete(ByVal id As String,
                            ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSDBWEBEntities
                Dim lc As CoreModule = db.CoreModules.Where(Function(s) s.ModuleID = id).SingleOrDefault
                db.CoreModules.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Edit(ByVal pCode As String,
                         ByVal pDescription As String,
                         ByVal pDescriptionT As String,
                         ByVal pUserId As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim lc As CoreModule = db.CoreModules.Where(Function(s) s.ModuleID = pCode).SingleOrDefault
            If lc IsNot Nothing Then
                lc.ModuleNameEN = pDescription
                lc.ModuleNameTH = pDescriptionT

            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Add(ByVal pCode As String,
                        ByVal pDescription As String,
                        ByVal pDescriptionT As String,
                        ByVal pUserId As String) As Boolean

        Using db As New PNSDBWEBEntities
            Dim lc As CoreModule = New CoreModule
            lc.ModuleID = pCode
            lc.ModuleNameEN = pDescription
            lc.ModuleNameTH = pDescriptionT

            db.CoreModules.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function
#End Region

End Class

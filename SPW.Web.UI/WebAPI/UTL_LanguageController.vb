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
Public Class UTL_LanguageController
    Inherits ApiController

    Private db As New PNSDBWEBEntities
    ' GET api/<controller>
    Public Function GetValues(ByVal query As String, ByVal mode As String) As Results.JsonResult(Of GeneralViewModels)
        Dim returnResult As GeneralViewModels = New GeneralViewModels
        mode = mode.ToLower
        Try
            If mode.ToLower = "search" Then
                Dim q = query.Split("_")
                Dim txtResourceName = q(0)
                Dim txtResourceValueLC = q(1)
                Dim txtResourceValueEN = q(2)
                Dim txtResourceType = q(3)
                Dim txtMenuID = q(4)
                Dim MenuID = 0
                If txtMenuID <> "" Then
                    MenuID = Integer.Parse(txtMenuID)
                End If

                returnResult.Datas.Data1 = db.CoreWebResources.Where(Function(x) ((txtMenuID = "" Or x.MenuID = 99999 Or x.MenuID = MenuID) _
                                                       And (x.ResourceName = txtResourceName Or txtResourceName = "") _
                                                       And (x.ResourceValueLC = txtResourceValueLC Or txtResourceValueLC = "") _
                                                       And (x.ResourceValueEN = txtResourceValueEN Or txtResourceValueEN = "") _
                                                       And (x.ResourceType = txtResourceType Or txtResourceType = "")
                                                        )).OrderBy(Function(x) x.MenuID).ToList

            ElseIf mode.ToLower = "notset" Then
                    Dim c = db.CoreWebResources.Where(Function(x) x.MenuID = 99999).Select(Function(x) x.ResourceName)
                Dim e = db.CoreErrorLogs.Where(Function(x) x.ErrorFunction = "GetResource").Select(Function(x) x.ErrorDescription).Distinct
                Dim diff = e.Except(c).OrderBy(Function(x) x).ToList
                Dim listRes As New List(Of CoreWebResource)
                For Each s As String In diff
                    listRes.Add(New CoreWebResource With {.ResourceName = s, .ResourceType = "Text", .MenuID = "99999"})
                Next
                returnResult.Datas.Data1 = listRes

            End If

        Catch ex As Exception
            returnResult.ErrorView.IsError = True
            returnResult.ErrorView.Message = ex.GetBaseException.Message
        End Try
        Return Json(returnResult)
    End Function

    ' GET api/<controller>/5
    Public Function GetValue(ByVal id As Integer) As String
        Return "value"
    End Function

    ' POST api/<controller>
    Public Function PostValue(ByVal viewModel As List(Of CoreWebResource)) As Results.JsonResult(Of GeneralViewModels)
        Dim returnResult As GeneralViewModels = New GeneralViewModels

        Try
            db.CoreWebResources.AddRange(viewModel)
            db.SaveChanges()
        Catch ex As Exception
            returnResult.ErrorView.IsError = True
            returnResult.ErrorView.Message = ex.GetBaseException.Message

        End Try
        Return Json(returnResult)
    End Function

    ' PUT api/<controller>/5
    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE api/<controller>/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub
End Class

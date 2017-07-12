Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Globalization
Imports System.Threading
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class GridViewPaging
    Inherits System.Web.UI.UserControl

    Public pagingClickArgs As EventHandler

    Public SelectedIndexChanged As EventHandler
    Public Width As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        Try
            trErrorMessage.Visible = False
            If Not IsPostBack Then
                hddCurrentPage.Value = "1"
                SelectedPageNo.Text = "1"
                GetPageDisplaySummary()
            End If
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Public Sub LoadPaging()

        If hddCurrentPage.Value = "0" Then
            hddCurrentPage.Value = "1"
            SelectedPageNo.Text = "1"
        End If
        If hddCurrentPage.Value = "1" Then
            hddCurrentPage.Value = "1"
            SelectedPageNo.Text = "1"
        End If
        GetPageDisplaySummary()
        tblpagging.Width = Width
    End Sub

    Protected Sub First_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not IsValid() Then
                Return
            End If


            hddCurrentPage.Value = "1"
            SelectedPageNo.Text = "1"
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Public Sub Previous_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not IsValid() Then
                Return
            End If


            If Convert.ToInt32(hddCurrentPage.Value) > 1 Then
                hddCurrentPage.Value = (Convert.ToInt32(hddCurrentPage.Value) - 1).ToString()
                SelectedPageNo.Text = (Convert.ToInt32(hddCurrentPage.Value)).ToString()
            End If
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Protected Sub SelectedPageNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not IsValid() Then
                Return
            End If


            Dim hddCurrentPageNo As Integer = Convert.ToInt32(hddCurrentPage.Value)
            If hddCurrentPageNo < GetTotalPagesCount() Then
                hddCurrentPage.Value = (hddCurrentPageNo).ToString()
            End If
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Protected Sub Next_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not IsValid() Then
                Return
            End If


            Dim hddCurrentPageNo As Integer = Convert.ToInt32(hddCurrentPage.Value)
            If hddCurrentPageNo < GetTotalPagesCount() Then
                hddCurrentPage.Value = (hddCurrentPageNo + 1).ToString()
                SelectedPageNo.Text = (hddCurrentPageNo + 1).ToString()
            End If
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Protected Sub GO_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not IsValidTextBox() Then
                Return
            End If
            Dim hddCurrentPageNo As Integer = Convert.ToInt32(SelectedPageNo.Text)
            If hddCurrentPageNo < 0 Then

                hddCurrentPage.Value = "1"
                SelectedPageNo.Text = "1"
            End If
            If hddCurrentPageNo < GetTotalPagesCount() Then
                hddCurrentPage.Value = (hddCurrentPageNo).ToString()
                SelectedPageNo.Text = (hddCurrentPageNo).ToString()
            Else
                hddCurrentPage.Value = (GetTotalPagesCount()).ToString()
                SelectedPageNo.Text = (GetTotalPagesCount()).ToString()
            End If
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Protected Sub Last_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Not IsValid() Then
                Return
            End If


            hddCurrentPage.Value = GetTotalPagesCount().ToString()
            SelectedPageNo.Text = GetTotalPagesCount().ToString()
            GetPageDisplaySummary()
            pagingClickArgs(sender, e)
        Catch ex As Exception
            ShowGridViewPagingErrorMessage(ex.Message.ToString())
        End Try
    End Sub
    Private Function GetTotalPagesCount() As Integer
        Try
            If PageRowSize.SelectedValue <> "0" Then
                Dim totalPages As Integer = Math.Ceiling(Convert.ToInt32(hddTotalRows.Value) / Convert.ToInt32(PageRowSize.SelectedValue))

                Return totalPages
            Else
                Return 1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub GetPageDisplaySummary()
        Try
            GO.Text = GetWebMessage("msg_pagegotopage", "MSG", "1")
            lblPageSize.Text = GetWebMessage("msg_pagesize", "MSG", "1")
            Dim totalRecords As Integer = Convert.ToInt32(hddTotalRows.Value)
            If totalRecords <> 0 Then
                PageDisplaySummary.Text = Me.GetWebMessage("msg_page", "MSG", "1") + hddCurrentPage.Value.ToString() + Me.GetWebMessage("msg_pageof", "MSG", "1") + GetTotalPagesCount().ToString()

                Dim startRow As Integer = (Convert.ToInt32(PageRowSize.SelectedValue) * (Convert.ToInt32(hddCurrentPage.Value) - 1)) + 1
                Dim endRow As Integer = Convert.ToInt32(PageRowSize.SelectedValue) * Convert.ToInt32(hddCurrentPage.Value)
                If PageRowSize.SelectedValue = "0" Then
                    endRow = totalRecords
                End If

                If totalRecords >= endRow Then
                    RecordDisplaySummary.Text = Me.GetWebMessage("msg_pagerecord", "MSG", "1") + startRow.ToString() + " - " + endRow.ToString() + Me.GetWebMessage("msg_pagerecordof", "MSG", "1") + totalRecords.ToString()
                Else
                    RecordDisplaySummary.Text = Me.GetWebMessage("msg_pagerecord", "MSG", "1") + startRow.ToString() + " - " + totalRecords.ToString() + Me.GetWebMessage("msg_pagerecordof", "MSG", "1") + totalRecords.ToString()
                End If
            Else
                PageDisplaySummary.Text = Me.GetWebMessage("msg_page", "MSG", "1") + "0" + Me.GetWebMessage("msg_pageof", "MSG", "1") + "0"
                RecordDisplaySummary.Text = Me.GetWebMessage("msg_pagerecord", "MSG", "1") + "0" + " - " + "0" + Me.GetWebMessage("msg_pagerecordof", "MSG", "1") + totalRecords.ToString()
                SelectedPageNo.Text = "0"
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function IsValidTextBox() As Boolean
        Try
            Dim totalRecords As Integer = Convert.ToInt32(hddTotalRows.Value)
            If totalRecords = 0 Then
                Return False
            End If
            If [String].IsNullOrEmpty(SelectedPageNo.Text.Trim()) OrElse (SelectedPageNo.Text = "0") Then
                SelectedPageNo.Text = "1"
                Return True
            ElseIf Not IsNumeric(SelectedPageNo.Text) Then
                ShowGridViewPagingErrorMessage("Please Insert Valid Page No.")
                Return False
            Else
                Return True
            End If
        Catch generatedExceptionName As FormatException
            Return False
        End Try
    End Function
    Private Function IsValid() As Boolean
        Try
            Dim totalRecords As Integer = Convert.ToInt32(hddTotalRows.Value)
            If totalRecords = 0 Then
                Return False
            End If
            If PageRowSize.SelectedValue = "0" Then
                Return False
            End If
            If [String].IsNullOrEmpty(hddCurrentPage.Value.Trim()) OrElse (hddCurrentPage.Value = "0") Then
                hddCurrentPage.Value = "1"
                SelectedPageNo.Text = "1"
                Return False
            ElseIf Not IsNumeric(hddCurrentPage.Value) Then
                ShowGridViewPagingErrorMessage("Please Insert Valid Page No.")
                Return False
            Else
                Return True
            End If
        Catch generatedExceptionName As FormatException
            Return False
        End Try
    End Function
    Private Function IsNumeric(ByVal PageNo As String) As Boolean
        Try
            Dim i As Integer = Convert.ToInt32(PageNo)
            Return True
        Catch generatedExceptionName As FormatException
            Return False
        End Try
    End Function
    Private Sub ShowGridViewPagingErrorMessage(ByVal msg As String)
        trErrorMessage.Visible = True
        GridViewPagingError.Text = Convert.ToString("Error: ") & msg
    End Sub

    Protected Sub PageRowSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles PageRowSize.SelectedIndexChanged
        SelectedIndexChanged(sender, e)
    End Sub

    Public Function GetWebMessage(ByVal messageName As String, _
                                  ByVal messageType As String, _
                                  ByVal messageMenuID As String) As String
        Dim ctx As PRREntities = PRREntities.Context
        Dim resource As CoreWebResource = ctx.CoreWebResources.Where(Function(s) s.ResourceName.ToLower() = messageName.ToLower() And s.ResourceType.ToLower() = messageType.ToLower() And s.MenuID = messageMenuID).SingleOrDefault
        If resource IsNot Nothing Then
            If WebCulture() IsNot Nothing Then
                If WebCulture.ToLower = "th" Then
                    If Not IsDBNull(resource.RESOURCEVALUELC) Then
                        Return resource.RESOURCEVALUELC
                    End If
                ElseIf WebCulture.ToLower = "en" Then
                    If Not IsDBNull(resource.RESOURCEVALUELC) Then
                        Return resource.RESOURCEVALUEEN
                    End If
                End If
            Else
                If Not IsDBNull(resource.RESOURCEVALUELC) Then
                    Return resource.RESOURCEVALUELC
                End If
            End If
        Else
            Throw New Exception(String.Format("No resource name [{0}]", messageName))
        End If
        Return String.Empty
    End Function

    Public Function WebCulture() As String
        If Session("PRR.application.language") Is Nothing Then
            Session.Add("PRR.application.language", "th")
        End If
        Return Session("PRR.application.language").ToString
    End Function

End Class
Imports System.Web
Imports System.Data
Imports System.Data.OleDb
Public Class ClsCustomer_Information
    Dim DBAccess As New DBAccess
    Public gConn As New ClsOleDb
    Dim sql As String = ""

    Public Function getStatusContCode(ByVal pFcontcode As String) As DataSet
        Dim ds As New DataSet

        ds = DBAccess.GetFStatusContCode(pFcontcode)

        Return ds
    End Function

    Public Function getOd50rcvd(ByVal pYear As String) As String
        Dim dt As New DataTable
        Dim _FCONTCODE As String = ""
        sql = "  select top 1 * from od50rcvd where FCONTCODE Like '" & pYear & "%' order by FCONTCODE desc   "

        dt = DBAccess.GetDataTable(sql, "GetData")
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                _FCONTCODE = dt.Rows(0)("FCONTCODE").ToString
            End If
        End If
        Return _FCONTCODE
    End Function


    'Select Case top 1 * from od50rcvd where FCONTCODE Like '18%' order by FCONTCODE desc
End Class

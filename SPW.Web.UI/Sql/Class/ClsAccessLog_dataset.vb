Imports System.Web
Imports System.Data
Imports System.Data.OleDb
Public Class ClsAccessLog_dataset
    Dim DBAccess As New DBAccess
    Public gConn As New ClsOleDb
    Public Function AccessLogs(ByVal pMenu As String, _
                               ByVal pSubMenu As String, _
                               ByVal pUserID As String, _
                               ByVal pTableName As String) As DataSet
        Dim ds As New DataSet

        ds = DBAccess.ExecuteNonQueryStore("SP_AccessLog", _
                                           pTableName)

        Return ds
    End Function
End Class

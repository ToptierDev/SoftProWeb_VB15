Imports System.Web
Imports System.Data
Imports System.Data.OleDb
Public Class ClsUser_Information
    Dim DBAccess As New DBAccess
    Public gConn As New ClsOleDb
    Dim sql As String = ""

    Public Function GetCoreUsersProject(ByVal pUserID As String) As DataTable
        Dim dt As New DataTable
        sql = " select a.ID, a.UserID, a.FREPRJNO, b.FREPRJNM "
        sql += " from PNSDBWEB.dbo.CoreUsersProject as a "
        sql += " left join PNSWEB.dbo.ED01PROJ as b "
        sql += " on a.FREPRJNO = b.FREPRJNO "
        sql += " where a.UserID = '" & pUserID & "' "

        dt = DBAccess.GetDataTable(sql, "GetData")
        Return dt
    End Function

    Public Function GetCoreUsersProjectByUse(ByVal pFREPRJNO As String) As DataTable
        Dim dt As New DataTable
        sql = " select '' as ID, '' as UserID, b.FREPRJNO, b.FREPRJNM "
        sql += " from PNSWEB.dbo.ED01PROJ as b "
        sql += " where b.FREPRJNO in (" & pFREPRJNO & ") "

        dt = DBAccess.GetDataTable(sql, "GetData")
        Return dt
    End Function


    Public Function GetCoreUsersCompany(ByVal pUserID As String) As DataTable
        Dim dt As New DataTable
        sql = " select a.ID, a.UserID, a.COMID , b.FDIVNAMET , b.FDIVNAME "
        sql += " from PNSDBWEB.dbo.CoreUsersCompany as a "
        sql += " left join PNSWEB.dbo.BD10DIVI as b "
        sql += " on a.COMID = b.COMID "
        sql += " where a.UserID = '" & pUserID & "' "

        dt = DBAccess.GetDataTable(sql, "GetData")
        Return dt
    End Function


    Public Function GetCoreUsersCompanyByUse(ByVal pFCOMCODE As String) As DataTable

        Dim dt As New DataTable
        sql = " select '' as ID, '' as UserID, b.COMID, b.FDIVNAMET , b.FDIVNAME "
        sql += " from PNSWEB.dbo.BD10DIVI as b "
        sql += " where b.COMID  in (" & pFCOMCODE & ") or b.COMID  in (" & pFCOMCODE & ") "

        dt = DBAccess.GetDataTable(sql, "GetData")
        Return dt
    End Function

End Class

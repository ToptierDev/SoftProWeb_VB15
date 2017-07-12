Imports System.Web
Imports System.Data
Imports System.Data.OleDb
Public Class ClsADI_ImportPriceList
    Dim DBAccess As New DBAccess
    Public gConn As New ClsOleDb
    Dim sql As String = ""

    Public Function GetPhaseCheck(ByVal pFREPRJNO As String, ByVal pFREPHASE As String) As DataTable
        Dim dt As New DataTable

        sql = "  select a.FREZONE,a.FSERIALNO,a.FPDCODE,a.FREPRJNO,a.FREPHASE,a.FRESTATUS,b.FGPPERCENT    "
        sql += " from ED03UNIT a left join ED11COST b on a.FREPRJNO = b.FREPRJNO and a.FSERIALNO = b.FSERIALNO 	"
        sql += " Where a.FREPRJNO ='" & pFREPRJNO & "' and a.FREPHASE='" & pFREPHASE & "' "


        dt = DBAccess.GetDataTable(sql, "GetData")
        Return dt
    End Function

End Class

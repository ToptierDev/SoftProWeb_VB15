Imports System.Web
Imports System.Data
Imports System.Data.OleDb
Public Class ClsADI_ImportStandardTargetPrice
    Dim DBAccess As New DBAccess
    Public gConn As New ClsOleDb
    Dim sql As String = ""

    Public Function GetED11COST(ByVal pFREPRJNO As String, ByVal pFREPHASE As String, ByVal bFUPFLAG As Boolean, ByVal bLang As String) As DataTable
        Dim dt As New DataTable

        'sql = "  select	A.FRNID, A.FREPRJNO as ProjectCode, A.FSERIALNO	 as UnitNo,	 Format(isnull(A.FPOVERAREA,0), '##,##0.00') as LanPriceSqw   "
        'sql += "    , Format(isnull(A.FCOSTPRICE,0), '##,##0.00') as CostPrice	 , Format(isnull(A.FGPPRICE,0), '##,##0.00') as StandardPrice	,Format(isnull(A.FGPPERCENT,0), '##,##0.00')	as GP   "
        'sql += "  	, Format(isnull(A.FLOCATEPRICE,0), '##,##0.00') as LocatePrice , Format(isnull(A.FTARGETPRICE,0), '##,##0.00') as TargetPrice "
        sql = "  select	A.FRNID, A.FREPRJNO as ProjectCode, A.FSERIALNO	 as UnitNo,	 isnull(A.FPOVERAREA,0) as LanPriceSqw   "
        sql += "    , isnull(A.FCOSTPRICE,0) as CostPrice	 , isnull(A.FGPPRICE,0) as StandardPrice	,isnull(A.FGPPERCENT,0)	as GP   "
        sql += "  	, isnull(A.FLOCATEPRICE,0) as LocatePrice , isnull(A.FTARGETPRICE,0) as TargetPrice "
        If bLang = "TH" Then
            sql += "    , case when LEN(DAY(A.FLOADDATE)) =  1 then '0' + Convert(varchar(2),DAY(A.FLOADDATE)) else Convert(varchar(2),DAY(A.FLOADDATE)) end   + '/' +  "
            sql += "      case when LEN(MONTH(A.FLOADDATE)) =  1 then '0' + Convert(varchar(2),MONTH(A.FLOADDATE)) else Convert(varchar(2),MONTH(A.FLOADDATE)) end   + '/' + "
            sql += "      Convert(varchar(4),year(A.FLOADDATE)+543) as ImportDate "
        Else
            sql += "    , Convert(varchar(10),A.FLOADDATE,103) as  ImportDate   "
        End If

        If bFUPFLAG = True Then
            sql += "  	, '' as Approve   "
            sql += "  	, '' as Approvers   "
        Else
            sql += "  	, A.FUPFLAG as Approve "
            sql += "  	, A.FUSERID as Approvers   "
        End If
        sql += "  	from "
        sql += "    ED11COST As a inner Join "
        sql += "    ED03UNIT As b On a.FREPRJNO=B.FREPRJNO And a.FSERIALNO=B.FSERIALNO "
        sql += " Where b.FREPRJNO ='" & pFREPRJNO & "' and B.FREPHASE='" & pFREPHASE & "' "

        If bFUPFLAG = True Then
            sql += " and (A.FUPFLAG<>'Y' or A.FUPFLAG is null) "
        Else
            sql += " and A.FUPFLAG='Y' "
        End If

        dt = DBAccess.GetDataTable(sql, "GetData")
        Return dt
    End Function

    Public Function GetSumStandardPrice(ByVal pFREPRJNO As String, ByVal pFREPHASE As String, ByVal bFUPFLAG As Boolean) As DataTable
        Dim dt As New DataTable

        sql = "  select	sum(A.FGPPRICE) As SumStandardPrice   "
        sql += "  	from "
        sql += "    ED11COST As a inner Join "
        sql += "    ED03UNIT As b On a.FREPRJNO=B.FREPRJNO And a.FSERIALNO=B.FSERIALNO "
        sql += " Where b.FREPRJNO ='" & pFREPRJNO & "' and B.FREPHASE='" & pFREPHASE & "' "

        If bFUPFLAG = True Then
            sql += " and (A.FUPFLAG<>'Y' or A.FUPFLAG is null) "
        Else
            sql += " and A.FUPFLAG='Y' "
        End If

        dt = DBAccess.GetDataTable(sql, "GetData")
        Return dt
    End Function

    Public Function GetSumTargetPrice(ByVal pFREPRJNO As String, ByVal pFREPHASE As String, ByVal bFUPFLAG As Boolean) As DataTable
        Dim dt As New DataTable

        sql = "  select	sum(A.FTARGETPRICE) As SumTargetPrice   "
        sql += "  	from "
        sql += "    ED11COST As a inner Join "
        sql += "    ED03UNIT As b On a.FREPRJNO=B.FREPRJNO And a.FSERIALNO=B.FSERIALNO "
        sql += " Where b.FREPRJNO ='" & pFREPRJNO & "' and B.FREPHASE='" & pFREPHASE & "' "

        If bFUPFLAG = True Then
            sql += " and (A.FUPFLAG<>'Y' or A.FUPFLAG is null) "
        Else
            sql += " and A.FUPFLAG='Y' "
        End If

        dt = DBAccess.GetDataTable(sql, "GetData")
        Return dt
    End Function

End Class

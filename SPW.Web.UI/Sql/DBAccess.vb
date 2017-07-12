Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class DBAccess
    'Public Mycon As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString
    'Public TimeAuthen As String = System.Configuration.ConfigurationManager.AppSettings("TimeAuthen").ToString

    Dim Mycon As String = ConfigurationSettings.AppSettings("ConnectionString")
    Dim TimeAuthen As String = ConfigurationSettings.AppSettings("TimeAuthen")

    Public Function GetConnection() As SqlConnection
        Dim con As SqlConnection = New SqlConnection
        With con
            .ConnectionString = Mycon
        End With

        Return con
    End Function
    Function ExecuteNonQueryStore(ByVal pStoreName As String,
                                  ByVal pTableName As String) As DataSet
        Dim ds As DataSet = New DataSet
        Dim con As SqlConnection = New SqlConnection
        With con
            If .State = ConnectionState.Open Then .Close()
            .ConnectionString = Mycon
            .Open()
        End With

        Dim mycom As SqlCommand = New SqlCommand(pStoreName, con)
        mycom.CommandType = CommandType.StoredProcedure
        Dim da As SqlDataAdapter = New SqlDataAdapter(mycom)

        Try
            da.Fill(ds, "reader")
        Catch
            ds = Nothing
        End Try
        Return ds
    End Function

    Function GetFStatusContCode(ByVal pParameters As String) As DataSet
        Dim ds As DataSet = New DataSet
        Dim con As SqlConnection = New SqlConnection
        With con
            If .State = ConnectionState.Open Then .Close()
            .ConnectionString = Mycon
            .Open()
        End With

        Dim pStoreName As String = "SP_FStatusContCode"
        Dim mycom As SqlCommand = New SqlCommand(pStoreName, con)
        mycom.CommandType = CommandType.StoredProcedure
        mycom.Parameters.Add(New SqlParameter("fcontcode", DbType.String)).Value = pParameters

        Dim da As SqlDataAdapter = New SqlDataAdapter(mycom)

        Try
            da.Fill(ds, "reader")
        Catch
            ds = Nothing
        End Try
        Return ds
    End Function

    Function ExecuteNonQuery(ByVal SQL As String) As Boolean

        Dim con As SqlConnection = New SqlConnection
        With con
            If .State = ConnectionState.Open Then .Close()
            .ConnectionString = Mycon
            .Open()
        End With

        Dim mycom As SqlCommand = New SqlCommand(SQL, con)
        Dim rowAffect As Integer

        Try
            rowAffect = mycom.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
        End Try

    End Function

    Function ExecuteReader(ByVal SQL As String)

        Dim con As SqlConnection = New SqlConnection
        With con
            If .State = ConnectionState.Open Then .Close()
            .ConnectionString = Mycon
            .Open()
        End With

        Dim ds As DataSet = New DataSet
        Dim da As SqlDataAdapter = New SqlDataAdapter(SQL, con)
        da.Fill(ds, "reader")

        con.Close()
        Return ds.Tables("reader").DefaultView
    End Function

    Function ExecuteReader(ByVal Con As SqlConnection, ByVal Trans As SqlTransaction, ByVal SQL As String) As DataTable
        'use old connection for Transation
        Dim ds As DataSet = New DataSet
        Dim cmd As New SqlCommand(SQL, Con, Trans)
        Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
        da.Fill(ds, "reader")

        Return ds.Tables("reader")
    End Function

    Public Function GetDataTable(ByVal sql As String, ByVal iTableName As String) As DataTable
        Dim da As SqlDataAdapter = New SqlDataAdapter(sql, Mycon)
        Dim dt As New DataTable
        da.Fill(dt)
        dt.TableName = iTableName
        da.Dispose() : da = Nothing

        Return dt
    End Function

    Function GetMax(ByVal Table As String, ByVal ColumnName As String)
        'หาจำนวนมากที่สุด

        Dim con As SqlConnection = New SqlConnection
        With con
            If .State = ConnectionState.Open Then .Close()
            .ConnectionString = Mycon
            .Open()
        End With

        Dim sql As String
        sql = "select max(" & ColumnName & ")  from " & Table & ""

        Dim mycom As SqlCommand = New SqlCommand(sql, con)
        Dim Max As Integer
        Try
            Max = mycom.ExecuteScalar()
        Catch ex As Exception
            Max = 0
        End Try

        con.Close()

        Return Max

    End Function

    Function ReadXML(ByVal Path As String)
        Dim ds As DataSet = New DataSet
        ds.ReadXml(Path)

        Return ds.Tables(0).DefaultView
    End Function
    Function TimeAuthen_m()

        Return TimeAuthen
    End Function

    Function CheckMAC(ByVal Mac As String)
        '  เช็ค รูปแบบ
        '  Dim a As Boolean = False
        Dim regex As New System.Text.RegularExpressions.Regex("\d\d-\d\d-\d\d-\d\d-\d\d-\d\d")

        Dim match As System.Text.RegularExpressions.Match

        match = regex.Match(Mac)

        If Not match.Success Then
            ' b = True
            Return True
        End If

        Return False
    End Function

    Public Function ExecuteReturnExcel(ByVal SheetName As String, ByVal paths As String)
        Dim xlsSQL As String = "select * from [" & SheetName & "$]"
        Dim xlsStrConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & paths & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1;"""
        Dim sql As String = ""

        Try
            Dim xlsDdpter As New OleDb.OleDbDataAdapter(xlsSQL, xlsStrConn)
            Dim xlsDataSet As New DataSet()
            Dim ds1 As New DataSet()
            xlsDdpter.Fill(xlsDataSet, "xls")

            Return xlsDataSet.Tables("xls").DefaultView
        Catch ex As Exception
            Return Nothing
        End Try
        '+++++++++++++dataset++++++++++++++++++
    End Function
End Class

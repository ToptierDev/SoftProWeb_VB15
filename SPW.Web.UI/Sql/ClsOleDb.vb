Imports System.Data
Imports System.Data.OleDb

Public Class ClsOleDb
    Private cOleDbErrorMsg As String
    Private cOleDbConn As OleDbConnection
    Private cOleDbConnectionString As String
    Public ReadOnly Property ClassErrorMsg() As String
        Get
            Return cOleDbErrorMsg
        End Get
    End Property
    Public ReadOnly Property State() As Boolean
        Get
            Return cOleDbConn.State
        End Get
    End Property
    Public Property ConnectionString() As String
        Get
            Return cOleDbConnectionString
        End Get
        Set(ByVal iConnectionString As String)
            cOleDbConnectionString = iConnectionString
        End Set
    End Property
    Public Sub Open()
        Try
            If cOleDbConnectionString = "" Then cOleDbErrorMsg = "No connection found" : Exit Sub
            cOleDbConn = New OleDbConnection(cOleDbConnectionString)
            cOleDbConn.Open()
        Catch ex1 As OleDbException
            cOleDbErrorMsg = ex1.Message
        End Try
    End Sub
    Public Sub Close()
        Try
            cOleDbConn.Close() : cOleDbConn.Dispose()
        Catch ex As Exception
            cOleDbErrorMsg = ex.Message
        End Try
    End Sub
    Public Function OleDataSet(ByVal vSQL As String, ByVal TabName As String) As DataSet
        Try
            Dim MyDa As New OleDbDataAdapter(vSQL, cOleDbConn)
            Dim Ds As New DataSet
            Ds.Clear()
            MyDa.Fill(Ds, TabName)
            OleDataSet = Ds
        Catch ex1 As System.Data.OleDb.OleDbException
            cOleDbErrorMsg = ex1.Message
            Return Nothing
        End Try
    End Function
    Public Function OleDataTable(ByVal vSQL As String) As DataTable
        Try
            Dim MyDa As New OleDbDataAdapter(vSQL, cOleDbConn)
            Dim Ds As New DataSet
            Ds.Clear()
            MyDa.Fill(Ds, "DATA")
            OleDataTable = Ds.Tables("DATA")
        Catch ex1 As System.Data.OleDb.OleDbException
            cOleDbErrorMsg = ex1.Message
            Return Nothing
        End Try
    End Function
    Public Sub OleCommand(ByVal vSQL As String)
        Try
            Dim cmd As OleDbCommand = New OleDbCommand(vSQL)
            cmd.Connection = cOleDbConn
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
        Catch ex1 As System.Data.OleDb.OleDbException
            cOleDbErrorMsg = ex1.Message
        End Try
    End Sub


    Public Function OleCommandReader(ByVal vSQL As String) As DataTable
        Try

            Dim myReader As OleDbDataReader


            Dim cmd As OleDbCommand = New OleDbCommand(vSQL)
            cmd.Connection = cOleDbConn
            cmd.CommandType = CommandType.Text
            myReader = cmd.ExecuteReader()

            Using myDataTable As New DataTable

                Try
                    myDataTable.Load(myReader)
                    Return myDataTable
                Catch ex As Exception
                    Return Nothing
                End Try

            End Using

        Catch ex1 As System.Data.OleDb.OleDbException
            cOleDbErrorMsg = ex1.Message
            Return Nothing
        End Try
    End Function

    Public Function DbExcute(ByVal iSQL As String) As Boolean
        Try
            Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(iSQL, cOleDbConn)
            cmd.CommandTimeout = 600
            cmd.ExecuteNonQuery()
            DbExcute = True
            cmd.Dispose() : cmd = Nothing
        Catch ex As Exception
            DbExcute = False
        End Try
    End Function

    Public Function DbExecuteScalar(ByVal iSQL As String) As Integer
        Try
            Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(iSQL, cOleDbConn)
            DbExecuteScalar = CType(cmd.ExecuteScalar(), Integer)
            cmd.Dispose() : cmd = Nothing
        Catch ex As Exception
            DbExecuteScalar = -1
        End Try
    End Function
End Class

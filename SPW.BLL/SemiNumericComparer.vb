
Public Class SemiNumericComparer
    Implements IComparer(Of String)
    Public Function Compare(s1 As String, s2 As String) As Integer
        If IsNumeric(s1) AndAlso IsNumeric(s2) Then
            If Convert.ToInt32(s1) > Convert.ToInt32(s2) Then
                Return 1
            End If
            If Convert.ToInt32(s1) < Convert.ToInt32(s2) Then
                Return -1
            End If
            If Convert.ToInt32(s1) = Convert.ToInt32(s2) Then
                Return 0
            End If
        End If

        If IsNumeric(s1) AndAlso Not IsNumeric(s2) Then
            Return -1
        End If

        If Not IsNumeric(s1) AndAlso IsNumeric(s2) Then
            Return 1
        End If

        Return String.Compare(s1, s2, True)
    End Function

    Public Shared Function IsNumeric(value As Object) As Boolean
        Try
            Dim i As Integer = Convert.ToInt32(value.ToString())
            Return True
        Catch generatedExceptionName As FormatException
            Return False
        End Try
    End Function

    Private Function IComparer_Compare(x As String, y As String) As Integer Implements IComparer(Of String).Compare
        Return Compare(x, y)
    End Function
End Class


Public Class ErrorViewModels

    Public Property IsError As Boolean = False
    Public Property Code As String = String.Empty
    Public Property Message As String = String.Empty
    Public Property Detail As String = String.Empty
    Public Property Api As String = String.Empty
    Public Property Verb As String = String.Empty
    Public Property StackTrace As String = String.Empty
    Public Property ErrorObject As Object = New Object()


End Class



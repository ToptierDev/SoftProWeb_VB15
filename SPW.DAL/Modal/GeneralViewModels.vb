Public Class GeneralViewModels
    Public Sub New()

        ErrorView = New ErrorViewModels()
        Datas = New GeneralDataViewModel()
    End Sub



    Private newErrorViewModels As ErrorViewModels
    Public Property ErrorView() As ErrorViewModels
        Get
            Return newErrorViewModels
        End Get
        Set(ByVal value As ErrorViewModels)
            newErrorViewModels = value
        End Set
    End Property

    Private newDatasValue As GeneralDataViewModel
    Public Property Datas() As GeneralDataViewModel
        Get
            Return newDatasValue
        End Get
        Set(ByVal value As GeneralDataViewModel)
            newDatasValue = value
        End Set
    End Property

End Class



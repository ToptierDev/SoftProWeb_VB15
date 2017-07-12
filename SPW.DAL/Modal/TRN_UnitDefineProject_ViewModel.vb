Public Class TRN_UnitDefineProject_ViewModel
    Public Property ED01PROJ() As ED01PROJ
    Public Property SD05PDDS() As SD05PDDS
    Public Property SD03TYPE() As SD03TYPE
    Public Property FD11PROP() As FD11PROP
    Public Property ED03UNIT() As ED03UNIT
    Public Property List_SD05PDDS() As List(Of SD05PDDS)
    Public Property List_ED03UNIT() As List(Of ED03UNIT)
    Public Property List_vwED03UNIT() As List(Of vw_UnitDefineProject)

End Class

Public Class TRN_UnitDefineProject_Detail_ViewModel
    Public Property ED01PROJ() As ED01PROJ
    Public Property SD05PDDS() As SD05PDDS
    Public Property SD03TYPE() As SD03TYPE
    Public Property FD11PROP() As FD11PROP
    Public Property ED03UNIT() As List(Of ED03UNIT)
End Class
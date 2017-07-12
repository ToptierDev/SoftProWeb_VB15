Public Class ProjectPhaseZoneBlockMaster_ViewModel
    Public projects As List(Of ProjectPhaseZoneBlockMaster_Project_ViewModel)
End Class
Public Class ProjectPhaseZoneBlockMaster_Project_ViewModel
    Public FREPRJNO As String
    Public ED01PROJ As ED01PROJ
    Public phases As List(Of ProjectPhaseZoneBlockMaster_Phase_ViewModel)
End Class
Public Class ProjectPhaseZoneBlockMaster_Phase_ViewModel
    Public FREPHASE As String
    Public ED02PHAS As ED02PHAS
    Public zones As List(Of ProjectPhaseZoneBlockMaster_Zone_ViewModel)
End Class
Public Class ProjectPhaseZoneBlockMaster_Zone_ViewModel
    Public FREZONE As String
    Public ED04RECF As ED04RECF
    Public blocks As List(Of ProjectPhaseZoneBlockMaster_Block_ViewModel)
End Class
Public Class ProjectPhaseZoneBlockMaster_Block_ViewModel
    Public FREBLOCK As String
    Public ED04BLOK As ED04BLOK
End Class



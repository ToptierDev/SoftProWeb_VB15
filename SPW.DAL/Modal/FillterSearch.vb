Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class FillterSearch
    Public Sub New()
    End Sub

    Public Enum SortTypeEnum
        None = 0
        Asc = 1
        Desc = 2
    End Enum

    Public Property Keyword() As String
        Get
            Return m_Keyword
        End Get
        Set(ByVal value As String)
            m_Keyword = Value
        End Set
    End Property
    Private m_Keyword As String
    Public Property SortType() As Nullable(Of Byte)
        Get
            Return m_SortType
        End Get
        Set(ByVal value As Nullable(Of Byte))
            m_SortType = value
        End Set
    End Property
    Private m_SortType As Nullable(Of Byte)
    Public Property SortBy() As String
        Get
            Return m_SortBy
        End Get
        Set(ByVal value As String)
            m_SortBy = Value
        End Set
    End Property
    Private m_SortBy As String
    Public Property Page() As Integer
        Get
            Return m_Page
        End Get
        Set(ByVal value As Integer)
            m_Page = Value
        End Set
    End Property
    Private m_Page As Integer
    Public Property PageSize() As Integer
        Get
            Return m_PageSize
        End Get
        Set(ByVal value As Integer)
            m_PageSize = Value
        End Set
    End Property
    Private m_PageSize As Integer
End Class

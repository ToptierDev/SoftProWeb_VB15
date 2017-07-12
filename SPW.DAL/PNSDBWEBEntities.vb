Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Web

Partial Public Class PNSDBWEBEntities
    Public Shared ReadOnly Property Context() As PNSDBWEBEntities
        Get
            Dim objectContextKey As String = HttpContext.Current.GetHashCode().ToString("x")
            If Not HttpContext.Current.Items.Contains(objectContextKey) Then
                HttpContext.Current.Items.Add(objectContextKey, New PNSDBWEBEntities())
            End If
            Return TryCast(HttpContext.Current.Items(objectContextKey), PNSDBWEBEntities)
        End Get
    End Property
End Class

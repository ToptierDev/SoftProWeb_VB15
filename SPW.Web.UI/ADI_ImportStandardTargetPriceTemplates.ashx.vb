Imports System.Web
Imports System.Web.Services
Imports NPOI.HSSF
Imports NPOI.HSSF.UserModel
Imports NPOI.XSSF
Imports NPOI.XSSF.UserModel
Imports System.IO

Imports SPW.DAL
Imports SPW.BLL
Imports SPW.Helper
Imports System.Resources
Imports System.Globalization

Public Class ADI_ImportStandardTargetPriceTemplates
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim strInfoLang As String = context.Request.QueryString("infoLang")
        'Dim strInfoRoleCode As String = context.Request.QueryString("infoRoleCode")

        Dim _Filename As String = ""
        If strInfoLang.ToUpper = "TH" Then
            _Filename = "TH"
        Else
            _Filename = "EN"
        End If

        Dim workbook As New XSSFWorkbook(New FileStream(context.Server.MapPath("~/Templates/Template_StandardTargetPrice_" & _Filename & ".xlsx"), FileMode.Open, FileAccess.Read))

        Dim worksheet = workbook.GetSheet("Ducument")


        'Dim startRow As Integer = 0

        'For i As Integer = 0 To dt.Rows.Count - 1
        '    Dim TypeCode As String = String.Empty
        '    Dim TypeName As String = String.Empty

        '    Dim row = worksheet.CreateRow(startRow)

        '    row.CreateCell(0).SetCellValue(TypeCode)
        '    row.CreateCell(1).SetCellValue(TypeName)
        '    row.CreateCell(2).SetCellValue(TypeName)
        '    row.CreateCell(3).SetCellValue(TypeName)
        '    row.CreateCell(4).SetCellValue(TypeName)
        '    row.CreateCell(5).SetCellValue(TypeName)
        '    row.CreateCell(6).SetCellValue(TypeName)

        'Next

        context.Response.Clear()
        context.Response.Buffer = True
        context.Response.Charset = ""
        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"

        Dim ms As New MemoryStream
        workbook.Write(ms)

        context.Response.AddHeader("Content-Disposition", "attachment;filename=StandardTargetPrice_Template.xlsx")
        context.Response.AddHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        'context.Response.BinaryWrite(ms.GetBuffer())
        context.Response.BinaryWrite(ms.ToArray())
        context.Response.Flush()
        context.Response.Close()
        context.Response.End()

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class
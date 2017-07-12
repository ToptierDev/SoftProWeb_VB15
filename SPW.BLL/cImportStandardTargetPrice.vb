Imports SPW.BLL
Imports SPW.DAL
Imports System.Text
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Security.Cryptography
Imports System.Configuration

Public Class cImportStandardTargetPrice

#Region "ADI_ImportStandardTargetPrice.aspx"
    ''Get Project All
    Public Function GetProjectAll() As List(Of ED01PROJ)

        Using db As New PNSWEBEntities
            Dim lc As List(Of ED01PROJ) = db.ED01PROJ.OrderBy(Function(s) s.FREPRJNO).ToList
            Return lc
        End Using

    End Function

    ''Get Phase All
    Public Function GetPhaseAll(ByVal _FREPRJNO As String) As List(Of String)

        Using db As New PNSWEBEntities
            Dim lc1 = db.ED03UNIT.Where(Function(s) s.FREPRJNO = _FREPRJNO And s.FREPHASE IsNot Nothing And s.FREPHASE <> "")

            'Dim lc = From prod In lc1
            '         Group prod By prod.FREPHASE Into grouping = Group

            Dim lc As List(Of String) = lc1.Select(Function(s) s.FREPHASE).Distinct().ToList

            'Dim lc As List(Of ED03UNIT) = db.ED03UNIT.Where(Function(s) s.FREPRJNO = _FREPRJNO).OrderBy(Function(s) s.FREPHASE).ToList
            Return lc
        End Using

    End Function

    Public Function GetPhaseCheck(ByVal _FREPRJNO As String, ByVal _FREPHASE As String) As List(Of ED03UNIT)
        'FSERIALNO
        Using db As New PNSWEBEntities
            Dim lc As List(Of ED03UNIT) = db.ED03UNIT.Where(Function(s) s.FREPRJNO = _FREPRJNO And s.FREPHASE = _FREPHASE).ToList
            Return lc
        End Using

    End Function

    'CREATE TABLE [dbo].[ED11COST](
    '[FRNID] [int] IDENTITY(1,1) Not NULL,
    '[FREPRJNO] [varchar](6) NULL,
    '[FSERIALNO] [varchar](6) NULL,
    '[FCOSTPRICE] [float] NULL,
    '[FGPPERCENT] [float] NULL,
    '[FGPPRICE] [float] NULL,
    '[FLOCATEPRICE] [float] NULL,
    '[FTARGETPRICE] [float] NULL,
    '[FPOVERAREA] [float] NULL,
    '[FUSERID] [nchar](3) NULL,
    '[FLOADDATE] [datetime] NULL,
    '[FUPFLAG] [nchar](1) NULL,
    '[FUPDATE] [datetime] NULL,
    '[FUPTIME] [nvarchar](8) NULL,

    Public Function AddUploadAll(ByRef dtFileUpload As DataTable,
                                 ByVal pGpPercent As String,
                                        ByVal pUserId As String) As Boolean
        Try
            Using db As New PNSWEBEntities

                For Each dr As DataRow In dtFileUpload.Rows
                    If dr("FRESTATUS").ToString <> "2" AndAlso
                             dr("FRESTATUS").ToString <> "3" AndAlso
                             dr("FRESTATUS").ToString <> "4" Then
                        'DeleteED11COST
                        'Dim lst As List(Of ED11COST) = db.ED11COST.Where(Function(s) s.FREPRJNO = dr("ProjectCode").ToString And s.FSERIALNO = dr("UnitNo").ToString).ToList
                        'For Each u As ED11COST In lst
                        '    db.ED11COST.Remove(u)
                        'Next

                        Dim lst As List(Of ED11COST) = db.ED11COST.ToArray.Where(Function(s) s.FREPRJNO = dr("ProjectCode") And s.FSERIALNO = dr("UnitNo")).ToList
                        For Each u As ED11COST In lst
                            db.ED11COST.Remove(u)
                        Next

                        'AddED11COST
                        Dim lc As ED11COST = New ED11COST

                        lc.FREPRJNO = dr("ProjectCode").ToString
                        lc.FSERIALNO = dr("UnitNo").ToString
                        If dr("LanPriceSqw").ToString = String.Empty Then
                            lc.FPOVERAREA = 0
                        Else
                            lc.FPOVERAREA = dr("LanPriceSqw").ToString.Replace(",", "")
                        End If
                        If dr("CostPrice").ToString = String.Empty Then
                            lc.FCOSTPRICE = 0
                        Else
                            lc.FCOSTPRICE = dr("CostPrice").ToString.Replace(",", "")
                        End If
                        If dr("StandardPrice").ToString = String.Empty Then
                            lc.FGPPRICE = 0
                        Else
                            lc.FGPPRICE = dr("StandardPrice").ToString.Replace(",", "")
                        End If
                        If dr("LocatePrice").ToString = String.Empty Then
                            lc.FLOCATEPRICE = 0
                        Else
                            lc.FLOCATEPRICE = dr("LocatePrice").ToString.Replace(",", "")
                        End If
                        If dr("TargetPrice").ToString = String.Empty Then
                            lc.FTARGETPRICE = 0
                        Else
                            lc.FTARGETPRICE = dr("TargetPrice").ToString.Replace(",", "")
                        End If
                        If pGpPercent = String.Empty Then
                            lc.FGPPERCENT = 0
                        Else
                            lc.FGPPERCENT = pGpPercent
                        End If

                        lc.FUSERID = pUserId
                        lc.FLOADDATE = Date.Now
                        lc.FUPFLAG = "N"
                        'lc.FUPFLAG = "1"
                        'lc.FUPDATE = Date.Now
                        'lc.FUPTIME = "1"

                        db.ED11COST.Add(lc)

                    End If
                Next

                db.SaveChanges()

                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function UpdateApproveAll(ByRef dtFileUpload As DataTable,
                                        ByVal pUserId As String) As Boolean
        Try

            Using db As New PNSWEBEntities
                For Each dr As DataRow In dtFileUpload.Rows
                    Dim lc As ED11COST = db.ED11COST.ToArray.Where(Function(s) s.FRNID = dr("FRNID")).SingleOrDefault
                    If lc IsNot Nothing Then
                        lc.FUSERID = pUserId
                        lc.FUPDATE = Date.Now
                        lc.FUPFLAG = "Y"
                        lc.FUPTIME = Date.Now.ToString("HH:mm:ss")
                    End If
                Next
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetPosition(ByVal _PositionCode As String) As String
        'FSERIALNO
        Dim _PriceApprove As String = ""
        Using db As New PNSDBWEBEntities
            Try
                Dim lc As CorePosition = db.CorePositions.Where(Function(s) s.PositionCode = _PositionCode).SingleOrDefault
                _PriceApprove = lc.PriceApprove
            Catch ex As Exception
                _PriceApprove = ""
            End Try

            Return _PriceApprove
        End Using

    End Function

#End Region

#Region "Dropdownlist"

    Public Function LoadProject(ByVal pKeyID As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                Dim qury = (From m In db.ED01PROJ.OrderBy(Function(s) s.FREPRJNO)
                            Select m.FREPRJNO & "-" & m.FREPRJNM).ToList()

                Return qury.ToList()
            ElseIf pKeyID <> String.Empty Then
                Dim qury = (From m In db.ED01PROJ.OrderBy(Function(s) s.FREPRJNO)
                            Select m.FREPRJNO & "-" & m.FREPRJNM) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                Return qury.ToList()
            End If
        End Using
        Return Nothing
    End Function

    Public Function LoadPhase(ByVal pKeyID As String, ByVal pFREPRJNO As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                Dim qury = (From m In db.ED03UNIT.Where(Function(s) s.FREPRJNO = pFREPRJNO And s.FREPHASE IsNot Nothing And s.FREPHASE <> "").OrderBy(Function(s) s.FREPHASE)
                            Select m.FREPHASE).Distinct().ToList

                Return qury.ToList()
            ElseIf pKeyID <> String.Empty Then
                Dim qury = (From m In db.ED03UNIT.Where(Function(s) s.FREPRJNO = pFREPRJNO And s.FREPHASE IsNot Nothing And s.FREPHASE <> "").OrderBy(Function(s) s.FREPHASE)
                            Select m.FREPHASE) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).Distinct().ToList()

                Return qury.ToList()
            End If
        End Using
        Return Nothing
    End Function

    Public Function LoadProject() As List(Of ED01PROJ)
        Using db As New PNSWEBEntities
            Dim qury As List(Of ED01PROJ) = db.ED01PROJ.OrderBy(Function(s) s.FREPRJNO).ToList
            qury = qury.Select(Function(m) New ED01PROJ With {.FREPRJNO = m.FREPRJNO, .FREPRJNM = String.Format("{0} - {1}", m.FREPRJNO, m.FREPRJNM)}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function
    Public Function LoadPhase(ByVal pProject As String) As List(Of ED02PHAS)
        Using db As New PNSWEBEntities
            Dim qury As List(Of ED02PHAS) = db.ED02PHAS.Where(Function(s) s.FREPRJNO = pProject).OrderBy(Function(s) s.FREPHASE).ToList
            qury = qury.Select(Function(m) New ED02PHAS With {.FREPHASE = m.FREPHASE}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function

#End Region

End Class

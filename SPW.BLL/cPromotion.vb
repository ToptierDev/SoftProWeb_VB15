Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cPromotion

#Region "INT_Promotion.aspx"

    Public Function Loaddata(ByVal pPromotionCode As String,
                             ByVal pActive As Boolean,
                             ByVal strLG As String) As List(Of Promotion_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.SD05PDDS
                       Group Join mProject In db.ED01PROJ On l.FMODEL Equals mProject.FREPRJNO
                       Into mProj = Group From mProject In mProj.DefaultIfEmpty()
                       Group Join mTypeHouse In db.SD05PDDS.Where(Function(s) s.FCLASS = "0") On l.FPDGRP Equals mTypeHouse.FPDCODE
                       Into mTypeH = Group From mTypeHouse In mTypeH.DefaultIfEmpty()
                       Group Join mBD01STB1 In db.BD01STB1 On l.FPDCODE Equals mBD01STB1.FPDCODE
                           Into mBD = Group From mBD01STB1 In mBD.DefaultIfEmpty()
                       Select New Promotion_ViewModel With {
                               .FPDCODE = l.FPDCODE,
                               .FPDNAME = l.FPDNAME,
                               .FPDNAMET = l.FPDNAMET,
                               .FREPRJNM = mProject.FREPRJNM,
                               .FPDCODETYPE = mTypeHouse.FPDCODE,
                               .FPDNAMETYPE = mTypeHouse.FPDNAME,
                               .FPDNAMETYPET = mTypeHouse.FPDNAMET,
                               .FNOTUSE = l.FNOTUSE,
                               .CreateDate = l.StmDate,
                               .FUPDFLAG = mBD01STB1.FUPDFLAG
                           }

            If Not String.IsNullOrEmpty(pPromotionCode) Then
                qury = qury.Where(Function(s) s.FPDCODE.ToUpper.Replace(" ", "").Contains(pPromotionCode.ToUpper.Replace(" ", "")) Or
                                              s.FPDNAME.ToUpper.Replace(" ", "").Contains(pPromotionCode.ToUpper.Replace(" ", "")) Or
                                              s.FPDNAMET.ToUpper.Replace(" ", "").Contains(pPromotionCode.ToUpper.Replace(" ", "")))
            End If
            If Not String.IsNullOrEmpty(pActive) Then
                If pActive Then
                    qury = qury.Where(Function(s) s.FNOTUSE = "N")
                Else
                    qury = qury.Where(Function(s) s.FNOTUSE = "Y")
                End If
            End If

            qury = qury.OrderBy(Function(s) s.FPDCODE)

            Dim lists = qury.ToList()
            Return lists
        End Using
    End Function

    Public Function LoadEdit(ByVal pPromoCode As String,
                             ByVal strLG As String) As Promotion_ViewModel

        Using db As New PNSWEBEntities
            Dim qury = From l In db.SD05PDDS
                       Group Join mProject In db.ED01PROJ On mProject.FREPRJNO Equals l.FMODEL
                       Into mProj = Group From mProject In mProj.DefaultIfEmpty()
                       Group Join mTypeHouse In db.SD05PDDS.Where(Function(s) s.FCLASS = "0") On mTypeHouse.FPDCODE Equals l.FPDGRP
                       Into mTypeH = Group From mTypeHouse In mTypeH.DefaultIfEmpty()
                       Group Join mBD01STB1 In db.BD01STB1 On mBD01STB1.FPDCODE Equals l.FPDCODE
                       Into mBD01SB1 = Group From mBD01STB1 In mBD01SB1.DefaultIfEmpty()
                       Select New Promotion_ViewModel With {
                            .FPDCODE = l.FPDCODE,
                            .FPDNAME = l.FPDNAME,
                            .FPDNAMET = l.FPDNAMET,
                            .FREPRJNO = mProject.FREPRJNO,
                            .FREPRJNM = mProject.FREPRJNM,
                            .FPDCODETYPE = mTypeHouse.FPDCODE,
                            .FPDNAMETYPE = mTypeHouse.FPDNAME,
                            .FPDNAMETYPET = mTypeHouse.FPDNAMET,
                            .FNOTUSE = l.FNOTUSE,
                            .FUPDBY = mBD01STB1.FUPDBY,
                            .FUPDFLAG = mBD01STB1.FUPDFLAG,
                            .FBATCHSZU = mBD01STB1.FBATCHSZU,
                            .FPCOMPLETE = mBD01STB1.FPCOMPLETE,
                            .FSTDCOST = l.FSTDCOST,
                            .FSTDPRICE = l.FSTDPRICE,
                            .FOPDESC = mBD01STB1.FOPDESC,
                            .FOPSEQ = mBD01STB1.FOPSEQ,
                            .CreateDate = l.StmDate
                        }

            If Not String.IsNullOrEmpty(pPromoCode) Then
                qury = qury.Where(Function(s) s.FPDCODE = pPromoCode)
            End If

            qury = qury.OrderBy(Function(s) s.FPDCODE)

            Dim lists = qury.FirstOrDefault()
            Return lists
        End Using
    End Function

    Public Function Delete(ByVal pPromoCode As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lslt As List(Of BD01STB2) = db.BD01STB2.Where(Function(s) s.FPDCODE = pPromoCode).ToList
                For Each u As BD01STB2 In lslt
                    db.BD01STB2.Remove(u)
                Next

                Dim lst As BD01STB1 = db.BD01STB1.Where(Function(s) s.FPDCODE = pPromoCode).SingleOrDefault
                If lst IsNot Nothing Then
                    db.BD01STB1.Remove(lst)
                End If

                Dim lc As SD05PDDS = db.SD05PDDS.Where(Function(s) s.FPDCODE = pPromoCode).SingleOrDefault
                If lc IsNot Nothing Then
                    db.SD05PDDS.Remove(lc)
                End If


                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Add(ByVal pPromotionCode As String,
                        ByVal pPromotionName As String,
                        ByVal pPromotionAC As String,
                        ByVal pApproveBy As String,
                        ByVal pFlagPromotionStatus As Boolean,
                        ByVal pFlagStandBooking As Boolean,
                        ByVal pFlagUtility As Boolean,
                        ByVal pFlagPR As Boolean,
                        ByVal pFlagVat As Boolean,
                        ByVal pPriceNotOver As String,
                        ByVal pPriceCashDiscount As String,
                        ByVal pProject As String,
                        ByVal pFPDCode As String,
                        ByVal pFPDName As String,
                        ByVal pDecsription As String,
                        ByVal dt As DataTable,
                        ByVal strUserId As String,
                        ByVal pLG As String,
                        ByVal pPromotionNameT As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As SD05PDDS = New SD05PDDS
            lc.FPDCODE = pPromotionCode
            If pPromotionNameT <> String.Empty Then
                lc.FPDNAMET = pPromotionNameT
            Else
                lc.FPDNAMET = String.Empty
            End If

            If pPromotionName <> String.Empty Then
                lc.FPDNAME = pPromotionName
            Else
                lc.FPDNAME = String.Empty
            End If

            If pPromotionAC <> String.Empty Then
                lc.FACCD = pPromotionAC
            Else
                lc.FACCD = String.Empty
            End If
            '    If pApproveBy <> String.Empty Then
            '        lc.ApproveBy = pApproveBy
            '    Else
            '        lc.ApproveBy = String.Empty
            '    End If

            If pFlagPromotionStatus Then
                lc.FNOTUSE = "Y"
            Else
                lc.FNOTUSE = "N"
            End If

            '    lc.FlagStandardBooking = pFlagStandBooking
            '    lc.FlagUtility = pFlagUtility

            If pPriceNotOver <> String.Empty Then
                lc.FSTDCOST = CDec(pPriceNotOver)
            Else
                lc.FSTDCOST = Nothing
            End If
            If pPriceCashDiscount <> String.Empty Then
                lc.FSTDPRICE = CDec(pPriceCashDiscount)
            Else
                lc.FSTDPRICE = Nothing
            End If
            If pProject <> String.Empty Then
                lc.FMODEL = pProject
            Else
                lc.FMODEL = Nothing
            End If
            If pFPDCode <> String.Empty Then
                lc.FPDGRP = pFPDCode
            Else
                lc.FPDGRP = Nothing
            End If
            lc.StmDate = DateTime.Now
            lc.FENTTYPE = "6"
            db.SD05PDDS.Add(lc)

            Dim lst As BD01STB1 = New BD01STB1
            lst.FPDCODE = pPromotionCode
            lst.FROUTECODE = "001"
            lst.FPDQTY = 1
            lst.FOPCODE = ""
            If pFlagUtility Then
                lst.FOPSEQ = "00102"
            Else
                lst.FOPSEQ = "00101"
            End If
            lst.FOPSET = 0
            lst.FOPRUN =
            lst.FOPRUNUT = 0
            lst.FOPRUNSPD = 0
            lst.FOPDEL = 0
            lst.FOPQTY = 0
            lst.FRDLABOUR = 0
            lst.FRFOH = 0
            lst.FRVOH = 0
            lst.FAMATLCOST = 0
            lst.FADLABOUR = 0
            lst.FAFOH = 0
            lst.FAVOH = 0
            lst.FTMATLCOST = 0
            lst.FTDLABOUR = 0
            lst.FTFOH = 0
            lst.FTVOH = 0
            lst.FPCHARGE = 0
            lst.FACHARGE = 0
            lst.FALABOUR = 0
            lst.FSUMPBOQ = 0

            If pFlagPR Then
                lst.FBATCHSZU = 1
            Else
                lst.FBATCHSZU = 0
            End If
            If pFlagVat Then
                lst.FPCOMPLETE = 1
            Else
                lst.FPCOMPLETE = 0
            End If
            If pDecsription <> String.Empty Then
                lst.FOPDESC = pDecsription
            End If
            db.BD01STB1.Add(lst)

            Dim lslt As List(Of BD01STB2) = db.BD01STB2.Where(Function(s) s.FPDCODE = pPromotionCode).ToList
            For Each u As BD01STB2 In lslt
                db.BD01STB2.Remove(u)
            Next
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Dim strID As Integer = 0
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim strItemNo As String = String.Empty
                        If dt.Rows(i)("FlagSetAdd").ToString = "0" Then
                            If dt.Rows(i)("ProductCode").ToString <> String.Empty Then
                                strItemNo = strID
                                For ii As Integer = 0 To 4
                                    If strItemNo.Length < 5 Then
                                        strItemNo = "0" & strItemNo
                                    End If
                                Next
                                Dim m As BD01STB2 = New BD01STB2
                                m.FPDCODE = pPromotionCode
                                m.FROUTECODE = "001"
                                If pFlagUtility Then
                                    m.FOPSEQ = "00102"
                                Else
                                    m.FOPSEQ = "00101"
                                End If
                                m.FCOMPPSCR = 0
                                m.FUDL = 0
                                m.FADL = 0
                                m.FCOMPQSCR = 0
                                m.FQTYA = 0
                                m.FQTYB = 0
                                m.FQTYC = 0
                                m.FCOMPPSCRA = 0
                                m.FCOMPPSCRB = 0
                                m.FCOMPPSCRC = 0
                                m.FITEMNO = strItemNo

                                m.FCOMPCODE = dt.Rows(i)("ProductCode").ToString

                                If dt.Rows(i)("QTY").ToString <> String.Empty Then
                                    m.FQTY = CDec(dt.Rows(i)("QTY").ToString)
                                Else
                                    m.FQTY = Nothing
                                End If

                                If dt.Rows(i)("UnitPrice").ToString <> String.Empty Then
                                    m.FUCOST = CDec(dt.Rows(i)("UnitPrice").ToString)
                                End If

                                db.BD01STB2.Add(m)
                            End If
                        End If
                        strID += 1
                    Next
                End If
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Edit(ByVal pPromotionCode As String,
                         ByVal pPromotionName As String,
                         ByVal pPromotionAC As String,
                         ByVal pApproveBy As String,
                         ByVal pFlagPromotionStatus As Boolean,
                         ByVal pFlagStandBooking As Boolean,
                         ByVal pFlagUtility As Boolean,
                         ByVal pFlagPR As Boolean,
                         ByVal pFlagVat As Boolean,
                         ByVal pPriceNotOver As String,
                         ByVal pPriceCashDiscount As String,
                         ByVal pProject As String,
                         ByVal pFPDCode As String,
                         ByVal pFPDName As String,
                         ByVal pDecsription As String,
                         ByVal dt As DataTable,
                         ByVal strUserId As String,
                         ByVal pLG As String,
                         ByVal pPromotionNameT As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As SD05PDDS = db.SD05PDDS.Where(Function(s) s.FPDCODE = pPromotionCode).SingleOrDefault
            If lc IsNot Nothing Then
                If pPromotionNameT <> String.Empty Then
                    lc.FPDNAMET = pPromotionNameT
                Else
                    lc.FPDNAMET = String.Empty
                End If

                If pPromotionName <> String.Empty Then
                    lc.FPDNAME = pPromotionName
                Else
                    lc.FPDNAME = String.Empty
                End If

                '    If pPromotionAC <> String.Empty Then
                '        lc.PromoAC = pPromotionAC
                '    Else
                '        lc.PromoAC = String.Empty
                '    End If
                '    If pApproveBy <> String.Empty Then
                '        lc.ApproveBy = pApproveBy
                '    Else
                '        lc.ApproveBy = String.Empty
                '    End If

                If pFlagPromotionStatus Then
                    lc.FNOTUSE = "Y"
                Else
                    lc.FNOTUSE = "N"
                End If

                '    lc.FlagStandardBooking = pFlagStandBooking
                '    lc.FlagUtility = pFlagUtility

                If pPriceNotOver <> String.Empty Then
                    lc.FSTDCOST = CDbl(pPriceNotOver)
                Else
                    lc.FSTDCOST = Nothing
                End If
                If pPriceCashDiscount <> String.Empty Then
                    lc.FSTDPRICE = CDbl(pPriceCashDiscount)
                Else
                    lc.FSTDPRICE = Nothing
                End If
                If pProject <> String.Empty Then
                    lc.FMODEL = pProject
                Else
                    lc.FMODEL = Nothing
                End If
                If pFPDCode <> String.Empty Then
                    lc.FPDGRP = pFPDCode
                Else
                    lc.FPDGRP = Nothing
                End If
                lc.StmDate = DateTime.Now
            End If


            Dim lslts As List(Of BD01STB1) = db.BD01STB1.Where(Function(s) s.FPDCODE = pPromotionCode).ToList
            For Each u As BD01STB1 In lslts
                db.BD01STB1.Remove(u)
            Next

            Dim lst As BD01STB1 = New BD01STB1
            lst.FPDCODE = pPromotionCode
            lst.FROUTECODE = "001"
            lst.FPDQTY = 1
            lst.FOPCODE = ""
            If pFlagUtility Then
                lst.FOPSEQ = "00102"
            Else
                lst.FOPSEQ = "00101"
            End If
            lst.FOPSET = 0
            lst.FOPRUN =
            lst.FOPRUNUT = 0
            lst.FOPRUNSPD = 0
            lst.FOPDEL = 0
            lst.FOPQTY = 0
            lst.FRDLABOUR = 0
            lst.FRFOH = 0
            lst.FRVOH = 0
            lst.FAMATLCOST = 0
            lst.FADLABOUR = 0
            lst.FAFOH = 0
            lst.FAVOH = 0
            lst.FTMATLCOST = 0
            lst.FTDLABOUR = 0
            lst.FTFOH = 0
            lst.FTVOH = 0
            lst.FPCHARGE = 0
            lst.FACHARGE = 0
            lst.FALABOUR = 0
            lst.FSUMPBOQ = 0

            If pFlagPR Then
                lst.FBATCHSZU = 1
            Else
                lst.FBATCHSZU = 0
            End If
            If pFlagVat Then
                lst.FPCOMPLETE = 1
            Else
                lst.FPCOMPLETE = 0
            End If
            If pDecsription <> String.Empty Then
                lst.FOPDESC = pDecsription
            End If
            db.BD01STB1.Add(lst)


            Dim lslt As List(Of BD01STB2) = db.BD01STB2.Where(Function(s) s.FPDCODE = pPromotionCode).ToList
            For Each u As BD01STB2 In lslt
                db.BD01STB2.Remove(u)
            Next
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Dim strID As Integer = 0
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim strItemNo As String = String.Empty
                        If dt.Rows(i)("FlagSetAdd").ToString = "0" Then
                            If dt.Rows(i)("ProductCode").ToString <> String.Empty Then
                                strItemNo = strID
                                For ii As Integer = 0 To 4
                                    If strItemNo.Length < 5 Then
                                        strItemNo = "0" & strItemNo
                                    End If
                                Next
                                Dim m As BD01STB2 = New BD01STB2
                                m.FPDCODE = pPromotionCode
                                m.FROUTECODE = "001"
                                If pFlagUtility Then
                                    m.FOPSEQ = "00102"
                                Else
                                    m.FOPSEQ = "00101"
                                End If
                                m.FCOMPPSCR = 0
                                m.FUDL = 0
                                m.FADL = 0
                                m.FCOMPQSCR = 0
                                m.FQTYA = 0
                                m.FQTYB = 0
                                m.FQTYC = 0
                                m.FCOMPPSCRA = 0
                                m.FCOMPPSCRB = 0
                                m.FCOMPPSCRC = 0
                                m.FITEMNO = strItemNo

                                m.FCOMPCODE = dt.Rows(i)("ProductCode").ToString

                                If dt.Rows(i)("QTY").ToString <> String.Empty Then
                                    m.FQTY = CDbl(dt.Rows(i)("QTY").ToString)
                                End If

                                If dt.Rows(i)("UnitPrice").ToString <> String.Empty Then
                                    m.FUCOST = CDec(dt.Rows(i)("UnitPrice").ToString)
                                End If

                                db.BD01STB2.Add(m)
                            End If
                        End If
                        strID += 1
                    Next
                End If
            End If
            db.SaveChanges()
            Return True
        End Using
        Return False
    End Function

    Public Function LoadPromotionDetail(ByVal pPromotionCode As String,
                                        ByVal pFlagUtility As Boolean) As List(Of Promotion_ViewModel)
        Using db As New PNSWEBEntities
            '.Where(Function(s) s.FACCD = "1133001")
            If pFlagUtility = False Then
                Dim qury = From mBD01STB2 In db.BD01STB2
                           Group Join l In db.SD05PDDS.Where(Function(s) s.FENTTYPE <> "2" And s.FENTTYPE IsNot Nothing And s.FENTTYPE <> "") On mBD01STB2.FCOMPCODE Equals l.FPDCODE
                           Into ls = Group From l In ls.DefaultIfEmpty()
                           Select New Promotion_ViewModel With {
                                .FPDCODE = mBD01STB2.FPDCODE,
                                .FCOMPCODE = mBD01STB2.FCOMPCODE,
                                .FCOMPNAME = l.FPDNAME,
                                .FCOMPNAMET = l.FPDNAMET,
                                .FUNITMT = l.FUNITM,
                                .FUNITMT2 = l.FUNITMT,
                                .FQTY = mBD01STB2.FQTY,
                                .FUCOST = mBD01STB2.FUCOST,
                                .FITEMNO = mBD01STB2.FITEMNO
                            }

                If Not String.IsNullOrEmpty(pPromotionCode) Then
                    qury = qury.Where(Function(s) s.FPDCODE = pPromotionCode)
                End If

                qury = qury.OrderBy(Function(s) s.FITEMNO)

                Dim lists = qury.ToList()
                Return lists
            Else
                Dim qury = From l In db.SD05PDDS
                           Group Join mBD01STB2 In db.BD01STB2 On mBD01STB2.FPDCODE Equals l.FPDCODE
                           Into mBD01SB2 = Group From mBD01STB2 In mBD01SB2.DefaultIfEmpty()
                           Group Join mBD41CRDT In db.BD41CRDT On mBD01STB2.FCOMPCODE Equals mBD41CRDT.FCREDITCD
                           Into mBD41CRD = Group From mBD41CRDT In mBD41CRD.DefaultIfEmpty()
                           Select New Promotion_ViewModel With {
                                .FPDCODE = l.FPDCODE,
                                .FCOMPCODE = mBD01STB2.FCOMPCODE,
                                .FCOMPNAME = mBD41CRDT.FCREDITDS,
                                .FCOMPNAMET = mBD41CRDT.FCREDITDS,
                                .FQTY = mBD01STB2.FQTY,
                                .FUCOST = mBD01STB2.FUCOST,
                                .FITEMNO = mBD01STB2.FITEMNO
                            }

                If Not String.IsNullOrEmpty(pPromotionCode) Then
                    qury = qury.Where(Function(s) s.FPDCODE = pPromotionCode)
                End If

                qury = qury.OrderBy(Function(s) s.FITEMNO)

                Dim lists = qury.ToList()
                Return lists
            End If

        End Using
    End Function

    Public Function chkProduct(ByVal pArray As List(Of String),
                               ByVal pChk As Boolean) As Boolean
        Using db As New PNSWEBEntities
            Dim bool As Boolean = False
            If pChk = False Then
                Dim lc As List(Of SD05PDDS) = db.SD05PDDS.Where(Function(s) pArray.Contains(s.FPDCODE)).ToList
                If lc IsNot Nothing Then
                    If lc.Count > 0 Then
                        bool = True
                    End If
                End If
                Return bool
            Else
                Dim lc As List(Of BD41CRDT) = db.BD41CRDT.Where(Function(s) pArray.Contains(s.FCREDITCD)).ToList
                If lc IsNot Nothing Then
                    If lc.Count > 0 Then
                        bool = True
                    End If
                End If
                Return bool
            End If

        End Using
    End Function

    'Public Function LoadFPDName(ByVal pFPDCode As String) As ProductTypeByProject
    '    Using db As New PNSWEBEntities
    '        Dim lc As ProductTypeByProject = db.ProductTypeByProjects.Where(Function(s) s.FPDCODE = pFPDCode).FirstOrDefault
    '        Return lc
    '    End Using
    'End Function

#Region "Approve,UnApprove"
    Public Function CheckApprove(ByVal pPromoCode As String,
                                 ByVal pUserId As String) As BD01STB1
        Try
            Using db As New PNSWEBEntities
                Dim lc As BD01STB1 = db.BD01STB1.Where(Function(s) s.FPDCODE = pPromoCode _
                                                               And (s.FOPSEQ = "00101" Or s.FOPSEQ = "00102") _
                                                               And s.FROUTECODE = "001" _
                                                               And s.FOPCODE = "").FirstOrDefault
                Return lc
            End Using
        Catch ex As Exception
        End Try
    End Function
    Public Function Approve(ByVal pPromoCode As String,
                            ByVal pUserId As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim pendingApprove As BD01STB1 = db.BD01STB1.Where(Function(s) s.FPDCODE = pPromoCode _
                                                               And s.FOPSEQ = "00101" _
                                                               And s.FROUTECODE = "001" _
                                                               And s.FOPCODE = "").FirstOrDefault
                If pendingApprove IsNot Nothing Then
                    pendingApprove.FUPDBY = pUserId
                    pendingApprove.FUPDDATE = DateTime.Now
                    pendingApprove.FUPDFLAG = "Y"
                End If

                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function UnApprove(ByVal pPromoCode As String,
                            ByVal pUserId As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim pendingApprove As BD01STB1 = db.BD01STB1.Where(Function(s) s.FPDCODE = pPromoCode _
                                                               And s.FOPSEQ = "00101" _
                                                               And s.FROUTECODE = "001" _
                                                               And s.FOPCODE = "").SingleOrDefault
                If pendingApprove IsNot Nothing Then
                    pendingApprove.FUPDBY = Nothing
                    pendingApprove.FUPDDATE = DateTime.Now
                    pendingApprove.FUPDFLAG = Nothing
                End If

                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Dropdownlist"
    Public Function LoadProjectEdit(ByVal pProject As String) As ED01PROJ
        Using db As New PNSWEBEntities
            Dim lst As ED01PROJ = db.ED01PROJ.Where(Function(s) s.FREPRJNO = pProject).FirstOrDefault
            Return lst
        End Using
    End Function
    Public Function LoadProject() As List(Of ED01PROJ)
        Using db As New PNSWEBEntities
            Dim lc As List(Of ED01PROJ) = db.ED01PROJ.OrderBy(Function(s) s.FREPRJNO).ToList
            Return lc.Select(Function(s) New ED01PROJ With {.FREPRJNO = s.FREPRJNO, .FREPRJNM = String.Format("{0} - {1}", s.FREPRJNO, s.FREPRJNM)}).ToList()
        End Using
    End Function
#End Region

#End Region
End Class

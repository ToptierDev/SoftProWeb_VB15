Imports SPW.BLL
Imports SPW.DAL
Public Class cProject

#Region "Project.aspx"
    'Public Function ListSBU() As List(Of zProjectCategory1)

    '    Using db As New PRREntities
    '        Dim query = db.zProjectCategory1.OrderBy(Function(s) s.ProjectCatId)

    '        Return query.ToList
    '    End Using

    'End Function

    'Public Function ListBRAND() As List(Of zProjectCategory)

    '    Using db As New PRREntities
    '        Dim query = db.zProjectCategories.OrderBy(Function(s) s.ProjectCatId)

    '        Return query.ToList
    '    End Using

    'End Function

    'Public Function ListGodown() As List(Of zProjectCategory)

    '    Using db As New PRREntities
    '        Dim query = db.zProjectCategories.OrderBy(Function(s) s.ProjectCatId)

    '        Return query.ToList
    '    End Using

    'End Function


    'Public Function ListEd01proj(ByVal _freprjno As String) As ED01PROJ

    '    Using db As New PRREntities
    '        Dim query = db.ED01PROJ.Where(Function(s) s.FREPRJNO = _freprjno).OrderBy(Function(s) s.FREPRJNO)

    '        Return query.FirstOrDefault
    '    End Using

    'End Function

    'Public Function ListLd07azip(ByVal _fprovcd As String, ByVal _fcitycd As String) As LD07AZIP

    '    Using db As New PRREntities
    '        Dim query = db.LD07AZIP.Where(Function(s) s.FPROVCD = _fprovcd And s.FCITYCD = _fcitycd)

    '        Return query.FirstOrDefault
    '    End Using

    'End Function

#End Region

#Region "ORG_Project.aspx"
    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal strUserID As String) As List(Of vw_Project_join)

        Using db As New PNSWEB_SoftProEntities
            Dim lc As List(Of vw_Project_join) = db.vw_Project_join.OrderBy(Function(s) s.FREPRJNO).ToList

            Return lc
        End Using

    End Function
    Public Function GetED01PROJByID(ByVal id As String,
                                    ByVal strUserID As String) As ED01PROJ

        Using db As New PNSWEBEntities
            Dim lc As ED01PROJ = db.ED01PROJ.Where(Function(s) s.FREPRJNO = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function LoadED01PROJLAND(ByVal pCode As String) As List(Of FD11PROP)

        Using db As New PNSWEBEntities
            Dim lc As List(Of FD11PROP) = db.FD11PROP.Where(Function(s) s.FREPRJNO = pCode).ToList
            Return lc.ToList
        End Using

    End Function

    Public Function Delete(ByVal id As String,
                           ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As ED01PROJ = db.ED01PROJ.Where(Function(s) s.FREPRJNO = id).SingleOrDefault
                Dim lst As List(Of ED01PROJLAND) = db.ED01PROJLAND.Where(Function(s) s.FREPRJNO = id).ToList
                For Each u As ED01PROJLAND In lst
                    db.ED01PROJLAND.Remove(u)
                Next
                db.ED01PROJ.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Edit(ByVal pFreprjno As String,
                         ByVal chbFboiyn As Boolean,
                         ByVal pFRePrjNm As String,
                         ByVal pFrelocat1 As String,
                         ByVal pFrelocat2 As String,
                         ByVal pFrelocat3 As String,
                         ByVal pFreprovinc As String,
                         ByVal pFrepostal As String,
                         ByVal pFtotarea As String,
                         ByVal pFnoofland As String,
                         ByRef pCodeMat As String,
                         ByVal pFredesc1 As String,
                         ByVal pFredesc2 As String,
                         ByVal pFredesc3 As String,
                         ByVal p2 As String,
                         ByVal pDate0 As String,
                         ByVal pDate13 As String,
                         ByVal p11 As String,
                         ByVal pDate1 As String,
                         ByVal pDate14 As String,
                         ByVal p12 As String,
                         ByVal pDate2 As String,
                         ByVal pDate15 As String,
                         ByVal p13 As String,
                         ByVal pDate3 As String,
                         ByVal pDate16 As String,
                         ByVal p14 As String,
                         ByVal pDate4 As String,
                         ByVal pDate17 As String,
                         ByVal p15 As String,
                         ByVal pDate5 As String,
                         ByVal pDate18 As String,
                         ByVal p16 As String,
                         ByVal pDate6 As String,
                         ByVal pDate19 As String,
                         ByVal p17 As String,
                         ByVal pDate7 As String,
                         ByVal pDate20 As String,
                         ByVal p18 As String,
                         ByVal pDate8 As String,
                         ByVal pDate21 As String,
                         ByVal p19 As String,
                         ByVal pDate9 As String,
                         ByVal pDate22 As String,
                         ByVal p20 As String,
                         ByVal pDate10 As String,
                         ByVal pDate23 As String,
                         ByVal p21 As String,
                         ByVal pDate11 As String,
                         ByVal pDate24 As String,
                         ByVal p22 As String,
                         ByVal pDate12 As String,
                         ByVal pDate25 As String,
                         ByVal dt As DataTable,
                         ByVal dts As DataTable,
                         ByVal pUserId As String,
                         ByVal pFLADNO As String,
                         ByVal pFPCLANDNO As String,
                         ByVal pFTYPECODE As String,
                         ByVal pFBRAND As String,
                         ByVal pFPLANDATEST As String,
                         ByVal pFPLANDATEFN As String,
                         ByVal pFCONSTATUS As String,
                         ByVal pFCONDATESTR As String,
                         ByVal pFCONDATEFIN As String,
                         ByVal pFSALESTATUS As String,
                         ByVal pFSALEDATESTR As String,
                         ByVal pFSALEDATEFIN As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As ED01PROJ = db.ED01PROJ.Where(Function(s) s.FREPRJNO = pFreprjno).SingleOrDefault
            If lc IsNot Nothing Then
                lc.FREPRJNO = pFreprjno
                lc.FBOIYN = IIf(chbFboiyn, "Y", "N")
                lc.FREPRJNM = pFRePrjNm
                lc.FRELOCAT1 = pFrelocat1
                lc.FRELOCAT2 = pFrelocat2
                lc.FRELOCAT3 = pFrelocat3
                lc.FREPROVINC = pFreprovinc
                lc.FREPOSTAL = pFrepostal
                If pFtotarea <> String.Empty Then
                    lc.FTOTAREA = CDec(pFtotarea)
                Else
                    lc.FTOTAREA = Nothing
                End If
                If pFnoofland <> String.Empty Then
                    lc.FNOOFLAND = CDec(pFnoofland)
                Else
                    lc.FNOOFLAND = Nothing
                End If
                lc.FGDCODE = pCodeMat
                If pCodeMat <> String.Empty Then
                    Dim lcs As SD02GODN = db.SD02GODN.Where(Function(s) s.FGDCODE = lc.FGDCODE).FirstOrDefault
                    lcs.FDESC = pFRePrjNm
                    pCodeMat = pCodeMat & " - " & pFRePrjNm
                End If
                lc.FREDESC1 = pFredesc1
                lc.FREDESC2 = pFredesc2
                lc.FREDESC3 = pFredesc3
                If pFLADNO <> String.Empty Then
                    lc.FLANDNO = pFLADNO
                Else
                    lc.FLANDNO = Nothing
                End If
                If pFPCLANDNO <> String.Empty Then
                    lc.FPCLANDNO = pFPCLANDNO
                Else
                    lc.FPCLANDNO = Nothing
                End If
                '1
                lc.FREQNO = p2
                If pDate0 <> String.Empty Then
                    lc.FREQDATE = CDate(pDate0)
                Else
                    lc.FREQDATE = Nothing
                End If
                If pDate13 <> String.Empty Then
                    lc.FREQEDATE = CDate(pDate13)
                Else
                    lc.FREQEDATE = Nothing
                End If
                '2
                lc.FCONSTRNO = p11
                If pDate1 <> String.Empty Then
                    lc.FCONSTRDT = CDate(pDate1)
                Else
                    lc.FCONSTRDT = Nothing
                End If
                If pDate14 <> String.Empty Then
                    lc.FCONSTREDT = CDate(pDate14)
                Else
                    lc.FCONSTREDT = Nothing
                End If
                '3
                lc.FCONSTRNO2 = p12
                If pDate2 <> String.Empty Then
                    lc.FCONSTRDT2 = CDate(pDate2)
                Else
                    lc.FCONSTRDT2 = Nothing
                End If
                If pDate15 <> String.Empty Then
                    lc.FCONSTREDT2 = CDate(pDate15)
                Else
                    lc.FCONSTREDT2 = Nothing
                End If
                '4
                lc.FREQNO1 = p13
                If pDate3 <> String.Empty Then
                    lc.FREQDT1 = CDate(pDate3)
                Else
                    lc.FREQDT1 = Nothing
                End If
                If pDate16 <> String.Empty Then
                    lc.FREQEDT1 = CDate(pDate16)
                Else
                    lc.FREQEDT1 = Nothing
                End If
                '5
                lc.FREQNO2 = p14
                If pDate4 <> String.Empty Then
                    lc.FREQDT2 = CDate(pDate4)
                Else
                    lc.FREQDT2 = Nothing
                End If
                If pDate17 <> String.Empty Then
                    lc.FREQEDT2 = CDate(pDate17)
                Else
                    lc.FREQEDT2 = Nothing
                End If
                '6
                lc.FREQNO3 = p15
                If pDate5 <> String.Empty Then
                    lc.FREQDT3 = CDate(pDate5)
                Else
                    lc.FREQDT3 = Nothing
                End If
                If pDate18 <> String.Empty Then
                    lc.FREQEDT3 = CDate(pDate18)
                Else
                    lc.FREQEDT3 = Nothing
                End If
                '7
                lc.FREQNO4 = p16
                If pDate6 <> String.Empty Then
                    lc.FREQDT4 = CDate(pDate6)
                Else
                    lc.FREQDT4 = Nothing
                End If
                If pDate19 <> String.Empty Then
                    lc.FREQEDT4 = CDate(pDate19)
                Else
                    lc.FREQEDT4 = Nothing
                End If
                '8
                lc.FREQNO5 = p17
                If pDate7 <> String.Empty Then
                    lc.FREQDT5 = CDate(pDate7)
                Else
                    lc.FREQDT5 = Nothing
                End If
                If pDate20 <> String.Empty Then
                    lc.FREQEDT5 = CDate(pDate20)
                Else
                    lc.FREQEDT5 = Nothing
                End If
                '9
                lc.FREQNO6 = p18
                If pDate8 <> String.Empty Then
                    lc.FREQDT6 = CDate(pDate8)
                Else
                    lc.FREQDT6 = Nothing
                End If
                If pDate21 <> String.Empty Then
                    lc.FREQEDT6 = CDate(pDate21)
                Else
                    lc.FREQEDT6 = Nothing
                End If
                '10
                lc.FREQNO7 = p19
                If pDate9 <> String.Empty Then
                    lc.FREQDT7 = CDate(pDate9)
                Else
                    lc.FREQDT7 = Nothing
                End If
                If pDate22 <> String.Empty Then
                    lc.FREQEDT7 = CDate(pDate22)
                Else
                    lc.FREQEDT7 = Nothing
                End If
                '11
                lc.FREQNO8 = p20
                If pDate10 <> String.Empty Then
                    lc.FREQDT8 = CDate(pDate10)
                Else
                    lc.FREQDT8 = Nothing
                End If
                If pDate23 <> String.Empty Then
                    lc.FREQEDT8 = CDate(pDate23)
                Else
                    lc.FREQEDT8 = Nothing
                End If
                '12
                lc.FREQNO9 = p21
                If pDate11 <> String.Empty Then
                    lc.FREQDT9 = CDate(pDate11)
                Else
                    lc.FREQDT9 = Nothing
                End If
                If pDate24 <> String.Empty Then
                    lc.FREQEDT9 = CDate(pDate24)
                Else
                    lc.FREQEDT9 = Nothing
                End If
                '13
                lc.FREQNO10 = p22
                If pDate12 <> String.Empty Then
                    lc.FREQDT10 = CDate(pDate12)
                Else
                    lc.FREQDT10 = Nothing
                End If
                If pDate25 <> String.Empty Then
                    lc.FREQEDT10 = CDate(pDate25)
                Else
                    lc.FREQEDT10 = Nothing
                End If

                lc.FREQDOC = String.Empty
                lc.FCONSTRDOC = String.Empty
                lc.FCONSTRDOC1 = String.Empty
                lc.FREQDOC1 = String.Empty
                lc.FREQDOC2 = String.Empty
                lc.FREQDOC3 = String.Empty
                lc.FREQDOC4 = String.Empty
                lc.FREQDOC5 = String.Empty
                lc.FREQDOC6 = String.Empty
                lc.FREQDOC7 = String.Empty
                lc.FREQDOC8 = String.Empty
                lc.FREQDOC9 = String.Empty
                lc.FREQDOC10 = String.Empty
                lc.FLANDDOC = String.Empty
                If dts IsNot Nothing Then
                    If dts.Rows.Count > 0 Then
                        For i As Integer = 0 To dts.Rows.Count - 1
                            If dts.Rows(i)("Column").ToString = "FREQDOC" Then
                                lc.FREQDOC = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FCONSTRDOC" Then
                                lc.FCONSTRDOC = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FCONSTRDOC1" Then
                                lc.FCONSTRDOC1 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FREQDOC1" Then
                                lc.FREQDOC1 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FREQDOC2" Then
                                lc.FREQDOC2 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FREQDOC3" Then
                                lc.FREQDOC3 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FREQDOC4" Then
                                lc.FREQDOC4 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FREQDOC5" Then
                                lc.FREQDOC5 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FREQDOC6" Then
                                lc.FREQDOC6 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FREQDOC7" Then
                                lc.FREQDOC7 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FREQDOC8" Then
                                lc.FREQDOC8 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FREQDOC9" Then
                                lc.FREQDOC9 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FREQDOC10" Then
                                lc.FREQDOC10 = dts.Rows(i)("FileName").ToString
                            ElseIf dts.Rows(i)("Column").ToString = "FLANDDOC" Then
                                lc.FLANDDOC = dts.Rows(i)("FileName").ToString
                            End If
                        Next
                    End If
                End If

                If pFTYPECODE <> String.Empty Then
                    lc.FTYPECODE = pFTYPECODE
                Else
                    lc.FTYPECODE = Nothing
                End If

                If pFBRAND <> String.Empty Then
                    lc.FBRAND = pFBRAND
                Else
                    lc.FBRAND = Nothing
                End If

                If pFPLANDATEST <> String.Empty Then
                    lc.FPLANDATEST = CDate(pFPLANDATEST)
                Else
                    lc.FPLANDATEST = Nothing
                End If

                If pFPLANDATEFN <> String.Empty Then
                    lc.FPLANDATEFN = CDate(pFPLANDATEFN)
                Else
                    lc.FPLANDATEFN = Nothing
                End If

                If pFCONSTATUS <> String.Empty Then
                    lc.FCONSTATUS = pFCONSTATUS
                Else
                    lc.FCONSTATUS = Nothing
                End If

                If pFCONDATESTR <> String.Empty Then
                    lc.FCONDATESTR = CDate(pFCONDATESTR)
                Else
                    lc.FCONDATESTR = Nothing
                End If

                If pFCONDATEFIN <> String.Empty Then
                    lc.FCONDATEFIN = CDate(pFCONDATEFIN)
                Else
                    lc.FCONDATEFIN = Nothing
                End If

                If pFSALESTATUS <> String.Empty Then
                    lc.FSALESTATUS = pFSALESTATUS
                Else
                    lc.FSALESTATUS = Nothing
                End If

                If pFSALEDATESTR <> String.Empty Then
                    lc.FSALEDATESTR = CDate(pFSALEDATESTR)
                Else
                    lc.FSALEDATESTR = Nothing
                End If

                If pFSALEDATEFIN <> String.Empty Then
                    lc.FSALEDATEFIN = CDate(pFSALEDATEFIN)
                Else
                    lc.FSALEDATEFIN = Nothing
                End If


                'Dim lst As List(Of ED01PROJLAND) = db.ED01PROJLAND.Where(Function(s) s.FREPRJNO = pFreprjno).ToList
                'For Each u As ED01PROJLAND In lst
                '    db.ED01PROJLAND.Remove(u)
                'Next
                'If dt IsNot Nothing Then
                '    If dt.Rows.Count > 0 Then
                '        For i As Integer = 0 To dt.Rows.Count - 1
                '            Dim m As ED01PROJLAND = New ED01PROJLAND
                '            If dt.Rows(i)("FlagSetAdd").ToString = "0" Then
                '                m.FREPRJNO = pFreprjno

                '                If dt.Rows(i)("FLANDNO").ToString <> String.Empty Then
                '                    m.FLANDNO = dt.Rows(i)("FLANDNO").ToString
                '                End If

                '                If dt.Rows(i)("FPCLANDNO").ToString <> String.Empty Then
                '                    m.FPCLANDNO = dt.Rows(i)("FPCLANDNO").ToString
                '                End If

                '                If dt.Rows(i)("FTOTAREA").ToString <> String.Empty Then
                '                    m.FTOTAREA = dt.Rows(i)("FTOTAREA").ToString
                '                End If
                '                db.ED01PROJLAND.Add(m)
                '            End If
                '        Next
                '    End If
                'End If
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Add(ByVal pFreprjno As String,
                        ByVal chbFboiyn As Boolean,
                        ByVal pFRePrjNm As String,
                        ByVal pFrelocat1 As String,
                        ByVal pFrelocat2 As String,
                        ByVal pFrelocat3 As String,
                        ByVal pFreprovinc As String,
                        ByVal pFrepostal As String,
                        ByVal pFtotarea As String,
                        ByVal pFnoofland As String,
                        ByRef pCodeMat As String,
                        ByVal pFredesc1 As String,
                        ByVal pFredesc2 As String,
                        ByVal pFredesc3 As String,
                        ByVal p2 As String,
                        ByVal pDate0 As String,
                        ByVal pDate13 As String,
                        ByVal p11 As String,
                        ByVal pDate1 As String,
                        ByVal pDate14 As String,
                        ByVal p12 As String,
                        ByVal pDate2 As String,
                        ByVal pDate15 As String,
                        ByVal p13 As String,
                        ByVal pDate3 As String,
                        ByVal pDate16 As String,
                        ByVal p14 As String,
                        ByVal pDate4 As String,
                        ByVal pDate17 As String,
                        ByVal p15 As String,
                        ByVal pDate5 As String,
                        ByVal pDate18 As String,
                        ByVal p16 As String,
                        ByVal pDate6 As String,
                        ByVal pDate19 As String,
                        ByVal p17 As String,
                        ByVal pDate7 As String,
                        ByVal pDate20 As String,
                        ByVal p18 As String,
                        ByVal pDate8 As String,
                        ByVal pDate21 As String,
                        ByVal p19 As String,
                        ByVal pDate9 As String,
                        ByVal pDate22 As String,
                        ByVal p20 As String,
                        ByVal pDate10 As String,
                        ByVal pDate23 As String,
                        ByVal p21 As String,
                        ByVal pDate11 As String,
                        ByVal pDate24 As String,
                        ByVal p22 As String,
                        ByVal pDate12 As String,
                        ByVal pDate25 As String,
                        ByVal dt As DataTable,
                        ByVal dts As DataTable,
                        ByVal pUserId As String,
                        ByVal pFLADNO As String,
                        ByVal pFPCLANDNO As String,
                        ByVal pFTYPECODE As String,
                        ByVal pFBRAND As String,
                        ByVal pFPLANDATEST As String,
                        ByVal pFPLANDATEFN As String,
                        ByVal pFCONSTATUS As String,
                        ByVal pFCONDATESTR As String,
                        ByVal pFCONDATEFIN As String,
                        ByVal pFSALESTATUS As String,
                        ByVal pFSALEDATESTR As String,
                        ByVal pFSALEDATEFIN As String) As Boolean


        'ByVal pFTYPECODE As String, ByVal pFBRAND As String, ByVal pFPLANDATEST As String, ByVal pFPLANDATEFN As String, ByVal pFCONSTATUS As String, ByVal pFCONDATESTR As String, ByVal pFCONDATEFIN As String, ByVal pFSALESTATUS As String, ByVal pFSALEDATESTR As String, ByVal pFSALEDATEFIN As String
        'FTYPECODE, FBRAND, FPLANDATEST, FPLANDATEFN, FCONSTATUS, FCONDATESTR, FCONDATEFIN, FSALESTATUS, FSALEDATESTR, FSALEDATEFIN

        Using db As New PNSWEBEntities
            Dim lc As ED01PROJ = New ED01PROJ
            lc.FREPRJNO = pFreprjno
            lc.FBOIYN = IIf(chbFboiyn, "Y", "N")
            lc.FREPRJNM = pFRePrjNm
            lc.FRELOCAT1 = pFrelocat1
            lc.FRELOCAT2 = pFrelocat2
            lc.FRELOCAT3 = pFrelocat3
            lc.FREPROVINC = pFreprovinc
            lc.FREPOSTAL = pFrepostal
            If pFtotarea <> String.Empty Then
                lc.FTOTAREA = CDec(pFtotarea)
            End If
            If pFnoofland <> String.Empty Then
                lc.FNOOFLAND = CDec(pFnoofland)
            End If


            If pFRePrjNm <> String.Empty Then
                Dim lcs As SD02GODN = New SD02GODN
                pCodeMat = LoadCodeMatGetID()
                lcs.FGDCODE = pCodeMat
                lcs.FDESC = pFRePrjNm
                lc.FGDCODE = pCodeMat

                pCodeMat = pCodeMat & " - " & pFRePrjNm

                db.SD02GODN.Add(lcs)
            End If

            lc.FREDESC1 = pFredesc1
            lc.FREDESC2 = pFredesc2
            lc.FREDESC3 = pFredesc3

            If pFLADNO <> String.Empty Then
                lc.FLANDNO = pFLADNO
            Else
                lc.FLANDNO = Nothing
            End If
            If pFPCLANDNO <> String.Empty Then
                lc.FPCLANDNO = pFPCLANDNO
            Else
                lc.FPCLANDNO = Nothing
            End If
            '1
            lc.FREQNO = p2
            If pDate0 <> String.Empty Then
                lc.FREQDATE = CDate(pDate0)
            End If
            If pDate13 <> String.Empty Then
                lc.FREQEDATE = CDate(pDate13)
            End If
            '2
            lc.FCONSTRNO = p11
            If pDate1 <> String.Empty Then
                lc.FCONSTRDT = CDate(pDate1)
            End If
            If pDate14 <> String.Empty Then
                lc.FCONSTREDT = CDate(pDate14)
            End If
            '3
            lc.FCONSTRNO2 = p12
            If pDate2 <> String.Empty Then
                lc.FCONSTRDT2 = CDate(pDate2)
            End If
            If pDate15 <> String.Empty Then
                lc.FCONSTREDT2 = CDate(pDate15)
            End If
            '4
            lc.FREQNO1 = p13
            If pDate3 <> String.Empty Then
                lc.FREQDT1 = CDate(pDate3)
            End If
            If pDate16 <> String.Empty Then
                lc.FREQEDT1 = CDate(pDate16)
            End If
            '5
            lc.FREQNO2 = p14
            If pDate4 <> String.Empty Then
                lc.FREQDT2 = CDate(pDate4)
            End If
            If pDate17 <> String.Empty Then
                lc.FREQEDT2 = CDate(pDate17)
            End If
            '6
            lc.FREQNO3 = p15
            If pDate5 <> String.Empty Then
                lc.FREQDT3 = CDate(pDate5)
            End If
            If pDate18 <> String.Empty Then
                lc.FREQEDT3 = CDate(pDate18)
            End If
            '7
            lc.FREQNO4 = p16
            If pDate6 <> String.Empty Then
                lc.FREQDT4 = CDate(pDate6)
            End If
            If pDate19 <> String.Empty Then
                lc.FREQEDT4 = CDate(pDate19)
            End If
            '8
            lc.FREQNO5 = p17
            If pDate7 <> String.Empty Then
                lc.FREQDT5 = CDate(pDate7)
            End If
            If pDate20 <> String.Empty Then
                lc.FREQEDT5 = CDate(pDate20)
            End If
            '9
            lc.FREQNO6 = p18
            If pDate8 <> String.Empty Then
                lc.FREQDT6 = CDate(pDate8)
            End If
            If pDate21 <> String.Empty Then
                lc.FREQEDT6 = CDate(pDate21)
            End If
            '10
            lc.FREQNO7 = p19
            If pDate9 <> String.Empty Then
                lc.FREQDT7 = CDate(pDate9)
            End If
            If pDate22 <> String.Empty Then
                lc.FREQEDT7 = CDate(pDate22)
            End If
            '11
            lc.FREQNO8 = p20
            If pDate10 <> String.Empty Then
                lc.FREQDT8 = CDate(pDate10)
            End If
            If pDate23 <> String.Empty Then
                lc.FREQEDT8 = CDate(pDate23)
            End If
            '12
            lc.FREQNO9 = p21
            If pDate11 <> String.Empty Then
                lc.FREQDT9 = CDate(pDate11)
            End If
            If pDate24 <> String.Empty Then
                lc.FREQEDT9 = CDate(pDate24)
            End If
            '13
            lc.FREQNO10 = p22
            If pDate12 <> String.Empty Then
                lc.FREQDT10 = CDate(pDate12)
            End If
            If pDate25 <> String.Empty Then
                lc.FREQEDT10 = CDate(pDate25)
            End If

            lc.FREQDOC = String.Empty
            lc.FCONSTRDOC = String.Empty
            lc.FCONSTRDOC1 = String.Empty
            lc.FREQDOC1 = String.Empty
            lc.FREQDOC2 = String.Empty
            lc.FREQDOC3 = String.Empty
            lc.FREQDOC4 = String.Empty
            lc.FREQDOC5 = String.Empty
            lc.FREQDOC6 = String.Empty
            lc.FREQDOC7 = String.Empty
            lc.FREQDOC8 = String.Empty
            lc.FREQDOC9 = String.Empty
            lc.FREQDOC10 = String.Empty
            lc.FLANDDOC = String.Empty

            If dts IsNot Nothing Then
                If dts.Rows.Count > 0 Then

                    For i As Integer = 0 To dts.Rows.Count - 1
                        If dts.Rows(i)("Column").ToString = "FREQDOC" Then
                            lc.FREQDOC = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FCONSTRDOC" Then
                            lc.FCONSTRDOC = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FCONSTRDOC1" Then
                            lc.FCONSTRDOC1 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FREQDOC1" Then
                            lc.FREQDOC1 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FREQDOC2" Then
                            lc.FREQDOC2 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FREQDOC3" Then
                            lc.FREQDOC3 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FREQDOC4" Then
                            lc.FREQDOC4 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FREQDOC5" Then
                            lc.FREQDOC5 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FREQDOC6" Then
                            lc.FREQDOC6 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FREQDOC7" Then
                            lc.FREQDOC7 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FREQDOC8" Then
                            lc.FREQDOC8 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FREQDOC9" Then
                            lc.FREQDOC9 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FREQDOC10" Then
                            lc.FREQDOC10 = dts.Rows(i)("FileName").ToString
                        ElseIf dts.Rows(i)("Column").ToString = "FLANDDOC" Then
                            lc.FLANDDOC = dts.Rows(i)("FileName").ToString
                        End If
                    Next
                End If
            End If

            If pFTYPECODE <> String.Empty Then
                lc.FTYPECODE = pFTYPECODE
            Else
                lc.FTYPECODE = Nothing
            End If

            If pFBRAND <> String.Empty Then
                lc.FBRAND = pFBRAND
            Else
                lc.FBRAND = Nothing
            End If

            If pFPLANDATEST <> String.Empty Then
                lc.FPLANDATEST = CDate(pFPLANDATEST)
            Else
                lc.FPLANDATEST = Nothing
            End If

            If pFPLANDATEFN <> String.Empty Then
                lc.FPLANDATEFN = CDate(pFPLANDATEFN)
            Else
                lc.FPLANDATEFN = Nothing
            End If

            If pFCONSTATUS <> String.Empty Then
                lc.FCONSTATUS = pFCONSTATUS
            Else
                lc.FCONSTATUS = Nothing
            End If

            If pFCONDATESTR <> String.Empty Then
                lc.FCONDATESTR = CDate(pFCONDATESTR)
            Else
                lc.FCONDATESTR = Nothing
            End If

            If pFCONDATEFIN <> String.Empty Then
                lc.FCONDATEFIN = CDate(pFCONDATEFIN)
            Else
                lc.FCONDATEFIN = Nothing
            End If

            If pFSALESTATUS <> String.Empty Then
                lc.FSALESTATUS = pFSALESTATUS
            Else
                lc.FSALESTATUS = Nothing
            End If

            If pFSALEDATESTR <> String.Empty Then
                lc.FSALEDATESTR = CDate(pFSALEDATESTR)
            Else
                lc.FSALEDATESTR = Nothing
            End If

            If pFSALEDATEFIN <> String.Empty Then
                lc.FSALEDATEFIN = CDate(pFSALEDATEFIN)
            Else
                lc.FSALEDATEFIN = Nothing
            End If


            'Dim lst As List(Of ED01PROJLAND) = db.ED01PROJLAND.Where(Function(s) s.FREPRJNO = pFreprjno).ToList
            'For Each u As ED01PROJLAND In lst
            '    db.ED01PROJLAND.Remove(u)
            'Next
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        For i As Integer = 0 To dt.Rows.Count - 1
            '            Dim m As ED01PROJLAND = New ED01PROJLAND
            '            If dt.Rows(i)("FlagSetAdd").ToString = "0" Then
            '                m.FREPRJNO = pFreprjno

            '                If dt.Rows(i)("FLANDNO").ToString <> String.Empty Then
            '                    m.FLANDNO = dt.Rows(i)("FLANDNO").ToString
            '                End If

            '                If dt.Rows(i)("FPCLANDNO").ToString <> String.Empty Then
            '                    m.FPCLANDNO = dt.Rows(i)("FPCLANDNO").ToString
            '                End If

            '                If dt.Rows(i)("FTOTAREA").ToString <> String.Empty Then
            '                    m.FTOTAREA = dt.Rows(i)("FTOTAREA").ToString
            '                End If
            '                db.ED01PROJLAND.Add(m)
            '            End If
            '        Next
            '    End If
            'End If

            db.ED01PROJ.Add(lc)
            db.SaveChanges()
            Return True
        End Using
    End Function

    Public Function LoadCodeMatGetID() As String
        Using db As New PNSWEBEntities
            Dim pID As String = String.Empty
            Dim lcMax As SD02GODN = db.SD02GODN.Where(Function(s) s.FGDCODE <> "99").OrderByDescending(Function(s) CInt(s.FGDCODE)).FirstOrDefault
            If lcMax IsNot Nothing Then
                pID = CInt(lcMax.FGDCODE) + 1
            End If
            Return pID
        End Using
    End Function
    Public Function LoadProjectType() As List(Of SD03TYPE)
        Using db As New PNSWEBEntities
            Dim lc As List(Of SD03TYPE) = db.SD03TYPE.Where(Function(s) s.FENTTYPE = 1).OrderBy(Function(s) s.FTYCODE).ToList
            Return lc
        End Using
    End Function

#Region "Dropdownlist"
    Public Function LoadCodeMatEdit(ByVal pID As String) As SD02GODN
        Using db As New PNSWEBEntities
            Dim lc As SD02GODN = db.SD02GODN.Where(Function(s) s.FGDCODE = pID).OrderBy(Function(s) s.FGDCODE).FirstOrDefault
            Return lc
        End Using
    End Function

    Public Function LoadCodeMat(ByVal pKeyID As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                Dim qury = (From m In db.SD02GODN.OrderBy(Function(s) s.FGDCODE)
                            Select m.FGDCODE & "-" & m.FDESC).ToList()

                Return qury.ToList()
            ElseIf pKeyID <> String.Empty Then
                Dim qury = (From m In db.SD02GODN.OrderBy(Function(s) s.FGDCODE)
                            Select m.FGDCODE & "-" & m.FDESC) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                Return qury.ToList()
            End If
            Return Nothing
        End Using
    End Function
#End Region
#Region "Cdate(SubDate"
    Function SubDate(ByVal TempDate As String) As String
        TempDate = TempDate.Split("/")(1) & "/" & TempDate.Split("/")(0) & "/" & TempDate.Split("/")(2)
        Return TempDate
    End Function
#End Region

#End Region

End Class

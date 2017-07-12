Imports SPW.BLL
Imports SPW.DAL
Public Class cCustomerInformation
#Region "TRN_CustomerInformation.aspx"

    Public Function LoadLD07AZIP(ByVal pFPROVINCE As String,
                                 ByVal pFCITY As String,
                                 ByVal pFPOSTAL As String) As LD07AZIP

        Using db As New PNSWEBEntities
            Dim query = db.LD07AZIP.Where(Function(s) s.FPROVINCE = pFPROVINCE And s.FCITY = pFCITY And s.FPOSTAL = pFPOSTAL)

            Return query.FirstOrDefault
        End Using
    End Function

    Public Function LoadFPROVINCECode(ByVal pFPROVINCE As String) As LD07AZIP

        Using db As New PNSWEBEntities
            Dim query = db.LD07AZIP.Where(Function(s) s.FPROVINCE = pFPROVINCE)

            Return query.FirstOrDefault
        End Using
    End Function

    Public Function LoadFCITYCode(ByVal pFCITY As String) As LD07AZIP

        Using db As New PNSWEBEntities
            Dim query = db.LD07AZIP.Where(Function(s) s.FCITY = pFCITY)

            Return query.FirstOrDefault
        End Using
    End Function



    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal strUserID As String) As List(Of OD50RCVD)

        Dim ctx As New PNSWEBEntities
        Dim qury = From lc In ctx.OD50RCVD.Where(Function(s) 1 = 1)

        If Not String.IsNullOrEmpty(fillter.Keyword) Then
            qury = qury.Where(Function(s) s.FCONTCODE.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.FCONTENM.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.FCONTTNM.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.FTELNO.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")) Or
                                              s.FPEOPLEID.ToUpper.Replace(" ", "").Contains(fillter.Keyword.ToUpper.Replace(" ", "")))
        End If


        Select Case fillter.SortType '0-None , 1 -Asc , 2 - Desc
            Case 1
                If fillter.SortBy.ToLower = "fcontcode" Then
                    qury = qury.OrderBy(Function(s) s.FCONTCODE)
                ElseIf fillter.SortBy.ToLower = "fcontenm" Then
                    qury = qury.OrderBy(Function(s) s.FCONTENM)
                ElseIf fillter.SortBy.ToLower = "fconttnm" Then
                    qury = qury.OrderBy(Function(s) s.FCONTTNM)
                ElseIf fillter.SortBy.ToLower = "ftelno" Then
                    qury = qury.OrderBy(Function(s) s.FTELNO)
                ElseIf fillter.SortBy.ToLower = "fpeopleid" Then
                    qury = qury.OrderBy(Function(s) s.FPEOPLEID)
                End If
                Exit Select
            Case 2
                If fillter.SortBy.ToLower = "fcontcode" Then
                    qury = qury.OrderByDescending(Function(s) s.FCONTCODE)
                ElseIf fillter.SortBy.ToLower = "fcontenm" Then
                    qury = qury.OrderByDescending(Function(s) s.FCONTENM)
                ElseIf fillter.SortBy.ToLower = "fconttnm" Then
                    qury = qury.OrderByDescending(Function(s) s.FCONTTNM)
                ElseIf fillter.SortBy.ToLower = "ftelno" Then
                    qury = qury.OrderByDescending(Function(s) s.FTELNO)
                ElseIf fillter.SortBy.ToLower = "fpeopleid" Then
                    qury = qury.OrderByDescending(Function(s) s.FPEOPLEID)
                End If
                Exit Select
            Case Else
                qury = qury.OrderBy(Function(s) s.FCONTCODE)
                Exit Select
        End Select

        If qury IsNot Nothing Then
            TotalRow = qury.Count
        End If

        If (fillter.PageSize > 0 And fillter.Page >= 0) Then
            qury = qury.Skip((fillter.Page - 1) * fillter.PageSize).Take(fillter.PageSize)
        End If

        Return qury.ToList

    End Function

    Public Function LoadPeopleIDMaster() As List(Of String)

        Using db As New PNSWEBEntities
            Dim query As List(Of String) = db.OD50RCVD.Where(Function(s) s.FPEOPLEID IsNot Nothing).Select(Function(s) s.FPEOPLEID).Distinct.ToList

            Return query
        End Using
    End Function

    Public Function LoadEditCusInfo(ByVal id As String,
                                 ByVal pUserID As String) As OD50RCVD

        Using db As New PNSWEBEntities
            Dim query = db.OD50RCVD.Where(Function(s) s.FCONTCODE = id)

            Return query.FirstOrDefault
        End Using
    End Function

    Public Function Delete(ByVal id As String,
                           ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As OD50RCVD = db.OD50RCVD.Where(Function(s) s.FCONTCODE = id).SingleOrDefault
                db.OD50RCVD.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Edit(ByVal pFCONTCODE As String,
                         ByVal pFMEMBERCD As String,
                         ByVal pFPRECODE As String,
                         ByVal pFPRELEN As String,
                         ByVal pFCONTTNM As String,
                         ByVal pFCONTENM As String,
                         ByVal pFBIRTH As String,
                         ByVal pFSEX As String,
                         ByVal pFADD1 As String,
                         ByVal pFADD2 As String,
                         ByVal pFADD3 As String,
                         ByVal pFPROVINCE As String,
                         ByVal pFPOSTAL As String,
                         ByVal pFPROVCD As String,
                         ByVal pFCITYCD As String,
                         ByVal pFTELNO As String,
                         ByVal pFFAX As String,
                         ByVal pFEMAIL As String,
                         ByVal pFMOBILE As String,
                         ByVal pFPEOPLEID As String,
                         ByVal pFMD As String,
                         ByVal pFCONT As String,
                         ByVal pFTELC As String,
                         ByVal pFHADD1 As String,
                         ByVal pFHADD2 As String,
                         ByVal pFHADD3 As String,
                         ByVal pFHPROVINCE As String,
                         ByVal pFHPOSTAL As String,
                         ByVal pFHPROVCD As String,
                         ByVal pFHCITYCD As String,
                         ByVal pFHTEL As String,
                         ByVal pFOFFNAME As String,
                         ByVal pFOFFADD1 As String,
                         ByVal pFOFFADD2 As String,
                         ByVal pFOFFADD3 As String,
                         ByVal pFOFFPROV As String,
                         ByVal pFOFFZIP As String,
                         ByVal pFOFFPROVCD As String,
                         ByVal pFOFFCITYCD As String,
                         ByVal pFOFFTEL As String,
                         ByVal pFOFFICETYPE As String,
                         ByVal pFPOSITION As String,
                         ByVal pFMARSTAT As String,
                         ByVal pFMARDATE As String,
                         ByVal pFSTDATE As String,
                         ByVal pFMEMBEREXP As String,
                         ByVal pFPDISC As String,
                         ByVal pFNMCODE As String,
                         ByVal pFAINCOME As String,
                         ByVal pFFNDBUDG As String,
                         ByVal pFFNDLOCAT As String,
                         ByVal pFLIKEYN As String,
                         ByVal pFLIKEREAS As String,
                         ByVal pFLIKETYPE As String,
                         ByVal pFLIKERSDS As String,
                         ByVal pFMEDIACD As String,
                         ByVal pFSALESAREA As String,
                         ByVal pFCUCATE As String,
                         ByVal pFTRNNO As String,
                         ByVal pFTRNITEM As String,
                         ByVal pFCONTTYPE As String,
                         ByVal pFPRNMARK As String,
                         ByVal pFBILLADD1 As String,
                         ByVal pFBILLADD2 As String,
                         ByVal pFBILLADD3 As String,
                         ByVal pFBILLPROV As String,
                         ByVal pFBILLZIP As String,
                         ByVal pFBILLTO As String,
                         ByVal pFBILLYN As String,
                         ByVal pFBILLNMNO As String,
                         ByVal pFENTDATE As String,
                         ByVal pFENTBY As String,
                         ByVal pFENTTYPE As String,
                         ByVal pFUPDDATE As String,
                         ByVal pFUPDBY As String,
                         ByVal pFOFFCODE As String,
                         ByVal pFNOTE As String,
                         ByVal pFISGROUP As String,
                         ByVal pFSPECNM As String,
                         ByVal pFREPRJNO As String,
                         ByVal pFPDCODE As String,
                         ByVal pFRECBY As String,
                         ByVal pFNATION As String,
                         ByVal pFLEADATE As String,
                         ByVal pFEXPDATE As String,
                         ByVal pFPRNAME As String,
                         ByVal pFMRNAME As String,
                         ByVal pFMNATION As String,
                         ByVal pFSTATUS As String,
                         ByVal pFSALECODE As String,
                         ByVal pINCOME As String,
                         ByVal pGRADE As String,
                         ByVal pFLOTNO As String,
                         ByVal pBUDGET As String,
                         ByVal pCOMPARE As String,
                         ByVal pCALLIN As String,
                         ByVal pWALKIN As String,
                         ByVal pCALLCENTER As String,
                         ByVal pFDATE1 As String,
                         ByVal pFDATE2 As String,
                         ByVal pFDATE3 As String,
                         ByVal pFDATE4 As String,
                         ByVal pFDESC1 As String,
                         ByVal pFDESC2 As String,
                         ByVal pFDESC3 As String,
                         ByVal pFDESC4 As String,
                         ByVal pFROAD As String,
                         ByVal pIsThailandor As String,
                         ByVal pSAPTaxCode As String,
                         ByVal pStmDate As String,
                         ByVal pLastUpDate As String,
                         ByVal pFMOBILE2 As String,
                         ByVal pFLINE As String,
                         ByVal pFFACEBOOK As String,
                         ByVal pTaxNo As String,
                         ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As OD50RCVD = db.OD50RCVD.Where(Function(s) s.FCONTCODE = pFCONTCODE).SingleOrDefault
            If lc IsNot Nothing Then
                Dim _date As Date = Date.Now
                lc.FCONTCODE = pFCONTCODE
                lc.FMEMBERCD = pFMEMBERCD
                lc.FPRECODE = IIf(pFPRECODE = "True", "0", "1")
                If pFPRELEN <> String.Empty Then
                    lc.FPRELEN = CDec(pFPRELEN)
                Else
                    lc.FPRELEN = 0
                End If
                lc.FCONTTNM = pFCONTTNM
                lc.FCONTENM = pFCONTENM
                If pFBIRTH <> String.Empty Then
                    lc.FBIRTH = Date.ParseExact(pFBIRTH, "dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
                Else
                    lc.FBIRTH = Nothing
                End If
                If pFSEX <> String.Empty Then
                    lc.FSEX = pFSEX
                Else
                    lc.FSEX = Nothing
                End If
                If pFADD1 <> String.Empty Then
                    lc.FADD1 = pFADD1
                Else
                    lc.FADD1 = Nothing
                End If
                If pFADD2 <> String.Empty Then
                    lc.FADD2 = pFADD2
                Else
                    lc.FADD2 = Nothing
                End If
                If pFADD3 <> String.Empty Then
                    lc.FADD3 = pFADD3
                Else
                    lc.FADD3 = Nothing
                End If
                If pFPROVINCE <> String.Empty Then
                    lc.FPROVINCE = pFPROVINCE
                Else
                    lc.FPROVINCE = Nothing
                End If
                If pFPOSTAL <> String.Empty Then
                    lc.FPOSTAL = pFPOSTAL
                Else
                    lc.FPOSTAL = Nothing
                End If
                If pFPROVCD <> String.Empty Then
                    lc.FPROVCD = pFPROVCD
                Else
                    lc.FPROVCD = Nothing
                End If
                If pFCITYCD <> String.Empty Then
                    lc.FCITYCD = pFCITYCD
                Else
                    lc.FCITYCD = Nothing
                End If
                If pFTELNO <> String.Empty Then
                    lc.FTELNO = pFTELNO
                Else
                    lc.FTELNO = Nothing
                End If
                If pFFAX <> String.Empty Then
                    lc.FFAX = pFFAX
                Else
                    lc.FFAX = Nothing
                End If
                If pFEMAIL <> String.Empty Then
                    lc.FEMAIL = pFEMAIL
                Else
                    lc.FEMAIL = Nothing
                End If
                If pFMOBILE <> String.Empty Then
                    lc.FMOBILE = pFMOBILE
                Else
                    lc.FMOBILE = Nothing
                End If
                If pFPEOPLEID <> String.Empty Then
                    lc.FPEOPLEID = pFPEOPLEID.Replace(" ", "")
                Else
                    lc.FPEOPLEID = Nothing
                End If
                If pFMD <> String.Empty Then
                    lc.FMD = pFMD
                Else
                    lc.FMD = Nothing
                End If
                If pFCONT <> String.Empty Then
                    lc.FCONT = pFCONT
                Else
                    lc.FCONT = Nothing
                End If
                If pFTELC <> String.Empty Then
                    lc.FTELC = pFTELC
                Else
                    lc.FTELC = Nothing
                End If
                If pFHADD1 <> String.Empty Then
                    lc.FHADD1 = pFHADD1
                Else
                    lc.FHADD1 = Nothing
                End If
                If pFHADD2 <> String.Empty Then
                    lc.FHADD2 = pFHADD2
                Else
                    lc.FHADD2 = Nothing
                End If
                If pFHADD3 <> String.Empty Then
                    lc.FHADD3 = pFHADD3
                Else
                    lc.FHADD3 = Nothing
                End If
                If pFHPROVINCE <> String.Empty Then
                    lc.FHPROVINCE = pFHPROVINCE
                Else
                    lc.FHPROVINCE = Nothing
                End If
                If pFHPOSTAL <> String.Empty Then
                    lc.FHPOSTAL = pFHPOSTAL
                Else
                    lc.FHPOSTAL = Nothing
                End If
                If pFHPROVCD <> String.Empty Then
                    lc.FHPROVCD = pFHPROVCD
                Else
                    lc.FHPROVCD = Nothing
                End If
                If pFHCITYCD <> String.Empty Then
                    lc.FHCITYCD = pFHCITYCD
                Else
                    lc.FHCITYCD = Nothing
                End If
                If pFHTEL <> String.Empty Then
                    lc.FHTEL = pFHTEL
                Else
                    lc.FHTEL = Nothing
                End If
                If pFOFFNAME <> String.Empty Then
                    lc.FOFFNAME = pFOFFNAME
                Else
                    lc.FOFFNAME = Nothing
                End If
                If pFOFFADD1 <> String.Empty Then
                    lc.FOFFADD1 = pFOFFADD1
                Else
                    lc.FOFFADD1 = Nothing
                End If
                If pFOFFADD2 <> String.Empty Then
                    lc.FOFFADD2 = pFOFFADD2
                Else
                    lc.FOFFADD2 = Nothing
                End If
                If pFOFFADD3 <> String.Empty Then
                    lc.FOFFADD3 = pFOFFADD3
                Else
                    lc.FOFFADD3 = Nothing
                End If
                If pFOFFPROV <> String.Empty Then
                    lc.FOFFPROV = pFOFFPROV
                Else
                    lc.FOFFPROV = Nothing
                End If
                If pFOFFZIP <> String.Empty Then
                    lc.FOFFZIP = pFOFFZIP
                Else
                    lc.FOFFZIP = Nothing
                End If
                If pFOFFPROVCD <> String.Empty Then
                    lc.FOFFPROVCD = pFOFFPROVCD
                Else
                    lc.FOFFPROVCD = Nothing
                End If
                If pFOFFCITYCD <> String.Empty Then
                    lc.FOFFCITYCD = pFOFFCITYCD
                Else
                    lc.FOFFCITYCD = Nothing
                End If
                If pFOFFTEL <> String.Empty Then
                    lc.FOFFTEL = pFOFFTEL
                Else
                    lc.FOFFTEL = Nothing
                End If
                If pFOFFICETYPE <> String.Empty Then
                    lc.FOFFICETYPE = pFOFFICETYPE
                Else
                    lc.FOFFICETYPE = Nothing
                End If
                If pFPOSITION <> String.Empty Then
                    lc.FPOSITION = pFPOSITION
                Else
                    lc.FPOSITION = Nothing
                End If
                If pFMARSTAT <> String.Empty Then
                    lc.FMARSTAT = pFMARSTAT
                Else
                    lc.FMARSTAT = Nothing
                End If
                If pFMARDATE <> String.Empty Then
                    lc.FMARDATE = Date.ParseExact(pFMARDATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Else
                    lc.FMARDATE = Nothing
                End If
                If pFSTDATE <> String.Empty Then
                    lc.FSTDATE = Date.ParseExact(pFSTDATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Else
                    lc.FSTDATE = Nothing
                End If
                If pFMEMBEREXP <> String.Empty Then
                    lc.FMEMBEREXP = Date.ParseExact(pFMEMBEREXP, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Else
                    lc.FMEMBEREXP = Nothing
                End If
                If pFPDISC <> String.Empty Then
                    lc.FPDISC = CDec(pFPDISC)
                Else
                    lc.FPDISC = Nothing
                End If
                If pFNMCODE <> String.Empty Then
                    lc.FNMCODE = pFNMCODE
                Else
                    lc.FNMCODE = Nothing
                End If
                If pFAINCOME <> String.Empty Then
                    lc.FAINCOME = CDec(pFAINCOME)
                Else
                    lc.FAINCOME = Nothing
                End If
                If pFFNDBUDG <> String.Empty Then
                    lc.FFNDBUDG = CDec(pFFNDBUDG)
                Else
                    lc.FFNDBUDG = Nothing
                End If
                If pFFNDLOCAT <> String.Empty Then
                    lc.FFNDLOCAT = pFFNDLOCAT
                Else
                    lc.FFNDLOCAT = Nothing
                End If
                If pFLIKEYN <> String.Empty Then
                    lc.FLIKEYN = pFLIKEYN
                Else
                    lc.FLIKEYN = Nothing
                End If
                If pFLIKEREAS <> String.Empty Then
                    lc.FLIKEREAS = pFLIKEREAS
                Else
                    lc.FLIKEREAS = Nothing
                End If
                If pFLIKETYPE <> String.Empty Then
                    lc.FLIKETYPE = pFLIKETYPE
                Else
                    lc.FLIKETYPE = Nothing
                End If
                If pFLIKERSDS <> String.Empty Then
                    lc.FLIKERSDS = pFLIKERSDS
                Else
                    lc.FLIKERSDS = Nothing
                End If
                If pFMEDIACD <> String.Empty Then
                    lc.FMEDIACD = pFMEDIACD
                Else
                    lc.FMEDIACD = Nothing
                End If
                If pFSALESAREA <> String.Empty Then
                    lc.FSALESAREA = pFSALESAREA
                Else
                    lc.FSALESAREA = Nothing
                End If
                If pFCUCATE <> String.Empty Then
                    lc.FCUCATE = pFCUCATE
                Else
                    lc.FCUCATE = Nothing
                End If
                If pFTRNNO <> String.Empty Then
                    lc.FTRNNO = pFTRNNO
                Else
                    lc.FTRNNO = Nothing
                End If
                If pFTRNITEM <> String.Empty Then
                    lc.FTRNITEM = pFTRNITEM
                Else
                    lc.FTRNITEM = Nothing
                End If
                If pFCONTTYPE <> String.Empty Then
                    lc.FCONTTYPE = pFCONTTYPE
                Else
                    lc.FCONTTYPE = Nothing
                End If
                If pFPRNMARK <> String.Empty Then
                    lc.FPRNMARK = pFPRNMARK
                Else
                    lc.FPRNMARK = Nothing
                End If
                If pFBILLADD1 <> String.Empty Then
                    lc.FBILLADD1 = pFBILLADD1
                Else
                    lc.FBILLADD1 = Nothing
                End If
                If pFBILLADD2 <> String.Empty Then
                    lc.FBILLADD2 = pFBILLADD2
                Else
                    lc.FBILLADD2 = Nothing
                End If
                If pFBILLADD3 <> String.Empty Then
                    lc.FBILLADD3 = pFBILLADD3
                Else
                    lc.FBILLADD3 = Nothing
                End If
                If pFBILLPROV <> String.Empty Then
                    lc.FBILLPROV = pFBILLPROV
                Else
                    lc.FBILLPROV = Nothing
                End If
                If pFBILLZIP <> String.Empty Then
                    lc.FBILLZIP = pFBILLZIP
                Else
                    lc.FBILLZIP = Nothing
                End If
                If pFBILLTO <> String.Empty Then
                    lc.FBILLTO = pFBILLTO
                Else
                    lc.FBILLTO = Nothing
                End If
                If pFBILLYN <> String.Empty Then
                    lc.FBILLYN = pFBILLYN
                Else
                    lc.FBILLYN = Nothing
                End If
                If pFBILLNMNO <> String.Empty Then
                    lc.FBILLNMNO = pFBILLNMNO
                Else
                    lc.FBILLNMNO = Nothing
                End If
                If pFENTDATE <> String.Empty Then
                    lc.FENTDATE = Date.ParseExact(pFENTDATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Else
                    lc.FENTDATE = Nothing
                End If
                If pFENTBY <> String.Empty Then
                    lc.FENTBY = pFENTBY
                Else
                    lc.FENTBY = Nothing
                End If
                If pFENTTYPE <> String.Empty Then
                    lc.FENTTYPE = pFENTTYPE
                Else
                    lc.FENTTYPE = Nothing
                End If
                lc.FUPDDATE = _date
                lc.FUPDBY = pUserId
                'If pFUPDDATE <> String.Empty Then
                '    lc.FUPDDATE =  Date.ParseExact(pFUPDDATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                'Else
                '    lc.FUPDDATE = Nothing
                'End If
                'If pFUPDBY <> String.Empty Then
                '    lc.FUPDBY = pFUPDBY
                'Else
                '    lc.FUPDBY = Nothing
                'End If
                If pFOFFCODE <> String.Empty Then
                    lc.FOFFCODE = pFOFFCODE
                Else
                    lc.FOFFCODE = Nothing
                End If
                If pFNOTE <> String.Empty Then
                    lc.FNOTE = pFNOTE
                Else
                    lc.FNOTE = Nothing
                End If
                If pFISGROUP <> String.Empty Then
                    lc.FISGROUP = pFISGROUP
                Else
                    lc.FISGROUP = Nothing
                End If
                If pFSPECNM <> String.Empty Then
                    lc.FSPECNM = pFSPECNM
                Else
                    lc.FSPECNM = Nothing
                End If
                If pFREPRJNO <> String.Empty Then
                    lc.FREPRJNO = pFREPRJNO
                Else
                    lc.FREPRJNO = Nothing
                End If
                If pFPDCODE <> String.Empty Then
                    lc.FPDCODE = pFPDCODE
                Else
                    lc.FPDCODE = Nothing
                End If
                If pFRECBY <> String.Empty Then
                    lc.FRECBY = pFRECBY
                Else
                    lc.FRECBY = Nothing
                End If
                If pFNATION <> String.Empty Then
                    lc.FNATION = pFNATION
                Else
                    lc.FNATION = Nothing
                End If
                If pFLEADATE <> String.Empty Then
                    lc.FLEADATE = Date.ParseExact(pFLEADATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Else
                    lc.FLEADATE = Nothing
                End If
                If pFEXPDATE <> String.Empty Then
                    lc.FEXPDATE = Date.ParseExact(pFEXPDATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Else
                    lc.FEXPDATE = Nothing
                End If
                If pFPRNAME <> String.Empty Then
                    lc.FPRNAME = pFPRNAME
                Else
                    lc.FPRNAME = Nothing
                End If
                If pFMRNAME <> String.Empty Then
                    lc.FMRNAME = pFMRNAME
                Else
                    lc.FMRNAME = Nothing
                End If
                If pFMNATION <> String.Empty Then
                    lc.FMNATION = pFMNATION
                Else
                    lc.FMNATION = Nothing
                End If
                If pFSTATUS <> String.Empty Then
                    lc.FSTATUS = pFSTATUS
                Else
                    lc.FSTATUS = Nothing
                End If
                If pFSALECODE <> String.Empty Then
                    lc.FSALECODE = pFSALECODE
                Else
                    lc.FSALECODE = Nothing
                End If
                If pINCOME <> String.Empty Then
                    lc.INCOME = pINCOME
                Else
                    lc.INCOME = Nothing
                End If
                If pGRADE <> String.Empty Then
                    lc.GRADE = pGRADE
                Else
                    lc.GRADE = Nothing
                End If
                If pFLOTNO <> String.Empty Then
                    lc.FLOTNO = pFLOTNO
                Else
                    lc.FLOTNO = Nothing
                End If
                If pBUDGET <> String.Empty Then
                    lc.BUDGET = pBUDGET
                Else
                    lc.BUDGET = Nothing
                End If
                If pCOMPARE <> String.Empty Then
                    lc.COMPARE = pCOMPARE
                Else
                    lc.COMPARE = Nothing
                End If
                If pCALLIN <> String.Empty Then
                    lc.CALLIN = CInt(pCALLIN)
                Else
                    lc.CALLIN = Nothing
                End If
                If pWALKIN <> String.Empty Then
                    lc.WALKIN = CInt(pWALKIN)
                Else
                    lc.WALKIN = Nothing
                End If
                If pCALLCENTER <> String.Empty Then
                    lc.CALLCENTER = CInt(pCALLCENTER)
                Else
                    lc.CALLCENTER = Nothing
                End If
                If pFDATE1 <> String.Empty Then
                    lc.FDATE1 = Date.ParseExact(pFDATE1, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Else
                    lc.FDATE1 = Nothing
                End If
                If pFDATE2 <> String.Empty Then
                    lc.FDATE2 = Date.ParseExact(pFDATE2, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Else
                    lc.FDATE2 = Nothing
                End If
                If pFDATE3 <> String.Empty Then
                    lc.FDATE3 = Date.ParseExact(pFDATE3, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Else
                    lc.FDATE3 = Nothing
                End If
                If pFDATE4 <> String.Empty Then
                    lc.FDATE4 = Date.ParseExact(pFDATE4, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Else
                    lc.FDATE4 = Nothing
                End If
                If pFDESC1 <> String.Empty Then
                    lc.FDESC1 = pFDESC1
                Else
                    lc.FDESC1 = Nothing
                End If
                If pFDESC2 <> String.Empty Then
                    lc.FDESC2 = pFDESC2
                Else
                    lc.FDESC2 = Nothing
                End If
                If pFDESC3 <> String.Empty Then
                    lc.FDESC3 = pFDESC3
                Else
                    lc.FDESC3 = Nothing
                End If
                If pFDESC4 <> String.Empty Then
                    lc.FDESC4 = pFDESC4
                Else
                    lc.FDESC4 = Nothing
                End If
                If pFROAD <> String.Empty Then
                    lc.FROAD = pFROAD
                Else
                    lc.FROAD = Nothing
                End If
                lc.IsThailandor = IIf(pIsThailandor = "True", "1", "0")
                If pSAPTaxCode <> String.Empty Then
                    lc.SAPTaxCode = pSAPTaxCode
                Else
                    lc.SAPTaxCode = Nothing
                End If
                'If pStmDate <> String.Empty Then
                '    lc.StmDate = Date.ParseExact(pStmDate, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                'Else
                '    lc.StmDate = Nothing
                'End If
                lc.LastUpDate = _date
                'If pLastUpDate <> String.Empty Then
                '    lc.LastUpDate = Date.ParseExact(pLastUpDate, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                'Else
                '    lc.LastUpDate = Nothing
                'End If

                If pFMOBILE2 <> String.Empty Then
                    lc.FMOBILE2 = pFMOBILE2
                Else
                    lc.FMOBILE2 = Nothing
                End If
                If pFLINE <> String.Empty Then
                    lc.FLINE = pFLINE
                Else
                    lc.FLINE = Nothing
                End If
                If pFFACEBOOK <> String.Empty Then
                    lc.FFACEBOOK = pFFACEBOOK
                Else
                    lc.FFACEBOOK = Nothing
                End If
                If pTaxNo <> String.Empty Then
                    lc.FTAXNO = pTaxNo
                Else
                    lc.FTAXNO = Nothing
                End If

            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Add(ByVal pFCONTCODE As String,
                         ByVal pFMEMBERCD As String,
                         ByVal pFPRECODE As String,
                         ByVal pFPRELEN As String,
                         ByVal pFCONTTNM As String,
                         ByVal pFCONTENM As String,
                         ByVal pFBIRTH As String,
                         ByVal pFSEX As String,
                         ByVal pFADD1 As String,
                         ByVal pFADD2 As String,
                         ByVal pFADD3 As String,
                         ByVal pFPROVINCE As String,
                         ByVal pFPOSTAL As String,
                         ByVal pFPROVCD As String,
                         ByVal pFCITYCD As String,
                         ByVal pFTELNO As String,
                         ByVal pFFAX As String,
                         ByVal pFEMAIL As String,
                         ByVal pFMOBILE As String,
                         ByVal pFPEOPLEID As String,
                         ByVal pFMD As String,
                         ByVal pFCONT As String,
                         ByVal pFTELC As String,
                         ByVal pFHADD1 As String,
                         ByVal pFHADD2 As String,
                         ByVal pFHADD3 As String,
                         ByVal pFHPROVINCE As String,
                         ByVal pFHPOSTAL As String,
                         ByVal pFHPROVCD As String,
                         ByVal pFHCITYCD As String,
                         ByVal pFHTEL As String,
                         ByVal pFOFFNAME As String,
                         ByVal pFOFFADD1 As String,
                         ByVal pFOFFADD2 As String,
                         ByVal pFOFFADD3 As String,
                         ByVal pFOFFPROV As String,
                         ByVal pFOFFZIP As String,
                         ByVal pFOFFPROVCD As String,
                         ByVal pFOFFCITYCD As String,
                         ByVal pFOFFTEL As String,
                         ByVal pFOFFICETYPE As String,
                         ByVal pFPOSITION As String,
                         ByVal pFMARSTAT As String,
                         ByVal pFMARDATE As String,
                         ByVal pFSTDATE As String,
                         ByVal pFMEMBEREXP As String,
                         ByVal pFPDISC As String,
                         ByVal pFNMCODE As String,
                         ByVal pFAINCOME As String,
                         ByVal pFFNDBUDG As String,
                         ByVal pFFNDLOCAT As String,
                         ByVal pFLIKEYN As String,
                         ByVal pFLIKEREAS As String,
                         ByVal pFLIKETYPE As String,
                         ByVal pFLIKERSDS As String,
                         ByVal pFMEDIACD As String,
                         ByVal pFSALESAREA As String,
                         ByVal pFCUCATE As String,
                         ByVal pFTRNNO As String,
                         ByVal pFTRNITEM As String,
                         ByVal pFCONTTYPE As String,
                         ByVal pFPRNMARK As String,
                         ByVal pFBILLADD1 As String,
                         ByVal pFBILLADD2 As String,
                         ByVal pFBILLADD3 As String,
                         ByVal pFBILLPROV As String,
                         ByVal pFBILLZIP As String,
                         ByVal pFBILLTO As String,
                         ByVal pFBILLYN As String,
                         ByVal pFBILLNMNO As String,
                         ByVal pFENTDATE As String,
                         ByVal pFENTBY As String,
                         ByVal pFENTTYPE As String,
                         ByVal pFUPDDATE As String,
                         ByVal pFUPDBY As String,
                         ByVal pFOFFCODE As String,
                         ByVal pFNOTE As String,
                         ByVal pFISGROUP As String,
                         ByVal pFSPECNM As String,
                         ByVal pFREPRJNO As String,
                         ByVal pFPDCODE As String,
                         ByVal pFRECBY As String,
                         ByVal pFNATION As String,
                         ByVal pFLEADATE As String,
                         ByVal pFEXPDATE As String,
                         ByVal pFPRNAME As String,
                         ByVal pFMRNAME As String,
                         ByVal pFMNATION As String,
                         ByVal pFSTATUS As String,
                         ByVal pFSALECODE As String,
                         ByVal pINCOME As String,
                         ByVal pGRADE As String,
                         ByVal pFLOTNO As String,
                         ByVal pBUDGET As String,
                         ByVal pCOMPARE As String,
                         ByVal pCALLIN As String,
                         ByVal pWALKIN As String,
                         ByVal pCALLCENTER As String,
                         ByVal pFDATE1 As String,
                         ByVal pFDATE2 As String,
                         ByVal pFDATE3 As String,
                         ByVal pFDATE4 As String,
                         ByVal pFDESC1 As String,
                         ByVal pFDESC2 As String,
                         ByVal pFDESC3 As String,
                         ByVal pFDESC4 As String,
                         ByVal pFROAD As String,
                         ByVal pIsThailandor As String,
                         ByVal pSAPTaxCode As String,
                         ByVal pStmDate As String,
                         ByVal pLastUpDate As String,
                         ByVal pFMOBILE2 As String,
                         ByVal pFLINE As String,
                         ByVal pFFACEBOOK As String,
                         ByVal pTaxNo As String,
                         ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities

            Dim _date As Date = Date.Now
            Dim lc As OD50RCVD = New OD50RCVD

            lc.FCONTCODE = pFCONTCODE
            lc.FMEMBERCD = pFMEMBERCD
            lc.FPRECODE = IIf(pFPRECODE = "True", "0", "1")
            If pFPRELEN <> String.Empty Then
                lc.FPRELEN = CDec(pFPRELEN)
            Else
                lc.FPRELEN = 0
            End If
            lc.FCONTTNM = pFCONTTNM
            lc.FCONTENM = pFCONTENM
            If pFBIRTH <> String.Empty Then
                lc.FBIRTH = Date.ParseExact(pFBIRTH, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FBIRTH = Nothing
            End If
            If pFSEX <> String.Empty Then
                lc.FSEX = pFSEX
            Else
                lc.FSEX = Nothing
            End If
            If pFADD1 <> String.Empty Then
                lc.FADD1 = pFADD1
            Else
                lc.FADD1 = Nothing
            End If
            If pFADD2 <> String.Empty Then
                lc.FADD2 = pFADD2
            Else
                lc.FADD2 = Nothing
            End If
            If pFADD3 <> String.Empty Then
                lc.FADD3 = pFADD3
            Else
                lc.FADD3 = Nothing
            End If
            If pFPROVINCE <> String.Empty Then
                lc.FPROVINCE = pFPROVINCE
            Else
                lc.FPROVINCE = Nothing
            End If
            If pFPOSTAL <> String.Empty Then
                lc.FPOSTAL = pFPOSTAL
            Else
                lc.FPOSTAL = Nothing
            End If
            If pFPROVCD <> String.Empty Then
                lc.FPROVCD = pFPROVCD
            Else
                lc.FPROVCD = Nothing
            End If
            If pFCITYCD <> String.Empty Then
                lc.FCITYCD = pFCITYCD
            Else
                lc.FCITYCD = Nothing
            End If
            If pFTELNO <> String.Empty Then
                lc.FTELNO = pFTELNO
            Else
                lc.FTELNO = Nothing
            End If
            If pFFAX <> String.Empty Then
                lc.FFAX = pFFAX
            Else
                lc.FFAX = Nothing
            End If
            If pFEMAIL <> String.Empty Then
                lc.FEMAIL = pFEMAIL
            Else
                lc.FEMAIL = Nothing
            End If
            If pFMOBILE <> String.Empty Then
                lc.FMOBILE = pFMOBILE
            Else
                lc.FMOBILE = Nothing
            End If
            If pFPEOPLEID <> String.Empty Then
                lc.FPEOPLEID = pFPEOPLEID.Replace(" ", "")
            Else
                lc.FPEOPLEID = Nothing
            End If
            If pFMD <> String.Empty Then
                lc.FMD = pFMD
            Else
                lc.FMD = Nothing
            End If
            If pFCONT <> String.Empty Then
                lc.FCONT = pFCONT
            Else
                lc.FCONT = Nothing
            End If
            If pFTELC <> String.Empty Then
                lc.FTELC = pFTELC
            Else
                lc.FTELC = Nothing
            End If
            If pFHADD1 <> String.Empty Then
                lc.FHADD1 = pFHADD1
            Else
                lc.FHADD1 = Nothing
            End If
            If pFHADD2 <> String.Empty Then
                lc.FHADD2 = pFHADD2
            Else
                lc.FHADD2 = Nothing
            End If
            If pFHADD3 <> String.Empty Then
                lc.FHADD3 = pFHADD3
            Else
                lc.FHADD3 = Nothing
            End If
            If pFHPROVINCE <> String.Empty Then
                lc.FHPROVINCE = pFHPROVINCE
            Else
                lc.FHPROVINCE = Nothing
            End If
            If pFHPOSTAL <> String.Empty Then
                lc.FHPOSTAL = pFHPOSTAL
            Else
                lc.FHPOSTAL = Nothing
            End If
            If pFHPROVCD <> String.Empty Then
                lc.FHPROVCD = pFHPROVCD
            Else
                lc.FHPROVCD = Nothing
            End If
            If pFHCITYCD <> String.Empty Then
                lc.FHCITYCD = pFHCITYCD
            Else
                lc.FHCITYCD = Nothing
            End If
            If pFHTEL <> String.Empty Then
                lc.FHTEL = pFHTEL
            Else
                lc.FHTEL = Nothing
            End If
            If pFOFFNAME <> String.Empty Then
                lc.FOFFNAME = pFOFFNAME
            Else
                lc.FOFFNAME = Nothing
            End If
            If pFOFFADD1 <> String.Empty Then
                lc.FOFFADD1 = pFOFFADD1
            Else
                lc.FOFFADD1 = Nothing
            End If
            If pFOFFADD2 <> String.Empty Then
                lc.FOFFADD2 = pFOFFADD2
            Else
                lc.FOFFADD2 = Nothing
            End If
            If pFOFFADD3 <> String.Empty Then
                lc.FOFFADD3 = pFOFFADD3
            Else
                lc.FOFFADD3 = Nothing
            End If
            If pFOFFPROV <> String.Empty Then
                lc.FOFFPROV = pFOFFPROV
            Else
                lc.FOFFPROV = Nothing
            End If
            If pFOFFZIP <> String.Empty Then
                lc.FOFFZIP = pFOFFZIP
            Else
                lc.FOFFZIP = Nothing
            End If
            If pFOFFPROVCD <> String.Empty Then
                lc.FOFFPROVCD = pFOFFPROVCD
            Else
                lc.FOFFPROVCD = Nothing
            End If
            If pFOFFCITYCD <> String.Empty Then
                lc.FOFFCITYCD = pFOFFCITYCD
            Else
                lc.FOFFCITYCD = Nothing
            End If
            If pFOFFTEL <> String.Empty Then
                lc.FOFFTEL = pFOFFTEL
            Else
                lc.FOFFTEL = Nothing
            End If
            If pFOFFICETYPE <> String.Empty Then
                lc.FOFFICETYPE = pFOFFICETYPE
            Else
                lc.FOFFICETYPE = Nothing
            End If
            If pFPOSITION <> String.Empty Then
                lc.FPOSITION = pFPOSITION
            Else
                lc.FPOSITION = Nothing
            End If
            If pFMARSTAT <> String.Empty Then
                lc.FMARSTAT = pFMARSTAT
            Else
                lc.FMARSTAT = Nothing
            End If
            If pFMARDATE <> String.Empty Then
                lc.FMARDATE = Date.ParseExact(pFMARDATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FMARDATE = Nothing
            End If
            If pFSTDATE <> String.Empty Then
                lc.FSTDATE = Date.ParseExact(pFSTDATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FSTDATE = Nothing
            End If
            If pFMEMBEREXP <> String.Empty Then
                lc.FMEMBEREXP = Date.ParseExact(pFMEMBEREXP, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FMEMBEREXP = Nothing
            End If
            If pFPDISC <> String.Empty Then
                lc.FPDISC = CDec(pFPDISC)
            Else
                lc.FPDISC = Nothing
            End If
            If pFNMCODE <> String.Empty Then
                lc.FNMCODE = pFNMCODE
            Else
                lc.FNMCODE = Nothing
            End If
            If pFAINCOME <> String.Empty Then
                lc.FAINCOME = CDec(pFAINCOME)
            Else
                lc.FAINCOME = Nothing
            End If
            If pFFNDBUDG <> String.Empty Then
                lc.FFNDBUDG = CDec(pFFNDBUDG)
            Else
                lc.FFNDBUDG = Nothing
            End If
            If pFFNDLOCAT <> String.Empty Then
                lc.FFNDLOCAT = pFFNDLOCAT
            Else
                lc.FFNDLOCAT = Nothing
            End If
            If pFLIKEYN <> String.Empty Then
                lc.FLIKEYN = pFLIKEYN
            Else
                lc.FLIKEYN = Nothing
            End If
            If pFLIKEREAS <> String.Empty Then
                lc.FLIKEREAS = pFLIKEREAS
            Else
                lc.FLIKEREAS = Nothing
            End If
            If pFLIKETYPE <> String.Empty Then
                lc.FLIKETYPE = pFLIKETYPE
            Else
                lc.FLIKETYPE = Nothing
            End If
            If pFLIKERSDS <> String.Empty Then
                lc.FLIKERSDS = pFLIKERSDS
            Else
                lc.FLIKERSDS = Nothing
            End If
            If pFMEDIACD <> String.Empty Then
                lc.FMEDIACD = pFMEDIACD
            Else
                lc.FMEDIACD = Nothing
            End If
            If pFSALESAREA <> String.Empty Then
                lc.FSALESAREA = pFSALESAREA
            Else
                lc.FSALESAREA = Nothing
            End If
            If pFCUCATE <> String.Empty Then
                lc.FCUCATE = pFCUCATE
            Else
                lc.FCUCATE = Nothing
            End If
            If pFTRNNO <> String.Empty Then
                lc.FTRNNO = pFTRNNO
            Else
                lc.FTRNNO = Nothing
            End If
            If pFTRNITEM <> String.Empty Then
                lc.FTRNITEM = pFTRNITEM
            Else
                lc.FTRNITEM = Nothing
            End If
            If pFCONTTYPE <> String.Empty Then
                lc.FCONTTYPE = pFCONTTYPE
            Else
                lc.FCONTTYPE = Nothing
            End If
            If pFPRNMARK <> String.Empty Then
                lc.FPRNMARK = pFPRNMARK
            Else
                lc.FPRNMARK = Nothing
            End If
            If pFBILLADD1 <> String.Empty Then
                lc.FBILLADD1 = pFBILLADD1
            Else
                lc.FBILLADD1 = Nothing
            End If
            If pFBILLADD2 <> String.Empty Then
                lc.FBILLADD2 = pFBILLADD2
            Else
                lc.FBILLADD2 = Nothing
            End If
            If pFBILLADD3 <> String.Empty Then
                lc.FBILLADD3 = pFBILLADD3
            Else
                lc.FBILLADD3 = Nothing
            End If
            If pFBILLPROV <> String.Empty Then
                lc.FBILLPROV = pFBILLPROV
            Else
                lc.FBILLPROV = Nothing
            End If
            If pFBILLZIP <> String.Empty Then
                lc.FBILLZIP = pFBILLZIP
            Else
                lc.FBILLZIP = Nothing
            End If
            If pFBILLTO <> String.Empty Then
                lc.FBILLTO = pFBILLTO
            Else
                lc.FBILLTO = Nothing
            End If
            If pFBILLYN <> String.Empty Then
                lc.FBILLYN = pFBILLYN
            Else
                lc.FBILLYN = Nothing
            End If
            If pFBILLNMNO <> String.Empty Then
                lc.FBILLNMNO = pFBILLNMNO
            Else
                lc.FBILLNMNO = Nothing
            End If
            If pFENTDATE <> String.Empty Then
                lc.FENTDATE = Date.ParseExact(pFENTDATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FENTDATE = Nothing
            End If
            If pFENTBY <> String.Empty Then
                lc.FENTBY = pFENTBY
            Else
                lc.FENTBY = Nothing
            End If
            If pFENTTYPE <> String.Empty Then
                lc.FENTTYPE = pFENTTYPE
            Else
                lc.FENTTYPE = Nothing
            End If
            'If pFUPDDATE <> String.Empty Then
            '    lc.FUPDDATE = Date.ParseExact(pFUPDDATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            'Else
            '    lc.FUPDDATE = Nothing
            'End If
            'If pFUPDBY <> String.Empty Then
            '    lc.FUPDBY = pFUPDBY
            'Else
            '    lc.FUPDBY = Nothing
            'End If
            If pFOFFCODE <> String.Empty Then
                lc.FOFFCODE = pFOFFCODE
            Else
                lc.FOFFCODE = Nothing
            End If
            If pFNOTE <> String.Empty Then
                lc.FNOTE = pFNOTE
            Else
                lc.FNOTE = Nothing
            End If
            If pFISGROUP <> String.Empty Then
                lc.FISGROUP = pFISGROUP
            Else
                lc.FISGROUP = Nothing
            End If
            If pFSPECNM <> String.Empty Then
                lc.FSPECNM = pFSPECNM
            Else
                lc.FSPECNM = Nothing
            End If
            If pFREPRJNO <> String.Empty Then
                lc.FREPRJNO = pFREPRJNO
            Else
                lc.FREPRJNO = Nothing
            End If
            If pFPDCODE <> String.Empty Then
                lc.FPDCODE = pFPDCODE
            Else
                lc.FPDCODE = Nothing
            End If
            If pFRECBY <> String.Empty Then
                lc.FRECBY = pFRECBY
            Else
                lc.FRECBY = Nothing
            End If
            If pFNATION <> String.Empty Then
                lc.FNATION = pFNATION
            Else
                lc.FNATION = Nothing
            End If
            If pFLEADATE <> String.Empty Then
                lc.FLEADATE = Date.ParseExact(pFLEADATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FLEADATE = Nothing
            End If
            If pFEXPDATE <> String.Empty Then
                lc.FEXPDATE = Date.ParseExact(pFEXPDATE, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FEXPDATE = Nothing
            End If
            If pFPRNAME <> String.Empty Then
                lc.FPRNAME = pFPRNAME
            Else
                lc.FPRNAME = Nothing
            End If
            If pFMRNAME <> String.Empty Then
                lc.FMRNAME = pFMRNAME
            Else
                lc.FMRNAME = Nothing
            End If
            If pFMNATION <> String.Empty Then
                lc.FMNATION = pFMNATION
            Else
                lc.FMNATION = Nothing
            End If
            If pFSTATUS <> String.Empty Then
                lc.FSTATUS = pFSTATUS
            Else
                lc.FSTATUS = Nothing
            End If
            If pFSALECODE <> String.Empty Then
                lc.FSALECODE = pFSALECODE
            Else
                lc.FSALECODE = Nothing
            End If
            If pINCOME <> String.Empty Then
                lc.INCOME = pINCOME
            Else
                lc.INCOME = Nothing
            End If
            If pGRADE <> String.Empty Then
                lc.GRADE = pGRADE
            Else
                lc.GRADE = Nothing
            End If
            If pFLOTNO <> String.Empty Then
                lc.FLOTNO = pFLOTNO
            Else
                lc.FLOTNO = Nothing
            End If
            If pBUDGET <> String.Empty Then
                lc.BUDGET = pBUDGET
            Else
                lc.BUDGET = Nothing
            End If
            If pCOMPARE <> String.Empty Then
                lc.COMPARE = pCOMPARE
            Else
                lc.COMPARE = Nothing
            End If
            If pCALLIN <> String.Empty Then
                lc.CALLIN = CInt(pCALLIN)
            Else
                lc.CALLIN = Nothing
            End If
            If pWALKIN <> String.Empty Then
                lc.WALKIN = CInt(pWALKIN)
            Else
                lc.WALKIN = Nothing
            End If
            If pCALLCENTER <> String.Empty Then
                lc.CALLCENTER = CInt(pCALLCENTER)
            Else
                lc.CALLCENTER = Nothing
            End If
            If pFDATE1 <> String.Empty Then
                lc.FDATE1 = Date.ParseExact(pFDATE1, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FDATE1 = Nothing
            End If
            If pFDATE2 <> String.Empty Then
                lc.FDATE2 = Date.ParseExact(pFDATE2, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FDATE2 = Nothing
            End If
            If pFDATE3 <> String.Empty Then
                lc.FDATE3 = Date.ParseExact(pFDATE3, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FDATE3 = Nothing
            End If
            If pFDATE4 <> String.Empty Then
                lc.FDATE4 = Date.ParseExact(pFDATE4, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            Else
                lc.FDATE4 = Nothing
            End If
            If pFDESC1 <> String.Empty Then
                lc.FDESC1 = pFDESC1
            Else
                lc.FDESC1 = Nothing
            End If
            If pFDESC2 <> String.Empty Then
                lc.FDESC2 = pFDESC2
            Else
                lc.FDESC2 = Nothing
            End If
            If pFDESC3 <> String.Empty Then
                lc.FDESC3 = pFDESC3
            Else
                lc.FDESC3 = Nothing
            End If
            If pFDESC4 <> String.Empty Then
                lc.FDESC4 = pFDESC4
            Else
                lc.FDESC4 = Nothing
            End If
            If pFROAD <> String.Empty Then
                lc.FROAD = pFROAD
            Else
                lc.FROAD = Nothing
            End If
            lc.IsThailandor = IIf(pIsThailandor = "True", "1", "0")
            If pSAPTaxCode <> String.Empty Then
                lc.SAPTaxCode = pSAPTaxCode
            Else
                lc.SAPTaxCode = Nothing
            End If

            'เฉพาะ Insert
            lc.StmDate = _date
            lc.LastUpDate = _date

            If pFMOBILE2 <> String.Empty Then
                lc.FMOBILE2 = pFMOBILE2
            Else
                lc.FMOBILE2 = Nothing
            End If
            If pFLINE <> String.Empty Then
                lc.FLINE = pFLINE
            Else
                lc.FLINE = Nothing
            End If
            If pFFACEBOOK <> String.Empty Then
                lc.FFACEBOOK = pFFACEBOOK
            Else
                lc.FFACEBOOK = Nothing
            End If
            If pTaxNo <> String.Empty Then
                lc.FTAXNO = pTaxNo
            Else
                lc.FTAXNO = Nothing
            End If

            db.OD50RCVD.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function GetOd50rcvdByName(ByVal pFCONTTNM As String,
                                    ByVal pFCONTENM As String) As OD50RCVD

        Using db As New PNSWEBEntities
            Dim lc As OD50RCVD = db.OD50RCVD.Where(Function(s) s.FCONTTNM = pFCONTTNM OrElse s.FCONTENM = pFCONTENM).SingleOrDefault
            Return lc
        End Using

    End Function


    Public Function getUsedCustomerCode() As List(Of String)

        Using db As New PNSWEBEntities
            Dim query = ((From a In db.OD11BKT1 Select a.FCUCODE1) _
                        .Union _
                        (From a In db.OD11BKT1 Select a.FCUCODE2) _
                        .Union _
                        (From a In db.OD11BKT1 Select a.FCUCODE3) _
                        .Union _
                        (From a In db.OD20LAGR Select a.FCUCODE)).Where(Function(s) s <> "").ToList


            Return query
        End Using
    End Function
#End Region

End Class

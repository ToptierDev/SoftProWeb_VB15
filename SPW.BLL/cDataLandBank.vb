Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cDataLandBank

#Region "TRN_DataLandBank.aspx"
    Public Function Loaddata(ByVal pKeyword As String,
                             ByVal strUserID As String) As List(Of FD11PROP)

        Using db As New PNSWEBEntities
            Dim lc As List(Of FD11PROP) = db.FD11PROP.OrderBy(Function(s) s.FASSETNO).ToList
            If pKeyword <> String.Empty Then
                lc = db.FD11PROP.Where(Function(s) s.FASSETNO.ToUpper.Replace(" ", "").Contains(pKeyword.ToUpper.Replace(" ", "")) Or
                                                   s.FPCPIECE.ToUpper.Replace(" ", "").Contains(pKeyword.ToUpper.Replace(" ", ""))).ToList
            End If
            Return lc.ToList
        End Using

    End Function

    Public Function GetByID(ByVal id As String,
                            ByVal strUserID As String) As FD11PROP

        Using db As New PNSWEBEntities
            Dim lc As FD11PROP = db.FD11PROP.Where(Function(s) s.FASSETNO = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function Delete(ByVal id As String,
                           ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As FD11PROP = db.FD11PROP.Where(Function(s) s.FASSETNO = id).SingleOrDefault
                db.FD11PROP.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Edit(ByVal pFASSETNO As String,
                         ByVal pFASSETBJ2 As Boolean,
                         ByVal pFASSETOBJ As String,
                         ByVal pFASSETNM As String,
                         ByVal pFENDATE As String,
                         ByVal pFDESC1 As String,
                         ByVal pFPCPIECE As String,
                         ByVal pFPCINST As String,
                         ByVal pFPCLNDNO As String,
                         ByVal pFPCINST2 As String,
                         ByVal pFPCBETWEEN As String,
                         ByVal pFPCTABON As String,
                         ByVal pFPCWIDTH As String,
                         ByVal pFPCAMPHE As String,
                         ByVal pFPCINST3 As String,
                         ByVal pFPCPROVINCE As String,
                         ByVal pFASCOLOR As String,
                         ByVal pFPCLANDOWN As String,
                         ByVal pFQTY1 As String,
                         ByVal pFQTY2 As String,
                         ByVal pFQTY3 As String,
                         ByVal pFPCLANDOWN2 As String,
                         ByVal pFMKPRCU As String,
                         ByVal pFMCOMPCOMP As String,
                         ByVal pFASSPRCU As String,
                         ByVal pFKPRCA As String,
                         ByVal pFASSPRCA As String,
                         ByVal pFMKPRCBY As String,
                         ByVal pFASSWHO As String,
                         ByVal pCLANDOWNTL As String,
                         ByVal pFASSPRCDT As String,
                         ByVal pFMKPRCDT As String,
                         ByVal pFASSCHG As String,
                         ByVal pFASSETST As String,
                         ByVal pFASSETST2 As String,
                         ByVal pFAGRNO As String,
                         ByVal pFAGRNO2 As String,
                         ByVal pFMORTGYN As Boolean,
                         ByVal pFMORTGYN2 As String,
                         ByVal pFSTDATE As String,
                         ByVal pFMORTGT As String,
                         ByVal pNote As String,
                         ByVal pKeyCode As String,
                         ByVal strUserID As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As FD11PROP = db.FD11PROP.Where(Function(s) s.FASSETNO = pFASSETNO).SingleOrDefault
            If lc IsNot Nothing Then
                'lc.FENTTYPE = "1"
                If pFASSETBJ2 Then
                    lc.FASSETOBJ2 = "3"
                Else
                    lc.FASSETOBJ2 = Nothing
                End If
                If pFASSETOBJ <> String.Empty Then
                    lc.FASSETOBJ = pFASSETOBJ
                Else
                    lc.FASSETOBJ = Nothing
                End If
                If pFASSETNM <> String.Empty Then
                    lc.FASSETNM = pFASSETNM
                Else
                    lc.FASSETNM = Nothing
                End If
                If pFENDATE <> String.Empty Then
                    lc.FENDATE = CDate(pFENDATE)
                Else
                    lc.FENDATE = Nothing
                End If
                If pFDESC1 <> String.Empty Then
                    lc.FDES1 = pFDESC1
                Else
                    lc.FDES1 = Nothing
                End If
                If pFPCPIECE <> String.Empty Then
                    lc.FPCPIECE = pFPCPIECE
                Else
                    lc.FPCPIECE = Nothing
                End If
                If pFPCINST <> String.Empty Then
                    lc.FPCINST = pFPCINST
                Else
                    lc.FPCINST = Nothing
                End If
                If pFPCLNDNO <> String.Empty Then
                    lc.FPCLNDNO = pFPCLNDNO
                Else
                    lc.FPCLNDNO = Nothing
                End If
                If pFPCINST2 <> String.Empty Then
                    lc.FPCINST2 = pFPCINST2
                Else
                    lc.FPCINST2 = Nothing
                End If
                If pFPCBETWEEN <> String.Empty Then
                    lc.FPCBETWEEN = pFPCBETWEEN
                Else
                    lc.FPCBETWEEN = Nothing
                End If
                If pFPCTABON <> String.Empty Then
                    lc.FPCTAMBON = pFPCTABON
                Else
                    lc.FPCTAMBON = Nothing
                End If
                If pFPCWIDTH <> String.Empty Then
                    lc.FPCWIDTH = pFPCWIDTH
                Else
                    lc.FPCWIDTH = Nothing
                End If
                If pFPCAMPHE <> String.Empty Then
                    lc.FPCAMPHER = pFPCAMPHE
                Else
                    lc.FPCAMPHER = Nothing
                End If
                If pFPCINST3 <> String.Empty Then
                    lc.FPCINST3 = pFPCINST3
                Else
                    lc.FPCINST3 = Nothing
                End If
                If pFPCPROVINCE <> String.Empty Then
                    lc.FPCPROVINC = pFPCPROVINCE
                Else
                    lc.FPCPROVINC = Nothing
                End If
                If pFASCOLOR <> String.Empty Then
                    lc.FASCOLOR = pFASCOLOR
                Else
                    lc.FASCOLOR = Nothing
                End If
                If pFPCLANDOWN <> String.Empty Then
                    lc.FPCLANDOWN = pFPCLANDOWN
                Else
                    lc.FPCLANDOWN = Nothing
                End If
                If pFQTY1 <> String.Empty Then
                    Dim IntTemp1 As Integer = CInt(pFQTY1.Split("-")(0))
                    Dim IntTemp2 As Integer = CInt(pFQTY1.Split("-")(1))
                    Dim IntTemp3 As Integer = CInt(pFQTY1.Split("-")(2))
                    lc.FQTY = CDec((IntTemp1 * 400) + (IntTemp2 * 100) + IntTemp3)
                    'lc.FQTY = CDec(pFQTY1)
                Else
                    lc.FQTY = Nothing
                End If
                If pFPCLANDOWN2 <> String.Empty Then
                    lc.FPCLANDOWN2 = pFPCLANDOWN2
                Else
                    lc.FPCLANDOWN2 = Nothing
                End If
                If pFMKPRCU <> String.Empty Then
                    lc.FMKPRCU = CDec(pFMKPRCU)
                Else
                    lc.FMKPRCU = Nothing
                End If
                If pFMCOMPCOMP <> String.Empty Then
                    lc.FLCOMPCOMP = pFMCOMPCOMP
                Else
                    lc.FLCOMPCOMP = Nothing
                End If
                If pFASSPRCU <> String.Empty Then
                    lc.FASSPRCU = CDec(pFASSPRCU)
                Else
                    lc.FASSPRCU = Nothing
                End If
                If pFKPRCA <> String.Empty Then
                    lc.FMKPRCA = CDec(pFKPRCA)
                Else
                    lc.FMKPRCA = Nothing
                End If
                If pFASSPRCA <> String.Empty Then
                    lc.FASSPRCA = CDec(pFASSPRCA)
                Else
                    lc.FASSPRCA = Nothing
                End If
                If pFMKPRCBY <> String.Empty Then
                    lc.FMKPRCBY = pFMKPRCBY
                Else
                    lc.FMKPRCBY = Nothing
                End If
                If pFASSWHO <> String.Empty Then
                    lc.FASSWHO = pFASSWHO
                Else
                    lc.FASSWHO = Nothing
                End If
                If pCLANDOWNTL <> String.Empty Then
                    lc.FPCLANDOWTL = pCLANDOWNTL
                Else
                    lc.FPCLANDOWTL = Nothing
                End If
                If pFASSPRCDT <> String.Empty Then
                    lc.FASSPRCDT = CDate(pFASSPRCDT)
                Else
                    lc.FASSPRCDT = Nothing
                End If
                If pFMKPRCDT <> String.Empty Then
                    lc.FMKPRCDT = CDate(pFMKPRCDT)
                Else
                    lc.FMKPRCDT = Nothing
                End If
                If pFASSCHG <> String.Empty Then
                    lc.FASSCHG = CDec(pFASSCHG)
                Else
                    lc.FASSCHG = Nothing
                End If
                If pFASSETST <> String.Empty And
                   pFASSETST2 <> String.Empty Then
                    lc.FASSETST = pFASSETST2 & pFASSETST
                ElseIf pFASSETST <> String.Empty And
                       pFASSETST2 = String.Empty Then
                    lc.FASSETST = pFASSETST
                ElseIf pFASSETST = String.Empty And
                       pFASSETST2 <> String.Empty Then
                    lc.FASSETST = pFASSETST2
                ElseIf pFASSETST = String.Empty And
                       pFASSETST2 = String.Empty Then
                    lc.FASSETST = "cc"
                End If
                If pFAGRNO <> String.Empty Then
                    lc.FAGRNO = pFAGRNO
                Else
                    lc.FAGRNO = Nothing
                End If

                'lc.FAGRNO2 = pFAGRNO2

                If pFMORTGYN Then
                    lc.FMORTGYN = "Y"
                Else
                    lc.FMORTGYN = "N"
                End If

                'lc.FMORTGYN2 = pFMORTGYN2

                If pFSTDATE <> String.Empty Then
                    lc.FSTDATE = CDate(pFSTDATE)
                Else
                    lc.FSTDATE = Nothing
                End If

                If pFMORTGT <> String.Empty Then
                    lc.FMORTGDT = CDate(pFMORTGT)
                Else
                    lc.FMORTGDT = Nothing
                End If
                If pNote <> String.Empty Then
                    lc.FPCNOTE = pNote
                Else
                    lc.FPCNOTE = Nothing
                End If

                If pKeyCode <> String.Empty Then
                    lc.FPCPROVCD = pKeyCode.Substring(0, 2)
                    lc.FPCCITYCD = pKeyCode.Substring(2, 2)
                Else
                    lc.FPCPROVCD = Nothing
                    lc.FPCCITYCD = Nothing
                End If
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Add(ByVal pFASSETNO As String,
                        ByVal pFASSETBJ2 As Boolean,
                        ByVal pFASSETOBJ As String,
                        ByVal pFASSETNM As String,
                        ByVal pFENDATE As String,
                        ByVal pFDESC1 As String,
                        ByVal pFPCPIECE As String,
                        ByVal pFPCINST As String,
                        ByVal pFPCLNDNO As String,
                        ByVal pFPCINST2 As String,
                        ByVal pFPCBETWEEN As String,
                        ByVal pFPCTABON As String,
                        ByVal pFPCWIDTH As String,
                        ByVal pFPCAMPHE As String,
                        ByVal pFPCINST3 As String,
                        ByVal pFPCPROVINCE As String,
                        ByVal pFASCOLOR As String,
                        ByVal pFPCLANDOWN As String,
                        ByVal pFQTY1 As String,
                        ByVal pFQTY2 As String,
                        ByVal pFQTY3 As String,
                        ByVal pFPCLANDOWN2 As String,
                        ByVal pFMKPRCU As String,
                        ByVal pFMCOMPCOMP As String,
                        ByVal pFASSPRCU As String,
                        ByVal pFKPRCA As String,
                        ByVal pFASSPRCA As String,
                        ByVal pFMKPRCBY As String,
                        ByVal pFASSWHO As String,
                        ByVal pCLANDOWNTL As String,
                        ByVal pFASSPRCDT As String,
                        ByVal pFMKPRCDT As String,
                        ByVal pFASSCHG As String,
                        ByVal pFASSETST As String,
                        ByVal pFASSETST2 As String,
                        ByVal pFAGRNO As String,
                        ByVal pFAGRNO2 As String,
                        ByVal pFMORTGYN As Boolean,
                        ByVal pFMORTGYN2 As String,
                        ByVal pFSTDATE As String,
                        ByVal pFMORTGT As String,
                        ByVal pNote As String,
                        ByVal pKeyCode As String,
                        ByVal strUserID As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As FD11PROP = New FD11PROP
            lc.FENTTYPE = "1"
            If pFASSETNO <> String.Empty Then
                lc.FASSETNO = pFASSETNO
            End If
            If pFASSETBJ2 Then
                lc.FASSETOBJ2 = "3"
            Else
                lc.FASSETOBJ2 = Nothing
            End If
            If pFASSETOBJ <> String.Empty Then
                lc.FASSETOBJ = pFASSETOBJ
            Else
                lc.FASSETOBJ = Nothing
            End If
            If pFASSETNM <> String.Empty Then
                lc.FASSETNM = pFASSETNM
            Else
                lc.FASSETNM = Nothing
            End If
            If pFENDATE <> String.Empty Then
                lc.FENDATE = CDate(pFENDATE)
            Else
                lc.FENDATE = Nothing
            End If
            If pFDESC1 <> String.Empty Then
                lc.FDES1 = pFDESC1
            Else
                lc.FDES1 = Nothing
            End If
            If pFPCPIECE <> String.Empty Then
                lc.FPCPIECE = pFPCPIECE
            Else
                lc.FPCPIECE = Nothing
            End If
            If pFPCINST <> String.Empty Then
                lc.FPCINST = pFPCINST
            Else
                lc.FPCINST = Nothing
            End If
            If pFPCLNDNO <> String.Empty Then
                lc.FPCLNDNO = pFPCLNDNO
            Else
                lc.FPCLNDNO = Nothing
            End If
            If pFPCINST2 <> String.Empty Then
                lc.FPCINST2 = pFPCINST2
            Else
                lc.FPCINST2 = Nothing
            End If
            If pFPCBETWEEN <> String.Empty Then
                lc.FPCBETWEEN = pFPCBETWEEN
            Else
                lc.FPCBETWEEN = Nothing
            End If
            If pFPCTABON <> String.Empty Then
                lc.FPCTAMBON = pFPCTABON
            Else
                lc.FPCTAMBON = Nothing
            End If
            If pFPCWIDTH <> String.Empty Then
                lc.FPCWIDTH = pFPCWIDTH
            Else
                lc.FPCWIDTH = Nothing
            End If
            If pFPCAMPHE <> String.Empty Then
                lc.FPCAMPHER = pFPCAMPHE
            Else
                lc.FPCAMPHER = Nothing
            End If
            If pFPCINST3 <> String.Empty Then
                lc.FPCINST3 = pFPCINST3
            Else
                lc.FPCINST3 = Nothing
            End If
            If pFPCPROVINCE <> String.Empty Then
                lc.FPCPROVINC = pFPCPROVINCE
            Else
                lc.FPCPROVINC = Nothing
            End If
            If pFASCOLOR <> String.Empty Then
                lc.FASCOLOR = pFASCOLOR
            Else
                lc.FASCOLOR = Nothing
            End If
            If pFPCLANDOWN <> String.Empty Then
                lc.FPCLANDOWN = pFPCLANDOWN
            Else
                lc.FPCLANDOWN = Nothing
            End If
            If pFQTY1 <> String.Empty Then
                Dim IntTemp1 As Integer = CInt(pFQTY1.Split("-")(0))
                Dim IntTemp2 As Integer = CInt(pFQTY1.Split("-")(1))
                Dim IntTemp3 As Integer = CInt(pFQTY1.Split("-")(2))
                lc.FQTY = CDec((IntTemp1 * 400) + (IntTemp2 * 100) + IntTemp3)
                'lc.FQTY = CDec(pFQTY1)
            Else
                lc.FQTY = Nothing
            End If
            If pFPCLANDOWN2 <> String.Empty Then
                lc.FPCLANDOWN2 = pFPCLANDOWN2
            Else
                lc.FPCLANDOWN2 = Nothing
            End If
            If pFMKPRCU <> String.Empty Then
                lc.FMKPRCU = CDec(pFMKPRCU)
            Else
                lc.FMKPRCU = Nothing
            End If
            If pFMCOMPCOMP <> String.Empty Then
                lc.FLCOMPCOMP = pFMCOMPCOMP
            Else
                lc.FLCOMPCOMP = Nothing
            End If
            If pFASSPRCU <> String.Empty Then
                lc.FASSPRCU = CDec(pFASSPRCU)
            Else
                lc.FASSPRCU = Nothing
            End If
            If pFKPRCA <> String.Empty Then
                lc.FMKPRCA = CDec(pFKPRCA)
            Else
                lc.FMKPRCA = Nothing
            End If
            If pFASSPRCA <> String.Empty Then
                lc.FASSPRCA = CDec(pFASSPRCA)
            Else
                lc.FASSPRCA = Nothing
            End If
            If pFMKPRCBY <> String.Empty Then
                lc.FMKPRCBY = pFMKPRCBY
            Else
                lc.FMKPRCBY = Nothing
            End If
            If pFASSWHO <> String.Empty Then
                lc.FASSWHO = pFASSWHO
            Else
                lc.FASSWHO = Nothing
            End If
            If pCLANDOWNTL <> String.Empty Then
                lc.FPCLANDOWTL = pCLANDOWNTL
            Else
                lc.FPCLANDOWTL = Nothing
            End If
            If pFASSPRCDT <> String.Empty Then
                lc.FASSPRCDT = CDate(pFASSPRCDT)
            Else
                lc.FASSPRCDT = Nothing
            End If
            If pFMKPRCDT <> String.Empty Then
                lc.FMKPRCDT = CDate(pFMKPRCDT)
            Else
                lc.FMKPRCDT = Nothing
            End If
            If pFASSCHG <> String.Empty Then
                lc.FASSCHG = CDec(pFASSCHG)
            Else
                lc.FASSCHG = Nothing
            End If
            If pFASSETST <> String.Empty And
               pFASSETST2 <> String.Empty Then
                lc.FASSETST = pFASSETST2 & pFASSETST
            ElseIf pFASSETST <> String.Empty And
                   pFASSETST2 = String.Empty Then
                lc.FASSETST = pFASSETST
            ElseIf pFASSETST = String.Empty And
                   pFASSETST2 <> String.Empty Then
                lc.FASSETST = pFASSETST2
            ElseIf pFASSETST = String.Empty And
                   pFASSETST2 = String.Empty Then
                lc.FASSETST = "cc"
            End If
            If pFAGRNO <> String.Empty Then
                lc.FAGRNO = pFAGRNO
            Else
                lc.FAGRNO = Nothing
            End If

            'lc.FAGRNO2 = pFAGRNO2

            If pFMORTGYN Then
                lc.FMORTGYN = "Y"
            Else
                lc.FMORTGYN = "N"
            End If

            'lc.FMORTGYN2 = pFMORTGYN2

            If pFSTDATE <> String.Empty Then
                lc.FSTDATE = CDate(pFSTDATE)
            Else
                lc.FSTDATE = Nothing
            End If

            If pFMORTGT <> String.Empty Then
                lc.FMORTGDT = CDate(pFMORTGT)
            Else
                lc.FMORTGDT = Nothing
            End If
            If pNote <> String.Empty Then
                lc.FPCNOTE = pNote
            Else
                lc.FPCNOTE = Nothing
            End If

            If pKeyCode <> String.Empty Then
                lc.FPCPROVCD = pKeyCode.Substring(0, 2)
                lc.FPCCITYCD = pKeyCode.Substring(2, 2)
            Else
                lc.FPCPROVCD = Nothing
                lc.FPCCITYCD = Nothing
            End If

            db.FD11PROP.Add(lc)
            db.SaveChanges()
            Return True
        End Using
    End Function

#Region "SubDate"
    Function SubDate(ByVal TempDate As String) As String
        TempDate = TempDate.Split("/")(1) & "/" & TempDate.Split("/")(0) & "/" & TempDate.Split("/")(2)
        Return TempDate
    End Function
#End Region
#End Region

End Class

Imports SPW.BLL
Imports SPW.DAL
Imports System.Text
Imports System.Threading
Imports System.Globalization
Imports System.Resources
Imports System.IO
Imports System.Security.Cryptography
Imports System.Configuration

Public Class cImportPriceList

#Region "ADI_ImportPriceList.aspx"
    Public Function GetPhaseCheck(ByVal _FREPRJNO As String, ByVal _FREPHASE As String) As List(Of ED03UNIT)
        'FSERIALNO
        Using db As New PNSWEBEntities
            Dim lc As List(Of ED03UNIT) = db.ED03UNIT.Where(Function(s) s.FREPRJNO = _FREPRJNO And s.FREPHASE = _FREPHASE).ToList
            Return lc
        End Using

    End Function

    Public Function GetFtrnNoMax(ByVal pYear As String, ByVal pFtrnCD As String) As ED11PAJ1
        Try
            Using db As New PNSWEBEntities
                Dim lst As List(Of ED11PAJ1) = db.ED11PAJ1.Where(Function(s) s.FTRNYEAR = pYear And s.FTRNCD = pFtrnCD).OrderByDescending(Function(s) s.FTRNNO).ToList
                Dim lc As ED11PAJ1 = lst.First()
                Return lc
            End Using
        Catch ex As Exception
        End Try

    End Function


    Public Function GetED04RECF(ByVal pPhase As String,
                                 ByVal drHead As DataRow) As ED04RECF
        Try
            Using db As New PNSWEBEntities
                Dim lst As List(Of ED04RECF) = db.ED04RECF.Where(Function(s) s.FREPRJNO = drHead("ProjectCode").ToString And s.FREPHASE = pPhase And s.FREZONE = drHead("Zone").ToString).ToList
                Dim lc As ED04RECF = lst.First()
                Return lc
            End Using
        Catch ex As Exception
        End Try

    End Function

    Public Function AddUploadAll(ByRef dtFileUpload As DataTable,
                                 ByRef dtGroupZoneHead As DataTable,
                                 ByVal pPublicUtilityPerMonth As String,
                                 ByVal pPublicUtilityPerMonthMoney As String,
                                 ByVal pDownPayment As String,
                                 ByVal pPhase As String,
                                 ByVal pUserId As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim pDate As DateTime = Date.Now
                Dim pYear As String = ""
                Dim sFtrnNO As String = ""
                Dim drDetail() As DataRow
                Dim iItemNo As Integer = 0

                Dim listLC As List(Of ED11PAJ1) = New List(Of ED11PAJ1)
                Dim listLC2 As List(Of ED11PAJ2) = New List(Of ED11PAJ2)
                Dim listLCAdd As List(Of ED04RECF) = New List(Of ED04RECF)
                pDate = Date.Now
                pYear = pDate.ToString("yyyy", New CultureInfo("en-US"))
                For Each drHead As DataRow In dtGroupZoneHead.Rows

                    drDetail = dtFileUpload.Select("Zone='" & drHead("Zone").ToString & "'")
                    If drDetail IsNot Nothing Then

                        'AddED11PAJ1
                        Dim lc As ED11PAJ1 = New ED11PAJ1

                        lc.FTRNYEAR = pYear                     'ปี                      
                        lc.FTRNCD = "PA"                        'PA
                        If sFtrnNO = "" Then
                            Dim lcFtrnNO As ED11PAJ1 = GetFtrnNoMax(pYear, lc.FTRNCD)
                            If lcFtrnNO IsNot Nothing Then
                                'PA1400001
                                sFtrnNO = lc.FTRNCD & pYear.Substring(2, 2) & String.Format("{0:D5}", CInt(lcFtrnNO.FTRNNO.Substring(4) + 1))
                            Else
                                sFtrnNO = lc.FTRNCD & pYear.Substring(2, 2) & "00001"
                            End If
                        Else
                            sFtrnNO = sFtrnNO.Substring(0, 4) & String.Format("{0:D5}", CInt(sFtrnNO.Substring(4) + 1))
                        End If
                        ' ftrncd & ftrnyear.sustring(2, 2) & select max(FTRNNO) from ed11paj1  where ftrnyear = '2017' and ftrncd = 'PA' (Max + 1)
                        'FtrnNO
                        drHead("FtrnNO") = sFtrnNO
                        lc.FTRNNO = sFtrnNO                    'Adjustment #          
                        lc.FVOUDATE = pDate                     'Issue Date             วันที่นำเข้า
                        lc.FMDATE = pDate                       'Effective Date         วันที่ Effective
                        lc.FUPDFLAG = Nothing                   'Update Flag            Null
                        'เพิ่ม space ให้รองรับกับระบบเก่า
                        lc.FREPRJNO = drHead("ProjectCode").ToString + " " 'รหัสโครงการ              รหัสโครงการ
                        lc.FREPHASE = pPhase                    'เฟส                    เฟส
                        lc.FREZONE = drHead("Zone").ToString        'โซน                    โซน
                        If drHead("LandPrice").ToString = String.Empty Then 'ราคาที่ดินปกติ             ราคาที่ดิน
                            lc.FAREAUPRC = 0
                        Else
                            lc.FAREAUPRC = drHead("LandPrice").ToString.Replace(",", "")
                        End If
                        lc.FADJTYPE = 1                         '1 แบบกลุม , 2 แบบเจาะจง
                        If pDownPayment = String.Empty Then '% เงินดาวน์
                            lc.FMDOWN = 0
                        Else
                            lc.FMDOWN = pDownPayment.Replace(",", "")
                        End If
                        If pPublicUtilityPerMonthMoney = String.Empty Then 'ค่าสาธารณูปโภค A		กี่บาท
                            lc.FPPUBLICA = 0
                        Else
                            lc.FPPUBLICA = pPublicUtilityPerMonthMoney.Replace(",", "")
                        End If
                        'oad
                        'db.ED11PAJ1.Add(lc)
                        listLC.Add(lc)
                        For Each dr As DataRow In drDetail
                            'AddED11PAJ2
                            Dim lc2 As ED11PAJ2 = New ED11PAJ2
                            dr("FtrnNO") = sFtrnNO
                            lc2.FTRNNO = sFtrnNO '**** Adjustment #		
                            lc2.FPDCODE = dr("Type").ToString 'แบบบ้าน	
                            If dr("StandardPrice").ToString = String.Empty Then 'Standard Price		
                                lc2.FSTDPRICE = 0
                            Else
                                lc2.FSTDPRICE = dr("StandardPrice").ToString.Replace(",", "")
                            End If
                            lc2.FSERIALNO = dr("UnitNo").ToString '**** แปลง        เลขที่แปลง
                            iItemNo = iItemNo + 1
                            lc2.FITEMNO = iItemNo 'ลำดับ		Running (1, 2)
                            lc2.FPRICEREM = dr("Remark").ToString 'Remark		


                            If dr("CostPrice").ToString = String.Empty Then 'ราคาต้นทุน		
                                lc2.FCOSTPRICE = 0
                            Else
                                lc2.FCOSTPRICE = dr("CostPrice").ToString.Replace(",", "")
                            End If

                            If dr("GPTarget").ToString = String.Empty Then 'GP Target		
                                lc2.FGPFIELD = 0
                            Else
                                lc2.FGPFIELD = dr("GPTarget").ToString.Replace(",", "")
                            End If

                            If dr("PriceList").ToString = String.Empty Then 'Price List		
                                lc2.FGPPRICE = 0
                            Else
                                lc2.FGPPRICE = dr("PriceList").ToString.Replace(",", "")
                            End If
                            If dr("LocationPrice").ToString = String.Empty Then 'Location Price		
                                lc2.FLOCATIONPRICE = 0
                            Else
                                lc2.FLOCATIONPRICE = dr("LocationPrice").ToString.Replace(",", "")
                            End If
                            If dr("LocationPrice").ToString = String.Empty Then 'Target Price	
                                lc2.FTARGETPRICE = 0
                            Else
                                lc2.FTARGETPRICE = dr("TargetPrice").ToString.Replace(",", "")
                            End If
                            listLC2.Add(lc2)
                            'db.ED11PAJ2.Add(lc2)

                        Next


                        'AddED04RECF
                        'freprjno**** รหัสโครงการ
                        'FREphase**** เฟส
                        'FREZone**** โซน
                        'fpbooka จำนวนเงินจอง
                        'frcontract	% เงินทำสัญญา
                        'frdown	% เงินดาวน์
                        'fmpublic    ค่าสาธารณูปโภคต่อเดือน		กี่บาท
                        'fppublica   ค่าสาธารณูปโภค A		กี่เดือน
                        'flastupd    วันเวลาที่ Update
                        'fupdby  ผู้ Update
                        'fupdno  FTRNNO Table ed11paj1
                        Dim pProjectCode As String = drHead("ProjectCode").ToString.Trim
                        Dim pZone As String = drHead("Zone").ToString
                        Dim lcEdit = db.ED04RECF.Where(Function(s) s.FREPRJNO.Trim = pProjectCode And s.FREPHASE = pPhase And s.FREZONE = pZone).SingleOrDefault

                        If lcEdit IsNot Nothing Then

                            'lcEdit.FREPRJNO = drHead("ProjectCode").ToString
                            'lcEdit.FREPHASE = pPhase
                            'lcEdit.FREZONE = drHead("Zone").ToString
                            If drHead("BookingAmount").ToString = String.Empty Then 'Booking Amount	 จำนวนเงินจอง
                                lcEdit.FPBOOKA = 0
                            Else
                                lcEdit.FPBOOKA = drHead("BookingAmount").ToString.Replace(",", "")
                            End If

                            'FRCONTRACT-% เงินทำสัญญา,FPCONTRACTA- เงินทำสัญญา
                            'If drHead("ContractAmount").ToString = String.Empty Then 'Contract Amount เงินทำสัญญา
                            '    lcEdit.FRCONTRACT = 0
                            'Else
                            '    lcEdit.FRCONTRACT = drHead("ContractAmount").ToString.Replace(",", "")
                            'End If
                            If drHead("ContractAmount").ToString = String.Empty Then 'Contract Amount เงินทำสัญญา
                                lcEdit.FPCONTRACTA = 0
                            Else
                                lcEdit.FPCONTRACTA = drHead("ContractAmount").ToString.Replace(",", "")
                            End If

                            If pDownPayment = String.Empty Then '% เงินดาวน์
                                lcEdit.FRDOWN = 0
                            Else
                                lcEdit.FRDOWN = pDownPayment.Replace(",", "")
                            End If
                            If pPublicUtilityPerMonth = String.Empty Then 'ค่าสาธารณูปโภคต่อเดือน		กี่เดือน
                                lcEdit.FMPUBLIC = 0
                            Else
                                lcEdit.FMPUBLIC = pPublicUtilityPerMonth.Replace(",", "")
                            End If
                            If pPublicUtilityPerMonthMoney = String.Empty Then 'ค่าสาธารณูปโภค A		กี่บาท
                                lcEdit.FPPUBLICA = 0
                            Else
                                lcEdit.FPPUBLICA = pPublicUtilityPerMonthMoney.Replace(",", "")
                            End If
                            lcEdit.FLASTUPD = Date.Now   'วันเวลาที่ Update
                            lcEdit.FUPDBY = pUserId  'ผู้ Update
                            lcEdit.FUPDNO = sFtrnNO 'FTRNNO Table ed11paj1

                        Else
                            Dim lcAdd As ED04RECF = New ED04RECF

                            'เพิ่ม space ให้รองรับกับระบบเก่า
                            lcAdd.FREPRJNO = drHead("ProjectCode").ToString + " " 'รหัสโครงการ 
                            lcAdd.FREPHASE = pPhase
                            lcAdd.FREZONE = drHead("Zone").ToString
                            If drHead("BookingAmount").ToString = String.Empty Then 'Booking Amount	 จำนวนเงินจอง
                                lcAdd.FPBOOKA = 0
                            Else
                                lcAdd.FPBOOKA = drHead("BookingAmount").ToString.Replace(",", "")
                            End If

                            'If drHead("ContractAmount").ToString = String.Empty Then 'Contract Amount เงินทำสัญญา
                            '    lcAdd.FRCONTRACT = 0
                            'Else
                            '    lcAdd.FRCONTRACT = drHead("ContractAmount").ToString.Replace(",", "")
                            'End If
                            If drHead("ContractAmount").ToString = String.Empty Then 'Contract Amount เงินทำสัญญา
                                lcAdd.FPCONTRACTA = 0
                            Else
                                lcAdd.FPCONTRACTA = drHead("ContractAmount").ToString.Replace(",", "")
                            End If
                            If pDownPayment = String.Empty Then '% เงินดาวน์
                                lcAdd.FRDOWN = 0
                            Else
                                lcAdd.FRDOWN = pDownPayment.Replace(",", "")
                            End If
                            If pPublicUtilityPerMonth = String.Empty Then 'ค่าสาธารณูปโภคต่อเดือน		กี่เดือน
                                lcAdd.FMPUBLIC = 0
                            Else
                                lcAdd.FMPUBLIC = pPublicUtilityPerMonth.Replace(",", "")
                            End If
                            If pPublicUtilityPerMonthMoney = String.Empty Then 'ค่าสาธารณูปโภค A		กี่บาท
                                lcAdd.FPPUBLICA = 0
                            Else
                                lcAdd.FPPUBLICA = pPublicUtilityPerMonthMoney.Replace(",", "")
                            End If
                            lcAdd.FLASTUPD = Date.Now   'วันเวลาที่ Update
                            lcAdd.FUPDBY = pUserId  'ผู้ Update
                            lcAdd.FUPDNO = sFtrnNO 'FTRNNO Table ed11paj1

                            'db.ED04RECF.Add(lcAdd)
                            'db.SaveChanges()
                            'listLCAdd.Add(lcAdd)
                        End If

                    End If

                Next
                If listLCAdd.Count > 0 Then
                    db.ED04RECF.AddRange(listLCAdd)
                    db.SaveChanges()
                End If
                'For Each a In listLC
                '    db.ED11PAJ1.Add(a)
                '    db.SaveChanges()
                'Next

                'For Each a In listLC2
                '    db.ED11PAJ2.Add(a)
                '    db.SaveChanges()
                'Next
                db.ED11PAJ1.AddRange(listLC)
                db.ED11PAJ2.AddRange(listLC2)
                db.SaveChanges()


                Return True
            End Using

        Catch ex As Exception
            Return False
        End Try
    End Function



    'max(FTRNNO) from ed11paj1
#End Region

#Region "Dropdownlist"
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

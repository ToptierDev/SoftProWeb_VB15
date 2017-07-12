Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cSetTypeHouse

#Region "ORG_SetTypeHouse.aspx"

    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal pUserID As String,
                             ByVal pTypeHouse As String,
                             ByVal strLG As String) As List(Of SetTypeHouse_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.SD05PDDS
                       Group Join mTypeHouse In db.SD03TYPE On l.FTYCODE Equals mTypeHouse.FTYCODE
                           Into mTypeh = Group From mTypeHouse In mTypeh.DefaultIfEmpty()
                       Group Join mTypeCreate In db.ZTypeHouseCons On l.FDUTYCODE Equals mTypeCreate.TypeHouseConsCode
                           Into mTypec = Group From mTypeCreate In mTypec.DefaultIfEmpty()
                       Where mTypeHouse.FENTTYPE = "1"
                       Select New SetTypeHouse_ViewModel With {
                               .TypeCode = l.FTYCODE,
                               .TypeDescription = mTypeHouse.FDESC,
                               .TypeHouseCode = l.FPDCODE,
                               .TypeHouseName = l.FPDNAMET,
                               .TypeCreate = mTypeCreate.TypeHouseConsDesc
                           }

            If pTypeHouse <> String.Empty Then
                qury = qury.Where(Function(s) s.TypeCode = pTypeHouse)
            End If
            qury = qury.OrderBy(Function(s) s.TypeHouseCode)


            If qury IsNot Nothing Then
                TotalRow = qury.Count
            End If

            If (fillter.PageSize > 0 And fillter.Page >= 0) Then
                qury = qury.Skip((fillter.Page - 1) * fillter.PageSize).Take(fillter.PageSize)
            End If

            Dim lists = qury.ToList()
            Return lists
        End Using
    End Function

    Public Function GetByID(ByVal id As String,
                            ByVal strUserID As String) As SD05PDDS

        Using db As New PNSWEBEntities
            Dim lc As SD05PDDS = db.SD05PDDS.Where(Function(s) s.FPDCODE = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function GetByIDSD05PDSP(ByVal id As String,
                                    ByVal strUserID As String) As SD05PDSP

        Using db As New PNSWEBEntities
            Dim lc As SD05PDSP = db.SD05PDSP.Where(Function(s) s.FPDCODE = id).FirstOrDefault
            Return lc
        End Using

    End Function

    Public Function Edit(ByVal pTypeHouseCode As String,
                         ByVal pTypeCreate As String,
                         ByVal pTypeHouse As String,
                         ByVal pNameEN As String,
                         ByVal pNameTH As String,
                         ByVal pNames As String,
                         ByVal pBOI As Boolean,
                         ByVal pModel As String,
                         ByVal pUnitEN As String,
                         ByVal pUnitThai As String,
                         ByVal pSQW As String,
                         ByVal pSQM As String,
                         ByVal pBSQM As String,
                         ByVal pPSQM As String,
                         ByVal pMeter As String,
                         ByVal pBedRoom As String,
                         ByVal pBathRoom As String,
                         ByVal pMeterWater As String,
                         ByVal pNote As String,
                         ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As SD05PDDS = db.SD05PDDS.Where(Function(s) s.FPDCODE = pTypeHouseCode).SingleOrDefault
            If lc IsNot Nothing Then
                If pTypeCreate <> String.Empty Then
                    lc.FDUTYCODE = pTypeCreate
                Else
                    lc.FDUTYCODE = Nothing
                End If
                If pTypeHouse <> String.Empty Then
                    lc.FTYCODE = pTypeHouse
                Else
                    lc.FTYCODE = Nothing
                End If
                If pNameEN <> String.Empty Then
                    lc.FPDNAME = pNameEN
                Else
                    lc.FPDNAME = Nothing
                End If
                If pNameTH <> String.Empty Then
                    lc.FPDNAMET = pNameTH
                Else
                    lc.FPDNAMET = Nothing
                End If
                If pNames <> String.Empty Then
                    lc.FPDSHORTNM = pNames
                Else
                    lc.FPDSHORTNM = Nothing
                End If
                lc.FPTYPE = IIf(pBOI, "Y", "N")
                If pModel <> String.Empty Then
                    lc.FMODEL = pModel
                Else
                    lc.FMODEL = Nothing
                End If
                If pUnitEN <> String.Empty Then
                    lc.FUNITM = pUnitEN
                Else
                    lc.FUNITM = Nothing
                End If
                If pUnitThai <> String.Empty Then
                    lc.FUNITMT = pUnitThai
                Else
                    lc.FUNITMT = Nothing
                End If
                If pSQW <> String.Empty Then
                    lc.FSTDAREA = CDbl(pSQW)
                Else
                    lc.FSTDAREA = Nothing
                End If
                If pBSQM <> String.Empty Then
                    lc.FSTDBUILT = CDbl(pBSQM)
                Else
                    lc.FSTDBUILT = Nothing
                End If
                If pPSQM <> String.Empty Then
                    lc.FPSQM = CDbl(pPSQM)
                Else
                    lc.FPSQM = Nothing
                End If
                If pMeter <> String.Empty Then
                    lc.FEMETERSZ = pMeter
                Else
                    lc.FEMETERSZ = Nothing
                End If
                If pBedRoom <> String.Empty Then
                    lc.FNOBEDRM = CDbl(pBedRoom)
                Else
                    lc.FNOBEDRM = Nothing
                End If
                If pBathRoom <> String.Empty Then
                    lc.FNOBATHRM = CDbl(pBathRoom)
                Else
                    lc.FNOBATHRM = Nothing
                End If
                If pMeterWater <> String.Empty Then
                    lc.FWMETERSZ = CDbl(pMeterWater)
                Else
                    lc.FWMETERSZ = Nothing
                End If

                'lc.FUPDBY = pUserId
                lc.FENTDATE = DateTime.Now

                Dim lst As List(Of SD05PDSP) = db.SD05PDSP.Where(Function(s) s.FPDCODE = pTypeHouseCode).ToList
                For Each u As SD05PDSP In lst
                    db.SD05PDSP.Remove(u)
                Next
                Dim ls As SD05PDSP = New SD05PDSP
                ls.FPDCODE = pTypeHouseCode
                If pNote <> String.Empty Then
                    ls.FDESC = pNote
                End If
                If pTypeHouse <> String.Empty Then
                    ls.FITEMNO = pTypeHouse
                End If
                If pTypeCreate <> String.Empty Then
                    ls.FITEMTYPE = pTypeCreate
                End If

                db.SD05PDSP.Add(ls)

            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Add(ByVal pTypeHouseCode As String,
                        ByVal pTypeCreate As String,
                        ByVal pTypeHouse As String,
                        ByVal pNameEN As String,
                        ByVal pNameTH As String,
                        ByVal pNames As String,
                        ByVal pBOI As Boolean,
                        ByVal pModel As String,
                        ByVal pUnitEN As String,
                        ByVal pUnitThai As String,
                        ByVal pSQW As String,
                        ByVal pSQM As String,
                        ByVal pBSQM As String,
                        ByVal pPSQM As String,
                        ByVal pMeter As String,
                        ByVal pBedRoom As String,
                        ByVal pBathRoom As String,
                        ByVal pMeterWater As String,
                        ByVal pNote As String,
                        ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As SD05PDDS = New SD05PDDS
            If pTypeHouseCode <> String.Empty Then
                lc.FPDCODE = pTypeHouseCode
            End If
            If pTypeCreate <> String.Empty Then
                lc.FDUTYCODE = pTypeCreate
            Else
                lc.FDUTYCODE = Nothing
            End If
            If pTypeHouse <> String.Empty Then
                lc.FTYCODE = pTypeHouse
            Else
                lc.FTYCODE = Nothing
            End If
            If pNameEN <> String.Empty Then
                lc.FPDNAME = pNameEN
            Else
                lc.FPDNAME = Nothing
            End If
            If pNameTH <> String.Empty Then
                lc.FPDNAMET = pNameTH
            Else
                lc.FPDNAMET = Nothing
            End If
            If pNames <> String.Empty Then
                lc.FPDSHORTNM = pNames
            Else
                lc.FPDSHORTNM = Nothing
            End If
            lc.FPTYPE = IIf(pBOI, "Y", "N")
            If pModel <> String.Empty Then
                lc.FMODEL = pModel
            Else
                lc.FMODEL = Nothing
            End If
            If pUnitEN <> String.Empty Then
                lc.FUNITM = pUnitEN
            Else
                lc.FUNITM = Nothing
            End If
            If pUnitThai <> String.Empty Then
                lc.FUNITMT = pUnitThai
            Else
                lc.FUNITMT = Nothing
            End If
            If pSQW <> String.Empty Then
                lc.FSTDAREA = CDbl(pSQW)
            Else
                lc.FSTDAREA = Nothing
            End If
            If pBSQM <> String.Empty Then
                lc.FSTDBUILT = CDbl(pBSQM)
            Else
                lc.FSTDBUILT = Nothing
            End If
            If pPSQM <> String.Empty Then
                lc.FPSQM = CDbl(pPSQM)
            Else
                lc.FPSQM = Nothing
            End If
            If pMeter <> String.Empty Then
                lc.FEMETERSZ = pMeter
            Else
                lc.FEMETERSZ = Nothing
            End If
            If pBedRoom <> String.Empty Then
                lc.FNOBEDRM = CDbl(pBedRoom)
            Else
                lc.FNOBEDRM = Nothing
            End If
            If pBathRoom <> String.Empty Then
                lc.FNOBATHRM = CDbl(pBathRoom)
            Else
                lc.FNOBATHRM = Nothing
            End If
            If pMeterWater <> String.Empty Then
                lc.FWMETERSZ = CDbl(pMeterWater)
            Else
                lc.FWMETERSZ = Nothing
            End If

            'lc.FUPDBY = pUserId
            lc.FENTDATE = DateTime.Now

            Dim lst As List(Of SD05PDSP) = db.SD05PDSP.Where(Function(s) s.FPDCODE = pTypeHouseCode).ToList
            For Each u As SD05PDSP In lst
                db.SD05PDSP.Remove(u)
            Next
            Dim ls As SD05PDSP = New SD05PDSP
            ls.FPDCODE = pTypeHouseCode
            If pNote <> String.Empty Then
                ls.FDESC = pNote
            End If
            If pTypeHouse <> String.Empty Then
                ls.FITEMNO = pTypeHouse
            End If
            If pTypeCreate <> String.Empty Then
                ls.FITEMTYPE = pTypeCreate
            End If

            db.SD05PDDS.Add(lc)
            db.SD05PDSP.Add(ls)
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Delete(ByVal id As String,
                           ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As SD05PDDS = db.SD05PDDS.Where(Function(s) s.FPDCODE = id).SingleOrDefault
                db.SD05PDDS.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function


#Region "Dropdownlist"
    Public Function LoadTypeHouseEdit(ByVal pID As String) As SD03TYPE
        Using db As New PNSWEBEntities
            Dim lc As SD03TYPE = db.SD03TYPE.Where(Function(s) s.FTYCODE = pID).SingleOrDefault
            Return lc
        End Using
    End Function
    Public Function LoadTypeHouse() As List(Of ZTypeHouseCon)
        Using db As New PNSWEBEntities
            Dim qury = db.ZTypeHouseCons.OrderBy(Function(s) s.TypeHouseConsCode).ToList
            qury = qury.Select(Function(m) New ZTypeHouseCon With {.TypeHouseConsCode = m.TypeHouseConsCode, .TypeHouseConsDesc = String.Format("{0} - {1}", m.TypeHouseConsCode, m.TypeHouseConsDesc)}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function

    Public Function LoadTypeHouseSD03TYPE() As List(Of SD03TYPE)
        Using db As New PNSWEBEntities
            Dim qury = db.SD03TYPE.Where(Function(s) s.FENTTYPE = "1").OrderBy(Function(s) s.FTYCODE).ToList
            qury = qury.Select(Function(m) New SD03TYPE With {.FTYCODE = m.FTYCODE, .FDESC = String.Format("{0} - {1}", m.FTYCODE, m.FDESC)}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function

    Public Function LoadTypeHouse(ByVal pKeyID As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                Dim qury = (From m In db.SD03TYPE.Where(Function(s) s.FENTTYPE = "1").OrderBy(Function(s) s.FTYCODE)
                            Select m.FTYCODE & "-" & m.FDESC).ToList()

                Return qury.ToList()
            ElseIf pKeyID <> String.Empty Then
                Dim qury = (From m In db.SD03TYPE.Where(Function(s) s.FENTTYPE = "1").OrderBy(Function(s) s.FTYCODE)
                            Select m.FTYCODE & "-" & m.FDESC) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                Return qury.ToList()
            End If
        End Using
        Return Nothing
    End Function
    Public Function LoadTypeCreateEdit(ByVal pID As String) As ZTypeHouseCon
        Using db As New PNSWEBEntities
            Dim lc As ZTypeHouseCon = db.ZTypeHouseCons.Where(Function(s) s.TypeHouseConsCode = pID).SingleOrDefault
            Return lc
        End Using
    End Function
    Public Function LoadTypeCreate(ByVal pKeyID As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                Dim qury = (From m In db.ZTypeHouseCons.OrderBy(Function(s) s.TypeHouseConsCode)
                            Select m.TypeHouseConsCode & "-" & m.TypeHouseConsDesc).ToList()

                Return qury.ToList()
            ElseIf pKeyID <> String.Empty Then
                Dim qury = (From m In db.ZTypeHouseCons.OrderBy(Function(s) s.TypeHouseConsCode)
                            Select m.TypeHouseConsCode & "-" & m.TypeHouseConsDesc) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                Return qury.ToList()
            End If
        End Using
        Return Nothing
    End Function
#End Region


    Public Function getUsedMaster() As List(Of String)

        Using db As New PNSWEBEntities
            Dim query = (From a In db.ED03UNIT Select a.FPDCODE) _
                        .Where(Function(s) s <> "").Distinct.ToList


            Return query
        End Using
    End Function
#End Region

End Class

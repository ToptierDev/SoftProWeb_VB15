Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cDivision

#Region "MST_Division.aspx"
    Public Function LoaddataDivision(ByVal fillter As FillterSearch,
                                     ByRef TotalRow As Integer,
                                     ByVal strUserID As String) As List(Of BD10DIVI)

        Using db As New PNSWEBEntities
            Dim lc As List(Of BD10DIVI) = db.BD10DIVI.OrderBy(Function(s) s.FDIVCODE).ToList
            Return lc.ToList
        End Using

    End Function

    Public Function GetDivisionByID(ByVal id As String,
                                    ByVal strUserID As String) As BD10DIVI

        Using db As New PNSWEBEntities
            Dim lc As BD10DIVI = db.BD10DIVI.Where(Function(s) s.FDIVCODE = id).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function GetDivisionByComID(ByVal pComID As String,
                                       ByVal strUserID As String) As BD10DIVI

        Using db As New PNSWEBEntities
            Dim lc As BD10DIVI = db.BD10DIVI.Where(Function(s) s.COMID = pComID).SingleOrDefault
            Return lc
        End Using

    End Function

    Public Function GetDivisionUserCompanyByComID(ByVal pComID As String,
                                                  ByVal strUserID As String) As List(Of CoreUsersCompany)

        Using db As New PNSDBWEBEntities
            Dim lc As List(Of CoreUsersCompany) = db.CoreUsersCompanies.Where(Function(s) s.COMID = pComID).ToList
            Return lc
        End Using

    End Function

    Public Function DivisionDelete(ByVal id As String,
                                   ByVal strUserID As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As BD10DIVI = db.BD10DIVI.Where(Function(s) s.FDIVCODE = id).SingleOrDefault
                db.BD10DIVI.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DivisionsEdit(ByVal pCode As String,
                                  ByVal pComID As String,
                                  ByVal pNameEN As String,
                                  ByVal pNameTH As String,
                                  ByVal pMG As String,
                                  ByVal pAddress1 As String,
                                  ByVal pAddress2 As String,
                                  ByVal pAddress3 As String,
                                  ByVal pPostal As String,
                                  ByVal pTaxNo As String,
                                  ByVal pSocialID As String,
                                  ByVal pSocialBranch As String,
                                  ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As BD10DIVI = db.BD10DIVI.Where(Function(s) s.FDIVCODE = pCode).SingleOrDefault
            If lc IsNot Nothing Then
                lc.COMID = pComID
                lc.FDIVNAME = pNameEN
                lc.FDIVNAMET = pNameTH
                lc.FDIVMG = pMG
                lc.FADD1 = pAddress1
                lc.FADD2 = pAddress2
                lc.FADD3 = pAddress3
                lc.FPOSTAL = pPostal
                lc.FCOMPTAXNO = pTaxNo
                lc.FSOCIALNO = pSocialID
                lc.FSCBRANCH = pSocialBranch
            End If
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function DivisionAdd(ByVal pCode As String,
                                ByVal pComID As String,
                                ByVal pNameEN As String,
                                ByVal pNameTH As String,
                                ByVal pMG As String,
                                ByVal pAddress1 As String,
                                ByVal pAddress2 As String,
                                ByVal pAddress3 As String,
                                ByVal pPostal As String,
                                ByVal pTaxNo As String,
                                ByVal pSocialID As String,
                                ByVal pSocialBranch As String,
                                ByVal pUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As BD10DIVI = New BD10DIVI
            Dim pmaxid As String = db.BD10DIVI.Select(Function(s) CInt(s.FDIVCODE)).OrderByDescending(Function(s) CInt(s)).FirstOrDefault
            If pmaxid.Length = 1 Then
                pmaxid = "0" & CInt(pmaxid) + 1
            Else
                pmaxid += 1
            End If
            lc.FDIVCODE = pmaxid
            lc.COMID = pComID
            lc.FDIVNAME = pNameEN
            lc.FDIVNAMET = pNameTH
            lc.FDIVMG = pMG
            lc.FADD1 = pAddress1
            lc.FADD2 = pAddress2
            lc.FADD3 = pAddress3
            lc.FPOSTAL = pPostal
            lc.FCOMPTAXNO = pTaxNo
            lc.FSOCIALNO = pSocialID
            lc.FSCBRANCH = pSocialBranch

            db.BD10DIVI.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function getUsedMaster() As List(Of String)

        Using db As New PNSDBWEBEntities
            Dim query = (From a In db.CoreUsersCompanies Select a.COMID).Where(Function(s) s <> "").Distinct.ToList


            Return query
        End Using
    End Function
#End Region

End Class

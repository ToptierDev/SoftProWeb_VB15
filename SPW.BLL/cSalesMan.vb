Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL

Public Class cSalesMan

#Region "MST_Sales.aspx"

    Public Function Loaddata(ByVal fillter As FillterSearch,
                             ByRef TotalRow As Integer,
                             ByVal pSalesManGroup As String,
                             ByVal pDivision As String,
                             ByVal pDepartment As String,
                             ByVal pSection As String,
                             ByVal pZone As String,
                             ByVal pACtive As String,
                             ByVal strLG As String) As List(Of SalesMan_ViewModel)

        Using db As New PNSWEBEntities
            Dim qury = From l In db.LD01SMAN
                       Group Join mSaleG In db.LD08SMCT On mSaleG.FSMTYPE Equals l.FSMTYPE
                       Into mSalesGroup = Group From mSaleG In mSalesGroup.DefaultIfEmpty()
                       Group Join mDiv In db.BD10DIVI On mDiv.FDIVCODE Equals l.FDIVCODE
                       Into mDivision = Group From mDiv In mDivision.DefaultIfEmpty()
                       Group Join mDep In db.BD11DEPT On mDep.FDPCODE Equals l.FDPCODE
                       Into mDepartment = Group From mDep In mDepartment.DefaultIfEmpty()
                       Group Join mZo In db.LD07SLRT On mZo.FSLROUTE Equals l.FSLROUTE
                       Into mZone = Group From mZo In mZone.DefaultIfEmpty()
                       Group Join mSec In db.BD12SECT On mSec.FSECCODE Equals l.FSECCODE
                       Into mSection = Group From mSec In mSection.DefaultIfEmpty()
                       Select New SalesMan_ViewModel With {
                           .SalesManCode = l.FSMCODE,
                           .UserID = l.FUSERID,
                           .NameEN = l.FSMNAME,
                           .NameTH = l.FSMNAMET,
                           .TypeCode = l.FSMTYPE,
                           .DivCode = l.FDIVCODE,
                           .DepCode = l.FDPCODE,
                           .ZoneCode = l.FSLROUTE,
                           .ProjectCode = l.FREPRJNO,
                           .SectionCode = l.FSECCODE,
                           .TypeDescription = mSaleG.FSMTYPEDS,
                           .DivisionDescriptionTH = mDiv.FDIVNAMET,
                           .DivisionDescriptionEN = mDiv.FDIVNAME,
                           .DepartmentDescription = mDep.FDPNAME,
                           .ZoneDescription = mZo.FSLROUTENM,
                           .SectionDescription = mSec.FSECNAME,
                           .status = l.FSMSTATUS
                       }

            If pSalesManGroup <> String.Empty Then
                qury = qury.Where(Function(s) s.TypeCode = pSalesManGroup)
            End If

            If pDivision <> String.Empty Then
                qury = qury.Where(Function(s) s.DivCode = pDivision)
            End If

            If pDepartment <> String.Empty Then
                qury = qury.Where(Function(s) s.DepCode = pDepartment)
            End If

            If pSection <> String.Empty Then
                qury = qury.Where(Function(s) s.SectionCode = pSection)
            End If

            If pZone <> String.Empty Then
                qury = qury.Where(Function(s) s.ZoneCode = pZone)
            End If
            If pACtive = "1" Then
                qury = qury.Where(Function(s) s.status = 1)
            ElseIf pACtive = "2" Then
                qury = qury.Where(Function(s) s.status = 0)
            End If

            qury = qury.OrderBy(Function(s) s.SalesManCode)

            If (fillter.PageSize > 0 And fillter.Page >= 0) Then
                qury = qury.Skip((fillter.Page - 1) * fillter.PageSize).Take(fillter.PageSize)
            End If

            Dim lists = qury.ToList()
            Return lists
        End Using
    End Function

    Public Function LoadEditSalesMan(ByVal pID As String,
                                     ByVal strLG As String) As SalesMan_ViewModel

        Using db As New PNSWEBEntities
            Dim qury = From l In db.LD01SMAN
                       Group Join mSaleG In db.LD08SMCT On mSaleG.FSMTYPE Equals l.FSMTYPE
                       Into mSalesGroup = Group From mSaleG In mSalesGroup.DefaultIfEmpty()
                       Group Join mDiv In db.BD10DIVI On mDiv.FDIVCODE Equals l.FDIVCODE
                       Into mDivision = Group From mDiv In mDivision.DefaultIfEmpty()
                       Group Join mDep In db.BD11DEPT On mDep.FDPCODE Equals l.FDPCODE
                       Into mDepartment = Group From mDep In mDepartment.DefaultIfEmpty()
                       Group Join mZo In db.LD07SLRT On mZo.FSLROUTE Equals l.FSLROUTE
                       Into mZone = Group From mZo In mZone.DefaultIfEmpty()
                       Group Join mSec In db.BD12SECT On mSec.FSECCODE Equals l.FSECCODE
                       Into mSection = Group From mSec In mSection.DefaultIfEmpty()
                       Select New SalesMan_ViewModel With {
                           .SalesManCode = l.FSMCODE,
                           .UserID = l.FUSERID,
                           .NameEN = l.FSMNAME,
                           .NameTH = l.FSMNAMET,
                           .TypeCode = l.FSMTYPE,
                           .DivCode = l.FDIVCODE,
                           .DepCode = l.FDPCODE,
                           .ZoneCode = l.FSLROUTE,
                           .ProjectCode = l.FREPRJNO,
                           .SectionCode = l.FSECCODE,
                           .TypeDescription = mSaleG.FSMTYPEDS,
                           .DivisionDescriptionTH = mDiv.FDIVNAMET,
                           .DivisionDescriptionEN = mDiv.FDIVNAME,
                           .DepartmentDescription = mDep.FDPNAME,
                           .ZoneDescription = mZo.FSLROUTENM,
                           .SectionDescription = mSec.FSECNAME,
                           .status = l.FSMSTATUS
                       }
            If pID <> String.Empty Then
                qury = qury.Where(Function(s) s.SalesManCode = pID)
            End If
            Dim lists = qury.FirstOrDefault
            Return lists
        End Using
    End Function

    Public Function Delete(ByVal pKeyId As String) As Boolean
        Try
            Using db As New PNSWEBEntities
                Dim lc As LD01SMAN = db.LD01SMAN.Where(Function(s) s.FSMCODE = pKeyId).SingleOrDefault

                db.LD01SMAN.Remove(lc)
                db.SaveChanges()
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Add(ByRef pSaleManCode As String,
                        ByVal pUserID As String,
                        ByVal pNameEN As String,
                        ByVal pNameTH As String,
                        ByVal pType As String,
                        ByVal pDiv As String,
                        ByVal pDept As String,
                        ByVal pZone As String,
                        ByVal pSection As String,
                        ByVal pProject As String,
                        ByVal pActive As String,
                        ByVal strUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As LD01SMAN = New LD01SMAN
            Dim pID As Integer = 0
            Dim lcMax As LD01SMAN = db.LD01SMAN.OrderByDescending(Function(s) CInt(s.FSMCODE)).FirstOrDefault
            If lcMax IsNot Nothing Then
                pID = CInt(lcMax.FSMCODE) + 1
            End If
            pSaleManCode = pID
            lc.FSMCODE = pID
            If pUserID <> String.Empty Then
                lc.FUSERID = pUserID
            End If
            If pNameEN <> String.Empty Then
                lc.FSMNAME = pNameEN
            End If
            If pNameTH <> String.Empty Then
                lc.FSMNAMET = pNameTH
            End If
            If pType <> String.Empty Then
                lc.FSMTYPE = pType
            End If
            If pDiv <> String.Empty Then
                lc.FDIVCODE = pDiv
            End If
            If pDept <> String.Empty Then
                lc.FDPCODE = pDept
            End If
            If pZone <> String.Empty Then
                lc.FSLROUTE = pZone
            End If
            If pSection <> String.Empty Then
                lc.FSECCODE = pSection
            End If
            If pProject <> String.Empty Then
                lc.FREPRJNO = pProject
            End If
            If pActive = "1" Then
                lc.FSMSTATUS = 1
            ElseIf pActive = "2" Then
                lc.FSMSTATUS = 0
            End If

            db.LD01SMAN.Add(lc)
            db.SaveChanges()
            Return True
        End Using

    End Function

    Public Function Edit(ByVal pSaleManCode As String,
                         ByVal pUserID As String,
                         ByVal pNameEN As String,
                         ByVal pNameTH As String,
                         ByVal pType As String,
                         ByVal pDiv As String,
                         ByVal pDept As String,
                         ByVal pZone As String,
                         ByVal pSection As String,
                         ByVal pProject As String,
                         ByVal pActive As String,
                         ByVal strUserId As String) As Boolean

        Using db As New PNSWEBEntities
            Dim lc As LD01SMAN = db.LD01SMAN.Where(Function(s) s.FSMCODE = pSaleManCode).SingleOrDefault
            If lc IsNot Nothing Then
                If pUserID <> String.Empty Then
                    lc.FUSERID = pUserID
                Else
                    lc.FUSERID = String.Empty
                End If
                If pNameEN <> String.Empty Then
                    lc.FSMNAME = pNameEN
                Else
                    lc.FSMNAME = String.Empty
                End If
                If pNameTH <> String.Empty Then
                    lc.FSMNAMET = pNameTH
                Else
                    lc.FSMNAMET = String.Empty
                End If
                If pType <> String.Empty Then
                    lc.FSMTYPE = pType
                Else
                    lc.FSMTYPE = String.Empty
                End If
                If pDiv <> String.Empty Then
                    lc.FDIVCODE = pDiv
                Else
                    lc.FDIVCODE = String.Empty
                End If
                If pDept <> String.Empty Then
                    lc.FDPCODE = pDept
                Else
                    lc.FDPCODE = String.Empty
                End If
                If pZone <> String.Empty Then
                    lc.FSLROUTE = pZone
                Else
                    lc.FSLROUTE = String.Empty
                End If
                If pSection <> String.Empty Then
                    lc.FSECCODE = pSection
                Else
                    lc.FSECCODE = String.Empty
                End If
                If pProject <> String.Empty Then
                    lc.FREPRJNO = pProject
                Else
                    lc.FREPRJNO = String.Empty
                End If
                If pActive = "1" Then
                    lc.FSMSTATUS = 1
                ElseIf pActive = "2" Then
                    lc.FSMSTATUS = 0
                End If

                db.SaveChanges()
                Return True
            End If
        End Using
        Return False
    End Function

#Region "Dropdownlist"
    Public Function LoadSalsManGroup() As List(Of LD08SMCT)
        Using db As New PNSWEBEntities
            Dim qury As List(Of LD08SMCT) = db.LD08SMCT.OrderBy(Function(s) s.FSMTYPE).ToList
            qury = qury.Select(Function(m) New LD08SMCT With {.FSMTYPE = m.FSMTYPE, .FSMTYPEDS = String.Format("{0} - {1}", m.FSMTYPE, m.FSMTYPEDS)}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function
    Public Function LoadDivision() As List(Of BD10DIVI)
        Using db As New PNSWEBEntities
            Dim qury As List(Of BD10DIVI) = db.BD10DIVI.OrderBy(Function(s) s.FDIVCODE).ToList
            qury = qury.Select(Function(m) New BD10DIVI With {.FDIVCODE = m.FDIVCODE,
                                                              .FDIVNAMET = String.Format("{0} - {1}", m.FDIVCODE, m.FDIVNAMET),
                                                              .FDIVNAME = String.Format("{0} - {1}", m.FDIVCODE, m.FDIVNAME)}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function
    Public Function LoadDepartment() As List(Of BD11DEPT)
        Using db As New PNSWEBEntities
            Dim qury = db.BD11DEPT.OrderBy(Function(s) s.FDPCODE).ToList
            qury = qury.Select(Function(m) New BD11DEPT With {.FDPCODE = m.FDPCODE, .FDPNAME = String.Format("{0} - {1}", m.FDPCODE, m.FDPNAME)}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function
    Public Function LoadSection(ByVal pDepartment As String) As List(Of BD12SECT)
        Using db As New PNSWEBEntities
            If pDepartment <> String.Empty Then
                Dim qury = db.BD12SECT.Where(Function(s) s.FDPCODE = pDepartment).OrderBy(Function(s) s.FSECCODE).ToList
                qury = qury.Select(Function(m) New BD12SECT With {.FSECCODE = m.FSECCODE, .FSECNAME = String.Format("{0} - {1}", m.FSECCODE, m.FSECNAME)}).ToList()

                Return qury.ToList()
            Else
                Dim qury = db.BD12SECT.OrderBy(Function(s) s.FSECCODE).ToList
                qury = qury.Select(Function(m) New BD12SECT With {.FSECCODE = m.FSECCODE, .FSECNAME = String.Format("{0} - {1}", m.FSECCODE, m.FSECNAME)}).ToList()

                Return qury.ToList()
            End If
        End Using
        Return Nothing
    End Function
    Public Function LoadZone() As List(Of LD07SLRT)
        Using db As New PNSWEBEntities
            Dim qury = db.LD07SLRT.OrderBy(Function(s) s.FSLROUTE).ToList
            qury = qury.Select(Function(m) New LD07SLRT With {.FSLROUTE = m.FSLROUTE, .FSLROUTENM = String.Format("{0} - {1}", m.FSLROUTE, m.FSLROUTENM)}).ToList()

            Return qury.ToList()
        End Using
        Return Nothing
    End Function
    Public Function LoadSalsManGroup(ByVal pKeyID As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                Dim qury = (From m In db.LD08SMCT.OrderBy(Function(s) s.FSMTYPE)
                            Select m.FSMTYPE & "-" & m.FSMTYPEDS).ToList()

                Return qury.ToList()
            ElseIf pKeyID <> String.Empty Then
                Dim qury = (From m In db.LD08SMCT.OrderBy(Function(s) s.FSMTYPE)
                            Select m.FSMTYPE & "-" & m.FSMTYPEDS) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                Return qury.ToList()
            End If
        End Using
        Return Nothing
    End Function
    Public Function LoadDivision(ByVal pKeyID As String,
                                 ByVal pLG As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                If pLG = "TH" Then
                    Dim qury = (From m In db.BD10DIVI.OrderBy(Function(s) s.FDIVCODE)
                                Select m.FDIVCODE & "-" & m.FDIVNAMET).ToList()
                    Return qury.ToList()
                Else
                    Dim qury = (From m In db.BD10DIVI.OrderBy(Function(s) s.FDIVCODE)
                                Select m.FDIVCODE & "-" & m.FDIVNAME).ToList()
                    Return qury.ToList()
                End If

            ElseIf pKeyID <> String.Empty Then
                If pLG = "TH" Then
                    Dim qury = (From m In db.BD10DIVI.OrderBy(Function(s) s.FDIVCODE)
                                Select m.FDIVCODE & "-" & m.FDIVNAMET) _
                            .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()
                    Return qury.ToList()
                Else
                    Dim qury = (From m In db.BD10DIVI.OrderBy(Function(s) s.FDIVCODE)
                                Select m.FDIVCODE & "-" & m.FDIVNAME).ToList() _
                            .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()
                    Return qury.ToList()
                End If
            End If
        End Using
        Return Nothing
    End Function
    Public Function LoadDepartment(ByVal pKeyID As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                Dim qury = (From m In db.BD11DEPT.OrderBy(Function(s) s.FDPCODE)
                            Select m.FDPCODE & "-" & m.FDPNAME).ToList()

                Return qury.ToList()
            ElseIf pKeyID <> String.Empty Then
                Dim qury = (From m In db.BD11DEPT.OrderBy(Function(s) s.FDPCODE)
                            Select m.FDPCODE & "-" & m.FDPNAME) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                Return qury.ToList()
            End If
        End Using
        Return Nothing
    End Function
    Public Function LoadSection(ByVal pKeyID As String,
                                ByVal pDepartment As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pDepartment <> String.Empty Then
                If pKeyID = String.Empty Then
                    Dim qury = (From m In db.BD12SECT.Where(Function(s) s.FDPCODE = pDepartment).OrderBy(Function(s) s.FSECCODE)
                                Select m.FSECCODE & "-" & m.FSECNAME).ToList()

                    Return qury.ToList()
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = (From m In db.BD12SECT.Where(Function(s) s.FDPCODE = pDepartment).OrderBy(Function(s) s.FSECCODE)
                                Select m.FSECCODE & "-" & m.FSECNAME) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                    Return qury.ToList()
                End If
            Else
                If pKeyID = String.Empty Then
                    Dim qury = (From m In db.BD12SECT.OrderBy(Function(s) s.FSECCODE)
                                Select m.FSECCODE & "-" & m.FSECNAME).ToList()

                    Return qury.ToList()
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = (From m In db.BD12SECT.OrderBy(Function(s) s.FSECCODE)
                                Select m.FSECCODE & "-" & m.FSECNAME) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                    Return qury.ToList()
                End If
            End If
        End Using
        Return Nothing
    End Function
    Public Function LoadZone(ByVal pKeyID As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pKeyID = String.Empty Then
                Dim qury = (From m In db.LD07SLRT.OrderBy(Function(s) s.FSLROUTE)
                            Select m.FSLROUTE & "-" & m.FSLROUTENM).ToList()

                Return qury.ToList()
            ElseIf pKeyID <> String.Empty Then
                Dim qury = (From m In db.LD07SLRT.OrderBy(Function(s) s.FSLROUTE)
                            Select m.FSLROUTE & "-" & m.FSLROUTENM) _
                           .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                Return qury.ToList()
            End If
        End Using
        Return Nothing
    End Function
#End Region

    Public Function getUsedMaster() As List(Of String)

        Using db As New PNSWEBEntities
            Dim query = ((From a In db.OD11BKT1 Select a.FSMCODE) _
                        .Union _
                        (From a In db.OD20LAGR Select a.FSMCODE)).Where(Function(s) s <> "").Distinct.ToList


            Return query
        End Using
    End Function

    ''Get Project All
    Public Function GetProjectAll() As List(Of ED01PROJ)

        Using db As New PNSWEBEntities
            Dim lc As List(Of ED01PROJ) = db.ED01PROJ.ToList
            Return lc
        End Using

    End Function

    Public Function GetProjectByUse(ByVal pFREPRJNO As String) As List(Of ED01PROJ)

        Using db As New PNSWEBEntities
            Dim lc As List(Of ED01PROJ) = db.ED01PROJ.Where(Function(s) s.FREPRJNO.Contains(pFREPRJNO)).ToList
            Return lc
        End Using

    End Function

#End Region
End Class

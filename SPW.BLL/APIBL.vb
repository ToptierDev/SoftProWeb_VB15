Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports SPW.BLL
Imports SPW.DAL
Public Class APIBL
#Region "INT_Promotion.aspx"

    Public Function LoadPromotion(ByVal pKeyID As String,
                                  ByVal pLG As String) As List(Of String)
        Using db As New PNSWEBEntities
            If pLG = "TH" Then
                If pKeyID = String.Empty Then
                    Dim qury = (From m In db.SD05PDDS.Where(Function(s) s.FACCD = "1134001").OrderBy(Function(s) s.FPDCODE)
                                Select m.FPDCODE & "-" & m.FPDNAMET).ToList()

                    Return qury.ToList()
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = (From m In db.SD05PDDS.Where(Function(s) s.FACCD = "1134001").OrderBy(Function(s) s.FPDCODE)
                                Select m.FPDCODE & "-" & m.FPDNAMET) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                    Return qury.ToList()
                End If
            Else
                If pKeyID = String.Empty Then
                    Dim qury = (From m In db.SD05PDDS.Where(Function(s) s.FACCD = "1134001").OrderBy(Function(s) s.FPDCODE)
                                Select m.FPDCODE & "-" & m.FPDNAME).ToList()

                    Return qury.ToList()
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = (From m In db.SD05PDDS.Where(Function(s) s.FACCD = "1134001").OrderBy(Function(s) s.FPDCODE)
                                Select m.FPDCODE & "-" & m.FPDNAME) _
                               .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))).ToList()

                    Return qury.ToList()
                End If
            End If
        End Using
        Return Nothing
    End Function

    Public Function LoadProduct(ByVal pKeyID As String,
                                ByVal pLG As String,
                                ByVal ptFlag As Boolean) As List(Of Promotion_ViewModel)
        Using db As New PNSWEBEntities
            If ptFlag = False Then
                If pLG = "TH" Then
                    If pKeyID = String.Empty Then
                        Dim qury = From l In db.SD05PDDS.Where(Function(s) s.FENTTYPE <> "2" And s.FENTTYPE IsNot Nothing And s.FENTTYPE <> "")
                                   Select New Promotion_ViewModel With
                                   {
                                        .Code = l.FPDCODE,
                                        .Description = l.FPDNAMET,
                                        .Unit = l.FUNITMT
                                   }

                        Return qury.Take(50).ToList()
                    ElseIf pKeyID <> String.Empty Then
                        Dim qury = From l In db.SD05PDDS.Where(Function(s) s.FENTTYPE <> "2" And s.FENTTYPE IsNot Nothing And s.FENTTYPE <> "" And (s.FPDCODE.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", "")) Or
                                                      s.FPDNAMET.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", "")) Or
                                                      s.FUNITMT.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))))
                                   Select New Promotion_ViewModel With
                                   {
                                        .Code = l.FPDCODE,
                                        .Description = l.FPDNAMET,
                                        .Unit = l.FUNITMT
                                   }

                        qury = qury

                        Return qury.Take(50).ToList()
                    End If
                Else
                    If pKeyID = String.Empty Then
                        Dim qury = From l In db.SD05PDDS.Where(Function(s) s.FENTTYPE <> "2" And s.FENTTYPE IsNot Nothing And s.FENTTYPE <> "")
                                   Select New Promotion_ViewModel With
                                   {
                                        .Code = l.FPDCODE,
                                        .Description = l.FPDNAME,
                                        .Unit = l.FUNITM
                                   }
                        Return qury.Take(50).ToList()
                    ElseIf pKeyID <> String.Empty Then
                        Dim qury = From l In db.SD05PDDS.Where(Function(s) s.FENTTYPE <> "2" And s.FENTTYPE IsNot Nothing And s.FENTTYPE <> "" And (s.FPDCODE.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", "")) Or
                                                      s.FPDNAME.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", "")) Or
                                                      s.FUNITM.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))))
                                   Select New Promotion_ViewModel With
                                   {
                                        .Code = l.FPDCODE,
                                        .Description = l.FPDNAME,
                                        .Unit = l.FUNITM
                                   }

                        Return qury.Take(50).ToList()
                    End If
                End If
            Else
                If pKeyID = String.Empty Then
                    Dim qury = From l In db.BD41CRDT.Where(Function(s) s.FCREDITCD IsNot Nothing And s.FCREDITCD <> "")
                               Select New Promotion_ViewModel With
                                   {
                                        .Code = l.FCREDITCD,
                                        .Description = l.FCREDITDS,
                                        .Unit = String.Empty
                                   }

                    Return qury.Take(50).ToList()
                ElseIf pKeyID <> String.Empty Then
                    Dim qury = From l In db.BD41CRDT.Where(Function(s) s.FCREDITCD IsNot Nothing And s.FCREDITCD <> "" And (s.FCREDITCD.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", "")) Or
                                                                       s.FCREDITDS.ToUpper.Replace(" ", "").Contains(pKeyID.ToUpper.Replace(" ", ""))))
                               Select New Promotion_ViewModel With
                               {
                                    .Code = l.FCREDITCD,
                                    .Description = l.FCREDITDS,
                                    .Unit = String.Empty
                               }

                    qury = qury

                    Return qury.Take(50).ToList()
                End If
            End If
        End Using
        Return Nothing
    End Function

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

    Public Function GetFPD(ByVal pFPD As String,
                           ByVal ptProject As String) As List(Of String)

        Using db As New PNSWEBEntities

            If pFPD = String.Empty And
               ptProject = String.Empty Then
                Dim olc = (From m In db.SD05PDDS.Where(Function(s) s.FCLASS = "0").OrderBy(Function(s) s.FPDCODE)
                           Select m.FPDCODE & ":" & m.FPDNAME).ToList()

                Return olc.ToList
            ElseIf pFPD = String.Empty And
                   ptProject <> String.Empty Then
                Dim olc = (From m In db.SD05PDDS.Where(Function(s) s.FMODEL = ptProject And s.FCLASS = "0").OrderBy(Function(s) s.FPDCODE)
                           Select m.FPDCODE & ":" & m.FPDNAME).ToList

                Return olc.ToList
            ElseIf pFPD <> String.Empty And
                   ptProject = String.Empty Then
                Dim olc = (From m In db.SD05PDDS.Where(Function(s) s.FCLASS = "0").OrderBy(Function(s) s.FPDCODE)
                           Select m.FPDCODE & ":" & m.FPDNAME) _
                          .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pFPD.ToUpper.Replace(" ", ""))).ToList()

                Return olc.ToList
            ElseIf pFPD <> String.Empty And
                   ptProject <> String.Empty Then
                Dim olc = (From m In db.SD05PDDS.Where(Function(s) s.FMODEL = ptProject And s.FCLASS = "0").OrderBy(Function(s) s.FPDCODE)
                           Select m.FPDCODE & ":" & m.FPDNAME) _
                          .Where(Function(s) s.ToUpper.Replace(" ", "").Contains(pFPD.ToUpper.Replace(" ", ""))).ToList()

                Return olc.ToList
            End If

            Return Nothing
        End Using

    End Function
#End Region

#Region "ORG_Project.aspx"
    Public Function GetAuto(ByVal pKey As String) As List(Of APIProject_ViewModel)

        Using db As New PNSWEBEntities

            If pKey = String.Empty Then
                Dim qury = From m In db.LD07AZIP
                           Select New APIProject_ViewModel With {
                              .Description = m.FCITY & " " & m.FPROVINCE,
                              .Postal = m.FPOSTAL,
                              .KeyID = m.FPROVCD & m.FCITYCD
                          }
                Dim lists = qury.ToList()
                Return lists
            ElseIf pKey <> String.Empty Then
                Dim qury = From m In db.LD07AZIP
                           Select New APIProject_ViewModel With {
                              .Description = m.FCITY & " " & m.FPROVINCE,
                              .Postal = m.FPOSTAL,
                              .KeyID = m.FPROVCD & m.FCITYCD
                          }

                If Not String.IsNullOrEmpty(pKey) Then
                    qury = qury.Where(Function(s) (s.Description.ToUpper.Replace(" ", "").Contains(pKey.ToUpper.Replace(" ", "")) Or
                                                   s.Postal.ToUpper.Replace(" ", "").Contains(pKey.ToUpper.Replace(" ", "")) Or
                                                   s.KeyID.ToUpper.Replace(" ", "").Contains(pKey.ToUpper.Replace(" ", ""))))
                End If

                Dim lists = qury.ToList()
                Return lists
            End If

            Return Nothing
        End Using

    End Function
#End Region

#Region "TRN_DataLandBank.aspx"
    Public Function GetAutoNew(ByVal pKey As String) As List(Of APIProject_ViewModel)

        Using db As New PNSWEBEntities

            If pKey = String.Empty Then
                Dim qury = From m In db.LD07AZIP
                           Select New APIProject_ViewModel With {
                              .Description = m.FCITY & " " & m.FPROVINCE,
                              .Postal = m.FPOSTAL,
                              .KeyID = m.FPROVCD & m.FCITYCD
                          }
                Dim lists = qury.ToList()
                Return lists
            ElseIf pKey <> String.Empty Then
                Dim qury = From m In db.LD07AZIP
                           Select New APIProject_ViewModel With {
                              .Description = m.FCITY & " " & m.FPROVINCE,
                              .Postal = m.FPOSTAL,
                              .KeyID = m.FPROVCD & m.FCITYCD
                          }

                If Not String.IsNullOrEmpty(pKey) Then
                    qury = qury.Where(Function(s) (s.Description.ToUpper.Replace(" ", "").Contains(pKey.ToUpper.Replace(" ", "")) Or
                                                   s.Postal.ToUpper.Replace(" ", "").Contains(pKey.ToUpper.Replace(" ", "")) Or
                                                   s.KeyID.ToUpper.Replace(" ", "").Contains(pKey.ToUpper.Replace(" ", ""))))
                End If

                Dim lists = qury.ToList()
                Return lists
            End If

            Return Nothing
        End Using

    End Function
#End Region

    Public Function getUsedProjectPriceListEdit(ByVal pProjectCode As String,
                                                ByVal pPhase As String,
                                                ByVal pZone As String) As List(Of String)

        Using db As New PNSWEBEntities
            If pProjectCode <> String.Empty And pPhase <> String.Empty Then
                Dim qury = From aj1 In db.ED11PAJ1
                           Join aj2 In db.ED11PAJ2 On aj1.FTRNNO Equals aj2.FTRNNO
                           Join ed03 In db.ED03UNIT.Where(Function(s) s.FRESTATUS <> "0" And s.FRESTATUS IsNot Nothing And s.FRESTATUS <> "")
                           On aj1.FREPRJNO Equals ed03.FREPRJNO And aj1.FREPHASE Equals ed03.FREPHASE And aj1.FREZONE Equals ed03.FREZONE
                           Select New Zone_ViewModel With {
                               .FREPRJNO = aj1.FREPRJNO,
                               .PhaseCode = aj1.FREPHASE,
                               .ZoneCode = aj1.FREZONE,
                               .FSERIALNO = aj2.FSERIALNO
                           }
                If Not String.IsNullOrEmpty(pProjectCode) Then
                    qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProjectCode)
                End If

                If Not String.IsNullOrEmpty(pPhase) Then
                    qury = qury.Where(Function(s) s.PhaseCode = pPhase)
                End If

                If Not String.IsNullOrEmpty(pZone) Then
                    qury = qury.Where(Function(s) s.ZoneCode = pZone)
                End If


                Dim lists = qury.ToList
                Dim str As List(Of String) = lists.Select(Function(s) s.FSERIALNO).Distinct.ToList
                Return str
            End If
        End Using
    End Function

    Public Function getUsedProjectPriceListAdd(ByVal pProjectCode As String,
                                               ByVal pPhase As String,
                                               ByVal pZone As String) As List(Of String)

        Using db As New PNSWEBEntities
            If pProjectCode <> String.Empty And pPhase <> String.Empty Then
                Dim qury = From aj1 In db.ED11PAJ1
                           Join aj2 In db.ED11PAJ2 On aj1.FTRNNO Equals aj2.FTRNNO
                           Select New Zone_ViewModel With {
                               .FREPRJNO = aj1.FREPRJNO,
                               .PhaseCode = aj1.FREPHASE,
                               .ZoneCode = aj1.FREZONE,
                               .FSERIALNO = aj2.FSERIALNO
                           }
                If Not String.IsNullOrEmpty(pProjectCode) Then
                    qury = qury.Where(Function(s) s.FREPRJNO.Trim = pProjectCode)
                End If

                If Not String.IsNullOrEmpty(pPhase) Then
                    qury = qury.Where(Function(s) s.PhaseCode <> pPhase)
                End If

                If Not String.IsNullOrEmpty(pZone) Then
                    qury = qury.Where(Function(s) s.ZoneCode <> pZone)
                End If

                Dim lists = qury.ToList
                Dim str As List(Of String) = lists.Select(Function(s) s.FSERIALNO).Distinct.ToList
                Return str
            End If
        End Using
    End Function
End Class

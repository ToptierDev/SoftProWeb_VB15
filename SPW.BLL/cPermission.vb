Imports SPW.BLL
Imports SPW.DAL


Public Class cPermission

#Region "Project"

    Public Function LoadProject(ByVal sUserId As String) As List(Of ED01PROJ)

        Dim possibleFreprjNo As List(Of String)
        Using db1 As New PNSDBWEBEntities
            Dim possibleFreprjNoCanView As List(Of CoreUsersProject) = db1.CoreUsersProjects.Where(Function(s) s.UserID = sUserId).ToList
            possibleFreprjNo = possibleFreprjNoCanView.Select(Function(s) s.FREPRJNO).ToList()
        End Using

        Using db As New PNSWEBEntities

            Dim qury As List(Of ED01PROJ) = db.ED01PROJ.Where(Function(s) possibleFreprjNo.Contains(s.FREPRJNO)).OrderBy(Function(s) s.FREPRJNO).ToList
            qury = qury.Select(Function(m) New ED01PROJ With {
                                   .FREPRJNO = m.FREPRJNO,
                                   .FREPRJNM = String.Format("{0} - {1}", m.FREPRJNO, m.FREPRJNM),
                                   .FRELOCAT1 = m.FRELOCAT1,
                                   .FRELOCAT2 = m.FRELOCAT2,
                                   .FRELOCAT3 = m.FRELOCAT3,
                                   .FREPROVINC = m.FREPROVINC,
                                   .FREPOSTAL = m.FREPOSTAL,
                                   .FTOTAREA = m.FTOTAREA,
                                   .FNOOFLAND = m.FNOOFLAND,
                                   .FLANDNO = m.FLANDNO
                                   }).ToList()

            Return qury.ToList()
        End Using

    End Function


#End Region


End Class

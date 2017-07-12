Imports SPW.DAL
Imports System.Web.Script.Serialization
Public Class HelperLog
    Public Shared Function AccessLog(ByVal ptUserID As String,
                                     ByVal pnMenuID As Integer,
                                     ByVal ptIPAddress As String) As Boolean
        Try
            Dim lcAccessLog As New CoreAccessLog
            lcAccessLog.FUSERID = ptUserID
            lcAccessLog.MENUID = pnMenuID
            lcAccessLog.ACCESSTIME = Now
            lcAccessLog.IPADDRESS = ptIPAddress
            lcAccessLog.UpdateBy = ptUserID
            lcAccessLog.UpdateDate = Now

            Dim ctx As PNSDBWEBEntities = PNSDBWEBEntities.Context
            Using db As New PNSDBWEBEntities
                db.CoreAccessLogs.Add(lcAccessLog)
                db.SaveChanges()
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



    Public Shared Function ErrorLog(ByVal ptUserID As String,
                                    ByVal pnMenuID As String,
                                    ByVal ptIPAddress As String,
                                    ByVal ptFunction As String,
                                    ByVal ptMessage As Exception) As Boolean
        Try
            Dim lcErrorLog As New CoreErrorLog
            Dim serializer As New JavaScriptSerializer
            'Dim s As String = serializer.Serialize(ptMessage)
            lcErrorLog.MenuID = pnMenuID
            lcErrorLog.UserID = ptUserID
            lcErrorLog.ErrorFunction = ptFunction
            lcErrorLog.ErrorDescription = ptMessage.Message
            lcErrorLog.ErrorDescription1 = ptMessage.StackTrace
            lcErrorLog.ErrorDescription2 = serializer.Serialize(ptMessage.InnerException)
            lcErrorLog.ErrorDescription3 = Nothing
            lcErrorLog.ErrorTime = Now
            lcErrorLog.IPAddress = ptIPAddress
            lcErrorLog.ID = Nothing
            Using db As New PNSDBWEBEntities
                db.CoreErrorLogs.Add(lcErrorLog)
                db.SaveChanges()
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function LoadMenuID(ByVal pManuLocation As String) As String
        Try
            Using db As New PNSDBWEBEntities
                Dim lcMenu As CoreMenu = db.CoreMenus.Where(Function(s) s.EnableFlag = 1 _
                                                                   And s.MenuLocation = pManuLocation.Replace("/", "")).FirstOrDefault

                Return lcMenu.MenuID.ToString

            End Using
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
End Class

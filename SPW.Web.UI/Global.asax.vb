
Imports System.Web.Http
Imports System.Web.Routing
Imports Newtonsoft.Json.Serialization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        RouteTable.Routes.MapHttpRoute(name:="DefaultApi", routeTemplate:="webapi/{controller}/{id}", defaults:=New With {
      Key .id = System.Web.Http.RouteParameter.[Optional]
  })

        'Dim config = GlobalConfiguration.Configuration
        'config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = New CamelCasePropertyNamesContractResolver()
        'config.Formatters.JsonFormatter.UseDataContractJsonSerializer = False

    End Sub
End Class
using AspnetWebApi2Helpers.Serialization;
using AspnetWebApi2Helpers.Serialization.Protobuf;
using CrmApi.Data;
using System.Web.Http;

namespace CrmApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Configure formatters to handle cyclical references
            //config.Formatters.JsonPreserveReferences();
            //config.Formatters.XmlPreserveReferences();
            //config.Formatters.ProtobufPreserveReferences(typeof(Brand).Assembly);


            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling= Newtonsoft.Json.PreserveReferencesHandling.Objects;


            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

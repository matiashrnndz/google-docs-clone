using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //var cors = new EnableCorsAttribute("*", "*", "GET, HEAD, OPTIONS, POST, PUT");
            //config.EnableCors(cors);

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes

            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddUser",
                routeTemplate: "api/users/{useremail}",
                defaults: new { useremail = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "AddDocument",
               routeTemplate: "api/users/{useremail}/documents/{documentid}",
               defaults: new { useremail = RouteParameter.Optional, documentid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddHeader",
                routeTemplate: "api/documents/{documentid}/headers/{headerid}",
                defaults: new { documentid = RouteParameter.Optional, headerid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddParagraph",
                routeTemplate: "api/documents/{documentid}/paragraphs/{paragraphid}",
                defaults: new { documentid = RouteParameter.Optional, paragraphid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddFooter",
                routeTemplate: "api/documents/{documentid}/footers/{footerid}",
                defaults: new { documentid = RouteParameter.Optional, footerid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddTextToHeader",
                routeTemplate: "api/headers/{headerid}/texts/{textid}",
                defaults: new { headerid = RouteParameter.Optional, textid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddTextToParagraph",
                routeTemplate: "api/paragraphs/{paragraphid}/texts/{textid}",
                defaults: new { paragraphid = RouteParameter.Optional, textid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddTextToFooter",
                routeTemplate: "api/footers/{footerid}/texts/{textid}",
                defaults: new { footerid = RouteParameter.Optional, textid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddFormat",
                routeTemplate: "api/formats/{formatname}",
                defaults: new { formatname = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddStyleClass",
                routeTemplate: "api/styleclasses/{styleclassname}",
                defaults: new { styleclassname = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AddStyle",
                routeTemplate: "api/styles/{styleid}",
                defaults: new { styleid = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "AddComment",
               routeTemplate: "api/comments/{documentid}",
               defaults: new { documentid = RouteParameter.Optional }
           );
        }
    }
}

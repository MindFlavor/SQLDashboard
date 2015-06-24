using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SQLDashboard
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "database",
                routeTemplate: "api/{controller}/{guid_mssql}",
                defaults: new { guid_mssql = RouteParameter.Optional }
            );
        }
    }
}

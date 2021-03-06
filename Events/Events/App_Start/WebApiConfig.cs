﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Events.Abstract;
using Events.Concrete;
using Events.Infrastructure;

namespace Events
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IEventsRepository, EFEventsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhotosRepository, EFPhotosRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommentsRepository, EFCommentsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISubscribeRepository, EFSubscriptionRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGcmRegIdsRepository, EFGcmRegIdsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IEventSubscribersRepository, EFEventSubscribersRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserFileRepository, EFUserFileRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDataRepository, EFDataRepository>(new HierarchicalLifetimeManager());
            
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new UniversalSortableDateTimeConverter());

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

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Core.Domain.Shop;
using Microsoft.AspNetCore.Http;

namespace Service
{
    public static class ServiceContainer
    {
        public static void Build(ContainerBuilder containerBuilder)
        {
            var servicesAssembly = Assembly.GetExecutingAssembly();
            var coreAssembly = Assembly.GetAssembly(typeof(Shop));
            containerBuilder.RegisterAssemblyTypes(servicesAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            containerBuilder.RegisterAssemblyTypes(coreAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            containerBuilder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
        }
    }
}

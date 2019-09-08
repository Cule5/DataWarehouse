using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class InfrastructureContainer
    {
        public static void Build(ContainerBuilder containerBuilder)
        {

            var infrastructureAssembly = Assembly.GetExecutingAssembly();
            containerBuilder.RegisterAssemblyTypes(infrastructureAssembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            var optionsBuilder =
                new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(
                    @"Server=localhost;Database=DataWarehouse;Trusted_Connection=True;");


            containerBuilder.RegisterType<AppDbContext>()
                //.WithParameter("options", optionsBuilder.Options)
                .InstancePerLifetimeScope();
        }
    }
}

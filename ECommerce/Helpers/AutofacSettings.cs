using Autofac;
using Autofac.Integration.Mvc;
using ECommerce.AuthorizeAttributes;
using ECommerce.DAL.Repository;
using ECommerce.Service.Interfaces;
using ECommerce.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ECommerce.Helpers
{
    public class AutofacSettings
    {
        public static void Setup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder = ConfigureBuilder(builder);

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
        }

        private static ContainerBuilder ConfigureBuilder(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>().As<IProductRepository>().SingleInstance();
            builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            builder.RegisterFilterProvider();
            builder.RegisterType<AdminAuthorize>().PropertiesAutowired().SingleInstance();

            return builder;
        }
    }
}
using Autofac;
using BookStore.Contracts.Services.General;
using BookStore.Services.General;
using System;

namespace BookStore.Bootstrap
{
    public class AppContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            RegisterViewModels(builder);

            RegisterServices(builder);

            _container = builder.Build();
        }

        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            //builder.RegisterType<LoginViewModel>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            //services - data
            //builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();

            //services - general
            builder.RegisterType<NavigationService>().As<INavigationService>();

            //General
            //builder.RegisterType<GenericRepository>().As<IGenericRepository>();
        }
    }
}

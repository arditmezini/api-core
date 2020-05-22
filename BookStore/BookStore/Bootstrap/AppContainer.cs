using Autofac;
using BookStore.Contracts.Services.Data;
using BookStore.Contracts.Services.General;
using BookStore.Services.Data;
using BookStore.Services.General;
using BookStore.ViewModels;
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
            builder.RegisterType<RegistrationViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MenuViewModel>();
            builder.RegisterType<HomeViewModel>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            //services - data
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();

            //services - general
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<ConnectionService>().As<IConnectionService>();
            builder.RegisterType<DialogService>().As<IDialogService>();

            //General
            //builder.RegisterType<GenericRepository>().As<IGenericRepository>();
        }
    }
}
﻿using Autofac;
using BookStore.Contracts.Repository;
using BookStore.Contracts.Services.Data;
using BookStore.Contracts.Services.General;
using BookStore.Repository;
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
            builder.RegisterType<ProfileViewModel>();
            builder.RegisterType<AuthorViewModel>();
            builder.RegisterType<NewsViewModel>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            //services - data
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<StatisticsService>().As<IStatisticsService>();
            builder.RegisterType<AuthorService>().As<IAuthorService>();

            //services - general
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<ConnectionService>().As<IConnectionService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();

            //General
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();
        }
    }
}
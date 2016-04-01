using Autofac;
using Autofac.Integration.Mvc;
using BlueZero.Air.Data;
using BlueZero.Air.Data.Services;
using BlueZero.Air.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
using BlueZero.Air.Mailers;

namespace BlueZero.Air
{
    public class ResolverConfig
    {
        public static void RegisterDependencyResolver(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            ConfigureContainerBuilder(builder);

            IContainer container = builder.Build();

            // register MVC dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // register Web API dependency resolver
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            // MVC controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // modules
            builder.RegisterModule(new LogInjectionModule());

            // database
            builder.RegisterType<DataContext>().AsImplementedInterfaces();            

            // data services            
            builder.RegisterType<ParentService>().AsImplementedInterfaces();
            builder.RegisterType<ChildService>().AsImplementedInterfaces();
            builder.RegisterType<ActivityService>().AsImplementedInterfaces();
            builder.RegisterType<NoteService>().AsImplementedInterfaces();
            builder.RegisterType<BottleService>().AsImplementedInterfaces();
            builder.RegisterType<CarerService>().AsImplementedInterfaces();
            builder.RegisterType<DrinkService>().AsImplementedInterfaces();
            builder.RegisterType<FirstAidService>().AsImplementedInterfaces();
            builder.RegisterType<MealService>().AsImplementedInterfaces();
            builder.RegisterType<MedicineService>().AsImplementedInterfaces();
            builder.RegisterType<MilestoneService>().AsImplementedInterfaces();
            builder.RegisterType<NappyService>().AsImplementedInterfaces();
            builder.RegisterType<SickService>().AsImplementedInterfaces();
            builder.RegisterType<SleepService>().AsImplementedInterfaces();
            builder.RegisterType<SnackService>().AsImplementedInterfaces();
            builder.RegisterType<ChartService>().AsImplementedInterfaces();

            // support types
            builder.RegisterType<UserNotifier>().AsImplementedInterfaces();
            builder.RegisterType<UserMailer>().AsImplementedInterfaces();
            builder.RegisterType<RandomDataGenerator>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<GCMPushService>().AsImplementedInterfaces().SingleInstance();    
        }
    }
}
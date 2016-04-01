using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace BlueZero.Air
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // JSON only
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // drink api
            config.Routes.MapHttpRoute(
                name: "DrinkApi",
                routeTemplate: "api/child/{childId}/drink/{drinkId}",
                defaults: new { controller = "Drink", drinkId = RouteParameter.Optional }
            );

            // sick api
            config.Routes.MapHttpRoute(
                name: "SickApi",
                routeTemplate: "api/child/{childId}/sick/{sickId}",
                defaults: new { controller = "Sick", sickId = RouteParameter.Optional }
            );

            // snack api
            config.Routes.MapHttpRoute(
                name: "SnackApi",
                routeTemplate: "api/child/{childId}/snack/{snackId}",
                defaults: new { controller = "Sleep", snackId = RouteParameter.Optional }
            );

            // sleep api
            config.Routes.MapHttpRoute(
                name: "SleepApi",
                routeTemplate: "api/child/{childId}/sleep/{sleepId}",
                defaults: new { controller = "Sleep", sleepId = RouteParameter.Optional }
            );

            // note api
            config.Routes.MapHttpRoute(
                name: "NoteApi",
                routeTemplate: "api/child/{childId}/note/{noteId}",
                defaults: new { controller = "Note", noteId = RouteParameter.Optional }
            );

            // nappy api
            config.Routes.MapHttpRoute(
                name: "NappyApi",
                routeTemplate: "api/child/{childId}/nappy/{nappyId}",
                defaults: new { controller = "Nappy", nappyId = RouteParameter.Optional }
            );

            // milestone api
            config.Routes.MapHttpRoute(
                name: "MilestoneApi",
                routeTemplate: "api/child/{childId}/milestone/{milestoneId}",
                defaults: new { controller = "Milestone", milestoneId = RouteParameter.Optional }
            );

            // medicine api
            config.Routes.MapHttpRoute(
                name: "MedicineApi",
                routeTemplate: "api/child/{childId}/medicine/{medicineId}",
                defaults: new { controller = "Medicine", medicineId = RouteParameter.Optional }
            );

            // meal api
            config.Routes.MapHttpRoute(
                name: "MealApi",
                routeTemplate: "api/child/{childId}/meal/{mealId}",
                defaults: new { controller = "Meal", mealId = RouteParameter.Optional }
            );

            // activity api
            config.Routes.MapHttpRoute(
                name: "ActivityApi",
                routeTemplate: "api/child/{childId}/activity/{activityId}",
                defaults: new { controller = "activity", activityId = RouteParameter.Optional }
            );

            // bottle api
            config.Routes.MapHttpRoute(
                name: "BottleApi",
                routeTemplate: "api/child/{childId}/bottle/{bottleId}",
                defaults: new { controller = "Bottle", bottleId = RouteParameter.Optional }
            );

            // first aid api
            config.Routes.MapHttpRoute(
                name: "FirstAidApi",
                routeTemplate: "api/child/{childId}/firstaid/{firstaidId}",
                defaults: new { controller = "FirstAid", firstaidId = RouteParameter.Optional }
            );

            // child api
            config.Routes.MapHttpRoute(
                name: "ChildApi",
                routeTemplate: "api/child/{id}",
                defaults: new { controller = "Child", id = RouteParameter.Optional }
            );

            // carer api
            config.Routes.MapHttpRoute(
                name: "CarerApi",
                routeTemplate: "api/carer",
                defaults: new { controller = "Carer" }
            );

            // gcm registration api
            config.Routes.MapHttpRoute(
                name: "GCMRegistrationApi",
                routeTemplate: "api/gcm/{registrationId}",
                defaults: new { controller = "GCMRegistration", registrationId = RouteParameter.Optional }
            );

            // default api route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );            
        }
    }
}

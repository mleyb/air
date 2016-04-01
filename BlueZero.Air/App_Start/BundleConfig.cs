using System.Web;
using System.Web.Optimization;

namespace BlueZero.Air
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundles(bundles);

            RegisterStyleBundles(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-migrate").Include(
                        "~/Scripts/jquery-migrate-{version}.js"));                                    

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                        "~/Scripts/jquery.signalR*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/highcharts").Include(
                        "~/Scripts/Highcharts-2.2.1/js/highcharts.js"));            

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.js"));            

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));            

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-validation").Include(
                        "~/Scripts/bootstrap-validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Scripts/datatables.js",
                        "~/Scripts/datatables-bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
                        "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/site-template.js",
                        "~/Scripts/site.js"));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            var bootstrapCss = new StyleBundle("~/Content/bootstrap",
                                               "//netdna.bootstrapcdn.com/twitter-bootstrap/2.2.2/css/bootstrap-combined.min.css").Include(
                                   "~/Content/bootstrap.css");

            var siteCss = new StyleBundle("~/Content/css").Include(
                                          "~/Content/datepicker-bootstrap.css",
                                          "~/Content/datatables-bootstrap.css",
                                          "~/Content/toastr.css",
                                          "~/Content/font-awesome.css",
                                          "~/Content/site.css");

            bundles.Add(bootstrapCss);
            bundles.Add(siteCss);
        }
    }
}
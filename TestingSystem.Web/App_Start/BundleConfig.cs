using System.Web;
using System.Web.Optimization;

namespace TestingSystem.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScriptBundles(bundles);

            RegisterStylesBundles(bundles);

            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterStylesBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/bootstrap.css"));

            //bundles.Add(new StyleBundle("~/Content/kendo/styles").Include(
            //            "~/Content/kendo/kendo.common.min.css",
            //            "~/Content/kendo/kendo.common-bootstrap.min.css",
            //            "~/Content/kendo/kendo.silver.min.css"));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                    "~/Content/site.css"));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/kendo")
            //   .Include("~/Scripts/kendo/kendo.all.min.js",
            //   "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery")
            //    .Include("~/Scripts/kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/tweenlite").Include(
               "~/Scripts/TweenLite.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive").Include(
             "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
        }
    }
}

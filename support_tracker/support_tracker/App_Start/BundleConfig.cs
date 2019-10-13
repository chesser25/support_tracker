using System.Web.Optimization;

namespace support_tracker.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            var fontAwesomePath = "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css";
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/font-awesome", fontAwesomePath));
            bundles.Add(new StyleBundle("~/Content/messageList").Include("~/Content/MessageList.css"));
            bundles.Add(new StyleBundle("~/Content/pagedList").Include("~/Content/PagedList.css"));
            bundles.Add(new StyleBundle("~/Content/site").Include("~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
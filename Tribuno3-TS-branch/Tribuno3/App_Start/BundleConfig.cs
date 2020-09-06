using System.Web;
using System.Web.Optimization;

namespace Tribuno3
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                       "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-mask-min").Include(
                       "~/Scripts/jquery-mask-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jtable").Include(
                      "~/Scripts/jtable/jquery.jtable.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.Jtable").Include(
                      "~/Scripts/jtable/jquery.jtable.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                         "~/Scripts/ScriptTribuno/validacao_ptbr.js",                  
                         "~/Scripts/ScriptTribuno/Mascara.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/navbar").Include(
                     "~/Scripts/ScriptTribuno/Login/Principal/scripts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/CSS/Principal/styles.css"));
        }
    }
}

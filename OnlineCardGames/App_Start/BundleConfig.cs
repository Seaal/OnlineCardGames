﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace OnlineCardGames
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.signalR-2.2.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/sammy-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/Site.css"));

            ScriptBundle angularBundle = new ScriptBundle("~/bundles/app");
            angularBundle.IncludeDirectory("~/App", "*.js", true);
            angularBundle.Orderer = new AngularBundleOrderer();

            bundles.Add(angularBundle);
        }
    }

    public class AngularBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            List<BundleFile> newFiles = new List<BundleFile>(files.Count());

            newFiles.AddRange(files.Where(f => f.IncludedVirtualPath.EndsWith(".module.js")));
            newFiles.AddRange(files.Where(f => !f.IncludedVirtualPath.EndsWith(".module.js")));

            return newFiles;
        }
    }
}

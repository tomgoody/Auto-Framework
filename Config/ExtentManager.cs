

using AventStack.ExtentReports;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using System;

namespace vendorPOM.Config
{
    public  class ExtentManager
    {
        private static ExtentReports extent;
        private static ExtentTest test;
        private static ExtentSparkReporter htmlReporter;

        public static ExtentReports GetExtent()
        {
            if (extent == null)
            {
                var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                var actualPath = path.Substring(0, path.LastIndexOf("bin"));
                var projectPath = new Uri(actualPath).LocalPath;
                Directory.CreateDirectory(projectPath.ToString() + "Reports");
                var reportPath = projectPath + "Reports\\ExtentReport.html";

                extent = new ExtentReports();
                htmlReporter = new ExtentSparkReporter(reportPath);
                extent.AttachReporter(htmlReporter);

            }
            return extent;

        }

        public static ExtentReports getInstance()
        {
            return GetExtent();
        }

        public static ExtentTest CreateTest(String name, String category)
        {
            test = extent.CreateTest(name, category);
            return test;
        }
        public static ExtentTest CreateTest(String name)
        {
            test = extent.CreateTest(name);
            return test;
        }


    }
    
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hangfire;

namespace hangfire_console_issue64_repro.Controllers
{
    public class HomeController : Controller
    {
        private static bool jobStarted = false;
        public IActionResult Index()
        {
            if (!jobStarted) {
                BackgroundJob.Enqueue<MyJob>(myJob => myJob.Run(null, null));
                jobStarted = true;
            }
            return Redirect("/hangfire/jobs/processing");
        }

    }
}

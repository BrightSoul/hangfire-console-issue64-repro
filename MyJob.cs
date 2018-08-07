using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

namespace hangfire_console_issue64_repro
{
public class MyJob
    {
        public void Run(PerformContext context, IJobCancellationToken cancellationToken) {
            var startDate = DateTime.Now;
            var endDate = startDate.AddHours(2);
            while(DateTime.Now < endDate) {
                if (cancellationToken.ShutdownToken.IsCancellationRequested) {
                    break;
                }
                Thread.Sleep(3000);
                long secondsElapsed = Convert.ToInt64((DateTime.Now-startDate).TotalSeconds);
                context.WriteLine("I'm alive!");
                context.WriteLine($"{secondsElapsed} seconds elapsed");
            }
        }
    }
}
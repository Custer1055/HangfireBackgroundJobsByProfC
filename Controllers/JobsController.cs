using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HangfireBackgroundJobsByProf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class JobsController : ControllerBase
    {
        // Fire-and-Forget job
        [HttpPost("fire-and-forget")]
        public IActionResult CreateFireAndForgetJob()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget job by Prof has executed."));
            return Ok("Fire-and-forget job by Prof has been created.");
        }

        // Delayed Job
        [HttpPost("delayed-job")]
        public IActionResult CreateDelayedJob()
        {
            BackgroundJob.Schedule(() => Console.WriteLine("Delayed Job Executed."), TimeSpan.FromSeconds(30));
            return Ok("Delayed Job has been created. Will execute in 30 seconds.");
        }

        // Recurring Job
        [HttpPost("recurring-job")]
        public IActionResult CreateRecurringJob()
        {
            RecurringJob.AddOrUpdate("my-recurring-job", () => Console.WriteLine("Recurring Job Executed"), Cron.Minutely);
            return Ok("Recurring job has been cretaed. will execute every minute.");
        }

        // Continuation Job
        [HttpPost("continuation-job")]
        public IActionResult CreateContinuationJob()
        {
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Initial Job Executed."));
            BackgroundJob.ContinueWith(jobId, () => Console.WriteLine("Continuation job executed after initial job"));
            return Ok("Continuation job has been created");
        }
    }
}

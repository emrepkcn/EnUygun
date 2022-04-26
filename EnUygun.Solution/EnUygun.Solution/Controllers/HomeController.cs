using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnUygun.BusinessLayer;
using System.Threading.Tasks;
using EnUygun.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using EnUygun.Solution.Models;
namespace EnUygun.Solution.Controllers
{
    public class HomeController : Controller
    {
        private DevelopersManager devManager = new DevelopersManager();
        private JobManager jobManager = new JobManager();
        private ProviderBll providerbll = new ProviderBll();

        public ActionResult Index()
        {
            var isUpdatedDb = providerbll.AddOrUpdateProvidersData();
            var jobList = jobManager.List();
            var devList = devManager.List();
            var groupLevel = devList.GroupBy(x => x.DevLevel).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.DevLevel).ToList();
            int weeks = 1;
            int hourinaweek = 45;
            var taskLevel = 0;

            List<List<DevJobVidewData>> tasklist = new List<List<DevJobVidewData>>(groupLevel.Count);

            while (jobList.Count > 0)
            {
                foreach (var dev in groupLevel)
                {
                    int count = 0;
                    List<DevJobVidewData> tasks = new List<DevJobVidewData>();
                    foreach (var job in jobList.Where(x => x.TaskLevel == dev.DevLevel).OrderBy(x => x.TaskLevel).ToList())
                    {
                        taskLevel = job.TaskLevel;
                        int left = count + job.TaskTime;
                        if (left <= hourinaweek)
                        {
                            tasks.Add(new DevJobVidewData { TotalTime = count, Weeks = weeks, DevName = dev.DevName, TaskLevel = job.TaskLevel, TaskName = job.TaskName, TaskTime = job.TaskTime });
                            count += job.TaskTime;
                        }
                    }
                    var remainder = hourinaweek - count;
                    var timecallist = jobList
                   .Select(x => new { job = x, timecall = TimeCal(x.TaskLevel, x.TaskTime, dev.DevLevel) }).ToList();
                    
                    var findtime = timecallist.FirstOrDefault(x => x.timecall == remainder);
                    if (findtime != null)
                        tasks.Add(new DevJobVidewData { TotalTime = count + (int)findtime.timecall, Weeks = weeks, DevName = dev.DevName, TaskLevel = findtime.job.TaskLevel, TaskName = findtime.job.TaskName, TaskTime = findtime.job.TaskTime });
                    else
                    {                       
                        decimal res = remainder;
                        timecallist.Where(x => x.timecall < remainder).ToList().ForEach(x =>
                        {
                            res -= x.timecall;
                            if (res > 0)
                                tasks.Add(new DevJobVidewData { TotalTime = count + (int)x.timecall, Weeks = weeks, DevName = dev.DevName, TaskLevel = x.job.TaskLevel, TaskName = x.job.TaskName, TaskTime = x.job.TaskTime });
                        });
                    }

                    tasklist.Add(tasks);
                    var removelist = jobList.Where(a => tasks.Select(x => x.TaskName).Contains(a.TaskName)).ToList();
                    removelist.ForEach(x => jobList.Remove(x));
                }
                weeks += 1;
            }
            var result = tasklist.Where(x => x.Count > 0).SelectMany(x => x).ToList();
            return View(result);
        }

        private decimal TimeCal(int jobDifficulty, int time, int devLevel)
        {
            return ((decimal)jobDifficulty / devLevel * time);
        }
    }
}
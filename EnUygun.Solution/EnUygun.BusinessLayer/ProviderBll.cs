using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnUygun.Services;
using EnUygun.Services.Provider;
using EnUygun.Entities;

namespace EnUygun.BusinessLayer
{
    public class ProviderBll
    {

        public bool AddOrUpdateProvidersData()
        {
            return Task.Run<bool>(async () =>
            {
                var ProviderManager = new ProviderManager<JobsModel>(new OneProvider(), new TwoProvider());
                List<JobsModel> ProviderJobs = await ProviderManager.GetDataAsync();

                var jobmanager = new JobManager();

                var idlist = ProviderJobs.Select(a => a.id).ToList();
                var sameList = jobmanager.ListQueryable().Where(x => idlist.Contains(x.TaskName)).ToList();
                var Updatelist = ProviderJobs.Where(x => sameList.Any(a => a.TaskName == x.id & (a.TaskLevel != x.zorluk | a.TaskTime != x.sure)))
                .Select(x =>
                {
                    var find = sameList.FirstOrDefault(a => a.TaskName == x.id);
                    var hasdif = find?.TaskLevel != x.zorluk | find?.TaskTime != x.sure;
                    if (hasdif)
                    {
                        find.TaskLevel = x.zorluk;
                        find.TaskTime = x.sure;
                    }
                    return hasdif ? find : default;
                }
                ).Where(a => a != null).ToList();
                var insertList = ProviderJobs.Where(x => !sameList.Select(a => a.TaskName).Contains(x.id))
                .Select(x => new Job { TaskName = x.id, TaskLevel = x.zorluk, TaskTime = x.zorluk, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now }).ToList();
                if (Updatelist.Count() > 0 && jobmanager.Update(Updatelist) != Updatelist.Count())
                {
                    //update olmadı
                    return false;
                }
                else if (Updatelist.Count() == 0)//update datası yok

                if (insertList.Count() > 0 && jobmanager.Insert(insertList) != insertList.Count())
                {
                    //insert olmadı
                    Console.WriteLine("insert Has Failed");

                    return false;
                }
                else if (Updatelist.Count() == 0)//insert datası yok
                    Console.WriteLine("There is not any insert data exist");
                Console.WriteLine("addorupdate completed");

                return true;
            }).Result;
        }
    }
}

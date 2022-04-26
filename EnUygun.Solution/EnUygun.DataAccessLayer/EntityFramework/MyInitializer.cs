using System;
using System.Collections.Generic;
using System.Linq;
using EnUygun.Entities;
using System.Data.Entity;

namespace EnUygun.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            for (int i = 1; i <= 5; i++)
            {
                Developers dev = new Developers()
                {
                    DevName = "DEV" + i,
                    DevLevel =  i,
                    DevTime = 1,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                };
                context.Developers.Add(dev);
            }
            context.SaveChanges();
        }
    }
}

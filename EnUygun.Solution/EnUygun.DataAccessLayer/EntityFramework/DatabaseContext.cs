using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using EnUygun.Entities;

namespace EnUygun.DataAccessLayer.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Developers> Developers { get; set; }
        public DbSet<Job> Jobs { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}

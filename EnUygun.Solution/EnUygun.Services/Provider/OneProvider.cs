using EnUygun.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnUygun.Services.Provider
{
    public class OneProvider : BaseProvider<JobsModel>
    {
        public OneProvider() : base(new Uri("http://www.mocky.io/v2/5d47f24c330000623fa3ebfa")) { }

        public async override Task<List<JobsModel>> GetDataAsync()
        {
            return await base.GetDataAsync();
        }
    }
}

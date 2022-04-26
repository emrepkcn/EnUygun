using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnUygun.Services.Provider.Interfaces
{
    public interface IProviderGetData<T> where T : class
    {
        Task<List<T>> GetDataAsync();
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnUygun.Services.Provider.Interfaces;
using EnUygun.Services.Provider;
using EnUygun.Entities;
namespace EnUygun.Services
{
    public class ProviderManager<T> : IProviderGetData<T> where T : class
    {

        IProviderGetData<T>[] Providers { get; }

        public ProviderManager(params IProviderGetData<T>[] providers)
        {
            this.Providers = providers;
        }

        public async Task<List<T>> GetDataAsync()
        {
            var result = Providers?.Where(x => x != null).ToList().Select(a => a.GetDataAsync()).ToArray();
            var r1 = await Task.WhenAll(result);
            return r1.SelectMany(x => x).ToList();
        }
    }
}

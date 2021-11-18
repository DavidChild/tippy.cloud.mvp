using Bitter.Base;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace tippy.cloud.Client
{
    public interface IMyHttp
    {
        Task<bool> Get<Tout>(string uri, dynamic dyParames, Action<Result<Tout>> callback) where Tout : class, new();
        Task<bool>  Post<T, Tout>(string uri, T value, Action<Result<Tout>> callback)
            where T : class, new()
            where Tout :class, new();
       
    }
}
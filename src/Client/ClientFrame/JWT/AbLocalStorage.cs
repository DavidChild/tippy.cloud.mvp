using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace tippy.cloud.Client
{
    public abstract class ILocalStorage
    {
       
       public abstract Task<T> GetLocalStorage<T>(string key);
        public abstract  Task SetLocalStorage<T>(string key, T value) where T : class;
    }
}
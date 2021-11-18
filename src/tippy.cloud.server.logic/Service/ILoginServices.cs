using Bitter.Base;
using System.Threading.Tasks;
using tippy.cloud.Shared;

namespace tippy.cloud.server.logic
{
    public interface ILoginServices
    {
        Task<Result> LoginChecked(LoginDto indto);
        Task<Result> SetTokinIntoCache(LoginDto_Out outdto);
    }
}
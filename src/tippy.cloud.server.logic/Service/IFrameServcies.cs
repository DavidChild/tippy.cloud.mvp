using Bitter.Base;
using System.Threading.Tasks;

namespace Bzero.Frame.Business
{
    public interface IFrameServcies
    {
        Task<Result> GetMenus();
        Task<Result> GetTenantUserInfo();
    }
}
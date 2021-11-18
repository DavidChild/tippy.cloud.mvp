using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using tippy.cloud.entity;

namespace tippy.cloud.server.logic.Base
{
    public interface IBaseServices
    {
        string this[string key] { get; }

        IHttpContextAccessor httpContextAccessor { get; }
        IUserRight user { get; set; }

        int GetUserId();
        Task<IUserRight> GetUserInstace();
    }
}
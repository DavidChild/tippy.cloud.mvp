using Bitter.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tippy.cloud.Shared;

namespace tippy.cloud.Client
{
    public interface IAuthService
    {
        Task<Result<LoginDto_Out>> Login(LoginDto request);
        Task Logout();
   
    }
}

using Bitter.Base;
using Bzero.Frame.Business;
using Bzero.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using Bitter.Tools.Utils;
using Microsoft.AspNetCore.Http;
using tippy.cloud.Server;

namespace Bzero.Server.Controllers
{

    [Route("tippy/[controller]/[action]")]
    [TippyAuth]
    public class FrameController : TippyBaseController
    {

        private IFrameServcies _services { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FrameController(IFrameServcies services, IHttpContextAccessor httpContextAccessor)
        {
            _services = services;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public Task<Result> GetMenus()
        {
            return _services.GetMenus();
        }

        [HttpGet]
        public Task<Result> GetTenantUserInfo()
        {
            return _services.GetTenantUserInfo();
        }


    }
}

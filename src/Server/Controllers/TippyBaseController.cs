using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Bitter.Core;
using Bitter.ServiceBase;
using Bitter.Tools.Utils;
using Bzero.Services.Base;
using tippy.cloud.entity;
using tippy.cloud.server.logic.Base;

namespace Bzero.Server.Controllers
{
    [Route("bzero/[controller]/[action]")]
    public class TippyBaseController : ApiController
    {
        protected IUserRight user { get; set; }
        private BaseServices baseServices { get; set; }
        public   TippyBaseController(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor) {

            baseServices = new BaseServices(httpContextAccessor);
            user = baseServices.GetUserInstace().Result;
          }
        public TippyBaseController()
        {
          
        }
    }
}

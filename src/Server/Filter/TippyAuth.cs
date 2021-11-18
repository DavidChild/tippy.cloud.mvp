using Bitter.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitter.Base;
using Bzero.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using tippy.cloud.server.logic.Base;

namespace tippy.cloud.Server
{
    public class TippyAuth : MyToken
    {
        private IHttpContextAccessor httpContextAccessor { get; set; }
        private IBaseServices baseServices { get; set; }

        /// <summary>
        /// token 登入鉴权
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        public override bool CheckAuthorization(string accessToken, ActionExecutingContext actionContext)
        {
            BaseServices s = new BaseServices(actionContext.HttpContext.Request);
            var id = s.GetUserId();
            if (id > 0) return true;
            return false;
        }


     
    }
}

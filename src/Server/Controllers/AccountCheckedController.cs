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
using tippy.cloud.server.logic;
using tippy.cloud.Shared;
using tippy.cloud.entity;
using tippy.cloud.Server;
using tippy.cloud.server.logic.CacheKey;
using tippy.cloud.Shared.Domain;
using tippy.cloud.server.logic.Frame;

namespace Bzero.Server.Controllers
{

    [Route("tippy/[controller]/[action]")]
    public class AccountCheckedController : TippyBaseController
    {

        private  ILoginServices _loginServices { get; set; }
        public AccountCheckedController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        [HttpPost]
        public async  Task<Result> Login([FromBody] LoginDto rqtDto)
        {

            Result result = new Result();
            result = await _loginServices.LoginChecked(rqtDto);
            if (result.code == 0) return result;
            IUserRight myuser = (IUserRight)result.@object;
            JwtOption jwtoption = JsonConvert.DeserializeObject<JwtOption>(Bitter.Base.NetCore.ConfigManage.JsonConfigMange.GetInstance().AppSettings[Appsettingkey.jwt].ToString());
            var claims = new Claim[]
            {
                 new(ClaimTypes.NameIdentifier,myuser.userid.ToSafeString()),
                 new(ClaimTypes.Name, myuser.usermobile.ToSafeString()), 
            };
            var jwtSetting = jwtoption;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddHours(jwtSetting.ExpiryInHours);
            var token = new JwtSecurityToken(jwtSetting.Issuer, jwtSetting.Audience, claims, expires: expiry, signingCredentials: creds);
            var tokenText = new JwtSecurityTokenHandler().WriteToken(token);
           
            LoginDto_Out loginout = new LoginDto_Out()
            {
                user = (TenatUser)myuser,
                authtoken = tokenText,

            };
            var resultset= await  _loginServices.SetTokinIntoCache(loginout);
            if (resultset.code == 0) return resultset;
            return result.SetSucessResult(loginout);


        }
        [HttpPost]
        public Task<Result> JoinNow([FromBody] RegisterModel rqtDto)
        {
            Result re = new Result();
            try
            {
              re = Register.AddUser(rqtDto);
           
            }
            catch (Exception ex)
            {
                re.SetZeroResult(ex);
            }
            return Task.FromResult<Result>(re);
        }
    }
}

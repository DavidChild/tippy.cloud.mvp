
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/2 17:29:31  
** desc： 
** Ver.:  V1.0.0
** Copyright (C) 2021 @ Right by Nervos。
** Update-Reason: Update-Time: Update-Author: 
*********************************************************************************/
using Bitter.Base;
using Bitter.Core;
using Bitter.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitter.Tools.Utils;
using Microsoft.AspNetCore.Http;
using CacheManager.Core;
using tippy.cloud.server.logic.Base;
using tippy.cloud.entity;
using tippy.cloud.Shared;
using tippy.cloud.server.logic;
namespace Bzero.Frame.Business
{
    public class LoginServices : BaseServices, ILoginServices
    {
        public LoginServices(IHttpContextAccessor accessor) : base(accessor)
        {

        }

        public async Task<Result> LoginChecked(LoginDto indto)
        {
            Result re = new Result() { };
            try
            {
                var puser = db.FindQuery<SysBaseUserInfo>().Where(p => p.FUseraccount == indto.Account).Find().FirstOrDefault();
                if (puser.FId <= 0)
                {
                    re.SetZeroResult("账号密码错误.");

                }
                else if (puser.FPwd != indto.Password)
                {
                    re.SetZeroResult("账号密码错误.");
                }
                else
                {

                    tippy.cloud.entity.IUserRight ur = await GetCacheEntity.GetUserInfo(puser.FId);
                    re.SetSucessResult(ur);
                }

            }
            catch (Exception ex)
            {

                re.SetZeroResult(ex);
            }
            return re;
        }
        /// <summary>
        /// 设置token
        /// </summary>
        /// <param name="outdto"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
       public async Task<Result> SetTokinIntoCache(LoginDto_Out outdto)
        {
            Result re = new Result() { };
            try
            {

                SysBaseTokenInfo tokenInfo = new SysBaseTokenInfo();
                tokenInfo.FCreateTime = DateTime.Now;
                tokenInfo.FCreateUserId = outdto.user.userid;
                tokenInfo.FTargetUserId = outdto.user.userid;
                tokenInfo.FClientId = this[RequestHeadDefine.head_clientid].ToSafeString("");
                tokenInfo.FClientType = this[RequestHeadDefine.head_clientype].ToSafeString("");
                tokenInfo.FToken = outdto.authtoken;
                tokenInfo.FExpireTime = DateTime.Now.AddHours(this.GetAddCacheSlidHours());
                TockenCacheDto tocken = new TockenCacheDto();
                TimeSpan timespan = new TimeSpan(this.GetAddCacheSlidHours(), 0, 0); //默认不设置，取168小时
                tocken.expiretime = tokenInfo.FExpireTime;
                tocken.userid = outdto.user.userid;
                if (tokenInfo.Insert().Submit() > 0)
                {
                    CacheInstace<TockenCacheDto>.GetMemeryInstace().Add(new CacheManager.Core.CacheItem<TockenCacheDto>(this.GetTockenCacheKey(outdto.authtoken), tocken, ExpirationMode.Sliding, timespan));
                }

                return re.SetSucessResult("");
            }
            catch (Exception)

            {

                re.SetZeroResult("登入失败");
            }
            return re;
        }




    }
}

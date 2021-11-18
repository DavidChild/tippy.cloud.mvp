
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/2 17:45:03  
** desc： 
** Ver.:  V1.0.0
** Copyright (C) 2021 筑龙 版权所有。
** Update-Reason: Update-Time: Update-Author: 
*********************************************************************************/
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitter.Base;
using Bitter.Tools;
using Bitter.Tools.Utils;
using Bitter.Core;
using tippy.cloud.entity;
using tippy.cloud.server.logic.CacheKey;
using tippy.cloud.Shared;

namespace tippy.cloud.server.logic.Base
{
    public class BaseServices : IBaseServices
    {



        private IHttpContextAccessor _httpContextAccessor { get; set; }

        private HttpRequest _request { get; set; }
        public IHttpContextAccessor httpContextAccessor { get { return _httpContextAccessor; } }

        public BaseServices(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;
        }

        public BaseServices(HttpRequest request)
        {
            _request = request;
        }




        protected string GetTockenCacheKey(string authtoken)
        {

            return $"{Cachekey.cache_user_token}_{authtoken}_{this[RequestHeadDefine.head_clientid]}";
        }
        /// <summary>
        /// 获取滑动时间增量值
        /// </summary>
        /// <returns></returns>
        protected int GetAddCacheSlidHours()
        {
            return Bitter.Base.NetCore.ConfigManage.JsonConfigMange.GetInstance().AppSettings[Appsettingkey.user_token_cache_expire].ToSafeInt32(168);
        }

        /// <summary>
        /// 检验Token是否合法
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int GetUserId()
        {

            var token = this[RequestHeadDefine.head_accesstocken].ToSafeString();
            if (string.IsNullOrEmpty(token))
            {
                return 0;
            }
            var cachetoken = GetTockenCacheKey(token);
            if (CacheInstace<TockenCacheDto>.GetRedisInstanceDefaultMemery().Exists(cachetoken))
            {
                var userid = 0;
                var tockeninfo = CacheInstace<TockenCacheDto>.GetRedisInstanceDefaultMemery().Get<TockenCacheDto>(cachetoken);
                userid = tockeninfo.userid;
                if (tockeninfo.expiretime < DateTime.Now.AddHours(12))
                {
                    var time = DateTime.Now.AddHours(this.GetAddCacheSlidHours());
                    CacheInstace<TockenCacheDto>.GetRedisInstanceDefaultMemery().Update(cachetoken, (mytockeninfo) =>
                    {
                        var p = db.FindQuery<SysBaseTokenInfo>().Where(p => p.FToken == token).Find().FirstOrDefault();
                        p.FExpireTime = time;
                        p.Update().Submit();
                        userid = p.FTargetUserId.ToSafeInt32(0);
                        mytockeninfo.expiretime = time;
                        return mytockeninfo;
                    });
                    CacheInstace<TockenCacheDto>.GetRedisInstanceDefaultMemery().Expire(cachetoken, new TimeSpan(this.GetAddCacheSlidHours(), 0, 0));
                }
                return userid;


            }
            else
            {
                return 0;
            }
        }

        protected Lazy<IUserRight> user
        {
            get
            {
                return new Lazy<IUserRight>(() =>
                {
                    int userid = GetUserId();
                    var uinfo = db.FindQuery<SysBaseUserInfo>().QueryById(userid);
                    return new TenatUser()
                    {
                        userid = uinfo.FId,
                        tenantId = uinfo.FId,
                        usermobile = uinfo.FMobile,
                        userpico = uinfo.FUserIcon,
                        username = uinfo.FUsername
                    };
                });
            }
        }
        /// <summary>
        /// 当前缓存Key
        /// </summary>
        protected string TockenCacheKey
        {
            get
            {
                return GetTockenCacheKey(this[RequestHeadDefine.head_accesstocken].ToSafeString(""));
            }
        }

        IUserRight IBaseServices.user { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<IUserRight> GetUserInstace()
        {
            var userid = GetUserId();
            IUserRight ur = await GetCacheEntity.GetUserInfo(userid);
            return ur;
        }

        public string this[string key]
        {
            get
            {
                
               
                try
                {
                    if (httpContextAccessor != null)
                    {
                        var headers = httpContextAccessor.HttpContext.Request.Headers;
                        return headers[key].ToSafeString("");

                    }
                    else
                    {
                        return Bitter.ServiceBase.Header.GetHeaderValue(_request, key);
                      
                    }
                    
                       
                }
                catch (Exception ex)
                {
                    LogService.Default.Fatal("");
                }
                return "";
            }
        }
    }
}

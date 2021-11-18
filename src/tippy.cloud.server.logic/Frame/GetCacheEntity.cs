
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/6 15:21:11  
** desc： 
** Ver.:  V1.0.0
** Copyright (C) 2021 筑龙 版权所有。
** Update-Reason: Update-Time: Update-Author: 
*********************************************************************************/
using Bitter.Core;
using Bitter.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitter.Tools.Utils;
using Bitter.Base;
using CacheManager.Core;
using tippy.cloud.entity;
using tippy.cloud.server.logic.CacheKey;


namespace tippy.cloud.server.logic.Base
{
   public class GetCacheEntity
    {
        //根据Tocken从数据库获取用户信息
        public static Task<IUserRight> GetUserInfo(int  userid)
        {
            var usercachekey = $"{ Cachekey.cache_user_key}_{userid.ToSafeInt32(0)}";
            var ur = new TenatUser();
            if (Bitter.Base.CacheInstace<IUserRight>.GetRedisInstanceDefaultMemery().Exists(usercachekey))
            {
                ur = CacheInstace<TenatUser>.GetRedisInstanceDefaultMemery().Get(usercachekey);
            }
            else
            {
                var  mis=   Bitter.Base.NetCore.ConfigManage.JsonConfigMange.GetInstance().AppSettings[Appsettingkey.user_cache_expire].ToSafeInt32(60);
                var userinfo = db.FindQuery<SysBaseUserInfo>().QueryById(userid);
                if (userinfo.FId > 0)
                {
                    ur.userid = userinfo.FId;
                    ur.username = userinfo.FUsername;
                    ur.deptid = userinfo.FDeptId.ToSafeInt32(0);
                    ur.postionid = userinfo.ToSafeInt32(0);
                }
                CacheItem<TenatUser> item = new CacheItem<TenatUser>(usercachekey, ur, ExpirationMode.Absolute, new TimeSpan(0, 0, mis));
                CacheInstace<TenatUser>.GetRedisInstanceDefaultMemery().Add(item);
             }
            return Task<IUserRight>.FromResult((IUserRight)ur);

        }

    }
}


/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/6 15:55:59  
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

namespace tippy.cloud.server.logic.CacheKey
{
    public static class Appsettingkey
    {

        #region bitter.json 中的配置
        //用户信息缓存失效时间
        public const string user_cache_expire = "user_cache_expire";
        //token滑动SPANTIME
        public const string user_token_cache_expire = "token_expire";
        #endregion

        #region bitter.json 中的配置
        //jwt 配置信息
        public const string jwt = "jwt";
        #endregion
    }
}

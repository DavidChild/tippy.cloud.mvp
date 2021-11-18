
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/11 11:31:38  
** desc： 
** Ver.:  V1.0.0
** Copyright (C) 2021 @ Right by Nervos。
** Update-Reason: Update-Time: Update-Author: 
*********************************************************************************/
using Bitter.Tools;
using Bitter.Tools.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using tippy.cloud.Shared;

namespace tippy.cloud.Client
{
   public class SetRequestHeader
    {
        public static async Task<bool> SetHttpHead(HttpClient httpClient, IConfiguration _config, ILocalStorage _localStorage)
        {
            //请求前先进行处理
            httpClient.BaseAddress = new Uri(GloableSettig.GetGloableSettigInsance(_config).hosturi);
            httpClient.DefaultRequestHeaders.Add(RequestHeadDefine.head_clientid, GloableSettig.GetGloableSettigInsance(_config).applicationName);
            httpClient.DefaultRequestHeaders.Add(RequestHeadDefine.head_clientype, GloableSettig.GetGloableSettigInsance(_config).clientType.ToSafeString());
            var token = await _localStorage.GetLocalStorage<string>(GloableSettig.GetGloableSettigInsance(_config).authToken);
            httpClient.DefaultRequestHeaders.Add(RequestHeadDefine.head_accesstocken, token);
            return true;

        }
    }
}

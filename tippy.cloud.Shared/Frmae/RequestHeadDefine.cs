
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/7 9:09:30  
** desc： 
** Ver.:  V1.0.0
** Copyright (C) 2021 @ Right by Nervos。
** Update-Reason: Update-Time: Update-Author: 
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tippy.cloud.Shared
{
    public static class RequestHeadDefine
    {
        #region
        //会话令牌
        public const string head_accesstocken = "AccessToken";
        //请求令牌
        public const string head_requesttockn = "RequestTocken";
        //客户端名称 应用名称
        public const string head_clientid = "ClientId";
        //客户端类型 APP,PC
        public const string head_clientype = "ClientType";

        #endregion
    }
}

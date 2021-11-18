
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/7 8:27:59  
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

namespace Bzero.Services.Base
{
    public class SSO
    {
       /// <summary>
       /// 记录当前的人员的登入端的记录情况,实现多客户控制，单点登入,
       /// </summary>
       public Dictionary<string, string> clienttypes { get; set; }

       public string thecurrenttoken { get; set; }




    }
}

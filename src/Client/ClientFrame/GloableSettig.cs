using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitter.Tools.Utils;
using tippy.cloud.Shared;

namespace tippy.cloud.Client
{
    public class GloableSettig
    {
        IConfiguration _cf { get; set; }
        public  string hosturi {get;set;}

        public static GloableSettig gloableSettig { get; set; }
       
        public GloableSettig(IConfiguration _config)
        {
            _cf = _config;
           
        }
        //设置当前应用的应用名称
        public  string applicationName = "bzero.test";

        //localstorage 缓存键
        public string authToken = "authtoken";
        //设置当前应用的客户端是app还是pc
        public  int clientType = (int)RequestClientTyps.pc;

        public static GloableSettig GetGloableSettigInsance(IConfiguration _config)
        {

            if (gloableSettig != null)
            {
                gloableSettig.hosturi = gloableSettig._cf.GetValue<string>("host").ToSafeString();
            }
            else
            {
                gloableSettig = new GloableSettig(_config);
                gloableSettig.hosturi = gloableSettig._cf.GetValue<string>("host").ToSafeString();
            }
            return gloableSettig;
        }

    }
   
}
 
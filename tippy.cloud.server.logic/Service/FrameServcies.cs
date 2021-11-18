
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/16 9:21:54  
** desc： 
** Ver.:  V1.0.0
** Copyright (C) 2021 筑龙 版权所有。
** Update-Reason: Update-Time: Update-Author: 
*********************************************************************************/
using Bitter.Base;
using Bitter.Core;
using Bitter.Tools;
using Bzero.Services.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using tippy.cloud.entity;
using tippy.cloud.server.logic.Base;
using Bitter.Tools.Utils;
using tippy.cloud.Shared.Domain;
using tippy.cloud.server.logic.CacheKey;
using tippy.cloud.Shared;

namespace Bzero.Frame.Business
{
    public class FrameServcies : BaseServices, IFrameServcies
    {

        public FrameServcies(IHttpContextAccessor accessor) : base(accessor)
        {

        }


        public Task<Result> GetMenus()
        {
            Result re = new Result();
            try
            {
                List<MenuItem> mylt = new List<MenuItem>();
                CacheInstace<List<MenuItem>>.GetRedisInstanceDefaultMemery().TryGetOrAdd(Cachekey.cache_bzero_menus,
                    (key) => {
                        var mlt = GetRootMenus();
                        foreach (var item in mlt)
                        {
                            GetChildrenMenus(item);
                        }
                        return mlt;

                    }, out mylt);


                re.SetSucessResult(mylt);



            }
            catch (Exception ex)
            {
                re.SetZeroResult(ex);
            }
            return Task.FromResult<Result>(re);
        }


        public List<MenuItem> GetRootMenus()
        {
            var lt = db.FindQuery<SysBaseMenu>().Where(p => p.FIsDeleted.ToSafeInt32(0) == 0 && p.FParentId.ToSafeInt32(0) == 0).Find()?.ToList().Select<SysBaseMenu, MenuItem>((f) => new MenuItem { Icon = f.FIcon, Key = f.FKey, Path = f.FPath, Name = f.FName, id = f.FId });
            return lt.ToList();
        }

        public void GetChildrenMenus(MenuItem parentMenu)
        {

            var lt = db.FindQuery<SysBaseMenu>().Where(p => p.FIsDeleted.ToSafeInt32(0) == 0 && p.FParentId.ToSafeInt32(0) == parentMenu.id).Find()?.ToList().Select<SysBaseMenu, MenuItem>((f) => new MenuItem { Icon = f.FIcon, Key = f.FKey, Path = f.FPath, Name = f.FName, id = f.FId });
            if (lt.Count() <= 0) return;
            foreach (var item in lt)
            {
                if (parentMenu.Children == null)
                {
                    parentMenu.Children = new List<MenuItem>();
                }
                parentMenu.Children.Add(item);
                GetChildrenMenus(item);
            }
        }

        public Task<Result> GetTenantUserInfo()
        {
            Result re = new Result();
            try
            {
                CurrentUser user = new CurrentUser()
                {
                    Address = "",
                    Avatar = this.user.Value.userpico,
                    Phone = this.user.Value.usermobile,
                    NotifyCount = 0,
                    Country = "",
                    Name = this.user.Value.username
                };
                re.SetSucessResult(user);
            }
            catch (Exception ex)
            {
                re.SetZeroResult(ex);
            }
            return Task.FromResult<Result>(re);


        }
    }
}

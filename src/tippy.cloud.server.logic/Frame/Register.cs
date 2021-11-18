using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tippy.cloud.Shared.Domain;
using tippy.cloud.entity;
using Bitter.Core;
using Bitter.Base;

namespace tippy.cloud.server.logic.Frame
{
    public  class Register
    {
        public static  Result  AddUser(RegisterModel model)
        {
            Result re = new Result();
            if (model != null)
            {
                if (db.FindQuery<SysBaseUserInfo>().Where(p => p.FUseraccount == model.UserName).FindCount() > 0)
                {
                    return re.SetZeroResult($"the account:{model.UserName} is exis,please change your account.");
                }
                if (db.FindQuery<SysBaseUserInfo>().Where(p => p.FMobile == model.Phone).FindCount() > 0)
                {
                    return re.SetZeroResult($"the phone number:{model.Phone} is exis,please change your account.");
                }
                SysBaseUserInfo user = new SysBaseUserInfo();
                user.FCreatimeTime = DateTime.Now;
                user.FMobile = model.Phone;
                user.FPwd = model.Password;
                user.FUsername = model.UserName;
                user.FUsermobile = model.Phone;
                user.FUserIcon = model.Icon;
                var id = user.Insert().Submit();
                re.SetSucessResult(id);
                
            }
            return re.SetZeroResult("join fail");
        }
    }
}

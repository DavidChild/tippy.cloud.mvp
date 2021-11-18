
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/2 17:12:05  
** desc： 
** Ver.:  V1.0.0
** Copyright (C) 2021 DavidChild 版权所有。
** Update-Reason: Update-Time: Update-Author: 
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tippy.cloud.entity
{
    public class TenatUser : IUserRight
    {
        //当前人员ID
        public int userid { get; set; }
        //当前人员姓名
        public string username { get; set; }

        //当前人员部门
        public int deptid { get; set; }

        //当前人员岗位
        public int postionid { get; set; }


        //当前登入人头像
        public string userpico { get; set; }
        public string usermobile { get; set; }

        public int tenantId { get; set; }

     
    }
}

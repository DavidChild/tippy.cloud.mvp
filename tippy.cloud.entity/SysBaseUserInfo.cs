
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/2 16:52:28  
** desc： 
** Ver.:  V1.0.0
** Copyright (C) 2021 DavidChild 版权所有。
** Update-Reason: Update-Time: Update-Author: 
*********************************************************************************/
using Bitter.Core;
using Bitter.Tools;
using Bitter.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tippy.cloud.entity
{
    [TableName("sys_base_user")]
    public class SysBaseUserInfo : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Identity]
        [Display(Name = @"")]
        public virtual Int32 FId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String FUsername { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String FUseraccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String FUsermobile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String FPwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? FSex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? FDeptId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? FPositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual DateTime? FCreatimeTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? FCreateUserId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual string  FMobile { get; set; }
        // <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual string FUserIcon { get; set; }

    }

}

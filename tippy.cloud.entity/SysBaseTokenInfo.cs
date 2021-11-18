
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/8/2 16:55:50  
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
    [TableName("sys_base_token")]
    public class SysBaseTokenInfo : BaseModel
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
        public virtual String FToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? FTargetUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual DateTime? FCreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual Int32? FCreateUserId { get; set; }


        /// <summary>
        ///  客户端编号
        /// </summary>
        [Display(Name = @"")]
        public virtual string FClientId { get; set; }

        /// <summary>
        ///  客户端类型 pc,app
        /// </summary>
        [Display(Name = @"")]
        public virtual string FClientType { get; set; }

        /// <summary>
        ///  过期时间
        /// </summary>
        [Display(Name = @"")]
        public virtual DateTime FExpireTime { get; set; }

    }

}

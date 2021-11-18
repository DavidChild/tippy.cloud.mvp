
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
    [TableName("sys_base_menu")]
    public class SysBaseMenu : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Identity]
        [Display(Name = @"")]
        public virtual Int32 FId { get; set; }

        public virtual Int32? FParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String FName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual string FIcon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String FPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual String FKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual DateTime? FCreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual int? FCreateUserId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Display(Name = @"")]
        public virtual int? FIsDeleted { get; set; }
        


    }

}

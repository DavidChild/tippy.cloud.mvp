
/********************************************************************************
** auth： DavidChild/JASON
** date： 2021/7/25 18:37:08  
** desc： 
** Ver.:  V1.0.0
** Copyright (C) 2021 @ Right by Nervos。
** Update-Reason: Update-Time: Update-Author: 
*********************************************************************************/
using Bitter.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tippy.cloud.entity;

namespace tippy.cloud.Shared
{
  public class LoginDto
    {
        [Display(Name = "账号")]
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Account { get; set; }

        [Display(Name = "密码")]
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; }

        public string LoginType { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Captcha { get; set; }
    }

    public class LoginDto_Out
    {
        public TenatUser user { get; set; }

      
        public string authtoken { get; set; }
    }
}

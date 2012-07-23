using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoveKaoExam.Models
{
    public class 爱考网绑定账号
    {
        /// <summary>
        /// 账号 包含用户名和邮箱地址
        /// </summary>
        public string 账号 { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string 密码 { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string 确认密码 { get; set; }
        
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string 邮箱 { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string 验证码 { get; set; }

        /// <summary>
        /// 用户操作类型 0表示绑定未绑定的账号，1表示修改绑定过的账号
        /// </summary>
        public int 用户操作类型 { get; set; }
        
        /// <summary>
        /// 账号绑定类型 0表示绑定新账号，1表示绑定已有账号
        /// </summary>
        public int 用户绑定类型 { get; set; }
    }
}
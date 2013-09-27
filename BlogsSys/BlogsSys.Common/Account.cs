using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blogs.Common
{
    public class Account
    {
        /// <summary>
        /// 主键
        /// </summary>   
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 登录的用户名
        /// </summary>
        public string PersonName { get; set; }
        /// <summary>
        /// 角色的集合
        /// </summary>
        public List<string> RoleIds { get; set; }

        /// <summary>
        /// 菜单的集合
        /// </summary>
        public List<string> MenuIds { get; set; }
    }
}

using System;

namespace Udx.Admin.Models
{
    public class UserListOutput: UserGetOutput
    {
        /// <summary>
        /// 是否负责人
        /// </summary>
        public string IsMain { get; set; } 
    }
}

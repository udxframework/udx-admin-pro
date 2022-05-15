using System;
using System.Collections.Generic;
using Udx.Auth;

namespace Udx.Admin.Models
{
    public class AuthLoginResponse: IUser
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }

        // <summary>
        /// 账号
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }
        public string IsAdmin { get; set; }
        public string Token { get; set; }

        public DateTime? Expires { get; set; }
        public string Modules { get; set; }
        public List<RuleModule> RuleModules() => Modules.ToObject<List<RuleModule>>();
    }
}

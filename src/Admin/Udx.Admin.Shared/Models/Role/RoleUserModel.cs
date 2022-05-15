using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udx.Dbs.Entities;

namespace Udx.Admin.Models
{
    public class RoleUserModel:RoleUserEntity
    {
        public bool Checked { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string ModuleType { get; set; }
        public string Description { get; set; }
    }
}

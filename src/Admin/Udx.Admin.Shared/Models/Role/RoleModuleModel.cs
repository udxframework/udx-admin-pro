using Udx.Dbs.Entities;

namespace Udx.Admin.Models
{
    public class RoleModuleModel:RoleModuleEntity
    {
        public bool Checked { get; set; }
        public string ParentId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleKey { get; set; }
        public string Icon { get; set; }
        public string RouterPath { get; set; }

        public string RoleModuleActions { get; set; }

    }
}

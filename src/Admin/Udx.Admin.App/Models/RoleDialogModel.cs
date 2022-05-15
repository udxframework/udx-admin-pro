using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udx.Admin.App.Models
{
    public enum RoleDialogType { User }
    public enum SelectType { radio, checkbox }
    public class RoleDialogModel {

       public RoleDialogType Type { get; set; }
        public string Id { get; set; }
        public SelectType SelectType { get; set; }

        public string Name { get; set; }


    }
}

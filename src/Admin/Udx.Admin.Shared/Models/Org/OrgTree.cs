using System.Collections.Generic;

namespace Udx.Admin.Models;

public class OrgTree
    {
        public string Id { get; set; }
        public string OrgName { get; set; }
        public bool Expand { get; set; }
        public List<OrgTree> Items { get; set; }
    }

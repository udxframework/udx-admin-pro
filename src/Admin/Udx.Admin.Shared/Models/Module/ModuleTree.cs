using System.Collections.Generic;

namespace Udx.Admin.Models;

public class ModuleTree
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Expand { get; set; }

        public string Icon { get; set; }
        public List<ModuleTree> Items { get; set; }
    }

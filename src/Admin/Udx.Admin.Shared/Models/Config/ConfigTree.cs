using System.Collections.Generic;

namespace Udx.Admin.Models;

public class ConfigTree
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Expand { get; set; }
        public List<ConfigTree> Items { get; set; }
    }

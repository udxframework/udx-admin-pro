namespace Udx.Cms.Models;
public class CmsCategoryTree
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool Expand { get; set; }
        public List<CmsCategoryTree> Items { get; set; }
}

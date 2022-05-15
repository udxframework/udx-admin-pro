namespace Udx.Cms.Repositorys
{
    /// <summary>
    /// 分类持久层
    /// </summary>

    public class CmsCategoryRepository : CmsRepositoryBase<CmsCategoryEntity>, ICmsCategoryRepository
    {

        /// <summary>
        /// 分类
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        public CmsCategoryRepository(CmsContext dbContext, IUser user) : base(dbContext, user) { }


        /// <summary>
        /// 获取分类的树形数据
        /// </summary>
        /// <param name="queryRequest"></param>
        /// <returns></returns>
        public async Task<List<CmsCategoryTree>> GetCmsCategoryTreeAsync(QueryModel queryRequest)
        {
            queryRequest.IsNotPage = true;
            var result = await PageLinqQueryAsync<CmsCategoryEntity>(queryRequest);
            var list = result.Data.ToList();
            List<CmsCategoryTree> GetTree(List<CmsCategoryTree> tree)
            {
                foreach (var t in tree)
                {
                    t.Items = (from m in list
                               where m.ParentId == t.Id
                               orderby m.Sort
                               select new CmsCategoryTree()
                               {
                                   Name =m.Name,
                                   Id = m.Id,
                                   
                               }).ToList();
                    if (t.Items != null)
                        GetTree(t.Items);
                }
                return tree;
            }
            var root = list.Where(x => list.All(y => y.Id != x.ParentId)).OrderBy(o=>o.Sort).Select(m=> new CmsCategoryTree()
            {
                Name = m.Name,
                Id = m.Id,
                Expand = true
            });
            var tree = GetTree(root.ToList());

            return tree;

        }

    }
}

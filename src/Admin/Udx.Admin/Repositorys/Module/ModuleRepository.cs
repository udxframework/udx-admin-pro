namespace Udx.Admin.Repositorys;
    /// <summary>
    /// 模块持久层
    /// </summary>

    public class ModuleRepository : AdminRepositoryBase<ModuleEntity>, IModuleRepository
    {

        /// <summary>
        /// 模块
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        public ModuleRepository(AdminContext dbContext, IUser user) : base(dbContext, user) { }

       
        /// <summary>
        /// 获取模块的树形数据
        /// </summary>
        /// <param name="queryRequest"></param>
        /// <returns></returns>
        public async Task<List<ModuleTree>> GetModuleTreeAsync(QueryModel queryRequest)
        {
            queryRequest.IsNotPage = true;
            var result = await PageLinqQueryAsync<ModuleEntity>(queryRequest);
            var list = result.Data;
            List<ModuleTree> GetTree(List<ModuleTree> tree)
            {
                foreach (var t in tree)
                {
                    t.Items = (from m in list
                               where m.ParentId == t.Id
                               orderby m.Sort
                               select new ModuleTree()
                               {
                                   Name =m.Name,
                                   Id = m.Id,
                                   Icon = m.Icon
                               }).ToList();
                    if (t.Items != null)
                        GetTree(t.Items);
                }
                return tree;
            }
            var root = list.Where(x => list.All(y => y.Id != x.ParentId)).OrderBy(o=>o.Sort).Select(m=> new ModuleTree()
            {
                Name = m.Name,
                Id = m.Id,
                Icon = m.Icon,
                Expand = true
            });
            var tree = GetTree(root.ToList());

            return tree;

        }

    }
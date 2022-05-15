using Microsoft.EntityFrameworkCore;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// 模块持久层
    /// </summary>

    public class ConfigRepository : AdminRepositoryBase<ConfigEntity>, IConfigRepository
    {

        /// <summary>
        /// 模块
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        public ConfigRepository(AdminContext dbContext, IUser user) : base(dbContext, user) { }

        /// <summary>
        /// 获取配置的树形数据
        /// </summary>
        /// <param name="queryRequest"></param>
        /// <returns></returns>
        public async Task<List<ConfigTree>> GetConfigTreeAsync(QueryModel queryRequest)
        {           
            queryRequest.IsNotPage = true;            
            var result = await PageLinqQueryAsync<ConfigEntity>(queryRequest);
            var list = result.Data;
            List<ConfigTree> GetTree(List<ConfigTree> tree)
            {
                foreach (var t in tree)
                {
                    t.Items = (from m in list
                                  where m.ParentId == t.Id
                                  orderby m.Sort
                                  select new ConfigTree()
                                  {
                                      Name = m.ConfigKey ?? "" + "【" + m.ConfigTitle ?? "" + "】",
                                      Id = m.Id
                                  }).ToList();
                    if (t.Items != null)
                        GetTree(t.Items);
                }
                return tree;
            }
            var root = list.Where(x => list.All(y => y.Id != x.ParentId)).OrderBy(o => o.Sort).Select(m => new ConfigTree()
            {
                Name = m.ConfigKey ?? "" + "【" + m.ConfigTitle ?? "" + "】",
                Id = m.Id,
                Expand = true
            });
            List<ConfigTree> tree = GetTree(root.ToList());

            return tree;

        }
        /// <summary>
        /// 获取配置的明细列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ConfigDetailEntity>> GetConfigDetailListAsync(string id)
        {

            var list = await _adminContext.ConfigDetailEntity.Where(c => c.ConfigId == id).ToListAsync();
            return list;
        }
       
        /// <summary>
        /// 配置的主子表保存
        /// </summary>
        /// <param name="saveModel"></param>
        public async Task<int> SaveConfigModel(ConfigSaveModel saveModel)
        {
            using var context = new AdminContext() ;
            if (string.IsNullOrEmpty(saveModel.ConfigId))
            {
                var config = saveModel.Config;
                config.Id = config.Id ?? System.Guid.NewGuid().ToString();
                if (saveModel.Operater == SaveOperater.Add)
                {

                    var entity = config.Mapper<ConfigEntity>();
                    entity.CreatedTime = DateTime.Now;
                    entity.CreatedUserId = _identityContext.Id;
                    entity.CreatedUserName = _identityContext.Name;
                    entity.CreatedTime = System.DateTime.Now;
                    context.ConfigEntity.Add(entity);
                }
                else
                {
                    var entity2 = await context.ConfigEntity.SingleOrDefaultAsync(x => x.Id == config.Id);
                    config.Mapper(entity2);
                    //修改
                    entity2.ModifiedUserId = _identityContext.Id;
                    entity2.ModifiedUserName = _identityContext.Name;
                    entity2.ModifiedTime = System.DateTime.Now;
                    context.Entry(entity2).State = EntityState.Modified;
                }
                saveModel.ConfigId = config.Id;
            }
            //保存configDetail
            //先删除
            var configId= saveModel.ConfigId;
            var del = context.ConfigDetailEntity.Where(t => t.ConfigId == configId).ToList();
            foreach (var t in del)
            {
                context.ConfigDetailEntity.Remove(t);
            }
            //在插入
            foreach (var cd in saveModel.ConfigDetails)
            {
                var detailEntity = cd.Mapper<ConfigDetailEntity>();
                detailEntity.Id = detailEntity.Id?? System.Guid.NewGuid().ToString();
                detailEntity.ConfigId = configId;
                detailEntity.CreatedTime = DateTime.Now;
                detailEntity.CreatedUserId = _identityContext.Id;
                detailEntity.CreatedUserName = _identityContext.Name;
                detailEntity.CreatedTime = System.DateTime.Now;
                context.ConfigDetailEntity.Add(detailEntity);
            }
            var result = await context.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// 删除一个配置包括子表明细
        /// </summary>
        public async Task<int> DeleteConfigAsync(string id)
        {
            using var context = new AdminContext();
            var config = await context.ConfigEntity.SingleOrDefaultAsync(x => x.Id == id);
            context.ConfigEntity.Remove(config);
            var del = context.ConfigDetailEntity.Where(t => t.ConfigId == config.Id).ToList();
            foreach (var t in del)
            {
                context.ConfigDetailEntity.Remove(t);
            }
            var result = await context.SaveChangesAsync();
            return result;
        }

    }
}

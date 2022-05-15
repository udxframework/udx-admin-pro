using Udx.Dbs.Entities;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// IConfigRepository
    /// </summary>
    public interface IConfigRepository : Udx.Repositorys.IUdxRepository<ConfigEntity>
    {
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="queryRequest"></param>
        /// <returns></returns>
        public  Task<List<ConfigTree>> GetConfigTreeAsync(QueryModel queryRequest);
        /// <summary>
        /// 获取配置明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<ConfigDetailEntity>> GetConfigDetailListAsync(string id);


        /// <summary>
        /// 配置的主子表保存
        /// </summary>
        /// <param name="saveModel"></param>
        public Task<int> SaveConfigModel(ConfigSaveModel saveModel);
        /// <summary>
        /// 根据配置Id删除主子配置数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  Task<int> DeleteConfigAsync(string id);
    }
}

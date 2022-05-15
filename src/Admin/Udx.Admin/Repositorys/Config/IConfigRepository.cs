using Udx.Dbs.Entities;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// IConfigRepository
    /// </summary>
    public interface IConfigRepository : Udx.Repositorys.IUdxRepository<ConfigEntity>
    {
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="queryRequest"></param>
        /// <returns></returns>
        public  Task<List<ConfigTree>> GetConfigTreeAsync(QueryModel queryRequest);
        /// <summary>
        /// ��ȡ������ϸ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<ConfigDetailEntity>> GetConfigDetailListAsync(string id);


        /// <summary>
        /// ���õ����ӱ���
        /// </summary>
        /// <param name="saveModel"></param>
        public Task<int> SaveConfigModel(ConfigSaveModel saveModel);
        /// <summary>
        /// ��������Idɾ��������������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  Task<int> DeleteConfigAsync(string id);
    }
}

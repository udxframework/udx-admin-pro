using Udx.DBUtility;
using Udx.Models;

namespace Udx.Cms.Repositorys
{
    /// <summary>
    /// CmsContentRepository
    /// </summary>
    public class CmsContentRepository : CmsRepositoryBase<CmsContentEntity>, ICmsContentRepository
    {
      
       /// <summary>
       /// 
       /// </summary>
       /// <param name="dbContext"></param>
       /// <param name="user"></param>
        public CmsContentRepository(CmsContext dbContext, IUser user) : base(dbContext, user) { }

        /// <summary>
        /// 分页查询内容列表
        /// </summary>
        /// <param name="queryRequest"></param>
        /// <returns></returns>
        public async Task<QueryResponse<IEnumerable<T>>> CmsContentQueryAsync<T>(QueryModel queryRequest) 
        {            
            var list = await _context.Set<CmsContentEntity>().AsNoTracking().Pager(queryRequest);
            var result = new QueryResponse<IEnumerable<T>>()
            {
                PageSize = list.PageSize,
                CurrentPage = list.CurrentPage,
                PageCount = list.PageCount,
                RowsCount = list.RowsCount,
                Data = list.Data.Mapper<IEnumerable<T>>()
            };
            return result;
        }

    }
}

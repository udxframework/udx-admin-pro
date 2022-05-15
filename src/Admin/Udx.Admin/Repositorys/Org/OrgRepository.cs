using Udx.Auth;
using Udx.Dbs.Entities;
using Udx.DBUtility;
using Udx.Models;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// 组织机构持久层
    /// </summary>

    public class OrgRepository : AdminRepositoryBase<OrgEntity>, IOrgRepository
    {

        AdminRepositoryBase<OrgUserEntity> orgUserRepository;
        /// <summary>
        /// 组织机构
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        public OrgRepository(AdminContext dbContext, IUser user) : base(dbContext, user) {

            orgUserRepository = new AdminRepositoryBase<OrgUserEntity>(dbContext, user);
        }
        /// <summary>
        /// 获取组织机构的树形数据
        /// </summary>
        /// <param name="queryRequest"></param>
        /// <returns></returns>
        public async Task<List<OrgTree>> GetOrgTreeAsync(QueryModel queryRequest)
        {
            queryRequest.IsNotPage = true;
            var result = await PageLinqQueryAsync<OrgEntity>(queryRequest);
            var list = result.Data;
            List<OrgTree> GetTree(List<OrgTree> tree)
            {
                foreach (var t in tree)
                {
                    t.Items = (from m in list
                               where m.ParentId == t.Id
                               orderby m.Sort
                               select new OrgTree()
                               {
                                   OrgName =m.OrgName,
                                   Id = m.Id
                               }).ToList();
                    if (t.Items != null)
                        GetTree(t.Items);
                }
                return tree;
            }
            var root = list.Where(x => list.All(y => y.Id != x.ParentId)).OrderBy(o => o.Sort).Select(m => new OrgTree()
            {
                OrgName = m.OrgName,
                Id = m.Id,
                Expand = true
            });
            var tree = GetTree(root.ToList());

            return tree;

        }
        /// <summary>
        /// 获取组织机构用户
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<List<OrgUserModel>> GetOrgUserAsync(string orgId) {

           using var ctx =new AdminContext();           
                var list = from  ou in ctx.OrgUserEntity.Where(a => a.OrgId == orgId)
                           join user in ctx.UserEntity
                            on ou.UserId  equals user.Id into leftAu
                           from ou2 in leftAu.DefaultIfEmpty()
                           select new OrgUserModel
                           {
                               Id = ou.Id,
                               OrgId = ou.OrgId,
                               UserId = ou.UserId,
                               Name = ou2.Name,
                               UserName = ou2.UserName,
                               CreatedTime = ou.CreatedTime,
                               CreatedUserId = ou.CreatedUserId,
                               CreatedUserName = ou.CreatedUserName
                           };

           var result = list.ToList();
            return await Task.FromResult(result);

        }
        /// <summary>
        /// 保存组织机构用户
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public async Task<int> SaveOrgUserAsync(SaveModel<OrgUserEntity> saveModel)
        {           
            var result = await orgUserRepository.SaveModelAsync(saveModel);
            return result;
        }
        /// <summary>
        /// 查询组织机构用户
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OrgUserEntity> GetOrgUserAsync(string orgId,string userId)
        {
            var result = await orgUserRepository.FindAsync(s=>s.OrgId== orgId && s.UserId== userId);
            return await Task.FromResult(result);
        }
    }
}

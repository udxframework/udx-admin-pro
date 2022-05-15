using System.Collections.Generic;

namespace Udx.Admin.Repositorys;

    /// <summary>
    /// 角色模块持久层
    /// </summary>

    public class RoleModuleRepository : AdminRepositoryBase<RoleEntity>, IRoleModuleRepository
    {

        /// <summary>
        /// 角色
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        public RoleModuleRepository(AdminContext dbContext, IUser user) : base(dbContext, user) { }

        

        #region 角色模块

       
        /// <summary>
        /// 获取角色的模块
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<RoleModuleTree>> GetRoleModulesAsync(string roleId)
        {
            using var ctx = new AdminContext();
            var list = from module in ctx.ModuleEntity
                       join rm in ctx.RoleModuleEntity.Where(a => a.RoleId == roleId)
                       on module.Id equals rm.ModuleId into leftRoleModule
                       from RoleModule in leftRoleModule.DefaultIfEmpty()
                       join role in ctx.RoleEntity.Where(r => r.Id == roleId ) on RoleModule.RoleId equals role.Id into leftRole2
                       from role2 in leftRole2.DefaultIfEmpty()
                       select new RoleModuleTree
                       {
                           Checked = RoleModule != null,
                           Id = module.Id,
                           ParentId = module.ParentId,
                           Name = module.Name,
                            Key = module.Key,
                           RoleModuleActions = RoleModule.ModuleActions,
                            Actions = module.Actions,
                            Icon = module.Icon,
                            Path = module.Path,
                            ModuleType = module.ModuleType,
                            Sort = module.Sort,
                       };


            var result = list.ToList();
            return await Task.FromResult(result);

        }
        /// <summary>
        /// 获取角色模块树
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>

        public async Task<List<RoleModuleTree>> GetRoleModuleTreeAsync(string roleId)
        {
            var list =await GetRoleModulesAsync(roleId);
            List<RoleModuleTree> GetModuleTree(List<RoleModuleTree> tree)
            {
                foreach (var t in tree)
                {
                    t.ActionList = t.GetRoleModuleActions(t.Actions, t.RoleModuleActions);
                    t.Items = (from m in list
                               where m.ParentId == t.Id
                               orderby m.Sort ascending
                               select m
                                ).ToList();
                    if (t.Items != null)
                        GetModuleTree(t.Items);
                }
                return tree;
            }
            List<RoleModuleTree> moduleTree = GetModuleTree(list.Where(x => list.All(y => y.Id != x.ParentId)).OrderBy(o=>o.Sort).ToList());
            return moduleTree;
        }
        
        /// <summary>
        /// 保存角色的模块
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        public async Task<bool> SaveRoleModuleAsync(RoleModuleSaveModel saveModel)
        {
            using var ctx = new AdminContext();
             ctx.RoleModuleEntity.RemoveRange(ctx.RoleModuleEntity.Where(r => r.RoleId == saveModel.RoleId));               
           
            var adds = saveModel.Models?.ForEach<RoleModuleEntity>(
                   model => new RoleModuleEntity()
                   {
                       Id = System.Guid.NewGuid().ToString(),
                       RoleId = model.RoleId,
                       ModuleId = model.ModuleId,
                       ModuleActions = model.ModuleActions,
                       CreatedTime = DateTime.Now,
                       CreatedUserId = _identityContext.Id,
                       CreatedUserName = _identityContext.Name,
                   }
                );
     
        ctx.RoleModuleEntity.AddRange(adds);
            return await ctx.SaveChangesAsync()>0;
        }

    #endregion


    /// <summary>
    /// 获取用户的模块
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<RuleModule>> GetUserModulesAsync(string userId)
    {
        var list = await GetUserConfigModulesAsync(userId);
        var group = new List<RuleModule>();
        foreach (var item in list) {
            var g = group.Find(s => s.Id == item.Id);
            if (g!=null)
            {
                //g.Actions = item.GetUserModuleActions(g.Actions ?? ""+item.RoleModuleActions ?? "");
                g.ActionList = item.GetRoleModuleActionList(g.ActionList, item.RoleModuleActions);
            }
            else {
                item.ActionList = item.GetRoleModuleActions(item.Actions, item.RoleModuleActions);
                group.Add(new RuleModule { 
                                Id = item.Id,
                                ParentId=item.ParentId,
                                Key = item.Key,
                                Name = item.Name,
                                Actions = item.Actions,
                                ActionList = item.ActionList,
                                Icon = item.Icon,
                                Path   = item.Path,
                    Status = item.Status,
                    HideInMenu = item.HideInMenu,
                    Sort =item.Sort
                });
            }        
        }
        return await Task.FromResult(group);

    }

    /// <summary>
    /// 获取管理员的模块
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<RoleModuleTree>> GetUserConfigModulesAsync(string userId)
    {
        using var ctx = new AdminContext();
        var user = await ctx.UserEntity.FindAsync(userId);
        if(user == null)
            return null;
        var userConfig =  ctx.UserConfigEntity.FirstOrDefault(f => f.UserId == userId);
        if (userConfig != null && userConfig.IsAdmin) //管理员返回所有模块和模块权限
        {
            var list = from module in ctx.ModuleEntity
                       select new RoleModuleTree()
                       {
                           Id = module.Id,
                           ParentId = module.ParentId,
                           Name = module.Name,
                           Key = module.Key,
                           Actions = module.Actions??"",
                           RoleModuleActions = module.Actions??"", //返回所有模块权限
                           Icon = module.Icon,
                           Path = module.Path,
                           Status = module.Status,
                           HideInMenu = module.HideInMenu,
                           Sort = module.Sort
                       };
            return list.ToList();
        }
        else {
            var list = from roleUser in ctx.RoleUserEntity
                       join role in ctx.RoleEntity on roleUser.RoleId equals role.Id
                       join rm in ctx.RoleModuleEntity on roleUser.RoleId equals rm.RoleId
                       join module in ctx.ModuleEntity on rm.ModuleId equals module.Id
                       where roleUser.UserId == user.Id && module.Status != "禁用" && role.Status== "启用"
                       select new RoleModuleTree()
                       {
                           Id = module.Id,
                           ParentId = module.ParentId,
                           Name = module.Name,
                           Key = module.Key,
                           Actions = module.Actions??"",
                           RoleModuleActions = rm.ModuleActions ?? "",
                           Icon = module.Icon,
                           Path = module.Path,
                           Status = module.Status,
                           HideInMenu = module.HideInMenu,
                           Sort = module.Sort
                       };
            return list.ToList();
        }

    }
}


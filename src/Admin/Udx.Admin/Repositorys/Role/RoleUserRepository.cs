using System.Collections.Generic;

namespace Udx.Admin.Repositorys;

    /// <summary>
    /// 角色用户持久层
    /// </summary>

    public class RoleUserRepository : AdminRepositoryBase<RoleEntity>, IRoleUserRepository
    {

        /// <summary>
        /// 角色
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        public RoleUserRepository(AdminContext dbContext, IUser user) : base(dbContext, user) { }

    #region 角色用户

    /// <summary>
    /// 获取用户的角色
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<RoleUserModel>> GetUserRolesAsync(string userId)
    {

        using var ctx = new AdminContext();
        var list = from role in ctx.RoleEntity
                   join ru in ctx.RoleUserEntity.Where(a => a.UserId == userId)
                   on role.Id equals ru.RoleId into leftRoleUser
                   from RoleUser in leftRoleUser.DefaultIfEmpty()
                   join user in ctx.UserEntity.Where(u => u.Id == userId) on RoleUser.UserId equals user.Id into leftUser2
                   from user2 in leftUser2.DefaultIfEmpty()
                   select new RoleUserModel
                   {
                       Checked = RoleUser != null,
                       Id = RoleUser.Id,
                       RoleId = role.Id,
                       UserId = userId,
                       Name = user2.Name,
                       UserName = user2.UserName,
                       RoleName = role.RoleName,
                       ModuleType = role.ModuleType,
                       Description = role.Description,
                       CreatedTime = RoleUser.CreatedTime,
                       CreatedUserId = RoleUser.CreatedUserId,
                       CreatedUserName = RoleUser.CreatedUserName
                   };


        var result = list.ToList();
        return await Task.FromResult(result);

    }
    /// <summary>
    /// 获取角色的用户
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<List<RoleUserModel>> GetRoleUsersAsync(string roleId)
    {

        using var ctx = new AdminContext();
        var list = from user in ctx.UserEntity
                   join ru in ctx.RoleUserEntity.Where(a => a.RoleId == roleId)
                   on user.Id equals ru.UserId into leftRoleUser
                   from RoleUser in leftRoleUser.DefaultIfEmpty()
                   join role in ctx.RoleEntity.Where(r => r.Id == roleId) on RoleUser.RoleId equals role.Id into leftRole2
                   from role2 in leftRole2.DefaultIfEmpty()
                   select new RoleUserModel
                   {
                       Checked = RoleUser != null,
                       Id = role2.Id,
                       RoleId = roleId,
                       UserId = user.Id,
                       Name = user.Name,
                       UserName = user.UserName,
                       RoleName = role2.RoleName,
                       CreatedTime = role2.CreatedTime,
                       CreatedUserId = role2.CreatedUserId,
                       CreatedUserName = role2.CreatedUserName
                   };


        var result = list.ToList();
        return await Task.FromResult(result);

    }
    /// <summary>
    /// 保存用户角色
    /// </summary>
    /// <param name="saveModel"></param>
    /// <returns></returns>
    public async Task<bool> SaveRoleUserAsync(RoleUserSaveModel saveModel)
    {
        using var ctx = new AdminContext();
        if (!string.IsNullOrEmpty(saveModel.UserId))
        {
            ctx.RoleUserEntity.RemoveRange(ctx.RoleUserEntity.Where(r => r.UserId == saveModel.UserId));
        }
        else if (!string.IsNullOrEmpty(saveModel.RoleId))
        {
            ctx.RoleUserEntity.RemoveRange(ctx.RoleUserEntity.Where(r => r.RoleId == saveModel.RoleId));
        }
        var add = saveModel.Models?.ForEach<RoleUserEntity>(
               model => new RoleUserEntity()
               {
                   Id = System.Guid.NewGuid().ToString(),
                   UserId = model.UserId,
                   RoleId = model.RoleId,
                   CreatedTime = DateTime.Now,
                   CreatedUserId = _identityContext.Id,
                   CreatedUserName = _identityContext.Name,
               }
            );
        ctx.RoleUserEntity.AddRange(add);
        return await ctx.SaveChangesAsync() > 0;
    }

    #endregion
}


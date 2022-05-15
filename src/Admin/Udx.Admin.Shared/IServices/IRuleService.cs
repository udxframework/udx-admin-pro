using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udx.Admin.Models;
using Udx.Auth;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// 权限规则服务
    /// </summary>	
    public interface IRuleService
	{

        #region 角色用户


        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<DataResponse<List<RoleUserModel>>> GetUserRolesAsync(DataRequest<string> request);


        /// <summary>
        /// 获取角色的用户
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        Task<DataResponse<List<RoleUserModel>>> GetRoleUsersAsync(DataRequest<string> request);

        /// <summary>
        /// 保存角色和用户
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> SaveRoleUserAsync(DataRequest<RoleUserSaveModel> request);

        #endregion

        #region 角色模块     

        /// <summary>
        /// 获取角色的用户
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        Task<DataResponse<List<RoleModuleTree>>> GetRoleModulesAsync(DataRequest<string> request);

        /// <summary>
        /// 保存角色和用户
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> SaveRoleModuleAsync(DataRequest<RoleModuleSaveModel> request);

        #endregion

        #region 用户的模块权限
        /// <summary>
        /// 获取用户的模块权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<DataResponse<List<RuleModule>>> GetUserModulesAsync(DataRequest<string> request);
        #endregion

    }
}

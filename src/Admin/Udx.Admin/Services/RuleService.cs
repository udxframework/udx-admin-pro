using Udx.Admin.IServices;
using Udx.Admin.Repositorys;

namespace Udx.Admin.Services
{
    /// <summary>
    /// 权限规则服务
    /// </summary>	
    public class RuleService : AdminService, IRuleService
    {
        private readonly IUser _user;
        private readonly IRoleRepository _RoleRepository;
        private readonly IRoleUserRepository _RoleUserRepository;
        private readonly IRoleModuleRepository _RoleModuleRepository;
        /// <summary>
        /// 权限规则服务
        /// </summary>
        /// <param name="user"></param>
        /// <param name="RoleRepository"></param>
        /// <param name="RoleUserRepository"></param>
        /// <param name="RoleModuleRepository"></param>
        public RuleService(
            IUser user,
            IRoleRepository RoleRepository,
            IRoleUserRepository RoleUserRepository,
            IRoleModuleRepository RoleModuleRepository
        )
        {
            _user = user;
            _RoleRepository = RoleRepository;
            _RoleUserRepository = RoleUserRepository;
            _RoleModuleRepository = RoleModuleRepository;
        }

        /// <summary>
        /// 根据用户Id获取用户的角色信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<List<RoleUserModel>>> GetUserRolesAsync(DataRequest<string> request)
        {
            var userId = request.ObjectData;
            var entity = await _RoleUserRepository.GetUserRolesAsync(userId);
            
            if (entity != null)
                 return DataResponse< List<RoleUserModel>>.Success("成功", entity);
            else
                return DataResponse<List<RoleUserModel>>.Fail("没有查询到数据！");

        }
        /// <summary>
        /// 根据角色Id获取角色下的用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<List<RoleUserModel>>> GetRoleUsersAsync(DataRequest<string> request)
        {
            var roleId = request.ObjectData;
            var entity = await _RoleUserRepository.GetRoleUsersAsync(roleId);

            if (entity != null)
                return DataResponse<List<RoleUserModel>>.Success("成功", entity);
            else
                return DataResponse<List<RoleUserModel>>.Fail("没有查询到数据！");

        }

        /// <summary>
        /// 保存角色用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> SaveRoleUserAsync(DataRequest<RoleUserSaveModel> request)
        {
            var saveModel = request.ObjectData;
            var result = await _RoleUserRepository.SaveRoleUserAsync(saveModel);
            return DataResponse<bool>.Result(result, result);
        }
        /// <summary>
        /// 根据角色Id获取角色下的模块信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<List<RoleModuleTree>>> GetRoleModulesAsync(DataRequest<string> request)
        {
            var roleId = request.ObjectData;
            var entity = await _RoleModuleRepository.GetRoleModuleTreeAsync(roleId);

            if (entity != null)
                return DataResponse<List<RoleModuleTree>>.Success("成功", entity);
            else
                return DataResponse<List<RoleModuleTree>>.Fail("没有查询到数据！");
        }
        /// <summary>
        /// 保存角色模块
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> SaveRoleModuleAsync(DataRequest<RoleModuleSaveModel> request)
        {
            var saveModel = request.ObjectData;
            var result = await _RoleModuleRepository.SaveRoleModuleAsync(saveModel);
            return DataResponse<bool>.Result(result, result);
        }
        /// <summary>
        /// 获取用户的模块权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<List<RuleModule>>> GetUserModulesAsync(DataRequest<string> request)
        {
            string userId = request.ObjectData;
            var entity = await _RoleModuleRepository.GetUserModulesAsync(userId);

            if (entity != null)
                return DataResponse<List<RuleModule>>.Success("成功", entity);
            else
                return DataResponse<List<RuleModule>>.Fail("没有查询到数据！");
        }
    }
}

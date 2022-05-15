using Udx.Models;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// IUserRepository
    /// </summary>
    public interface IUserRepository : Udx.Repositorys.IUdxRepository<UserEntity>
    {
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        Task<int> SaveUserAsync(SaveModel<UserEditModel> operaterModel);
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        Task<ExportModel> ExportAsync(QueryModel queryModel);
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserEntity> RegisterAsync(RegisterModel model);
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        Task<int> SaveUserListAsync(SaveModel<List<UserEditModel>> operaterModel);
        /// <summary>
        /// 批量重置密码
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        Task<int> ResetPwdAsync(List<UserEditModel> operaterModel);
        /// <summary>
        /// 验证旧密码
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        Task<bool> ValidPwdAsync(ChangePwdModel operaterModel);

        Task<bool> ChangePwdAsync(ChangePwdModel operaterModel);
    }
}

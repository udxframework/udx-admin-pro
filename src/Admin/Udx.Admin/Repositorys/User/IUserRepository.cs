using Udx.Models;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// IUserRepository
    /// </summary>
    public interface IUserRepository : Udx.Repositorys.IUdxRepository<UserEntity>
    {
        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        Task<int> SaveUserAsync(SaveModel<UserEditModel> operaterModel);
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        Task<ExportModel> ExportAsync(QueryModel queryModel);
        /// <summary>
        /// �û�ע��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserEntity> RegisterAsync(RegisterModel model);
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        Task<int> SaveUserListAsync(SaveModel<List<UserEditModel>> operaterModel);
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        Task<int> ResetPwdAsync(List<UserEditModel> operaterModel);
        /// <summary>
        /// ��֤������
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        Task<bool> ValidPwdAsync(ChangePwdModel operaterModel);

        Task<bool> ChangePwdAsync(ChangePwdModel operaterModel);
    }
}

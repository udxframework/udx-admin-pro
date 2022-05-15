using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udx.Admin.Models;
using Udx.Auth;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// Ȩ�޹������
    /// </summary>	
    public interface IRuleService
	{

        #region ��ɫ�û�


        /// <summary>
        /// ��ȡ�û��Ľ�ɫ
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns></returns>
        Task<DataResponse<List<RoleUserModel>>> GetUserRolesAsync(DataRequest<string> request);


        /// <summary>
        /// ��ȡ��ɫ���û�
        /// </summary>
        /// <param name="roleId">��ɫId</param>
        /// <returns></returns>
        Task<DataResponse<List<RoleUserModel>>> GetRoleUsersAsync(DataRequest<string> request);

        /// <summary>
        /// �����ɫ���û�
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> SaveRoleUserAsync(DataRequest<RoleUserSaveModel> request);

        #endregion

        #region ��ɫģ��     

        /// <summary>
        /// ��ȡ��ɫ���û�
        /// </summary>
        /// <param name="roleId">��ɫId</param>
        /// <returns></returns>
        Task<DataResponse<List<RoleModuleTree>>> GetRoleModulesAsync(DataRequest<string> request);

        /// <summary>
        /// �����ɫ���û�
        /// </summary>
        /// <param name="saveModel"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> SaveRoleModuleAsync(DataRequest<RoleModuleSaveModel> request);

        #endregion

        #region �û���ģ��Ȩ��
        /// <summary>
        /// ��ȡ�û���ģ��Ȩ��
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns></returns>
        Task<DataResponse<List<RuleModule>>> GetUserModulesAsync(DataRequest<string> request);
        #endregion

    }
}

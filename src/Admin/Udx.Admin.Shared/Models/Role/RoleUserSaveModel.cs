using System.Collections.Generic;

namespace Udx.Admin.Models;

/// <summary>
/// 角色用户保存实体
/// </summary>
public class RoleUserSaveModel
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// 角色Id
    /// </summary>
    public string RoleId { get; set; }

    /// <summary>
    /// 保存的列表
    /// </summary>
        public List<RoleUserModel> Models { get; set; }
       
    }
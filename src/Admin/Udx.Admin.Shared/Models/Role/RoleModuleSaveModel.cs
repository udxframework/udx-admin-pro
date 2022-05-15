using System.Collections.Generic;

namespace Udx.Admin.Models;

/// <summary>
/// 角色模块保存
/// </summary>
public class RoleModuleSaveModel
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public string RoleId { get; set; }

    /// <summary>
    /// 保存的列表
    /// </summary>
        public List<RoleModuleModel> Models { get; set; }
       
    }
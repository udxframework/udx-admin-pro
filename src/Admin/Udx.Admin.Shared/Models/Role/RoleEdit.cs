using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Udx.Entites.BaseEntites;

namespace Udx.Admin.Models;

    /// <summary>
    /// Role的编辑实体
    /// </summary>
    public partial class RoleEdit : Udx.Dbs.Entities.RoleEntity
    {
       
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        public override string RoleName { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        [Required(ErrorMessage = "请输入状态")]
        public override string Status { get; set; }



    /// <summary>
    /// 模块
    /// </summary>
    public override string ModuleType { get; set; }


    /// <summary>
    /// 模块
    /// </summary>
    public  List<string> ModuleTypeOptions { get; set; }


    /// <summary>
    /// 提示信息
    /// </summary>
    public string MessageInfo { get; set; }
    }
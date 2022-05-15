using System.ComponentModel.DataAnnotations;
using Udx.Entites.BaseEntites;

namespace Udx.Admin.Models;

    /// <summary>
    /// Module的编辑实体
    /// </summary>
    public partial class ModuleEdit : Udx.Dbs.Entities.ModuleEntity
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称标题")]
        public override string Name { get; set; }

        /// <summary>
        /// 模块Key
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "请输入模块Key,要求唯一")]
        public override string Key { get; set; }

    /// <summary>
    /// 模块Type
    /// </summary>
    [StringLength(100)]
    [Required(ErrorMessage = "请选择系统模块")]
    public override string ModuleType { get; set; }

    /// <summary>
    /// 提示信息
    /// </summary>
    public string MessageInfo { get; set; }

    }
using System.ComponentModel.DataAnnotations;
using Udx.Entites.BaseEntites;

namespace Udx.Admin.Models;

    /// <summary>
    /// Config的编辑实体
    /// </summary>
    public partial class ConfigEdit : EntityFull
    {
    public string ParentId { get; set; }

    /// <summary>
    /// 请输入配置名称
    /// </summary>
    [Required(ErrorMessage = "请输入配置名称")]
    public string ConfigTitle { get; set; }
    /// <summary>
    /// 请输入键Key
    /// </summary>
    [Required(ErrorMessage = "请输入键Key")]
        public string ConfigKey { get; set; }


    public string ConfigValue { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; } 

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public decimal Sort { get; set; }

    /// <summary>
    /// 提示信息
    /// </summary>
    public string MessageInfo { get; set; }
    }
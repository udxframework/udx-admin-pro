using System.ComponentModel.DataAnnotations;
using Udx.Entites.BaseEntites;

namespace Udx.Admin.Models;

    /// <summary>
    /// Org的编辑实体
    /// </summary>
    public partial class OrgEdit : EntityFull
    {
    public string ParentId { get; set; }
    /// <summary>
    /// 组织机构名称
    /// </summary>
    [Required(ErrorMessage = "请输入名称标题")]
        public string OrgName { get; set; }


   

        /// <summary>
        /// 组织机构代码
        /// </summary>
        [StringLength(100)]
        public string OrgCode { get; set; }


       
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
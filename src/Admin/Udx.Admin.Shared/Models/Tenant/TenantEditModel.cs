using System;
using System.ComponentModel.DataAnnotations;
using Udx.Entites.BaseEntites;

namespace Udx.Admin.Models
{
    /// <summary>
    /// 修改
    /// </summary>
    public partial class TenantEditModel : Udx.Dbs.Entities.TenantEntity
    {

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "请输入编码")]
        public override string Code { get; set; } = "0";


        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        public override string Name { get; set; }

        public string MessageInfo { get; set; }
    }
}

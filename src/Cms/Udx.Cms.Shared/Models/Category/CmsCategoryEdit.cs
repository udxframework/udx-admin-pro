namespace Udx.Cms.Models;

/// <summary>
/// CmsCategory的编辑实体
/// </summary>
public  class CmsCategoryEdit : Udx.Dbs.Entities.CmsCategoryEntity
    {
    /// <summary>
    /// 分类标题
    /// </summary>
    [StringLength(50)]
    [Required(ErrorMessage = "请输入分类标题")]
        public override string Name { get; set; }

    /// <summary>
    /// 分类状态
    /// </summary>
    [StringLength(20)]
        [Required(ErrorMessage = "请选择分类状态")]
        public override string Status { get; set; }

    /// <summary>
    /// 分类分组Group
    /// </summary>
    [StringLength(20)]
    [Required(ErrorMessage = "请选择分类分组")]
    public override string Group { get; set; }

    /// <summary>
    /// 提示信息
    /// </summary>
    public string MessageInfo { get; set; }

    }

namespace Udx.Cms.Models;
/// <summary>
/// 修改
/// </summary>
public class CmsContentEditModel : Udx.Dbs.Entities.CmsContentEntity
{

    /// <summary>
    /// 标题
    /// </summary>
    [Required(ErrorMessage = "请输入标题")]
    public override string Title { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [Required(ErrorMessage = "请选择分类")]
    public override string CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public IEnumerable<string> CategoryIds {
       get{
            if (string.IsNullOrEmpty(CategoryId))
            {
                return new List<string>();
            }
            return new List<string> { CategoryId };
        }
        set { CategoryId = string.Join(",",value); }
    }
    /// <summary>
    /// 内容
    /// </summary>
    [Required(ErrorMessage = "请输入内容")]
    [StringLength(999999)]
    public override string Contents { get; set; }

    [StringLength(255)]
    public override string Summary { get; set; }

    public string MessageInfo { get; set; }

   


}
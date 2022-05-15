using Udx.Entites.BaseEntites;

namespace Udx.Cms.Models;
public class CmsContentListResponse: EntityFull
{
    

    public  string Title { get; set; }

    
    public  string Author { get; set; }

   
  
    public  string CoverImgUrl { get; set; }

   
    public  string CategoryId { get; set; }
        
    public  string Status { get; set; }
    public string Summary { get; set; }


    public  decimal Sort { get; set; }
    /// <summary>
    /// 阅读数
    /// </summary>
    public  int BrowseCount { get; set; }

    /// <summary>
    /// 在看数
    /// </summary>
    public  int LookCount { get; set; }
    /// <summary>
    /// 回复数
    /// </summary>
    public  int CommentCount { get; set; }
    /// <summary>
    /// 点赞数
    /// </summary>
    public  int LikeCount { get; set; }
}

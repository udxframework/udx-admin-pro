
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Udx.Dbs.Entities;
using Udx.Entites.BaseEntites;

namespace Udx.Admin.Models;

    /// <summary>
    /// 组织机构用户的实体
    /// </summary>
    public partial class OrgUserModel :OrgUserEntity
    {
   

    /// <summary>
    /// 账号
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }
}
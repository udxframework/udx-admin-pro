using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udx.Dbs.Migrations.Cms
{
    public partial class CmsDbSchame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "udx_cms_category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, comment: "主键Id"),
                    TenantId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "租户Id"),
                    Version = table.Column<long>(type: "bigint", nullable: false, comment: "版本"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "是否删除"),
                    CreatedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "创建者Id"),
                    CreatedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "创建者"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "创建时间"),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "修改者Id"),
                    ModifiedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改者"),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "修改时间"),
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "上级Id"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "名称"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "状态"),
                    Group = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "分类分组"),
                    Sort = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_cms_category", x => x.Id);
                },
                comment: "Cms分类表");

            migrationBuilder.CreateTable(
                name: "udx_cms_content",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, comment: "主键Id"),
                    TenantId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "租户Id"),
                    Version = table.Column<long>(type: "bigint", nullable: false, comment: "版本"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "是否删除"),
                    CreatedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "创建者Id"),
                    CreatedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "创建者"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "创建时间"),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "修改者Id"),
                    ModifiedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改者"),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "修改时间"),
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "父页面ID"),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "标题"),
                    Author = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true, comment: "作者"),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "内容"),
                    CoverImgUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "封面图片"),
                    Summary = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "摘要"),
                    CategoryId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "分类Id"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "状态"),
                    Tags = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "标签"),
                    IsComment = table.Column<bool>(type: "bit", nullable: false, comment: "是否留言"),
                    Reference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "引用"),
                    Sort = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "排序"),
                    BrowseCount = table.Column<int>(type: "int", nullable: false, comment: "阅读数"),
                    LookCount = table.Column<int>(type: "int", nullable: false, comment: "在看数"),
                    CommentCount = table.Column<int>(type: "int", nullable: false, comment: "回复数"),
                    LikeCount = table.Column<int>(type: "int", nullable: false, comment: "点赞数")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_cms_content", x => x.Id);
                },
                comment: "CMS内容表");

            migrationBuilder.CreateTable(
                name: "udx_cms_content_comment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, comment: "主键Id"),
                    TenantId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "租户Id"),
                    Version = table.Column<long>(type: "bigint", nullable: false, comment: "版本"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "是否删除"),
                    CreatedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "创建者Id"),
                    CreatedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "创建者"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "创建时间"),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "修改者Id"),
                    ModifiedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改者"),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "修改时间"),
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "父ID"),
                    ContentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "内容Id"),
                    Author = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "作者"),
                    Messages = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "内容"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "状态"),
                    Sort = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_cms_content_comment", x => x.Id);
                },
                comment: "CMS留言表");

            migrationBuilder.CreateTable(
                name: "udx_cms_file",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, comment: "主键Id"),
                    TenantId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "租户Id"),
                    Version = table.Column<long>(type: "bigint", nullable: false, comment: "版本"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "是否删除"),
                    CreatedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "创建者Id"),
                    CreatedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "创建者"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "创建时间"),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "修改者Id"),
                    ModifiedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "修改者"),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "修改时间"),
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "父ID"),
                    ObjectId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "MongoDB ObjectId"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "标题"),
                    Author = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "作者"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "状态"),
                    Summary = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true, comment: "摘要"),
                    CategoryId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "分类Id"),
                    Sort = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_cms_file", x => x.Id);
                },
                comment: "CMS文件表");

            migrationBuilder.CreateIndex(
                name: "Index_CmsCategory_ParentId",
                table: "udx_cms_category",
                columns: new[] { "TenantId", "ParentId" });

            migrationBuilder.CreateIndex(
                name: "Index_CmsContent_CategoryId",
                table: "udx_cms_content",
                columns: new[] { "TenantId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "Index_CmsContentComment_ContentId",
                table: "udx_cms_content_comment",
                columns: new[] { "TenantId", "ContentId" });

            migrationBuilder.CreateIndex(
                name: "Index_CmsFile_ParentId",
                table: "udx_cms_file",
                columns: new[] { "TenantId", "ParentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "udx_cms_category");

            migrationBuilder.DropTable(
                name: "udx_cms_content");

            migrationBuilder.DropTable(
                name: "udx_cms_content_comment");

            migrationBuilder.DropTable(
                name: "udx_cms_file");
        }
    }
}

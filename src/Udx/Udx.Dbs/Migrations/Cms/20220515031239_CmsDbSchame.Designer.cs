// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Udx.Cms;

#nullable disable

namespace Udx.Dbs.Migrations.Cms
{
    [DbContext(typeof(CmsContext))]
    [Migration("20220515031239_CmsDbSchame")]
    partial class CmsDbSchame
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Udx.Dbs.Entities.CmsCategoryEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(0)
                        .HasComment("主键Id");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6)
                        .HasComment("创建时间");

                    b.Property<string>("CreatedUserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(4)
                        .HasComment("创建者Id");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(5)
                        .HasComment("创建者");

                    b.Property<string>("Group")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("分类分组");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnOrder(3)
                        .HasComment("是否删除");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(9)
                        .HasComment("修改时间");

                    b.Property<string>("ModifiedUserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(7)
                        .HasComment("修改者Id");

                    b.Property<string>("ModifiedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(8)
                        .HasComment("修改者");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("名称");

                    b.Property<string>("ParentId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasComment("上级Id");

                    b.Property<decimal>("Sort")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("排序");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("状态");

                    b.Property<string>("TenantId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(1)
                        .HasComment("租户Id");

                    b.Property<long>("Version")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2)
                        .HasComment("版本");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TenantId", "ParentId" }, "Index_CmsCategory_ParentId");

                    b.ToTable("udx_cms_category");

                    b.HasComment("Cms分类表");
                });

            modelBuilder.Entity("Udx.Dbs.Entities.CmsContentCommentEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(0)
                        .HasComment("主键Id");

                    b.Property<string>("Author")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasComment("作者");

                    b.Property<string>("ContentId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasComment("内容Id");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6)
                        .HasComment("创建时间");

                    b.Property<string>("CreatedUserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(4)
                        .HasComment("创建者Id");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(5)
                        .HasComment("创建者");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnOrder(3)
                        .HasComment("是否删除");

                    b.Property<string>("Messages")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("内容");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(9)
                        .HasComment("修改时间");

                    b.Property<string>("ModifiedUserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(7)
                        .HasComment("修改者Id");

                    b.Property<string>("ModifiedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(8)
                        .HasComment("修改者");

                    b.Property<string>("ParentId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasComment("父ID");

                    b.Property<decimal>("Sort")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("排序");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("状态");

                    b.Property<string>("TenantId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(1)
                        .HasComment("租户Id");

                    b.Property<long>("Version")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2)
                        .HasComment("版本");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TenantId", "ContentId" }, "Index_CmsContentComment_ContentId");

                    b.ToTable("udx_cms_content_comment");

                    b.HasComment("CMS留言表");
                });

            modelBuilder.Entity("Udx.Dbs.Entities.CmsContentEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(0)
                        .HasComment("主键Id");

                    b.Property<string>("Author")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasComment("作者");

                    b.Property<int>("BrowseCount")
                        .HasColumnType("int")
                        .HasComment("阅读数");

                    b.Property<string>("CategoryId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasComment("分类Id");

                    b.Property<int>("CommentCount")
                        .HasColumnType("int")
                        .HasComment("回复数");

                    b.Property<string>("Contents")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("内容");

                    b.Property<string>("CoverImgUrl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasComment("封面图片");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6)
                        .HasComment("创建时间");

                    b.Property<string>("CreatedUserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(4)
                        .HasComment("创建者Id");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(5)
                        .HasComment("创建者");

                    b.Property<bool>("IsComment")
                        .HasColumnType("bit")
                        .HasComment("是否留言");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnOrder(3)
                        .HasComment("是否删除");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int")
                        .HasComment("点赞数");

                    b.Property<int>("LookCount")
                        .HasColumnType("int")
                        .HasComment("在看数");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(9)
                        .HasComment("修改时间");

                    b.Property<string>("ModifiedUserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(7)
                        .HasComment("修改者Id");

                    b.Property<string>("ModifiedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(8)
                        .HasComment("修改者");

                    b.Property<string>("ParentId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasComment("父页面ID");

                    b.Property<string>("Reference")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasComment("引用");

                    b.Property<decimal>("Sort")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("排序");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("状态");

                    b.Property<string>("Summary")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasComment("摘要");

                    b.Property<string>("Tags")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasComment("标签");

                    b.Property<string>("TenantId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(1)
                        .HasComment("租户Id");

                    b.Property<string>("Title")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasComment("标题");

                    b.Property<long>("Version")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2)
                        .HasComment("版本");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TenantId", "CategoryId" }, "Index_CmsContent_CategoryId");

                    b.ToTable("udx_cms_content");

                    b.HasComment("CMS内容表");
                });

            modelBuilder.Entity("Udx.Dbs.Entities.CmsFileEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(0)
                        .HasComment("主键Id");

                    b.Property<string>("Author")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasComment("作者");

                    b.Property<string>("CategoryId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasComment("分类Id");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(6)
                        .HasComment("创建时间");

                    b.Property<string>("CreatedUserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(4)
                        .HasComment("创建者Id");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(5)
                        .HasComment("创建者");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnOrder(3)
                        .HasComment("是否删除");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(9)
                        .HasComment("修改时间");

                    b.Property<string>("ModifiedUserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(7)
                        .HasComment("修改者Id");

                    b.Property<string>("ModifiedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(8)
                        .HasComment("修改者");

                    b.Property<string>("ObjectId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("MongoDB ObjectId");

                    b.Property<string>("ParentId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasComment("父ID");

                    b.Property<decimal>("Sort")
                        .HasColumnType("decimal(18,2)")
                        .HasComment("排序");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("状态");

                    b.Property<string>("Summary")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)")
                        .HasComment("摘要");

                    b.Property<string>("TenantId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnOrder(1)
                        .HasComment("租户Id");

                    b.Property<string>("Title")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasComment("标题");

                    b.Property<long>("Version")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2)
                        .HasComment("版本");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TenantId", "ParentId" }, "Index_CmsFile_ParentId");

                    b.ToTable("udx_cms_file");

                    b.HasComment("CMS文件表");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udx.Dbs.Migrations
{
    public partial class AdminDbSchame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "udx_admin_config",
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
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "父Id"),
                    ConfigTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "键标题"),
                    ConfigKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "键Key"),
                    ConfigValue = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "值Value"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "状态"),
                    Sort = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "排序"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "描述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_config", x => x.Id);
                },
                comment: "配置表");

            migrationBuilder.CreateTable(
                name: "udx_admin_config_detail",
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
                    ConfigId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "配置父表Id"),
                    ConfigDetailTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "键标题"),
                    ConfigDetailKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "键Key"),
                    ConfigDetailValue = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "值Value"),
                    Sort = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "排序"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "状态"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "描述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_config_detail", x => x.Id);
                },
                comment: "配置子表");

            migrationBuilder.CreateTable(
                name: "udx_admin_module",
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
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "父Id"),
                    ModuleType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "模块分类"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "模块名称"),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "模块Key"),
                    Actions = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "模块Actions"),
                    Icon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "模块Icon"),
                    Path = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "模块路由Path"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "状态"),
                    HideInMenu = table.Column<bool>(type: "bit", nullable: false, comment: "是否隐藏"),
                    Sort = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "排序"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "描述")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_module", x => x.Id);
                },
                comment: "模块表");

            migrationBuilder.CreateTable(
                name: "udx_admin_number",
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
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "名称"),
                    Template = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "模板"),
                    MaxNumber = table.Column<int>(type: "int", nullable: false, comment: "当前最大流水号"),
                    LastNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "最后生成的流水号")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_number", x => x.Id);
                },
                comment: "流水号规则配置表");

            migrationBuilder.CreateTable(
                name: "udx_admin_org",
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
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "父Id"),
                    OrgName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "组织机构名称"),
                    OrgCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "组织机构代码"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "状态"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "描述"),
                    Sort = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_org", x => x.Id);
                },
                comment: "组织机构");

            migrationBuilder.CreateTable(
                name: "udx_admin_org_user",
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
                    OrgId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "组织机构Id"),
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "用户Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_org_user", x => x.Id);
                },
                comment: "组织机构用户");

            migrationBuilder.CreateTable(
                name: "udx_admin_role",
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
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "名称"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "状态"),
                    ModuleType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "模块分类"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "说明"),
                    Sort = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_role", x => x.Id);
                },
                comment: "角色表");

            migrationBuilder.CreateTable(
                name: "udx_admin_role_module",
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
                    RoleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "角色Id"),
                    ModuleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "模块Id"),
                    ModuleActions = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "模块Actions")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_role_module", x => x.Id);
                },
                comment: "角色模块表");

            migrationBuilder.CreateTable(
                name: "udx_admin_role_user",
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
                    RoleId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "角色Id"),
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "用户Id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_role_user", x => x.Id);
                },
                comment: "角色用户表");

            migrationBuilder.CreateTable(
                name: "udx_admin_tenant",
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
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "编码"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "名称"),
                    DbType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "数据库类型"),
                    ConnectionString = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "连接字符串"),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, comment: "是否启用"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "说明"),
                    Sort = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "排序")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_tenant", x => x.Id);
                },
                comment: "租户表");

            migrationBuilder.CreateTable(
                name: "udx_admin_user",
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
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "账号"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "姓名"),
                    NickName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "昵称"),
                    Avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "头像"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Email"),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Phone"),
                    Signature = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "Signature"),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Country"),
                    RegionCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "区域代码RegionCode"),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "Address"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "状态"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "描述"),
                    OrgId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "默认组织机构Id"),
                    IsAdmin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "是否数据权限超级用户")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_user", x => x.Id);
                },
                comment: "用户表");

            migrationBuilder.CreateTable(
                name: "udx_admin_user_config",
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
                    ParentId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "用户Id"),
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "用户Id"),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "是否管理员"),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "主题皮肤")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_user_config", x => x.Id);
                },
                comment: "用户配置表");

            migrationBuilder.CreateTable(
                name: "udx_admin_user_oauth",
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
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "用户Id"),
                    OAuthName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "OAuthName"),
                    Unionid = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Unionid"),
                    Openid = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "Openid"),
                    NickName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, comment: "昵称"),
                    Avatar = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "头像"),
                    BindStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "绑定状态(已绑定/未绑定)"),
                    MsgTip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "消息提醒(已开启/未开启)"),
                    OAuthResult = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "绑定结果")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_user_oauth", x => x.Id);
                },
                comment: "用户账户OAuth绑定表");

            migrationBuilder.CreateTable(
                name: "udx_admin_user_password",
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
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "用户Id"),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "登录密码"),
                    Password1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "密码1"),
                    Password2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "密码2"),
                    Password3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "密码3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_admin_user_password", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "udx_eventbus_eventdata",
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
                    EventId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EventData = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "消息事件数据"),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriberType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriberDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_eventbus_eventdata", x => x.Id);
                },
                comment: "事件数据表");

            migrationBuilder.CreateTable(
                name: "udx_log_info",
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
                    LogType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "日志分类"),
                    Logger = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "日志数据"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "日志说明")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udx_log_info", x => x.Id);
                },
                comment: "日志表");

            migrationBuilder.CreateIndex(
                name: "Index_Config_TenantId",
                table: "udx_admin_config",
                columns: new[] { "TenantId", "ConfigKey" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [ConfigKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_ConfigDetail_TenantId",
                table: "udx_admin_config_detail",
                columns: new[] { "TenantId", "ConfigDetailKey" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [ConfigDetailKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_Module_TenantId_ModuleKey",
                table: "udx_admin_module",
                columns: new[] { "TenantId", "Id" });

            migrationBuilder.CreateIndex(
                name: "Index_udx_admin_number",
                table: "udx_admin_number",
                columns: new[] { "TenantId", "Name" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_Org_TenantId",
                table: "udx_admin_org",
                columns: new[] { "TenantId", "OrgCode" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [OrgCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_Org_UserId",
                table: "udx_admin_org_user",
                columns: new[] { "TenantId", "OrgId", "UserId" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [OrgId] IS NOT NULL AND [UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_Role_TenantId",
                table: "udx_admin_role",
                columns: new[] { "TenantId", "RoleName" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [RoleName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_Role_ModuleId",
                table: "udx_admin_role_module",
                columns: new[] { "TenantId", "RoleId", "ModuleId" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [RoleId] IS NOT NULL AND [ModuleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_Role_UserId",
                table: "udx_admin_role_user",
                columns: new[] { "TenantId", "RoleId", "UserId" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [RoleId] IS NOT NULL AND [UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_Tenant_Code",
                table: "udx_admin_tenant",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_User_TenantId",
                table: "udx_admin_user",
                columns: new[] { "TenantId", "UserName" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_UserConfig_TenantId",
                table: "udx_admin_user_config",
                columns: new[] { "TenantId", "UserId" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_User__OAuthName_TenantId",
                table: "udx_admin_user_oauth",
                columns: new[] { "TenantId", "UserId", "OAuthName" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [UserId] IS NOT NULL AND [OAuthName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_UserPassword_UserId",
                table: "udx_admin_user_password",
                columns: new[] { "TenantId", "UserId" },
                unique: true,
                filter: "[TenantId] IS NOT NULL AND [UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_EventData_TenantId",
                table: "udx_eventbus_eventdata",
                columns: new[] { "TenantId", "EventId" });

            migrationBuilder.CreateIndex(
                name: "Index_Log_TenantId_LogType",
                table: "udx_log_info",
                columns: new[] { "TenantId", "LogType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "udx_admin_config");

            migrationBuilder.DropTable(
                name: "udx_admin_config_detail");

            migrationBuilder.DropTable(
                name: "udx_admin_module");

            migrationBuilder.DropTable(
                name: "udx_admin_number");

            migrationBuilder.DropTable(
                name: "udx_admin_org");

            migrationBuilder.DropTable(
                name: "udx_admin_org_user");

            migrationBuilder.DropTable(
                name: "udx_admin_role");

            migrationBuilder.DropTable(
                name: "udx_admin_role_module");

            migrationBuilder.DropTable(
                name: "udx_admin_role_user");

            migrationBuilder.DropTable(
                name: "udx_admin_tenant");

            migrationBuilder.DropTable(
                name: "udx_admin_user");

            migrationBuilder.DropTable(
                name: "udx_admin_user_config");

            migrationBuilder.DropTable(
                name: "udx_admin_user_oauth");

            migrationBuilder.DropTable(
                name: "udx_admin_user_password");

            migrationBuilder.DropTable(
                name: "udx_eventbus_eventdata");

            migrationBuilder.DropTable(
                name: "udx_log_info");
        }
    }
}

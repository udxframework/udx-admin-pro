using MiniExcelLibs;
using System.IO;
using Udx.Dbs.Cache;
using Udx.Encryptions;
using Udx.Extensions;
using Udx.Mongo.Bucket;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// UserRepository
    /// </summary>
    public class UserRepository : AdminRepositoryBase<UserEntity>, IUserRepository
    {
        readonly ILogger _logger;

        private readonly IEventPublisher _eventPublisher;
        /// <summary>
        /// UserRepository
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        /// <param name="logger">日志</param>
        public UserRepository(AdminContext dbContext, IUser user, IEventPublisher eventPublisher, ILogger<UserRepository> logger) : base(dbContext, user) {

            _logger = logger;
            _eventPublisher = eventPublisher;

        }


       
     
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        public async Task<int> SaveUserAsync(SaveModel<UserEditModel> operaterModel)
        {
          
            var saveModel = operaterModel.Mapper<SaveModel<UserEntity>>();
            int result;
            //TODO:新增时候密码设置默认88888888
            if (saveModel.Operater == SaveOperater.Add)
            {
                var password = operaterModel.Model.Password ?? saveModel.Model.UserName+"123456";
                saveModel.Model.Id = System.Guid.NewGuid().ToString();
                var pwd = MD5Encryption.Encrypt($"{ saveModel.Model.Id.ToLower()}-udx-{password.ToLower()}");
                result = await SaveModelAsync(saveModel) ;
                //新增密码到密码表
                await AddAsync(new UserPasswordEntity { UserId = saveModel.Model.Id,Password = pwd});
                await AddAsync(new UserConfigEntity
                {
                    UserId = saveModel.Model.Id,
                    IsAdmin = false
                });
                operaterModel.Model.Id = saveModel.Model.Id;
            }
            else {

                result = await SaveModelAsync(saveModel) ;
            }
            return result;
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        public async Task<ExportModel> ExportAsync(QueryModel queryModel)
        {
            var model = new ExportModel()
            {
                Name = $"用户信息-{System.DateTime.Now.GetFormatString()}.xlsx"

            };
            using var ctx = new AdminContext();
            var reader = ctx.SqlExecuteReader("select * from udx_admin_user(nolock)");
            using (var stream = new MemoryStream())
            {
                await MiniExcel.SaveAsAsync(stream, reader);
                using StreamReader streamReader = new StreamReader(stream);
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                var exportBucket = new ExportBucket();
                model.BucketName = exportBucket.BucketName;
                model.ObjectId = await exportBucket.UploadFile(model.Name, streamReader.BaseStream);
            }
            return model;
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        public async Task<int> SaveUserListAsync(SaveModel<List<UserEditModel>> operaterModel)
        {

            var saveModel = operaterModel.Mapper<SaveModel<List<UserEntity>>>();
            int result;
            //TODO:新增时候密码设置默认88888888
            if (saveModel.Operater == SaveOperater.Add)
            {
                List<UserPasswordEntity> pwdModel = new List<UserPasswordEntity>();
                List<UserConfigEntity> configModel = new List<UserConfigEntity>();

                foreach (var item in saveModel.Model)
                {
                    item.Id = System.Guid.NewGuid().ToString();
                    var pwd = MD5Encryption.Encrypt($"{ item.Id.ToLower()}-udx-{item.UserName}123456");
                    pwdModel.Add(new UserPasswordEntity() { UserId = item.Id, Password = pwd });
                    configModel.Add(new UserConfigEntity()
                    {
                        UserId = item.Id,
                        IsAdmin = false
                    });
                }
                await AddListAsync<UserPasswordEntity>(pwdModel);
                await AddListAsync<UserConfigEntity>(configModel);
            }

            result = await SaveModelListAsync(saveModel);

            return result;
        }

        /// <summary>
        /// 批量重置密码
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        public async Task<int> ResetPwdAsync(List<UserEditModel> operaterModel)
        {
            List<UserPasswordEntity> pwdModel = new List<UserPasswordEntity>();
            foreach (var item in operaterModel)
            {
                var pwd = MD5Encryption.Encrypt($"{ item.Id.ToLower()}-udx-{item.UserName}123456");
                var dts = _adminContext.UserPasswordEntity.Where(p => p.UserId == item.Id).ToList();
                dts.ForEach(p => p.Password = pwd);
                pwdModel.AddRange(dts);
            }
             return  await UpdateListAsync(pwdModel);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserEntity> RegisterAsync(RegisterModel model)
        {            
            var userEntity = model.Mapper<UserEntity>();
            using var context = new AdminContext();
            userEntity.Id = model.Id?? System.Guid.NewGuid().ToString();
            userEntity.Status = "启用";
            userEntity.CreatedTime = DateTime.Now;
            userEntity.CreatedUserId = _identityContext.Id;
            userEntity.CreatedUserName = _identityContext.Name;
            userEntity.CreatedTime = System.DateTime.Now;
            await context.UserEntity.AddAsync(userEntity);
            var pwd = MD5Encryption.Encrypt($"{ userEntity.Id.ToLower()}-udx-{model.Password}");
            await context.UserPasswordEntity.AddAsync(new UserPasswordEntity {
                UserId = userEntity.Id,
                Password = pwd 
            });
            //注册后的默认配置
            await context.UserConfigEntity.AddAsync(new UserConfigEntity
            {
                UserId = userEntity.Id,
                IsAdmin = false
            });
            //注册后的默认角色
            var roleId = await ConfigCache.GetConfigDetailValueAsync("Admin-User", "DefaultRegisterRoleId");
            if (!string.IsNullOrEmpty(roleId))
            {
                await context.RoleUserEntity.AddAsync(new RoleUserEntity()
                {
                    RoleId = roleId.ToString(),
                    UserId = userEntity.Id,

                });
            }
            await context.SaveChangesAsync();
            return userEntity;
        }

        /// <summary>
        /// 验证旧密码
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        public async Task<bool> ValidPwdAsync(ChangePwdModel operaterModel)
        {
            var dts = _adminContext.UserPasswordEntity.Where(p => p.UserId == _identityContext.Id).ToList();
            if(dts.Count>0)
            {
                if(operaterModel.OldPwdMd5 != dts.First().Password)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        public async  Task<bool> ChangePwdAsync(ChangePwdModel operaterModel)
        {
            List<UserPasswordEntity> pwdModel = new List<UserPasswordEntity>();
            var dts = _adminContext.UserPasswordEntity.Where(p => p.UserId == _identityContext.Id).ToList();
            dts.ForEach(p => p.Password = operaterModel.PwdMd5);
            pwdModel.AddRange(dts);
            var num =  await UpdateListAsync<UserPasswordEntity>(pwdModel);
            return num > 0;
        }
    }
}

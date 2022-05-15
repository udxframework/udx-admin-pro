using Udx.Admin.IServices;
using Udx.Admin.Repositorys;

namespace Udx.Admin.Services;

/// <summary>
/// 用户服务
/// </summary>	
public class UserService : AdminService,IUserService
{
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    private readonly IEventPublisher _eventPublisher;
    /// <summary>
    /// 用户服务
    /// </summary>
    /// <param name="user"></param>
    /// <param name="userRepository"></param>
    public UserService(
        IUser user,
        IUserRepository userRepository,
        IEventPublisher eventPublisher
    )
    {
        _user = user;
        _userRepository = userRepository;
        _eventPublisher = eventPublisher;
    }
   

    /// <summary>
    /// 获取登录用户信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<AuthLoginResponse>> GetLoginUserAsync(DataRequest<string> request)
    {
        var id = request.ObjectData;
        var entityDto = await _userRepository.GetAsync<AuthLoginResponse>(id);
        return new DataResponse<AuthLoginResponse>(){
            Successful = true,
            ObjectData= entityDto
        };
    }
    /// <summary>
    /// 根据主键Id获取用户信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<UserGetOutput>> GetAsync(DataRequest<string> request)
    {
        var id = request.ObjectData;
        var entity = await _userRepository.GetAsync(id);
        var data = entity.Mapper<UserGetOutput>();
        
        if (data!=null)
             return DataResponse<UserGetOutput>.Success("成功",data);
        else
            return DataResponse<UserGetOutput>.Fail("没有查询到数据！", data);

    }
   
   /// <summary>
   /// 分页获取用户信息
   /// </summary>
   /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<QueryResponse<IEnumerable<UserListOutput>>>> PageQueryAsync(DataRequest<QueryModel> request)
    {
        var queryRequest = request.ObjectData;
        var result = await _userRepository.PageLinqQueryAsync<UserListOutput>(queryRequest);
        return DataResponse<QueryResponse<IEnumerable<UserListOutput>>>.Success(result);
    }
    /// <summary>
    /// 导出用户信息
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<ExportModel>> ExportAsync(DataRequest<QueryModel> request)
    {
        var host = $"{Request.Scheme}://{Request.Host}";
        var queryModel = request.ObjectData;
        var model = await _userRepository.ExportAsync(queryModel);
        model.Url = $"{host}/Udx/Admin/IFileService/Export/{model.ObjectId}/";
        return DataResponse<ExportModel>.Success(model);
    }
    /// <summary>
    /// 保存操作实体
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<UserEditModel>> request)
    {
        var operaterModel = request.ObjectData;
        var result = await _userRepository.SaveUserAsync(operaterModel) > 0;
        return new DataResponse<bool>()
        {
            Successful = result,
            ObjectData = result,
            Code = operaterModel.Model.Id
        };
    }

    /// <summary>
    /// 批量保存
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<DataResponse<bool>> SaveListAsync(DataRequest<SaveModel<List<UserEditModel>>> request)
    {
        var operaterModel = request.ObjectData;

        var result = await _userRepository.SaveUserListAsync(operaterModel) > 0;
       
        return new DataResponse<bool>()
        {
            Successful = result,
            ObjectData = result,
            Code = "0"
        };
    }
    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async  Task<DataResponse<bool>> ResetPwdAsync(DataRequest<List<UserEditModel>> request)
    {
        var operaterModel = request.ObjectData;
        var result = await _userRepository.ResetPwdAsync(operaterModel) > 0;
        return new DataResponse<bool>()
        {
            Successful = result,
            ObjectData = result,
            Code = "0"
        };
    }


    /// <summary>
    /// 更新密码
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<bool>> ChangePasswordAsync(DataRequest<UserChangePasswordInput> request)
    {
        var input = request.ObjectData;
        if (input.ConfirmPassword != input.NewPassword)
        {
            return DataResponse<bool>.Fail("新密码和确认密码不一致！", false);
        }
        var entity = await _userRepository.GetEntityAsync<UserPasswordEntity>(input.Id);
        var oldPassword = MD5Encryption.Encrypt($"{ input.Id.ToLower()}-udx-{ input.OldPassword}");
        if (oldPassword != entity.Password)
        {
            return DataResponse<bool>.Fail("旧密码不正确！",false);
        }

        input.Password = MD5Encryption.Encrypt($"{ input.Id.ToLower()}-udx-{ input.NewPassword}");

        entity = input.Mapper(entity);
        var result = await _userRepository.UpdateAsync(entity) >0;

        return new DataResponse<bool>()
        {
            Successful = result,
            ObjectData = result,
            Message = "更新密码失败!"
        };
    }

    /// <summary>
    /// 更新密码
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<bool>> ChangePwdAsync(DataRequest<ChangePwdModel> request)
    {
        var operaterModel = request.ObjectData;
        //验证旧密码
        var valid = await _userRepository.ValidPwdAsync(operaterModel);
        if(valid)
        {
            //修改新密码
            var result = await _userRepository.ChangePwdAsync(operaterModel);
         
            await _eventPublisher.PublishAsync(new EventData()
            {
                EventId = EventIds.UserPassword,
                Payload = new
                {
                    Data = operaterModel,
                    Action = this.GetType().FullName,
                },
                User = _user
            });

            return new DataResponse<bool>()
            {
                Successful = result,
                ObjectData = result,
                Message = "更新密码成功!"
            };

        }
        else
        {
            return new DataResponse<bool>()
            {
                Successful = false,
                ObjectData = false,
                Message = "输入旧密码不对!"
            };
        }
  

       
    }

}

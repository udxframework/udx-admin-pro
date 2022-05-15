using Udx.Admin.IServices;
using Udx.Admin.Repositorys;


namespace Udx.Admin.Services
{
    /// <summary>
    /// 
    /// </summary>

    public class AuthService : AdminService, IAuthService
    {
        private readonly IUserToken _userToken;
        private readonly IUser _user;
        private readonly AppConfig _appConfig;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly JwtConfig _jwtConfig;
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userToken"></param>
        /// <param name="user"></param>
        /// <param name="appConfig"></param>
        /// <param name="userRepository"></param>
        /// <param name="logger"></param>
        /// <param name="jwtConfig"></param>
        public AuthService(
            IUserToken userToken,
            IUser user,
            AppConfig appConfig,
            IUserRepository userRepository,
            ILogger<AuthService> logger,JwtConfig jwtConfig, IEventPublisher eventPublisher)
        {
            _userToken = userToken;
            _user = user;
            _appConfig = appConfig;
            _userRepository = userRepository;
            _logger = logger;
            _jwtConfig = jwtConfig;
            _eventPublisher = eventPublisher;
         }
        /// <summary>
        /// 获得token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [NonAction]
        private string GetToken(AuthLoginResponse user)
        {            
            var token = _userToken.Create(new[]
            {
                new Claim(ClaimAttributes.UserId, user.Id),
                new Claim(ClaimAttributes.Name, user.Name??""),
                new Claim(ClaimAttributes.UserName, user.UserName??""),
                new Claim(ClaimAttributes.UserNickName, user.NickName??user.UserName??""),
                new Claim(ClaimAttributes.TenantId, user.TenantId??""),
                new Claim(ClaimAttributes.IsAdmin, user.IsAdmin??""),
                new Claim(ClaimAttributes.Expires, user.Expires.ToString())
            });
            return token;
        }
        /// <summary>
        /// 用户登录
        /// 根据登录信息生成Token
        /// </summary>
        /// <param name="input">登录信息</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<string>> LoginTokenAsync(AuthLoginRequest input)
        {
          
            var res = await LoginAsync(input);
            var  output = DataResponse<string>.Success("登录成功!",null);
            if (res.Successful)
            {
                output.ObjectData = GetToken(res.ObjectData);
                output.Tag = res.ObjectData.Id;
            }
            else {
                output = DataResponse<string>.Fail(res.Message!=null?res.Message:"登录失败!",null);
            }
            return output;

        }

        /// <summary>
        /// 刷新Token
        /// 以旧换新
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public DataResponse<string> Refresh([BindRequired] string token)
        {
            var userClaims = _userToken.Decode(token);
            if (userClaims == null || userClaims.Length == 0)
            {
                return DataResponse<string>.Fail();
            }

            var refreshExpires = userClaims.FirstOrDefault(a => a.Type == ClaimAttributes.RefreshExpires)?.Value;
            if (refreshExpires.IsNull())
            {
                return DataResponse<string>.Fail();
            }

            if (refreshExpires.ToLong() <= DateTime.Now.ToTimestamp())
            {
                return DataResponse<string>.Fail("登录信息已过期");
            }

            var userId = userClaims.FirstOrDefault(a => a.Type == ClaimAttributes.UserId)?.Value;
            if (userId.IsNull())
            {
                return DataResponse<string>.Fail();
            }

            return DataResponse<string>.Success("刷新成功", GetToken(new AuthLoginResponse()));
        }
        /// <summary>
        /// 用户登录
        /// 返回登录信息
        /// </summary>
        /// <param name="input">登录信息</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<AuthLoginResponse>> LoginAsync(AuthLoginRequest input)
        {
            UserEntity user = null;

           
            var result = DataResponse<AuthLoginResponse>.Success("登录成功！");
            user = (await _userRepository.FindAsync(a => a.UserName == input.UserName));
            if (user == null)
            {
                throw new Exception("账户不存在!");
            }
            else
            {
                if (user.Status != "启用") {
                    throw new Exception("账户状态不正常，登录失败!");
                }               
                var u = user.Mapper<AuthLoginResponse>();
                u.TenantId=u.TenantId??"0";
                u.Token = GetToken(u);
                u.Expires = DateTime.Now.AddMinutes(_jwtConfig.Expires);
                
                result.ObjectData = u;
                //用户登录事件记录
                await _eventPublisher.PublishAsync(new EventData()
                {
                    EventId = EventIds.UserLogin,
                    Payload = new
                    {
                        User = user,
                        Host = Request.Headers["Host"].ToString(),
                        Action = this.GetType().FullName
                    },
                    User = _user
                });

            }
            return result;

            
        }
        
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<bool>> RegisterAsync(DataRequest<RegisterModel> request)
        {
            var model = request.ObjectData;
            UserEntity user = null;
            var result = DataResponse<bool>.Success("注册成功！");
            user = (await _userRepository.FindAsync(a => a.UserName == model.UserName));
            if (user != null)
            {
                throw new Exception("账户已存在!");
            }
            else
            {
                user = await _userRepository.RegisterAsync(model);
                //用户注册事件记录
                await _eventPublisher.PublishAsync(new EventData()
                {
                    EventId = EventIds.UserRegister,
                    Payload = new
                    {
                        Model = model,
                        User = user,
                        Action = this.GetType().FullName
                    },
                    User = _user
                });

            }
            return result;
        }
      
      
    }
}

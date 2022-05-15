using Udx.Auth;
using Udx.Models;

namespace Udx.Admin.Models
{ 
    public class LoginResult: DataResponse<AccessTokenResponse>
    {
        public AccessTokenResponse TokenModel { get; set; }
    }
}

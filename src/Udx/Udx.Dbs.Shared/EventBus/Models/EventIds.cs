using System.Collections.Generic;

namespace Udx.EventBus
{
    /// <summary>
    /// 消息总线配置中心
    /// </summary>
    public class EventIds
    {
        #region Admin User相关

        public const string UserLogin = "User:Login";
        public const string UserPassword = "User:Password";
        public const string UserRegister= "User:Register";
        #endregion
        #region Dms 飞书消息推送相关

        public const string FsMessage = "Fs:Message";
        #endregion
    }
}

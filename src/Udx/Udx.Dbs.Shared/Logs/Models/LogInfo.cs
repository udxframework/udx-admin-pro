namespace Udx.Logs.Models
{
    public class LogInfo
    {
        /// <summary>
        /// 日志分类
        /// </summary>
        public  string LogType { get; set; }

        /// <summary>
        /// 日志数据
        /// </summary>
        public  dynamic LogData { get; set; }

        /// <summary>
        /// 日志说明
        /// </summary>
        public  string Description { get; set; }

    }
}

namespace Udx.Dbs.Entities;
    /// <summary>
    /// 事件消息数据表
    /// </summary>
    [Comment("事件数据表")]
    [Table("udx_eventbus_eventdata")]
    [Index(nameof(TenantId), nameof(EventId), Name = "Index_EventData_TenantId")]
    public class EventDataEntity : EntityFull
    {
    /// <summary>
    /// 事件Id
    /// </summary>
    public string EventId { get; set; }
    /// <summary>
    /// 消息事件数据
    /// </summary>
    [Comment("消息事件数据")]
    [StringLength(-1)]
    public string EventData { get; set; }

    /// <summary>
    /// 消息发送时间
    /// </summary>
    public DateTime PublishDate { get; set; } = System.DateTime.UtcNow;

    /// <summary>
    /// 订阅类型
    /// </summary>
    public string SubscriberType { get; set; }
    /// <summary>
    /// 订阅时间
    /// </summary>
    public DateTime SubscriberDate { get; set; }

        public string ResultStatus { get; set; }

        public string ResultMessage { get; set; }

    }

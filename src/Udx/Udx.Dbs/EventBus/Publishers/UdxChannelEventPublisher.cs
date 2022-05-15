namespace Udx.EventBus;

/// <summary>
/// 基于内存通道事件发布者（默认实现）
/// </summary>
internal sealed partial class UdxChannelEventPublisher : BasePublisher,IEventPublisher
{
    /// <summary>
    /// 事件源存储器
    /// </summary>
    private readonly IEventSourceStorer _eventSourceStorer;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventSourceStorer">事件源存储器</param>
    public UdxChannelEventPublisher(IEventSourceStorer eventSourceStorer)
    {
        _eventSourceStorer = eventSourceStorer;
    }

    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventSource">事件源</param>
    /// <returns><see cref="Task"/> 实例</returns>
    public async Task PublishAsync(IEventSource eventData)
    {
        await EventBusHelper.SaveEventDataAsync(eventData);
        await _eventSourceStorer.WriteAsync(eventData, eventData.CancellationToken);       
       
    }

    /// <summary>
    /// 延迟发布一条消息
    /// </summary>
    /// <param name="eventSource">事件源</param>
    /// <param name="delay">延迟数（毫秒）</param>
    /// <returns><see cref="Task"/> 实例</returns>
    public Task PublishDelayAsync(IEventSource eventSource, long delay)
    {
        
        // 创建新线程
        Task.Factory.StartNew(async () =>
        {
            await EventBusHelper.SaveEventDataAsync(eventSource);
            // 延迟 delay 毫秒
            await Task.Delay(TimeSpan.FromMilliseconds(delay), eventSource.CancellationToken);

            await _eventSourceStorer.WriteAsync(eventSource, eventSource.CancellationToken);

        }, eventSource.CancellationToken);
        return Task.CompletedTask;
    }
  

}

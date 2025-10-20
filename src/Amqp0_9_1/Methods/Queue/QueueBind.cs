using System.Buffers;
using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Queue;

internal sealed class QueueBind(
    string queueName,
    string exchangeName,
    string routingKey,
    bool isNoWait = true) : AmqpMethod
{
    internal override ushort ClassId => 50;
    internal override ushort MethodId => 20;

    public ushort Reserved1 {get;} = 0;
    public string QueueName { get; } = queueName;
    public string ExchangeName { get; } = exchangeName;
    public string RoutingKey { get; } = routingKey;
    public bool IsNoWait { get; } = isNoWait;

    //TODO: Add Arguments init
    public Dictionary<string, object> Arguments { get; } = [];

    internal override ReadOnlySpan<byte> GetPayload()
    {
        var buffer = InitiateBuffer();
        buffer.Write(Amqp0_9_1Writer.EncodeShort(Reserved1));
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(QueueName));
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(ExchangeName));
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(RoutingKey));
        
        byte flags = 0;
        if (IsNoWait) flags |= 1 << 0;
        
        buffer.Write([flags]);
        buffer.Write(Amqp0_9_1Writer.EncodeFieldTable(Arguments));
        return buffer.WrittenSpan;
    }
}
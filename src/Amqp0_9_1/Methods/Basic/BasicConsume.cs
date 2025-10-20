using System.Buffers;
using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Basic;

internal sealed class BasicConsume(
    string queueName,
    string? consumerTag = null,
    bool isNoLocal = false,
    bool isNoAck = false,
    bool isExclusive = false,
    bool isNoWait = false) 
    : AmqpMethod
{
    internal override ushort ClassId => 60;
    internal override ushort MethodId => 20;

    public ushort Reserved1 {get;} = 0;
    public string QueueName { get; set; } = queueName;
    public string ConsumerTag { get; set; } = consumerTag ?? string.Empty;
    public bool IsNoLocal { get; set; } = isNoLocal;
    public bool IsNoAck { get; set; } = isNoAck;
    public bool IsExclusive { get; set; } = isExclusive;
    public bool IsNoWait { get; set; } = isNoWait;

    //TODO: Add Arguments init
    public Dictionary<string, object> Arguments { get; set; } = [];


    internal override ReadOnlySpan<byte> GetPayload()
    {
        var buffer = InitiateBuffer();
        buffer.Write(Amqp0_9_1Writer.EncodeShort(Reserved1));
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(QueueName));
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(ConsumerTag));
        
        byte flags = 0;
        if (IsNoLocal) flags |= 1 << 0;
        if (IsNoAck) flags |= 1 << 1;
        if (IsExclusive) flags |= 1 << 2;
        if (IsNoWait) flags |= 1 << 3;
        
        buffer.Write([flags]);
        buffer.Write(Amqp0_9_1Writer.EncodeFieldTable(Arguments));
        return buffer.WrittenSpan;
    }
}

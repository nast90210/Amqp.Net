using System.Buffers;
using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Exchange;

internal sealed class ExchangeDeclare(
    string exchangeName, 
    string exchangeType, 
    bool isPassive = false, 
    bool isDurable = true, 
    bool isAutoDelete = false, 
    bool isInternal = false, 
    bool isNoWait = true) : AmqpMethod
{
    internal override ushort ClassId => 40;
    internal override ushort MethodId => 10;

    public ushort Reserved1 {get;} = 0;
    public string ExchangeName { get; } = exchangeName;
    public string ExchangeType { get; } = exchangeType;
    public bool IsPassive { get; } = isPassive;
    public bool IsDurable { get; } = isDurable;
    public bool IsAutoDelete { get; } = isAutoDelete;
    public bool IsInternal { get; } = isInternal;
    public bool IsNoWait { get; } = isNoWait;
    
    //TODO: Add Arguments init
    public Dictionary<string, object> Arguments { get; } = [];

    internal override ReadOnlySpan<byte> GetPayload()
    {
        var buffer = InitiateBuffer();
        buffer.Write(Amqp0_9_1Writer.EncodeShort(Reserved1));
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(ExchangeName));
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(ExchangeType));
        
        byte flags = 0;
        if (IsPassive) flags |= 1 << 0;
        if (IsDurable) flags |= 1 << 1;
        if (IsAutoDelete) flags |= 1 << 2;
        if (IsInternal) flags |= 1 << 3;
        if (IsNoWait) flags |= 1 << 4;
        
        buffer.Write([flags]);
        buffer.Write(Amqp0_9_1Writer.EncodeFieldTable(Arguments));
        return buffer.WrittenSpan;
    }
}
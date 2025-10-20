using System;
using System.Buffers;
using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Channel;

internal sealed class ChannelOpen : AmqpMethod
{
    internal override ushort ClassId => 20;
    internal override ushort MethodId => 10;

    public string Reserved1 { get; set; } = string.Empty;

    internal override ReadOnlySpan<byte> GetPayload()
    {
        var buffer = InitiateBuffer();
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(Reserved1));
        return buffer.WrittenSpan;
    }
}

using System.Buffers;
using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Connection;

internal sealed class ConnectionOpen(string virtualHost) : AmqpMethod
{
    internal override ushort ClassId => 10;
    internal override ushort MethodId => 40;

    public string VirtualHost { get; set; } = virtualHost;
    public string Reserved1   { get; set; } = string.Empty;
    public bool   Reserved2   { get; set; } = false;

    internal override ReadOnlySpan<byte> GetPayload()
    {
        var buffer = InitiateBuffer();
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(VirtualHost));
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(Reserved1));
        buffer.Write(Amqp0_9_1Writer.EncodeBool(Reserved2));
        return buffer.WrittenSpan;
    }
}

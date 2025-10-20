using System.Buffers;
using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Connection;

internal sealed class ConnectionTuneOk(ushort channelMax, uint frameMax, ushort heartbeat) : AmqpMethod
{
    internal override ushort ClassId => 10;
    internal override ushort MethodId => 31;

    public ushort ChannelMax { get; set; } = channelMax;
    public uint FrameMax { get; set; } = frameMax;
    public ushort Heartbeat { get; set; } = heartbeat;

    internal override ReadOnlySpan<byte> GetPayload()
    {
        var buffer = InitiateBuffer();
        buffer.Write(Amqp0_9_1Writer.EncodeShort(ChannelMax));
        buffer.Write(Amqp0_9_1Writer.EncodeLong(FrameMax));
        buffer.Write(Amqp0_9_1Writer.EncodeShort(Heartbeat));
        return buffer.WrittenSpan;
    }
}

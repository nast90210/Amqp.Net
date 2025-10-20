using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Connection;

internal sealed class ConnectionTune : AmqpMethod
{
    internal override ushort ClassId => 10;
    internal override ushort MethodId => 30;

    public ushort ChannelMax { get; set; }
    public uint   FrameMax   { get; set; }
    public ushort Heartbeat  { get; set; }

    public ConnectionTune(byte[] payload)
    {
        int offset = 0;

        var classId = Amqp0_9_1Reader.DecodeShort(payload, ref offset);
        var methodId = Amqp0_9_1Reader.DecodeShort(payload, ref offset);

        Validate(classId, methodId);

        ChannelMax = Amqp0_9_1Reader.DecodeShort(payload, ref offset);
        FrameMax = Amqp0_9_1Reader.DecodeLong(payload, ref offset);
        Heartbeat= Amqp0_9_1Reader.DecodeShort(payload, ref offset);
    }

    internal override ReadOnlySpan<byte> GetPayload()
    {
        throw new NotImplementedException();
    }
}

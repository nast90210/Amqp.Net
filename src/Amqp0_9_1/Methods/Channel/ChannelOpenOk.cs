using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Channel;

internal sealed class ChannelOpenOk : AmqpMethod
{
    internal override ushort ClassId => 20;
    internal override ushort MethodId => 11;

    public string Reserved1 { get; set; }

    public ChannelOpenOk(byte[] payload)
    {
        int offset = 0;

        var classId = Amqp0_9_1Reader.DecodeShort(payload, ref offset);
        var methodId = Amqp0_9_1Reader.DecodeShort(payload, ref offset);

        Validate(classId, methodId);

        Reserved1 = Amqp0_9_1Reader.DecodeShortStr(payload, ref offset);
    }

    internal override ReadOnlySpan<byte> GetPayload()
    {
        throw new NotImplementedException();
    }
}

using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Basic;

internal sealed class BasicConsumeOk : AmqpMethod
{
    internal override ushort ClassId => 60;
    internal override ushort MethodId => 21; // 60‑21

    public string ConsumerTag { get; set; }

    public BasicConsumeOk(byte[] payload)
    {
        int offset = 0;

        var classId = Amqp0_9_1Reader.DecodeShort(payload, ref offset);
        var methodId = Amqp0_9_1Reader.DecodeShort(payload, ref offset);

        Validate(classId, methodId);

        ConsumerTag = Amqp0_9_1Reader.DecodeShortStr(payload, ref offset);
    }

    internal override ReadOnlySpan<byte> GetPayload()
    {
        throw new NotImplementedException();
    }
}

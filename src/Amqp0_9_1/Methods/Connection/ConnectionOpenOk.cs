using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Connection;

internal sealed class ConnectionOpenOk : AmqpMethod
{
    internal override ushort ClassId => 10;
    internal override ushort MethodId => 41;

    public string KnownHosts { get; set; } = string.Empty;

    public ConnectionOpenOk(byte[] payload)
    {
        int offset = 0;

        var classId = Amqp0_9_1Reader.DecodeShort(payload, ref offset);
        var methodId = Amqp0_9_1Reader.DecodeShort(payload, ref offset);

        Validate(classId, methodId);

        KnownHosts = Amqp0_9_1Reader.DecodeShortStr(payload, ref offset);
    }

    internal override ReadOnlySpan<byte> GetPayload()
    {
        throw new NotImplementedException();
    }
}

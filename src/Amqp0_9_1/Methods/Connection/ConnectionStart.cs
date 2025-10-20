using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Connection;

internal sealed class ConnectionStart : AmqpMethod
{
    internal override ushort ClassId => 10;
    internal override ushort MethodId => 10;

    public ushort VersionMajor { get; set; }
    public ushort VersionMinor { get; set; }
    public Dictionary<string, object> ServerProperties { get; set; } = [];
    public string Mechanisms { get; set; } = string.Empty;
    public string Locales { get; set; } = string.Empty;

    public ConnectionStart(byte[] payload)
    {
        int offset = 0;

        var classId = Amqp0_9_1Reader.DecodeShort(payload, ref offset);
        var methodId = Amqp0_9_1Reader.DecodeShort(payload, ref offset);

        Validate(classId, methodId);
        
        VersionMajor = Amqp0_9_1Reader.DecodeOctet(payload, ref offset);
        VersionMinor = Amqp0_9_1Reader.DecodeOctet(payload, ref offset);
        ServerProperties = Amqp0_9_1Reader.DecodeFieldTable(payload, ref offset);
        Mechanisms = Amqp0_9_1Reader.DecodeLongStr(payload, ref offset);
        Locales = Amqp0_9_1Reader.DecodeLongStr(payload, ref offset);
    }

    internal override ReadOnlySpan<byte> GetPayload()
    {
        throw new NotImplementedException();
    }
}

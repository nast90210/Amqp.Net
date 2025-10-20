using System.Buffers;
using System.Net;
using Amqp.Core.Primitives.SASL;
using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;
using Amqp0_9_1.Methods.Connection.Properties;

namespace Amqp0_9_1.Methods.Connection;

internal sealed class ConnectionStartOk : AmqpMethod
{
    internal override ushort ClassId => 10;
    internal override ushort MethodId => 11;

    public ConnectionStartOkProperties ClientProperties { get; set; } = new();
    public string Mechanism { get; set; }
    public SaslPlainResponse Response { get; set; }
    public string Locale { get; set; }

    internal ConnectionStartOk(
        ConnectionStartOkProperties clientProperties,
        string mechanism,
        SaslPlainResponse response,
        string locale)
    {
        ClientProperties = clientProperties;
        Mechanism = mechanism;
        Response = response;
        Locale = locale;
    }

    internal override ReadOnlySpan<byte> GetPayload()
    {
        var buffer = InitiateBuffer();
        buffer.Write(Amqp0_9_1Writer.EncodeFieldTable(ClientProperties.ToDictionary()));
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(Mechanism));
        buffer.Write(Amqp0_9_1Writer.EncodeLong((uint)Response.Length));
        buffer.Write(Response.AsArray());
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(Locale));
        return buffer.WrittenSpan;
    }
}

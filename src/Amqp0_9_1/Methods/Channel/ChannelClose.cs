using System.Buffers;
using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Encoding;

namespace Amqp0_9_1.Methods.Channel;

internal sealed class ChannelClose(
    ushort replyCode,
    string replyText,
    ushort? exceptionClassId = 0,
    ushort? exceptionMethodId = 0) : AmqpMethod
{
    internal override ushort ClassId => 20;
    internal override ushort MethodId => 50;

    public ushort ReplyCode { get; set; } = replyCode;
    public string ReplyText { get; set; } = replyText;

    public ushort ExceptionClassId { get; set; } = exceptionClassId ?? 0;
    public ushort ExceptionMethodId { get; set; } = exceptionMethodId ?? 0;

    internal override ReadOnlySpan<byte> GetPayload()
    {
        var buffer = InitiateBuffer();
        buffer.Write(Amqp0_9_1Writer.EncodeShort(ReplyCode));
        buffer.Write(Amqp0_9_1Writer.EncodeShortStr(ReplyText));
        buffer.Write(Amqp0_9_1Writer.EncodeShort(ExceptionClassId));
        buffer.Write(Amqp0_9_1Writer.EncodeShort(ExceptionMethodId));
        return buffer.WrittenSpan;
    }
}

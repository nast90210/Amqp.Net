using Amqp0_9_1.Abstractions;
using Amqp0_9_1.Methods.Basic;
using Amqp0_9_1.Methods.Channel;
using Amqp0_9_1.Methods.Connection;

namespace Amqp0_9_1.Methods;

internal static class MethodFactory
{
    public static AmqpMethod Create(ushort classId, ushort methodId, byte[] payload)
    {
        switch (classId)
        {
            case 10:
                switch (methodId)
                {
                    case 10:
                        return new ConnectionStart(payload);
                    case 30:
                        return new ConnectionTune(payload);
                    case 41:
                        return new ConnectionOpenOk(payload);
                }
                break;
            case 20:
                switch (methodId)
                {
                    case 11:
                        return new ChannelOpenOk(payload);
                }
                break;
            case 60:
                switch(methodId)
                {
                    case 21:
                        return new BasicConsumeOk(payload);
                }
                break;
            default:
                throw new NotSupportedException($"Unknown class-id {classId}.");
        }
        
        throw new NotSupportedException($"Unknown method-id {methodId} for class-id {classId}.");
    }
}

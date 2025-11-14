using System.Globalization;

namespace Amqp0_9_1.Clients.Options;

public sealed class AmqpConnectionOptions
{
    public ushort ChannelMax { get; set; } = ushort.MaxValue;
    public uint FrameMax { get; set; } = 131072;
    public ushort Heartbeat { get; set; } = 60;
    public TimeSpan ConnectTimeout { get; set; } = TimeSpan.FromSeconds(30);
    public string Locale { get; set; } = CultureInfo.CurrentCulture.Name;
}
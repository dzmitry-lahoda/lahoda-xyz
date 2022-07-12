#nullable enable
using System;
using System.Collections.Generic;
using MessagePack;

[MessagePackObject]
public class ClientCommand
{
    [Key(0)]
    public uint ClientTick;
    [Key(1)]
    public uint LastObservedServerTick;
    [Key(2)]
    public IEnumerable<uint> Flashes;
}

[MessagePackObject]
public class ServerCommand
{
    [Key(0)]
    public uint ServerTick;
    [Key(1)]
    public uint LastObservedClientTick;

    [Key(2)]
    public IEnumerable<uint> Flashes;
}

public class Peer
{
    public const ushort TickMilliseconds = 30;
    public uint Time;

    public const uint StartTick = 10;

    public const ushort RttCap =(ushort) StartTick * TickMilliseconds;

    public uint LocalTimeMilliseconds => (uint)Environment.TickCount;
}
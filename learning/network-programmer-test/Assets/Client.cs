#nullable enable
using System.Collections.Generic;
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Linq;
using MessagePack;
using UnityEngine;
using System.Text;

class ClientStateMachine
{
    private ClientStateMachine() { }
    Gabsee.NetManager peer;

    public sealed class Initial : ClientStateMachine
    {
        public IPEndPoint settings;
        public static ClientStateMachine New(IPEndPoint ep)
        {
            var peer = new Gabsee.NetManager();
            return new Initial { peer = peer, settings = ep };
        }

        public ClientStateMachine Update()
        {
            var started = Started.New(this);
            peer.StartClient($"{this.settings.Address}", this.settings.Port);
            Debug.Log($"Started");
            return started;
        }
    }

    public sealed class Started : ClientStateMachine
    {
        public IPEndPoint? connected;
        public static ClientStateMachine New(Initial before)
        {
            var self = new Started { peer = before.peer };
            self.peer.OnConnection = (peer) =>
            {
                self.connected = peer;
            };
            return self;
        }


        public ClientStateMachine Update()
        {
            if (connected != null)
            {
                Debug.Log("Connected");
                return Initialized.New(this);
            }
            peer.Update();
            return this;
        }

        public override string ToString() => $"Started({peer}; {connected})";
    }

    sealed public class Initialized : ClientStateMachine
    {
        public IPEndPoint? connected;
        public uint? LastObservedServerTick;

        public static ClientStateMachine New(Started before)
        {
            var self = new Initialized { peer = before.peer, connected = before.connected };
            self.peer.OnNetworkReceive = (ep, data) =>
            {
                var command = MessagePackSerializer.Deserialize<ServerCommand>(data);
                self.LastObservedServerTick = command.ServerTick;
            };
            return self;
        }

        public ClientStateMachine Update()
        {
            peer.Update();
            if (LastObservedServerTick != null)
            {
                return Running.New(this);
            }
            return this;
        }
    }

    sealed public class Running : ClientStateMachine
    {
        public uint Time;
        public uint CurrentTick;
        public uint LastObservedServerTick;
        public ushort Rtt = Peer.RttCap;

        public IPEndPoint connected;

        public HashSet<uint> commands = new HashSet<uint>();

        public SortedDictionary<uint, bool> flashes = new SortedDictionary<uint, bool>();
        public SortedDictionary<uint, uint> ticks = new SortedDictionary<uint, uint>();

        public static ClientStateMachine New(Initialized before)
        {
            var self = new Running { peer = before.peer, connected = before.connected!, LastObservedServerTick = before.LastObservedServerTick.Value, CurrentTick = before.LastObservedServerTick.Value };
            self.peer.OnNetworkReceive = (ep, data) =>
            {
                Debug.Log("Client received data");
                var command = MessagePackSerializer.Deserialize<ServerCommand>(data);
                foreach (var flash in command.Flashes)
                {
                    Debug.Log("Got flash on Client");
                    if (!self.flashes.ContainsKey(flash))
                        self.flashes[flash] = default;
                }
                self.LastObservedServerTick = command.ServerTick;
                if (self.ticks.ContainsKey(command.LastObservedClientTick))
                {
                    var sent = self.ticks[command.LastObservedClientTick];
                    var rtt = (uint)Environment.TickCount - sent;
                    self.Rtt = (ushort)Math.Min((ushort)rtt, Peer.RttCap);
                }

            };
            return self;
        }

        public ClientStateMachine Update(ushort millisecondsInTick, uint now)
        {
            peer.Update();
            if (Time + millisecondsInTick <= now)
            {
                Time = now;
                var future = Rtt / millisecondsInTick;
                while (CurrentTick < LastObservedServerTick + future)
                {
                    CurrentTick += 1;
                    commands = new HashSet<uint>(commands.Where(x => x > LastObservedServerTick - 10).ToArray());
                    var command = new ClientCommand { ClientTick = CurrentTick, LastObservedServerTick = LastObservedServerTick, Flashes = commands };
                    var data = MessagePackSerializer.Serialize(command);
                    ticks[CurrentTick] = now;
                    peer.Send(connected, data);
                }
                var results = flashes.Where(x => x.Key + 10 <= CurrentTick).ToArray();
                foreach (var r in results)
                {
                    flashes.Remove(r.Key);
                }
                ticks = new SortedDictionary<uint, uint>(ticks.Where(x => x.Key + 10 > CurrentTick).ToDictionary(x => x.Key, x => x.Value));
            }
            return this;
        }

        public void Input(ushort millisecondsInTick, ushort inMilliseconds)
        {
            commands.Add(CurrentTick + (uint)inMilliseconds / millisecondsInTick);
        }

        public bool Render(ushort millisecondsInTick)
        {
            var results = flashes.Where(x => x.Key <= CurrentTick && x.Key >= CurrentTick - (Rtt / millisecondsInTick + 1) && x.Value == false).ToArray();
            foreach (var result in results)
            {
                Debug.Log("Inacting flash on client");
                flashes[result.Key] = true;
            }
            return results.Any();
        }

        public string ToDebugString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("CurrentTick: " + CurrentTick + "\n");
            sb.AppendLine("LastObservedServerTick:" + LastObservedServerTick + "\n");
            sb.AppendLine("Rtt:" + Rtt + "\n");
            sb.AppendLine("Flashes:" + flashes.Count() + "\n");

            return sb.ToString();
        }
    }
}

class ClientPeer : Peer
{
    public ClientStateMachine state;

    public void Start(IPEndPoint ep)
    {
        state = ClientStateMachine.Initial.New(ep);
    }

    public bool Update(ushort? inMilliseconds)
    {
        switch (state)
        {
            case ClientStateMachine.Initial initial:
                state = initial.Update();
                break;
            case ClientStateMachine.Started started:
                state = started.Update();
                break;
            case ClientStateMachine.Initialized initialized:
                state = initialized.Update();
                break;
            case ClientStateMachine.Running running:
                if (inMilliseconds.HasValue)
                    running.Input(Peer.TickMilliseconds, inMilliseconds.Value);
                state = running.Update(Peer.TickMilliseconds, this.LocalTimeMilliseconds);
                return running.Render(Peer.TickMilliseconds);
        }

        return false;
    }

    public string ToDebugString() => state is ClientStateMachine.Running running ? running.ToDebugString() : state.ToString();
}
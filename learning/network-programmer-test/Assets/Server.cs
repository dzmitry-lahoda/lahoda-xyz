#nullable enable
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using MessagePack;

public class Server : Peer
{
    public Gabsee.NetManager peer = new Gabsee.NetManager();

    public uint CurrentTick = StartTick;

    public HashSet<uint> Flashes = new HashSet<uint>();

    public Dictionary<IPEndPoint, uint> clients = new Dictionary<IPEndPoint, uint>();

    public void Start(ushort port)
    {
        peer.OnNetworkReceive = OnNetworkReceive;
        peer.OnConnection = OnConnection;
        peer.StartServer(port);
        Time = LocalTimeMilliseconds;
    }

    public void OnConnection(IPEndPoint endpoint)
    {
        Debug.Log($"Server got client connected to {endpoint}");
        clients[endpoint] = 0;
    }

    public void OnNetworkReceive(IPEndPoint endpoint, byte[] data)
    {
        var command = MessagePackSerializer.Deserialize<ClientCommand>(data, MessagePack.Resolvers.ContractlessStandardResolver.Options);
        clients[endpoint] = command.ClientTick;
        foreach (var flash in command.Flashes)
        {
            if (flash > CurrentTick - StartTick)
            {
                Debug.Log("Put flash into server");
                Flashes.Add(flash);
            }
        }
    }

    public void Update()
    {
        peer.Update();
        var now = LocalTimeMilliseconds;
        if (Time + TickMilliseconds < now)
        {
            Debug.Log("Server tick");
            Time = now;
            CurrentTick += 1;
            Flashes = new HashSet<uint>(Flashes.Where(x => x > CurrentTick - StartTick).ToArray());
            foreach (var client in clients)
            {
                Debug.Log("Server sends to: " + client.Key);
                var data = MessagePackSerializer.Serialize(new ServerCommand
                {
                    Flashes = Flashes,
                    ServerTick = CurrentTick,
                    LastObservedClientTick = client.Value,
                });
                peer.Send(client.Key, data);
            }

        }
    }
}
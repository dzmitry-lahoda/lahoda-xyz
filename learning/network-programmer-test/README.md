# Network programmer test

<p align="center">
    <img src="https://images.squarespace-cdn.com/content/v1/5f2408dcdc63bd117de38332/1601898431269-C1QS9C0NJ78RQI7Z48FB/GabseeLogo2.png)" />
</p>
<p align="center">
    [ <a href="https://www.gabsee.com/">Gabsee</a> ]
    [ <a href="https://www.storiesone.com/">Stories One</a> ]
</p>

## Context

Gabsee is looking for a talented network programmer who takes ownership of players' online experience. You care about providing our players with the ultimate multiplayer experience they deserve.

For **Stories One**, we developed a networking stack to run the game on multiple interconnected authoritative game servers, serving up to a hundred concurrent users synchronized in real time. Our framework is integrated into Unity, based on ECS and combines the usage of different open source libraries (Entitas, LiteNetLib, MessagePack).

This is a solid foundation for the project but now we’re looking for someone who will help us to go to the next step for our ambitions, to deliver a smooth game and social experience to millions of players.

## Objective

The goal of this test is to provide a solution to synchronize several clients and “start something” at the exact same time.

Compute latency between clients and server with your own logic and network protocol. Use it to synchronize the execution of an action on all connected clients. This action can be just a flash on clients' screen. The most important thing is to have all clients running this flash at the same time.

## Constraints

You **can't** use external library or package. Only Unity 2020.3.21f1 and `Gabsee.NetManager` present in Scripts folder.

@dzmitry-lahoda: Not sure if that was supposed, but I had to use something for serde, used MessagPack because rolling own serde within 5 hours is unrealistic:)

In Package directory, you can see LiteNetLib. It's a dependency for `Gabsee.NetManager`. Even if this library is already include in the project, dont use it directly.

## Gabsee.NetManager

You can see all methods and comments directly in `Assets/Scripts/Gabsee/NetManager.cs` file.

Short example for client and server.

```cs
using UnityEngine;
using Gabsee;

public class Client : MonoBehaviour
{
    private NetManager m_netManager = new NetManager();

    private void Start()
    {
        m_netManager.OnConnection = (endPoint) =>
        {
            Debug.Log($"New connection with {endPoint}");
            byte[] data = /* data for the server */;
            m_netManager.Send(data);
        };

        m_netManager.StartClient("localhost", 1234);
    }

    private void Update()
    {
        m_netManager.Update();
    }
}
```

```cs
using UnityEngine;
using Gabsee;

public class Server : MonoBehaviour
{
    private NetManager m_netManager = new NetManager();

    private void Start()
    {
        m_netManager.OnConnection = (endPoint) =>
        {
            Debug.Log($"New client {endPoint}");
        };

        m_netManager.OnDisconnection = (endPoint) =>
        {
            Debug.Log($"Connection closed {endPoint}");
        };

        m_netManager.OnNetworkReceive = (endPoint, data) =>
        {
            Debug.Log($"Received {data.Length} bytes from {endPoint}");
            byte[] response = /* data for the client */;
            m_netManager.Send(endPoint, response);
        };

        m_netManager.StartServer(1234);
    }

    private void Update()
    {
        m_netManager.Update();
    }
}
```

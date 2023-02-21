#nullable enable
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class ClientServerManager : MonoBehaviour
{
    public Text Client1StateView;
    public Text Client2StateView;

    public InputField Client1Input;
    public InputField Client2Input;

    public Text ServerStateView;

    Server? server;
    ClientPeer? client1;
    ClientPeer? client2;

    public uint flash1Counter;
    public uint flash2Counter;

    public void StartServer()
    {
        server = new Server();
        server?.Start(42123);
    }

    public void StartClient1()
    {
        client1 = new ClientPeer();
        client1?.Start(new IPEndPoint(IPAddress.Loopback, 42123));
        Debug.Log("Client1 started");
    }

    public void StartClient2()
    {
        client2 = new ClientPeer();
        client2?.Start(new IPEndPoint(IPAddress.Loopback, 42123));
        Debug.Log("Client2 started");
    }

    ushort? flash1;

    public void FlashClient1()
    {
        flash1 = ushort.Parse(Client1Input.text);
    }

    ushort? flash2;

    public void FlashClient2()
    {
        flash2 = ushort.Parse(Client2Input.text);
    }

    void Start()
    {
        Client1StateView = GameObject.Find("ClientText1").GetComponent<Text>();
        Client2StateView = GameObject.Find("ClientText2").GetComponent<Text>();
    }

    void Update()
    {
        server?.Update();

        var render2 = client2?.Update(flash2);
        flash2 = null;
        if (render2.HasValue && render2.Value == true)
        {
            flash2Counter += 1;
            Debug.Log("Flash on client 2");
            GameObject.Find("ClientText2").GetComponent<Text>().text = flash2Counter.ToString();
        }

        var render1 = client1?.Update(flash1);
        flash1 = null;
        if (render1.HasValue && render1.Value == true)
        {
            flash1Counter += 1;
            Debug.Log("Flash on client 1");
            GameObject.Find("ClientText1").GetComponent<Text>().text = flash1Counter.ToString();
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(800, 100, 200, 300), client1?.ToDebugString());
        GUI.Label(new Rect(400, 100, 200, 300), client2?.ToDebugString());
    }
}

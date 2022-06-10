using System;
using System.Net;
using System.Net.Sockets;
namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            var tcp = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 30011));
            tcp.Start();
            Console.WriteLine("Waiting for connection");
            var socket = tcp.AcceptSocket();
            Console.WriteLine("Connected on " + socket.RemoteEndPoint);
            Console.WriteLine("Press any key to stop");
            Console.ReadKey();            
        }
    }
}

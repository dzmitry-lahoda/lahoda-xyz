using System;
using System.Net;
using System.Net.Sockets;
namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            var tcp = new TcpClient();
            tcp.Connect(IPAddress.Parse("127.0.0.1"), 30011);// put 30012 to tunnel
            Console.WriteLine(tcp.Connected);

            Console.WriteLine("Press any key to stop");
            Console.ReadKey();            
        }
    }
}

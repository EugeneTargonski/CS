using System;
using System.IO;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string localAddr = "10.10.10.10";
            int port = 8888;

            var client = new TcpClient(localAddr, port);

            string request = Console.ReadLine();

            using (var bw = new BinaryWriter(client.GetStream()))
                using (var br = new BinaryReader(client.GetStream()))
                {
                    bw.Write(request);
                    switch (request)
                    {
                        case "stop":
                            Console.WriteLine("Server stopped");
                            break;
                        default:
                            Console.WriteLine(br.ReadString());
                            break;
                    }
                }

            Console.ReadLine();
        }
    }
}

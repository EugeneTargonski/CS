using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace SimpleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string localAddr = "10.10.222.222";
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

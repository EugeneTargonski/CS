using System;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace SimpleServer
{
    class Program
    {
        static void Main(string[] args)
        {

            int port = 8888;

            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            bool stop = false;
            while (!stop)
            {
                var client = listener.AcceptTcpClient();
                using (var br = new BinaryReader(client.GetStream()))
                using (var bw = new BinaryWriter(client.GetStream()))
                {
                    var request = br.ReadString();
                    Console.WriteLine(request);

                    switch (request)
                    {
                        case "stop":
                            stop = true;
                            break;
                        default:
                            bw.Write("Привет " + request);
                            break;
                    }

                }
            }
        }
    }
}

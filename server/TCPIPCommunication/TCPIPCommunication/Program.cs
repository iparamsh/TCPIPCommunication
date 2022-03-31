using System;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace TCPIPCommunication
{
    class Program   //server side
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 8080);
            listener.Start();
            while (true) 
            {
                Console.WriteLine("waiting for a connection");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("client accepted");
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());
                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    //real bytes counter
                    int recv = 0;
                    foreach (byte b in buffer) 
                    {
                        if (b != 0)
                            recv++;
                    }
                    string request = Encoding.UTF8.GetString(buffer, 0, recv);
                    Console.WriteLine("request received: " + request);
                    writer.WriteLine("daaaam boy, you did it");
                    writer.Flush(); //sending the message to client

                }
                catch(Exception e)
                {
                    Console.WriteLine("something crashed...");
                    writer.WriteLine(e.ToString());
                }
            }
        }
    }
}

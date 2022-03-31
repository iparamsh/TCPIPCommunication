using System;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace TCPIPCommunicationClient
{
    class Program   //client side
    {
        static void Main(string[] args)
        {
        connection:
            try
            {
                //sending data
                TcpClient client = new TcpClient("127.0.0.1", 8080);
                string message = "Hi, I am client. U hear me?";

                byte[] sendData = new byte[Encoding.ASCII.GetByteCount(message) + 1];
                sendData = Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
                Console.WriteLine("sending data to server...");

                //receiving data from server

                StreamReader reader = new StreamReader(stream);
                Console.WriteLine(reader.ReadLine());

                stream.Close();
                client.Close();

                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("failed to connect...");
                goto connection;
            }
        }
    }
}

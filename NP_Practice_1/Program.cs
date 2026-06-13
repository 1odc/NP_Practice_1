using System.Net;
using System.Net.Sockets;
using System.Text;

Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try
{
    IPAddress localIP = IPAddress.Parse("127.0.0.1");
    IPEndPoint serverEP = new IPEndPoint(localIP, 10000);
    clientSocket.Connect(serverEP);
    Console.WriteLine("What do you want to request? (time/date): ");
    string choice = Console.ReadLine().ToLower();

    byte[] buff = Encoding.UTF8.GetBytes(choice);
    clientSocket.Send(buff);
    buff = new byte[1024];
    int len = clientSocket.Receive(buff);
    string receivedMessage = Encoding.UTF8.GetString(buff, 0, len);
    Console.WriteLine($"Response from server: {receivedMessage}");
}
finally
{
    clientSocket.Shutdown(SocketShutdown.Both);
    clientSocket.Close();
}
void Example1()
{
    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    try
    {
        IPAddress localIP = IPAddress.Parse("127.0.0.1");
        IPEndPoint serverEP = new IPEndPoint(localIP, 10000);
        clientSocket.Connect(serverEP);
        string message = "Hello, server!";
        byte[] buff = Encoding.UTF8.GetBytes(message);
        clientSocket.Send(buff);
        buff = new byte[4096];
        int len = clientSocket.Receive(buff);
        string receivedMessage = Encoding.UTF8.GetString(buff, 0, len);
        Console.WriteLine($"Response from server: {receivedMessage}");
    }
    finally
    {
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
}
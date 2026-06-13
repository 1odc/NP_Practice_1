using System.Net;
using System.Net.Sockets;
using System.Text;

Socket socket = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);
socket.Bind(new IPEndPoint(IPAddress.Any, 10000));
socket.Listen(10);
Console.WriteLine("Server starts listening on port: 10000");
while (true)
{
    Socket clientSocket = socket.Accept();
    byte[] buff = new byte[1024];
    int len = clientSocket.Receive(buff);
    string request = Encoding.UTF8.GetString(buff, 0, len).ToLower();
    string response;

    if (request == "time")
        response = DateTime.Now.ToLongTimeString();
    else if (request == "date")
        response = DateTime.Now.ToLongDateString();
    else
        response = "Wrong request";
    buff = Encoding.UTF8.GetBytes(response);
    clientSocket.Send(buff);
    Console.WriteLine($"Client requested {request}");
    clientSocket.Shutdown(SocketShutdown.Both);
    clientSocket.Close();
}

void Example1()
{
    Socket socket = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);
    socket.Bind(new IPEndPoint(IPAddress.Any, 10000));
    socket.Listen(10);
    Console.WriteLine("Server starts listening on port: 10000");
    while (true)
    {
        Socket clientSocket = socket.Accept();
        EndPoint? remoteEP = clientSocket.RemoteEndPoint;
        if (remoteEP is not null && remoteEP is IPEndPoint remoteIPEndPoint)
        {
            Console.WriteLine($"Client connected: {remoteIPEndPoint.Address}:{remoteIPEndPoint.Port}");
        }
        byte[] buff = new byte[4096];
        int len = clientSocket.Receive(buff);
        string receivedMessage = Encoding.UTF8.GetString(buff, 0, len);
        Console.WriteLine($"At {DateTime.Now.ToLongTimeString()} Received message from client: {receivedMessage}");
        string respondMessage = "Hello client";
        buff = Encoding.UTF8.GetBytes($"{respondMessage}. Time: {DateTime.Now.ToLongTimeString()}");
        clientSocket.Send(buff);
        Console.WriteLine("Respond sent!");
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
}
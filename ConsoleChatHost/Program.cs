// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;


//complete server initialization
var endpoint = new IPEndPoint(IPAddress.Any, 10005);
TcpListener server = new TcpListener(endpoint);
server.Start();
Console.WriteLine("Server listening on port 6969...");

//to get stream we first need to have client
TcpClient client = server.AcceptTcpClient();
Console.WriteLine("Client connected");

//get data stream to send and receive data
var stream = client.GetStream();
Console.WriteLine("Got stream...");

var writer = new StreamWriter(stream);
var reader = new StreamReader(stream);

// to read and write we have to have two thread one for receiving other for writing
// because we want to input data from console we will uset current thread as writer thread and than we will create a new thread for receiving or reading data from stream

var receiverThread = new Thread(ReceiverMethod);
receiverThread.Start();

while (true)
{
    string text = Console.ReadLine();
    writer.WriteLine(text);
    // it is so important to flush the data from the writer 
    writer.Flush();
    if (text == "exit")
    {
        break;
    }
}

writer.Close();
server.Stop();


void ReceiverMethod()
{
    while (true)
    {
        var data = reader.ReadLine();
        if (data != null)
        {
            Console.WriteLine(data);
            if (data == "exit")
            {
                break;
            }
        }
    }
    
    reader.Close();
}
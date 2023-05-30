using System.Net.Sockets;

namespace TCPClient;
class Program
{
    static async Task Main(string[] args)
    {
        if (args is null)
        {
            Console.WriteLine("ERROR at Args !!");
        }

        Console.WriteLine("CLIENT Started.. OK..");

        //metot1
        //await ConnectAsTcpClient("127.0.0.1", 6666);

        //metot2
        ClientDemo client = new ClientDemo("127.0.0.1", 6666);
    }


        //metot1
        static async Task ConnectAsTcpClient(string ip, int port)
        {
        //private static readonly string ClientRequestString = "Hello Mr. Server";
            string ClientRequestString = "Hello Mr. Server";

            for (; ; )
            {
                try
                {
                    await Task.Delay(millisecondsDelay: 1500);
                    using (var tcpClient = new TcpClient())
                    {
                        Console.WriteLine("[Client] Attempting connection to server " + ip + ":" + port);
                        Task connectTask = tcpClient.ConnectAsync(ip, port);
                        Task timeoutTask = Task.Delay(millisecondsDelay: 6100);
                        if (await Task.WhenAny(connectTask, timeoutTask) == timeoutTask)
                        {
                            throw new TimeoutException();
                        }


                        Console.WriteLine("[Client] Connected to server");
                        using (var networkStream = tcpClient.GetStream())
                        using (var reader = new StreamReader(networkStream))
                        using (var writer = new StreamWriter(networkStream) { AutoFlush = true })
                        {
                            Console.WriteLine(string.Format("[Client] Writing request '{0}'", ClientRequestString));
                            await writer.WriteLineAsync(ClientRequestString);

                        // ### not good, server can send a event, at moment is closing the connection and reconnect.
                        // ### I need a event, if new data available
                        // ### How can I do it?
                            try
                            {
                                while (true)
                                {
                                    var response = await reader.ReadLineAsync();
                                    if (response == null)
                                    {
                                        break;
                                    }
                                    Console.WriteLine(string.Format("[Client] Server response was '{0}'", response));
                                }
                                Console.WriteLine("[Client] Server disconnected");
                            }
                            catch (IOException)
                            {
                                Console.WriteLine("[Client] Server disconnected");
                            }
                        }
                    }
                }
                catch (TimeoutException)
                {
                    // reconnect
                    Console.WriteLine("[Client] Timeout - No connection");
                }
            }
        }
}

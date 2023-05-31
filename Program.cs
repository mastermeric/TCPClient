using System.Security.AccessControl;
using System.Net.Sockets;
using System.Text;

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
        //ClientDemo client = new ClientDemo("127.0.0.1", 6666);


        //metot2


        for (int i = 0; i < 5; i++)
        {
            Task.Run(() => {
                ClientDemo client = new ClientDemo("127.0.0.1", 6666);
            });
            /*
            ClientDemo client = new ClientDemo("127.0.0.1", 6666);
            Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
            t.Start(client);
            */
        }
        Console.ReadLine();
    }

    private static StreamReader _sReader;
    private static StreamWriter _sWriter;
    private static void HandleClient(object? obj) {
        TcpClient _client = (TcpClient)obj;

        _client = new TcpClient();
        _client.ConnectAsync("127.0.0.1", 6666);

        _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
        _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

        String sData = "";

        try
        {
            while (true)
            {
                Console.Write("> ");
                sData = Console.ReadLine();

                //====================  Send Data To  Server  ============================
                //Yontem1 :
                // write data and make sure to flush, or the buffer will continue to
                // grow, and your data might not be sent when you want it, and will only be sent once the buffer is filled.
                _sWriter.WriteLine(sData);
                _sWriter.Flush();

                //Yontem2 :
                // NetworkStream stream = _client.GetStream();
                // Byte[] data = System.Text.Encoding.ASCII.GetBytes(sData);
                // stream.Write(data, 0, data.Length);
                //========================================================================


                //====================  Receive Data From Server  ============================
                //Yontem1 :
                //String sDataIncomming = _sReader.ReadLine();

                //Yontem2 :
                // Byte[] data = new Byte[256];
                // NetworkStream stream = _client.GetStream();
                // Int32 bytes = stream.Read(data, 0, data.Length);
                // string response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //============================================================================
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: Cannot Connect to Server !");
        }
    }
}

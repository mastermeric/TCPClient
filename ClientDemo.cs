using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
   class ClientDemo { private TcpClient _client;

private StreamReader _sReader;
private StreamWriter _sWriter;

public ClientDemo(String ipAddress, int portNum)
{

    //MericY : Connect ASYNC ??
    _client = new TcpClient();
    _client.ConnectAsync(ipAddress, portNum);

    HandleCommunication();
}

private void HandleCommunication()
{
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
} }
}
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

        //ClientDemo client = new ClientDemo("127.0.0.1", 6666);
        //ClientDemo client = new ClientDemo("37.148.212.45", 6666);

        //metot2
        for (int i = 0; i < 5; i++)
        {
            var res = Task.Run(() => {
                ClientDemo client = new ClientDemo("127.0.0.1", 6666);
            });
        }

        Console.ReadLine();
    }
}

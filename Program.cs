using NetworkSourceSimulator;
using System.Text.Json;

namespace FlightRadar
{

    internal class Program
    {
        static bool _exitCommand = false;

        static Mutex dataMutex = new Mutex();

        static void Main(string[] args)
        {

            Data data1 = new Data();
            NetworkSourceSimulator.NetworkSourceSimulator server = new NetworkSourceSimulator.NetworkSourceSimulator("example_data.ftr", 10, 50);
            Thread serverThread = new Thread(() => ServerWork(server));

            Generator generator = new Generator(dataMutex);
            MessageHandler messageHandler = new MessageHandler(data1, server, generator);
            server.OnNewDataReady += messageHandler.HandleNewDataReady;

            Thread terminalThread = new Thread(() =>  TerminalWork(data1) );

            serverThread.Start();
            terminalThread.Start();

            while (!_exitCommand) { }

            serverThread.Interrupt();

            data1.WriteToJson("DataInJson.json");      
            

        }

        static void ServerWork(NetworkSourceSimulator.NetworkSourceSimulator server)
        {
            try
            {
                server.Run();
                Console.WriteLine("Server: all data has been send");
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Server was interapted");
            }                    
            _exitCommand = true;
        }

        static void TerminalWork(Data data)
        {
            while (!_exitCommand)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    _exitCommand = true;
                }

                if (input.ToLower() == "print")
                {
                    dataMutex.WaitOne();
                    DateTime dateTime = DateTime.Now;
                    string fileName = "snapshot_" + dateTime.Hour.ToString() + "_" + dateTime.Minute.ToString() +
                        "_" + dateTime.Second.ToString() + ".json";
                    data.WriteToJson(fileName);
                    dataMutex.ReleaseMutex();
                }
            }
        }

        
    }
}

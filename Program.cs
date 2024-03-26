using FlightRadar.Sources_and_storages;
using NetworkSourceSimulator;
using System.Text.Json;

namespace FlightRadar
{

    internal class Program
    {
        static bool exitCommand = false;
        static bool serverEnd = false;

        static Mutex dataMutex = new Mutex();

        static void Main(string[] args)
        {
            
            Data data1 = new Data();
            Generator generator = new Generator(dataMutex);
            Reader myReader = new Reader("example_data.ftr", generator);
            myReader.ReadAll(out data1);
            serverEnd = true;

            Thread terminalThread = new Thread(() =>  TerminalWork(data1) );

            Thread GUIThread = new Thread(() => GraphicalWork());

            terminalThread.Start();
            GUIThread.Start();

            while ((!exitCommand) && (!serverEnd)) { }

            if (exitCommand)
            {
                GUIThread.Interrupt();
                Console.WriteLine("Application interapted");
                return;
            }

            FlightsGUIData GUIData = new FlightsGUIData();
            DataToFlightGUITransformer transformer = new DataToFlightGUITransformer(data1.FlightList, GUIData, data1.AirportList);
            transformer.DataToGUIDataFirstTime();           
            
            while (!exitCommand)
            {
                Thread.Sleep(1000);
                transformer.DataToGUIData();
                FlightTrackerGUI.Runner.UpdateGUI(GUIData);              
            }
            GUIThread.Interrupt();

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
            serverEnd = true;
        }

        static void TerminalWork(Data data)
        {
            while (!exitCommand)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    serverEnd = true;
                    exitCommand = true;
                    
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
                if (input.ToLower() == "sotp")
                {
                    serverEnd = true;
                }
            }
        }

        static void GraphicalWork()
        {
            try
            {
                FlightTrackerGUI.Runner.Run();
            }
            catch(ThreadInterruptedException)
            {
                Console.WriteLine("GUI: End work");
            }
        }

        
    }
}

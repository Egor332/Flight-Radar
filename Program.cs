using FlightRadar.Entities.Classes.Media;
using FlightRadar.Interfaces;
using FlightRadar.Sources_and_storages;
using FlightRadar.Sources_and_storages.Storages;
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
            MediaData defaultMedia = new MediaData();
            Generator generator = new Generator(dataMutex);

            // use reading from ftr file
            Reader myReader = new Reader("example_data.ftr", generator);
            myReader.ReadAll(out data1);
            serverEnd = true;

            Thread terminalThread = new Thread(() =>  TerminalWork(data1, defaultMedia) );

            Thread GUIThread = new Thread(() => GraphicalWork());

            terminalThread.Start();
            GUIThread.Start();

            while ((!exitCommand) && (!serverEnd)) { }

            if (exitCommand)
            {
                GUIThread.Interrupt();
                Console.WriteLine("Application interrupted");
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

        static void TerminalWork(Data data, MediaData? mediaData)
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
                if (input.ToLower() == "report")
                {
                    List<IReportable> objectsToReport = new List<IReportable>();
                    objectsToReport.AddRange(data.PassengerPlaneList);
                    objectsToReport.AddRange(data.CargoPlaneList);
                    objectsToReport.AddRange(data.AirportList);
                    NewsGenerator newsGenerator = new NewsGenerator(objectsToReport, mediaData.ListOfMedia);
                    string? reportString;
                    while ((reportString = newsGenerator.GenerateNextNews()) != null)
                    {
                        Console.WriteLine(reportString);
                    }
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

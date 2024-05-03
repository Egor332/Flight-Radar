using FlightRadar.Entities.Classes.Media;
using FlightRadar.Interfaces;
using FlightRadar.Sources_and_storages;
using FlightRadar.Sources_and_storages.Information_getters;
using FlightRadar.Sources_and_storages.Storages;
using NetworkSourceSimulator;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            ChangeLogger changeLogger = new ChangeLogger(new StreamWriter("log_from_" + DateTime.Now.ToString("d_MM"), true));

            Thread terminalThread = new Thread(() => TerminalWork(data1, defaultMedia));

            Thread GUIThread = new Thread(() => GraphicalWork());

            // use reading from ftr file
            UseReadingFormFile(out data1, generator);
                       
            terminalThread.Start();
            GUIThread.Start();

            // use updates
            UseServerReadingUpdates(data1, generator, "example.ftre", changeLogger);

            while ((!exitCommand) && (!serverEnd)) { }

            if (exitCommand)
            {
                GUIThread.Interrupt();
                Console.WriteLine("Application interrupted");
                return;
            }

            FlightsGUIData GUIData = new FlightsGUIData();
            DataToFlightGUITransformer transformer = new DataToFlightGUITransformer(data1.FlightDictionary.Values.ToList(), GUIData, data1.AirportDictionary);
            transformer.DataToGUIDataFirstTime();           
            
            while (!exitCommand)
            {
                Thread.Sleep(1000);
                transformer.DataToGUIData();
                FlightTrackerGUI.Runner.UpdateGUI(GUIData);              
            }
            GUIThread.Interrupt();

            changeLogger.Close();

            data1.WriteToJson("DataInJson.json");
        }

        private static void Server_OnContactInfoUpdate(object sender, ContactInfoUpdateArgs args)
        {
            throw new NotImplementedException();
        }

        static void ServerWork(NetworkSourceSimulator.NetworkSourceSimulator server, int sleepTime)
        {
            try
            {
                Thread.Sleep(sleepTime);
                server.Run();
                Console.WriteLine("Server: all data has been send");
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Server was interapted");
            }                    
            serverEnd = true;
        }

        static void UseServerReading(Data data, Generator generator, string fileName)
        {
            NetworkSourceSimulator.NetworkSourceSimulator server = new NetworkSourceSimulator.NetworkSourceSimulator(fileName, 10, 50);
            Thread serverThread = new Thread(() => ServerWork(server, 0));
            MessageHandler messageHandler = new MessageHandler(data, server, generator);
            server.OnNewDataReady += messageHandler.HandleNewDataReady;
            serverThread.Start();
        }

        static void UseServerReadingUpdates(Data data, Generator generator, string fileName, ChangeLogger changeLogger)
        {
            NetworkSourceSimulator.NetworkSourceSimulator server = new NetworkSourceSimulator.NetworkSourceSimulator(fileName, 10, 50);
            Thread serverThread = new Thread(() => ServerWork(server, 4000));                        
            MessageHandler messageHandler = new MessageHandler(data, server, generator, changeLogger);
            server.OnNewDataReady += messageHandler.HandleNewDataReady;
            server.OnIDUpdate += messageHandler.HandleIDUpdate;
            server.OnPositionUpdate += messageHandler.HandlePositionUpdate;
            server.OnContactInfoUpdate += messageHandler.HandleContactInfoUpdate;
            serverThread.Start();

        }

        static void UseReadingFormFile(out Data data, Generator generator)
        {
            Reader myReader = new Reader("example_data.ftr", generator);
            myReader.ReadAll(out data);
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
                    List<IReportable> objectsToReport =
                    [
                        .. data.PassengerPlaneDictionary.Values.ToList(),
                        .. data.CargoPlaneDictionary.Values.ToList(),
                        .. data.AirportDictionary.Values.ToList(),
                    ];
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

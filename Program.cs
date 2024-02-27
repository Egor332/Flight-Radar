using System.Text.Json;

namespace FlightRadar
{
    internal class Program
    {
       
        static void Main(string[] args)
        {           

            Data data1;
            Reader myReader = new Reader("example_data.ftr");
            myReader.ReadAll(out data1);
            data1.WriteToJson("DataInJson.json");
           
        
        }
    }
}

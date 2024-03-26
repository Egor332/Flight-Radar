using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlightRadar
{
    internal class Reader
    {
        private string _FileName;
        private Generator _Generator;

        public Reader(string fileName)
        {
            _FileName = fileName;
        }

        public void ReadAll(out Data data)
        {
            data = new Data();

            StreamReader sr = new StreamReader(_FileName);
            
            string line;
            while ((line = sr.ReadLine()) != null)
            {                
                string[] argumentsArray = line.Split(',');
                _Generator.Generate(argumentsArray, data);
            }


        }
    }
}

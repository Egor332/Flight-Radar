using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Query.Exceptions
{
    internal class InvalidFieldException : Exception
    {
        public InvalidFieldException(string word) { Console.WriteLine("InvalidFieldException: typo in word:" + word); }
        public InvalidFieldException() { Console.WriteLine("InvalidFieldException: too much fields provided");  }
    }
}

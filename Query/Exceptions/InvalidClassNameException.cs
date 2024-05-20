using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Query.Exceptions
{
    internal class InvalidClassNameException : Exception
    {
        public InvalidClassNameException(string word) : base()
        {
            Console.WriteLine("InvalidClassNameException: typo in word: " + word);
        }
        public InvalidClassNameException() : base() { }
    }
}

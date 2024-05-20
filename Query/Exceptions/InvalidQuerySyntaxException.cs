using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Query.Exceptions
{
    public class InvalidQuerySyntaxException: Exception
    {
        public InvalidQuerySyntaxException(string word) : base()
        {
            Console.WriteLine("InvalidQuerySyntaxException: typo in word: " + word);
        }
        public InvalidQuerySyntaxException() : base() { }
    }
}

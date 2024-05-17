using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Query
{
    internal class QuerySeparator
    {
        private static readonly Dictionary<string, Action<string[]>> _QueryDictionaty = new Dictionary<string, Action<string[]>>
        {
            { "display", (words) => DisplaySeparation(words) },
            { "update", (words) => UpdateSeparation(words) },
            { "delete", (words) => DeleteSeparation(words) },
            { "add", (words) => AddSeparation(words) }
        };

        private string _QuerryString;

        public QuerySeparator(string queryString)
        {
            _QuerryString = queryString;
        }

        public void TrySeparateQuery()
        {
            string[] words = _QuerryString.Split(' ');
            try
            {
                _QueryDictionaty[words[0].ToLower()].Invoke(words);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exceptions.InvalidQuerySyntaxException(words[0]);
            }                                              
        }

        private static void DisplaySeparation(string[] words)
        {
            List<string> fieldsName = new List<string>();
            int indicator = 0;
            string from;
            List<string> whereConditions = new List<string>();
            for(int i = 1; i < words.Length; i++)
            {
                if ((words[i].ToLower() == "from") || (words[i].ToLower() == "where"))
                {
                    indicator++;
                    continue;
                }
                if (indicator == 0)
                {
                    fieldsName.Add(words[i].Replace(",", ""));
                    continue;
                }
                if (indicator == 1)
                {
                    from = words[i];
                    indicator++;
                    continue;
                }
                if (indicator == 3)
                {
                    whereConditions.Add(words[i].Replace(",", ""));
                    continue;
                }
                throw new Exceptions.InvalidQuerySyntaxException();
            }
        }

        private static void UpdateSeparation(string[] words)
        {
            string from = words[1];
            List<string> toSet = new List<string>();
            List<string> whereConditions = new List<string>();
            int indicator = 0;
            for (int i = 2; i < words.Length; i++)
            {
                if ((words[i].ToLower() == "set") || (words[i].ToLower() == "where"))
                {
                    indicator++;
                    continue;
                }
                if (indicator == 1)
                {
                    toSet.Add(words[i].Replace(",", ""));
                    continue;
                }
                if (indicator == 2)
                {
                    whereConditions.Add(words[i].Replace(",", ""));
                    continue;
                }
                throw new Exceptions.InvalidQuerySyntaxException();

            }
                      

        }

        private static void DeleteSeparation(string[] words)
        {
            string from = words[1];
            if (words[2].ToLower() != "where")
            {
                throw new Exceptions.InvalidQuerySyntaxException(words[2]);
            }
            List<string> whereConditions = new List<string>();
            for (int i = 3; i < words.Length; i++)
            {
                whereConditions.Add(words[i].Replace(",", ""));
            }

        }

        private static void AddSeparation(string[] words)
        {
            string from = words[1];
            if (words[2].ToLower() != "new")
            {
                throw new Exceptions.InvalidQuerySyntaxException(words[2]);
            }
            List<string> toSet = new List<string>();
            for (int i = 3; i < words.Length; i++)
            {
                toSet.Add(words[i].Replace(",", ""));
            }

        }
    }
}

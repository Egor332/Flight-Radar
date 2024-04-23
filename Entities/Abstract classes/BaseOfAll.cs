
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal abstract class BaseOfAll
    {
        public UInt64 Id;

        public BaseOfAll(UInt64 id)
        {
            this.Id = id;
        }

        public BaseOfAll(string id)
        {
            UInt64.TryParse(id, out Id);
        }

        public BaseOfAll(byte[] args)
        {
            Id = BitConverter.ToUInt64(args, 7);
        }

        protected void ReadCharArray(byte[] source, UInt16 length, int offset, char[] target) 
        { 
            for (int i = 0; i < length; i++)
            {
                target[i] = (char)(source[offset + i]);
            }
        }
    }
}

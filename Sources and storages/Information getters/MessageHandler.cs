using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar
{
    internal class MessageHandler
    {
        private Data _Data;
        private NetworkSourceSimulator.NetworkSourceSimulator _Server;
        private Generator _Generator;

        public MessageHandler(Data data, NetworkSourceSimulator.NetworkSourceSimulator server, Generator generator)
        {
            _Data = data;
            _Server = server;
            _Generator = generator;
        }

        public void HandleNewDataReady(object sender, NewDataReadyArgs e)
        {
            MessageToData(e.MessageIndex);
        }

        private void MessageToData(int index)
        {
            Message message = _Server.GetMessageAt(index);
            
            _Generator.Generate(message.MessageBytes, _Data);
            
        }

        public void HandleIDUpdate(object sender, IDUpdateArgs e)
        {

        }


        public void HandlePositionUpdate(object sender, PositionUpdateArgs e)
        {

        }

        public void HandleContactInfoUpdate(object sender, ContactInfoUpdateArgs e)
        {

        }
    }
}

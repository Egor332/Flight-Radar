using FlightRadar.Entities.Abstract_classes;
using FlightRadar.Sources_and_storages.Information_getters;
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
        private ChangeLogger? _ChangeLogger = null;

        public MessageHandler(Data data, NetworkSourceSimulator.NetworkSourceSimulator server, Generator generator, ChangeLogger logger)
        {
            _Data = data;
            _Server = server;
            _Generator = generator;            
            _ChangeLogger = logger;
        }

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
            BaseOfAll? objectToChange = SearchThroughAll(e.ObjectID, e.NewObjectID);
            if (objectToChange == null)
            {
                _ChangeLogger.LogWrongId(e.ObjectID);
                return;
            }
            objectToChange.Id = e.NewObjectID;
            _ChangeLogger.LogIdChange(e);

        }

        private BaseOfAll? SearchThroughAll(UInt64 Id, UInt64 newId)
        {
            if (_Data.CrewDictionary.ContainsKey(Id))
            {
                try { _Data.CrewDictionary.Add(newId, _Data.CrewDictionary[Id]); }
                catch (System.ArgumentException ex) { _ChangeLogger.LogBusyId(newId); return null; }

                _Data.CrewDictionary.Remove(Id);
                return _Data.CrewDictionary[newId];
            }

            if (_Data.PassengerDictionary.ContainsKey(Id))
            {
                try { _Data.PassengerDictionary.Add(newId, _Data.PassengerDictionary[Id]); }
                catch (System.ArgumentException ex) { _ChangeLogger.LogBusyId(newId); return null; }
                _Data.PassengerDictionary.Remove(Id);
                return _Data.PassengerDictionary[newId];
            }

            if (_Data.CargoDictionary.ContainsKey(Id))
            {
                try { _Data.CargoDictionary.Add(newId, _Data.CargoDictionary[Id]); }
                catch (System.ArgumentException ex) { _ChangeLogger.LogBusyId(newId); return null; }
                _Data.CargoDictionary.Remove(Id);
                return _Data.CargoDictionary[newId];
            }

            if (_Data.CargoPlaneDictionary.ContainsKey(Id))
            {
                try { _Data.CargoPlaneDictionary.Add(newId, _Data.CargoPlaneDictionary[Id]); }
                catch (System.ArgumentException ex) { _ChangeLogger.LogBusyId(newId); return null; }
                _Data.CargoPlaneDictionary.Remove(Id);
                return _Data.CargoPlaneDictionary[newId];
            }

            if (_Data.PassengerPlaneDictionary.ContainsKey(Id))
            {
                try { _Data.PassengerPlaneDictionary.Add(newId, _Data.PassengerPlaneDictionary[Id]); }
                catch (System.ArgumentException ex) { _ChangeLogger.LogBusyId(newId); return null; }
                _Data.PassengerPlaneDictionary.Remove(Id);
                return _Data.PassengerPlaneDictionary[newId];
            }

            if (_Data.AirportDictionary.ContainsKey(Id))
            {
                try { _Data.AirportDictionary.Add(newId, _Data.AirportDictionary[Id]); }
                catch (System.ArgumentException ex) { _ChangeLogger.LogBusyId(newId); return null; }
                _Data.AirportDictionary.Remove(Id);
                return _Data.AirportDictionary[newId];
            }

            if (_Data.FlightDictionary.ContainsKey(Id))
            {
                try { _Data.FlightDictionary.Add(newId, _Data.FlightDictionary[Id]); }
                catch (System.ArgumentException ex) { _ChangeLogger.LogBusyId(newId); return null; }
                _Data.FlightDictionary.Remove(Id);
                return _Data.FlightDictionary[newId];
            }

            return null;

        }

        public void HandlePositionUpdate(object sender, PositionUpdateArgs e)
        {
            PositionObject position = SearchThroughAllPosition(e);
            if (position == null)
            {
                _ChangeLogger.LogWrongId(e.ObjectID);
                return;
            }
            position.Longitude = e.Longitude;
            position.Latitude = e.Latitude;
            position.AMSL = e.AMSL;
            _ChangeLogger.LogPositionChange(e);
        }

        private PositionObject? SearchThroughAllPosition(PositionUpdateArgs e)
        {
            if (_Data.AirportDictionary.ContainsKey(e.ObjectID))
            {
                return _Data.AirportDictionary[e.ObjectID];
            }

            if (_Data.FlightDictionary.ContainsKey(e.ObjectID))
            {
                
                _Data.FlightDictionary[e.ObjectID].SetStartArgs(e.Latitude, e.Longitude, TimeOnly.FromDateTime(DateTime.Now));
                return _Data.FlightDictionary[e.ObjectID];
            }

            return null;
        }

        public void HandleContactInfoUpdate(object sender, ContactInfoUpdateArgs e)
        {
            Human? human = SearchThroughAllHuman(e.ObjectID);
            if (human == null) 
            {
                _ChangeLogger.LogWrongId(e.ObjectID);
                return;
            }
            human.Phone = e.PhoneNumber;
            human.Email = e.EmailAddress;
            _ChangeLogger.LogContactUpdateChange(e);
        }

        private Human? SearchThroughAllHuman(UInt64 Id)
        {
            if (_Data.CrewDictionary.ContainsKey(Id))
            {
                return _Data.CrewDictionary[Id];
            }
            
            if (_Data.PassengerDictionary.ContainsKey(Id))
            {
                return _Data.PassengerDictionary[Id];
            }

            return null;
        }
    }
}

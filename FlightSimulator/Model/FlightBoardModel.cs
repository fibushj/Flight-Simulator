using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class FlightBoardModel : BaseNotify
    {
        //a boolean which becomes true when the Disconnect button is pressed
        volatile Boolean stop = false;
        ITCPServer server;
        ITCPClient client;
        private double lat;
        private double lon;
        public double Lon
        {
            get { return lon; }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        public double Lat
        {
            get { return lat; }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }

        public FlightBoardModel() { }

       public void StartInfoChannel()
        {
            server = MyTCPServer.Instance;
            //setting it yet again to false in case disconnect is pressed, and then connect again
            stop = false;
            server.EstablishConnection();
        }

        public void StartCommandsChannel()
        {
            client = MyTCPClient.Instance;
            client.Connect();
        }

        public void TreatReceivedData()
        {
            double[] values;
            while (!stop)
            {
                values = server.Read();
                //those are the respective place of Lon and Lat according to generic_small.xml
                Lon = values[0];
                Lat = values[1];
            }
        }
       public void Stop()
        {
            stop = true;
            
        }
        public void Disconnect()
        {
            server.CloseConnection();
            client.Disconnect();
        }


    }
}

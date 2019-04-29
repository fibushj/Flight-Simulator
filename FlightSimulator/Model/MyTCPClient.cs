using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class MyTCPClient : ITCPClient
    {
        private TcpClient client;
        private StreamWriter writer = null;
        #region Singleton
        private static ITCPClient m_Instance = null;
        public static ITCPClient Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new MyTCPClient();
                }
                return m_Instance;
            }
        }
        #endregion
        public void Connect()
        {
            /* getting the information of the IP address and port from the singleton instance of 
             ApllicationSettingsModel that was provided to us  */
            string ip = ApplicationSettingsModel.Instance.FlightServerIP;
            int port = ApplicationSettingsModel.Instance.FlightCommandPort;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ep);
            writer = new StreamWriter(client.GetStream());
            Debug.WriteLine("Commands channel established");
        }

        public void Disconnect()
        {
            writer.Close();
            client.Close();
        }

        public void Send(string message)
        {
            if (writer != null)
            {
                writer.WriteLine(message);
                Debug.WriteLine("the command: " + message + " was delivered successfully");
                //forcing the writer to send the message immediately
                writer.Flush();
            }
            else
            {
                Debug.WriteLine("Commands channel hasn't yet been established, please wait");
            }

        }
    }
}

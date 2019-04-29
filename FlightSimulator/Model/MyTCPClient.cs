using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
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
            string ip = ApplicationSettingsModel.Instance.FlightServerIP;
            int port = ApplicationSettingsModel.Instance.FlightCommandPort;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ep);
            writer = new StreamWriter(client.GetStream());
            Console.WriteLine("Commands channel established");
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
                Console.WriteLine("the command: " + message + " was delivered successfully");
                writer.Flush();
            }
            else
            {
                Console.WriteLine("Commands channel hasn't yet been established, please wait");
            }

        }
    }
}

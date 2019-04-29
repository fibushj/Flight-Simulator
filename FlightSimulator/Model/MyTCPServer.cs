using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    class MyTCPServer : ITCPServer
    {
        private TcpClient client = null;
        private TcpListener listener = null;
        NetworkStream stream;
        #region Singleton
        private static ITCPServer m_Instance = null;
        public static ITCPServer Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new MyTCPServer();
                }
                return m_Instance;
            }
        }
        #endregion

        public void EstablishConnection()
        {
               
            int port = ApplicationSettingsModel.Instance.FlightInfoPort;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            Debug.WriteLine("Info channel established");
            listener.Start();
            client = listener.AcceptTcpClient();
            stream = client.GetStream();
            Debug.WriteLine("Simulator connected");

        }

        public void CloseConnection()
        {
            stream.Dispose();
            client.Close();
            listener.Stop();
        }
      
        /* In our case, the method returns a double array which contains 2 values - lon and lat - received
         from the simulator */
        public double[] Read()
        {
            Byte[] bytes = new byte[client.ReceiveBufferSize];
            stream.Read(bytes, 0, client.ReceiveBufferSize);
            string message = Encoding.ASCII.GetString(bytes);
            double[] returnedValues = new double[2];
            if (message.Contains(","))
            {
                /* There's no need for doing extra splitting (more that 3 substrings) since lon and lat are the
                 * first values that appear in the string. Delimiting by comma */
                string[] splittedSubstrings = message.Split(new[] { ',' }, 3, StringSplitOptions.None);
                returnedValues[0] = double.Parse(splittedSubstrings[0]);
                returnedValues[1] = double.Parse(splittedSubstrings[1]);
            }
          
            return returnedValues;                                  
        }
    }
}

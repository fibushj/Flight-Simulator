using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface ITCPClient
    {
            void Connect();
            void Disconnect();
            void Send(string message);
    }
}

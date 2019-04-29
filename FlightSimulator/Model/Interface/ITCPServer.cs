using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface ITCPServer
    {
        void EstablishConnection();
        void CloseConnection();
        double[] Read();

    }
}

using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class JoystickModel
    {
        ITCPClient client;
        public JoystickModel()
        {
            client = MyTCPClient.Instance;
        }

        public void SendValue(string property, double value)
        {
            string message;
            //sending the following messages according to the format the simulator expects
            if (property.Equals("Rudder"))
            {
                message = "set /controls/flight/rudder ";
            }
            else if (property.Equals("Throttle"))
            {
                message = "set /controls/engines/current-engine/throttle ";
            }
            else if (property.Equals("Elevator"))
            {
                message = "set /controls/flight/elevator ";
            }
            else if (property.Equals("Aileron"))
            {
                message = "set /controls/flight/aileron ";
            }
            else
            {
                return;
            }
            message += value;
            client.Send(message);
           
        }
    }
}

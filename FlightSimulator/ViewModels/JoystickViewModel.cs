using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class JoystickViewModel
    {
        private JoystickModel model;

        public JoystickViewModel(JoystickModel model)
        {
            this.model = model;

        }

        public double Rudder
        {
            set
            {
                model.SendValue("Rudder", value);
            }
        }
        public double Throttle
        {
            set
            {
                model.SendValue("Throttle", value);
            }
        }
        public double Aileron
        {
            set
            {
                model.SendValue("Aileron", value);
            }
        }
        public double Elevator
        {
            set
            {
                model.SendValue("Elevator", value);
            }
        }
    }
}

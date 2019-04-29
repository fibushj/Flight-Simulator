using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class AutoPilotModel
    {
        private ITCPClient client;
        public AutoPilotModel() {
            client = MyTCPClient.Instance;

        }

        public void Send(string commands)
        {
            string[] commandsArray = commands.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            new Thread(delegate ()
            {
                foreach (string command in commandsArray)
                {
                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        client.Send(command);
                        Thread.Sleep(2000);
                    }
                }
            }).Start();
            
        }
    }
}

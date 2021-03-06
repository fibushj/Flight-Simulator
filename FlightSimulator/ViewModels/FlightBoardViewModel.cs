﻿using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel model;
        private Boolean stop = false;

        public double Lon
        {
            get { return model.Lon; }
        }

        public double Lat
        {
            get { return model.Lat; }
        }

       

        public FlightBoardViewModel(FlightBoardModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        private ICommand connectCommand;

        public ICommand ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new CommandHandler(() => OnConnectClick()));
            }
        }

        private Thread t = null;
        private void OnConnectClick()
        {
            //running the info channel in a new thread
            t = new Thread(() =>
              {
                  //the purpose of this boolean is explained in Joystick.xaml.cs
                  stop = false;
                  model.StartInfoChannel();
                  model.StartCommandsChannel();
                  //now that both channels were established, treating the data received from the simulator
                  model.TreatReceivedData();
              });
            t.Start();            
        }

        private ICommand disconnectCommand;

        public ICommand DisconnectCommand
        {
            get
            {
                return disconnectCommand ?? (disconnectCommand = new CommandHandler(() => OnDisconnectClick()));
            }
        }

        private void OnDisconnectClick()
        {
            stop = true;
            //gracefully closing the connection and the info channel thread
            model.Stop();
            if (t.IsAlive)
            {
                if (t != Thread.CurrentThread)
                {
                    t.Join();
                }
            }
            Debug.WriteLine("The thread of info channel has successfully finished");
            model.Disconnect();
            Debug.WriteLine("Successfully disconnected both channels");
        }

        public Boolean ShouldStop() { return stop; }
        


        private ICommand settingsCommand;

        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnSettingsClick()));
            }
        }

        private void OnSettingsClick()
        {
            Views.SettingsWindow window = new Views.SettingsWindow();
            window.Show();
        }
       
    }
}

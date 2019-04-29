using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModels
{
    public class AutoPilotViewModel : BaseNotify
    {

        private AutoPilotModel autoPilotModel;

        public AutoPilotViewModel(AutoPilotModel model)
        {
            autoPilotModel = model;
            Commands = string.Empty;
            IsTyping = false;
            
        }

        private string commands;       
        public string Commands
        {
            get
            {
                return commands;
            }
            set
            {
                IsTyping = true;
                commands = value;                
                NotifyPropertyChanged("Commands");
            }
        }
        private bool isTyping;
        //the property indicates whether or not the user is currently typing somethind in the textbox
        public bool IsTyping
        {
            get
            {
                return isTyping;
            }
            set
            {
                isTyping = value;
                NotifyPropertyChanged("IsTyping");
            }
        }

        private ICommand okCommand;

        public ICommand OKCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() => OnOKClick()));
            }
        }

        private void OnOKClick()
        {
            IsTyping = false; ;
            autoPilotModel.Send(commands);
        }

        private ICommand cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new CommandHandler(() => OnCancelClick()));
            }
        }

        private void OnCancelClick()
        {
            Commands = string.Empty;
            IsTyping = false; 
        }
    }
}

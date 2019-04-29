using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for AutoPilot.xaml
    /// </summary>
    public partial class AutoPilot : UserControl
    {
        private AutoPilotViewModel vm;
        private Brush customLightRed;

        public AutoPilot()
        {
            InitializeComponent();
            vm = new AutoPilotViewModel(new AutoPilotModel());
            DataContext = vm;
            //this is the exact color from the instructions
            customLightRed = (Brush)new BrushConverter().ConvertFrom("#FFB6C1");

            vm.PropertyChanged += delegate (object sender, System.ComponentModel.PropertyChangedEventArgs e)
             {
                 /* if the user is typing, that means that the text has been changed, thus the background should 
                  * change to light red, otherwise it should be white */
                 if (e.PropertyName.Equals("IsTyping"))
                 {                     
                     if (vm.IsTyping)
                     {
                         textBox.Background = customLightRed;
                     }
                     else
                     {
                         textBox.Background = Brushes.White;
                     }
                 }
             };
        }
    }
}

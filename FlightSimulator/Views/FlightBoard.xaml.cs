using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for FlightBoard.xaml
    /// </summary>
    public partial class FlightBoard : UserControl
    {
        private FlightBoardViewModel vm;
        ObservableDataSource<Point> planeLocations = null;
        public FlightBoard()
        {
            InitializeComponent();
            vm = new FlightBoardViewModel(new FlightBoardModel());
            DataContext = vm;
            vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);

            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            /* there's no need to enter the if statement when the property changed is Lon, since both are always 
             * updated one after the other, thus resulting in drawing the same point twice (and it's problematic 
             * in the case of the first point, when Lat is automatically initialized to be zero and hasn't been
             * updated yet with its real value */
            if(e.PropertyName.Equals("Lat"))
            {
                Point p1 = new Point(vm.Lat, vm.Lon);
                if (!vm.ShouldStop())
                {
                    planeLocations.AppendAsync(Dispatcher, p1);
                }
            }
        }

       
    }

}


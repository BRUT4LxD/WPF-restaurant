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

namespace WPF_restauration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReservationHourTextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("Hour changed");
        }

        private void ReservationPlacesTextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("Places changed");
        }

        private void ReservationSurnameTextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("Surname changed");
        }

        private void ReservationButtonClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Button clicked");
        }
    }
}

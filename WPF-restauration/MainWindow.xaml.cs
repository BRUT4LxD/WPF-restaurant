using Database.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WPF_restauration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void MainWindowEventHandler();
        public ConcurrentDictionary<int, bool> TablesOccupation = new ConcurrentDictionary<int, bool>();
        public Dictionary<int, Rectangle> TablesBackgrounds = new Dictionary<int, Rectangle>();
        public static object TablesLock = new object();
        public static object TablesUnlock = new object();
        public static object NumberOfWorkersLock = new object();
        public int NumberOfWorkers = 0;

        public MainWindow()
        {
            InitializeComponent();
            TablesOccupation.TryAdd(0, false);
            TablesOccupation.TryAdd(1, false);
            TablesOccupation.TryAdd(2, false);
            TablesOccupation.TryAdd(3, false);
            TablesOccupation.TryAdd(4, false);
            TablesOccupation.TryAdd(5, false);

            TablesBackgrounds.Add(0, Table8Availability_1);
            TablesBackgrounds.Add(1, Table6Availability_1);
            TablesBackgrounds.Add(2, Table8Availability_2);
            TablesBackgrounds.Add(3, Table6Availability_2);
            TablesBackgrounds.Add(4, Table8Availability_3);
            TablesBackgrounds.Add(5, Table6Availability_3);

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

        private async void StartWorkersButtonClicked(object sender, RoutedEventArgs e)
        {
            Interlocked.Increment(ref NumberOfWorkers);
            numberOfWorkers.Text = NumberOfWorkers.ToString();
            var tables = await new TablesRepository(App.RestaurantContext).GetTablesAsync();
            var w1 = new ClientWorker(Dispatcher, tables, 3000, 3000, this, new ReservationsRepository(App.RestaurantContext));

            w1.Run();
        }
    }
}

using Database.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using WPF_restauration.UIModels;

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
        public List<UITable> UITables;
        public int NumberOfWorkers = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTables();
            InitializeTablesOccupation();
            InitializeTablesBackground();
        }
        private void InitializeTablesOccupation()
        {
            TablesOccupation.TryAdd(0, false);
            TablesOccupation.TryAdd(1, false);
            TablesOccupation.TryAdd(2, false);
            TablesOccupation.TryAdd(3, false);
            TablesOccupation.TryAdd(4, false);
            TablesOccupation.TryAdd(5, false);
        }

        private void InitializeTablesBackground()
        {
            TablesBackgrounds.Add(0, Table8Availability_1);
            TablesBackgrounds.Add(1, Table6Availability_1);
            TablesBackgrounds.Add(2, Table8Availability_2);
            TablesBackgrounds.Add(3, Table6Availability_2);
            TablesBackgrounds.Add(4, Table8Availability_3);
            TablesBackgrounds.Add(5, Table6Availability_3);
        }
        private void InitializeTables()
        {
            UITables = new List<UITable>
            {
                new UITable(this, (a, b) => a.Dispatcher.Invoke(() =>Table1Name.Text = b)),
                new UITable(this, (a, b) => a.Dispatcher.Invoke(() =>Table2Name.Text = b)),
                new UITable(this, (a, b) => a.Dispatcher.Invoke(() =>Table3Name.Text = b)),
                new UITable(this, (a, b) => a.Dispatcher.Invoke(() =>Table4Name.Text = b)),
                new UITable(this, (a, b) => a.Dispatcher.Invoke(() =>Table5Name.Text = b)),
                new UITable(this, (a, b) => a.Dispatcher.Invoke(() =>Table6Name.Text = b)),
            };
        }

        private void ReservationButtonClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Button clicked");
        }

        private void StartWorkersButtonClicked(object sender, RoutedEventArgs e)
        {
            Interlocked.Increment(ref NumberOfWorkers);
            numberOfWorkers.Text = NumberOfWorkers.ToString();

            var w1 = new ClientWorker(Dispatcher, UITables, 3000, 3000, this, new ReservationsRepository(App.RestaurantContext));

            w1.Run();
        }
    }
}

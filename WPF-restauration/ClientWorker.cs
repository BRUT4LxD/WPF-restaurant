using Database.Model;
using Database.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using WPF_restauration.UIModels;

namespace WPF_restauration
{
    internal class ClientWorker
    {
        private List<UITable> _tables;
        private readonly int _respawnTimeInMs;
        private readonly int _occupationTimeInMs;
        private readonly Dispatcher _dispatcher;
        private readonly MainWindow _mainWindow;
        private static readonly int TriesToSit = 15;
        private readonly ReservationsRepository _reservationsRepository;

        public ClientWorker(
            Dispatcher dispatcher,
            List<UITable> tables,
            int respawnTimeInMs,
            int occupationTimeInMs,
            MainWindow mainWindow,
            ReservationsRepository reservationsRepository)
        {
            _tables = tables;
            _respawnTimeInMs = respawnTimeInMs;
            _occupationTimeInMs = occupationTimeInMs;
            _dispatcher = dispatcher;
            _mainWindow = mainWindow;
            _reservationsRepository = reservationsRepository;
        }

        public void Run()
        {
            var thread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                int tries = 0;
                while(tries <= TriesToSit)
                {
                    Thread.Sleep(new Random().Next(1000, 1000 + _respawnTimeInMs));
                    int tableId = 0;
                    lock(MainWindow.TablesLock)
                    { 
                        tableId = new Random().Next(6);
                        int counter = 0;
                        while (counter++ < 6 && _mainWindow.TablesOccupation[++tableId % 6]) ;
                        tableId %= 6;
                        if (counter == 6)
                        {
                            tries++;
                            continue;
                        }

                        ReserveTable(tableId);

                    }
                    Thread.Sleep(new Random().Next(_occupationTimeInMs));
                    lock (MainWindow.TablesUnlock)
                    {
                        FreeTable(tableId);
                    }

                    UpdateNumberOfWorkers();
                    break;
                }
            });
            thread.Start();
        }

        private Reservation AddReservation(int tableId)
        {
            var reservation = new Reservation
            {
                StartTime = DateTime.Now,
                Name = DataFactory.Names[new Random().Next(DataFactory.Names.Count)],
                TableId = tableId,
                RestaurantId = 1
            };
            Task.Run(() => _reservationsRepository.AddAsync(reservation));
            return reservation;
        }

        private void ReserveTable(int tableId)
        {
            _mainWindow.TablesOccupation[tableId] = true;

            var reservation = AddReservation(tableId);

            _tables[tableId].UpdateTable(reservation.Name);

            _dispatcher?.Invoke(() => _mainWindow.TablesBackgrounds[tableId].Fill = new SolidColorBrush(Color.FromRgb(190, 20, 20)));
        }

        private void FreeTable(int tableId)
        {
            _mainWindow.TablesOccupation[tableId] = false;

            _dispatcher?.Invoke(() => _mainWindow.TablesBackgrounds[tableId].Fill = new SolidColorBrush(Color.FromRgb(115, 229, 53)));
        }   

        private void UpdateNumberOfWorkers()
        {
            Interlocked.Decrement(ref _mainWindow.NumberOfWorkers);
            _dispatcher?.Invoke(() => _mainWindow.numberOfWorkers.Text = _mainWindow.NumberOfWorkers.ToString());
        }
    }
}

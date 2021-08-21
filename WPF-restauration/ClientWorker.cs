using Database.Model;
using Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using WPF_restauration.UIModels;

namespace WPF_restauration
{
    internal class ClientWorker
    {
        private List<UITable> _tables;
        private readonly int _respawnTimeInMs;
        private readonly int _occupationTimeInMs;
        private readonly MainWindow _mainWindow;
        private static readonly int TriesToSit = 15;
        private readonly ReservationsRepository _reservationsRepository;
        private readonly string _clientName;
        private static readonly Random R = new();

        public ClientWorker(
            List<UITable> tables,
            int respawnTimeInMs,
            int occupationTimeInMs,
            MainWindow mainWindow,
            ReservationsRepository reservationsRepository,
            string clientName = "")
        {
            _tables = tables;
            _respawnTimeInMs = respawnTimeInMs;
            _occupationTimeInMs = occupationTimeInMs;
            _mainWindow = mainWindow;
            _reservationsRepository = reservationsRepository;
            _clientName = string.IsNullOrEmpty(clientName) ? DataFactory.Names[R.Next(DataFactory.Names.Count)] : clientName;
        }

        public void Run()
        {
            var thread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                int tries = 0;
                while(tries <= TriesToSit)
                {
                    Thread.Sleep(R.Next(1000, 1000 + _respawnTimeInMs));
                    int tableId = 0;
                    lock(MainWindow.TablesLock)
                    { 
                        tableId = R.Next(6);
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
                    Thread.Sleep(R.Next(_occupationTimeInMs));
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
                Name = _clientName,
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

            _mainWindow.Dispatcher?.Invoke(() => _mainWindow.TablesBackgrounds[tableId].Fill = new SolidColorBrush(Color.FromRgb(190, 20, 20)));
        }

        private async void FreeTable(int tableId)
        {
            _mainWindow.TablesOccupation[tableId] = false;

            var reservations = await _reservationsRepository.GetOngoingReservations();

            var reservation = reservations.First(e => e.TableId == tableId);
            reservation.EndTime = DateTime.Now;

            await _reservationsRepository.UpdateAsync(reservation);

            _mainWindow.Dispatcher?.Invoke(() => _mainWindow.TablesBackgrounds[tableId].Fill = new SolidColorBrush(Color.FromRgb(115, 229, 53)));
            _tables[tableId].UpdateTable("");
        }

        private void UpdateNumberOfWorkers()
        {
            Interlocked.Decrement(ref _mainWindow.NumberOfWorkers);
            _mainWindow.Dispatcher?.Invoke(() => _mainWindow.numberOfWorkers.Text = _mainWindow.NumberOfWorkers.ToString());
        }
    }
}

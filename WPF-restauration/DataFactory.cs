using Database.Model;
using Database.Repositories;
using System.Threading.Tasks;

namespace WPF_restauration
{
    public static  class DataFactory
    {
        private static readonly ReservationsRepository reservationsRepository = new(App.RestaurantContext);

        private static readonly TablesRepository tablesRepository = new(App.RestaurantContext);

        private static readonly RestaurantRepository restaurantRepository = new(App.RestaurantContext);

        public static async Task CreateInitialData()
        {
            await AddTables();
            await AddRestaurant();
        }

        private static async Task AddTables()
        {
            for (int i = 0; i < 3; i++)
            {
                await tablesRepository.AddAsync(new Table { AvailablePlaces = 6 });
            }

            for (int i = 0; i < 3; i++)
            {
                await tablesRepository.AddAsync(new Table { AvailablePlaces = 8 });
            }
        }

        private static async Task AddRestaurant()
        {
            await restaurantRepository.AddAsync(
                new Restaurant {
                    Name = "Best Restaurantos",
                    Tables = await tablesRepository.GetTablesAsync() 
                });
        }
    }
}

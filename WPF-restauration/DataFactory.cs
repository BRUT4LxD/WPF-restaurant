using Database.Model;
using Database.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPF_restauration
{
    public static class DataFactory
    {
        private static readonly TablesRepository tablesRepository = new(App.RestaurantContext);

        private static readonly RestaurantRepository restaurantRepository = new(App.RestaurantContext);

        public static List<string> Names = new()
        {
            "Zuzanna",
            "Julia",
            "Zofia",
            "Hanna",
            "Maja",
            "Lena",
            "Alicja",
            "Oliwia",
            "Lauram",
            "Maria",
            "Antoni",
            "Jan",
            "Jakub",
            "ALeksander",
            "Francisze",
            "Szymon",
            "Filip",
            "Mikołaj",
            "Stanisław",
            "Wojciech"
        };

        public static async Task CreateInitialData()
        {
            await AddTables();
            await AddRestaurant();
        }

        private static async Task AddTables()
        {
            for (int i = 0; i < 3; i++)
            {
                await tablesRepository.AddAsync(new Table { Id = i * 2 + 1, AvailablePlaces = 6 });
            }

            for (int i = 0; i < 3; i++)
            {
                await tablesRepository.AddAsync(new Table { Id = i * 2,  AvailablePlaces = 8 });
            }
        }

        private static async Task AddRestaurant()
        {
            await restaurantRepository.AddAsync(
                new Restaurant {
                    Id = 1,
                    Name = "Best Restaurantos",
                    Tables = await tablesRepository.GetTablesAsync()
                });
        }
    }
}

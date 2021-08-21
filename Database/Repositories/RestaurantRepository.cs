using Database.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class RestaurantRepository
    {
        private readonly RestaurantContext _context;

        public RestaurantRepository(RestaurantContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Restaurant restaurant)
        {
            await _context.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task<Restaurant> GetRestaurant(int id) => await _context.Restaurants.FindAsync(id);

        public async Task<IEnumerable<Restaurant>> GetRestaurants() => await _context.Restaurants.ToListAsync();

    }
}

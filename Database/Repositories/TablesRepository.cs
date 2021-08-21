using Database.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class TablesRepository
    {
        private readonly RestaurantContext _context;

        public TablesRepository(RestaurantContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Table table) {
            await _context.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task<Table> GetTableAsync(int id) => await _context.Tables.FindAsync(id);

        public async Task<List<Table>> GetTablesAsync() => await _context.Tables.ToListAsync();

    }
}

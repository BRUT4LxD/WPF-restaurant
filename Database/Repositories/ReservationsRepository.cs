using Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class ReservationsRepository
    {
        private readonly RestaurantContext _context;

        public ReservationsRepository(RestaurantContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<Reservation> GetReservation(int id) => await _context.Reservations.FindAsync(id);

        public async Task<List<Reservation>> GetOngoingReservation() => await _context.Reservations.Where(e => e.EndTime > DateTime.Now).ToListAsync();

        public Task<List<Reservation>> GetReservations() => _context.Reservations.ToListAsync();

    }
}

using Microsoft.EntityFrameworkCore;
using ReservationCinema.Data;
using ReservationCinema.Models;

namespace ReservationCinema.Repositories
{
    public class CinemaRepository : Repository<Cinema>, ICinemaRepository
    {
        public CinemaRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IEnumerable<Cinema> Search(string query)
        {
            return _dbSet
                .Where(c => c.Nom.Contains(query) || c.Ville.Contains(query))
                .ToList();
        }
    }
}


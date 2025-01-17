using ReservationCinema.Data;
using ReservationCinema.Models;

namespace ReservationCinema.Repositories
{
    public class CinemaRepository : Repository<Cinema>,ICinemaRepository
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
        public (IEnumerable<Cinema> Cinemas, int TotalCount) GetCinemas(string query, int page, int pageSize)
        {
            var queryable = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(c => c.Nom.Contains(query) || c.Ville.Contains(query));
            }

            var totalCount = queryable.Count(); // Nombre total d'enregistrements correspondant à la recherche
            var cinemas = queryable
                .OrderBy(c => c.Nom) // Optionnel : tri des résultats
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (cinemas, totalCount);
        }

        // Utilisation de la générique pour le getById
        public Cinema GetCinemaById(int id)
        {
            return GetById(id);
        }

    }
}

using ReservationCinema.Models;

namespace ReservationCinema.Repositories
{
    public interface ICinemaRepository : IRepository<Cinema>
    {
        IEnumerable<Cinema> Search(string query);
    }
}

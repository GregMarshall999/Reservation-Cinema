using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationCinema.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ReservationCinema.Repositories
{
    public interface ICinemaRepository
    {
        IEnumerable<Cinema> Search(string query);
        (IEnumerable<Cinema> Cinemas, int TotalCount) GetCinemas(string query, int page, int pageSize);
        Cinema GetCinemaById(int id);
    }
}

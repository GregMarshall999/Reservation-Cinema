using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReservationCinema.Data;
using ReservationCinema.Dto;
using ReservationCinema.Models;

namespace ReservationCinema.Repositories
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(ApplicationDbContext context) : base(context)
        {
            // Le constructeur est utilisé pour initialiser le contexte ou d'autres éléments nécessaires
        }

        // Définition de la méthode SearchFilm en dehors du constructeur
        public PaginationDto<List<Film>> SearchFilms(string query, int page)
        {
            // Taille de la page (fixe ou paramétrable)
            var pageSize = 10;

            // Récupère les films filtrés par le titre (en fonction de la recherche)
            var queryFilms = _context.Films
                .Include(f => f.Seances)
                    .ThenInclude(s => s.Horaire)
                .Include(f => f.Seances)
                    .ThenInclude(s => s.Salle)
                        .ThenInclude(s => s.Cinema)
                .Where(f => string.IsNullOrEmpty(query) || f.Titre.Contains(query));

            // Calcul du total de films pour la pagination
            var totalFilms = queryFilms.Count();

            // Applique la pagination (skip et take)
            var paginatedFilms = queryFilms
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Retourne le résultat paginé
            return new PaginationDto<List<Film>>
            {
                Page = page,
                Total = totalFilms,
                Data = paginatedFilms
            };
        }

        public IEnumerable<Film> GetFilmsForCinemaToday(int cinemaId)
        {
            // Récupérer les films pour ce cinéma avec les séances programmées aujourd'hui
            var films = _context.Films
                                .Where(f => f.Seances
                                             .Any(s => s.Salle.CinemaId == cinemaId && s.Horaire.HeureDebut.Date == DateTime.Today))
                                .Include(f => f.Seances)  
                                .ThenInclude(s => s.Horaire)  
                                .ThenInclude(h => h.Seances) 
                                .Include(f => f.Seances)
                                .ThenInclude(s => s.Salle) 
                                .ThenInclude(s => s.Cinema) 
                                .ToList();

            return films;
        }

    }
}
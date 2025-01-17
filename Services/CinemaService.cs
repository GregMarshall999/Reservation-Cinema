using ReservationCinema.Dto;
using ReservationCinema.Models;
using ReservationCinema.Repositories;

namespace ReservationCinema.Services
{
    public class CinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public CinemaService(ICinemaRepository cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }


        public IEnumerable<Cinema> Search(string query)
        {
            return _cinemaRepository.Search(query);
        }

        public PaginationDto<IEnumerable<CinemaDto>> GetPaginatedCinemas(string query, int page, int pageSize)
        {
            // Récupérer les cinémas paginés depuis le repository
            var (cinemas, totalCount) = _cinemaRepository.GetCinemas(query, page, pageSize);

            // Mapper chaque Cinema vers CinemaDto
            var cinemaDtos = cinemas.Select(c => new CinemaDto
            {
                Id = c.Id,
                Nom = c.Nom,
                Ville = c.Ville,
                Rue = c.Rue,
                Numero = c.Numero
            }).ToList();  // Convertir en liste

            // Créer et retourner le PaginationDto avec les CinemaDto
            var paginationDto = new PaginationDto<IEnumerable<CinemaDto>>
            {
                Page = page,
                Total = totalCount,
                Data = cinemaDtos  // Utiliser la liste de CinemaDto
            };

            return paginationDto;
        }

        public CinemaDto GetCinemaById(int id)
        {
            // Récupérer l'entité Cinema par son Id
            var cinema = _cinemaRepository.GetCinemaById(id);

            if (cinema == null)
            {
                return null;  // Ou lancer une exception si nécessaire
            }

            // Convertir Cinema en CinemaDto
            var cinemaDto = new CinemaDto
            {
                Id = cinema.Id,
                Nom = cinema.Nom,
                Ville = cinema.Ville,
                Rue = cinema.Rue,
                Numero = cinema.Numero
            };

            return cinemaDto;
        }



    }
}
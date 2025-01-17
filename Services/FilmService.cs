﻿using ReservationCinema.Dto;
using ReservationCinema.Models;
using ReservationCinema.Repositories;

namespace ReservationCinema.Services
{
    public class FilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public PaginationDto<List<FilmDto>> SearchFilms(string query, int page)
        {
            // Récupère les films paginés depuis le repository
            var paginatedFilms = _filmRepository.SearchFilms(query, page);

            // Transforme les films en FilmDto
            var filmDtos = paginatedFilms.Data.Select(f => new FilmDto
            {
                Titre = f.Titre,
                Genre = f.Genre,
                Annee = f.Annee.ToString("yyyy"),
                //ImageUrl = f.ImageUrl,
                Seances = f.Seances.Select(s => new SeanceDto
                {
                    CinemaNom = s.Salle.Cinema.Nom,
                    Ville = s.Salle.Cinema.Ville,
                    SalleId = s.Salle.Id,
                    HeureDebut = s.Horaire.HeureDebut.ToString("HH:mm"),
                    HeureFin = s.Horaire.HeureFin.ToString("HH:mm"),
                    Tarif = s.Tarif
                }).ToList()
            }).ToList();

            // Retourne les films paginés avec les DTO
            return new PaginationDto<List<FilmDto>>
            {
                Page = paginatedFilms.Page,
                Total = paginatedFilms.Total,
                Data = filmDtos
            };
        }

        public IEnumerable<FilmDto> GetFilmsForCinemaToday(int cinemaId)
        {
            // Récupérer les films pour le cinéma spécifié
            var films = _filmRepository.GetFilmsForCinemaToday(cinemaId);

            // Mapper les films et leurs séances en DTOs
            return films.Select(f => new FilmDto
            {
                Titre = f.Titre,
                Genre = f.Genre,
                Annee = f.Annee.ToString("yyyy"),  // Pour conversion date -> string
                Seances = f.Seances
                            .Where(s => s.Salle.CinemaId == cinemaId && s.Horaire.HeureDebut.Date == DateTime.Today)
                            .Select(s => new SeanceDto
                            {
                                CinemaNom = s.Salle.Cinema.Nom,
                                Ville = s.Salle.Cinema.Ville,
                                SalleId = s.Salle.Id,
                                HeureDebut = s.Horaire.HeureDebut.ToString("yyyy-MM-dd HH:mm"), // Pour conversion date -> string
                                HeureFin = s.Horaire.HeureFin.ToString("yyyy-MM-dd HH:mm"),
                                Tarif = s.Tarif
                            })
                            .ToList()
            }).ToList();
        }


    }
}

namespace ReservationCinema.Dto
{
    public class SeanceDto
    {
        public SalleDto Salle { get; set; }
        public HoraireDto Horaire { get; set; }
        public float Tarif { get; set; }
    }
}

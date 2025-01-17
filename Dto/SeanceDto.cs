namespace ReservationCinema.Dto
{
    public class SeanceDto
    {
        public string CinemaNom { get; set; }
        public string Ville { get; set; }
        public int SalleId { get; set; }
        public string HeureDebut { get; set; }
        public string HeureFin { get; set; }
        public float Tarif { get; set; }
    }
}

namespace ReservationCinema.Dto
{
    public class PaginationDto<T>
    {
        public required int Page { get; set; }
        public required int Total { get; set; }
        public required T Data { get; set; }
    }
}

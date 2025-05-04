namespace Anamnese.API.ORM.Filters
{
    public class PacientFilter
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Uf { get; set; }
        public string? Gender { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

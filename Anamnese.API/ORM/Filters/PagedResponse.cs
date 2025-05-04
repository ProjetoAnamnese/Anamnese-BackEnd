namespace Anamnese.API.ORM.Filters
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PerPage { get; set; }
    }

}

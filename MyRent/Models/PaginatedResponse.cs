namespace MyRent.Models
{
    public class PaginatedResponse<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int TotalItems { get; set; } = 0;
    }
}

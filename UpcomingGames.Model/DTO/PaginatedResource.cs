namespace UpcomingGamesBackend.Model.DTO;

public class PaginatedResource<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public T Data { get; set; }
}
namespace vault_gps.Application.DTOs;

public class PaginatedResponse<T>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();
}


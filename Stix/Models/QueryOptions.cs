namespace Stix.Models;


// WIP
public class QueryOptions
{
    public IList<FilterBy>? Filter { get; set; } = new List<FilterBy>();
    public SortBy? SortBy { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
}

public class FilterBy
{
    public string? Property { get; set; }
    public object? Value { get; set; }
}

public class SortBy
{
    public string? Property{ get; set; }
    public SortOrder Order { get; set; }
}


public enum SortOrder
{
    ASC,
    DESC
}
namespace Domain;

public class Quote
{
    public int Id { get; set; }
    public string? Author { get; set; }
    public string? Quotetext { get; set; }
    public int CategoryId { get; set; }
}

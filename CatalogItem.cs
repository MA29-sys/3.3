namespace katalog;

public class CatalogItem
{
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Rating { get; set; }

    public override string ToString()
    {
        return Title;
    }
}

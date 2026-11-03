using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace katalog;

public static class CatalogRepository
{
    public static List<CatalogItem> LoadItems()
    {
        var path = GetDataPath();

        if (!File.Exists(path))
        {
            return new List<CatalogItem>();
        }

        var items = new List<CatalogItem>();

        foreach (var line in File.ReadAllLines(path, Encoding.UTF8))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split(';');
            if (parts.Length != 3)
            {
                continue;
            }

            var title = parts[0].Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                continue;
            }

            if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var year))
            {
                continue;
            }

            if (!int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var rating))
            {
                continue;
            }

            items.Add(new CatalogItem { Title = title, Year = year, Rating = rating });
        }

        return items;
    }

    public static void SaveItems(List<CatalogItem> items)
    {
        var path = GetDataPath();
        var directory = Path.GetDirectoryName(path);

        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var content = string.Join(
            Environment.NewLine,
            items.Select(item => $"{item.Title};{item.Year};{item.Rating}"));

        File.WriteAllText(path, content + (items.Count > 0 ? Environment.NewLine : string.Empty), Encoding.UTF8);
    }

    private static string GetDataPath()
    {
        var current = AppContext.BaseDirectory;

        while (!string.IsNullOrEmpty(current))
        {
            var candidate = Path.Combine(current, "dane", "pozycje.txt");
            if (File.Exists(candidate))
            {
                return candidate;
            }

            var parent = Directory.GetParent(current);
            if (parent is null)
            {
                break;
            }

            current = parent.FullName;
        }

        return Path.Combine(AppContext.BaseDirectory, "dane", "pozycje.txt");
    }
}

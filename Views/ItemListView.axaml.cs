using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class ItemListView : UserControl
{
    public event Action<CatalogItem>? ItemSelected;
    public event Action? AddItemRequested;

    public ItemListView()
    {
        InitializeComponent();
    }

    public void LoadItems(IEnumerable<CatalogItem> items)
    {
        ItemListPanel.Children.Clear();

        foreach (var item in items)
        {
            var button = new Button
            {
                Content = $"{item.Title} ({item.Year}) — ocena: {item.Rating}/10",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 0, 0, 4),
                Padding = new Thickness(12)
            };

            button.Click += (_, _) => ItemSelected?.Invoke(item);
            ItemListPanel.Children.Add(button);
        }

        if (!ItemListPanel.Children.OfType<Button>().Any())
        {
            var emptyText = new TextBlock
            {
                Text = "Brak pozycji w katalogu.",
                Foreground = Avalonia.Media.Brushes.Gray,
                FontSize = 16,
                Margin = new Thickness(0, 12, 0, 0)
            };

            ItemListPanel.Children.Add(emptyText);
        }
    }

    private void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        AddItemRequested?.Invoke();
    }
}

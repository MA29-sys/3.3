using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class ItemDetailsView : UserControl
{
    public event Action? GoBackRequested;
    public event Action? AddItemRequested;

    public ItemDetailsView()
    {
        InitializeComponent();
    }

    public void LoadItem(CatalogItem item)
    {
        TitleText.Text = item.Title;
        YearText.Text = $"Rok: {item.Year}";
        RatingText.Text = $"Ocena: {item.Rating}/10";
        DescriptionText.Text = $"Pozycja {item.Title} znajduje się w katalogu.";
    }

    private void BackButton_Click(object? sender, RoutedEventArgs e)
    {
        GoBackRequested?.Invoke();
    }

    private void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        AddItemRequested?.Invoke();
    }
}

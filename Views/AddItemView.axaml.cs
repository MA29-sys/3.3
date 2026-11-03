using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace katalog;

public partial class AddItemView : UserControl
{
    public event Action<CatalogItem>? SaveRequested;
    public event Action? CancelRequested;

    public AddItemView()
    {
        InitializeComponent();
        Reset();
    }

    public void Reset()
    {
        TitleTextBox.Text = string.Empty;
        YearTextBox.Text = string.Empty;
        RatingTextBox.Text = string.Empty;
        ErrorText.Text = string.Empty;
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var title = TitleTextBox.Text?.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            ErrorText.Text = "Tytuł nie może być pusty.";
            return;
        }

        if (!int.TryParse(YearTextBox.Text, out var year) || year < 0)
        {
            ErrorText.Text = "Podaj poprawny rok wydania.";
            return;
        }

        if (!int.TryParse(RatingTextBox.Text, out var rating) || rating < 1 || rating > 10)
        {
            ErrorText.Text = "Ocena musi być liczbą od 1 do 10.";
            return;
        }

        SaveRequested?.Invoke(new CatalogItem { Title = title, Year = year, Rating = rating });
        Reset();
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        Reset();
        CancelRequested?.Invoke();
    }
}

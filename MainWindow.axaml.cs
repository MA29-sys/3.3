using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace katalog;

public partial class MainWindow : Window
{
    private readonly List<CatalogItem> _items;
    private readonly ItemListView _itemListView;
    private readonly ItemDetailsView _itemDetailsView;
    private readonly AddItemView _addItemView;

    public MainWindow()
    {
        InitializeComponent();

        _items = CatalogRepository.LoadItems();
        _itemListView = new ItemListView();
        _itemDetailsView = new ItemDetailsView();
        _addItemView = new AddItemView();

        _itemListView.ItemSelected += ShowItemDetails;
        _itemListView.AddItemRequested += ShowAddForm;
        _itemDetailsView.GoBackRequested += ShowList;
        _itemDetailsView.AddItemRequested += ShowAddForm;
        _addItemView.SaveRequested += SaveNewItem;
        _addItemView.CancelRequested += ShowList;

        ShowList();
    }

    private void ShowList()
    {
        _itemListView.LoadItems(_items);
        MainContent.Content = _itemListView;
    }

    private void ShowItemDetails(CatalogItem item)
    {
        _itemDetailsView.LoadItem(item);
        MainContent.Content = _itemDetailsView;
    }

    private void ShowAddForm()
    {
        _addItemView.Reset();
        MainContent.Content = _addItemView;
    }

    private void SaveNewItem(CatalogItem item)
    {
        _items.Add(item);
        CatalogRepository.SaveItems(_items);
        ShowList();
    }
}
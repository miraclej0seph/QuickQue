using System.Collections.ObjectModel;
using QuickQue.Models;
using QuickQue.Services;

namespace QuickQue.Views;

public partial class History : ContentPage
{
    private readonly ObservableCollection<QueueHistoryItem> historyItems = [];

    public History()
    {
        InitializeComponent();
        HistoryList.ItemsSource = historyItems;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ReloadHistory();
    }

    private void ReloadHistory()
    {
        historyItems.Clear();

        foreach (QueueHistoryItem item in QueueHistoryStore.GetAll())
        {
            historyItems.Add(item);
        }
    }
}

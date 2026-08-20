using System.Text.Json;
using QuickQue.Models;

namespace QuickQue.Services;

public static class QueueHistoryStore
{
    private const string HistoryKey = "queue_calculation_history_v1";
    private const int MaximumEntries = 100;

    public static IReadOnlyList<QueueHistoryItem> GetAll()
    {
        string json = Preferences.Default.Get(HistoryKey, string.Empty);

        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        try
        {
            return JsonSerializer.Deserialize<List<QueueHistoryItem>>(json) ?? [];
        }
        catch (JsonException)
        {
            return [];
        }
    }

    public static void Add(QueueHistoryItem item)
    {
        List<QueueHistoryItem> entries = GetAll().ToList();
        entries.Insert(0, item);

        if (entries.Count > MaximumEntries)
        {
            entries.RemoveRange(MaximumEntries, entries.Count - MaximumEntries);
        }

        Preferences.Default.Set(HistoryKey, JsonSerializer.Serialize(entries));
    }
}

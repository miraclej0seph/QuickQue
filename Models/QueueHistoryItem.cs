namespace QuickQue.Models;

public sealed class QueueHistoryItem
{
    public string CafeName { get; set; } = string.Empty;

    public int PeopleAhead { get; set; }

    public int WaitTimeMinutes { get; set; }

    public DateTime EstimatedReadyAt { get; set; }

    public DateTime CalculatedAt { get; set; }
}

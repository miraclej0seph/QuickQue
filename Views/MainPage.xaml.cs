using QuickQue.Models;
using QuickQue.Services;

namespace QuickQue.Views;

public partial class MainPage : ContentPage
{
    private readonly Dictionary<string, int> cafeSpeeds = new()
    {
        { "Main Campus Café", 2 },     // fast
        { "Library Café", 3 },       // medium
        { "Student Hub Café", 4 },    // slow
    };

    public MainPage()
    {
        InitializeComponent();

        CafePicker.ItemsSource = cafeSpeeds.Keys.ToList();
        ResultsCard.IsVisible = false;
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        // SAFELY read selected café
        string? cafe = CafePicker.SelectedItem as string;

        if (cafe is null)
        {
            WaitTimeLabel.Text = "Please select a café.";
            return;
        }

        // SAFELY read number of people
        if (!int.TryParse(QueueEntry.Text, out int people) || people < 0)
        {
            WaitTimeLabel.Text = "Enter a valid number.";
            return;
        }

        // SAFELY get café speed
        if (!cafeSpeeds.TryGetValue(cafe, out int speed))
        {
            WaitTimeLabel.Text = "Invalid café selected.";
            return;
        }

        // CALCULATE WAIT TIME
        int waitTime = people * speed;
        DateTime calculatedAt = DateTime.Now;
        DateTime estimatedReadyAt = calculatedAt.AddMinutes(waitTime);

        ResultsCard.IsVisible = true;

        WaitTimeLabel.Text = $"Estimated wait time: {waitTime} minutes";
        StatusLabel.Text = $"Queue size: {people}";
        RecommendationLabel.Text = waitTime > 20
            ? "Recommendation: Try another café!"
            : "You're good to go!";
        CompletionTimeLabel.Text = $"Ready by: {estimatedReadyAt:h:mm tt}";

        QueueProgress.Progress = Math.Min(waitTime / 30.0, 1.0);

        QueueHistoryStore.Add(new QueueHistoryItem
        {
            CafeName = cafe,
            PeopleAhead = people,
            WaitTimeMinutes = waitTime,
            EstimatedReadyAt = estimatedReadyAt,
            CalculatedAt = calculatedAt
        });
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        CafePicker.SelectedItem = null;
        QueueEntry.Text = "";
        WaitTimeLabel.Text = "";
        StatusLabel.Text = "";
        RecommendationLabel.Text = "";
        CompletionTimeLabel.Text = "";
        QueueProgress.Progress = 0;
        ResultsCard.IsVisible = false;
    }
}

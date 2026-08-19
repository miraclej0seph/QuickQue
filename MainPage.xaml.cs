namespace QuickQue;

public partial class MainPage : ContentPage
{
    Dictionary<string, int> cafeSpeeds = new()
    {
        { "Starbrew", 2 },     // fast
        { "BeanCo", 3 },       // medium
        { "LatteLand", 4 },    // slow
        { "BrewHub", 5 }       // very slow
    };

    public MainPage()
    {
        InitializeComponent();

        CafePicker.ItemsSource = cafeSpeeds.Keys.ToList();
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
        if (!int.TryParse(QueueEntry.Text, out int people))
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

        WaitTimeLabel.Text = $"Estimated wait time: {waitTime} minutes";
        StatusLabel.Text = $"Queue size: {people}";
        RecommendationLabel.Text = waitTime > 20
            ? "Recommendation: Try another café!"
            : "You're good to go!";
        CompletionTimeLabel.Text = $"Ready by: {DateTime.Now.AddMinutes(waitTime):h:mm tt}";

        QueueProgress.Progress = Math.Min(waitTime / 30.0, 1.0);
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
    }
}

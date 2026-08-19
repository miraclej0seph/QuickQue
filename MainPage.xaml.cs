namespace QuickQue
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Add café options
            CafePicker.Items.Add("Main Campus Café");
            CafePicker.Items.Add("Library Café");
            CafePicker.Items.Add("Student Hub Café");
        }

        private void OnCalculateClicked(object sender, EventArgs e)
        {
            // Validation
            if (CafePicker.SelectedIndex == -1 || string.IsNullOrWhiteSpace(QueueEntry.Text))
            {
                WaitTimeLabel.Text = "Please select a café and enter people.";
                return;
            }

            if (!int.TryParse(QueueEntry.Text, out int people) || people < 0)
            {
                WaitTimeLabel.Text = "Enter a valid non-negative number.";
                return;
            }

            // Service time per café
            double serviceTimePerPerson = CafePicker.SelectedIndex switch
            {
                0 => 2.0,   // Main Campus
                1 => 3.0,   // Library
                2 => 1.5,   // Student Hub
                _ => 2.0
            };

            // Calculate wait time
            double waitTime = people * serviceTimePerPerson;
            WaitTimeLabel.Text = $"Estimated wait: {waitTime} minutes";

            UpdateStatusAndRecommendation(people);
            UpdateCompletionTime(waitTime);
            UpdateProgress(people);
        }

        private void UpdateStatusAndRecommendation(int people)
        {
            if (people <= 3)
            {
                StatusLabel.Text = "Quiet";
                StatusLabel.TextColor = Colors.Green;
                RecommendationLabel.Text = "Great time to grab something!";
            }
            else if (people <= 7)
            {
                StatusLabel.Text = "Moderate";
                StatusLabel.TextColor = Colors.Orange;
                RecommendationLabel.Text = "The wait should be manageable.";
            }
            else if (people <= 10)
            {
                StatusLabel.Text = "Busy";
                StatusLabel.TextColor = Colors.Red;
                RecommendationLabel.Text = "You may want to come back later.";
            }
            else
            {
                StatusLabel.Text = "Very Busy";
                StatusLabel.TextColor = Colors.DarkRed;
                RecommendationLabel.Text = "Consider trying another café.";
            }
        }

        private void UpdateCompletionTime(double waitTime)
        {
            var completionTime = DateTime.Now.AddMinutes(waitTime);
            CompletionTimeLabel.Text = $"You’ll reach the front at {completionTime:hh:mm tt}";
        }

        private void UpdateProgress(int people)
        {
            QueueProgress.Progress = Math.Min(people / 10.0, 1.0);
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            CafePicker.SelectedIndex = -1;
            QueueEntry.Text = string.Empty;
            WaitTimeLabel.Text = string.Empty;
            StatusLabel.Text = string.Empty;
            RecommendationLabel.Text = string.Empty;
            CompletionTimeLabel.Text = string.Empty;
            QueueProgress.Progress = 0;
        }
    }
}

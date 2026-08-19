namespace QuickQue
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        // This method returns the service time based on the selected cafe option.
        private double GetCafeServiceTime()
        {
            return CafePicker.SelectedIndex switch
            {
                0 => 2.0,
                1 => 3.0,
                2 => 1.5,
                _ => 0
            };
        }
    }
}

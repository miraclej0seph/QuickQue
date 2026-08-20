namespace QuickQue
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
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

        // This method returns a recommendation based on the number of people waiting.
        private string GetRecommendation(int peopleWaiting)
        {
            if (peopleWaiting <= 3)
            {
                return "Great time to grab something!";
            }
            else if (peopleWaiting <= 7)
            {
                return "The wait should be manageable.";
            }
            else if (peopleWaiting <= 10)
            {
                return "You may want to come back later.";
            }
            else
            {
                return "Consider trying another café.";
            }
        }

        // This method changes the image displayed based on the selected cafe option.
        private void CafePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CafePicker.SelectedIndex)
            {
                case 0:
                    CafeImage.Source = "maincafe.jpg";
                    break;

                case 1:
                    CafeImage.Source = "librarycafe.png";
                    break;

                case 2:
                    CafeImage.Source = "studenthubcafe.jpg";
                    break;
            }
        }
    }
}

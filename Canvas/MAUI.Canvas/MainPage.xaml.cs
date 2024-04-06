namespace MAUI.Canvas
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void InstructorViewClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Instructor");
        }

        private void StudentViewClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Student");
        }

        

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }

}

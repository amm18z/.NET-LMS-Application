namespace MAUI.Canvas
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void PeopleViewClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//People");
        }

        private void CoursesViewClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Courses");
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

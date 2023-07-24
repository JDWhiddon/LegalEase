using PracticeManagement.MAUI.ViewModels;

namespace PracticeManagement.MAUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Delete();
        }

        private void SearchClicked(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).Search();
        }
        
        private void ClientsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Clients");
        }

        private void ProjectsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Projects");
        }

        private void EmployeesClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Employees");
        }
        private void TimeClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Times");
        }
    }
}
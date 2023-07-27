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

      

     
        
        private void ClientsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Clients");
        }

      
        private void EmployeesClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Employees");
        }
        
        
    }
}
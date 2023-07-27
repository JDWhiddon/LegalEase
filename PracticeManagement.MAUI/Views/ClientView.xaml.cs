using PracticeManagement.CLI.Models;
using PracticeManagement.MAUI.ViewModels;

namespace PracticeManagement.MAUI.Views;


public partial class ClientView : ContentPage
{

    public ClientView()
	{
		InitializeComponent();
        BindingContext = new MainViewModel();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
       (BindingContext as MainViewModel).RefreshClients();
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClientClicked(object sender, EventArgs e)
    {
        (BindingContext as MainViewModel).ToggleAddingClient();
    }

    private void SaveClicked(object sender, EventArgs e)
    {
        (BindingContext as MainViewModel).ExecuteAddClient();
        (BindingContext as MainViewModel).ToggleAddingClient();
    }




    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as MainViewModel).RefreshClients();
    }
}

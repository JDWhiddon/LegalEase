using PracticeManagement.CLI.Models;
using PracticeManagement.Library.Services;
using PracticeManagement.MAUI.ViewModels;

namespace PracticeManagement.MAUI.Views;

[QueryProperty(nameof(ClientId), "clientId")]



public partial class ClientDetailsView : ContentPage
{
    public int ClientId { get; set; }

    public bool IsAddingProject { get; set; }

    public ClientDetailsView()
    {
        InitializeComponent();
        BindingContext = new ClientViewModel();
        IsAddingProject = false;
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).AddOrUpdate();
        Shell.Current.GoToAsync("//Clients");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Clients");
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ClientViewModel(ClientId);
        (BindingContext as ClientViewModel).RefreshProjects();
    }

    private void AddProjectClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).ToggleAddingProject();
        //   Shell.Current.GoToAsync($"//ProjectDetails?clientId={ClientId}");
    }

    private void DeleteProjectClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).RefreshProjects();
    }

    private void SaveClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).ExecuteAddProject();
        (BindingContext as ClientViewModel).ToggleAddingProject();
    }
    private void DeleteBillClicked(object sender, EventArgs e)
    {
        (BindingContext as ClientViewModel).RefreshBills();
    }

}
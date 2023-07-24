using PracticeManagement.CLI.Models;
using PracticeManagement.Library.Services;
using PracticeManagement.MAUI.ViewModels;

namespace PracticeManagement.MAUI.Views;

[QueryProperty(nameof(ClientId), "clientId")]
[QueryProperty(nameof(ProjectId), "projectId")]

public partial class ProjectDetailView : ContentPage
{
    public int ClientId { get; set; }
    public int ProjectId { get; set; }
    public ProjectDetailView()
    {
        InitializeComponent();
    }


    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        if (ClientId != 0)
        {
            BindingContext = new ProjectViewModel(ClientId);
        }
        if (ProjectId != 0)  
        {
            BindingContext = new ProjectViewModel(ProjectId, true);
        }
        (BindingContext as ProjectViewModel).RefreshTimes();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Clients");
    }

    private void GenerateBill(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel).GenerateBill();
        var clientId = ClientService.Current.Get(ProjectId);
        Shell.Current.GoToAsync($"//ClientDetails?clientId={clientId}");
    }
}
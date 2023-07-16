using PracticeManagement.CLI.Models;
using PracticeManagement.Library.Services;
using PracticeManagement.MAUI.ViewModels;

namespace PracticeManagement.MAUI.Views;

[QueryProperty(nameof(EmployeeId), "employeeId")]

public partial class EmployeeDetailView : ContentPage
{
	public EmployeeDetailView()
	{
		InitializeComponent();
		BindingContext = new EmployeeViewModel();
    }
    public int EmployeeId { get; set; }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Employees");
    }
    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).AddOrUpdate();
        Shell.Current.GoToAsync("//Employees");
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new EmployeeViewModel(EmployeeId);
    }
}
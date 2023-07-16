using PracticeManagement.MAUI.ViewModels;

namespace PracticeManagement.MAUI.Views;

public partial class EmployeeView : ContentPage
{
    public EmployeeView()
    {
        InitializeComponent();
        BindingContext = new EmployeeViewModel();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).Delete();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).Search();
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//EmployeeDetails");
    }
    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as EmployeeViewModel).Edit();
    }

    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as EmployeeViewModel).RefreshEmployeeList();
    }
}
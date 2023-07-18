using PracticeManagement.MAUI.ViewModels;

namespace PracticeManagement.MAUI.Views;


public partial class TimerView : ContentPage
{
	public int projectId { get; set; }
    public TimerView(int projectId)
	{
        InitializeComponent();
		BindingContext = new TimerViewModel(projectId);
	}

    private void EnterTimeClicked(object sender, EventArgs e)
    {
        decimal hours = (BindingContext as TimerViewModel).CalculateTime();
        Shell.Current.GoToAsync($"//TimeEntry?projectId={(BindingContext as TimerViewModel)?.Project.Id}&time={hours}");
        Application.Current?.CloseWindow(Window);
    }
}
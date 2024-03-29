using PracticeManagement.CLI.Models;
using PracticeManagement.MAUI.ViewModels;

namespace PracticeManagement.MAUI.Views;

[QueryProperty(nameof(Time), "time")]
[QueryProperty(nameof(ProjectId), "projectId")]
[QueryProperty(nameof(TimeId), "timeId")]

public partial class TimeEntryView : ContentPage
{
    public decimal Time { get; set; }
    public int ProjectId { get; set; }
    public int TimeId { get; set; }
    public TimeEntryView()
	{
		InitializeComponent();

	}    
    private void OnArrived(object sender, NavigatedToEventArgs e)
    {
        if(TimeId != 0)
        {
            BindingContext = new TimeEntryViewModel(TimeId);
        }
        else
        {
            BindingContext = new TimeEntryViewModel(ProjectId, Time);
        }
    }


}
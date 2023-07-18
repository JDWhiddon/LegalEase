using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PracticeManagement.Library.Services;
using PracticeManagement.CLI.Models;
using PracticeManagement.MAUI.Views;
using System.Windows.Input;
using PracticeManagement.Library.Models;

namespace PracticeManagement.MAUI.ViewModels
{
    public class ProjectViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Project Model { get; set; }
        public TimeEntryViewModel SelectedTime { get; set; }

        public ObservableCollection<TimeEntryViewModel> Times
        {
            get
            {
                if (Model == null || Model.Id == 0)
                {
                    return new ObservableCollection<TimeEntryViewModel>();
                }
                return new ObservableCollection<TimeEntryViewModel>(TimeService
                    .Current.ListOfTimes.Where(p => p.ProjectId == Model.Id)
                    .Select(r => new TimeEntryViewModel(r)));
            }
        }

        public ICommand AddOrUpdateCommand { get; private set; }
        public ICommand TimerCommand { get; private set; }
        public ICommand EditEntryCommand { get; private set; }
        public ICommand DeleteEntryCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand ToggleProjectStatusCommand { get; private set; }

        public string Display
        {
            get
            {
                return Model.ToString();
            }
        }
        private void ExecuteEdit() 
        {
            ProjectService.Current.AddOrUpdate(Model);
            Shell.Current.GoToAsync($"//ClientDetails?clientId={Model.ClientId}");
        }
        private void ExecuteAdd()
        {
            ProjectService.Current.AddOrUpdate(Model);
            Shell.Current.GoToAsync($"//ClientDetails?clientId={Model.ClientId}");
        }

        private void ExecuteTimer()
        {
            var window = new Window(new TimerView(Model.Id)) {
                Width = 250,
                Height = 350,
                X = 0,
                Y = 0
            };
            Application.Current.OpenWindow(window);
        }

        private void ExecuteEditEntry()
        {
            if (SelectedTime == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//TimeEntry?timeId={SelectedTime.Model.Id}");
        }

        private void ExecuteDeleteEntry()
        {
                if (SelectedTime == null)
                {
                    return;
                }
            TimeService.Current.Delete(SelectedTime.Model.Id);
            NotifyPropertyChanged("Times");
            Shell.Current.GoToAsync($"//ProjectDetails?projectId={Model.Id}");
        }



        private void ExecuteToggleProjectStatus()
        {
            ProjectService.Current.ExecuteToggleProjectStatus(Model);
            Shell.Current.GoToAsync($"//ClientDetails?clientId={Model.ClientId}");
        }

        public void SetUpCommands()
        {
            AddOrUpdateCommand = new Command(ExecuteAdd);
            TimerCommand = new Command(ExecuteTimer);
            ToggleProjectStatusCommand = new Command(ExecuteToggleProjectStatus);
            EditEntryCommand = new Command(ExecuteEditEntry);
            DeleteEntryCommand = new Command(ExecuteDeleteEntry);
        }
        
        public ProjectViewModel(int clientId)
        { 
            Model = new Project { ClientId = clientId };
            SetUpCommands();
        }

        public ProjectViewModel(int projectId, bool isProjectId)
        {
            Project project = ProjectService.Current.Get(projectId);
            Model = project;
            AddOrUpdateCommand = new Command(ExecuteEdit);
            SetUpCommands();
        }

        public ProjectViewModel(Project model)
        {
            Model = model;
            AddOrUpdateCommand = new Command(ExecuteEdit);
            SetUpCommands();
        }

        public ProjectViewModel()
        {
            Model = new Project();
            SetUpCommands();
        }

        public void RefreshTimes()
        {
            NotifyPropertyChanged(nameof(Times));
        }

        public void AddTime(int employeeId, decimal rate)
        {

        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

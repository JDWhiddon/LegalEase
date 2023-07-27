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
using System.Diagnostics;
using System.Windows.Input;
using PracticeManagement.Library.Models;

namespace PracticeManagement.MAUI.ViewModels
{
    public class TimeEntryViewModel
    {
        public Time Model { get; set; }

        public ICommand AddOrUpdateCommand { get; private set; }
        public ICommand DeleteEntryCommand { get; private set; }
        public ICommand EditEntryCommand { get; private set; }



        public string Display
        {
            get
            {
                return Model.ToString();
            }
        }

        private void ExecuteEditEntry()
        {
            Shell.Current.GoToAsync($"//TimeEntry?timeId={Model.Id}");
        }


        private void ExecuteAdd()
        {
            TimeService.Current.AddOrUpdate(Model);
            Shell.Current.GoToAsync($"//ProjectDetails?projectId={Model.ProjectId}");
        }

        private void ExecuteDelete()
        {
            TimeService.Current.Delete(Model.Id);
            Shell.Current.GoToAsync($"//ProjectDetails?projectId={Model.Id}");
        }

        public void SetUpCommands()
        {
            AddOrUpdateCommand = new Command(ExecuteAdd);
            DeleteEntryCommand = new Command(ExecuteDelete);
            EditEntryCommand = new Command(ExecuteEditEntry);
        }

        public TimeEntryViewModel(int projectid, decimal hours)
        {
            Model = new Time { ProjectId = projectid, Hours = hours };

            SetUpCommands();
        }

        public TimeEntryViewModel(int timeId)
        {
            Model = TimeService.Current.Get(timeId);
            SetUpCommands();
        }

        public TimeEntryViewModel(Time model)
        {
            Model = model;
            SetUpCommands();
        }

        public TimeEntryViewModel()
        {
            Model = new Time();
            SetUpCommands();
        }

    }
}

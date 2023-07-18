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

namespace PracticeManagement.MAUI.ViewModels
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Project Project { get; set; }

        public string TimerDisplay
        {
            get
            {
                var time = stopwatch.Elapsed;
                var str = string.Format("{0:00}:{1:00}:{2:00}",
              time.Hours,
              time.Minutes,
              time.Seconds);
                return str;
            }
        }

        public string ProjectDisplay
        {
            get
            {
                return Project.LongName;
            }
        }
        
        private IDispatcherTimer timer { get; set; }
        private Stopwatch stopwatch { get; set; }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public void ExecuteStart()
        {
            stopwatch.Start();
            timer.Start();
        }

        public void ExecuteStop()
        {
            stopwatch.Stop();
        }

        public void SetupCommands()
        {
            StartCommand = new Command(ExecuteStart);
            StopCommand = new Command(ExecuteStop);
        }

        public decimal CalculateTime()
        {
            TimeSpan elapsed = stopwatch.Elapsed;
            decimal hours = (decimal)elapsed.TotalHours;
            decimal minutes = (decimal)elapsed.Minutes / 60;
            decimal seconds = (decimal)elapsed.Seconds;
            decimal total = seconds;
            //decimal total = hours + minutes;
            return Math.Round(total, 2);
        }

        public TimerViewModel(int projectId)
        {
            Project = ProjectService.Current.Get(projectId) ?? new Project();
            stopwatch = new Stopwatch();
            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.IsRepeating = true;

            timer.Tick += Timer_Tick;
            SetupCommands();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timer.IsRunning)
            {
                NotifyPropertyChanged(nameof(TimerDisplay));
            }

        }

        private void AddEntry(int employeeId)
        {
           
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

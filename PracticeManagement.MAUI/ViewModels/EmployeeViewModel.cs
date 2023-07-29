using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PracticeManagement.Library.Services;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.Models;
using System.Windows.Input;
using PracticeManagement.Library.DTO;

namespace PracticeManagement.MAUI.ViewModels
{
    internal class EmployeeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ObservableCollection<EmployeeDTO> Employees
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<EmployeeDTO>(EmployeeService.Current.ListOfEmployees);
                }
                return new ObservableCollection<EmployeeDTO>(EmployeeService.Current.Search(Query));
            }
        }

        public EmployeeDTO SelectedEmployee { get; set; }

        public EmployeeDTO Model { get; set; }
        public void Delete()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            EmployeeService.Current.Delete(SelectedEmployee.Id);
            NotifyPropertyChanged("Employees");
        }

        public string Query { get; set; }

        public void Search()
        {
            NotifyPropertyChanged("Employees");
        }

        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        public void ExecuteDelete(int id)
        {
            EmployeeService.Current.Delete(id);
        }

        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//EmployeeDetails?employeeId={id}");
        }
        public void Edit()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            ExecuteEdit(SelectedEmployee.Id);
            NotifyPropertyChanged("Employee");
        }
        public EmployeeViewModel()
        {
            Model = new EmployeeDTO();
            SetupCommands();
        }

        public EmployeeViewModel(int employeeId)
        {
          if (employeeId > 0)
            {
                Model = EmployeeService.Current.Get(employeeId);
            }
            else
            {
                Model = new EmployeeDTO();
            }

            SetupCommands();
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ClientViewModel).Model.Id));

            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientViewModel).Model.Id));
        }

        public void AddOrUpdate()
        {
            EmployeeService.Current.AddOrUpdate(Model);
            RefreshEmployeeList();
        }

        public void RefreshEmployeeList()
        {
            NotifyPropertyChanged("Employees");
        }

    }
}


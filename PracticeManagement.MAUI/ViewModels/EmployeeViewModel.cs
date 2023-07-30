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


        public ObservableCollection<EmployeeViewModel> Employees
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<EmployeeViewModel>(EmployeeService
                        .Current.ListOfEmployees
                        .Select(r => new EmployeeViewModel(r)));
                }
                List<EmployeeDTO> searchResults = EmployeeService.Current.Search(Query);
                return new ObservableCollection<EmployeeViewModel>(searchResults.Select(r => new EmployeeViewModel(r)));
            }
        }


        public Employee SelectedEmployee { get; set; }

        public EmployeeDTO Model { get; set; }

        public string Query { get; set; }

        public void Search()
        {
            NotifyPropertyChanged("Employees");
        }

        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand AddOrUpdateCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }

        public void ExecuteDelete()
        {
            EmployeeService.Current.Delete(Model.Id);
            EmployeeService.Current.RefreshEmployeeList();
            NotifyPropertyChanged("Employees");
        }

        public void RefreshEmployees()
        {
            NotifyPropertyChanged("Employees");
        }
        public void ExecuteAdd()
        {
            EmployeeDTO newEmployee = new EmployeeDTO()
            {
                Name = NewEmployeeName,
                Rate = NewEmployeeRate
            };
            EmployeeService.Current.AddOrUpdate(newEmployee);
            EmployeeService.Current.RefreshEmployeeList();
            NewEmployeeName = string.Empty;
            NewEmployeeRate = 0;
            SetupCommands();
        }

        public void ExecuteAddOrUpdate()
        {
            EmployeeService.Current.AddOrUpdate(Model);

        }

        public void ExecuteEdit()
        {
            Shell.Current.GoToAsync($"//EmployeeDetails?employeeId={Model.Id}");
            EmployeeService.Current.RefreshEmployeeList();

        }
        public void Edit()
        {
            if (SelectedEmployee == null)
            {
                return;
            }
        }
        public EmployeeViewModel()
        {
            Model = new EmployeeDTO();
            SetupCommands();
        }

        public EmployeeViewModel(EmployeeDTO dto)
        {
            Model = dto;
            SetupCommands();
        }

        public EmployeeViewModel(int id)
        {
            if (id >= 0)
            {
                Model = EmployeeService.Current.Get(id);
            }
            else
            {
                Model = new EmployeeDTO();
            }
        }
        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }



        public void SetupCommands()
        {
            EditCommand = new Command(ExecuteEdit);
            DeleteCommand = new Command(ExecuteDelete);
            AddOrUpdateCommand = new Command(ExecuteAddOrUpdate);
            SearchCommand = new Command(ExecuteSearch);
        }

        public void ExecuteSearch()
        {
            RefreshEmployeeList();
        }


        public void RefreshEmployeeList()
        {
            NotifyPropertyChanged("Employees");
        }

        public void ToggleAddingEmployee()
        {
            if (EntryIsVisible == true)
            {
                EntryIsVisible = false;
                RateIsVisible = false;
                AddIsVisible = true;
            }
            else if (EntryIsVisible == false)
            {
                EntryIsVisible = true;
                AddIsVisible = false;
                RateIsVisible = true;
            }

        }

        private bool rateIsVisible = false;
        private bool entryIsVisible = false;
        private bool addIsVisible = true;
        private string newEmployeeName = string.Empty;
        private decimal newEmployeeRate = 0;

        public string NewEmployeeName
        {
            get => newEmployeeName;
            set
            {
                if (newEmployeeName == value) return;
                newEmployeeName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewEmployeeName)));
            }
        }

        public decimal NewEmployeeRate
        {
            get =>newEmployeeRate;
            set
            {
                if (newEmployeeRate == value) return;
                newEmployeeRate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewEmployeeRate)));
            }
        }



        public bool EntryIsVisible
        {
            get => entryIsVisible;
            set
            {
                if (entryIsVisible == value) return;
                entryIsVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EntryIsVisible)));
            }
        }

        public bool RateIsVisible
        {
            get => rateIsVisible;
            set
            {
                if (rateIsVisible == value) return;
                rateIsVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RateIsVisible)));
            }
        }

        public bool AddIsVisible
        {
            get => addIsVisible;
            set
            {
                if (addIsVisible == value) return;
                addIsVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddIsVisible)));
            }
        }

    }
}


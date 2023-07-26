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
using System.Windows.Input;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Models;

namespace PracticeManagement.MAUI.ViewModels
{
    internal class ClientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ProjectViewModel SelectedProject { get; set; }
        public BillViewModel SelectedBill { get; set; }


        public ObservableCollection<ClientDTO> Clients
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<ClientDTO>(ClientService.Current.ListOfClients);
                }
                return new ObservableCollection<ClientDTO>(ClientService.Current.Search(Query));
            }
        }

        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                if (Model == null || Model.Id == 0)
                {
                    return new ObservableCollection<ProjectViewModel>();
                }
                return new ObservableCollection<ProjectViewModel>(ProjectService
                    .Current.ListOfProjects.Where(p => p.ClientId == Model.Id)
                    .Select(r => new ProjectViewModel(r)));
            }
        }

        public ObservableCollection<BillViewModel> Bills
        {
            get
            {
                if (Model == null || Model.Id == 0)
                {
                    return new ObservableCollection<BillViewModel>();
                }
                return new ObservableCollection<BillViewModel>(BillService
                    .Current.ListOfBills.Where(p => p.ClientId == Model.Id)
                    .Select(r => new BillViewModel(r)));
            }
        }


        public ClientDTO SelectedClient { get; set; }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Delete()
        {

            if (SelectedClient == null || ProjectService.Current.ListOfProjects.Any(p => p.ClientId == SelectedClient.Id && p.IsActive)) 
            {
                return;
            }
            ClientService.Current.Delete(SelectedClient.Id);
            NotifyPropertyChanged("Clients");
        }

        public void DeleteProject()
        {
            if (SelectedProject == null || SelectedProject.Model.IsActive == true)
            {
                return;
            }
            ProjectService.Current.Delete(SelectedProject.Model.Id);
            NotifyPropertyChanged("Projects");
        }

        public void DeleteBill()
        { 
            if (SelectedBill == null)
            {
                return;
            }
            BillService.Current.Delete(SelectedBill.Model.Id);
            NotifyPropertyChanged("Bills");
        }
        public void Edit()
        {
            if (SelectedClient == null)
            {
                return;
            }
            ExecuteEdit(SelectedClient.Id);
            NotifyPropertyChanged("Clients");
        }

        public void EditProject()
        {
            if (SelectedProject == null)
            {
                return;
            }
            ExecuteEditProject(SelectedProject);
            NotifyPropertyChanged("Projects");
        }

        public void ExecuteEditProject(ProjectViewModel _project)
        {
            Shell.Current.GoToAsync($"//ProjectDetails?projectId={_project.Model.Id}");
        }

        public void ShowProjects()
        {
            if (SelectedClient == null)
            {
                return;
            }
            ExecuteShowProjects(SelectedClient.Id);
            NotifyPropertyChanged("Clients");
        }

        public void ShowProjects(int ClientId)
        {
            ExecuteShowProjects(ClientId);
            NotifyPropertyChanged("Clients");
        }

        public void RefreshClientList()
        {
            NotifyPropertyChanged("Clients");
        }

        public string Query { get; set; }

        public void Search()
        {
            NotifyPropertyChanged("Clients");
        }
        public ClientDTO Model { get; set; }
        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }
        public void RefreshProjects()
        {
            NotifyPropertyChanged(nameof(Projects));
        }

        public void ToggleAddingProject()
        {
            if (IsVisible == true)
            {
                IsVisible = false;
                AddIsVisible = true;
            }
            else if (IsVisible == false)
            {
                IsVisible = true;
                AddIsVisible = false;
            }

        }

        private bool isVisible = false;
        private bool addIsVisible = true;
        private string newProjectName = string.Empty;

        public string NewProjectName
        {
            get => newProjectName;
            set
            {
                if (newProjectName == value) return;
                newProjectName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewProjectName)));
            }
        }
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (isVisible == value) return;
                isVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible)));
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

        public void RefreshBills()
        {
            NotifyPropertyChanged(nameof(Bills));
        }

        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand ShowProjectsCommand { get; private set; }
        public ICommand AddProjectCommand { get; private set; }

        public void ExecuteDelete(int id)
        {
            ClientService.Current.Delete(id);
        }
        public void ExecuteAddProject()
        {
            if (NewProjectName == string.Empty) { return; }
            Project _project = new Project
            {
                ClientId = Model.Id,
                LongName = NewProjectName
            };
            ProjectService.Current.AddOrUpdate(_project);
            NewProjectName = string.Empty;
            RefreshProjects();
        }

        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//ClientDetails?clientId={id}");
        }


        public void ExecuteShowProjects(int id)
        {
            Shell.Current.GoToAsync($"//Projects?clientId={id}");
        }


        private void SetupCommands()
        {
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ClientViewModel).Model.Id));

            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientViewModel).Model.Id));

            ShowProjectsCommand = new Command(
                (c) => ExecuteShowProjects((c as ClientViewModel).Model.Id));

            AddProjectCommand = new Command(
                (c) => ExecuteAddProject());

        }

        public ClientViewModel(ClientDTO client)
        {
            Model = client;
            SetupCommands();
        }

        public ClientViewModel(int clientId)
        {
            if (clientId > 0)
            {
                Model = ClientService.Current.Get(clientId);
            }
            else
            {
                Model = new ClientDTO();
            }

            SetupCommands();
        }



        public ClientViewModel()
        {
            Model = new ClientDTO();
            SetupCommands();
        }

        public void AddOrUpdate()
        {
            ClientService.Current.AddOrUpdate(Model);
        }
        
    }
}

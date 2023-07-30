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
using PracticeManagement.Library.DTO;
using System.Windows.Input;

namespace PracticeManagement.MAUI.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public ClientDTO Model { get; set; }

        public string Query { get; set; }

        public ObservableCollection<ClientViewModel> Clients
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<ClientViewModel>(ClientService
                        .Current.ListOfClients
                        .Select(r => new ClientViewModel(r)) );
                }
                List<ClientDTO> searchResults = ClientService.Current.Search(Query);
                return new ObservableCollection<ClientViewModel>(searchResults.Select(r => new ClientViewModel(r)));
            }
        }
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

        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand AddProjectCommand { get; private set; }
        public ICommand ShowProjectsCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ExecuteDelete(int id)
        {
            ClientService.Current.Delete(id);
        }

        public void ExecuteShowProjects(int id)
        {
            Shell.Current.GoToAsync($"//Projects?clientId={id}");
        }

        public void ExecuteEdit(int id)
        {
            Shell.Current.GoToAsync($"//ClientDetails?clientId={id}");
        }

        public void RefreshClients()
        {
            NotifyPropertyChanged(nameof(Clients));
        }

        public void ExecuteAddClient()
        {
            if (NewClientName == string.Empty) { return; }
            ClientDTO _client = new ClientDTO
            {
                Name = NewClientName
            };
            ClientService.Current.AddOrUpdate(_client);
            NewClientName = string.Empty;
            RefreshClients();
            SetupCommands();
        }
        public void ExecuteAddProject()
        {
            AddOrUpdate(); //save the client so that we have an id to link the project to
            //TODO: if we cancel the creation of this client, we need to delete it on cancel.
            Shell.Current.GoToAsync($"//ProjectDetails?clientId={Model.Id}");
        }
        public void Search()
        {
            NotifyPropertyChanged("Clients");
        }

        private void SetupCommands()
        {
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ClientViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientViewModel).Model.Id));
            ShowProjectsCommand = new Command(
                (c) => ExecuteShowProjects((c as ClientViewModel).Model.Id));
            SearchCommand = new Command(Search);
        }

        public void ToggleAddingClient()
        {
            if (EntryIsVisible == true)
            {
                EntryIsVisible = false;
                AddIsVisible = true;
            }
            else if (EntryIsVisible == false)
            {
                EntryIsVisible = true;
                AddIsVisible = false;
            }

        }

        private bool entryIsVisible = false;
        private bool addIsVisible = true;
        private string newClientName = string.Empty;

        public string NewClientName
        {
            get => newClientName;
            set
            {
                if (newClientName == value) return;
                newClientName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewClientName)));
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

        public MainViewModel(ClientDTO client)
        {
            Model = client;
            SetupCommands();
        }

        public MainViewModel(int clientId)
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

        public MainViewModel()
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

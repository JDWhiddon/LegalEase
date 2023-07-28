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

namespace PracticeManagement.MAUI.ViewModels
{
    public class ProjectViewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ClientDTO Client { get; set; }

        public Project SelectedProject { get; set; }
        public ObservableCollection<ProjectDTO> ListOfProjects
        {
            get
            {
                if (Client == null || Client.Id == 0)
                {
                    return new ObservableCollection<ProjectDTO>();
                }
                return new ObservableCollection<ProjectDTO>(ProjectService.Current.Search(Query));
            }
        }

        public ICommand DeleteCommand { get; private set; }


        public void Delete()
        {
            if (SelectedProject == null)
            {
                return;
            }
            ProjectService.Current.Delete(SelectedProject.Id);
            NotifyPropertyChanged("Projects");
        }

        public void ExecuteDelete(int id)
        {
            ProjectService.Current.Delete(id);
        }
        public string Query { get; set; }
        public ProjectViewViewModel(int clientId)
        {
            if (clientId >= 0)
            {
                Client = ClientService.Current.Get(clientId);
            }
            else
            {

                Client = new ClientDTO();
            }
        }

        public void Search()
        {
            NotifyPropertyChanged("Projects");
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

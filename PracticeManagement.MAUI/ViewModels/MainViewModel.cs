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

namespace PracticeManagement.MAUI.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCollection<ClientDTO> Clients
        {
            get {
                if (string.IsNullOrEmpty(Query))
                {
                    return new ObservableCollection<ClientDTO>(ClientService.Current.ListOfClients);   
                }
                return new ObservableCollection<ClientDTO>(ClientService.Current.Search(Query));
            }
        }

        public Client SelectedClient { get; set; }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Delete()
        {
            if (SelectedClient == null)
            {
                return;
            }
            ClientService.Current.Delete(SelectedClient.Id);
            NotifyPropertyChanged("Clients");
        }

        public string Query { get; set; }

        public void Search()
        {
            NotifyPropertyChanged("Clients");
        }

    }
}

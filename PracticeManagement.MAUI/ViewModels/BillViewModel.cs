using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.Models;
using PracticeManagement.Library.Services;

namespace PracticeManagement.MAUI.ViewModels
{
    public class BillViewModel : INotifyPropertyChanged
    {
        public BillViewModel()
        {
            Model = new Bill();
            SetUpCommands();

        }
        public BillViewModel(Bill bill)
        {
            Model = bill;
            SetUpCommands();
        }
        public string Display
        {
            get
            {
                return Model.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Bill Model { get; set; }

        public ObservableCollection<Bill> Bills
        {
            get
            {
                return new ObservableCollection<Bill>(BillService.Current.ListOfBills);
            }
        }

        public Bill SelectedBill { get; set; }


        public ICommand DeleteCommand { get; private set; }

        public void SetUpCommands()
        {
            DeleteCommand = new Command(ExecuteDelete);
        }
        public void ExecuteDelete()
        {
            BillService.Current.Delete(Model.Id);
            NotifyPropertyChanged("Bills");
        }
    }
}

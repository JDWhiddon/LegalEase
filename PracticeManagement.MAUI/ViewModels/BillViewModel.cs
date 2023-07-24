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
        }
        public BillViewModel(Bill bill)
        {
            Model = bill;
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

        public void Delete()
        {
            if (SelectedBill == null)
            {
                return;
            }
            BillService.Current.Delete(SelectedBill.Id);
            NotifyPropertyChanged("Bills");
        }

        public void ExecuteDelete(int id)
        {
            BillService.Current.Delete(id);
        }
    }
}

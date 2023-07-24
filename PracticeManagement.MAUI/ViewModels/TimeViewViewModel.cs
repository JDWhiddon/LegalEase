using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeManagement.Library.Models;
using PracticeManagement.Library.Services;

namespace PracticeManagement.MAUI.ViewModels
{
    public class TimeViewViewModel
    {
        public ObservableCollection<Time> ListOfTimes
        {
            get
            {
                return new ObservableCollection<Time>(TimeService.Current.ListOfTimes);
            }
        }
    }
}

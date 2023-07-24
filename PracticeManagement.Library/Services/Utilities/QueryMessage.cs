using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeManagement.Library.Services.Utilities
{
    public class QueryMessage
    {
        private string? query;
        public string Query
        {
            get => string.Empty;
            set
            {
                query = value;
            }
        }
    }
}

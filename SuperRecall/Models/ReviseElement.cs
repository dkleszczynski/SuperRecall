using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperRecall.Models
{
    [Serializable]
    public class ReviseElement : Element
    {
        public ObservableCollection<DateTime> ReviewDates { get; set; }
    }
}

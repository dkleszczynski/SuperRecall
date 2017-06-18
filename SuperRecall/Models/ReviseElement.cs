using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperRecall.Models
{
    [Serializable]
    public class ReviseElement : Element
    {
        public List<DateTime> ReviewDates { get; set; }
    }
}

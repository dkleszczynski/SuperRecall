using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperRecall.Models
{
    [Serializable]
    public class QueueElement : Element
    {
        public int LearningProgress { get; set; }
    }
}

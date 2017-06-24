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
        private int _learningProgress;

        public int LearningProgress
        {
            get { return _learningProgress; }
            set
            {
                _learningProgress = value;
                OnPropertyChanged("LearningProgress");
            }
        }
    }
}

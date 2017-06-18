using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperRecall.Models
{
    [Serializable]
    public abstract class Element
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string AnswerNote { get; set; }
        public string Group { get; set; }
        public bool IsSelected { get; set; }
        public string AudioPath { get; set; }
        public string ImagePath { get; set; }
        public DateTime AddingTime { get; set; }
    }
}

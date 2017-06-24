using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperRecall.Models
{
    [Serializable]
    public abstract class Element : INotifyPropertyChanged
    {
        private string _question;
        private string _answer;
        private string _group;
        private bool _isSelected;
        private string _audioPath;
        private string _imagePath;
        private DateTime _addingTime; 
               
        public event PropertyChangedEventHandler PropertyChanged;

        public string Question
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged("Question");
            }
        }

        public string Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged("Answer");
            }
        }

        public string Group
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged("Group");
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public string AudioPath
        {
            get { return _audioPath; }
            set
            {
                _audioPath = value;
                OnPropertyChanged("AudioPath");
            }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        public DateTime AddingTime
        {
            get { return _addingTime; }
            set
            {
                _addingTime = value;
                OnPropertyChanged("AddingTime");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

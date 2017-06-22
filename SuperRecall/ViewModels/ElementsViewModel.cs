using SuperRecall.Models;
using SuperRecall.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SuperRecall.ViewModels
{
    public class ElementsViewModel : INotifyPropertyChanged
    {
        private IElementsManagementService _elementsManagementService;
        private string _searchText;

        public ObservableCollection<Element> Elements { get; set; }
        public Element SelectedElement { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICollectionView ElementsView { get; private set; }

        public ICommand ShowOptionsMenuCommand { get; set; }
        public ICommand SearchBoxTextChangedCommand { get; set; }
                
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        public ElementsViewModel(IElementsManagementService elementsManagementService)
        {
            _elementsManagementService = elementsManagementService;
            Elements = new ObservableCollection<Element>(_elementsManagementService.LoadElements());
            ShowOptionsMenuCommand = new RelayCommand(ShowOptionsMenuExecute);
            SearchBoxTextChangedCommand = new RelayCommand(SearchBoxTextChangeExecute);

            ElementsView = CollectionViewSource.GetDefaultView(Elements);
            ElementsView.Filter = ElementsFilter;
        }

        private bool ElementsFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchText))
            {
                return true;
            }
            else
            {
                Element element = (Element)item;

                if (element.Question.Contains(SearchText) || element.Answer.Contains(SearchText))
                {
                    return true;
                }
            }

            return false;
        }

        public void ShowOptionsMenuExecute(Object sender)
        {
            ContextMenu menu;
            ResourceDictionary dictionary = new ResourceDictionary();
            dictionary.Source = new Uri("../Resources/ElementsViewResourceDictionary.xaml",
                                        UriKind.RelativeOrAbsolute);

            if (SelectedElement is QueueElement)
            {
                menu = (ContextMenu)dictionary["QueueMenu"];
            }
            else
            {
                menu = (ContextMenu)dictionary["ReviseMenu"];
            }

            menu.PlacementTarget = (Button)sender;
            menu.IsOpen = true;
        }

        public void SearchBoxTextChangeExecute(Object sender)
        {
            ElementsView.Refresh();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

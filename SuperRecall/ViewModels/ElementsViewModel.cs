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
using SuperRecall.GlobalEnums;

namespace SuperRecall.ViewModels
{
    public class ElementsViewModel : INotifyPropertyChanged
    {
        public const int ItemsPerPage = 2;

        private IElementsManagementService _elementsManagementService;
        private string _searchText;
        private int _currentPage;
        private int _pageCount;
        private List<KeyValuePair<SourceType, string>> _sourceList;
        private List<string> _groupList;
        private string _selectedGroup;
        private int _selectedGroupIndex;

        public ObservableCollection<Element> Elements { get; set; }
        public Element SelectedElement { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICollectionView ElementsView { get; private set; }
        public List<Element> ItemsInPage { get; private set; }
        public KeyValuePair<SourceType, string> SelectedSource { get; set; }
        public bool SelectedCheckBoxIsChecked { get; set; }
                
        public ICommand ShowOptionsMenuCommand { get; set; }
        public ICommand SearchBoxTextChangedCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand SourceSelectionChangedCommand { get; set; }
        public ICommand GroupSelectionChangedCommand { get; set; }
        public ICommand GroupListLoadedCommand { get; set; }
        public ICommand SelectedCheckBoxClickCommand { get; set; }
        public ICommand ElementSelectionClickCommand { get; set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;
                OnPropertyChanged("PageCount");
            }
        }

        public List<KeyValuePair<SourceType, string>> SourceList
        {
            get { return _sourceList; }
            set
            {
                _sourceList = value;
                OnPropertyChanged("SourceList");
            }
        }

        public List<string> GroupList
        {
            get { return _groupList; }
            set
            {
                _groupList = value;
                OnPropertyChanged("GroupList");
            }
        }

        public string SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                OnPropertyChanged("SelectedGroup");
            }
        }

        public int SelectedGroupIndex
        {
            get { return _selectedGroupIndex; }
            set
            {
                _selectedGroupIndex = value;
                OnPropertyChanged("SelectedGroupIndex");
            }
        }

        public ElementsViewModel(IElementsManagementService elementsManagementService)
        {
            _elementsManagementService = elementsManagementService;
            Elements = new ObservableCollection<Element>(_elementsManagementService.LoadElements());
            _groupList = new List<string>(_elementsManagementService.LoadGroups());
            _groupList.Insert(0, "all");
            
            ShowOptionsMenuCommand = new RelayCommand(ShowOptionsMenuExecute);
            SearchBoxTextChangedCommand = new RelayCommand(SearchBoxTextChangeExecute);
            PreviousPageCommand = new RelayCommand(PreviousPageExecute);
            NextPageCommand = new RelayCommand(NextPageExecute);
            SourceSelectionChangedCommand = new RelayCommand(SourceSelectionChangedExecute);
            GroupSelectionChangedCommand = new RelayCommand(GroupSelectionChangedExecute);
            GroupListLoadedCommand = new RelayCommand(GroupListLoadedExecute);
            SelectedCheckBoxClickCommand = new RelayCommand(SelectedCheckBoxClickExecute);
            ElementSelectionClickCommand = new RelayCommand(ElementSelectionClickExecute);

            SourcesListPrepare();

            ElementsView = CollectionViewSource.GetDefaultView(Elements);
            ElementsView.Filter = ElementsFilter;
            CurrentPage = 1;
            PagePrepare();
        }

        private bool ElementsFilter(object item)
        {
            Element element = (Element)item;

            if (SelectedSource.Key == SourceType.Queue)
            {
                if (element is ReviseElement)
                {
                    return false;
                }
            }

            if (SelectedSource.Key == SourceType.Revision)
            {
                if (element is QueueElement)
                {
                    return false;
                }
            }

            //If selected option for group is not all and the group name is different
            if (SelectedGroupIndex != 0 && SelectedGroup.Equals(element.Group) == false)
            {
                return false;
            }

            //If only selected elements should be displayed
            if (SelectedCheckBoxIsChecked == true && element.IsSelected == false)
            {
                return false;
            }

            if (String.IsNullOrEmpty(SearchText))
            {
                return true;
            }
            else
            {
                if (element.Question.Contains(SearchText) || element.Answer.Contains(SearchText))
                {
                    return true;
                }
            }

            return false;
        }

        private bool PagingFilter(object item)
        {
            Element element = (Element)item;

            return ElementsView.Contains(element) && ItemsInPage.Contains(element);
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
            ElementsView.Filter = ElementsFilter;
            CurrentPage = 1;
                        
            PagePrepare();
            ElementsView.Refresh();
        }

        public void PreviousPageExecute(Object sender)
        {
            ElementsView.Filter = ElementsFilter;
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
            else
            {
                CurrentPage = PageCount;
            }

            PagePrepare();
            ElementsView.Refresh();
        }

        public void NextPageExecute(Object sender)
        {
            ElementsView.Filter = ElementsFilter;
            if (CurrentPage + 1 <= PageCount)
            {
                CurrentPage++;
            }
            else
            {
                CurrentPage = 1;
            }

            PagePrepare();
            ElementsView.Refresh();
        }

        public void SourceSelectionChangedExecute(Object sender)
        {
            ElementsView.Filter = ElementsFilter;
            CurrentPage = 1;

            PagePrepare();
            ElementsView.Refresh();
        }

        public void GroupSelectionChangedExecute(Object sender)
        {
            ElementsView.Filter = ElementsFilter;
            CurrentPage = 1;

            PagePrepare();
            ElementsView.Refresh();
        }

        public void GroupListLoadedExecute(Object sender)
        {
            SelectedGroupIndex = 0;
        }

        public void SelectedCheckBoxClickExecute(Object sender)
        {
            ElementsView.Filter = ElementsFilter;
            CurrentPage = 1;

            PagePrepare();
            ElementsView.Refresh();
        }

        public void ElementSelectionClickExecute(Object sender)
        {
            SelectedElement.IsSelected = !SelectedElement.IsSelected;
            ElementsView.Filter = ElementsFilter;
            CurrentPage = 1;

            PagePrepare();
            ElementsView.Refresh();
        }

        private void PagePrepare()
        {
            int filteredCount = ElementsView.Cast<Element>().ToList().Count;
            PageCount = (int)Math.Ceiling((double)filteredCount / ItemsPerPage);

            if (PageCount == 0)
            {
                CurrentPage = 0;
                return;
            }

            int startIndex = (CurrentPage - 1) * ItemsPerPage;
            int currentIndex = 0;
            ItemsInPage = new List<Element>();

            foreach (Element element in ElementsView)
            {
                if (ItemsInPage.Count + 1 > ItemsPerPage)
                {
                    break;
                }

                if (currentIndex >= startIndex)
                {
                    ItemsInPage.Add(element);
                }

                currentIndex++;
            }

            //Refiltering
            ElementsView.Filter = PagingFilter;
        }
        
        private void SourcesListPrepare()
        {
            _sourceList = new List<KeyValuePair<SourceType, string>>()
            {
                new KeyValuePair<SourceType, string>(SourceType.Everywhere, "everywhere"),
                new KeyValuePair<SourceType, string>(SourceType.Queue, "queue"),
                new KeyValuePair<SourceType, string>(SourceType.Revision, "revision")
            };
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

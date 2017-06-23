﻿using SuperRecall.Models;
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
        private List<KeyValuePair<SourceType, string>> _sourcesList;

        public ObservableCollection<Element> Elements { get; set; }
        public Element SelectedElement { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICollectionView ElementsView { get; private set; }
        public List<Element> ItemsInPage { get; private set; }
        public KeyValuePair<SourceType, string> SelectedSource { get; set; }
        
        public ICommand ShowOptionsMenuCommand { get; set; }
        public ICommand SearchBoxTextChangedCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand SourceSelectedCommand { get; set; }

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

        public List<KeyValuePair<SourceType, string>> SourcesList
        {
            get { return _sourcesList; }
            set
            {
                _sourcesList = value;
                OnPropertyChanged("SourcesList");
            }
        }

        public ElementsViewModel(IElementsManagementService elementsManagementService)
        {
            _elementsManagementService = elementsManagementService;
            Elements = new ObservableCollection<Element>(_elementsManagementService.LoadElements());
            ShowOptionsMenuCommand = new RelayCommand(ShowOptionsMenuExecute);
            SearchBoxTextChangedCommand = new RelayCommand(SearchBoxTextChangeExecute);
            PreviousPageCommand = new RelayCommand(PreviousPageExecute);
            NextPageCommand = new RelayCommand(NextPageExecute);
            SourceSelectedCommand = new RelayCommand(SourceSelectedExecute);

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

        public void SourceSelectedExecute(Object sender)
        {
            ElementsView.Filter = ElementsFilter;
            CurrentPage = 1;

            PagePrepare();
            ElementsView.Refresh();
        }

        private void PagePrepare()
        {
            int filteredCount = ElementsView.Cast<Element>().ToList().Count;
            PageCount = (int)Math.Ceiling((double)filteredCount / ItemsPerPage);
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
            _sourcesList = new List<KeyValuePair<SourceType, string>>()
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

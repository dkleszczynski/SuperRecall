using SuperRecall.Models;
using SuperRecall.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SuperRecall.ViewModels
{
    public class ElementsViewModel
    {
        private IElementsManagementService _elementsManagementService;

        public DataGrid ElementsGrid { get; set; }
        public Element SelectedElement { get; set; }
        public ObservableCollection<Element> Elements { get; set; }
        public ICommand ShowOptionsMenuCommand { get; set; }
       
        public ElementsViewModel(IElementsManagementService elementsManagementService)
        {
            _elementsManagementService = elementsManagementService;
            Elements = new ObservableCollection<Element>(_elementsManagementService.LoadElements());
            ShowOptionsMenuCommand = new RelayCommand(ShowOptionsMenu);
        }
        
        public void ShowOptionsMenu(Object sender)
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
    }
}

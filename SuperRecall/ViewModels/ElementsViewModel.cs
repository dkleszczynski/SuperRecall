using SuperRecall.Models;
using SuperRecall.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperRecall.ViewModels
{
    public class ElementsViewModel
    {
        private IElementsManagementService _elementsManagementService;
        public ObservableCollection<Element> Elements { get; set; }
        
        public ElementsViewModel(IElementsManagementService elementsManagementService)
        {
            _elementsManagementService = elementsManagementService;
            Elements = new ObservableCollection<Element>(_elementsManagementService.LoadElements());
        }
    }
}

using SuperRecall.Models;
using SuperRecall.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperRecall.Services
{
    public class ElementsManagementService : IElementsManagementService
    {
        private List<Element> _elementsMockup = new List<Element>();

        public List<Element> LoadElements()
        {
            _elementsMockup.Add(new QueueElement() { Question = "zielony", Answer = "green" });
            _elementsMockup.Add(new QueueElement() { Question = "czerwony", Answer = "red" });
            return _elementsMockup;
        }
    }
}

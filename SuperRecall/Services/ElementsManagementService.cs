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
            _elementsMockup.Add(new ReviseElement() { Question = "zielony", Answer = "green", Group = "default", ReviewDates = new List<DateTime>() { new DateTime(2017, 06, 22)} });
            _elementsMockup.Add(new QueueElement() { Question = "czerwony", Answer = "red", Group = "default", LearningProgress = 0 });
            _elementsMockup.Add(new QueueElement() { Question = "żółty", Answer = "yellow", Group = "default", LearningProgress = 40 });
            return _elementsMockup;
        }
    }
}

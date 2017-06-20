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
            _elementsMockup.Add(new ReviseElement() { Question = "zielony", Answer = "green", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "czerwony", Answer = "red", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "żółty", Answer = "yellow", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "fioletowy", Answer = "purple", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "złoty", Answer = "golden", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "srebny", Answer = "silver", Group = "default" });

            _elementsMockup.Add(new QueueElement() { Question = "zielony", Answer = "green", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "czerwony", Answer = "red", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "żółty", Answer = "yellow", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "fioletowy", Answer = "purple", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "złoty", Answer = "golden", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "srebny", Answer = "silver", Group = "default" });

            _elementsMockup.Add(new QueueElement() { Question = "zielony", Answer = "green", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "czerwony", Answer = "red", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "żółty", Answer = "yellow", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "fioletowy", Answer = "purple", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "złoty", Answer = "golden", Group = "default" });
            _elementsMockup.Add(new QueueElement() { Question = "srebny", Answer = "silver", Group = "default" });

            return _elementsMockup;
        }
    }
}

using SuperRecall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperRecall.Services.Interfaces
{
    public interface IElementsManagementService
    {
        List<Element> LoadElements();
        List<string> LoadGroups();
    }
}

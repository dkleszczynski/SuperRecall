using SuperRecall.Services;
using SuperRecall.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperRecall.Views
{
    /// <summary>
    /// Interaction logic for ElementsView.xaml
    /// </summary>
    public partial class ElementsView : Window
    {
        public ElementsView()
        {
            InitializeComponent();
            DataContext = new ElementsViewModel(new ElementsManagementService());
        }
    }
}

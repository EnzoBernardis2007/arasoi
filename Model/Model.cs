using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfArasoi.ViewModel;

namespace WpfArasoi.Model
{
    internal class Model
    {
        // Needs to get view model to automatic reload in all functions
        public MainWindowViewModel ViewModel = new MainWindowViewModel();
    }
}

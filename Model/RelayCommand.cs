using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfArasoi.Prefab
{
    public class RelayCommand : ICommand
    {
        /* The RelayCommand class is for binding actions to UI events, such as button clicks, 
         * without the need to directly define the behavior in the view's codebehind. */

        private Action _execute;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        // Define a RelayCommand object
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Define if the object can be used
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        // Execute function
        public void Execute(object parameter)
        {
            _execute();
        }

        // Notifies WPF that the execution state of a command has changed
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

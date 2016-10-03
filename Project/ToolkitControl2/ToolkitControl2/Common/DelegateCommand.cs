using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToolkitControl2.Common
{
    class DelegateCommand : ICommand
    {
        private readonly Action commandExecuteAction;

        private readonly Func<bool> commandCanExecute;

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            commandExecuteAction = execute;
            commandCanExecute = canExecute ?? (() => true);
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            try
            {
                return commandCanExecute();
            }
            catch
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            try
            {
                commandExecuteAction();
            }
            catch
            {
                Debugger.Break();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

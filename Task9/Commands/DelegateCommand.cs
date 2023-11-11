using System;
using System.Windows.Input;

namespace Task9.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;
        public DelegateCommand(Action<object> execute,Func<object,bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public void RaiseCanExecuteChangedEvent() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => canExecute is null || canExecute(parameter);
        public void Execute(object parameter) => execute(parameter);
    }
}

using System;
using System.Windows.Input;

namespace MediaShop.Commands
{
    /// <summary>
    /// Used to make commands
    /// </summary>
    class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }

#pragma warning disable 0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

    }
}

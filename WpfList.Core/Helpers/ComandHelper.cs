using System;
using System.Windows.Input;

namespace WpfList.Core
{
    class ComandHelper : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action mFunction { get; set; }
        public ComandHelper(Action action)
        {
            mFunction = action;
        }

        public bool CanExecute(object parameter)
        {
           return true;
        }

        public void Execute(object parameter)
        {
            mFunction();
        }
    }
}

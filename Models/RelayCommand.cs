using System.Windows.Input;

namespace BadanieKrwi.Models
{
    /// <summary>
    /// Klasa implementująca interfejs ICommand, która pozwala na przekazywanie logiki biznesowej jako delegata.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Inicjalizuje nową instancję klasy RelayCommand, która zawsze może być wykonywana.
        /// </summary>
        /// <param name="execute">Delegat, który ma zostać wywołany podczas wykonywania polecenia.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Inicjalizuje nową instancję klasy RelayCommand.
        /// </summary>
        /// <param name="execute">Delegat, który ma zostać wywołany podczas wykonywania polecenia.</param>
        /// <param name="canExecute">Delegat, który określa, czy polecenie może być wykonywane w bieżącym stanie aplikacji.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Występuje, gdy zmienia się możliwość wykonywania polecenia.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Określa, czy to polecenie może być wywoływane w bieżącym stanie aplikacji.
        /// </summary>
        /// <param name="parameter">Parametr używany do określenia, czy to polecenie może być wywoływane w bieżącym stanie aplikacji.</param>
        /// <returns>True, jeśli to polecenie może być wywoływane; w przeciwnym razie false.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Wykonuje to polecenie.
        /// </summary>
        /// <param name="parameter">Parametr polecenia.</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}

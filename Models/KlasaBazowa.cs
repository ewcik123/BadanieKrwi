using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BadanieKrwi.Models
{
    public abstract class KlasaBazowa : INotifyPropertyChanged
    {
        /// <summary>
        /// Występuje, gdy zmienia się wartość właściwości.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Podnosi zdarzenie PropertyChanged.
        /// </summary>
        /// <param name="propertyName">Nazwa właściwości, której wartość uległa zmianie.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
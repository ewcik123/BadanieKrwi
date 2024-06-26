﻿using MahApps.Metro.Controls.Dialogs;
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

        public async Task<MessageDialogResult> ShowMessageAsync(WiadomoscModel wiadomosc, object context = null, IDialogCoordinator dialogCoordinator = null)
        {
            if (dialogCoordinator == null || context == null)
                return MessageDialogResult.Canceled;

            return await dialogCoordinator.ShowMessageAsync(context, wiadomosc.Tytul, wiadomosc.Tresc);
        }

        public async Task<MessageDialogResult> ShowMessageAsync(WiadomoscModel wiadomosc, MessageDialogStyle messageDialogStyle, object context = null, IDialogCoordinator dialogCoordinator = null)
        {
            if (dialogCoordinator == null || context == null)
                return MessageDialogResult.Canceled;

            return await dialogCoordinator.ShowMessageAsync(context, wiadomosc.Tytul, wiadomosc.Tresc, messageDialogStyle);
        }

        public async Task<MessageDialogResult> ShowMessageAsync(WiadomoscModel wiadomosc, MessageDialogStyle messageDialogStyle, object context = null, IDialogCoordinator dialogCoordinator = null, MetroDialogSettings metroDialogSettings = null)
        {
            if (dialogCoordinator == null || context == null)
                return MessageDialogResult.Canceled;

            return await dialogCoordinator.ShowMessageAsync(context, wiadomosc.Tytul, wiadomosc.Tresc, messageDialogStyle, metroDialogSettings);
        }
    }
}
using BadanieKrwi.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BadanieKrwi.Views
{
    /// <summary>
    /// Interaction logic for BadanieOkno.xaml
    /// </summary>
    public partial class BadanieOkno : MetroWindow
    {
        public BadanieOkno()
        {
            InitializeComponent();
            (DataContext as BadanieViewModel)!.DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
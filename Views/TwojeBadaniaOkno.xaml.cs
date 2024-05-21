using BadanieKrwi.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BadanieKrwi.Views
{
    /// <summary>
    /// Interaction logic for TwojeBadaniaOkno.xaml
    /// </summary>
    public partial class TwojeBadaniaOkno : MetroWindow
    {
        public TwojeBadaniaOkno()
        {
            InitializeComponent();
            (DataContext as TwojeBadanieViewModel)!.DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
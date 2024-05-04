using BadanieKrwi.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BadanieKrwi.Views
{
    /// <summary>
    /// Interaction logic for NoweBadanieOkno.xaml
    /// </summary>
    public partial class NoweBadanieOkno : MetroWindow
    {
        public NoweBadanieOkno()
        {
            InitializeComponent();
            (DataContext as NoweBadaniaViewModel)!.DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
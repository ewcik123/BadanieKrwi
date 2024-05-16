using BadanieKrwi.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BadanieKrwi.Views
{
    /// <summary>
    /// Interaction logic for KalendarzBadanOkno.xaml
    /// </summary>
    public partial class KalendarzBadanOkno : MetroWindow
    {
        public KalendarzBadanOkno()
        {
            InitializeComponent();
            (DataContext as KalendarzBadaniaViewModel)!.DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
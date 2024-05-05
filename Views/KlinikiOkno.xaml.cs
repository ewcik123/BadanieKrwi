using BadanieKrwi.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BadanieKrwi.Views
{
    /// <summary>
    /// Interaction logic for KlinikiOkno.xaml
    /// </summary>
    public partial class KlinikiOkno : MetroWindow
    {
        public KlinikiOkno()
        {
            InitializeComponent();
            (DataContext as KlinikiViewModel)!.DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
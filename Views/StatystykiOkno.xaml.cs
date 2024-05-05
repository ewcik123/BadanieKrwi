using BadanieKrwi.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BadanieKrwi.Views
{
    /// <summary>
    /// Interaction logic for StatystykiOkno.xaml
    /// </summary>
    public partial class StatystykiOkno : MetroWindow
    {
        public StatystykiOkno()
        {
            InitializeComponent();
            (DataContext as StatystykiViewModel)!.DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
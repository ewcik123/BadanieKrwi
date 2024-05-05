using BadanieKrwi.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BadanieKrwi.Views
{
    /// <summary>
    /// Interaction logic for SzczegolyStezeniaSubstancjiOkno.xaml
    /// </summary>
    public partial class SzczegolyStezeniaSubstancjiOkno : MetroWindow
    {
        public SzczegolyStezeniaSubstancjiOkno()
        {
            InitializeComponent();
            (DataContext as SzczegolyStezeniaSubstancjiViewModel)!.DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
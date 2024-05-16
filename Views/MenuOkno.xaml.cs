using BadanieKrwi.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BadanieKrwi.Views
{
    /// <summary>
    /// Interaction logic for MenuOkno.xaml
    /// </summary>
    public partial class MenuOkno : MetroWindow
    {
        public MenuOkno()
        {
            InitializeComponent();
            (DataContext as MenuViewModel)!.DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
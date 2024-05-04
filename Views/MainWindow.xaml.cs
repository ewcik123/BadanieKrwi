using BadanieKrwi.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace BadanieKrwi.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            (DataContext as MainWindowViewModel)!.DialogCoordinator = DialogCoordinator.Instance;
            (DataContext as MainWindowViewModel)!.RejestracjaVM.DialogCoordinator = DialogCoordinator.Instance;
        }
    }
}
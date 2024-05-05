using BadanieKrwi.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace BadanieKrwi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplyDatabaseMigrations();
        }

        private static void ApplyDatabaseMigrations()
        {
            try
            {
                using var context = new AppDbContext();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Problem podczas migracji bazy danych podczas uruchamiania aplikacji:\n{ex.Message}", "Migracja",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
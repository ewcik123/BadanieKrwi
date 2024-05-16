using BadanieKrwi.Models.Database;
using System.Configuration;

namespace BadanieKrwi.Models
{
    public class MenadzerPowiadomien
    {
        private readonly static MenadzerPowiadomien _instance = new();
        public static MenadzerPowiadomien Instance => _instance;

        private readonly EmailSender _sender;

        private MenadzerPowiadomien()
        {
            _sender = new EmailSender(ConfigurationManager.AppSettings["EmailSmtp"],
                Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]),
                ConfigurationManager.AppSettings["EmailSender"],
                ConfigurationManager.AppSettings["EmailSenderPassword"]);
        }

        public async void WyslijPowiadomienieOBadaniu(KalendarzBadanModel kalendarz)
        {
            if (kalendarz == null)
                return;

            using AppDbContext cont = new();
            var klinika = cont.Kliniki.FirstOrDefault(x => x.Id == kalendarz.IdKliniki);
            if (klinika != null)
            {
                _sender.SendEmail(Globals.ZalogowanyUzytkownik.Email, $"Przypomnienie o wizycie na badania - {kalendarz.TypBadania}",
                    $"Dzień dobry <b>{Globals.ZalogowanyUzytkownik.Imie} {Globals.ZalogowanyUzytkownik.Nazwisko}</b><br>" +
                    $"W dniu <b>{kalendarz.DataBadania}</b> masz umówioną wizytę na badania.<br><br><b>Badanie:</b> {kalendarz.TypBadania}<br>" +
                    $"<b>Klinika:</b> {klinika.Nazwa}<br>" +
                    $"<b>Treść:</b><br>{kalendarz.Tresc}");
            }
        }
    }
}
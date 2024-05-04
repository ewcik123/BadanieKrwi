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

        public void WyslijPowiadomienieOBadaniu(DateTime dataBadania)
        {
            _sender.SendEmail(Globals.ZalogowanyUzytkownik.Email, "Przypomnienie o wizycie na badania",
                $"Bry {Globals.ZalogowanyUzytkownik.Imie} {Globals.ZalogowanyUzytkownik.Nazwisko}<br>" +
                $"W dniu <b>{dataBadania}</b> masz umówioną wizytę na badania.");
        }
    }
}
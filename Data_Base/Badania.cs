using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BadanieKrwi.Data_Base
{
    public class Badania
    {
        public int Id { get; set; }
        public int IdKliniki { get; set; }
        public int IdUzytkownika { get; set; }

        public DateTime DataBadania { get; set; }
        public decimal StezenieErytrocytowRBC { get; set; }
        public decimal HemoglobinaHb { get; set; }
        public decimal HematokrytHtc { get; set; }
        public decimal SredniaObjetoscErytrocytuMCV { get; set; }
        public decimal SredniaMasaHemoglobinyMCH { get; set; }
        public decimal SrednieSteczenieHemoglobinyMCHC { get; set; }
        public decimal RozpietoscRozkladuObjetosciErytrocytowRDWCV { get; set; }
        public decimal RetikulocytyRC { get; set; }
        public decimal StezenieLeukocytowWBC { get; set; }
        public decimal Neutrofile { get; set; }
        public decimal Bazofile { get; set; }
        public decimal Eozynofile { get; set; }
        public decimal Limfocyty { get; set; }
        public decimal Monocyty { get; set; }
        public decimal PlytkiKrwiPLT { get; set; }
        public decimal SredniaObjetoscKrwiMPV { get; set; }

    }
}

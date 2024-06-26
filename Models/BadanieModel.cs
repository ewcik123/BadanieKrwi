﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BadanieKrwi.Models
{
    public class BadanieModel : KlasaBazowa
    {
        #region Properties
        [NotMapped]
        public bool CzyZmodyfikowano { get; set; }

        private Guid _id;
        public Guid Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private Guid _idUzytkownika;
        public Guid IdUzytkownika
        {
            get => _idUzytkownika;
            set
            {
                if (_idUzytkownika != value)
                {
                    _idUzytkownika = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _typBadania;
        public string TypBadania
        {
            get => _typBadania;
            set
            {
                if (_typBadania != value)
                {
                    _typBadania = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _dataBadania;
        public DateTime DataBadania
        {
            get => _dataBadania;
            set
            {
                if (_dataBadania != value)
                {
                    _dataBadania = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private string _nazwaKliniki;
        public string NazwaKliniki
        {
            get => _nazwaKliniki;
            set
            {
                if (_nazwaKliniki != value)
                {
                    _nazwaKliniki = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private int _stezenieErytrocytowRbc;
        public int StezenieErytrocytowRbc
        {
            get => _stezenieErytrocytowRbc;
            set
            {
                if (_stezenieErytrocytowRbc != value)
                {
                    _stezenieErytrocytowRbc = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private int _hemoglobinaHb;
        public int HemoglobinaHb
        {
            get => _hemoglobinaHb;
            set
            {
                if (_hemoglobinaHb != value)
                {
                    _hemoglobinaHb = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private int _hematokrytHtc;
        public int HematokrytHtc
        {
            get => _hematokrytHtc;
            set
            {
                if (_hematokrytHtc != value)
                {
                    _hematokrytHtc = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private double _sredniaObjetoscErytrocytuMcv;
        public double SredniaObjetoscErytrocytuMcv
        {
            get => _sredniaObjetoscErytrocytuMcv;
            set
            {
                if (_sredniaObjetoscErytrocytuMcv != value)
                {
                    _sredniaObjetoscErytrocytuMcv = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private double _sredniaMasaHemoglobinyWErytrocycieMch;
        public double SredniaMasaHemoglobinyWErytrocycieMch
        {
            get => _sredniaMasaHemoglobinyWErytrocycieMch;
            set
            {
                if (_sredniaMasaHemoglobinyWErytrocycieMch != value)
                {
                    _sredniaMasaHemoglobinyWErytrocycieMch = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private double _srednieStezenieHemoglobinyWErytrocytachMchc;
        public double SrednieStezenieHemoglobinyWErytrocytachMchc
        {
            get => _srednieStezenieHemoglobinyWErytrocytachMchc;
            set
            {
                if (_srednieStezenieHemoglobinyWErytrocytachMchc != value)
                {
                    _srednieStezenieHemoglobinyWErytrocytachMchc = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private double _rozpietoscRozkladuObjetosciErytrocytowRdwCw;
        public double RozpietoscRozkladuObjetosciErytrocytowRdwCw
        {
            get => _rozpietoscRozkladuObjetosciErytrocytowRdwCw;
            set
            {
                if (_rozpietoscRozkladuObjetosciErytrocytowRdwCw != value)
                {
                    _rozpietoscRozkladuObjetosciErytrocytowRdwCw = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private double _retikulocytyRc;
        public double RetikulocytyRc
        {
            get => _retikulocytyRc;
            set
            {
                if (_retikulocytyRc != value)
                {
                    _retikulocytyRc = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private int _stezenieLeukocytowWbc;
        public int StezenieLeukocytowWbc
        {
            get => _stezenieLeukocytowWbc;
            set
            {
                if (_stezenieLeukocytowWbc != value)
                {
                    _stezenieLeukocytowWbc = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private int _neutrofile;
        public int Neutrofile
        {
            get => _neutrofile;
            set
            {
                if (_neutrofile != value)
                {
                    _neutrofile = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private int _bazofile;
        public int Bazofile
        {
            get => _bazofile;
            set
            {
                if (_bazofile != value)
                {
                    _bazofile = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private int _eozynofile;
        public int Eozynofile
        {
            get => _eozynofile;
            set
            {
                if (_eozynofile != value)
                {
                    _eozynofile = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private int _limfocyty;
        public int Limfocyty
        {
            get => _limfocyty;
            set
            {
                if (_limfocyty != value)
                {
                    _limfocyty = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private int _monocyty;
        public int Monocyty
        {
            get => _monocyty;
            set
            {
                if (_monocyty != value)
                {
                    _monocyty = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private int _plytkiKrwiPlt;
        public int PlytkiKrwiPlt
        {
            get => _plytkiKrwiPlt;
            set
            {
                if (_plytkiKrwiPlt != value)
                {
                    _plytkiKrwiPlt = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private double _sredniaObjetoscKrwiMpv;
        public double SredniaObjetoscKrwiMpv
        {
            get => _sredniaObjetoscKrwiMpv;
            set
            {
                if (_sredniaObjetoscKrwiMpv != value)
                {
                    _sredniaObjetoscKrwiMpv = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private double _zelazo;
        public double Zelazo
        {
            get => _zelazo;
            set
            {
                if (_zelazo != value)
                {
                    _zelazo = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }

        private double _magnez;
        public double Magnez
        {
            get => _magnez;
            set
            {
                if (_magnez != value)
                {
                    _magnez = value;
                    CzyZmodyfikowano = true;
                    OnPropertyChanged();
                }
            }
        }
        #endregion Properties

        #region Constructors
        public BadanieModel()
        {
            DomyslneWartosci();
            CzyZmodyfikowano = false;
            TypBadania = Globals.TypBadania;
        }

        public BadanieModel(BadanieModel badanie)
        {
            AktualizujBadanie(badanie);
        }
        #endregion Constructors

        #region Methods
        private void DomyslneWartosci()
        {
            DataBadania = DateTime.Now;
            TypBadania = Globals.TypBadania;
            NazwaKliniki = null;
            StezenieErytrocytowRbc = 1;
            HemoglobinaHb = 1;
            HematokrytHtc = 1;
            SredniaMasaHemoglobinyWErytrocycieMch = 1;
            SredniaObjetoscErytrocytuMcv = 1;
            SrednieStezenieHemoglobinyWErytrocytachMchc = 1;
            RozpietoscRozkladuObjetosciErytrocytowRdwCw = 1;
            RetikulocytyRc = 1;
            StezenieLeukocytowWbc = 1;
            Neutrofile = 1;
            Bazofile = 1;
            Eozynofile = 1;
            Limfocyty = 1;
            Monocyty = 1;
            PlytkiKrwiPlt = 1;
            SredniaObjetoscKrwiMpv = 1;
            Zelazo = 1;
            Magnez = 1;
        }

        public void AktualizujBadanie(BadanieModel badanie)
        {
            if (badanie == null)
                return;

            DataBadania = badanie.DataBadania;
            Id = badanie.Id;
            IdUzytkownika = badanie.IdUzytkownika;
            TypBadania = badanie.TypBadania;
            DataBadania = badanie.DataBadania;
            NazwaKliniki = badanie.NazwaKliniki;
            StezenieErytrocytowRbc = badanie.StezenieErytrocytowRbc;
            HemoglobinaHb = badanie.HemoglobinaHb;
            HematokrytHtc = badanie.HematokrytHtc;
            SredniaMasaHemoglobinyWErytrocycieMch = badanie.SredniaMasaHemoglobinyWErytrocycieMch;
            SredniaObjetoscErytrocytuMcv = badanie.SredniaObjetoscErytrocytuMcv;
            SrednieStezenieHemoglobinyWErytrocytachMchc = badanie.SrednieStezenieHemoglobinyWErytrocytachMchc;
            RozpietoscRozkladuObjetosciErytrocytowRdwCw = badanie.RozpietoscRozkladuObjetosciErytrocytowRdwCw;
            RetikulocytyRc = badanie.RetikulocytyRc;
            StezenieLeukocytowWbc = badanie.StezenieLeukocytowWbc;
            Neutrofile = badanie.Neutrofile;
            Bazofile = badanie.Bazofile;
            Eozynofile = badanie.Eozynofile;
            Limfocyty = badanie.Limfocyty;
            Monocyty = badanie.Monocyty;
            PlytkiKrwiPlt = badanie.PlytkiKrwiPlt;
            SredniaObjetoscKrwiMpv = badanie.SredniaObjetoscKrwiMpv;
            Zelazo = badanie.Zelazo;
            Magnez = badanie.Magnez;
        }

        #endregion Methods
    }
}
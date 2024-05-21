using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace BadanieKrwi.Models
{
    public class MenadzerPdf
    {
        private readonly BaseFont _baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.EMBEDDED);
        private readonly Font _defaultFont;
        private readonly Font _naglowekFont;
        private readonly Font _naglowek2Font;
        private readonly Font _defaultFontBolt;

        private static MenadzerPdf _instance = new();
        public static MenadzerPdf Instance => _instance;

        private MenadzerPdf()
        {
            _defaultFont = new(_baseFont, 12f, Font.NORMAL);
            _naglowekFont = new(_baseFont, 20f, Font.BOLD);
            _naglowek2Font = new(_baseFont, 16f, Font.BOLD);
            _defaultFontBolt = new(_baseFont, 12f, Font.BOLD);
        }

        public bool Generuj(string filePath, BadanieModel badanie)
        {
            Document doc = new();
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);

                FileStream fStream = new(filePath, FileMode.CreateNew);
                PdfWriter writer = PdfWriter.GetInstance(doc, fStream);
                doc.Open();

                // Nagłówek
                doc.Add(Naglowek(badanie.TypBadania));
                doc.Add(Separator());

                // Info podstawowe
                doc.Add(TekstWartosc("Data badania:".PadRight(18), badanie.DataBadania.ToString()));
                doc.Add(TekstWartosc("Typ badania:".PadRight(19), badanie.TypBadania));
                doc.Add(TekstWartosc("Nazwa Kliniki:".PadRight(19), badanie.NazwaKliniki));
                doc.Add(TekstWartosc("Pracownik:".PadRight(20), $"{Globals.ZalogowanyUzytkownik.Imie} {Globals.ZalogowanyUzytkownik.Nazwisko}"));
                doc.Add(Separator());

                // Morfologia krwi
                doc.Add(Naglowek2("Morfologia krwi", Element.ALIGN_LEFT));
                doc.Add(Separator());
                var morfologiaKrwi = new Dictionary<string, string>()
                {
                    {"Stężenie Erytrocytów RBC [liczba/μl]", badanie.StezenieErytrocytowRbc.ToString() },
                    {"Hemoglobina Hb [g/dl]", badanie.HemoglobinaHb.ToString() },
                    {"Hematokryt Htc [%]", badanie.HematokrytHtc.ToString() },
                    {"Średnia objętość erytrocytu MCV [fl]", badanie.SredniaObjetoscErytrocytuMcv.ToString() },
                    {"Średnia masa hemoglobiny w erytrocycie MCH [pg]", badanie.SredniaMasaHemoglobinyWErytrocycieMch.ToString() },
                    {"Średnie stężenie hemoglobiny w erytrocytach MCHC [g/dl]", badanie.SrednieStezenieHemoglobinyWErytrocytachMchc.ToString() },
                    {"Rozpiętość rozkładu objętości erytrocytów RDW-CV [%]", badanie.RozpietoscRozkladuObjetosciErytrocytowRdwCw.ToString() },
                    {"Retikulocyty RC [%]", badanie.RetikulocytyRc.ToString() },
                    {"Stężenie leukocytów WBC [liczba/μl]", badanie.StezenieLeukocytowWbc.ToString() },
                    {"Neutrofile [liczba/μl]", badanie.Neutrofile.ToString() },
                    {"Bazofile [liczba/μl]", badanie.Bazofile.ToString() },
                    {"Eozynofile [liczba/μl]", badanie.Eozynofile.ToString() },
                    {"Limfocyty [liczba/μl]", badanie.Limfocyty.ToString() },
                    {"Monocyty [liczba/μl]", badanie.Monocyty.ToString() },
                    {"Płytki krwi PLT [liczba/μl]", badanie.PlytkiKrwiPlt.ToString() },
                    {"Średnia objętość krwi MPV [fl]", badanie.SredniaObjetoscKrwiMpv.ToString() },
                    {"Żelazo", badanie.Zelazo.ToString() },
                    {"Magnez", badanie.Magnez.ToString() },
                };

                doc.Add(TabelaKluczWartosc(morfologiaKrwi));
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Nie udało się wygenerować Pdf!", ex);
            }
            finally
            {
                doc.Close();
            }
        }

        private Chunk TekstPogrubiony(string tekst)
        {
            var res = new Chunk(tekst, _defaultFontBolt);
            return res;
        }

        private Chunk Tekst(string tekst)
        {
            var res = new Chunk(tekst, _defaultFont);
            return res;
        }

        private Paragraph TekstWartosc(string tekst, string wartosc)
        {
            var res = new Paragraph
            {
                TekstPogrubiony(tekst),
                Tekst(wartosc)
            };

            return res;
        }

        private PdfPTable TabelaKluczWartosc(Dictionary<string, string> kluczWartosc)
        {
            var table = new PdfPTable(2)
            {
                HorizontalAlignment = Element.ALIGN_LEFT
            };

            // Znajdź najdłuższy klucz
            float maxWidth = 0;
            foreach (var para in kluczWartosc)
            {
                var keyWidth = _defaultFontBolt.GetCalculatedBaseFont(true).GetWidthPoint(para.Key, _defaultFont.Size);
                maxWidth = Math.Max(maxWidth, keyWidth);
            }

            // Dodaj kolumny z odpowiednimi szerokościami
            table.SetWidths(new float[] { maxWidth, 70 });

            // Dodanie nagłówków kolumn
            table.AddCell(new PdfPCell(new Phrase("Nazwa", _defaultFontBolt)));
            table.AddCell(new PdfPCell(new Phrase("Wartość", _defaultFontBolt)));

            // Dodaj komórki do tabeli
            foreach (var para in kluczWartosc)
            {
                table.AddCell(new PdfPCell(new Phrase(para.Key, _defaultFont)));
                table.AddCell(new PdfPCell(new Phrase(para.Value, _defaultFont)));
            }

            return table;
        }

        private Paragraph Naglowek(string tekst, int alignment = Element.ALIGN_CENTER)
        {
            var par = new Paragraph($"Badanie krwi - {tekst}", _naglowekFont)
            {
                Alignment = alignment
            };
            return par;
        }

        private Paragraph Naglowek2(string tekst, int alignment = Element.ALIGN_CENTER)
        {
            var par = new Paragraph(tekst, _naglowek2Font)
            {
                Alignment = alignment
            };
            return par;
        }

        private Paragraph Separator()
            => new(" ") { Leading = 12f };
    }
}
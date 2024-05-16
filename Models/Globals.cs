using BadanieKrwi.Models.Database;

namespace BadanieKrwi.Models
{
    public class Globals
    {
        public static Uzytkownik ZalogowanyUzytkownik { get; set; }

        public static List<string> TypyBadan
            => [
                "ALAT (ALT)",
                "Albumina",
                "AspAT (AST)",
                "Białko C-reaktywne (CRP)",
                "Bilirubina bezpośrednia",
                "Bilirubina całkowita",
                "Elektrolity (sód, potas, chlorki)",
                "FT4 (wolna tyroksyna)",
                "Ferrytyna",
                "Glukoza na czczo",
                "Hemoglobina glikowana (HbA1c)",
                "Kreatynina",
                "Kwas moczowy",
                "Lipidogram (cholesterol całkowity, LDL, HDL, trójglicerydy)",
                "Magnez",
                "TSH (tyreotropina)",
                "Wapń całkowity",
                "Witamina D (25(OH)D)",
                "Żelazo",
                "Morfologia krwi",
            ];
    }
}
using Dictionary.Language;

namespace Corpus
{
    public class EnglishSplitter : SentenceSplitter
    {
        protected override string UpperCaseLetters() {
            return EnglishLanguage.UPPERCASE_LETTERS;
        }

        protected override string LowerCaseLetters() {
            return EnglishLanguage.LOWERCASE_LETTERS;
        }

        protected override string[] ShortCuts() {
            return new string[]{"dr", "prof", "org", "II", "III", "IV", "VI", "VII", "VIII", "IX",
                "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX",
                "XX", "min", "km", "jr", "mrs", "sir"};
        }

    }
}
using Dictionary.Language;

namespace Corpus
{
    public class EnglishSplitter : SentenceSplitter
    {
        /// <summary>
        /// Returns English UPPERCASE letters.
        /// </summary>
        /// <returns>English UPPERCASE letters.</returns>
        protected override string UpperCaseLetters() {
            return EnglishLanguage.UPPERCASE_LETTERS;
        }

        /// <summary>
        /// Returns English lowercase letters.
        /// </summary>
        /// <returns>English lowercase letters.</returns>
        protected override string LowerCaseLetters() {
            return EnglishLanguage.LOWERCASE_LETTERS;
        }

        /// <summary>
        /// Returns shortcut words in English language.
        /// </summary>
        /// <returns>Shortcut words in English language.</returns>
        protected override string[] ShortCuts() {
            return new string[]{"dr", "prof", "org", "II", "III", "IV", "VI", "VII", "VIII", "IX",
                "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX",
                "XX", "min", "km", "jr", "mrs", "sir"};
        }

    }
}
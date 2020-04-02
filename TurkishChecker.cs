using Dictionary.Language;

namespace Corpus
{
    public class TurkishChecker : LanguageChecker
    {

        /**
         * <summary>The isValidWord method takes an input String as a word than define all valid characters as a validCharacters String which has
         * letters (abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ),
         * extended language characters (âàáäãèéêëíîòóôûúqwxÂÈÉÊËÌÒÛQWX),
         * digits (0123456789),
         * separators ({@literal ()[]{}"'״＂՛}),
         * sentence enders (.?!…),
         * arithmetic chars (+-/=\*),
         * punctuation chars (,:;),
         * special-meaning chars
         * Then, loops through input word's each char and if a char in word does not in the validCharacters string it returns
         * false, true otherwise.</summary>
         * <param name="word">String to check validity.</param>
         * <returns>true if each char in word is valid, false otherwise.</returns>
         */
        public bool IsValidWord(string word)
        {
            var specialMeaningCharacters = "$\\_|@%#£§&><";
            var validCharacters = TurkishLanguage.LETTERS + TurkishLanguage.EXTENDED_LANGUAGE_CHARACTERS + TurkishLanguage.DIGITS + SentenceSplitter.SEPARATORS + SentenceSplitter.SENTENCE_ENDERS + TurkishLanguage.ARITHMETIC_CHARACTERS + SentenceSplitter.PUNCTUATION_CHARACTERS + specialMeaningCharacters;
            foreach (var t in word)
            {
                if (!validCharacters.Contains("" + t)) {
                    return false;
                }
            }
            return true;
            
        }
    }
}
using System;
using System.Collections.Generic;
using Dictionary.Language;

namespace Corpus
{
    public class TurkishSplitter : SentenceSplitter
    {
        /**
         * The contains method takes a String and a char input then check the given String contains the given char.
         *
         * @param s         String input to search for the char.
         * @param character Char input to look for in String.
         * @return true if char is found, false otherwise.
         */
        private bool Contains(String s, char character)
        {
            return s.Contains("" + character);
        }

        /**
         * The listContains method has a String array shortcuts which holds the possible abbreviations that might end with a '.' but not a
         * sentence finisher word. It also takes a String as an input and loops through the shortcuts array and returns
         * true if given String has any matching item in the shortcuts array.
         *
         * @param currentWord String input to check.
         * @return true if contains any abbreviations, false otherwise.
         */
        private bool ListContains(string currentWord)
        {
            string[] shortcuts =
            {
                "alb", "bnb", "bkz", "bşk", "co", "dr", "dç", "der", "em", "gn",
                "hz", "kd", "kur", "kuv", "ltd", "md", "mr", "mö", "muh", "müh",
                "no", "öğr", "op", "opr", "org", "sf", "tuğ", "uzm", "vb", "vd",
                "yön", "yrb", "yrd", "üniv", "fak", "prof", "dz", "yd", "krm", "gen",
                "pte", "p", "av", "II", "III", "IV", "VI", "VII", "VIII", "IX",
                "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX",
                "XX", "tuğa", "plt", "tğm", "tic", "srv", "bl", "dipl", "not", "min",
                "cul", "san", "rzv", "or", "kor", "tüm", "st", "sn", "fr", "pl",
                "ka", "tk", "ko", "vs", "yard", "bknz", "doç", "gör", "müz", "oyn",
                "m", "s", "kr", "ms", "hv", "uz", "re", "ph", "mc", "ed",
                "km", "yb", "bk", "jr", "bn", "os", "mrs", "bld", "sen", "alm",
                "sir", "ord", "dir", "yay", "man", "brm", "edt", "dec", "mah", "cad",
                "vol", "kom", "sok", "apt", "elk", "mad", "ort", "cap", "ste", "exc",
                "ef"
            };
            foreach (var shortcut in shortcuts)
            {
                if (currentWord.Equals(shortcut, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /**
         * The isNextCharUpperCaseOrDigit method takes a String line and an int i as inputs. First it compares each char in    
         * the input line with " " and SEPARATORS ({@literal ()[]{}"'״＂՛}) and increment i by one until a mismatch or end of line.
         * When i equals to line length or contains one of the uppercase letters or digits it returns true, false otherwise.
         *
         * @param line String to check.
         * @param i    int defining starting index.
         * @return true if next char is uppercase or digit, false otherwise.
         */
        private bool IsNextCharUpperCaseOrDigit(string line, int i)
        {
            while (i < line.Length && (line[i] == ' ' || Contains(SEPARATORS, line[i])))
            {
                i++;
            }

            if (i == line.Length || Contains(TurkishLanguage.UPPERCASE_LETTERS + Language.DIGITS + "-", line[i]))
            {
                return true;
            }

            return false;
        }

        /**
         * The isPreviousWordUpperCase method takes a String line and an int i as inputs. First it compares each char in
         * the input line with " " and checks each char whether they are lowercase letters or one of the qxw. And decrement
         * input i by one till this condition is false.
         * When i equals to -1 or contains one of the uppercase letters or one of the QXW it returns true, false otherwise.
         *
         * @param line String to check.
         * @param i    int defining ending index.
         * @return true if previous char is uppercase or one of the QXW, false otherwise.
         */
        private bool IsPreviousWordUpperCase(string line, int i)
        {
            while (i >= 0 && (line[i] == ' ' ||
                              Contains(TurkishLanguage.LOWERCASE_LETTERS + "qxw", line[i])))
            {
                i--;
            }

            if (i == -1 || Contains(TurkishLanguage.UPPERCASE_LETTERS + "QWX", line[i]))
            {
                return true;
            }

            return false;
        }

        /**
         * The isNextCharUpperCase method takes a String line and an int i as inputs. First it compares each char in
         * the input line with " " and increment i by one until a mismatch or end of line.
         * When i equals to line length or contains one of the uppercase letters it returns true, false otherwise.
         *
         * @param line String to check.
         * @param i    int defining starting index.
         * @return true if next char is uppercase, false otherwise.
         */
        private bool IsNextCharUpperCase(string line, int i)
        {
            while (i < line.Length && (line[i] == ' '))
            {
                i++;
            }

            if (i == line.Length || Contains(TurkishLanguage.UPPERCASE_LETTERS + "\"\'", line[i]))
            {
                return true;
            }

            return false;
        }

        /**
         * The isNameShortcut method takes a String word as an input. First, if the word length is 1, and currentWord
         * contains UPPERCASE_LETTERS letters than it returns true.
         * Secondly, if the length of the word is 3 (i.e it is a shortcut) and it has a '.' at its 1st index and
         * currentWord's 2nd  index is an uppercase letter it also returns true. (Ex : m.A)
         *
         * @param currentWord String input to check whether it is a shortcut.
         * @return true if given input is a shortcut, false otherwise.
         */
        private bool IsNameShortcut(string currentWord)
        {
            if (currentWord.Length == 1 && TurkishLanguage.UPPERCASE_LETTERS.Contains(currentWord))
            {
                return true;
            }

            if (currentWord.Length == 3 && currentWord[1] == '.' &&
                Contains(TurkishLanguage.UPPERCASE_LETTERS, currentWord[2]))
            {
                return true;
            }

            return false;
        }

        /**
         * The repeatControl method takes a String word as an input, and a boolean exceptionMode and compress the repetitive chars. With
         * the presence of exceptionMode it directly returns the given word. Then it declares a counter i and loops till the end of the
         * given word. It compares the char at index i with the char at index (i+2) if they are equal then it compares the char at index i
         * with the char at index (i+1) and increments i by one and returns concatenated  result String with char at index i.
         *
         * @param word          String input.
         * @param exceptionMode boolean input for exceptional cases.
         * @return String result.
         */
        private string RepeatControl(string word, bool exceptionMode)
        {
            if (exceptionMode)
            {
                return word;
            }

            var i = 0;
            var result = "";
            while (i < word.Length)
            {
                if (i < word.Length - 2 && word[i] == word[i + 1] &&
                    word[i] == word[i + 2])
                {
                    while (i < word.Length - 1 && word[i] == word[i + 1])
                    {
                        i++;
                    }
                }

                result += word[i];
                i++;
            }

            return result;
        }

        /**
         * The isApostrophe method takes a String line and an integer i as inputs. Initially declares a String apostropheLetters
         * which consists of abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ, âàáäãèéêëíîòóôûúqwxÂÈÉÊËÌÒÛQWX and 0123456789.
         * Then, it returns true if the result of contains method which checks the existence of previous char and next char
         * at apostropheLetters returns true, returns false otherwise.
         *
         * @param line String input to check.
         * @param i    index.
         * @return true if apostropheLetters contains previous char and next char, false otherwise.
         */
        private bool IsApostrophe(string line, int i)
        {
            var apostropheLetters = TurkishLanguage.LETTERS + Language.EXTENDED_LANGUAGE_CHARACTERS +
                                    Language.DIGITS;
            if (i + 1 < line.Length)
            {
                var previousChar = line[i - 1];
                var nextChar = line[i + 1];
                return Contains(apostropheLetters, previousChar) && Contains(apostropheLetters, nextChar);
            }

            return false;
        }

        /**
         * The numberExistsBeforeAndAfter method takes a String line and an integer i as inputs. Then, it returns true if
         * the result of contains method, which compares the previous char and next char with 0123456789, returns true and
         * false otherwise.
         *
         * @param line String input to check.
         * @param i    index.
         * @return true if previous char and next char is a digit, false otherwise.
         */
        private bool NumberExistsBeforeAndAfter(string line, int i)
        {
            if (i + 1 < line.Length && i > 0)
            {
                var previousChar = line[i - 1];
                var nextChar = line[i + 1];
                return Contains(Language.DIGITS, previousChar) && Contains(TurkishLanguage.DIGITS, nextChar);
            }

            return false;
        }

        /**
         * The isTime method takes a String line and an integer i as inputs. Then, it returns true if
         * the result of the contains method, which compares the previous char, next char and two next chars with 0123456789,
         * returns true and false otherwise.
         *
         * @param line String input to check.
         * @param i    index.
         * @return true if previous char, next char and two next chars are digit, false otherwise.
         */
        private bool IsTime(string line, int i)
        {
            if (i + 2 < line.Length)
            {
                char previousChar = line[i - 1];
                char nextChar = line[i + 1];
                char twoNextChar = line[i + 2];
                return Contains(Language.DIGITS, previousChar) && Contains(Language.DIGITS, nextChar) &&
                       Contains(Language.DIGITS, twoNextChar);
            }

            return false;
        }

        public override List<Sentence> Split(string line)
        {
        }
    }
}
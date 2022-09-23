using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dictionary.Dictionary;
using Dictionary.Language;

namespace Corpus
{
    public abstract class SentenceSplitter
    {
        public static string SEPARATORS = "\n()[]{}\"'\u05F4\uFF02\u055B’”‘“–\u00AD\u200B\t&\u2009\u202F\uFEFF";
        public static string SENTENCE_ENDERS = ".?!…";
        public static string PUNCTUATION_CHARACTERS = ",:;‚";
        public static string APOSTROPHES = "'’‘\u055B";

        protected abstract string UpperCaseLetters();
        protected abstract string LowerCaseLetters();
        protected abstract string[] ShortCuts();

        /**
         * The Contains method takes a String and a char input then check the given String Contains the given char.
         *
         * @param s         String input to search for the char.
         * @param character Char input to look for in String.
         * @return true if char is found, false otherwise.
         */
        private bool Contains(string s, char character)
        {
            return s.Contains("" + character);
        }

        /**
         * The listContains method has a String array shortcuts which holds the possible abbreviations that might end with a '.' but not a
         * sentence finisher word. It also takes a String as an input and loops through the shortcuts array and returns
         * true if given String has any matching item in the shortcuts array.
         *
         * @param currentWord String input to check.
         * @return true if Contains any abbreviations, false otherwise.
         */
        private bool ListContains(string currentWord)
        {
            foreach (var shortcut in ShortCuts())
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
         * When i equals to line length or Contains one of the uppercase letters or digits it returns true, false otherwise.
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

            if (i == line.Length || Contains(UpperCaseLetters() + Language.DIGITS + "-", line[i]))
            {
                return true;
            }

            return false;
        }

        /**
         * The isPreviousWordUpperCase method takes a String line and an int i as inputs. First it compares each char in
         * the input line with " " and checks each char whether they are lowercase letters or one of the qxw. And decrement
         * input i by one till this condition is false.
         * When i equals to -1 or Contains one of the uppercase letters or one of the QXW it returns true, false otherwise.
         *
         * @param line String to check.
         * @param i    int defining ending index.
         * @return true if previous char is uppercase or one of the QXW, false otherwise.
         */
        private bool IsPreviousWordUpperCase(string line, int i)
        {
            while (i >= 0 && (line[i] == ' ' ||
                              Contains(LowerCaseLetters() + "qxw", line[i])))
            {
                i--;
            }

            if (i == -1 || Contains(UpperCaseLetters() + "QWX", line[i]))
            {
                return true;
            }

            return false;
        }

        /**
         * The isNextCharUpperCase method takes a String line and an int i as inputs. First it compares each char in
         * the input line with " " and increment i by one until a mismatch or end of line.
         * When i equals to line length or Contains one of the uppercase letters it returns true, false otherwise.
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

            if (i == line.Length || Contains(UpperCaseLetters() + "\"\'", line[i]))
            {
                return true;
            }

            return false;
        }

        /**
         * The isNameShortcut method takes a String word as an input. First, if the word length is 1, and currentWord
         * Contains UPPERCASE_LETTERS letters than it returns true.
         * Secondly, if the length of the word is 3 (i.e it is a shortcut) and it has a '.' at its 1st index and
         * currentWord's 2nd  index is an uppercase letter it also returns true. (Ex : m.A)
         *
         * @param currentWord String input to check whether it is a shortcut.
         * @return true if given input is a shortcut, false otherwise.
         */
        private bool IsNameShortcut(string currentWord)
        {
            if (currentWord.Length == 1 && UpperCaseLetters().Contains(currentWord))
            {
                return true;
            }

            if (currentWord.Length == 3 && currentWord[1] == '.' &&
                Contains(UpperCaseLetters(), currentWord[2]))
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
         * Then, it returns true if the result of Contains method which checks the existence of previous char and next char
         * at apostropheLetters returns true, returns false otherwise.
         *
         * @param line String input to check.
         * @param i    index.
         * @return true if apostropheLetters Contains previous char and next char, false otherwise.
         */
        private bool IsApostrophe(string line, int i)
        {
            var apostropheLetters = UpperCaseLetters() + LowerCaseLetters() + Language.EXTENDED_LANGUAGE_CHARACTERS +
                                    Language.DIGITS;
            if (i > 0 && i + 1 < line.Length)
            {
                var previousChar = line[i - 1];
                var nextChar = line[i + 1];
                return Contains(apostropheLetters, previousChar) && Contains(apostropheLetters, nextChar);
            }

            return false;
        }

        /**
         * The numberExistsBeforeAndAfter method takes a String line and an integer i as inputs. Then, it returns true if
         * the result of Contains method, which compares the previous char and next char with 0123456789, returns true and
         * false otherwise.
         *
         * @param line String input to check.
         * @param i    index.
         * @return true if previous char and next char is a digit, false otherwise.
         */
        private bool NumberExistsBeforeAndAfter(string line, int i)
        {
            if (i > 0 && i + 1 < line.Length)
            {
                var previousChar = line[i - 1];
                var nextChar = line[i + 1];
                return Contains(Language.DIGITS, previousChar) && Contains(Language.DIGITS, nextChar);
            }

            return false;
        }

        /**
         * The isTime method takes a String line and an integer i as inputs. Then, it returns true if
         * the result of the Contains method, which compares the previous char, next char and two next chars with 0123456789,
         * returns true and false otherwise.
         *
         * @param line String input to check.
         * @param i    index.
         * @return true if previous char, next char and two next chars are digit, false otherwise.
         */
        private bool IsTime(string line, int i)
        {
            if (i > 0 && i + 2 < line.Length)
            {
                var previousChar = line[i - 1];
                var nextChar = line[i + 1];
                var twoNextChar = line[i + 2];
                return Contains(Language.DIGITS, previousChar) && Contains(Language.DIGITS, nextChar) &&
                       Contains(Language.DIGITS, twoNextChar);
            }

            return false;
        }

        /**
         * The split method takes a String line as an input. Firstly it creates a new sentence as currentSentence a new {@link ArrayList}
         * as sentences. Then loops till the end of the line and checks some conditions;
         * If the char at ith index is a separator;
         * ' : assigns currentWord as currentWord'
         * { : increment the curlyBracketCount
         * } : decrement the curlyBracketCount
         * " : increment the specialQuotaCount
         * " : decrement the specialQuotaCount
         * ( : increment roundParenthesisCount
         * ) : decrement roundParenthesisCount
         * [ : increment bracketCount
         * ] : decrement bracketCount
         * " : assign quotaCount as 1- quotaCount
         * ' : assign apostropheCount as 1- apostropheCount
         * If the currentWord is not empty, it adds the currentWord after repeatControl to currentSentence.
         * If the char at index i is " and  bracketCount, specialQuotaCount, curlyBracketCount, roundParenthesisCount, and
         * quotaCount equal to 0 and also the next char is uppercase or digit, it adds currentSentence to sentences.
         * If the char at ith index is a sentence ender;
         * . and currentWord is www : assigns webMode as true. Ex: www.google.com
         * . and currentWord is a digit or in web or e-mail modes : assigns currentWord as currentWord+char(i) Ex: 1.
         * . and currentWord is a shortcut or an abbreviation : assigns currentWord as currentWord+char(i) and adds currentWord to currentSentence. Ex : bkz.
         * ' and next char is uppercase or digit: add word to currentSentence as ' and add currentSentence to sentences.
         *
         * If the char at index i is ' ', i.e space, add word to currentSentence and assign "" to currentSentence.
         * If the char at index i is -,  add word to currentSentence and add sentences when the wordCount of currentSentence greater than 0.
         * If the char at ith index is a punctuation;
         * : and if currentWord is "https" : assign webMode as true.
         * , and there exists a number before and after : assign currentWord as currentWord+char(i) Ex: 1,2
         * : and if line is a time : assign currentWord as currentWord+char(i) Ex: 12:14:24
         * - and there exists a number before and after : assign currentWord as currentWord+char(i) Ex: 12-1
         * {@literal @} : assign emailMode as true.
         *
         * @param line String input to split.
         * @return sentences {@link ArrayList} which holds split line.
         */
        public List<Sentence> Split(string line)
        {
            bool emailMode = false, webMode = false;
            int i = 0,
                specialQuotaCount = 0,
                roundParenthesisCount = 0,
                bracketCount = 0,
                curlyBracketCount = 0,
                quotaCount = 0,
                apostropheCount = 0;
            var currentSentence = new Sentence();
            var currentWord = "";
            var sentences = new List<Sentence>();
            while (i < line.Length)
            {
                if (Contains(SEPARATORS, line[i]))
                {
                    if (Contains(APOSTROPHES, line[i]) && currentWord != "" && IsApostrophe(line, i))
                    {
                        currentWord = currentWord + line[i];
                    }
                    else
                    {
                        if (currentWord != "")
                        {
                            currentSentence.AddWord(new Word(RepeatControl(currentWord, webMode || emailMode)));
                        }

                        if (line[i] != '\n')
                        {
                            currentSentence.AddWord(new Word("" + line[i]));
                        }

                        currentWord = "";
                        switch (line[i])
                        {
                            case '{':
                                curlyBracketCount++;
                                break;
                            case '}':
                                curlyBracketCount--;
                                break;
                            case '\uFF02':
                                specialQuotaCount++;
                                break;
                            case '\u05F4':
                                specialQuotaCount--;
                                break;
                            case '“':
                                specialQuotaCount++;
                                break;
                            case '”':
                                specialQuotaCount--;
                                break;
                            case '‘':
                                specialQuotaCount++;
                                break;
                            case '’':
                                specialQuotaCount--;
                                break;
                            case '(':
                                roundParenthesisCount++;
                                break;
                            case ')':
                                roundParenthesisCount--;
                                break;
                            case '[':
                                bracketCount++;
                                break;
                            case ']':
                                bracketCount--;
                                break;
                            case '"':
                                quotaCount = 1 - quotaCount;
                                break;
                            case '\'':
                                apostropheCount = 1 - apostropheCount;
                                break;
                        }

                        if (line[i] == '"' && bracketCount == 0 && specialQuotaCount == 0 &&
                            curlyBracketCount == 0 &&
                            roundParenthesisCount == 0 && quotaCount == 0 && IsNextCharUpperCaseOrDigit(line, i + 1))
                        {
                            sentences.Add(currentSentence);
                            currentSentence = new Sentence();
                        }
                    }
                }
                else
                {
                    if (Contains(SENTENCE_ENDERS, line[i]))
                    {
                        if (line[i] == '.' && currentWord.Equals("www", StringComparison.OrdinalIgnoreCase))
                        {
                            webMode = true;
                        }

                        if (line[i] == '.' && currentWord != "" &&
                            (webMode || emailMode || (Contains(Language.DIGITS, line[i - 1]) &&
                                                      !IsNextCharUpperCaseOrDigit(line, i + 1))))
                        {
                            currentWord = currentWord + line[i];
                            currentSentence.AddWord(new Word(currentWord));
                            currentWord = "";
                        }
                        else
                        {
                            if (line[i] == '.' && (ListContains(currentWord) || IsNameShortcut(currentWord)))
                            {
                                currentWord = currentWord + line[i];
                                currentSentence.AddWord(new Word(currentWord));
                                currentWord = "";
                            }
                            else
                            {
                                if (line[i] == '.' && NumberExistsBeforeAndAfter(line, i))
                                {
                                    currentWord = currentWord + line[i];
                                }
                                else
                                {
                                    if (currentWord != "")
                                    {
                                        currentSentence.AddWord(new Word(RepeatControl(currentWord,
                                            webMode || emailMode)));
                                    }

                                    currentWord = "" + line[i];
                                    do
                                    {
                                        i++;
                                    } while (i < line.Length && Contains(SENTENCE_ENDERS, line[i]));

                                    i--;
                                    currentSentence.AddWord(new Word(currentWord));
                                    if (roundParenthesisCount == 0 && bracketCount == 0 && curlyBracketCount == 0 &&
                                        quotaCount == 0)
                                    {
                                        if (i + 1 < line.Length && line[i + 1] == '\'' && apostropheCount == 1 &&
                                            IsNextCharUpperCaseOrDigit(line, i + 2))
                                        {
                                            currentSentence.AddWord(new Word("'"));
                                            i++;
                                            sentences.Add(currentSentence);
                                            currentSentence = new Sentence();
                                        }
                                        else
                                        {
                                            if (i + 2 < line.Length && line[i + 1] == ' ' &&
                                                line[i + 2] == '\'' && apostropheCount == 1 &&
                                                IsNextCharUpperCaseOrDigit(line, i + 3))
                                            {
                                                currentSentence.AddWord(new Word("'"));
                                                i += 2;
                                                sentences.Add(currentSentence);
                                                currentSentence = new Sentence();
                                            }
                                            else
                                            {
                                                if (IsNextCharUpperCaseOrDigit(line, i + 1))
                                                {
                                                    sentences.Add(currentSentence);
                                                    currentSentence = new Sentence();
                                                }
                                            }
                                        }
                                    }
                                    currentWord = "";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (line[i] == ' ')
                        {
                            emailMode = false;
                            webMode = false;
                            if (currentWord != "")
                            {
                                currentSentence.AddWord(new Word(RepeatControl(currentWord, false)));
                                currentWord = "";
                            }
                        }
                        else
                        {
                            if (line[i] == '-' && !webMode && roundParenthesisCount == 0 &&
                                IsNextCharUpperCase(line, i + 1) && !IsPreviousWordUpperCase(line, i - 1))
                            {
                                if (currentWord != "" && !Language.DIGITS.Contains(currentWord))
                                {
                                    currentSentence.AddWord(new Word(RepeatControl(currentWord, emailMode)));
                                }

                                if (currentSentence.WordCount() > 0)
                                {
                                    sentences.Add(currentSentence);
                                }

                                currentSentence = new Sentence();
                                roundParenthesisCount =
                                    bracketCount = curlyBracketCount = quotaCount = specialQuotaCount = 0;
                                if (currentWord != "" && Regex.IsMatch(currentWord, "\\d+"))
                                {
                                    currentSentence.AddWord(new Word(currentWord + " -"));
                                }
                                else
                                {
                                    currentSentence.AddWord(new Word("-"));
                                }

                                currentWord = "";
                            }
                            else
                            {
                                if (Contains(PUNCTUATION_CHARACTERS, line[i]) ||
                                    Contains(Language.ARITHMETIC_CHARACTERS, line[i]))
                                {
                                    if (line[i] == ':' &&
                                        (currentWord.Equals("http", StringComparison.OrdinalIgnoreCase) ||
                                         currentWord.Equals("https", StringComparison.OrdinalIgnoreCase)))
                                    {
                                        webMode = true;
                                    }

                                    if (webMode)
                                    {
                                        //Constructing web address. Web address can contain both punctuation and arithmetic characters
                                        currentWord = currentWord + line[i];
                                    }
                                    else
                                    {
                                        if (line[i] == ',' && NumberExistsBeforeAndAfter(line, i))
                                        {
                                            currentWord = currentWord + line[i];
                                        }
                                        else
                                        {
                                            if (line[i] == ':' && IsTime(line, i))
                                            {
                                                currentWord = currentWord + line[i];
                                            }
                                            else
                                            {
                                                if (line[i] == '-' && NumberExistsBeforeAndAfter(line, i))
                                                {
                                                    currentWord = currentWord + line[i];
                                                }
                                                else
                                                {
                                                    if (currentWord != "")
                                                    {
                                                        currentSentence.AddWord(new Word(RepeatControl(currentWord,
                                                            emailMode)));
                                                    }

                                                    currentSentence.AddWord(new Word("" + line[i]));
                                                    currentWord = "";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (line[i] == '@')
                                    {
                                        //Constructing e-mail address
                                        currentWord = currentWord + line[i];
                                        emailMode = true;
                                    }
                                    else
                                    {
                                        currentWord = currentWord + line[i];
                                    }
                                }
                            }
                        }
                    }
                }

                i++;
            }

            if (currentWord != "")
            {
                currentSentence.AddWord(new Word(RepeatControl(currentWord, webMode || emailMode)));
            }

            if (currentSentence.WordCount() > 0)
            {
                sentences.Add(currentSentence);
            }

            return sentences;
        }
    }
}
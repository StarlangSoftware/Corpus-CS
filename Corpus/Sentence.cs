using System;
using System.Collections.Generic;
using System.IO;
using Dictionary.Dictionary;

namespace Corpus
{
    public class Sentence : ICloneable
    {
        protected List<Word> words;

        /**
         * <summary>An empty constructor of {@link Sentence} class. Creates an {@link ArrayList} of words.</summary>
         */
        public Sentence()
        {
            words = new List<Word>();
        }

        /**
         * <summary>The overridden clone method which creates a new sentence and clone words to this sentence.</summary>
         *
         * <returns>Sentence type sentence.</returns>
         */
        public object Clone()
        {
            var s = new Sentence();
            foreach (var w in words)
            {
                s.AddWord(w);
            }

            return s;
        }

        /**
         * <summary>Another constructor of {@link Sentence} class which takes a file as an input. It reads each word in the file
         * and adds to words {@link ArrayList}.</summary>
         *
         * <param name="file">input file to read words from.</param>
         */
        public Sentence(StreamReader file)
        {
            words = new List<Word>();
            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                var items = line.Split();
                foreach (var item in items)
                {
                    words.Add(new Word(item));
                }
            }
        }

        /**
         * <summary>The equals method takes a Sentence as an input. First compares the sizes of both {@link ArrayList} words and words
         * of the Sentence input. If they are not equal then it returns false. Than it compares each word in the {@link ArrayList}.
         * If they are equal, it returns true.</summary>
         *
         * <param name="s">Sentence to compare.</param>
         * <returns>true if words of two sentences are equal.</returns>
         */
        public override bool Equals(object s)
        {
            if (words.Count != ((Sentence) s).words.Count)
                return false;
            for (var i = 0; i < words.Count; i++)
                if (words[i].GetName().CompareTo(((Sentence) s).words[i].GetName()) != 0)
                    return false;
            return true;
        }

        /**
         * <summary>Another constructor of {@link Sentence} class which takes a sentence String as an input. It parses the sentence by
         * " " and adds each word to the newly created {@link ArrayList} words.</summary>
         *
         * <param name="sentence">String input to parse.</param>
         */
        public Sentence(string sentence)
        {
            words = new List<Word>();
            var wordArray = sentence.Split(" ");
            foreach (var word in wordArray)
            {
                if (word != "")
                {
                    words.Add(new Word(word));
                }
            }
        }

        /**
         * <summary>Another constructor of {@link Sentence} class with two inputs; a String sentence and a {@link LanguageChecker}
         * languageChecker. It parses a sentence by " " and then check the language considerations. If it is a valid word,
         * it adds this word to the newly created {@link ArrayList} words.</summary>
         *
         * <param name="sentence">       String input.</param>
         * <param name="languageChecker">{@link LanguageChecker} type input.</param>
         */
        public Sentence(string sentence, LanguageChecker languageChecker)
        {
            words = new List<Word>();
            var wordArray = sentence.Split(" ");
            foreach (var word in wordArray)
            {
                if (word != "" && languageChecker.IsValidWord(word))
                {
                    words.Add(new Word(word));
                }
            }
        }

        /**
         * <summary>The getWord method takes an index input and gets the word at that index.</summary>
         *
         * <param name="index">is used to get the word.</param>
         * <returns>the word in given index.</returns>
         */
        public Word GetWord(int index)
        {
            return words[index];
        }

        /**
         * <summary>The getWords method returns the {@link ArrayList} words.</summary>
         *
         * <returns>words ArrayList.</returns>
         */
        public List<Word> GetWords()
        {
            return words;
        }

        /**
         * <summary>The getStrings method loops through the words {@link ArrayList} and adds each words' names to the newly created
         * {@link ArrayList} result.</summary>
         *
         * <returns>result ArrayList which holds names of the words.</returns>
         */
        public List<string> GetStrings()
        {
            var result = new List<string>();
            foreach (var word in words)
            {
                result.Add(word.GetName());
            }

            return result;
        }

        /**
         * <summary>The getIndex method takes a word as an input and finds the index of that word in the words {@link ArrayList} if it exists.</summary>
         *
         * <param name="word">Word type input to search for.</param>
         * <returns>index of the found input, -1 if not found.</returns>
         */
        public int GetIndex(Word word)
        {
            var i = 0;
            foreach (var w in words)
            {
                if (w.Equals(word))
                    return i;
                i++;
            }

            return -1;
        }

        /**
         * <summary>The wordCount method finds the size of the words {@link ArrayList}.</summary>
         *
         * <returns>the size of the words {@link ArrayList}.</returns>
         */
        public int WordCount()
        {
            return words.Count;
        }

        /**
         * <summary>The addWord method takes a word as an input and adds this word to the words {@link ArrayList}.</summary>
         *
         * <param name="word">Word to add words {@link ArrayList}.</param>
         */
        public void AddWord(Word word)
        {
            words.Add(word);
        }

        /**
         * <summary>The charCount method finds the total number of chars in each word of words {@link ArrayList}.</summary>
         *
         * <returns>number of the chars in the whole sentence.</returns>
         */
        public int CharCount()
        {
            var sum = 0;
            foreach (var word in words)
            {
                sum += word.CharCount();
            }

            return sum;
        }

        /**
         * <summary>The InsertWord method takes an index and a word as inputs. It adds the word to the given index in
         * words {@link ArrayList}.</summary>
         *
         * <param name="i">      index.</param>
         * <param name="newWord">to add the words {@link ArrayList}.</param>
         */
        public void InsertWord(int i, Word newWord)
        {
            words.Insert(i, newWord);
        }

        /**
         * <summary>The replaceWord method takes an index and a word as inputs. It removes the word at given index from words
         * {@link ArrayList} and then adds the given word to given index of words.</summary>
         *
         * <param name="i">      index.</param>
         * <param name="newWord">to add the words {@link ArrayList}.</param>
         */
        public void ReplaceWord(int i, Word newWord)
        {
            words.RemoveAt(i);
            words.Insert(i, newWord);
        }

        /**
         * <summary>The safeIndex method takes an index as an input and checks whether this index is between 0 and the size of the words.</summary>
         *
         * <param name="index">is used to check the safety.</param>
         * <returns>true if an index is safe, false otherwise.</returns>
         */
        public bool SafeIndex(int index)
        {
            return index >= 0 && index < words.Count;
        }

        /**
         * <summary>The overridden toString method returns an accumulated string of each word in words {@link ArrayList}.</summary>
         *
         * <returns>String result which has all the word in words {@link ArrayList}.</returns>
         */
        public override string ToString()
        {
            if (words.Count > 0)
            {
                var result = words[0].ToString();
                for (var i = 1; i < words.Count; i++)
                {
                    result = result + " " + words[i];
                }

                return result;
            }

            return "";
        }

        /**
         * <summary>The toWords method returns an accumulated string of each word's names in words {@link ArrayList}.</summary>
         *
         * <returns>String result which has all the names of each item in words {@link ArrayList}.</returns>
         */
        public string ToWords()
        {
            if (words.Count > 0)
            {
                var result = words[0].GetName();
                for (var i = 1; i < words.Count; i++)
                {
                    result = result + " " + words[i].GetName();
                }

                return result;
            }

            return "";
        }

        /**
         * <summary>The writeToFile method writes the given file by using toString method.</summary>
         *
         * <param name="file">to write in.</param>
         */
        public void WriteToFile(StreamWriter file)
        {
            file.WriteLine(ToString());
            file.Close();
        }
    }
}
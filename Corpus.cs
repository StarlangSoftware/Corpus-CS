using System.Collections.Generic;
using System.IO;
using DataStructure;
using Dictionary.Dictionary;

namespace Corpus
{
    public class Corpus
    {
        protected List<Paragraph> paragraphs;
        protected List<Sentence> sentences;
        protected CounterHashMap<Word> wordList;
        protected string fileName;

        /**
         * <summary>A constructor of {@link Corpus} class which creates new {@link ArrayList} for sentences and a {@link CounterHashMap}
         * for wordList.</summary>
         */
        public Corpus()
        {
            sentences = new List<Sentence>();
            paragraphs = new List<Paragraph>();
            wordList = new CounterHashMap<Word>();
        }

        /**
         * <summary>The emptyCopy method returns new Corpus.</summary>
         *
         * <returns>new {@link Corpus}.</returns>
         */
        public Corpus EmptyCopy()
        {
            return new Corpus();
        }

        /**
         * <summary>Another constructor of {@link Corpus} class which takes a file name as an input. Then reads the input file line by line
         * and calls addSentence method with each read line.</summary>
         *
         * <param name="fileName">String file name input that will be read.</param>
         */
        public Corpus(string fileName)
        {
            this.fileName = fileName;
            var streamReader = new StreamReader(fileName);
            var line = streamReader.ReadLine();
            while (line != null)
            {
                AddSentence(new Sentence(line));
                line = streamReader.ReadLine();
            }
        }

        /**
         * <summary>Another constructor of {@link Corpus} class which takes {@link SentenceSplitter}  as an input besides the file name.
         * It reads input file line by line and calls the sentenceSplitter method with each line, then calls addSentence method
         * with each sentence.</summary>
         *
         * <param name="fileName">        String file name input that will be read.</param>
         * <param name="sentenceSplitter">{@link SentenceSplitter} type input.</param>
         */
        public Corpus(string fileName, SentenceSplitter sentenceSplitter)
        {
            List<Sentence> sentences;
            var streamReader = new StreamReader(fileName);
            var line = streamReader.ReadLine();
            while (line != null)
            {
                sentences = sentenceSplitter.Split(line);
                var paragraph = new Paragraph();
                foreach (var s in sentences)
                {
                    paragraph.AddSentence(s);
                }

                AddParagraph(paragraph);
                line = streamReader.ReadLine();
            }
        }

        /**
         * <summary>Another constructor of {@link Corpus} class which also takes languageChecker input besides file name input.
         * It reads input file line by line and add each sentence also by using the languageChecker input which simply checks
         * the validity of the sentence.</summary>
         *
         * <param name="fileName">       String file name input that will be read.</param>
         * <param name="languageChecker">{@link LanguageChecker} type input.</param>
         */
        public Corpus(string fileName, LanguageChecker languageChecker) : base()
        {
            var streamReader = new StreamReader(fileName);
            var line = streamReader.ReadLine();
            while (line != null)
            {
                AddSentence(new Sentence(line, languageChecker));
                line = streamReader.ReadLine();
            }
        }

        /**
         * <summary>The combine method takes a {@link Corpus} as an input and adds each sentence of sentences {@link ArrayList}.</summary>
         *
         * <param name="corpus">{@link Corpus} type input.</param>
         */
        public void Combine(Corpus corpus)
        {
            foreach (var sentence in corpus.sentences)
            {
                AddSentence(sentence);
            }
        }

        /**
         * <summary>The addSentence method takes a Sentence as an input. It adds given input to sentences {@link ArrayList} and loops
         * through the each word in sentence and puts these words into wordList {@link CounterHashMap}.</summary>
         *
         * <param name="s">Sentence type input that will be added to sentences {@link ArrayList} and its words will be added to wordList</param>
         *          {@link CounterHashMap}.
         */
        public void AddSentence(Sentence s)
        {
            sentences.Add(s);
            for (var i = 0; i < s.WordCount(); i++)
            {
                var w = s.GetWord(i);
                wordList.Put(w);
            }
        }

        /**
         * <summary>The numberOfWords method loops through the sentences {@link ArrayList} and accumulates the number of words in sentence.</summary>
         *
         * <returns>size which holds the total number of words.</returns>
         */
        public int NumberOfWords()
        {
            var size = 0;
            foreach (var s in sentences)
            {
                size += s.WordCount();
            }

            return size;
        }

        /**
         * <summary>The contains method takes a String word as an input and checks whether wordList {@link CounterHashMap} has the
         * given word and returns true if so, otherwise returns false.</summary>
         *
         * <param name="word">String input to check.</param>
         * <returns>true if wordList has the given word, false otherwise.</returns>
         */
        public bool Contains(string word)
        {
            return wordList.ContainsKey(new Word(word));
        }

        /**
         * <summary>The addParagraph method takes a {@link Paragraph} type input. It gets the sentences in the given paragraph and
         * add these to the sentences {@link ArrayList} and the words in the sentences to the wordList {@link CounterHashMap}.</summary>
         *
         * <param name="p">{@link Paragraph} type input to add sentences and wordList.</param>
         */
        public void AddParagraph(Paragraph p)
        {
            paragraphs.Add(p);
            for (var i = 0; i < p.SentenceCount(); i++)
                AddSentence(p.GetSentence(i));
        }

        /**
         * <summary>Getter for the file name.</summary>
         *
         * <returns>file name.</returns>
         */
        public string GetFileName()
        {
            return fileName;
        }

        /**
         * <summary>Getter for the wordList.</summary>
         *
         * <returns>the keySet of wordList.</returns>
         */
        public List<Word> GetWordList()
        {
            return new List<Word>(wordList.Keys);
        }

        /**
         * <summary>The wordCount method returns the size of the wordList {@link CounterHashMap}.</summary>
         *
         * <returns>the size of the wordList {@link CounterHashMap}.</returns>
         */
        public int WordCount()
        {
            return ((Dictionary<Word, int>) wordList).Count;
        }

        /**
         * <summary>The getCount method returns the count value of given word.</summary>
         *
         * <param name="word">Word type input to check.</param>
         * <returns>the count value of given word.</returns>
         */
        public int GetCount(Word word)
        {
            return wordList[word];
        }

        /**
         * <summary>The sentenceCount method returns the size of the sentences {@link ArrayList}.</summary>
         *
         * <returns>the size of the sentences {@link ArrayList}.</returns>
         */
        public int SentenceCount()
        {
            return sentences.Count;
        }

        /**
         * <summary>Getter for getting a sentence at given index.</summary>
         *
         * <param name="index">to get sentence from.</param>
         * <returns>the sentence at given index.</returns>
         */
        public Sentence GetSentence(int index)
        {
            return sentences[index];
        }

        /**
         * <summary>The paragraphCount method returns the size of the paragraphs {@link ArrayList}.</summary>
         *
         * <returns>the size of the paragraphs {@link ArrayList}.</returns>
         */
        public int ParagraphCount()
        {
            return paragraphs.Count;
        }

        /**
         * <summary>Getter for getting a paragraph at given index.</summary>
         *
         * <param name="index">to get paragraph from.</param>
         * <returns>the paragraph at given index.</returns>
         */
        public Paragraph GetParagraph(int index)
        {
            return paragraphs[index];
        }

        /**
         * <summary>The maxSentenceLength method finds the sentence with the maximum number of words and returns this number.</summary>
         *
         * <returns>maximum length.</returns>
         */
        public int MaxSentenceLength()
        {
            var maxLength = 0;
            foreach (var s in sentences)
            {
                if (s.WordCount() + 1 > maxLength)
                    maxLength = s.WordCount() + 1;
            }

            return maxLength;
        }

        /**
         * <summary>The getAllWordsAsArrayList method creates new {@link ArrayList} of ArrayLists and adds each word in each sentence of sentences
         * {@link ArrayList} into new {@link ArrayList}.</summary>
         *
         * <returns>newly created and populated {@link ArrayList}.</returns>
         */
        public List<List<Word>> GetAllWordsAsArrayList()
        {
            var allWords = new List<List<Word>>();
            for (var i = 0; i < SentenceCount(); i++)
            {
                allWords.Add(GetSentence(i).GetWords());
            }

            return allWords;
        }

        /**
         * <summary>The getAllWordsAsArray method creates new array of {@link ArrayList} and adds each word in each sentence of sentences
         * into this array.</summary>
         *
         * <returns>newly created and populated array.</returns>
         */
        public List<Word>[] GetAllWordsAsArray()
        {
            var allWords = new List<Word>[SentenceCount()];
            for (var i = 0; i < allWords.Length; i++)
            {
                allWords[i] = GetSentence(i).GetWords();
            }

            return allWords;
        }

        /**
         * <summary>The shuffleSentences method randomly shuffles sentences {@link ArrayList} with given seed value.</summary>
         *
         * <param name="seed">value to randomize shuffling.</param>
         */
        public void ShuffleSentences(int seed)
        {
        }

        /**
         * <summary>The getTrainCorpus method takes two integer inputs foldNo and foldCount for determining train data size and count of fold respectively.
         * Initially creates a new empty Corpus, then finds the sentenceCount as N. Then, starting from the index 0 it loops through
         * the index (foldNo * N) / foldCount and add each sentence of sentences {@link ArrayList} to new Corpus. Later on,
         * starting from the index ((foldNo + 1) * N) / foldCount, it loops through the index N and add each sentence of
         * sentences {@link ArrayList} to new Corpus.</summary>
         *
         * <param name="foldNo">   Integer input for train set size.</param>
         * <param name="foldCount">Integer input for counting fold.</param>
         * <returns>the newly created and populated Corpus.</returns>
         */
        public Corpus GetTrainCorpus(int foldNo, int foldCount)
        {
            var trainCorpus = EmptyCopy();
            var n = SentenceCount();
            for (var i = 0; i < foldNo * n / foldCount; i++)
            {
                trainCorpus.AddSentence(sentences[i]);
            }

            for (var i = (foldNo + 1) * n / foldCount; i < n; i++)
            {
                trainCorpus.AddSentence(sentences[i]);
            }

            return trainCorpus;
        }

        /**
         * <summary>The getTestCorpus method takes two integer inputs foldNo and foldCount for determining test data size and count of
         * fold respectively. Initially creates a new empty Corpus, then finds the sentenceCount as N.
         * Then, starting from the index (foldNo * N) / foldCount it loops through the index ((foldNo + 1) * N) / foldCount and
         * add each sentence of sentences {@link ArrayList} to new Corpus.</summary>
         *
         * <param name="foldNo">   Integer input for test size.</param>
         * <param name="foldCount">Integer input counting fold.</param>
         * <returns>the newly created and populated Corpus.</returns>
         */
        public Corpus GetTestCorpus(int foldNo, int foldCount)
        {
            var testCorpus = EmptyCopy();
            var n = SentenceCount();
            for (var i = foldNo * n / foldCount; i < (foldNo + 1) * n / foldCount; i++)
            {
                testCorpus.AddSentence(sentences[i]);
            }

            return testCorpus;
        }

        /**
         * <summary>The writeToFile method takes a String file name input and writes sentence of sentences {@link ArrayList} into this file.</summary>
         *
         * <param name="fileName">file to write the sentences.</param>
         */
        public void WriteToFile(string fileName)
        {
            var streamWriter = new StreamWriter(fileName);
            foreach (var sentence in sentences)
            {
                streamWriter.WriteLine(sentence.ToString());
            }

            streamWriter.Close();
        }

    }
}
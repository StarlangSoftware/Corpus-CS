using System.Collections.Generic;
using System.IO;

namespace Corpus
{
    public class CorpusStream : AbstractCorpus
    {
        private StreamReader _reader;
        private string _fileName;

        /// <summary>
        /// Constructor for CorpusStream. CorpusStream is used for reading very large corpora that does not fit in memory as
        /// a whole. For that reason, sentences are read one by one.
        /// </summary>
        /// <param name="fileName">File name of the corpus stream.</param>
        public CorpusStream(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Implements open method in AbstractCorpus. Initializes file reader.
        /// </summary>
        public override void Open()
        {
            _reader = new StreamReader(_fileName);
        }

        /// <summary>
        /// Implements close method in AbstractCorpus. Closes the file reader.
        /// </summary>
        public override void Close()
        {
            _reader.Close();
        }

        /// <summary>
        /// Implements getSentence method in AbstractCorpus. Reads from the file buffer next sentence and returns it. If
        /// there are no sentences to be read, returns null.
        /// </summary>
        /// <returns>Next read sentence from file buffer or null.</returns>
        public override Sentence GetSentence()
        {
            var line = _reader.ReadLine();
            if (line != null)
            {
                return new Sentence(line);
            }

            return null;
        }

        /// <summary>
        /// Reads more than one line (lineCount lines) from the buffer, stores them in an array list and returns that
        /// array list. If there are no lineCount lines to be read, the method reads only available lines and returns them.
        /// </summary>
        /// <param name="lineCount">Maximum number of lines to read.</param>
        /// <returns>A list of read lines.</returns>
        public List<Sentence> GetSentenceBatch(int lineCount)
        {
            var sentences = new List<Sentence>();
            for (var i = 0; i < lineCount; i++)
            {
                var line = _reader.ReadLine();
                if (line != null)
                {
                    sentences.Add(new Sentence(line));
                }
                else
                {
                    break;
                }
            }

            return sentences;
        }
    }
}
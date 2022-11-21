using System.Collections.Generic;
using System.IO;

namespace Corpus
{
    public class CorpusStream : AbstractCorpus
    {
        private StreamReader _reader;
        private string _fileName;

        public CorpusStream(string fileName)
        {
            _fileName = fileName;
        }

        public override void Open()
        {
            _reader = new StreamReader(_fileName);
        }

        public override void Close()
        {
            _reader.Close();
        }

        public override Sentence GetSentence()
        {
            var line = _reader.ReadLine();
            if (line != null)
            {
                return new Sentence(line);
            }

            return null;
        }

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
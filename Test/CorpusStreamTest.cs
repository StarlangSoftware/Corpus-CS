using Corpus;
using NUnit.Framework;

namespace Test
{
    public class CorpusStreamTest
    {
        [Test]
        public void TestNumberOfWords1()
        {
            var wordCount = 0;
            var corpusStream = new CorpusStream("../../../corpus.txt");
            corpusStream.Open();
            Sentence sentence = corpusStream.GetSentence();
            while (sentence != null){
                wordCount += sentence.WordCount();
                sentence = corpusStream.GetSentence();
            }
            Assert.AreEqual(826680, wordCount);
        }

        [Test]
        public void TestNumberOfWords2()
        {
            var wordCount = 0;
            var corpusStream = new CorpusStream("../../../corpus.txt");
            corpusStream.Open();
            var sentences = corpusStream.GetSentenceBatch(100);
            while (sentences.Count != 0){
                foreach (Sentence sentence in sentences){
                    wordCount += sentence.WordCount();
                }
                sentences = corpusStream.GetSentenceBatch(100);
            }
            Assert.AreEqual(826680, wordCount);
        }

    }
}
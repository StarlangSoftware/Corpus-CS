using Dictionary.Dictionary;
using NUnit.Framework;

namespace Test
{
    public class CorpusTest
    {
        Corpus.Corpus simpleCorpus, corpus;

        [SetUp]
        public void Setup()
        {
            corpus = new Corpus.Corpus("../../../corpus.txt");
            simpleCorpus = new Corpus.Corpus("../../../simplecorpus.txt");
        }

        [Test]
        public void TestNumberOfWords()
        {
            Assert.AreEqual(826680, corpus.NumberOfWords());
            Assert.AreEqual(24, simpleCorpus.NumberOfWords());
        }

        [Test]
        public void TestContains()
        {
            Assert.True(corpus.Contains("atatürk"));
            foreach (Word word in corpus.GetWordList()){
                Assert.True(corpus.Contains(word.GetName()));
            }
            Assert.True(simpleCorpus.Contains("mehmet"));
            foreach (Word word in simpleCorpus.GetWordList()){
                Assert.True(simpleCorpus.Contains(word.GetName()));
            }
        }

        [Test]
        public void TestWordCount()
        {
            Assert.AreEqual(98199, corpus.WordCount());
            Assert.AreEqual(12, simpleCorpus.WordCount());
        }

        [Test]
        public void TestGetCount()
        {
            Assert.AreEqual(309, corpus.GetCount(new Word("mustafa")));
            Assert.AreEqual(109, corpus.GetCount(new Word("kemal")));
            Assert.AreEqual(122, corpus.GetCount(new Word("atatürk")));
            Assert.AreEqual(4, simpleCorpus.GetCount(new Word("ali")));
            Assert.AreEqual(3, simpleCorpus.GetCount(new Word("gitti")));
            Assert.AreEqual(4, simpleCorpus.GetCount(new Word("at")));
        }

        [Test]
        public void TestSentenceCount()
        {
            Assert.AreEqual(50000, corpus.SentenceCount());
            Assert.AreEqual(5, simpleCorpus.SentenceCount());
        }

        [Test]
        public void TestMaxSentenceLength()
        {
            Assert.AreEqual(1092, corpus.MaxSentenceLength());
            Assert.AreEqual(6, simpleCorpus.MaxSentenceLength());
        }
    }
}
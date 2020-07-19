using Corpus;
using Dictionary.Dictionary;
using NUnit.Framework;

namespace Test
{
    public class SentenceTest
    {
        Sentence sentence;

        [SetUp]
        public void Setup()
        {
            sentence = new Sentence();
            sentence.AddWord(new Word("ali"));
            sentence.AddWord(new Word("topu"));
            sentence.AddWord(new Word("at"));
            sentence.AddWord(new Word("mehmet"));
            sentence.AddWord(new Word("ayşeyle"));
            sentence.AddWord(new Word("gitti"));
        }
        
        [Test]
        public void TestGetWord() {
            Assert.AreEqual(new Word("ali"), sentence.GetWord(0));
            Assert.AreEqual(new Word("at"), sentence.GetWord(2));
            Assert.AreEqual(new Word("gitti"), sentence.GetWord(5));
        }

        [Test]
        public void TestGetIndex() {
            Assert.AreEqual(0, sentence.GetIndex(new Word("ali")));
            Assert.AreEqual(2, sentence.GetIndex(new Word("at")));
            Assert.AreEqual(5, sentence.GetIndex(new Word("gitti")));
        }

        [Test]
        public void TestWordCount() {
            Assert.AreEqual(6, sentence.WordCount());
        }

        [Test]
        public void TestCharCount() {
            Assert.AreEqual(27, sentence.CharCount());
        }
    }
}
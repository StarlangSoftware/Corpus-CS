using Corpus;
using NUnit.Framework;

namespace Test
{
    public class TurkishSplitterTest
    {
        TurkishSplitter splitter;

        [SetUp]
        public void Setup()
        {
            splitter = new TurkishSplitter();
        }

        [Test]
        public void TestSplit1()
        {
            Assert.AreEqual(14, splitter.Split("Cin Ali, bak! " +
                                            "At. " +
                                            "Bak, Cin Ali, bak. " +
                                            "Bu at. " +
                                            "Baba, o atı bana al. " +
                                            "Cin Ali, bu at. " +
                                            "O da ot. " +
                                            "Baba, bu ata ot al. " +
                                            "Cin Ali, bu ot, o da at. " +
                                            "Otu al, ata ver. " +
                                            "Bak, Suna! " +
                                            "Cin Ali, ata ot verdi. " +
                                            "Su verdi. " +
                                            "Cin Ali, ata bir kova da su verdi.").Count);
        }
        
        [Test]
        public void TestSplit2()
        {
            Assert.AreEqual(1, splitter.Split("WWW.GOOGLE.COM").Count);
        }

        [Test]
        public void TestSplit3()
        {
            Assert.AreEqual(1, splitter.Split("www.google.com").Count);
        }

        [Test]
        public void TestSplit4()
        {
            Assert.AreEqual(1, splitter.Split("1.adımda ve 2.adımda ne yaptın").Count);
            Assert.AreEqual(7, splitter.Split("1.adımda ve 2.adımda ne yaptın")[0].WordCount());
        }

        [Test]
        public void TestSplit5()
        {
            Assert.AreEqual(1, splitter.Split("1. adımda ve 2. adımda ne yaptın").Count);
            Assert.AreEqual(7, splitter.Split("1. adımda ve 2. adımda ne yaptın")[0].WordCount());
        }

        [Test]
        public void TestSplit6()
        {
            Assert.AreEqual(1, splitter.Split("Burada II. Murat ve I. Ahmet oyun oynadı").Count);
            Assert.AreEqual(8, splitter.Split("Burada II. Murat ve I. Ahmet oyun oynadı")[0].WordCount());
        }

    }
}
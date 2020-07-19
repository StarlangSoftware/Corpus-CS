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
        public void TestSplit()
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
    }
}
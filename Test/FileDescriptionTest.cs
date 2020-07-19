using Corpus;
using NUnit.Framework;

namespace Test
{
    public class FileDescriptionTest
    {
        [Test]
        public void TestGetIndex()
        {
            var fileDescription = new FileDescription("mypath", "1234.train");
            Assert.AreEqual(1234, fileDescription.GetIndex());
            fileDescription = new FileDescription("mypath", "0000.test");
            Assert.AreEqual(0, fileDescription.GetIndex());
            fileDescription = new FileDescription("mypath", "0003.dev");
            Assert.AreEqual(3, fileDescription.GetIndex());
            fileDescription = new FileDescription("mypath", "0020.train");
            Assert.AreEqual(20, fileDescription.GetIndex());
            fileDescription = new FileDescription("mypath", "0304.dev");
            Assert.AreEqual(304, fileDescription.GetIndex());
        }

        [Test]
        public void TestGetExtension()
        {
            var fileDescription = new FileDescription("mypath", "1234.train");
            Assert.AreEqual("train", fileDescription.GetExtension());
            fileDescription = new FileDescription("mypath", "0000.test");
            Assert.AreEqual("test", fileDescription.GetExtension());
            fileDescription = new FileDescription("mypath", "0003.dev");
            Assert.AreEqual("dev", fileDescription.GetExtension());
        }

        [Test]
        public void TestGetFileName()
        {
            var fileDescription = new FileDescription("mypath", "0003.train");
            Assert.AreEqual("mypath/0003.train", fileDescription.GetFileName());
            Assert.AreEqual("newpath/0003.train", fileDescription.GetFileName("newpath"));
            Assert.AreEqual("newpath/0000.train", fileDescription.GetFileName("newpath", 0));
            Assert.AreEqual("newpath/0020.train", fileDescription.GetFileName("newpath", 20));
            Assert.AreEqual("newpath/0103.train", fileDescription.GetFileName("newpath", 103));
            Assert.AreEqual("newpath/0000.dev", fileDescription.GetFileName("newpath", 0, "dev"));
            Assert.AreEqual("newpath/0020.dev", fileDescription.GetFileName("newpath", 20, "dev"));
            Assert.AreEqual("newpath/0103.dev", fileDescription.GetFileName("newpath", 103, "dev"));
        }
    }
}
using Laba1;
using NUnit.Framework;

namespace LabaTests
{
    public class SerializerTest
    {
        private Serializer serializer;

        [SetUp]
        public void Setup()
        {
            serializer = new Serializer();
        }

        [Test]
        public void TestJson()
        {
            var oldDisk = new SsdDisk
            {
                DiskSizeMb = 500,
                DataTransferRateMbps = 2000,
                FormFactor = FormFactor.M2,
                Model = "Panterra Ultra Soft"
            };

            var diskJson = serializer.SerializeJson(oldDisk);
            var newDisk = serializer.DeserializeJson<SsdDisk>(diskJson);

            Assert.AreEqual(oldDisk.DataTransferRateMbps, newDisk.DataTransferRateMbps);
            Assert.AreEqual(oldDisk.DiskSizeMb, newDisk.DiskSizeMb);
            Assert.AreEqual(oldDisk.FormFactor, newDisk.FormFactor);
            Assert.AreEqual(oldDisk.Model, newDisk.Model);
        }

        [Test]
        public void TestJsonRandomType()
        {
            var json = serializer.SerializeJson(42);
            var actual = serializer.DeserializeJson<int>(json);

            Assert.AreEqual(42 ,actual);
        }

        [Test]
        public void TestXmlRandomType()
        {
            var json = serializer.SerializeXml("randomString");
            var actual = serializer.DeserializeXml<string>(json);

            Assert.AreEqual("randomString" ,actual);
        }

        [Test]
        public void TestXml()
        {
            var oldDisk = new SsdDisk
            {
                DiskSizeMb = 500,
                DataTransferRateMbps = 2000,
                FormFactor = FormFactor.M2,
                Model = "Panterra Ultra Soft"
            };

            var diskXml = serializer.SerializeXml(oldDisk);
            var newDisk = serializer.DeserializeXml<SsdDisk>(diskXml);

            Assert.AreEqual(oldDisk.DataTransferRateMbps, newDisk.DataTransferRateMbps);
            Assert.AreEqual(oldDisk.DiskSizeMb, newDisk.DiskSizeMb);
            Assert.AreEqual(oldDisk.FormFactor, newDisk.FormFactor);
            Assert.AreEqual(oldDisk.Model, newDisk.Model);
        }

    }
}
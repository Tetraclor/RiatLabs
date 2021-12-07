using System.Threading;
using FluentAssertions;
using Laba1;
using Laba2;
using NUnit.Framework;

namespace LabaTests
{
    public class ClientTest
    {
        private MyClient Client;

        [SetUp]
        public void Setup()
        {
            Client = new MyClient("http://localhost:5000/api/");            
        }

        [Test]
        public void TestPing()
        {
            Client.Ping();
        }

        static Input input;

        [Test]
        
        public void PostAndGetData()
        {
            input = new Input()
            {
                K = 3,
                Muls = new int[] { 1, 5, 10},
                Sums = new decimal[] { (decimal)0.3, (decimal)0.5, (decimal) 0.3 }
            };
            Client.Post(input);

            var should = Program.CalcOutput(input);
            var output = Client.Get();

            output.Should().BeEquivalentTo(should);
        }

        [Test]
        public void TestStop()
        {
            Client.Ping();
            Client.Stop();

            Thread.Sleep(1000);
            try
            {
                Client.Ping();
            }
            catch
            {
                Assert.IsTrue(true);
                return;
            }

            Assert.Fail();
        }
    }
}
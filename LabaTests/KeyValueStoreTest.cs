using System;
using FluentAssertions;
using Laba2;
using NUnit.Framework;

namespace LabaTests
{

    public class KeyValueStoreTest
    {
        private KeyValueStore keyValueStore;

        [SetUp]
        public void Setup()
        {
            keyValueStore = new KeyValueStore("http://localhost:5000/study/");
            //keyValueStore = new KeyValueStore();
        }

        [Test]
        public void TestFindNull()
        {
            var randomKey = Guid.NewGuid().ToString();
            var actual = keyValueStore.Find(randomKey);
            actual.Should().BeNull();
        }

        [Test]
        public void TestCreateAndFind()
        {
            var randomKey = Guid.NewGuid().ToString();
            var randomValue = Guid.NewGuid().ToString();

            var keyValue = new KeyValue
            {
                Key = randomKey,
                Value = randomValue
            };

            keyValueStore.Create(keyValue);

            var actual = keyValueStore.Find(keyValue.Key);
            actual.Should().BeEquivalentTo(keyValue);
        }

        [Test]
        public void TestCreateAndUpdateAndFind()
        {
            var randomKey = Guid.NewGuid().ToString();
            var randomValue = Guid.NewGuid().ToString();

            var keyValue = new KeyValue
            {
                Key = randomKey,
                Value = randomValue
            };

            keyValueStore.Create(keyValue);
            var newValue = "updatedByTest" + Guid.NewGuid();
            keyValueStore.Update(keyValue.Key, newValue);

            var actual = keyValueStore.Find(keyValue.Key);
            actual.Key.Should().Be(keyValue.Key);
            actual.Value.Should().Be(newValue);
        }

        [Test]
        public void TestUpdateNonExistent()
        {
            var randomKey = Guid.NewGuid().ToString();
            
            var exception = Assert.Throws<Exception>(() => keyValueStore.Update(randomKey, Guid.NewGuid().ToString()));
            exception.Message.Should()
                .BeEquivalentTo(
                    $"Плохой http status code BadRequest. Сообщение Key {randomKey} is not presented in store.");

        }

        [Test]
        public void TestCreateExistedKey()
        {
            var randomKey = Guid.NewGuid().ToString();
            var randomValue = Guid.NewGuid().ToString();

            var keyValue = new KeyValue
            {
                Key = randomKey,
                Value = randomValue
            };

            keyValueStore.Create(keyValue);

            var actual = keyValueStore.Find(keyValue.Key);
            actual.Should().BeEquivalentTo(keyValue);

            var exception = Assert.Throws<Exception>(() => keyValueStore.Create(keyValue));
            exception.Message.Should()
                .BeEquivalentTo(
                    $"Плохой http status code BadRequest. Сообщение Key {keyValue.Key} is already presented in store.");
        }
    }
}
namespace Laba2
{
    public interface IKeyValueStore
    {
        void Create(KeyValue keyValue);
        KeyValue Find(string key);
        void Update(string key, string value);
    }
}
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Laba1
{
    public class Serializer
    {
        public string SerializeJson<T>(T disk)
        {
            return JsonConvert.SerializeObject(disk);
        }

        public T DeserializeJson<T>(string disk)
        {
            return JsonConvert.DeserializeObject<T>(disk);
        }

        private static readonly XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            OmitXmlDeclaration = true
        };

        private static readonly ConcurrentDictionary<Type, XmlSerializer> serializers 
            = new ConcurrentDictionary<Type, XmlSerializer>();

        private static readonly XmlSerializerNamespaces ns =
            new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

        public string SerializeXml<T>(T disk)
        {
            using var stream = new StringWriter();
            using var writer = XmlWriter.Create(stream, settings);

            var serializer = serializers.GetOrAdd(typeof(T), CreateSerializer);

            serializer.Serialize(writer, disk, ns);
            return stream.ToString();
        }

        private static XmlSerializer CreateSerializer(Type type) => new XmlSerializer(type);

        public T DeserializeXml<T>(string disk)
        {
            var serializer = serializers.GetOrAdd(typeof(T), CreateSerializer);

            using var reader = new StringReader(disk);
            return (T)serializer.Deserialize(reader);
        }
    }
}
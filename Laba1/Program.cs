using System;
using System.IO;
using System.Linq;

namespace Laba1
{
    public class Program
    {
        static void Main(string[] args)
        {

            var input = @"Xml
<Input>
 <K>10</K>
 <Sums>
 <decimal>1.01</decimal>
 <decimal>2.02</decimal>
 </Sums>
 <Muls>
 <int>1</int>
 <int>4</int>
 </Muls>
</Input>
";
            Problem1(new StringReader(input), Console.Out);

            input = @"Json
{""K"":10,""Sums"":[1.01,2.02],""Muls"":[1,4]}
";
            Problem1(new StringReader(input), Console.Out);

            return;
           

            var ssdDisk = new SsdDisk
            {
                DiskSizeMb = 500,
                DataTransferRateMbps = 2000,
                FormFactor = FormFactor.M2,
                Model = "Panterra Ultra Soft"
            };

            Console.WriteLine($"Несериализованный объект {ssdDisk}");

            var serializer = new Serializer();
            var ssdDiskSerialized = serializer.SerializeJson(ssdDisk);

            Console.WriteLine($"Cериализованный объект {ssdDiskSerialized}");

            var deserializedDisk = serializer.DeserializeJson<SsdDisk>(ssdDiskSerialized);

            Console.WriteLine($"Десериализованный диск" +
                              $"DTR - {deserializedDisk.DataTransferRateMbps} \n" +
                              $"DS - {deserializedDisk.DiskSizeMb} \n" +
                              $"FF - {deserializedDisk.FormFactor}\n" +
                              $"M - {deserializedDisk.Model}.");

            var ssdDiskSerializedXml = serializer.SerializeXml(ssdDisk);

            Console.WriteLine($"Cериализованный объект XML {ssdDiskSerializedXml}");

            var deserializedDiskXml = serializer.DeserializeXml<SsdDisk>(ssdDiskSerializedXml);

            Console.WriteLine($"Десериализованный диск XML \n" +
                              $"DTR - {deserializedDiskXml.DataTransferRateMbps} \n" +
                              $"DS - {deserializedDiskXml.DiskSizeMb} \n" +
                              $"FF - {deserializedDiskXml.FormFactor}\n" +
                              $"M - {deserializedDiskXml.Model}.");
        }

        static void Problem1(TextReader reader, TextWriter writer)
        {
            var serializer = new Serializer();
            var typeSer = reader.ReadLine();
            var inputText = reader.ReadToEnd();
            Input input;
            if(typeSer == "Xml")
            {
                input = serializer.DeserializeXml<Input>(inputText);
            }
            else if(typeSer == "Json")
            {
                input = serializer.DeserializeJson<Input>(inputText);
            }
            else
            {
                return;
            }


            var output =CalcOutput(input);


            var outstr = "";

            if (typeSer == "Xml")
            {
                outstr = serializer.SerializeXml(output);
            }
            else if (typeSer == "Json")
            {
                outstr = serializer.SerializeJson(output);
            }

            writer.WriteLine(outstr);
        }

        public static Output CalcOutput(Input input)
        {
            if (input == null) return null;

            var sorted = input.Sums.ToList();
            sorted.AddRange(input.Muls.Select(v => (decimal)v));
            sorted.Sort();

            var output = new Output()
            {
                SumResult = input.Sums.Sum() * input.K,
                MulResult = input.Muls.Aggregate((a, b) => a * b),
                SortedInputs = sorted.ToArray()
            };

            return output;
        } 
    }
}
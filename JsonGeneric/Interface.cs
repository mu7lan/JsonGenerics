using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonGeneric
{
    public class Organisation
    {
        public string Name { get; set; }

        [JsonConverter(typeof(TycoonConverter))]
        public IPerson Owner { get; set; }

        [JsonConverter(typeof(parsecConverter))]
        public parsec o { get; set; }
    }

    public interface IPerson
    {
        string Name { get; set; }
    }

    public class Tycoon : IPerson
    {
        public string Name { get; set; }
    }
    //----------------------------------
    public interface IParsec
    {
        string Name { get; set; }
    }

    public class parsec : IParsec
    {
        public string Name { get; set; }
    }

    class Interface
    {
    }
    public class TycoonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IPerson));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Tycoon>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }

    public class parsecConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IParsec));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<parsec>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Left as an exercise to the reader :)
            //    throw new NotImplementedException();
            serializer.Serialize(writer, value);
        }
    }
}

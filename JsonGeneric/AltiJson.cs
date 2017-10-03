using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonGeneric
{

    //NOT USED FOR NOW... :D

    public static class AltiJsonSerializer
    {
        public static T DeserializeJson<T>(this string jsonString, IEnumerable<Type> knownTypes = null)
        {
            if (string.IsNullOrEmpty(jsonString))
                return default(T);
            return JsonConvert.DeserializeObject<T>(jsonString,
                                                    new JsonSerializerSettings
                                                    {
                                                        TypeNameHandling = TypeNameHandling.Auto,
                                                        Converters = new List<JsonConverter>(
                                                                new JsonConverter[]
                                                                {
                                                           new JsonKnownTypeConverter(knownTypes)
                                                                })

                                                    });
        }

        public static string SerializeJson(this object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize, Formatting.Indented,
                                               new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, NullValueHandling = NullValueHandling.Ignore });
        }
    }

    public class JsonKnownTypeConverter : JsonConverter
    {
        public IEnumerable<Type> KnownTypes { get; set; }
        public JsonKnownTypeConverter(IEnumerable<Type> knownTypes)
        {
            KnownTypes = knownTypes;
        }

        protected object Create(Type objectType, JObject jObject)
        {
            if (jObject["$type"] != null)
            {
                string typeName = jObject["$type"].ToString();
                return Activator.CreateInstance(KnownTypes.First(x => typeName.Contains("." + x.Name + ",")));
            }
            throw new InvalidOperationException("No supported type");
        }

        public override bool CanConvert(Type objectType)
        {
            if (KnownTypes == null)
                return false;
            return (objectType.IsInterface || objectType.IsAbstract) && KnownTypes.Any(objectType.IsAssignableFrom);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);
            // Create target object based on JObject
            var target = Create(objectType, jObject);
            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    } 
}

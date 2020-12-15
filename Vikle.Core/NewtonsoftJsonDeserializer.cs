// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license.

using Newtonsoft.Json;
using System.IO;
using RestSharp.Serializers;
using RestSharp.Deserializers;

namespace Vikle.Core
{
    /// <summary>
    /// This class contains the custom implementation of a Json deserializer to workaround a problem with
    /// Restsharp requests deserialization with the DateTime type objects
    /// https://www.bytefish.de/blog/restsharp_custom_json_serializer.html
    /// https://stackoverflow.com/questions/45210430/restsharp-converts-datetimes-to-utc
    /// </summary>
    public class NewtonsoftJsonSerializer : ISerializer, IDeserializer
    {
        private Newtonsoft.Json.JsonSerializer serializer;

        public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;           
        }

        public string ContentType {
            get { return "application/json"; } // Probably used for Serialization?
            set { }
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public static NewtonsoftJsonSerializer Default
        {
            get
            {
                return new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                }); 
            }
        }
    }
}
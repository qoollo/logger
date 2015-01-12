

//  old Serializers
//using Newtonsoft.Json;
//using Newtonsoft.Json.Bson;

//using System.Runtime.Serialization.Json;
//using fastBinaryJSON;
//using System.Runtime.Serialization.Json;

using System;
using System.IO;
using Qoollo.Logger.Exceptions;

namespace Qoollo.Logger.Helpers
{
    /// <summary>
    /// Log message serializer
    /// </summary>
    internal static class Serializer
    {
        #region Binary

        private static System.Runtime.Serialization.DataContractSerializer _loggingEventSerializer;
        private static System.Xml.XmlDictionaryReaderQuotas _serializerReaderQuotas;
        private static readonly object _loggingEventSerializerLock = new object();

        private static System.Runtime.Serialization.DataContractSerializer LoggingEventSerializer
        {
            get
            {
                if (_loggingEventSerializer == null)
                {
                    lock (_loggingEventSerializerLock)
                    {
                        if (_loggingEventSerializer == null)
                            _loggingEventSerializer = new System.Runtime.Serialization.DataContractSerializer(typeof(Qoollo.Logger.Common.LoggingEvent));
                    }
                }
                return _loggingEventSerializer;
            }
        }

        private static System.Xml.XmlDictionaryReaderQuotas SerializerReaderQuotas
        {
            get
            {
                if (_serializerReaderQuotas == null)
                {
                    lock (_loggingEventSerializerLock)
                    {
                        if (_serializerReaderQuotas == null)
                            _serializerReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas() { MaxStringContentLength = 32 * 1024 };
                    }
                }
                return _serializerReaderQuotas;
            }
        }



        /// <summary>
        /// Сериализация объекта в бинарный вид
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns></returns>
        public static byte[] Serialize(Qoollo.Logger.Common.LoggingEvent obj)
        {
            var serializer = LoggingEventSerializer;

            var ms = new MemoryStream(2048);
            var writer = System.Xml.XmlDictionaryWriter.CreateBinaryWriter(ms);
            serializer.WriteObject(writer, obj);
            writer.Flush();

            return ms.ToArray();
        }

        /// <summary>
        /// Десериализация
        /// </summary>
        /// <param name="buffer">Набор байт</param>
        /// <returns></returns>
        public static Qoollo.Logger.Common.LoggingEvent Deserialize(byte[] buffer)
        {
            try
            {
                var serializer = LoggingEventSerializer;

                var reader = System.Xml.XmlDictionaryReader.CreateBinaryReader(buffer, SerializerReaderQuotas);
                var body = (Qoollo.Logger.Common.LoggingEvent)serializer.ReadObject(reader);

                return body;
            }
            catch (System.Runtime.Serialization.SerializationException ex)
            {
                throw new LoggerSerializationException("Unexpected message deserialization error.", ex);
            }
        }




        ///// <summary>
        ///// Сериализация объекта в бинарный вид
        ///// </summary>
        ///// <typeparam name="T">Тип</typeparam>
        ///// <param name="obj">Объект</param>
        ///// <returns></returns>
        //public static byte[] Serialize<T>(T obj)
        //{
        //    var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
            
        //    var ms = new MemoryStream();
        //    var writer = System.Xml.XmlDictionaryWriter.CreateBinaryWriter(ms);
        //    serializer.WriteObject(writer, obj);
        //    writer.Flush();

        //    return ms.ToArray();
        //}

        ///// <summary>
        ///// Десериализация
        ///// </summary>
        ///// <typeparam name="T">Тип</typeparam>
        ///// <param name="buffer">Набор байт</param>
        ///// <returns></returns>
        //public static T Deserialize<T>(byte[] buffer)
        //{
        //    try
        //    {
        //        var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));

        //        var reader = System.Xml.XmlDictionaryReader.CreateBinaryReader(buffer, new System.Xml.XmlDictionaryReaderQuotas());
        //        T body = (T)serializer.ReadObject(reader);

        //        return body;
        //    }
        //    catch (System.Runtime.Serialization.SerializationException ex)
        //    {
        //        throw new LoggerSerializationException("Ошибка при десериализации.", ex);
        //    }
        //}


        #endregion

        #region protobuf

        ///// <summary>
        ///// Сериализация объекта в бинарный вид
        ///// </summary>
        ///// <typeparam name="T">Тип</typeparam>
        ///// <param name="obj">Объект</param>
        ///// <returns></returns>
        //public static byte[] Serialize<T>(T obj)
        //{
        //    var ms = new MemoryStream();
        //    ProtoBuf.Serializer.Serialize<T>(ms, obj);

        //    byte[] src = ms.ToArray();
        //    byte[] dist = new byte[(int) ms.Length];

        //    Buffer.BlockCopy(src, 0, dist, 0, (int)ms.Length);

        //    return dist;
        //}

        ///// <summary>
        ///// Десериализация
        ///// </summary>
        ///// <typeparam name="T">Тип</typeparam>
        ///// <param name="buffer">Набор байт</param>
        ///// <returns></returns>
        //public static T Deserialize<T>(byte[] buffer)
        //{
        //    try
        //    {
        //        var ms = new MemoryStream(buffer);
        //        T body = ProtoBuf.Serializer.Deserialize<T>(ms);

        //        return body;
        //    }
        //    catch (ProtoBuf.ProtoException ex)
        //    {
        //        throw new LoggerSerializationException("Ошибка при десериализации.", ex);
        //    }
        //}

        #endregion

        #region FastBinaryJSON

        //public static byte[] Serialize(LoggingEvent data)
        //{
        //    var bytes = BJSON.Instance.ToBJSON(data);

        //    return bytes;
        //}

        //public static LoggingEvent Deserialize(byte[] bytes)
        //{
        //    var data = BJSON.Instance.ToObject<LoggingEvent>(bytes);

        //    return data;
        //}

        #endregion

        #region JSON

        //private static readonly JsonSerializer Serializer = new JsonSerializer();

        //public static string SerializeToJsonString(object objectToSerialize)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        var serializer =
        //                new DataContractJsonSerializer(objectToSerialize.GetType());
        //        serializer.WriteObject(ms, objectToSerialize);
        //        ms.Position = 0;

        //        using (var reader = new StreamReader(ms))
        //        {
        //            return reader.ReadToEnd();
        //        }
        //    }
        //}

        //public static LoggingEvent DeserializeData(string jsonData)
        //{
        //    using (var ms = new MemoryStream(Encoding.Unicode.Serialize(jsonData)))
        //    {
        //        var serializer =
        //                new DataContractJsonSerializer(typeof(LoggingEvent));

        //        return (LoggingEvent)serializer.ReadObject(ms);
        //    }
        //}


        //public static byte[] Serialize<T>(T obj)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    Serializer.Serialize<T>(ms, obj);
        //    byte[] body = ms.GetBuffer();
        //    Buffer.BlockCopy(body, 0, body, 0, (int)ms.Length);
        //    return body;
        //}

        //public static T Deserialize<T>(byte[] buffer)
        //{
        //    MemoryStream ms = new MemoryStream(buffer);
        //    T body = Serializer.Deserialize<T>(ms);
        //    return body;
        //}

        //public static byte[] SerializeToJson(LoggingEvent msg)
        //{
        //    var stream = new MemoryStream();
        //    var writer = new BsonWriter(stream);

        //    Serializer.Serialize(writer, msg);
        //    byte[] bodyBytes = stream.GetBuffer();

        //    int length = (int)stream.Length;
        //    byte[] lengthBytes = BitConverter.Serialize(length);

        //    var result = new byte[length + lengthBytes.Length];
        //    Buffer.BlockCopy(lengthBytes, 0, result, 0, lengthBytes.Length);
        //    Buffer.BlockCopy(bodyBytes, 0, result, lengthBytes.Length, (int)stream.Length);

        //    return result;
        //}

        #endregion
    }
}

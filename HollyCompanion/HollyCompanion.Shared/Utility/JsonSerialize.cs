using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace HollyCompanion.Utility
{
    public class JsonSerialize
    {

        public static String Serialize<T>(T obj,Encoding encode)
        {
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream=new MemoryStream();
            try
            {

                dataContractJsonSerializer.WriteObject(stream, obj);
                byte[] bytes = stream.ToArray();
                if (encode == null)
                    return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                return encode.GetString(bytes, 0, bytes.Length);
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        public static T Deserialize<T>(string jStr,Encoding encode)
        {
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
            try
            {
                Encoding encoding = encode ?? Encoding.UTF8;
                MemoryStream stream = new MemoryStream(encoding.GetBytes(jStr));
                T obj = (T)dataContractJsonSerializer.ReadObject(stream);
                return obj;
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }


    }
}

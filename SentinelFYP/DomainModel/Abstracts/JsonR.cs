using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace DomainModel.Abstracts
{
    public abstract class JsonR
    {
        public static string JsonSerializer(object obj)
        {
            if (null == obj)
                throw new ArgumentNullException("object");
            
            try
            {
                JavaScriptSerializer oS = new JavaScriptSerializer();
                return oS.Serialize(obj);
            }
            catch (Exception ex)
            {
                throw new FormatException(ex.Message, ex.InnerException);
            }
        }

        public static T JsonDeserializer<T>(string strJsonString)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(strJsonString);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strJsonString));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
            catch (Exception ex)
            {
                throw new FormatException(ex.Message, ex.InnerException);
            }
        }

        public static T JsonDeserializer<T>(Stream oJsonStream)
        {
            try
            {
                using (var streamReader = new StreamReader(oJsonStream))
                {
                    var serializer = new JsonSerializer();
                    return (T)serializer.Deserialize(streamReader, typeof(T));
                }
            }
            catch (Exception ex)
            {
                throw new FormatException(ex.Message, ex.InnerException);
            }
        }
    }
}

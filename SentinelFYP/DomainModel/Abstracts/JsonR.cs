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
    public static class JsonR
    {
        public static string JsonSerializer(object obj)
        {
            JavaScriptSerializer oS = new JavaScriptSerializer();
            return oS.Serialize(obj);
        }

        public static T JsonDeserializer<T>(string strJsonString)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(strJsonString);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strJsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }
}

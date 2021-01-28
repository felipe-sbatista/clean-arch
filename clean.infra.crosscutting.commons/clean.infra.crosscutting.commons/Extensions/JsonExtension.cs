using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace clean.infra.crosscutting.commons.Extensions
{
    public static class JsonExtension
    {
        public static JsonSerializerSettings JsonSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                };
            }
        }

        public static string ToJson(this object obj, bool useSettings = false)
        {
            if (useSettings) return JsonConvert.SerializeObject(obj, JsonSettings);

            return JsonConvert.SerializeObject(obj);
        }

        public static (bool isParseOk, string result, string errorMessage) TryParseToJson(this object obj, bool useSettings = false)
        {
            try
            {
                return (true, obj.ToJson(useSettings), default);
            }catch(Exception ex)
            {
                return (false, default, ex.Message);
            }
        }

        public static T ToObject<T>(this string strToObj, bool useSettings = false)
        {
            if (useSettings) return JsonConvert.DeserializeObject<T>(strToObj, JsonSettings);

            return JsonConvert.DeserializeObject<T>(strToObj);
        }

        public static (bool isParseOk, T result, string errorMessage) TryParseToObject<T>(this string strToObj, bool useSettings = false)
        {
            try
            {
                return (true, strToObj.ToObject<T>(useSettings), default);
            }
            catch (Exception ex)
            {
                return (false, default, ex.Message);
            }
        }

    }
}

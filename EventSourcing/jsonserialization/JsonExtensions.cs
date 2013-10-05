using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using ImpromptuInterface;

namespace jsonserialization
{
    public static class JsonExtensions
    {
        public static object FromJson(this string jsonObj)
        {
            return Json.Decode(jsonObj);
        }


        public static string ToJson(this object obj)
        {
            var json = ObjectToJson(obj);
            return PrettifyJson(json);
        }

        private static string ObjectToJson(object obj)
        {
            if (obj.GetType() == typeof(DynamicJsonObject)) return DynamicJsonObjectToJson(obj as DynamicJsonObject);
            if (!(obj is ExpandoObject)) return Json.Encode(obj);

            var json = "{";
            var dict = obj as IDictionary<string, object>;
            var iKey = 0;
            foreach (var key in dict.Keys)
            {
                if (iKey++ > 0) json += ", ";
                json += "\"" + key + "\": ";

                if (dict[key].GetType() == typeof(DynamicJsonObject))
                    json += DynamicJsonObjectToJson(dict[key] as DynamicJsonObject);
                else if (dict[key] is ExpandoObject)
                    json += ObjectToJson(dict[key]);
                else
                    json += Json.Encode(dict[key]);
            }
            json += "}";
            return json;
        }

        static string DynamicJsonObjectToJson(this DynamicJsonObject obj)
        {
            var json = "{";
            var iName = 0;
            foreach (var name in obj.GetDynamicMemberNames())
            {
                if (iName++ > 0) json += ", ";
                json += "\"" + name + "\": ";

                object value = Impromptu.InvokeGet(obj, name);
                json += ObjectToJson(value);
            }
            json += "}";
            return json;
        }


        // source: http://stackoverflow.com/questions/4580397/json-formatter-in-c
        private const string INDENT_STRING = "  ";
        private static string PrettifyJson(string str)
        {
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ToList().ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ToList().ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ToList().ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}

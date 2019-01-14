using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using EventExtraction.DataType;

namespace EventExtraction
{
    public static class ContentExtractor
    {
        public static List<T> JsonReader<T>(string path)
        {
            //反序列化
            using (StreamReader r = new StreamReader(@"E:\data\test.txt"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }

        public static List<T> LineJsonReader<T>(string path)
        {
            List<T> result = new List<T>();
            string line;
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                result.Add(JsonConvert.DeserializeObject<T>(line));
            }

            file.Close();
            return result;
        }

        public static List<T> CsvReader<T>(string path, bool skipFirstLine = true)
        {
            List<T> result = new List<T>();
            string[] strs = File.ReadAllLines(path);

            for (int i = skipFirstLine ? 1 : 0 ; i < strs.Length; i++)
            {
                var t = strs[i].Split(',');
                T item = (T)Activator.CreateInstance(typeof(T), t);
                result.Add(item);
            }

            return result;
        }
    }
}

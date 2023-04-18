using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegisterTaskWPF.Models;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace RegisterTaskWPF.Helpers
{
    public class FileHelper
    {
        public static void WritePersonJson(Person person)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter($"{person.Name}.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    serializer.Serialize(jw, person);
                }
            }
        }

        public static Person ReadPersonJson(string fileName)
        {
            Person persons = null;
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader($"{fileName}.json"))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    persons = serializer.Deserialize<Person>(jr);
                }
            }
            return persons;
        }

        public static void WritePersonXml(Person person)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            using (TextWriter writer = new StreamWriter($"{person.Name}.xml"))
            {
                serializer.Serialize(writer, person);
            }
        }

        public static Person ReadPersonXml(string fileName)
        {
            Person persons = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            using (TextReader reader = new StreamReader($"{fileName}.xml"))
            {
                persons = (Person)serializer.Deserialize(reader);
            }
            return persons;
        }
    }
}

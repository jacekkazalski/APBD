using lab02.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace lab02
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var path = args[0];
            string test = "tesad";

            var fi = new FileInfo(path);
            // FileInfo fi = new(path);

            var fileContent = new List<string>();

            using (StreamReader streamReader = new(fi.OpenRead()))
            {
                //string line = "";
                //string line = null;
                string line = string.Empty;

                while((line = await streamReader.ReadLineAsync()) != null)
                {
                    fileContent.Add(line);
                }
            }

            //streamReader.Dispose();
            // foreach (var item in File.ReadLines(path)){} - wczytuje od razu cały plik do pamieci
            // DateTime - typ daty

            foreach(var item in fileContent)
            {
                //cw
                Console.WriteLine(item);
            }

            var hashSet = new HashSet<Student>(new MyComparer());
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorLib;
namespace VectorTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //StreamReader reader = new StreamReader("inputText.txt");
            //var words = reader.ReadLine().Split(' ');

            var vector = new Vector("all");

            // foreach (var word in words)
            // {
            //  vector.WriteMessage(word);
            // }
            Console.WriteLine("ready");
            var line = Console.ReadLine();
            while (line != "")
            {
                vector.WriteMessage(line);
                line = Console.ReadLine();
            }
            
        }
    }
}

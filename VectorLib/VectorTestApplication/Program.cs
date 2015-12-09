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

            var vector = new Vector("testConsole");

            // foreach (var word in words)
            // {
            //  vector.WriteMessage(word);
            // }
            var start = DateTime.UtcNow.Ticks; 
            for(int i = 0; i < 1000; i++)
            {
                vector.WriteMessage(i.ToString());
            }
            var end = DateTime.UtcNow.Ticks;
            var delta = TimeSpan.FromTicks(end - start);

            Console.ReadLine();
        }
    }
}

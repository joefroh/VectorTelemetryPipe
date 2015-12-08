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
            StreamReader reader = new StreamReader("inputText.txt");
            var words = reader.ReadLine().Split(' ');

            var vector = new Vector();

            foreach (var word in words)
            {
                vector.WriteMessage(word);
            }
        }
    }
}

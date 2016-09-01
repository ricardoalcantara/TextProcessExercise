using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileStopWords = "stopwords.txt";
            var fileText = "sample_text2.txt";

            var stopWords = File.ReadAllLines(fileStopWords);
            Console.WriteLine($"stopWords: {stopWords.Length}");

            var text = File.ReadAllText(fileText);
            text = text.Replace('\n', ' ');

            var words = text.Split(' ');
            Console.WriteLine($"words: {words.Length}");

            var cw = new Regex("[a-zA-Z0-1]");

            Console.WriteLine();

            var stemmer = new PortugueseStemmer();
            var cWords = words
                            .Select(x => stemmer.WordStemming(x))
                            .Where( x => cw.IsMatch(x) && stopWords.All( y => y.Trim().ToLower() != x.Trim().ToLower()))
                            .GroupBy(x => x);

            Console.WriteLine($"cWords: {cWords.Count()}");

            foreach(var word in cWords.OrderByDescending(x => x.Count()).Take(15)) 
            {
                Console.WriteLine($"{word.Key}: {word.Count()}");
            }
        }
    }
}

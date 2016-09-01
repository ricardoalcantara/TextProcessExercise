using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var util = new Util();
            var stopWatch = new System.Diagnostics.Stopwatch();

            var fileStopWords = "stopwords.txt";
            //var fileText = "sample_text2.txt";

            stopWatch.Start();
            var stopWords = File.ReadAllLines(fileStopWords).Select( x => x.ToLower());
            Console.WriteLine($"stopWords: {stopWords.Count()} \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();

            //var text = File.ReadAllText(fileText)?.ToLower();
            var text = util.GetHTTPText("https://pt.wikipedia.org/wiki/Pok%C3%A9mon_GO", "//div[@id='bodyContent']").Result.ToLower();
            Console.WriteLine($"ReadAllText \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();
            
            text = text.Replace('\n', ' ');
            text = text.Replace('\t', ' ');

            Console.WriteLine($"Replace \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();

            var tokenA = util.TokenizerA(text);

            Console.WriteLine($"tokenA {tokenA.Count()} \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();

            var tokenB = util.TokenizerB(text);

            Console.WriteLine($"tokenB {tokenB.Count()} \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();


            var words = text.Split(' ');

            Console.WriteLine($"Split \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();

            Console.WriteLine($"words: {words.Length}");

//            var cw = new Regex("[a-zA-Z0-1]");

            Console.WriteLine();

            var stemmer = new PortugueseStemmer();
            var cWords = tokenB
                            .Where( x => x.Length > 2 && stopWords.All( y => y.Trim() != x.Trim()))
                            .Select(x => stemmer.WordStemming(x))
                            .GroupBy(x => x).ToList();

            Console.WriteLine($"Select, Where n GroupBy \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();

            Console.WriteLine($"cWords: {cWords.Count()}");

            Console.WriteLine($"Count \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();

            foreach(var word in cWords.OrderByDescending(x => x.Count()).Take(15)) 
            {
                Console.WriteLine($"{word.Key}: {word.Count()}");
            }
        }        
    }
}

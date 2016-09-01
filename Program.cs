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
            var stopWatch = new System.Diagnostics.Stopwatch();

            var fileStopWords = "stopwords.txt";
            var fileText = "sample_text2.txt";

            stopWatch.Start();
            var stopWords = File.ReadAllLines(fileStopWords);
            Console.WriteLine($"stopWords: {stopWords.Length} \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();

            var text = File.ReadAllText(fileText);
            Console.WriteLine($"ReadAllText \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();
            
            text = text.Replace('\n', ' ');
            text = text.Replace('\t', ' ');

            Console.WriteLine($"Replace \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();

            var tokenA = tokenizerA(text);

            Console.WriteLine($"tokenA {tokenA.Count()} \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();

            var tokenB = tokenizerA(text);

            Console.WriteLine($"tokenB {tokenB.Count()} \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();


            var words = text.Split(' ');

            Console.WriteLine($"Split \t {stopWatch.ElapsedMilliseconds} ms");
            stopWatch.Restart();

            Console.WriteLine($"words: {words.Length}");

            var cw = new Regex("[a-zA-Z0-1]");

            Console.WriteLine();

            var stemmer = new PortugueseStemmer();
            var cWords = tokenB
                            .Select(x => stemmer.WordStemming(x.ToLower()))
                            .Where( x => cw.IsMatch(x) && stopWords.All( y => y.Trim().ToLower() != x.Trim().ToLower()))
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

        public static List<string> tokenizerA(string text) {
            var splitRegex = new Regex("[,.?! ]");

            var list = new List<string>();

            string tmp = string.Empty;

            foreach(char c in text) {
                if (c == ' ') {
                    if (tmp.Length > 0) {
                        list.Add(tmp);
                        tmp = string.Empty;
                    }
                } else if (splitRegex.IsMatch(c.ToString())) {
                    if (tmp.Length > 0) {
                        list.Add(tmp);
                        tmp = string.Empty;
                    }

                    list.Add(c.ToString());
                } else {
                    tmp += c;
                }                
            }

            return list;
        }

        public static List<string> tokenizerB(string text) {
            var splitRegex = new Regex("[,.?!]");

            var list = new List<string>();

            string tmp = string.Empty;

            foreach(char c in text) {
                if (!splitRegex.IsMatch(c.ToString())) {
                    if (c == ' ') continue;
                    tmp += c;
                } else {
                    if (tmp.Length > 0) {
                        list.Add(tmp);
                        tmp = string.Empty;
                    }

                    list.Add(c.ToString());
                }                           
            }

            return list;
        }
    }
}

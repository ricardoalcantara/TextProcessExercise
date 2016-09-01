using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ConsoleApplication
{
	public class Util 
    {
        public Util() 
        {

        }

        public List<string> TokenizerA(string text) 
        {
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

        public List<string> TokenizerB(string text) 
        {
            var splitRegex = new Regex("[,.?!()]");

            var list = new List<string>();

            string tmp = string.Empty;

            foreach(char c in text) {
                if (!splitRegex.IsMatch(c.ToString())) {
                     if (c == ' ') {
                        if (tmp.Length > 0) {
                            list.Add(tmp);
                            tmp = string.Empty;
                        }
                    } else {
                        tmp += c;
                    }
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

        public async Task<string> GetHTTPText(string url, string path = null) 
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

            var response = await httpClient.GetAsync(new Uri(url));

            response.EnsureSuccessStatusCode();

            var html =  await response.Content.ReadAsStringAsync();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var node = htmlDocument.DocumentNode;

            var currPath = path == null ? "//body" : path;

            var body = node.SelectSingleNode(currPath);

            return body.InnerText;
            //using (var responseStream = await response.Content.ReadAsStreamAsync())
            //using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
            //using (var streamReader = new StreamReader(decompressedStream))
            // {
            //     return streamReader.ReadToEnd();
            // };
            
        }
    }
}
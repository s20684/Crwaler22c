using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            var websiteUrl = args[0];
            Console.WriteLine(websiteUrl);

            //HttpClient httpClient = new HttpClient();

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(websiteUrl);
            //Console.WriteLine(response);
            var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(content);
            //var regex = new Regex("\\w{5}@pja.edu.pl");
            //var regex = new Regex("\\w{5}@\\w{3}.\\w{3}.\\w{2}");
            //var regex = new Regex("\\w{5}@.*");
            var regex = new Regex("([a-zA-Z0-9+._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9_-]+)");
            //var a = $"Content {content}";
            //var b = @"\.-]";

            var matchCollection = regex.Matches(content);
            foreach (var item in matchCollection) {
                Console.WriteLine(item);
            }
            
        }
    }
}

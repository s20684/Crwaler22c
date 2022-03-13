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

            if (args.Length < 1){
                Console.WriteLine("Brak argumentów");
                throw new ArgumentNullException();  
            }

            if (!(Uri.IsWellFormedUriString(websiteUrl, UriKind.Absolute))){
                Console.WriteLine("Niepoprawny adres URL");
                throw new ArgumentException();
            }

            var httpClient = new HttpClient();

            try
            {
                var response = await httpClient.GetAsync(websiteUrl);
                var content = await response.Content.ReadAsStringAsync();
                var regex = new Regex("([a-zA-Z0-9+._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9_-]+)");
                var matchCollection = regex.Matches(content);
                if (matchCollection.Count == 0)
                {
                    Console.WriteLine("Nie znaleziono adresów email");
                }
                else
                {
                    HashSet<String> hashSetMatch = new();
                    foreach (Match matchItem in matchCollection)
                    {
                        hashSetMatch.Add(matchItem.Value);
                    }
                    if (hashSetMatch.Count >= 1)
                        foreach (string item in hashSetMatch)
                            Console.WriteLine(item);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd w czasie pobierania strony");
            }
            finally
            {
                httpClient.Dispose();
            }
            }
            
        }
    }

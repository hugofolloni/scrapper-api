using scrapper_api.Models;
using HtmlAgilityPack;
using NuGet.Common;


namespace scrapper_api.Controllers{
    public class Scrapper {
        public Page generateRandomPage(string? name){
            Page page = new Page();
            Random random = new Random();

            page.Name = name;

            int number = random.Next(0, 20);
            page.Count = number;

            return page;
        }

        public int getCount(string? Name, string? Word){
            string Response = CallUrl(Name).Result;
            List<string> Tokens = ParseHtml(Response);

            int Count = 0;

            foreach(string Token in Tokens){
                if(Token.Contains(Word!)){
                    Count++;
                }
            }

            return Count;
        }

        private static async Task<string> CallUrl(string? fullUrl){
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);
            return response;
        }

        private List<string> ParseHtml(string html){
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var Texts = htmlDoc.DocumentNode.InnerText.Split(" ");
            List<string> Strings = new List<string>();            

            foreach(string Element in Texts){
                if(Element.Length > 0 && !Element.Contains("\n")){
                    Strings.Add(Element);
                }
            }


            return Strings;
        }
    }
}
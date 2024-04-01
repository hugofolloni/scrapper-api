using scrapper_api.Models;

namespace scrapper_api.Controllers{
    public class Scrapper {
        public Page generateRandomPage(string? name){
            Page page = new Page();
            Random random = new Random();

            page.Name = name;

            int number = random.Next(0, 20);
            Console.Write(number);
            page.Count = number;

            return page;
        }
    }
}
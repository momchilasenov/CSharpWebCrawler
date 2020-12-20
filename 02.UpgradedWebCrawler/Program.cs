using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace UpgradedWebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            StartCrawler();

            Console.ReadLine();
            
        }

        private static async Task StartCrawler()
        {
            var url = "https://laptop.bg/laptops-all?utf8=%E2%9C%93&price_btw_all=all&search%5Bprice_gte%5D=&search%5Bprice_lte%5D=&search%5Bbrand_id_in%5D%5B%5D=591149100&search%5Bbrand_id_in%5D%5B%5D=1239602261&type_laptop_id_in_all=all&used_for_type_id_in_all=all&basic_color_id_in_all=all&cpu_type_id_in_all=all&ram_type_capacity_id_in_all=all&videocard_version_id_in_all=all&vga_type_id_in_all=all&hdd_type_size_id_in_all=all&ssd_size_id_in_all=all&display_size_type_btw_all=all&display_type_id_in_all=all&display_refreshrate_id_in_all=all&resolution_size_id_in_all=all&battery_type_id_in_all=all&weight_type_btw_all=all&os_type_id_in_all=all&warranty_size_btw_all=all&per_page=100&search%5Bs%5D=promo_asc";

            HttpClient httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            html = Regex.Replace(html, @"\n", "");

            Regex regex = new Regex(@"<h2>(?<model>.*?)<\/h2>.*?<li>(?<cpu>.*?)<\/li>.*?<span class='price'>(?<price>.*?)<span class='currency'>");

            MatchCollection matches = regex.Matches(html);
            int counter = 0;

            foreach (Match match in matches)
            {
                counter++;

                Laptop laptop = new Laptop();
                laptop.Model = match.Groups["model"].Value;
                laptop.CPU = match.Groups["cpu"].Value;
                laptop.Price = int.Parse(match.Groups["price"].Value);

                if (laptop.Model.Contains('>') || laptop.CPU.Contains('>'))
                {
                    counter--;
                }
                else
                {
                    Console.WriteLine($"Laptop #{counter} {laptop.Model}");
                    Console.WriteLine($"CPU: {laptop.CPU}");
                    Console.WriteLine($"Price: {laptop.Price}");
                    Console.WriteLine();
                }
            }
        }
    }

    public class Laptop
    {
        public string Model { get; set; }

        public string CPU { get; set; }

        public int Price { get; set; }

    }
}

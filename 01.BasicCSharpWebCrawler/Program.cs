using System;
using System.IO;
using System.Text.RegularExpressions;

namespace BasicCSharpWebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            string laptopbg = "";
            using (StreamReader reader =
                new StreamReader("C:\\Users\\Momchil Asenov\\source\\repos\\BasicCSharpWebCrawler\\BasicCSharpWebCrawler\\pageSource.txt"))
            {
                laptopbg = reader.ReadToEnd();
            }

            laptopbg = Regex.Replace(laptopbg, @"\n", "");

            Regex regex = new Regex(@"<h2>(?<model>.*?)<\/h2>.*?<li>(?<cpu>.*?)<\/li>.*?<span class='price'>(?<price>.*?)<");

            MatchCollection matches = regex.Matches(laptopbg);
            int counter = 0;

            foreach (Match match in matches)
            {
                string model = match.Groups["model"].Value;
                string cpu = match.Groups["cpu"].Value;
                int price = int.Parse(match.Groups["price"].Value);
                counter++;

                if (match.Groups["model"].Value.Contains('>') || match.Groups["cpu"].Value.Contains('>'))
                {
                    counter--;
                }
                else
                {
                    Console.WriteLine($"Laptop #{counter} {model}");
                    Console.WriteLine($"CPU: {cpu}");
                    Console.WriteLine($"Price: {price}");
                    Console.WriteLine();
                }
            }
        }
    }
}

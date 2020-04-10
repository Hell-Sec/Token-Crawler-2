using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;

namespace Discord_Token_Scraper
{
	class Program
	{
		static void Main(string[] args)
		{

			var version = ("0.1");
			var Log = "[LOG]";
		Scraper:
			Console.Clear();
			Console.Title = "Lucifer's Angel | Token Scraper";
			var Anon = new string[]
			{
				$"Token Scraper by Lucifer's Angel\nVersion : {version}"
			};

			Console.ForegroundColor = ConsoleColor.Blue;

			foreach (string line in Anon) {Console.WriteLine(line); }
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.White;

			Console.WriteLine("Enter Link To Scrape From");
			Console.Write("> ");

			string link = Console.ReadLine();

			try
			{
				int count = 0;
				List<string> Links = new List<string>();
				using (WebClient wc = new WebClient())
				{
					string s = wc.DownloadString(link);
					Regex r = new Regex(@"[\w-]{24}\.[\w-]{6}\.[\w-]{27}");
					foreach (Match m in r.Matches(s))
					{
						count++;
						Links.Add(m.ToString());
						Console.WriteLine($"{count} : {m}");
					}
				}

				using (TextWriter tw = new StreamWriter($"Scraped.txt"))
				{
					foreach (string line in Links)
					{
						tw.WriteLine(line.ToString());
					}
				}

				{
					Console.WriteLine();
					Console.WriteLine($"{Log} : Scraped {count.ToString()} Tokens From {link}");
					Console.WriteLine("Press Enter To Continue");
					Console.ReadKey();

					goto Scraper;
				}
			}
			catch
			{
				Console.Clear();

				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("[ERROR] : Enter A Valid Link");
				Thread.Sleep(5000);

				goto Scraper;
			}
		}
	}
}

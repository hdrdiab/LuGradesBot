using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LuGradesBot.Services
{
	class GradesParser : IGradesParser
	{
		Login login;

		public string GetSeason()
		{
			Console.WriteLine("\nEnter the season (spring or fall):");
			string season = Console.ReadLine();
			return season;
		}

		public string GetYear()
		{
			Console.WriteLine("\nEnter the academic year of the form yyyy-yyyy:");
			string year = Console.ReadLine();
			return year;
		}

		public void SelectYear(IWebDriver driver)
		{
			string _year = GetYear();
			int i = 0;
			SelectElement select = new SelectElement(driver.FindElement(By.Id("maincontent_academicdrop")));
			var options = select.Options;
			while (i == 0)
			{
				foreach (IWebElement option in options)
				{
					if (_year == option.Text)
					{
						i++;
						break;
					}
				}
				if(i == 0)
				{
					Console.WriteLine("\nNot Found");
					_year=GetYear();
				}
			}
			select.SelectByText(_year);
		}

		public void SelectSeason(IWebDriver driver)
		{
			string _season = GetSeason();
			int i = 0;
			SelectElement select = new SelectElement(driver.FindElement(By.Id("maincontent_semesterdrop")));
			var options = select.Options;
			while (i == 0)
			{
				foreach (IWebElement option in options)
				{
					if (_season == option.Text)
					{
						i++;
						break;
					}
				}
				if (i == 0)
				{
					Console.WriteLine("\nNot Found");
					_season = GetSeason();
				}
			}
			select.SelectByText(_season);
		}

		public void GetGrades(IWebDriver driver)
		{
			IWebElement table = driver.FindElement(By.Id("maincontent_GridView1"));
			IList<IWebElement> trows = table.FindElements(By.TagName("tr"));
			foreach(IWebElement trow in trows)
			{
				Console.WriteLine(trow.Text + "\n");
			}
		}
	}
}

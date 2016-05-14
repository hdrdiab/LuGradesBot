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
		ILogin login;
		public string GetSeason()
		{
			Console.WriteLine("\nEnter the season (spring or fall):");
			string season = Console.ReadLine();
			return season;
		}

		public string GetYear()
		{
			Console.WriteLine("\nEnter the academic year (yyyy-yyyy):");
			string year = Console.ReadLine();
			return year;
		}

		public void SelectYear(IWebDriver driver)
		{
			if (login.CheckUrl(driver, Globals.AccountUrl))
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
					if (i == 0)
					{
						Console.WriteLine("\nNot Found");
						_year = GetYear();
					}
				}
				select.SelectByText(_year);
			}
			else
			{
				Console.WriteLine("Refreshing..");
				driver.Navigate().Refresh();
			}
		}

		public void SelectSeason(IWebDriver driver)
		{
			if (login.CheckUrl(driver, Globals.AccountUrl))
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
			else
			{
				Console.WriteLine("Refreshing..");
				driver.Navigate().Refresh();
			}
		}

		public void GetGrades(IWebDriver driver)
		{
			if (login.CheckUrl(driver, Globals.AccountUrl))
			{
				int i;
				IWebElement table = driver.FindElement(By.Id("maincontent_GridView1"));
				IList<IWebElement> trows = table.FindElements(By.TagName("span"));
				Console.WriteLine("\n");
				for (i = 3; i < trows.Count; i++)
				{
					if (trows[i].Text != trows[i - 1].Text)
						Console.WriteLine(trows[i].Text);
				}
			}
			else
			{
				Console.WriteLine("Refreshing..");
				driver.Navigate().Refresh();
			}
		}
	}
}

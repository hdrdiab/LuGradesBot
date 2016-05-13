using LuGradesBot.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Drawing;

namespace LuGradesBot
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			using (IWebDriver driver = new FirefoxDriver())
			{
				//driver.Manage().Window.Position=new Point(-2000, 0);
				Login login = new Login();
				login.StartPage(driver);
				login.SubmitForm(driver);
				GradesParser parser = new GradesParser();
				parser.SelectYear(driver);
				parser.SelectSeason(driver);
				parser.GetGrades(driver);
				Console.ReadKey();
				driver.Quit();
			}
		}
	}
}
using LuGradesBot.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Drawing;
using System.Timers;

namespace LuGradesBot
{
	internal class Program
	{
		private static Timer aTimer;
		private static void Main(string[] args)
		{
			FirefoxProfile profile = new FirefoxProfile();
			using (IWebDriver driver = new FirefoxDriver(profile))
			{
				//driver.Manage().Window.Position=new Point(-2000, 0);
				Login login = new Login();
				login.StartPage(driver);
				login.SubmitForm(driver);
				GradesParser parser = new GradesParser(login);
				parser.SelectYear(driver);
				parser.SelectSeason(driver);
				parser.GetGrades(driver);
				aTimer = new Timer(60000);
				Console.WriteLine("\n Page will refresh every 1 minute,Press Any Key to stop and exit:");
				aTimer.Elapsed += (sender, e) => parser.RefreshPage(sender, e,driver);
				aTimer.Enabled = true;
				Console.ReadKey();
				driver.Quit();
			}
		}
	}
}
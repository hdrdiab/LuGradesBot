using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;

namespace LuGradesBot
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			using (IWebDriver driver = new ChromeDriver(@"C:\Program Files (x86)\chromedriver_win32"))
			{
				//driver.Manage().Window.Position=new Point(-2000, 0);
				Login login = new Login();
				login.StartPage(driver);
				login.SubmitForm(driver);
				Console.ReadKey();
				driver.Quit();
			}
		}
	}
}
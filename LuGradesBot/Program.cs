using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace LuGradesBot
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//using (IWebDriver driver = new ChromeDriver(@"C:\Program Files (x86)\chromedriver_win32"))
			//{
			//	driver.Navigate().GoToUrl("http://ulfg.ul.edu.lb/login.aspx");
			//	Console.WriteLine("Username:");
			//	string uname = Console.ReadLine();
			//	Console.WriteLine("/n Password:");
			//	string pass = Console.ReadLine();
			//	try
			//	{
			//		IWebElement username = driver.FindElement(By.Id("maincontent_tbUser"));
			//		IWebElement password = driver.FindElement(By.Id("maincontent_tbPassword"));
			//		username.SendKeys(uname);
			//		password.SendKeys(pass);
			//		driver.FindElement(By.Id("maincontent_Button1")).Click();
			//		if (String.Compare(driver.Url.ToString(), "ulfg.ul.edu.lb/evaluation/formcgeneral.aspx") < 0)
			//		{
			//			driver.Navigate().GoToUrl("http://ulfg.ul.edu.lb/account/gradeuser1.aspx");
			//		}
			//	}
			//	catch (TimeoutException e)
			//	{
			//		Console.WriteLine(e.ToString());
			//	}

			//	Console.ReadKey();
			//	driver.Quit();
			//}
		}
	}
}
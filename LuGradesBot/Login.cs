﻿using LuGradesBot.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
namespace LuGradesBot
{
	internal class Login : ILogin
	{

		public bool CheckUrl(IWebDriver driver, string url)
		{
			try
			{
				var x = String.Compare(driver.Url.ToString(), url);
				if (x == 0)
					return true;
				else
					return false;
			}
			catch(WebDriverException e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}
		public string ReadPassword()
		{
			string pass = "";
			ConsoleKeyInfo key;
			do
			{
				key = Console.ReadKey(true);
				// Backspace Should Not Work
				if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
				{
					pass += key.KeyChar;
					Console.Write("*");
				}
				else
				{
					if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
					{
						pass = pass.Substring(0, (pass.Length - 1));
						Console.Write("\b \b");
					}
				}
			}
			// Stops Receving Keys Once Enter is Pressed
			while (key.Key != ConsoleKey.Enter);
			return pass;
		}
		public void StartPage(IWebDriver driver)
		{
				Console.WriteLine("Connecting...\n");
				driver.Navigate().GoToUrl("http://ulfg.ul.edu.lb/login.aspx");

			while (!CheckUrl(driver,Globals.StartUrl))
			{				
				try
				{
					Console.WriteLine("Refreshing...\n");
					driver.Navigate().Refresh();
				}
				catch(WebDriverException e) 
				{
					Console.WriteLine(e.Message);
					Console.WriteLine("\nRefreshing..\n");
					driver.Navigate().Refresh();
				}
			}
			Console.WriteLine("Connected :)");
		}

		public void SubmitForm(IWebDriver driver)
		{
			Console.WriteLine("UserName:");
			string _username = Console.ReadLine();
			Console.WriteLine("\nPassword:");
			string _password = ReadPassword();
			try
			{
				IWebElement username = driver.FindElement(By.Id("maincontent_tbUser"));
				IWebElement password = driver.FindElement(By.Id("maincontent_tbPassword"));
				username.SendKeys(_username);
				password.SendKeys(_password);
				driver.FindElement(By.Id("maincontent_Button1")).Click();
			}
			catch (WebDriverException e)
			{
				Console.WriteLine(e.ToString());
				Console.WriteLine("\nReconnecting");
				SubmitForm(driver);
			}
			if (CheckUrl(driver, Globals.StartUrl))
			{
				Console.WriteLine("\nCheck UserName and Password ");
				SubmitForm(driver);
			}
			while (!CheckUrl(driver, Globals.AccountUrl))
			{
				driver.Navigate().GoToUrl("http://ulfg.ul.edu.lb/account/gradeuser1.aspx");
			}
			Console.WriteLine("\nLogged in Successfully");
		}
	}
}

﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LuGradesBot.Services
{
	interface IGradesParser
	{
		string GetYear();
		string GetSeason();
		void SelectYear(IWebDriver driver);
		void SelectSeason(IWebDriver driver);
		void GetGrades(IWebDriver driver);
		void RefreshPage(object sender, ElapsedEventArgs e, IWebDriver driver);
	}
}

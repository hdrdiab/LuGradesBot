using OpenQA.Selenium;
using System.Timers;

namespace LuGradesBot.Services
{
	internal interface IGradesParser
	{
		string GetYear();

		string GetSeason();

		void SelectYear(IWebDriver driver);

		void SelectSeason(IWebDriver driver);

		void GetGrades(IWebDriver driver);

		void RefreshPage(object sender, ElapsedEventArgs e, IWebDriver driver);
	}
}
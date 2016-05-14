using OpenQA.Selenium;

namespace LuGradesBot.Services
{
	internal interface ILogin
	{
		bool CheckUrl(IWebDriver driver, string url);

		string ReadPassword();

		void StartPage(IWebDriver driver);

		void SubmitForm(IWebDriver driver);
	}
}
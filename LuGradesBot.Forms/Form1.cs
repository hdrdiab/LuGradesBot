using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuGradesBot.Forms
{
    public partial class Form1 : Form
    {
        string _username,_password;
        string homeUrl = "http://ulfg.ul.edu.lb/login.aspx";
        string gradesUrl = "http://ulfg.ul.edu.lb/account/gradeuser1.aspx";
		string accountUrl = "http://ulfg.ul.edu.lb/account/account.aspx";

		WebBrowser browser;
        public Form1()
        {
            InitializeComponent();
            browser = new WebBrowser();
            gradesListView.Columns.Add("Subject", 100, HorizontalAlignment.Center);
            gradesListView.Columns.Add("MidTerm", 70, HorizontalAlignment.Center);
            gradesListView.Columns.Add("FinalTerm", 70, HorizontalAlignment.Center);
            gradesListView.View = View.Details;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Navigate(homeUrl);
			Progress.Text = "Loading...";
            browser.DocumentCompleted += HomePageLoaded;
            _username = username.Text;
            _password = password.Text;
        }

        private void HomePageLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
        {			
			browser.DocumentCompleted -= HomePageLoaded;
            browser.DocumentCompleted += AuthenticatingUser;
            var document = browser.Document;
            var usernameField = document.GetElementById("maincontent_tbUser");
            var passwordField = document.GetElementById("maincontent_tbPassword");
            var submitButton = document.GetElementById("maincontent_Button1");
			//var errorMessage = document.GetElementById("maincontent_lblError");
			if (_username != "" && _password != "")
			{
				usernameField.InnerText = _username;
				passwordField.InnerText = _password;
				submitButton.InvokeMember("click");
				//progressBar.Value = 10;
				Progress.Text = "Form Submitted";
			}
			else
			{
				Progress.Text = "Empty Username or Password !!";
			}
        }
		private void AuthenticatingUser(object sender, WebBrowserDocumentCompletedEventArgs e)
		{

			if (browser.Url.ToString() == homeUrl)
			{
				Progress.Text = "Wrong Username or Password";
			}
			else
			{
				browser.DocumentCompleted -= AuthenticatingUser;
				browser.DocumentCompleted += NavigateToGradesPage;
				browser.Navigate(accountUrl);
				progressBar.Value = 10;
				Progress.Text = "Logging in..";

			}
		}
        private void NavigateToGradesPage(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            browser.DocumentCompleted -= NavigateToGradesPage;
            browser.DocumentCompleted += SelectYearDropDown;
            browser.Navigate(gradesUrl);
            fullNameLabel.Text = "Welcome "+ browser.Document.GetElementById("logincontent_ucLogin1_Label1").InnerText;
            progressBar.Value = 30;
            Progress.Text = "Navigating to Grades Page";
        }

        private void SelectYearDropDown(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            browser.DocumentCompleted -= SelectYearDropDown;
            browser.DocumentCompleted += SelectSeasonDropDown;
            var document = browser.Document;
            var dropdown = document.GetElementById("maincontent_academicdrop");
            dropdown.SetAttribute("value", "15");
            dropdown.InvokeMember("onchange");
            progressBar.Value = 50;
            Progress.Text = "Selecting Year";
        }

        private void SelectSeasonDropDown(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            browser.DocumentCompleted -= SelectSeasonDropDown;
            browser.DocumentCompleted += PrintGrades;
            var document = browser.Document;
            var dropdown = document.GetElementById("maincontent_semesterdrop");
            dropdown.SetAttribute("value", "3");
            dropdown.InvokeMember("onchange");
            progressBar.Value = 70;
            Progress.Text = "Selecting Season";
        }

        private void PrintGrades(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            progressBar.Value = 100;
            Progress.Text = "Successfull";
            var document = browser.Document;
            GetGrades();
        }


        public void GetGrades()
        {
            int count = 0;
            bool firstThree = true;
            var document = browser.Document;
            var gradesContainer = document.GetElementById("maincontent_GridView1");
            var elements = gradesContainer.GetElementsByTagName("span");
            var grade = new Grade();
            foreach(HtmlElement span in elements)
            {
                if (count == 3)
                {
                    if (firstThree)
                        firstThree = false;
                    else
                    {
                        var item = new ListViewItem(grade.Name);
                        item.SubItems.Add(grade.MidTerm);
                        item.SubItems.Add(grade.FinalGrade);
                        gradesListView.Items.Add(item);
                    }
                    count = 0;
                }
                switch (count)
                {
                    case 0:
                        grade.Name = span.InnerText;
                        break;
                    case 1:
                        grade.MidTerm = span.InnerText;
                        break;
                    case 2:
                        grade.FinalGrade = span.InnerText;
                        break;
                    default:
                        break;
                }
                count++;
            }
        }
    }
}

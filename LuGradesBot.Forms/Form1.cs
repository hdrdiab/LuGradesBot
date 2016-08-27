using LuGradesBot.Forms.Models;
using System;
using System.Windows.Forms;

namespace LuGradesBot.Forms
{
	public partial class Form1 : Form
	{
		private string _username, _password, _academicyear, _season;
		private string homeUrl = "http://ulfg.ul.edu.lb/login.aspx";
		private string gradesUrl = "http://ulfg.ul.edu.lb/account/gradeuser1.aspx";
		private string accountUrl = "http://ulfg.ul.edu.lb/account/account.aspx";

		private WebBrowser browser;

		public Form1()
		{
			InitializeComponent();
			browser = new WebBrowser();
			gradesListView.Columns.Add("Subject", 100, HorizontalAlignment.Center);
			gradesListView.Columns.Add("MidTerm", 70, HorizontalAlignment.Center);
			gradesListView.Columns.Add("FinalTerm", 70, HorizontalAlignment.Center);
			gradesListView.View = View.Details;

			comboBox1.DataSource = new AcademicYear[] {
						   new AcademicYear{ Id = 7,  Year = "2011-2012" },
						   new AcademicYear{ Id = 9,  Year = "2012-2013" },
						   new AcademicYear{ Id = 12, Year = "2013-2014" },
						   new AcademicYear{ Id = 14, Year = "2014-2015" },
						   new AcademicYear{ Id = 15, Year = "2015-2016" },
						   new AcademicYear{ Id = 16, Year = "2016-2017" }
				};
			comboBox1.DisplayMember = "Year";
			comboBox1.ValueMember = "Id";
			comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

			comboBox2.DataSource = new Season[]
			{
				new Season { Id = 2, season = "Fall"  },
				new Season { Id = 3,season="Spring" }
			};
			comboBox2.DisplayMember = "season";
			comboBox2.ValueMember = "Id";
			comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_username = username.Text;
			_password = password.Text;
			if (_username != "" && _password != "")
			{
				browser.Navigate(homeUrl);
				progressBar.Value = 0 ;
				Progress.Text = "Loading...";
				browser.DocumentCompleted += HomePageLoaded;
				_academicyear = comboBox1.SelectedValue.ToString();
				_season = comboBox2.SelectedValue.ToString();
			}
			else
			{
				Progress.Text = "Empty Username or Password";
			}
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
				usernameField.InnerText = _username;
				passwordField.InnerText = _password;
				submitButton.InvokeMember("click");
				progressBar.Value = 0;
				Progress.Text = "Form Submitted";
		
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
			fullNameLabel.Text = "Welcome " + browser.Document.GetElementById("logincontent_ucLogin1_Label1").InnerText;
			progressBar.Value = 30;
			Progress.Text = "Navigating to Grades Page";
		}

		private void SelectYearDropDown(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			browser.DocumentCompleted -= SelectYearDropDown;
			browser.DocumentCompleted += SelectSeasonDropDown;
			var document = browser.Document;
			var dropdown = document.GetElementById("maincontent_academicdrop");
			dropdown.SetAttribute("value", _academicyear);
			dropdown.InvokeMember("onchange");
			progressBar.Value = 50;
			Progress.Text = "Selecting Year";
		}

		private void fullNameLabel_Click(object sender, EventArgs e)
		{
		}

		private void SelectSeasonDropDown(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			browser.DocumentCompleted -= SelectSeasonDropDown;
			browser.DocumentCompleted += PrintGrades;
			var document = browser.Document;
			var dropdown = document.GetElementById("maincontent_semesterdrop");
			dropdown.SetAttribute("value", _season);
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
			try
			{
				var elements = gradesContainer.GetElementsByTagName("span");
				var grade = new Grade();
				foreach (HtmlElement span in elements)
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
			catch
			{
				Progress.Text = "No Grades Available";
			}
		}
	}
}
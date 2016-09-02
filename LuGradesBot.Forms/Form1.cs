using LuGradesBot.Forms.Models;
using mshtml;
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
		private static Timer aTimer;
		private WebBrowser browser;
		private int counter = 0;

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
			button2.Visible = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_username = username.Text;
			_password = password.Text;
			if (_username != "" && _password != "")
			{
				browser.Navigate(homeUrl);

				//Serves as Reset button also in case pressed while the app is running
				if (aTimer !=null)
					aTimer.Dispose();
				fullNameLabel.ResetText();
				button2.Visible = false;
				label4.ResetText();
				gradesListView.Items.Clear();
				progressBar.Value = 0 ;
				browser.DocumentCompleted -= PrintGrades;

				browser.DocumentCompleted -= AuthenticatingUser;
				
			    browser.DocumentCompleted += HomePageLoaded;
				_academicyear = comboBox1.SelectedValue.ToString();
				_season = comboBox2.SelectedValue.ToString();
				Progress.Text = "Loading...";
				
				//to avoid pressing while loading the app
				button1.Visible = false;
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
				button1.Visible = true;
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
			aTimer = new Timer();
			aTimer.Tick += new EventHandler(RefreshPage);
			aTimer.Interval = 60000;
			aTimer.Start();
			fullNameLabel.Text = "Welcome " + browser.Document.GetElementById("logincontent_ucLogin1_Label1").InnerText;
			progressBar.Value = 30;
			Progress.Text = "Navigating to Grades Page";
		}

		private void SelectYearDropDown(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (browser.Url.ToString() == gradesUrl)
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
			else
			{
				if (browser.Url.ToString() == accountUrl)
				{
					browser.Navigate(gradesUrl);
					aTimer.Start();
				}


				else
				{
					counter++;
					aTimer.Stop();
					Progress.Text = "Filling Evaluation no." + counter.ToString();
					browser.DocumentCompleted -= SelectYearDropDown;
					browser.DocumentCompleted += SelectYearDropDown;
					var document = browser.Document;
					HtmlElement head = document.GetElementsByTagName("head")[0];
					HtmlElement scriptEl = document.CreateElement("script");
					IHTMLScriptElement element = (IHTMLScriptElement)scriptEl.DomElement;
					element.text =
						"function fillEvaluation(){"
						+ "var elements = document.getElementsByTagName('input');"
						+ "var i = 6;"
						+ "var j = elements.length - 1;"
						+ "while (i < elements.length)"
						+ "{elements[i].checked = true;"
						+ "i = i + 5;}"
						+ "elements[j].click(); }";
					head.AppendChild(scriptEl);
					document.InvokeScript("fillEvaluation");
				}
			}
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

		private void button2_Click(object sender, EventArgs e)
		{
			aTimer.Dispose();
			button2.Visible = false;
			label4.ResetText();
		}

		private void PrintGrades(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			progressBar.Value = 100;
            Progress.Text = "Successfull";
			label4.Text = "The Grades will refresh every 60 sec ...";
			button2.Visible = true;
			button1.Visible = true;
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
		private void RefreshPage(object sender, EventArgs e)
		{
			browser.Navigate(gradesUrl);
			gradesListView.Items.Clear();
			button2.Visible = false;
			button1.Visible = false;
			browser.DocumentCompleted -= PrintGrades;
			browser.DocumentCompleted += SelectYearDropDown;
		}
	}
}
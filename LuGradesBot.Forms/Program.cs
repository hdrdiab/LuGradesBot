using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuGradesBot.Forms
{
    static class Program
    {
		private static Timer aTimer;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
        static void Main()
        {
			
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Form1 form = new Form1();
			Application.Run(form);
		}
	}
}

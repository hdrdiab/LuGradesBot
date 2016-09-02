namespace LuGradesBot.Forms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.button1 = new System.Windows.Forms.Button();
			this.username = new System.Windows.Forms.TextBox();
			this.password = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.Progress = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.gradesListView = new System.Windows.Forms.ListView();
			this.fullNameLabel = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(92, 282);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(98, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Submit";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// username
			// 
			this.username.Location = new System.Drawing.Point(61, 143);
			this.username.Name = "username";
			this.username.Size = new System.Drawing.Size(162, 20);
			this.username.TabIndex = 1;
			// 
			// password
			// 
			this.password.Location = new System.Drawing.Point(61, 218);
			this.password.Name = "password";
			this.password.Size = new System.Drawing.Size(162, 20);
			this.password.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft MHei", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(55, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(179, 71);
			this.label1.TabIndex = 0;
			this.label1.Text = "LuGradesBot";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(61, 124);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Username";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(61, 199);
			this.label3.Name = "label3";
			this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Password";
			// 
			// Progress
			// 
			this.Progress.AutoSize = true;
			this.Progress.Location = new System.Drawing.Point(114, 352);
			this.Progress.Name = "Progress";
			this.Progress.Size = new System.Drawing.Size(48, 13);
			this.Progress.TabIndex = 5;
			this.Progress.Text = "Progress";
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 324);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(277, 20);
			this.progressBar.TabIndex = 6;
			// 
			// gradesListView
			// 
			this.gradesListView.Location = new System.Drawing.Point(358, 52);
			this.gradesListView.Name = "gradesListView";
			this.gradesListView.Size = new System.Drawing.Size(307, 291);
			this.gradesListView.TabIndex = 7;
			this.gradesListView.UseCompatibleStateImageBehavior = false;
			// 
			// fullNameLabel
			// 
			this.fullNameLabel.AutoSize = true;
			this.fullNameLabel.Location = new System.Drawing.Point(548, 25);
			this.fullNameLabel.Name = "fullNameLabel";
			this.fullNameLabel.Size = new System.Drawing.Size(0, 13);
			this.fullNameLabel.TabIndex = 8;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(358, 22);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(86, 21);
			this.comboBox1.TabIndex = 9;
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(450, 22);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(81, 21);
			this.comboBox2.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(355, 352);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(0, 13);
			this.label4.TabIndex = 11;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(581, 347);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(66, 22);
			this.button2.TabIndex = 12;
			this.button2.Text = "Stop";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(727, 374);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.fullNameLabel);
			this.Controls.Add(this.gradesListView);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.Progress);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.password);
			this.Controls.Add(this.username);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion


		private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Progress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ListView gradesListView;
        private System.Windows.Forms.Label fullNameLabel;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button2;
	}
}


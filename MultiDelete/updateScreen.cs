using System;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MultiDelete
{
    public partial class updateScreen : Form
    {
        static MultiDelete multiDelete = new MultiDelete();
        string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete";

        public updateScreen()
        {
            InitializeComponent();
        }

        private void updateScreen_Load(object sender, EventArgs e)
        {
            updatePanel.Controls.Clear();
            Label heading = new Label();
            heading.TextAlign = ContentAlignment.MiddleCenter;
            heading.AutoSize = false;
            heading.Font = new Font("Roboto", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            heading.ForeColor = Color.FromArgb(194, 194, 194);
            heading.Size = new Size(461, 33);
            heading.TabStop = false;
            
            if(multiDelete.isUpdateAvailable())
            {
                heading.Text = "Update Available!";
            } else
            {
                heading.Text = "No Updates found!";
            }
            updatePanel.Controls.Add(heading);

            Label version = new Label();
            version.TextAlign = ContentAlignment.MiddleCenter;
            version.AutoSize = true;
            version.Font = new Font("Roboto", 17F, FontStyle.Bold, GraphicsUnit.Point);
            version.ForeColor = Color.FromArgb(194, 194, 194);
            version.TabStop = false;
            version.Text = multiDelete.getVersion();
            updatePanel.Controls.Add(version);

            //Downlad Changelog
            WebClient wc = new WebClient();
            wc.DownloadFile("https://github.com/greyhayv/MultiDelete/releases/latest", programsPath + @"\changelogs.txt");

            string[] changelogs = File.ReadAllLines(programsPath + @"\changelogs.txt");

            //Adds relvant lines to changelogs
            foreach(string line in changelogs)
            {
                if(line.Contains("<h1>"))
                {
                    Label lineLabel = new Label();
                    lineLabel.AutoSize = true;
                    lineLabel.MaximumSize = new Size(461, 999999999);
                    lineLabel.Font = new Font("Roboto", 15F, FontStyle.Bold, GraphicsUnit.Point);
                    lineLabel.ForeColor = Color.FromArgb(194, 194, 194);
                    lineLabel.TabStop = false;
                    string text = line.Substring(line.IndexOf("<h1>") + 4);
                    text = text.Remove(text.Length - 5);
                    lineLabel.Text = text;
                    updatePanel.Controls.Add(lineLabel);
                } else if(line.Contains("<p>"))
                {
                    Label lineLabel = new Label();
                    lineLabel.AutoSize = true;
                    lineLabel.MaximumSize = new Size(461, 999999999);
                    lineLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
                    lineLabel.ForeColor = Color.FromArgb(194, 194, 194);
                    lineLabel.TabStop = false;
                    string text = line.Substring(line.IndexOf("<p>") + 3);
                    if(line.EndsWith("<br>") || line.EndsWith("</p>"))
                    {
                        text = text.Remove(text.Length - 4);
                    } else if(line.EndsWith("</p></div>")) {
                        text = text.Remove(text.Length - 10);
                    }
                    lineLabel.Text = text;
                    updatePanel.Controls.Add(lineLabel);
                } else if(line.EndsWith("<br>") || line.EndsWith("</p>"))
                {
                    Label lineLabel = new Label();
                    lineLabel.AutoSize = true;
                    lineLabel.MaximumSize = new Size(461, 999999999);
                    lineLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
                    lineLabel.ForeColor = Color.FromArgb(194, 194, 194);
                    lineLabel.TabStop = false;
                    string text = line.Remove(line.Length - 4);
                    lineLabel.Text = text;
                    updatePanel.Controls.Add(lineLabel);
                } else if(line.EndsWith("</p></div>"))
                {
                    Label lineLabel = new Label();
                    lineLabel.AutoSize = true;
                    lineLabel.MaximumSize = new Size(461, 999999999);
                    lineLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
                    lineLabel.ForeColor = Color.FromArgb(194, 194, 194);
                    lineLabel.TabStop = false;
                    string text = line.Remove(line.Length - 10);
                    lineLabel.Text = text;
                    updatePanel.Controls.Add(lineLabel);
                }
            }
        }

        private void remindMeLaterButton_Click(object sender, EventArgs e)
        {
            focusLabel.Focus();
            Close();
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            focusLabel.Focus();
            downloadNewsetVersion();
        }

        private void downloadNewsetVersion()
        {
            string version = multiDelete.getVersion();
            string url = "https://github.com/greyhayv/MultiDelete/releases/download/" + version + "/MultiDelete" + version.Substring(1) + "_Installer.exe";
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}

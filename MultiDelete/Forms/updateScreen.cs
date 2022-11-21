using System;
using System.Net;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace MultiDelete
{
    public partial class updateScreen : Form
    {
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
            heading.ForeColor = MultiDelete.fontColor;
            heading.Size = new Size(461, 33);
            heading.TabStop = false;
            
            if(MultiDelete.updateAvailable)
            {
                heading.Text = "Update Available!";
                closeButton.Visible = false;
                downloadButton.Visible = true;
                remindMeLaterButton.Visible = true;
            } else
            {
                heading.Text = "No Updates found!";
                closeButton.Visible = true;
                downloadButton.Visible = false;
                remindMeLaterButton.Visible = false;

            }
            updatePanel.Controls.Add(heading);

            Label version = new Label();
            version.TextAlign = ContentAlignment.MiddleCenter;
            version.AutoSize = true;
            version.Font = new Font("Roboto", 17F, FontStyle.Bold, GraphicsUnit.Point);
            version.ForeColor = MultiDelete.fontColor;
            version.TabStop = false;
            version.Text = MultiDelete.newestVersion;
            updatePanel.Controls.Add(version);

            //Read Changelog
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead("https://github.com/greyhayv/MultiDelete/releases/latest");
            StreamReader sr = new StreamReader(stream);

            List<string> changelogs = new List<string>();

            string line2;
            while((line2 = sr.ReadLine()) != null)
            {
                changelogs.Add(line2);
            }

            //Adds relvant lines to changelogs
            foreach(string line in changelogs)
            {
                if(line.Contains("<h1>"))
                {
                    Label lineLabel = new Label();
                    lineLabel.AutoSize = true;
                    lineLabel.MaximumSize = new Size(461, 999999999);
                    lineLabel.Font = new Font("Roboto", 15F, FontStyle.Bold, GraphicsUnit.Point);
                    lineLabel.ForeColor = MultiDelete.fontColor;
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
                    lineLabel.ForeColor = MultiDelete.fontColor;
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
                    lineLabel.ForeColor = MultiDelete.fontColor;
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
                    lineLabel.ForeColor = MultiDelete.fontColor;
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
            string version = MultiDelete.newestVersion;
            string url = "https://github.com/greyhayv/MultiDelete/releases/download/" + version + "/MultiDelete" + version.Substring(1) + "_Installer.exe";
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            focusLabel.Focus();
            Close();
        }
    }
}

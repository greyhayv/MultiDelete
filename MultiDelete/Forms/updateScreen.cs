using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MultiDelete
{
    public partial class updateScreen : Form
    {
        private string newestVersion;

        public updateScreen(bool updateAvailable) {
            InitializeComponent();

            Label heading = new Label();
            heading.TextAlign = ContentAlignment.MiddleCenter;
            heading.AutoSize = false;
            heading.Font = new Font("Roboto", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            heading.ForeColor = MultiDelete.fontColor;
            heading.Size = new Size(461, 33);
            heading.TabStop = false;
            
            if(updateAvailable) {
                heading.Text = "Update Available!";
                closeButton.Visible = false;
            } else {
                heading.Text = "No Updates found!";
                downloadButton.Visible = false;
                remindMeLaterButton.Visible = false;
            }
            updatePanel.Controls.Add(heading);

            loadChangelogs();
        }

        private async void loadChangelogs() {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MultiDelete", MultiDelete.version));

            HttpResponseMessage response = await client.GetAsync("https://api.github.com/repos/greyhayv/MultiDelete/releases/latest");
            ReleaseInfo latestRelease = JsonSerializer.Deserialize<ReleaseInfo>(await response.Content.ReadAsStringAsync());

            newestVersion = latestRelease.tag_name;

            Label versionLabel = new Label();
            versionLabel.TextAlign = ContentAlignment.MiddleCenter;
            versionLabel.AutoSize = true;
            versionLabel.Font = new Font("Roboto", 18, FontStyle.Bold, GraphicsUnit.Point);
            versionLabel.ForeColor = MultiDelete.fontColor;
            versionLabel.TabStop = false;
            versionLabel.Text = newestVersion;
            updatePanel.Controls.Add(versionLabel);
            
            using(StringReader reader = new StringReader(latestRelease.body)) {
                string line;
                while((line = reader.ReadLine()) != null) {
                    Label label = new Label();
                    label.AutoSize = true;
                    label.MaximumSize = new Size(updatePanel.Width, label.MaximumSize.Height);
                    label.ForeColor = MultiDelete.fontColor;
                    label.TabStop = false;
                    if(!String.IsNullOrWhiteSpace(line)) {
                        label.Padding = new Padding(0, 10, 0, 0);
                    }

                    if(line.StartsWith("# ")) {
                        label.Font = new Font("Roboto", 16, FontStyle.Bold, GraphicsUnit.Point);
                        label.Text = line.Substring(2);
                    } else {
                        label.Font = new Font("Roboto", 12.5F, GraphicsUnit.Point);
                        label.Text = line;
                    }

                    updatePanel.Controls.Add(label);
                }
            }
        }

        private void updateScreen_Load(object sender, EventArgs e) {
            
        }

        private void remindMeLaterButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void downloadButton_Click(object sender, EventArgs e) {
            Focus();
            downloadNewsetVersion();
        }

        private void downloadNewsetVersion() {
            string version = newestVersion;
            string url = "https://github.com/greyhayv/MultiDelete/releases/download/" + version + "/MultiDelete" + version.Substring(1) + "_Installer.exe";
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void closeButton_Click(object sender, EventArgs e) {
            Close();
        }
    }
}

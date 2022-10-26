using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiDelete.Controls
{
    internal class FolderTextBox : Panel
    {
        TextBox textBox = new TextBox();
        Button folderButton = new Button();
        private string folderDialogDescription;

        public FolderTextBox()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;

            textBox.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.ForeColor = ColorTranslator.FromHtml("#C2C2C2");
            textBox.Size = new Size(200, 22);
            textBox.TabStop = false;
            textBox.PlaceholderText = "Recordings Path";

            folderButton.Size = new Size(22, 22);
            folderButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            folderButton.FlatStyle = FlatStyle.Popup;
            folderButton.TabStop = false;
            folderButton.UseVisualStyleBackColor = false;
            folderButton.Image = Properties.Resources.foldericon;
            folderButton.Padding = new Padding(0, 0, 1, 0);
            folderButton.Click += new EventHandler(folderButton_Click);
            toolTip.SetToolTip(folderButton, "Browse");

            Size = new Size(250, 22);
            Controls.Add(textBox);
            textBox.Location = new Point(0, 0);
            Controls.Add(folderButton);
            folderButton.Location = new Point(205, 0);
        }

        public void setToolTip(string str)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(textBox, str);
        }

        public void setText(string text)
        {
            textBox.Text = text;
        }

        public string getText()
        {
            return textBox.Text;
        }

        public void setEnabled(bool enabled)
        {
            textBox.Enabled = enabled;
            folderButton.Enabled = enabled;
        }

        public void setFolderDialogDescription(string descrition)
        {
            folderDialogDescription = descrition;
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            Focus();
            var fbd = new FolderBrowserDialog();
            fbd.UseDescriptionForTitle = true;
            fbd.Description = folderDialogDescription;
            fbd.ShowDialog();
            if (fbd.SelectedPath != "")
            {
                textBox.Text = fbd.SelectedPath;
            }
        }
    }
}

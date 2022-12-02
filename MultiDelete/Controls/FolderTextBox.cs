using System;
using System.Drawing;
using System.Windows.Forms;

namespace MultiDelete
{
    public class FolderTextBox : Panel
    {
        private BTextBox textBox = new BTextBox();
        private BButton folderButton = new BButton();
        private string folderDialogDescription;
        private Color borderColor;

        public Color BorderColor { get => borderColor; set { borderColor = value; textBox.BorderColor = value; folderButton.BorderColor = value; } }
        public override Color BackColor { get => base.BackColor; set { base.BackColor = value; textBox.BackColor = value; } }
        public override Color ForeColor { get => base.ForeColor; set { base.ForeColor = value; textBox.ForeColor = value; } }
        public string PlaceholderText { get => textBox.PlaceholderText; set => textBox.PlaceholderText = value; }
        public string ToolTip { get => textBox.ToolTip; set => textBox.ToolTip = value; }
        public override string Text { get => textBox.Text; set => textBox.Text = value; }
        public bool FTBEnabled { get => textBox.Enabled; set { textBox.Enabled = value; folderButton.Enabled = value; } }
        public string FolderDialogDescription { get => folderDialogDescription; set => folderDialogDescription = value;}

        public FolderTextBox()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;

            folderButton.BorderRadius = 10;
            folderButton.Size = new Size(22, 22);
            folderButton.UseVisualStyleBackColor = false;
            folderButton.Image = Properties.Resources.foldericon;
            folderButton.Click += new EventHandler(folderButton_Click);
            toolTip.SetToolTip(folderButton, "Browse");

            Size = new Size(250, 25);
            Controls.Add(textBox);
            textBox.Location = new Point(0, 0);
            Controls.Add(folderButton);
            folderButton.Location = new Point(205, 1);
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            Focus();
            var fbd = new FolderBrowserDialog();
            fbd.UseDescriptionForTitle = true;
            fbd.Description = folderDialogDescription;
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                textBox.Text = fbd.SelectedPath;
            }
        }
    }
}

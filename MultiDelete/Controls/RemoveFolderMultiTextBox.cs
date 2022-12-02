using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MultiDelete
{
    internal class RemoveFolderMultiTextBox : MultiTextBox
    {
        private List<BButton> removeButtons = new List<BButton>();
        private List<BButton> folderButtons = new List<BButton>();
        private string folderDialogDescription;

        public override string ToolTip { get => base.ToolTip; set { 
            base.ToolTip = value; 
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            foreach(BButton button in removeButtons) {
                toolTip.SetToolTip(button, value);
            }
            foreach(BButton button in folderButtons) {
                toolTip.SetToolTip(button, value);
            }
        } }
        public override Color BackColor { get => base.BackColor; set { 
            base.BackColor = value;
            foreach(BButton button in folderButtons) {
                button.BackColor = value;
            }
            foreach(BButton button in removeButtons) {
                button.BackColor = value;
            }
        } }
        public override Color BorderColor { get => base.BorderColor; set { 
            base.BorderColor = value;
            foreach(BButton button in folderButtons) {
                button.BorderColor = value;
            }
            foreach(BButton button in removeButtons) {
                button.BorderColor = value;
            }
        } }
        public override Color ForeColor { get => base.ForeColor; set { 
            base.ForeColor = value; 
            foreach(BButton button in folderButtons) {
                button.ForeColor = value;
            }
            foreach(BButton button in removeButtons) {
                button.ForeColor = value;
            }
        } }
        public override bool MTBEnabled { get => base.MTBEnabled; set { 
            base.MTBEnabled = value;
            foreach(BButton button in removeButtons) {
                button.Enabled = value;
            }
            foreach(BButton button in folderButtons) {
                button.Enabled = value;
            }
        } }
        public string FolderDialogDescription { get => folderDialogDescription; set => folderDialogDescription = value; }

        public override void createNewTextBox()
        {
            base.createNewTextBox();

            BButton removeButton = new BButton();
            removeButton.Size = new Size(22, 22);
            removeButton.TabStop = false;
            removeButton.UseVisualStyleBackColor = false;
            removeButton.Image = Properties.Resources.x;
            removeButton.Click += new EventHandler(removeButton_click);
            removeButton.BorderSize = 1;
            removeButton.BorderRadius = 10;
            removeButton.BorderColor = BorderColor;
            removeButton.ToolTip = ToolTip;
            removeButtons.Add(removeButton);

            BButton folderButton = new BButton();
            folderButton.Size = new Size(22, 22);
            folderButton.TabStop = false;
            folderButton.UseVisualStyleBackColor = false;
            folderButton.Image = Properties.Resources.foldericon;
            folderButton.Click += new EventHandler(folderButton_click);
            folderButton.BorderSize = 1;
            folderButton.BorderRadius = 10;
            folderButton.BorderColor = BorderColor;
            folderButton.ToolTip = ToolTip;
            folderButtons.Add(folderButton);

            panels[panels.Count - 1].Controls.Add(removeButton);
            panels[panels.Count - 1].Controls.Add(folderButton);

            setRemoveButtonVisibilaty();
        }

        public override void deleteTextBox(int i)
        {
            base.deleteTextBox(i);

            removeButtons.RemoveAt(i);
            folderButtons.RemoveAt(i);

            setRemoveButtonVisibilaty();
        }

        private void setRemoveButtonVisibilaty()
        {
            for (int i = 0; i < removeButtons.Count; i++)
            {
                if (i >= removeButtons.Count - 1)
                {
                    removeButtons[i].Visible = false;
                    return;
                }
                
                removeButtons[i].Visible = true;
            }
        }

        private void removeButton_click(object sender, EventArgs e)
        {
            Focus();

            deleteTextBox(removeButtons.IndexOf((BButton)sender));
        }

        private void folderButton_click(object sender, EventArgs e)
        {
            Focus();

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.UseDescriptionForTitle = true;
            fbd.Description = folderDialogDescription;
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                textBoxes[folderButtons.IndexOf((BButton)sender)].Text = fbd.SelectedPath;
            }
        }
    }
}

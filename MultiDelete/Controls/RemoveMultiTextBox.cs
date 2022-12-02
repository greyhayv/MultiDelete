using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MultiDelete
{
    internal class RemoveMultiTextBox : MultiTextBox
    {
        private List<BButton> removeButtons = new List<BButton>();

        public override string ToolTip { get => base.ToolTip; set { 
            base.ToolTip = value;
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            foreach(BButton button in removeButtons) {
                toolTip.SetToolTip(button, value);
            }
        } }
        public override Color BorderColor { get => base.BorderColor; set { 
            base.BorderColor = value;
            foreach(BButton button in removeButtons) {
                button.BorderColor = value;
            }
        } }
        public override Color BackColor { get => base.BackColor; set { 
            base.BackColor = value;
            foreach(BButton button in removeButtons) {
                button.BackColor = value;
            }
        } }
        public override Color ForeColor { get => base.ForeColor; set { 
            base.ForeColor = value;
            foreach(BButton button in removeButtons) {
                button.ForeColor = value;
            }
        } }
        public override bool MTBEnabled { get => base.MTBEnabled; set { 
            base.MTBEnabled = value;
            foreach(BButton button in removeButtons) {
                button.Enabled = value;
            }
        } }

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
            
            panels[panels.Count - 1].Controls.Add(removeButton);

            setRemoveButtonVisibilaty();
        }

        public override void deleteTextBox(int i)
        {
            base.deleteTextBox(i);

            removeButtons.RemoveAt(i);

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
    }
}

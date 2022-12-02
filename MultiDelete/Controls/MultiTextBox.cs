using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MultiDelete
{
    internal class MultiTextBox : FlowLayoutPanel
    {
        public List<BTextBox> textBoxes = new List<BTextBox>();
        public List<FlowLayoutPanel> panels = new List<FlowLayoutPanel>();

        private Color borderColor = Color.FromArgb(194, 194, 194);
        private string toolTipStr;

        public virtual string ToolTip { get => toolTipStr; set {
            foreach(BTextBox textBox in textBoxes)
            {
                textBox.ToolTip = value;
            }
            toolTipStr = value;
        } }
        public virtual Color BorderColor { get => borderColor; set { 
            borderColor = value;
            foreach(BTextBox textBox in textBoxes) {
                textBox.BorderColor = value;
            }
        } }
        public override Color BackColor { get => base.BackColor; set { 
            base.BackColor = value;
            foreach(BTextBox textBox in textBoxes) {
                textBox.BackColor = value;
            }
        } }
        public override Color ForeColor { get => base.ForeColor; set { 
            base.ForeColor = value;
            foreach(BTextBox textBox in textBoxes) {
                textBox.ForeColor = value;
            }
        } }
        public List<string> Texts { get {
            List<string> texts = new List<string>();
            for (int i = 0; i < textBoxes.Count - 1; i++)
            {
                texts.Add(textBoxes[i].Text);
            }
            return texts;
        } set {
            foreach(BTextBox textBox in textBoxes) {
                textBox.Text = "";
            }
            for(int i = 0; i < value.Count; i++)
            {
                textBoxes[i].Text = value[i];
            }
        } }
        public virtual bool MTBEnabled { get => textBoxes[0].Enabled; set { 
            foreach(BTextBox textBox in textBoxes)
            {
                textBox.TextBoxEnabled = value;
            }
        } }

        public MultiTextBox()
        {
            AutoScroll = false;
            FlowDirection = FlowDirection.TopDown;
            TabStop = false;
            AutoSize = true;

            createNewTextBox();
        }

        public virtual void createNewTextBox()
        {
            BTextBox textBox = new BTextBox();
            textBox.BorderColor = borderColor;
            textBox.BackColor = BackColor;
            textBox.ForeColor = ForeColor;
            textBox.textBox.TextChanged += new EventHandler(textChanged);
            textBox.ToolTip = toolTipStr;
            textBoxes.Add(textBox);

            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.FlowDirection = FlowDirection.LeftToRight;
            panel.WrapContents = false;
            panel.Size = new Size(275, 28);
            panel.Controls.Add(textBox);
            panels.Add(panel);

            Controls.Add(panel);
        }

        public virtual void deleteTextBox(int i)
        {
            Controls.Remove(panels[i]);
            textBoxes.RemoveAt(i);
            panels.RemoveAt(i);
        }

        public void textChanged(object sender, EventArgs e)
        {
            //Deletes TextBoxes if they are empty
            for(int i = 0; i < textBoxes.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(textBoxes[i].Text))
                {
                    continue;
                }

                if(i < textBoxes.Count - 2)
                {
                    deleteTextBox(i);
                    return;
                }

                if(i == textBoxes.Count - 2 && textBoxes.Count > 1)
                {
                    deleteTextBox(textBoxes.Count - 1);
                }
            }

            //Creates a new TextBox if last TextBox has Text
            if(!string.IsNullOrWhiteSpace(textBoxes[textBoxes.Count - 1].Text))
            {
                createNewTextBox();
            }
        }

        public void addTexts(List<string> texts)
        {
            foreach(string text in texts)
            {
                textBoxes[textBoxes.Count - 1].Text = text;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiDelete
{
    internal class MultiTextBox : FlowLayoutPanel
    {
        public readonly List<BTextBox> textBoxes = new List<BTextBox>();
        private Color borderColor;

        public string toolTipStr;
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

        public MultiTextBox()
        {
            AutoScroll = false;
            FlowDirection = FlowDirection.TopDown;
            TabStop = false;
            AutoSize = true;
            borderColor = Color.FromArgb(194, 194, 194);

            createNewTextBox();
        }

        public List<string> getTexts()
        {
            List<string> texts = new List<string>();
            for (int i = 0; i < textBoxes.Count - 1; i++)
            {
                texts.Add(textBoxes[i].Text);
            }

            return texts;
        }

        public virtual void createNewTextBox()
        {
            BTextBox textBox = new BTextBox();
            textBox.BorderSize = 1;
            textBox.BorderColor = borderColor;
            textBox.UnderlineStyle = false;
            textBox.BackColor = BackColor;
            textBox.ForeColor = ForeColor;
            textBox.textBox.TextChanged += new EventHandler(textChanged);
            if(toolTipStr != null)
            {
                textBox.setToolTip(toolTipStr);
            }

            textBoxes.Add(textBox);
            Controls.Add(textBox);
        }

        public void setToolTip(string str)
        {
            foreach(BTextBox textBox in textBoxes)
            {
                textBox.setToolTip(str);
            }

            toolTipStr = str;
        }

        public virtual void deleteTextBox(int i)
        {
            Controls.Remove(textBoxes[i]);
            textBoxes.RemoveAt(i);
        }

        public void textChanged(object sender, EventArgs e)
        {
            //Deletes TextBoxes if they are empty
            for (int i = 0; i < textBoxes.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(textBoxes[i].Text))
                {
                    continue;
                }

                if (i < textBoxes.Count - 2)
                {
                    deleteTextBox(i);
                    return;
                } else if (i == textBoxes.Count - 2 && textBoxes.Count > 1)
                {
                    deleteTextBox(textBoxes.Count - 1);
                }
            }

            //Creates a new TextBox if last TextBox has Text
            if (!string.IsNullOrWhiteSpace(textBoxes[textBoxes.Count - 1].Text))
            {
                createNewTextBox();
            }
        }

        public void setTexts(string[] texts)
        {
            for(int i = 0; i < texts.Length; i++)
            {
                textBoxes[i].Text = texts[i];
            }
        }

        public void addTexts(List<string> texts)
        {
            foreach(string text in texts)
            {
                textBoxes[textBoxes.Count - 1].Text = text;
            }
        }

        public virtual void disableTextBoxes()
        {
            foreach(BTextBox textBox in textBoxes)
            {
                textBox.setEnabled(false);
            }
        }

        public virtual void enableTextBoxes()
        {
            foreach(BTextBox textBox in textBoxes)
            {
                textBox.setEnabled(true);
            }
        }
    }
}

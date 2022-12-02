using System;
using System.Windows.Forms;

namespace MultiDelete 
{
    internal class BNumericUpDown : BTextBox
    {
        private decimal maximum = 100;
        private decimal minimum = 0;

        private string oldText = string.Empty;

        public decimal Maximum { get => maximum; set => maximum = value; }
        public decimal Minimum { get => minimum; set => minimum = value; }
        public decimal Value { get => string.IsNullOrWhiteSpace(textBox.Text) ? 0 : Decimal.Parse(textBox.Text); set {
            if(value > maximum) {
                textBox.Text = maximum.ToString();
                return;
            }
            if(value < minimum) {
                textBox.Text = minimum.ToString();
                return;
            }
            textBox.Text = value.ToString();
        }}

        public BNumericUpDown() {
            textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            textBox.TextChanged += new EventHandler(textBox_TextChanged);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox_TextChanged(object sender, EventArgs e) {
            decimal result;
            if(!decimal.TryParse(textBox.Text, out result) && !string.IsNullOrWhiteSpace(textBox.Text)) {
                textBox.Text = oldText;
                return;
            }
            oldText = textBox.Text;

            if(string.IsNullOrWhiteSpace(textBox.Text)) {
                return;
            }

            if(decimal.Parse(textBox.Text) > maximum) {
                textBox.Text = maximum.ToString();
            }

            if(decimal.Parse(textBox.Text) < minimum) {
                textBox.Text = minimum.ToString();
            }
        }
    }
}
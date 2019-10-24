using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorPicker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Color.FromArgb(r, g, b);
            colorDialog1.ShowDialog();
            r = colorDialog1.Color.R;
            g = colorDialog1.Color.G;
            b = colorDialog1.Color.B;
            ResetControllers(r, g, b);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Clipboard.GetText();

            ResetControllers(r,g,b);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = Clipboard.GetText();            
        }

        public byte r = 50, g=50, b=50;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            r = (byte)numericUpDown1.Value;
            ResetControllers(r, g, b);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            g = (byte)numericUpDown2.Value;
            ResetControllers(r, g, b);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            b = (byte)numericUpDown3.Value;
            ResetControllers(r, g, b);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            r = (byte)trackBar1.Value;
            ResetControllers(r, g, b);
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            g = (byte)trackBar2.Value;
            ResetControllers(r, g, b);
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            b = (byte)trackBar3.Value;
            ResetControllers(r, g, b);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TryParseColor();
        }

        private void TryParseColor()
        {
            string text = textBox1.Text;
            if (text.Contains(","))
                text = text.Replace(" ", "");
            text = text.Replace("(", "");
            text = text.Replace(")", "");
            Regex regex = new Regex("[0-9]{1,3}[, ][0-9]{1,3}[, ][0-9]{1,3}");
            if (regex.IsMatch(text))
                SetColor(text);
            else MessageBox.Show("Color not Found!", "ParseColor",MessageBoxButtons.OK,MessageBoxIcon.Error);         
        }

        private void SetColor(string text)
        {
            string[] splited;
            if (text.Contains(","))
                splited = text.Split(',');
            else
                splited = text.Split(' ');

            r = (byte)Int32.Parse(splited[0]);
            g = (byte)Int32.Parse(splited[1]);
            b = (byte)Int32.Parse(splited[2]);
            ResetControllers(r, g, b);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        public void ShowColor()
        {
            Color c = Color.FromArgb(r, g, b);

            panel1.BackColor = c;
        }

        public void ResetControllers(byte rr, byte gg, byte bb)
        {
            trackBar1.Value = rr;
            trackBar2.Value = gg;
            trackBar3.Value = bb;

            numericUpDown1.Value = rr;
            numericUpDown2.Value = gg;
            numericUpDown3.Value = bb;

            label2.Text = $"R {rr.ToString()} G {gg.ToString()} B {bb.ToString()}";
            textBox2.Text = $"({rr.ToString()},{gg.ToString()},{bb.ToString()})";
            ShowColor();
        }
    }
}

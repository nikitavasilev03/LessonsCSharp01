using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_L
{
    public partial class Form1 : Form
    {
        char sign = 'n';
        public Form1()
        {
            InitializeComponent();
        }

        public void numbutton_Click(object sender, EventArgs e)
        {
            string text = (sender as Button).Text;
            textBox1.Text += text;
        }

        public void signbutton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            if (sign == 'n')
            {
                sign = (sender as Button).Text[0];
                textBox1.Text += sign;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (sign == 'n' || textBox1.Text[textBox1.Text.Length - 1] == sign)
                return;
            try
            {
                string[] strs = textBox1.Text.Split(sign);
                double number1 = Convert.ToDouble(strs[0]);
                double number2 = Convert.ToDouble(strs[1]);
                double result = 0;
                switch (sign) 
                {
                    case '+':
                        result = number1 + number2;
                        break;
                    case '-':
                        result = number1 - number2;
                        break;
                    case '×':
                        result = number1 * number2;
                        break;
                    case '÷':
                        result = number1 / number2;
                        break;
                }
                textBox1.Text += "=" + result;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private void btn_clr_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            sign = 'n';
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            int index = textBox1.Text.IndexOf("=");
            if (index == -1)
            {
                if (textBox1.Text[textBox1.Text.Length - 1] == sign)
                    sign = 'n';
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
            }
            else
            {
                textBox1.Text = textBox1.Text.Remove(index, textBox1.Text.Length - index);
            }
        }
    }
}

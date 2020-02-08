using System;
using System.Windows.Forms;
//using System.Windows.Forms;

namespace Lesson2
{
    public partial class Form1 : Form
    {
        UserControl1 control1;
        UserControl2 control2;
        public Form1()
        {
            InitializeComponent();
            
            control1 = new UserControl1();
            control1.Left = 1;
            control1.Top = 1;
            control1.Parent = this;
            control1.Visible = true;

            control2 = new UserControl2();
            control2.Left = 1;
            control2.Top = 1;
            control2.Parent = this;
            control2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (control1.Visible)
            {
                control1.Visible = false;
                control2.Visible = true;
            }
            else if (control2.Visible)
            {
                control2.Visible = false;
                //control3.Visible = true;
            }
        }
    }
}

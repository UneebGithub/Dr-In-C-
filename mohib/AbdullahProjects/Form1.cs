using System;
using System.Windows.Forms;

namespace AbdullahProjectss
{
    public partial class Form1 : Form
    {
        public bool check;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
            textBox1.Visible = false;
            textBox2.Visible = false;
            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            check=true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button1.Visible = true;
            label3.Text = "Doctor Login";
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            string user_name = textBox1.Text;
            string user_pass = textBox2.Text;
            if (check == true)
            {
                if (user_name.Equals("admin") && user_pass.Equals("12a"))
                {
                    Doctors dr = new Doctors();
                    dr.Show();
                }
                else
                {
                    MessageBox.Show("Wrong username or password.");
                }

            }
            else if(check==false)
            {
                     if (user_name.Equals("ph1") && user_pass.Equals("123"))
                     {
                    PT pT = new PT();
                    pT.Show();
                     }
                else
                {
                    MessageBox.Show("Wrong username or password.");
                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            check = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button1.Visible = true;
            label3.Text = "Ph Login";
        }
    }
}

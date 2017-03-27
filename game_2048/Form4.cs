using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_2048
{
    public partial class Form4 : Form
    {
        private int a;
        public Form4()
        {
            InitializeComponent();
            System.IO.StreamReader sr = new System.IO.StreamReader("HighScores.txt");
            a = Convert.ToInt32(sr.ReadLine());
            hs1.Text = Convert.ToString(a);
            a = Convert.ToInt32(sr.ReadLine());
            hs2.Text = Convert.ToString(a);
            a = Convert.ToInt32(sr.ReadLine());
            hs3.Text = Convert.ToString(a);
            a = Convert.ToInt32(sr.ReadLine());
            hs4.Text = Convert.ToString(a);
            a = Convert.ToInt32(sr.ReadLine());
            hs5.Text = Convert.ToString(a);
            sr.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter("HighScores.txt");
            sw.WriteLine("0");hs1.Text = "0";
            sw.WriteLine("0"); hs2.Text = "0";
            sw.WriteLine("0"); hs3.Text = "0";
            sw.WriteLine("0"); hs4.Text = "0";
            sw.WriteLine("0"); hs5.Text = "0";
            sw.Close();
        }
    }
}

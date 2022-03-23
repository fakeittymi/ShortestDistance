using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TestProg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Экземпляр класса для управления данными
        /// </summary>
        private static Manager manager = new Manager();

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = Convert.ToString(e.X) + " : " + Convert.ToString(e.Y);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                Graphics g = pictureBox1.CreateGraphics();           
                Point _point = new Point(e.X, e.Y);
                g.FillEllipse(new SolidBrush(Color.Red), e.X, e.Y, 10, 10);
                manager.AddRed(_point);

            }

            if (e.Button == MouseButtons.Right)
            {
                Graphics g = pictureBox1.CreateGraphics();
                Point _point = new Point(e.X, e.Y);
                g.FillEllipse(new SolidBrush(Color.Green), e.X, e.Y, 10, 10);
                manager.AddGreen(_point);        
            }
        }

        private void отрисоватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            manager.Draw(g);
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            manager.Clear();
        }
    }
}

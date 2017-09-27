using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 贪吃蛇
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private palette p;
        private int level=5;
        private int highest_score=0;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(p!=null)
            {
                p.PaintPalette(e.Graphics);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.W||e.KeyCode==Keys.Up)
            {
                p.Direction = palette.directions.up;
            }
            else if(e.KeyCode==Keys.S||e.KeyCode==Keys.Down)
            {
                p.Direction = palette.directions.down;
            }
            else if(e.KeyCode==Keys.A||e.KeyCode==Keys.Left)
            {
                p.Direction = palette.directions.left;
            }
            else if(e.KeyCode==Keys.D||e.KeyCode==Keys.Right)
            {
                p.Direction = palette.directions.right;
            }
            label2.Text = Convert.ToString(p.score);
            if(p.score>highest_score)
            {
                highest_score = p.score;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void 开始游戏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int width, height, size;
            width = pictureBox1.Width / 15;
            height = pictureBox1.Height / 15;
            size = 15;

            p = new palette(width, height, size, pictureBox1.BackColor, Graphics.FromHwnd(pictureBox1.Handle), level);

            p.Start();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            level = 8;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            level = 7;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            level = 6;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            level = 5;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            level = 4;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            level = 3;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            level = 2;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            level = 1;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }

        private void 查看最高分数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("当前最高分数为：" + highest_score, "最高分");       }
    }
}

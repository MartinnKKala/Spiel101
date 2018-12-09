using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spiel_101
{
    public partial class Form1 : Form
    {
        bool goleft = false;
        bool goright = false;
        bool jump = false;
        bool face = false;

        int jumpSpeed = 5;
        int health = 100;
        int force = 8;
        int score = 0;

        public Form1()
        {
            InitializeComponent();
            label1.Text = "health: " + health;
            label2.Text = "Score: " + score;
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                goleft = true;
            if (e.KeyCode == Keys.D)
                goright = true;
            if (e.KeyCode == Keys.Space && !jump)
                jump = true;
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                goleft = false;
            if (e.KeyCode == Keys.D)
                goright = false;
            if (jump)
                jump = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Player.Top += jumpSpeed;
            if (jump && force < 0)
                jump = false;

            if (goleft)
            {
                if (Player.Left >= 0)
                {
                    Player.Left -= 5;
                }
                if(!face)
                {
                    Player.Image = Image.FromFile(@"Spiel101Data\SpriteLeft.png");
                    face = true;
                }
            }
                
            if (goright)
            {
                if (Player.Left<340)
                {
                    Player.Left += 5;
                }
                if(face)
                {
                    Player.Image = Image.FromFile(@"Spiel101Data\SpriteRight.png");
                    face = false;
                }
            }

            if (jump)
            {
                jumpSpeed = -12;
                jumpSpeed += (8-force)/2;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (x.Tag == "Platform"||x.Tag=="PlatformHot"))
                {
                    if(x.Tag=="Platform")
                    {
                        if (Player.Bounds.IntersectsWith(x.Bounds) && !jump)
                        {
                            force = 8;
                            Player.Top = x.Top - Player.Height;
                            jumpSpeed = 0;
                        }
                    }
                    if(x.Tag=="PlatformHot")
                    {
                        if (Player.Bounds.IntersectsWith(x.Bounds) && !jump)
                        {
                            force = 8;
                            Player.Top = x.Top - Player.Height;
                            jumpSpeed = 0;
                            health-=2;
                            label1.Text = "health: " + health;
                        }
                    }
                    
                }
                if (x is PictureBox && x.Tag == "Door")
                {
                    if (Player.Bounds.IntersectsWith(x.Bounds)&&!jump)
                    {
                        timer1.Stop();
                        MessageBox.Show("Fin." + "\n" + "score: " + score + "/80");
                        Close();
                    }
                }
                if(x is PictureBox && x.Tag == "Coin")
                {
                    if(Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        Controls.Remove(x);
                        score += 10;
                        label2.Text = "Score: " + score;
                    }
                }
            }
            if(health<=0)
            {
                timer1.Stop();
                MessageBox.Show("You suck and you know you do.");
                Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
    }
}

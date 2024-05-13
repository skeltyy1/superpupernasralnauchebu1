using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace superpupernasralnauchebu
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private PictureBox fruit;
        private PictureBox[] snake = new PictureBox[400];
        private Label labelscore;
        private int dirX, dirY;
        private int width = 900;
        private int height = 800;
        private int sizeofside = 40;
        private int score=0;
        public Form1()
        {
            InitializeComponent();
            this.Width = width;
            this.Height = height;
            dirX = 1;
            dirY = 0;
            labelscore = new Label();
            labelscore.Text = "Рекорд: 0";
            labelscore.Location = new Point(810, 10);
            this.Controls.Add(labelscore);
            snake[0] = new PictureBox();
            snake[0].Location = new Point(200, 200);
            snake[0].Size = new Size(sizeofside, sizeofside);
            snake[0].BackColor = Color.Green;
            this.Controls.Add(snake[0]);
            fruit = new PictureBox();
            fruit.BackColor = Color.Red;
            fruit.Size = new Size(sizeofside, sizeofside);
            generateMap();
            generatefruit();
            timer.Tick += new EventHandler(update);
            timer.Interval = 250;
            timer.Start();
            this.KeyDown += new KeyEventHandler(OKP);
        }
        private void generatefruit()
        {
            Random r = new Random();
            rI = r.Next(0, height - sizeofside);
            int tempI = rI % sizeofside;
            rI -= tempI;
            rJ = r.Next(0, height - sizeofside);
            int tempJ = rJ % sizeofside;
            rJ -= tempJ;
            fruit.Location = new Point(rI, rJ);
            this.Controls.Add(fruit);
        }
        private void checkBorders()
        {
            if (snake[0].Location.X < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelscore.Text = "Score: " + score;
                    dirX = 1;
            }
            if (snake[0].Location.X > height)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelscore.Text = "Score: " + score;
                dirX = -1;
            }
            if (snake[0].Location.Y < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelscore.Text = "Score: " + score;
                dirY = 1;
            }
            if (snake[0].Location.Y > height)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelscore.Text = "Score: " + score;
                dirY = -1;
            }
        }
        private void harakirisnake()
        {
            for (int _i = 1; _i < score; _i++)
            {
                if (snake[0].Location == snake[_i].Location)
                {
                    for (int _j = _i; _j <= score; _j++)
                        this.Controls.Remove(snake[_j]);
                    score = score - (score - _i + 1);
                    labelscore.Text = "Score: " + score;
                }
            }
        }
        private void eatfruit()
        {
            if (snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                labelscore.Text = "Рекорд: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score-1].Location.X + 40 * dirX, snake[score - 1].Location.Y - 40 * dirY);
                snake[score].Size = new Size(sizeofside, sizeofside);
                snake[score].BackColor = Color.Green;
                this.Controls.Add(snake[score]);
                generatefruit();
            }
        }
        private void generateMap()
        {
            for (int i = 0; i < width / sizeofside; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, sizeofside * i);
                pic.Size = new Size(width - 100, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= height / sizeofside; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point( sizeofside * i,0);
                pic.Size = new Size(1, width);
                this.Controls.Add(pic);
            }
        }

        private void movesnake()
        {
            for (int i=score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * (sizeofside), snake[0].Location.Y + dirY * (sizeofside));
            harakirisnake();
        }
        private void update(Object myObject, EventArgs eventArgs)
        {
            eatfruit();
            movesnake();
            //cube.Location = new Point(cube.Location.X+dirX*sizeofside, cube.Location.Y + dirY * sizeofside);
        }


        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                   dirX = 1;
                   dirY = 0;
                    break;
                case "Left":
                   dirX = -1;
                   dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY= 1;
                    dirX = 0;
                    break;
            }
        }
    }
}

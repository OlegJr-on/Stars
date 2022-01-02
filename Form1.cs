using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form // наслідування даного класа від класа Form
    {
        class Star // ініціалізація класа для в подальшому створенню зірок, по створеним полям(координатам)
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }
        }

        private Star[] stars = new Star[20000];// масив зірок який і представляє загалом все сузір'я
        Random random = new Random();
        private Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);// при кожному виклику функції фон змінюється на чорний 
            foreach (var stars in stars)// цикл в якому ми створюємо зірки
            {
                DrawBall(stars); // відмальовуємо кожну зірку
                MoveStar(stars); // задаєм координати руху
            }
            pictureBox1.Refresh();

        }

        private void MoveStar(Star stars) 
        {
            stars.Z -= 10; // швидкість руху зірок
            if (stars.Z<1)
            {
                stars.X = random.Next(-pictureBox1.Width, pictureBox1.Width); // рух зірок по осі х
                stars.Y = random.Next(-pictureBox1.Height, pictureBox1.Height);// рух зірок по осі у
                stars.Z = random.Next(1, pictureBox1.Width);// рух зірок по осі z

            }
        }

        private void DrawBall(Star stars)
        {
            float starsize = Map(stars.Z,0,pictureBox1.Width,7,0);
            float x = Map(stars.X / stars.Z, 0, 1, 0, pictureBox1.Width) + pictureBox1.Width / 2;
            float y = Map(stars.Y / stars.Z, 0, 1, 0, pictureBox1.Height) + pictureBox1.Height / 2;

            graphics.FillEllipse(Brushes.Crimson, x+100, y-100, starsize, starsize);
            graphics.FillEllipse(Brushes.Red, x + 200, y - 40, starsize, starsize);
            //graphics.FillEllipse(Brushes.Aquamarine, x + 100, y + 100, starsize, starsize);

        }
        

        private float Map(float n, float start1, float stop1, float start2, float stop2)
        {
            return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star
                {
                    X = random.Next(-pictureBox1.Width, pictureBox1.Width),
                    Y = random.Next(-pictureBox1.Height, pictureBox1.Height),
                    Z = random.Next(1, pictureBox1.Width)
                };


                timer1.Start();
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        static int n = 3;
        Ball[] balls = new Ball[n];
        Thread[] threads = new Thread[n];
        int Count = 0;
        System.Windows.Forms.Timer timer;


        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < n; i++)
                balls[i] = new Ball();

            timer = new System.Windows.Forms.Timer() { Interval = 50 };
            timer.Tick += (s, e) =>
            {
                Count = 0;
                for (int i = 0; i < n; i++)
                    Count += (threads[i].IsAlive) ? 0 : 1;
                if (Count == n)
                {
                    timer.Stop();
                    MessageBox.Show("End");
                }
            };
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < n; i++)
                balls[i].fillBall(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                threads[i] = new Thread(new ParameterizedThreadStart(balls[i].Move));
                threads[i].Start(panel1);
                timer.Start();
            }
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                if(threads[i].IsAlive)
                    threads[i].Suspend();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
            {
                if(threads[i].IsAlive)
                    threads[i].Resume();
            }
        }
    }
}

public class Ball
{
    public int x = rand.Next(0, 450);
    public int y = rand.Next(0, 355);
    private static Random r = new Random();
    private static Random rand = new Random();
    //private static Random ran1 = new Random((int)DateTime.Now.Ticks);
    SolidBrush brush = new SolidBrush(Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256)));

    public void fillBall(PaintEventArgs e)
    {
        e.Graphics.FillEllipse(brush, x, y, 45, 45);
    }

    public void Move(object panel1)
    {
        char[] symbol = new char[3] { '-', '+', '0'};
        char a = symbol[new Random((int)DateTime.Now.Ticks).Next(0, symbol.Length)];
        char b = symbol[new Random().Next(0, symbol.Length)];
        int imp = new Random((int)DateTime.Now.Ticks).Next(1, 10);
        Control control = (Control)panel1;
        int t = imp;
        for (int i = 1; i < t*300; i++)
        {
            if (i % 300 == 0)
            {
                imp--;
            }
            if (x >= control.Width - 45 - imp)
                a = '-';
            else
            {
                if (y >= control.Height - 45 - imp)
                {
                    b = '-';
                }
                else
                {
                    if (x <= imp)
                        a = '+';
                    else
                        if (y <= imp)
                        b = '+';
                }
            }
            

                if (a == '-')
                {
                    if (b == '-')
                    {
                        x = x - imp;
                        y = y - imp;
                    }
                    else
                    {
                        if (b == '+')
                        {
                            x = x - imp;
                            y = y + imp;
                        }
                        else
                        {
                            x = x - imp;
                        }

                    }
                }
                else
                {
                    if (a == '+')
                    {
                        if (b == '-')
                        {
                            x = x + imp;
                            y = y - imp;
                        }
                        else
                        {
                            if (b == '+')
                            {
                                x = x + imp;
                                y = y + imp;
                            }
                            else
                            {
                                x = x + imp;
                            }
                        }

                    }
                    else
                    {
                        if (b == '-')
                        {
                            y = y - imp;
                        }
                        else
                        {
                            if (b == '+')
                            {
                                y = y + imp;
                            }
                            else
                            {
                                y = y + imp;
                            }
                        }
                    }
                }
            control.Invalidate();
            Thread.Sleep(20);
        }
    }
}

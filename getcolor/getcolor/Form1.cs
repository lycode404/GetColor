﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace getcolor
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            int r = trackBar1.Value;
            int g = trackBar2.Value;
            int b = trackBar3.Value;
            label1.Text = "R:" + r.ToString();
            label3.Text = "G:" + g.ToString();
            label2.Text = "B:" + b.ToString();
            pictureBox1.BackColor = Color.FromArgb(r,g,b);
            


        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                all_disabled();
                timer1.Start();
                button1.Enabled = true;
            }
            else
            {
                all_enabled();
                timer1.Stop();
                button1.Enabled = false;
                button1.Text = "冻结(&F)";
            }
        }
        private void all_enabled()
        {
            trackBar1.Enabled = true;
            trackBar2.Enabled = true;
            trackBar3.Enabled = true;
        }
        private void all_disabled()
        {
            trackBar1.Enabled = false;
            trackBar2.Enabled = false;
            trackBar3.Enabled = false;

        }
        public Color GetColor(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = Control.MousePosition.X;
            int y = Control.MousePosition.Y;
            pictureBox1.BackColor = GetColor(x,y);
            int r = pictureBox1.BackColor.R;
            int g = pictureBox1.BackColor.G;
            int b = pictureBox1.BackColor.B;
            label1.Text = "R:" + r.ToString();
            label3.Text = "G:" + g.ToString();
            label2.Text = "B:" + b.ToString();
            trackBar1.Value = r;
            trackBar2.Value = g;
            trackBar3.Value = b;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "冻结(&F)")
            {
                timer1.Stop();
                button1.Text = "恢复(&F)";
            }
            else
            {
                timer1.Start();
                button1.Text = "冻结(&F)";
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("作者：LYao\r\n版本：v1.0","About");
        }
    }
}
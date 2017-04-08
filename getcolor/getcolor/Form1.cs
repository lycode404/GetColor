using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace getcolor
{
    public partial class Form1 : Form
    {

        bool showpic = false;//easter egg trigger
        public Form1()
        {
            InitializeComponent();
        }
        #region dllimports
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)//用户拖动滑动条时
        {
            int r = trackBar1.Value;//读取滑动条value
            int g = trackBar2.Value;
            int b = trackBar3.Value;
            label1.Text = "R:" + r.ToString();//同步GUI上显示的RGB值
            label3.Text = "G:" + g.ToString();
            label2.Text = "B:" + b.ToString();
            pictureBox1.BackColor = Color.FromArgb(r,g,b);//设置颜色预览区的颜色
            #region 一系列处理程序，用于同步GUI上显示的颜色16进制值
            string[] rgb_hex =
            {
                r.ToString("x").ToUpper(),
                g.ToString("x").ToUpper(),
                b.ToString("x").ToUpper()
            };
            int i = 0;
            foreach (string str in rgb_hex)
            {
                if (str.Length!=2)
                {
                    rgb_hex[i] = "0" + rgb_hex[i];
                }
                i++;
            }
            label4.Text = "#";
            foreach (string str in rgb_hex)
            {
                label4.Text = label4.Text + str;
            }
            #endregion
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)//取色模式切换时
        {
            if (radioButton2.Checked == true)//屏幕取色
            {
                all_disabled();
                timer1.Start();
                button1.Enabled = true;
            }
            else//手动取色
            {
                all_enabled();
                timer1.Stop();
                button1.Enabled = false;
                button1.Text = "冻结(&F)";
            }
        }
        #region 简单处理程序，用于取色模式切换时批量调整控件enable属性
        private void all_enabled()//
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
        #endregion
        public Color GetColor(int x, int y)//读取鼠标所在位置的颜色
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }

        private void timer1_Tick(object sender, EventArgs e)//定时器（用于实时同步屏幕取色的颜色值并在GUI上显示）
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

        private void button1_Click(object sender, EventArgs e)//冻结按钮（屏幕取色切换至手动取色）
        {           
                timer1.Stop();
                button1.Text = "恢复(&F)";
                radioButton1.Checked = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)//单击调出系统自带的颜色设置器并在颜色设置完成后同步到gui
        {
            colorDialog1.Color = pictureBox1.BackColor;
            if (colorDialog1.ShowDialog()==DialogResult.OK)
            {
                trackBar1.Value = colorDialog1.Color.R;
                trackBar2.Value = colorDialog1.Color.G;
                trackBar3.Value = colorDialog1.Color.B;

            }
        }

        private void label4_DoubleClick(object sender, EventArgs e)//手动编辑颜色16进制值
        {
            textBox1.Visible = true;
            textBox1.Text = label4.Text.Substring(1,6);
        }

        private void Form1_Resize(object sender, EventArgs e)//Easter egg trigger
        {
            if (WindowState == FormWindowState.Minimized)
            {
                showpic = !showpic;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)//手动编辑16进制值时，按下回车键保存设置
        {
            if (e.KeyCode==Keys.Enter)
            {
                try
                {
                    label4.Text = "#"+textBox1.Text;
                    int red, green, blue;
                    red = Convert.ToInt32(label4.Text.Substring(1, 2),16);
                    green = Convert.ToInt32(label4.Text.Substring(3, 2),16);
                    blue = Convert.ToInt32(label4.Text.Substring(5, 2),16);
                    trackBar1.Value = red;
                    trackBar2.Value = green;
                    trackBar3.Value = blue;
                    textBox1.Visible = false;
                }
                catch(Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void 显示比色板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorimg climg = new colorimg(trackBar1.Value, trackBar2.Value, trackBar3.Value, showpic);
            climg.Show();
        }

        private void 联网查看详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.color-hex.com/color/" + label4.Text.Replace("#", ""));
        }

        private void textBox1_Click(object sender, EventArgs e)//手动编辑16进制值时全选文本以便用户操作
        {
            textBox1.SelectAll();
        }
    }
}

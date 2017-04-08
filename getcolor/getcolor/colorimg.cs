using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace getcolor
{
    public partial class colorimg : Form
    {
        int red, green, blue;//rgb颜色值
        bool showpic;//easter egg trigger
        string color_hex;//颜色的16进制值（string）
        bool isshowing = false;//easter egg trigger

        private void colorimg_DoubleClick(object sender, EventArgs e)
        {
        }

        private void colorimg_Click(object sender, EventArgs e)//easter egg trigger
        {
            if (showpic==true)
            {
                isshowing = !isshowing;
            }
            if (isshowing == true)
            {
                BackColor = Color.FromArgb(240,240,240);
                if (color_hex == "#39C5BB" & showpic == true)
                {
                    BackgroundImage = Properties.Resources._39C5BB;
                }
                else if (color_hex == "#FFE212" & showpic == true)
                {
                    BackgroundImage = Properties.Resources._FFE212;
                }
                else if (color_hex == "#FFBFCB" & showpic == true)
                {
                    BackgroundImage = Properties.Resources._FFBFCB;
                }
                else if (color_hex == "#D80000" & showpic == true)
                {
                    BackgroundImage = Properties.Resources._D80000;
                }
                else if (color_hex == "#66CCFF" & showpic == true)
                {
                    BackgroundImage = Properties.Resources._66CCFF;
                }
                else { }
            }
            else
            {
                BackgroundImage = null;
                BackColor = Color.FromArgb(red, green, blue);
            }
        }

        public colorimg(int r,int g,int b,bool pic)//比色卡初始化并对传入变量进行处理
        {
            InitializeComponent();
            red = r;
            green = g;
            blue = b;
            showpic = pic;
            string[] rgb_hex =
            {
                red.ToString("x").ToUpper(),
                green.ToString("x").ToUpper(),
                blue.ToString("x").ToUpper()
            };
            int i = 0;
            foreach (string str in rgb_hex)
            {
                if (str.Length != 2)
                {
                    rgb_hex[i] = "0" + rgb_hex[i];
                }
                i++;
            }
            color_hex = "#";
            foreach (string str in rgb_hex)
            {
                color_hex = color_hex + str;
            }
            Text = "比色板-"+color_hex;

        }

        private void colorimg_Load(object sender, EventArgs e)//设置比色卡颜色
        {
                BackColor = Color.FromArgb(red, green, blue);
        }
    }
}

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
        int red, green, blue;
        bool showpic;

        private void colorimg_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.color-hex.com/color/" + Text.Replace("#", ""));
        }

        private void colorimg_Click(object sender, EventArgs e)
        {
            BackgroundImage = null;
            BackColor = Color.FromArgb(red, green, blue);
        }

        public colorimg(int r,int g,int b,bool pic)
        {
            InitializeComponent();
            red = r;
            green = g;
            blue = b;
            showpic = pic;
        }

        private void colorimg_Load(object sender, EventArgs e)
        {

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
            Text = "#";
            foreach (string str in rgb_hex)
            {
                Text = Text + str;
            }

            if (Text=="#39C5BB"&showpic==true)
            {
                BackgroundImage = Properties.Resources._39C5BB;
            }
            else if (Text == "#FFE212" & showpic == true)
            {
                BackgroundImage = Properties.Resources._FFE212;
            }
            else if (Text == "#FFBFCB" & showpic == true)
            {
                BackgroundImage = Properties.Resources._FFBFCB;
            }
            else if (Text == "#D80000" & showpic == true)
            {
                BackgroundImage = Properties.Resources._D80000;
            }
            else if (Text == "#66CCFF" & showpic == true)
            {
                BackgroundImage = Properties.Resources._66CCFF;
            }
            else
            {
                BackColor = Color.FromArgb(red, green, blue);
            }
        }
    }
}

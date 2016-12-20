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

namespace cydiacenTool
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;//去掉窗体边框
            this.Location = new Point(0, 0);//窗体左上角位置
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, //窗体宽度
                Screen.PrimaryScreen.Bounds.Height); //窗体高度
            pictureBox1.Dock = DockStyle.Fill;//图片框填充窗体
            label1.Width = 100;
            label1.Height = 100;
        }

        Bitmap screenBmp;//保存全屏的截图
        [DllImport("User32")]
        public static extern IntPtr GetDC(IntPtr h);
        [DllImport("gdi32")]
        public static extern uint GetPixel(IntPtr h, Point p);
        private void Form2_Load(object sender, EventArgs e)
        {
            //保存按钮的位置
            button1.Location = new Point(this.Width - button1.Width, this.Height - button1.Height);
            button2.Location = new Point(this.Width - button1.Width - button2.Width-10, this.Height - button1.Height);
            screenBmp = GetScreen();//获取屏幕图像保存到screenBmp中
            pictureBox1.Image = GetScreen();//获取全屏图像到图片框中
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {//这一步是在图片上面填充一层半透明黑色（QQ截图那种）
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(125, 0, 0, 0)))
                {
                    //g.FillRectangle(sb, 0, 0, this.Width, this.Height);//填充整个窗体
                }
            }
        }
        public Bitmap GetScreen()
        {//获取整个屏幕图像
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, this.Size);
            }
            return bmp;
        }
        int sx, sy;//鼠标点下时候的坐标信息
        int w, h;//拉出来的区域大小
        bool isDrawRect;//是否在窗体上绘制矩形
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                //MessageBox.Show("a");
                IntPtr h = GetDC(new IntPtr(0)); //取屏幕，0代表着全屏
                Point p = new Point(MousePosition.X, MousePosition.Y);
                uint color = GetPixel(h, p);        //取颜色喽
                uint red = (color & 0xFF);            //转换红色
                uint green = (color & 0xFF00) / 256;    //转换绿色
                uint blue = (color & 0xFF0000) / 65536;//转换蓝色
                textBox1.Text = "#" + red.ToString("x").PadLeft(2, '0') + green.ToString("x").PadLeft(2, '0') + blue.ToString("x").PadLeft(2, '0');
                Clipboard.Clear();
                Clipboard.SetDataObject(textBox1.Text,false);
                textBox1.Copy();
                this.Close();
            }
            else
            {
                sx = MousePosition.X;//记录当前鼠标坐标信息
                sy = MousePosition.Y;
                isDrawRect = true;//鼠标点下是绘制矩形
                w = h = 0;
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("a");
            
                //IntPtr hp = GetDC(new IntPtr(0)); //取屏幕，0代表着全屏
                //Point p = new Point(MousePosition.X, MousePosition.Y);
                //uint color = GetPixel(hp, p);        //取颜色喽
                //uint red = (color & 0xFF);            //转换红色
                //uint green = (color & 0xFF00) / 256;    //转换绿色
                //uint blue = (color & 0xFF0000) / 65536;//转换蓝色

                //textBox1.Text = "#" + red.ToString("x").PadLeft(2, '0') + green.ToString("x").PadLeft(2, '0') + blue.ToString("x").PadLeft(2, '0');
                //Clipboard.SetText(textBox1.Text);
                //textBox1.Copy();
                //this.Close();
            //picColor.BackColor = Color.FromArgb((int)red, (int)green, (int)blue);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawRect)
            {
//如果不允许绘制 直接返回
                IntPtr hh = GetDC(new IntPtr(0)); //取屏幕，0代表着全屏
                Point pp = new Point(MousePosition.X, MousePosition.Y);
                uint color = GetPixel(hh, pp); //取颜色喽
                uint red = (color & 0xFF); //转换红色
                uint green = (color & 0xFF00)/256; //转换绿色
                uint blue = (color & 0xFF0000)/65536; //转换蓝色
                label1.BackColor = Color.FromArgb((int) red, (int) green, (int) blue);
                label1.Location = new Point(0, 0);
                return;
            }
            pictureBox1.Refresh(); //刷新窗体（主要是在move事件里面在不停绘制绘制一次刷新一次（上次绘制的就被清楚了））
                using (Graphics g = pictureBox1.CreateGraphics())
                {
                    using (Pen p = new Pen(Color.FromArgb(255, 24, 215, 255)))
                    {
//创建画笔
                        w = MousePosition.X - sx; //当前鼠标x坐标减去点下鼠标时的x坐标就是宽度（注意负数）
                        h = MousePosition.Y - sy;
                        g.DrawRectangle(p, sx, sy, w, h); //绘制矩形
                    }
                }

                //textBox1.Location = new Point(MousePosition.X, MousePosition.Y);

            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawRect = false;// 鼠标抬起进制绘制矩形 并且把矩形区域的图像绘制出来
            if (sx > 0 && sy > 0&&h>1&&w>1)
            {
                Bitmap Bmp = new Bitmap(w + 1, h + 1);
                using (Graphics g = Graphics.FromImage(Bmp))
                {
                    Rectangle destRect = new Rectangle(0, 0, w + 1, h + 1);//在画布上要显示的区域
                    Rectangle srcRect = new Rectangle(sx, sy, w + 1, h + 1);//要截取的图像上面的区域
                    g.DrawImage(screenBmp, destRect, srcRect, GraphicsUnit.Pixel);//在screenBmp上截取图像保存到bmp中
                }
                Clipboard.SetImage(Bmp);
                new Form3().Show();
            }
            this.Close();
                //using (Graphics g = pictureBox1.CreateGraphics())
                //{
                //    Rectangle destRect = new Rectangle(sx, sy, w + 1, h + 1);//在画布上要显示的区域（记得像素加1）
                //    Rectangle srcRect = new Rectangle(sx, sy, w + 1, h + 1);//图像上要截取的区域
                //    g.DrawImage(screenBmp, destRect, srcRect, GraphicsUnit.Pixel);//加图像绘制到画布上
                //}
            }
        // 保存截图
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveBmp = new SaveFileDialog();
            saveBmp.Filter = "bmp格式|*.bmp|jpg格式|*.jpg";//过滤器
            saveBmp.FileName = "截图";//默认名字
            saveBmp.ShowDialog();//弹出对话框
            if (saveBmp.FileName != "")
            {
                Bitmap Bmp = new Bitmap(w + 1, h + 1);//创建新图像（因为刚才那个是直接在窗体上绘制的 保存的时候根据坐标重新绘制一下）
                using (Graphics g = Graphics.FromImage(Bmp))
                {
                    Rectangle destRect = new Rectangle(0, 0, w + 1, h + 1);//在画布上要显示的区域
                    Rectangle srcRect = new Rectangle(sx, sy, w + 1, h + 1);//要截取的图像上面的区域
                    g.DrawImage(screenBmp, destRect, srcRect, GraphicsUnit.Pixel);//在screenBmp上截取图像保存到bmp中
                    if (saveBmp.FilterIndex == 0)
                    {//判断选中的什么格式 然后保存
                        Bmp.Save(saveBmp.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                    else
                    {
                        Bmp.Save(saveBmp.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap Bmp = new Bitmap(w + 1, h + 1);
            using (Graphics g = Graphics.FromImage(Bmp))
            {
                Rectangle destRect = new Rectangle(0, 0, w + 1, h + 1);//在画布上要显示的区域
                Rectangle srcRect = new Rectangle(sx, sy, w + 1, h + 1);//要截取的图像上面的区域
                g.DrawImage(screenBmp, destRect, srcRect, GraphicsUnit.Pixel);//在screenBmp上截取图像保存到bmp中
            }
            Clipboard.SetImage(Bmp);

            new Form3().Show();
            
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        
    }
}

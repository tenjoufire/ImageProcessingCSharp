using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Imaging.ComplexFilters;

namespace Fourier
{
    public partial class Form1 : Form
    {
        private PictureBox pb1, pb2;
        private Bitmap img1, img2, highImg, lowImg;
        private Button bt1, bt2, bth, btl;
        private bool gere;
        //private bool flag = false;

        public Form1()
        {
            InitializeComponent();
            this.Text = "やったね！たえちゃん フーリエ変換めう！";
            this.Width = 770;
            this.Height = 290;

            loadImg();

            pb1 = new PictureBox();
            pb1.Location = new Point(0, 0);
            pb1.Size = new Size(256, 256);

            pb2 = new PictureBox();
            pb2.Location = new Point(500, 0);
            pb2.Size = new Size(256, 256);

            bt1 = new Button();
            bt1.Parent = this;
            bt1.Location = new Point(270, 40);
            bt1.Text = "image1";
            bt1.Click += new EventHandler(bt1Click);

            bt2 = new Button();
            bt2.Parent = this;
            bt2.Location = new Point(270, 80);
            bt2.Text = "image2";
            bt2.Click += new EventHandler(bt2Click);

            bth = new Button();
            bth.Parent = this;
            bth.Location = new Point(370, 40);
            bth.Text = "hi-pass";
            bth.Click += new EventHandler(bthClick);

            btl = new Button();
            btl.Parent = this;
            btl.Location = new Point(370, 80);
            btl.Text = "low-pass";
            btl.Click += new EventHandler(btlClick);


            Controls.Add(pb1);
            Controls.Add(pb2);
            Controls.Add(bt1);
            Controls.Add(bt2);
            Controls.Add(bth);
            Controls.Add(btl);
        }

        private void bt1Click(object sender, EventArgs e)
        {
            pb1.Image = img1;
            pb1.Refresh();
            gere = true;
        }

        private void bt2Click(object sender, EventArgs e)
        {
            pb1.Image = img2;
            pb1.Refresh();
            gere = false;
        }

        private void bthClick(object sender, EventArgs e)
        {
            if (gere) highpass(img1);
            else highpass(img2);
            pb2.Image = highImg;
            pb2.Refresh();
        }

        private void btlClick(object sender, EventArgs e)
        {
            if (gere) lowpass(img1);
            else lowpass(img2);
            pb2.Image = lowImg;
            pb2.Refresh();
        }


        private void loadImg()
        {
            //FFTを使用する都合上画像の解像度は2のべき乗でなければならない
            img1 = new Bitmap("sengp.bmp");
            img2 = new Bitmap("ten.bmp");
        }

        private void highpass(Bitmap img)
        {
            //グレースケール化
            Bitmap gray = new Grayscale(0.2125, 0.7154, 0.0721).Apply(img);
            //専用のクラスに画像を導入
            var complexImg = ComplexImage.FromBitmap(gray);
            //フーリエ変換
            complexImg.ForwardFourierTransform();
            //ハイパスフィルタ
            FrequencyFilter filter = new FrequencyFilter(new AForge.IntRange(20, 128));
            filter.Apply(complexImg);
            //フーリエ逆変換
            complexImg.BackwardFourierTransform();
            highImg = complexImg.ToBitmap();
        }

        private void lowpass(Bitmap img)
        {
            //グレースケール化
            Bitmap gray = new Grayscale(0.2125, 0.7154, 0.0721).Apply(img);
            //専用のクラスに画像を導入
            var complexImg = ComplexImage.FromBitmap(gray);
            //フーリエ変換
            complexImg.ForwardFourierTransform();
            //ローパスフィルタ
            FrequencyFilter filter = new FrequencyFilter(new AForge.IntRange(0, 20));
            filter.Apply(complexImg);
            //フーリエ逆変換
            complexImg.BackwardFourierTransform();
            lowImg = complexImg.ToBitmap();
        }

        
    }
}

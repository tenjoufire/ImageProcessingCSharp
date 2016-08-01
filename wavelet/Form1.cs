using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveletTransform
{
    public partial class Form1 : Form
    {
        private PictureBox picBox;
        private Button bt1;
        private Bitmap img;

        public Form1()
        {
            InitializeComponent();
            this.Text = "HANE2000";
            this.Width = 512;
            this.Height = 700;

            picBox = new PictureBox();
            picBox.Location = new Point(0, 0);
            picBox.Size = new Size(512, 512);

            bt1 = new Button();
            bt1.Parent = this;
            bt1.SetBounds(this.Width / 2 - 100, this.Height - 100, 200, 30);
            //bt1.Location = new Point(this.Width / 2, this.Height - 50);
            bt1.Text = "Let's Wavelet Transform!";
            bt1.Click += new EventHandler(LoadImg);

            Controls.Add(picBox);
            Controls.Add(bt1);
        }

        private void LoadImg(object sender, EventArgs e)
        {
            //Load Image from current directory
            img = new Bitmap("hanes.bmp");
            //waveImg = Wavelet(img);
            picBox.Image = Wavelet(img);

        }

        private Bitmap Wavelet(Bitmap im)
        {
            //入力画像のグレースケール化
            var grayImg = new AForge.Imaging.Filters.Grayscale(0.2125, 0.7154, 0.0721).Apply(im);

            //wavelet変換用のHaar関数を設定
            var wavelet = new Accord.Imaging.Filters.WaveletTransform(new Accord.Math.Wavelets.Haar(3));

            //wavelet変換を実行
            return wavelet.Apply(grayImg);
        }
    }
}

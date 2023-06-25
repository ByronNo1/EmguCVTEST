using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        Image<Bgr, Byte> imgBGR;
        Mat one_channel_H;
        Mat one_channel_S;
        Mat one_channel_V;
        Mat one_Low;
        Mat one_High;
        Mat one_Result;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // CvInvoke.NamedWindow("First Window");
            //Create an image of 480x200 with color yellow
            //
             imgBGR = new Image<Bgr, Byte>(@"D:\NEWUPDATA\_Haved\testPicture\20221030\南米那斯\FAIL\2022-10-30-135207.jpg");// path can be absolute or relative.

            Image<Bgr, Byte> img_hsv = new Image<Bgr, Byte>(imgBGR.Width, imgBGR.Height);



            CvInvoke.CvtColor(imgBGR, img_hsv, ColorConversion.Bgr2Hsv, 0); //轉換成HSV

            VectorOfMat vm = new VectorOfMat();
            CvInvoke.Split(img_hsv, vm);//分割图像
            var vms = vm.GetOutputArray();
            one_channel_H = vms.GetMat(0);//H
            one_channel_S = vms.GetMat(1);//S
            one_channel_V = vms.GetMat(2);//V

            //CvInvoke.Imshow("TEST H", one_channel_H);
            //CvInvoke.Imshow("TEST S", one_channel_S);
            //CvInvoke.Imshow("TEST V", one_channel_V);

            IInputArray lowArray = new ScalarArray(new MCvScalar(110, 10, 105));
            IInputArray HighArray = new ScalarArray(new MCvScalar(140, 160, 200));

            Image<Bgr, Byte> img_hsv_out = new Image<Bgr, Byte>(imgBGR.Width, imgBGR.Height);


            Mat outRegion = new Mat();
            CvInvoke.InRange(img_hsv, lowArray, HighArray, img_hsv_out);


            // CvInvoke.BitwiseAnd(img_hsv, img_hsv, img_hsv_out, outRegion);
            // Image<Emgu.CV.Structure.Gray, Byte> one_channel_H_out = new Image<Emgu.CV.Structure.Gray, Byte>(imgBGR.Width, imgBGR.Height);
            //Mat one_channel_H_out = new Mat();//H
            // CvInvoke.Threshold(one_channel_H, one_channel_H_out, 112, 112, ThresholdType.Trunc);
            //CvInvoke.InRange(one_channel_H, lowArray, HighArray, one_channel_H_out);

             one_Low = new Mat();
             one_High = new Mat();
             one_Result = new Mat();
            CvInvoke.Threshold(one_channel_H, one_Low, 80, 255, ThresholdType.Binary);
            CvInvoke.Threshold(one_channel_H, one_High, 138, 255, ThresholdType.Binary);
            CvInvoke.Subtract(one_Low, one_High, one_Result);

            Bitmap bitmap = one_channel_H.ToBitmap();//Mat转成Bitmap


            one_channel_H.Save(Application.StartupPath + "\\one_channel_H.jpg");
            pictureBox1.Image = bitmap;

            CvInvoke.Imshow("TEST L", one_Low);
            CvInvoke.Imshow("TEST H", one_High);
            CvInvoke.Imshow("TEST Finsih", one_Result);



            //using (Image<Bgr, Byte> img1 = new Image<Bgr, byte>(480, 200, new Bgr(0, 255, 255)))
            //{
            //    //字体外观
            //    FontFace fontFace = new FontFace();
            //    //字体大小
            //    double fontSize = 1;
            //    img1.Draw("Hello, world", new Point(125, 100), fontFace, fontSize, new Bgr(255, 0, 0));
            //    //CvInvoke.Imshow("First Window", img1);
            //    //CvInvoke.WaitKey(0);
            //    //CvInvoke.DestroyWindow("First Window");
            //}
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
               if  (((NumericUpDown)sender).Name == "numericUpDown1" )
                {
                    CvInvoke.Threshold(one_channel_H, one_Low, (int)((NumericUpDown)sender).Value, 255, ThresholdType.Binary); //86
                }
                else
                {
                    CvInvoke.Threshold(one_channel_H, one_High, (int)((NumericUpDown)sender).Value, 255, ThresholdType.Binary); //201
                }
                CvInvoke.Subtract(one_Low, one_High, one_Result);
                Bitmap bitmap = one_Result.ToBitmap();//Mat转成Bitmap
                pictureBox1.Image = bitmap;
            }
            catch (Exception)
            {

            }
      

        }
    }
}

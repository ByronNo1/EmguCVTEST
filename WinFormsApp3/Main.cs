using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Dnn;

using com.ipevo.windows;
using com.ipevo.windows.CameraKit;
using com.ipevo.windows.ToolKit;
using System.Runtime.InteropServices;


using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;
using System.Reflection;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Emgu.CV.Reg;

namespace WinFormsApp3
{

    public partial class Main : Form
    {
        Image<Bgr, Byte> imgBGR;
        Mat one_channel_H;
        Mat one_channel_S;
        Mat one_channel_V;
        Mat one_Low;
        Mat one_High;
        Mat one_Result;
        int minPixel = 25;//最小數限制
        Task TT1 = null;
        List<string> ListPath = new List<string>();
        List<VectorOfVectorOfPoint> ListVectorOfPoint = new List<VectorOfVectorOfPoint>();
        VideoCapture Cap = null;
        Thread CCDthread = null;
        bool isCCDGet = false;
        CCStatsOp[] statsop;
        MCvPoint2D64f[] centroidPoints;
        Image<Gray, byte> imgCC;




        public struct CCStatsOp
        {
            public Rectangle Rectangle;
            public int Area;
        }

        public Main()
        {
            InitializeComponent();
        }


        private void testFun()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    testFun();
                }));
                return;
            }


            Mat tmpMat = new Mat();


            //analysis_Fun(file, out tmpMat, out tmpVectorOfVectorOfPoint);
            string file = txt_Path.Text + @"\" + listB_Path.SelectedItem.ToString();

            VectorOfVectorOfPoint resultcontours = new VectorOfVectorOfPoint();
            Mat resultImgBGR = new Mat();
            Mat _channel_H = new Mat(), _channel_S = new Mat(), _channel_V = new Mat();
            Mat _one_Low = new Mat(), _one_High = new Mat(), _one_Result = new Mat();

            Image<Bgr, Byte> tmpImgBGR = new Image<Bgr, Byte>(file);// path can be absolute or relative.

            pictureBox0.Image = tmpImgBGR.ToBitmap();
            Application.DoEvents();
            //MessageBox.Show("RGB");
            Image<Bgr, Byte> _img_hsv = new Image<Bgr, Byte>(tmpImgBGR.Width, tmpImgBGR.Height);
            CvInvoke.CvtColor(tmpImgBGR, _img_hsv, ColorConversion.Bgr2Hsv, 0); //轉換成HSV
            pictureBox0.Image = _img_hsv.ToBitmap();
            Application.DoEvents();
            // MessageBox.Show("HSV");


            VectorOfMat vm = new VectorOfMat();
            CvInvoke.Split(_img_hsv, vm);//分割图像
            var vms = vm.GetOutputArray();
            _channel_H = vms.GetMat(0);//H
            pictureBox0.Image = _channel_H.ToBitmap();
            _channel_H.Save("H.jpg");
            Application.DoEvents();
            //MessageBox.Show("H");
            _channel_S = vms.GetMat(1);//S
            pictureBox0.Image = _channel_S.ToBitmap();
            _channel_S.Save("S.jpg");
            Application.DoEvents();
            // MessageBox.Show("S");

            _channel_V = vms.GetMat(2);//V
            pictureBox0.Image = _channel_V.ToBitmap();
            _channel_V.Save("V.jpg");
            Application.DoEvents();
            // MessageBox.Show("V");

            CvInvoke.Threshold(_channel_S, _one_Low, 65, 255, ThresholdType.Binary); //
            CvInvoke.Threshold(_channel_S, _one_High, 255, 255, ThresholdType.BinaryInv); //
            CvInvoke.BitwiseAnd(_one_Low, _one_High, _one_Result);

            pictureBox0.Image = _one_Result.ToBitmap();
            Application.DoEvents();
            //MessageBox.Show("_one_Result");

            // pictureBox0.Image.Save(Application.StartupPath + @"\test.jpg");
            Image<Bgr, Byte> draw = tmpImgBGR.Clone();
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(_one_Result, contours, null, RetrType.Tree, ChainApproxMethod.ChainApproxNone);



            Image<Gray, Byte> GaryIMG = tmpImgBGR.Convert<Gray, byte>().ThresholdBinary(new Gray(100), new Gray(255))
                    .Dilate(1).Erode(1);

            pictureBox0.Image = GaryIMG.ToBitmap();
            Application.DoEvents();
            //MessageBox.Show("GaryIMG");



            Mat TESTMAT = new Mat();
            Mat imgLabel = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            Mat element = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross,
                    new Size(31, 31), new Point(-1, -1));

            CvInvoke.Imshow("contours", GaryIMG);
            CvInvoke.WaitKey(0);

            //CvInvoke.Dilate(GaryIMG, GaryIMG, element, new Point(-1, -1), 3, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
            //CvInvoke.Erode(GaryIMG, GaryIMG, element, new Point(-1, -1), 3, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
            //CvInvoke.Imshow("contours", GaryIMG);
            //CvInvoke.WaitKey(0);

            //int nLabel = CvInvoke.ConnectedComponents(_one_Result, TESTMAT);
            CvInvoke.FindContours(GaryIMG, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxNone);
            //CvInvoke.FindContours(GaryIMG, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxNone);
            var nLabels = CvInvoke.ConnectedComponentsWithStats(GaryIMG, imgLabel, stats, centroids);
            centroidPoints = new MCvPoint2D64f[nLabels];
            centroids.CopyTo(centroidPoints);
            imgCC = imgLabel.ToImage<Gray, byte>();

            statsop = new CCStatsOp[nLabels];
            stats.CopyTo(statsop);

            //CvInvoke.FindContours(_one_Result, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            _img_hsv = _one_Result.ToImage<Bgr, Byte>();
            for (int label = 1; label < nLabels; label++)
            {
                var temp = imgCC.InRange(new Gray(label), new Gray(label));

                int x = (int)centroidPoints[label].X;
                int y = (int)centroidPoints[label].Y;

                var t = tmpImgBGR.Copy();
                CvInvoke.PutText(t, "o", new Point(x, y), Emgu.CV.CvEnum.FontFace.HersheyPlain, 0.8, new MCvScalar(0, 0, 255), 2);

                Rectangle rect = statsop[label].Rectangle;
                t.Draw(rect, new Bgr(0, 0, 255), 2);

                label1.Text = statsop[label].Area.ToString();



               
            }


            
            CvInvoke.Dilate(GaryIMG, GaryIMG, element, new Point(-1, -1), 3, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
            CvInvoke.Erode(GaryIMG, GaryIMG, element, new Point(-1, -1), 3, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));

            for (int i = 0; i < contours.Size; i++)
            {
                CvInvoke.DrawContours(draw, contours, i, new MCvScalar(255, 0, 0), 5);
                CvInvoke.Imshow("contours", draw);
                CvInvoke.WaitKey(0);
            }
            CvInvoke.Dilate(GaryIMG, GaryIMG, element, new Point(-1, -1), 3, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
            CvInvoke.Erode(GaryIMG, GaryIMG, element, new Point(-1, -1), 3, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
            CvInvoke.CvtColor(GaryIMG, draw, ColorConversion.Gray2Bgr, 0);
            for (int i = 0; i < contours.Size; i++)
            {
                CvInvoke.DrawContours(draw, contours, i, new MCvScalar(255, 255, 0), 5);
                CvInvoke.Imshow("contours", draw);
                CvInvoke.WaitKey(0);
            }
            CvInvoke.Imshow("contours", draw);
            CvInvoke.WaitKey(0);
           
            int count = contours.Size;
            List<int> listArea = new List<int>();

            for (int i = 0; i < count; i++)
            {
                using (VectorOfPoint contour = contours[i])
                {
                    // 使用 BoundingRectangle 取得框選矩形
                    Rectangle BoundingBox = CvInvoke.BoundingRectangle(contour);
                    int intArea = BoundingBox.Height * BoundingBox.Width;
                    listArea.Add(intArea);
                }

            }

            double average1 = listArea.Average();
            double sumOfSquaresOfDifferences = listArea.Select(val => (val - average1) * (val - average1)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / listArea.Count);
            double Minsd = average1 - 2 * sd;
            for (int i = 0; i < count; i++)
            {

                using (VectorOfPoint contour = contours[i])
                {
                    // 使用 BoundingRectangle 取得框選矩形
                    Rectangle BoundingBox = CvInvoke.BoundingRectangle(contour);
                    int intArea = BoundingBox.Height * BoundingBox.Width;
                    //if (intArea > Minsd )
                    {
                        BoundingBox = new Rectangle(BoundingBox.X - 8, BoundingBox.Y - 8, BoundingBox.Width + 15, BoundingBox.Height + 15);
                        CvInvoke.Rectangle(draw, BoundingBox, new MCvScalar(255, 0, 255, 255), 3);
                    }
                }

                pictureBox0.Image = draw.ToBitmap();

                Application.DoEvents();
                //MessageBox.Show("Rectangle" + i);
            }
            pictureBox0.Image = draw.ToBitmap();

            resultcontours = contours;
            resultImgBGR = tmpImgBGR.Mat;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {


                //imgBGR = new Image<Bgr, Byte>(txt_Path + @"\" + listB_Path.SelectedItem.ToString());// path can be absolute or relative.

                Task tt = new Task(new Action(() =>
                {

                    testFun();


                }));
                tt.Start();

            }
            catch (Exception)
            {

            }



        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (((NumericUpDown)sender).Name == "numericUpDown1")
                {
                    // CvInvoke.Threshold(one_channel_H, one_Low, (int)((NumericUpDown)sender).Value, 255, ThresholdType.Binary); //1
                    CvInvoke.Threshold(one_channel_H, one_Low, (int)((NumericUpDown)sender).Value, 255, ThresholdType.Binary); //1
                }
                else
                {
                    //CvInvoke.Threshold(one_channel_H, one_High, (int)((NumericUpDown)sender).Value, 255, ThresholdType.Binary); //70
                    CvInvoke.Threshold(one_channel_H, one_High, (int)((NumericUpDown)sender).Value, 255, ThresholdType.BinaryInv); //70
                }
                //CvInvoke.Subtract(one_Low, one_High, one_Result);
                CvInvoke.BitwiseAnd(one_Low, one_High, one_Result);
                Bitmap bitmap = one_Result.ToBitmap();//Mat转成Bitmap
                CvInvoke.Imshow("High", one_High);
                CvInvoke.Imshow("one_Low", one_Low);
                pictureBox0.Image = bitmap;
            }
            catch (Exception)
            {

            }


        }

        private void btnMorphology_Click(object sender, EventArgs e)
        {

            MorphologyErodeDilate();

        }


        private void MorphologyErodeDilate()
        {
            if (this.InvokeRequired) //Invoke別種用法
            {
                this.Invoke(new EventHandler(delegate
                {
                    MorphologyErodeDilate();
                }));
                return;
            }
            if (listB_Path.Items.Count == 0)
            {
                return;
            }

            if (listB_Path.SelectedIndex < 0)
            {
                listB_Path.SelectedIndex = 0;
            }
            string file = Path.Combine(txt_Path.Text, listB_Path.SelectedItem.ToString());
            if (File.Exists(file) == false)
            {
                return;
            }

            Image<Emgu.CV.Structure.Gray, Byte> tmpImg = new Image<Emgu.CV.Structure.Gray, Byte>(file);// path can be absolute or relative.
            Image<Emgu.CV.Structure.Gray, Byte> tmpImgA = new Image<Gray, byte>(tmpImg.Size);
            Image<Emgu.CV.Structure.Gray, Byte> tmpImgB = new Image<Gray, byte>(tmpImg.Size);

            Mat element = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross,
                    new Size(25, 25), new Point(-1, -1));

            //    dilate(srcImage, dstImage, structuringE, Point(2, 2), 1, BORDER_DEFAULT); 
            Image<Emgu.CV.Structure.Rgb, Byte> tmpImgRGB = new Image<Emgu.CV.Structure.Rgb, Byte>(file);// path can be absolute or relative.
            Image<Bgr, Byte> _img_Gray = new Image<Bgr, Byte>(tmpImgRGB.Width, tmpImgRGB.Height);
            CvInvoke.CvtColor(tmpImgRGB, _img_Gray, ColorConversion.Bgr2Gray, 0); //轉換成_img_Gray
            //MessageBox.Show("原圖");
            picOrigin.Image = tmpImgRGB.ToBitmap();
            Application.DoEvents();


            //CvInvoke.Threshold(_img_Gray, tmpImgA, 0, 255, ThresholdType.Otsu); //大津演算法

            CvInvoke.Threshold(_img_Gray, tmpImgA, trackBarThreshold.Value, 255, ThresholdType.BinaryInv);

            //MessageBox.Show("二值化");
            picBinary.Image = tmpImgA.ToBitmap();
            Application.DoEvents();


            CvInvoke.Erode(tmpImgA, tmpImgB, element, new Point(-1, -1), 3, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
            // MessageBox.Show("腐蝕");
            picErosion.Image = tmpImgB.ToBitmap();
            Application.DoEvents();

            CvInvoke.Dilate(tmpImgA, tmpImgB, element, new Point(-1, -1), 3, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
            // MessageBox.Show("膨脹");
            pictDilation.Image = tmpImgB.ToBitmap();
            Application.DoEvents();

            return;
        }

        private void btn_browser_Click(object sender, EventArgs e)
        {

            OpenFileDialog folderBrowser = new OpenFileDialog();
            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                txt_Path.Text = folderPath;
                CHKbrowser(folderPath);  //創造相對應的TXT

                // ...
            }
        }

        private void CHKbrowser(string folderPath)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(delegate  //另外的寫法
                {
                    CHKbrowser(folderPath);
                }));
                return;
            }


            try
            {
                listB_Path.Items.Clear();
                ListPath.Clear();
                if (txt_Path.Text.Equals(Path.GetFullPath(folderPath)) == false)
                {
                    txt_Path.Text = Path.GetFullPath(folderPath);
                }
                string[] files = Directory.GetFiles(folderPath);
                foreach (var file in files)
                {
                    try
                    {
                        if (Path.GetExtension(file).ToUpper() == ".JPG")  //找到JPG檔案，創造相同名稱的TXT
                        {

                            ListPath.Add(file);
                            listB_Path.Items.Add(Path.GetFileName(file));
                            string strSaveTxtPath = Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file) + ".txt";

                            if (File.Exists(strSaveTxtPath) == false)
                            {
                                using (StreamWriter SW = new StreamWriter(strSaveTxtPath, false))
                                {
                                }
                            }


                        }

                    }
                    catch (Exception)
                    {

                    }
                    Application.DoEvents();
                }

            }
            catch (Exception)
            {

            }
        }



        private void btn_Start_Click(object sender, EventArgs e)
        {
            Color C = ((System.Windows.Forms.Button)sender).BackColor;
            GC.Collect();
            try
            {
                ((Button)sender).Enabled = false;
                ((Button)sender).BackColor = Color.Green;
                Application.DoEvents();
                //   ListPathHObject.Clear();


                if (txt_Path.Text != "" && Directory.Exists(txt_Path.Text))
                {
                    TT1 = Task.Run(() =>
                    {
                        Label_ALL_FunTxt(txt_Path.Text);  //開始自動標記出咖啡豆的位置，並記錄到TXT
                    });

                    while (TT1.IsCompleted == false)
                    {
                        Application.DoEvents();
                        Thread.Sleep(1);
                    }
                    TT1 = null;
                }
            }
            catch (Exception)
            {

            }
            ((Button)sender).Enabled = true;
            ((Button)sender).BackColor = C;
        }




        private void Label_ALL_FunTxt(string _strPath)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    Label_ALL_FunTxt(_strPath);
                }));
                return;
            }


            // Local iconic variables 
            string[] files = Directory.GetFiles(_strPath);

            ListVectorOfPoint.Clear();
            listB_Path.Items.Clear();
            ListPath.Clear();

            foreach (var file in files)
            {
                try
                {
                    if (Path.GetExtension(file).ToUpper() == ".JPG")
                    {
                        Mat tmpMat = new Mat();
                        VectorOfVectorOfPoint tmpVectorOfVectorOfPoint = new VectorOfVectorOfPoint();
                        ListPath.Add(file);
                        listB_Path.Items.Add(Path.GetFileName(file));
                        //analysis_Fun(file, out tmpMat, out tmpVectorOfVectorOfPoint);
                        analysis_FunWhite(file, out tmpMat, out tmpVectorOfVectorOfPoint);
                        ListVectorOfPoint.Add(tmpVectorOfVectorOfPoint);
                        SaveTxt(file, tmpMat, tmpVectorOfVectorOfPoint);
                    }

                }
                catch (Exception)
                {

                }
                Application.DoEvents();




                ////SAVE
                //SaveTxt(strImgPath, hv_W, hv_H, hv_depth, hv_Row1, hv_Column1, hv_Row2, hv_Column2);

                //ho_Image.Dispose();
                //ho_RegionDilation.Dispose();

                //hv_W.Dispose(); hv_H.Dispose();
                //hv_Row1.Dispose(); hv_Column1.Dispose(); hv_Row2.Dispose(); hv_Column2.Dispose();
                //hv_Number.Dispose();

                //if (isFrmClose)
                //{
                //    return;
                //}

            }

        }



        private void analysis_Fun(string file)
        {
            analysis_Fun(file, out _, out _);
        }

        private void analysis_Fun(string file, out Mat resultImgBGR, out VectorOfVectorOfPoint resultcontours)
        {
            resultcontours = new VectorOfVectorOfPoint();
            resultImgBGR = new Mat();
            Mat _channel_H = new Mat(), _channel_S = new Mat(), _channel_V = new Mat();
            Mat _one_Low = new Mat(), _one_High = new Mat(), _one_Result = new Mat();

            Image<Bgr, Byte> tmpImgBGR = new Image<Bgr, Byte>(file);// path can be absolute or relative.


            Image<Bgr, Byte> _img_hsv = new Image<Bgr, Byte>(tmpImgBGR.Width, tmpImgBGR.Height);
            CvInvoke.CvtColor(tmpImgBGR, _img_hsv, ColorConversion.Bgr2Hsv, 0); //轉換成HSV
            VectorOfMat vm = new VectorOfMat();
            CvInvoke.Split(_img_hsv, vm);//分割图像
            var vms = vm.GetOutputArray();
            _channel_H = vms.GetMat(0);//H
            _channel_S = vms.GetMat(1);//S
            _channel_V = vms.GetMat(2);//V


            CvInvoke.Threshold(_channel_H, _one_Low, 0, 255, ThresholdType.Binary); //
            CvInvoke.Threshold(_channel_H, _one_High, 70, 255, ThresholdType.BinaryInv); //
            CvInvoke.BitwiseAnd(_one_Low, _one_High, _one_Result);

            Image<Bgr, Byte> draw = tmpImgBGR.Clone();
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(_one_Result, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            int count = contours.Size;
            for (int i = 0; i < count; i++)
            {
                using (VectorOfPoint contour = contours[i])
                {
                    // 使用 BoundingRectangle 取得框選矩形
                    Rectangle BoundingBox = CvInvoke.BoundingRectangle(contour);
                    if (BoundingBox.Width > minPixel && BoundingBox.Height > minPixel)
                    {
                        BoundingBox = new Rectangle(BoundingBox.X - 8, BoundingBox.Y - 8, BoundingBox.Width + 15, BoundingBox.Height + 15);
                        CvInvoke.Rectangle(draw, BoundingBox, new MCvScalar(255, 0, 255, 255), 3);
                    }
                }
            }
            pictureBox0.Image = draw.ToBitmap();


            resultcontours = contours;
            resultImgBGR = tmpImgBGR.Mat;

        }


        private void analysis_FunWhite(string file, out Mat resultImgBGR, out VectorOfVectorOfPoint resultcontours)
        {
            resultcontours = new VectorOfVectorOfPoint();
            resultImgBGR = new Mat();
            Mat _channel_H = new Mat(), _channel_S = new Mat(), _channel_V = new Mat();
            Mat _one_Low = new Mat(), _one_High = new Mat(), _one_Result = new Mat();

            Image<Bgr, Byte> tmpImgBGR = new Image<Bgr, Byte>(file);// path can be absolute or relative.


            Image<Bgr, Byte> _img_hsv = new Image<Bgr, Byte>(tmpImgBGR.Width, tmpImgBGR.Height);
            CvInvoke.CvtColor(tmpImgBGR, _img_hsv, ColorConversion.Bgr2Hsv, 0); //轉換成HSV
            VectorOfMat vm = new VectorOfMat();
            CvInvoke.Split(_img_hsv, vm);//分割图像
            var vms = vm.GetOutputArray();
            _channel_H = vms.GetMat(0);//H
            _channel_S = vms.GetMat(1);//S
            _channel_V = vms.GetMat(2);//V


            CvInvoke.Threshold(_channel_S, _one_Low, 65, 255, ThresholdType.Binary); //
            CvInvoke.Threshold(_channel_S, _one_High, 255, 255, ThresholdType.BinaryInv); //
            CvInvoke.BitwiseAnd(_one_Low, _one_High, _one_Result);

            Image<Bgr, Byte> draw = tmpImgBGR.Clone();
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(_one_Result, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            int count = contours.Size;
            for (int i = 0; i < count; i++)
            {
                using (VectorOfPoint contour = contours[i])
                {
                    // 使用 BoundingRectangle 取得框選矩形
                    Rectangle BoundingBox = CvInvoke.BoundingRectangle(contour);
                    if (BoundingBox.Width > minPixel && BoundingBox.Height > minPixel)
                    {
                        BoundingBox = new Rectangle(BoundingBox.X - 8, BoundingBox.Y - 8, BoundingBox.Width + 15, BoundingBox.Height + 15);
                        CvInvoke.Rectangle(draw, BoundingBox, new MCvScalar(255, 0, 255, 255), 3);
                    }
                }
            }
            pictureBox0.Image = draw.ToBitmap();


            resultcontours = contours;
            resultImgBGR = tmpImgBGR.Mat;

        }


        private void SaveTxt(string _strPath, Mat _ImgBGR, VectorOfVectorOfPoint _contours)
        { //圖片深度
            string strSaveTxtPath = Path.GetDirectoryName(_strPath) + "\\" + Path.GetFileNameWithoutExtension(_strPath) + ".txt";
            string strLabel = "";

            if (txtLabel.Text == "" || int.TryParse(txtLabel.Text, out _) == false)
            {
                strLabel = "0";
            }
            else
            {
                strLabel = txtLabel.Text;
            }
            Image<Bgr, byte> tImgBGR = _ImgBGR.ToImage<Bgr, byte>();


            int intWidth = (int)tImgBGR.Width; // 照片尺寸 width
            int intHeight = (int)tImgBGR.Height; // 照片尺寸 height
            double DouCenterW = 0;
            double DouCenterH = 0;
            double xmin, ymin, xmax, ymax;
            double Label_H, Label_W; // 矩陣長寬
            double ratio_Center_H, ratio_Center_W; // 矩陣中心跟原圖片比例
            double ratio_Length_H, ratio_Length_W; // 矩陣中心跟原圖片比例
            string StrSaveData = "";

            //              <----------------------------intWidth------------------------------->
            //              ---------------------------------------------------------------------
            //       ^      |                                                                   |
            //       |      |                                                                   |
            //       |      |                                                                   |
            //       |      |                                                                   |
            //       |      |                              Label_W                              |
            //       |      |                         ******************                        |
            //   intHeight  |                         *                *                        |
            //       |      |               Label_H   *                *                        |
            //       |      |                         *                *                        |
            //       |      |                         ******************                        |
            //       |      |                                                                   |
            //       |      |                                                                   |
            //       |      |                                                                   |
            //       v      |                                                                   |
            //              ---------------------------------------------------------------------
            //

            //              <----------------------------ImgHeight------------------------------->          |              <----------------------------ImgWidth------------------------------->
            //              ---------------------------------------------------------------------           |              ---------------------------------------------------------------------
            //       ^      |                       ^                                           |           |       ^      |                                                                   |
            //       |      |                       |                                           |           |       |      |                                                                   |
            //       |      |                       |                                           |           |       |      |                                                                   |
            //       |      |                       |     Center_W                              |           |       |      |                         Rectangle_Width                           |
            //       |      | <---------------------|--------->                                 |           |       |      |                         <---------------->                        |
            //       |      |                       |  ******************                       |           |       |      |                        ^******************                        |
            //   ImgHeight  |                       |  *                *                       |           |   ImgHeight  |                        |*                *                        |
            //       |      |             Center_H  v  *       X        *                       |           |       |      |     Rectangle_Height   |*                *                        |
            //       |      |                          *                *                       |           |       |      |                        |*                *                        |
            //       |      |                          ******************                       |           |       |      |                        v******************                        |
            //       |      |                                                                   |           |       |      |                                                                   |
            //       |      |                                                                   |           |       |      |                                                                   |
            //       |      |                                                                   |           |       |      |                                                                   |
            //       v      |                                                                   |           |       v      |                                                                   |
            //              ---------------------------------------------------------------------           |              ---------------------------------------------------------------------
            //                                                                                              |





            using (StreamWriter SW = new StreamWriter(strSaveTxtPath, false))
            {
                int count = _contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (VectorOfPoint contour = _contours[i])
                    {
                        Rectangle BoundingBox = CvInvoke.BoundingRectangle(contour);
                        if (BoundingBox.Width > minPixel && BoundingBox.Height > minPixel)
                        {
                            BoundingBox = new Rectangle(BoundingBox.X - 8, BoundingBox.Y - 8, BoundingBox.Width + 15, BoundingBox.Height + 15);
                            //node = doc.CreateNode(XmlNodeType.Element, "xmin", null);
                            xmin = BoundingBox.Left; // 標籤的座標位置 
                                                     //node = doc.CreateNode(XmlNodeType.Element, "ymin", null);
                            ymin = BoundingBox.Top; // 標籤的座標位置 
                                                    //node = doc.CreateNode(XmlNodeType.Element, "xmax", null);
                            xmax = BoundingBox.Right; // 標籤的座標位置  
                                                      //node = doc.CreateNode(XmlNodeType.Element, "ymax", null);
                            ymax = BoundingBox.Bottom; // 標籤的座標位置                

                            DouCenterW = (xmin + xmax) / 2;
                            DouCenterH = (ymin + ymax) / 2;
                            Label_W = xmax - xmin;
                            Label_H = ymax - ymin;
                            ratio_Center_W = Math.Round(DouCenterW / intWidth, 10);
                            ratio_Center_H = Math.Round(DouCenterH / intHeight, 10);
                            ratio_Length_W = Math.Round(Label_W / intWidth, 10);
                            ratio_Length_H = Math.Round(Label_H / intHeight, 10);
                            StrSaveData = strLabel + " " + ratio_Center_W + " " + ratio_Center_H + " " + ratio_Length_W + " " + ratio_Length_H;
                            SW.WriteLine(StrSaveData);
                        }
                    }
                }
            }

        }




        


        private void CutTxt_Fun(string _strPath)
        {


            string file = _strPath;
            string strPathTxt;
            strPathTxt = file.Replace(Path.GetExtension(file), ".txt");
            string[] strstr;
            List<String> tmpList = new List<string>();


            double ratio_Center_W, DouCenterW;
            double ratio_Center_H, DouCenterH;
            double ratio_Length_W, Label_W;
            double ratio_Length_H, Label_H;
            double xmin, ymin, xmax, ymax;

            if (File.Exists(file))
            {
                Image<Bgr, Byte> draw = new Image<Bgr, Byte>(file);
                using (StreamReader SR = new StreamReader(strPathTxt))
                {
                    while (SR.Peek() >= 0)
                    {
                        tmpList.Add(SR.ReadLine());
                    }

                    for (int i = 0; i < tmpList.Count; i++)
                    {
                        strstr = tmpList[i].Split(' ');
                        if (strstr.Length == 5)
                        {
                            if (double.TryParse(strstr[1], out ratio_Center_W) && double.TryParse(strstr[2], out ratio_Center_H) &&
                                double.TryParse(strstr[3], out ratio_Length_W) && double.TryParse(strstr[4], out ratio_Length_H))
                            {
                                DouCenterW = ratio_Center_W * draw.Width;
                                DouCenterH = ratio_Center_H * draw.Height;
                                Label_W = ratio_Length_W * draw.Width;
                                Label_H = ratio_Length_H * draw.Height;
                                xmax = DouCenterW + Label_W / 2;
                                xmin = DouCenterW - Label_W / 2;
                                ymax = DouCenterH + Label_H / 2;
                                ymin = DouCenterH - Label_H / 2;
                                Rectangle BoundingBox;
                                BoundingBox = new Rectangle((int)Math.Round(xmin, 0) - 6, (int)Math.Round(ymin, 0) - 6, (int)Math.Round(Label_W, 0) + 11, (int)Math.Round(Label_H, 0) + 11);
                                // CvInvoke.Rectangle(draw, BoundingBox, new MCvScalar(255, 0, 255, 255), 3);
                                if (BoundingBox.Width > 50 && BoundingBox.Height > 50)
                                {
                                    Mat image_cut = new Mat(draw.Mat, BoundingBox);      //从img中按照rect进行切割，此时修改image_cut时image中对应部分也会修改，因此需要copy
                                    Image<Bgr, Byte> image_copy = image_cut.ToImage<Bgr, Byte>();   //clone函数创建新的图片
                                                                                                    //image_copy.Save(Path.GetDirectoryName(file) + @"\" + strstr[0] + @"\" + Path.GetFileNameWithoutExtension(file) + i.ToString("_000") + Path.GetExtension(file));

                                    if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(file), strstr[0])))
                                    {
                                        //创建文件夹
                                        try
                                        {
                                            Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(file), strstr[0]));
                                        }
                                        catch (Exception e)
                                        {
                                        }
                                    }

                                    image_copy.Save(Path.Combine(Path.GetDirectoryName(file), strstr[0], Path.GetFileNameWithoutExtension(file) + i.ToString("_000") + Path.GetExtension(file)));
                                    // image_copy.Save(Path.GetDirectoryName(file) + @"\" + strstr[0] + @"\" + Path.GetFileNameWithoutExtension(file) + i.ToString("_000") + Path.GetExtension(file));
                                }
                            }
                        }
                    }
                    pictureBox0.Image = draw.ToBitmap();
                }
            }
        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            if (Cap == null)
            {
                Cap = new VideoCapture(0);
                //Cap.Set(CapProp.FrameWidth, 1920);
                //Cap.Set(CapProp.FrameHeight, 1440);
                Cap.Set(CapProp.FrameWidth, 1920);
                Cap.Set(CapProp.FrameHeight, 1080);
                if (CCDthread == null)
                {
                    isCCDGet = true;
                    CCDthread = new Thread(GetFrame);
                    CCDthread.Start();
                }
            }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            isCCDGet = false;
            if (Cap != null)
            {
                Cap.Stop();
                Cap.Dispose();
                Cap = null;
            }
            if (CCDthread != null)
            {
                if (CCDthread.IsAlive)
                {
                    Thread.Sleep(100);
                    CCDthread.Abort();
                }

                CCDthread = null;

            }
        }

        private void GetFrame()
        {

            Mat img = new Mat();
            DateTime tt = DateTime.Now;
            Bitmap img2 = new Bitmap(1920, 1080);
            int FPS = 0;
            try
            {

             
                while (isCCDGet)
                {
                    img = new Mat();
                    Cap.Read(img);
                    img2 = new Bitmap(img.Width, img.Height);
                    //img = Cap.QueryFrame(); // 去query該畫面
                    // pictureBox0.Image = frame.ToBitmap(); // 把畫面轉換成bitmap型態，在餵給pictureBox元件
                    img2 = img.Clone().ToBitmap();
                    if (picCCDLive.InvokeRequired)
                    {
                        picCCDLive.Invoke(new EventHandler(delegate
                       {
                           if (img2 != null)
                           {
                               picCCDLive.Image = img2;
                           }
                       }));
                    }
                    else
                    {
                        if (img2 != null)
                        {
                            picCCDLive.Image = img2;
                        }
                    }
                    FPS++;

                    //Application.DoEvents();
                    img.Dispose();
                    if (DateTime.Now.Subtract(tt).TotalMilliseconds > 1000)
                    {
                        //txtFPS.BeginInvoke(new Action(() =>
                        //{
                        //    txtFPS.Text = FPS.ToString();
                        //}));
                        tt = DateTime.Now;
                        GC.Collect();
                        FPS = 0;

                    }
                    Thread.Sleep(30);
                }
            }
            catch (Exception)
            {

            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            Color C = ((Button)sender).BackColor;
            GC.Collect();
            try
            {
                ((Button)sender).Enabled = false;
                ((Button)sender).BackColor = Color.Green;
                Application.DoEvents();

                if (txt_Path.Text != "" && Directory.Exists(txt_Path.Text))
                {
                    TT1 = Task.Run(() =>
                    {
                        CutTxt_ALL_Fun(txt_Path.Text);
                    });

                    while (TT1.IsCompleted == false)
                    {
                        Application.DoEvents();
                        Thread.Sleep(1);
                    }
                    TT1 = null;
                }
            }
            catch (Exception)
            {

            }
            ((Button)sender).Enabled = true;
            ((Button)sender).BackColor = C;

        }

        private void txt_Path_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CHKbrowser(((TextBox)(sender)).Text);
            }
        }

        private void CutTxt_ALL_Fun(string _strPath)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    CutTxt_ALL_Fun(_strPath);
                }));
                return;
            }

            // Local iconic variables 
            string[] files = Directory.GetFiles(_strPath);
            foreach (var file in files)
            {
                try
                {
                    if (Path.GetExtension(file).ToUpper() == ".JPG")
                    {
                        CutTxt_Fun(file);
                    }
                }
                catch (Exception)
                {

                }
                Application.DoEvents();
            }
            


        


        }


        public static List<float[]> ArrayTo2DList(Array array)
        {
            System.Collections.IEnumerator enumerator = array.GetEnumerator();
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            List<float[]> list = new List<float[]>();
            List<float> temp = new List<float>();

            for (int i = 0; i < rows; i++)
            {
                temp.Clear();
                for (int j = 0; j < cols; j++)
                {
                    temp.Add(float.Parse(array.GetValue(i, j).ToString()));
                }
                list.Add(temp.ToArray());
            }

            return list;
        }

        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            labThreshold.Text = ((System.Windows.Forms.TrackBar)sender).Value.ToString();

            try
            {
                // ErodeDilate();
            }
            catch (Exception)
            {
            }

        }

        private void trackBarThreshold_KeyUp(object sender, KeyEventArgs e)
        {
            labThreshold.Text = ((System.Windows.Forms.TrackBar)sender).Value.ToString();

            try
            {
                MorphologyErodeDilate();
            }
            catch (Exception)
            {
            }
        }

        private void trackBarThreshold_MouseUp(object sender, MouseEventArgs e)
        {
            labThreshold.Text = ((System.Windows.Forms.TrackBar)sender).Value.ToString();

            try
            {
                MorphologyErodeDilate();
            }
            catch (Exception)
            {
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //string folderPath = "img";
            //CHKbrowser(folderPath);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Cap = new VideoCapture(0);
            //Cap.Set(CapProp.FrameWidth, 1920);
            //Cap.Set(CapProp.FrameHeight, 1440);
            Cap.Set(CapProp.FrameWidth, 1920);
            Cap.Set(CapProp.FrameHeight, 1080);
            Mat img = new Mat();
            DateTime tt = DateTime.Now;
            Bitmap img2 = new Bitmap(1920, 1080);


            img = new Mat();
            Cap.Read(img);

            // Image<Bgr, Byte> frame = Cap.QueryFrame(); // 去query該畫面
           // pictureBox1.Image = frame.ToBitmap(); // 把畫面轉換成bitmap型態，在餵給pictureBox元件


            picCCDLive.Image = img.Clone().ToBitmap();
            //img = Cap.QueryFrame(); // 去query該畫面
            // pictureBox0.Image = frame.ToBitmap(); // 把畫面轉換成bitmap型態，在餵給pictureBox元件
            //picCCDLive.BeginInvoke(new EventHandler(delegate
            //{
            //    img2 = img.Clone().ToBitmap();
            //    if (img2 != null)
            //    {
            //        picCCDLive.Image = img.Clone().ToBitmap();
            //    }
            //}));

            img.Dispose();
            Cap.Dispose();

        }

        private void btnAutoFindShow_Click(object sender, EventArgs e)
        {
            int index = 0;
            index = ((ListBox)listB_Path).SelectedIndex;
            if (index < 0)
            {
                if (listB_Path.Items.Count > 0)
                {
                    listB_Path.SelectedIndex = 0;
                    index = 0;
                }
                else
                {
                    return;
                }
            }
            try
            {
                //讀取TXT 畫出咖啡豆的位置

                VectorOfVectorOfPoint tmpVectorOfVectorOfPoint = new VectorOfVectorOfPoint();
                string file = ListPath[index];
                string strPathTxt;
                strPathTxt = file.Replace(Path.GetExtension(file), ".txt");
                string[] strstr;
                List<String> tmpList = new List<string>();


                double ratio_Center_W, DouCenterW;
                double ratio_Center_H, DouCenterH;
                double ratio_Length_W, Label_W;
                double ratio_Length_H, Label_H;
                double xmin, ymin, xmax, ymax;

                if (File.Exists(strPathTxt) && File.Exists(file)) 
                {
                    Image<Bgr, Byte> draw = new Image<Bgr, Byte>(file);
                    using (StreamReader SR = new StreamReader(strPathTxt))
                    {
                        while (SR.Peek() >= 0)
                        {
                            tmpList.Add(SR.ReadLine());
                        }

                        for (int i = 0; i < tmpList.Count; i++)
                        {
                            strstr = tmpList[i].Split(' ');
                            if (strstr.Length == 5)
                            {
                                if (double.TryParse(strstr[1], out ratio_Center_W) && double.TryParse(strstr[2], out ratio_Center_H) &&
                                    double.TryParse(strstr[3], out ratio_Length_W) && double.TryParse(strstr[4], out ratio_Length_H))
                                {
                                    DouCenterW = ratio_Center_W * draw.Width;
                                    DouCenterH = ratio_Center_H * draw.Height;
                                    Label_W = ratio_Length_W * draw.Width;
                                    Label_H = ratio_Length_H * draw.Height;
                                    xmax = DouCenterW + Label_W / 2;
                                    xmin = DouCenterW - Label_W / 2;
                                    ymax = DouCenterH + Label_H / 2;
                                    ymin = DouCenterH - Label_H / 2;
                                    Rectangle BoundingBox;
                                    BoundingBox = new Rectangle((int)Math.Round(xmin, 0), (int)Math.Round(ymin, 0), (int)Math.Round(Label_W, 0), (int)Math.Round(Label_H, 0));
                                    CvInvoke.Rectangle(draw, BoundingBox, new MCvScalar(255, 0, 255, 255), 3);

                                }
                            }
                        }
                        pictureBox0.Image = draw.ToBitmap();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Explorer.exe", $"/e, {txt_Path.Text}");
            }
            catch (Exception)
            {

            }
        }


        private void listB_Path_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnMorphology.Visible)
            {
                btnMorphology_Click(btnMorphology,null);
            }
            if (btnAutoFindShow.Visible)
            {
                btnAutoFindShow_Click(btnAutoFindShow,null);
            }


        }

    }







}

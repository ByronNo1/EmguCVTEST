
namespace WinFormsApp3
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.btnMorphology = new System.Windows.Forms.Button();
            this.btn_browser = new System.Windows.Forms.Button();
            this.txt_Path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.listB_Path = new System.Windows.Forms.ListBox();
            this.pictDilation = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPTEST = new System.Windows.Forms.TabPage();
            this.labOriginal = new System.Windows.Forms.Label();
            this.labThreshold = new System.Windows.Forms.Label();
            this.picOrigin = new System.Windows.Forms.PictureBox();
            this.picBinary = new System.Windows.Forms.PictureBox();
            this.picErosion = new System.Windows.Forms.PictureBox();
            this.trackBarThreshold = new System.Windows.Forms.TrackBar();
            this.tabPAutoFind = new System.Windows.Forms.TabPage();
            this.btnOpen = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnAutoFindShow = new System.Windows.Forms.Button();
            this.pictureBox0 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTOOL = new System.Windows.Forms.TabPage();
            this.tabPageCCD = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFPS = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.picCCDLive = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictDilation)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPTEST.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOrigin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBinary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picErosion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).BeginInit();
            this.tabPAutoFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox0)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageTOOL.SuspendLayout();
            this.tabPageCCD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCCDLive)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2510, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 67);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(2374, 449);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(274, 30);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(2374, 501);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(274, 30);
            this.numericUpDown2.TabIndex = 2;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // btnMorphology
            // 
            this.btnMorphology.Location = new System.Drawing.Point(1903, 58);
            this.btnMorphology.Name = "btnMorphology";
            this.btnMorphology.Size = new System.Drawing.Size(264, 149);
            this.btnMorphology.TabIndex = 3;
            this.btnMorphology.Text = "形態學";
            this.btnMorphology.UseVisualStyleBackColor = true;
            this.btnMorphology.Click += new System.EventHandler(this.btnMorphology_Click);
            // 
            // btn_browser
            // 
            this.btn_browser.Location = new System.Drawing.Point(3, 13);
            this.btn_browser.Name = "btn_browser";
            this.btn_browser.Size = new System.Drawing.Size(264, 58);
            this.btn_browser.TabIndex = 4;
            this.btn_browser.Text = "browser ";
            this.btn_browser.UseVisualStyleBackColor = true;
            this.btn_browser.Click += new System.EventHandler(this.btn_browser_Click);
            // 
            // txt_Path
            // 
            this.txt_Path.Location = new System.Drawing.Point(284, 28);
            this.txt_Path.Name = "txt_Path";
            this.txt_Path.Size = new System.Drawing.Size(1403, 30);
            this.txt_Path.TabIndex = 5;
            this.txt_Path.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Path_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1824, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "label= 0:NG 1:OK";
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(1815, 54);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(203, 30);
            this.txtLabel.TabIndex = 5;
            this.txtLabel.Text = "0";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(1980, 6);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(190, 83);
            this.btn_Start.TabIndex = 7;
            this.btn_Start.Text = "Start Auto Find";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // listB_Path
            // 
            this.listB_Path.FormattingEnabled = true;
            this.listB_Path.ItemHeight = 23;
            this.listB_Path.Location = new System.Drawing.Point(2232, 127);
            this.listB_Path.Name = "listB_Path";
            this.listB_Path.Size = new System.Drawing.Size(458, 1177);
            this.listB_Path.TabIndex = 8;
            this.listB_Path.SelectedIndexChanged += new System.EventHandler(this.listB_Path_SelectedIndexChanged);
            // 
            // pictDilation
            // 
            this.pictDilation.Location = new System.Drawing.Point(999, 632);
            this.pictDilation.Name = "pictDilation";
            this.pictDilation.Size = new System.Drawing.Size(889, 505);
            this.pictDilation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictDilation.TabIndex = 10;
            this.pictDilation.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(392, 593);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 36);
            this.label2.TabIndex = 6;
            this.label2.Text = "二值化圖";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(1417, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 36);
            this.label3.TabIndex = 6;
            this.label3.Text = "腐蝕";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(1417, 593);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 36);
            this.label4.TabIndex = 6;
            this.label4.Text = "膨脹";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl2);
            this.panel1.Controls.Add(this.btn_browser);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.listB_Path);
            this.panel1.Controls.Add(this.txt_Path);
            this.panel1.Controls.Add(this.txtLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.numericUpDown2);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2807, 1345);
            this.panel1.TabIndex = 12;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPTEST);
            this.tabControl2.Controls.Add(this.tabPAutoFind);
            this.tabControl2.Location = new System.Drawing.Point(3, 90);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(2194, 1217);
            this.tabControl2.TabIndex = 13;
            // 
            // tabPTEST
            // 
            this.tabPTEST.Controls.Add(this.label2);
            this.tabPTEST.Controls.Add(this.labOriginal);
            this.tabPTEST.Controls.Add(this.labThreshold);
            this.tabPTEST.Controls.Add(this.label3);
            this.tabPTEST.Controls.Add(this.label4);
            this.tabPTEST.Controls.Add(this.picOrigin);
            this.tabPTEST.Controls.Add(this.picBinary);
            this.tabPTEST.Controls.Add(this.picErosion);
            this.tabPTEST.Controls.Add(this.btnMorphology);
            this.tabPTEST.Controls.Add(this.pictDilation);
            this.tabPTEST.Controls.Add(this.trackBarThreshold);
            this.tabPTEST.Location = new System.Drawing.Point(4, 32);
            this.tabPTEST.Name = "tabPTEST";
            this.tabPTEST.Padding = new System.Windows.Forms.Padding(3);
            this.tabPTEST.Size = new System.Drawing.Size(2186, 1181);
            this.tabPTEST.TabIndex = 1;
            this.tabPTEST.Text = "基礎形態學";
            this.tabPTEST.UseVisualStyleBackColor = true;
            // 
            // labOriginal
            // 
            this.labOriginal.AutoSize = true;
            this.labOriginal.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labOriginal.Location = new System.Drawing.Point(405, 19);
            this.labOriginal.Name = "labOriginal";
            this.labOriginal.Size = new System.Drawing.Size(71, 36);
            this.labOriginal.TabIndex = 6;
            this.labOriginal.Text = "原圖";
            // 
            // labThreshold
            // 
            this.labThreshold.AutoSize = true;
            this.labThreshold.Font = new System.Drawing.Font("Microsoft JhengHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labThreshold.Location = new System.Drawing.Point(2096, 272);
            this.labThreshold.Name = "labThreshold";
            this.labThreshold.Size = new System.Drawing.Size(63, 36);
            this.labThreshold.TabIndex = 6;
            this.labThreshold.Text = "125";
            // 
            // picOrigin
            // 
            this.picOrigin.Location = new System.Drawing.Point(15, 58);
            this.picOrigin.Name = "picOrigin";
            this.picOrigin.Size = new System.Drawing.Size(889, 505);
            this.picOrigin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOrigin.TabIndex = 10;
            this.picOrigin.TabStop = false;
            // 
            // picBinary
            // 
            this.picBinary.Location = new System.Drawing.Point(15, 632);
            this.picBinary.Name = "picBinary";
            this.picBinary.Size = new System.Drawing.Size(889, 505);
            this.picBinary.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBinary.TabIndex = 10;
            this.picBinary.TabStop = false;
            // 
            // picErosion
            // 
            this.picErosion.Location = new System.Drawing.Point(999, 58);
            this.picErosion.Name = "picErosion";
            this.picErosion.Size = new System.Drawing.Size(889, 505);
            this.picErosion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picErosion.TabIndex = 10;
            this.picErosion.TabStop = false;
            // 
            // trackBarThreshold
            // 
            this.trackBarThreshold.Location = new System.Drawing.Point(1903, 230);
            this.trackBarThreshold.Maximum = 255;
            this.trackBarThreshold.Minimum = 1;
            this.trackBarThreshold.Name = "trackBarThreshold";
            this.trackBarThreshold.Size = new System.Drawing.Size(264, 69);
            this.trackBarThreshold.TabIndex = 11;
            this.trackBarThreshold.Value = 125;
            this.trackBarThreshold.Scroll += new System.EventHandler(this.trackBarThreshold_Scroll);
            this.trackBarThreshold.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trackBarThreshold_KeyUp);
            this.trackBarThreshold.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarThreshold_MouseUp);
            // 
            // tabPAutoFind
            // 
            this.tabPAutoFind.Controls.Add(this.btnOpen);
            this.tabPAutoFind.Controls.Add(this.button6);
            this.tabPAutoFind.Controls.Add(this.btnAutoFindShow);
            this.tabPAutoFind.Controls.Add(this.pictureBox0);
            this.tabPAutoFind.Controls.Add(this.btn_Start);
            this.tabPAutoFind.Location = new System.Drawing.Point(4, 32);
            this.tabPAutoFind.Name = "tabPAutoFind";
            this.tabPAutoFind.Padding = new System.Windows.Forms.Padding(3);
            this.tabPAutoFind.Size = new System.Drawing.Size(2186, 1181);
            this.tabPAutoFind.TabIndex = 0;
            this.tabPAutoFind.Text = "自動抓取咖啡";
            this.tabPAutoFind.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(1980, 168);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(190, 67);
            this.btnOpen.TabIndex = 14;
            this.btnOpen.Text = "Open Explorer";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1980, 95);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(190, 67);
            this.button6.TabIndex = 14;
            this.button6.Text = "Cutting";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnAutoFindShow
            // 
            this.btnAutoFindShow.Location = new System.Drawing.Point(1980, 306);
            this.btnAutoFindShow.Name = "btnAutoFindShow";
            this.btnAutoFindShow.Size = new System.Drawing.Size(190, 69);
            this.btnAutoFindShow.TabIndex = 13;
            this.btnAutoFindShow.Text = "顯示個別結果";
            this.btnAutoFindShow.UseVisualStyleBackColor = true;
            this.btnAutoFindShow.Click += new System.EventHandler(this.btnAutoFindShow_Click);
            // 
            // pictureBox0
            // 
            this.pictureBox0.Location = new System.Drawing.Point(6, 6);
            this.pictureBox0.Name = "pictureBox0";
            this.pictureBox0.Size = new System.Drawing.Size(1955, 1148);
            this.pictureBox0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox0.TabIndex = 12;
            this.pictureBox0.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPageTOOL);
            this.tabControl1.Controls.Add(this.tabPageCCD);
            this.tabControl1.ItemSize = new System.Drawing.Size(200, 50);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(2869, 1409);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPageTOOL
            // 
            this.tabPageTOOL.Controls.Add(this.panel1);
            this.tabPageTOOL.Location = new System.Drawing.Point(4, 54);
            this.tabPageTOOL.Name = "tabPageTOOL";
            this.tabPageTOOL.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTOOL.Size = new System.Drawing.Size(2861, 1351);
            this.tabPageTOOL.TabIndex = 1;
            this.tabPageTOOL.Text = "Auto Label";
            this.tabPageTOOL.UseVisualStyleBackColor = true;
            // 
            // tabPageCCD
            // 
            this.tabPageCCD.Controls.Add(this.button2);
            this.tabPageCCD.Controls.Add(this.txtFPS);
            this.tabPageCCD.Controls.Add(this.button5);
            this.tabPageCCD.Controls.Add(this.button4);
            this.tabPageCCD.Controls.Add(this.picCCDLive);
            this.tabPageCCD.Location = new System.Drawing.Point(4, 54);
            this.tabPageCCD.Name = "tabPageCCD";
            this.tabPageCCD.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCCD.Size = new System.Drawing.Size(2861, 1351);
            this.tabPageCCD.TabIndex = 0;
            this.tabPageCCD.Text = "CCD LIVE";
            this.tabPageCCD.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1888, 449);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(379, 178);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // txtFPS
            // 
            this.txtFPS.Location = new System.Drawing.Point(1909, 29);
            this.txtFPS.Name = "txtFPS";
            this.txtFPS.Size = new System.Drawing.Size(208, 30);
            this.txtFPS.TabIndex = 3;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1897, 272);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(262, 113);
            this.button5.TabIndex = 2;
            this.button5.Text = "CCD Disconnect";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1897, 103);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(262, 135);
            this.button4.TabIndex = 1;
            this.button4.Text = "CCD Connect";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // picCCDLive
            // 
            this.picCCDLive.Location = new System.Drawing.Point(-4, 3);
            this.picCCDLive.Name = "picCCDLive";
            this.picCCDLive.Size = new System.Drawing.Size(1862, 1348);
            this.picCCDLive.TabIndex = 0;
            this.picCCDLive.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2869, 1463);
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictDilation)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPTEST.ResumeLayout(false);
            this.tabPTEST.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOrigin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBinary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picErosion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).EndInit();
            this.tabPAutoFind.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox0)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageTOOL.ResumeLayout(false);
            this.tabPageCCD.ResumeLayout(false);
            this.tabPageCCD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCCDLive)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button btnMorphology;
        private System.Windows.Forms.Button btn_browser;
        private System.Windows.Forms.TextBox txt_Path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.ListBox listB_Path;
        private System.Windows.Forms.PictureBox pictDilation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageCCD;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox picCCDLive;
        private System.Windows.Forms.TabPage tabPageTOOL;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtFPS;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPAutoFind;
        private System.Windows.Forms.PictureBox pictureBox0;
        private System.Windows.Forms.TabPage tabPTEST;
        private System.Windows.Forms.PictureBox picBinary;
        private System.Windows.Forms.PictureBox picErosion;
        private System.Windows.Forms.Label labOriginal;
        private System.Windows.Forms.PictureBox picOrigin;
        private System.Windows.Forms.TrackBar trackBarThreshold;
        private System.Windows.Forms.Label labThreshold;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnAutoFindShow;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnOpen;
    }
}


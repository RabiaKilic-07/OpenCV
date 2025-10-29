namespace OpenCV
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            btnBaslat = new Button();
            lblDurum = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            imageBox1 = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)imageBox1).BeginInit();
            SuspendLayout();
            // 
            // btnBaslat
            // 
            btnBaslat.Location = new Point(509, 70);
            btnBaslat.Name = "btnBaslat";
            btnBaslat.Size = new Size(156, 86);
            btnBaslat.TabIndex = 0;
            btnBaslat.Text = "Kamerayı Başlat/Durdur";
            btnBaslat.UseVisualStyleBackColor = true;
            btnBaslat.Click += btnBaslat_Click;
            // 
            // lblDurum
            // 
            lblDurum.AutoSize = true;
            lblDurum.Location = new Point(530, 228);
            lblDurum.Name = "lblDurum";
            lblDurum.Size = new Size(98, 20);
            lblDurum.TabIndex = 1;
            lblDurum.Text = "Hazırlanıyor...";
            // 
            // timer1
            // 
            timer1.Interval = 30;
            // 
            // imageBox1
            // 
            imageBox1.Location = new Point(93, 88);
            imageBox1.Name = "imageBox1";
            imageBox1.Size = new Size(220, 215);
            imageBox1.TabIndex = 2;
            imageBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(imageBox1);
            Controls.Add(lblDurum);
            Controls.Add(btnBaslat);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)imageBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBaslat;
        private Label lblDurum;
        private System.Windows.Forms.Timer timer1;
        private Emgu.CV.UI.ImageBox imageBox1;
    }
}

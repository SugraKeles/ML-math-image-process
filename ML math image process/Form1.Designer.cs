namespace ML_math_image_process
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.PictureImage = new System.Windows.Forms.PictureBox();
            this.Clean_button1 = new System.Windows.Forms.Button();
            this.Tahmin_button1 = new System.Windows.Forms.Button();
            this.Sonuc_label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureImage)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureImage
            // 
            this.PictureImage.BackColor = System.Drawing.Color.White;
            this.PictureImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureImage.Location = new System.Drawing.Point(203, 12);
            this.PictureImage.Name = "PictureImage";
            this.PictureImage.Size = new System.Drawing.Size(300, 300);
            this.PictureImage.TabIndex = 0;
            this.PictureImage.TabStop = false;
            this.PictureImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDown);
            this.PictureImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseMove);
            this.PictureImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
            // 
            // Clean_button1
            // 
            this.Clean_button1.Location = new System.Drawing.Point(408, 333);
            this.Clean_button1.Name = "Clean_button1";
            this.Clean_button1.Size = new System.Drawing.Size(95, 39);
            this.Clean_button1.TabIndex = 1;
            this.Clean_button1.Text = "TEMİZLE";
            this.Clean_button1.UseVisualStyleBackColor = true;
            this.Clean_button1.Click += new System.EventHandler(this.Clean_button1_Click);
            // 
            // Tahmin_button1
            // 
            this.Tahmin_button1.Location = new System.Drawing.Point(203, 333);
            this.Tahmin_button1.Name = "Tahmin_button1";
            this.Tahmin_button1.Size = new System.Drawing.Size(100, 39);
            this.Tahmin_button1.TabIndex = 2;
            this.Tahmin_button1.Text = "Tahmin Et";
            this.Tahmin_button1.UseVisualStyleBackColor = true;
            this.Tahmin_button1.Click += new System.EventHandler(this.Tahmin_button1_Click);
            // 
            // Sonuc_label1
            // 
            this.Sonuc_label1.AutoSize = true;
            this.Sonuc_label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Sonuc_label1.Location = new System.Drawing.Point(83, 385);
            this.Sonuc_label1.Name = "Sonuc_label1";
            this.Sonuc_label1.Size = new System.Drawing.Size(157, 46);
            this.Sonuc_label1.TabIndex = 3;
            this.Sonuc_label1.Text = "Sonuç: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 450);
            this.Controls.Add(this.Sonuc_label1);
            this.Controls.Add(this.Tahmin_button1);
            this.Controls.Add(this.Clean_button1);
            this.Controls.Add(this.PictureImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PictureImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureImage;
        private System.Windows.Forms.Button Clean_button1;
        private System.Windows.Forms.Button Tahmin_button1;
        private System.Windows.Forms.Label Sonuc_label1;
    }
}


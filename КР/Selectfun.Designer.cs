
namespace КР
{
    partial class Selectfun
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButImage = new System.Windows.Forms.Button();
            this.ButPdf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButImage
            // 
            this.ButImage.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButImage.Location = new System.Drawing.Point(12, 12);
            this.ButImage.Name = "ButImage";
            this.ButImage.Size = new System.Drawing.Size(310, 33);
            this.ButImage.TabIndex = 0;
            this.ButImage.Text = "Стеганография в изображениях";
            this.ButImage.UseVisualStyleBackColor = true;
            this.ButImage.Click += new System.EventHandler(this.ButImage_Click);
            // 
            // ButPdf
            // 
            this.ButPdf.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButPdf.Location = new System.Drawing.Point(12, 66);
            this.ButPdf.Name = "ButPdf";
            this.ButPdf.Size = new System.Drawing.Size(310, 33);
            this.ButPdf.TabIndex = 1;
            this.ButPdf.Text = "Стеганография в pdf";
            this.ButPdf.UseVisualStyleBackColor = true;
            this.ButPdf.Click += new System.EventHandler(this.ButPdf_Click);
            // 
            // Selectfun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(334, 111);
            this.Controls.Add(this.ButPdf);
            this.Controls.Add(this.ButImage);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Selectfun";
            this.Text = "Режим работы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Selectfun_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButImage;
        private System.Windows.Forms.Button ButPdf;
    }
}

namespace КР
{
    partial class KeyForm
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
            this.LabKey = new System.Windows.Forms.Label();
            this.TBKey = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.LabConf = new System.Windows.Forms.Label();
            this.TBConf = new System.Windows.Forms.TextBox();
            this.PanelKey = new System.Windows.Forms.Panel();
            this.PanelConf = new System.Windows.Forms.Panel();
            this.PanelKey.SuspendLayout();
            this.PanelConf.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabKey
            // 
            this.LabKey.AutoSize = true;
            this.LabKey.Location = new System.Drawing.Point(4, 8);
            this.LabKey.Name = "LabKey";
            this.LabKey.Size = new System.Drawing.Size(55, 21);
            this.LabKey.TabIndex = 0;
            this.LabKey.Text = "Ключ";
            // 
            // TBKey
            // 
            this.TBKey.Location = new System.Drawing.Point(165, 5);
            this.TBKey.Name = "TBKey";
            this.TBKey.PasswordChar = '*';
            this.TBKey.Size = new System.Drawing.Size(201, 29);
            this.TBKey.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(145, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "ОК";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LabConf
            // 
            this.LabConf.AutoSize = true;
            this.LabConf.Location = new System.Drawing.Point(4, 5);
            this.LabConf.Name = "LabConf";
            this.LabConf.Size = new System.Drawing.Size(136, 21);
            this.LabConf.TabIndex = 3;
            this.LabConf.Text = "Подтверждение";
            // 
            // TBConf
            // 
            this.TBConf.Location = new System.Drawing.Point(165, 2);
            this.TBConf.Name = "TBConf";
            this.TBConf.PasswordChar = '*';
            this.TBConf.Size = new System.Drawing.Size(201, 29);
            this.TBConf.TabIndex = 4;
            // 
            // PanelKey
            // 
            this.PanelKey.Controls.Add(this.TBKey);
            this.PanelKey.Controls.Add(this.LabKey);
            this.PanelKey.Location = new System.Drawing.Point(6, 2);
            this.PanelKey.Name = "PanelKey";
            this.PanelKey.Size = new System.Drawing.Size(375, 34);
            this.PanelKey.TabIndex = 5;
            // 
            // PanelConf
            // 
            this.PanelConf.Controls.Add(this.TBConf);
            this.PanelConf.Controls.Add(this.LabConf);
            this.PanelConf.Location = new System.Drawing.Point(6, 40);
            this.PanelConf.Name = "PanelConf";
            this.PanelConf.Size = new System.Drawing.Size(375, 35);
            this.PanelConf.TabIndex = 6;
            // 
            // KeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.PanelConf);
            this.Controls.Add(this.PanelKey);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "KeyForm";
            this.Text = "Ввод ключа";
            this.Shown += new System.EventHandler(this.KeyForm_Shown);
            this.PanelKey.ResumeLayout(false);
            this.PanelKey.PerformLayout();
            this.PanelConf.ResumeLayout(false);
            this.PanelConf.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabKey;
        private System.Windows.Forms.TextBox TBKey;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LabConf;
        private System.Windows.Forms.TextBox TBConf;
        private System.Windows.Forms.Panel PanelKey;
        private System.Windows.Forms.Panel PanelConf;
    }
}
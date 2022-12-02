
namespace КР
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelNameFile = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.RBExtPict = new System.Windows.Forms.RadioButton();
            this.MainGB = new System.Windows.Forms.GroupBox();
            this.ButProc = new System.Windows.Forms.Button();
            this.CBEnc = new System.Windows.Forms.CheckBox();
            this.TBTypeText = new System.Windows.Forms.TextBox();
            this.RBInfText = new System.Windows.Forms.RadioButton();
            this.RBInfFile = new System.Windows.Forms.RadioButton();
            this.RBStegPict = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.MainGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(584, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьФайлToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 19);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выбратьФайлToolStripMenuItem
            // 
            this.выбратьФайлToolStripMenuItem.Name = "выбратьФайлToolStripMenuItem";
            this.выбратьФайлToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.выбратьФайлToolStripMenuItem.Text = "Выбрать файл";
            this.выбратьФайлToolStripMenuItem.Click += new System.EventHandler(this.выбратьФайлToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 19);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // LabelNameFile
            // 
            this.LabelNameFile.BackColor = System.Drawing.Color.Transparent;
            this.LabelNameFile.Location = new System.Drawing.Point(0, 30);
            this.LabelNameFile.Name = "LabelNameFile";
            this.LabelNameFile.Size = new System.Drawing.Size(584, 21);
            this.LabelNameFile.TabIndex = 1;
            this.LabelNameFile.Text = "Файл не загружен";
            this.LabelNameFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.RBExtPict);
            this.MainPanel.Controls.Add(this.MainGB);
            this.MainPanel.Controls.Add(this.RBStegPict);
            this.MainPanel.Location = new System.Drawing.Point(15, 60);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(555, 290);
            this.MainPanel.TabIndex = 4;
            // 
            // RBExtPict
            // 
            this.RBExtPict.AutoSize = true;
            this.RBExtPict.Location = new System.Drawing.Point(15, 40);
            this.RBExtPict.Name = "RBExtPict";
            this.RBExtPict.Size = new System.Drawing.Size(340, 25);
            this.RBExtPict.TabIndex = 3;
            this.RBExtPict.TabStop = true;
            this.RBExtPict.Text = "Извлечь информацию из изображений";
            this.RBExtPict.UseVisualStyleBackColor = true;
            this.RBExtPict.CheckedChanged += new System.EventHandler(this.RBExtPict_CheckedChanged);
            // 
            // MainGB
            // 
            this.MainGB.Controls.Add(this.ButProc);
            this.MainGB.Controls.Add(this.CBEnc);
            this.MainGB.Controls.Add(this.TBTypeText);
            this.MainGB.Controls.Add(this.RBInfText);
            this.MainGB.Controls.Add(this.RBInfFile);
            this.MainGB.Location = new System.Drawing.Point(15, 65);
            this.MainGB.Name = "MainGB";
            this.MainGB.Size = new System.Drawing.Size(525, 220);
            this.MainGB.TabIndex = 2;
            this.MainGB.TabStop = false;
            this.MainGB.Text = "Ввод скрываемой информации";
            // 
            // ButProc
            // 
            this.ButProc.Location = new System.Drawing.Point(80, 182);
            this.ButProc.Name = "ButProc";
            this.ButProc.Size = new System.Drawing.Size(350, 30);
            this.ButProc.TabIndex = 4;
            this.ButProc.Text = "Произвести сокрытие информации";
            this.ButProc.UseVisualStyleBackColor = true;
            this.ButProc.Click += new System.EventHandler(this.ButProc_Click);
            // 
            // CBEnc
            // 
            this.CBEnc.AutoSize = true;
            this.CBEnc.Location = new System.Drawing.Point(296, 25);
            this.CBEnc.Name = "CBEnc";
            this.CBEnc.Size = new System.Drawing.Size(134, 25);
            this.CBEnc.TabIndex = 3;
            this.CBEnc.Text = "Шифрование";
            this.CBEnc.UseVisualStyleBackColor = true;
            this.CBEnc.CheckedChanged += new System.EventHandler(this.CBEnc_CheckedChanged);
            // 
            // TBTypeText
            // 
            this.TBTypeText.Location = new System.Drawing.Point(10, 87);
            this.TBTypeText.Multiline = true;
            this.TBTypeText.Name = "TBTypeText";
            this.TBTypeText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBTypeText.Size = new System.Drawing.Size(505, 89);
            this.TBTypeText.TabIndex = 2;
            // 
            // RBInfText
            // 
            this.RBInfText.AutoSize = true;
            this.RBInfText.Location = new System.Drawing.Point(10, 56);
            this.RBInfText.Name = "RBInfText";
            this.RBInfText.Size = new System.Drawing.Size(166, 25);
            this.RBInfText.TabIndex = 1;
            this.RBInfText.TabStop = true;
            this.RBInfText.Text = "В текстовое поле";
            this.RBInfText.UseVisualStyleBackColor = true;
            this.RBInfText.CheckedChanged += new System.EventHandler(this.RBInfText_CheckedChanged);
            // 
            // RBInfFile
            // 
            this.RBInfFile.AutoSize = true;
            this.RBInfFile.Location = new System.Drawing.Point(10, 25);
            this.RBInfFile.Name = "RBInfFile";
            this.RBInfFile.Size = new System.Drawing.Size(84, 25);
            this.RBInfFile.TabIndex = 0;
            this.RBInfFile.TabStop = true;
            this.RBInfFile.Text = "В файл";
            this.RBInfFile.UseVisualStyleBackColor = true;
            this.RBInfFile.CheckedChanged += new System.EventHandler(this.RBInfFile_CheckedChanged);
            // 
            // RBStegPict
            // 
            this.RBStegPict.AutoSize = true;
            this.RBStegPict.Location = new System.Drawing.Point(15, 15);
            this.RBStegPict.Name = "RBStegPict";
            this.RBStegPict.Size = new System.Drawing.Size(373, 25);
            this.RBStegPict.TabIndex = 1;
            this.RBStegPict.TabStop = true;
            this.RBStegPict.Text = "Скрыть информацию внутрь изображений";
            this.RBStegPict.UseVisualStyleBackColor = true;
            this.RBStegPict.CheckedChanged += new System.EventHandler(this.RBStegPict_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::КР.Properties.Resources.Фон;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.LabelNameFile);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form1";
            this.Text = "Курсовая работа";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.MainGB.ResumeLayout(false);
            this.MainGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.Label LabelNameFile;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.RadioButton RBStegPict;
        private System.Windows.Forms.GroupBox MainGB;
        private System.Windows.Forms.RadioButton RBInfText;
        private System.Windows.Forms.RadioButton RBInfFile;
        private System.Windows.Forms.TextBox TBTypeText;
        private System.Windows.Forms.RadioButton RBExtPict;
        private System.Windows.Forms.Button ButProc;
        private System.Windows.Forms.CheckBox CBEnc;
    }
}


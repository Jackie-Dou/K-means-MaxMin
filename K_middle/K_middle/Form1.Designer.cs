
namespace K_middle
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
            this.picbxMain = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picbxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // picbxMain
            // 
            this.picbxMain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.picbxMain.Location = new System.Drawing.Point(-1, -1);
            this.picbxMain.Name = "picbxMain";
            this.picbxMain.Size = new System.Drawing.Size(687, 635);
            this.picbxMain.TabIndex = 0;
            this.picbxMain.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(692, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 55);
            this.button1.TabIndex = 1;
            this.button1.Text = "Сделать хорошо";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 654);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.picbxMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Алгоритм К-средних";
            ((System.ComponentModel.ISupportInitialize)(this.picbxMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picbxMain;
        private System.Windows.Forms.Button button1;
    }
}


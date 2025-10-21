namespace SpeechToTextCsharpProject
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
            this.txtMetin = new System.Windows.Forms.TextBox();
            this.btnDinleBasla = new System.Windows.Forms.Button();
            this.btnDinleDur = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMetin
            // 
            this.txtMetin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtMetin.Location = new System.Drawing.Point(12, 36);
            this.txtMetin.Multiline = true;
            this.txtMetin.Name = "txtMetin";
            this.txtMetin.Size = new System.Drawing.Size(576, 187);
            this.txtMetin.TabIndex = 0;
            // 
            // btnDinleBasla
            // 
            this.btnDinleBasla.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDinleBasla.Location = new System.Drawing.Point(12, 249);
            this.btnDinleBasla.Name = "btnDinleBasla";
            this.btnDinleBasla.Size = new System.Drawing.Size(200, 62);
            this.btnDinleBasla.TabIndex = 1;
            this.btnDinleBasla.Text = "Start Record";
            this.btnDinleBasla.UseVisualStyleBackColor = true;
            this.btnDinleBasla.Click += new System.EventHandler(this.btnDinleBasla_Click);
            // 
            // btnDinleDur
            // 
            this.btnDinleDur.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDinleDur.Location = new System.Drawing.Point(12, 330);
            this.btnDinleDur.Name = "btnDinleDur";
            this.btnDinleDur.Size = new System.Drawing.Size(200, 62);
            this.btnDinleDur.TabIndex = 2;
            this.btnDinleDur.Text = "Stop Record";
            this.btnDinleDur.UseVisualStyleBackColor = true;
            this.btnDinleDur.Click += new System.EventHandler(this.btnDinleDur_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 404);
            this.Controls.Add(this.btnDinleDur);
            this.Controls.Add(this.btnDinleBasla);
            this.Controls.Add(this.txtMetin);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMetin;
        private System.Windows.Forms.Button btnDinleBasla;
        private System.Windows.Forms.Button btnDinleDur;
    }
}


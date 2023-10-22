
namespace PasswordWallet
{
    partial class Settings
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
            this.okBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDirectLoadFile = new System.Windows.Forms.TextBox();
            this.txtEncryptedFile = new System.Windows.Forms.TextBox();
            this.btnLoadDirectFile = new System.Windows.Forms.Button();
            this.btnLoadEncryptedFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(380, 82);
            this.okBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(100, 28);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Direct load file";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enctrypted file";
            // 
            // txtDirectLoadFile
            // 
            this.txtDirectLoadFile.Location = new System.Drawing.Point(123, 7);
            this.txtDirectLoadFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDirectLoadFile.Name = "txtDirectLoadFile";
            this.txtDirectLoadFile.Size = new System.Drawing.Size(304, 22);
            this.txtDirectLoadFile.TabIndex = 3;
            // 
            // txtEncryptedFile
            // 
            this.txtEncryptedFile.Location = new System.Drawing.Point(123, 49);
            this.txtEncryptedFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEncryptedFile.Name = "txtEncryptedFile";
            this.txtEncryptedFile.Size = new System.Drawing.Size(304, 22);
            this.txtEncryptedFile.TabIndex = 4;
            // 
            // btnLoadDirectFile
            // 
            this.btnLoadDirectFile.Location = new System.Drawing.Point(436, 5);
            this.btnLoadDirectFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoadDirectFile.Name = "btnLoadDirectFile";
            this.btnLoadDirectFile.Size = new System.Drawing.Size(44, 28);
            this.btnLoadDirectFile.TabIndex = 5;
            this.btnLoadDirectFile.Text = "...";
            this.btnLoadDirectFile.UseVisualStyleBackColor = true;
            this.btnLoadDirectFile.Click += new System.EventHandler(this.btnLoadDirectFile_Click);
            // 
            // btnLoadEncryptedFile
            // 
            this.btnLoadEncryptedFile.Location = new System.Drawing.Point(436, 47);
            this.btnLoadEncryptedFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoadEncryptedFile.Name = "btnLoadEncryptedFile";
            this.btnLoadEncryptedFile.Size = new System.Drawing.Size(44, 28);
            this.btnLoadEncryptedFile.TabIndex = 6;
            this.btnLoadEncryptedFile.Text = "...";
            this.btnLoadEncryptedFile.UseVisualStyleBackColor = true;
            this.btnLoadEncryptedFile.Click += new System.EventHandler(this.btnLoadEncryptedFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Settings
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(513, 128);
            this.Controls.Add(this.btnLoadEncryptedFile);
            this.Controls.Add(this.btnLoadDirectFile);
            this.Controls.Add(this.txtEncryptedFile);
            this.Controls.Add(this.txtDirectLoadFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okBtn);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDirectLoadFile;
        private System.Windows.Forms.TextBox txtEncryptedFile;
        private System.Windows.Forms.Button btnLoadDirectFile;
        private System.Windows.Forms.Button btnLoadEncryptedFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
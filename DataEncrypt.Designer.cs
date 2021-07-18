namespace PasswordWallet
{
    partial class DataEncrypt
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtEncryptedFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDecryptedFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCertificate = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPickFile = new System.Windows.Forms.Button();
            this.btnPickFile1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Encrytped File";
            // 
            // txtEncryptedFile
            // 
            this.txtEncryptedFile.Location = new System.Drawing.Point(282, 70);
            this.txtEncryptedFile.Name = "txtEncryptedFile";
            this.txtEncryptedFile.Size = new System.Drawing.Size(745, 38);
            this.txtEncryptedFile.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Decrypted File";
            // 
            // txtDecryptedFile
            // 
            this.txtDecryptedFile.Location = new System.Drawing.Point(282, 147);
            this.txtDecryptedFile.Name = "txtDecryptedFile";
            this.txtDecryptedFile.Size = new System.Drawing.Size(745, 38);
            this.txtDecryptedFile.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "Certificate";
            // 
            // txtCertificate
            // 
            this.txtCertificate.Location = new System.Drawing.Point(282, 218);
            this.txtCertificate.Name = "txtCertificate";
            this.txtCertificate.Size = new System.Drawing.Size(745, 38);
            this.txtCertificate.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(983, 279);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(170, 64);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(754, 279);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(170, 64);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnPickFile
            // 
            this.btnPickFile.Location = new System.Drawing.Point(1067, 63);
            this.btnPickFile.Name = "btnPickFile";
            this.btnPickFile.Size = new System.Drawing.Size(84, 53);
            this.btnPickFile.TabIndex = 2;
            this.btnPickFile.Text = "...";
            this.btnPickFile.UseVisualStyleBackColor = true;
            this.btnPickFile.Click += new System.EventHandler(this.BtnPickFile_Click);
            // 
            // btnPickFile1
            // 
            this.btnPickFile1.Location = new System.Drawing.Point(1066, 143);
            this.btnPickFile1.Name = "btnPickFile1";
            this.btnPickFile1.Size = new System.Drawing.Size(84, 53);
            this.btnPickFile1.TabIndex = 5;
            this.btnPickFile1.Text = "...";
            this.btnPickFile1.UseVisualStyleBackColor = true;
            this.btnPickFile1.Click += new System.EventHandler(this.BtnPickFile1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // DataEncrypt
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 378);
            this.Controls.Add(this.btnPickFile1);
            this.Controls.Add(this.btnPickFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtCertificate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDecryptedFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEncryptedFile);
            this.Controls.Add(this.label1);
            this.Name = "DataEncrypt";
            this.Text = "Load Encrypted Data File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEncryptedFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDecryptedFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCertificate;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPickFile;
        private System.Windows.Forms.Button btnPickFile1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
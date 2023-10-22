namespace PasswordWallet
{
    partial class Wallet
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wallet));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnAddEntry = new System.Windows.Forms.ToolStripButton();
            this.BtnRemoveEntry = new System.Windows.Forms.ToolStripButton();
            this.BtnEditEntry = new System.Windows.Forms.ToolStripButton();
            this.BtnSaveChanges = new System.Windows.Forms.ToolStripButton();
            this.BtnMagnify = new System.Windows.Forms.ToolStripButton();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.passwordDataSource)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnAddEntry,
            this.BtnRemoveEntry,
            this.BtnEditEntry,
            this.BtnSaveChanges,
            this.BtnMagnify,
            this.btnSettings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(893, 47);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnAddEntry
            // 
            this.BtnAddEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnAddEntry.Image = ((System.Drawing.Image)(resources.GetObject("BtnAddEntry.Image")));
            this.BtnAddEntry.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnAddEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnAddEntry.Name = "BtnAddEntry";
            this.BtnAddEntry.Size = new System.Drawing.Size(36, 44);
            this.BtnAddEntry.Text = "Add Entry";
            this.BtnAddEntry.ToolTipText = "Add Account (Ctrl-A)";
            this.BtnAddEntry.Click += new System.EventHandler(this.BtnAddEntry_Click);
            // 
            // BtnRemoveEntry
            // 
            this.BtnRemoveEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnRemoveEntry.Image = ((System.Drawing.Image)(resources.GetObject("BtnRemoveEntry.Image")));
            this.BtnRemoveEntry.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnRemoveEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnRemoveEntry.Name = "BtnRemoveEntry";
            this.BtnRemoveEntry.Size = new System.Drawing.Size(36, 44);
            this.BtnRemoveEntry.Text = "Remove Account";
            this.BtnRemoveEntry.ToolTipText = "Remove Account (Ctrl-R)";
            this.BtnRemoveEntry.Click += new System.EventHandler(this.BtnRemoveEntry_Click);
            // 
            // BtnEditEntry
            // 
            this.BtnEditEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnEditEntry.Image = ((System.Drawing.Image)(resources.GetObject("BtnEditEntry.Image")));
            this.BtnEditEntry.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnEditEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnEditEntry.Name = "BtnEditEntry";
            this.BtnEditEntry.Size = new System.Drawing.Size(36, 44);
            this.BtnEditEntry.Text = "Edit Account";
            this.BtnEditEntry.ToolTipText = "Edit Account (Ctrl-E)";
            this.BtnEditEntry.Click += new System.EventHandler(this.BtnEditEntry_Click);
            // 
            // BtnSaveChanges
            // 
            this.BtnSaveChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnSaveChanges.Image = ((System.Drawing.Image)(resources.GetObject("BtnSaveChanges.Image")));
            this.BtnSaveChanges.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnSaveChanges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSaveChanges.Name = "BtnSaveChanges";
            this.BtnSaveChanges.Size = new System.Drawing.Size(36, 44);
            this.BtnSaveChanges.Text = "Save Changes";
            this.BtnSaveChanges.ToolTipText = "Save Changes (Ctrl-S)";
            this.BtnSaveChanges.Click += new System.EventHandler(this.BtnSaveChanges_Click);
            // 
            // BtnMagnify
            // 
            this.BtnMagnify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnMagnify.Image = ((System.Drawing.Image)(resources.GetObject("BtnMagnify.Image")));
            this.BtnMagnify.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnMagnify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnMagnify.Name = "BtnMagnify";
            this.BtnMagnify.Size = new System.Drawing.Size(36, 44);
            this.BtnMagnify.Text = "toolStripButton1";
            this.BtnMagnify.ToolTipText = "Magnify (Ctrl-M)";
            this.BtnMagnify.Click += new System.EventHandler(this.BtnMagnify_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(44, 44);
            this.btnSettings.Text = "toolStripButton1";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(-57, 96);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(1);
            this.dataGridView.MaximumSize = new System.Drawing.Size(1249, 671);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 50;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView.RowTemplate.Height = 30;
            this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(951, 389);
            this.dataGridView.TabIndex = 2;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.DataGridView_SelectionChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(45, 50);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(1);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(183, 22);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtFilter_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter:";
            // 
            // Wallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(893, 497);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Wallet";
            this.Text = "Password Wallet";
            this.Load += new System.EventHandler(this.Wallet_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.passwordDataSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource passwordDataSource;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BtnAddEntry;
        private System.Windows.Forms.ToolStripButton BtnRemoveEntry;
        private System.Windows.Forms.ToolStripButton BtnEditEntry;
        private System.Windows.Forms.ToolStripButton BtnSaveChanges;
        private System.Windows.Forms.ToolStripButton BtnMagnify;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btnSettings;
    }
}


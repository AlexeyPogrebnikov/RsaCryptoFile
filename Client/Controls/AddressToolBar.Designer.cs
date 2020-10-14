namespace CryptoFile.Client.Controls {
	partial class AddressToolBar {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressToolBar));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.addressToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.addressToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addressToolStripLabel,
            this.addressToolStripButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(381, 25);
			this.toolStrip.TabIndex = 0;
			// 
			// addressToolStripLabel
			// 
			this.addressToolStripLabel.Name = "addressToolStripLabel";
			this.addressToolStripLabel.Size = new System.Drawing.Size(50, 22);
			this.addressToolStripLabel.Text = "Address:";
			// 
			// addressToolStripButton
			// 
			this.addressToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.addressToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("addressToolStripButton.Image")));
			this.addressToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addressToolStripButton.Name = "addressToolStripButton";
			this.addressToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.addressToolStripButton.Text = "...";
			this.addressToolStripButton.ToolTipText = "Browse";
			this.addressToolStripButton.Click += new System.EventHandler(this.addressToolStripButton_Click);
			// 
			// AddressToolBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStrip);
			this.MaximumSize = new System.Drawing.Size(10000, 25);
			this.MinimumSize = new System.Drawing.Size(100, 25);
			this.Name = "AddressToolBar";
			this.Size = new System.Drawing.Size(381, 25);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripLabel addressToolStripLabel;
		private System.Windows.Forms.ToolStripButton addressToolStripButton;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
	}
}

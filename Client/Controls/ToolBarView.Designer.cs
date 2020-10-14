namespace CryptoFile.Client.Controls {
	partial class ToolBarView {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolBarView));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toUpperDirectoryToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cipherToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.decipherToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.generateKeysToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toUpperDirectoryToolStripButton,
            this.refreshToolStripButton,
            this.toolStripSeparator1,
            this.cipherToolStripButton,
            this.decipherToolStripButton,
            this.generateKeysToolStripButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(267, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip1";
			// 
			// toUpperDirectoryToolStripButton
			// 
			this.toUpperDirectoryToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toUpperDirectoryToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("toUpperDirectoryToolStripButton.Image")));
			this.toUpperDirectoryToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toUpperDirectoryToolStripButton.Name = "toUpperDirectoryToolStripButton";
			this.toUpperDirectoryToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.toUpperDirectoryToolStripButton.Text = "toolStripButton1";
			this.toUpperDirectoryToolStripButton.ToolTipText = "Up";
			this.toUpperDirectoryToolStripButton.Click += new System.EventHandler(this.toUpperDirectoryToolStripButton_Click);
			// 
			// refreshToolStripButton
			// 
			this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.refreshToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripButton.Image")));
			this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshToolStripButton.Name = "refreshToolStripButton";
			this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.refreshToolStripButton.Text = "toolStripButton2";
			this.refreshToolStripButton.ToolTipText = "Refresh";
			this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// cipherToolStripButton
			// 
			this.cipherToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cipherToolStripButton.Enabled = false;
			this.cipherToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cipherToolStripButton.Image")));
			this.cipherToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cipherToolStripButton.Name = "cipherToolStripButton";
			this.cipherToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.cipherToolStripButton.Text = "toolStripButton3";
			this.cipherToolStripButton.ToolTipText = "Cipher";
			this.cipherToolStripButton.Click += new System.EventHandler(this.cipherToolStripButton_Click);
			// 
			// decipherToolStripButton
			// 
			this.decipherToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.decipherToolStripButton.Enabled = false;
			this.decipherToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("decipherToolStripButton.Image")));
			this.decipherToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.decipherToolStripButton.Name = "decipherToolStripButton";
			this.decipherToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.decipherToolStripButton.Text = "toolStripButton4";
			this.decipherToolStripButton.ToolTipText = "Decipher";
			this.decipherToolStripButton.Click += new System.EventHandler(this.decipherToolStripButton_Click);
			// 
			// generateKeysToolStripButton
			// 
			this.generateKeysToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.generateKeysToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("generateKeysToolStripButton.Image")));
			this.generateKeysToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.generateKeysToolStripButton.Name = "generateKeysToolStripButton";
			this.generateKeysToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.generateKeysToolStripButton.Text = "toolStripButton5";
			this.generateKeysToolStripButton.ToolTipText = "Generate keys";
			this.generateKeysToolStripButton.Click += new System.EventHandler(this.generateKeysToolStripButton_Click);
			// 
			// ToolBarView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStrip);
			this.MaximumSize = new System.Drawing.Size(10000, 25);
			this.MinimumSize = new System.Drawing.Size(100, 25);
			this.Name = "ToolBarView";
			this.Size = new System.Drawing.Size(267, 25);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton toUpperDirectoryToolStripButton;
		private System.Windows.Forms.ToolStripButton refreshToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton cipherToolStripButton;
		private System.Windows.Forms.ToolStripButton decipherToolStripButton;
		private System.Windows.Forms.ToolStripButton generateKeysToolStripButton;
	}
}
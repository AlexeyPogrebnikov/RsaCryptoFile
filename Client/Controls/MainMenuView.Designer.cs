namespace CryptoFile.Client.Controls
{
	partial class MainMenuView {
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
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cipherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.decipherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.serviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generateKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.serviceToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(288, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cipherToolStripMenuItem,
            this.decipherToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// cipherToolStripMenuItem
			// 
			this.cipherToolStripMenuItem.Enabled = false;
			this.cipherToolStripMenuItem.Image = global::CryptoFile.Client.Properties.Resources.cipher;
			this.cipherToolStripMenuItem.Name = "cipherToolStripMenuItem";
			this.cipherToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.cipherToolStripMenuItem.Text = "Cipher";
			this.cipherToolStripMenuItem.Click += new System.EventHandler(this.cipherToolStripMenuItem_Click);
			// 
			// decipherToolStripMenuItem
			// 
			this.decipherToolStripMenuItem.Enabled = false;
			this.decipherToolStripMenuItem.Image = global::CryptoFile.Client.Properties.Resources.decipher;
			this.decipherToolStripMenuItem.Name = "decipherToolStripMenuItem";
			this.decipherToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.decipherToolStripMenuItem.Text = "Decipher";
			this.decipherToolStripMenuItem.Click += new System.EventHandler(this.decipherToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// serviceToolStripMenuItem
			// 
			this.serviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.generateKeysToolStripMenuItem});
			this.serviceToolStripMenuItem.Name = "serviceToolStripMenuItem";
			this.serviceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.serviceToolStripMenuItem.Text = "Service";
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.propertiesToolStripMenuItem.Text = "Properties";
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
			// 
			// languageToolStripMenuItem
			// 
			this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.russianToolStripMenuItem});
			this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
			this.languageToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.languageToolStripMenuItem.Text = "Language";
			// 
			// englishToolStripMenuItem
			// 
			this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
			this.englishToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.englishToolStripMenuItem.Text = "English";
			this.englishToolStripMenuItem.CheckedChanged += new System.EventHandler(this.englishToolStripMenuItem_CheckedChanged);
			this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
			// 
			// russianToolStripMenuItem
			// 
			this.russianToolStripMenuItem.Name = "russianToolStripMenuItem";
			this.russianToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.russianToolStripMenuItem.Text = "Русский";
			this.russianToolStripMenuItem.CheckedChanged += new System.EventHandler(this.russianToolStripMenuItem_CheckedChanged);
			this.russianToolStripMenuItem.Click += new System.EventHandler(this.russianToolStripMenuItem_Click);
			// 
			// generateKeysToolStripMenuItem
			// 
			this.generateKeysToolStripMenuItem.Name = "generateKeysToolStripMenuItem";
			this.generateKeysToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.generateKeysToolStripMenuItem.Text = "Generate keys";
			this.generateKeysToolStripMenuItem.Click += new System.EventHandler(this.generateKeysToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutProgramToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutProgramToolStripMenuItem
			// 
			this.aboutProgramToolStripMenuItem.Name = "aboutProgramToolStripMenuItem";
			this.aboutProgramToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.aboutProgramToolStripMenuItem.Text = "About program";
			this.aboutProgramToolStripMenuItem.Click += new System.EventHandler(this.aboutProgramToolStripMenuItem_Click);
			// 
			// MainMenuView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.menuStrip);
			this.MaximumSize = new System.Drawing.Size(10000, 24);
			this.MinimumSize = new System.Drawing.Size(100, 24);
			this.Name = "MainMenuView";
			this.Size = new System.Drawing.Size(288, 24);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cipherToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem decipherToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutProgramToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem serviceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem russianToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem generateKeysToolStripMenuItem;
	}
}
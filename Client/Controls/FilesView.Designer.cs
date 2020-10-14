namespace CryptoFile.Client.Controls {
	partial class FilesView {
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
			this.components = new System.ComponentModel.Container();
			this.listView = new System.Windows.Forms.ListView();
			this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.lengthColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.typeColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.modifiedColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cipherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.decipherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView
			// 
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.lengthColumnHeader,
            this.typeColumnHeader,
            this.modifiedColumnHeader});
			this.listView.ContextMenuStrip = this.contextMenuStrip;
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.FullRowSelect = true;
			this.listView.GridLines = true;
			this.listView.Location = new System.Drawing.Point(0, 0);
			this.listView.Name = "listView";
			this.listView.OwnerDraw = true;
			this.listView.Size = new System.Drawing.Size(466, 150);
			this.listView.SmallImageList = this.imageList;
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
			this.listView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(listView_DrawItem);
			this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
			this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
			this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
			this.listView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_KeyDown);
			this.listView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(listView_DrawSubItem);
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 175;
			// 
			// lengthColumnHeader
			// 
			this.lengthColumnHeader.Text = "Length";
			this.lengthColumnHeader.Width = 114;
			// 
			// typeColumnHeader
			// 
			this.typeColumnHeader.Text = "Type";
			this.typeColumnHeader.Width = 88;
			// 
			// modifiedColumnHeader
			// 
			this.modifiedColumnHeader.Text = "Modified";
			this.modifiedColumnHeader.Width = 88;
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.cipherToolStripMenuItem,
            this.decipherToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(128, 70);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// cipherToolStripMenuItem
			// 
			this.cipherToolStripMenuItem.Image = global::CryptoFile.Client.Properties.Resources.cipher;
			this.cipherToolStripMenuItem.Name = "cipherToolStripMenuItem";
			this.cipherToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.cipherToolStripMenuItem.Text = "Cipher";
			this.cipherToolStripMenuItem.Click += new System.EventHandler(this.cipherToolStripMenuItem_Click);
			// 
			// decipherToolStripMenuItem
			// 
			this.decipherToolStripMenuItem.Image = global::CryptoFile.Client.Properties.Resources.decipher;
			this.decipherToolStripMenuItem.Name = "decipherToolStripMenuItem";
			this.decipherToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.decipherToolStripMenuItem.Text = "Decipher";
			this.decipherToolStripMenuItem.Click += new System.EventHandler(this.decipherToolStripMenuItem_Click);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// FilesView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listView);
			this.Name = "FilesView";
			this.Size = new System.Drawing.Size(466, 150);
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private System.Windows.Forms.ColumnHeader lengthColumnHeader;
		private System.Windows.Forms.ColumnHeader modifiedColumnHeader;
		private System.Windows.Forms.ColumnHeader typeColumnHeader;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cipherToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem decipherToolStripMenuItem;
	}
}

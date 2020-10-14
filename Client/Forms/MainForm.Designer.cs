using CryptoFile.Client.Controls;

namespace CryptoFile.Client.Forms {
	partial class MainForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.filesView = new CryptoFile.Client.Controls.FilesView();
			this.mainMenuView = new CryptoFile.Client.Controls.MainMenuView();
			this.toolBarView = new CryptoFile.Client.Controls.ToolBarView();
			this.addressToolBar = new CryptoFile.Client.Controls.AddressToolBar();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.filesView, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.mainMenuView, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.toolBarView, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.addressToolBar, 0, 2);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 4;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(469, 316);
			this.tableLayoutPanel.TabIndex = 1;
			// 
			// filesView
			// 
			this.filesView.CipherEnabled = true;
			this.filesView.DecipherEnabled = true;
			this.filesView.DirectoryImage = null;
			this.filesView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.filesView.IsTerminal = false;
			this.filesView.Location = new System.Drawing.Point(0, 74);
			this.filesView.Margin = new System.Windows.Forms.Padding(0);
			this.filesView.Name = "filesView";
			this.filesView.OpenDirectoryEnabled = true;
			this.filesView.RsaFileColor = System.Drawing.Color.Empty;
			this.filesView.Size = new System.Drawing.Size(469, 242);
			this.filesView.SortingInfo = null;
			this.filesView.TabIndex = 3;
			// 
			// mainMenuView
			// 
			this.mainMenuView.CipherEnabled = false;
			this.mainMenuView.DecipherEnabled = false;
			this.mainMenuView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainMenuView.Language = CryptoFile.Client.Configuration.Language.English;
			this.mainMenuView.Location = new System.Drawing.Point(0, 0);
			this.mainMenuView.Margin = new System.Windows.Forms.Padding(0);
			this.mainMenuView.MaximumSize = new System.Drawing.Size(10000, 24);
			this.mainMenuView.MinimumSize = new System.Drawing.Size(100, 24);
			this.mainMenuView.Name = "mainMenuView";
			this.mainMenuView.Size = new System.Drawing.Size(469, 24);
			this.mainMenuView.TabIndex = 0;
			// 
			// toolBarView
			// 
			this.toolBarView.CipherEnabled = false;
			this.toolBarView.DecipherEnabled = false;
			this.toolBarView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolBarView.Location = new System.Drawing.Point(0, 24);
			this.toolBarView.Margin = new System.Windows.Forms.Padding(0);
			this.toolBarView.MaximumSize = new System.Drawing.Size(10000, 25);
			this.toolBarView.MinimumSize = new System.Drawing.Size(100, 25);
			this.toolBarView.Name = "toolBarView";
			this.toolBarView.Size = new System.Drawing.Size(469, 25);
			this.toolBarView.TabIndex = 1;
			this.toolBarView.ToUpperDirectoryEnabled = true;
			// 
			// addressToolBar
			// 
			this.addressToolBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.addressToolBar.Location = new System.Drawing.Point(0, 49);
			this.addressToolBar.Margin = new System.Windows.Forms.Padding(0);
			this.addressToolBar.MaximumSize = new System.Drawing.Size(10000, 25);
			this.addressToolBar.MinimumSize = new System.Drawing.Size(100, 25);
			this.addressToolBar.Name = "addressToolBar";
			this.addressToolBar.Path = "";
			this.addressToolBar.Size = new System.Drawing.Size(469, 25);
			this.addressToolBar.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(469, 316);
			this.Controls.Add(this.tableLayoutPanel);
			this.MinimumSize = new System.Drawing.Size(477, 350);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RSA CryptoFile";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.tableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.MainMenuView mainMenuView;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private ToolBarView toolBarView;
		private FilesView filesView;
		private AddressToolBar addressToolBar;
	}
}


namespace CryptoFile.Client.Forms {
	partial class CipherForm {
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
			this.components = new System.ComponentModel.Container();
			this.publicKeyTextBox = new System.Windows.Forms.TextBox();
			this.publicKeyLabel = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.cipherButton = new System.Windows.Forms.Button();
			this.outputFileTextBox = new System.Windows.Forms.TextBox();
			this.outputFileLabel = new System.Windows.Forms.Label();
			this.outputFileButton = new System.Windows.Forms.Button();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.progressLabel = new System.Windows.Forms.Label();
			this.closeWindowCheckBox = new System.Windows.Forms.CheckBox();
			this.selectedFileEntitiesLabel = new System.Windows.Forms.Label();
			this.selectedFileEntitiesListView = new System.Windows.Forms.ListView();
			this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.lengthColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.totalLengthLabel = new System.Windows.Forms.Label();
			this.zipСompressionСheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// publicKeyTextBox
			// 
			this.publicKeyTextBox.Location = new System.Drawing.Point(12, 224);
			this.publicKeyTextBox.Name = "publicKeyTextBox";
			this.publicKeyTextBox.Size = new System.Drawing.Size(367, 20);
			this.publicKeyTextBox.TabIndex = 8;
			this.publicKeyTextBox.TextChanged += new System.EventHandler(this.publicKeyTextBox_TextChanged);
			// 
			// publicKeyLabel
			// 
			this.publicKeyLabel.AutoSize = true;
			this.publicKeyLabel.Location = new System.Drawing.Point(12, 208);
			this.publicKeyLabel.Name = "publicKeyLabel";
			this.publicKeyLabel.Size = new System.Drawing.Size(59, 13);
			this.publicKeyLabel.TabIndex = 7;
			this.publicKeyLabel.Text = "Public key:";
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(304, 347);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 9;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// cipherButton
			// 
			this.cipherButton.Enabled = false;
			this.cipherButton.Location = new System.Drawing.Point(214, 347);
			this.cipherButton.Name = "cipherButton";
			this.cipherButton.Size = new System.Drawing.Size(84, 23);
			this.cipherButton.TabIndex = 10;
			this.cipherButton.Text = "Cipher";
			this.cipherButton.UseVisualStyleBackColor = true;
			this.cipherButton.Click += new System.EventHandler(this.cipherButton_Click);
			// 
			// outputFileTextBox
			// 
			this.outputFileTextBox.Location = new System.Drawing.Point(12, 176);
			this.outputFileTextBox.Name = "outputFileTextBox";
			this.outputFileTextBox.Size = new System.Drawing.Size(336, 20);
			this.outputFileTextBox.TabIndex = 13;
			this.outputFileTextBox.TextChanged += new System.EventHandler(this.outputFileTextBox_TextChanged);
			// 
			// outputFileLabel
			// 
			this.outputFileLabel.AutoSize = true;
			this.outputFileLabel.Location = new System.Drawing.Point(12, 160);
			this.outputFileLabel.Name = "outputFileLabel";
			this.outputFileLabel.Size = new System.Drawing.Size(58, 13);
			this.outputFileLabel.TabIndex = 14;
			this.outputFileLabel.Text = "Output file:";
			// 
			// outputFileButton
			// 
			this.outputFileButton.Location = new System.Drawing.Point(354, 175);
			this.outputFileButton.Name = "outputFileButton";
			this.outputFileButton.Size = new System.Drawing.Size(25, 23);
			this.outputFileButton.TabIndex = 16;
			this.outputFileButton.Text = "...";
			this.outputFileButton.UseVisualStyleBackColor = true;
			this.outputFileButton.Click += new System.EventHandler(this.outputFileButton_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "RSA files|*.rsa";
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 272);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(367, 23);
			this.progressBar.TabIndex = 17;
			// 
			// progressLabel
			// 
			this.progressLabel.AutoSize = true;
			this.progressLabel.Location = new System.Drawing.Point(12, 256);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(51, 13);
			this.progressLabel.TabIndex = 18;
			this.progressLabel.Text = "Progress:";
			// 
			// closeWindowCheckBox
			// 
			this.closeWindowCheckBox.AutoSize = true;
			this.closeWindowCheckBox.Checked = true;
			this.closeWindowCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.closeWindowCheckBox.Location = new System.Drawing.Point(15, 301);
			this.closeWindowCheckBox.Name = "closeWindowCheckBox";
			this.closeWindowCheckBox.Size = new System.Drawing.Size(161, 17);
			this.closeWindowCheckBox.TabIndex = 19;
			this.closeWindowCheckBox.Text = "Close window after complete";
			this.closeWindowCheckBox.UseVisualStyleBackColor = true;
			// 
			// selectedFileEntitiesLabel
			// 
			this.selectedFileEntitiesLabel.AutoSize = true;
			this.selectedFileEntitiesLabel.Location = new System.Drawing.Point(12, 9);
			this.selectedFileEntitiesLabel.Name = "selectedFileEntitiesLabel";
			this.selectedFileEntitiesLabel.Size = new System.Drawing.Size(128, 13);
			this.selectedFileEntitiesLabel.TabIndex = 20;
			this.selectedFileEntitiesLabel.Text = "Selected files and folders:";
			// 
			// selectedFileEntitiesListView
			// 
			this.selectedFileEntitiesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.lengthColumnHeader});
			this.selectedFileEntitiesListView.FullRowSelect = true;
			this.selectedFileEntitiesListView.GridLines = true;
			this.selectedFileEntitiesListView.Location = new System.Drawing.Point(12, 25);
			this.selectedFileEntitiesListView.Name = "selectedFileEntitiesListView";
			this.selectedFileEntitiesListView.Size = new System.Drawing.Size(367, 97);
			this.selectedFileEntitiesListView.SmallImageList = this.imageList;
			this.selectedFileEntitiesListView.TabIndex = 21;
			this.selectedFileEntitiesListView.UseCompatibleStateImageBehavior = false;
			this.selectedFileEntitiesListView.View = System.Windows.Forms.View.Details;
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 276;
			// 
			// lengthColumnHeader
			// 
			this.lengthColumnHeader.Text = "Length";
			this.lengthColumnHeader.Width = 72;
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// totalLengthLabel
			// 
			this.totalLengthLabel.AutoSize = true;
			this.totalLengthLabel.Location = new System.Drawing.Point(12, 134);
			this.totalLengthLabel.Name = "totalLengthLabel";
			this.totalLengthLabel.Size = new System.Drawing.Size(66, 13);
			this.totalLengthLabel.TabIndex = 22;
			this.totalLengthLabel.Text = "Total length:";
			// 
			// zipСompressionСheckBox
			// 
			this.zipСompressionСheckBox.AutoSize = true;
			this.zipСompressionСheckBox.Location = new System.Drawing.Point(15, 324);
			this.zipСompressionСheckBox.Name = "zipСompressionСheckBox";
			this.zipСompressionСheckBox.Size = new System.Drawing.Size(103, 17);
			this.zipСompressionСheckBox.TabIndex = 23;
			this.zipСompressionСheckBox.Text = "Zip compression";
			this.zipСompressionСheckBox.UseVisualStyleBackColor = true;
			// 
			// CipherForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(393, 384);
			this.Controls.Add(this.zipСompressionСheckBox);
			this.Controls.Add(this.totalLengthLabel);
			this.Controls.Add(this.selectedFileEntitiesListView);
			this.Controls.Add(this.selectedFileEntitiesLabel);
			this.Controls.Add(this.closeWindowCheckBox);
			this.Controls.Add(this.progressLabel);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.outputFileButton);
			this.Controls.Add(this.outputFileLabel);
			this.Controls.Add(this.outputFileTextBox);
			this.Controls.Add(this.cipherButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.publicKeyTextBox);
			this.Controls.Add(this.publicKeyLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "CipherForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Encryption";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox publicKeyTextBox;
		private System.Windows.Forms.Label publicKeyLabel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button cipherButton;
		private System.Windows.Forms.TextBox outputFileTextBox;
		private System.Windows.Forms.Label outputFileLabel;
		private System.Windows.Forms.Button outputFileButton;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label progressLabel;
		private System.Windows.Forms.CheckBox closeWindowCheckBox;
		private System.Windows.Forms.Label selectedFileEntitiesLabel;
		private System.Windows.Forms.ListView selectedFileEntitiesListView;
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private System.Windows.Forms.ColumnHeader lengthColumnHeader;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Label totalLengthLabel;
		private System.Windows.Forms.CheckBox zipСompressionСheckBox;
	}
}
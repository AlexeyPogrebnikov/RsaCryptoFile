namespace CryptoFile.Client.Forms {
	partial class DecipherForm {
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
			this.cancelButton = new System.Windows.Forms.Button();
			this.decryptButton = new System.Windows.Forms.Button();
			this.inputFileLabel = new System.Windows.Forms.Label();
			this.inputFileTextBox = new System.Windows.Forms.TextBox();
			this.outputDirectoryPathTextBox = new System.Windows.Forms.TextBox();
			this.privateKeyTextBox = new System.Windows.Forms.TextBox();
			this.outputDirectoryPathButton = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.outputDirectoryPathLabel = new System.Windows.Forms.Label();
			this.privateKeyLabel = new System.Windows.Forms.Label();
			this.progressLabel = new System.Windows.Forms.Label();
			this.closeWindowCheckBox = new System.Windows.Forms.CheckBox();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(312, 223);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 0;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// decryptButton
			// 
			this.decryptButton.Enabled = false;
			this.decryptButton.Location = new System.Drawing.Point(200, 223);
			this.decryptButton.Name = "decryptButton";
			this.decryptButton.Size = new System.Drawing.Size(106, 23);
			this.decryptButton.TabIndex = 1;
			this.decryptButton.Text = "Decrypt";
			this.decryptButton.UseVisualStyleBackColor = true;
			this.decryptButton.Click += new System.EventHandler(this.decipherButton_Click);
			// 
			// inputFileLabel
			// 
			this.inputFileLabel.AutoSize = true;
			this.inputFileLabel.Location = new System.Drawing.Point(12, 9);
			this.inputFileLabel.Name = "inputFileLabel";
			this.inputFileLabel.Size = new System.Drawing.Size(50, 13);
			this.inputFileLabel.TabIndex = 2;
			this.inputFileLabel.Text = "Input file:";
			// 
			// inputFileTextBox
			// 
			this.inputFileTextBox.Location = new System.Drawing.Point(12, 25);
			this.inputFileTextBox.Name = "inputFileTextBox";
			this.inputFileTextBox.ReadOnly = true;
			this.inputFileTextBox.Size = new System.Drawing.Size(375, 20);
			this.inputFileTextBox.TabIndex = 3;
			// 
			// outputDirectoryPathTextBox
			// 
			this.outputDirectoryPathTextBox.Location = new System.Drawing.Point(12, 73);
			this.outputDirectoryPathTextBox.Name = "outputDirectoryPathTextBox";
			this.outputDirectoryPathTextBox.Size = new System.Drawing.Size(344, 20);
			this.outputDirectoryPathTextBox.TabIndex = 4;
			this.outputDirectoryPathTextBox.TextChanged += new System.EventHandler(this.outputFileTextBox_TextChanged);
			// 
			// privateKeyTextBox
			// 
			this.privateKeyTextBox.Location = new System.Drawing.Point(12, 122);
			this.privateKeyTextBox.Name = "privateKeyTextBox";
			this.privateKeyTextBox.Size = new System.Drawing.Size(375, 20);
			this.privateKeyTextBox.TabIndex = 5;
			this.privateKeyTextBox.TextChanged += new System.EventHandler(this.privateKeyTextBox_TextChanged);
			// 
			// outputDirectoryPathButton
			// 
			this.outputDirectoryPathButton.Location = new System.Drawing.Point(362, 71);
			this.outputDirectoryPathButton.Name = "outputDirectoryPathButton";
			this.outputDirectoryPathButton.Size = new System.Drawing.Size(25, 23);
			this.outputDirectoryPathButton.TabIndex = 7;
			this.outputDirectoryPathButton.Text = "...";
			this.outputDirectoryPathButton.UseVisualStyleBackColor = true;
			this.outputDirectoryPathButton.Click += new System.EventHandler(this.outputFileButton_Click);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 171);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(375, 23);
			this.progressBar.TabIndex = 8;
			// 
			// outputDirectoryPathLabel
			// 
			this.outputDirectoryPathLabel.AutoSize = true;
			this.outputDirectoryPathLabel.Location = new System.Drawing.Point(12, 57);
			this.outputDirectoryPathLabel.Name = "outputDirectoryPathLabel";
			this.outputDirectoryPathLabel.Size = new System.Drawing.Size(66, 13);
			this.outputDirectoryPathLabel.TabIndex = 9;
			this.outputDirectoryPathLabel.Text = "Output path:";
			// 
			// privateKeyLabel
			// 
			this.privateKeyLabel.AutoSize = true;
			this.privateKeyLabel.Location = new System.Drawing.Point(12, 106);
			this.privateKeyLabel.Name = "privateKeyLabel";
			this.privateKeyLabel.Size = new System.Drawing.Size(63, 13);
			this.privateKeyLabel.TabIndex = 10;
			this.privateKeyLabel.Text = "Private key:";
			// 
			// progressLabel
			// 
			this.progressLabel.AutoSize = true;
			this.progressLabel.Location = new System.Drawing.Point(12, 155);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(51, 13);
			this.progressLabel.TabIndex = 11;
			this.progressLabel.Text = "Progress:";
			// 
			// closeWindowCheckBox
			// 
			this.closeWindowCheckBox.AutoSize = true;
			this.closeWindowCheckBox.Checked = true;
			this.closeWindowCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.closeWindowCheckBox.Location = new System.Drawing.Point(12, 200);
			this.closeWindowCheckBox.Name = "closeWindowCheckBox";
			this.closeWindowCheckBox.Size = new System.Drawing.Size(161, 17);
			this.closeWindowCheckBox.TabIndex = 20;
			this.closeWindowCheckBox.Text = "Close window after complete";
			this.closeWindowCheckBox.UseVisualStyleBackColor = true;
			// 
			// DecipherForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(399, 261);
			this.Controls.Add(this.closeWindowCheckBox);
			this.Controls.Add(this.progressLabel);
			this.Controls.Add(this.privateKeyLabel);
			this.Controls.Add(this.outputDirectoryPathLabel);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.outputDirectoryPathButton);
			this.Controls.Add(this.privateKeyTextBox);
			this.Controls.Add(this.outputDirectoryPathTextBox);
			this.Controls.Add(this.inputFileTextBox);
			this.Controls.Add(this.inputFileLabel);
			this.Controls.Add(this.decryptButton);
			this.Controls.Add(this.cancelButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "DecipherForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Decipher";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button decryptButton;
		private System.Windows.Forms.Label inputFileLabel;
		private System.Windows.Forms.TextBox inputFileTextBox;
		private System.Windows.Forms.TextBox outputDirectoryPathTextBox;
		private System.Windows.Forms.TextBox privateKeyTextBox;
		private System.Windows.Forms.Button outputDirectoryPathButton;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label outputDirectoryPathLabel;
		private System.Windows.Forms.Label privateKeyLabel;
		private System.Windows.Forms.Label progressLabel;
		private System.Windows.Forms.CheckBox closeWindowCheckBox;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
	}
}
namespace CryptoFile.Client.Forms
{
	partial class PropertiesForm
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
			this.rsaFileColorLabel = new System.Windows.Forms.Label();
			this.rsaFileColorPanel = new System.Windows.Forms.Panel();
			this.rsaFileColorButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.SuspendLayout();
			// 
			// rsaFileColorLabel
			// 
			this.rsaFileColorLabel.AutoSize = true;
			this.rsaFileColorLabel.Location = new System.Drawing.Point(12, 9);
			this.rsaFileColorLabel.Name = "rsaFileColorLabel";
			this.rsaFileColorLabel.Size = new System.Drawing.Size(74, 13);
			this.rsaFileColorLabel.TabIndex = 0;
			this.rsaFileColorLabel.Text = "RSA file color:";
			// 
			// rsaFileColorPanel
			// 
			this.rsaFileColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rsaFileColorPanel.Location = new System.Drawing.Point(12, 25);
			this.rsaFileColorPanel.Name = "rsaFileColorPanel";
			this.rsaFileColorPanel.Size = new System.Drawing.Size(200, 23);
			this.rsaFileColorPanel.TabIndex = 1;
			// 
			// rsaFileColorButton
			// 
			this.rsaFileColorButton.Location = new System.Drawing.Point(218, 25);
			this.rsaFileColorButton.Name = "rsaFileColorButton";
			this.rsaFileColorButton.Size = new System.Drawing.Size(30, 23);
			this.rsaFileColorButton.TabIndex = 2;
			this.rsaFileColorButton.Text = "...";
			this.rsaFileColorButton.UseVisualStyleBackColor = true;
			this.rsaFileColorButton.Click += new System.EventHandler(this.rsaFileColorButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(173, 54);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(92, 54);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// PropertiesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(261, 88);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.rsaFileColorButton);
			this.Controls.Add(this.rsaFileColorPanel);
			this.Controls.Add(this.rsaFileColorLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "PropertiesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Properties";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label rsaFileColorLabel;
		private System.Windows.Forms.Panel rsaFileColorPanel;
		private System.Windows.Forms.Button rsaFileColorButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.ColorDialog colorDialog;
	}
}
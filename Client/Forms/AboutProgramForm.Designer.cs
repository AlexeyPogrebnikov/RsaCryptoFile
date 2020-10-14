namespace CryptoFile.Client.Forms
{
	partial class AboutProgramForm
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
			this.authorLabel = new System.Windows.Forms.Label();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.emailLinkLabel = new System.Windows.Forms.LinkLabel();
			this.versionLabel = new System.Windows.Forms.Label();
			this.copyrightLabel = new System.Windows.Forms.Label();
			this.emailLabel = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// authorLabel
			// 
			this.authorLabel.AutoSize = true;
			this.authorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.authorLabel.Location = new System.Drawing.Point(12, 248);
			this.authorLabel.Name = "authorLabel";
			this.authorLabel.Size = new System.Drawing.Size(199, 16);
			this.authorLabel.TabIndex = 1;
			this.authorLabel.Text = "Author: Alexey Pogrebnikov";
			// 
			// pictureBox
			// 
			this.pictureBox.Image = global::CryptoFile.Client.Properties.Resources.photo;
			this.pictureBox.Location = new System.Drawing.Point(12, 12);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(300, 200);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// emailLinkLabel
			// 
			this.emailLinkLabel.AutoSize = true;
			this.emailLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.emailLinkLabel.Location = new System.Drawing.Point(64, 270);
			this.emailLinkLabel.Name = "emailLinkLabel";
			this.emailLinkLabel.Size = new System.Drawing.Size(138, 16);
			this.emailLinkLabel.TabIndex = 2;
			this.emailLinkLabel.TabStop = true;
			this.emailLinkLabel.Text = "wpsite@gmail.com";
			this.emailLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.emailLinkLabel_LinkClicked);
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.versionLabel.Location = new System.Drawing.Point(12, 226);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(89, 16);
			this.versionLabel.TabIndex = 3;
			this.versionLabel.Text = "Version: 1.2";
			// 
			// copyrightLabel
			// 
			this.copyrightLabel.AutoSize = true;
			this.copyrightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.copyrightLabel.Location = new System.Drawing.Point(12, 296);
			this.copyrightLabel.Name = "copyrightLabel";
			this.copyrightLabel.Size = new System.Drawing.Size(168, 16);
			this.copyrightLabel.TabIndex = 4;
			this.copyrightLabel.Text = "Copyright © 2009 - 2011";
			// 
			// emailLabel
			// 
			this.emailLabel.AutoSize = true;
			this.emailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.emailLabel.Location = new System.Drawing.Point(12, 270);
			this.emailLabel.Name = "emailLabel";
			this.emailLabel.Size = new System.Drawing.Size(55, 16);
			this.emailLabel.TabIndex = 5;
			this.emailLabel.Text = "e-mail:";
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new System.Drawing.Point(237, 321);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// AboutProgramForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(324, 356);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.emailLabel);
			this.Controls.Add(this.copyrightLabel);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.emailLinkLabel);
			this.Controls.Add(this.authorLabel);
			this.Controls.Add(this.pictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "AboutProgramForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Label authorLabel;
		private System.Windows.Forms.LinkLabel emailLinkLabel;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.Label copyrightLabel;
		private System.Windows.Forms.Label emailLabel;
		private System.Windows.Forms.Button okButton;
	}
}